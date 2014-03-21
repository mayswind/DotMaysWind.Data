using System;
using System.Data.Common;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Command.Function;
using DotMaysWind.Data.Command.Pager;

namespace DotMaysWind.Data
{
    /// <summary>
    /// SQLite数据库
    /// </summary>
    public class SQLiteDatabase : AbstractDatabase
    {
        #region 常量
        private static readonly String[] SQLiteDatePart = { "%Y", "", "%m", "%W", "%j", "%d", "%w", "%H", "%M", "%S" };
        #endregion

        #region 属性
        /// <summary>
        /// 获取当前数据库类型
        /// </summary>
        public override DatabaseType DatabaseType
        {
            get { return DatabaseType.SQLite; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化数据库类
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="dbProvider">数据库提供者</param>
        internal SQLiteDatabase(DbProviderFactory dbProvider, String connectionString)
            : base(dbProvider, connectionString) { }
        #endregion

        #region 方法
        /// <summary>
        /// 获取分页后的选择语句
        /// </summary>
        /// <param name="sourceCommand">源选择语句</param>
        /// <param name="realPageIndex">实际页面索引</param>
        /// <param name="realPageCount">实际页面数量</param>
        /// <param name="orderReverse">是否反转</param>
        /// <returns>分页后的选择语句</returns>
        internal override String InternalGetPagerSelectCommand(SelectCommand sourceCommand, Int32 realPageIndex, Int32 realPageCount, Boolean orderReverse)
        {
            return MySQLSelectPager.InternalGetPagerCommand(sourceCommand, realPageIndex, realPageCount, orderReverse);
        }

        /// <summary>
        /// 获取部分日期类型字符串表示
        /// </summary>
        /// <param name="type">部分日期类型</param>
        /// <returns>部分日期类型字符串表示</returns>
        internal override String InternalGetDatePart(Byte type)
        {
            if (type == (Byte)SqlDatePartType.Quarter)
            {
                return SQLiteDatabase.SQLiteDatePart[(Byte)(SqlDatePartType.Month)];
            }
            else
            {
                return SQLiteDatabase.SQLiteDatePart[type];
            }
        }

        /// <summary>
        /// 获取最后一条记录需要查询的名称
        /// </summary>
        /// <returns>需要查询的名称</returns>
        internal override String InternalGetIdentityFieldName()
        {
            return "LAST_INSERT_ROWID()";
        }

        /// <summary>
        /// 获取判断为空函数
        /// </summary>
        /// <param name="parameter">函数条件</param>
        /// <param name="contentTwo">内容2</param>
        /// <returns>判断为空函数</returns>
        internal override String InternalGetIsNullFunction(String parameter, String contentTwo)
        {
            return String.Format("IFNULL({0}, {1})", parameter, contentTwo);
        }

        /// <summary>
        /// 获取字符串长度函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <returns>字符串长度函数</returns>
        internal override String InternalGetLengthFunction(String parameter)
        {
            return String.Format("LENGTH({0})", parameter);
        }

        /// <summary>
        /// 获取字符串提取函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <param name="start">子串起始位置</param>
        /// <param name="length">子串长度</param>
        /// <returns>字符串提取函数</returns>
        internal override String InternalGetMidFunction(String parameter, String start, String length)
        {
            return String.Format("SUBSTR({0}, {1}, {2})", parameter, start, length);
        }

        /// <summary>
        /// 获取当前日期函数
        /// </summary>
        /// <returns>当前日期函数</returns>
        internal override String InternalGetNowFunction()
        {
            return String.Format("DATETIME('NOW')");
        }

        /// <summary>
        /// 获取当前日期函数
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <param name="datepartType">获取部分的类型</param>
        /// <returns>当前日期函数</returns>
        internal override String InternalGetDatePartFunction(String parameter, String datepartType)
        {
            Byte type = Convert.ToByte(datepartType);

            if (type == (Byte)SqlDatePartType.Quarter)
            {
                return String.Format("ROUND(STRFTIME('{0}', {1})/4.0+1)", this.InternalGetDatePart(type), parameter);
            }
            else if (type == (Byte)SqlDatePartType.DayOfWeek)
            {
                return String.Format("(STRFTIME('{0}', {1})+1)", this.InternalGetDatePart(type), parameter);
            }
            else
            {
                return String.Format("STRFTIME('{0}', {1})", this.InternalGetDatePart(type), parameter);
            }
        }
        #endregion
    }
}