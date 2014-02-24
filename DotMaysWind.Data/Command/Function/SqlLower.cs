using System;

namespace DotMaysWind.Data.Command.Function
{
    /// <summary>
    /// Sql小写函数类
    /// </summary>
    public class SqlLower : AbstractSqlBasicFunction
    {
        #region 构造方法
        /// <summary>
        /// 初始化Sql小写函数类
        /// </summary>
        /// <param name="parameter">函数参数</param>
        public SqlLower(String parameter)
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
            if (dbType == DatabaseType.Access)
            {
                return String.Format("LCASE({0})", this._parameter);
            }
            else
            {
                return String.Format("LOWER ({0})", this._parameter);
            }
        }
        #endregion
    }
}