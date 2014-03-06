using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Helper;

namespace DotMaysWind.Data
{
    /// <summary>
    /// 数据库类
    /// </summary>
    public class Database
    {
        #region 字段
        private String _connectionString = null;
        private DbProviderFactory _dbProvider = null;
        private DatabaseType _dbtype = DatabaseType.Unknown;
        #endregion

        #region 属性
        /// <summary>
        /// 获取当前数据库类型
        /// </summary>
        public DatabaseType DatabaseType
        {
            get { return this._dbtype; }
        }

        /// <summary>
        /// 获取当前数据库数据库提供者
        /// </summary>
        public DbProviderFactory DatabaseProvider
        {
            get { return this._dbProvider; }
        }

        /// <summary>
        /// 获取当前数据库数据库提供者名称
        /// </summary>
        public String ProviderName
        {
            get { return this._dbProvider.GetType().ToString(); }
        }

        /// <summary>
        /// 获取当前数据库连接字符串
        /// </summary>
        public String ConnectionString
        {
            get { return this._connectionString; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化数据库类
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="dbProvider">数据库提供者</param>
        internal Database(String connectionString, DbProviderFactory dbProvider)
        {
            this._connectionString = connectionString;
            this._dbProvider = dbProvider;
            this._dbtype = DatabaseTypeHelper.InternalGetDatabaseType(this._connectionString, this._dbProvider);

            if (this._dbtype == DatabaseType.Unknown)
            {
                throw new DatabaseNotSupportException("This database is not supported!");
            }
        }
        #endregion

        #region CreateCommand
        /// <summary>
        /// 创建新的Sql插入语句类
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        public InsertCommand CreateInsertCommand(String tableName)
        {
            return new InsertCommand(this, tableName);
        }
        
        /// <summary>
        /// 创建新的Sql更新语句类
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        public UpdateCommand CreateUpdateCommand(String tableName)
        {
            return new UpdateCommand(this, tableName);
        }

        /// <summary>
        /// 创建新的Sql删除语句类
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        public DeleteCommand CreateDeleteCommand(String tableName)
        {
            return new DeleteCommand(this, tableName);
        }

        /// <summary>
        /// 创建新的Sql选择语句类
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        public SelectCommand CreateSelectCommand(String tableName)
        {
            return new SelectCommand(this, tableName);
        }

        /// <summary>
        /// 创建新的Sql选择语句类
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="tableAliasesName">数据表别名</param>
        public SelectCommand CreateSelectCommand(String tableName, String tableAliasesName)
        {
            return new SelectCommand(this, tableName + ' ' + tableAliasesName);
        }

        /// <summary>
        /// 创建新的Sql选择语句类
        /// </summary>
        /// <param name="from">选择的从Sql语句</param>
        /// <param name="fromAliasesName">从Sql语句的别名</param>
        public SelectCommand CreateSelectCommand(SelectCommand from, String fromAliasesName)
        {
            return new SelectCommand(this, from, fromAliasesName);
        }

        /// <summary>
        /// 创建新的Sql选择语句类
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="pageSize">页面大小</param>
        public SelectCommand CreateSelectCommand(String tableName, Int32 pageSize)
        {
            return new SelectCommand(this, tableName, pageSize);
        }

        /// <summary>
        /// 创建新的Sql选择语句类
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="tableAliasesName">数据表别名</param>
        /// <param name="pageSize">页面大小</param>
        public SelectCommand CreateSelectCommand(String tableName, String tableAliasesName, Int32 pageSize)
        {
            return new SelectCommand(this, tableName + ' ' + tableAliasesName, pageSize);
        }

        /// <summary>
        /// 创建新的Sql选择语句类
        /// </summary>
        /// <param name="from">选择的从Sql语句</param>
        /// <param name="fromAliasesName">从Sql语句的别名</param>
        /// <param name="pageSize">页面大小</param>
        public SelectCommand CreateSelectCommand(SelectCommand from, String fromAliasesName, Int32 pageSize)
        {
            return new SelectCommand(this, from, fromAliasesName, pageSize);
        }

        /// <summary>
        /// 创建新的Sql选择语句类
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="recordCount">记录总数</param>
        public SelectCommand CreateSelectCommand(String tableName, Int32 pageSize, Int32 pageIndex, Int32 recordCount)
        {
            return new SelectCommand(this, tableName, pageSize, pageIndex, recordCount);
        }

        /// <summary>
        /// 创建新的Sql选择语句类
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="tableAliasesName">数据表别名</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="recordCount">记录总数</param>
        public SelectCommand CreateSelectCommand(String tableName, String tableAliasesName, Int32 pageSize, Int32 pageIndex, Int32 recordCount)
        {
            return new SelectCommand(this, tableName + ' ' + tableAliasesName, pageSize, pageIndex, recordCount);
        }

        /// <summary>
        /// 创建新的Sql选择语句类
        /// </summary>
        /// <param name="isFromSql">是否从Sql语句中选择</param>
        /// <param name="from">数据表或Sql语句</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="recordCount">记录总数</param>
        public SelectCommand CreateSelectCommand(Boolean isFromSql, String from, Int32 pageSize, Int32 pageIndex, Int32 recordCount)
        {
            return new SelectCommand(this, isFromSql, from, pageSize, pageIndex, recordCount);
        }

        /// <summary>
        /// 创建新的Sql自定义语句类
        /// </summary>
        /// <param name="commandType">语句类型</param>
        /// <param name="commandString">语句内容</param>
        public CustomCommand CreateCustomCommand(SqlCommandType commandType, String commandString)
        {
            return new CustomCommand(this, commandType, commandString);
        }

        /// <summary>
        /// 根据Sql语句创建数据库命令
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>数据库命令</returns>
        public DbCommand CreateDbCommand(ISqlCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            return command.ToDbCommand();
        }

        /// <summary>
        /// 创建数据库命令
        /// </summary>
        /// <returns>数据库命令</returns>
        public DbCommand CreateDbCommand()
        {
            return this._dbProvider.CreateCommand();
        }
        #endregion

        #region CreateTransaction
        /// <summary>
        /// 创建新的事务
        /// </summary>
        public Transaction CreateTransaction()
        {
            return new Transaction(this, false);
        }

        /// <summary>
        /// 创建新的事务
        /// </summary>
        /// <param name="autoOpen">是否自动打开连接</param>
        public Transaction CreateTransaction(Boolean autoOpen)
        {
            return new Transaction(this, autoOpen);
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="command">指定Sql语句</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        public T ExecuteScalar<T>(ISqlCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            return this.ExecuteScalar<T>(command.ToDbCommand());
        }

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        public Object ExecuteScalar(ISqlCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            return this.ExecuteScalar(command.ToDbCommand());
        }

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="dbCommand">数据库命令</param>
        /// <returns>返回的结果</returns>
        public T ExecuteScalar<T>(DbCommand dbCommand)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            Object obj = this.ExecuteScalar(dbCommand);
            return (Convert.IsDBNull(obj) ? default(T) : (T)obj);
        }

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <exception cref="ArgumentNullException">数据库命令不能为空</exception>
        /// <returns>返回的结果</returns>
        public Object ExecuteScalar(DbCommand dbCommand)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            Object result;
            using (DatabaseConnectionWrapper wrapper = this.GetConnection())
            {
                dbCommand.Connection = wrapper.Connection;
                result = dbCommand.ExecuteScalar();
            }

            return result;
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// 返回执行指定Sql语句后影响的行数
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>受影响的行数</returns>
        public Int32 ExecuteNonQuery(ISqlCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            return this.ExecuteNonQuery(command.ToDbCommand());
        }

        /// <summary>
        /// 返回执行指定Sql语句后影响的行数
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <exception cref="ArgumentNullException">数据库命令不能为空</exception>
        /// <returns>受影响的行数</returns>
        public Int32 ExecuteNonQuery(DbCommand dbCommand)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            Int32 result;
            using (DatabaseConnectionWrapper wrapper = this.GetConnection())
            {
                dbCommand.Connection = wrapper.Connection;
                result = dbCommand.ExecuteNonQuery();
            }

            return result;
        }

        /// <summary>
        /// 返回执行多个指定Sql语句后影响的行数
        /// </summary>
        /// <param name="commands">指定Sql语句组</param>
        /// <returns>受影响的行数</returns>
        public Int32 ExecuteNonQuery(params ISqlCommand[] commands)
        {
            Transaction transaction = null;
            Int32 count = 0;

            if (commands == null || commands.Length <= 0)
            {
                return 0;
            }

            using (transaction = this.CreateTransaction())
            {
                try
                {
                    transaction.Open();

                    for (Int32 i = 0; i < commands.Length; i++)
                    {
                        count += transaction.ExecuteNonQuery(commands[i]);
                    }

                    transaction.Commit();
                }
                catch
                {
                    count = 0;

                    transaction.Rollback();
                }
                finally
                {
                    transaction.Close();
                    transaction.Dispose();
                }
            }

            return count;
        }

        /// <summary>
        /// 返回执行多个指定Sql语句后影响的行数
        /// </summary>
        /// <param name="commands">指定Sql语句组</param>
        /// <returns>受影响的行数</returns>
        public Int32 ExecuteNonQuery(List<ISqlCommand> commands)
        {
            return this.ExecuteNonQuery(commands.ToArray());
        }
        #endregion

        #region ExecuteReader
        /// <summary>
        /// 获得指定Sql语句查询下的数据读取器
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>数据读取器</returns>
        public IDataReader ExecuteReader(ISqlCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            return this.ExecuteReader(command.ToDbCommand());
        }

        /// <summary>
        /// 获得指定Sql语句查询下的数据读取器
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <exception cref="ArgumentNullException">数据库命令不能为空</exception>
        /// <returns>数据读取器</returns>
        public IDataReader ExecuteReader(DbCommand dbCommand)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            IDataReader result;
            using (DatabaseConnectionWrapper wrapper = this.GetConnection())
            {
                dbCommand.Connection = wrapper.Connection;
                result = dbCommand.ExecuteReader();
            }

            return result;
        }
        #endregion

        #region ExecuteDataSet/Table/Row
        /// <summary>
        /// 获得指定Sql语句查询下的数据集
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>数据集</returns>
        public DataSet ExecuteDataSet(ISqlCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            return this.ExecuteDataSet(command.ToDbCommand());
        }

        /// <summary>
        /// 获得指定Sql语句查询下的数据集
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <exception cref="ArgumentNullException">数据库命令不能为空</exception>
        /// <returns>数据集</returns>
        public DataSet ExecuteDataSet(DbCommand dbCommand)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            DataSet dataSet = null;
            using (DatabaseConnectionWrapper wrapper = this.GetConnection())
            {
                dbCommand.Connection = wrapper.Connection;
                dataSet = DataSetHelper.InternalCreateDataSet(this._dbProvider, dbCommand);
            }

            return dataSet;
        }

        /// <summary>
        /// 获得指定Sql语句查询下的数据表
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>数据表</returns>
        public DataTable ExecuteDataTable(ISqlCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            DataSet dataSet = this.ExecuteDataSet(command);
            return (dataSet != null && dataSet.Tables != null && dataSet.Tables.Count > 0 ? dataSet.Tables[0] : null);
        }

        /// <summary>
        /// 获得指定Sql语句查询下的数据表
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <exception cref="ArgumentNullException">数据库命令不能为空</exception>
        /// <returns>数据表</returns>
        public DataTable ExecuteDataTable(DbCommand dbCommand)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            DataSet dataSet = this.ExecuteDataSet(dbCommand);
            return (dataSet != null && dataSet.Tables != null && dataSet.Tables.Count > 0 ? dataSet.Tables[0] : null);
        }

        /// <summary>
        /// 获得指定Sql语句查询下的单行数据
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>单行数据</returns>
        public DataRow ExecuteDataRow(ISqlCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            DataTable dataTable = this.ExecuteDataTable(command);
            return (dataTable != null && dataTable.Rows != null && dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null);
        }

        /// <summary>
        /// 获得指定Sql语句查询下的单行数据
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <exception cref="ArgumentNullException">数据库命令不能为空</exception>
        /// <returns>单行数据</returns>
        public DataRow ExecuteDataRow(DbCommand dbCommand)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            DataTable dataTable = this.ExecuteDataTable(dbCommand);
            return (dataTable != null && dataTable.Rows != null && dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null);
        }
        #endregion

        #region 内部方法
        internal DatabaseConnectionWrapper GetNewConnection()
        {
            DbConnection connection = null;

            try
            {
                connection = this._dbProvider.CreateConnection();
                connection.ConnectionString = this._connectionString;

                connection.Open();
            }
            catch
            {
                if (connection != null)
                {
                    connection.Close();
                }

                throw;
            }

            return new DatabaseConnectionWrapper(connection);
        }

        internal DatabaseConnectionWrapper GetConnection()
        {
            DatabaseConnectionWrapper connection = TransactionScopeConnections.GetConnection(this);
            return connection ?? this.GetNewConnection();
        }
        #endregion

        #region 重载方法
        /// <summary>
        /// 返回当前对象的信息
        /// </summary>
        /// <returns>当前对象的信息</returns>
        public override String ToString()
        {
            return String.Format("{0}, {1}", base.ToString(), this._dbProvider.ToString());
        }
        #endregion
    }
}