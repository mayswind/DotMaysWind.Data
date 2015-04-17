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
            public T Object
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

        #region CreateSqlCommand
        /// <summary>
        /// 添加新的Sql插入语句类
        /// </summary>
        /// <returns>Sql插入语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class ProductItemDataProvider : AbstractDatabaseTable<ProductItem>
        /// {
        ///     //other necessary code
        ///     
        ///     public Boolean AddEntities(Int32 itemType, List<Int32> productIds)
        ///     {
        ///         return this.CreateCollection()
        ///             .AddSome<Int32>(productIds, (me, item) =>
        ///             {
        ///                 me.Insert()
        ///                     .Set(PRODUCTID, item.Object)
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
        /// public class ProductDataProvider : AbstractDatabaseTable<Product>
        /// {
        ///     //other necessary code
        ///     
        ///     public Boolean AddEntity(Product product)
        ///     {
        ///         return this.CreateCollection()
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
        /// public class ProductItemDataProvider : AbstractDatabaseTable<ProductItem>
        /// {
        ///     //other necessary code
        ///     
        ///     public Boolean DeleteEntities(List<Int32> productIds)
        ///     {
        ///         return this.CreateCollection()
        ///             .AddSome<Int32>(productIds, (me, item) =>
        ///             {
        ///                 me.Delete()
        ///                     .Where(c => c.Equal(PRODUCTID, item.Object));
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
        /// public class ProductDataProvider : AbstractDatabaseTable<Product>
        /// {
        ///     //other necessary code
        ///     
        ///     public Boolean AddEntity(Product product)
        ///     {
        ///         return this.CreateCollection()
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

        #region AddSomeSqlCommands
        /// <summary>
        /// 根据指定集合执行指定次数
        /// </summary>
        /// <param name="collection">待遍历的集合</param>
        /// <param name="action">待执行的语句</param>
        /// <returns>当前集合</returns>
        public CommandCollection AddSome<T>(IEnumerable<T> collection, Action<CommandCollection, ObjectItem<T>> action)
        {
            Int32 idx = 0;

            foreach (T obj in collection)
            {
                action(this, new ObjectItem<T>(obj, idx++));
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