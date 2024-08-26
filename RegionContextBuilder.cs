using Ligl.LegalManagement.Repository.Interface;
using Ligl.Core.Sdk.Shared.Repository.CustomModel;
using Ligl.Core.Sdk.Shared.Repository.Master.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Xml.Serialization;

namespace Ligl.LegalManagement.Repository
{

    public class RegionContextBuilder : IRegionContextBuilder
    {
        private const string ClassName = nameof(RegionContextBuilder);
        private const int MaxTryCount = 3;
        private const int MaxDelayTryInSeconds = 30;

        private readonly IMasterBaseUnitOfWork _masterBaseUnitOfWork;
        private readonly ILogger<RegionContextBuilder> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionContextBuilder"/> class.
        /// </summary>
        /// <param name="httpContextAccessory"></param>
        /// <param name="masterBaseUnitOfWork">The master unit of work.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">The configuration.</param>
        public RegionContextBuilder(ILogger<RegionContextBuilder> logger,
            IConfiguration configuration, IHttpContextAccessor httpContextAccessory, IMasterBaseUnitOfWork masterBaseUnitOfWork)
        {
            _httpContextAccessor = httpContextAccessory;
            _masterBaseUnitOfWork = masterBaseUnitOfWork;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Generates the context.
        /// </summary>
        /// <returns></returns>
        public Task<string> GenerateContextAsync()
        {
            const string methodName = $"{ClassName} - {nameof(RegionContext)}";
            var connectionString = string.Empty;
            try
            {
                _logger.LogInformation(message: "Started execution of {methodName}", methodName);

                if (!_httpContextAccessor.HttpContext.Items.ContainsKey("connection"))
                    throw new NullReferenceException("Current session cannot be null");

                if (_httpContextAccessor.HttpContext.Items["connection"] == null)
                    Task.FromResult(string.Empty);
                else
                    connectionString = _httpContextAccessor.HttpContext.Items["connection"].ToString();

                var serializer = new XmlSerializer(typeof(EntityConfig));
                var reader = new StringReader(connectionString!);

                if (serializer.Deserialize(reader) is not EntityConfig config)
                {
                    _logger.LogError("Error in processing the config file");
                    throw new Exception("Error in processing the config file");
                }

                var encryptedConnectionString = config.IntegratedSecurity
                    ?
                    $"Server={config.DataSource};Database={config.InitialCatalog};Integrated Security={config.IntegratedSecurity};"
                    : $"Server={config.DataSource};Database={config.InitialCatalog};{config.Credentials};";

                encryptedConnectionString = string.Concat(encryptedConnectionString, "Encrypt=false");

                return Task.FromResult(encryptedConnectionString);
            }
            catch (Exception e)
            {
                _logger.LogError("Error in {Name} - {Message} /n {Trace}",
                    methodName, e.Message, e.StackTrace);
                throw;
            }
        }
    }
}
