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
    public class BDCuenta
    {
        public NpgsqlCommand cmd;
        public NpgsqlConnection conexionRetorno;
        Conexion conexion = new Conexion();
        public void InsertCuenta(ObjCuenta nuevaCuenta)
        {
            conexionRetorno = conexion.ConexionBD();

            string query = "INSERT INTO cuenta (cedula_cuenta, saldo_actual, fecha_creacion, estado) " +
                           "VALUES (" + nuevaCuenta.cedula_cuenta + ", " + nuevaCuenta.saldo_actual + ", '" +
                           nuevaCuenta.fecha_creacion.ToString("yyyy-MM-dd") + "', '" +
                           (nuevaCuenta.estado ? "true" : "false") + "')";

            cmd = new NpgsqlCommand(query, conexionRetorno);
            cmd.ExecuteNonQuery();
            conexionRetorno.Close();
        }
        public DataTable BuscarCuentasActivasPorCedulaParcial(int cedulaParcial)
        {
            DataTable tabla = new DataTable();

            string query = @"
                SELECT 
                    cta.cedula_cuenta, 
                    cli.nombre, 
                    cli.telefono, 
                    cli.direccion,
                    cta.saldo_actual, 
                    cta.fecha_creacion
                FROM 
                    cuenta cta
                JOIN 
                    cliente cli 
                    ON cli.cedula = cta.cedula_cuenta
                WHERE 
                    cta.estado = true
                    AND CAST(cli.cedula AS TEXT) LIKE '%" + cedulaParcial + "%';";

            using (NpgsqlConnection conn = conexion.ConexionBD())
            {
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }
    }
}
