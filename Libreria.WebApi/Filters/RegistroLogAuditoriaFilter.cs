using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libreria.WebApi.Filters
{
    public class RegistroLogAuditoriaFilter : IAsyncActionFilter
    {
        public ILogger _logger;

        public RegistroLogAuditoriaFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<RegistroLogAuditoriaFilter>();
        }

        

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            // Do something before the action executes.
            _logger.LogInformation("Estoy dentro de auditoria");
            // next() calls the action method.
            var resultContext = await next();
            // resultContext.Result is set.
            // Do something after the action executes.
            _logger.LogInformation("El resultado es:" + resultContext.Result);
            context.HttpContext.Response.Headers.Add("Extra", new string[] { "Auditado" });
        }
    }
}
