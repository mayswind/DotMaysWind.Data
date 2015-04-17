using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Command.Function;

namespace DotMaysWind.Data
{
    /// <summary>
    /// 数据库接口
    /// </summary>
    public interface IDatabase
    {
        #region 属性
        /// <summary>
        /// 获取当前数据库类型
        /// </summary>
        DatabaseType DatabaseType { get; }

        /// <summary>
        /// 获取当前数据库数据库提供者名称
        /// </summary>
        String ProviderName { get; }

        /// <summary>
        /// 获取当前数据库连接字符串
        /// </summary>
        String ConnectionString { get; }

        /// <summary>
        /// 获取Sql数据库支持的函数
        /// </summary>
        SqlFunctions Functions { get; }
        #endregion

        #region CreateDbObject
        /// <summary>
        /// 创建新的数据库参数
        /// </summary>
        /// <returns>数据库参数</returns>
        DbParameter CreateDbParameter();

        /// <summary>
        /// 创建新的数据库命令
        /// </summary>
        /// <returns>数据库命令</returns>
        DbCommand CreateDbCommand();

        /// <summary>
        /// 创建新的数据库连接
        /// </summary>
        /// <returns>数据库连接</returns>
        DbConnection CreateDbConnection();

        /// <summary>
        /// 创建新的数据库事务
        /// </summary>
        /// <returns>数据库事务</returns>
        DbTransaction CreateDbTransaction();
        #endregion

        #region CreateSqlCommand
        /// <summary>
        /// 创建新的Sql插入语句
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <returns>Sql插入语句</returns>
        InsertCommand CreateInsertCommand(String tableName);

        /// <summary>
        /// 创建新的Sql更新语句
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <returns>Sql更新语句</returns>
        UpdateCommand CreateUpdateCommand(String tableName);

        /// <summary>
        /// 创建新的Sql删除语句
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <returns>Sql删除语句</returns>
        DeleteCommand CreateDeleteCommand(String tableName);

        /// <summary>
        /// 创建新的Sql选择语句
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <returns>Sql选择语句</returns>
        SelectCommand CreateSelectCommand(String tableName);

        /// <summary>
        /// 创建新的Sql选择语句
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="tableAliasesName">数据表别名</param>
        /// <returns>Sql选择语句</returns>
        SelectCommand CreateSelectCommand(String tableName, String tableAliasesName);

        /// <summary>
        /// 创建新的Sql选择语句
        /// </summary>
        /// <param name="from">选择的从Sql语句</param>
        /// <param name="fromAliasesName">从Sql语句的别名</param>
        /// <returns>Sql选择语句</returns>
        SelectCommand CreateSelectCommand(SelectCommand from, String fromAliasesName);

        /// <summary>
        /// 创建新的Sql自定义语句
        /// </summary>
        /// <param name="commandType">Sql语句类型</param>
        /// <param name="commandString">语句内容</param>
        /// <returns>Sql自定义语句</returns>
        CustomCommand CreateCustomCommand(SqlCommandType commandType, String commandString);

        /// <summary>
        /// 创建新的Sql语句集合
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <returns>Sql语句集合</returns>
        CommandCollection CreateCommandSequence(String tableName);
        #endregion

        #region UsingConnection/Transaction
        /// <summary>
        /// 使用持续数据库连接执行操作
        /// </summary>
        /// <param name="action">使用持续连接的操作</param>
        /// <returns>内部返回内容</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// 
        /// db.UsingConnection(c =>
        /// {
        ///     InsertCommand cmd1 = db.CreateInsertCommand("Table1");
        ///     InsertCommand cmd2 = db.CreateInsertCommand("Table2");
        /// 
        ///     db.ExecuteNonQuery(cmd1, c);
        ///     db.ExecuteNonQuery(cmd2, c);
        /// });
        /// ]]>
        /// </code>
        /// </example>
        void UsingConnection(Action<DbConnection> action);

        /// <summary>
        /// 使用持续数据库连接执行操作
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="function">使用持续连接的操作</param>
        /// <returns>内部返回内容</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// 
        /// db.UsingConnection(c =>
        /// {
        ///     SelectCommand cmd1 = db.CreateSelectCommand("Table1");
        ///     SelectCommand cmd2 = db.CreateSelectCommand("Table2");
        /// 
        ///     Int32 count1 = cmd1.Count(c);
        ///     Int32 count2 = cmd2.Count(c);
        /// 
        ///     return count1 + count2;
        /// });
        /// ]]>
        /// </code>
        /// </example>
        T UsingConnection<T>(Func<DbConnection, T> function);

        /// <summary>
        /// 使用数据库事务执行操作
        /// </summary>
        /// <param name="action">使用事务的操作</param>
        /// <returns>内部返回内容</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// 
        /// db.UsingTransaction(t =>
        /// {
        ///     try
        ///     {
        ///         InsertCommand cmd1 = db.CreateInsertCommand("Table1");
        ///         InsertCommand cmd2 = db.CreateInsertCommand("Table2");
        /// 
        ///         db.ExecuteNonQuery(cmd1, t);
        ///         db.ExecuteNonQuery(cmd2, t);
        /// 
        ///         t.Commit();
        ///     }
        ///     catch (Exception ex)
        ///     {
        ///         t.Rollback();
        ///     }
        /// });
        /// ]]>
        /// </code>
        /// </example>
        void UsingTransaction(Action<DbTransaction> action);

        /// <summary>
        /// 使用数据库事务执行操作
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="function">使用事务的操作</param>
        /// <returns>内部返回内容</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// 
        /// db.UsingTransaction(t =>
        /// {
        ///     try
        ///     {
        ///         Int32 result = 0;
        /// 
        ///         InsertCommand cmd1 = db.CreateInsertCommand("Table1");
        ///         InsertCommand cmd2 = db.CreateInsertCommand("Table2");
        /// 
        ///         result += db.ExecuteNonQuery(cmd1, t);
        ///         result += db.ExecuteNonQuery(cmd2, t);
        /// 
        ///         t.Commit();
        /// 
        ///         return result;
        ///     }
        ///     catch (Exception ex)
        ///     {
        ///         t.Rollback();
        /// 
        ///         return -1;
        ///     }
        /// });
        /// ]]>
        /// </code>
        /// </example>
        T UsingTransaction<T>(Func<DbTransaction, T> function);
        #endregion

        #region UsingDataReader
        /// <summary>
        /// 使用数据库读取器执行操作
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="transaction">数据库事务</param>
        /// <param name="action">使用数据库读取器的操作</param>
        void UsingDataReader(ISqlCommand command, DbTransaction transaction, Action<IDataReader> action);

        /// <summary>
        /// 使用数据库读取器执行操作
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="command">指定Sql语句</param>
        /// <param name="transaction">数据库事务</param>
        /// <param name="function">使用数据库读取器的操作</param>
        /// <returns>返回的内容</returns>
        T UsingDataReader<T>(ISqlCommand command, DbTransaction transaction, Func<IDataReader, T> function);

        /// <summary>
        /// 使用数据库读取器执行操作
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="action">使用数据库读取器的操作</param>
        void UsingDataReader(ISqlCommand command, DbConnection connection, Action<IDataReader> action);

        /// <summary>
        /// 使用数据库读取器执行操作
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="command">指定Sql语句</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="function">使用数据库读取器的操作</param>
        /// <returns>返回的内容</returns>
        T UsingDataReader<T>(ISqlCommand command, DbConnection connection, Func<IDataReader, T> function);

        /// <summary>
        /// 使用数据库读取器执行操作
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="action">使用数据库读取器的操作</param>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("Table1");
        /// 
        /// List<String> result = new List<String>();
        /// 
        /// db.UsingDataReader(cmd, r =>
        /// {
        ///     while (r.Read())
        ///     {
        ///         result.Add(r["Column1"] as String);
        ///     }
        /// });
        /// ]]>
        /// </code>
        /// </example>
        void UsingDataReader(ISqlCommand command, Action<IDataReader> action);

        /// <summary>
        /// 使用数据库读取器执行操作
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="command">指定Sql语句</param>
        /// <param name="function">使用数据库读取器的操作</param>
        /// <returns>返回的内容</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// IDatabase db = DatabaseFactory.CreateDatabase();
        /// SelectCommand cmd = db.CreateSelectCommand("Table1");
        /// 
        /// List<String> result = db.UsingDataReader(cmd, r =>
        /// {
        ///     List<String> list = new List<String>();
        /// 
        ///     while (r.Read())
        ///     {
        ///         list.Add(r["Column1"] as String);
        ///     }
        /// 
        ///     return list;
        /// });
        /// ]]>
        /// </code>
        /// </example>
        T UsingDataReader<T>(ISqlCommand command, Func<IDataReader, T> function);
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
        T ExecuteScalar<T>(ISqlCommand command, DbTransaction transaction);

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="command">指定Sql语句</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        T ExecuteScalar<T>(ISqlCommand command, DbConnection connection);

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="command">指定Sql语句</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        T ExecuteScalar<T>(ISqlCommand command);

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="command">指定Sql语句</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        T ExecuteScalar<T>(ISqlCommand command, DataType dataType, DbTransaction transaction);

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="command">指定Sql语句</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        T ExecuteScalar<T>(ISqlCommand command, DataType dataType, DbConnection connection);

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="command">指定Sql语句</param>
        /// <param name="dataType">数据类型</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        T ExecuteScalar<T>(ISqlCommand command, DataType dataType);

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        Object ExecuteScalar(ISqlCommand command, DbTransaction transaction);

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        Object ExecuteScalar(ISqlCommand command, DbConnection connection);

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        Object ExecuteScalar(ISqlCommand command);
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// 返回执行指定Sql语句后影响的行数
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>受影响的行数</returns>
        Int32 ExecuteNonQuery(ISqlCommand command, DbTransaction transaction);

        /// <summary>
        /// 返回执行指定Sql语句后影响的行数
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>受影响的行数</returns>
        Int32 ExecuteNonQuery(ISqlCommand command, DbConnection connection);

        /// <summary>
        /// 返回执行指定Sql语句后影响的行数
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>受影响的行数</returns>
        Int32 ExecuteNonQuery(ISqlCommand command);

        /// <summary>
        /// 返回执行多个指定Sql语句后影响的行数
        /// </summary>
        /// <param name="commands">指定Sql语句组</param>
        /// <returns>受影响的行数</returns>
        Int32 ExecuteNonQuery(params ISqlCommand[] commands);

        /// <summary>
        /// 返回执行多个指定Sql语句后影响的行数
        /// </summary>
        /// <param name="commands">指定Sql语句组</param>
        /// <returns>受影响的行数</returns>
        Int32 ExecuteNonQuery(IEnumerable<ISqlCommand> commands);
        #endregion

        #region ExecuteReader
        /// <summary>
        /// 获得指定Sql语句查询下的数据读取器
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>数据读取器</returns>
        IDataReader ExecuteReader(ISqlCommand command, DbTransaction transaction);

        /// <summary>
        /// 获得指定Sql语句查询下的数据读取器
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>数据读取器</returns>
        IDataReader ExecuteReader(ISqlCommand command, DbConnection connection);
        #endregion

        #region ExecuteDataSet/Table/Row
        /// <summary>
        /// 获得指定Sql语句查询下的数据集
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>数据集</returns>
        DataSet ExecuteDataSet(ISqlCommand command, DbTransaction transaction);

        /// <summary>
        /// 获得指定Sql语句查询下的数据集
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>数据集</returns>
        DataSet ExecuteDataSet(ISqlCommand command, DbConnection connection);

        /// <summary>
        /// 获得指定Sql语句查询下的数据集
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>数据集</returns>
        DataSet ExecuteDataSet(ISqlCommand command);

        /// <summary>
        /// 获得指定Sql语句查询下的数据表
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(ISqlCommand command, DbTransaction transaction);

        /// <summary>
        /// 获得指定Sql语句查询下的数据表
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(ISqlCommand command, DbConnection connection);

        /// <summary>
        /// 获得指定Sql语句查询下的数据表
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(ISqlCommand command);

        /// <summary>
        /// 获得指定Sql语句查询下的单行数据
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>单行数据</returns>
        DataRow ExecuteDataRow(ISqlCommand command, DbTransaction transaction);

        /// <summary>
        /// 获得指定Sql语句查询下的单行数据
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>单行数据</returns>
        DataRow ExecuteDataRow(ISqlCommand command, DbConnection connection);

        /// <summary>
        /// 获得指定Sql语句查询下的单行数据
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>单行数据</returns>
        DataRow ExecuteDataRow(ISqlCommand command);
        #endregion
    }
}