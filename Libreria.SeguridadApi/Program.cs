using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Libreria.SeguridadApi.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Libreria.SeguridadApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var servidor = CreateWebHostBuilder(args).Build();
            CrearActualizarBaseDatos(servidor);
            servidor.Run();
        }

        private static void CrearActualizarBaseDatos(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                
                try
                {
                    var contextDb = services.GetRequiredService<ApplicationContext>();
                    contextDb.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                    var log = loggerFactory.CreateLogger<ApplicationContext>();
                    log.LogError(ex.Message);
                }

            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
