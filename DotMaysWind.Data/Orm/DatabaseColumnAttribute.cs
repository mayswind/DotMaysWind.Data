using System;

namespace DotMaysWind.Data.Orm
{
    /// <summary>
    /// 数据库列特性
    /// </summary>
    /// <example>
    /// <code lang="C#">
    /// <![CDATA[
    /// [DatabaseTable("tbl_Users")]
    /// public class User
    /// {
    ///     [DatabaseColumn("UserID")]
    ///     public Int32 UserID { get; set; }
    ///     
    ///     [DatabaseColumn("UserName")]
    ///     public String UserName { get; set; }
    ///     
    ///     [DatabaseColumn("UserType", DataType.Byte)]
    ///     public Int32 UserType { get; set; }
    ///     
    ///     [DatabaseColumn("CreateTime")]
    ///     public DateTime? CreateTime { get; set; }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class DatabaseColumnAttribute : Attribute
    {
        #region 字段
        private String _columnName;
        private DataType? _dataType;
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
        /// 获取或设置数据列数据类型
        /// </summary>
        public DataType? DataType
        {
            get { return this._dataType; }
            set { this._dataType = value; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化数据库列特性
        /// </summary>
        /// <param name="columnName">数据库列名</param>
        public DatabaseColumnAttribute(String columnName)
        {
            this._columnName = columnName;
        }

        /// <summary>
        /// 初始化数据库列特性
        /// </summary>
        /// <param name="columnName">数据库列名</param>
        /// <param name="dataType">数据类型</param>
        public DatabaseColumnAttribute(String columnName, DataType dataType)
        {
            this._columnName = columnName;
            this._dataType = dataType;
        }
        #endregion
    }
}