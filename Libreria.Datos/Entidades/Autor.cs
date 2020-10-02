using System;
using System.Collections.Generic;
using System.Text;

namespace Libreria.Datos.Entidades
{
    public class Autor : AbsEntity<int>
    {

        public string Nombre { get; set; }
        public DateTime FechaRegistro { get; set; }
        public List<Libro> Libros { get; set; }
    }
}
