using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Libreria.Datos;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Libreria.WebApi
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
            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var contextDb = services.GetRequiredService<LibreriaDbContext>();
                    LibreriaDbContextSeed.SeedAsync(contextDb, loggerFactory);
                }
                catch (Exception ex)
                {
                    var log = loggerFactory.CreateLogger<LibreriaDbContextSeed>();
                    log.LogError(ex.Message);
                }
            
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
