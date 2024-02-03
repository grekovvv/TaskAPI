using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace TaskAPI.Filters
{
    public class AuthorizationFilter : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthorizationFilter> _logger;
        private readonly IWebHostEnvironment _webHost;
        private readonly string _token;
        public AuthorizationFilter(IConfiguration configuration, ILogger<AuthorizationFilter> logger, IWebHostEnvironment webHost)
        {
            _configuration = configuration;
            _logger = logger;
            _webHost = webHost;
            _token = configuration.GetValue<string>("Token") ?? throw new ArgumentNullException("Token");
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (_webHost.IsDevelopment())
            {
                //for debug
                //return Task.CompletedTask;
            }

            var token = context.HttpContext.Request.Headers["Authorization"];
            var ip = context.HttpContext.Connection.RemoteIpAddress?.ToString();

            if (token.Count != 0 && token[0].Equals(_token))
            {
                return Task.CompletedTask;
            }

            _logger.LogWarning($"Invalid Token Header:{JsonSerializer.Serialize(token)} Ip:{ip}");
            context.Result = new StatusCodeResult(401);
            return Task.CompletedTask;
        }
    }
}
