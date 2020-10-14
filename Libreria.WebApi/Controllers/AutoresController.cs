﻿using System;
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
using Libreria.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;

namespace Libreria.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AutoresController : ControllerBase
    {
        private readonly IAutorRepository _repository;
        private readonly ILibroRepository _repositoryLibro;

        public AutoresController(IAutorRepository repository, ILibroRepository repositoryLibro)
        {
            _repository = repository;
            _repositoryLibro = repositoryLibro;
        }

        // GET: api/Autores
        [HttpGet]
        [AllowAnonymous]
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
        public async Task<IActionResult> PutAutor(int id, AutorVM modelo)
        {
            if (id != modelo.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var autor = new Autor
                    {
                        Id = modelo.Id,
                        Nombre = modelo.Nombre,
                        FechaRegistro = modelo.FechaRegistro.Value
                    };
                    await _repository.UpdateAsync(autor);
                    return NoContent();
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
            }

            return ValidationProblem(ModelState);
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

        // GET: api/Autores/5/Libros
        [HttpGet("{id}/Libros")]
        public async Task<ActionResult<List<LibroVM>>> GetLibrosPorAutor(int id)
        {
            var autor = await _repository.GetLibrosPorAutorId(id);
            if (autor != null)
            {
                if (autor.Libros.Count > 0)
                {
                    return Ok(autor.Libros.Select(l => new LibroVM() { 
                        Id = l.Id,
                        Precio = l.Precio,
                        Publicado = l.Publicado,
                        Titulo = l.Titulo,
                        Autor = new AutorLibroVM() { 
                            Nombre = l.Autor.Nombre,
                            Id = l.Autor.Id
                        }
                    }).ToList());
                }
                return NoContent();
            }
            return NotFound();
        }

        [EncriptacionResultFilter]
        // POST: api/Autores/5/Libros
        [HttpPost("{id}/Libros")]
        public async Task<ActionResult<LibroVM>> PostAutor(int id, LibroVM modelo)
        {
            if (modelo.Autor == null || id != modelo.Autor.Id)
            {
                return BadRequest();
            }

            var autor = await _repository.GetByIdAsync(id);

            if (autor == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                var libro = new Libro() { 
                    Titulo = modelo.Titulo,
                    Precio = modelo.Precio,
                    Publicado = modelo.Publicado,
                    Autor = autor
                };

                await _repositoryLibro.AddAsync(libro);

                modelo.Id = libro.Id;
                return CreatedAtAction("GetLibrosPorAutor", new { id = autor.Id }, modelo);
            }
            return ValidationProblem(ModelState);
        }
    }
}
