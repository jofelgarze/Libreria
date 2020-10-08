using Libreria.Datos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Datos
{
    public class LibreriaDbContextSeed
    {

        public static async Task SeedAsync(LibreriaDbContext context, ILoggerFactory loggerFactory, int? retry = 0) {

            int reintentoPorIndisponibilidad = retry.Value;

            try {
                context.Database.Migrate();

                if (!context.Autores.Any())
                {
                    context.Autores.AddRange(GetAutoresIniciales());
                    await context.SaveChangesAsync();
                }
            } catch (Exception ex) {
                if (reintentoPorIndisponibilidad < 3)
                {
                    reintentoPorIndisponibilidad++;
                    var log = loggerFactory.CreateLogger<LibreriaDbContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(context, loggerFactory, reintentoPorIndisponibilidad);
                }
            }

        }

        private static List<Autor> GetAutoresIniciales()
        {
            return new List<Autor>
            {
                new Autor(){
                    Nombre = "Andres Paladin",
                    FechaRegistro = DateTime.Now.AddDays(-150)
                },
                new Autor(){
                    Nombre = "Adriana Merchan",
                    FechaRegistro = DateTime.Now.AddDays(-140)
                }
            };
        }
    }
}
