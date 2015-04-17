using System;

namespace DotMaysWind.Data.Command.Join
{
    /// <summary>
    /// Sql连接语句接口
    /// </summary>
    public interface ISqlJoin
    {
        /// <summary>
        /// 获取连接模式
        /// </summary>
        SqlJoinMode JoinMode { get; }

        /// <summary>
        /// 获取连接语句类型
        /// </summary>
        SqlJoinType JoinType { get; }

        /// <summary>
        /// 获取当前表格名称
        /// </summary>
        String CurrentTableName { get; }

        /// <summary>
        /// 获取或设置当前表格主键
        /// </summary>
        String CurrentTableKeyField { get; set; }

        /// <summary>
        /// 获取或设置另个表格主键
        /// </summary>
        String AnotherTableKeyField { get; set; }

        /// <summary>
        /// 获取连接语句参集合
        /// </summary>
        /// <returns>连接语句参集合</returns>
        DataParameter[] GetAllParameters();

        /// <summary>
        /// 获取连接语句内容
        /// </summary>
        /// <returns>连接语句内容</returns>
        String GetClauseText();
    }
}