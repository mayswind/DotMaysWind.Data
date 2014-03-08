using System;
using System.Linq.Expressions;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Linq.Helper;
using DotMaysWind.Data.Orm;

namespace DotMaysWind.Data.Linq
{
    /// <summary>
    /// 更新语句扩展类
    /// </summary>
    public static class UpdateCommandExtension
    {
        /// <summary>
        /// 设置指定查询的语句并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="cmd">更新语句</param>
        /// <param name="expr">Linq表达式</param>
        /// <exception cref="LinqNotSupportedException">Linq操作不支持</exception>
        /// <returns>当前语句</returns>
        public static UpdateCommand Where<T>(this UpdateCommand cmd, Expression<Func<T, Boolean>> expr)
        {
            return cmd.Where(SqlLinqCondition.Create<T>(cmd, expr));
        }

        /// <summary>
        /// 更新指定参数并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="cmd">更新语句</param>
        /// <param name="expr">实体类属性</param>
        /// <param name="value">内容</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        public static UpdateCommand Set<T>(this UpdateCommand cmd, Expression<Func<T, Object>> expr, Object value)
        {
            MemberExpression left = ExpressionHelper.GetMemberExpression(expr.Body);

            if (left == null)
            {
                throw new ExpressionInvalidException();
            }

            DatabaseColumnAttribute attr = ExpressionHelper.GetColumnAttributeWithDbType(cmd, left);

            if (attr == null)
            {
                throw new NullAttributeException();
            }

            return cmd.Set(attr.ColumnName, attr.DbType.Value, value);
        }

        /// <summary>
        /// 指定字段名自增并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="cmd">更新语句</param>
        /// <param name="expr">实体类属性</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        public static UpdateCommand Increase<T>(this UpdateCommand cmd, Expression<Func<T, Object>> expr)
        {
            MemberExpression left = ExpressionHelper.GetMemberExpression(expr.Body);

            if (left == null)
            {
                throw new ExpressionInvalidException();
            }

            DatabaseColumnAttribute attr = ExpressionHelper.GetColumnAttribute(cmd, left);

            if (attr == null)
            {
                throw new NullAttributeException();
            }

            return cmd.Increase(attr.ColumnName);
        }

        /// <summary>
        /// 指定字段名自减并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="cmd">更新语句</param>
        /// <param name="expr">实体类属性</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        public static UpdateCommand Decrease<T>(this UpdateCommand cmd, Expression<Func<T, Object>> expr)
        {
            MemberExpression left = ExpressionHelper.GetMemberExpression(expr.Body);

            if (left == null)
            {
                throw new ExpressionInvalidException();
            }

            DatabaseColumnAttribute attr = ExpressionHelper.GetColumnAttribute(cmd, left);

            if (attr == null)
            {
                throw new NullAttributeException();
            }

            return cmd.Decrease(attr.ColumnName);
        }
    }
}