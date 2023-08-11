using System.Security.Cryptography;
using System.Text;

namespace PruebaTec.Models
{
    public class HashConverter
    {
        public string Hashear(string pass)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));
                string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                // Truncar el hash a 200 caracteres
                if (hashString.Length > 200)
                {
                    hashString = hashString.Substring(0, 200);
                }

                return hashString;
            }
        }
    }
}
