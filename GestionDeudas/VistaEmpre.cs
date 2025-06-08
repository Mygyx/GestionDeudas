using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Objetos;

namespace GestionDeudas
{
    public partial class VistaEmpre : Form
    {
        Empresa empresa = new Empresa();
        Cliente cliente = new Cliente();
        public VistaEmpre()
        {
            InitializeComponent();
        }
        public void EstilizarDGV(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // ← Ocupa todo el ancho sin salirse
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;

            // Colores y estilo general
            dgv.BackgroundColor = ColorTranslator.FromHtml("#EBF5FB");
            dgv.BorderStyle = BorderStyle.None;
            dgv.GridColor = Color.FromArgb(220, 220, 220);

            // Cabecera
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 32;

            // Celdas
            dgv.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#EBF5FB");
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(33, 33, 33);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(214, 234, 248);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            dgv.RowTemplate.Height = 30;
        }

        private void VistaEmpre_Load(object sender, EventArgs e)
        {
            EstilizarDGV(dgvCli);
            EstilizarDGV(dgvEmp);
            empresa.CargarEmpresasEnDGV(dgvEmp);
            cliente.CargarClientesEnDGV(dgvCli);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                ObjEmpresa objEmpresa = new ObjEmpresa()
                {
                    nombre = txtNombre.Text,
                    encargado = txtEnc.Text,
                    estado = chxEst.Checked,
                    telefono = txtTel.Text,
                };

                empresa.AgregarEmpresa(objEmpresa);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            empresa.CargarEmpresasEnDGV(dgvEmp);
          
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvEmp.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvEmp.SelectedRows[0]; // Primera fila seleccionada

                try
                {
                    ObjEmpresa objEmpresa = new ObjEmpresa()
                    {
                        id = Convert.ToInt32(fila.Cells[0].Value),
                        nombre = txtNombre.Text,
                        encargado = txtEnc.Text,
                        estado = chxEst.Checked,
                        telefono = txtTel.Text,

                    };

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila primero.");
            }
            empresa.CargarEmpresasEnDGV(dgvEmp);
            cliente.CargarClientesEnDGV(dgvCli);
        }

        private void dgvCli_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvEmp.SelectedRows.Count > 0 && dgvCli.SelectedRows.Count > 0)
            {
                DataGridViewRow filaEmp = dgvEmp.SelectedRows[0]; // Primera fila seleccionada
                DataGridViewRow filaCLi = dgvCli.SelectedRows[0];

                int idEmpre = Convert.ToInt32(filaEmp.Cells[0].Value);
                int cedCli = Convert.ToInt32(filaCLi.Cells[0].Value);

                cliente.ActulizarClienteDeEmpresa(cedCli, idEmpre);

            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila primero.");
            }
            empresa.CargarEmpresasEnDGV(dgvEmp);
            cliente.CargarClientesEnDGV(dgvCli);
        }
    }
}
