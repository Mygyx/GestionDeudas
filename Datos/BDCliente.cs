using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using Objetos;

namespace Datos
{
    public class BDCliente
    {
        public NpgsqlCommand cmd;
        public NpgsqlConnection conexionRetorno;
        Conexion conexion = new Conexion();
        public void InsertCliente(ObjCliente nuevoCliente)
        {
            conexionRetorno = conexion.ConexionBD();

            string query = "INSERT INTO cliente (cedula, nombre, telefono, direccion, estado, fecha_creacion) " +
                           "VALUES (" + nuevoCliente.cedula + ", '" + nuevoCliente.nombre + "', '" +
                           nuevoCliente.telefono + "', '" + nuevoCliente.direccion + "', '" +
                           (nuevoCliente.estado ? "true" : "false") + "', '" +
                           nuevoCliente.fecha_creacion.ToString("yyyy-MM-dd") + "')";

            cmd = new NpgsqlCommand(query, conexionRetorno);
            cmd.ExecuteNonQuery();
            conexionRetorno.Close();
        }
        public void UpdateCliente(ObjCliente cliente)
        {
            conexionRetorno = conexion.ConexionBD();

            string query = "UPDATE cliente SET " +
                           "nombre = '" + cliente.nombre + "', " +
                           "telefono = '" + cliente.telefono + "', " +
                           "direccion = '" + cliente.direccion + "', " +
                           "estado = '" + (cliente.estado ? "true" : "false") + "', " +
                           "fecha_creacion = '" + cliente.fecha_creacion.ToString("yyyy-MM-dd") + "' " +
                           "WHERE cedula = " + cliente.cedula;

            cmd = new NpgsqlCommand(query, conexionRetorno);
            cmd.ExecuteNonQuery();
            conexionRetorno.Close();
        }
        public void ActualizarEmpresaCliente(int cedulaCliente, int nuevoIdEmpresa)
        {
            try
            {
                conexionRetorno = conexion.ConexionBD();

                string query = "UPDATE cliente SET id_empresa = " + nuevoIdEmpresa +
                               " WHERE cedula = " + cedulaCliente;

                cmd = new NpgsqlCommand(query, conexionRetorno);
                int filasAfectadas = cmd.ExecuteNonQuery();
                conexionRetorno.Close();

                if (filasAfectadas > 0)
                    Console.WriteLine("Empresa del cliente actualizada correctamente.");
                else
                    Console.WriteLine("No se encontró un cliente con esa cédula.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar la empresa del cliente:\n" + ex.Message);
            }
        }

        public DataTable GetClientesConEmpresa()
        {
            DataTable dt = new DataTable();

            try
            {
                conexionRetorno = conexion.ConexionBD();

                string query = @"
            SELECT 
                c.cedula,
                c.nombre,
                c.telefono,
                c.direccion,
                c.estado,
                c.fecha_creacion,
                e.nombre AS nombre_empresa
            FROM cliente c
            LEFT JOIN empresa e ON c.id_empresa = e.id";

                cmd = new NpgsqlCommand(query, conexionRetorno);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);

                da.Fill(dt);
                conexionRetorno.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener clientes:\n" + ex.Message);
            }

            return dt;
        }


    }
}
