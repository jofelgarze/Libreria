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
    public class CuentaController : Controller
    {
        private readonly IWebApiService _apiService;
        private readonly ISeguridadApiService _seguridadService;
        private readonly ILogger _logger;

        public CuentaController(IWebApiService apiService, ISeguridadApiService seguridadService, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AutoresController>();
            _apiService = apiService;
            _seguridadService = seguridadService;
        }

        // GET: Cuenta/Details/5
        public ActionResult Login()
        {
            return View();
        }

        // GET: Cuenta/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginVm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _seguridadService.Login(model);
                    HttpContext.Session.SetString("token", result.access_token);
                    return RedirectToAction("Index", "Autores", null);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            return View(model);
        }

        // GET: Cuenta/Details/5
        public ActionResult Registrar()
        {
            return View();
        }

        // POST: Cuenta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: Cuenta/Create
        public async Task<ActionResult> Registrar(RegistroVm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _seguridadService.Registrar(model);
                    HttpContext.Session.SetString("token", result.access_token);
                    return RedirectToAction("Index", "Autores", null);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

            }
            return View(model);
        }
        
    }
}