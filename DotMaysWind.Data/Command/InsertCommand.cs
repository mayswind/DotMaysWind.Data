using System;
using System.Collections.Generic;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql插入语句类
    /// </summary>
    public sealed class InsertCommand : AbstractSqlCommand
    {
        #region 字段
        /// <summary>
        /// 基础参数组
        /// </summary>
        private List<DataParameter> _insertFields;
        #endregion

        #region 属性
        /// <summary>
        /// 获取语句类型
        /// </summary>
        public override SqlCommandType CommandType
        {
            get { return SqlCommandType.Insert; }
        }

        /// <summary>
        /// 获取要插入的字段
        /// </summary>
        public DataParameter[] InsertFields
        {
            get { return this._insertFields.ToArray(); }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化Sql插入语句类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="rootSource">创建时的根来源</param>
        /// <param name="tableName">数据表名称</param>
        internal InsertCommand(AbstractDatabase database, AbstractSqlCommand rootSource, String tableName)
            : base(database, rootSource, tableName)
        {
            this._insertFields = new List<DataParameter>();
        }
        #endregion

        #region 方法
        #region Then
        /// <summary>
        /// 执行自定义代码而不中断当前语句链
        /// </summary>
        /// <param name="action">待执行的方法</param>
        /// <returns>当前语句</returns>
        public InsertCommand Then(Action<InsertCommand> action)
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
        public T Then<T>(Func<InsertCommand, T> func)
        {
            return func(this);
        }
        #endregion

        #region Set
        /// <summary>
        /// 插入指定参数组并返回当前语句
        /// </summary>
        /// <param name="insertParams">要插入的参数组</param>
        /// <returns>当前语句</returns>
        public InsertCommand Set(params DataParameter[] insertParams)
        {
            if (insertParams != null)
            {
                this._insertFields.AddRange(insertParams);
                this._parameters.AddRange(insertParams);
            }

            return this;
        }

        /// <summary>
        /// 插入指定参数并返回当前语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">内容</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// InsertCommand cmd = db.CreateInsertCommand("tbl_Users")
        ///     .Set("UserID", 1)
        ///     .Set("UserName", "admin");
        /// 
        /// //INSERT INTO tbl_Users (UserID, UserName) VALUES (@UserID, @UserName)
        /// //@UserID = 1
        /// //@UserName = "admin"
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public InsertCommand Set(String columnName, Object value)
        {
            DataParameter parameter = this.CreateDataParameter(columnName, value);
            this._insertFields.Add(parameter);
            this._parameters.Add(parameter);
            
            return this;
        }

        /// <summary>
        /// 插入指定参数并返回当前语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">内容</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// InsertCommand cmd = db.CreateInsertCommand("tbl_Users")
        ///     .Set("UserID", DataType.Int32, 1)
        ///     .Set("UserName", DataType.String, "admin");
        /// 
        /// //INSERT INTO tbl_Users (UserID, UserName) VALUES (@UserID, @UserName)
        /// //@UserID = 1
        /// //@UserName = "admin"
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public InsertCommand Set(String columnName, DataType dataType, Object value)
        {
            DataParameter parameter = this.CreateDataParameter(columnName, dataType, value);
            this._insertFields.Add(parameter);
            this._parameters.Add(parameter);

            return this;
        }

        /// <summary>
        /// 插入指定参数并返回当前语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="function">函数</param>
        /// <exception cref="ArgumentNullException">函数不能为空</exception>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// InsertCommand cmd = db.CreateInsertCommand("tbl_Users")
        ///     .Set("UserID", 1)
        ///     .Set("UserName", "admin")
        ///     .Set("CreateTime", db.Functions.Now());
        /// 
        /// //INSERT INTO tbl_Users (UserID, UserName, CreateTime) VALUES (@UserID, @UserName, GETDATE())
        /// //@UserID = 1
        /// //@UserName = "admin"
        /// //"GETDATE()" will be changed into "NOW()" in Access or MySQL, "SYSDATE" in Oracle, or "DATETIME('NOW')" in SQLite.
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public InsertCommand Set(String columnName, ISqlFunction function)
        {
            if (function == null)
            {
                throw new ArgumentNullException("function");
            }

            this._insertFields.Add(this.CreateDataParameterCustomAction(columnName, function.GetCommandText()));

            if (function.HasParameters)
            {
                this._parameters.AddRange(function.GetAllParameters());
            }

            return this;
        }

        /// <summary>
        /// 插入指定参数并返回当前语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// 
        /// InsertCommand cmd = db.CreateInsertCommand("tbl_Users")
        ///     .Set("UserID", 1)
        ///     .Set("UserName", "admin")
        ///     .Set("UploadCount", 
        ///         "tbl_Uploads", s =>
        ///         {
        ///             s.Query(SqlAggregateFunction.Count)
        ///                 .Where(c => c.Equal("UserID", 1));
        ///         }
        ///     );
        /// 
        /// //INSERT INTO tbl_Users (UserID, UserName, UploadCount) VALUES (@UserID_Insert, @UserName, (SELECT COUNT(*) FROM tbl_Uploads WHERE UserID = @UserID_Select))
        /// //@UserID_Insert = 1
        /// //@UserName = "admin"
        /// //@UserID_Select = 1
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public InsertCommand Set(String columnName, String tableName, Action<SelectCommand> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            SelectCommand command = this._database.InternalCreateSelectCommand((this.RootSource == null ? this : this.RootSource), tableName);
            action(command);

            this._insertFields.Add(this.CreateDataParameterCustomAction(columnName, command.GetCommandText()));

            DataParameter[] parameters = command.GetAllParameters();

            if (parameters != null)
            {
                this._parameters.AddRange(command.GetAllParameters());
            }

            return this;
        }

        /// <summary>
        /// 插入指定参数并返回当前语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>当前语句</returns>
        public InsertCommand Set(String columnName, Action<SelectCommand> action)
        {
            return this.Set(columnName, this.TableName, action);
        }
        #endregion

        #region SetWithCondition
        /// <summary>
        /// 插入指定参数并返回当前语句
        /// </summary>
        /// <param name="condition">插入条件（当满足条件时插入该指定字段）</param>
        /// <param name="columnName">字段名</param>
        /// <param name="value">内容</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// Boolean insertPhoto = true;
        /// 
        /// InsertCommand cmd = db.CreateInsertCommand("tbl_Users")
        ///     .Set("UserID", 1)
        ///     .Set("UserName", "admin");
        ///     .Set((() => { return insertPhoto; }), "PhotoUrl", "url");
        /// 
        /// //INSERT INTO tbl_Users (UserID, UserName, PhotoUrl) VALUES (@UserID, @UserName, @PhotoUrl)
        /// //@UserID = 1
        /// //@UserName = "admin"
        /// //@PhotoUrl = "url"
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public InsertCommand Set(Func<Boolean> condition, String columnName, Object value)
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
        /// 插入指定参数并返回当前语句
        /// </summary>
        /// <param name="condition">插入条件（当满足条件时插入该指定字段）</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">内容</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// Boolean insertPhoto = true;
        /// 
        /// InsertCommand cmd = db.CreateInsertCommand("tbl_Users")
        ///     .Set("UserID", DataType.Int32, 1)
        ///     .Set("UserName", DataType.String, "admin");
        ///     .Set((() => { return insertPhoto; }), "PhotoUrl", DataType.String, "url");
        /// 
        /// //INSERT INTO tbl_Users (UserID, UserName, PhotoUrl) VALUES (@UserID, @UserName, @PhotoUrl)
        /// //@UserID = 1
        /// //@UserName = "admin"
        /// //@PhotoUrl = "url"
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public InsertCommand Set(Func<Boolean> condition, String columnName, DataType dataType, Object value)
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
        /// 插入指定参数并返回当前语句
        /// </summary>
        /// <param name="condition">插入条件（当满足条件时插入该指定字段）</param>
        /// <param name="columnName">字段名</param>
        /// <param name="function">函数</param>
        /// <exception cref="ArgumentNullException">函数不能为空</exception>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// Boolean insertTime = true;
        /// 
        /// InsertCommand cmd = db.CreateInsertCommand("tbl_Users")
        ///     .Set("UserID", 1)
        ///     .Set("UserName", "admin")
        ///     .Set((() => { return insertTime; }), "CreateTime", db.Functions.Now());
        /// 
        /// //INSERT INTO tbl_Users (UserID, UserName, CreateTime) VALUES (@UserID, @UserName, GETDATE())
        /// //@UserID = 1
        /// //@UserName = "admin"
        /// //"GETDATE()" will be changed into "NOW()" in Access or MySQL, "SYSDATE" in Oracle, or "DATETIME('NOW')" in SQLite.
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public InsertCommand Set(Func<Boolean> condition, String columnName, ISqlFunction function)
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
        /// 插入指定参数并返回当前语句
        /// </summary>
        /// <param name="condition">更新条件（当满足条件时更新该指定字段）</param>
        /// <param name="columnName">字段名</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// Boolean insertCount = true;
        /// 
        /// InsertCommand cmd = db.CreateInsertCommand("tbl_Users")
        ///     .Set("UserID", 1)
        ///     .Set("UserName", "admin")
        ///     .Set((() => { return insertCount; }), "UploadCount", 
        ///         "tbl_Uploads", s =>
        ///         {
        ///             s.Query(SqlAggregateFunction.Count)
        ///                 .Where(c => c.Equal("UserID", 1));
        ///         }
        ///     );
        /// 
        /// //INSERT INTO tbl_Users (UserID, UserName, UploadCount) VALUES (@UserID_Insert, @UserName, (SELECT COUNT(*) FROM tbl_Uploads WHERE UserID = @UserID_Select))
        /// //@UserID_Insert = 1
        /// //@UserName = "admin"
        /// //@UserID_Select = 1
        /// 
        /// Boolean success = cmd.Result() > 0;
        /// ]]>
        /// </code>
        /// </example>
        public InsertCommand Set(Func<Boolean> condition, String columnName, String tableName, Action<SelectCommand> action)
        {
            if (condition())
            {
                return this.Set(columnName, tableName, action);
            }
            else
            {
                return this;
            }
        }

        /// <summary>
        /// 插入指定参数并返回当前语句
        /// </summary>
        /// <param name="condition">更新条件（当满足条件时更新该指定字段）</param>
        /// <param name="columnName">字段名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>当前语句</returns>
        public InsertCommand Set(Func<Boolean> condition, String columnName, Action<SelectCommand> action)
        {
            if (condition())
            {
                return this.Set(columnName, action);
            }
            else
            {
                return this;
            }
        }
        #endregion

        #region GetCommandText
        /// <summary>
        /// 获取Sql语句内容
        /// </summary>
        /// <returns>Sql语句内容</returns>
        public override String GetCommandText()
        {
            SqlCommandBuilder sb = new SqlCommandBuilder(this.Database);
            sb.AppendInsertPrefix().AppendTableName(this._tableName);

            if (this._insertFields.Count > 0)
            {
                sb.AppendAllColumnNamesWithParentheses(this._insertFields).AppendInsertValues().AppendAllParameterNamesWithParentheses(this._insertFields);
            }

            return sb.ToString();
        }
        #endregion
        #endregion
    }
}