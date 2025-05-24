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
                cl.cedula,
                cl.nombre,
                cl.telefono,
                cl.direccion,
                cl.fecha_creacion,
                CASE 
                    WHEN cu.cedula_cuenta IS NULL THEN 'Sin cuenta'
                    WHEN cu.estado = true THEN 'Cuenta activa'
                    ELSE 'Cuenta inactiva'
                END AS estado_cuenta
            FROM cliente cl
            LEFT JOIN cuenta cu ON cl.cedula = cu.cedula_cuenta
            WHERE cl.estado = true
              AND (
                  CAST(cl.cedula AS TEXT) ILIKE '%" + cedulaParcial + @"%' OR
                  cl.nombre ILIKE '%" + cedulaParcial + @"%'
              );";
            using (NpgsqlConnection conn = conexion.ConexionBD())
            {
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }
        public DataTable BuscarCuentasActivas()
        {
            DataTable tabla = new DataTable();

            string query = @"
            SELECT 
                cl.cedula,
                cl.nombre,
                cl.telefono,
                cl.direccion,
                cl.fecha_creacion,
                CASE 
                    WHEN cu.cedula_cuenta IS NULL THEN 'Sin cuenta'
                    WHEN cu.estado = true THEN 'Cuenta activa'
                    ELSE 'Cuenta inactiva'
                END AS estado_cuenta
            FROM cliente cl
            LEFT JOIN cuenta cu ON cl.cedula = cu.cedula_cuenta
            WHERE cl.estado = true;";


            using (NpgsqlConnection conn = conexion.ConexionBD())
            {
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }
        public DataTable ObtenerClientesActivosConDetalleCuenta()
        {
            DataTable tabla = new DataTable();

            using (var conn = conexion.ConexionBD())
            {
                string query = @"
            SELECT 
                cl.cedula,
                cl.nombre,
                cl.telefono,
                cl.direccion,
                cl.fecha_creacion,
                CASE 
                    WHEN cu.cedula_cuenta IS NULL THEN 'Sin cuenta'
                    WHEN cu.estado = true THEN 'Cuenta activa'
                    ELSE 'Cuenta inactiva'
                END AS estado_cuenta
            FROM cliente cl
            LEFT JOIN cuenta cu ON cl.cedula = cu.cedula_cuenta
            WHERE cl.estado = true;";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }


        public DataTable BuscarClientesActivosConDetalleCuenta(string filtro)
        {
            DataTable tabla = new DataTable();

            using (var conn = conexion.ConexionBD())
            {
                string query = @"
            SELECT 
                cl.cedula,
                cl.nombre,
                cl.telefono,
                cl.direccion,
                cl.fecha_creacion,
                CASE 
                    WHEN cu.cedula_cuenta IS NULL THEN 'Sin cuenta'
                    WHEN cu.estado = true THEN 'Cuenta activa'
                    ELSE 'Cuenta inactiva'
                END AS estado_cuenta
            FROM cliente cl
            LEFT JOIN cuenta cu ON cl.cedula = cu.cedula_cuenta
            WHERE cl.estado = true
              AND (
                  CAST(cl.cedula AS TEXT) ILIKE '%" + filtro + @"%' OR
                  cl.nombre ILIKE '%" + filtro + @"%'
              );";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }

        public void ActualizarEstadoCuenta(int cedulaCuenta, bool nuevoEstado)
        {
            using (var conn = conexion.ConexionBD())
            {
                string query = $@"
            UPDATE cuenta
            SET estado = {nuevoEstado}
            WHERE cedula_cuenta = {cedulaCuenta};";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public DataTable ObtenerCuentasActivasConCliente()
        {
            DataTable tabla = new DataTable();

            using (var conn = conexion.ConexionBD())
            {
                string query = @"
            SELECT 
                cu.cedula_cuenta AS cuenta,
                cl.nombre,
                cl.telefono,
                cl.direccion,
                cu.saldo_actual AS saldo,
                cu.fecha_creacion AS fecha
            FROM cuenta cu
            JOIN cliente cl ON cu.cedula_cuenta = cl.cedula
            WHERE cu.estado = true;";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }
        public DataTable ObtenerCuentasActivasConCliente2()
        {
            DataTable tabla = new DataTable();

            using (var conn = conexion.ConexionBD())
            {
                string query = @"
            SELECT 
                cu.cedula_cuenta AS cuenta,
                cl.nombre,
                cl.telefono,
                cl.direccion,
                cu.saldo_actual AS saldo,
                cu.fecha_creacion AS fecha
            FROM cuenta cu
            JOIN cliente cl ON cu.cedula_cuenta = cl.cedula
            WHERE cu.estado = true;";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }
        public DataTable BuscarCuentasActivasConCliente(string filtro)
        {
            DataTable tabla = new DataTable();

            using (var conn = conexion.ConexionBD())
            {
                string query = $@"
            SELECT 
                cu.cedula_cuenta AS cuenta,
                cl.nombre,
                cl.telefono,
                cl.direccion,
                cu.saldo_actual AS saldo,
                cu.fecha_creacion AS fecha
            FROM cuenta cu
            JOIN cliente cl ON cu.cedula_cuenta = cl.cedula
            WHERE cu.estado = true
              AND (
                  CAST(cu.cedula_cuenta AS TEXT) ILIKE '%{filtro}%' OR
                  cl.nombre ILIKE '%{filtro}%'
              );";

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
