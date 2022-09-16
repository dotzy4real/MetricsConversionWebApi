using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using LinqToDB;
using LinqToDB.Common;
using LinqToDB.Data;
using LinqToDB.DataProvider;
using LinqToDB.DataProvider.PostgreSQL;
using LinqToDB.SqlQuery;
using Npgsql;
using MetricsConversion.Domain.Common;
using MetricsConversion.Data.Core;

namespace MetricsConversion.Data
{
    /// <summary>
    /// Represents the MS SQL Server data provider
    /// </summary>
    public partial class PostgreSqlDataProvider : BaseDataProvider, IDefaultDataProvider
    {
        #region Utils

        /// <summary>
        /// Configures the data context
        /// </summary>
        /// <param name="dataContext">Data context to configure</param>
        private void ConfigureDataContext(IDataContext dataContext)
        {
            AdditionalSchema.SetConvertExpression<string, Guid>(strGuid => new Guid(strGuid));
        }

        /// <summary>
        /// Get the name of the sequence associated with a identity column
        /// </summary>
        /// <param name="dataConnection">A database connection object</param>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>Returns the name of the sequence, or NULL if no sequence is associated with the column</returns>
        private string GetSequenceName<TEntity>(DataConnection dataConnection) where TEntity : BaseEntity
        {
            if (dataConnection is null)
                throw new ArgumentNullException(nameof(dataConnection));

            var descriptor = GetEntityDescriptor<TEntity>();

            if (descriptor is null)
                throw new Exception($"Mapped entity descriptor is not found: {typeof(TEntity).Name}");

            var tableName = descriptor.TableName;
            var columnName = descriptor.Columns.FirstOrDefault(x => x.IsIdentity && x.IsPrimaryKey)?.ColumnName;

            if (string.IsNullOrEmpty(columnName))
                throw new Exception("A table's primary key does not have an identity constraint");

            return dataConnection.Query<string>($"SELECT pg_get_serial_sequence('\"{tableName}\"', '{columnName}');")
                .FirstOrDefault();
        }


        #endregion

        #region Methods

        /// <summary>
        /// Create the database
        /// </summary>
        /// <param name="collation">Collation</param>
        /// <param name="triesToConnect">Count of tries to connect to the database after creating; set 0 if no need to connect after creating</param>
        public void CreateDatabase(string collation, int triesToConnect = 10)
        {
            if (IsDatabaseExists())
                return;

            var builder = GetConnectionStringBuilder();

            //gets database name
            var databaseName = builder.Database;

            //now create connection string to 'postgres' - default administrative connection database.
            builder.Database = "postgres";

            using (var connection = GetInternalDbConnection(builder.ConnectionString))
            {
                var query = $"CREATE DATABASE \"{databaseName}\" WITH OWNER = '{builder.Username}'";
                if (!string.IsNullOrWhiteSpace(collation))
                    query = $"{query} LC_COLLATE = '{collation}'";

                var command = connection.CreateCommand();
                command.CommandText = query;
                command.Connection.Open();

                command.ExecuteNonQuery();
            }

            //try connect
            if (triesToConnect <= 0)
                return;

            //sometimes on slow servers (hosting) there could be situations when database requires some time to be created.
            //but we have already started creation of tables and sample data.
            //as a result there is an exception thrown and the installation process cannot continue.
            //that's why we are in a cycle of "triesToConnect" times trying to connect to a database with a delay of one second.
            for (var i = 0; i <= triesToConnect; i++)
            {
                if (i == triesToConnect)
                    throw new Exception("Unable to connect to the new database. Please try one more time");

                if (!IsDatabaseExists())
                    Thread.Sleep(1000);
                else
                {
                    builder.Database = databaseName;
                    using var connection = GetInternalDbConnection(builder.ConnectionString) as NpgsqlConnection;
                    var command = connection.CreateCommand();
                    command.CommandText = "CREATE EXTENSION IF NOT EXISTS citext; CREATE EXTENSION IF NOT EXISTS pgcrypto;";
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    connection.ReloadTypes();

                    break;
                }
            }
        }

        /// <summary>
        /// Checks if the specified database exists, returns true if database exists
        /// </summary>
        /// <returns>Returns true if the database exists.</returns>
        public bool IsDatabaseExists()
        {
            try
            {
                var connectionString = DataSettingsManager.LoadSettings().ConnectionString;
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    //just try to connect
                    connection.Open();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get SQL commands from the script
        /// </summary>
        /// <param name="sql">SQL script</param>
        /// <returns>List of commands</returns>
        private static IList<string> GetCommandsFromScript(string sql)
        {
            var commands = new List<string>();

            var batches = Regex.Split(sql, @"DELIMITER \;", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            if (batches.Length > 0)
            {
                commands.AddRange(
                    batches
                        .Where(b => !string.IsNullOrWhiteSpace(b))
                        .Select(b =>
                        {
                            b = Regex.Replace(b, @"(DELIMITER )?\$\$", string.Empty);
                            b = Regex.Replace(b, @"#(.*?)\r?\n", "/* $1 */");
                            b = Regex.Replace(b, @"(\r?\n)|(\t)", " ");

                            return b;
                        }));
            }

            return commands;
        }

        /// <summary>
        /// Execute commands from a file with SQL script against the context database
        /// </summary>
        /// <param name="fileProvider">File provider</param>
        /// <param name="filePath">Path to the file</param>
        protected void ExecuteSqlScriptFromFile(IDefaultFileProvider fileProvider, string filePath)
        {
            filePath = fileProvider.MapPath(filePath);
            if (!fileProvider.FileExists(filePath))
                return;

            ExecuteSqlScript(fileProvider.ReadAllText(filePath, Encoding.Default));
        }

        /// <summary>
        /// Execute commands from the SQL script
        /// </summary>
        /// <param name="sql">SQL script</param>
        public void ExecuteSqlScript(string sql)
        {
            var sqlCommands = GetCommandsFromScript(sql);

            using var currentConnection = CreateDataConnection();
            foreach (var command in sqlCommands)
                currentConnection.Execute(command);
        }

        /// <summary>
        /// Creates the database connection
        /// </summary>
        protected override DataConnection CreateDataConnection()
        {
            var dataContext = CreateDataConnection(LinqToDbDataProvider);
            dataContext.MappingSchema.SetDataType(
                typeof(string),
                new SqlDataType(new DbDataType(typeof(string), "citext")));

            return dataContext;

            /*var dataContext = CreateDataConnection(LinqToDbDataProvider);
            ConfigureDataContext(dataContext);

            return dataContext;*/
        }

        protected NpgsqlConnectionStringBuilder GetConnectionStringBuilder()
        {
            return new NpgsqlConnectionStringBuilder(GetCurrentConnectionString());
        }


        /// <summary>
        /// Initialize database
        /// </summary>
        public void InitializeDatabase()
        {
            /*var migrationManager = EngineContext.Current.Resolve<IMigrationManager>();
            migrationManager.ApplyUpMigrations();*/

            //create stored procedures 
            var fileProvider = EngineContext.Current.Resolve<IDefaultFileProvider>();
            ExecuteSqlScriptFromFile(fileProvider, DefaultDataDefaults.PostgreSQLStoredProceduresFilePath);
        }

        public virtual string GetTableName<T>() where T: BaseEntity
        {
            using var currentConnection = CreateDataConnection();
            var tableName = currentConnection.GetTable<T>().TableName;
            return tableName;
        }


        /// <summary>
        /// Get the current identity value
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <returns>Integer identity; null if cannot get the result</returns>
        public virtual int? GetTableIdent<T>() where T: BaseEntity
        {
            using var currentConnection = CreateDataConnection();
            var seqName = GetSequenceName<T>(currentConnection);

            var result = currentConnection.Query<decimal?>($"SELECT COALESCE(last_value + CASE WHEN is_called THEN 1 ELSE 0 END, 1) as Value FROM {seqName};")
                .FirstOrDefault();

            return result.HasValue ? Convert.ToInt32(result) : 1;
        }

        /// <summary>
        /// Set table identity (is supported)
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="ident">Identity value</param>
        public virtual void SetTableIdent<T>(int ident) where T : BaseEntity
        {
            using var currentConnection = CreateDataConnection();
            var currentIdent = GetTableIdent<T>();
            if (!currentIdent.HasValue || ident <= currentIdent.Value)
                return;

            var seqName = GetSequenceName<T>(currentConnection);

            var tableName = currentConnection.GetTable<T>().TableName;

            currentConnection.Execute($" TRUNCATE {tableName} RESTART IDENTITY; ");
        }

        /// <summary>
        /// Creates a backup of the database
        /// </summary>
        public virtual void BackupDatabase(string fileName)
        {
            throw new DataException("This database provider does not support backup");
        }

        /// <summary>
        /// Restores the database from a backup
        /// </summary>
        /// <param name="backupFileName">The name of the backup file</param>
        public virtual void RestoreDatabase(string backupFileName)
        {
            throw new DataException("This database provider does not support backup");
        }

        /// <summary>
        /// Re-index database tables
        /// </summary>
        public virtual void ReIndexTables()
        {
            using var currentConnection = CreateDataConnection();
            var commandText = $"REINDEX DATABASE \"{currentConnection.Connection.Database}\";";

            currentConnection.Execute(commandText);
        }

        /// <summary>
        /// Build the connection string
        /// </summary>
        /// <param name="connectionString">Connection string info</param>
        /// <returns>Connection string</returns>
        public virtual string BuildConnectionString(IConnectionStringInfo connectionString)
        {
            if (connectionString is null)
                throw new ArgumentNullException(nameof(connectionString));

            if (connectionString.IntegratedSecurity)
                throw new Exception("Data provider supports connection only with login and password");

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = connectionString.ServerName,
                Database = connectionString.DatabaseName,
                PersistSecurityInfo = false,
                IntegratedSecurity = connectionString.IntegratedSecurity
            };

            if (!connectionString.IntegratedSecurity)
            {
                builder.Username = connectionString.Username;
                builder.Password = connectionString.Password;
            }

            return builder.ConnectionString;
        }

        /// <summary>
        /// Gets the name of a foreign key
        /// </summary>
        /// <param name="foreignTable">Foreign key table</param>
        /// <param name="foreignColumn">Foreign key column name</param>
        /// <param name="primaryTable">Primary table</param>
        /// <param name="primaryColumn">Primary key column name</param>
        /// <returns>Name of a foreign key</returns>
        public virtual string CreateForeignKeyName(string foreignTable, string foreignColumn, string primaryTable, string primaryColumn)
        {
            return $"FK_{foreignTable}_{foreignColumn}_{primaryTable}_{primaryColumn}";
        }

        /// <summary>
        /// Gets the name of an index
        /// </summary>
        /// <param name="targetTable">Target table name</param>
        /// <param name="targetColumn">Target column name</param>
        /// <returns>Name of an index</returns>
        public virtual string GetIndexName(string targetTable, string targetColumn)
        {
            return $"IX_{targetTable}_{targetColumn}";
        }


        /// <summary>
        /// Gets a connection to the database for a current data provider
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns>Connection to a database</returns>
        protected override IDbConnection GetInternalDbConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException(nameof(connectionString));

            return new NpgsqlConnection(connectionString);
        }
        #endregion


        #region Properties

        /// <summary>
        /// Sql server data provider
        /// </summary>
        protected override LinqToDB.DataProvider.IDataProvider LinqToDbDataProvider => new PostgreSQLDataProvider(PostgreSQLVersion.v95);

        /// <summary>
        /// Gets allowed a limit input value of the data for hashing functions, returns 0 if not limited
        /// </summary>
        public int SupportedLengthOfBinaryHash { get; } = 8000;

        /// <summary>
        /// Gets a value indicating whether this data provider supports backup
        /// </summary>
        public virtual bool BackupSupported => false;
        #endregion
    }
}
