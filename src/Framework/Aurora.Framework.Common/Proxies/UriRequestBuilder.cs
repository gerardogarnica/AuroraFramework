using System.Text;
using System.Web;

namespace Aurora.Framework.Proxies
{
    /// <summary>
    /// Clase que construye la codificación de parámetros para la invocación de un servicio en formato Uri.
    /// </summary>
    public class UriRequestBuilder
    {
        #region Miembros privados de la clase

        private readonly StringBuilder _builder;
        private bool _hasArguments;

        #endregion

        #region Propiedades de la clase

        /// <summary>
        /// Formato Uri de parámetros para la invocación de un servicio.
        /// </summary>
        public string ParametersUri => _builder.ToString();

        #endregion

        #region Constructores de la clase

        private UriRequestBuilder()
        {
            _builder = new StringBuilder();
            _hasArguments = false;
        }

        #endregion

        #region Métodos públicos de la clase

        /// <summary>
        /// Obtiene una instancia de la clase <c>UriRequestBuilder</c>.
        /// </summary>
        /// <returns>Devuelve una instancia de la clase <c>UriRequestBuilder</c>.</returns>
        public static UriRequestBuilder GetBuilder()
        {
            return new UriRequestBuilder();
        }

        /// <summary>
        /// Obtiene una instancia de la clase <c>UriRequestBuilder</c>.
        /// </summary>
        /// <param name="parameters">Parámetros codificados en formato Uri.</param>
        /// <returns>Devuelve una instancia de la clase <c>UriRequestBuilder</c>.</returns>
        public static UriRequestBuilder GetBuilder(string parameters)
        {
            var builder = new UriRequestBuilder();

            if (!string.IsNullOrWhiteSpace(parameters))
            {
                builder._builder.Append(parameters);
            }

            return builder;
        }

        /// <summary>
        /// Agrega un parámetro de tipo bool.
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <param name="value">Valor del parámetro de tipo bool. Si este valor es nulo, el parámetro se ignora.</param>
        public UriRequestBuilder AddBoolean(string name, bool? value)
        {
            if (value.HasValue)
            {
                AddParameter(name, value.Value.ToString());
            }

            return this;
        }

        /// <summary>
        /// Agrega un parámetro de tipo entero.
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <param name="value">Valor del parámetro de tipo entero. Si este valor es nulo, el parámetro se ignora.</param>
        public UriRequestBuilder AddInteger(string name, int? value)
        {
            if (value.HasValue)
            {
                AddParameter(name, value.Value.ToString());
            }

            return this;
        }

        /// <summary>
        /// Agrega un parámetro de tipo texto.
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <param name="value">Valor del parámetro de tipo texto. Si este valor es nulo, el parámetro se ignora.</param>
        public UriRequestBuilder AddString(string name, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                AddParameter(name, value);
            }

            return this;
        }

        #endregion

        #region Métodos privados de la clase

        private void AddParameter(string name, string value)
        {
            if (_builder.Length > 0)
            {
                if (_hasArguments)
                    _builder.Append("&");
                else
                    _builder.Append("?");
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                _builder.Append(HttpUtility.UrlEncode(name));
                _builder.Append("=");
            }

            _builder.Append(HttpUtility.UrlEncode(value));
            _hasArguments = true;
        }

        #endregion
    }
}