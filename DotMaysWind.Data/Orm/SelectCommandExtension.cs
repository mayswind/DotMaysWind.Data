using System;
using System.Collections.Generic;
using System.Data.Common;

using DotMaysWind.Data.Command;

namespace DotMaysWind.Data.Orm
{
    /// <summary>
    /// 选择语句扩展方法类
    /// </summary>
    public static class SelectCommandExtension
    {
        #region ToEntity
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="args">创建实体时的额外参数</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class UserDataProvider : AbstractDatabaseTable<User>
        /// {
        ///     //other necessary code
        ///     
        ///     public User GetEntity(Int32 userID)
        ///     {
        ///         return this.Select()
        ///             .Where(c => c.Equal(UserIDColumn, userID))
        ///             .ToEntityWithArgs<User>(this, "argument");
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static T ToEntityWithArgs<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, Object args) where T : class
        {
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }

            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            cmd.Top(1);

            return table.InternalGetEntity(cmd, cmd.ToDataTable(), args);
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class UserDataProvider : AbstractDatabaseTable<User>
        /// {
        ///     //other necessary code
        ///     
        ///     public User GetEntity(Int32 userID)
        ///     {
        ///         return this.Select()
        ///             .Where(c => c.Equal(UserIDColumn, userID))
        ///             .ToEntity<User>(this);
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static T ToEntity<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table) where T : class
        {
            return SelectCommandExtension.ToEntityWithArgs<T>(cmd, table, null);
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="args">创建实体时的额外参数</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class UserDataProvider : AbstractDatabaseTable<User>
        /// {
        ///     //other necessary code
        ///     
        ///     public User UpdateAndGetEntity(User user)
        ///     {
        ///         return this.Database.UsingConnection(conn =>
        ///         {
        ///             Boolean success = this.Update()
        ///                 .Set(UserNameColumn, user.UserName)
        ///                 .Where(c => c.Equal(UserIDColumn, user.UserID))
        ///                 .Result(conn) > 0;
        ///     
        ///             if (!success)
        ///             {
        ///                 throw new Exception("Failed to update!");
        ///             }
        ///             
        ///             return this.Select()
        ///                 .Where(c => c.Equal(UserIDColumn, user.UserID))
        ///                 .ToEntityWithArgs<User>(this, conn, "argument");
        ///         });
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static T ToEntityWithArgs<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, DbConnection connection, Object args) where T : class
        {
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }

            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            cmd.Top(1);

            return table.InternalGetEntity(cmd, cmd.ToDataTable(connection), args);
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class UserDataProvider : AbstractDatabaseTable<User>
        /// {
        ///     //other necessary code
        ///     
        ///     public User UpdateAndGetEntity(User user)
        ///     {
        ///         return this.Database.UsingConnection(conn =>
        ///         {
        ///             Boolean success = this.Update()
        ///                 .Set(UserNameColumn, user.UserName)
        ///                 .Where(c => c.Equal(UserIDColumn, user.UserID))
        ///                 .Result(conn) > 0;
        ///     
        ///             if (!success)
        ///             {
        ///                 throw new Exception("Failed to update!");
        ///             }
        ///             
        ///             return this.Select()
        ///                 .Where(c => c.Equal(UserIDColumn, user.UserID))
        ///                 .ToEntity<User>(this, conn);
        ///         });
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static T ToEntity<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, DbConnection connection) where T : class
        {
            return SelectCommandExtension.ToEntityWithArgs<T>(cmd, table, connection, null);
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="transaction">数据库事务</param>
        /// <param name="args">创建实体时的额外参数</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体</returns>
        public static T ToEntityWithArgs<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, DbTransaction transaction, Object args) where T : class
        {
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }

            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            cmd.Top(1);

            return table.InternalGetEntity(cmd, cmd.ToDataTable(transaction), args);
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体</returns>
        public static T ToEntity<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, DbTransaction transaction) where T : class
        {
            return SelectCommandExtension.ToEntityWithArgs<T>(cmd, table, transaction, null);
        }
        #endregion

        #region ToEntityArray
        /// <summary>
        /// 获取实体数组
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="args">创建实体时的额外参数</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体数组</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class UserDataProvider : AbstractDatabaseTable<User>
        /// {
        ///     //other necessary code
        ///     
        ///     public User[] GetAllEntities()
        ///     {
        ///         return this.Select()
        ///             .ToEntityArray<User>(this, "argument");
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static T[] ToEntityArrayWithArgs<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, Object args) where T : class
        {
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }

            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            return table.InternalGetEntityArray(cmd, cmd.ToDataTable(), args);
        }

        /// <summary>
        /// 获取实体数组
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体数组</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class UserDataProvider : AbstractDatabaseTable<User>
        /// {
        ///     //other necessary code
        ///     
        ///     public User[] GetAllEntities()
        ///     {
        ///         return this.Select()
        ///             .ToEntityArray<User>(this);
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static T[] ToEntityArray<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table) where T : class
        {
            return SelectCommandExtension.ToEntityArrayWithArgs<T>(cmd, table, null);
        }

        /// <summary>
        /// 获取实体数组
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="args">创建实体时的额外参数</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体数组</returns>
        public static T[] ToEntityArrayWithArgs<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, DbConnection connection, Object args) where T : class
        {
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }

            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            return table.InternalGetEntityArray(cmd, cmd.ToDataTable(connection), args);
        }

        /// <summary>
        /// 获取实体数组
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体数组</returns>
        public static T[] ToEntityArray<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, DbConnection connection) where T : class
        {
            return SelectCommandExtension.ToEntityArrayWithArgs<T>(cmd, table, connection, null);
        }

        /// <summary>
        /// 获取实体数组
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="transaction">数据库事务</param>
        /// <param name="args">创建实体时的额外参数</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体数组</returns>
        public static T[] ToEntityArrayWithArgs<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, DbTransaction transaction, Object args) where T : class
        {
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }

            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            return table.InternalGetEntityArray(cmd, cmd.ToDataTable(transaction), args);
        }

        /// <summary>
        /// 获取实体数组
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体数组</returns>
        public static T[] ToEntityArray<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, DbTransaction transaction) where T : class
        {
            return SelectCommandExtension.ToEntityArrayWithArgs<T>(cmd, table, transaction, null);
        }
        #endregion

        #region ToEntityList
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="args">创建实体时的额外参数</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体列表</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class UserDataProvider : AbstractDatabaseTable<User>
        /// {
        ///     //other necessary code
        ///     
        ///     public List<User> GetAllEntities()
        ///     {
        ///         return this.Select()
        ///             .ToEntityList<User>(this, "argument");
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static List<T> ToEntityListWithArgs<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, Object args) where T : class
        {
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }

            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            return table.InternalGetEntityList(cmd, cmd.ToDataTable(), args);
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体列表</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class UserDataProvider : AbstractDatabaseTable<User>
        /// {
        ///     //other necessary code
        ///     
        ///     public List<User> GetAllEntities()
        ///     {
        ///         return this.Select()
        ///             .ToEntityList<User>(this);
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static List<T> ToEntityList<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table) where T : class
        {
            return SelectCommandExtension.ToEntityListWithArgs<T>(cmd, table, null);
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="args">创建实体时的额外参数</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体列表</returns>
        public static List<T> ToEntityListWithArgs<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, DbConnection connection, Object args) where T : class
        {
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }

            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            return table.InternalGetEntityList(cmd, cmd.ToDataTable(connection), args);
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体列表</returns>
        public static List<T> ToEntityList<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, DbConnection connection) where T : class
        {
            return SelectCommandExtension.ToEntityListWithArgs<T>(cmd, table, connection, null);
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="transaction">数据库事务</param>
        /// <param name="args">创建实体时的额外参数</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体列表</returns>
        public static List<T> ToEntityListWithArgs<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, DbTransaction transaction, Object args) where T : class
        {
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }

            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            return table.InternalGetEntityList(cmd, cmd.ToDataTable(transaction), args);
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体列表</returns>
        public static List<T> ToEntityList<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, DbTransaction transaction) where T : class
        {
            return SelectCommandExtension.ToEntityListWithArgs<T>(cmd, table, transaction, null);
        }
        #endregion

        #region ToEntityDictionary
        /// <summary>
        /// 获取实体字典
        /// </summary>
        /// <typeparam name="TKey">字典键类型</typeparam>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="keyColumnName">键列名称</param>
        /// <param name="args">创建实体时的额外参数</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体字典</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class UserDataProvider : AbstractDatabaseTable<User>
        /// {
        ///     //other necessary code
        ///     
        ///     public Dictionary<String, User> GetAllEntities()
        ///     {
        ///         return this.Select()
        ///             .ToEntityDictionary<String, User>(this, "UserName", "argument");
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static Dictionary<TKey, T> ToEntityDictionaryWithArgs<TKey, T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, String keyColumnName, Object args) where T : class
        {
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }

            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            return table.InternalGetEntityDictionary<TKey>(cmd, cmd.ToDataTable(), keyColumnName, args);
        }

        /// <summary>
        /// 获取实体字典
        /// </summary>
        /// <typeparam name="TKey">字典键类型</typeparam>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="keyColumnName">键列名称</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体字典</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class UserDataProvider : AbstractDatabaseTable<User>
        /// {
        ///     //other necessary code
        ///     
        ///     public Dictionary<String, User> GetAllEntities()
        ///     {
        ///         return this.Select()
        ///             .ToEntityDictionary<String, User>(this, "UserName");
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static Dictionary<TKey, T> ToEntityDictionary<TKey, T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, String keyColumnName) where T : class
        {
            return SelectCommandExtension.ToEntityDictionaryWithArgs<TKey, T>(cmd, table, keyColumnName, null);
        }

        /// <summary>
        /// 获取实体字典
        /// </summary>
        /// <typeparam name="TKey">字典键类型</typeparam>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="keyColumnName">键列名称</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="args">创建实体时的额外参数</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体字典</returns>
        public static Dictionary<TKey, T> ToEntityDictionaryWithArgs<TKey, T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, String keyColumnName, DbConnection connection, Object args) where T : class
        {
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }

            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            return table.InternalGetEntityDictionary<TKey>(cmd, cmd.ToDataTable(connection), keyColumnName, args);
        }

        /// <summary>
        /// 获取实体字典
        /// </summary>
        /// <typeparam name="TKey">字典键类型</typeparam>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="keyColumnName">键列名称</param>
        /// <param name="connection">数据库连接</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体字典</returns>
        public static Dictionary<TKey, T> ToEntityDictionary<TKey, T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, String keyColumnName, DbConnection connection) where T : class
        {
            return SelectCommandExtension.ToEntityDictionaryWithArgs<TKey, T>(cmd, table, keyColumnName, connection, null);
        }

        /// <summary>
        /// 获取实体字典
        /// </summary>
        /// <typeparam name="TKey">字典键类型</typeparam>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="keyColumnName">键列名称</param>
        /// <param name="transaction">数据库事务</param>
        /// <param name="args">创建实体时的额外参数</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体字典</returns>
        public static Dictionary<TKey, T> ToEntityDictionaryWithArgs<TKey, T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, String keyColumnName, DbTransaction transaction, Object args) where T : class
        {
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }

            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            return table.InternalGetEntityDictionary<TKey>(cmd, cmd.ToDataTable(transaction), keyColumnName, args);
        }

        /// <summary>
        /// 获取实体字典
        /// </summary>
        /// <typeparam name="TKey">字典键类型</typeparam>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <param name="keyColumnName">键列名称</param>
        /// <param name="transaction">数据库事务</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体字典</returns>
        public static Dictionary<TKey, T> ToEntityDictionary<TKey, T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, String keyColumnName, DbTransaction transaction) where T : class
        {
            return SelectCommandExtension.ToEntityDictionaryWithArgs<TKey, T>(cmd, table, keyColumnName, transaction, null);
        }
        #endregion
    }
}