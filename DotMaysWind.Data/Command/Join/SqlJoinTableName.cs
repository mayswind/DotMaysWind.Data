using System;
using System.Text;

namespace DotMaysWind.Data.Command.Join
{
    /// <summary>
    /// Sql连接表名语句类
    /// </summary>
    public class SqlJoinTableName : AbstractSqlJoin
    {
        #region 字段
        private String _anotherTableName;
        #endregion

        #region 属性
        /// <summary>
        /// 获取连接模式
        /// </summary>
        public override SqlJoinMode JoinMode
        {
            get { return SqlJoinMode.JoinTableName; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化Sql连接语句类
        /// </summary>
        /// <param name="cmd">选择语句</param>
        /// <param name="joinType">连接模式</param>
        /// <param name="currentTableName">当前表格名称</param>
        /// <param name="currentTableField">当前表格主键</param>
        /// <param name="anotherTableName">另个表格名称</param>
        /// <param name="anotherTableField">另个表格主键</param>
        internal SqlJoinTableName(SelectCommand cmd, SqlJoinType joinType, String currentTableName, String currentTableField, String anotherTableName, String anotherTableField)
            : base(cmd, joinType, currentTableName, currentTableField, anotherTableField)
        {
            this._anotherTableName = anotherTableName;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 输出连接语句
        /// </summary>
        /// <returns>连接语句</returns>
        public override String GetSqlClause()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(SqlJoinTypes.InternalGetTypeName(this._joinType)).Append(' ').Append(this._anotherTableName).Append(" ON ");
            sb.Append(this._currentTableName).Append('.').Append(this._currentTableKeyField).Append(" = ");
            sb.Append(this._anotherTableName).Append('.').Append(this._anotherTableKeyField);

            return sb.ToString();
        }
        #endregion
    }
}