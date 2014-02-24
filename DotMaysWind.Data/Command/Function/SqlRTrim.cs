using System;

namespace DotMaysWind.Data.Command.Function
{
    /// <summary>
    /// Sql去除右边空格函数类
    /// </summary>
    public class SqlRTrim : AbstractSqlBasicFunction
    {
        #region 构造方法
        /// <summary>
        /// 初始化Sql去除右边空格函数类
        /// </summary>
        /// <param name="parameter">函数参数</param>
        public SqlRTrim(String parameter)
            : base(parameter) { }
        #endregion

        #region 方法
        /// <summary>
        /// 获取函数拼接后字符串
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns>函数拼接后字符串</returns>
        public override String ToString(DatabaseType dbType)
        {
            return String.Format("RTRIM({0})", this._parameter);
        }
        #endregion
    }
}