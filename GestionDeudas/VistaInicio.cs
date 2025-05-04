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

namespace GestionDeudas
{
    public partial class VistaInicio : Form
    {
        Verificacion verificacion = new Verificacion();
        public VistaInicio()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void VistaInicio_Load(object sender, EventArgs e)
        {
            if (verificacion.VeficacionBaseDatos())
            {
                MessageBox.Show("La base de datos esta lista.");
            }
            else 
            {
                MessageBox.Show("Problemas con la conexion con la base de datos");    
            }
        }
    }
}
