using System;

namespace DotMaysWind.Data.Command.Join
{
    /// <summary>
    /// Sql语句连接类型
    /// </summary>
    public enum SqlJoinType : byte
    {
        /// <summary>
        /// 内连接
        /// </summary>
        InnerJoin = 0,

        /// <summary>
        /// 左连接
        /// </summary>
        LeftJoin = 1,

        /// <summary>
        /// 右连接
        /// </summary>
        RightJoin = 2,

        /// <summary>
        /// 全连接
        /// </summary>
        FullJoin = 3
    }

    /// <summary>
    /// Sql条件连接类型类
    /// </summary>
    internal static class SqlJoinTypes
    {
        #region 常量
        private static readonly String[] TypeNames = new String[] {
            "INNER JOIN",
            "LEFT JOIN",
            "RIGHT JOIN",
            "FULL JOIN"
        };
        #endregion

        #region 方法
        /// <summary>
        /// 获取连接类型名称
        /// </summary>
        /// <param name="type">连接类型</param>
        /// <returns>连接类型名称</returns>
        internal static String InternalGetTypeName(SqlJoinType type)
        {
            Byte index = (Byte)type;
            return TypeNames[index];
        }
        #endregion
    }
}