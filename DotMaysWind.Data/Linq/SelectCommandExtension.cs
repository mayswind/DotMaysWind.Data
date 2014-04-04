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
        #region Query
        /// <summary>
        /// 查询指定字段名并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="cmd">查询语句</param>
        /// <param name="expr">实体类属性</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class UserDataProvider : DatabaseTable<User>
        /// {
        ///     //other necessary code
        ///     
        ///     public User GetEntity(Int32 userID)
        ///     {
        ///         return this.Select()
        ///             .Querys<User>(c => new { c.UserID, c.UserName })
        ///             .Where<User>(c => c.UserID == userID)
        ///             .ToEntity<User>(this) > 0;
        ///         
        ///         //SELECT UserID, UserName From tbl_Users WHERE UserID = @UserID
        ///         //@UserID = userID
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static SelectCommand Querys<T>(this SelectCommand cmd, Expression<Func<T, Object>> expr)
        {
            NewExpression left = expr.Body as NewExpression;

            if (left == null)
            {
                throw new ExpressionInvalidException();
            }

            foreach (MemberExpression member in left.Arguments)
            {
                DatabaseColumnAttribute attr = ExpressionHelper.GetColumnAttribute(cmd, member);

                if (attr == null)
                {
                    continue;
                }

                cmd.Query(attr.ColumnName);
            }

            return cmd;
        }

        /// <summary>
        /// 查询指定字段名并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="cmd">查询语句</param>
        /// <param name="expr">实体类属性</param>
        /// <param name="aliasesName">字段名的别名</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class UserDataProvider : DatabaseTable<User>
        /// {
        ///     //other necessary code
        ///     
        ///     public User GetEntity(Int32 userID)
        ///     {
        ///         return this.Select()
        ///             .Query<User>(c => c.UserName, "Name")
        ///             .Where<User>(c => c.UserID == userID)
        ///             .ToEntity<User>(this);
        ///         
        ///         //SELECT UserID, UserName AS Name From tbl_Users WHERE UserID = @UserID
        ///         //@UserID = userID
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static SelectCommand Query<T>(this SelectCommand cmd, Expression<Func<T, Object>> expr, String aliasesName)
        {
            DatabaseColumnAttribute attr = SelectCommandExtension.GetColumnAttribute(cmd, expr.Body);

            return cmd.Query(attr.ColumnName, aliasesName);
        }

        /// <summary>
        /// 查询指定字段名并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="cmd">查询语句</param>
        /// <param name="expr">实体类属性</param>
        /// <param name="function">合计函数类型</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class UserDataProvider : DatabaseTable<User>
        /// {
        ///     //other necessary code
        ///     
        ///     public Int32 Count()
        ///     {
        ///         return this.Select()
        ///             .Query<User>(c => c.UserID, SqlAggregateFunction.Count)
        ///             .Result<Int32>();
        ///         
        ///         //SELECT COUNT(UserID) From tbl_Users
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static SelectCommand Query<T>(this SelectCommand cmd, Expression<Func<T, Object>> expr, SqlAggregateFunction function)
        {
            DatabaseColumnAttribute attr = SelectCommandExtension.GetColumnAttribute(cmd, expr.Body);

            return cmd.Query(function, attr.ColumnName);
        }

        /// <summary>
        /// 查询指定字段名并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="cmd">查询语句</param>
        /// <param name="expr">实体类属性</param>
        /// <param name="function">合计函数类型</param>
        /// <param name="aliasesName">字段名的别名</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class UserDataProvider : DatabaseTable<User>
        /// {
        ///     //other necessary code
        ///     
        ///     public Int32 Count()
        ///     {
        ///         return this.Select()
        ///             .Query<User>(c => c.UserID, SqlAggregateFunction.Count, "UserCount")
        ///             .Result<Int32>();
        ///         
        ///         //SELECT COUNT(UserID) AS UserCount From tbl_Users
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static SelectCommand Query<T>(this SelectCommand cmd, Expression<Func<T, Object>> expr, SqlAggregateFunction function, String aliasesName)
        {
            DatabaseColumnAttribute attr = SelectCommandExtension.GetColumnAttribute(cmd, expr.Body);

            return cmd.Query(function, attr.ColumnName, aliasesName);
        }
        #endregion

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
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class UserDataProvider : DatabaseTable<User>
        /// {
        ///     //other necessary code
        ///     
        ///     public User GetEntity(Int32 userID)
        ///     {
        ///         return this.Select()
        ///             .Querys<User>(c => new { c.UserID, c.UserName })
        ///             .Where<User>(c => c.UserID == userID)
        ///             .ToEntity<User>(this) > 0;
        ///         
        ///         //SELECT UserID, UserName From tbl_Users WHERE UserID = @UserID
        ///         //@UserID = userID
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static SelectCommand Where<T>(this SelectCommand cmd, Expression<Func<T, Boolean>> expr)
        {
            return cmd.Where(SqlLinqCondition.Create<T>(cmd, expr));
        }

        /// <summary>
        /// 按指定列排序并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="cmd">查询语句</param>
        /// <param name="expr">实体类属性</param>
        /// <param name="orderType">排序类型</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class UserDataProvider : DatabaseTable<User>
        /// {
        ///     //other necessary code
        ///     
        ///     public List<User> GetAllEntities()
        ///     {
        ///         return this.Select()
        ///             .Querys<User>(c => new { c.UserID, c.UserName })
        ///             .OrderBy<User>(c => c.UserID, SqlOrderType.Desc)
        ///             .ToEntityList<User>(this);
        ///         
        ///         //SELECT UserID, UserName From tbl_Users ORDER BY UserID DESC
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static SelectCommand OrderBy<T>(this SelectCommand cmd, Expression<Func<T, Object>> expr, SqlOrderType orderType)
        {
            DatabaseColumnAttribute attr = SelectCommandExtension.GetColumnAttribute(cmd, expr.Body);

            return cmd.OrderBy(attr.ColumnName, orderType);
        }

        /// <summary>
        /// 分组指定字段名并返回当前语句
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="cmd">查询语句</param>
        /// <param name="expr">实体类属性</param>
        /// <exception cref="ExpressionInvalidException">表达式不正确</exception>
        /// <exception cref="NullAttributeException">没有设置特性</exception>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class UserDataProvider : DatabaseTable<User>
        /// {
        ///     //other necessary code
        ///     
        ///     public DataTable GetAllEntities()
        ///     {
        ///         return this.Select()
        ///             .Querys<User>(c => new { c.UserType })
        ///             .GroupBy<User>(c => c.UserType)
        ///             .ToDataTable(this);
        ///         
        ///         //SELECT UserType From tbl_Users GROUP BY UserType
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static SelectCommand GroupBy<T>(this SelectCommand cmd, Expression<Func<T, Object>> expr)
        {
            DatabaseColumnAttribute attr = SelectCommandExtension.GetColumnAttribute(cmd, expr.Body);

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
        /// <param name="cmd">查询语句</param>
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
        /// <param name="cmd">查询语句</param>
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
        /// <param name="cmd">查询语句</param>
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
        /// <param name="cmd">查询语句</param>
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
        /// <param name="cmd">查询语句</param>
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
        /// <param name="cmd">查询语句</param>
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
        /// <param name="cmd">查询语句</param>
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
        /// <param name="cmd">查询语句</param>
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
        /// <param name="cmd">查询语句</param>
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
        /// <param name="cmd">查询语句</param>
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

        #region 私有方法
        private static DatabaseColumnAttribute GetColumnAttribute(SelectCommand cmd, Expression expr)
        {
            MemberExpression left = ExpressionHelper.GetMemberExpression(expr);

            if (expr == null)
            {
                throw new ExpressionInvalidException();
            }

            DatabaseColumnAttribute attr = ExpressionHelper.GetColumnAttribute(cmd, left);

            if (attr == null)
            {
                throw new NullAttributeException();
            }

            return attr;
        }
        #endregion
    }
}