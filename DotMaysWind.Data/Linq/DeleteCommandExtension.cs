using System;
using System.Linq.Expressions;

using DotMaysWind.Data.Command;

namespace DotMaysWind.Data.Linq
{
    /// <summary>
    /// 删除语句扩展类
    /// </summary>
    public static class DeleteCommandExtension
    {
        /// <summary>
        /// 设置指定查询的语句并返回当前语句
        /// </summary>
        /// <param name="cmd">删除语句</param>
        /// <param name="expr">Linq表达式</param>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <exception cref="LinqNotSupportedException">Linq操作不支持</exception>
        /// <returns>当前语句</returns>
        public static DeleteCommand Where<T>(this DeleteCommand cmd, Expression<Func<T, Boolean>> expr)
        {
            return cmd.Where(SqlLinqCondition.Create<T>(expr));
        }
    }
}