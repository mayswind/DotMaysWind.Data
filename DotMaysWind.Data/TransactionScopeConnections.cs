using System;
using System.Collections.Generic;
using System.Threading;
using System.Transactions;

using SystemTransaction = System.Transactions.Transaction;

namespace DotMaysWind.Data
{
	/// <summary>
	/// 事务连接管理类
	/// </summary>
	internal static class TransactionScopeConnections
    {
        #region 字段
        private static readonly Dictionary<SystemTransaction, Dictionary<String, DatabaseConnectionWrapper>> TransactionConnections = null;
        #endregion

        #region 构造方法
        static TransactionScopeConnections()
        {
            TransactionConnections = new Dictionary<SystemTransaction, Dictionary<String, DatabaseConnectionWrapper>>();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="db">数据库</param>
        /// <returns>数据库连接</returns>
        public static DatabaseConnectionWrapper GetConnection(Database db)
		{
            SystemTransaction currentTransaction = SystemTransaction.Current;

			if (currentTransaction == null)
			{
				return null;
			}

            Dictionary<SystemTransaction, Dictionary<String, DatabaseConnectionWrapper>> obj;

            Monitor.Enter(obj = TransactionConnections);
			Dictionary<String, DatabaseConnectionWrapper> connectionList;

			try
			{
                if (!TransactionConnections.TryGetValue(currentTransaction, out connectionList))
				{
					connectionList = new Dictionary<String, DatabaseConnectionWrapper>();
                    TransactionConnections.Add(currentTransaction, connectionList);

					currentTransaction.TransactionCompleted += new TransactionCompletedEventHandler(TransactionScopeConnections.OnTransactionCompleted);
				}
			}
			finally
			{
                Monitor.Exit(obj);
			}

			Dictionary<String, DatabaseConnectionWrapper> obj2;

			Monitor.Enter(obj2 = connectionList);
			DatabaseConnectionWrapper connection;

			try
			{
                if (!connectionList.TryGetValue(db.ConnectionString, out connection))
				{
                    connection = db.GetNewConnection();
                    connectionList.Add(db.ConnectionString, connection);
				}

                connection.AddRef();
			}
			finally
			{
				Monitor.Exit(obj2);
			}

            return connection;
        }
        #endregion

        #region 私有方法
        private static void OnTransactionCompleted(Object sender, TransactionEventArgs e)
		{
			Dictionary<SystemTransaction, Dictionary<String, DatabaseConnectionWrapper>> obj;

            Monitor.Enter(obj = TransactionConnections);
			Dictionary<String, DatabaseConnectionWrapper> connectionList;

			try
			{
                if (!TransactionConnections.TryGetValue(e.Transaction, out connectionList))
				{
					return;
				}

                TransactionConnections.Remove(e.Transaction);
			}
			finally
			{
				Monitor.Exit(obj);
			}

			Dictionary<String, DatabaseConnectionWrapper> obj2;

			Monitor.Enter(obj2 = connectionList);

			try
			{
				foreach (DatabaseConnectionWrapper connectionWrapper in connectionList.Values)
				{
					connectionWrapper.Dispose();
				}
			}
			finally
			{
				Monitor.Exit(obj2);
			}
        }
        #endregion
    }
}