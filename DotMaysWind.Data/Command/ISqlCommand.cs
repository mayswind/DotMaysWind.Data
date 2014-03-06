using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql语句接口
    /// </summary>
    public interface ISqlCommand
    {
        /// <summary>
        /// 获取语句类型
        /// </summary>
        SqlCommandType CommandType { get; }
        
        /// <summary>
        /// 获取数据库类型
        /// </summary>
        DatabaseType DatabaseType { get; }

        /// <summary>
        /// 获取数据表名称
        /// </summary>
        String TableName { get; }

        /// <summary>
        /// 输出条件语句内容
        /// </summary>
        /// <returns>条件语句内容</returns>
        List<SqlParameter> GetAllParameters();

        /// <summary>
        /// 输出SQL语句
        /// </summary>
        /// <returns>SQL语句</returns>
        String GetSqlCommand();

        /// <summary>
        /// 输出SQL语句
        /// </summary>
        DbCommand ToDbCommand();
    }
}