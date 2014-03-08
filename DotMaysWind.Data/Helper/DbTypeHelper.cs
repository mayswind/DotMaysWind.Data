using System;
using System.Collections.Generic;
using System.Data;

namespace DotMaysWind.Data.Helper
{
    /// <summary>
    /// 数据类型辅助类
    /// </summary>
    internal static class DbTypeHelper
    {
        #region 静态字段
        private static Dictionary<Type, DbType> TypeDict;
        #endregion

        #region 静态构造方法
        static DbTypeHelper()
        {
            TypeDict = new Dictionary<Type, DbType>();

            TypeDict[typeof(Byte[])] = DbType.Binary;

            TypeDict[typeof(Boolean)] = DbType.Boolean;
            TypeDict[typeof(Boolean?)] = DbType.Boolean;
            
            TypeDict[typeof(Byte)] = DbType.Byte;
            TypeDict[typeof(Byte?)] = DbType.Byte;
            TypeDict[typeof(SByte)] = DbType.SByte;
            TypeDict[typeof(SByte?)] = DbType.SByte;

            TypeDict[typeof(Int16)] = DbType.Int16;
            TypeDict[typeof(Int16?)] = DbType.Int16;
            TypeDict[typeof(Int32)] = DbType.Int32;
            TypeDict[typeof(Int32?)] = DbType.Int32;
            TypeDict[typeof(Int64)] = DbType.Int64;
            TypeDict[typeof(Int64?)] = DbType.Int64;
            TypeDict[typeof(UInt16)] = DbType.UInt16;
            TypeDict[typeof(UInt16?)] = DbType.UInt16;
            TypeDict[typeof(UInt32)] = DbType.UInt32;
            TypeDict[typeof(UInt32?)] = DbType.UInt32;
            TypeDict[typeof(UInt64)] = DbType.UInt64;
            TypeDict[typeof(UInt64?)] = DbType.UInt64;

            TypeDict[typeof(Single)] = DbType.Single;
            TypeDict[typeof(Single?)] = DbType.Single;
            TypeDict[typeof(Double)] = DbType.Double;
            TypeDict[typeof(Double?)] = DbType.Double;
            TypeDict[typeof(Decimal)] = DbType.Decimal;
            TypeDict[typeof(Decimal?)] = DbType.Decimal;
            
            TypeDict[typeof(DateTime)] = DbType.DateTime;
            TypeDict[typeof(DateTime?)] = DbType.DateTime;
            TypeDict[typeof(DateTimeOffset)] = DbType.DateTimeOffset;
            TypeDict[typeof(DateTimeOffset?)] = DbType.DateTimeOffset;

            TypeDict[typeof(String)] = DbType.String;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 根据参数类型获取数据类型
        /// </summary>
        /// <param name="type">参数类型</param>
        /// <returns>数据类型</returns>
        internal static DbType InternalGetDbType(Type type)
        {
            DbType dbType = DbType.String;

            if (!TypeDict.TryGetValue(type, out dbType))
            {
                dbType = DbType.String;
            }

            return dbType;
        }

        /// <summary>
        /// 根据参数值获取数据类型
        /// </summary>
        /// <param name="value">参数值</param>
        /// <returns>数据类型</returns>
        internal static DbType InternalGetDbType(Object value)
        {
            DbType dbType = DbType.String;

            if (value == null)
            {
                return dbType;
            }

            Type type = value.GetType();
            
            if (!TypeDict.TryGetValue(type, out dbType))
            {
                dbType = DbType.String;
            }

            return dbType;
        }
        #endregion
    }
}