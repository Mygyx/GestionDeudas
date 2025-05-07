using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Objetos;

namespace Negocio
{
    public class Usuario
    {
        BDUsuario bd = new BDUsuario();
        public ObjUsuario GetUsuario(int cedula, string contra)
        {
            DataTable dataTable = bd.ObtenerUsuario(cedula, contra);
            ObjUsuario objUsuario = null;

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];

                objUsuario = new ObjUsuario()
                {
                    cedula = Convert.ToInt32(row["cedula"]),
                    nombre = row["nombre"].ToString(),
                    clave = row["contrasenna"].ToString(),
                    fecha_creacion = DateTime.Parse(row["fecha_creacion"].ToString()),
                    correo = row["correo"].ToString(),
                    estado = Convert.ToBoolean(row["estado"])
                };
            }

            return objUsuario;
        }

    }
}
