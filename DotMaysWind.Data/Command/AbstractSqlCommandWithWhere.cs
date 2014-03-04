using System;
using System.Collections.Generic;
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
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置查询语句
        /// </summary>
        public ISqlCondition SqlWhere
        {
            get { return this._where; }
            set { this._where = value; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的有Where语句的Sql语句抽象类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="tableName">表格名称</param>
        internal AbstractSqlCommandWithWhere(Database database, String tableName)
            : base(database, tableName) { }
        #endregion

        #region 方法
        /// <summary>
        /// 输出SQL语句
        /// </summary>
        public override DbCommand ToDbCommand()
        {
            DbCommand dbCommand = this.CreateDbCommand();
            SqlParameter[] extraParameters = (this._where == null ? null : this._where.GetAllParameters());

            if (extraParameters != null)
            {
                this.AddParameterToDbCommand(dbCommand, extraParameters);
            }

            return dbCommand;
        }

        /// <summary>
        /// 获取所有参数集合
        /// </summary>
        /// <returns>所有参数集合</returns>
        public override List<SqlParameter> GetAllParameters()
        {
            List<SqlParameter> result = new List<SqlParameter>();
            result.AddRange(this._parameters);

            SqlParameter[] whereParameters = (this._where == null ? null : this._where.GetAllParameters());
            if (whereParameters != null)
            {
                result.AddRange(whereParameters);
            }

            return result;
        }
        #endregion
    }
}