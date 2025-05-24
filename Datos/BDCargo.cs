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

        public DataTable ObtenerMovimientosDataTable()
        {
            DataTable tabla = new DataTable();

            using (var conn = conexion.ConexionBD())
            {
                string query = @"
                SELECT 
                    'Cargo' AS tipo_movimiento,
                    c.id,
                    c.monto,
                    c.fecha,
                    c.descripcion,
                    cl.nombre AS nombre_cliente,
                    u.nombre AS nombre_usuario,
                    c.saldo_anterior
                FROM cargo c
                JOIN cliente cl ON c.id_cliente = cl.cedula
                JOIN usuario u ON c.id_usuario = u.cedula

                UNION ALL

                SELECT 
                    'Abono' AS tipo_movimiento,
                    a.id,
                    a.monto,
                    a.fecha,
                    'Abono realizado' AS descripcion,
                    cl.nombre AS nombre_cliente,
                    u.nombre AS nombre_usuario,
                    a.saldo_anterior
                FROM abono a
                JOIN cliente cl ON a.id_cliente = cl.cedula
                JOIN usuario u ON a.id_usuario = u.cedula

                ORDER BY fecha ASC;";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }
        public DataTable ObtenerMovimientosDataTable(string cedulaParcial)
        {
            DataTable tabla = new DataTable();

            using (var conn = conexion.ConexionBD())
            {
                string query = @"
            SELECT 
                'Cargo' AS tipo_movimiento,
                c.id,
                c.monto,
                c.fecha,
                c.descripcion,
                cl.nombre AS nombre_cliente,
                u.nombre AS nombre_usuario,
                c.saldo_anterior
            FROM cargo c
            JOIN cliente cl ON c.id_cliente = cl.cedula
            JOIN usuario u ON c.id_usuario = u.cedula
            WHERE CAST(cl.cedula AS TEXT) LIKE @cedulaFiltro

            UNION ALL

            SELECT 
                'Abono' AS tipo_movimiento,
                a.id,
                a.monto,
                a.fecha,
                'Abono realizado' AS descripcion,
                cl.nombre AS nombre_cliente,
                u.nombre AS nombre_usuario,
                a.saldo_anterior
            FROM abono a
            JOIN cliente cl ON a.id_cliente = cl.cedula
            JOIN usuario u ON a.id_usuario = u.cedula
            WHERE CAST(cl.cedula AS TEXT) LIKE @cedulaFiltro

            ORDER BY fecha ASC;";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cedulaFiltro", "%" + cedulaParcial + "%");
                    using (var adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(tabla);
                    }
                }
            }

            return tabla;
        }

    }
}
