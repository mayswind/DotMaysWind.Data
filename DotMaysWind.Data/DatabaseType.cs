using System;

namespace DotMaysWind.Data
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DatabaseType : byte
    {
        /// <summary>
        /// 未知数据库类型
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Microsoft Access
        /// </summary>
        Access = 1,

        /// <summary>
        /// Microsoft Sql Server Compact Edition
        /// </summary>
        SqlServerCe = 2,

        /// <summary>
        /// Microsoft Sql Server
        /// </summary>
        SqlServer = 3,

        /// <summary>
        /// SQLite
        /// </summary>
        SQLite = 4,

        /// <summary>
        /// MySQL
        /// </summary>
        MySQL = 5,

        /// <summary>
        /// Oracle
        /// </summary>
        Oracle = 6
    }
}