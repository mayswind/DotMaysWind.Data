using System;
using System.Data.Common;
using System.Threading;

namespace DotMaysWind.Data
{
    /// <summary>
    /// 数据库连接包装类
    /// </summary>
    internal class DatabaseConnectionWrapper : IDisposable
    {
        #region 字段
        private Int32 _refCount;
        private DbConnection _connection;
        #endregion

        #region 属性
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        internal DbConnection Connection
        {
            get { return this._connection; }
        }
        
        /// <summary>
        /// 获取当前是否已释放资源
        /// </summary>
        internal Boolean IsDisposed
        {
            get { return this._refCount == 0; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的数据库连接包装类
        /// </summary>
        /// <param name="connection">数据库连接</param>
        internal DatabaseConnectionWrapper(DbConnection connection)
        {
            this._connection = connection;
            this._refCount = 1;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }
        
        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing">是否释放</param>
        internal void Dispose(Boolean disposing)
        {
            if (disposing && Interlocked.Decrement(ref this._refCount) == 0)
            {
                this._connection.Dispose();
                this._connection = null;
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// 增加引用次数并返回当前类
        /// </summary>
        /// <returns>数据库连接包装类</returns>
        internal DatabaseConnectionWrapper AddRef()
        {
            Interlocked.Increment(ref this._refCount);
            return this;
        }
        #endregion
    }
}