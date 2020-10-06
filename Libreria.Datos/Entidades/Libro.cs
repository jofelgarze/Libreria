using System;
using System.Collections.Generic;
using System.Text;

namespace Libreria.Datos.Entidades
{
    public class Libro : AbsEntity<int>
    {
        public string Titulo { get; set; }
        public Autor Autor { get; set; }
        public bool Publicado { get; set; }
        public decimal Precio { get; set; }

    }
}
