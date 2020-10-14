using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Liberia.WebSite.Models;
using Liberia.WebSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Liberia.WebSite.Controllers
{
    public class AutoresController : Controller
    {
        private readonly IWebApiService _apiService;
        private readonly ISeguridadApiService _seguridadService;
        private readonly ILogger _logger;

        public AutoresController(IWebApiService apiService, ISeguridadApiService seguridadService, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AutoresController>();
            _apiService = apiService;
            _seguridadService = seguridadService;
        }

        // GET: Autores
        public async Task<ActionResult> Index()
        {
            //Se puede estar guardado en una variable de sesion para poder reutilizarlo
            var token = HttpContext.Session.GetString("token");

            if (token == null)
            {
                return RedirectToAction("Login", "Cuenta");
            }

            try
            {
                //Tambien puede obtener el token desde el servicio de seguridad api 
                //var token = await _seguridadService.Registrar(new RegistroVm { Usuario = "jgarciaz", Password = "123456", ConfirmarPassword = "123456" });
                //_logger.LogInformation("El token de seguridad es: " + result.access_token);

                var result2 = await _seguridadService.UserInfor(token);
                _logger.LogInformation("El usuario autenticado es: " + result2);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            var autores = await _apiService.GetAutoresAsync(token);
            return View(autores);
        }

        // GET: Autores/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Autores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Autor model)
        {
            //Se puede estar guardado en una variable de sesion para poder reutilizarlo
            var token = HttpContext.Session.GetString("token");

            if (ModelState.IsValid)
            {
                try
                {
                    model.FechaRegistro = DateTime.Now;
                    await _apiService.CreateAutorAsync(token, model);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View(model);
        }

        // GET: Autores/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Autores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Autores/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Autores/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}