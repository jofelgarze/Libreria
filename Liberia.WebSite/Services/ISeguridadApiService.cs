using Liberia.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liberia.WebSite.Services
{
    public interface ISeguridadApiService
    {
        Task<JsonToken> Login(LoginVm model);

        Task<JsonToken> Registrar(RegistroVm model);

        Task<string> UserInfor(string token);
    }
}
