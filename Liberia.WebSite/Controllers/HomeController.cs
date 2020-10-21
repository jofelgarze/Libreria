using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Liberia.WebSite.Models;
using System.Net.Http;
using Liberia.WebSite.Services;

namespace Liberia.WebSite.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebApiService _apiService;

        public HomeController(IHttpClientFactory httpClientFactory, IWebApiService apiService)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            //Modelo automatizado
            //var cliente = new webapiClient("https://localhost:5011", _httpClientFactory.CreateClient());
            //var result = await cliente.AutoresAllAsync();

            //Metodo manual
            var urlBase = "https://localhost:5011";
            var clienteHttp = _httpClientFactory.CreateClient();

            clienteHttp.BaseAddress = new Uri(urlBase);
            var respuesta = await clienteHttp.GetAsync("/api/Autores");
            var contenido = await respuesta.Content.ReadAsStringAsync();

            var autores = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Autor>>(contenido);

            ViewBag.Datos = autores;
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
