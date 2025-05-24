using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Objetos;

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

        public void InsertarUsuario(ObjUsuario usuario)
        {
            using (var conn = conexion.ConexionBD())
            {
                string query = $@"
            INSERT INTO usuario (cedula, nombre, contrasenna, fecha_creacion, correo, estado)
            VALUES ({usuario.cedula}, 
                    '{usuario.nombre}', 
                    '{usuario.clave}', 
                    '{usuario.fecha_creacion:yyyy-MM-dd}', 
                    '{usuario.correo}', 
                    {usuario.estado});";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void ActualizarUsuario(ObjUsuario usuario)
        {
            using (var conn = conexion.ConexionBD())
            {
                string query = $@"
            UPDATE usuario
            SET nombre = '{usuario.nombre}',
                correo = '{usuario.correo}',
                estado = {usuario.estado}
            WHERE cedula = {usuario.cedula};";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable ObtenerUsuariosTpdos()
        {
            DataTable tabla = new DataTable();

            using (var conn = conexion.ConexionBD())
            {
                string query = "SELECT * FROM usuario ORDER BY cedula;";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }
        public DataTable BuscarUsuariosPorCedula(string cedulaParcial)
        {
            DataTable tabla = new DataTable();

            using (var conn = conexion.ConexionBD())
            {
                string query = $@"
            SELECT * 
            FROM usuario 
            WHERE CAST(cedula AS TEXT) LIKE '%{cedulaParcial}%'
            ORDER BY cedula;";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }

    }
}
