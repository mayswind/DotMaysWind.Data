using System;
using System.Linq.Expressions;

using DotMaysWind.Data.Command.Condition;
using DotMaysWind.Data.Orm.Helper;

namespace DotMaysWind.Data.Linq
{
    /// <summary>
    /// Sql条件语句类
    /// </summary>
    public static class SqlLinqCondition
    {
        #region 方法
        /// <summary>
        /// 创建新的Sql条件语句
        /// </summary>
        /// <param name="expr">Linq表达式</param>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <exception cref="LinqNotSupportedException">Linq操作不支持</exception>
        /// <returns>Sql条件语句</returns>
        public static AbstractSqlCondition Create<T>(Expression<Func<T, Boolean>> expr)
        {
            return SqlLinqCondition.ParseCondition(expr.Body);
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取Sql语句条件
        /// </summary>
        /// <param name="expr">Linq表达式</param>
        /// <returns>Sql语句条件</returns>
        private static AbstractSqlCondition ParseCondition(Expression expr)
        {
            BinaryExpression bexpr = expr as BinaryExpression;

            if (bexpr != null)
            {
                return ParseBinaryExpression(bexpr);
            }

            throw new LinqNotSupportedException("Not supported this linq operation!");
        }

        #region 二元运算
        private static AbstractSqlCondition ParseBinaryExpression(BinaryExpression expr)
        {
            switch (expr.NodeType)
            {
                case ExpressionType.Equal:
                    return ParseBinaryExpression(expr, SqlOperator.Equal);
                case ExpressionType.NotEqual:
                    return ParseBinaryExpression(expr, SqlOperator.NotEqual);
                case ExpressionType.GreaterThan:
                    return ParseBinaryExpression(expr, SqlOperator.GreaterThan);
                case ExpressionType.LessThan:
                    return ParseBinaryExpression(expr, SqlOperator.LessThan);
                case ExpressionType.GreaterThanOrEqual:
                    return ParseBinaryExpression(expr, SqlOperator.GreaterThanOrEqual);
                case ExpressionType.LessThanOrEqual:
                    return ParseBinaryExpression(expr, SqlOperator.LessThanOrEqual);
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return SqlLinqCondition.ParseCondition(expr.Left) & SqlLinqCondition.ParseCondition(expr.Right);
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return SqlLinqCondition.ParseCondition(expr.Left) | SqlLinqCondition.ParseCondition(expr.Right);
                default:
                    throw new LinqNotSupportedException("Not supported this linq operation!");
            }
        }

        private static AbstractSqlCondition ParseBinaryExpression(BinaryExpression expr, SqlOperator op)
        {
            MemberExpression left = SqlLinqCondition.GetMemberExpression(expr.Left);
            String columnName = SqlLinqCondition.GetColumnName(left);
            String entityName = left.Expression.ToString();

            SqlParameter param = null;
            SqlBasicParameterCondition condition = null;

            if (expr.Right.NodeType == ExpressionType.MemberAccess)//如果右侧为实体属性
            {
                MemberExpression right = expr.Right as MemberExpression;

                if (right.Expression != null && String.Equals(entityName, right.Expression.ToString()))
                {
                    String columnName2 = SqlLinqCondition.GetColumnName(right);

                    param = SqlParameter.CreateCustomAction(columnName, columnName2);
                    condition = SqlCondition.Create(param, op);

                    return condition;
                }
            }

            Object value = SqlLinqCondition.GetExpressionValue(expr.Right);

            if (value == null && (op == SqlOperator.Equal || op == SqlOperator.NotEqual))
            {
                op = (op == SqlOperator.Equal ? SqlOperator.IsNull : SqlOperator.IsNotNull);
            }

            param = SqlParameter.Create(columnName, value);
            condition = SqlCondition.Create(param, op);

            return condition;
        }
        #endregion

        #region 通用方法
        private static MemberExpression GetMemberExpression(Expression expr)
        {
            if (expr.NodeType == ExpressionType.Convert)
            {
                expr = (expr as UnaryExpression).Operand;
            }

            MemberExpression mexpr = expr as MemberExpression;

            return mexpr;
        }

        private static String GetColumnName(MemberExpression expr)
        {
            return EntityHelper.InternalGetColumnName(expr.Member.DeclaringType, expr.Member.Name);
        }

        private static Object GetExpressionValue(Expression expr)
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
        #endregion
        #endregion
    }
}