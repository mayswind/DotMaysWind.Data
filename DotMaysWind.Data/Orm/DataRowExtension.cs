using System;
using System.Data;

namespace DotMaysWind.Data.Orm
{
    /// <summary>
    /// 数据行扩展方法类
    /// </summary>
    public static class DataRowExtension
    {
        #region ToEntity
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="row">数据行</param>
        /// <param name="dbtable">数据库表格</param>
        /// <param name="args">创建实体时的额外参数</param>
        /// <exception cref="ArgumentNullException">数据行或数据库表格不能为空</exception>
        /// <returns>数据实体</returns>
        public static T ToEntityWithArgs<T>(this DataRow row, AbstractDatabaseTable<T> dbtable, Object args) where T : class
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }

            if (dbtable == null)
            {
                throw new ArgumentNullException("dbtable");
            }

            return dbtable.InternalGetEntity(null, row, args);
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="row">数据行</param>
        /// <param name="dbtable">数据库表格</param>
        /// <exception cref="ArgumentNullException">数据行或数据库表格不能为空</exception>
        /// <returns>数据实体</returns>
        public static T ToEntity<T>(this DataRow row, AbstractDatabaseTable<T> dbtable) where T : class
        {
            return DataRowExtension.ToEntityWithArgs<T>(row, dbtable, null);
        }
        #endregion

        #region Then
        /// <summary>
        /// 对指定数据行进行操作
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="action">操作方法</param>
        /// <exception cref="ArgumentNullException">数据行或操作方法不能为空</exception>
        public static void Then(this DataRow row, Action<EntityCreatingArgs> action)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            action(new EntityCreatingArgs(row, row.Table.Columns, null));
        }

        /// <summary>
        /// 对指定数据行进行操作
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="row">数据行</param>
        /// <param name="func">操作方法</param>
        /// <exception cref="ArgumentNullException">数据行或操作方法不能为空</exception>
        public static T Then<T>(this DataRow row, Func<EntityCreatingArgs, T> func)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }

            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return func(new EntityCreatingArgs(row, row.Table.Columns, null));
        }
        #endregion
    }
}
