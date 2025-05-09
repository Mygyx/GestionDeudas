using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objetos
{
    public class ObjCuenta
    {
        public int cedula_cuenta { get; set; }
        public decimal saldo_actual { get; set; }
        public DateTime fecha_creacion { get; set; }
        public bool estado { get; set; }
    }
}
