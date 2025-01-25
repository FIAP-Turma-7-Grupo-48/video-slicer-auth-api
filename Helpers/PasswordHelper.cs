using System.Security.Cryptography;
using System.Text;

namespace Domain.Helpers
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Converte a senha em bytes e calcula o hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Converte o array de bytes em uma string hexadecimal
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string storedHash, string providedPassword)
        {
            // Hash the provided password using SHA256
            using (var sha256 = SHA256.Create())
            {
                var providedPasswordBytes = Encoding.UTF8.GetBytes(providedPassword);
                var hashBytes = sha256.ComputeHash(providedPasswordBytes);

                StringBuilder builder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                // Compare the provided hash with the stored hash
                return builder.ToString() == storedHash;
            }
        }
    }
}