using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos;
using Objetos;

namespace Negocio
{
    public class Cuenta
    {
        BDCuenta bd = new BDCuenta();
        public void insertarCuenta(ObjCuenta obj)
        {
            bd.InsertCuenta(obj);
        }

        public void mostrarCuentrasPorCedula(string cedula, DataGridView dgv)
        {
            DataTable dt = bd.BuscarCuentasActivasConCliente(cedula);

            dgv.Rows.Clear(); // Limpia filas anteriores si las hay

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgv.Rows.Add(
                    dt.Rows[i]["cuenta"],
                    dt.Rows[i]["nombre"],
                    dt.Rows[i]["telefono"],
                    dt.Rows[i]["direccion"],
                    dt.Rows[i]["saldo"],
                    Convert.ToDateTime(dt.Rows[i]["fecha"]).ToString("yyyy-MM-dd")
                );
            }
        }
        public void mostrarCuentras( DataGridView dgv)
        {
            DataTable dt = bd.ObtenerCuentasActivasConCliente2();

            dgv.Rows.Clear(); // Limpia filas anteriores si las hay

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgv.Rows.Add(
                    dt.Rows[i]["cuenta"],
                    dt.Rows[i]["nombre"],
                    dt.Rows[i]["telefono"],
                    dt.Rows[i]["direccion"],
                    dt.Rows[i]["saldo"],
                    Convert.ToDateTime(dt.Rows[i]["fecha"]).ToString("yyyy-MM-dd")
                );
            }
        }
        public void CargarClientesActivosConCuentaEnDGV(DataGridView dgv)
        {
           
            DataTable tabla = bd.ObtenerClientesActivosConDetalleCuenta();

            dgv.Rows.Clear(); // Limpia filas anteriores

            foreach (DataRow fila in tabla.Rows)
            {
                dgv.Rows.Add(
                    fila["cedula"].ToString(),
                    fila["nombre"].ToString(),
                    fila["telefono"].ToString(),
                    fila["direccion"].ToString(),
                    Convert.ToDateTime(fila["fecha_creacion"]).ToShortDateString(),
                    fila["estado_cuenta"].ToString()
                );
            }
        }
        public void CargarClientesFiltradosEnDGV(DataGridView dgv, string filtro)
        {
          
            DataTable tabla = bd.BuscarClientesActivosConDetalleCuenta(filtro);

            dgv.Rows.Clear(); // Limpia filas anteriores

            foreach (DataRow fila in tabla.Rows)
            {
                dgv.Rows.Add(
                    fila["cedula"].ToString(),
                    fila["nombre"].ToString(),
                    fila["telefono"].ToString(),
                    fila["direccion"].ToString(),
                    Convert.ToDateTime(fila["fecha_creacion"]).ToShortDateString(),
                    fila["estado_cuenta"].ToString()
                );
            }
        }

        public void actilizarEstado(int ced, bool est) {
            bd.ActualizarEstadoCuenta(ced, est);
        }


    }
}
