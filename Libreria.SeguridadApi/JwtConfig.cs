﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libreria.SeguridadApi
{
    public class JwtConfig
    {
        public const string Clave = "Deben poner un valor considerablemente largo para que funcione...";
        public const string Emisor = "https://localhost:4001";
        public const string Audiencia = "https://localhost:5001";
    }
}
