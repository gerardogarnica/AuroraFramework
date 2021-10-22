using System;
using System.Text;

namespace Aurora.Framework.Cryptography
{
    /// <summary>
    /// Contiene las propiedades y métodos de la llave simétrica para cifrado y descifrado.
    /// </summary>
    internal class SymmetricKey
    {
        private const char cTokenSeparator = '$';

        internal byte[] Key { get; set; }
        internal byte[] IV { get; set; }

        internal static SymmetricKey FromToken(byte[] token)
        {
            var tokens = Encoding
                .UTF8
                .GetString(token)
                .Split(cTokenSeparator);

            return new SymmetricKey
            {
                Key = Convert.FromBase64String(tokens[0]),
                IV = Convert.FromBase64String(tokens[1])
            };
        }

        internal byte[] ToToken()
        {
            var tokens = string.Concat(
                Convert.ToBase64String(Key),
                cTokenSeparator,
                Convert.ToBase64String(IV));

            return Encoding
                .UTF8
                .GetBytes(tokens);
        }
    }
}