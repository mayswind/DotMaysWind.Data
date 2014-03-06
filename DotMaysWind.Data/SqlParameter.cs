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
        /// 获取或设置参数名
        /// </summary>
        public String ParameterName
        {
            get { return this._parameter.ParameterName; }
        }

        /// <summary>
        /// 获取或设置数据类型
        /// </summary>
        public DbType DbType
        {
            get { return this._parameter.DbType; }
            set { this._parameter.DbType = value; }
        }

        /// <summary>
        /// 获取或设置SQLServer数据类型
        /// </summary>
        public SqlDbType SqlDbType
        {
            get { return this._parameter.SqlDbType; }
            set { this._parameter.SqlDbType = value; }
        }

        /// <summary>
        /// 获取或设置参数值
        /// </summary>
        public Object Value
        {
            get
            {
                if (this._parameter.Value is DateTime)
                {
                    DateTime dt = (DateTime)this._parameter.Value;
                    return dt.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    return this._parameter.Value;
                }
            }
            set { this._parameter.Value = value; }
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
        /// <param name="columnName">字段名</param>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">赋值内容</param>
        private SqlParameter(String columnName, String parameterName, Object value)
        {
            this._parameter = new BaseSqlParameter();
            this._parameter.SourceColumn = columnName;
            this._parameter.ParameterName = Constants.GeneralParameterNamePrefix + parameterName;
            this._parameter.Value = (value ?? DBNull.Value);

            this._isUseParameter = true;
        }

        /// <summary>
        /// 初始化Sql语句参数类
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="action">赋值操作</param>
        private SqlParameter(String columnName, String action)
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
        /// <param name="columnName">字段名</param>
        /// <param name="value">赋值内容</param>
        /// <returns>Sql语句参数类</returns>
        public static SqlParameter Create(String columnName, Object value)
        {
            return SqlParameter.Create(columnName, columnName.Replace("(", "_").Replace(")", ""), DbTypeHelper.InternalGetDbType(value), value);
        }

        /// <summary>
        /// 创建新的Sql语句参数类
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">字段类型</param>
        /// <param name="value">赋值内容</param>
        /// <returns>Sql语句参数类</returns>
        public static SqlParameter Create(String columnName, DbType dbType, Object value)
        {
            return SqlParameter.Create(columnName, columnName.Replace("(", "_").Replace(")", ""), dbType, value);
        }

        /// <summary>
        /// 创建新的Sql语句参数类
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">字段类型</param>
        /// <param name="value">赋值内容</param>
        /// <returns>Sql语句参数类</returns>
        public static SqlParameter Create(String columnName, SqlDbType dbType, Object value)
        {
            return SqlParameter.Create(columnName, columnName.Replace("(", "").Replace(")", ""), dbType, value);
        }

        /// <summary>
        /// 创建新的Sql语句参数类
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">赋值内容</param>
        /// <returns>Sql语句参数类</returns>
        public static SqlParameter Create(String columnName, String parameterName, Object value)
        {
            return SqlParameter.Create(columnName, parameterName, DbTypeHelper.InternalGetDbType(value), value);
        }

        /// <summary>
        /// 创建新的Sql语句参数类
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="parameterName">参数名</param>
        /// <param name="dbType">字段类型</param>
        /// <param name="value">赋值内容</param>
        /// <returns>Sql语句参数类</returns>
        public static SqlParameter Create(String columnName, String parameterName, DbType dbType, Object value)
        {
            SqlParameter param = new SqlParameter(columnName, parameterName, value);
            param.DbType = dbType;

            return param;
        }

        /// <summary>
        /// 创建新的Sql语句参数类
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="parameterName">参数名</param>
        /// <param name="dbType">字段类型</param>
        /// <param name="value">赋值内容</param>
        /// <returns>Sql语句参数类</returns>
        public static SqlParameter Create(String columnName, String parameterName, SqlDbType dbType, Object value)
        {
            SqlParameter param = new SqlParameter(columnName, parameterName, value);
            param.SqlDbType = dbType;

            return param;
        }
        #endregion

        #region 创建操作型Sql语句参数类
        /// <summary>
        /// 创建新的Sql语句参数类
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="action">赋值操作</param>
        /// <returns>Sql语句参数类</returns>
        public static SqlParameter CreateCustomAction(String columnName, String action)
        {
            return new SqlParameter(columnName, action);
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