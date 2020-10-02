using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Libreria.Datos;
using Libreria.Datos.Entidades;
using Libreria.Negocio.Base;
using Libreria.WebApi.Models;

namespace Libreria.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly IAutorRepository _repository;
        public AutoresController(IAutorRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Autores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAutores()
        {
            var result = await _repository.GetAllAsync();
            return result.ToList();
        }

        // GET: api/Autores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Autor>> GetAutor(int id)
        {
            var autor = await _repository.GetByIdAsync(id);

            if (autor == null)
            {
                return NotFound();
            }

            return autor;
        }

        // GET: api/Autores/5/Libros
        [HttpGet("{id}/Libros")]
        public async Task<ActionResult<List<Libro>>> GetLibrosPorAutor(int id)
        {           
            return new List<Libro>();
        }

        // POST: api/Autores
        [HttpPost]
        public async Task<ActionResult<Autor>> PostAutor(AutorVM modelo)
        {
            if (ModelState.IsValid)
            {
                var autor = new Autor
                {
                    Nombre = modelo.Nombre,
                    FechaRegistro = modelo.FechaRegistro.Value
                }; 

                await _repository.AddAsync(autor);

                return CreatedAtAction("GetAutor", new { id = autor.Id }, autor);
            }
            return ValidationProblem(ModelState);
        }

        // PUT: api/Autores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutor(int id, Autor autor)
        {
            if (id != autor.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateAsync(autor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.ExistsById(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       
        // DELETE: api/Autores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Autor>> DeleteAutor(int id)
        {
            var autor = await _repository.GetByIdAsync(id);
            if (autor == null)
            {
                return NotFound();
            }

            await _repository.DeleteteAsync(autor);

            return autor;
        }

    }
}
