using System;
using System.Collections.Generic;

namespace DotMaysWind.Data.Helper
{
    /// <summary>
    /// 数据类型辅助类
    /// </summary>
    internal static class DataTypeHelper
    {
        #region 静态字段
        private static Dictionary<Type, DataType> TypeDict;
        #endregion

        #region 静态构造方法
        static DataTypeHelper()
        {
            TypeDict = new Dictionary<Type, DataType>();

            TypeDict[typeof(Byte[])] = DataType.Binary;

            TypeDict[typeof(Boolean)] = DataType.Boolean;
            TypeDict[typeof(Boolean?)] = DataType.Boolean;
            
            TypeDict[typeof(Byte)] = DataType.Byte;
            TypeDict[typeof(Byte?)] = DataType.Byte;
            TypeDict[typeof(SByte)] = DataType.SByte;
            TypeDict[typeof(SByte?)] = DataType.SByte;

            TypeDict[typeof(Int16)] = DataType.Int16;
            TypeDict[typeof(Int16?)] = DataType.Int16;
            TypeDict[typeof(Int32)] = DataType.Int32;
            TypeDict[typeof(Int32?)] = DataType.Int32;
            TypeDict[typeof(Int64)] = DataType.Int64;
            TypeDict[typeof(Int64?)] = DataType.Int64;
            TypeDict[typeof(UInt16)] = DataType.UInt16;
            TypeDict[typeof(UInt16?)] = DataType.UInt16;
            TypeDict[typeof(UInt32)] = DataType.UInt32;
            TypeDict[typeof(UInt32?)] = DataType.UInt32;
            TypeDict[typeof(UInt64)] = DataType.UInt64;
            TypeDict[typeof(UInt64?)] = DataType.UInt64;

            TypeDict[typeof(Single)] = DataType.Single;
            TypeDict[typeof(Single?)] = DataType.Single;
            TypeDict[typeof(Double)] = DataType.Double;
            TypeDict[typeof(Double?)] = DataType.Double;
            TypeDict[typeof(Decimal)] = DataType.Decimal;
            TypeDict[typeof(Decimal?)] = DataType.Decimal;
            
            TypeDict[typeof(DateTime)] = DataType.DateTime;
            TypeDict[typeof(DateTime?)] = DataType.DateTime;
            TypeDict[typeof(DateTimeOffset)] = DataType.DateTimeOffset;
            TypeDict[typeof(DateTimeOffset?)] = DataType.DateTimeOffset;

            TypeDict[typeof(String)] = DataType.String;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 根据参数类型获取数据类型
        /// </summary>
        /// <param name="type">参数类型</param>
        /// <returns>数据类型</returns>
        internal static DataType InternalGetDataType(Type type)
        {
            DataType dataType = DataType.String;

            if (!TypeDict.TryGetValue(type, out dataType))
            {
                dataType = DataType.String;
            }

            return dataType;
        }

        /// <summary>
        /// 根据参数值获取数据类型
        /// </summary>
        /// <param name="value">参数值</param>
        /// <returns>数据类型</returns>
        internal static DataType InternalGetDataType(Object value)
        {
            DataType dataType = DataType.String;

            if (value == null)
            {
                return dataType;
            }

            Type type = value.GetType();
            
            if (!TypeDict.TryGetValue(type, out dataType))
            {
                dataType = DataType.String;
            }

            return dataType;
        }
        #endregion
    }
}