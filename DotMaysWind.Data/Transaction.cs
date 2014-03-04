using System;
using System.Data;
using System.Data.Common;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Helper;

namespace DotMaysWind.Data
{
    /// <summary>
    /// 事务封装类
    /// </summary>
    public class Transaction : IDisposable
    {
        #region 字段
        private Database _database = null;
        private DbConnection _connection = null;
        private DbTransaction _transcation = null;
        #endregion

        #region 属性
        /// <summary>
        /// 获取当前事务的数据库
        /// </summary>
        public Database Database
        {
            get { return this._database; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的事务
        /// </summary>
        /// <param name="database">数据库类</param>
        /// <param name="autoOpen">是否自动打开连接</param>
        /// <exception cref="ArgumentNullException">数据库不能为空</exception>
        internal Transaction(Database database, Boolean autoOpen)
        {
            if (database == null)
            {
                throw new ArgumentNullException("database");
            }

            this._database = database;

            if (autoOpen)
            {
                this.Open();
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 开始事务
        /// </summary>
        public void Open()
        {
            this._connection = this._database.DatabaseProvider.CreateConnection();
            this._connection.ConnectionString = this._database.ConnectionString;
            this._connection.Open();

            this._transcation = this._connection.BeginTransaction();
        }

        /// <summary>
        /// 关闭事务
        /// </summary>
        public void Close()
        {
            if (this._connection != null)
            {
                this._connection.Close();
            }
        }

        /// <summary>
        /// 释放事务所占连接和空间
        /// </summary>
        public void Dispose()
        {
            if (this._transcation != null)
            {
                this._transcation.Dispose();
                this._transcation = null;
            }

            if (this._connection != null)
            {
                this._connection.Dispose();
                this._connection = null;
            }
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <exception cref="NullReferenceException">当前事务没有开启</exception>
        public Boolean Commit()
        {
            if (this._transcation == null)
            {
                throw new NullReferenceException();
            }

            Boolean flag = false;

            try
            {
                this._transcation.Commit();
                flag = true;
            }
            catch
            {
                flag = false;
            }

            return flag;
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            if (this._transcation != null)
            {
                this._transcation.Rollback();
            }
        }
        #endregion

        #region 执行方法
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
        /// <exception cref="NullReferenceException">当前事务没有开启</exception>
        /// <returns>返回的结果</returns>
        public Object ExecuteScalar(DbCommand dbCommand)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            if (this._transcation == null)
            {
                throw new NullReferenceException();
            }

            dbCommand.Connection = this._connection;
            dbCommand.Transaction = this._transcation;

            return dbCommand.ExecuteScalar();
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

            return this.ExecuteNonQuery(command.ToDbCommand());
        }

        /// <summary>
        /// 返回执行指定Sql语句后影响的行数
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <exception cref="ArgumentNullException">数据库命令不能为空</exception>
        /// <exception cref="NullReferenceException">当前事务没有开启</exception>
        /// <returns>受影响的行数</returns>
        public Int32 ExecuteNonQuery(DbCommand dbCommand)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            if (this._transcation == null)
            {
                throw new NullReferenceException();
            }

            dbCommand.Connection = this._connection;
            dbCommand.Transaction = this._transcation;

            return dbCommand.ExecuteNonQuery();
        }

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
        /// <exception cref="NullReferenceException">当前事务没有开启</exception>
        /// <returns>数据读取器</returns>
        public IDataReader ExecuteReader(DbCommand dbCommand)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            if (this._transcation == null)
            {
                throw new NullReferenceException();
            }

            dbCommand.Connection = this._connection;
            dbCommand.Transaction = this._transcation;

            return dbCommand.ExecuteReader();
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

            return this.ExecuteDataSet(command.ToDbCommand());
        }

        /// <summary>
        /// 获得指定Sql语句查询下的数据集
        /// </summary>
        /// <param name="dbCommand">数据库命令</param>
        /// <exception cref="ArgumentNullException">数据库命令不能为空</exception>
        /// <exception cref="NullReferenceException">当前事务没有开启</exception>
        /// <returns>数据集</returns>
        public DataSet ExecuteDataSet(DbCommand dbCommand)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            if (this._transcation == null)
            {
                throw new NullReferenceException();
            }

            dbCommand.Connection = this._connection;
            dbCommand.Transaction = this._transcation;

            DataSet dataSet = DataSetHelper.InternalCreateDataSet(this._database.DatabaseProvider, dbCommand);

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
    }
}