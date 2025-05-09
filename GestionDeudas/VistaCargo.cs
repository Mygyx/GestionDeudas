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

        private void VistaCargo_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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
            if (txtIdCli.Text != "") {
                Cuenta.mostrarCuentrasPorCedula(Convert.ToInt32(txtIdCli.Text),dgv);
            }
           
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
