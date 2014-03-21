using System;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql部分日期类型
    /// </summary>
    public enum SqlDatePartType : byte
    {
        /// <summary>
        /// 年份
        /// </summary>
        Year = 0,

        /// <summary>
        /// 季度
        /// </summary>
        Quarter = 1,

        /// <summary>
        /// 月份
        /// </summary>
        Month = 2,

        /// <summary>
        /// 年中第几周
        /// </summary>
        WeekOfYear = 3,

        /// <summary>
        /// 年中第几天
        /// </summary>
        DayOfYear = 4,

        /// <summary>
        /// 月中第几天
        /// </summary>
        DayOfMonth = 5,

        /// <summary>
        /// 周中第几天
        /// </summary>
        DayOfWeek = 6,

        /// <summary>
        /// 小时
        /// </summary>
        Hour = 7,

        /// <summary>
        /// 分钟
        /// </summary>
        Minute = 8,

        /// <summary>
        /// 秒钟
        /// </summary>
        Second = 9
    }
}