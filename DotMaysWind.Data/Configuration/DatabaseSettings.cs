using System;
using System.Configuration;

namespace DotMaysWind.Data.Configuration
{
    /// <summary>
    /// 数据库配置类
    /// </summary>
    public class DatabaseSettings : ConfigurationSection
    {
        /// <summary>
        /// 获取或设置默认数据库名称
        /// </summary>
        [ConfigurationProperty("defaultDatabase", IsRequired = true)]
        public String DefaultDatabase
        {
            get { return base["defaultDatabase"] as String; }
            set { base["defaultDatabase"] = value; }
        }
    }
}