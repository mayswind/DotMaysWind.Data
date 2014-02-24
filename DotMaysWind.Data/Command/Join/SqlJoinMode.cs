using System;

namespace DotMaysWind.Data.Command.Join
{
    /// <summary>
    /// Sql语句连接模式
    /// </summary>
    public enum SqlJoinMode : byte
    {
        /// <summary>
        /// 连接表格名称
        /// </summary>
        JoinTableName = 0,

        /// <summary>
        /// 连接表格查询
        /// </summary>
        JoinTableCommand = 1
    }
}