using System;

namespace DotMaysWind.Data.Command.Condition
{
    /// <summary>
    /// Sql条件语句连接类型
    /// </summary>
    public enum SqlWhereConcatType : byte
    {
        /// <summary>
        /// 与连接
        /// </summary>
        And = 0,

        /// <summary>
        /// 或连接
        /// </summary>
        Or = 1
    }
}