using System.Reflection;
using System.Runtime.Serialization;

namespace Ligl.LegalManagement.Api.Middleware
{
    public static class CustomHeaderMiddleWare
    {
        public static IApplicationBuilder UseCustomHeaderMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomHeader>();
        }
                
    }
    public class CustomHeader(RequestDelegate next)
    {
        private enum CustomHeaders
        {
            [EnumMember(Value = "x-lid")]
            LegalGroupId = 0,
        }

        public async Task InvokeAsync(HttpContext context)
        {
            foreach (var type in Enum.GetValues<CustomHeaders>())
            {
                var headerName = type.GetEnumMemberValue()!;

                _ = context.Request.Headers.TryGetValue(headerName, out var headerValue);
                if (context.Items.ContainsKey(headerValue))
                {
                    context.Items[headerName] = headerValue;
                }
                else
                {
                    context.Items.Add(headerName, headerValue);
                }
            }

            await next(context);
        }
    }


    public static class EnumExtension
    {
        public static string? GetEnumMemberValue<T>(this T value)
            where T : Enum
        {
            return typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString())
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;
        }
    }
}
