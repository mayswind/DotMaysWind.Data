using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using BaseSqlParameter = System.Data.SqlClient.SqlParameter;

using DotMaysWind.Data.Helper;

namespace DotMaysWind.Data
{
    /// <summary>
    /// Sql语句参数类
    /// </summary>
    public class SqlParameter
    {
        #region 字段
        private BaseSqlParameter _parameter;
        private Boolean _isUseParameter;
        #endregion

        #region 属性
        /// <summary>
        /// 获取字段名
        /// </summary>
        public String ColumnName
        {
            get { return this._parameter.SourceColumn; }
        }

        /// <summary>
        /// 获取参数名
        /// </summary>
        public String ParameterName
        {
            get { return this._parameter.ParameterName; }
        }

        /// <summary>
        /// 获取数据类型
        /// </summary>
        public DbType DbType
        {
            get { return this._parameter.DbType; }
        }

        /// <summary>
        /// 获取参数值
        /// </summary>
        public Object Value
        {
            get { return this._parameter.Value; }
        }

        /// <summary>
        /// 获取是否使用参数方式（即不使用赋值操作方式）
        /// </summary>
        internal Boolean IsUseParameter
        {
            get { return this._isUseParameter; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化Sql语句参数类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="columnName">字段名</param>
        /// <param name="parameterIndex">参数索引</param>
        /// <param name="value">赋值内容</param>
        private SqlParameter(AbstractDatabase database, String columnName, Int32 parameterIndex, Object value)
        {
            this._parameter = new BaseSqlParameter();
            this._parameter.SourceColumn = columnName;
            this._parameter.ParameterName = database.InternalGetParameterName("PN_IDX_" + parameterIndex.ToString());

            if (value == null)
            {
                this._parameter.Value = DBNull.Value;
            }
            else if (value is DateTime)
            {
                DateTime dt = (DateTime)value;
                this._parameter.Value = dt.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                this._parameter.Value = value;
            }

            this._isUseParameter = true;
        }

        /// <summary>
        /// 初始化Sql语句参数类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="columnName">字段名</param>
        /// <param name="action">赋值操作</param>
        private SqlParameter(AbstractDatabase database, String columnName, String action)
        {
            this._parameter = new BaseSqlParameter();
            this._parameter.SourceColumn = columnName;
            this._parameter.Value = action;

            this._isUseParameter = false;
        }
        #endregion

        #region 静态方法
        #region 创建参数型Sql语句参数类
        /// <summary>
        /// 创建新的Sql语句参数类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="columnName">字段名</param>
        /// <param name="parameterIndex">参数索引</param>
        /// <param name="value">赋值内容</param>
        /// <returns>Sql语句参数类</returns>
        internal static SqlParameter InternalCreate(AbstractDatabase database, String columnName, Int32 parameterIndex, Object value)
        {
            SqlParameter param = new SqlParameter(database, columnName, parameterIndex, value);
            param._parameter.DbType = DbTypeHelper.InternalGetDbType(value);

            return param;
        }

        /// <summary>
        /// 创建新的Sql语句参数类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="columnName">字段名</param>
        /// <param name="parameterIndex">参数索引</param>
        /// <param name="sqlDbType">字段类型</param>
        /// <param name="value">赋值内容</param>
        /// <returns>Sql语句参数类</returns>
        internal static SqlParameter InternalCreate(AbstractDatabase database, String columnName, Int32 parameterIndex, SqlDbType sqlDbType, Object value)
        {
            SqlParameter param = new SqlParameter(database, columnName, parameterIndex, value);
            param._parameter.SqlDbType = sqlDbType;

            return param;
        }

        /// <summary>
        /// 创建新的Sql语句参数类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="columnName">字段名</param>
        /// <param name="parameterIndex">参数索引</param>
        /// <param name="dbType">字段类型</param>
        /// <param name="value">赋值内容</param>
        /// <returns>Sql语句参数类</returns>
        internal static SqlParameter InternalCreate(AbstractDatabase database, String columnName, Int32 parameterIndex, DbType dbType, Object value)
        {
            SqlParameter param = new SqlParameter(database, columnName, parameterIndex, value);
            param._parameter.DbType = dbType;

            return param;
        }
        #endregion

        #region 创建操作型Sql语句参数类
        /// <summary>
        /// 创建新的Sql语句参数类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="columnName">字段名</param>
        /// <param name="action">赋值操作</param>
        /// <returns>Sql语句参数类</returns>
        internal static SqlParameter InternalCreateCustomAction(AbstractDatabase database, String columnName, String action)
        {
            return new SqlParameter(database, columnName, action);
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
            return this._parameter.GetHashCode();
        }

        /// <summary>
        /// 判断两个Sql语句参数是否相同
        /// </summary>
        /// <param name="obj">待比较的Sql语句参数</param>
        /// <returns>两个Sql语句参数是否相同</returns>
        public override Boolean Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            SqlParameter param = obj as SqlParameter;

            if (param == null)
            {
                return false;
            }

            if (this._isUseParameter != param._isUseParameter)
            {
                return false;
            }

            if (this.DbType != param.DbType)
            {
                return false;
            }

            if (!String.Equals(this.ColumnName, param.ColumnName))
            {
                return false;
            }

            if (!String.Equals(this.ParameterName, param.ParameterName))
            {
                return false;
            }

            if (!Object.Equals(this.Value, param.Value))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 判断两个Sql语句参数是否相同
        /// </summary>
        /// <param name="obj">待比较的Sql语句参数</param>
        /// <param name="obj2">待比较的第二个Sql语句参数</param>
        /// <returns>两个Sql语句参数是否相同</returns>
        public static Boolean operator ==(SqlParameter obj, SqlParameter obj2)
        {
            return Object.Equals(obj, obj2);
        }

        /// <summary>
        /// 判断两个Sql语句参数是否不同
        /// </summary>
        /// <param name="obj">待比较的Sql语句参数</param>
        /// <param name="obj2">待比较的第二个Sql语句参数</param>
        /// <returns>两个Sql语句参数是否不同</returns>
        public static Boolean operator !=(SqlParameter obj, SqlParameter obj2)
        {
            return !Object.Equals(obj, obj2);
        }

        /// <summary>
        /// 返回当前对象的信息
        /// </summary>
        /// <returns>当前对象的信息</returns>
        public override String ToString()
        {
            return String.Format("{0}, {1}, {2}", base.ToString(), this._parameter.ParameterName, this._parameter.DbType.ToString());
        }
        #endregion
    }
}