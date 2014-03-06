using System;

namespace DotMaysWind.Data.Command.Function
{
    /// <summary>
    /// Sql去除左边空格函数类
    /// </summary>
    public class SqlLTrim : AbstractSqlBasicFunction
    {
        #region 构造方法
        /// <summary>
        /// 初始化Sql去除左边空格函数类
        /// </summary>
        /// <param name="parameter">函数参数</param>
        public SqlLTrim(String parameter)
            : base(parameter) { }
        #endregion

        #region 方法
        /// <summary>
        /// 获取函数拼接后字符串
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns>函数拼接后字符串</returns>
        public override String GetSqlFunction(DatabaseType dbType)
        {
            return String.Format("LTRIM({0})", this._parameter);
        }
        #endregion
    }
}