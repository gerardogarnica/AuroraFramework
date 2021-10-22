using System.IO;
using System.Security.Cryptography;

namespace Aurora.Framework.Cryptography
{
    /// <summary>
    /// Proporciona los métodos de cifrado y descifrado simétrico mediante
    /// el algoritmo del estándar de cifrado avanzado (AES).
    /// </summary>
    internal static class SymmetricCryptography
    {
        internal static SymmetricKey CreateSymmetricKey()
        {
            using (var aes = GetAesCryptoServiceProvider())
            {
                aes.GenerateKey();
                aes.GenerateIV();

                return new SymmetricKey
                {
                    Key = aes.Key,
                    IV = aes.IV
                };
            }
        }

        internal static byte[] Encrypt(byte[] data, SymmetricKey symmetricKey)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var aes = GetAesCryptoServiceProvider(symmetricKey))
                {
                    using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    {
                        using (var cryptoStream = new CryptoStream(outputStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(data, 0, data.Length);
                            cryptoStream.FlushFinalBlock();
                        }
                    }
                }

                return outputStream.ToArray();
            }
        }

        internal static byte[] Decrypt(byte[] data, SymmetricKey symmetricKey)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var aes = GetAesCryptoServiceProvider(symmetricKey))
                {
                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    {
                        using (var inputStream = new MemoryStream(data))
                        {
                            using (var cryptoStream = new CryptoStream(inputStream, decryptor, CryptoStreamMode.Read))
                            {
                                cryptoStream.CopyTo(outputStream);
                            }
                        }
                    }
                }

                return outputStream.ToArray();
            }
        }

        private static AesCryptoServiceProvider GetAesCryptoServiceProvider()
        {
            return new AesCryptoServiceProvider
            {
                KeySize = 256,
                BlockSize = 128,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };
        }

        private static AesCryptoServiceProvider GetAesCryptoServiceProvider(SymmetricKey symmetricKey)
        {
            var aes = GetAesCryptoServiceProvider();

            if (symmetricKey != null)
            {
                aes.Key = symmetricKey.Key;
                aes.IV = symmetricKey.IV;
            }
            else
            {
                aes.GenerateKey();
                aes.GenerateIV();
            }

            return aes;
        }
    }
}