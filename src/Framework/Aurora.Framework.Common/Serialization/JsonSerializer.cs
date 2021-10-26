using Newtonsoft.Json;
using System.IO;

namespace Aurora.Framework.Serialization
{
    /// <summary>
    /// Proveedor de serialización en formato JSON.
    /// </summary>
    public class JsonSerializer
    {
        /// <summary>
        /// Carga un archivo que contiene un texto en formato JSON desde una ruta especificada
        /// y devuelve un objeto de tipo <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto que se requiere deserializar desde el archivo.</typeparam>
        /// <param name="filePath">Ruta del archivo que contiene un texto en formato JSON.</param>
        public static T LoadFromFile<T>(string filePath)
        {
            using var reader = new StreamReader(filePath);

            var jsonValue = reader.ReadToEnd();
            return Deserialize<T>(jsonValue);
        }

        /// <summary>
        /// Deserializa una cadena de texto en formato JSON y devuelve un objeto de tipo <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto que se requiere deserializar desde la cadena de texto en formato JSON.</typeparam>
        /// <param name="jsonValue">Cadena de texto en formato JSON.</param>
        public static T Deserialize<T>(string jsonValue)
        {
            return JsonConvert.DeserializeObject<T>(jsonValue);
        }

        /// <summary>
        /// Serializa un objeto especificado de tipo <typeparamref name="T"/> a una cadena de texto en formato JSON.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto que se requiere convertir en una cadena de texto en formato JSON.</typeparam>
        /// <param name="entity">Objeto que se requiere convertir en una cadena de texto en formato JSON.</param>
        public static string Serialize<T>(T entity)
        {
            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };

            return JsonConvert.SerializeObject(entity, settings);
        }
    }
}