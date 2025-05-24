using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Datos
{
    public class BDReportes
    {
        public NpgsqlCommand cmd;
        public NpgsqlConnection conexionRetorno;
        Conexion conexion = new Conexion();

        public DataTable ObtenerReporteMovimientosPorCedula(string filtroCedula)
        {
            DataTable tabla = new DataTable();

            using (var conn = conexion.ConexionBD())
            {
                string query = $@"
            SELECT 
                cl.cedula,
                cl.nombre AS cliente_nombre,
                cl.telefono,
                cl.direccion,
                cu.saldo_actual,
                cu.fecha_creacion AS fecha_cuenta,
                'Abono' AS tipo_movimiento,
                ab.monto,
                ab.fecha AS fecha_movimiento,
                ab.saldo_anterior,
                u.nombre AS usuario_responsable
            FROM abono ab
            JOIN cliente cl ON ab.id_cliente = cl.cedula
            JOIN cuenta cu ON cl.cedula = cu.cedula_cuenta
            JOIN usuario u ON ab.id_usuario = u.cedula
            WHERE CAST(cl.cedula AS TEXT) ILIKE '%{filtroCedula}%'

            UNION ALL

            SELECT 
                cl.cedula,
                cl.nombre AS cliente_nombre,
                cl.telefono,
                cl.direccion,
                cu.saldo_actual,
                cu.fecha_creacion AS fecha_cuenta,
                'Cargo' AS tipo_movimiento,
                ca.monto,
                ca.fecha AS fecha_movimiento,
                ca.saldo_anterior,
                u.nombre AS usuario_responsable
            FROM cargo ca
            JOIN cliente cl ON ca.id_cliente = cl.cedula
            JOIN cuenta cu ON cl.cedula = cu.cedula_cuenta
            JOIN usuario u ON ca.id_usuario = u.cedula
            WHERE CAST(cl.cedula AS TEXT) ILIKE '%{filtroCedula}%'

            ORDER BY cedula, fecha_movimiento;";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }

        public DataTable ObtenerReporteMovimientos()
        {
            DataTable tabla = new DataTable();

            using (var conn = conexion.ConexionBD())
            {
                string query = @"
            SELECT 
                cl.cedula,
                cl.nombre AS cliente_nombre,
                cl.telefono,
                cl.direccion,
                cu.saldo_actual,
                cu.fecha_creacion AS fecha_cuenta,
                'Abono' AS tipo_movimiento,
                ab.monto,
                ab.fecha AS fecha_movimiento,
                ab.saldo_anterior,
                u.nombre AS usuario_responsable
            FROM abono ab
            JOIN cliente cl ON ab.id_cliente = cl.cedula
            JOIN cuenta cu ON cl.cedula = cu.cedula_cuenta
            JOIN usuario u ON ab.id_usuario = u.cedula

            UNION ALL

            SELECT 
                cl.cedula,
                cl.nombre AS cliente_nombre,
                cl.telefono,
                cl.direccion,
                cu.saldo_actual,
                cu.fecha_creacion AS fecha_cuenta,
                'Cargo' AS tipo_movimiento,
                ca.monto,
                ca.fecha AS fecha_movimiento,
                ca.saldo_anterior,
                u.nombre AS usuario_responsable
            FROM cargo ca
            JOIN cliente cl ON ca.id_cliente = cl.cedula
            JOIN cuenta cu ON cl.cedula = cu.cedula_cuenta
            JOIN usuario u ON ca.id_usuario = u.cedula

            ORDER BY cedula, fecha_movimiento;";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }
        public DataTable ObtenerReporteMovimientosPorFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable tabla = new DataTable();

            using (var conn = conexion.ConexionBD())
            {
                string query = $@"
            SELECT 
                cl.cedula,
                cl.nombre AS cliente_nombre,
                cl.telefono,
                cl.direccion,
                cu.saldo_actual,
                cu.fecha_creacion AS fecha_cuenta,
                'Abono' AS tipo_movimiento,
                ab.monto,
                ab.fecha AS fecha_movimiento,
                ab.saldo_anterior,
                u.nombre AS usuario_responsable
            FROM abono ab
            JOIN cliente cl ON ab.id_cliente = cl.cedula
            JOIN cuenta cu ON cl.cedula = cu.cedula_cuenta
            JOIN usuario u ON ab.id_usuario = u.cedula
            WHERE ab.fecha BETWEEN '{fechaInicio:yyyy-MM-dd}' AND '{fechaFin:yyyy-MM-dd} 23:59:59'

            UNION ALL

            SELECT 
                cl.cedula,
                cl.nombre AS cliente_nombre,
                cl.telefono,
                cl.direccion,
                cu.saldo_actual,
                cu.fecha_creacion AS fecha_cuenta,
                'Cargo' AS tipo_movimiento,
                ca.monto,
                ca.fecha AS fecha_movimiento,
                ca.saldo_anterior,
                u.nombre AS usuario_responsable
            FROM cargo ca
            JOIN cliente cl ON ca.id_cliente = cl.cedula
            JOIN cuenta cu ON cl.cedula = cu.cedula_cuenta
            JOIN usuario u ON ca.id_usuario = u.cedula
            WHERE ca.fecha BETWEEN '{fechaInicio:yyyy-MM-dd}' AND '{fechaFin:yyyy-MM-dd} 23:59:59'

            ORDER BY cedula, fecha_movimiento;";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }
        public DataTable ObtenerReporteMovimientosPorMonto(decimal montoMin, decimal montoMax)
        {
            DataTable tabla = new DataTable();

            using (var conn = conexion.ConexionBD())
            {
                string query = $@"
            SELECT 
                cl.cedula,
                cl.nombre AS cliente_nombre,
                cl.telefono,
                cl.direccion,
                cu.saldo_actual,
                cu.fecha_creacion AS fecha_cuenta,
                'Abono' AS tipo_movimiento,
                ab.monto,
                ab.fecha AS fecha_movimiento,
                ab.saldo_anterior,
                u.nombre AS usuario_responsable
            FROM abono ab
            JOIN cliente cl ON ab.id_cliente = cl.cedula
            JOIN cuenta cu ON cl.cedula = cu.cedula_cuenta
            JOIN usuario u ON ab.id_usuario = u.cedula
            WHERE ab.monto BETWEEN {montoMin} AND {montoMax}

            UNION ALL

            SELECT 
                cl.cedula,
                cl.nombre AS cliente_nombre,
                cl.telefono,
                cl.direccion,
                cu.saldo_actual,
                cu.fecha_creacion AS fecha_cuenta,
                'Cargo' AS tipo_movimiento,
                ca.monto,
                ca.fecha AS fecha_movimiento,
                ca.saldo_anterior,
                u.nombre AS usuario_responsable
            FROM cargo ca
            JOIN cliente cl ON ca.id_cliente = cl.cedula
            JOIN cuenta cu ON cl.cedula = cu.cedula_cuenta
            JOIN usuario u ON ca.id_usuario = u.cedula
            WHERE ca.monto BETWEEN {montoMin} AND {montoMax}

            ORDER BY cedula, fecha_movimiento;";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }
        public DataTable ObtenerMovimientosPorUsuarioConFiltroCedula(string filtroCedula)
        {
            DataTable tabla = new DataTable();

            using (var conn = conexion.ConexionBD())
            {
                string query = $@"
            SELECT 
                u.cedula AS cedula_usuario,
                u.nombre AS nombre_usuario,
                cl.cedula AS cedula_cliente,
                cl.nombre AS nombre_cliente,
                cl.telefono,
                cl.direccion,
                cu.saldo_actual,
                cu.fecha_creacion AS fecha_cuenta,
                'Abono' AS tipo_movimiento,
                ab.monto,
                ab.fecha AS fecha_movimiento,
                ab.saldo_anterior
            FROM abono ab
            JOIN usuario u ON ab.id_usuario = u.cedula
            JOIN cliente cl ON ab.id_cliente = cl.cedula
            JOIN cuenta cu ON cl.cedula = cu.cedula_cuenta
            WHERE CAST(cl.cedula AS TEXT) ILIKE '%{filtroCedula}%'

            UNION ALL

            SELECT 
                u.cedula AS cedula_usuario,
                u.nombre AS nombre_usuario,
                cl.cedula AS cedula_cliente,
                cl.nombre AS nombre_cliente,
                cl.telefono,
                cl.direccion,
                cu.saldo_actual,
                cu.fecha_creacion AS fecha_cuenta,
                'Cargo' AS tipo_movimiento,
                ca.monto,
                ca.fecha AS fecha_movimiento,
                ca.saldo_anterior
            FROM cargo ca
            JOIN usuario u ON ca.id_usuario = u.cedula
            JOIN cliente cl ON ca.id_cliente = cl.cedula
            JOIN cuenta cu ON cl.cedula = cu.cedula_cuenta
            WHERE CAST(cl.cedula AS TEXT) ILIKE '%{filtroCedula}%'

            ORDER BY nombre_usuario, fecha_movimiento;";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }
        public DataTable ObtenerMovimientosPorUsuario()
        {
            DataTable tabla = new DataTable();

            using (var conn = conexion.ConexionBD())
            {
                string query = @"
            SELECT 
                u.cedula AS cedula_usuario,
                u.nombre AS nombre_usuario,
                cl.cedula AS cedula_cliente,
                cl.nombre AS nombre_cliente,
                cl.telefono,
                cl.direccion,
                cu.saldo_actual,
                cu.fecha_creacion AS fecha_cuenta,
                'Abono' AS tipo_movimiento,
                ab.monto,
                ab.fecha AS fecha_movimiento,
                ab.saldo_anterior
            FROM abono ab
            JOIN usuario u ON ab.id_usuario = u.cedula
            JOIN cliente cl ON ab.id_cliente = cl.cedula
            JOIN cuenta cu ON cl.cedula = cu.cedula_cuenta

            UNION ALL

            SELECT 
                u.cedula AS cedula_usuario,
                u.nombre AS nombre_usuario,
                cl.cedula AS cedula_cliente,
                cl.nombre AS nombre_cliente,
                cl.telefono,
                cl.direccion,
                cu.saldo_actual,
                cu.fecha_creacion AS fecha_cuenta,
                'Cargo' AS tipo_movimiento,
                ca.monto,
                ca.fecha AS fecha_movimiento,
                ca.saldo_anterior
            FROM cargo ca
            JOIN usuario u ON ca.id_usuario = u.cedula
            JOIN cliente cl ON ca.id_cliente = cl.cedula
            JOIN cuenta cu ON cl.cedula = cu.cedula_cuenta

            ORDER BY nombre_usuario, fecha_movimiento;";

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
