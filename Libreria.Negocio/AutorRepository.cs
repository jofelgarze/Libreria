using Libreria.Datos;
using Libreria.Datos.Entidades;
using Libreria.Negocio.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Negocio
{
    public class AutorRepository : Repository<Autor, int>, IAutorRepository
    {
        public AutorRepository(LibreriaDbContext context): base(context)
        {
        }

        public async Task<IEnumerable<Autor>> GetAutoresConPublicacionPendiente()
        {
            return await _context.Autores.Include("Libros")
                .Where(a => a.Libros.Any(l => !l.Publicado)).ToListAsync();
        }
    }
}
