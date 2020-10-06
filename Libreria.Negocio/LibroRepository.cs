using Libreria.Datos;
using Libreria.Datos.Entidades;
using Libreria.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libreria.Negocio
{
    public class LibroRepository : Repository<Libro,int>, ILibroRepository
    {
        public LibroRepository(LibreriaDbContext context): base(context)
        {
        }
    }
}
