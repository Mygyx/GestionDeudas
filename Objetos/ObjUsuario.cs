using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objetos
{
    public class ObjUsuario
    {
        public int cedula { get; set; }

        public string nombre { get; set; }

        public string clave { get; set; }

        public DateTime fecha_creacion { get; set; }

        public string correo { get; set; }

        public bool estado { get; set; }
    }
}
