using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{
    public class Verificacion
    {
        Conexion conexion = new Conexion();
        public bool VeficacionBaseDatos() {
            return conexion.ProbarConexion();
        }
    }
}
