using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itransition.Task3
{
    using System.Security.Cryptography;
    using System.Text;
    using Org.BouncyCastle.Crypto.Digests; // For SHA3, or use built-in HMACSHA256 for SHA256

    public class KeyGenerator
    {
        private readonly byte[] key;

        public KeyGenerator()
        {
            key = GenerateKey();
        }

        // Generate a cryptographically strong random key (32 bytes = 256 bits)
        public byte[] GenerateKey()
        {
            byte[] key = new byte[32];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }
            return key;
        }

        // Calculate HMAC based on the move (message) and key using SHA-256
        public string CalculateHMAC(string message)
        {
            using (var hmacsha256 = new HMACSHA256(key))
            {
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                byte[] hashBytes = hmacsha256.ComputeHash(messageBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        // Get the key as a hexadecimal string
        public string GetKeyString()
        {
            return BitConverter.ToString(key).Replace("-", "").ToLower();
        }
    }

}
