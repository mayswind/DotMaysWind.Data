using System;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql语句排序类型
    /// </summary>
    public enum SqlOrderType : byte
    {
        /// <summary>
        /// 正序(A-Z)
        /// </summary>
        Asc = 0,

        /// <summary>
        /// 倒序(Z-A)
        /// </summary>
        Desc = 1
    }
}