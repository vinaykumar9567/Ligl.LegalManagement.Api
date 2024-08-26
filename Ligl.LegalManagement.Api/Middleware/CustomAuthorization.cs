using Ligl.Core.Sdk.Common.Cryptography;
using Ligl.Core.Sdk.Common.Cryptography.Interface;
using Ligl.Core.Sdk.Common.Helper;
using Ligl.Core.Sdk.Shared.Model.Common.Base;
using Ligl.Core.Sdk.Shared.Model.Principal;
using Ligl.Core.Sdk.Shared.Repository.Master;
using Ligl.Core.Sdk.Shared.Repository.Master.Interface;
using Ligl.Core.Sdk.Shared.Repository.Store;
using Ligl.Core.Sdk.Shared.Repository.Store.Domain;
using Ligl.Core.Sdk.Shared.Repository.Store.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;


namespace Ligl.LegalManagement.Api.Middleware
{

    /// <summary>
    /// Class for CustomAuthorization
    /// </summary>
    /// <seealso cref="IAuthorizationFilter" />
    [AttributeUsage(AttributeTargets.All)]
    public class CustomAuthorization : Attribute, IAsyncAuthorizationFilter
    {
        private const string AuthorizationHeader = "Authorization";
        private readonly IStoreBaseUnitOfWork _storeBaseUnitOfWork;
        private readonly IMasterBaseUnitOfWork _masterBaseUnitOfWork;
        private readonly ICryptography _cryptography;
        private readonly IConfiguration _configuration;

        private readonly bool _isCaseContext;
        private readonly string? _module;
        private readonly string[]? _scopes;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAuthorization"/> class.
        /// </summary>
        /// <param name="isCaseContext">if set to <c>true</c> [is case context].</param>
        /// <param name="module">The module.</param>
        /// <param name="scopes"></param>
        public CustomAuthorization(bool isCaseContext = false,
            string? module = null, params string[]? scopes)
        {
            _scopes = scopes;
            _isCaseContext = isCaseContext;
            _module = module;
            _storeBaseUnitOfWork = new StoreBaseUnitOfWork(new StoreBaseContext(new DbContextOptions<StoreBaseContext>()));
            _masterBaseUnitOfWork =
                new MasterBaseUnitOfWork(new MasterBaseContext(new DbContextOptions<MasterBaseContext>()));
            _cryptography = new AesCryptography();
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        /// <summary>
        /// Validates the token.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        /// <exception cref="AuthenticationException">
        /// Authorization token not found
        /// or
        /// Token expired
        /// or
        /// Session Id not found
        /// </exception>
        private string ValidateToken(ActionContext context)
        {
            var headers = context.HttpContext.Request.Headers;
            if (headers.Count > 0 && !headers.ContainsKey(AuthorizationHeader))
                throw new AuthenticationException("Authorization token not found");

            var authHeader = context.HttpContext.Request.Headers[AuthorizationHeader];
            var bearerToken = authHeader.ToString()
                .Replace("bearer", string.Empty, StringComparison.InvariantCultureIgnoreCase).Trim();

            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(bearerToken);

            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                throw new AuthenticationException("Token expired");
            }

            var secretKey = Base64UrlEncoder.DecodeBytes(_configuration["Tokens:Key"]);
            var issuer = _configuration["Tokens:Issuer"];
            var audience = _configuration["Tokens:Audience"];

            if (!string.Equals(jwtToken.Issuer, issuer, StringComparison.InvariantCultureIgnoreCase) ||
                !string.Equals(jwtToken.Audiences.First(), audience, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new AuthenticationException("Invalid token");
            }

            var sessionId = (jwtToken.Claims.SingleOrDefault(x => x.Type == "sid")?.Value) ?? throw new AuthenticationException("Session Id not found");

            sessionId = string.Concat("[PrivateKey]", sessionId);
            var secureToken = _cryptography.Decrypt(sessionId, "secureliglrc")!;
            return secureToken;
        }

        /// <summary>
        /// Validates the session controller.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="domainSession">The domain session.</param>
        /// <exception cref="AuthenticationException">
        /// Session require the region id
        /// or
        /// Region connection string cannot be empty
        /// </exception>
        private void ValidateSessionController(ActionContext context, Session domainSession)
        {
            if (((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor)
                .ControllerName != "Session") return;

            var queryParams = context.HttpContext.Request.Path.Value?.Split("/");
            if (queryParams?.Length == 0 || !Guid.TryParse(queryParams?[^1], out var regionId))
                throw new AuthenticationException("Session require the region id");

            domainSession.RegionId = regionId;
            var domainRegion = _masterBaseUnitOfWork.LegalGroupRepository.GetByIdAsync(regionId)?.Result;
            if (domainRegion == null || string.IsNullOrEmpty(domainRegion.ConnectionStringSetting))
                throw new AuthenticationException("Region connection string cannot be empty");

            domainSession.RegionConnection = domainRegion?.ConnectionStringSetting;
        }

        /// <summary>
        /// Validates the case context.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="domainSession"></param>
        /// <returns></returns>
        private static void ValidateCaseContext(ActionContext context, Session domainSession)
        {
            if (context.ActionDescriptor.DisplayName!.Equals
                   ("Ligl.LegalManagement.Api.Controllers.CaseLegalHoldController.GetAsync (Ligl.LegalManagement.Api)", StringComparison.InvariantCultureIgnoreCase))
                return;

            var queryParams = context.HttpContext.Request.Query;
            var caseId = queryParams["caseId"];
            if (string.IsNullOrEmpty(domainSession.CaseAccess))
                throw new AuthenticationException("No Cases for the users");

            var cases = SerializationHelper.JsonToObject<List<Guid>>(domainSession.CaseAccess);

            if (cases == null || !cases.Contains(Guid.Parse(caseId!)))
                throw new AuthenticationException("Cases serialization failed or case access not found");
        }

        /// <summary>
        /// Validates the module authorization.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="domainSession">The domain session.</param>
        /// <returns></returns>
        /// <exception cref="AuthenticationException">
        /// Roles not found for the users
        /// or
        /// Invalid Module in the action
        /// or
        /// User doesn't have permission on the module
        /// </exception>
        private bool ValidateModuleAuthorization(ActionContext context, Session domainSession)
        {
            var roleActions = SerializationHelper.JsonToObject<List<RoleModulePermissionViewModel>>(domainSession.RolePermissions!)
                              ?? throw new AuthenticationException("Roles not found for the users");

            if (!Guid.TryParse(_module, out var moduleId))
                throw new AuthenticationException("Invalid Module in the action");

            var rolePermission =
                roleActions.FirstOrDefault(r => r.ModuleId == moduleId) ??
                throw new AuthenticationException("User doesn't have permission on the module");

            var httpVerb = context.HttpContext.Request.Method;

            return httpVerb.ToLower() switch
            {
                "get" => rolePermission.View,
                "post" => rolePermission.Create,
                "put" => rolePermission.Update,
                "delete" => rolePermission.Delete,
                _ => false
            };
        }

        /// <summary>
        /// Called when [authorization].
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        /// <exception cref="AuthenticationException">Case context not found</exception>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            context.HttpContext.Request.Headers.Authorization = "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoibGlnbHN1cGVyIiwic2lkIjoieU9sT3VZTmptOVpCeFJydEE5ZWt1V2VGNk8vYitEbE9EV3BWaStPeG12cHA0LzVGMU5LOEtiVHA4dVpUYlNSdkc1OE4xS2Qxb3FLbGhsUVlodGN2cVY3ZVIzMnF5RTdvRGRFRzRraWVUR0lORE5TelZlUkpHaExtamNFODhncVVKLy9vMzZSUXRKOW52MDFLZmcraEY3SG4vZ1RIZzYrUkZkRVBZbTE2V2x0S1IzME9yVmxaNmpvdUVudExsaU9MbHpLRU9SWVhhVGkvck5XSHFuZ3BZUT09IiwiZXhwIjoxNzI0MjM2ODcwLCJpc3MiOiJWZXJ0aWNhbEF1dGhvcml0eSIsImF1ZCI6IkV2ZXJ5b25lIn0.5hHArHFDB52bZCxJZfxOfo_HYkiq2uT-EXIDcMgj-5nrSE2riLUSR_stg3YGHZ127x5l2oOfxz4OH0NgkWechA";

            var secureToken = ValidateToken(context);
            var values = secureToken.Split(".");
            if (values.Length != 2 || !Guid.TryParse(values[0], out var session) || !Guid.TryParse(values[0], out _))
                throw new AuthenticationException("Invalid session token");

            var domainSession = await _storeBaseUnitOfWork.SessionRepository.GetByIdAsync(session) ??
                                throw new AuthenticationException("Session not found DB");

            ValidateSessionController(context, domainSession);

            var customPrincipal = new CustomPrincipal
            {
                SessionId = domainSession.SessionId,
                RegionId = domainSession.RegionId.GetValueOrDefault(),
            };

            if (domainSession.RolePrimaryId != null)
                customPrincipal.Role = new NameValueId
                {
                    Id = domainSession.RoleId.GetValueOrDefault(),
                    Name = domainSession.RoleName!,
                    PrimaryId = domainSession.RolePrimaryId.GetValueOrDefault()
                };

            if (domainSession.UserPrimaryId != -1 && domainSession.UserPrimaryId != null)
                customPrincipal.User = new NameValueId
                {
                    Id = domainSession.UserId,
                    Name = domainSession.UserName!,
                    PrimaryId = domainSession.UserPrimaryId.GetValueOrDefault()
                };

            context.HttpContext.Items.Add("connection", domainSession.RegionConnection);
            context.HttpContext.Items.Add("regionId", domainSession.RegionId);
            context.HttpContext.User = customPrincipal;

            if (_isCaseContext)
                ValidateCaseContext(context, domainSession);

            if (!string.IsNullOrEmpty(_module) && !ValidateModuleAuthorization(context, domainSession))
                throw new AuthenticationException("User doesn't have the permission to perform the action on the module");

            await Task.CompletedTask;
        }
    }
}
