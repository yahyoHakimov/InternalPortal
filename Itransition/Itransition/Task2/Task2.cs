namespace Itransition.Task2
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Org.BouncyCastle.Crypto.Digests;  // Import BouncyCastle for SHA3

    class Program
    {
        /*static void Main(string[] args)
        {
            // Path to the folder with the 256 binary files
            string folderPath = @"D:\Itransition\extracted"; // Update this path to your folder
            string email = "hakimovyahyo7@gmail.com".ToLower();

            // Check if the folder exists
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Error: The folder does not exist.");
                return;
            }

            // Get all files in the folder
            var files = Directory.EnumerateFiles(folderPath).ToArray();

            // Ensure exactly 256 files are present
            if (files.Length != 256)
            {
                Console.WriteLine($"Error: The number of files is {files.Length}, but it should be 256.");
                return;
            }

            // Create an instance of SHA3-256 from BouncyCastle
            var sha3Digest = new Sha3Digest(256);

            // Calculate SHA3-256 hashes for each file
            var hashes = files.Select(filePath =>
            {
                // Read the binary content of the file
                byte[] fileBytes = File.ReadAllBytes(filePath);

                // Reset the digest for each file
                sha3Digest.Reset();

                // Process the file and calculate the hash
                sha3Digest.BlockUpdate(fileBytes, 0, fileBytes.Length);
                byte[] hash = new byte[32]; // 32 bytes for SHA3-256
                sha3Digest.DoFinal(hash, 0);

                // Convert the hash to a lower-case hexadecimal string
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }).ToList();

            // Sort the hashes in ascending order
            hashes.Sort();

            // Concatenate sorted hashes and append email at the end
            string concatenatedHashes = string.Concat(hashes) + email;

            // Compute the final SHA3-256 hash of the concatenated result
            byte[] finalBytes = Encoding.UTF8.GetBytes(concatenatedHashes);
            sha3Digest.Reset();
            sha3Digest.BlockUpdate(finalBytes, 0, finalBytes.Length);
            byte[] finalHash = new byte[32];
            sha3Digest.DoFinal(finalHash, 0);

            // Output the final hash as a 64-character hexadecimal string
            string finalHashString = BitConverter.ToString(finalHash).Replace("-", "").ToLower();
            Console.WriteLine($"Final SHA3-256: {finalHashString}");
        }*/
    }

}
