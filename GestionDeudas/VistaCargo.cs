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
    public partial class VistaCargo : Form
    {
        ObjUsuario usuarioIngresado;
        Cargo cargo = new Cargo();
        Cuenta Cuenta = new Cuenta();
        public VistaCargo(ObjUsuario obj)
        {
            InitializeComponent();
            usuarioIngresado = obj;
        }
        public void ColorearFilasPorTipo(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    string tipo = row.Cells[0].Value.ToString();

                    if (tipo.Equals("Abono", StringComparison.OrdinalIgnoreCase))
                    {
                        row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#E8F6EF"); // verde claro
                    }
                    else if (tipo.Equals("Cargo", StringComparison.OrdinalIgnoreCase))
                    {
                        row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FDEDEC"); // rojo claro
                    }
                }
            }
        }

        private void VistaCargo_Load(object sender, EventArgs e)
        {
            EstilizarDGV(dgv);
            EstilizarDGV(dgvMov);
            Cuenta.mostrarCuentras(dgv);
            cargo.CargarMovimientosEnDGV(dgvMov);
            ColorearFilasPorTipo(dgvMov);

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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ObjCargo obj = new ObjCargo()
                {
                    monto = Convert.ToDecimal(txtMonto.Text),
                    descripcion = txtDir.Text,
                    id_cliente = Convert.ToInt32(txtIdCli.Text),
                    id_usuario = usuarioIngresado.cedula
                };


                cargo.insertarCargo(obj);
            }
            catch (Exception ex) { 
                MessageBox.Show(ex.Message);
            }
            if (txtIdCli.Text != "")
            {
                Cuenta.mostrarCuentrasPorCedula(txtIdCli.Text, dgv);
                cargo.CargarMovimientosEnDGVParcial(txtIdCli.Text, dgvMov);
                ColorearFilasPorTipo(dgvMov);
            }
            else
            {
                Cuenta.mostrarCuentras(dgv);
                cargo.CargarMovimientosEnDGV(dgvMov);
                ColorearFilasPorTipo(dgvMov);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtDir_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtMonto_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtIdCli_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (!System.Text.RegularExpressions.Regex.IsMatch(txt.Text, @"^\d*$"))
            {
                // Si contiene algo que no es un dígito, lo eliminamos
                int pos = txt.SelectionStart - 1;
                txt.Text = new string(txt.Text.Where(char.IsDigit).ToArray());
                txt.SelectionStart = Math.Max(pos, 0);

            }
            if (txtIdCli.Text != "")
            {
                Cuenta.mostrarCuentrasPorCedula(txtIdCli.Text, dgv);
                cargo.CargarMovimientosEnDGVParcial(txtIdCli.Text,dgvMov);
            }
            else {
                Cuenta.mostrarCuentras(dgv);
                cargo.CargarMovimientosEnDGV(dgvMov);
            }
           
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
