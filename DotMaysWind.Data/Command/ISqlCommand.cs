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
        /// 获取数据表名称
        /// </summary>
        String TableName { get; }

        /// <summary>
        /// 获取所有参数集合
        /// </summary>
        /// <returns>所有参数集合</returns>
        SqlParameter[] GetAllParameters();

        /// <summary>
        /// 获取Sql语句内容
        /// </summary>
        /// <returns>Sql语句内容</returns>
        String GetCommandText();

        /// <summary>
        /// 输出数据库命令
        /// </summary>
        /// <returns>数据库命令</returns>
        DbCommand ToDbCommand();
    }
}