using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos;
using Objetos;

namespace Negocio
{
    public class Empresa
    {
        BDEmpresa bd = new BDEmpresa();

        public void AgregarEmpresa(ObjEmpresa objEmpresa) { 
            bd.InsertEmpresa(objEmpresa);
        }

        public void ModificarEmpresa(ObjEmpresa objEmpresa) { 
            bd.UpdateEmpresa(objEmpresa);   
        }

        public void CargarEmpresasEnDGV(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear(); // Limpia solo las filas

                DataTable dt = bd.GetEmpresas(); // Tu función que retorna el DataTable

                foreach (DataRow row in dt.Rows)
                {
                    dgv.Rows.Add(
                        row["id"],
                        row["nombre"],
                        row["encargado"],
                        (bool)row["estado"] ? "Activo" : "Inactivo",
                        Convert.ToDateTime(row["fecha_creacion"]).ToString("yyyy-MM-dd"),
                        row["telefono"]
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empresas:\n" + ex.Message);
            }
        }


    }
}
