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
    public partial class VistaInicio : Form
    {
        Verificacion verificacion = new Verificacion();
        Usuario usuario = new Usuario();
       
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
                MessageBox.Show("L.a base de datos esta lista.");
            }
            else 
            {
                MessageBox.Show("Problemas con la conexion con la base de datos");    
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ObjUsuario usuarioIngresado = GetUsuario();
            if (usuarioIngresado == null) {
                MessageBox.Show("Este usuarion no esta registrado en el sistema\nRevise las redenciales de los campos pedidos");
            }
            else
            {
                VistaHome vistaHome = new VistaHome(usuarioIngresado);
                vistaHome.Show();
                this.Hide();
            }
        }
        public ObjUsuario GetUsuario() {
            try
            {
                int cedula = Convert.ToInt32(txtUsu.Text);
                string contra = Encriptar.GenerarHash(txtContra.Text);
                return usuario.GetUsuario(cedula, contra);
            } catch (Exception ex) {
                MessageBox.Show("Error: " + ex);
                return null;
            }
        
        }
    }
}
