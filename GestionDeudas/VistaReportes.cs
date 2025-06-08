using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Objetos;

namespace GestionDeudas
{
    public partial class VistaReportes : Form
    {
        ObjUsuario usuario;

        public VistaReportes(ObjUsuario objUsuario)
        {
            usuario = objUsuario;
            InitializeComponent();
        }

        private void AbrirFormularioEnPanel(Form formulario, Panel panelContenedor)
        {
            panelContenedor.Controls.Clear();              // Limpia el panel
            formulario.TopLevel = false;                   // Indica que no es un formulario de nivel superior
            formulario.FormBorderStyle = FormBorderStyle.None; // Quita bordes
            formulario.Dock = DockStyle.Fill;              // Ocupa todo el panel
            panelContenedor.Controls.Add(formulario);      // Agrega al panel
            formulario.Show();                             // Muestra el formulario
        }
        private void VistaReportes_Load(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new VistaRPClientes(usuario), pnl);
        }

        private void rpCli_CheckedChanged(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new VistaRPClientes(usuario), pnl);
        }

        private void rpFecha_CheckedChanged(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new VistaRPFecha(usuario), pnl);
        }

        private void prMon_CheckedChanged(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new VistaRPMonto(usuario), pnl);
        }

        private void rpUsu_CheckedChanged(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new VistaRPUsuario(usuario), pnl);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new VistaRPEmpresas(usuario), pnl);
        }
    }
}
