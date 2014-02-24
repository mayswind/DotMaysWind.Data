using System;

namespace DotMaysWind.Data.Command.Function
{
    /// <summary>
    /// Sql字符串字数函数类
    /// </summary>
    public class SqlNow : AbstractSqlBasicFunction
    {
        #region 构造方法
        /// <summary>
        /// 初始化Sql字符串字数函数类
        /// </summary>
        public SqlNow() : base(String.Empty) { }
        #endregion

        #region 方法
        /// <summary>
        /// 获取函数拼接后字符串
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns>函数拼接后字符串</returns>
        public override String ToString(DatabaseType dbType)
        {
            if (dbType == DatabaseType.SqlServer || dbType == DatabaseType.SqlServerCe)
            {
                return "GETDATE()";
            }
            else if (dbType == DatabaseType.SQLite)
            {
                return "DATETIME('NOW')";
            }
            else if (dbType == DatabaseType.Oracle)
            {
                return "SYSDATE";
            }
            else
            {
                return "NOW()";
            }
        }
        #endregion
    }
}