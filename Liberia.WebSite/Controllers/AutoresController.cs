using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Liberia.WebSite.Models;
using Liberia.WebSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Liberia.WebSite.Controllers
{
    public class AutoresController : Controller
    {
        private readonly IWebApiService _apiService;

        public AutoresController(IWebApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: Autores
        public async Task<ActionResult> Index()
        {
            var autores = await _apiService.GetAutoresAsync();
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
            if (ModelState.IsValid)
            {
                try
                {
                    model.FechaRegistro = DateTime.Now;
                    await _apiService.CreateAutorAsync(model);

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