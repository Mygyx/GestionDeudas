using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Encriptar
    {
        public static string GenerarHash(string dato)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {

                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(dato));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
