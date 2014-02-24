using System;

namespace DotMaysWind.Data.Command.Function
{
    /// <summary>
    /// Sql判断为空函数类
    /// </summary>
    public class SqlIsNull : AbstractSqlBasicFunction
    {
        #region 字段
        private String _contentTwo;
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化Sql判断为空函数类
        /// </summary>
        /// <param name="condition">函数条件</param>
        /// <param name="contentTwo">内容2</param>
        public SqlIsNull(String condition, String contentTwo)
            : base(condition)
        {
            this._contentTwo = contentTwo;
        }
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
                return String.Format("IIF(ISNULL({0}), {1}, {0})", this._parameter, this._contentTwo);
            }
            else if (dbType == DatabaseType.MySQL || dbType == DatabaseType.SQLite)
            {
                return String.Format("IFNULL({0}, {1})", this._parameter, this._contentTwo);
            }
            else if (dbType == DatabaseType.Oracle)
            {
                return String.Format("NVL({0}, {1})", this._parameter, this._contentTwo);
            }
            else if (dbType == DatabaseType.SqlServerCe)
            {
                return String.Format("COALESCE({0}, {1})", this._parameter, this._contentTwo);
            }
            else//Sql Server
            {
                return String.Format("ISNULL({0}, {1})", this._parameter, this._contentTwo);
            }
        }
        #endregion
    }
}