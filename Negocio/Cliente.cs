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
    public class Cliente
    {
        BDCliente bd = new BDCliente();
        public void insetarCliente(ObjCliente obj) {
            bd.InsertCliente(obj);
        }

        public void ActulizarCliente(ObjCliente obj) { 
            bd.UpdateCliente(obj);
        }

        public void ActulizarClienteDeEmpresa(int cedulaCliente, int idEmpre) { 
            bd.ActualizarEmpresaCliente(cedulaCliente, idEmpre);
        }

        public void CargarClientesEnDGV(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear(); // Limpia las filas actuales

                DataTable dt = bd.GetClientesConEmpresa(); // Usa la función JOIN

                foreach (DataRow row in dt.Rows)
                {
                    dgv.Rows.Add(
                        row["cedula"],
                        row["nombre"],
                        row["telefono"],
                        row["direccion"],
                        (bool)row["estado"] ? "Activo" : "Inactivo",
                        Convert.ToDateTime(row["fecha_creacion"]).ToString("yyyy-MM-dd"),
                        row["nombre_empresa"]
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes en el DGV:\n" + ex.Message);
            }
        }


    }
}
