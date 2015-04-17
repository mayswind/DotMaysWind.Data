using System;
using System.Data.Common;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Command.Function;
using DotMaysWind.Data.Command.Pager;

namespace DotMaysWind.Data
{
    /// <summary>
    /// SQL Server Compact Edition 数据库类
    /// </summary>
    public class SqlServerCeDatabase : AbstractDatabase
    {
        #region 常量
        private static readonly String[] SqlServerCeDatePart = { "year", "quarter", "month", "week", "dayofyear", "day", "weekday", "hour", "minute", "second" };
        #endregion

        #region 属性
        /// <summary>
        /// 获取当前数据库类型
        /// </summary>
        public override DatabaseType DatabaseType
        {
            get { return DatabaseType.SqlServerCe; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化数据库类
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="dbProvider">数据库提供者</param>
        internal SqlServerCeDatabase(DbProviderFactory dbProvider, String connectionString)
            : base(dbProvider, connectionString) { }
        #endregion

        #region 方法
        /// <summary>
        /// 获取分页后的选择语句
        /// </summary>
        /// <param name="sourceCommand">源选择语句</param>
        /// <param name="orderReverse">是否反转</param>
        /// <returns>分页后的选择语句</returns>
        internal override String InternalGetPagerSelectCommand(SelectCommand sourceCommand, Boolean orderReverse)
        {
            if (sourceCommand.PageSize <= 0 || sourceCommand.RecordStart <= 0)//正常模式或分页模式中的子语句
            {
                return AccessSelectPager.InternalGetPagerCommand(sourceCommand, 0, orderReverse);
            }

            String cntCommand = AccessSelectPager.InternalGetCountCommand(sourceCommand);
            DataParameter[] parameters = sourceCommand.GetAllParameters();

            DbCommand dbCommand = this.CreateDbCommand(cntCommand, parameters);
            Int32 recordCount = this.ExecuteScalar<Int32>(dbCommand);

            return this.InternalGetPagerSelectCommand(sourceCommand, recordCount, orderReverse);
        }

        /// <summary>
        /// 获取分页后的选择语句
        /// </summary>
        /// <param name="sourceCommand">源选择语句</param>
        /// <param name="recordCount">记录数量</param>
        /// <param name="orderReverse">是否反转</param>
        /// <returns>分页后的选择语句</returns>
        internal override String InternalGetPagerSelectCommand(SelectCommand sourceCommand, Int32 recordCount, Boolean orderReverse)
        {
            if (sourceCommand.RecordStart < recordCount)
            {
                return AccessSelectPager.InternalGetPagerCommand(sourceCommand, recordCount, orderReverse);
            }
            else
            {
                return AccessSelectPager.InternalGetSelectNoneCommand(sourceCommand, orderReverse);
            }
        }

        /// <summary>
        /// 获取部分日期类型字符串表示
        /// </summary>
        /// <param name="type">部分日期类型</param>
        /// <returns>部分日期类型字符串表示</returns>
        internal override String InternalGetDatePart(Byte type)
        {
            return SqlServerCeDatabase.SqlServerCeDatePart[type];
        }

        /// <summary>
        /// 获取判断为空函数
        /// </summary>
        /// <param name="parameter">函数条件</param>
        /// <param name="contentTwo">内容2</param>
        /// <returns>判断为空函数</returns>
        internal override String InternalGetIsNullFunction(String parameter, String contentTwo)
        {
            return String.Format("COALESCE({0}, {1})", parameter, contentTwo);
        }
        #endregion
    }
}