using Aurora.Framework.Exceptions;
using System;
using System.IO;
using System.Text;

namespace Aurora.Framework.Cryptography
{
    /// <summary>
    /// Proveedor de cifrado y descifrado seguro de datos utilizando cadenas de texto.
    /// </summary>
    public static class EncryptionProvider
    {
        #region Cifrado de información

        /// <summary>
        /// Protege el contenido de una cadena de texto y devuelve una cadena con el contenido cifrado.
        /// </summary>
        /// <param name="content">Contenido a ser cifrado.</param>
        public static string Protect(string content)
        {
            // Si los datos son vacíos o nulos
            if (string.IsNullOrWhiteSpace(content))
            {
                return null;
            }

            try
            {
                // Se realiza el cifrado de información
                var rawContent = Encoding.UTF8.GetBytes(content);
                var encryptedContent = Protect(rawContent);

                // Se devuelve el contenido cifrado en una cadena de texto
                return Convert.ToBase64String(encryptedContent);
            }
            catch (Exception e)
            {
                throw new PlatformException(ExceptionMessages.ProtectEncryptionException, e);
            }
        }

        /// <summary>
        /// Protege el contenido de una cadena de texto y devuelve una cadena con el contenido cifrado
        /// y los datos de control de la cadena cifrada.
        /// </summary>
        /// <param name="content">Contenido a ser cifrado.</param>
        /// <param name="controlData">Datos de control para cifrar el contenido.</param>
        public static string Protect(string content, out string controlData)
        {
            controlData = null;

            // Si los datos son vacíos o nulos
            if (string.IsNullOrWhiteSpace(content))
            {
                return null;
            }

            try
            {
                // Se obtienen los datos de control
                var symetricKey = SymmetricCryptography.CreateSymmetricKey();
                var rawControlData = symetricKey.ToToken();

                // Se realiza el cifrado de información
                var rawContent = Encoding.UTF8.GetBytes(content);
                var encryptedContent = SymmetricCryptography.Encrypt(rawContent, symetricKey);

                // Se devuelve el contenido cifrado y los datos de control en cadenas de texto
                controlData = Convert.ToBase64String(rawControlData);
                return Convert.ToBase64String(encryptedContent);
            }
            catch (Exception e)
            {
                throw new PlatformException(ExceptionMessages.ProtectEncryptionException, e);
            }
        }

        /// <summary>
        /// Devuelve un arreglo de bytes con el contenido cifrado.
        /// </summary>
        /// <param name="rawContent">Contenido a ser cifrado.</param>
        private static byte[] Protect(byte[] rawContent)
        {
            // Si los datos son vacíos o nulos
            if (rawContent == null)
            {
                return null;
            }
            else if (rawContent.Length == 0)
            {
                return new byte[] { };
            }

            // Buffer donde se almacenarán los datos de salida
            using (var outputStream = new MemoryStream())
            {
                // Se crea la llave simétrica
                var symetricKey = SymmetricCryptography.CreateSymmetricKey();

                // Se realiza el cifrado de información del contenido original
                var encryptedContent = SymmetricCryptography.Encrypt(rawContent, symetricKey);

                // Se almacena la clave y el vector de inicialización en el buffer de salida
                outputStream.WriteByte((byte)symetricKey.IV.Length);
                outputStream.WriteByte((byte)symetricKey.Key.Length);
                outputStream.Write(symetricKey.Key, 0, symetricKey.Key.Length);
                outputStream.Write(symetricKey.IV, 0, symetricKey.IV.Length);

                // Se almacenan los datos cifrados
                outputStream.Write(encryptedContent, 0, encryptedContent.Length);

                // Se devuelve el contenido encriptado
                return outputStream.ToArray();
            }
        }

        #endregion

        #region Descifrado de información

        /// <summary>
        /// Desprotege el contenido cifrado de una cadena de texto y devuelve una cadena con el contenido original descifrado.
        /// </summary>
        /// <param name="content">Contenido a ser descifrado.</param>
        public static string Unprotect(string content)
        {
            // Si los datos son vacíos o nulos
            if (string.IsNullOrWhiteSpace(content))
            {
                return null;
            }

            try
            {
                // Se realiza el descifrado de información
                var rawContent = Convert.FromBase64String(content);
                var decryptedContent = Unprotect(rawContent);

                // Se devuelve el contenido descifrado en una cadena de texto
                return Encoding.UTF8.GetString(decryptedContent);
            }
            catch (Exception e)
            {
                throw new PlatformException(ExceptionMessages.UnprotectEncryptionException, e);
            }
        }

        /// <summary>
        /// Desprotege el contenido cifrado de una cadena de texto a partir de los datos de control
        /// y devuelve una cadena con el contenido original descifrado.
        /// </summary>
        /// <param name="content">Contenido a ser descifrado.</param>
        /// <param name="controlData">Datos de control para descifrar el contenido.</param>
        public static string Unprotect(string content, string controlData)
        {
            // Si los datos son vacíos o nulos
            if (string.IsNullOrWhiteSpace(content))
            {
                return null;
            }

            try
            {
                // Se descifran los datos de control
                var rawControlData = Convert.FromBase64String(controlData);
                var symetricKey = SymmetricKey.FromToken(rawControlData);

                // Se realiza el descifrado de información
                var rawContent = Convert.FromBase64String(content);
                var decryptedContent = SymmetricCryptography.Decrypt(rawContent, symetricKey);

                // Se devuelve el contenido descifrado en una cadena de texto
                return Encoding.UTF8.GetString(decryptedContent);
            }
            catch (Exception e)
            {
                throw new PlatformException(ExceptionMessages.UnprotectEncryptionException, e);
            }
        }

        /// <summary>
        /// Devuelve un arreglo de bytes con el contenido descifrado.
        /// </summary>
        /// <param name="rawContent">Contenido a ser descifrado.</param>
        private static byte[] Unprotect(byte[] rawContent)
        {
            SymmetricKey symmetricKey;
            byte[] cryptoContent;

            // Si los datos son vacíos o nulos
            if (rawContent == null)
            {
                return null;
            }
            else if (rawContent.Length == 0)
            {
                return new byte[] { };
            }

            // Buffer donde se almacenarán los datos de entrada
            using (var inputStream = new MemoryStream(rawContent))
            {
                // Se recuperan los tamaños de la clave simétrica y del vector de inicialización
                var ivSize = inputStream.ReadByte();
                var keySize = inputStream.ReadByte();

                // Se recupera la clave simétrica del buffer de salida
                var key = new byte[keySize];
                inputStream.Read(key, 0, keySize);

                // Se recupera el vector de inicialización del buffer de salida
                var iv = new byte[ivSize];
                inputStream.Read(iv, 0, ivSize);

                // Se crea la clave de cifrado
                symmetricKey = new SymmetricKey
                {
                    Key = key,
                    IV = iv
                };

                // Se copian los datos en el buffer de salida
                using (var outputStream = new MemoryStream())
                {
                    inputStream.CopyTo(outputStream);
                    cryptoContent = outputStream.ToArray();
                }
            }

            // Se devuelve el contenido desencriptado
            return SymmetricCryptography.Decrypt(cryptoContent, symmetricKey);
        }

        #endregion
    }
}