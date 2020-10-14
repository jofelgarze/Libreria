using Liberia.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Liberia.WebSite.Services
{
    public class WebApiService : IWebApiService
    {        
        private readonly HttpClient _httpClient;

        public WebApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient() ?? throw new ArgumentNullException(nameof(httpClientFactory));
            var urlBase = "https://localhost:5001";
            _httpClient.BaseAddress = new Uri(urlBase);
        }

        public async Task<List<Autor>> GetAutoresAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var respuesta = await _httpClient.GetAsync($"/api/Autores");
            var contenido = await respuesta.Content.ReadAsStringAsync();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Autor>>(contenido);
        }

        public async Task<Autor> GetAutorAsync(int id)
        {
            var respuesta = await _httpClient.GetAsync($"/api/Autores/{id}");
            var contenido = await respuesta.Content.ReadAsStringAsync();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Autor>(contenido);
        }
        
        public async Task<Autor> CreateAutorAsync(string token, Autor model)
        {
             _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var body = new StringContent(
                    Newtonsoft.Json.JsonConvert.SerializeObject(model),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );
            var respuesta = await _httpClient.PostAsync($"/api/Autores", body);
            var contenido = await respuesta.Content.ReadAsStringAsync();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Autor>(contenido);
        }
        
        public async Task UpdateAutorAsync(Autor model)
        {
            var body = new StringContent(
                    Newtonsoft.Json.JsonConvert.SerializeObject(model),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );
            var respuesta = await _httpClient.PutAsync($"/api/Autores/{model.Id}", body);
            
        }
       
        public async Task DeleteAutorAsync(int id)
        {
            var respuesta = await _httpClient.DeleteAsync($"/api/Autores/{id}");
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
