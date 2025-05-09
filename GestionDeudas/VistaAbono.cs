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
    public partial class VistaAbono : Form
    {
        ObjUsuario usuarioIngresado; 
        Abono abono = new Abono();
        public VistaAbono(ObjUsuario usuario)
        {
            InitializeComponent();
            usuarioIngresado = usuario;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                ObjAbono obj = new ObjAbono()
                {
                    monto = Convert.ToDecimal(txtMonto.Text),
                    id_cliente = Convert.ToInt32(txtIdCli.Text),
                    id_usuario = usuarioIngresado.cedula
                };
                abono.insertarAbono(obj);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }
    }
}
