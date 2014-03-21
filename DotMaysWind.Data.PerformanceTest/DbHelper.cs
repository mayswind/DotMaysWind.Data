using System;
using System.Data;
using System.Data.Common;

namespace DotMaysWind.Data.PerformanceTest
{
    /// <summary>
    /// 测试数据库辅助类
    /// </summary>
    internal static class DbHelper
    {
        #region 字段
        private static IDatabase _fakeDb;
        #endregion

        #region 属性
        /// <summary>
        /// 获取当前数据库
        /// </summary>
        public static IDatabase FakeDb
        {
            get { return _fakeDb; }
        }
        #endregion

        #region 构造方法
        static DbHelper()
        {
            _fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient");
        }
        #endregion

        #region 方法
        internal static DbParameter InternalCreateDbParameter(String columnName, String paramName, DbType type, Object value)
        {
            DbParameter param = _fakeDb.CreateDbParameter();

            param.SourceColumn = columnName;
            param.ParameterName = paramName;
            param.DbType = type;
            param.Value = value;

            return param;
        }
        #endregion
    }
}