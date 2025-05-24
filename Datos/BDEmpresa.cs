using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using Objetos;

namespace Datos
{
    public class BDEmpresa
    {
        public NpgsqlCommand cmd;
        public NpgsqlConnection conexionRetorno;
        Conexion conexion = new Conexion();

        public void InsertEmpresa(ObjEmpresa nuevaEmpresa)
        {
            try
            {
                conexionRetorno = conexion.ConexionBD();

                string query = "INSERT INTO empresa (nombre, encargado, estado, telefono) " +
                               "VALUES ('" + nuevaEmpresa.nombre + "', '" +
                                            nuevaEmpresa.encargado + "', " +
                                            nuevaEmpresa.estado + ", '" +
                                            nuevaEmpresa.telefono + "')";

                cmd = new NpgsqlCommand(query, conexionRetorno);
                cmd.ExecuteNonQuery();
                conexionRetorno.Close();

                MessageBox.Show("Empresa insertada correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar empresa:\n" + ex.Message);
            }
        }

        public void UpdateEmpresa(ObjEmpresa empresa)
        {
            try
            {
                conexionRetorno = conexion.ConexionBD();

                string query = "UPDATE empresa SET " +
                               "nombre = '" + empresa.nombre + "', " +
                               "encargado = '" + empresa.encargado + "', " +
                               "estado = " + empresa.estado + ", " +
                               "telefono = '" + empresa.telefono + "' " +
                               "WHERE id = " + empresa.id;

                cmd = new NpgsqlCommand(query, conexionRetorno);
                int rowsAffected = cmd.ExecuteNonQuery();
                conexionRetorno.Close();

                if (rowsAffected > 0)
                    MessageBox.Show("Empresa actualizada correctamente.");
                else
                    MessageBox.Show("No se encontró ninguna empresa con ese ID.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar empresa:\n" + ex.Message);
            }
        }

    }
}
