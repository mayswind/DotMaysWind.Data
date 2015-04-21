using System;
using System.Collections.Generic;
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
        /// 父数据库
        /// </summary>
        protected AbstractDatabase _database;

        /// <summary>
        /// 表格名称
        /// </summary>
        protected String _tableName;

        /// <summary>
        /// 基础参数组
        /// </summary>
        protected List<DataParameter> _parameters;

        /// <summary>
        /// 基础参数组索引
        /// </summary>
        private Int16 _parameterIndex;

        /// <summary>
        /// 创建时的根来源
        /// </summary>
        private AbstractSqlCommand _rootSource;

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
        /// 获取当前数据库
        /// </summary>
        public AbstractDatabase Database
        {
            get { return this._database; }
        }

        /// <summary>
        /// 获取数据表名称
        /// </summary>
        public String TableName
        {
            get { return this._tableName; }
        }
        
        /// <summary>
        /// 获取创建时的根来源
        /// </summary>
        internal AbstractSqlCommand RootSource
        {
            get { return this._rootSource; }
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
        /// <param name="rootSource">创建时的根来源</param>
        /// <param name="tableName">表格名称</param>
        /// <exception cref="ArgumentNullException">数据库不能为空</exception>
        protected AbstractSqlCommand(AbstractDatabase database, AbstractSqlCommand rootSource, String tableName)
        {
            if (database == null)
            {
                throw new ArgumentNullException("database");
            }

            this._database = database;
            this._rootSource = rootSource;
            this._tableName = tableName;
            this._parameters = new List<DataParameter>();
            this._parameterIndex = 0;
        }
        #endregion

        #region 创建参数
        /// <summary>
        /// 创建新的Sql语句参数类
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">赋值内容</param>
        /// <returns>Sql语句参数类</returns>
        public DataParameter CreateDataParameter(String columnName, Object value)
        {
            return DataParameter.InternalCreate(this._database, columnName, this.GetNewParameterIndex(), value);
        }

        /// <summary>
        /// 创建新的Sql语句参数类
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">字段类型</param>
        /// <param name="value">赋值内容</param>
        /// <returns>Sql语句参数类</returns>
        public DataParameter CreateDataParameter(String columnName, DataType dataType, Object value)
        {
            return DataParameter.InternalCreate(this._database, columnName, this.GetNewParameterIndex(), dataType, value);
        }

        /// <summary>
        /// 创建新的Sql语句参数类
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="action">赋值操作</param>
        /// <returns>Sql语句参数类</returns>
        public DataParameter CreateDataParameterCustomAction(String columnName, String action)
        {
            return DataParameter.InternalCreateCustomAction(this._database, columnName, action);
        }
        #endregion

        #region 创建数据库命令
        /// <summary>
        /// 创建数据库命令
        /// </summary>
        /// <returns>数据库命令</returns>
        protected DbCommand CreateDbCommand()
        {
            return this._database.CreateDbCommand(this.GetCommandText(), this._parameters.ToArray());
        }
        #endregion

        #region 输出方法
        /// <summary>
        /// 获取所有参数集合
        /// </summary>
        /// <returns>所有参数集合</returns>
        public virtual DataParameter[] GetAllParameters()
        {
            return this._parameters.ToArray();
        }

        /// <summary>
        /// 获取Sql语句内容
        /// </summary>
        /// <returns>Sql语句内容</returns>
        public abstract String GetCommandText();

        /// <summary>
        /// 输出数据库命令
        /// </summary>
        /// <returns>数据库命令</returns>
        public virtual DbCommand ToDbCommand()
        {
            return this.CreateDbCommand();
        }
        #endregion

        #region 输出结果
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
        /// 获取操作后影响的行数（Insert、Update或Delete）或结果（Select）
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <returns>影响的行数（Insert、Update或Delete）或结果（Select）</returns>
        public virtual Int32 Result(DbConnection connection)
        {
            if (this.CommandType == SqlCommandType.Select)
            {
                return this._database.ExecuteScalar<Int32>(this, connection);
            }
            else
            {
                return this._database.ExecuteNonQuery(this, connection);
            }
        }

        /// <summary>
        /// 获取操作后影响的行数（Insert、Update或Delete）或结果（Select）
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <returns>影响的行数（Insert、Update或Delete）或结果（Select）</returns>
        public virtual Int32 Result(DbTransaction transaction)
        {
            if (this.CommandType == SqlCommandType.Select)
            {
                return this._database.ExecuteScalar<Int32>(this, transaction);
            }
            else
            {
                return this._database.ExecuteNonQuery(this, transaction);
            }
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

            if (this.CommandType != cmd.CommandType)
            {
                return false;
            }

            if (!String.Equals(this.GetCommandText(), cmd.GetCommandText()))
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
            return String.Format("{0}, {1}", base.ToString(), this.GetCommandText());
        }
        #endregion

        #region 内部方法
        /// <summary>
        /// 添加参数组
        /// </summary>
        /// <param name="parameters">参数组列表</param>
        internal void InternalAddDataParameterList(DataParameter[] parameters)
        {
            this._parameters.AddRange(parameters);
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取新参数的索引
        /// </summary>
        private String GetNewParameterIndex()
        {
            if (this._rootSource != null)
            {
                return this._rootSource.GetNewParameterIndex();
            }

            if (this._parameterIndex >= Int16.MaxValue)
            {
                this._parameterIndex = 0;
            }

            return Convert.ToString(this._parameterIndex++, 16);
        }
        #endregion
    }
}