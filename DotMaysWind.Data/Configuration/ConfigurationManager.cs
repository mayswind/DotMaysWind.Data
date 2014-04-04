using System;

namespace DotMaysWind.Data.Configuration
{
    /// <summary>
    /// 数据库配置管理器
    /// </summary>
    internal static class ConfigurationManager
    {
        /// <summary>
        /// 读取数据库配置
        /// </summary>
        /// <returns>数据库配置</returns>
        internal static DatabaseSettings GetDatabaseConfiguration()
        {
            DatabaseSettings config = System.Configuration.ConfigurationManager.GetSection("dataConfiguration") as DatabaseSettings;

            return config;
        }
    }
}