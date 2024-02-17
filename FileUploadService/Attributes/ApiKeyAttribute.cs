using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FileUploadService.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAuthorizationFilter
    {
        private const string ApiKey = "X-API-KEY";
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string apiKeyVal = context.HttpContext.Request.Headers[ApiKey].FirstOrDefault("") ?? "";
            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(ApiKey) ?? "";
            if (!apiKey.Equals(apiKeyVal))
            {
                string messageError = "Invalid API key";
                context.Result = new UnauthorizedObjectResult(messageError);
            }
        }
    }
}
