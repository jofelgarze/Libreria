using System;
using System.Collections.Generic;
using System.Text;

namespace Libreria.Datos.Entidades
{
    public abstract class AbsEntity<K>
    {
        public K Id { get; set; }
    }
}
