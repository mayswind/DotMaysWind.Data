using System;

namespace DotMaysWind.Data
{
    /// <summary>
    /// 数据库提供者工厂类型
    /// </summary>
    internal struct DbProviderFactoryType
    {
        /// <summary>
        /// System.Data.OleDb
        /// </summary>
        internal const String OleDbDataProvider = "System.Data.OleDb";

        /// <summary>
        /// System.Data.SqlServerCe.3.5
        /// </summary>
        internal const String SqlServerCeDataProvider35 = "System.Data.SqlServerCe.3.5";

        /// <summary>
        /// System.Data.SqlServerCe.4.0
        /// </summary>
        internal const String SqlServerCeDataProvider40 = "System.Data.SqlServerCe.4.0";

        /// <summary>
        /// Microsoft.SqlServerCe.Client.3.5
        /// </summary>
        internal const String MicrosoftSqlServerCeDataProvider35 = "Microsoft.SqlServerCe.Client.3.5";

        /// <summary>
        /// Microsoft.SqlServerCe.Client.4.0
        /// </summary>
        internal const String MicrosoftSqlServerCeDataProvider40 = "Microsoft.SqlServerCe.Client.4.0";

        /// <summary>
        /// System.Data.SqlClient
        /// </summary>
        internal const String SqlClientDataProvider = "System.Data.SqlClient";

        /// <summary>
        /// System.Data.SQLite
        /// </summary>
        internal const String SQLiteDataProvider = "System.Data.SQLite";

        /// <summary>
        /// Mono.Data.Sqlite
        /// </summary>
        internal const String MonoSqliteDataProvider = "Mono.Data.Sqlite";

        /// <summary>
        /// MySql.Data.MySqlClient
        /// </summary>
        internal const String MySQLDataProvider = "MySql.Data.MySqlClient";

        /// <summary>
        /// System.Data.OracleClient
        /// </summary>
        internal const String OracleClientDataProvider = "System.Data.OracleClient";

        /// <summary>
        /// Oracle.DataAccess.Client
        /// </summary>
        internal const String OracleDataProvider = "Oracle.DataAccess.Client";
    }
}