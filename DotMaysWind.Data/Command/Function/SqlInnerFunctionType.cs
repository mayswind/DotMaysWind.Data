using System;

namespace DotMaysWind.Data.Command.Function
{
    /// <summary>
    /// Sql内置函数类型
    /// </summary>
    public enum SqlInnerFunctionType : byte
    {
        /// <summary>
        /// Sql判空函数
        /// </summary>
        IsNull = 0,

        /// <summary>
        /// Sql大写函数
        /// </summary>
        Upper = 1,

        /// <summary>
        /// Sql小写函数
        /// </summary>
        Lower = 2,

        /// <summary>
        /// Sql去除左边空格函数
        /// </summary>
        LTrim = 3,

        /// <summary>
        /// Sql去除右边空格函数
        /// </summary>
        RTrim = 4,

        /// <summary>
        /// Sql去除两边空格函数
        /// </summary>
        Trim = 5,

        /// <summary>
        /// Sql字符串长度函数
        /// </summary>
        Length = 6,

        /// <summary>
        /// Sql字符串提取函数
        /// </summary>
        Mid = 7,

        /// <summary>
        /// Sql取整函数
        /// </summary>
        Round = 8,

        /// <summary>
        /// Sql当前日期函数
        /// </summary>
        Now = 9,

        /// <summary>
        /// Sql部分日期函数
        /// </summary>
        DatePart = 10
    }
}