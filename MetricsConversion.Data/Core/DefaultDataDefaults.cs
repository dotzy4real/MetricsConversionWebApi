namespace MetricsConversion.Data.Core
{
    /// <summary>
    /// Represents default values related to Nop data
    /// </summary>
    public static partial class DefaultDataDefaults
    {
        /// <summary>
        /// Gets a path to the file that contains script to create SQL Server stored procedures
        /// </summary>
        public static string SqlServerStoredProceduresFilePath => "~/App_Data/Install/SqlServer.StoredProcedures.sql";

        /// <summary>
        /// Gets a path to the file that contains script to create MySQL stored procedures
        /// </summary>
        public static string MySQLStoredProceduresFilePath => "~/App_Data/Install/MySQL.StoredProcedures.sql";


        /// <summary>
        /// Gets a path to the file that contains script to create PostgreSQL stored procedures
        /// </summary>
        public static string PostgreSQLStoredProceduresFilePath => "~/App_Data/Install/PostgreSql.StoredProcedures.sql";
    }
}
