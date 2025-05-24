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
    public partial class VistaRPClientes : Form
    {

        ObjUsuario usuarioIngresado;

        Reportes Reportes = new Reportes();
        ExportarExcel excel = new ExportarExcel();
        public VistaRPClientes(ObjUsuario objUsuario)
        {
            usuarioIngresado = objUsuario;
            InitializeComponent();
            this.txtIdCli.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIdCli_KeyPress);
        }
        private void txtIdCli_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir números y teclas de control (como backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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





        public void ColorearFilasPorTipo(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells["tipo_movimiento"].Value != null)
                {
                    string tipo = row.Cells["tipo_movimiento"].Value.ToString();

                    if (tipo.Equals("Abono", StringComparison.OrdinalIgnoreCase))
                    {
                        row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#E8F6EF"); // verde claro
                    }
                    else if (tipo.Equals("Cargo", StringComparison.OrdinalIgnoreCase))
                    {
                        row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FDEDEC"); // rojo claro
                    }
                }
            }
        }

        private void VistaRPClientes_Load(object sender, EventArgs e)
        {
            Reportes.CargarMovimientosEnDGV(dgv);
            EstilizarDGV(dgv);
            ColorearFilasPorTipo(dgv);
        }

        private void txtIdCli_TextChanged(object sender, EventArgs e)
        {
            if (txtIdCli.Text != "")
            {
                Reportes.CargarMovimientosFiltradosEnDGV(dgv, txtIdCli.Text);
                ColorearFilasPorTipo(dgv);
            }
            else
            {
                Reportes.CargarMovimientosEnDGV(dgv);
                ColorearFilasPorTipo(dgv);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                GeneradorReporteBasico generador = new GeneradorReporteBasico(dgv, "Reporte de Deudas");

                string rutaDirectorio = @"C:\Users\migue\PDF";

                if (!System.IO.Directory.Exists(rutaDirectorio))
                {
                    System.IO.Directory.CreateDirectory(rutaDirectorio);
                }

                string rutaGuardado = System.IO.Path.Combine(rutaDirectorio, "reporte_deudas.pdf");

                generador.GenerarPDF(rutaGuardado);  // Primero guardar el archivo

                // Ahora convertir el PDF a un array de bytes desde el archivo guardado
                byte[] pdfBytes;
                try
                {
                    pdfBytes = System.IO.File.ReadAllBytes(rutaGuardado);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al leer el archivo PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.ToString());
                    return; // Importante: Salir si no se puede leer el archivo
                }

                MessageBox.Show($"El reporte se ha guardado en:\n{rutaGuardado}", "Guardado Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EnviarMail.EnviarCorreoConPDFEnMemoria(usuarioIngresado.correo, pdfBytes, "Reporte de sistema", "Reportes por cliente");
                // Eliminar el archivo después de enviarlo (opcional)
                try
                {
                    System.IO.File.Delete(rutaGuardado);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al eliminar el archivo temporal: {ex.Message}");
                    // No mostrar MessageBox para no interrumpir el flujo principal
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Archivo Excel (*.xlsx)|*.xlsx";
                sfd.FileName = "reporte.xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    excel.ExportarDGVaExcel(dgv, sfd.FileName, usuarioIngresado.correo);
                }
            }
        }

    }

}
