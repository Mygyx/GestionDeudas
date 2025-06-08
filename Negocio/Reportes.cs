using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos;

namespace Negocio
{
    public class Reportes
    {
        BDReportes bd = new BDReportes();
        public void CargarMovimientosFiltradosEnDGV(DataGridView dgv, string filtroCedula)
        {
            DataTable tabla = bd.ObtenerReporteMovimientosPorCedula(filtroCedula);

            dgv.Columns.Clear();
            dgv.Rows.Clear();

            dgv.Columns.Add("cedula", "Cédula");
            dgv.Columns.Add("cliente_nombre", "Nombre");
            dgv.Columns.Add("telefono", "Teléfono");
            dgv.Columns.Add("direccion", "Dirección");
            dgv.Columns.Add("saldo_actual", "Saldo Cuenta");
            dgv.Columns.Add("fecha_cuenta", "Fecha Cuenta");
            dgv.Columns.Add("tipo_movimiento", "Tipo Movimiento");
            dgv.Columns.Add("monto", "Monto");
            dgv.Columns.Add("fecha_movimiento", "Fecha Movimiento");
            dgv.Columns.Add("saldo_anterior", "Saldo Anterior");
            dgv.Columns.Add("usuario_responsable", "Usuario");

            foreach (DataRow fila in tabla.Rows)
            {
                dgv.Rows.Add(
                    fila["cedula"].ToString(),
                    fila["cliente_nombre"].ToString(),
                    fila["telefono"].ToString(),
                    fila["direccion"].ToString(),
                    fila["saldo_actual"].ToString(),
                    Convert.ToDateTime(fila["fecha_cuenta"]).ToShortDateString(),
                    fila["tipo_movimiento"].ToString(),
                    fila["monto"].ToString(),
                    Convert.ToDateTime(fila["fecha_movimiento"]).ToString("g"),
                    fila["saldo_anterior"].ToString(),
                    fila["usuario_responsable"].ToString()
                );
            }
        }

        public void CargarMovimientosEnDGV(DataGridView dgv)
        {
            DataTable tabla = bd.ObtenerReporteMovimientos();

            dgv.Columns.Clear();
            dgv.Rows.Clear();

            dgv.Columns.Add("cedula", "Cédula");
            dgv.Columns.Add("cliente_nombre", "Nombre");
            dgv.Columns.Add("telefono", "Teléfono");
            dgv.Columns.Add("direccion", "Dirección");
            dgv.Columns.Add("saldo_actual", "Saldo Cuenta");
            dgv.Columns.Add("fecha_cuenta", "Fecha Cuenta");
            dgv.Columns.Add("tipo_movimiento", "Tipo Movimiento");
            dgv.Columns.Add("monto", "Monto");
            dgv.Columns.Add("fecha_movimiento", "Fecha Movimiento");
            dgv.Columns.Add("saldo_anterior", "Saldo Anterior");
            dgv.Columns.Add("usuario_responsable", "Usuario");

            foreach (DataRow fila in tabla.Rows)
            {
                dgv.Rows.Add(
                    fila["cedula"].ToString(),
                    fila["cliente_nombre"].ToString(),
                    fila["telefono"].ToString(),
                    fila["direccion"].ToString(),
                    fila["saldo_actual"].ToString(),
                    Convert.ToDateTime(fila["fecha_cuenta"]).ToShortDateString(),
                    fila["tipo_movimiento"].ToString(),
                    fila["monto"].ToString(),
                    Convert.ToDateTime(fila["fecha_movimiento"]).ToString("g"),
                    fila["saldo_anterior"].ToString(),
                    fila["usuario_responsable"].ToString()
                );
            }
        }


        public void CargarMovimientosPorFechasEnDGV(DataGridView dgv, DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable tabla = bd.ObtenerReporteMovimientosPorFechas(fechaInicio, fechaFin);

            dgv.Columns.Clear();
            dgv.Rows.Clear();

            dgv.Columns.Add("cedula", "Cédula");
            dgv.Columns.Add("cliente_nombre", "Nombre");
            dgv.Columns.Add("telefono", "Teléfono");
            dgv.Columns.Add("direccion", "Dirección");
            dgv.Columns.Add("saldo_actual", "Saldo Cuenta");
            dgv.Columns.Add("fecha_cuenta", "Fecha Cuenta");
            dgv.Columns.Add("tipo_movimiento", "Tipo Movimiento");
            dgv.Columns.Add("monto", "Monto");
            dgv.Columns.Add("fecha_movimiento", "Fecha Movimiento");
            dgv.Columns.Add("saldo_anterior", "Saldo Anterior");
            dgv.Columns.Add("usuario_responsable", "Usuario");

            foreach (DataRow fila in tabla.Rows)
            {
                dgv.Rows.Add(
                    fila["cedula"].ToString(),
                    fila["cliente_nombre"].ToString(),
                    fila["telefono"].ToString(),
                    fila["direccion"].ToString(),
                    fila["saldo_actual"].ToString(),
                    Convert.ToDateTime(fila["fecha_cuenta"]).ToShortDateString(),
                    fila["tipo_movimiento"].ToString(),
                    fila["monto"].ToString(),
                    Convert.ToDateTime(fila["fecha_movimiento"]).ToString("g"),
                    fila["saldo_anterior"].ToString(),
                    fila["usuario_responsable"].ToString()
                );
            }
        }
        public void CargarMovimientosPorMontoEnDGV(DataGridView dgv, decimal montoMin, decimal montoMax)
        {
            DataTable tabla = bd.ObtenerReporteMovimientosPorMonto(montoMin, montoMax);

            dgv.Columns.Clear();
            dgv.Rows.Clear();

            dgv.Columns.Add("cedula", "Cédula");
            dgv.Columns.Add("cliente_nombre", "Nombre");
            dgv.Columns.Add("telefono", "Teléfono");
            dgv.Columns.Add("direccion", "Dirección");
            dgv.Columns.Add("saldo_actual", "Saldo Cuenta");
            dgv.Columns.Add("fecha_cuenta", "Fecha Cuenta");
            dgv.Columns.Add("tipo_movimiento", "Tipo Movimiento");
            dgv.Columns.Add("monto", "Monto");
            dgv.Columns.Add("fecha_movimiento", "Fecha Movimiento");
            dgv.Columns.Add("saldo_anterior", "Saldo Anterior");
            dgv.Columns.Add("usuario_responsable", "Usuario");

            foreach (DataRow fila in tabla.Rows)
            {
                dgv.Rows.Add(
                    fila["cedula"].ToString(),
                    fila["cliente_nombre"].ToString(),
                    fila["telefono"].ToString(),
                    fila["direccion"].ToString(),
                    fila["saldo_actual"].ToString(),
                    Convert.ToDateTime(fila["fecha_cuenta"]).ToShortDateString(),
                    fila["tipo_movimiento"].ToString(),
                    fila["monto"].ToString(),
                    Convert.ToDateTime(fila["fecha_movimiento"]).ToString("g"),
                    fila["saldo_anterior"].ToString(),
                    fila["usuario_responsable"].ToString()
                );
            }
        }
        public void CargarTodosLosMovimientosPorUsuarioEnDGV(DataGridView dgv)
        {
            DataTable tabla = bd.ObtenerMovimientosPorUsuario(); // Llama la función interna

            dgv.Columns.Clear();
            dgv.Rows.Clear();

            dgv.Columns.Add("cedula_usuario", "Cédula Usuario");
            dgv.Columns.Add("nombre_usuario", "Nombre Usuario");
            dgv.Columns.Add("cedula_cliente", "Cédula Cliente");
            dgv.Columns.Add("nombre_cliente", "Nombre Cliente");
            dgv.Columns.Add("telefono", "Teléfono");
            dgv.Columns.Add("direccion", "Dirección");
            dgv.Columns.Add("saldo_actual", "Saldo Cuenta");
            dgv.Columns.Add("fecha_cuenta", "Fecha Cuenta");
            dgv.Columns.Add("tipo_movimiento", "Tipo Movimiento");
            dgv.Columns.Add("monto", "Monto");
            dgv.Columns.Add("fecha_movimiento", "Fecha Movimiento");
            dgv.Columns.Add("saldo_anterior", "Saldo Anterior");

            foreach (DataRow fila in tabla.Rows)
            {
                dgv.Rows.Add(
                    fila["cedula_usuario"].ToString(),
                    fila["nombre_usuario"].ToString(),
                    fila["cedula_cliente"].ToString(),
                    fila["nombre_cliente"].ToString(),
                    fila["telefono"].ToString(),
                    fila["direccion"].ToString(),
                    fila["saldo_actual"].ToString(),
                    Convert.ToDateTime(fila["fecha_cuenta"]).ToShortDateString(),
                    fila["tipo_movimiento"].ToString(),
                    fila["monto"].ToString(),
                    Convert.ToDateTime(fila["fecha_movimiento"]).ToString("g"),
                    fila["saldo_anterior"].ToString()
                );
            }
        }
        public void CargarMovimientosPorUsuarioFiltradoEnDGV(DataGridView dgv, string cedulaParcial)
        {
            DataTable tabla = bd.ObtenerMovimientosPorUsuarioConFiltroCedula(cedulaParcial);

            dgv.Columns.Clear();
            dgv.Rows.Clear();

            dgv.Columns.Add("cedula_usuario", "Cédula Usuario");
            dgv.Columns.Add("nombre_usuario", "Nombre Usuario");
            dgv.Columns.Add("cedula_cliente", "Cédula Cliente");
            dgv.Columns.Add("nombre_cliente", "Nombre Cliente");
            dgv.Columns.Add("telefono", "Teléfono");
            dgv.Columns.Add("direccion", "Dirección");
            dgv.Columns.Add("saldo_actual", "Saldo Cuenta");
            dgv.Columns.Add("fecha_cuenta", "Fecha Cuenta");
            dgv.Columns.Add("tipo_movimiento", "Tipo Movimiento");
            dgv.Columns.Add("monto", "Monto");
            dgv.Columns.Add("fecha_movimiento", "Fecha Movimiento");
            dgv.Columns.Add("saldo_anterior", "Saldo Anterior");

            foreach (DataRow fila in tabla.Rows)
            {
                dgv.Rows.Add(
                    fila["cedula_usuario"].ToString(),
                    fila["nombre_usuario"].ToString(),
                    fila["cedula_cliente"].ToString(),
                    fila["nombre_cliente"].ToString(),
                    fila["telefono"].ToString(),
                    fila["direccion"].ToString(),
                    fila["saldo_actual"].ToString(),
                    Convert.ToDateTime(fila["fecha_cuenta"]).ToShortDateString(),
                    fila["tipo_movimiento"].ToString(),
                    fila["monto"].ToString(),
                    Convert.ToDateTime(fila["fecha_movimiento"]).ToString("g"),
                    fila["saldo_anterior"].ToString()
                );
            }
        }
        public void CargarMovimientosFiltradosEnDGVEmpresas(DataGridView dgv, string filtroCedula)
        {
            DataTable tabla;

            if (!string.IsNullOrWhiteSpace(filtroCedula))
                tabla = bd.ObtenerReporteMovimientosPorCedulaEmpresas(filtroCedula);
            else
                tabla = bd.ObtenerReporteTodosLosMovimientosEmpresas();

            dgv.Columns.Clear();
            dgv.Rows.Clear();

            dgv.Columns.Add("cedula", "Cédula");
            dgv.Columns.Add("cliente_nombre", "Nombre");
            dgv.Columns.Add("telefono", "Teléfono");
            dgv.Columns.Add("saldo_actual", "Saldo Cuenta");
            dgv.Columns.Add("fecha_cuenta", "Fecha Cuenta");
            dgv.Columns.Add("tipo_movimiento", "Tipo Movimiento");
            dgv.Columns.Add("monto", "Monto");
            dgv.Columns.Add("fecha_movimiento", "Fecha Movimiento");
            dgv.Columns.Add("saldo_anterior", "Saldo Anterior");
            dgv.Columns.Add("usuario_responsable", "Usuario");
            dgv.Columns.Add("empresa", "Empresa");

            foreach (DataRow fila in tabla.Rows)
            {
                dgv.Rows.Add(
                    fila["cedula"].ToString(),
                    fila["cliente_nombre"].ToString(),
                    fila["telefono"].ToString(),
                    fila["saldo_actual"].ToString(),
                    Convert.ToDateTime(fila["fecha_cuenta"]).ToShortDateString(),
                    fila["tipo_movimiento"].ToString(),
                    fila["monto"].ToString(),
                    Convert.ToDateTime(fila["fecha_movimiento"]).ToString("g"),
                    fila["saldo_anterior"].ToString(),
                    fila["usuario_responsable"].ToString(),
                    fila["empresa"].ToString()
                );
            }
        }


    }
}
