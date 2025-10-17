    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objetos
{
    public class ObjEmpresa
    {
        public int id { get; set; }

        public string nombre { get; set; }

        public string encargado { get; set; }

         public bool estado { get; set; }

        public DateTime fecha_creacion { get; set; }

        public string telefono { get; set; }
    }
}
