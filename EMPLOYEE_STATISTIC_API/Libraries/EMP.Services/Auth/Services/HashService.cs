
using EMP.Services.Auth.Interfaces;

namespace EMP.Services.Auth.Services
{
    public class HashService : IHashService
    {
        public string Generate(string pasword) =>
            BCrypt.Net.BCrypt.EnhancedHashPassword(pasword);

        public bool Verify(string password, string hashPassword) =>
            BCrypt.Net.BCrypt.EnhancedVerify(password, hashPassword);

        //public string ConvertBytesToHexString(byte[] bytes)
        //{
        //    StringBuilder builder = new StringBuilder();
        //    foreach (byte b in bytes)
        //    {
        //        builder.Append(b.ToString("x2")); // "x2" formats as a two-digit hexadecimal number
        //    }
        //    return builder.ToString();
        //}

        //public byte[] ComputeSha256Hash(string input)
        //{
        //    using (SHA256 sha256Hash = SHA256.Create())
        //    {
        //        // Convert the input string to a byte array using UTF8 encoding
        //        byte[] data = Encoding.UTF8.GetBytes(input);

        //        // Compute the hash
        //        return sha256Hash.ComputeHash(data);
        //    }
        //}
    }
}
