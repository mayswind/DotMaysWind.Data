using System;
using System.Linq.Expressions;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Command.Condition;
using DotMaysWind.Data.Linq.Helper;
using DotMaysWind.Data.Orm;
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
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="sourceCommand">来源语句</param>
        /// <param name="expr">Linq表达式</param>
        /// <exception cref="LinqNotSupportedException">Linq操作不支持</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>Sql条件语句</returns>
        public static AbstractSqlCondition Create<T>(AbstractSqlCommandWithWhere sourceCommand, Expression<Func<T, Boolean>> expr)
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
        private static AbstractSqlCondition ParseCondition(AbstractSqlCommandWithWhere sourceCommand, Expression expr)
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
        private static AbstractSqlCondition ParseBinaryExpression(AbstractSqlCommandWithWhere sourceCommand, BinaryExpression expr)
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

        private static AbstractSqlCondition ParseBinaryExpression(AbstractSqlCommandWithWhere sourceCommand, BinaryExpression expr, SqlOperator op)
        {
            MemberExpression left = ExpressionHelper.GetMemberExpression(expr.Left);
            String entityName = left.Expression.ToString();
            DatabaseColumnAttribute columnAttr = ExpressionHelper.GetColumnAttributeWithDataType(sourceCommand, left);

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

                    condition = SqlBasicParameterCondition.InternalCreateColumn(sourceCommand, columnAttr.ColumnName, op, columnName2);

                    return condition;
                }
            }

            Object value = ExpressionHelper.GetExpressionValue(expr.Right);

            if (value == null && (op == SqlOperator.Equal || op == SqlOperator.NotEqual))
            {
                op = (op == SqlOperator.Equal ? SqlOperator.IsNull : SqlOperator.IsNotNull);
            }

            condition = SqlBasicParameterCondition.InternalCreate(sourceCommand, columnAttr.ColumnName, op, columnAttr.DataType.Value, value);

            return condition;
        }
        #endregion

        #region 扩展方法
        private static AbstractSqlCondition ParseMethodCallExpression(AbstractSqlCommandWithWhere sourceCommand, MethodCallExpression expr)
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
                    return ParseBetweenCallExpression(sourceCommand, expr, false);
                case "NotBetween":
                    return ParseBetweenCallExpression(sourceCommand, expr, true);
                case "InThese":
                    return ParseInTheseCallExpression(sourceCommand, expr, false);
                case "NotInThese":
                    return ParseInTheseCallExpression(sourceCommand, expr, true);
                case "In":
                    return ParseInCallExpression(sourceCommand, expr, false);
                case "NotIn":
                    return ParseInCallExpression(sourceCommand, expr, true);
            }

            throw new LinqNotSupportedException("Not supported this method!");
        }

        private static AbstractSqlCondition ParseNullExpression(AbstractSqlCommandWithWhere sourceCommand, MethodCallExpression expr, Boolean isNot)
        {
            MemberExpression left = ExpressionHelper.GetMemberExpression(expr.Arguments[0]);
            DatabaseColumnAttribute columnAttr = ExpressionHelper.GetColumnAttributeWithDataType(sourceCommand, left);

            if (columnAttr == null)
            {
                throw new NullAttributeException();
            }

            AbstractSqlCondition condition = null;

            if (isNot)
            {
                condition = sourceCommand.ConditionBuilder.IsNotNull(columnAttr.ColumnName);
            }
            else
            {
                condition = sourceCommand.ConditionBuilder.IsNull(columnAttr.ColumnName);
            }

            return condition;
        }

        private static AbstractSqlCondition ParseLikeCallExpression(AbstractSqlCommandWithWhere sourceCommand, MethodCallExpression expr, String format, Boolean isNot)
        {
            MemberExpression left = ExpressionHelper.GetMemberExpression(expr.Arguments[0]);
            DatabaseColumnAttribute columnAttr = ExpressionHelper.GetColumnAttributeWithDataType(sourceCommand, left);
            String value = ExpressionHelper.GetExpressionValue(expr.Arguments[1]) as String;

            if (columnAttr == null)
            {
                throw new NullAttributeException();
            }

            AbstractSqlCondition condition = null;

            if (isNot)
            {
                condition = sourceCommand.ConditionBuilder.NotLike(columnAttr.ColumnName, columnAttr.DataType.Value, String.Format(format, value)); ;
            }
            else
            {
                condition = sourceCommand.ConditionBuilder.Like(columnAttr.ColumnName, columnAttr.DataType.Value, String.Format(format, value)); ;
            }

            return condition;
        }

        private static AbstractSqlCondition ParseBetweenCallExpression(AbstractSqlCommandWithWhere sourceCommand, MethodCallExpression expr, Boolean isNot)
        {
            MemberExpression left = ExpressionHelper.GetMemberExpression(expr.Arguments[0]);
            DatabaseColumnAttribute columnAttr = ExpressionHelper.GetColumnAttributeWithDataType(sourceCommand, left);
            Object start = ExpressionHelper.GetExpressionValue(expr.Arguments[1]) as Object;
            Object end = ExpressionHelper.GetExpressionValue(expr.Arguments[2]) as Object;

            if (columnAttr == null)
            {
                throw new NullAttributeException();
            }

            AbstractSqlCondition condition = null;

            if (isNot)
            {
                condition = sourceCommand.ConditionBuilder.NotBetweenNullable(columnAttr.ColumnName, columnAttr.DataType.Value, start, end);
            }
            else
            {
                condition = sourceCommand.ConditionBuilder.BetweenNullable(columnAttr.ColumnName, columnAttr.DataType.Value, start, end);
            }

            return condition;
        }

        private static AbstractSqlCondition ParseInTheseCallExpression(AbstractSqlCommandWithWhere sourceCommand, MethodCallExpression expr, Boolean isNot)
        {
            MemberExpression left = ExpressionHelper.GetMemberExpression(expr.Arguments[0]);
            DatabaseColumnAttribute columnAttr = ExpressionHelper.GetColumnAttributeWithDataType(sourceCommand, left);
            Array array = ExpressionHelper.GetExpressionValue(expr.Arguments[1]) as Array;

            if (columnAttr == null)
            {
                throw new NullAttributeException();
            }

            AbstractSqlCondition condition = SqlInsideParametersCondition.InternalCreate(sourceCommand, columnAttr.ColumnName, isNot, columnAttr.DataType.Value, array);

            return condition;
        }

        private static AbstractSqlCondition ParseInCallExpression(AbstractSqlCommandWithWhere sourceCommand, MethodCallExpression expr, Boolean isNot)
        {
            MemberExpression left = ExpressionHelper.GetMemberExpression(expr.Arguments[0]);
            DatabaseColumnAttribute columnAttr = ExpressionHelper.GetColumnAttributeWithDataType(sourceCommand, left);
            Action<SelectCommand> action = ExpressionHelper.GetExpressionValue(expr.Arguments[1]) as Action<SelectCommand>;

            if (columnAttr == null)
            {
                throw new NullAttributeException();
            }

            Type entityType = expr.Method.GetGenericArguments()[0];
            String anotherTableName = EntityHelper.InternalGetTableName(entityType);

            AbstractSqlCondition condition = SqlInsideCommandCondition.InternalCreate(sourceCommand, columnAttr.ColumnName, isNot, anotherTableName, action);

            return condition;
        }
        #endregion

        #region 一元方法
        private static AbstractSqlCondition ParseUnaryExpression(AbstractSqlCommandWithWhere sourceCommand, UnaryExpression expr)
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