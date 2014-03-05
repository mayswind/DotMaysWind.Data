using System;
using System.Data;

namespace DotMaysWind.Data.Orm
{
    /// <summary>
    /// 数据库列特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class DatabaseColumnAtrribute : Attribute
    {
        #region 字段
        private String _columnName;
        private DbType? _dbType;
        #endregion

        #region 属性
        /// <summary>
        /// 获取数据列名
        /// </summary>
        public String ColumnName
        {
            get { return this._columnName; }
        }

        /// <summary>
        /// 获取数据列数据类型
        /// </summary>
        public DbType? DbType
        {
            get { return this._dbType; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化数据库列特性
        /// </summary>
        /// <param name="columnName">数据库列名</param>
        public DatabaseColumnAtrribute(String columnName)
        {
            this._columnName = columnName;
        }

        /// <summary>
        /// 初始化数据库列特性
        /// </summary>
        /// <param name="columnName">数据库列名</param>
        /// <param name="dbType">数据类型</param>
        public DatabaseColumnAtrribute(String columnName, DbType dbType)
        {
            this._columnName = columnName;
            this._dbType = dbType;
        }
        #endregion
    }
}