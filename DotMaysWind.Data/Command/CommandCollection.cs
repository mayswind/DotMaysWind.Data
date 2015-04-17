using System;
using System.Collections;
using System.Collections.Generic;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql语句集合
    /// </summary>
    public sealed class CommandCollection : IEnumerable<ISqlCommand>
    {
        #region 字段
        /// <summary>
        /// 基础集合
        /// </summary>
        private List<ISqlCommand> _list;

        /// <summary>
        /// 父数据库
        /// </summary>
        private AbstractDatabase _database;

        /// <summary>
        /// 表格名称
        /// </summary>
        private String _tableName;
        #endregion

        #region 属性
        /// <summary>
        /// 获取当前数据库
        /// </summary>
        public AbstractDatabase Database
        {
            get { return this._database; }
        }

        /// <summary>
        /// 获取数据表名称
        /// </summary>
        public String TableName
        {
            get { return this._tableName; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化Sql语句集合
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="tableName">数据表名称</param>
        internal CommandCollection(AbstractDatabase database, String tableName)
        {
            this._list = new List<ISqlCommand>();
            this._database = database;
            this._tableName = tableName;
        }
        #endregion

        #region 方法
        #region CreateSqlCommand
        /// <summary>
        /// 创建新的Sql插入语句类
        /// </summary>
        /// <param name="action">待执行的语句</param>
        /// <returns>当前集合</returns>
        public CommandCollection Insert(Action<InsertCommand> action)
        {
            InsertCommand command = this._database.CreateInsertCommand(this.TableName);
            this._list.Add(command);
            action(command);

            return this;
        }

        /// <summary>
        /// 创建新的Sql更新语句类
        /// </summary>
        /// <param name="action">待执行的语句</param>
        /// <returns>当前集合</returns>
        public CommandCollection Update(Action<UpdateCommand> action)
        {
            UpdateCommand command = this._database.CreateUpdateCommand(this.TableName);
            this._list.Add(command);
            action(command);

            return this;
        }

        /// <summary>
        /// 创建新的Sql删除语句类
        /// </summary>
        /// <param name="action">待执行的语句</param>
        /// <returns>当前集合</returns>
        public CommandCollection Delete(Action<DeleteCommand> action)
        {
            DeleteCommand command = this._database.CreateDeleteCommand(this.TableName);
            this._list.Add(command);
            action(command);

            return this;
        }

        /// <summary>
        /// 创建新的Sql选择语句类
        /// </summary>
        /// <param name="action">待执行的语句</param>
        /// <returns>当前集合</returns>
        public CommandCollection Select(Action<SelectCommand> action)
        {
            SelectCommand command = this._database.CreateSelectCommand(this.TableName);
            this._list.Add(command);
            action(command);

            return this;
        }

        /// <summary>
        /// 创建新的Sql选择语句类
        /// </summary>
        /// <param name="action">待执行的语句</param>
        /// <param name="tableAliasesName">数据表别名</param>
        /// <returns>当前集合</returns>
        public CommandCollection Select(String tableAliasesName, Action<SelectCommand> action)
        {
            SelectCommand command = this._database.CreateSelectCommand(this.TableName, tableAliasesName);
            this._list.Add(command);
            action(command);

            return this;
        }
        #endregion

        #region AddSome
        /// <summary>
        /// 根据指定集合执行指定次数
        /// </summary>
        /// <param name="collection">待遍历的集合</param>
        /// <param name="action">待执行的语句</param>
        /// <returns>当前集合</returns>
        public CommandCollection AddSome<T>(IEnumerable<T> collection, Action<CommandCollection, T> action)
        {
            foreach (T obj in collection)
            {
                action(this, obj);
            }

            return this;
        }
        #endregion

        #region Result
        /// <summary>
        /// 获取操作后影响的行数
        /// </summary>
        /// <returns>影响的行数</returns>
        public Int32 Result()
        {
            return this._database.ExecuteNonQuery(this);
        }
        #endregion

        #region 重载接口实现
        /// <summary>
        /// 返回接口的迭代器
        /// </summary>
        /// <returns>迭代器</returns>
        IEnumerator<ISqlCommand> IEnumerable<ISqlCommand>.GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        /// <summary>
        /// 返回接口的迭代器
        /// </summary>
        /// <returns>迭代器</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._list.GetEnumerator();
        }
        #endregion
        #endregion
    }
}