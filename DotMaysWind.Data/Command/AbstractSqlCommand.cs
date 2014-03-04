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
            dbCommand.CommandText = this.ToString();

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
        public abstract override String ToString();
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
        /// <exception cref="NotSupportedException">Insert、Update或Delete不支持本方法</exception>
        public virtual T Result<T>()
        {
            if (this.CommandType == SqlCommandType.Select)
            {
                return this._database.ExecuteScalar<T>(this);
            }
            else
            {
                throw new NotSupportedException();
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

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table">数据库表格</param>
        /// <returns>数据实体</returns>
        public virtual T ToEntity<T>(AbstractDatabaseTable<T> table) where T : class
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            return table.GetEntity(this._database.ExecuteDataRow(this));
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table">数据库表格</param>
        /// <returns>数据实体列表</returns>
        public virtual List<T> ToEntityList<T>(AbstractDatabaseTable<T> table) where T : class
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            return table.GetEntities(this._database.ExecuteDataTable(this));
        }
        #endregion

        #region 私有方法
        private DbParameter CreateDbParameter(SqlParameter param)
        {
            DbParameter dbParameter = this._database.DatabaseProvider.CreateParameter();
            dbParameter.DbType = param.DbType;

            if (this._database.DatabaseType == DatabaseType.Oracle)
            {
                dbParameter.ParameterName = ":" + param.ParameterName;
            }
            else
            {
                dbParameter.ParameterName = "@" + param.ParameterName;
            }
            
            dbParameter.Value = param.Value;
            dbParameter.SourceVersion = DataRowVersion.Default;

            return dbParameter;
        }
        #endregion
    }
}