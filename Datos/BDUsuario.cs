using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Datos
{
    public class BDUsuario
    {
        public NpgsqlCommand cmd;
        public NpgsqlConnection conexionRetorno;
        Conexion conexion = new Conexion();
        public DataTable ObtenerUsuario(int cedula, string contrasena)
        {
            DataTable dt = new DataTable();
            conexionRetorno = conexion.ConexionBD();

            string query = "SELECT * FROM public.usuario " +
                           "WHERE cedula = " + cedula +
                           " AND contrasenna = '" + contrasena + "'" +
                           " AND estado = true";

            cmd = new NpgsqlCommand(query, conexionRetorno);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            da.Fill(dt);

            conexionRetorno.Close();
            return dt;
        }


    }
}
