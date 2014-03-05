using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

using DotMaysWind.Data.Helper;

namespace DotMaysWind.Data.Orm
{
    /// <summary>
    /// 数据库表类
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class DatabaseTable<T> : AbstractDatabaseTable<T> where T : class, new()
    {
        #region 字段
        private Dictionary<String, DatabaseColumnAtrribute> _columns;
        private Type _entityType;
        private String _tableName;
        #endregion

        #region 属性
        /// <summary>
        /// 获取数据表名
        /// </summary>
        protected override String TableName
        {
            get { return this._tableName; }
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
            this._tableName = this.GetTableName();
            this._columns = this.GetTableColumns();
        }
        #endregion

        #region 方法
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

                if (this._columns.TryGetValue(prop.Name, out attr) && attr != null)
                {
                    DbType dbType = (attr.DbType.HasValue ? attr.DbType.Value : DbTypeHelper.InternalGetDbType(prop.PropertyType));
                    Object value = this.LoadValue(row, columns, attr.ColumnName, dbType);

                    prop.SetValue(entity, value, null);
                }
            }

            return entity;
        }
        #endregion

        #region 私有方法
        private String GetTableName()
        {
            Object[] objs = this._entityType.GetCustomAttributes(typeof(DatabaseTableAtrribute), true);

            foreach (Object obj in objs)
            {
                DatabaseTableAtrribute attr = obj as DatabaseTableAtrribute;

                if (attr != null)
                {
                    return attr.TableName;
                }
            }

            return String.Empty;
        }

        private Dictionary<String, DatabaseColumnAtrribute> GetTableColumns()
        {
            Dictionary<String, DatabaseColumnAtrribute> dict = new Dictionary<String, DatabaseColumnAtrribute>();
            PropertyInfo[] props = this._entityType.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                Object[] objs = prop.GetCustomAttributes(typeof(DatabaseColumnAtrribute), true);

                foreach (Object obj in objs)
                {
                    DatabaseColumnAtrribute attr = obj as DatabaseColumnAtrribute;

                    if (attr != null)
                    {
                        dict[prop.Name] = attr;
                        break;
                    }
                }
            }

            return dict;
        }
        #endregion
    }
}