using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liberia.WebSite.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaRegistro { get; set; }
        public List<Libro> Libros { get; set; }

        public byte[] FotoPerfil { get; set; }
        public IFormFile ArchivoFoto { get; set; }
    }

    public class Libro {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public Autor Autor { get; set; }
        public bool Publicado { get; set; }
        public decimal Precio { get; set; }

    }
}
