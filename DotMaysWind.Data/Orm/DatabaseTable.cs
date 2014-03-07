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
    public class DatabaseTable<T> : AbstractDatabaseTable<T>, IDatabaseTableWithMapping where T : class, new()
    {
        #region 字段
        private Dictionary<String, DatabaseColumnAtrribute> _mapping;
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
        DatabaseColumnAtrribute IDatabaseTableWithMapping.this[String propertyName]
        {
            get
            {
                if (this._mapping == null)
                {
                    return null;
                }

                DatabaseColumnAtrribute attr = null;

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
        public DatabaseTable(Database baseDatabase)
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
        protected override InsertCommand Insert()
        {
            InsertCommand cmd = base.Insert();
            cmd.SourceDatabaseTable = this;

            return cmd;
        }

        /// <summary>
        /// 创建新的Sql更新语句类
        /// </summary>
        protected override UpdateCommand Update()
        {
            UpdateCommand cmd = base.Update();
            cmd.SourceDatabaseTable = this;

            return cmd;
        }

        /// <summary>
        /// 创建新的Sql删除语句类
        /// </summary>
        protected override DeleteCommand Delete()
        {
            DeleteCommand cmd = base.Delete();
            cmd.SourceDatabaseTable = this;

            return cmd;
        }

        /// <summary>
        /// 创建新的Sql选择语句类
        /// </summary>
        protected override SelectCommand Select()
        {
            SelectCommand cmd = base.Select();
            cmd.SourceDatabaseTable = this;

            return cmd;
        }

        /// <summary>
        /// 创建新的Sql选择语句类
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        protected override SelectCommand Select(Int32 pageSize)
        {
            SelectCommand cmd = base.Select(pageSize);
            cmd.SourceDatabaseTable = this;

            return cmd;
        }

        /// <summary>
        /// 创建新的Sql选择语句类
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="recordCount">记录总数</param>
        protected override SelectCommand Select(Int32 pageSize, Int32 pageIndex, Int32 recordCount)
        {
            SelectCommand cmd = base.Select(pageSize, pageIndex, recordCount);
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
        /// <returns>数据表实体</returns>
        protected override T CreateEntity(DataRow row, DataColumnCollection columns)
        {
            T entity = new T();
            PropertyInfo[] props = this._entityType.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                DatabaseColumnAtrribute attr = null;

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