using Libreria.Datos.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libreria.Datos
{
    public class LibreriaDbContext: DbContext
    {
        public LibreriaDbContext(DbContextOptions<LibreriaDbContext> options):base(options)
        {

        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}
