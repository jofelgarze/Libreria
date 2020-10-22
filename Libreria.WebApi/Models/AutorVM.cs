using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Libreria.WebApi.Models
{
    public class AutorVM
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "El nombre debe tener entre 5 y 150 caracteres.")]
        public string Nombre { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "La fecha es obligatoria.")]
        public DateTime? FechaRegistro { get; set; }
        public bool Activo { get; set; }
        public byte[] FotoPerfil { get; set; }
    }
}
