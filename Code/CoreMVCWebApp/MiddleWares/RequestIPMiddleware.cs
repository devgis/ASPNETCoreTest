using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
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

            //ReadConfig
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("myconfig.json"); 
            var config = builder.Build();
            _logger.LogInformation($"Hello Configure:{config["Myconfig:Hello"]}");
            await _next.Invoke(context);
        }
    }
}
