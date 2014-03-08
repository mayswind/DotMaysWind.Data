using System;
using System.Collections.Generic;
using System.Text;

namespace DotMaysWind.Data.Command.Join
{
    /// <summary>
    /// Sql连接表命令语句类
    /// </summary>
    public class SqlJoinTableCommand : AbstractSqlJoin
    {
        #region 字段
        private Int32 _anotherTableIndentity;
        private SelectCommand _anotherTableCommand;
        #endregion

        #region 属性
        /// <summary>
        /// 获取连接模式
        /// </summary>
        public override SqlJoinMode JoinMode
        {
            get { return SqlJoinMode.JoinTableCommand; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化Sql连接语句类
        /// </summary>
        /// <param name="joinType">连接模式</param>
        /// <param name="currentTableName">当前表格名称</param>
        /// <param name="currentTableField">当前表格主键</param>
        /// <param name="anotherTableCommand">另个表格命令</param>
        /// <param name="anotherTableIndentity">标识</param>
        /// <param name="anotherTableField">另个表格主键</param>
        internal SqlJoinTableCommand(SqlJoinType joinType, String currentTableName, String currentTableField, SelectCommand anotherTableCommand, Int32 anotherTableIndentity, String anotherTableField)
            : base(joinType, currentTableName, currentTableField, anotherTableField)
        {
            this._anotherTableIndentity = anotherTableIndentity;
            this._anotherTableCommand = anotherTableCommand;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取所有参数集合
        /// </summary>
        /// <returns>所有参数集合</returns>
        public override SqlParameter[] GetAllParameters()
        {
            if (this._anotherTableCommand == null)
            {
                return null;
            }

            return this._anotherTableCommand.GetAllParameters();
        }

        /// <summary>
        /// 输出连接语句
        /// </summary>
        /// <returns>连接语句</returns>
        public override String GetSqlClause()
        {
            if (this._anotherTableCommand == null)
            {
                return String.Empty;
            }

            StringBuilder sb = new StringBuilder();

            String anotherTableName = String.Format("TBL_{0}", this._anotherTableIndentity);
            String anotherTableContent = this._anotherTableCommand.GetSqlCommand(anotherTableName);

            sb.Append(SqlJoinTypes.InternalGetTypeName(this._joinType)).Append(' ').Append(this._anotherTableCommand.GetSqlCommand(anotherTableName)).Append(" ON ");
            sb.Append(this._currentTableName).Append('.').Append(this._currentTableKeyField).Append(" = ");
            sb.Append(anotherTableName).Append('.').Append(this._anotherTableKeyField);

            return sb.ToString();
        }
        #endregion
    }
}