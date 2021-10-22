using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;

namespace Aurora.Framework
{
    /// <summary>
    /// Clase que contiene métodos de utilidades para uso de aplicaciones.
    /// </summary>
    public static class CommonUtils
    {
        /// <summary>
        /// Devuelve el valor del booleano nulo en un valor booleano.
        /// </summary>
        /// <param name="value">Valor booleano que se va a convertir.</param>
        public static bool GetBoolean(bool? value)
        {
            return value.HasValue && Convert.ToBoolean(value);
        }

        /// <summary>
        /// Devuelve el valor del entero nulo en un entero de 16 bits.
        /// </summary>
        /// <param name="value">Entero de 16 bits que se va a convertir.</param>
        public static short GetInt16(short? value)
        {
            return value.HasValue ? Convert.ToInt16(value) : (short)0;
        }

        /// <summary>
        /// Devuelve el valor del entero nulo en un entero de 32 bits.
        /// </summary>
        /// <param name="value">Entero de 32 bits que se va a convertir.</param>
        public static int GetInt32(int? value)
        {
            return value.HasValue ? Convert.ToInt32(value) : 0;
        }

        /// <summary>
        /// Devuelve el valor del entero nulo en un entero de 64 bits.
        /// </summary>
        /// <param name="value">Entero de 64 bits que se va a convertir.</param>
        public static long GetInt64(long? value)
        {
            return value.HasValue ? Convert.ToInt64(value) : (long)0;
        }

        /// <summary>
        /// Devuelve la ruta completa del directorio donde se encuentra el ensamblado indicado.
        /// </summary>
        /// <param name="assembly">Ensamblado del cual se quiere obtener la ruta completa del directorio.</param>
        public static string GetAssemblyDirectory(Assembly assembly)
        {
            try
            {
                var uri = new UriBuilder(assembly.Location);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Obtiene la dirección IP versión 4 del equipo local.
        /// </summary>
        public static string GetLocalIpAddress()
        {
            try
            {
                var adresses = Dns.GetHostAddresses(Dns.GetHostName());

                return adresses
                    .ToList()
                    .Find(x => x.AddressFamily.Equals(AddressFamily.InterNetwork))
                    .ToString();
            }
            catch
            {
                return null;
            }
        }
    }
}