using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Helper;
using DotMaysWind.Data.Orm.Helper;

namespace DotMaysWind.Data.Orm
{
    /// <summary>
    /// 数据库表类
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <example>
    /// <code lang="C#">
    /// <![CDATA[
    /// using System;
    /// using System.Collections.Generic;
    /// 
    /// using DotMaysWind.Data;
    /// using DotMaysWind.Data.Linq;
    /// using DotMaysWind.Data.Orm;
    /// 
    /// [DatabaseTable("tbl_Users")]
    /// public class User
    /// {
    ///     [DatabaseColumn("UserID")]
    ///     public Int32 UserID { get; set; }
    ///     
    ///     [DatabaseColumn("UserName")]
    ///     public String UserName { get; set; }
    /// }
    /// 
    /// public class UserDataProvider : DatabaseTable<User>
    /// {
    ///     public UserDataProvider()
    ///         : base(MainDatabase.Instance) { }
    /// 
    ///     public Boolean InsertEntity(User user)
    ///     {
    ///         return this.Insert()
    ///             .Add(user)
    ///             .Result() > 0;
    ///     }
    /// 
    ///     public Boolean UpdateEntity(User user)
    ///     {
    ///         return this.Update()
    ///             .Set<User>(c => user.UserName, user.UserName)
    ///             .Where<User>(c => c.UserID == user.UserID)
    ///             .Result() > 0;
    ///     }
    /// 
    ///     public Boolean DeleteEntity(Int32 userID)
    ///     {
    ///         return this.Delete()
    ///             .Where<User>(c => c.UserID == userID)
    ///             .Result() > 0;
    ///     }
    /// 
    ///     public List<User> GetAllEntities()
    ///     {
    ///         return this.Select()
    ///             .Querys<User>(c => new { c.UserID, c.UserName })
    ///             .ToEntityList<User>(this);
    ///     }
    /// }
    /// 
    /// internal static class MainDatabase
    /// {
    ///     private static IDatabase _database;
    /// 
    ///     internal static IDatabase Instance
    ///     {
    ///         get { return _database; }
    ///     }
    /// 
    ///     static MainDatabase()
    ///     {
    ///         _database = DatabaseFactory.CreateDatabase("MainDatabase");
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public class DatabaseTable<T> : AbstractDatabaseTable<T>, IDatabaseTableWithMapping where T : class, new()
    {
        #region 字段
        private Dictionary<String, DatabaseColumnAttribute> _mapping;
        private Type _entityType;
        private String _tableName;
        #endregion

        #region 属性
        /// <summary>
        /// 获取数据表名
        /// </summary>
        public override String TableName
        {
            get { return this._tableName; }
        }

        /// <summary>
        /// 获取实体类型
        /// </summary>
        Type IDatabaseTableWithMapping.EntityType
        {
            get { return this._entityType; }
        }
        #endregion

        #region 索引器
        /// <summary>
        /// 获取实体类属性名称对应的字段特性
        /// </summary>
        /// <param name="propertyName">实体类属性名称</param>
        /// <returns>字段特性</returns>
        DatabaseColumnAttribute IDatabaseTableWithMapping.this[String propertyName]
        {
            get
            {
                if (this._mapping == null)
                {
                    return null;
                }

                DatabaseColumnAttribute attr = null;

                if (!this._mapping.TryGetValue(propertyName, out attr))
                {
                    return null;
                }

                return attr;
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的数据库表
        /// </summary>
        /// <param name="baseDatabase">数据表所在数据库</param>
        /// <exception cref="ArgumentNullException">数据库不能为空</exception>
        public DatabaseTable(IDatabase baseDatabase)
            : base(baseDatabase)
        {
            this._entityType = typeof(T);
            this._tableName = EntityHelper.InternalGetTableName(this._entityType);
            this._mapping = EntityHelper.InternalGetTableColumns(this._entityType);
        }
        #endregion

        #region CreateCommand
        /// <summary>
        /// 创建新的Sql插入语句类
        /// </summary>
        /// <returns>Sql插入语句</returns>
        protected override InsertCommand Insert()
        {
            InsertCommand cmd = base.Insert();
            cmd.SourceDatabaseTable = this;

            return cmd;
        }

        /// <summary>
        /// 创建新的Sql更新语句类
        /// </summary>
        /// <returns>Sql更新语句</returns>
        protected override UpdateCommand Update()
        {
            UpdateCommand cmd = base.Update();
            cmd.SourceDatabaseTable = this;

            return cmd;
        }

        /// <summary>
        /// 创建新的Sql删除语句类
        /// </summary>
        /// <returns>Sql删除语句</returns>
        protected override DeleteCommand Delete()
        {
            DeleteCommand cmd = base.Delete();
            cmd.SourceDatabaseTable = this;

            return cmd;
        }

        /// <summary>
        /// 创建新的Sql选择语句类
        /// </summary>
        /// <returns>Sql选择语句</returns>
        protected override SelectCommand Select()
        {
            SelectCommand cmd = base.Select();
            cmd.SourceDatabaseTable = this;

            return cmd;
        }

        /// <summary>
        /// 创建新的Sql选择语句类
        /// </summary>
        /// <param name="tableAliasesName">数据表别名</param>
        /// <returns>Sql选择语句</returns>
        protected override SelectCommand Select(String tableAliasesName)
        {
            SelectCommand cmd = base.Select();
            cmd.SourceDatabaseTable = this;

            return cmd;
        }
        #endregion

        #region 保护方法
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="columns">列集合</param>
        /// <param name="args">创建实体时的额外参数</param>
        /// <returns>数据表实体</returns>
        protected override T CreateEntity(DataRow row, DataColumnCollection columns, Object args)
        {
            T entity = new T();
            PropertyInfo[] props = this._entityType.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                DatabaseColumnAttribute attr = null;

                if (this._mapping.TryGetValue(prop.Name, out attr) && attr != null)
                {
                    DbType dbType = (attr.DbType.HasValue ? attr.DbType.Value : DbTypeHelper.InternalGetDbType(prop.PropertyType));
                    Object value;

                    if (EntityHelper.InternalIsNullableType(prop.PropertyType))
                    {
                        value = this.LoadNullableValue(row, columns, attr.ColumnName, dbType);
                    }
                    else
                    {
                        value = this.LoadValue(row, columns, attr.ColumnName, dbType);
                    }

                    prop.SetValue(entity, value, null);
                }
            }

            return entity;
        }
        #endregion
    }
}