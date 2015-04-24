using System;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql分类字段类
    /// </summary>
    public class SqlGroupByField
    {
        #region 字段
        private String _tableName;
        private String _columnName;

        private Boolean _useFunction;
        private String _function;
        #endregion

        #region 属性
        /// <summary>
        /// 获取表格名称
        /// </summary>
        public String TableName
        {
            get { return this._tableName; }
        }

        /// <summary>
        /// 获取或设置字段名
        /// </summary>
        public String FieldName
        {
            get { return this._columnName; }
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
        /// 初始化新的Sql分类字段类
        /// </summary>
        /// <param name="baseCommand">选择语句</param>
        /// <param name="tableName">表格名称</param>
        /// <param name="columnName">字段名</param>
        private SqlGroupByField(SelectCommand baseCommand, String tableName, String columnName)
        {
            this._tableName = tableName;
            this._columnName = columnName;
            this._useFunction = false;
            this._function = String.Empty;
        }

        /// <summary>
        /// 初始化新的Sql分类字段类
        /// </summary>
        /// <param name="baseCommand">选择语句</param>
        /// <param name="function">合计函数</param>
        private SqlGroupByField(SelectCommand baseCommand, String function)
        {
            this._tableName = String.Empty;
            this._columnName = String.Empty;
            this._useFunction = true;
            this._function = function;
        }
        #endregion

        #region 静态方法
        #region InternalCreate
        /// <summary>
        /// 创建新的Sql分类字段类
        /// </summary>
        /// <param name="cmd">选择语句</param>
        /// <param name="tableName">表格名称</param>
        /// <param name="columnName">字段名</param>
        internal static SqlGroupByField InternalCreate(SelectCommand cmd, String tableName, String columnName)
        {
            return new SqlGroupByField(cmd, tableName, columnName);
        }

        /// <summary>
        /// 创建新的Sql分类字段类
        /// </summary>
        /// <param name="cmd">选择语句</param>
        /// <param name="columnName">字段名</param>
        internal static SqlGroupByField InternalCreate(SelectCommand cmd, String columnName)
        {
            return new SqlGroupByField(cmd, String.Empty, columnName);
        }
        #endregion

        #region InternalCreateFromFunction
        /// <summary>
        /// 创建新的Sql分类字段类
        /// </summary>
        /// <param name="cmd">源选择语句</param>
        /// <param name="function">函数内容</param>
        internal static SqlGroupByField InternalCreateFromFunction(SelectCommand cmd, String function)
        {
            return new SqlGroupByField(cmd, function);
        }
        #endregion
        #endregion

        #region 重载方法和运算符
        /// <summary>
        /// 获取当前参数的哈希值
        /// </summary>
        /// <returns>当前参数的哈希值</returns>
        public override Int32 GetHashCode()
        {
            return this._columnName.GetHashCode();
        }

        /// <summary>
        /// 判断两个Sql分类字段是否相同
        /// </summary>
        /// <param name="obj">待比较的Sql分类字段</param>
        /// <returns>两个Sql分类字段是否相同</returns>
        public override Boolean Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            SqlGroupByField field = obj as SqlGroupByField;

            if (field == null)
            {
                return false;
            }

            if (!String.Equals(this._tableName, field._tableName))
            {
                return false;
            }

            if (!String.Equals(this._columnName, field._columnName))
            {
                return false;
            }

            if (this._useFunction != field._useFunction)
            {
                return false;
            }

            if (!String.Equals(this._function, field._function))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 判断两个Sql分类字段是否相同
        /// </summary>
        /// <param name="obj">待比较的Sql分类字段</param>
        /// <param name="obj2">待比较的第二个Sql分类字段</param>
        /// <returns>两个Sql分类字段是否相同</returns>
        public static Boolean operator ==(SqlGroupByField obj, SqlGroupByField obj2)
        {
            return Object.Equals(obj, obj2);
        }

        /// <summary>
        /// 判断两个Sql分类字段是否不同
        /// </summary>
        /// <param name="obj">待比较的Sql分类字段</param>
        /// <param name="obj2">待比较的第二个Sql分类字段</param>
        /// <returns>两个Sql分类字段是否不同</returns>
        public static Boolean operator !=(SqlGroupByField obj, SqlGroupByField obj2)
        {
            return !Object.Equals(obj, obj2);
        }

        /// <summary>
        /// 返回当前对象的信息
        /// </summary>
        /// <returns>当前对象的信息</returns>
        public override String ToString()
        {
            return String.Format("{0}, {1}", base.ToString(), this._function + this._columnName);
        }
        #endregion
    }
}