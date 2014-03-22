using System;
using System.Data;

using DotMaysWind.Data.Command.Condition;

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
        internal DeleteCommand(AbstractDatabase database, String tableName)
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
        /// 设置指定查询的语句并返回当前语句
        /// </summary>
        /// <param name="where">查询语句</param>
        /// <returns>当前语句</returns>
        public DeleteCommand Where(Func<SqlConditionBuilder, ISqlCondition> where)
        {
            this._where = where(this._conditionBuilder);

            return this;
        }

        /// <summary>
        /// 输出SQL语句
        /// </summary>
        /// <returns>SQL语句</returns>
        public override String GetSqlCommand()
        {
            SqlCommandBuilder sb = new SqlCommandBuilder(this.Database);
            sb.AppendDeletePrefix().AppendTableName(this._tableName).AppendWhere(this._where);

            return sb.ToString();
        }
        #endregion
    }
}