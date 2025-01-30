using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DGTIT.Checador.Helpers {
    internal class HashData {
        public static string HashSHA1(string password) {
            // Convert the input string to a byte array
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Initialize SHA1 and compute the hash
            using (SHA1 sha1 = SHA1.Create()) {
                byte[] hashBytes = sha1.ComputeHash(passwordBytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes) {
                    sb.Append(b.ToString("x2"));  // Converts each byte to a 2-character hex string
                }
                return sb.ToString();
            }
        }
    }
}
