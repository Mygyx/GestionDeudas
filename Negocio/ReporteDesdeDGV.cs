using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Negocio
{
    /// <summary>
    /// Clase para generar reportes PDF mejorados a partir de un DataGridView
    /// </summary>
    public class GeneradorReporteBasico : IDocument
    {
        private readonly DataGridView _dgv;
        private readonly string _titulo;
        private readonly Dictionary<string, float> _anchosColumnas;

        /// <summary>
        /// Constructor para el generador de reportes
        /// </summary>
        /// <param name="dgv">DataGridView con los datos a mostrar</param>
        /// <param name="titulo">Título principal del reporte</param>
        public GeneradorReporteBasico(DataGridView dgv, string titulo = "REPORTE DE DATOS")
        {
            _dgv = dgv;
            _titulo = titulo;
            _anchosColumnas = new Dictionary<string, float>();

            // Calcular anchos relativos basados en el ancho de las columnas del DataGridView
            CalcularAnchosColumnas();
        }

        /// <summary>
        /// Calcula anchos proporcionales para las columnas basados en la configuración del DataGridView
        /// </summary>
        private void CalcularAnchosColumnas()
        {
            float anchoTotal = 0;

            // Solo considerar columnas visibles
            var columnasVisibles = _dgv.Columns.Cast<DataGridViewColumn>()
                                            .Where(col => col.Visible).ToList();

            // Calcular el ancho total de todas las columnas visibles
            foreach (var col in columnasVisibles)
            {
                anchoTotal += col.Width;
            }

            // Calcular proporción para cada columna
            foreach (var col in columnasVisibles)
            {
                float proporcion = anchoTotal > 0 ? (float)col.Width / anchoTotal : 1f / columnasVisibles.Count;
                _anchosColumnas[col.HeaderText] = proporcion;
            }
        }

        /// <summary>
        /// Genera el PDF y lo guarda en la ruta especificada
        /// </summary>
        /// <param name="rutaDestino">Ruta completa donde se guardará el PDF</param>
        public void GenerarPDF(string rutaDestino)
        {
            try
            {
                // Configurar licencia si es necesario
                // QuestPDF.Settings.License = LicenseType.Community;

                Document.Create(container => Compose(container))
                    .GeneratePdf(rutaDestino);

                MessageBox.Show($"✅ PDF generado correctamente en:\n{rutaDestino}",
                                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error al generar PDF:\n{ex.Message}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DocumentMetadata GetMetadata() => new DocumentMetadata
        {
            Title = _titulo,
            Author = "Sistema de Gestión",
            Subject = "Reporte de Datos Financieros",
            Keywords = "reporte, datos, sistema",
            CreationDate = DateTime.Now
        };

        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4.Landscape()); // Usar orientación horizontal para más espacio
                page.Margin(30);
                page.DefaultTextStyle(x => x.FontSize(9));

                // Encabezado
                page.Header().Element(ComposeHeader);

                // Contenido principal
                page.Content().Element(ComposeContent);

                // Pie de página
                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("Página ").FontSize(9);
                    text.CurrentPageNumber().FontSize(9);
                    text.Span(" de ").FontSize(9);
                    text.TotalPages().FontSize(9);
                    text.Span($" - Generado el {DateTime.Now:dd/MM/yyyy HH:mm:ss}").FontSize(8).FontColor(Colors.Grey.Medium);
                });
            });
        }

        private void ComposeHeader(IContainer container)
        {
            container.Column(column =>
            {
                // Título principal
                column.Item().Text(_titulo)
                    .FontSize(20)
                    .Bold()
                    .FontColor(Colors.Blue.Darken2);

                // Fecha del reporte
                column.Item().Text($"Fecha del reporte: {DateTime.Now:dd/MM/yyyy HH:mm}")
                    .FontSize(10)
                    .Italic();

                // Línea separadora
                column.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Grey.Medium);

                // Resumen de datos
                column.Item().PaddingTop(5).Element(ComposeResumen);

                // Espacio antes de la tabla
                column.Item().PaddingTop(10);
            });
        }

        private void ComposeResumen(IContainer container)
        {
            if (_dgv.Rows.Count == 0) return;

            try
            {
                // Cálculo de indicadores
                decimal totalAbonos = 0;
                decimal totalCargos = 0;
                HashSet<string> clientes = new HashSet<string>();

                foreach (DataGridViewRow row in _dgv.Rows)
                {
                    if (row.IsNewRow) continue;

                    string cedula = ObtenerValorCelda(row, "Cédula");
                    if (!string.IsNullOrEmpty(cedula))
                        clientes.Add(cedula);

                    string tipoMov = ObtenerValorCelda(row, "Tipo Movimiento");
                    string montoStr = ObtenerValorCelda(row, "Monto");

                    if (decimal.TryParse(montoStr, out decimal monto))
                    {
                        if (tipoMov.Equals("Abono", StringComparison.OrdinalIgnoreCase))
                            totalAbonos += monto;
                        else if (tipoMov.Equals("Cargo", StringComparison.OrdinalIgnoreCase))
                            totalCargos += monto;
                    }
                }

                // Definimos un helper para crear las columnas
                Action<QuestPDF.Fluent.TableColumnsDefinitionDescriptor> definirColumnas = columnas =>
                {
                    columnas.ConstantColumn(100);
                    columnas.ConstantColumn(200);
                    columnas.ConstantColumn(200);
                };

                // Muestra el resumen en formato tabla simple
                container.Border(1).BorderColor(Colors.Grey.Medium).Padding(0).Table(table =>
                {
                    // Usamos ConstantColumn en lugar de RelativeColumn para evitar problemas de tipo
                    table.ColumnsDefinition(definirColumnas);

                    // Encabezado del resumen
                    table.Cell().ColumnSpan(3).Background(Colors.Grey.Lighten3)
                        .Padding(5).AlignCenter().Text("RESUMEN DE DATOS").Bold();

                    // Datos del resumen - Primera fila
                    table.Cell().Background(Colors.Grey.Lighten4).Border(1).BorderColor(Colors.Grey.Lighten2)
                        .Padding(5).AlignCenter().Text($"Clientes: {clientes.Count}").Bold();

                    table.Cell().Background(Colors.Grey.Lighten5).Border(1).BorderColor(Colors.Grey.Lighten2)
                        .Padding(5).AlignCenter().Text($"Total Abonos: ${totalAbonos:N0}")
                        .FontColor(Colors.Green.Medium).Bold();

                    table.Cell().Background(Colors.Grey.Lighten5).Border(1).BorderColor(Colors.Grey.Lighten2)
                        .Padding(5).AlignCenter().Text($"Total Cargos: ${totalCargos:N0}")
                        .FontColor(Colors.Red.Medium).Bold();
                });
            }
            catch (Exception ex)
            {
                container.Text($"No se pudo generar el resumen de datos: {ex.Message}")
                    .FontColor(Colors.Grey.Medium);
            }
        }

        private void ComposeContent(IContainer container)
        {
            // Obtener columnas visibles
            var columnasVisibles = _dgv.Columns.Cast<DataGridViewColumn>()
                                            .Where(col => col.Visible).ToList();

            if (columnasVisibles.Count == 0 || _dgv.Rows.Count == 0)
            {
                container.PaddingVertical(20)
                    .AlignCenter()
                    .Text("No hay datos para mostrar en el reporte.")
                    .FontSize(12)
                    .FontColor(Colors.Grey.Medium);
                return;
            }

            container.Table(table =>
            {
                // Definición de columnas con anchos relativos y algunos constantes
                table.ColumnsDefinition(columns =>
                {
                    foreach (var col in columnasVisibles)
                    {
                        switch (col.HeaderText)
                        {
                            case "Cédula":
                                columns.ConstantColumn(60);
                                break;
                            case "Nombre":
                                columns.RelativeColumn(); // Ancho relativo
                                break;
                            case "Dirección":
                                columns.RelativeColumn(2); // Ancho relativo, ocupa el doble del espacio
                                break;
                            case "Monto":
                            case "Saldo Cuenta":
                            case "Saldo Anterior":
                                columns.ConstantColumn(80);
                                break;
                            case "Fecha Movimiento":
                            case "Fecha Cuenta":
                                columns.ConstantColumn(100);
                                break;
                            default:
                                columns.RelativeColumn(); // Ancho relativo por defecto
                                break;
                        }
                    }
                });

                // Encabezados de tabla
                foreach (var col in columnasVisibles)
                {
                    table.Cell().Background(Colors.Blue.Darken2)
                        .Border(1).BorderColor(Colors.Grey.Lighten3)
                        .Padding(5)
                        .AlignCenter()
                        .Text(col.HeaderText)
                        .FontColor(Colors.White)
                        .Bold();
                }

                // Filas de datos
                bool filaPar = false;
                string ultimaCedula = null;
                int contadorFilas = 0;

                foreach (DataGridViewRow row in _dgv.Rows)
                {
                    if (row.IsNewRow) continue;
                    contadorFilas++;

                    // Alternamos color según cliente
                    string cedulaActual = ObtenerValorCelda(row, "Cédula");
                    if (ultimaCedula != cedulaActual)
                    {
                        filaPar = !filaPar;
                        ultimaCedula = cedulaActual;
                    }

                    // Color de fondo para la fila
                    var colorFondo = filaPar ? Colors.Grey.Lighten5 : Colors.White;

                    // Celdas de la fila
                    foreach (var col in columnasVisibles)
                    {
                        string valorCelda = row.Cells[col.Index].Value?.ToString() ?? "";
                        string nombreColumna = col.HeaderText;

                        // Configuración básica de la celda
                        var celda = table.Cell()
                            .Border(1)
                            .BorderColor(Colors.Grey.Lighten3)
                            .Background(colorFondo)
                            .Padding(4);

                        // Aplicamos formato según el tipo de columna
                        if (nombreColumna == "Tipo Movimiento")
                        {
                            bool esAbono = valorCelda.Equals("Abono", StringComparison.OrdinalIgnoreCase);
                            celda.Text(valorCelda)
                                .FontColor(esAbono ? Colors.Green.Medium : Colors.Red.Medium)
                                .Bold();
                        }
                        else if (nombreColumna == "Monto" || nombreColumna == "Saldo Cuenta" ||
                                 nombreColumna == "Saldo Anterior")
                        {
                            if (decimal.TryParse(valorCelda, out decimal monto))
                                celda.AlignRight().Text($"{monto:N0}");
                            else
                                celda.Text(valorCelda);
                        }
                        else if (nombreColumna.Contains("Fecha"))
                        {
                            celda.AlignCenter().Text(valorCelda);
                        }
                        else if (nombreColumna == "Cédula")
                        {
                            celda.AlignCenter().Text(valorCelda);
                        }
                        else
                        {
                            celda.Text(valorCelda);
                        }
                    }
                }

                // Si no hay datos, mostrar mensaje
                if (contadorFilas == 0)
                {
                    table.Cell().ColumnSpan((uint)columnasVisibles.Count)
                        .AlignCenter()
                        .Padding(10)
                        .Text("No hay datos disponibles para mostrar.")
                        .FontColor(Colors.Grey.Medium);
                }
            });
        }

        private string ObtenerValorCelda(DataGridViewRow row, string nombreColumna)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.OwningColumn.HeaderText == nombreColumna)
                    return cell.Value?.ToString() ?? "";
            }
            return "";
        }
    }
}