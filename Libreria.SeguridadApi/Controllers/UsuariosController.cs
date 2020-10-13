using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Libreria.SeguridadApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Libreria.SeguridadApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<UsuariosController> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET api/values
        [Authorize]
        [HttpGet("info")]
        public string Get()
        {
            return User.Identity.Name;
        }

        private string CrearTokenJson(IdentityUser usuario) {
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, usuario.UserName),
                        new Claim(ClaimTypes.Name, usuario.UserName),
                        new Claim("tipo_usuario", "operador"),
                    };

            var claveBytes = Encoding.UTF8.GetBytes(JwtConfig.Clave);
            var llave = new SymmetricSecurityKey(claveBytes);
            var algoritmo = SecurityAlgorithms.HmacSha256;

            var credencialesFirma = new SigningCredentials(llave, algoritmo);

            var token = new JwtSecurityToken(
                JwtConfig.Emisor,
                JwtConfig.Audiencia,
                claims,
                DateTime.Now,
                DateTime.Now.AddMinutes(15),
                credencialesFirma);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        [HttpPost("registrar")]
        public async Task<IActionResult> registrar(UsuarioNuevoVm modelo) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = new IdentityUser()
            {
                UserName = modelo.Usuario,
                Email = ""
            };

            try
            {
                var result = await _userManager.CreateAsync(usuario, modelo.Password);
                if (result.Succeeded)
                {                   
                    return Ok(new { access_token = CrearTokenJson(usuario) });
                }

                return BadRequest("No se pudo registrar el usuario");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login(LoginVm modelo) {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = await _userManager.FindByNameAsync(modelo.Usuario);

            if (usuario != null)
            {
                var result = await _signInManager.PasswordSignInAsync(usuario, modelo.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok(new { access_token = CrearTokenJson(usuario) });
                }
            }

            return BadRequest("No se pudo registrar el usuario");
        }

    }
}