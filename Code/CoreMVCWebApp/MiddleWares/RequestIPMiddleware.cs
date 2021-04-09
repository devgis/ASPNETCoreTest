using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCWebApp.MiddleWares
{
    public class RequestIPMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestIPMiddleware> _logger;

        public RequestIPMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestIPMiddleware>();

        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation($"User IP:{context.Connection.RemoteIpAddress.ToString()}");
            await _next.Invoke(context);
        }
    }
}
