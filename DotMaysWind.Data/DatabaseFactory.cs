using System;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;

using DotMaysWind.Data.Configuration;
using DotMaysWind.Data.Helper;

using SystemConfigurationManager = System.Configuration.ConfigurationManager;
using ConnectionStringSettings = System.Configuration.ConnectionStringSettings;

namespace DotMaysWind.Data
{
    /// <summary>
    /// 数据库工厂类
    /// </summary>
    public static class DatabaseFactory
    {
        #region 从连接字符串中创建
        /// <summary>
        /// 根据连接字符串创建Sql Server数据库
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns>数据库实体</returns>
        public static SqlServerDatabase CreateSqlServerDatabase(String connectionString)
        {
            return new SqlServerDatabase(SqlClientFactory.Instance, connectionString);
        }

        /// <summary>
        /// 根据连接字符串创建Oracle数据库
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns>数据库实体</returns>
        public static OracleDatabase CreateOracleDatabase(String connectionString)
        {
            return new OracleDatabase(OracleClientFactory.Instance, connectionString);
        }

        /// <summary>
        /// 根据连接字符串和数据库提供者创建数据库
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="providerName">数据库提供者名称</param>
        /// <returns>基数据库实体</returns>
        public static AbstractDatabase CreateDatabase(String connectionString, String providerName)
        {
            return DatabaseFactory.CreateDatabase(DbProviderFactories.GetFactory(providerName), connectionString);
        }

        /// <summary>
        /// 根据数据库提供者和连接字符串创建数据库
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="dbProvider">数据库提供者</param>
        /// <exception cref="DatabaseNotSupportException">指定数据库不支持</exception>
        /// <returns>数据库实体</returns>
        public static AbstractDatabase CreateDatabase(DbProviderFactory dbProvider, String connectionString)
        {
            DatabaseType dbType = DatabaseTypeHelper.InternalGetDatabaseType(dbProvider, connectionString);

            switch (dbType)
            {
                case DatabaseType.Access:
                    return new AccessDatabase(dbProvider, connectionString);
                case DatabaseType.MySQL:
                    return new MySQLDatabase(dbProvider, connectionString);
                case DatabaseType.Oracle:
                    return new OracleDatabase(dbProvider, connectionString);
                case DatabaseType.SQLite:
                    return new SQLiteDatabase(dbProvider, connectionString);
                case DatabaseType.SqlServer:
                    return new SqlServerDatabase(dbProvider, connectionString);
                case DatabaseType.SqlServerCe:
                    return new SqlServerCeDatabase(dbProvider, connectionString);
                default:
                    throw new DatabaseNotSupportException("This database is not supported!");
            }
        }
        #endregion

        #region 从配置文件中创建
        /// <summary>
        /// 根据配置文件创建默认数据库
        /// </summary>
        /// <exception cref="NullReferenceException">配置文件为空或默认数据库设置为空</exception>
        /// <returns>基数据库实体</returns>
        public static AbstractDatabase CreateDatabase()
        {
            DatabaseSettings config = ConfigurationManager.GetDatabaseConfiguration();

            if (config == null || String.IsNullOrEmpty(config.DefaultDatabase))
            {
                throw new NullReferenceException();
            }

            return DatabaseFactory.CreateDatabase(config.DefaultDatabase);
        }

        /// <summary>
        /// 根据配置文件中数据库配置名称创建数据库
        /// </summary>
        /// <param name="databaseName">数据库配置名称</param>
        /// <exception cref="NullReferenceException">数据库配置为空或不存在</exception>
        /// <returns>基数据库实体</returns>
        public static AbstractDatabase CreateDatabase(String databaseName)
        {
            if (SystemConfigurationManager.ConnectionStrings == null)
            {
                throw new NullReferenceException();
            }

            ConnectionStringSettings setting = SystemConfigurationManager.ConnectionStrings[databaseName];

            if (setting == null)
            {
                throw new NullReferenceException();
            }

            return DatabaseFactory.CreateDatabase(DbProviderFactories.GetFactory(setting.ProviderName), setting.ConnectionString);
        }
        #endregion
    }
}