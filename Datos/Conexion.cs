using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Datos
{
    public class Conexion
    {
        public Conexion() { }
        public NpgsqlConnection conexion;
        public NpgsqlConnection ConexionBD()
        {
            string servidor = "127.0.0.1"; // mejor que "localhost"
            string puerto = "5432";
            string usuario = "postgres";
            string clave = "1234"; // ⚠️ insegura, cámbiala pronto
            string baseDatos = "BDdeudasSuper";

            string cadenaConexion =
                "Host=" + servidor + ";" +
                "Port=" + puerto + ";" +
                "Username=" + usuario + ";" +
                "Password=" + clave + ";" +
                "Database=" + baseDatos + ";";

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
            catch (Exception ex)
            {
                Console.Write(ex);
                return false;
                
            }
        }

    }
}
