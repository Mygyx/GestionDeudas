using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Objetos;

namespace Datos
{
    public class BDAbono
    {
        public NpgsqlCommand cmd;
        public NpgsqlConnection conexionRetorno;
        Conexion conexion = new Conexion();

        public void InsertAbono(ObjAbono nuevoAbono)
        {
            try
            {
                conexionRetorno = conexion.ConexionBD();

                string query = "INSERT INTO abono (monto, id_cliente, id_usuario) " +
                               "VALUES (" + nuevoAbono.monto + ", " +
                               nuevoAbono.id_cliente + ", " +
                               nuevoAbono.id_usuario + ")";

                cmd = new NpgsqlCommand(query, conexionRetorno);
                cmd.ExecuteNonQuery();
                conexionRetorno.Close();

                Console.WriteLine("Abono insertado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar abono:\n" + ex.Message);
            }
        }

    }
}
