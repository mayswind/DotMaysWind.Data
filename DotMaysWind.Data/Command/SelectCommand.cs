using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using DotMaysWind.Data.Command.Condition;
using DotMaysWind.Data.Command.Join;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql选择语句类
    /// </summary>
    public sealed class SelectCommand : AbstractSqlCommandWithWhere
    {
        #region 常量
        private const Int32 RecordCountEmpty = -1;
        #endregion

        #region 字段
        private Boolean _isFromSql;

        private Int32 _recordCount;
        private Int32 _pageSize;
        private Int32 _recordStart;
        private Boolean _useDistinct;

        private List<SqlQueryField> _queryFields;
        private List<String> _groupbys;
        private List<SqlOrder> _orders;

        private Int32 _joinIndex;
        private List<ISqlJoin> _joins;
        private ISqlCondition _having;
        #endregion

        #region 属性
        /// <summary>
        /// 获取语句类型
        /// </summary>
        public override SqlCommandType CommandType
        {
            get { return SqlCommandType.Select; }
        }

        /// <summary>
        /// 获取是否从Sql语句中查询
        /// </summary>
        internal Boolean IsFromSql
        {
            get { return this._isFromSql; }
        }

        /// <summary>
        /// 获取页面大小
        /// </summary>
        public Int32 PageSize
        {
            get { return this._pageSize; }
            internal set { this._pageSize = value; }
        }

        /// <summary>
        /// 获取记录开始数
        /// </summary>
        public Int32 RecordStart
        {
            get { return this._recordStart; }
            internal set { this._recordStart = value; }
        }

        /// <summary>
        /// 获取或设置是否保证记录唯一
        /// </summary>
        public Boolean UseDistinct
        {
            get { return this._useDistinct; }
            set { this._useDistinct = value; }
        }

        /// <summary>
        /// 获取或设置查询字段名列表
        /// </summary>
        public List<SqlQueryField> QueryFields
        {
            get { return this._queryFields; }
            set { this._queryFields = value; }
        }

        /// <summary>
        /// 获取或设置分组字段名列表
        /// </summary>
        public List<String> GroupByColumns
        {
            get { return this._groupbys; }
            set { this._groupbys = value; }
        }

        /// <summary>
        /// 获取或设置查询排序条件列表
        /// </summary>
        public List<SqlOrder> SqlOrders
        {
            get { return this._orders; }
            set { this._orders = value; }
        }

        /// <summary>
        /// 获取连接索引
        /// </summary>
        private Int32 JoinIndex
        {
            get
            {
                if (this._joinIndex >= Int32.MaxValue)
                {
                    this._joinIndex = 0;
                }

                return this._joinIndex++;
            }
        }

        /// <summary>
        /// 获取或设置连接语句列表
        /// </summary>
        public List<ISqlJoin> SqlJoins
        {
            get { return this._joins; }
            set { this._joins = value; }
        }

        /// <summary>
        /// 获取或设置Having语句
        /// </summary>
        public ISqlCondition SqlHaving
        {
            get { return this._having; }
            set { this._having = value; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化Sql选择语句类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="tableName">数据表名称</param>
        internal SelectCommand(AbstractDatabase database, String tableName)
            : this(database, false, tableName) { }

        /// <summary>
        /// 初始化Sql选择语句类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="from">选择的从Sql语句</param>
        /// <param name="fromAliasesName">从Sql语句的别名</param>
        internal SelectCommand(AbstractDatabase database, SelectCommand from, String fromAliasesName)
            : this(database, true, from.GetCommandText(fromAliasesName)) { }

        /// <summary>
        /// 初始化Sql选择语句类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="isFromSql">是否从Sql语句中选择</param>
        /// <param name="from">数据表或Sql语句</param>
        internal SelectCommand(AbstractDatabase database, Boolean isFromSql, String from)
            : base(database, from)
        {
            this._isFromSql = isFromSql;
            this._queryFields = new List<SqlQueryField>();
            this._groupbys = new List<String>();
            this._orders = new List<SqlOrder>();
            this._joins = new List<ISqlJoin>();

            this._recordCount = SelectCommand.RecordCountEmpty;
            this._pageSize = 0;
            this._recordStart = 0;
            this._joinIndex = 0;
        }
        #endregion

        #region 方法
        #region Query
        #region InternalQuery
        /// <summary>
        /// 查询指定字段并返回当前语句
        /// </summary>
        /// <param name="queryFields">要查询的字段</param>
        /// <returns>当前语句</returns>
        internal SelectCommand InternalQuerys(params SqlQueryField[] queryFields)
        {
            if (queryFields != null)
            {
                this._queryFields.AddRange(queryFields);
            }

            return this;
        }
        #endregion

        #region QueryTableField
        /// <summary>
        /// 查询指定字段名并返回当前语句
        /// </summary>
        /// <param name="queryFields">要查询的字段名集合</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Querys("UserID", "UserName");
        /// 
        /// //SELECT UserID, UserName FROM tbl_Users
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand Querys(params String[] queryFields)
        {
            if (queryFields != null)
            {
                for (Int32 i = 0; i < queryFields.Length; i++)
                {
                    this._queryFields.Add(SqlQueryField.InternalCreateFromColumn(this, queryFields[i]));
                }
            }
            
            return this;
        }

        /// <summary>
        /// 查询指定字段名并返回当前语句
        /// </summary>
        /// <param name="tableName">表格名称</param>
        /// <param name="queryField">要查询的字段名</param>
        /// <param name="aliasesName">字段名的别名</param>
        /// <returns>当前语句</returns>
        public SelectCommand Query(String tableName, String queryField, String aliasesName)
        {
            this._queryFields.Add(SqlQueryField.InternalCreateFromColumn(this, tableName, queryField, aliasesName));
            return this;
        }

        /// <summary>
        /// 查询指定字段名并返回当前语句
        /// </summary>
        /// <param name="queryField">要查询的字段名</param>
        /// <param name="aliasesName">字段名的别名</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Query("UserID")
        ///     .Query("UserName")
        ///     .Query("LastLoginTime", "LastTime");
        /// 
        /// //SELECT UserID, UserName, LastLoginTime AS LastTime FROM tbl_Users
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand Query(String queryField, String aliasesName)
        {
            this._queryFields.Add(SqlQueryField.InternalCreateFromColumn(this, queryField, aliasesName));
            return this;
        }

        /// <summary>
        /// 查询指定字段名并返回当前语句
        /// </summary>
        /// <param name="queryField">要查询的字段名</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Query("UserID")
        ///     .Query("UserName");
        /// 
        /// //SELECT UserID, UserName FROM tbl_Users
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand Query(String queryField)
        {
            this._queryFields.Add(SqlQueryField.InternalCreateFromColumn(this, queryField));
            return this;
        }
        #endregion

        #region QueryTableFieldWithCondition
        /// <summary>
        /// 查询指定字段名并返回当前语句
        /// </summary>
        /// <param name="condition">查询条件（当满足条件时查询该指定字段名）</param>
        /// <param name="queryFields">要查询的字段名集合</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// Boolean getPassword = true;
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Querys("UserID", "UserName")
        ///     .Querys((() => { return getPassword; }), "Password");
        /// 
        /// //SELECT UserID, UserName FROM tbl_Users
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand Querys(Func<Boolean> condition, params String[] queryFields)
        {
            if (condition())
            {
                return this.Querys(queryFields);
            }
            else
            {
                return this;
            }
        }

        /// <summary>
        /// 查询指定字段名并返回当前语句
        /// </summary>
        /// <param name="condition">查询条件（当满足条件时查询该指定字段名）</param>
        /// <param name="tableName">表格名称</param>
        /// <param name="queryField">要查询的字段名</param>
        /// <param name="aliasesName">字段名的别名</param>
        /// <returns>当前语句</returns>
        public SelectCommand Query(Func<Boolean> condition, String tableName, String queryField, String aliasesName)
        {
            if (condition())
            {
                return this.Query(tableName, queryField, aliasesName);
            }
            else
            {
                return this;
            }
        }

        /// <summary>
        /// 查询指定字段名并返回当前语句
        /// </summary>
        /// <param name="condition">查询条件（当满足条件时查询该指定字段名）</param>
        /// <param name="queryField">要查询的字段名</param>
        /// <param name="aliasesName">字段名的别名</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// Boolean getPassword = true;
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Query("UserID")
        ///     .Query("UserName")
        ///     .Query("LastLoginTime", "LastTime")
        ///     .Query((() => { return getPassword; }), "Password", "UserPass");
        /// 
        /// //SELECT UserID, UserName, LastLoginTime AS LastTime FROM tbl_Users
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand Query(Func<Boolean> condition, String queryField, String aliasesName)
        {
            if (condition())
            {
                return this.Query(queryField, aliasesName);
            }
            else
            {
                return this;
            }
        }

        /// <summary>
        /// 查询指定字段名并返回当前语句
        /// </summary>
        /// <param name="condition">查询条件（当满足条件时查询该指定字段名）</param>
        /// <param name="queryField">要查询的字段名</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// Boolean getPassword = true;
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Query("UserID")
        ///     .Query("UserName")
        ///     .Query((() => { return getPassword; }), "Password");
        /// 
        /// //SELECT UserID, UserName FROM tbl_Users
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand Query(Func<Boolean> condition, String queryField)
        {
            if (condition())
            {
                return this.Query(queryField);
            }
            else
            {
                return this;
            }
        }
        #endregion

        #region QueryAggregateFunction
        /// <summary>
        /// 查询指定合计函数并返回当前语句
        /// </summary>
        /// <param name="tableName">表格名称</param>
        /// <param name="function">合计函数类型</param>
        /// <param name="columnName">要查询的字段名</param>
        /// <param name="aliasesName">别名</param>
        /// <returns>当前语句</returns>
        public SelectCommand Query(String tableName, SqlAggregateFunction function, String columnName, String aliasesName)
        {
            this._queryFields.Add(SqlQueryField.InternalCreateFromAggregateFunction(this, tableName, function, columnName, aliasesName));
            return this;
        }

        /// <summary>
        /// 查询指定合计函数并返回当前语句
        /// </summary>
        /// <param name="function">合计函数类型</param>
        /// <param name="columnName">要查询的字段名</param>
        /// <param name="aliasesName">别名</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Query(SqlAggregateFunction.Max, "UserID", "MaxID");
        /// 
        /// //SELECT MAX(UserID) AS MaxID FROM tbl_Users
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand Query(SqlAggregateFunction function, String columnName, String aliasesName)
        {
            this._queryFields.Add(SqlQueryField.InternalCreateFromAggregateFunction(this, function, columnName, aliasesName));
            return this;
        }

        /// <summary>
        /// 查询指定合计函数并返回当前语句
        /// </summary>
        /// <param name="tableName">表格名称</param>
        /// <param name="function">合计函数类型</param>
        /// <param name="columnName">要查询的字段名</param>
        /// <returns>当前语句</returns>
        public SelectCommand Query(String tableName, SqlAggregateFunction function, String columnName)
        {
            this._queryFields.Add(SqlQueryField.InternalCreateFromAggregateFunction(this, tableName, function, columnName));
            return this;
        }

        /// <summary>
        /// 查询指定合计函数并返回当前语句
        /// </summary>
        /// <param name="function">合计函数类型</param>
        /// <param name="columnName">要查询的字段名</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Query(SqlAggregateFunction.Max, "UserID");
        /// 
        /// //SELECT MAX(UserID) FROM tbl_Users
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand Query(SqlAggregateFunction function, String columnName)
        {
            this._queryFields.Add(SqlQueryField.InternalCreateFromAggregateFunction(this, function, columnName));
            return this;
        }

        /// <summary>
        /// 查询指定合计函数并返回当前语句
        /// </summary>
        /// <param name="tableName">表格名称</param>
        /// <param name="function">合计函数类型</param>
        /// <returns>当前语句</returns>
        public SelectCommand Query(String tableName, SqlAggregateFunction function)
        {
            this._queryFields.Add(SqlQueryField.InternalCreateFromAggregateFunction(this, tableName, function));
            return this;
        }

        /// <summary>
        /// 查询指定合计函数并返回当前语句
        /// </summary>
        /// <param name="function">合计函数类型</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Query(SqlAggregateFunction.Count);
        /// 
        /// //SELECT COUNT(*) FROM tbl_Users
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand Query(SqlAggregateFunction function)
        {
            this._queryFields.Add(SqlQueryField.InternalCreateFromAggregateFunction(this, function));
            return this;
        }
        #endregion

        #region QuerySqlFunction
        /// <summary>
        /// 查询指定函数语句并返回当前语句
        /// </summary>
        /// <param name="function">函数</param>
        /// <param name="aliasesName">别名</param>
        /// <exception cref="ArgumentNullException">函数不能为空</exception>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Query("UserID")
        ///     .Query(db.Functions.Length("UserName"), "NameLength");
        /// 
        /// //SELECT UserID, LEN(UserName) FROM tbl_Users
        /// //"LEN()" will be changed into "LENGTH()" in MySQL, Oracle, or SQLite.
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand Query(ISqlFunction function, String aliasesName)
        {
            if (function == null)
            {
                throw new ArgumentNullException("function");
            }

            this._queryFields.Add(SqlQueryField.InternalCreateFromFunction(this, function.GetCommandText(), aliasesName));

            if (function.HasParameters)
            {
                this._parameters.AddRange(function.GetAllParameters());
            }

            return this;
        }

        /// <summary>
        /// 查询指定函数语句并返回当前语句
        /// </summary>
        /// <param name="function">函数</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Query("UserID")
        ///     .Query(db.Functions.Upper("UserName"));
        /// 
        /// //SELECT UserID, UPPER(UserName) FROM tbl_Users
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand Query(ISqlFunction function)
        {
            return this.Query(function, String.Empty);
        }
        #endregion

        #region QuerySelectCommand
        /// <summary>
        /// 查询指定选择语句并返回当前语句
        /// </summary>
        /// <param name="command">选择语句</param>
        /// <param name="aliasesName">别名</param>
        /// <exception cref="ArgumentNullException">选择语句不能为空</exception>
        /// <returns>当前语句</returns>
        public SelectCommand Query(SelectCommand command, String aliasesName)
        {
            if (command == null)
            {
                throw new ArgumentNullException("function");
            }

            this._queryFields.Add(SqlQueryField.InternalCreateFromFunction(this, command.GetCommandText(true), aliasesName));

            SqlParameter[] parameters = command.GetAllParameters();
            if (parameters != null)
            {
                this._parameters.AddRange(parameters);
            }

            return this;
        }
        #endregion

        #region QueryIdentity
        /// <summary>
        /// 查询插入最后一条记录的ID并返回当前语句
        /// </summary>
        /// <exception cref="CommandNotSupportedException">Oracle 数据库不支持获取最后插入记录的标识</exception>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .QueryIdentity();
        /// 
        /// //SELECT @@IDENTITY FROM tbl_Users
        /// //"@@IDENTITY" will be changed into "LAST_INSERT_ROWID()" in SQLite, and Oracle does not support this method.
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand QueryIdentity()
        {
            this._queryFields.Add(SqlQueryField.InternalCreateFromFunction(this, this._database.InternalGetIdentityFieldName()));

            return this;
        }
        #endregion
        #endregion

        #region OrderBy
        /// <summary>
        /// 按指定列升序排序并返回当前语句
        /// </summary>
        /// <param name="columnNames">列名信息</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Querys("UserID", "UserName")
        ///     .OrderByAsc("CreateTime", "UserName");
        /// 
        /// //SELECT UserID, UserName FROM tbl_Users ORDER BY CreateTime, UserName
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand OrderByAsc(params String[] columnNames)
        {
            if (columnNames != null)
            {
                for (Int32 i = 0; i < columnNames.Length; i++)
                {
                    this._orders.Add(SqlOrder.Create(this, columnNames[i], SqlOrderType.Asc));
                }
            }

            return this;
        }

        /// <summary>
        /// 按指定列降序排序并返回当前语句
        /// </summary>
        /// <param name="columnNames">列名信息</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Querys("UserID", "UserName")
        ///     .OrderByDesc("CreateTime", "UserName");
        /// 
        /// //SELECT UserID, UserName FROM tbl_Users ORDER BY CreateTime DESC, UserName DESC
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand OrderByDesc(params String[] columnNames)
        {
            if (columnNames != null)
            {
                for (Int32 i = 0; i < columnNames.Length; i++)
                {
                    this._orders.Add(SqlOrder.Create(this, columnNames[i], SqlOrderType.Desc));
                }
            }

            return this;
        }

        /// <summary>
        /// 按指定列排序并返回当前语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="orderType">排序类型</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Querys("UserID", "UserName")
        ///     .OrderByAsc("UserName", SqlOrderType.Asc);
        /// 
        /// //SELECT UserID, UserName FROM tbl_Users ORDER BY UserName
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand OrderBy(String columnName, SqlOrderType orderType)
        {
            this._orders.Add(SqlOrder.Create(this, columnName, orderType));
            return this;
        }

        /// <summary>
        /// 按指定列排序并返回当前语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="isAscending">是否升序</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Querys("UserID", "UserName")
        ///     .OrderByAsc("UserName", true);
        /// 
        /// //SELECT UserID, UserName FROM tbl_Users ORDER BY UserName
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand OrderBy(String columnName, Boolean isAscending)
        {
            this._orders.Add(SqlOrder.Create(this, columnName, isAscending));
            return this;
        }
        #endregion

        #region Join
        #region JoinTableName
        /// <summary>
        /// 连接语句并返回当前语句
        /// </summary>
        /// <param name="joinType">连接模式</param>
        /// <param name="currentTableField">当前表格主键</param>
        /// <param name="anotherTableName">另个表格名称</param>
        /// <param name="anotherTableField">另个表格主键</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Querys("UserID", "UserName")
        ///     .Join(SqlJoinType.InnerJoin, "UserID", "tbl_Contacts", "UserID");
        /// 
        /// //SELECT UserID, UserName FROM tbl_Users INNER JOIN tbl_Contacts ON tbl_Users.UserID = tbl_Contacts.UserID
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand Join(SqlJoinType joinType, String currentTableField, String anotherTableName, String anotherTableField)
        {
            this._joins.Add(new SqlJoinTableName(this, joinType, this._tableName, currentTableField, anotherTableName, anotherTableField));
            return this;
        }

        /// <summary>
        /// 内连接语句并返回当前语句
        /// </summary>
        /// <param name="currentTableField">当前表格主键</param>
        /// <param name="anotherTableName">另个表格名称</param>
        /// <param name="anotherTableField">另个表格主键</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Querys("UserID", "UserName")
        ///     .InnerJoin("UserID", "tbl_Contacts", "UserID");
        /// 
        /// //SELECT UserID, UserName FROM tbl_Users INNER JOIN tbl_Contacts ON tbl_Users.UserID = tbl_Contacts.UserID
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand InnerJoin(String currentTableField, String anotherTableName, String anotherTableField)
        {
            this._joins.Add(new SqlJoinTableName(this, SqlJoinType.InnerJoin, this._tableName, currentTableField, anotherTableName, anotherTableField));
            return this;
        }

        /// <summary>
        /// 左连接语句并返回当前语句
        /// </summary>
        /// <param name="currentTableField">当前表格主键</param>
        /// <param name="anotherTableName">另个表格名称</param>
        /// <param name="anotherTableField">另个表格主键</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Querys("UserID", "UserName")
        ///     .LeftJoin("UserID", "tbl_Contacts", "UserID");
        /// 
        /// //SELECT UserID, UserName FROM tbl_Users LEFT JOIN tbl_Contacts ON tbl_Users.UserID = tbl_Contacts.UserID
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand LeftJoin(String currentTableField, String anotherTableName, String anotherTableField)
        {
            this._joins.Add(new SqlJoinTableName(this, SqlJoinType.LeftJoin, this._tableName, currentTableField, anotherTableName, anotherTableField));
            return this;
        }

        /// <summary>
        /// 右连接语句并返回当前语句
        /// </summary>
        /// <param name="currentTableField">当前表格主键</param>
        /// <param name="anotherTableName">另个表格名称</param>
        /// <param name="anotherTableField">另个表格主键</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Querys("UserID", "UserName")
        ///     .RightJoin("UserID", "tbl_Contacts", "UserID");
        /// 
        /// //SELECT UserID, UserName FROM tbl_Users RIGHT JOIN tbl_Contacts ON tbl_Users.UserID = tbl_Contacts.UserID
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand RightJoin(String currentTableField, String anotherTableName, String anotherTableField)
        {
            this._joins.Add(new SqlJoinTableName(this, SqlJoinType.RightJoin, this._tableName, currentTableField, anotherTableName, anotherTableField));
            return this;
        }

        /// <summary>
        /// 全连接语句并返回当前语句
        /// </summary>
        /// <param name="currentTableField">当前表格主键</param>
        /// <param name="anotherTableName">另个表格名称</param>
        /// <param name="anotherTableField">另个表格主键</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Querys("UserID", "UserName")
        ///     .FullJoin("UserID", "tbl_Contacts", "UserID");
        /// 
        /// //SELECT UserID, UserName FROM tbl_Users FULL JOIN tbl_Contacts ON tbl_Users.UserID = tbl_Contacts.UserID
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand FullJoin(String currentTableField, String anotherTableName, String anotherTableField)
        {
            this._joins.Add(new SqlJoinTableName(this, SqlJoinType.FullJoin, this._tableName, currentTableField, anotherTableName, anotherTableField));
            return this;
        }
        #endregion

        #region JoinTableCommand
        /// <summary>
        /// 连接语句并返回当前语句
        /// </summary>
        /// <param name="joinType">连接模式</param>
        /// <param name="currentTableField">当前表格主键</param>
        /// <param name="anotherTableCommand">另个表格命令</param>
        /// <param name="anotherTableField">另个表格主键</param>
        /// <returns>当前语句</returns>
        public SelectCommand Join(SqlJoinType joinType, String currentTableField, SelectCommand anotherTableCommand, String anotherTableField)
        {
            this._joins.Add(new SqlJoinTableCommand(this, joinType, this._tableName, currentTableField, anotherTableCommand, this.JoinIndex, anotherTableField));
            return this;
        }

        /// <summary>
        /// 内连接语句并返回当前语句
        /// </summary>
        /// <param name="currentTableField">当前表格主键</param>
        /// <param name="anotherTableCommand">另个表格命令</param>
        /// <param name="anotherTableField">另个表格主键</param>
        /// <returns>当前语句</returns>
        public SelectCommand InnerJoin(String currentTableField, SelectCommand anotherTableCommand, String anotherTableField)
        {
            this._joins.Add(new SqlJoinTableCommand(this, SqlJoinType.InnerJoin, this._tableName, currentTableField, anotherTableCommand, this.JoinIndex, anotherTableField));
            return this;
        }

        /// <summary>
        /// 左连接语句并返回当前语句
        /// </summary>
        /// <param name="currentTableField">当前表格主键</param>
        /// <param name="anotherTableCommand">另个表格命令</param>
        /// <param name="anotherTableField">另个表格主键</param>
        /// <returns>当前语句</returns>
        public SelectCommand LeftJoin(String currentTableField, SelectCommand anotherTableCommand, String anotherTableField)
        {
            this._joins.Add(new SqlJoinTableCommand(this, SqlJoinType.LeftJoin, this._tableName, currentTableField, anotherTableCommand, this.JoinIndex, anotherTableField));
            return this;
        }

        /// <summary>
        /// 右连接语句并返回当前语句
        /// </summary>
        /// <param name="currentTableField">当前表格主键</param>
        /// <param name="anotherTableCommand">另个表格命令</param>
        /// <param name="anotherTableField">另个表格主键</param>
        /// <returns>当前语句</returns>
        public SelectCommand RightJoin(String currentTableField, SelectCommand anotherTableCommand, String anotherTableField)
        {
            this._joins.Add(new SqlJoinTableCommand(this, SqlJoinType.RightJoin, this._tableName, currentTableField, anotherTableCommand, this.JoinIndex, anotherTableField));
            return this;
        }

        /// <summary>
        /// 全连接语句并返回当前语句
        /// </summary>
        /// <param name="currentTableField">当前表格主键</param>
        /// <param name="anotherTableCommand">另个表格命令</param>
        /// <param name="anotherTableField">另个表格主键</param>
        /// <returns>当前语句</returns>
        public SelectCommand FullJoin(String currentTableField, SelectCommand anotherTableCommand, String anotherTableField)
        {
            this._joins.Add(new SqlJoinTableCommand(this, SqlJoinType.FullJoin, this._tableName, currentTableField, anotherTableCommand, this.JoinIndex, anotherTableField));
            return this;
        }
        #endregion
        #endregion

        #region Top/Limit/Paged
        /// <summary>
        /// 设置选择记录数目返回记录唯一并返回当前语句
        /// </summary>
        /// <param name="pageSize">选择记录数量</param>
        /// <exception cref="OverflowException">页面大小不能小于0</exception>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Querys("UserID", "UserName")
        ///     .Top(1);
        /// 
        /// //SELECT TOP 1 UserID, UserName FROM tbl_Users
        /// //"TOP" will be changed into "LIMIT" in MySQL or SQLite, or "ROWNUM" in Oracle.
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand Top(Int32 pageSize)
        {
            if (pageSize < 0)
            {
                throw new OverflowException("Pagesize cannot be less than zero!");
            }

            this._recordCount = SelectCommand.RecordCountEmpty;
            this._pageSize = pageSize;
            this._recordStart = 0;

            return this;
        }

        /// <summary>
        /// 设置分页设置并返回当前语句
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="recordStart">记录起始数（从0开始计算）</param>
        /// <exception cref="OverflowException">页面大小不能小于0</exception>
        /// <returns>当前语句</returns>
        public SelectCommand Limit(Int32 pageSize, Int32 recordStart)
        {
            if (pageSize < 0)
            {
                throw new OverflowException("Pagesize cannot be less than zero!");
            }

            this._recordCount = SelectCommand.RecordCountEmpty;
            this._pageSize = pageSize;
            this._recordStart = recordStart;

            return this;
        }

        /// <summary>
        /// 设置分页设置并返回当前语句
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引（从1开始计算）</param>
        /// <exception cref="OverflowException">页面大小不能小于0</exception>
        /// <returns>当前语句</returns>
        public SelectCommand Paged(Int32 pageSize, Int32 pageIndex)
        {
            if (pageSize < 0)
            {
                throw new OverflowException("Pagesize cannot be less than zero!");
            }

            this._recordCount = SelectCommand.RecordCountEmpty;
            this._pageSize = pageSize;
            this._recordStart = (pageIndex <= 1 ? 0 : pageIndex - 1) * pageSize;

            return this;
        }

        /// <summary>
        /// 设置分页设置并返回当前语句
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引（从1开始计算）</param>
        /// <param name="recordCount">记录数量</param>
        /// <exception cref="OverflowException">记录数量和页面大小不能小于0</exception>
        /// <remarks>Access、SQL Server Ce等需要计算记录总数才能分页的使用此方法效率更高</remarks>
        /// <returns>当前语句</returns>
        public SelectCommand Paged(Int32 pageSize, Int32 pageIndex, Int32 recordCount)
        {
            this.Paged(pageSize, pageIndex);

            if (recordCount < 0)
            {
                throw new OverflowException("Record count cannot be less than zero!");
            }

            this._recordCount = recordCount;

            return this;
        }
        #endregion

        #region Distinct/GroupBy/Having/Where
        /// <summary>
        /// 设置保证返回记录唯一并返回当前语句
        /// </summary>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Querys("UserName")
        ///     .Distinct();
        /// 
        /// //SELECT DISTINCT UserName FROM tbl_Users
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand Distinct()
        {
            this._useDistinct = true;

            return this;
        }

        /// <summary>
        /// 分组指定字段名并返回当前语句
        /// </summary>
        /// <param name="groupBys">要分组的字段名集合</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Query("UserType")
        ///     .Query(SqlAggregateFunction.Count, "UserID", "UserCount")
        ///     .GroupBy("UserType");
        /// 
        /// //SELECT UserType, COUNT(UserID) AS UserCount FROM tbl_Users GROUP BY UserType
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand GroupBy(params String[] groupBys)
        {
            if (groupBys != null)
            {
                this._groupbys.AddRange(groupBys);
            }
            
            return this;
        }

        /// <summary>
        /// 设置指定查询的语句并返回当前语句
        /// </summary>
        /// <param name="having">查询语句</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Query("UserType")
        ///     .Query(SqlAggregateFunction.Count, "UserID", "UserCount")
        ///     .GroupBy("UserType");
        /// 
        /// cmd.Having(SqlCondition.Create(cmd, SqlAggregateFunction.Count, "UserID", SqlOperator.GreaterThanOrEqual, 20));
        /// 
        /// //SELECT UserType, COUNT(UserID) AS UserCount FROM tbl_Users GROUP BY UserType HAVING COUNT(UserID) >= 20
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand Having(ISqlCondition having)
        {
            this._having = having;

            return this;
        }

        /// <summary>
        /// 设置指定查询的语句并返回当前语句
        /// </summary>
        /// <param name="having">查询语句</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Query("UserType")
        ///     .Query(SqlAggregateFunction.Count, "UserID", "UserCount")
        ///     .GroupBy("UserType")
        ///     .Having(c => c.Create(SqlAggregateFunction.Count, "UserID", SqlOperator.GreaterThanOrEqual, 20));
        /// 
        /// //SELECT UserType, COUNT(UserID) AS UserCount FROM tbl_Users GROUP BY UserType HAVING COUNT(UserID) >= 20
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand Having(Func<SqlConditionBuilder, ISqlCondition> having)
        {
            this._where = having(this._conditionBuilder);

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
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Querys("UserID", "UserName");
        /// 
        /// cmd.Where(SqlCondition.Equal(cmd, "UserType", 0) & SqlCondition.LikeAll(cmd, "UserName", "admin"));
        /// 
        /// //SELECT UserID, UserName FROM tbl_Users WHERE UserType = @UserType AND UserName LIKE @UserName
        /// //@UserType = 0
        /// //@UserName = "%admin%"
        /// 
        /// DataRow row = cmd.ToDataRow();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand Where(ISqlCondition where)
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
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users")
        ///     .Querys("UserID", "UserName")
        ///     .Where(c => c.Equal("UserType", 0) & c.LikeAll("UserName", "admin"));
        /// 
        /// //SELECT UserID, UserName FROM tbl_Users WHERE UserType = @UserType AND UserName LIKE @UserName
        /// //@UserType = 0
        /// //@UserName = "%admin%"
        /// 
        /// DataTable table = cmd.ToDataTable();
        /// ]]>
        /// </code>
        /// </example>
        public SelectCommand Where(Func<SqlConditionBuilder, ISqlCondition> where)
        {
            this._where = where(this._conditionBuilder);

            return this;
        }
        #endregion

        #region 输出结果
        #region AggregateFunction
        #region Count
        /// <summary>
        /// 获取指定字段记录数
        /// </summary>
        /// <returns>指定字段记录数</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// Int32 count = db.CreateSelectCommand("tbl_Users")
        ///     .Count();
        /// 
        /// //SELECT COUNT(*) FROM tbl_Users
        /// ]]>
        /// </code>
        /// </example>
        public Int32 Count()
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Count).Result();
        }

        /// <summary>
        /// 获取指定字段记录数
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <returns>指定字段记录数</returns>
        public Int32 Count(DbConnection connection)
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Count).Result(connection);
        }

        /// <summary>
        /// 获取指定字段记录数
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <returns>指定字段记录数</returns>
        public Int32 Count(DbTransaction transaction)
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Count).Result(transaction);
        }

        /// <summary>
        /// 获取指定字段记录数
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <returns>指定字段记录数</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// Int32 count = db.CreateSelectCommand("tbl_Users")
        ///     .Count("UserID");
        /// 
        /// //SELECT COUNT(UserID) FROM tbl_Users
        /// ]]>
        /// </code>
        /// </example>
        public Int32 Count(String columnName)
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Count, columnName).Result();
        }

        /// <summary>
        /// 获取指定字段记录数
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="connection">数据库连接</param>
        /// <returns>指定字段记录数</returns>
        public Int32 Count(String columnName, DbConnection connection)
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Count, columnName).Result(connection);
        }

        /// <summary>
        /// 获取指定字段记录数
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns>指定字段记录数</returns>
        public Int32 Count(String columnName, DbTransaction transaction)
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Count, columnName).Result(transaction);
        }
        #endregion

        #region Count<T>
        /// <summary>
        /// 获取指定字段记录数
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <returns>指定字段记录数</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// Int32 count = db.CreateSelectCommand("tbl_Users")
        ///     .Count<Int32>();
        /// 
        /// //SELECT COUNT(*) FROM tbl_Users
        /// ]]>
        /// </code>
        /// </example>
        public T Count<T>()
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Count).Result<T>();
        }

        /// <summary>
        /// 获取指定字段记录数
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <returns>指定字段记录数</returns>
        public T Count<T>(DbConnection connection)
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Count).Result<T>(connection);
        }

        /// <summary>
        /// 获取指定字段记录数
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="transaction">数据库事务</param>
        /// <returns>指定字段记录数</returns>
        public T Count<T>(DbTransaction transaction)
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Count).Result<T>(transaction);
        }

        /// <summary>
        /// 获取指定字段记录数
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="columnName">字段名称</param>
        /// <returns>指定字段记录数</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// Int32 count = db.CreateSelectCommand("tbl_Users")
        ///     .Count<Int32>("UserID");
        /// 
        /// //SELECT COUNT(UserID) FROM tbl_Users
        /// ]]>
        /// </code>
        /// </example>
        public T Count<T>(String columnName)
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Count, columnName).Result<T>();
        }

        /// <summary>
        /// 获取指定字段记录数
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="columnName">字段名称</param>
        /// <param name="connection">数据库连接</param>
        /// <returns>指定字段记录数</returns>
        public T Count<T>(String columnName, DbConnection connection)
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Count, columnName).Result<T>(connection);
        }

        /// <summary>
        /// 获取指定字段记录数
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="columnName">字段名称</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns>指定字段记录数</returns>
        public T Count<T>(String columnName, DbTransaction transaction)
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Count, columnName).Result<T>(transaction);
        }
        #endregion

        #region Max<T>
        /// <summary>
        /// 获取指定字段最大值
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="columnName">字段名称</param>
        /// <returns>指定字段最大值</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// Int32 maxID = db.CreateSelectCommand("tbl_Users")
        ///     .Max<Int32>("UserID");
        /// 
        /// //SELECT MAX(UserID) FROM tbl_Users
        /// ]]>
        /// </code>
        /// </example>
        public T Max<T>(String columnName)
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Max, columnName).Result<T>();
        }

        /// <summary>
        /// 获取指定字段最大值
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="columnName">字段名称</param>
        /// <param name="connection">数据库连接</param>
        /// <returns>指定字段最大值</returns>
        public T Max<T>(String columnName, DbConnection connection)
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Max, columnName).Result<T>(connection);
        }

        /// <summary>
        /// 获取指定字段最大值
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="columnName">字段名称</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns>指定字段最大值</returns>
        public T Max<T>(String columnName, DbTransaction transaction)
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Max, columnName).Result<T>(transaction);
        }
        #endregion

        #region Min<T>
        /// <summary>
        /// 获取指定字段最小值
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="columnName">字段名称</param>
        /// <returns>指定字段最小值</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// Int32 minID = db.CreateSelectCommand("tbl_Users")
        ///     .Min<Int32>("UserID");
        /// 
        /// //SELECT MIN(UserID) FROM tbl_Users
        /// ]]>
        /// </code>
        /// </example>
        public T Min<T>(String columnName)
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Min, columnName).Result<T>();
        }

        /// <summary>
        /// 获取指定字段最小值
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="columnName">字段名称</param>
        /// <param name="connection">数据库连接</param>
        /// <returns>指定字段最小值</returns>
        public T Min<T>(String columnName, DbConnection connection)
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Min, columnName).Result<T>(connection);
        }

        /// <summary>
        /// 获取指定字段最小值
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="columnName">字段名称</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns>指定字段最小值</returns>
        public T Min<T>(String columnName, DbTransaction transaction)
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Min, columnName).Result<T>(transaction);
        }
        #endregion

        #region Avg<T>
        /// <summary>
        /// 获取指定字段平均值
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="columnName">字段名称</param>
        /// <returns>指定字段平均值</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// Int32 avgID = db.CreateSelectCommand("tbl_Users")
        ///     .Avg<Int32>("UserID");
        /// 
        /// //SELECT AVG(UserID) FROM tbl_Users
        /// ]]>
        /// </code>
        /// </example>
        public T Avg<T>(String columnName)
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Avg, columnName).Result<T>();
        }

        /// <summary>
        /// 获取指定字段平均值
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="columnName">字段名称</param>
        /// <param name="connection">数据库连接</param>
        /// <returns>指定字段平均值</returns>
        public T Avg<T>(String columnName, DbConnection connection)
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Avg, columnName).Result<T>(connection);
        }

        /// <summary>
        /// 获取指定字段平均值
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="columnName">字段名称</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns>指定字段平均值</returns>
        public T Avg<T>(String columnName, DbTransaction transaction)
        {
            this._queryFields.Clear();
            return this.Query(SqlAggregateFunction.Avg, columnName).Result<T>(transaction);
        }
        #endregion
        #endregion

        #region First
        /// <summary>
        /// 获取选择记录结果的第一条数据
        /// </summary>
        /// <returns>数据行</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// DataRow row = db.CreateSelectCommand("tbl_Users")
        ///     .Querys("UserID", "UserName")
        ///     .First();
        /// 
        /// //SELECT TOP 1 UserID, UserName FROM tbl_Users
        /// ]]>
        /// </code>
        /// </example>
        public DataRow First()
        {
            this._pageSize = 1;
            return this._database.ExecuteDataRow(this);
        }

        /// <summary>
        /// 获取选择记录结果的第一条数据
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <returns>数据行</returns>
        public DataRow First(DbConnection connection)
        {
            this._pageSize = 1;
            return this._database.ExecuteDataRow(this, connection);
        }

        /// <summary>
        /// 获取选择记录结果的第一条数据
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <returns>数据行</returns>
        public DataRow First(DbTransaction transaction)
        {
            this._pageSize = 1;
            return this._database.ExecuteDataRow(this, transaction);
        }
        #endregion

        #region Result
        /// <summary>
        /// 获取操作的结果（Select）
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <returns>操作的结果（Select）</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// Int32 maxID = db.CreateSelectCommand("tbl_Users")
        ///     .Query(SqlAggregateFunction.Max, "UserID");
        ///     .Result<Int32>();
        /// 
        /// //SELECT MAX(UserID) FROM tbl_Users
        /// ]]>
        /// </code>
        /// </example>
        public T Result<T>()
        {
            return this._database.ExecuteScalar<T>(this);
        }

        /// <summary>
        /// 获取操作的结果（Select）
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <returns>操作的结果（Select）</returns>
        public T Result<T>(DbConnection connection)
        {
            return this._database.ExecuteScalar<T>(this, connection);
        }

        /// <summary>
        /// 获取操作的结果（Select）
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="transaction">数据库事务</param>
        /// <returns>操作的结果（Select）</returns>
        public T Result<T>(DbTransaction transaction)
        {
            return this._database.ExecuteScalar<T>(this, transaction);
        }
        #endregion

        #region ToDataTable
        /// <summary>
        /// 获取数据表格
        /// </summary>
        /// <returns>数据表格</returns>
        public DataTable ToDataTable()
        {
            return this._database.ExecuteDataTable(this);
        }

        /// <summary>
        /// 获取数据表格
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <returns>数据表格</returns>
        public DataTable ToDataTable(DbConnection connection)
        {
            return this._database.ExecuteDataTable(this, connection);
        }

        /// <summary>
        /// 获取数据表格
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <returns>数据表格</returns>
        public DataTable ToDataTable(DbTransaction transaction)
        {
            return this._database.ExecuteDataTable(this, transaction);
        }
        #endregion

        #region ToDataRow
        /// <summary>
        /// 获取数据行
        /// </summary>
        /// <returns>数据行</returns>
        public DataRow ToDataRow()
        {
            return this._database.ExecuteDataRow(this);
        }

        /// <summary>
        /// 获取数据行
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <returns>数据行</returns>
        public DataRow ToDataRow(DbConnection connection)
        {
            return this._database.ExecuteDataRow(this, connection);
        }

        /// <summary>
        /// 获取数据行
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <returns>数据行</returns>
        public DataRow ToDataRow(DbTransaction transaction)
        {
            return this._database.ExecuteDataRow(this, transaction);
        }
        #endregion

        #region ToDataReader
        /// <summary>
        /// 获取数据读取器
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <returns>数据读取器</returns>
        public IDataReader ToDataReader(DbConnection connection)
        {
            return this._database.ExecuteReader(this, connection);
        }

        /// <summary>
        /// 获取数据读取器
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <returns>数据读取器</returns>
        public IDataReader ToDataReader(DbTransaction transaction)
        {
            return this._database.ExecuteReader(this, transaction);
        }
        #endregion

        #region UsingDataReader
        /// <summary>
        /// 使用数据库读取器执行操作
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="action">使用数据库读取器的操作</param>
        public void UsingDataReader(DbTransaction transaction, Action<IDataReader> action)
        {
            this._database.UsingDataReader(this, transaction, action);
        }

        /// <summary>
        /// 使用数据库读取器执行操作
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="transaction">数据库事务</param>
        /// <param name="function">使用数据库读取器的操作</param>
        /// <returns>返回的内容</returns>
        public T UsingDataReader<T>(DbTransaction transaction, Func<IDataReader, T> function)
        {
            return this._database.UsingDataReader(this, transaction, function);
        }

        /// <summary>
        /// 使用数据库读取器执行操作
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="action">使用数据库读取器的操作</param>
        public void UsingDataReader(DbConnection connection, Action<IDataReader> action)
        {
            this._database.UsingDataReader(this, connection, action);
        }

        /// <summary>
        /// 使用数据库读取器执行操作
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="function">使用数据库读取器的操作</param>
        /// <returns>返回的内容</returns>
        public T UsingDataReader<T>(DbConnection connection, Func<IDataReader, T> function)
        {
            return this._database.UsingDataReader(this, connection, function);
        }

        /// <summary>
        /// 使用数据库读取器执行操作
        /// </summary>
        /// <param name="action">使用数据库读取器的操作</param>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users");
        /// 
        /// List<String> result = new List<String>();
        /// 
        /// cmd.UsingDataReader(r =>
        /// {
        ///     while (r.Read())
        ///     {
        ///         result.Add(r["UserName"] as String);
        ///     }
        /// });
        /// ]]>
        /// </code>
        /// </example>
        public void UsingDataReader(Action<IDataReader> action)
        {
            this._database.UsingDataReader(this, action);
        }

        /// <summary>
        /// 使用数据库读取器执行操作
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="function">使用数据库读取器的操作</param>
        /// <returns>返回的内容</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("tbl_Users");
        /// 
        /// List<String> result = cmd.UsingDataReader(r =>
        /// {
        ///     List<String> list = new List<String>();
        /// 
        ///     while (r.Read())
        ///     {
        ///         list.Add(r["UserName"] as String);
        ///     }
        /// 
        ///     return list;
        /// });
        /// ]]>
        /// </code>
        /// </example>
        public T UsingDataReader<T>(Func<IDataReader, T> function)
        {
            return this._database.UsingDataReader(this, function);
        }
        #endregion
        #endregion

        #region GetCommandText
        /// <summary>
        /// 获取Sql语句内容
        /// </summary>
        /// <returns>Sql语句内容</returns>
        public override String GetCommandText()
        {
            return this.GetCommandText(false, String.Empty, false);
        }

        /// <summary>
        /// 获取Sql语句内容
        /// </summary>
        /// <param name="containParentheses">是否包含括号</param>
        /// <returns>Sql语句内容</returns>
        public String GetCommandText(Boolean containParentheses)
        {
            return this.GetCommandText(containParentheses, String.Empty, false);
        }

        /// <summary>
        /// 获取Sql语句内容
        /// </summary>
        /// <param name="aliasesName">别名</param>
        /// <returns>Sql语句内容</returns>
        public String GetCommandText(String aliasesName)
        {
            return this.GetCommandText(true, aliasesName, false);
        }

        /// <summary>
        /// 获取Sql语句内容
        /// </summary>
        /// <param name="aliasesName">别名</param>
        /// <param name="orderReverse">是否反转排序</param>
        /// <returns>Sql语句内容</returns>
        public String GetCommandText(String aliasesName, Boolean orderReverse)
        {
            return this.GetCommandText(true, aliasesName, orderReverse);
        }

        /// <summary>
        /// 获取Sql语句内容
        /// </summary>
        /// <param name="containParentheses">是否包含括号</param>
        /// <param name="aliasesName">别名</param>
        /// <param name="orderReverse">是否反转排序</param>
        /// <returns>Sql语句内容</returns>
        private String GetCommandText(Boolean containParentheses, String aliasesName, Boolean orderReverse)
        {
            Boolean hasAliasesName = !String.IsNullOrEmpty(aliasesName);
            String sql = (this._recordCount == SelectCommand.RecordCountEmpty ?
                this._database.InternalGetPagerSelectCommand(this, orderReverse) :
                this._database.InternalGetPagerSelectCommand(this, this._recordCount, orderReverse));

            if (containParentheses || hasAliasesName)
            {
                String format = String.Empty;

                if (hasAliasesName)
                {
                    format = "({0}) AS {1}";
                }
                else
                {
                    format = "({0})";
                }

                sql = String.Format(format, sql, aliasesName);
            }

            return sql;
        }
        #endregion

        #region ToDbCommand/GetAllParameters
        /// <summary>
        /// 输出数据库命令
        /// </summary>
        /// <returns>数据库命令</returns>
        public override DbCommand ToDbCommand()
        {
            DbCommand dbCommand = this.CreateDbCommand();

            for (Int32 i = 0; i < this._joins.Count; i++)
            {
                SqlParameter[] joinParameters = (this._joins[i] == null ? null : this._joins[i].GetAllParameters());
                if (joinParameters != null)
                {
                    this._database.AddParameterToDbCommand(dbCommand, joinParameters);
                }
            }

            SqlParameter[] whereParameters = (this._where == null ? null : this._where.GetAllParameters());
            if (whereParameters != null)
            {
                this._database.AddParameterToDbCommand(dbCommand, whereParameters);
            }

            SqlParameter[] havingParameters = (this._having == null ? null : this._having.GetAllParameters());
            if (havingParameters != null)
            {
                this._database.AddParameterToDbCommand(dbCommand, havingParameters);
            }

            return dbCommand;
        }

        /// <summary>
        /// 获取所有参数集合
        /// </summary>
        /// <returns>所有参数集合</returns>
        public override SqlParameter[] GetAllParameters()
        {
            List<SqlParameter> result = new List<SqlParameter>();
            result.AddRange(this._parameters);

            for (Int32 i = 0; i < this._joins.Count; i++)
            {
                SqlParameter[] joinParameters = (this._joins[i] == null ? null : this._joins[i].GetAllParameters());
                if (joinParameters != null)
                {
                    result.AddRange(joinParameters);
                }
            }

            SqlParameter[] whereParameters = (this._where == null ? null : this._where.GetAllParameters());
            if (whereParameters != null)
            {
                result.AddRange(whereParameters);
            }

            SqlParameter[] havingParameters = (this._having == null ? null : this._having.GetAllParameters());
            if (havingParameters != null)
            {
                result.AddRange(havingParameters);
            }

            return result.ToArray();
        }
        #endregion
        #endregion
    }
}