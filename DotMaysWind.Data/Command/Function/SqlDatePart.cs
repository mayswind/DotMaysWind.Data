using System;

namespace DotMaysWind.Data.Command.Function
{
    /// <summary>
    /// Sql部分日期类型
    /// </summary>
    public enum SqlDatePartType : byte
    {
        /// <summary>
        /// 年份
        /// </summary>
        Year = 0,

        /// <summary>
        /// 季度
        /// </summary>
        Quarter = 1,

        /// <summary>
        /// 月份
        /// </summary>
        Month = 2,

        /// <summary>
        /// 年中第几周
        /// </summary>
        WeekOfYear = 3,

        /// <summary>
        /// 年中第几天
        /// </summary>
        DayOfYear = 4,

        /// <summary>
        /// 月中第几天
        /// </summary>
        DayOfMonth = 5,
        
        /// <summary>
        /// 周中第几天
        /// </summary>
        DayOfWeek = 6,
        
        /// <summary>
        /// 小时
        /// </summary>
        Hour = 7,

        /// <summary>
        /// 分钟
        /// </summary>
        Minute = 8,

        /// <summary>
        /// 秒钟
        /// </summary>
        Second = 9
    }

    /// <summary>
    /// Sql部分日期函数类
    /// </summary>
    public class SqlDatePart : AbstractSqlBasicFunction
    {
        #region 常量
        private readonly String[] AccessDatePart = { "yyyy", "q", "m", "ww", "y", "d", "w", "h", "n", "s" };
        private readonly String[] MySQLDateFunction = { "YEAR", "QUARTER", "MONTH", "WEEK", "DAYOFYEAR", "DAYOFMONTH", "DAYOFWEEK", "HOUR", "MINUTE", "SECOND" };
        private readonly String[] SQLiteDatePart = { "%Y", "", "%m", "%W", "%j", "%d", "%w", "%H", "%M", "%S" };
        private readonly String[] OracleDatePart = { "yyyy", "q", "mm", "ww", "ddd", "dd", "d", "hh24", "mi", "ss" };
        private readonly String[] SqlServerDatePart = { "year", "quarter", "month", "week", "dayofyear", "day", "weekday", "hour", "minute", "second" };
        #endregion

        #region 字段
        private SqlDatePartType _datepartType;
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化Sql部分日期函数类
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <param name="datepartType">获取部分的类型</param>
        public SqlDatePart(String parameter, SqlDatePartType datepartType)
            : base(parameter)
        {
            this._datepartType = datepartType;
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
                return String.Format("DATEPART('{0}', {1})", AccessDatePart[(Byte)this._datepartType], this._parameter);
            }
            else if (dbType == DatabaseType.MySQL)
            {
                return String.Format("{0}({1})", MySQLDateFunction[(Byte)this._datepartType], this._parameter);
            }
            else if (dbType == DatabaseType.SQLite)
            {
                if (this._datepartType == SqlDatePartType.Quarter)
                {
                    return String.Format("ROUND(STRFTIME('{0}', {1})/4.0+1)", SQLiteDatePart[(Byte)SqlDatePartType.Month], this._parameter);
                }
                else if (this._datepartType == SqlDatePartType.DayOfWeek)
                {
                    return String.Format("(STRFTIME('{0}', {1})+1)", SQLiteDatePart[(Byte)this._datepartType], this._parameter);
                }
                else
                {
                    return String.Format("STRFTIME('{0}', {1})", SQLiteDatePart[(Byte)this._datepartType], this._parameter);
                }
            }
            else if (dbType == DatabaseType.Oracle)
            {
                return String.Format("TO_CHAR({1}, '{0}')", OracleDatePart[(Byte)this._datepartType], this._parameter);
            }
            else//Sql Server & Sql Server Ce
            {
                return String.Format("DATEPART({0}, {1})", SqlServerDatePart[(Byte)this._datepartType], this._parameter);
            }
        }
        #endregion
    }
}