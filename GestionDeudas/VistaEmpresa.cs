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
    public partial class VistaEmpresa : Form
    {
        Empresa empresa = new Empresa();
        Cliente cliente = new Cliente();    

        public VistaEmpresa()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                ObjEmpresa obj = new ObjEmpresa() {
                    nombre = txtNom.Text,
                    encargado = txtEnc.Text,
                    estado = chbEst.Checked,
                    telefono = txtTel.Text,
                };
                
                empresa.AgregarEmpresa(obj);    
                
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ObjEmpresa obj = new ObjEmpresa()
                {
                    nombre = txtNom.Text,
                    encargado = txtEnc.Text,
                    estado = chbEst.Checked,
                    telefono = txtTel.Text,
                };

                empresa.ModificarEmpresa(obj);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow rowEmpresa = dgvEmpreza.CurrentRow;
                DataGridViewRow rowCliente = dgvCliente.CurrentRow;
                int cedulaCliente = Convert.ToInt32(rowCliente.Cells[0].Value);
                int idEmpresa = Convert.ToInt32(rowEmpresa.Cells[0].Value);

                cliente.ActulizarEmpresa(
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
