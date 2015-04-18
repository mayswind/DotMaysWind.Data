using System;
using System.Collections.Generic;

using DotMaysWind.Data.Command.Condition;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql更新语句类
    /// </summary>
    public sealed class UpdateCommand : AbstractSqlCommandWithWhere
    {
        #region 字段
        /// <summary>
        /// 基础参数组
        /// </summary>
        private List<DataParameter> _updateFields;
        #endregion

        #region 属性
        /// <summary>
        /// 获取语句类型
        /// </summary>
        public override SqlCommandType CommandType
        {
            get { return SqlCommandType.Update; }
        }

        /// <summary>
        /// 获取要更新的字段
        /// </summary>
        public DataParameter[] UpdateFields
        {
            get { return this._updateFields.ToArray(); }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化Sql更新语句类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="tableName">数据表名称</param>
        internal UpdateCommand(AbstractDatabase database, String tableName)
            : base(database, tableName)
        {
            this._updateFields = new List<DataParameter>();
        }
        #endregion

        #region 方法
        #region Then
        /// <summary>
        /// 执行自定义代码而不中断当前语句链
        /// </summary>
        /// <param name="action">待执行的方法</param>
        /// <returns>当前语句</returns>
        public UpdateCommand Then(Action<UpdateCommand> action)
        {
            action(this);

            return this;
        }

        /// <summary>
        /// 执行自定义代码而不中断当前语句链
        /// </summary>
        /// <param name="func">待执行的方法</param>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <returns>自定义返回结果</returns>
        public T Then<T>(Func<UpdateCommand, T> func)
        {
            return func(this);
        }
        #endregion

        #region Set
        /// <summary>
        /// 更新指定参数组并返回当前语句
        /// </summary>
        /// <param name="updateParams">要更新的参数组</param>
        /// <returns>当前语句</returns>
        public UpdateCommand Set(params DataParameter[] updateParams)
        {
            if (updateParams != null)
            {
                this._updateFields.AddRange(updateParams);
                this._parameters.AddRange(updateParams);
            }

            return this;
        }

        /// <summary>
        /// 更新指定参数并返回当前语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">内容</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// UpdateCommand cmd = db.CreateUpdateCommand("tbl_Users")
        ///     .Set("UserName", "admin")
        ///     .Where(c => c.Equal("UserID", 1));
        /// 
        /// //UPDATE tbl_Users SET UserName = @UserName WHERE UserID = @UserID
        /// //@UserName = "admin"
        /// //@UserID = 1
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public UpdateCommand Set(String columnName, Object value)
        {
            DataParameter parameter = this.CreateDataParameter(columnName, value);
            this._updateFields.Add(parameter);
            this._parameters.Add(parameter);
            
            return this;
        }

        /// <summary>
        /// 更新指定参数并返回当前语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">内容</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// UpdateCommand cmd = db.CreateUpdateCommand("tbl_Users")
        ///     .Set("UserName", DataType.String, "admin");
        ///     .Where(c => c.Equal("UserID", 1));
        /// 
        /// //UPDATE tbl_Users SET UserName = @UserName WHERE UserID = @UserID
        /// //@UserName = "admin"
        /// //@UserID = 1
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public UpdateCommand Set(String columnName, DataType dataType, Object value)
        {
            DataParameter parameter = this.CreateDataParameter(columnName, dataType, value);
            this._updateFields.Add(parameter);
            this._parameters.Add(parameter);

            return this;
        }

        /// <summary>
        /// 更新指定参数并返回当前语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="function">函数</param>
        /// <exception cref="ArgumentNullException">函数不能为空</exception>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// UpdateCommand cmd = db.CreateUpdateCommand("tbl_Users")
        ///     .Set("LastLoginTime", db.Functions.Now());
        ///     .Where(c => c.Equal("UserID", 1));
        /// 
        /// //UPDATE tbl_Users SET LastLoginTime = GETDATE() WHERE UserID = @UserID
        /// //@UserID = 1
        /// //"GETDATE()" will be changed into "NOW()" in Access or MySQL, "SYSDATE" in Oracle, or "DATETIME('NOW')" in SQLite.
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public UpdateCommand Set(String columnName, ISqlFunction function)
        {
            if (function == null)
            {
                throw new ArgumentNullException("function");
            }

            this._updateFields.Add(this.CreateDataParameterCustomAction(columnName, function.GetCommandText()));

            if (function.HasParameters)
            {
                this._parameters.AddRange(function.GetAllParameters());
            }

            return this;
        }

        /// <summary>
        /// 更新指定参数并返回当前语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="command">选择语句</param>
        /// <exception cref="ArgumentNullException">选择语句不能为空</exception>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand countCmd = db.CreateSelectCommand("tbl_Uploads")
        ///     .Query(SqlAggregateFunction.Count)
        ///     .Where(c => c.Equal("UserID", 1));
        /// 
        /// UpdateCommand cmd = db.CreateUpdateCommand("tbl_Users")
        ///     .Set("UserName", "admin")
        ///     .Set("UploadCount", countCmd)
        ///     .Where(c => c.Equal("UserID", 1));
        /// 
        /// //UPDATE tbl_Users SET UserName = @UserName, UploadCount = (SELECT COUNT(*) FROM tbl_Uploads WHERE UserID = @UserID_Select) WHERE UserID = @UserID_Update
        /// //@UserName = "admin"
        /// //@UserID_Select = 1
        /// //@UserID_Update = 1
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public UpdateCommand Set(String columnName, SelectCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            this._updateFields.Add(this.CreateDataParameterCustomAction(columnName, command.GetCommandText()));

            DataParameter[] parameters = command.GetAllParameters();

            if (parameters != null)
            {
                this._parameters.AddRange(command.GetAllParameters());
            }

            return this;
        }
        #endregion

        #region SetWithCondition
        /// <summary>
        /// 更新指定参数并返回当前语句
        /// </summary>
        /// <param name="condition">更新条件（当满足条件时更新该指定字段）</param>
        /// <param name="columnName">字段名</param>
        /// <param name="value">内容</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// Boolean updatePassword = true;
        /// 
        /// UpdateCommand cmd = db.CreateUpdateCommand("tbl_Users")
        ///     .Set("UserName", "admin");
        ///     .Set((() => { return updatePassword; }), "Password", "newpassword");
        ///     .Where(c => c.Equal("UserID", 1));
        /// 
        /// //UPDATE tbl_Users SET UserName = @UserName, Password = @Password WHERE UserID = @UserID
        /// //@UserName = "admin"
        /// //@Password = "newpassword"
        /// //@UserID = 1
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public UpdateCommand Set(Func<Boolean> condition, String columnName, Object value)
        {
            if (condition())
            {
                return this.Set(columnName, value);
            }
            else
            {
                return this;
            }
        }

        /// <summary>
        /// 更新指定参数并返回当前语句
        /// </summary>
        /// <param name="condition">更新条件（当满足条件时更新该指定字段）</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">内容</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// Boolean updatePassword = true;
        /// 
        /// UpdateCommand cmd = db.CreateUpdateCommand("tbl_Users")
        ///     .Set("UserName", DataType.String, "admin");
        ///     .Set((() => { return updatePassword; }), "Password", DataType.String, "newpassword");
        ///     .Where(c => c.Equal("UserID", 1));
        /// 
        /// //UPDATE tbl_Users SET UserName = @UserName, Password = @Password WHERE UserID = @UserID
        /// //@UserName = "admin"
        /// //@Password = "newpassword"
        /// //@UserID = 1
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public UpdateCommand Set(Func<Boolean> condition, String columnName, DataType dataType, Object value)
        {
            if (condition())
            {
                return this.Set(columnName, dataType, value);
            }
            else
            {
                return this;
            }
        }

        /// <summary>
        /// 更新指定参数并返回当前语句
        /// </summary>
        /// <param name="condition">更新条件（当满足条件时更新该指定字段）</param>
        /// <param name="columnName">字段名</param>
        /// <param name="function">函数</param>
        /// <exception cref="ArgumentNullException">函数不能为空</exception>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// Boolean updateTime = true;
        /// 
        /// UpdateCommand cmd = db.CreateUpdateCommand("tbl_Users")
        ///     .Set((() => { return updateTime; }), "LastLoginTime", db.Functions.Now());
        ///     .Where(c => c.Equal("UserID", 1));
        /// 
        /// //UPDATE tbl_Users SET LastLoginTime = GETDATE() WHERE UserID = @UserID
        /// //@UserID = 1
        /// //"GETDATE()" will be changed into "NOW()" in Access or MySQL, "SYSDATE" in Oracle, or "DATETIME('NOW')" in SQLite.
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public UpdateCommand Set(Func<Boolean> condition, String columnName, ISqlFunction function)
        {
            if (condition())
            {
                return this.Set(columnName, function);
            }
            else
            {
                return this;
            }
        }

        /// <summary>
        /// 更新指定参数并返回当前语句
        /// </summary>
        /// <param name="condition">更新条件（当满足条件时更新该指定字段）</param>
        /// <param name="columnName">字段名</param>
        /// <param name="command">选择语句</param>
        /// <exception cref="ArgumentNullException">选择语句不能为空</exception>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// Boolean updateCount = true;
        /// 
        /// SelectCommand countCmd = db.CreateSelectCommand("tbl_Uploads")
        ///     .Query(SqlAggregateFunction.Count)
        ///     .Where(c => c.Equal("UserID", 1));
        /// 
        /// UpdateCommand cmd = db.CreateUpdateCommand("tbl_Users")
        ///     .Set("UserName", "admin")
        ///     .Set((() => { return updateCount; }), "UploadCount", countCmd)
        ///     .Where(c => c.Equal("UserID", 1));
        /// 
        /// //UPDATE tbl_Users SET UserName = @UserName, UploadCount = (SELECT COUNT(*) FROM tbl_Uploads WHERE UserID = @UserID_Select) WHERE UserID = @UserID_Update
        /// //@UserName = "admin"
        /// //@UserID_Select = 1
        /// //@UserID_Update = 1
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public UpdateCommand Set(Func<Boolean> condition, String columnName, SelectCommand command)
        {
            if (condition())
            {
                return this.Set(columnName, command);
            }
            else
            {
                return this;
            }
        }
        #endregion

        #region Increase/Decrease
        /// <summary>
        /// 指定字段名自增并返回当前语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// UpdateCommand cmd = db.CreateUpdateCommand("tbl_Users")
        ///     .Increase("LoginTimes")
        ///     .Where(c => c.Equal("UserID", 1));
        /// 
        /// //UPDATE tbl_Users SET LoginTimes = LoginTimes + 1 WHERE UserID = @UserID
        /// //@UserID = 1
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public UpdateCommand Increase(String columnName)
        {
            this._updateFields.Add(this.CreateDataParameterCustomAction(columnName, columnName + "+1"));

            return this;
        }

        /// <summary>
        /// 指定字段名自减并返回当前语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// UpdateCommand cmd = db.CreateUpdateCommand("tbl_Users")
        ///     .Decrease("LoginTimes")
        ///     .Where(c => c.Equal("UserID", 1));
        /// 
        /// //UPDATE tbl_Users SET LoginTimes = LoginTimes - 1 WHERE UserID = @UserID
        /// //@UserID = 1
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public UpdateCommand Decrease(String columnName)
        {
            this._updateFields.Add(this.CreateDataParameterCustomAction(columnName, columnName + "-1"));

            return this;
        }
        #endregion

        #region Where
        /// <summary>
        /// 设置指定查询的语句并返回当前语句
        /// </summary>
        /// <param name="where">查询语句</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// UpdateCommand cmd = db.CreateUpdateCommand("tbl_Users")
        ///     .Set("UserName", "admin");
        /// 
        /// cmd.Where(SqlCondition.Equal(cmd, "UserID", 1));
        /// 
        /// //UPDATE tbl_Users SET UserName = @UserName WHERE UserID = @UserID
        /// //@UserName = "admin"
        /// //@UserID = 1
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public UpdateCommand Where(ISqlCondition where)
        {
            this._where = where;

            return this;
        }

        /// <summary>
        /// 设置指定查询的语句并返回当前语句
        /// </summary>
        /// <param name="where">查询语句</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// UpdateCommand cmd = db.CreateUpdateCommand("tbl_Users")
        ///     .Set("UserName", "admin")
        ///     .Where(c => c.Equal("UserID", 1));
        /// 
        /// //UPDATE tbl_Users SET UserName = @UserName WHERE UserID = @UserID
        /// //@UserName = "admin"
        /// //@UserID = 1
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public UpdateCommand Where(Func<SqlConditionBuilder, ISqlCondition> where)
        {
            this._where = where(this._conditionBuilder);

            return this;
        }
        #endregion

        #region GetCommandText
        /// <summary>
        /// 获取Sql语句内容
        /// </summary>
        /// <returns>Sql语句内容</returns>
        public override String GetCommandText()
        {
            SqlCommandBuilder sb = new SqlCommandBuilder(this._database);

            sb.AppendUpdatePrefix().AppendTableName(this._tableName);

            if (this._updateFields.Count > 0)
            {
                sb.AppendUpdateSet();
            }

            sb.AppendAllParameterEquations(this._updateFields).AppendWhere(this._where);

            return sb.ToString();
        }
        #endregion
        #endregion
    }
}