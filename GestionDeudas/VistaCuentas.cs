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
    public partial class VistaCuentas : Form
    {
        Cuenta cuenta = new Cuenta();

        public VistaCuentas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ObjCuenta objCuenta = new ObjCuenta()
                {
                    cedula_cuenta = Convert.ToInt32(txtIdCli.Text),
                    saldo_actual = 0,
                    fecha_creacion = DateTime.Now,
                    estado = true,
                };

                cuenta.insertarCuenta(objCuenta);
            } catch (Exception ex) {
                MessageBox.Show("Error: " + ex);
            }
        }
    }
}
