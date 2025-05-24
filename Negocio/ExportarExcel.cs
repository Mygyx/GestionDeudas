using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.Windows.Forms;
using System.IO;

namespace Negocio
{
    public class ExportarExcel
    {

        public void ExportarDGVaExcel(DataGridView dgv, string rutaArchivo, string correoDestino)
        {
            using (var workbook = new ClosedXML.Excel.XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reporte");

                // Agrega encabezados
                int colIndex = 1;
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.Visible)
                    {
                        worksheet.Cell(1, colIndex).Value = col.HeaderText;
                        colIndex++;
                    }
                }

                // Agrega datos fila por fila
                int rowIndex = 2;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue;

                    int currentCol = 1;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (dgv.Columns[cell.ColumnIndex].Visible)
                        {
                            worksheet.Cell(rowIndex, currentCol).Value = cell.Value?.ToString() ?? "";
                            currentCol++;
                        }
                    }

                    rowIndex++;
                }

                // Guarda el archivo en disco
                workbook.SaveAs(rutaArchivo);

                // Lo carga en memoria para enviarlo por correo
                using (var memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream); // Guardar en memoria
                    byte[] excelBytes = memoryStream.ToArray(); // Convertir a byte[]

                    // Enviar por correo
                    string asunto = "📊 Reporte Excel del sistema";
                    string cuerpo = "Estimado usuario,\n\nAdjunto encontrará el reporte generado en formato Excel.";

                    EnviarMail.EnviarCorreoConExcelEnMemoria(correoDestino, excelBytes, asunto, cuerpo);
                }

                MessageBox.Show("✅ Excel exportado y enviado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
