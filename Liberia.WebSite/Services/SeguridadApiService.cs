using Liberia.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Liberia.WebSite.Services
{
    public class SeguridadApiService : ISeguridadApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private const string urlBase = "https://localhost:4001";
        public SeguridadApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpClient = _httpClientFactory.CreateClient() ?? throw new ArgumentNullException(nameof(httpClientFactory));
           
            _httpClient.BaseAddress = new Uri(urlBase);
        }

        public async Task<JsonToken> Login(LoginVm model)
        {
            var bodyObj = new StringContent(//Se puede usar objeto anonimo
                    Newtonsoft.Json.JsonConvert.SerializeObject(new { Usuario = model.Usuario, Password = model.Password}),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );
            var respuesta = await _httpClient.PostAsync($"/api/usuarios/login", bodyObj);
            var contenido = await respuesta.Content.ReadAsStringAsync();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<JsonToken>(contenido);
        }

        public async Task<JsonToken> Registrar(RegistroVm model)
        {
            var body = new StringContent(//O se puede usar una clase fuertemente tipada
                    Newtonsoft.Json.JsonConvert.SerializeObject(model),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );
            var respuesta = await _httpClient.PostAsync($"/api/usuarios/registrar", body);
            var contenido = await respuesta.Content.ReadAsStringAsync();

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {                
                return Newtonsoft.Json.JsonConvert.DeserializeObject<JsonToken>(contenido);
            }
            throw new ApplicationException(contenido);
        }

        public async Task<string> UserInfor(string token)
        {
            var httpClientAutorizado = _httpClientFactory.CreateClient();
            httpClientAutorizado.BaseAddress = new Uri(urlBase);
            httpClientAutorizado.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var respuesta = await httpClientAutorizado.GetAsync($"/api/usuarios/info");
            return await respuesta.Content.ReadAsStringAsync();
        }
    }
}
