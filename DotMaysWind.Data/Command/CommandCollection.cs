using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

using DotMaysWind.Data.Command.Condition;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql语句集合
    /// </summary>
    public sealed class CommandCollection : IEnumerable<ISqlCommand>
    {
        /// <summary>
        /// 对象条目
        /// </summary>
        public sealed class ObjectItem<T>
        {
            #region 字段
            private T _obj;
            private Int32 _index;
            #endregion

            #region 属性
            /// <summary>
            /// 获取当前条目所指对象
            /// </summary>
            public T Value
            {
                get { return this._obj; }
            }

            /// <summary>
            /// 获取当前条目的索引序号
            /// </summary>
            public Int32 Index
            {
                get { return this._index; }
            }
            #endregion

            #region 构造方法
            /// <summary>
            /// 初始化新的对象条目
            /// </summary>
            /// <param name="obj">当前条目所指对象</param>
            /// <param name="index">当前条目的索引序号</param>
            internal ObjectItem(T obj, Int32 index)
            {
                this._obj = obj;
                this._index = index;
            }
            #endregion
        }

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
        #region Add / AddRange
        /// <summary>
        /// 添加指定Sql语句
        /// </summary>
        /// <param name="command">待添加的语句</param>
        /// <returns>当前集合</returns>
        public CommandCollection Add(ISqlCommand command)
        {
            this._list.Add(command);

            return this;
        }

        /// <summary>
        /// 当满足指定条件时添加指定Sql语句
        /// </summary>
        /// <param name="condition">指定条件</param>
        /// <param name="command">待添加的语句</param>
        /// <returns>当前集合</returns>
        public CommandCollection Add(Func<Boolean> condition, ISqlCommand command)
        {
            if (condition())
            {
                this._list.Add(command);
            }

            return this;
        }

        /// <summary>
        /// 添加指定Sql语句集合
        /// </summary>
        /// <param name="commands">待添加的语句集合</param>
        /// <returns>当前集合</returns>
        public CommandCollection AddRange(IEnumerable<ISqlCommand> commands)
        {
            this._list.AddRange(commands);

            return this;
        }

        /// <summary>
        /// 当满足指定条件时添加指定Sql语句集合
        /// </summary>
        /// <param name="condition">指定条件</param>
        /// <param name="commands">待添加的语句集合</param>
        /// <returns>当前集合</returns>
        public CommandCollection AddRange(Func<Boolean> condition, IEnumerable<ISqlCommand> commands)
        {
            if (condition())
            {
                this._list.AddRange(commands);
            }

            return this;
        }
        #endregion

        #region AddSome
        /// <summary>
        /// 添加指定条语句
        /// </summary>
        /// <param name="collection">待遍历的集合</param>
        /// <param name="func">待执行的语句</param>
        /// <returns>当前集合</returns>
        public CommandCollection AddSome<T>(IEnumerable<T> collection, Func<ObjectItem<T>, ISqlCommand> func)
        {
            Int32 idx = 0;

            foreach (T obj in collection)
            {
                ISqlCommand command = func(new ObjectItem<T>(obj, idx++));

                if (command != null)
                {
                    this.Add(command);
                }
            }

            return this;
        }
        #endregion

        #region CreateSqlCommand
        /// <summary>
        /// 添加新的Sql插入语句类
        /// </summary>
        /// <returns>Sql插入语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class ProductItemRepository : AbstractDatabaseTable<ProductItem>
        /// {
        ///     //other necessary code
        ///     
        ///     public Boolean AddEntities(Int32 itemType, List<Int32> productIds)
        ///     {
        ///         return this.Sequence()
        ///             .AddSome<Int32>(productIds, (it, item) =>
        ///             {
        ///                 it.Insert()
        ///                     .Set(PRODUCTID, item.Value)
        ///                     .Set(PRODUCTTYPE, itemType);
        ///             })
        ///             .Result();
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public InsertCommand Insert()
        {
            InsertCommand command = this._database.CreateInsertCommand(this.TableName);
            this._list.Add(command);

            return command;
        }

        /// <summary>
        /// 添加新的Sql插入语句类
        /// </summary>
        /// <param name="action">待执行的语句</param>
        /// <returns>当前集合</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class ProductRepository : AbstractDatabaseTable<Product>
        /// {
        ///     //other necessary code
        ///     
        ///     public Boolean AddEntity(Product product)
        ///     {
        ///         return this.Sequence()
        ///             .Delete(d => d.Where(c => c.Equal(PRODUCTID, product.ID)))
        ///             .Insert(i => i.Set(PRODUCTID, product.ID).Set(PRODUCTNAME, product.Name))
        ///             .Result();
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public CommandCollection Insert(Action<InsertCommand> action)
        {
            InsertCommand command = this._database.CreateInsertCommand(this.TableName);
            this._list.Add(command);
            action(command);

            return this;
        }

        /// <summary>
        /// 当满足指定条件时添加指定Sql插入语句类
        /// </summary>
        /// <param name="condition">指定条件</param>
        /// <param name="action">待执行的语句</param>
        /// <returns>当前集合</returns>
        public CommandCollection Insert(Func<Boolean> condition, Action<InsertCommand> action)
        {
            if (condition())
            {
                InsertCommand command = this._database.CreateInsertCommand(this.TableName);
                this._list.Add(command);

                action(command);
            }

            return this;
        }

        /// <summary>
        /// 添加新的Sql更新语句类
        /// </summary>
        /// <returns>Sql更新语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class ProductItemRepository : AbstractDatabaseTable<ProductItem>
        /// {
        ///     //other necessary code
        ///     
        ///     public Boolean DeleteEntities(Int32 oldType, Int32 newType, List<Int32> productIds)
        ///     {
        ///         return this.Sequence()
        ///             .AddSome<Int32>(productIds, (it, item) =>
        ///             {
        ///                 it.Update()
        ///                     .Set(PRODUCTTYPE, newType)
        ///                     .Where(c => c.Equal(PRODUCTID, item.Value) & c.Equal(PRODUCTTYPE, oldType));
        ///             })
        ///             .Result();
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public UpdateCommand Update()
        {
            UpdateCommand command = this._database.CreateUpdateCommand(this.TableName);
            this._list.Add(command);

            return command;
        }

        /// <summary>
        /// 添加新的Sql更新语句类
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
        /// 当满足指定条件时添加指定Sql更新语句类
        /// </summary>
        /// <param name="condition">指定条件</param>
        /// <param name="action">待执行的语句</param>
        /// <returns>当前集合</returns>
        public CommandCollection Update(Func<Boolean> condition, Action<UpdateCommand> action)
        {
            if (condition())
            {
                UpdateCommand command = this._database.CreateUpdateCommand(this.TableName);
                this._list.Add(command);

                action(command);
            }

            return this;
        }

        /// <summary>
        /// 添加新的Sql删除语句类
        /// </summary>
        /// <returns>Sql删除语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class ProductItemRepository : AbstractDatabaseTable<ProductItem>
        /// {
        ///     //other necessary code
        ///     
        ///     public Boolean DeleteEntities(List<Int32> productIds)
        ///     {
        ///         return this.Sequence()
        ///             .AddSome<Int32>(productIds, (it, item) =>
        ///             {
        ///                 it.Delete()
        ///                     .Where(c => c.Equal(PRODUCTID, item.Value));
        ///             })
        ///             .Result();
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public DeleteCommand Delete()
        {
            DeleteCommand command = this._database.CreateDeleteCommand(this.TableName);
            this._list.Add(command);

            return command;
        }

        /// <summary>
        /// 添加新的Sql删除语句类
        /// </summary>
        /// <param name="action">待执行的语句</param>
        /// <returns>当前集合</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class ProductRepository : AbstractDatabaseTable<Product>
        /// {
        ///     //other necessary code
        ///     
        ///     public Boolean AddEntity(Product product)
        ///     {
        ///         return this.Sequence()
        ///             .Delete(d => d.Where(c => c.Equal(PRODUCTID, product.ID)))
        ///             .Insert(i => i.Set(PRODUCTID, product.ID).Set(PRODUCTNAME, product.Name))
        ///             .Result();
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public CommandCollection Delete(Action<DeleteCommand> action)
        {
            DeleteCommand command = this._database.CreateDeleteCommand(this.TableName);
            this._list.Add(command);
            action(command);

            return this;
        }

        /// <summary>
        /// 当满足指定条件时添加指定Sql删除语句类
        /// </summary>
        /// <param name="condition">指定条件</param>
        /// <param name="action">待执行的语句</param>
        /// <returns>当前集合</returns>
        public CommandCollection Delete(Func<Boolean> condition, Action<DeleteCommand> action)
        {
            if (condition())
            {
                DeleteCommand command = this._database.CreateDeleteCommand(this.TableName);
                this._list.Add(command);

                action(command);
            }

            return this;
        }

        /// <summary>
        /// 当添加指定Sql删除语句类
        /// </summary>
        /// <param name="where">查询语句</param>
        /// <returns>当前集合</returns>
        public CommandCollection Delete(Func<SqlConditionBuilder, ISqlCondition> where)
        {
            DeleteCommand command = this._database.CreateDeleteCommand(this.TableName);
            command.Where(where(command.ConditionBuilder));

            this._list.Add(command);

            return this;
        }

        /// <summary>
        /// 添加新的Sql选择语句类
        /// </summary>
        /// <returns>Sql选择语句</returns>
        public SelectCommand Select()
        {
            SelectCommand command = this._database.CreateSelectCommand(this.TableName);
            this._list.Add(command);

            return command;
        }

        /// <summary>
        /// 添加新的Sql选择语句类
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
        /// 当满足指定条件时添加指定Sql选择语句类
        /// </summary>
        /// <param name="condition">指定条件</param>
        /// <param name="action">待执行的语句</param>
        /// <returns>当前集合</returns>
        public CommandCollection Select(Func<Boolean> condition, Action<SelectCommand> action)
        {
            if (condition())
            {
                SelectCommand command = this._database.CreateSelectCommand(this.TableName);
                this._list.Add(command);

                action(command);
            }

            return this;
        }

        /// <summary>
        /// 添加新的Sql选择语句类
        /// </summary>
        /// <param name="tableAliasesName">数据表别名</param>
        /// <returns>Sql选择语句</returns>
        public SelectCommand Select(String tableAliasesName)
        {
            SelectCommand command = this._database.CreateSelectCommand(this.TableName, tableAliasesName);
            this._list.Add(command);

            return command;
        }

        /// <summary>
        /// 添加新的Sql选择语句类
        /// </summary>
        /// <param name="tableAliasesName">数据表别名</param>
        /// <param name="action">待执行的语句</param>
        /// <returns>当前集合</returns>
        public CommandCollection Select(String tableAliasesName, Action<SelectCommand> action)
        {
            SelectCommand command = this._database.CreateSelectCommand(this.TableName, tableAliasesName);
            this._list.Add(command);
            action(command);

            return this;
        }

        /// <summary>
        /// 当满足指定条件时添加指定Sql选择语句类
        /// </summary>
        /// <param name="tableAliasesName">数据表别名</param>
        /// <param name="condition">指定条件</param>
        /// <param name="action">待执行的语句</param>
        /// <returns>当前集合</returns>
        public CommandCollection Select(String tableAliasesName, Func<Boolean> condition, Action<SelectCommand> action)
        {
            if (condition())
            {
                SelectCommand command = this._database.CreateSelectCommand(this.TableName, tableAliasesName);
                this._list.Add(command);

                action(command);
            }

            return this;
        }
        #endregion

        #region Then
        /// <summary>
        /// 执行自定义代码而不中断当前语句链
        /// </summary>
        /// <param name="action">待执行的方法</param>
        /// <returns>当前语句</returns>
        public CommandCollection Then(Action<CommandCollection> action)
        {
            action(this);

            return this;
        }

        /// <summary>
        /// 执行自定义代码而不中断当前语句链
        /// </summary>
        /// <param name="func">待执行的方法</param>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <returns>自定义返回结果</returns>
        public T Then<T>(Func<CommandCollection, T> func)
        {
            return func(this);
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

        /// <summary>
        /// 获取操作后影响的行数
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <returns>影响的行数</returns>
        public Int32 Result(DbTransaction transaction)
        {
            return this._database.ExecuteNonQuery(this, transaction);
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