using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Command.Function;
using DotMaysWind.Data.Helper;

namespace DotMaysWind.Data
{
    /// <summary>
    /// 抽象数据库类
    /// </summary>
    public abstract class AbstractDatabase : IDatabase
    {
        #region 字段
        private String _connectionString = null;
        private DbProviderFactory _dbProvider = null;
        private SqlFunctions _sqlFunctions = null;
        #endregion

        #region 属性
        /// <summary>
        /// 获取当前数据库类型
        /// </summary>
        public abstract DatabaseType DatabaseType { get; }

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

        /// <summary>
        /// 获取Sql数据库支持的函数
        /// </summary>
        public SqlFunctions Functions
        {
            get { return this._sqlFunctions; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化数据库类
        /// </summary>
        /// <param name="dbProvider">数据库提供者</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <exception cref="ArgumentNullException">数据库提供者不能为空</exception>
        internal AbstractDatabase(DbProviderFactory dbProvider, String connectionString)
        {
            if (dbProvider == null)
            {
                throw new ArgumentNullException("dbProvider");
            }

            this._dbProvider = dbProvider;
            this._connectionString = connectionString;
            this._sqlFunctions = new SqlFunctions(this);
        }
        #endregion

        #region CreateDbObject
        /// <summary>
        /// 创建新的数据库参数
        /// </summary>
        /// <returns>数据库参数</returns>
        public DbParameter CreateDbParameter()
        {
            return this._dbProvider.CreateParameter();
        }

        /// <summary>
        /// 从Sql语句参数中创建数据库参数
        /// </summary>
        /// <param name="param">Sql语句参数</param>
        /// <returns>数据库参数</returns>
        public DbParameter CreateDbParameter(DataParameter param)
        {
            DbParameter dbParameter = this.CreateDbParameter();

            return param.InternalCopyToDbParameter(dbParameter);
        }

        /// <summary>
        /// 创建新的数据库命令
        /// </summary>
        /// <returns>数据库命令</returns>
        public DbCommand CreateDbCommand()
        {
            return this._dbProvider.CreateCommand();
        }

        /// <summary>
        /// 创建新的数据库命令
        /// </summary>
        /// <param name="commandText">Sql语句内容</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>数据库命令</returns>
        public DbCommand CreateDbCommand(String commandText, params DataParameter[] parameters)
        {
            DbCommand dbCommand = this.CreateDbCommand();
            dbCommand.CommandType = CommandType.Text;
            dbCommand.CommandText = commandText;

            if (parameters != null)
            {
                for (Int32 i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i].IsUseParameter)
                    {
                        dbCommand.Parameters.Add(this.CreateDbParameter(parameters[i]));
                    }
                }
            }

            return dbCommand;
        }

        /// <summary>
        /// 添加参数到数据库命令中
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <param name="extraParameters">额外参数组</param>
        public void AddParameterToDbCommand(DbCommand dbCommand, params DataParameter[] extraParameters)
        {
            if (extraParameters != null)
            {
                for (Int32 i = 0; i < extraParameters.Length; i++)
                {
                    if (extraParameters[i].IsUseParameter)
                    {
                        dbCommand.Parameters.Add(this.CreateDbParameter(extraParameters[i]));
                    }
                }
            }
        }

        /// <summary>
        /// 创建新的数据库连接
        /// </summary>
        /// <returns>数据库连接</returns>
        public DbConnection CreateDbConnection()
        {
            DbConnection conn = this._dbProvider.CreateConnection();
            conn.ConnectionString = this._connectionString;

            return conn;
        }

        /// <summary>
        /// 创建新的数据库事务
        /// </summary>
        /// <returns>数据库事务</returns>
        public DbTransaction CreateDbTransaction()
        {
            DbConnection conn = this.CreateDbConnection();
            conn.Open();

            return conn.BeginTransaction();
        }
        #endregion

        #region CreateSqlCommand
        /// <summary>
        /// 创建新的Sql插入语句
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <returns>Sql插入语句</returns>
        public InsertCommand CreateInsertCommand(String tableName)
        {
            return new InsertCommand(this, null, tableName);
        }
        
        /// <summary>
        /// 创建新的Sql更新语句
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <returns>Sql更新语句</returns>
        public UpdateCommand CreateUpdateCommand(String tableName)
        {
            return new UpdateCommand(this, null, tableName);
        }

        /// <summary>
        /// 创建新的Sql删除语句
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <returns>Sql删除语句</returns>
        public DeleteCommand CreateDeleteCommand(String tableName)
        {
            return new DeleteCommand(this, null, tableName);
        }

        /// <summary>
        /// 创建新的Sql选择语句
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <returns>Sql选择语句</returns>
        public SelectCommand CreateSelectCommand(String tableName)
        {
            return new SelectCommand(this, null, tableName);
        }

        /// <summary>
        /// 创建新的Sql选择语句
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="tableAliasesName">数据表别名</param>
        /// <returns>Sql选择语句</returns>
        public SelectCommand CreateSelectCommand(String tableName, String tableAliasesName)
        {
            return new SelectCommand(this, null, tableName + ' ' + tableAliasesName);
        }

        /// <summary>
        /// 创建新的自选择Sql选择语句
        /// </summary>
        /// <param name="innerTableName">内部数据表名称</param>
        /// <param name="innerTableAliasesName">内部数据表别名</param>
        /// <param name="createInnerCommandAction">创建内部语句的方法</param>
        /// <returns>Sql选择语句</returns>
        public SelectCommand CreateSelectCommand(String innerTableName, String innerTableAliasesName, Action<SelectCommand> createInnerCommandAction)
        {
            SelectCommand innerCommand = this.CreateSelectCommand(innerTableName);
            createInnerCommandAction(innerCommand);

            SelectCommand outerCommand = new SelectCommand(this, innerCommand, innerCommand, innerTableAliasesName);
            outerCommand.InternalAddDataParameterList(innerCommand.GetAllParameters());

            return outerCommand;
        }

        /// <summary>
        /// 创建新的Sql自定义语句
        /// </summary>
        /// <param name="commandType">Sql语句类型</param>
        /// <param name="commandString">语句内容</param>
        /// <returns>Sql自定义语句</returns>
        public CustomCommand CreateCustomCommand(SqlCommandType commandType, String commandString)
        {
            return new CustomCommand(this, commandType, commandString);
        }

        /// <summary>
        /// 创建新的Sql语句集合
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <returns>Sql语句集合</returns>
        public CommandCollection CreateCommandSequence(String tableName)
        {
            return new CommandCollection(this, tableName);
        }
        #endregion

        #region InternalCreateSqlCommand
        /// <summary>
        /// 创建新的Sql选择语句
        /// </summary>
        /// <param name="rootSource">创建时的根来源</param>
        /// <param name="tableName">数据表名称</param>
        /// <returns>Sql选择语句</returns>
        internal SelectCommand InternalCreateSelectCommand(AbstractSqlCommand rootSource, String tableName)
        {
            return new SelectCommand(this, rootSource, tableName);
        }

        /// <summary>
        /// 创建新的Sql选择语句
        /// </summary>
        /// <param name="rootSource">创建时的根来源</param>
        /// <param name="tableName">数据表名称</param>
        /// <param name="tableAliasesName">数据表别名</param>
        /// <returns>Sql选择语句</returns>
        internal SelectCommand InternalCreateSelectCommand(AbstractSqlCommand rootSource, String tableName, String tableAliasesName)
        {
            return new SelectCommand(this, rootSource, tableName + ' ' + tableAliasesName);
        }

        /// <summary>
        /// 创建新的Sql选择语句
        /// </summary>
        /// <param name="rootSource">创建时的根来源</param>
        /// <param name="from">选择的从Sql语句</param>
        /// <param name="fromAliasesName">从Sql语句的别名</param>
        /// <returns>Sql选择语句</returns>
        internal SelectCommand InternalCreateSelectCommand(AbstractSqlCommand rootSource, SelectCommand from, String fromAliasesName)
        {
            return new SelectCommand(this, rootSource, from, fromAliasesName);
        }
        #endregion

        #region UsingConnection/Transaction
        /// <summary>
        /// 使用持续数据库连接执行操作
        /// </summary>
        /// <param name="action">使用持续连接的操作</param>
        public void UsingConnection(Action<DbConnection> action)
        {
            this.UsingConnection(connection =>
            {
                action(connection);
                return true;
            });
        }

        /// <summary>
        /// 使用持续数据库连接执行操作
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="function">使用持续连接的操作</param>
        /// <returns>内部返回内容</returns>
        public T UsingConnection<T>(Func<DbConnection, T> function)
        {
            DbConnection connection = null;
            T result = default(T);

            try
            {
                connection = this.CreateDbConnection();
                connection.Open();

                result = function(connection);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                    connection = null;
                }
            }

            return result;
        }

        /// <summary>
        /// 使用数据库事务执行操作
        /// </summary>
        /// <param name="action">使用事务的操作</param>
        public void UsingTransaction(Action<DbTransaction> action)
        {
            this.UsingTransaction(transaction =>
            {
                action(transaction);
                return true;
            });
        }

        /// <summary>
        /// 使用数据库事务执行操作
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="function">使用事务的操作</param>
        /// <returns>内部返回内容</returns>
        public T UsingTransaction<T>(Func<DbTransaction, T> function)
        {
            DbConnection connection = null;
            DbTransaction transaction = null;
            T result = default(T);

            try
            {
                transaction = this.CreateDbTransaction();
                connection = transaction.Connection;

                result = function(transaction);
            }
            catch
            {
                result = default(T);

                throw;
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                    transaction = null;
                }

                if (connection != null)
                {
                    connection.Dispose();
                    connection = null;
                }
            }

            return result;
        }
        #endregion

        #region UsingDataReader
        /// <summary>
        /// 使用数据库读取器执行操作
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="transaction">数据库事务</param>
        /// <param name="action">使用数据库读取器的操作</param>
        public void UsingDataReader(ISqlCommand command, DbTransaction transaction, Action<IDataReader> action)
        {
            this.UsingDataReader(command, transaction, reader =>
            {
                action(reader);
                return true;
            });
        }

        /// <summary>
        /// 使用数据库读取器执行操作
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="command">指定Sql语句</param>
        /// <param name="transaction">数据库事务</param>
        /// <param name="function">使用数据库读取器的操作</param>
        /// <returns>返回的内容</returns>
        public T UsingDataReader<T>(ISqlCommand command, DbTransaction transaction, Func<IDataReader, T> function)
        {
            IDataReader reader = null;

            try
            {
                reader = this.ExecuteReader(command, transaction);
                return function(reader);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
        }

        /// <summary>
        /// 使用数据库读取器执行操作
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="action">使用数据库读取器的操作</param>
        public void UsingDataReader(ISqlCommand command, DbConnection connection, Action<IDataReader> action)
        {
            this.UsingDataReader(command, connection, reader =>
            {
                action(reader);
                return true;
            });
        }

        /// <summary>
        /// 使用数据库读取器执行操作
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="command">指定Sql语句</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="function">使用数据库读取器的操作</param>
        /// <returns>返回的内容</returns>
        public T UsingDataReader<T>(ISqlCommand command, DbConnection connection, Func<IDataReader, T> function)
        {
            IDataReader reader = null;

            try
            {
                reader = this.ExecuteReader(command, connection);
                return function(reader);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
        }

        /// <summary>
        /// 使用数据库读取器执行操作
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="action">使用数据库读取器的操作</param>
        public void UsingDataReader(ISqlCommand command, Action<IDataReader> action)
        {
            this.UsingDataReader(command, reader =>
            {
                action(reader);
                return true;
            });
        }

        /// <summary>
        /// 使用数据库读取器执行操作
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="command">指定Sql语句</param>
        /// <param name="function">使用数据库读取器的操作</param>
        /// <returns>返回的内容</returns>
        public T UsingDataReader<T>(ISqlCommand command, Func<IDataReader, T> function)
        {
            DatabaseConnectionWrapper wrapper = null;
            IDataReader reader = null;

            try
            {
                wrapper = this.InternalGetConnection();

                reader = this.ExecuteReader(command, wrapper.Connection);
                return function(reader);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }

                if (wrapper != null)
                {
                    wrapper.Dispose();
                }
            }
        }
        #endregion

        #region ExecuteDbCommandWithConnectionAndTransaction
        /// <summary>
        /// 将数据库连接和数据库事务添加到数据库命令中
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="transaction">数据库事务</param>
        private void AddTransactionAndConnectionToDbCommand(DbCommand dbCommand, DbConnection connection, DbTransaction transaction)
        {
            if (transaction == null && connection == null)
            {
                return;
            }

            if (connection == null)
            {
                dbCommand.Connection = transaction.Connection;
            }
            else
            {
                dbCommand.Connection = connection;
            }

            if (transaction != null)
            {
                dbCommand.Transaction = transaction;
            }
        }

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">数据库命令不能为空</exception>
        /// <returns>返回的结果</returns>
        public Object ExecuteScalar(DbCommand dbCommand, DbConnection connection, DbTransaction transaction)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            Object result;

            if (connection == null && transaction == null)
            {
                using (DatabaseConnectionWrapper wrapper = this.InternalGetConnection())
                {
                    dbCommand.Connection = wrapper.Connection;

                    result = dbCommand.ExecuteScalar();
                }
            }
            else
            {
                this.AddTransactionAndConnectionToDbCommand(dbCommand, connection, transaction);

                result = dbCommand.ExecuteScalar();
            }

            return result;
        }

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="dbCommand">数据库命令</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns>返回的结果</returns>
        public T ExecuteScalar<T>(DbCommand dbCommand, DataType dataType, DbConnection connection, DbTransaction transaction)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            Object obj = this.ExecuteScalar(dbCommand, connection, transaction);

            return (DbConvert.IsDBNull(obj) ? default(T) : (T)DbConvert.ToValue(obj, dataType));
        }

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="dbCommand">数据库命令</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="transaction">数据库事务</param>
        /// <returns>返回的结果</returns>
        public T ExecuteScalar<T>(DbCommand dbCommand, DbConnection connection, DbTransaction transaction)
        {
            DataType dataType = DataTypeHelper.InternalGetDataType(typeof(T));

            return this.ExecuteScalar<T>(dbCommand, dataType, connection, transaction);
        }

        /// <summary>
        /// 返回执行指定Sql语句后影响的行数
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">数据库命令不能为空</exception>
        /// <returns>受影响的行数</returns>
        public Int32 ExecuteNonQuery(DbCommand dbCommand, DbConnection connection, DbTransaction transaction)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            Int32 result;

            if (connection == null && transaction == null)
            {
                using (DatabaseConnectionWrapper wrapper = this.InternalGetConnection())
                {
                    dbCommand.Connection = wrapper.Connection;

                    result = dbCommand.ExecuteNonQuery();
                }
            }
            else
            {
                this.AddTransactionAndConnectionToDbCommand(dbCommand, connection, transaction);

                result = dbCommand.ExecuteNonQuery();
            }

            return result;
        }

        /// <summary>
        /// 获得指定Sql语句查询下的数据读取器
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">数据库命令不能为空，数据库连接和事务不能同时为空</exception>
        /// <returns>数据读取器</returns>
        public IDataReader ExecuteReader(DbCommand dbCommand, DbConnection connection, DbTransaction transaction)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            if (connection == null && transaction == null)
            {
                throw new ArgumentNullException("connection");
            }

            this.AddTransactionAndConnectionToDbCommand(dbCommand, connection, transaction);
            IDataReader result = dbCommand.ExecuteReader();

            return result;
        }

        /// <summary>
        /// 获得指定Sql语句查询下的数据集
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">数据库命令不能为空</exception>
        /// <returns>数据集</returns>
        public DataSet ExecuteDataSet(DbCommand dbCommand, DbConnection connection, DbTransaction transaction)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            DataSet dataSet = null;

            if (connection == null && transaction == null)
            {
                using (DatabaseConnectionWrapper wrapper = this.InternalGetConnection())
                {
                    dbCommand.Connection = wrapper.Connection;

                    dataSet = DataSetHelper.InternalCreateDataSet(this._dbProvider, dbCommand);
                }
            }
            else
            {
                this.AddTransactionAndConnectionToDbCommand(dbCommand, connection, transaction);

                dataSet = DataSetHelper.InternalCreateDataSet(this._dbProvider, dbCommand);
            }

            return dataSet;
        }

        /// <summary>
        /// 获得指定Sql语句查询下的数据表
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">数据库命令不能为空</exception>
        /// <returns>数据表</returns>
        public DataTable ExecuteDataTable(DbCommand dbCommand, DbConnection connection, DbTransaction transaction)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            DataSet dataSet = this.ExecuteDataSet(dbCommand, connection, transaction);
            return (dataSet != null && dataSet.Tables != null && dataSet.Tables.Count > 0 ? dataSet.Tables[0] : null);
        }

        /// <summary>
        /// 获得指定Sql语句查询下的单行数据
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">数据库命令不能为空</exception>
        /// <returns>单行数据</returns>
        public DataRow ExecuteDataRow(DbCommand dbCommand, DbConnection connection, DbTransaction transaction)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            DataTable dataTable = this.ExecuteDataTable(dbCommand, connection, transaction);
            return (dataTable != null && dataTable.Rows != null && dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null);
        }
        #endregion

        #region ExecuteDbCommand
        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <exception cref="ArgumentNullException">数据库命令不能为空</exception>
        /// <returns>返回的结果</returns>
        public Object ExecuteScalar(DbCommand dbCommand)
        {
            return this.ExecuteScalar(dbCommand, null, null);
        }

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="dbCommand">数据库命令</param>
        /// <param name="dataType">数据类型</param>
        /// <returns>返回的结果</returns>
        public T ExecuteScalar<T>(DbCommand dbCommand, DataType dataType)
        {
            return this.ExecuteScalar<T>(dbCommand, dataType, null, null);
        }

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="dbCommand">数据库命令</param>
        /// <returns>返回的结果</returns>
        public T ExecuteScalar<T>(DbCommand dbCommand)
        {
            return this.ExecuteScalar<T>(dbCommand, null, null);
        }

        /// <summary>
        /// 返回执行指定Sql语句后影响的行数
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <exception cref="ArgumentNullException">数据库命令不能为空</exception>
        /// <returns>受影响的行数</returns>
        public Int32 ExecuteNonQuery(DbCommand dbCommand)
        {
            return this.ExecuteNonQuery(dbCommand, null, null);
        }

        /// <summary>
        /// 获得指定Sql语句查询下的数据集
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <exception cref="ArgumentNullException">数据库命令不能为空</exception>
        /// <returns>数据集</returns>
        public DataSet ExecuteDataSet(DbCommand dbCommand)
        {
            return this.ExecuteDataSet(dbCommand, null, null);
        }

        /// <summary>
        /// 获得指定Sql语句查询下的数据表
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <exception cref="ArgumentNullException">数据库命令不能为空</exception>
        /// <returns>数据表</returns>
        public DataTable ExecuteDataTable(DbCommand dbCommand)
        {
            return this.ExecuteDataTable(dbCommand, null, null);
        }

        /// <summary>
        /// 获得指定Sql语句查询下的单行数据
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <exception cref="ArgumentNullException">数据库命令不能为空</exception>
        /// <returns>单行数据</returns>
        public DataRow ExecuteDataRow(DbCommand dbCommand)
        {
            return this.ExecuteDataRow(dbCommand, null, null);
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="command">指定Sql语句</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        public T ExecuteScalar<T>(ISqlCommand command, DbTransaction transaction)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteScalar<T>(dbCommand, null, transaction);
        }

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="command">指定Sql语句</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        public T ExecuteScalar<T>(ISqlCommand command, DbConnection connection)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteScalar<T>(dbCommand, connection, null);
        }

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

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteScalar<T>(dbCommand);
        }

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="command">指定Sql语句</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        public T ExecuteScalar<T>(ISqlCommand command, DataType dataType, DbTransaction transaction)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteScalar<T>(dbCommand, dataType, null, transaction);
        }

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="command">指定Sql语句</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        public T ExecuteScalar<T>(ISqlCommand command, DataType dataType, DbConnection connection)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteScalar<T>(dbCommand, dataType, connection, null);
        }

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="command">指定Sql语句</param>
        /// <param name="dataType">数据类型</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        public T ExecuteScalar<T>(ISqlCommand command, DataType dataType)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteScalar<T>(dbCommand, dataType);
        }

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        public Object ExecuteScalar(ISqlCommand command, DbTransaction transaction)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteScalar(dbCommand, null, transaction);
        }

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        public Object ExecuteScalar(ISqlCommand command, DbConnection connection)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteScalar(dbCommand, connection, null);
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

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteScalar(dbCommand);
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// 返回执行指定Sql语句后影响的行数
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>受影响的行数</returns>
        public Int32 ExecuteNonQuery(ISqlCommand command, DbTransaction transaction)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteNonQuery(dbCommand, null, transaction);
        }

        /// <summary>
        /// 返回执行指定Sql语句后影响的行数
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>受影响的行数</returns>
        public Int32 ExecuteNonQuery(ISqlCommand command, DbConnection connection)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteNonQuery(dbCommand, connection, null);
        }

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

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 返回执行多个指定Sql语句后影响的行数
        /// </summary>
        /// <param name="commands">指定Sql语句组</param>
        /// <returns>受影响的行数</returns>
        public Int32 ExecuteNonQuery(params ISqlCommand[] commands)
        {
            IEnumerable<ISqlCommand> collection = commands;
            return this.ExecuteNonQuery(collection);
        }

        /// <summary>
        /// 返回执行多个指定Sql语句后影响的行数
        /// </summary>
        /// <param name="commands">指定Sql语句组</param>
        /// <returns>受影响的行数</returns>
        public Int32 ExecuteNonQuery(IEnumerable<ISqlCommand> commands)
        {
            if (commands == null)
            {
                return 0;
            }

            Int32 result = this.UsingTransaction(transaction =>
            {
                Int32 count = 0;

                foreach (ISqlCommand command in commands)
                {
                    count += this.ExecuteNonQuery(command, transaction);
                }

                transaction.Commit();

                return count;
            });

            return result;
        }
        #endregion

        #region ExecuteReader
        /// <summary>
        /// 获得指定Sql语句查询下的数据读取器
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>数据读取器</returns>
        public IDataReader ExecuteReader(ISqlCommand command, DbTransaction transaction)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteReader(dbCommand, null, transaction);
        }

        /// <summary>
        /// 获得指定Sql语句查询下的数据读取器
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>数据读取器</returns>
        public IDataReader ExecuteReader(ISqlCommand command, DbConnection connection)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteReader(dbCommand, connection, null);
        }
        #endregion

        #region ExecuteDataSet/Table/Row
        /// <summary>
        /// 获得指定Sql语句查询下的数据集
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>数据集</returns>
        public DataSet ExecuteDataSet(ISqlCommand command, DbTransaction transaction)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteDataSet(dbCommand, null, transaction);
        }

        /// <summary>
        /// 获得指定Sql语句查询下的数据集
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>数据集</returns>
        public DataSet ExecuteDataSet(ISqlCommand command, DbConnection connection)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteDataSet(dbCommand, connection, null);
        }

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

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteDataSet(dbCommand);
        }

        /// <summary>
        /// 获得指定Sql语句查询下的数据表
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>数据表</returns>
        public DataTable ExecuteDataTable(ISqlCommand command, DbTransaction transaction)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteDataTable(dbCommand, null, transaction);
        }

        /// <summary>
        /// 获得指定Sql语句查询下的数据表
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>数据表</returns>
        public DataTable ExecuteDataTable(ISqlCommand command, DbConnection connection)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteDataTable(dbCommand, connection, null);
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

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteDataTable(dbCommand);
        }

        /// <summary>
        /// 获得指定Sql语句查询下的单行数据
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>单行数据</returns>
        public DataRow ExecuteDataRow(ISqlCommand command, DbTransaction transaction)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteDataRow(dbCommand, null, transaction);
        }

        /// <summary>
        /// 获得指定Sql语句查询下的单行数据
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>单行数据</returns>
        public DataRow ExecuteDataRow(ISqlCommand command, DbConnection connection)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteDataRow(dbCommand, connection, null);
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

            DbCommand dbCommand = command.ToDbCommand();
            return this.ExecuteDataRow(dbCommand);
        }
        #endregion

        #region 抽象方法
        /// <summary>
        /// 获取分页后的选择语句
        /// </summary>
        /// <param name="sourceCommand">源选择语句</param>
        /// <param name="orderReverse">是否反转</param>
        /// <returns>分页后的选择语句</returns>
        internal abstract String InternalGetPagerSelectCommand(SelectCommand sourceCommand, Boolean orderReverse);

        /// <summary>
        /// 获取部分日期类型字符串表示
        /// </summary>
        /// <param name="type">部分日期类型</param>
        /// <returns>部分日期类型字符串表示</returns>
        internal abstract String InternalGetDatePart(Byte type);
        #endregion

        #region 虚拟方法
        /// <summary>
        /// 获取分页后的选择语句
        /// </summary>
        /// <param name="sourceCommand">源选择语句</param>
        /// <param name="recordCount">记录数量</param>
        /// <param name="orderReverse">是否反转</param>
        /// <returns>分页后的选择语句</returns>
        internal virtual String InternalGetPagerSelectCommand(SelectCommand sourceCommand, Int32 recordCount, Boolean orderReverse)
        {
            return this.InternalGetPagerSelectCommand(sourceCommand, orderReverse);
        }

        /// <summary>
        /// 获取最后一条记录需要查询的名称
        /// </summary>
        /// <returns>需要查询的名称</returns>
        internal virtual String InternalGetIdentityFieldName()
        {
            return "@@IDENTITY";
        }

        /// <summary>
        /// 获取参数名
        /// </summary>
        /// <param name="parameterName">原始参数名</param>
        /// <returns>参数名</returns>
        internal virtual String InternalGetParameterName(String parameterName)
        {
            return "@" + parameterName;
        }

        /// <summary>
        /// 获取判断为空函数
        /// </summary>
        /// <param name="parameter">函数条件</param>
        /// <param name="contentTwo">内容2</param>
        /// <returns>判断为空函数</returns>
        internal virtual String InternalGetIsNullFunction(String parameter, String contentTwo)
        {
            return String.Format("ISNULL({0}, {1})", parameter, contentTwo);
        }

        /// <summary>
        /// 获取大写函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <returns>大写函数</returns>
        internal virtual String InternalGetUpperFunction(String parameter)
        {
            return String.Format("UPPER({0})", parameter);
        }

        /// <summary>
        /// 获取小写函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <returns>小写函数</returns>
        internal virtual String InternalGetLowerFunction(String parameter)
        {
            return String.Format("LOWER({0})", parameter);
        }

        /// <summary>
        /// 获取去除左边空格函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <returns>去除左边空格函数</returns>
        internal virtual String InternalGetLTrimFunction(String parameter)
        {
            return String.Format("LTRIM({0})", parameter);
        }

        /// <summary>
        /// 获取去除右边空格函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <returns>去除右边空格函数</returns>
        internal virtual String InternalGetRTrimFunction(String parameter)
        {
            return String.Format("RTRIM({0})", parameter);
        }

        /// <summary>
        /// 获取去除两段空格函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <returns>去除两段空格函数</returns>
        internal virtual String InternalGetTrimFunction(String parameter)
        {
            return String.Format("TRIM({0})", parameter);
        }

        /// <summary>
        /// 获取字符串长度函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <returns>字符串长度函数</returns>
        internal virtual String InternalGetLengthFunction(String parameter)
        {
            return String.Format("LEN({0})", parameter);
        }

        /// <summary>
        /// 获取字符串提取函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <param name="start">子串起始位置</param>
        /// <param name="length">子串长度</param>
        /// <returns>字符串提取函数</returns>
        internal virtual String InternalGetMidFunction(String parameter, String start, String length)
        {
            return String.Format("SUBSTRING({0}, {1}, {2})", parameter, start, length);
        }

        /// <summary>
        /// 获取取整函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <param name="decimals">小数位数</param>
        /// <returns>取整函数</returns>
        internal virtual String InternalGetRoundFunction(String parameter, String decimals)
        {
            return String.Format("ROUND({0}, {1})", parameter, decimals);
        }

        /// <summary>
        /// 获取当前日期函数
        /// </summary>
        /// <returns>当前日期函数</returns>
        internal virtual String InternalGetNowFunction()
        {
            return String.Format("GETDATE()");
        }

        /// <summary>
        /// 获取当前日期函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <param name="datepartType">获取部分的类型</param>
        /// <returns>当前日期函数</returns>
        internal virtual String InternalGetDatePartFunction(String parameter, String datepartType)
        {
            return String.Format("DATEPART({0}, {1})", this.InternalGetDatePart(Convert.ToByte(datepartType)), parameter);
        }
        #endregion

        #region 内部方法
        internal DatabaseConnectionWrapper InternalGetNewConnection()
        {
            DbConnection connection = null;

            try
            {
                connection = this.CreateDbConnection();

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

        internal DatabaseConnectionWrapper InternalGetConnection()
        {
            DatabaseConnectionWrapper connection = TransactionScopeConnections.GetConnection(this);
            return connection ?? this.InternalGetNewConnection();
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