using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libreria.WebApi.Filters
{
    public class EncriptacionResultFilter : ResultFilterAttribute
    {

        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            context.HttpContext.Response.Headers.Add("Especifico", new string[] { "Filtro de Anotacion" });
            return base.OnResultExecutionAsync(context, next);
        }

    }
}
