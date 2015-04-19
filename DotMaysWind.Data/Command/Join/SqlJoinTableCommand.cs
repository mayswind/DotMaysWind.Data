using System;
using System.Collections.Generic;
using System.Text;

namespace DotMaysWind.Data.Command.Join
{
    /// <summary>
    /// Sql连接表命令语句类
    /// </summary>
    public sealed class SqlJoinTableCommand : AbstractSqlJoin
    {
        #region 字段
        private String _anotherTableIdentity;
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
        /// <param name="cmd">选择语句</param>
        /// <param name="joinType">连接模式</param>
        /// <param name="currentTableName">当前表格名称</param>
        /// <param name="currentTableField">当前表格主键</param>
        /// <param name="anotherTableName">另个表格名称</param>
        /// <param name="createSelectAction">创建查询方法</param>
        /// <param name="anotherTableIdentity">标识</param>
        /// <param name="anotherTableField">另个表格主键</param>
        internal SqlJoinTableCommand(SelectCommand cmd, SqlJoinType joinType, String currentTableName, String currentTableField, String anotherTableName, String anotherTableField, String anotherTableIdentity, Action<SelectCommand> createSelectAction)
            : base(cmd, joinType, currentTableName, currentTableField, anotherTableField)
        {
            this._anotherTableIdentity = anotherTableIdentity;

            SelectCommand anotherTableCommand = cmd.Database.InternalCreateSelectCommand((cmd.RootSource == null ? cmd : cmd.RootSource), anotherTableName);
            createSelectAction(anotherTableCommand);

            this._anotherTableCommand = anotherTableCommand;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取所有参数集合
        /// </summary>
        /// <returns>所有参数集合</returns>
        public override DataParameter[] GetAllParameters()
        {
            if (this._anotherTableCommand == null)
            {
                return null;
            }

            return this._anotherTableCommand.GetAllParameters();
        }

        /// <summary>
        /// 获取连接语句内容
        /// </summary>
        /// <returns>连接语句内容</returns>
        public override String GetClauseText()
        {
            if (this._anotherTableCommand == null)
            {
                return String.Empty;
            }

            StringBuilder sb = new StringBuilder();

            String anotherTableName = "TBL_{0}" + this._anotherTableIdentity;
            String anotherTableContent = this._anotherTableCommand.GetCommandText(anotherTableName);

            sb.Append(SqlJoinTypes.InternalGetTypeName(this._joinType)).Append(' ').Append(this._anotherTableCommand.GetCommandText(anotherTableName)).Append(" ON ");
            sb.Append(this._currentTableName).Append('.').Append(this._currentTableKeyField).Append(" = ");
            sb.Append(anotherTableName).Append('.').Append(this._anotherTableKeyField);

            return sb.ToString();
        }
        #endregion
    }
}