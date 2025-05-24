using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;

namespace Negocio
{
    public class EnviarMail
    {
        private static readonly string FromEmail = "mageofozinearth@gmail.com";
        private static readonly string SmtpHost = "smtp.gmail.com";
        private static readonly int SmtpPort = 587;
        private static readonly string SmtpPassword = "ydjw ugqb bzcc gsxv";

        /// <summary>
        /// Envía un correo electrónico con un archivo PDF adjunto desde un array de bytes.
        /// </summary>
        /// <param name="correoDestino">La dirección de correo electrónico del destinatario.</param>
        /// <param name="pdfBytes">Un array de bytes que representa el contenido del archivo PDF.</param>
        /// <param name="asunto">El asunto del correo electrónico.</param>
        /// <param name="cuerpo">El cuerpo del correo electrónico.</param>
        public static void EnviarCorreoConPDFEnMemoria(string correoDestino, byte[] pdfBytes, string asunto, string cuerpo)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            mail.From = new MailAddress(FromEmail);
            mail.To.Add(correoDestino);
            mail.Subject = asunto;
            mail.Body = cuerpo;
            mail.IsBodyHtml = false;

            // Convertir el byte[] en un adjunto desde memoria
            MemoryStream adjuntoStream = new MemoryStream(pdfBytes);
            Attachment adjunto = new Attachment(adjuntoStream, "reporte.pdf", "application/pdf");
            mail.Attachments.Add(adjunto);

            // Configuración SMTP
            smtpClient.Host = SmtpHost;
            smtpClient.Port = SmtpPort;
            smtpClient.Credentials = new NetworkCredential(FromEmail, SmtpPassword);
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mail);
                MessageBox.Show("✅ Correo enviado correctamente con el PDF adjunto.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al enviar el correo: " + ex.Message);
            }
            finally
            {
                adjuntoStream.Dispose();
                mail.Dispose();
            }
        }

        /// <summary>
        /// Envía un correo electrónico con un PDF adjunto.  El PDF se pasa como parámetro.
        /// </summary>
        /// <param name="mail">Correo del Destinatario</param>
        /// <param name="pdfBytes">El PDF a enviar, en forma de array de bytes.</param>
        public static void EnviarMailYPdf(string mail, byte[] pdfBytes)
        {
            string asunto = "📄 Reporte del sistema - Reporte de Super la Familia";
            string cuerpo = "Estimado usuario,\n\nSuper la Familia se complace en enviarle su reporte de actividad. En adjunto encontrará un resumen detallado en formato PDF.";
            EnviarMail.EnviarCorreoConPDFEnMemoria(mail, pdfBytes, asunto, cuerpo);
        }

        public static void EnviarCorreoConExcelEnMemoria(string correoDestino, byte[] excelBytes, string asunto, string cuerpo)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            mail.From = new MailAddress(FromEmail);
            mail.To.Add(correoDestino);
            mail.Subject = asunto;
            mail.Body = cuerpo;
            mail.IsBodyHtml = false;

            // Convertir el byte[] en un adjunto desde memoria para el archivo Excel
            MemoryStream adjuntoStream = new MemoryStream(excelBytes);
            Attachment adjunto = new Attachment(adjuntoStream, "reporte.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            mail.Attachments.Add(adjunto);

            // Configuración SMTP
            smtpClient.Host = SmtpHost;
            smtpClient.Port = SmtpPort;
            smtpClient.Credentials = new NetworkCredential(FromEmail, SmtpPassword);
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mail);
                MessageBox.Show("✅ Correo enviado correctamente con el archivo Excel adjunto.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al enviar el correo: " + ex.Message);
            }
            finally
            {
                adjuntoStream.Dispose();
                mail.Dispose();
            }
        }

    }
}
