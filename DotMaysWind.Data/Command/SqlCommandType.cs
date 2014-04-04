using System;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql语句类型
    /// </summary>
    public enum SqlCommandType : byte
    {
        /// <summary>
        /// 选择语句
        /// </summary>
        Select = 0,

        /// <summary>
        /// 插入语句
        /// </summary>
        Insert = 1,

        /// <summary>
        /// 更新语句
        /// </summary>
        Update = 2,

        /// <summary>
        /// 删除语句
        /// </summary>
        Delete = 3
    }
}