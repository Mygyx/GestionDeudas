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
    public partial class VistaUsuario : Form
    {
        Usuario usuario = new Usuario();

        public VistaUsuario()
        {
            InitializeComponent();
            this.txtCed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIdCli_KeyPress);
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
                ObjUsuario objUsuario = new ObjUsuario()
                {
                    cedula = Convert.ToInt32(txtCed.Text),
                    nombre = txtNom.Text,
                    clave = Encriptar.GenerarHash(txtContra.Text),
                    correo = txtCorreo.Text,
                    estado = chbEst.Checked,
                    fecha_creacion = DateTime.Now, 
                };
                usuario.insertUsuario(objUsuario);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            if (txtCed.Text != "")
            {
                usuario.CargarUsuariosPorCedulaEnDGV(dgv, txtCed.Text);
            }
            else
            {
                usuario.CargarTodosLosUsuariosEnDGV(dgv);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ObjUsuario objUsuario = new ObjUsuario()
                {
                    cedula = Convert.ToInt32(txtCed.Text),
                    nombre = txtNom.Text,
                    
                    correo = txtCorreo.Text,
                    estado = chbEst.Checked,
                    
                };
                usuario.ActlizarUsuario(objUsuario);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (txtCed.Text != "")
            {
                usuario.CargarUsuariosPorCedulaEnDGV(dgv, txtCed.Text);
               
            }
            else
            {
                usuario.CargarTodosLosUsuariosEnDGV(dgv);
               
            }
        }

        private void txtIdCli_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir números y teclas de control (como backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCed_TextChanged(object sender, EventArgs e)
        {
            if (txtCed.Text != "")
            {
                usuario.CargarUsuariosPorCedulaEnDGV(dgv, txtCed.Text);
            }
            else {
                usuario.CargarTodosLosUsuariosEnDGV(dgv);
            }
        }

        private void VistaUsuario_Load(object sender, EventArgs e)
        {
            usuario.CargarTodosLosUsuariosEnDGV(dgv);
            EstilizarDGV(dgv);
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
