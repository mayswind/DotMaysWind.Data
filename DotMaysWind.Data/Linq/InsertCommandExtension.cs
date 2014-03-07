using System;
using System.Linq.Expressions;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Linq.Helper;
using DotMaysWind.Data.Orm;

namespace DotMaysWind.Data.Linq
{
    /// <summary>
    /// 插入语句扩展类
    /// </summary>
    public static class InsertCommandExtension
    {
        /// <summary>
        /// 插入指定参数并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="cmd">插入语句</param>
        /// <param name="expr">实体类属性</param>
        /// <param name="value">内容</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        public static InsertCommand Add<T>(this InsertCommand cmd, Expression<Func<T, Object>> expr, Object value)
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

            if (attr.DbType.HasValue)
            {
                return cmd.Add(attr.ColumnName, attr.DbType.Value, value);
            }
            else
            {
                return cmd.Add(attr.ColumnName, value);
            }
        }
    }
}