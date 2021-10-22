using System.Text;

namespace Aurora.Framework.Connections
{
    /// <summary>
    /// Representa la información de una cadena de conexión a una base de datos SQL Server.
    /// </summary>
    public class SqlConnectionStringHelper
    {
        #region Constantes

        private const string cSqlServerKeyName = "Data Source";
        private const string cSqlDatabaseKeyName = "Database";
        private const string cSqlWinAuthKeyName = "Integrated Security";
        private const string cSqlLoginKeyName = "User Id";
        private const string cSqlPwdKeyName = "Password";

        #endregion

        #region Miembros privados de la clase

        private string mSqlServer;
        private string mSqlDatabase;
        private SqlAuthenticationType mSqlAuthenticationType;
        private string mSqlUsername;
        private string mSqlPassword;
        private string mConnectionString;

        #endregion

        #region Propiedades de la clase

        /// <summary>
        /// Nombre del servidor de bases de datos de SQL Server.
        /// </summary>
        public string SqlServer
        {
            get
            {
                return mSqlServer;
            }
            internal set
            {
                mSqlServer = value ?? string.Empty;
                mConnectionString = null;
            }
        }

        /// <summary>
        /// Nombre de la base de datos de SQL Server.
        /// </summary>
        public string SqlDatabase
        {
            get
            {
                return mSqlDatabase;
            }
            internal set
            {
                mSqlDatabase = value ?? string.Empty;
                mConnectionString = null;
            }
        }

        /// <summary>
        /// Tipo de autenticación de SQL Server.
        /// </summary>
        public SqlAuthenticationType AuthenticationType
        {
            get
            {
                return mSqlAuthenticationType;
            }
            internal set
            {
                mSqlAuthenticationType = value;
                mConnectionString = null;
            }
        }

        /// <summary>
        /// Nombre de usuario para la autenticación de SQL Server.
        /// </summary>
        public string SqlUsername
        {
            get
            {
                return mSqlUsername;
            }
            internal set
            {
                mSqlUsername = value ?? string.Empty;
                mConnectionString = null;
            }
        }

        /// <summary>
        /// Contraseña de usuario para la autenticación de SQL Server.
        /// </summary>
        public string SqlPassword
        {
            get
            {
                return mSqlPassword;
            }
            internal set
            {
                mSqlPassword = value ?? string.Empty;
                mConnectionString = null;
            }
        }

        /// <summary>
        /// Cadena de conexión a una base de datos de SQL Server.
        /// </summary>
        public string ConnectionString
        {
            get
            {
                if (mConnectionString == null)
                {
                    GenerateConnectionString();
                }

                return mConnectionString;
            }
            internal set
            {
                GetConnectionString(value);
                mConnectionString = null;
            }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase SqlConnectionStringHelper.
        /// </summary>
        public SqlConnectionStringHelper()
            : this(null)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase SqlConnectionStringHelper
        /// con la cadena de conexión especificada.
        /// </summary>
        /// <param name="connectionString">Cadena de conexión a una base de datos de SQL Server.</param>
        public SqlConnectionStringHelper(string connectionString)
        {
            GetConnectionString(connectionString);
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase SqlConnectionStringHelper
        /// con el nombre de servidor y nombre de base de datos especificados.
        /// </summary>
        /// <param name="serverName">Nombre del servidor de bases de datos.</param>
        /// <param name="databaseName">Nombre de la base de datos.</param>
        public SqlConnectionStringHelper(string serverName, string databaseName)
        {
            mSqlServer = serverName;
            mSqlDatabase = databaseName;
            mSqlAuthenticationType = SqlAuthenticationType.WindowsAuthentication;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase SqlConnectionStringHelper
        /// con el nombre de servidor, nombre de base de datos, nombre de usuario
        /// y contraseña especificados.
        /// </summary>
        /// <param name="serverName">Nombre del servidor de bases de datos.</param>
        /// <param name="databaseName">Nombre de la base de datos.</param>
        /// <param name="userName">Nombre de usuario para la autenticación de SQL.</param>
        /// <param name="password">Contraseña de usuario para la autenticación de SQL.</param>
        public SqlConnectionStringHelper(
            string serverName, string databaseName, string userName, string password)
        {
            mSqlServer = serverName;
            mSqlDatabase = databaseName;
            mSqlAuthenticationType = SqlAuthenticationType.SqlAuthentication;
            mSqlUsername = userName;
            mSqlPassword = password;
        }

        #endregion

        #region Métodos para generar la cadena de conexión

        private void GenerateConnectionString()
        {
            var sb = new StringBuilder();

            // Se agrega el nombre del servidor SQL Server
            AppendKeyValue(sb, cSqlServerKeyName, mSqlServer);

            // Se agrega el nombre de la base de datos SQL
            AppendKeyValue(sb, cSqlDatabaseKeyName, mSqlDatabase);

            // Se agrega el tipo de autenticación
            if (mSqlAuthenticationType == SqlAuthenticationType.WindowsAuthentication)
            {
                // Se agrega la autenticación Windows
                AppendKeyValue(sb, cSqlWinAuthKeyName, "sspi");
            }
            else
            {
                // Se agrega la autenticación SQL
                AppendKeyValue(sb, cSqlLoginKeyName, mSqlUsername);
                AppendKeyValue(sb, cSqlPwdKeyName, mSqlPassword);
            }

            mConnectionString = sb.ToString();
        }

        #endregion

        #region Métodos para interpretar la cadena de conexión

        private void GetConnectionString(string connectionString)
        {
            var tmpConnString = connectionString ?? string.Empty;

            ResetFields();

            while (tmpConnString.Length > 0)
            {
                // Se extrae el nombre de la primera clave disponible
                var keyName = string.Empty;

                bool exit;
                do
                {
                    exit = true;
                    var position = tmpConnString.IndexOf("=");

                    if (position == -1)
                    {
                        keyName += tmpConnString;
                        tmpConnString = string.Empty;
                    }
                    else
                    {
                        keyName = tmpConnString.Substring(0, position);
                        tmpConnString = tmpConnString.Substring(position + 1);

                        if (tmpConnString.Length > 0 && tmpConnString[0].Equals("="))
                        {
                            keyName += "=";
                            tmpConnString = tmpConnString.Substring(1);
                            exit = false;
                        }
                    }
                }
                while (!exit);

                // Se extrae el valor de la clave extraída
                var value = string.Empty;
                if (tmpConnString.Length > 0)
                {
                    var separator = ";";
                    var quoted = false;

                    if (tmpConnString.IndexOf('\'') == 0)
                    {
                        quoted = true;
                        separator = "';";

                        tmpConnString = tmpConnString.Substring(1);
                    }

                    // Se busca el siguiente separador
                    var position = tmpConnString.IndexOf(separator);

                    if (position == -1)
                    {
                        value = tmpConnString;
                        tmpConnString = string.Empty;
                    }
                    else
                    {
                        value = tmpConnString.Substring(0, position);
                        tmpConnString = tmpConnString.Substring(position + separator.Length);
                    }

                    if (quoted)
                    {
                        value = value.Replace("''", "'");
                    }
                }

                // Se asigna el tipo de clave y valor que corresponde
                if (keyName == cSqlServerKeyName)
                {
                    mSqlServer = value;
                }
                else if (keyName == cSqlDatabaseKeyName)
                {
                    mSqlDatabase = value;
                }
                else if (keyName == cSqlWinAuthKeyName)
                {
                    mSqlAuthenticationType = value == "sspi" ?
                        SqlAuthenticationType.WindowsAuthentication :
                        SqlAuthenticationType.SqlAuthentication;
                }
                else if (keyName == cSqlLoginKeyName)
                {
                    mSqlUsername = value;
                }
                else if (keyName == cSqlPwdKeyName)
                {
                    mSqlPassword = value;
                }
                else
                {
                    var message = string.Format("Se produjo un error al obtener la cadena de conexión a la base de datos debido a un valor incorrecto en el texto. Clave no válida: {0}", keyName);

                    throw new Exceptions.PlatformException(message);
                }
            }
        }

        #endregion

        #region Métodos privados de la clase

        private void AppendKeyValue(StringBuilder builder, string keyName, string value)
        {
            // Validaciones de parámetros
            if (string.IsNullOrEmpty(keyName)) return;
            if (string.IsNullOrEmpty(value)) return;

            // Se agrega la clave
            builder.Append(keyName.Replace("=", "=="));

            // Se agrega el símbolo de igual
            builder.Append("=");

            // Se agrega el valor a la clave
            if (value.IndexOfAny(new char[] { '"', '\'', ';', '=' }) == -1)
            {
                builder.Append(value);
            }
            else
            {
                builder.AppendFormat("'{0}'", value.Replace("'", "''"));
            }

            // Se agrega el carácter de terminación del valor
            builder.Append(";");
        }

        private void ResetFields()
        {
            mSqlServer = string.Empty;
            mSqlDatabase = string.Empty;
            mSqlAuthenticationType = SqlAuthenticationType.SqlAuthentication;
            mSqlUsername = string.Empty;
            mSqlPassword = string.Empty;
            mConnectionString = null;
        }

        #endregion
    }
}