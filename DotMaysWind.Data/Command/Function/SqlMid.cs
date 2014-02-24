using System;

namespace DotMaysWind.Data.Command.Function
{
    /// <summary>
    /// Sql字符提取函数类
    /// </summary>
    public class SqlMid : AbstractSqlBasicFunction
    {
        #region 字段
        private Int32 _start;
        private Int32 _length;
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化Sql字符提取函数类
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <param name="start">子串起始位置</param>
        /// <param name="length">子串长度</param>
        public SqlMid(String parameter, Int32 start, Int32 length)
            : base(parameter)
        {
            this._start = start;
            this._length = length;
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
            if (dbType == DatabaseType.SqlServer || dbType == DatabaseType.SqlServerCe)
            {
                return String.Format("SUBSTRING({0}, {1}, {2})", this._parameter, this._start.ToString(), this._length.ToString());
            }
            else if (dbType == DatabaseType.MySQL || dbType == DatabaseType.SQLite || dbType == DatabaseType.Oracle)
            {
                return String.Format("SUBSTR({0}, {1}, {2})", this._parameter, this._start.ToString(), this._length.ToString());
            }
            else//Access
            {
                return String.Format("MID({0}, {1}, {2})", this._parameter, this._start.ToString(), this._length.ToString());
            }
        }
        #endregion
    }
}