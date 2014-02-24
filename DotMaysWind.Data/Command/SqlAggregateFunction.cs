using System;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql语言合计函数
    /// </summary>
    public enum SqlAggregateFunction : byte
    {
        /// <summary>
        /// 计数函数
        /// </summary>
        Count = 0,

        /// <summary>
        /// 求和函数
        /// </summary>
        Sum = 1,

        /// <summary>
        /// 最大值函数
        /// </summary>
        Max = 2,

        /// <summary>
        /// 最小值函数
        /// </summary>
        Min = 3,

        /// <summary>
        /// 平均函数
        /// </summary>
        Avg = 4,

        /// <summary>
        /// 第一个函数
        /// </summary>
        First = 5,

        /// <summary>
        /// 最后一个函数
        /// </summary>
        Last = 6,

        /// <summary>
        /// 给定样本的标准偏差函数
        /// </summary>
        Stdev = 7,

        /// <summary>
        /// 样本总体的标准偏差函数
        /// </summary>
        Stdevp = 8,

        /// <summary>
        /// 给定样本的方差函数
        /// </summary>
        Var = 9,

        /// <summary>
        /// 样本总体的方差函数
        /// </summary>
        Varp = 10
    }
}