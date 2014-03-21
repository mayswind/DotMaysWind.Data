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
        /// 创建新的Sql选择语句
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns>Sql选择语句</returns>
        SelectCommand CreateSelectCommand(String tableName, Int32 pageSize);

        /// <summary>
        /// 创建新的Sql选择语句
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="tableAliasesName">数据表别名</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns>Sql选择语句</returns>
        SelectCommand CreateSelectCommand(String tableName, String tableAliasesName, Int32 pageSize);

        /// <summary>
        /// 创建新的Sql选择语句
        /// </summary>
        /// <param name="from">选择的从Sql语句</param>
        /// <param name="fromAliasesName">从Sql语句的别名</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns>Sql选择语句</returns>
        SelectCommand CreateSelectCommand(SelectCommand from, String fromAliasesName, Int32 pageSize);

        /// <summary>
        /// 创建新的Sql选择语句
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns>Sql选择语句</returns>
        SelectCommand CreateSelectCommand(String tableName, Int32 pageSize, Int32 pageIndex, Int32 recordCount);

        /// <summary>
        /// 创建新的Sql选择语句
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="tableAliasesName">数据表别名</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns>Sql选择语句</returns>
        SelectCommand CreateSelectCommand(String tableName, String tableAliasesName, Int32 pageSize, Int32 pageIndex, Int32 recordCount);

        /// <summary>
        /// 创建新的Sql选择语句
        /// </summary>
        /// <param name="isFromSql">是否从Sql语句中选择</param>
        /// <param name="from">数据表或Sql语句</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns>Sql选择语句</returns>
        SelectCommand CreateSelectCommand(Boolean isFromSql, String from, Int32 pageSize, Int32 pageIndex, Int32 recordCount);

        /// <summary>
        /// 创建新的Sql自定义语句
        /// </summary>
        /// <param name="commandType">语句类型</param>
        /// <param name="commandString">语句内容</param>
        /// <returns>Sql自定义语句</returns>
        CustomCommand CreateCustomCommand(SqlCommandType commandType, String commandString);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        T ExecuteScalar<T>(ISqlCommand command, DbType dbType, DbTransaction transaction);

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="command">指定Sql语句</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        T ExecuteScalar<T>(ISqlCommand command, DbType dbType, DbConnection connection);

        /// <summary>
        /// 返回执行指定Sql语句后返回的结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="command">指定Sql语句</param>
        /// <param name="dbType">数据类型</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>返回的结果</returns>
        T ExecuteScalar<T>(ISqlCommand command, DbType dbType);

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
        Int32 ExecuteNonQuery(List<ISqlCommand> commands);
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

        /// <summary>
        /// 获得指定Sql语句查询下的数据读取器
        /// </summary>
        /// <param name="command">指定Sql语句</param>
        /// <exception cref="ArgumentNullException">Sql语句不能为空</exception>
        /// <returns>数据读取器</returns>
        IDataReader ExecuteReader(ISqlCommand command);
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