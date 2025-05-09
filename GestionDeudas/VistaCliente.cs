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
    public partial class VistaCliente : Form
    {
        Cliente cli = new Cliente();
        public VistaCliente()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ObjCliente cliente = new ObjCliente()
                {
                    cedula = Convert.ToInt32(txtIdCli.Text),
                    nombre = txtNom.Text,
                    telefono = txtTel.Text,
                    direccion = txtDir.Text,
                    estado = chbEstado.Checked,
                    fecha_creacion = DateTime.Now,
                };
                cli.insetarCliente(cliente);
            }
            catch (Exception ex) {
                MessageBox.Show("Error: " + ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ObjCliente cliente = new ObjCliente()
                {
                    cedula = Convert.ToInt32(txtIdCli.Text),
                    nombre = txtNom.Text,
                    telefono = txtTel.Text,
                    direccion = txtDir.Text,
                    estado = chbEstado.Checked,
                    fecha_creacion = DateTime.Now,
                };
                cli.ActulizarCliente(cliente);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }

        }
    }
}
