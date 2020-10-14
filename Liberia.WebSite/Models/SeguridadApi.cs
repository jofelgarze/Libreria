using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Liberia.WebSite.Models
{
    public class LoginVm
    {
        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "El nombre de usuario debe tener de 5 a 15 caracteres.")]
        public string Usuario { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "La contraseña debe tener de 5 a 15 caracteres.")]
        public string Password { get; set; }

    }

    public class RegistroVm
    {
        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "El nombre de usuario debe tener de 5 a 15 caracteres.")]
        public string Usuario { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "La contraseña debe tener de 5 a 15 caracteres.")]
        public string Password { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "La contraseña debe tener de 5 a 15 caracteres.")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarPassword { get; set; }
    }

    public class JsonToken 
    {
        public string access_token { get; set; }
    }
}
