using Liberia.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liberia.WebSite.Services
{
    public interface IWebApiService : IDisposable
    {

        Task<List<Autor>> GetAutoresAsync(string token);

        Task<Autor> GetAutorAsync(int id);

        Task<Autor> CreateAutorAsync(string token, Autor model);

        Task UpdateAutorAsync(Autor model);

        Task DeleteAutorAsync(int id);

    }
}
