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
    public class BDCargo
    {
        public NpgsqlCommand cmd;
        public NpgsqlConnection conexionRetorno;
        Conexion conexion = new Conexion();

        public void InsertCargo(ObjCargo nuevoCargo)
        {
            try
            {
                conexionRetorno = conexion.ConexionBD();

                string query = "INSERT INTO cargo (monto, descripcion, id_cliente, id_usuario) " +
                               "VALUES (" + nuevoCargo.monto + ", '" +
                               nuevoCargo.descripcion + "', " +
                               nuevoCargo.id_cliente + ", " +
                               nuevoCargo.id_usuario + ")";

                cmd = new NpgsqlCommand(query, conexionRetorno);
                cmd.ExecuteNonQuery();
                conexionRetorno.Close();

             
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar cargo:\n" + ex.Message);
            }
        }
    }
}
