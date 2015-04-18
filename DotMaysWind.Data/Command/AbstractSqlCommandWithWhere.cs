using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using DotMaysWind.Data.Command.Condition;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// 有Where语句的语句抽象类
    /// </summary>
    public abstract class AbstractSqlCommandWithWhere : AbstractSqlCommand
    {
        #region 字段
        /// <summary>
        /// Where条件语句
        /// </summary>
        protected ISqlCondition _where;

        /// <summary>
        /// 条件语句生成器
        /// </summary>
        protected SqlConditionBuilder _conditionBuilder;
        #endregion

        #region 属性
        /// <summary>
        /// 获取查询语句
        /// </summary>
        public ISqlCondition WhereCondition
        {
            get { return this._where; }
            internal set { this._where = value; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的有Where语句的Sql语句抽象类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="tableName">表格名称</param>
        protected AbstractSqlCommandWithWhere(AbstractDatabase database, String tableName)
            : base(database, tableName)
        {
            this._conditionBuilder = new SqlConditionBuilder(this);
        }
        #endregion

        #region 方法
        /// <summary>
        /// 输出数据库命令
        /// </summary>
        /// <returns>数据库命令</returns>
        public override DbCommand ToDbCommand()
        {
            DbCommand dbCommand = this.CreateDbCommand();
            DataParameter[] extraParameters = (this._where == null ? null : this._where.GetAllParameters());

            if (extraParameters != null)
            {
                this._database.AddParameterToDbCommand(dbCommand, extraParameters);
            }

            return dbCommand;
        }

        /// <summary>
        /// 获取所有参数集合
        /// </summary>
        /// <returns>所有参数集合</returns>
        public override DataParameter[] GetAllParameters()
        {
            List<DataParameter> result = new List<DataParameter>();
            result.AddRange(this._parameters);

            DataParameter[] whereParameters = (this._where == null ? null : this._where.GetAllParameters());
            if (whereParameters != null)
            {
                result.AddRange(whereParameters);
            }

            return result.ToArray();
        }
        #endregion
    }
}