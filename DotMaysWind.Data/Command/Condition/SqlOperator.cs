using System;

namespace DotMaysWind.Data.Command.Condition
{
    /// <summary>
    /// Sql条件语句运算符
    /// </summary>
    public enum SqlOperator : byte
    {
        /// <summary>
        /// 判断是否为空
        /// </summary>
        IsNull = 0,//IS NULL

        /// <summary>
        /// 判断是否不为空
        /// </summary>
        IsNotNull = 1,//IS NOT NULL

        /// <summary>
        /// 判断是否氙灯
        /// </summary>
        Equal = 100,//=

        /// <summary>
        /// 判断是否不等
        /// </summary>
        NotEqual = 101,//<>

        /// <summary>
        /// 判断是否大于
        /// </summary>
        GreaterThan = 102,//>

        /// <summary>
        /// 判断是否小于
        /// </summary>
        LessThan = 103,//<

        /// <summary>
        /// 判断是否大于等于
        /// </summary>
        GreaterThanOrEqual = 104,//>=

        /// <summary>
        /// 判断是否小于等于
        /// </summary>
        LessThanOrEqual = 105,//<=

        /// <summary>
        /// 判断是否相似
        /// </summary>
        Like = 106,//LIKE
        
        /// <summary>
        /// 判断是否不相似
        /// </summary>
        NotLike = 107,//NOT LIKE
        
        /// <summary>
        /// 判断是否在指定范围内
        /// </summary>
        Between = 200,//BETWEEN

        /// <summary>
        /// 判断是否不在指定范围内
        /// </summary>
        NotBetween = 201,//NOT BETWEEN
    }

    /// <summary>
    /// Sql条件语句运算符类
    /// </summary>
    internal static class SqlOperators
    {
        #region 常量
        private static readonly Int32[] OperatorTypeTotalSum = new Int32[] {
            0,
            2,
            10
        };

        private static readonly String[] OperatorFormats = new String[] {
            "{0} IS NULL",
            "{0} IS NOT NULL",

            "{0} = {1}",
            "{0} <> {1}",
            "{0} > {1}",
            "{0} < {1}",
            "{0} >= {1}",
            "{0} <= {1}",
            "{0} LIKE {1}",
            "{0} NOT LIKE {1}",

            "{0} BETWEEN {1} AND {2}",
            "{0} NOT BETWEEN {1} AND {2}"
        };
        #endregion

        #region 方法
        /// <summary>
        /// 获取运算符字符串格式
        /// </summary>
        /// <param name="op">运算符</param>
        /// <returns>字符串格式</returns>
        internal static String InternalGetOperatorFormat(SqlOperator op)
        {
            Byte index = (Byte)op;
            return SqlOperators.OperatorFormats[OperatorTypeTotalSum[index / 100] + index % 100];
        }
        #endregion
    }
}