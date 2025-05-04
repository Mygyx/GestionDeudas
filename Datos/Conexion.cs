using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Datos
{
    public class Conexion
    {
        public Conexion() { }
        public NpgsqlConnection conexion;
        public NpgsqlConnection ConexionBD()
        {
            string servidor = "localhost";
            string puerto = "5432";
            string usuario = "postgres";
            string clave = "1234";
            string baseDatos = "bddeudas";

            string cadenaConexion = "Server=" + servidor + ";" + "Port=" + puerto + ";" + "User Id=" + usuario + ";" + "Password=" + clave + ";" + "Database=" + baseDatos;
            conexion = new NpgsqlConnection(cadenaConexion);
            conexion.Open();

            return conexion;
        }
        public bool ProbarConexion()
        {
            try
            {
                using (var conn = ConexionBD())
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
