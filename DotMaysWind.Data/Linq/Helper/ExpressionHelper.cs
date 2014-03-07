using System;
using System.Linq.Expressions;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Orm;
using DotMaysWind.Data.Orm.Helper;

namespace DotMaysWind.Data.Linq.Helper
{
    /// <summary>
    /// 表达式辅助类
    /// </summary>
    internal static class ExpressionHelper
    {
        /// <summary>
        /// 获取表达式所指的属性
        /// </summary>
        /// <param name="expr">表达式</param>
        /// <returns>实体属性表达式</returns>
        internal static MemberExpression GetMemberExpression(Expression expr)
        {
            if (expr.NodeType == ExpressionType.Convert)
            {
                expr = (expr as UnaryExpression).Operand;
            }

            MemberExpression mexpr = expr as MemberExpression;

            return mexpr;
        }

        /// <summary>
        /// 获取表达式所指的列特性
        /// </summary>
        /// <param name="sourceCommand">来源Sql语句</param>
        /// <param name="expr">表达式</param>
        /// <returns>表达式所指的列特性</returns>
        internal static DatabaseColumnAtrribute GetColumnAttribute(AbstractSqlCommand sourceCommand, MemberExpression expr)
        {
            DatabaseColumnAtrribute attr = (sourceCommand != null && sourceCommand.SourceDatabaseTable != null ? sourceCommand.SourceDatabaseTable[expr.Member.Name] : null);

            if (attr == null)
            {
                attr = EntityHelper.InternalGetColumnAtrribute(expr.Member.DeclaringType, expr.Member.Name);
            }

            return attr;
        }

        /// <summary>
        /// 获取表达式所指的列名
        /// </summary>
        /// <param name="sourceCommand">来源Sql语句</param>
        /// <param name="expr">表达式</param>
        /// <returns>表达式所指的列名</returns>
        internal static String GetColumnName(AbstractSqlCommand sourceCommand, MemberExpression expr)
        {
            DatabaseColumnAtrribute attr = ExpressionHelper.GetColumnAttribute(sourceCommand, expr);

            return (attr != null ? attr.ColumnName : String.Empty);
        }

        /// <summary>
        /// 获取指定表达式的值
        /// </summary>
        /// <param name="expr">表达式</param>
        /// <returns>表达式的值</returns>
        internal static Object GetExpressionValue(Expression expr)
        {
            if (expr.NodeType == ExpressionType.Constant)
            {
                return (expr as ConstantExpression).Value;
            }
            else
            {
                return Expression.Lambda(expr).Compile().DynamicInvoke();
            }
        }
    }
}