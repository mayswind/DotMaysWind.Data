using System;
using System.Linq.Expressions;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Command.Condition;
using DotMaysWind.Data.Linq.Helper;
using DotMaysWind.Data.Orm;

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
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="sourceCommand">来源语句</param>
        /// <param name="expr">Linq表达式</param>
        /// <exception cref="LinqNotSupportedException">Linq操作不支持</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>Sql条件语句</returns>
        public static AbstractSqlCondition Create<T>(AbstractSqlCommand sourceCommand, Expression<Func<T, Boolean>> expr)
        {
            return SqlLinqCondition.ParseCondition(sourceCommand, expr.Body);
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取Sql语句条件
        /// </summary>
        /// <param name="sourceCommand">来源语句</param>
        /// <param name="expr">Linq表达式</param>
        /// <returns>Sql语句条件</returns>
        private static AbstractSqlCondition ParseCondition(AbstractSqlCommand sourceCommand, Expression expr)
        {
            BinaryExpression bexpr = expr as BinaryExpression;

            if (bexpr != null)
            {
                return ParseBinaryExpression(sourceCommand, bexpr);
            }

            MethodCallExpression mcexpr = expr as MethodCallExpression;

            if (mcexpr != null)
            {
                return ParseMethodCallExpression(sourceCommand, mcexpr);
            }

            UnaryExpression uexpr = expr as UnaryExpression;

            if (uexpr != null)
            {
                return ParseUnaryExpression(sourceCommand, uexpr);
            }

            throw new LinqNotSupportedException("Not supported this linq operation!");
        }

        #region 二元运算
        private static AbstractSqlCondition ParseBinaryExpression(AbstractSqlCommand sourceCommand, BinaryExpression expr)
        {
            switch (expr.NodeType)
            {
                case ExpressionType.Equal:
                    return ParseBinaryExpression(sourceCommand, expr, SqlOperator.Equal);
                case ExpressionType.NotEqual:
                    return ParseBinaryExpression(sourceCommand, expr, SqlOperator.NotEqual);
                case ExpressionType.GreaterThan:
                    return ParseBinaryExpression(sourceCommand, expr, SqlOperator.GreaterThan);
                case ExpressionType.LessThan:
                    return ParseBinaryExpression(sourceCommand, expr, SqlOperator.LessThan);
                case ExpressionType.GreaterThanOrEqual:
                    return ParseBinaryExpression(sourceCommand, expr, SqlOperator.GreaterThanOrEqual);
                case ExpressionType.LessThanOrEqual:
                    return ParseBinaryExpression(sourceCommand, expr, SqlOperator.LessThanOrEqual);
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return SqlLinqCondition.ParseCondition(sourceCommand, expr.Left) & SqlLinqCondition.ParseCondition(sourceCommand, expr.Right);
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return SqlLinqCondition.ParseCondition(sourceCommand, expr.Left) | SqlLinqCondition.ParseCondition(sourceCommand, expr.Right);
                default:
                    throw new LinqNotSupportedException("Not supported this linq operation!");
            }
        }

        private static AbstractSqlCondition ParseBinaryExpression(AbstractSqlCommand sourceCommand, BinaryExpression expr, SqlOperator op)
        {
            MemberExpression left = ExpressionHelper.GetMemberExpression(expr.Left);
            String entityName = left.Expression.ToString();
            DatabaseColumnAttribute columnAttr = ExpressionHelper.GetColumnAttributeWithDbType(sourceCommand, left);

            if (columnAttr == null)
            {
                throw new NullAttributeException();
            }

            SqlBasicParameterCondition condition = null;

            if (expr.Right.NodeType == ExpressionType.MemberAccess)//如果右侧为实体属性
            {
                MemberExpression right = expr.Right as MemberExpression;

                if (right.Expression != null && String.Equals(entityName, right.Expression.ToString()))
                {
                    String columnName2 = ExpressionHelper.GetColumnName(sourceCommand, right);

                    condition = SqlCondition.InternalCreateColumn(sourceCommand, columnAttr.ColumnName, op, columnName2);

                    return condition;
                }
            }

            Object value = ExpressionHelper.GetExpressionValue(expr.Right);

            if (value == null && (op == SqlOperator.Equal || op == SqlOperator.NotEqual))
            {
                op = (op == SqlOperator.Equal ? SqlOperator.IsNull : SqlOperator.IsNotNull);
            }

            condition = SqlCondition.InternalCreate(sourceCommand, columnAttr.ColumnName, op, columnAttr.DbType.Value, value);

            return condition;
        }
        #endregion

        #region 扩展方法
        private static AbstractSqlCondition ParseMethodCallExpression(AbstractSqlCommand sourceCommand, MethodCallExpression expr)
        {
            switch (expr.Method.Name)
            {
                case "IsNull":
                    return ParseNullExpression(sourceCommand, expr, false);
                case "IsNotNull":
                    return ParseNullExpression(sourceCommand, expr, true);
                case "Like":
                    return ParseLikeCallExpression(sourceCommand, expr, "{0}", false);
                case "LikeAll":
                case "Contains":
                    return ParseLikeCallExpression(sourceCommand, expr, "%{0}%", false);
                case "LikeStartWith":
                case "StartsWith":
                    return ParseLikeCallExpression(sourceCommand, expr, "{0}%", false);
                case "LikeEndWith":
                case "EndsWith":
                    return ParseLikeCallExpression(sourceCommand, expr, "%{0}", false);
                case "NotLike":
                    return ParseLikeCallExpression(sourceCommand, expr, "{0}", true);
                case "NotLikeAll":
                    return ParseLikeCallExpression(sourceCommand, expr, "%{0}%", true);
                case "NotLikeStartWith":
                    return ParseLikeCallExpression(sourceCommand, expr, "{0}%", true);
                case "NotLikeEndWith":
                    return ParseLikeCallExpression(sourceCommand, expr, "%{0}", true);
                case "Between":
                    return ParseBetweenCallExpression(sourceCommand, expr, false, false);
                case "NotBetween":
                    return ParseBetweenCallExpression(sourceCommand, expr, false, true);
                case "BetweenNullable":
                    return ParseBetweenCallExpression(sourceCommand, expr, true, false);
                case "NotBetweenNullable":
                    return ParseBetweenCallExpression(sourceCommand, expr, true, true);
                case "In":
                    return ParseInCallExpression(sourceCommand, expr, false);
                case "NotIn":
                    return ParseInCallExpression(sourceCommand, expr, true);
            }

            throw new LinqNotSupportedException("Not supported this method!");
        }

        private static AbstractSqlCondition ParseNullExpression(AbstractSqlCommand sourceCommand, MethodCallExpression expr, Boolean isNot)
        {
            MemberExpression left = ExpressionHelper.GetMemberExpression(expr.Arguments[0]);
            DatabaseColumnAttribute columnAttr = ExpressionHelper.GetColumnAttributeWithDbType(sourceCommand, left);

            if (columnAttr == null)
            {
                throw new NullAttributeException();
            }

            AbstractSqlCondition condition = SqlCondition.InternalIsNull(sourceCommand, columnAttr.ColumnName, isNot);

            return condition;
        }

        private static AbstractSqlCondition ParseLikeCallExpression(AbstractSqlCommand sourceCommand, MethodCallExpression expr, String format, Boolean isNot)
        {
            MemberExpression left = ExpressionHelper.GetMemberExpression(expr.Arguments[0]);
            DatabaseColumnAttribute columnAttr = ExpressionHelper.GetColumnAttributeWithDbType(sourceCommand, left);
            String value = ExpressionHelper.GetExpressionValue(expr.Arguments[1]) as String;

            if (columnAttr == null)
            {
                throw new NullAttributeException();
            }

            AbstractSqlCondition condition = SqlCondition.InternalLike(sourceCommand, columnAttr.ColumnName, isNot, columnAttr.DbType.Value, String.Format(format, value));

            return condition;
        }

        private static AbstractSqlCondition ParseBetweenCallExpression(AbstractSqlCommand sourceCommand, MethodCallExpression expr, Boolean supportNullable, Boolean isNot)
        {
            MemberExpression left = ExpressionHelper.GetMemberExpression(expr.Arguments[0]);
            DatabaseColumnAttribute columnAttr = ExpressionHelper.GetColumnAttributeWithDbType(sourceCommand, left);
            Object start = ExpressionHelper.GetExpressionValue(expr.Arguments[1]) as Object;
            Object end = ExpressionHelper.GetExpressionValue(expr.Arguments[2]) as Object;

            if (columnAttr == null)
            {
                throw new NullAttributeException();
            }

            if (supportNullable)
            {
                return SqlCondition.InternalBetweenNullable(sourceCommand, columnAttr.ColumnName, isNot, columnAttr.DbType.Value, start, end);
            }
            else
            {
                return SqlCondition.InternalBetween(sourceCommand, columnAttr.ColumnName, isNot, columnAttr.DbType.Value, start, end);
            }
        }

        private static AbstractSqlCondition ParseInCallExpression(AbstractSqlCommand sourceCommand, MethodCallExpression expr, Boolean isNot)
        {
            MemberExpression left = ExpressionHelper.GetMemberExpression(expr.Arguments[0]);
            DatabaseColumnAttribute columnAttr = ExpressionHelper.GetColumnAttributeWithDbType(sourceCommand, left);
            Object value = ExpressionHelper.GetExpressionValue(expr.Arguments[1]);

            if (columnAttr == null)
            {
                throw new NullAttributeException();
            }

            Array array = value as Array;

            if (array != null)
            {
                AbstractSqlCondition condition = SqlCondition.InternalIn(sourceCommand, columnAttr.ColumnName, isNot, columnAttr.DbType.Value, array);

                return condition;
            }

            SelectCommand cmd = value as SelectCommand;

            if (cmd != null)
            {
                AbstractSqlCondition condition = SqlCondition.InternalIn(sourceCommand, columnAttr.ColumnName, isNot, cmd);

                return condition;
            }

            throw new LinqNotSupportedException("Not supported this method!");
        }
        #endregion

        #region 一元方法
        private static AbstractSqlCondition ParseUnaryExpression(AbstractSqlCommand sourceCommand, UnaryExpression expr)
        {
            switch (expr.NodeType)
            {
                case ExpressionType.Not:
                    return !ParseCondition(sourceCommand, expr.Operand);
                default:
                    throw new LinqNotSupportedException("Not supported this linq operation!");
            }
        }
        #endregion
        #endregion
    }
}