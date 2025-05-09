using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Objetos;

namespace Negocio
{
    public class Abono
    {
        BDAbono bd = new BDAbono();
        public void insertarAbono(ObjAbono obj) {
            bd.InsertAbono(obj);
        }
    }
}
