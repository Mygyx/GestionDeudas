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
    public class Cargo
    {
        BDCargo bd = new BDCargo();

        public void insertarCargo(ObjCargo obj) {
            bd.InsertCargo(obj);
        }
        public void CargarMovimientosEnDGV(DataGridView dgv)
        {
           
            DataTable tabla = bd.ObtenerMovimientosDataTable();

            dgv.Rows.Clear(); // Limpia filas anteriores

            foreach (DataRow fila in tabla.Rows)
            {
                dgv.Rows.Add(
                    fila["tipo_movimiento"].ToString(),
                    fila["id"].ToString(),
                    fila["monto"].ToString(),
                    fila["fecha"].ToString(),
                    fila["descripcion"].ToString(),
                    fila["nombre_cliente"].ToString(),
                    fila["nombre_usuario"].ToString(),
                    fila["saldo_anterior"].ToString()
                );
            }
        }

        public void CargarMovimientosEnDGVParcial(string id, DataGridView dgv)
        {

            DataTable tabla = bd.ObtenerMovimientosDataTable(id);

            dgv.Rows.Clear(); // Limpia filas anteriores

            foreach (DataRow fila in tabla.Rows)
            {
                dgv.Rows.Add(
                    fila["tipo_movimiento"].ToString(),
                    fila["id"].ToString(),
                    fila["monto"].ToString(),
                    fila["fecha"].ToString(),
                    fila["descripcion"].ToString(),
                    fila["nombre_cliente"].ToString(),
                    fila["nombre_usuario"].ToString(),
                    fila["saldo_anterior"].ToString()
                );
            }
        }
    }
}
