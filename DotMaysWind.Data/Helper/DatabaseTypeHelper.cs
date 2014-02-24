using System;
using System.Data.Common;

namespace DotMaysWind.Data.Helper
{
    /// <summary>
    /// 数据库类型辅助类
    /// </summary>
    internal static class DatabaseTypeHelper
    {
        /// <summary>
        /// 获取数据库类型
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="dbProvider">数据库提供者</param>
        /// <returns>数据库类型</returns>
        internal static DatabaseType InternalGetDatabaseType(String connectionString, DbProviderFactory dbProvider)
        {
            String providerName = dbProvider.GetType().ToString().ToLowerInvariant();

            #region SqlServer
            if (providerName.IndexOf("system.data.sqlclient") >= 0)
            {
                return DatabaseType.SqlServer;
            }
            #endregion

            #region MySQL
            if (providerName.IndexOf("mysql.data.mysqlclient") >= 0)
            {
                return DatabaseType.MySQL;
            }
            #endregion

            #region Sqlite
            if (providerName.IndexOf("system.data.sqlite") >= 0)
            {
                return DatabaseType.SQLite;
            }

            if (providerName.IndexOf("mono.data.sqliteclient") >= 0)
            {
                return DatabaseType.SQLite;
            }
            #endregion

            #region SqlServerCe
            if (providerName.IndexOf("system.data.sqlserverce") >= 0)
            {
                return DatabaseType.SqlServerCe;
            }

            if (providerName.IndexOf("microsoft.sqlserverce.client") >= 0)
            {
                return DatabaseType.SqlServerCe;
            }
            #endregion

            #region Oracle
            if (providerName.IndexOf("system.data.oracleclient") >= 0)
            {
                return DatabaseType.Oracle;
            }

            if (providerName.IndexOf("oracle.dataaccess.client") >= 0)
            {
                return DatabaseType.Oracle;
            }
            #endregion

            #region Access
            if (providerName.IndexOf("system.data.oledb") >= 0)
            {
                String dataSource = "";
                String[] parameters = connectionString.Replace(" ", "").ToLowerInvariant().Split(';');

                if (parameters != null && parameters.Length > 0)
                {
                    Int32 dataSourcePos = -1;

                    for (Int32 i = 0; i < parameters.Length; i++)
                    {
                        dataSourcePos = parameters[i].IndexOf("datasource");

                        if (dataSourcePos > -1)
                        {
                            dataSource = parameters[i];
                            break;
                        }
                    }
                }

                if (dataSource.IndexOf(".mdb") > -1)
                {
                    return DatabaseType.Access;
                }
                else if (dataSource.IndexOf(".accdb") > -1)
                {
                    return DatabaseType.Access;
                }
            }
            #endregion

            return DatabaseType.Unknown;
        }
    }
}