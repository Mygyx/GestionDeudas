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

        public void mostrarCuentrasPorCedula(int cedula, DataGridView dgv)
        {
            DataTable dt = bd.BuscarCuentasActivasPorCedulaParcial(cedula);

            dgv.Rows.Clear(); // Limpia filas anteriores si las hay

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgv.Rows.Add(
                    dt.Rows[i]["cedula_cuenta"],
                    dt.Rows[i]["nombre"],
                    dt.Rows[i]["telefono"],
                    dt.Rows[i]["direccion"],
                    dt.Rows[i]["saldo_actual"],
                    Convert.ToDateTime(dt.Rows[i]["fecha_creacion"]).ToString("yyyy-MM-dd")
                );
            }
        }

    }
}
