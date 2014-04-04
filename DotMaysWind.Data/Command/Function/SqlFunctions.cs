using System;
using System.Collections.Generic;

namespace DotMaysWind.Data.Command.Function
{
    /// <summary>
    /// Sql函数类
    /// </summary>
    public class SqlFunctions
    {
        #region 字段
        private AbstractDatabase _baseDatabase;
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的Sql函数类
        /// </summary>
        /// <param name="baseDatabase">源数据库</param>
        public SqlFunctions(AbstractDatabase baseDatabase)
        {
            this._baseDatabase = baseDatabase;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取Sql判空函数
        /// </summary>
        /// <param name="condition">函数条件</param>
        /// <param name="contentTwo">内容2</param>
        /// <returns>Sql判空函数</returns>
        public SqlInnerFunction IsNull(String condition, String contentTwo)
        {
            return new SqlInnerFunction(this._baseDatabase, SqlInnerFunctionType.IsNull, condition, contentTwo);
        }

        /// <summary>
        /// 获取Sql大写函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <returns>Sql大写函数</returns>
        public SqlInnerFunction Upper(String parameter)
        {
            return new SqlInnerFunction(this._baseDatabase, SqlInnerFunctionType.Upper, parameter);
        }

        /// <summary>
        /// 获取Sql小写函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <returns>Sql小写函数</returns>
        public SqlInnerFunction Lower(String parameter)
        {
            return new SqlInnerFunction(this._baseDatabase, SqlInnerFunctionType.Lower, parameter);
        }

        /// <summary>
        /// 获取Sql去除左边空格函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <returns>Sql去除左边空格函数</returns>
        public SqlInnerFunction LTrim(String parameter)
        {
            return new SqlInnerFunction(this._baseDatabase, SqlInnerFunctionType.LTrim, parameter);
        }

        /// <summary>
        /// 获取Sql去除右边空格函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <returns>Sql去除右边空格函数</returns>
        public SqlInnerFunction RTrim(String parameter)
        {
            return new SqlInnerFunction(this._baseDatabase, SqlInnerFunctionType.RTrim, parameter);
        }

        /// <summary>
        /// 获取Sql去除两边空格函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <returns>Sql去除两边空格函数</returns>
        public SqlInnerFunction Trim(String parameter)
        {
            return new SqlInnerFunction(this._baseDatabase, SqlInnerFunctionType.Trim, parameter);
        }

        /// <summary>
        /// 获取Sql字符串长度函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <returns>Sql字符串长度函数</returns>
        public SqlInnerFunction Length(String parameter)
        {
            return new SqlInnerFunction(this._baseDatabase, SqlInnerFunctionType.Length, parameter);
        }

        /// <summary>
        /// 获取Sql字符串提取函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <param name="start">子串起始位置</param>
        /// <param name="length">子串长度</param>
        /// <returns>Sql字符串提取函数</returns>
        public SqlInnerFunction Mid(String parameter, Int32 start, Int32 length)
        {
            return new SqlInnerFunction(this._baseDatabase, SqlInnerFunctionType.Mid, parameter, start.ToString(), length.ToString());
        }

        /// <summary>
        /// 获取Sql取整函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <param name="decimals">小数位数</param>
        /// <returns>Sql取整函数</returns>
        public SqlInnerFunction Round(String parameter, Int32 decimals)
        {
            return new SqlInnerFunction(this._baseDatabase, SqlInnerFunctionType.Round, parameter, decimals.ToString());
        }

        /// <summary>
        /// 获取Sql当前日期函数
        /// </summary>
        /// <returns>Sql当前日期函数</returns>
        public SqlInnerFunction Now()
        {
            return new SqlInnerFunction(this._baseDatabase, SqlInnerFunctionType.Now);
        }

        /// <summary>
        /// 获取Sql部分日期函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <param name="datepartType">获取部分的类型</param>
        /// <returns>Sql部分日期函数</returns>
        public SqlInnerFunction DatePart(String parameter, SqlDatePartType datepartType)
        {
            return new SqlInnerFunction(this._baseDatabase, SqlInnerFunctionType.DatePart, parameter, ((Byte)datepartType).ToString());
        }
        #endregion
    }
}