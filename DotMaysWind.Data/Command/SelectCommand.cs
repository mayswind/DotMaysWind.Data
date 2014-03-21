using System;
using System.Collections.Generic;
using System.Data.Common;

using DotMaysWind.Data.Command.Condition;
using DotMaysWind.Data.Command.Join;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql选择语句类
    /// </summary>
    public class SelectCommand : AbstractSqlCommandWithWhere
    {
        #region 字段
        private Boolean _isFromSql;

        private Int32 _pageSize;
        private Int32 _pageIndex;
        private Int32 _recordCount;
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
        /// 获取或设置页面大小
        /// </summary>
        public Int32 PageSize
        {
            get { return this._pageSize; }
            set { this._pageSize = value; }
        }

        /// <summary>
        /// 获取或设置当前页码
        /// </summary>
        public Int32 PageIndex
        {
            get { return this._pageIndex; }
            set { this._pageIndex = value; }
        }

        /// <summary>
        /// 获取或设置记录总数
        /// </summary>
        public Int32 RecordCount
        {
            get { return this._recordCount; }
            set { this._recordCount = value; }
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
            : this(database, false, tableName, 0, 0, 0) { }

        /// <summary>
        /// 初始化Sql选择语句类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="from">选择的从Sql语句</param>
        /// <param name="fromAliasesName">从Sql语句的别名</param>
        internal SelectCommand(AbstractDatabase database, SelectCommand from, String fromAliasesName)
            : this(database, true, from.GetSqlCommand(fromAliasesName), 0, 0, 0) { }

        /// <summary>
        /// 初始化Sql选择语句类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="tableName">数据表名称</param>
        /// <param name="pageSize">页面大小</param>
        internal SelectCommand(AbstractDatabase database, String tableName, Int32 pageSize)
            : this(database, false, tableName, pageSize, 0, 0) { }

        /// <summary>
        /// 初始化Sql选择语句类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="from">选择的从Sql语句</param>
        /// <param name="fromAliasesName">从Sql语句的别名</param>
        /// <param name="pageSize">页面大小</param>
        internal SelectCommand(AbstractDatabase database, SelectCommand from, String fromAliasesName, Int32 pageSize)
            : this(database, true, from.GetSqlCommand(fromAliasesName), pageSize, 0, 0) { }

        /// <summary>
        /// 初始化Sql选择语句类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="tableName">数据表名称</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="recordCount">记录总数</param>
        internal SelectCommand(AbstractDatabase database, String tableName, Int32 pageSize, Int32 pageIndex, Int32 recordCount)
            : this(database, false, tableName, pageSize, pageIndex, recordCount) { }

        /// <summary>
        /// 初始化Sql选择语句类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="isFromSql">是否从Sql语句中选择</param>
        /// <param name="from">数据表或Sql语句</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="recordCount">记录总数</param>
        internal SelectCommand(AbstractDatabase database, Boolean isFromSql, String from, Int32 pageSize, Int32 pageIndex, Int32 recordCount)
            : base(database, from)
        {
            this._isFromSql = isFromSql;
            this._queryFields = new List<SqlQueryField>();
            this._groupbys = new List<String>();
            this._orders = new List<SqlOrder>();
            this._joins = new List<ISqlJoin>();

            this._pageSize = pageSize;
            this._pageIndex = pageIndex;
            this._recordCount = recordCount;
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
        public SelectCommand Query(String queryField)
        {
            this._queryFields.Add(SqlQueryField.InternalCreateFromColumn(this, queryField));
            return this;
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
        public SelectCommand Query(ISqlFunction function, String aliasesName)
        {
            if (function == null)
            {
                throw new ArgumentNullException("function");
            }

            this._queryFields.Add(SqlQueryField.InternalCreateFromFunction(this, function.GetSqlText(), aliasesName));

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

            this._queryFields.Add(SqlQueryField.InternalCreateFromFunction(this, command.GetSqlCommand(true), aliasesName));

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
        /// <exception cref="DatabaseNotSupportException">Oracle数据库不支持获取最后一条记录的ID</exception>
        /// <returns>当前语句</returns>
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
        public SelectCommand OrderBy(String columnName, Boolean isAscending)
        {
            this._orders.Add(SqlOrder.Create(this, columnName, isAscending));
            return this;
        }

        /// <summary>
        /// 按指定列升序并返回当前语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <returns>当前语句</returns>
        public SelectCommand OrderBy(String columnName)
        {
            this._orders.Add(SqlOrder.Create(this, columnName));
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

        #region AggregateFunction
        /// <summary>
        /// 获取指定字段记录数
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="columnName">字段名称</param>
        /// <returns>指定字段记录数</returns>
        public T Count<T>(String columnName)
        {
            return this.Query(SqlAggregateFunction.Count, columnName).Result<T>();
        }

        /// <summary>
        /// 获取指定字段记录数
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <returns>指定字段记录数</returns>
        public T Count<T>()
        {
            return this.Query(SqlAggregateFunction.Count).Result<T>();
        }

        /// <summary>
        /// 获取指定字段最大值
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="columnName">字段名称</param>
        /// <returns>指定字段最大值</returns>
        public T Max<T>(String columnName)
        {
            return this.Query(SqlAggregateFunction.Max, columnName).Result<T>();
        }

        /// <summary>
        /// 获取指定字段最小值
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="columnName">字段名称</param>
        /// <returns>指定字段最小值</returns>
        public T Min<T>(String columnName)
        {
            return this.Query(SqlAggregateFunction.Min, columnName).Result<T>();
        }

        /// <summary>
        /// 获取指定字段平均值
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <param name="columnName">字段名称</param>
        /// <returns>指定字段平均值</returns>
        public T Avg<T>(String columnName)
        {
            return this.Query(SqlAggregateFunction.Avg, columnName).Result<T>();
        }
        #endregion

        #region Top First Paged/Distinct/GroupBy/Having/Where
        /// <summary>
        /// 设置选择记录数目返回记录唯一并返回当前语句
        /// </summary>
        /// <param name="pageSize">选择记录数量</param>
        /// <returns>当前语句</returns>
        public SelectCommand Top(Int32 pageSize)
        {
            this._pageSize = pageSize;

            return this;
        }

        /// <summary>
        /// 设置选择记录数目为1返回记录唯一并返回当前语句
        /// </summary>
        /// <returns>当前语句</returns>
        public SelectCommand First()
        {
            this._pageSize = 1;

            return this;
        }

        /// <summary>
        /// 设置分页设置并返回当前语句
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns>当前语句</returns>
        public SelectCommand Paged(Int32 pageSize, Int32 pageIndex, Int32 recordCount)
        {
            this._pageSize = pageSize;
            this._pageIndex = pageIndex;
            this._recordCount = recordCount;

            return this;
        }

        /// <summary>
        /// 设置保证返回记录唯一并返回当前语句
        /// </summary>
        /// <returns>当前语句</returns>
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
        public SelectCommand Where(Func<SqlConditionBuilder, ISqlCondition> where)
        {
            this._where = where(this._conditionBuilder);

            return this;
        }
        #endregion

        #region GetSqlCommand
        /// <summary>
        /// 输出SQL语句
        /// </summary>
        /// <returns>SQL语句</returns>
        public override String GetSqlCommand()
        {
            return this.GetSqlCommand(false, String.Empty, false);
        }

        /// <summary>
        /// 输出SQL语句
        /// </summary>
        /// <param name="containParentheses">是否包含括号</param>
        /// <returns>SQL语句</returns>
        public String GetSqlCommand(Boolean containParentheses)
        {
            return this.GetSqlCommand(containParentheses, String.Empty, false);
        }

        /// <summary>
        /// 输出SQL语句
        /// </summary>
        /// <param name="aliasesName">别名</param>
        /// <returns>SQL语句</returns>
        public String GetSqlCommand(String aliasesName)
        {
            return this.GetSqlCommand(true, aliasesName, false);
        }

        /// <summary>
        /// 输出SQL语句
        /// </summary>
        /// <param name="aliasesName">别名</param>
        /// <param name="orderReverse">是否反转排序</param>
        /// <returns>SQL语句</returns>
        public String GetSqlCommand(String aliasesName, Boolean orderReverse)
        {
            return this.GetSqlCommand(true, aliasesName, orderReverse);
        }

        /// <summary>
        /// 输出SQL语句
        /// </summary>
        /// <param name="containParentheses">是否包含括号</param>
        /// <param name="aliasesName">别名</param>
        /// <param name="orderReverse">是否反转排序</param>
        /// <returns>SQL语句</returns>
        private String GetSqlCommand(Boolean containParentheses, String aliasesName, Boolean orderReverse)
        {
            Int32 pageCount = (this._pageSize == 0 ? 1 : (this._recordCount + this._pageSize - 1) / this._pageSize);
            Int32 pageIndex = (this._pageIndex <= 1 ? 1 : (this._pageIndex > pageCount ? pageCount : this._pageIndex));
            Boolean hasAliasesName = !String.IsNullOrEmpty(aliasesName);
            String sql = this._database.InternalGetPagerSelectCommand(this, pageIndex, pageCount, orderReverse);

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
        /// 输出SQL语句
        /// </summary>
        public override DbCommand ToDbCommand()
        {
            DbCommand dbCommand = this.CreateDbCommand();

            for (Int32 i = 0; i < this._joins.Count; i++)
            {
                SqlParameter[] joinParameters = (this._joins[i] == null ? null : this._joins[i].GetAllParameters());
                if (joinParameters != null)
                {
                    this.AddParameterToDbCommand(dbCommand, joinParameters);
                }
            }

            SqlParameter[] whereParameters = (this._where == null ? null : this._where.GetAllParameters());
            if (whereParameters != null)
            {
                this.AddParameterToDbCommand(dbCommand, whereParameters);
            }

            SqlParameter[] havingParameters = (this._having == null ? null : this._having.GetAllParameters());
            if (havingParameters != null)
            {
                this.AddParameterToDbCommand(dbCommand, havingParameters);
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