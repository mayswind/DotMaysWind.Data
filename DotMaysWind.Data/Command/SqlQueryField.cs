using System;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql查询字段类
    /// </summary>
    public class SqlQueryField
    {
        #region 字段
        private String _tableName;
        private String _fieldName;
        private String _aliasesName;

        private Boolean _useFunction;
        private String _function;
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置表格名称
        /// </summary>
        public String TableName
        {
            get { return this._tableName; }
            set { this._tableName = value; }
        }

        /// <summary>
        /// 获取或设置查询名称
        /// </summary>
        public String FieldName
        {
            get { return this._fieldName; }
            set { this._fieldName = value; }
        }

        /// <summary>
        /// 获取或设置字段的别名
        /// </summary>
        public String AliasesName
        {
            get { return this._aliasesName; }
            set { this._aliasesName = value; }
        }

        /// <summary>
        /// 获取是否使用函数
        /// </summary>
        internal Boolean UseFunction
        {
            get { return this._useFunction; }
        }

        /// <summary>
        /// 获取函数名称
        /// </summary>
        internal String Function
        {
            get { return this._function; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化Sql查询字段类
        /// </summary>
        /// <param name="tableName">表格名称</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="aliasesName">字段别名</param>
        private SqlQueryField(String tableName, String columnName, String aliasesName)
        {
            this._tableName = tableName;
            this._fieldName = columnName;
            this._aliasesName = aliasesName;
            this._useFunction = false;
            this._function = String.Empty;
        }

        /// <summary>
        /// 初始化Sql查询字段类
        /// </summary>
        /// <param name="tableName">表格名称</param>
        /// <param name="function">合计函数</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="aliasesName">字段别名</param>
        private SqlQueryField(String tableName, SqlAggregateFunction function, String columnName, String aliasesName)
        {
            this._tableName = tableName;
            this._fieldName = columnName;
            this._aliasesName = aliasesName;
            this._useFunction = true;
            this._function = function.ToString().ToUpperInvariant();
        }

        /// <summary>
        /// 初始化Sql查询字段类
        /// </summary>
        /// <param name="function">函数名称</param>
        /// <param name="aliasesName">字段别名</param>
        private SqlQueryField(String function, String aliasesName)
        {
            this._tableName = String.Empty;
            this._fieldName = String.Empty;
            this._aliasesName = aliasesName;
            this._useFunction = true;
            this._function = function;
        }
        #endregion

        #region 静态方法
        /// <summary>
        /// 创建新的Sql查询字段类
        /// </summary>
        /// <param name="tableName">表格名称</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="aliasesName">字段别名</param>
        internal static SqlQueryField InternalCreateFromColumn(String tableName, String columnName, String aliasesName)
        {
            return new SqlQueryField(tableName, columnName, aliasesName);
        }

        /// <summary>
        /// 创建新的Sql查询字段类
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="aliasesName">字段别名</param>
        internal static SqlQueryField InternalCreateFromColumn(String columnName, String aliasesName)
        {
            return new SqlQueryField(String.Empty, columnName, aliasesName);
        }

        /// <summary>
        /// 创建新的Sql查询字段类
        /// </summary>
        /// <param name="columnName">字段名称</param>
        internal static SqlQueryField InternalCreateFromColumn(String columnName)
        {
            return new SqlQueryField(String.Empty, columnName, String.Empty);
        }

        /// <summary>
        /// 创建新的Sql查询字段类
        /// </summary>
        /// <param name="tableName">表格名称</param>
        /// <param name="function">合计函数</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="aliasesName">字段别名</param>
        internal static SqlQueryField InternalCreateFromAggregateFunction(String tableName, SqlAggregateFunction function, String columnName, String aliasesName)
        {
            return new SqlQueryField(tableName, function, columnName, aliasesName);
        }

        /// <summary>
        /// 创建新的Sql查询字段类
        /// </summary>
        /// <param name="function">合计函数</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="aliasesName">字段别名</param>
        internal static SqlQueryField InternalCreateFromAggregateFunction(SqlAggregateFunction function, String columnName, String aliasesName)
        {
            return new SqlQueryField(String.Empty, function, columnName, aliasesName);
        }

        /// <summary>
        /// 创建新的Sql查询字段类
        /// </summary>
        /// <param name="tableName">表格名称</param>
        /// <param name="function">合计函数</param>
        /// <param name="columnName">字段名称</param>
        internal static SqlQueryField InternalCreateFromAggregateFunction(String tableName, SqlAggregateFunction function, String columnName)
        {
            return new SqlQueryField(tableName, function, columnName, String.Empty);
        }

        /// <summary>
        /// 创建新的Sql查询字段类
        /// </summary>
        /// <param name="function">合计函数</param>
        /// <param name="columnName">字段名称</param>
        internal static SqlQueryField InternalCreateFromAggregateFunction(SqlAggregateFunction function, String columnName)
        {
            return new SqlQueryField(String.Empty, function, columnName, String.Empty);
        }

        /// <summary>
        /// 创建新的Sql查询字段类
        /// </summary>
        /// <param name="tableName">表格名称</param>
        /// <param name="function">合计函数</param>
        internal static SqlQueryField InternalCreateFromAggregateFunction(String tableName, SqlAggregateFunction function)
        {
            return new SqlQueryField(tableName, function, "*", String.Empty);
        }

        /// <summary>
        /// 创建新的Sql查询字段类
        /// </summary>
        /// <param name="function">合计函数</param>
        internal static SqlQueryField InternalCreateFromAggregateFunction(SqlAggregateFunction function)
        {
            return new SqlQueryField(String.Empty, function, "*", String.Empty);
        }

        /// <summary>
        /// 创建新的Sql查询字段类
        /// </summary>
        /// <param name="function">函数名称</param>
        internal static SqlQueryField InternalCreateFromFunction(String function)
        {
            return new SqlQueryField(function, String.Empty);
        }

        /// <summary>
        /// 创建新的Sql查询字段类
        /// </summary>
        /// <param name="function">函数名称</param>
        /// <param name="aliasesName">别名</param>
        internal static SqlQueryField InternalCreateFromFunction(String function, String aliasesName)
        {
            return new SqlQueryField(function, aliasesName);
        }
        #endregion
    }
}