using System;

namespace DotMaysWind.Data.Command.Function
{
    /// <summary>
    /// Sql字符串字数函数类
    /// </summary>
    public class SqlLen : AbstractSqlBasicFunction
    {
        #region 构造方法
        /// <summary>
        /// 初始化Sql字符串字数函数类
        /// </summary>
        /// <param name="parameter">函数参数</param>
        public SqlLen(String parameter)
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
            if (dbType == DatabaseType.Access || dbType == DatabaseType.SqlServer || dbType == DatabaseType.SqlServerCe)
            {
                return String.Format("LEN({0})", this._parameter);
            }
            else
            {
                return String.Format("LENGTH({0})", this._parameter);
            }
        }
        #endregion
    }
}