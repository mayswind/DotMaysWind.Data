using System;
using System.Collections.Generic;
using System.Data;

using DotMaysWind.Data.Orm;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql插入语句类
    /// </summary>
    public class InsertCommand : AbstractSqlCommand
    {
        #region 属性
        /// <summary>
        /// 获取语句类型
        /// </summary>
        public override SqlCommandType CommandType
        {
            get { return SqlCommandType.Insert; }
        }

        /// <summary>
        /// 获取或设置要插入的参数组
        /// </summary>
        public List<SqlParameter> InsertParameters
        {
            get { return this._parameters; }
            set { this._parameters = value; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化Sql插入语句类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="tableName">数据表名称</param>
        public InsertCommand(Database database, String tableName)
            : base(database, tableName) { }
        #endregion

        #region 方法
        #region Add
        /// <summary>
        /// 插入指定参数组并返回当前语句
        /// </summary>
        /// <param name="insertParams">要插入的参数组</param>
        /// <returns>当前语句</returns>
        public InsertCommand Add(params SqlParameter[] insertParams)
        {
            if (insertParams != null)
            {
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
        public InsertCommand Add(String columnName, Object value)
        {
            this._parameters.Add(SqlParameter.Create(columnName, value));
            return this;
        }

        /// <summary>
        /// 插入指定参数并返回当前语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">内容</param>
        /// <returns>当前语句</returns>
        public InsertCommand Add(String columnName, DbType dbType, Object value)
        {
            this._parameters.Add(SqlParameter.Create(columnName, dbType, value));
            return this;
        }

        /// <summary>
        /// 插入指定参数并返回当前语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="function">函数</param>
        /// <exception cref="ArgumentNullException">函数不能为空</exception>
        /// <returns>当前语句</returns>
        public InsertCommand Add(String columnName, ISqlFunction function)
        {
            if (function == null)
            {
                throw new ArgumentNullException("function");
            }

            this._parameters.Add(SqlParameter.CreateCustomAction(columnName, function.GetSqlFunction(this.DatabaseType)));

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
        /// <param name="command">选择语句</param>
        /// <exception cref="ArgumentNullException">选择语句不能为空</exception>
        /// <returns>当前语句</returns>
        public InsertCommand Add(String columnName, SelectCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            this._parameters.Add(SqlParameter.CreateCustomAction(columnName, command.GetSqlCommand()));

            List<SqlParameter> parameters = command.GetAllParameters();

            if (parameters != null)
            {
                this._parameters.AddRange(command.GetAllParameters());
            }

            return this;
        }
        #endregion

        /// <summary>
        /// 输出SQL语句
        /// </summary>
        /// <returns>SQL语句</returns>
        public override String GetSqlCommand()
        {
            SqlCommandBuilder sb = new SqlCommandBuilder(this.DatabaseType);
            sb.AppendInsertPrefix().AppendTableName(this._tableName);

            if (this._parameters.Count > 0)
            {
                sb.AppendAllColumnNamesWithParentheses(this._parameters).AppendInsertValues().AppendAllParameterNamesWithParentheses(this._parameters);
            }

            return this.FollowingProcessSql(sb.ToString());
        }

        /// <summary>
        /// 获取数据行
        /// </summary>
        /// <exception cref="CommandNotSupportException">删除语句不支持获取数据行</exception>
        /// <returns>数据行</returns>
        public override DataRow ToDataRow()
        {
            throw new CommandNotSupportException();
        }

        /// <summary>
        /// 获取数据表格
        /// </summary>
        /// <exception cref="CommandNotSupportException">删除语句不支持获取数据表格</exception>
        /// <returns>数据表格</returns>
        public override DataTable ToDataTable()
        {
            throw new CommandNotSupportException();
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table">数据库表格</param>
        /// <exception cref="CommandNotSupportException">删除语句不支持获取实体</exception>
        /// <returns>数据实体</returns>
        public override T ToEntity<T>(AbstractDatabaseTable<T> table)
        {
            throw new CommandNotSupportException();
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table">数据库表格</param>
        /// <exception cref="CommandNotSupportException">删除语句不支持获取实体</exception>
        /// <returns>数据实体列表</returns>
        public override List<T> ToEntityList<T>(AbstractDatabaseTable<T> table)
        {
            throw new CommandNotSupportException();
        }
        #endregion
    }
}