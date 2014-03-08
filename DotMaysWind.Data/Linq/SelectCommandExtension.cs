using System;
using System.Linq.Expressions;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Command.Join;
using DotMaysWind.Data.Linq.Helper;
using DotMaysWind.Data.Orm;

namespace DotMaysWind.Data.Linq
{
    /// <summary>
    /// 选择语句扩展类
    /// </summary>
    public static class SelectCommandExtension
    {
        #region Having/Where/OrderBy/GroupBy
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

        /// <summary>
        /// 按指定列排序并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="cmd">更新语句</param>
        /// <param name="expr">实体类属性</param>
        /// <param name="orderType">排序类型</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        public static SelectCommand OrderBy<T>(this SelectCommand cmd, Expression<Func<T, Object>> expr, SqlOrderType orderType)
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

            return cmd.OrderBy(attr.ColumnName, orderType);
        }

        /// <summary>
        /// 分组指定字段名并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="cmd">更新语句</param>
        /// <param name="expr">实体类属性</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        public static SelectCommand GroupBy<T>(this SelectCommand cmd, Expression<Func<T, Object>> expr)
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

            return cmd.GroupBy(attr.ColumnName);
        }
        #endregion

        #region Join
        #region JoinTableName
        /// <summary>
        /// 连接语句并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <typeparam name="TAnother">另个表格的实体类类型</typeparam>
        /// <param name="cmd">更新语句</param>
        /// <param name="joinType">连接模式</param>
        /// <param name="currentExpr">当前实体类主键属性</param>
        /// <param name="anotherTableName">另个表格名称</param>
        /// <param name="anotherExpr">另个实体类主键属性</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        public static SelectCommand Join<T, TAnother>(this SelectCommand cmd, SqlJoinType joinType, Expression<Func<T, Object>> currentExpr, String anotherTableName, Expression<Func<T, Object>> anotherExpr)
        {
            MemberExpression currentLeft = ExpressionHelper.GetMemberExpression(currentExpr.Body);
            MemberExpression anotherLeft = ExpressionHelper.GetMemberExpression(anotherExpr.Body);

            if (currentLeft == null || anotherLeft == null)
            {
                throw new ExpressionInvalidException();
            }

            DatabaseColumnAttribute currentAttr = ExpressionHelper.GetColumnAttribute(cmd, currentLeft);
            DatabaseColumnAttribute anotherAttr = ExpressionHelper.GetColumnAttribute(cmd, anotherLeft);

            if (currentAttr == null || anotherAttr == null)
            {
                throw new NullAttributeException();
            }

            return cmd.Join(joinType, currentAttr.ColumnName, anotherTableName, anotherAttr.ColumnName);
        }

        /// <summary>
        /// 内连接语句并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <typeparam name="TAnother">另个表格的实体类类型</typeparam>
        /// <param name="cmd">更新语句</param>
        /// <param name="currentExpr">当前实体类主键属性</param>
        /// <param name="anotherTableName">另个表格名称</param>
        /// <param name="anotherExpr">另个实体类主键属性</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        public static SelectCommand InnerJoin<T, TAnother>(this SelectCommand cmd, Expression<Func<T, Object>> currentExpr, String anotherTableName, Expression<Func<T, Object>> anotherExpr)
        {
            return cmd.Join<T, TAnother>(SqlJoinType.InnerJoin, currentExpr, anotherTableName, anotherExpr);
        }

        /// <summary>
        /// 左连接语句并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <typeparam name="TAnother">另个表格的实体类类型</typeparam>
        /// <param name="cmd">更新语句</param>
        /// <param name="currentExpr">当前实体类主键属性</param>
        /// <param name="anotherTableName">另个表格名称</param>
        /// <param name="anotherExpr">另个实体类主键属性</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        public static SelectCommand LeftJoin<T, TAnother>(this SelectCommand cmd, Expression<Func<T, Object>> currentExpr, String anotherTableName, Expression<Func<T, Object>> anotherExpr)
        {
            return cmd.Join<T, TAnother>(SqlJoinType.LeftJoin, currentExpr, anotherTableName, anotherExpr);
        }

        /// <summary>
        /// 右连接语句并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <typeparam name="TAnother">另个表格的实体类类型</typeparam>
        /// <param name="cmd">更新语句</param>
        /// <param name="currentExpr">当前实体类主键属性</param>
        /// <param name="anotherTableName">另个表格名称</param>
        /// <param name="anotherExpr">另个实体类主键属性</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        public static SelectCommand RightJoin<T, TAnother>(this SelectCommand cmd, Expression<Func<T, Object>> currentExpr, String anotherTableName, Expression<Func<T, Object>> anotherExpr)
        {
            return cmd.Join<T, TAnother>(SqlJoinType.RightJoin, currentExpr, anotherTableName, anotherExpr);
        }

        /// <summary>
        /// 全连接语句并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <typeparam name="TAnother">另个表格的实体类类型</typeparam>
        /// <param name="cmd">更新语句</param>
        /// <param name="currentExpr">当前实体类主键属性</param>
        /// <param name="anotherTableName">另个表格名称</param>
        /// <param name="anotherExpr">另个实体类主键属性</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        public static SelectCommand FullJoin<T, TAnother>(this SelectCommand cmd, Expression<Func<T, Object>> currentExpr, String anotherTableName, Expression<Func<T, Object>> anotherExpr)
        {
            return cmd.Join<T, TAnother>(SqlJoinType.FullJoin, currentExpr, anotherTableName, anotherExpr);
        }
        #endregion

        #region JoinTableCommand
        /// <summary>
        /// 连接语句并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <typeparam name="TAnother">另个表格的实体类类型</typeparam>
        /// <param name="cmd">更新语句</param>
        /// <param name="joinType">连接模式</param>
        /// <param name="currentExpr">当前实体类主键属性</param>
        /// <param name="anotherTableCommand">另个表格命令</param>
        /// <param name="anotherExpr">另个实体类主键属性</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        public static SelectCommand Join<T, TAnother>(this SelectCommand cmd, SqlJoinType joinType, Expression<Func<T, Object>> currentExpr, SelectCommand anotherTableCommand, Expression<Func<T, Object>> anotherExpr)
        {
            MemberExpression currentLeft = ExpressionHelper.GetMemberExpression(currentExpr.Body);
            MemberExpression anotherLeft = ExpressionHelper.GetMemberExpression(anotherExpr.Body);

            if (currentLeft == null || anotherLeft == null)
            {
                throw new ExpressionInvalidException();
            }

            DatabaseColumnAttribute currentAttr = ExpressionHelper.GetColumnAttribute(cmd, currentLeft);
            DatabaseColumnAttribute anotherAttr = ExpressionHelper.GetColumnAttribute(cmd, anotherLeft);

            if (currentAttr == null || anotherAttr == null)
            {
                throw new NullAttributeException();
            }

            return cmd.Join(joinType, currentAttr.ColumnName, anotherTableCommand, anotherAttr.ColumnName);
        }
        
        /// <summary>
        /// 内连接语句并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <typeparam name="TAnother">另个表格的实体类类型</typeparam>
        /// <param name="cmd">更新语句</param>
        /// <param name="currentExpr">当前实体类主键属性</param>
        /// <param name="anotherTableCommand">另个表格命令</param>
        /// <param name="anotherExpr">另个实体类主键属性</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        public static SelectCommand InnerJoin<T, TAnother>(this SelectCommand cmd, Expression<Func<T, Object>> currentExpr, SelectCommand anotherTableCommand, Expression<Func<T, Object>> anotherExpr)
        {
            return cmd.Join<T, TAnother>(SqlJoinType.InnerJoin, currentExpr, anotherTableCommand, anotherExpr);
        }

        /// <summary>
        /// 左连接语句并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <typeparam name="TAnother">另个表格的实体类类型</typeparam>
        /// <param name="cmd">更新语句</param>
        /// <param name="currentExpr">当前实体类主键属性</param>
        /// <param name="anotherTableCommand">另个表格命令</param>
        /// <param name="anotherExpr">另个实体类主键属性</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        public static SelectCommand LeftJoin<T, TAnother>(this SelectCommand cmd, Expression<Func<T, Object>> currentExpr, SelectCommand anotherTableCommand, Expression<Func<T, Object>> anotherExpr)
        {
            return cmd.Join<T, TAnother>(SqlJoinType.LeftJoin, currentExpr, anotherTableCommand, anotherExpr);
        }

        /// <summary>
        /// 右连接语句并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <typeparam name="TAnother">另个表格的实体类类型</typeparam>
        /// <param name="cmd">更新语句</param>
        /// <param name="currentExpr">当前实体类主键属性</param>
        /// <param name="anotherTableCommand">另个表格命令</param>
        /// <param name="anotherExpr">另个实体类主键属性</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        public static SelectCommand RightJoin<T, TAnother>(this SelectCommand cmd, Expression<Func<T, Object>> currentExpr, SelectCommand anotherTableCommand, Expression<Func<T, Object>> anotherExpr)
        {
            return cmd.Join<T, TAnother>(SqlJoinType.RightJoin, currentExpr, anotherTableCommand, anotherExpr);
        }

        /// <summary>
        /// 全连接语句并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <typeparam name="TAnother">另个表格的实体类类型</typeparam>
        /// <param name="cmd">更新语句</param>
        /// <param name="currentExpr">当前实体类主键属性</param>
        /// <param name="anotherTableCommand">另个表格命令</param>
        /// <param name="anotherExpr">另个实体类主键属性</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        public static SelectCommand FullJoin<T, TAnother>(this SelectCommand cmd, Expression<Func<T, Object>> currentExpr, SelectCommand anotherTableCommand, Expression<Func<T, Object>> anotherExpr)
        {
            return cmd.Join<T, TAnother>(SqlJoinType.FullJoin, currentExpr, anotherTableCommand, anotherExpr);
        }
        #endregion
        #endregion
    }
}