using Libreria.Datos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Negocio.Base
{
    public interface IAutorRepository : IRepository<Autor, Int32>
    {
        Task<IEnumerable<Autor>> GetAutoresConPublicacionPendiente();

        Task<Autor> GetLibrosPorAutorId(int id);
    }
}
