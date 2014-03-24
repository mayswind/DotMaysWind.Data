using System;

namespace DotMaysWind.Data
{
    /// <summary>
    /// 数据库提供者工厂类型
    /// </summary>
    public struct DbProviderFactoryType
    {
        /// <summary>
        /// System.Data.OleDb
        /// </summary>
        public const String OleDbDataProvider = "System.Data.OleDb";

        /// <summary>
        /// System.Data.SqlServerCe.3.5
        /// </summary>
        public const String SqlServerCeDataProvider35 = "System.Data.SqlServerCe.3.5";

        /// <summary>
        /// System.Data.SqlServerCe.4.0
        /// </summary>
        public const String SqlServerCeDataProvider40 = "System.Data.SqlServerCe.4.0";

        /// <summary>
        /// Microsoft.SqlServerCe.Client.3.5
        /// </summary>
        public const String MicrosoftSqlServerCeDataProvider35 = "Microsoft.SqlServerCe.Client.3.5";

        /// <summary>
        /// Microsoft.SqlServerCe.Client.4.0
        /// </summary>
        public const String MicrosoftSqlServerCeDataProvider40 = "Microsoft.SqlServerCe.Client.4.0";

        /// <summary>
        /// System.Data.SqlClient
        /// </summary>
        public const String SqlClientDataProvider = "System.Data.SqlClient";

        /// <summary>
        /// System.Data.SQLite
        /// </summary>
        public const String SQLiteDataProvider = "System.Data.SQLite";

        /// <summary>
        /// Mono.Data.Sqlite
        /// </summary>
        public const String MonoSqliteDataProvider = "Mono.Data.Sqlite";

        /// <summary>
        /// MySql.Data.MySqlClient
        /// </summary>
        public const String MySQLDataProvider = "MySql.Data.MySqlClient";

        /// <summary>
        /// System.Data.OracleClient
        /// </summary>
        public const String OracleClientDataProvider = "System.Data.OracleClient";

        /// <summary>
        /// Oracle.DataAccess.Client
        /// </summary>
        public const String OracleDataProvider = "Oracle.DataAccess.Client";
    }
}