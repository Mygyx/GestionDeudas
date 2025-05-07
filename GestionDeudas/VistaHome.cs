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
    public partial class VistaHome : Form
    {
        ObjUsuario usuarioIngresado;
        public VistaHome(ObjUsuario usuarioIngresado)
        {
            InitializeComponent();
            this.usuarioIngresado = usuarioIngresado;
        }

        private void VistaHome_Load(object sender, EventArgs e)
        {

        }
    }
}
