using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
