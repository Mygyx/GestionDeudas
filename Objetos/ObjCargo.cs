using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objetos
{
    public class ObjCargo
    {
        public decimal monto { get; set; }
        public string descripcion { get; set; }
        public int id_cliente { get; set; }
        public int id_usuario { get; set; }
    }
}
