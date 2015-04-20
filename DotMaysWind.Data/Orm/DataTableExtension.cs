using System;
using System.Collections.Generic;
using System.Data;

namespace DotMaysWind.Data.Orm
{
    /// <summary>
    /// 数据表扩展方法类
    /// </summary>
    public static class DataTableExtension
    {
        #region ToEntityList
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table">数据表</param>
        /// <param name="dbtable">数据库表格</param>
        /// <param name="args">创建实体时的额外参数</param>
        /// <exception cref="ArgumentNullException">数据表或数据库表格不能为空</exception>
        /// <returns>数据实体列表</returns>
        public static List<T> ToEntityListWithArgs<T>(this DataTable table, AbstractDatabaseTable<T> dbtable, Object args) where T : class
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            if (dbtable == null)
            {
                throw new ArgumentNullException("dbtable");
            }

            return dbtable.InternalGetEntityList(null, table, args);
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table">数据表</param>
        /// <param name="dbtable">数据库表格</param>
        /// <exception cref="ArgumentNullException">数据表或数据库表格不能为空</exception>
        /// <returns>数据实体列表</returns>
        public static List<T> ToEntityList<T>(this DataTable table, AbstractDatabaseTable<T> dbtable) where T : class
        {
            return DataTableExtension.ToEntityListWithArgs<T>(table, dbtable, null);
        }
        #endregion

        #region ToEntityDictionary
        /// <summary>
        /// 获取实体字典
        /// </summary>
        /// <typeparam name="TKey">字典键类型</typeparam>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table">数据表</param>
        /// <param name="dbtable">数据库表格</param>
        /// <param name="keyColumnName">键列名称</param>
        /// <param name="args">创建实体时的额外参数</param>
        /// <exception cref="ArgumentNullException">数据表或数据库表格不能为空</exception>
        /// <returns>数据实体字典</returns>
        public static Dictionary<TKey, T> ToEntityDictionaryWithArgs<TKey, T>(this DataTable table, AbstractDatabaseTable<T> dbtable, String keyColumnName, Object args) where T : class
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            if (dbtable == null)
            {
                throw new ArgumentNullException("dbtable");
            }

            return dbtable.InternalGetEntityDictionary<TKey>(null, table, keyColumnName, args);
        }

        /// <summary>
        /// 获取实体字典
        /// </summary>
        /// <typeparam name="TKey">字典键类型</typeparam>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table">数据表</param>
        /// <param name="dbtable">数据库表格</param>
        /// <param name="keyColumnName">键列名称</param>
        /// <exception cref="ArgumentNullException">数据表或数据库表格不能为空</exception>
        /// <returns>数据实体字典</returns>
        public static Dictionary<TKey, T> ToEntityDictionary<TKey, T>(this DataTable table, AbstractDatabaseTable<T> dbtable, String keyColumnName) where T : class
        {
            return DataTableExtension.ToEntityDictionaryWithArgs<TKey, T>(table, dbtable, keyColumnName, null);
        }
        #endregion

        #region Each
        /// <summary>
        /// 对数据表数据遍历操作
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="action">操作方法</param>
        /// <exception cref="ArgumentNullException">数据行或操作方法不能为空</exception>
        public static void Each(this DataTable table, Action<EntityCreatingArgs> action)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            for (Int32 i = 0; i < table.Rows.Count; i++)
            {
                action(new EntityCreatingArgs(i, table.Rows[i], table.Columns, null));
            }
        }

        /// <summary>
        /// 对数据表数据遍历操作
        /// </summary>
        /// <typeparam name="T">返回集合元素类型</typeparam>
        /// <param name="table">数据表</param>
        /// <param name="action">操作方法</param>
        /// <exception cref="ArgumentNullException">数据行或操作方法不能为空</exception>
        public static List<T> EachToList<T>(this DataTable table, Action<List<T>, EntityCreatingArgs> action)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            List<T> list = new List<T>();

            for (Int32 i = 0; i < table.Rows.Count; i++)
            {
                action(list, new EntityCreatingArgs(i, table.Rows[i], table.Columns, null));
            }

            return list;
        }
        #endregion
    }
}