using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Libreria.Negocio;
using Libreria.Negocio.Base;
using Libreria.WebApi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Libreria.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Configuracion de Contextos de Base de Datos
            services.AddDbContext<Libreria.Datos.LibreriaDbContext>(config => {
                config.UseSqlServer(Configuration.GetConnectionString("LibreriaDb"), b => b.MigrationsAssembly("Libreria.Datos"));
                //config.UseInMemoryDatabase("DbMemory");
            }, ServiceLifetime.Singleton);

            //Configuracion de Injeccion de Dependencias
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped(typeof(IAutorRepository), typeof(AutorRepository));
            services.AddScoped(typeof(ILibroRepository), typeof(LibroRepository));

            services.AddTransient<IAutorRepository, AutorRepository>();
            services.AddTransient<ILibroRepository, LibroRepository>();
            //FIN -- Configuracion ID

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Autores API", Version = "v1"});
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(RegistroLogAuditoriaFilter)); //Agregando filtros de accion de forma GLOBAL

            });
                //.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Autores API v1");
            });
        }
    }
}
