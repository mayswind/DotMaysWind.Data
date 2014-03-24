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
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体</returns>
        public static T ToEntity<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table) where T : class
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

            return table.GetEntity(cmd.ToDataTable());
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
        public static T ToEntity<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table, DbConnection connection) where T : class
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

            return table.GetEntity(cmd.ToDataTable(connection));
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
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }

            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            cmd.Top(1);

            return table.GetEntity(cmd.ToDataTable(transaction));
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cmd">选择语句</param>
        /// <param name="table">数据库表格</param>
        /// <exception cref="ArgumentNullException">选择语句或数据库表格不能为空</exception>
        /// <returns>数据实体列表</returns>
        public static List<T> ToEntityList<T>(this SelectCommand cmd, AbstractDatabaseTable<T> table) where T : class
        {
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }

            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            return table.GetEntities(cmd.ToDataTable());
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
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }

            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            return table.GetEntities(cmd.ToDataTable(connection));
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
            if (cmd == null)
            {
                throw new ArgumentNullException("cmd");
            }

            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            return table.GetEntities(cmd.ToDataTable(transaction));
        }
    }
}