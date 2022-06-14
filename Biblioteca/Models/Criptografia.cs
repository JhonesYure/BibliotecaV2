using System;
using System.Security.Cryptography;
using System.Text;


namespace Biblioteca.Models
{
    public class Criptografia
    {
        public static string txtcripto(string txtOriginal)
        {
            MD5 MD5Hasher = MD5.Create();

            byte[] By = Encoding.Default.GetBytes(txtOriginal);
            byte[] bytescripto = MD5Hasher.ComputeHash(By);

            StringBuilder sb = new StringBuilder();

            foreach (byte b in bytescripto)
            {
                string Debug = b.ToString("x2");
                sb.Append(Debug);
            }
            return sb.ToString();
        }
    }
}