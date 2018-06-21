using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BenTools.Helpers.FileSystem
{
    /// <summary>
    /// This class should be remove and replace thank my news knowledge.
    /// </summary>
    public static class EncryptionHelper
    {
        private static readonly string encryptionKey = "Sample Key";

        public static string Md5Encrypt(string source)
        {
            byte[] hashBytes;
            byte[] bufferBytes;
            var DESCryptoProvider = new TripleDESCryptoServiceProvider();
            var MD5CryptoProvider = new MD5CryptoServiceProvider();

            bufferBytes = Encoding.UTF8.GetBytes(source);
            hashBytes = MD5CryptoProvider.ComputeHash(Encoding.UTF8.GetBytes(encryptionKey));
            DESCryptoProvider.Key = hashBytes;
            DESCryptoProvider.Mode = CipherMode.ECB;

            return Convert.ToBase64String(DESCryptoProvider.CreateEncryptor()
                          .TransformFinalBlock(bufferBytes, 0, bufferBytes.Length));
        }

        public static string Md5Decrypt(string encodedText)
        {
            byte[] hashBytes;
            var DESCryptoProvider = new TripleDESCryptoServiceProvider();
            var MD5CryptoProvider = new MD5CryptoServiceProvider();
            var bufferBytes = Convert.FromBase64String(encodedText);

            hashBytes = MD5CryptoProvider.ComputeHash(Encoding.UTF8.GetBytes(encryptionKey));
            DESCryptoProvider.Key = hashBytes;
            DESCryptoProvider.Mode = CipherMode.ECB;

            return Encoding.UTF8.GetString(DESCryptoProvider.CreateDecryptor()
                                .TransformFinalBlock(bufferBytes, 0, bufferBytes.Length));
        }

        public static string MD5(string data) => Encoding.ASCII.GetString(MD5hash(Encoding.ASCII.GetBytes(data)));

        private static byte[] MD5hash(byte[] data) => new MD5CryptoServiceProvider().ComputeHash(data);

        public static byte[] Md5EncryptWithoutKey(string textToEncrypt)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
                return md5.ComputeHash(Encoding.ASCII.GetBytes(textToEncrypt));
        }

        public static string MD5EncryptAsText(string textToEncrypt)
        {
            var stringBuilder = new StringBuilder();
            var textToEncryptHashedBytes = Md5EncryptWithoutKey(textToEncrypt);

            foreach (var oneByte in textToEncryptHashedBytes)
                stringBuilder.Append(oneByte.ToString("X2"));

            return stringBuilder.ToString();
        }

        public static byte[] AES256Encrypt(byte[] bytesToEncrypt, byte[] passwordBytes)
        {
            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = {1, 2, 3, 4, 5, 6, 7, 8};

            using (var memoryStream = new MemoryStream())
            using (var rijndaelManaged = new RijndaelManaged())
            {
                rijndaelManaged.KeySize = 256;
                rijndaelManaged.BlockSize = 128;

                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                rijndaelManaged.Key = key.GetBytes(rijndaelManaged.KeySize / 8);
                rijndaelManaged.IV = key.GetBytes(rijndaelManaged.BlockSize / 8);
                rijndaelManaged.Mode = CipherMode.CBC;

                using (var cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(bytesToEncrypt, 0, bytesToEncrypt.Length);
                    cryptoStream.Close();
                }

                return memoryStream.ToArray();
            }
        }

        public static byte[] AES256Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = {1, 2, 3, 4, 5, 6, 7, 8};

            using (var memoryStream = new MemoryStream())
            using (var rijndaelManaged = new RijndaelManaged())
            {
                rijndaelManaged.KeySize = 256;
                rijndaelManaged.BlockSize = 128;

                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                rijndaelManaged.Key = key.GetBytes(rijndaelManaged.KeySize / 8);
                rijndaelManaged.IV = key.GetBytes(rijndaelManaged.BlockSize / 8);
                rijndaelManaged.Mode = CipherMode.CBC;

                using (var cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                    cryptoStream.Close();
                }

                return memoryStream.ToArray();
            }
        }

        public static string AES256Encrypt(string input, string password)
        {
            // Get the bytes of the string
            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            var bytesEncrypted = AES256Encrypt(bytesToBeEncrypted, passwordBytes);
            var result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }

        public static string AES256Decrypt(string input, string password)
        {
            // Get the bytes of the string
            var bytesToBeDecrypted = Convert.FromBase64String(input);
            var passwordBytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
            var bytesDecrypted = AES256Decrypt(bytesToBeDecrypted, passwordBytes);

            return Encoding.UTF8.GetString(bytesDecrypted);
        }
    }
}
