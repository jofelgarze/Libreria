using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Libreria.WebApi.Models
{
    public class LibroVM
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Titulo es obligatorio.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "El Titulo debe tener entre 5 y 150 caracteres.")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "El autor es obligatorio.")]
        public AutorLibroVM Autor { get; set; }
        public bool Publicado { get; set; }
        public decimal Precio { get; set; }
    }

    public class AutorLibroVM {

        [Required(ErrorMessage = "El Id de autor es obligatorio.")]
        public int Id { get; set; }

        [StringLength(150, MinimumLength = 5, ErrorMessage = "El nombre debe tener entre 5 y 150 caracteres.")]
        public string Nombre { get; set; }
    }
}
