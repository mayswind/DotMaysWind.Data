using System;
using System.Linq.Expressions;

using DotMaysWind.Data.Command;

namespace DotMaysWind.Data.Linq
{
    /// <summary>
    /// 选择语句扩展类
    /// </summary>
    public static class SelectCommandExtension
    {
        /// <summary>
        /// 设置指定查询的语句并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="expr">Linq表达式</param>
        /// <exception cref="LinqNotSupportedException">Linq操作不支持</exception>
        /// <returns>当前语句</returns>
        public static SelectCommand Having<T>(this SelectCommand cmd, Expression<Func<T, Boolean>> expr)
        {
            return cmd.Having(SqlLinqCondition.Create<T>(cmd, expr));
        }
        /// <summary>
        /// 设置指定查询的语句并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="expr">Linq表达式</param>
        /// <exception cref="LinqNotSupportedException">Linq操作不支持</exception>
        /// <returns>当前语句</returns>
        public static SelectCommand Where<T>(this SelectCommand cmd, Expression<Func<T, Boolean>> expr)
        {
            return cmd.Where(SqlLinqCondition.Create<T>(cmd, expr));
        }
    }
}