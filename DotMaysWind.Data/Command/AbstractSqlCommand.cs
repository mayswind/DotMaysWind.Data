using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using DotMaysWind.Data.Orm;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql语句抽象类
    /// </summary>
    public abstract class AbstractSqlCommand : ISqlCommand
    {
        #region 字段
        /// <summary>
        /// 数据库类型
        /// </summary>
        protected Database _database;

        /// <summary>
        /// 表格名称
        /// </summary>
        protected String _tableName;

        /// <summary>
        /// 基础参数组
        /// </summary>
        protected List<SqlParameter> _parameters;

        /// <summary>
        /// 支持映射的数据表
        /// </summary>
        private IDatabaseTableWithMapping _sourceTable;
        #endregion

        #region 属性
        /// <summary>
        /// 获取语句类型
        /// </summary>
        public abstract SqlCommandType CommandType { get; }

        /// <summary>
        /// 获取数据库
        /// </summary>
        public Database Database
        {
            get { return this._database; }
        }

        /// <summary>
        /// 获取数据库类型
        /// </summary>
        public DatabaseType DatabaseType
        {
            get { return this._database.DatabaseType; }
        }

        /// <summary>
        /// 获取数据表名称
        /// </summary>
        public String TableName
        {
            get { return this._tableName; }
        }

        /// <summary>
        /// 获取或设置支持映射的数据表
        /// </summary>
        internal IDatabaseTableWithMapping SourceDatabaseTable
        {
            get { return this._sourceTable; }
            set { this._sourceTable = value; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的Sql语句抽象类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="tableName">表格名称</param>
        /// <exception cref="ArgumentNullException">数据库不能为空</exception>
        protected AbstractSqlCommand(Database database, String tableName)
        {
            if (database == null)
            {
                throw new ArgumentNullException("database");
            }

            this._database = database;
            this._tableName = tableName;
            this._parameters = new List<SqlParameter>();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 输出SQL语句
        /// </summary>
        /// <returns>数据库命令</returns>
        public virtual DbCommand ToDbCommand()
        {
            return this.CreateDbCommand();
        }

        /// <summary>
        /// 创建数据库命令
        /// </summary>
        /// <returns>数据库命令</returns>
        protected DbCommand CreateDbCommand()
        {
            DbCommand dbCommand = this._database.DatabaseProvider.CreateCommand();
            dbCommand.CommandType = System.Data.CommandType.Text;
            dbCommand.CommandText = this.GetSqlCommand();

            for (Int32 i = 0; i < this._parameters.Count; i++)
            {
                if (this._parameters[i].IsUseParameter)
                {
                    dbCommand.Parameters.Add(this.CreateDbParameter(this._parameters[i]));
                }
            }

            return dbCommand;
        }

        /// <summary>
        /// 添加参数到数据库命令中
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <param name="extraParameters">额外参数组</param>
        protected void AddParameterToDbCommand(DbCommand dbCommand, params SqlParameter[] extraParameters)
        {
            if (extraParameters == null)
            {
                return;
            }

            for (Int32 i = 0; i < extraParameters.Length; i++)
            {
                if (extraParameters[i].IsUseParameter)
                {
                    dbCommand.Parameters.Add(this.CreateDbParameter(extraParameters[i]));
                }
            }
        }

        /// <summary>
        /// 获取所有参数集合
        /// </summary>
        /// <returns>所有参数集合</returns>
        public virtual List<SqlParameter> GetAllParameters()
        {
            return this._parameters;
        }

        /// <summary>
        /// 输出SQL语句
        /// </summary>
        /// <returns>SQL语句</returns>
        public abstract String GetSqlCommand();

        /// <summary>
        /// 后处理Sql语句
        /// </summary>
        /// <returns>处理后的Sql语句</returns>
        protected String FollowingProcessSql(String originSql)
        {
            String result = originSql;

            if (this._database.DatabaseType == DatabaseType.Oracle)
            {
                result = result.Replace(Constants.GeneralParameterNamePrefix, Constants.OracleParameterNamePrefix);
            }

            return result;
        }
        #endregion

        #region Result
        /// <summary>
        /// 获取操作后影响的行数（Insert、Update或Delete）或结果（Select）
        /// </summary>
        /// <returns>影响的行数（Insert、Update或Delete）或结果（Select）</returns>
        public virtual Int32 Result()
        {
            if (this.CommandType == SqlCommandType.Select)
            {
                return this._database.ExecuteScalar<Int32>(this);
            }
            else
            {
                return this._database.ExecuteNonQuery(this);
            }
        }

        /// <summary>
        /// 获取操作的结果（Select）
        /// </summary>
        /// <typeparam name="T">返回结果的类型</typeparam>
        /// <returns>操作的结果（Select）</returns>
        /// <exception cref="CommandNotSupportException">Insert、Update或Delete不支持本方法</exception>
        public virtual T Result<T>()
        {
            if (this.CommandType == SqlCommandType.Select)
            {
                return this._database.ExecuteScalar<T>(this);
            }
            else
            {
                throw new CommandNotSupportException();
            }
        }

        /// <summary>
        /// 获取数据行
        /// </summary>
        /// <returns>数据行</returns>
        public virtual DataRow ToDataRow()
        {
            return this._database.ExecuteDataRow(this);
        }

        /// <summary>
        /// 获取数据表格
        /// </summary>
        /// <returns>数据表格</returns>
        public virtual DataTable ToDataTable()
        {
            return this._database.ExecuteDataTable(this);
        }
        #endregion

        #region 私有方法
        private DbParameter CreateDbParameter(SqlParameter param)
        {
            DbParameter dbParameter = this._database.DatabaseProvider.CreateParameter();
            dbParameter.DbType = param.DbType;

            if (this._database.DatabaseType == DatabaseType.Oracle)
            {
                dbParameter.ParameterName = param.ParameterName.Replace(Constants.GeneralParameterNamePrefix, Constants.OracleParameterNamePrefix);
            }
            else
            {
                dbParameter.ParameterName = param.ParameterName;
            }

            dbParameter.Value = param.Value;
            dbParameter.SourceVersion = DataRowVersion.Default;

            return dbParameter;
        }
        #endregion

        #region 重载方法和运算符
        /// <summary>
        /// 获取当前参数的哈希值
        /// </summary>
        /// <returns>当前参数的哈希值</returns>
        public override Int32 GetHashCode()
        {
            return this._parameters.GetHashCode();
        }

        /// <summary>
        /// 判断两个Sql语句是否相同
        /// </summary>
        /// <param name="obj">待比较的Sql语句</param>
        /// <returns>两个Sql语句是否相同</returns>
        public override Boolean Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            AbstractSqlCommand cmd = obj as AbstractSqlCommand;

            if (cmd == null)
            {
                return false;
            }

            if (!String.Equals(this._tableName, cmd._tableName))
            {
                return false;
            }

            if (!String.Equals(this.GetSqlCommand(), cmd.GetSqlCommand()))
            {
                return false;
            }

            if (this._parameters.Count != cmd._parameters.Count)
            {
                return false;
            }

            for (Int32 i = 0; i < this._parameters.Count; i++)
            {
                if ((this._parameters[i] != null && cmd._parameters[i] == null) || (this._parameters[i] == null && cmd._parameters[i] != null))
                {
                    return false;
                }

                if (!this._parameters[i].Equals(cmd._parameters[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 判断两个Sql语句是否相同
        /// </summary>
        /// <param name="obj">待比较的Sql语句</param>
        /// <param name="obj2">待比较的第二个Sql语句</param>
        /// <returns>两个Sql语句是否相同</returns>
        public static Boolean operator ==(AbstractSqlCommand obj, AbstractSqlCommand obj2)
        {
            return Object.Equals(obj, obj2);
        }

        /// <summary>
        /// 判断两个Sql语句是否不同
        /// </summary>
        /// <param name="obj">待比较的Sql语句</param>
        /// <param name="obj2">待比较的第二个Sql语句</param>
        /// <returns>两个Sql语句是否不同</returns>
        public static Boolean operator !=(AbstractSqlCommand obj, AbstractSqlCommand obj2)
        {
            return !Object.Equals(obj, obj2);
        }

        /// <summary>
        /// 返回当前对象的信息
        /// </summary>
        /// <returns>当前对象的信息</returns>
        public override String ToString()
        {
            return String.Format("{0}, {1}", base.ToString(), this.GetSqlCommand());
        }
        #endregion
    }
}