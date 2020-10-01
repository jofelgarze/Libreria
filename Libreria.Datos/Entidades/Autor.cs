using System;
using System.Collections.Generic;
using System.Text;

namespace Libreria.Datos.Entidades
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaRegistro { get; set; }
        //public List<Libro> Libros { get; set; }
    }
}
