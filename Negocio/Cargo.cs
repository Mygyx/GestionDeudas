using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Objetos;

namespace Negocio
{
    public class Cargo
    {
        BDCargo bd = new BDCargo();

        public void insertarCargo(ObjCargo obj) {
            bd.InsertCargo(obj);
        }

    }
}
