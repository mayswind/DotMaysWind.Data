using System;
using System.Collections.Generic;
using System.Data;

using DotMaysWind.Data.Command.Condition;
using DotMaysWind.Data.Orm;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql删除语句类
    /// </summary>
    public class DeleteCommand : AbstractSqlCommandWithWhere
    {
        #region 属性
        /// <summary>
        /// 获取语句类型
        /// </summary>
        public override SqlCommandType CommandType
        {
            get { return SqlCommandType.Delete; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化Sql删除语句类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="tableName">数据表名称</param>
        public DeleteCommand(Database database, String tableName)
            : base(database, tableName) { }
        #endregion

        #region 方法
        /// <summary>
        /// 设置指定查询的语句并返回当前语句
        /// </summary>
        /// <param name="where">查询语句</param>
        /// <returns>当前语句</returns>
        public DeleteCommand Where(ISqlCondition where)
        {
            this._where = where;

            return this;
        }

        /// <summary>
        /// 输出SQL语句
        /// </summary>
        /// <returns>SQL语句</returns>
        public override String ToString()
        {
            SqlCommandBuilder sb = new SqlCommandBuilder(this.DatabaseType);
            sb.AppendDeletePrefix().AppendTableName(this._tableName).AppendWhere(this._where);

            return sb.ToString();
        }

        /// <summary>
        /// 获取数据行
        /// </summary>
        /// <exception cref="NotSupportedException">删除语句不支持获取数据行</exception>
        /// <returns>数据行</returns>
        public override DataRow ToDataRow()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 获取数据表格
        /// </summary>
        /// <exception cref="NotSupportedException">删除语句不支持获取数据表格</exception>
        /// <returns>数据表格</returns>
        public override DataTable ToDataTable()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table">数据库表格</param>
        /// <exception cref="NotSupportedException">删除语句不支持获取实体</exception>
        /// <returns>数据实体</returns>
        public override T ToEntity<T>(AbstractDatabaseTable<T> table)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table">数据库表格</param>
        /// <exception cref="NotSupportedException">删除语句不支持获取实体</exception>
        /// <returns>数据实体列表</returns>
        public override List<T> ToEntityList<T>(AbstractDatabaseTable<T> table)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}