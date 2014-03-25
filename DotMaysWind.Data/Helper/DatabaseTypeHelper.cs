using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DotMaysWind.Data.Helper
{
    /// <summary>
    /// 数据库类型辅助类
    /// </summary>
    internal static class DatabaseTypeHelper
    {
        #region 字段
        private static Dictionary<String, DatabaseType> _typeDict;
        #endregion

        #region 构造方法
        static DatabaseTypeHelper()
        {
            _typeDict = new Dictionary<String, DatabaseType>();

            _typeDict[DbProviderFactoryType.OleDbDataProvider.ToLower()] = DatabaseType.Unknown;

            _typeDict[DbProviderFactoryType.SqlServerCeDataProvider35.ToLower()] = DatabaseType.SqlServerCe;
            _typeDict[DbProviderFactoryType.SqlServerCeDataProvider40.ToLower()] = DatabaseType.SqlServerCe;
            _typeDict[DbProviderFactoryType.MicrosoftSqlServerCeDataProvider35.ToLower()] = DatabaseType.SqlServerCe;
            _typeDict[DbProviderFactoryType.MicrosoftSqlServerCeDataProvider40.ToLower()] = DatabaseType.SqlServerCe;

            _typeDict[DbProviderFactoryType.SqlClientDataProvider.ToLower()] = DatabaseType.SqlServer;

            _typeDict[DbProviderFactoryType.SQLiteDataProvider.ToLower()] = DatabaseType.SQLite;
            _typeDict[DbProviderFactoryType.MonoSqliteDataProvider.ToLower()] = DatabaseType.SQLite;

            _typeDict[DbProviderFactoryType.MySQLDataProvider.ToLower()] = DatabaseType.MySQL;

            _typeDict[DbProviderFactoryType.OracleClientDataProvider.ToLower()] = DatabaseType.Oracle;
            _typeDict[DbProviderFactoryType.OracleDataProvider.ToLower()] = DatabaseType.Oracle;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取数据库类型
        /// </summary>
        /// <param name="dbProvider">数据库提供者</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns>数据库类型</returns>
        internal static DatabaseType InternalGetDatabaseType(DbProviderFactory dbProvider, String connectionString)
        {
            String providerName = dbProvider.GetType().ToString().ToLowerInvariant();

            foreach (KeyValuePair<String, DatabaseType> pair in _typeDict)
            {
                if (providerName.IndexOf(pair.Key) >= 0)
                {
                    return pair.Value;
                }
            }

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
        #endregion
    }
}