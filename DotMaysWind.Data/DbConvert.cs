using System;
using System.Data;

namespace DotMaysWind.Data
{
    /// <summary>
    /// 数据库类型转换器
    /// </summary>
    public static class DbConvert
    {
        /// <summary>
        /// 返回指定对象是否为DBNull
        /// </summary>
        /// <param name="value">指定对象</param>
        /// <returns>是否为DBNull</returns>
        public static Boolean IsDBNull(Object value)
        {
            if (value == DBNull.Value)
            {
                return true;
            }

            IConvertible convertible = value as IConvertible;

            return convertible != null && convertible.GetTypeCode() == TypeCode.DBNull;
        }

        /// <summary>
        /// 返回给定的DataTable是否为null
        /// </summary>
        /// <param name="dt">要判断的DataTable</param>
        /// <returns>给定的DataTable是否为null</returns>
        public static Boolean IsDataTableNull(DataTable dt)
        {
            return ((dt == null) || (dt.Rows == null));
        }

        /// <summary>
        /// 返回给定的DataTable是否为null或为空
        /// </summary>
        /// <param name="dt">要判断的DataTable</param>
        /// <returns>给定的DataTable是否为null或为空</returns>
        public static Boolean IsDataTableNullOrEmpty(DataTable dt)
        {
            return ((dt == null) || (dt.Rows == null) || (dt.Rows.Count < 1));
        }

        /// <summary>
        /// 将数据库字段转换为布尔型
        /// </summary>
        /// <param name="obj">数据库字段内容</param>
        /// <returns>布尔型内容</returns>
        public static Boolean ToBoolean(Object obj)
        {
            if (obj is Boolean)
            {
                return (Boolean)obj;
            }
            else
            {
                return Convert.ToBoolean(obj);
            }
        }

        /// <summary>
        /// 将数据库字段转换为字节型
        /// </summary>
        /// <param name="obj">数据库字段内容</param>
        /// <returns>字节型内容</returns>
        public static Char ToChar(Object obj)
        {
            if (obj is Char)
            {
                return (Char)obj;
            }
            else
            {
                return Convert.ToChar(obj);
            }
        }

        /// <summary>
        /// 将数据库字段转换为字节型
        /// </summary>
        /// <param name="obj">数据库字段内容</param>
        /// <returns>字节型内容</returns>
        public static Byte ToByte(Object obj)
        {
            if (obj is Byte)
            {
                return (Byte)obj;
            }
            else
            {
                return Convert.ToByte(obj);
            }
        }

        /// <summary>
        /// 将数据库字段转换为有符号字节型
        /// </summary>
        /// <param name="obj">数据库字段内容</param>
        /// <returns>有符号字节型内容</returns>
        public static SByte ToSByte(Object obj)
        {
            if (obj is SByte)
            {
                return (SByte)obj;
            }
            else
            {
                return Convert.ToSByte(obj);
            }
        }

        /// <summary>
        /// 将数据库字段转换为整型
        /// </summary>
        /// <param name="obj">数据库字段内容</param>
        /// <returns>整型内容</returns>
        public static Int16 ToInt16(Object obj)
        {
            if (obj is Int16)
            {
                return (Int16)obj;
            }
            else
            {
                return Convert.ToInt16(obj);
            }
        }

        /// <summary>
        /// 将数据库字段转换为无符号整型
        /// </summary>
        /// <param name="obj">数据库字段内容</param>
        /// <returns>无符号整型内容</returns>
        public static UInt16 ToUInt16(Object obj)
        {
            if (obj is UInt16)
            {
                return (UInt16)obj;
            }
            else
            {
                return Convert.ToUInt16(obj);
            }
        }

        /// <summary>
        /// 将数据库字段转换为整型
        /// </summary>
        /// <param name="obj">数据库字段内容</param>
        /// <returns>整型内容</returns>
        public static Int32 ToInt32(Object obj)
        {
            if (obj is Int32)
            {
                return (Int32)obj;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 将数据库字段转换为无符号整型
        /// </summary>
        /// <param name="obj">数据库字段内容</param>
        /// <returns>无符号整型内容</returns>
        public static UInt32 ToUInt32(Object obj)
        {
            if (obj is UInt32)
            {
                return (UInt32)obj;
            }
            else
            {
                return Convert.ToUInt32(obj);
            }
        }

        /// <summary>
        /// 将数据库字段转换为整型
        /// </summary>
        /// <param name="obj">数据库字段内容</param>
        /// <returns>整型内容</returns>
        public static Int64 ToInt64(Object obj)
        {
            if (obj is Int64)
            {
                return (Int64)obj;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }

        /// <summary>
        /// 将数据库字段转换为无符号整型
        /// </summary>
        /// <param name="obj">数据库字段内容</param>
        /// <returns>无符号整型内容</returns>
        public static UInt64 ToUInt64(Object obj)
        {
            if (obj is UInt64)
            {
                return (UInt64)obj;
            }
            else
            {
                return Convert.ToUInt64(obj);
            }
        }

        /// <summary>
        /// 将数据库字段转换为单精度浮点型
        /// </summary>
        /// <param name="obj">数据库字段内容</param>
        /// <returns>单精度浮点型内容</returns>
        public static Single ToSingle(Object obj)
        {
            if (obj is Single)
            {
                return (Single)obj;
            }
            else
            {
                return Convert.ToSingle(obj);
            }
        }

        /// <summary>
        /// 将数据库字段转换为双精度浮点型
        /// </summary>
        /// <param name="obj">数据库字段内容</param>
        /// <returns>双精度浮点型内容</returns>
        public static Double ToDouble(Object obj)
        {
            if (obj is Double)
            {
                return (Double)obj;
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }

        /// <summary>
        /// 将数据库字段转换为十进制数
        /// </summary>
        /// <param name="obj">数据库字段内容</param>
        /// <returns>十进制数内容</returns>
        public static Decimal ToDecimal(Object obj)
        {
            if (obj is Decimal)
            {
                return (Decimal)obj;
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }

        /// <summary>
        /// 将数据库字段转换为日期
        /// </summary>
        /// <param name="obj">数据库字段内容</param>
        /// <returns>日期内容</returns>
        public static DateTime ToDateTime(Object obj)
        {
            if (obj is DateTime)
            {
                return (DateTime)obj;
            }
            else
            {
                DateTime dt = DateTime.Parse(obj.ToString());
                return dt;
            }
        }

        /// <summary>
        /// 将数据库字段转换为日期
        /// </summary>
        /// <param name="obj">数据库字段内容</param>
        /// <returns>日期内容</returns>
        public static DateTimeOffset ToDateTimeOffset(Object obj)
        {
            if (obj is DateTimeOffset)
            {
                return (DateTimeOffset)obj;
            }
            else
            {
                DateTimeOffset dt = DateTimeOffset.Parse(obj.ToString());
                return dt;
            }
        }

        /// <summary>
        /// 将数据库字段转换为全局唯一标识符
        /// </summary>
        /// <param name="obj">数据库字段内容</param>
        /// <returns>全局唯一标识符内容</returns>
        public static Guid ToGuid(Object obj)
        {
            if (obj is Guid)
            {
                return (Guid)obj;
            }
            else
            {
                return new Guid(obj.ToString());
            }
        }

        /// <summary>
        /// 将数据库字段转换为字符串
        /// </summary>
        /// <param name="obj">数据库字段内容</param>
        /// <returns>字符串内容</returns>
        public static String ToString(Object obj)
        {
            String result = obj as String;

            if (!String.IsNullOrEmpty(result))
            {
                return result;
            }
            else
            {
                return obj.ToString();
            }
        }

        /// <summary>
        /// 将数据库字段按数据类型转换
        /// </summary>
        /// <param name="obj">原始数据库字段</param>
        /// <param name="dataType">数据类型</param>
        /// <returns>转换后的内容</returns>
        public static Object ToValue(Object obj, DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Binary:
                    return (Byte[])obj;
                case DataType.Boolean:
                    return DbConvert.ToBoolean(obj);
                case DataType.Byte:
                    return DbConvert.ToByte(obj);
                case DataType.SByte:
                    return DbConvert.ToSByte(obj);
                case DataType.Int16:
                    return DbConvert.ToInt16(obj);
                case DataType.Int32:
                    return DbConvert.ToInt32(obj);
                case DataType.Int64:
                    return DbConvert.ToInt64(obj);
                case DataType.UInt16:
                    return DbConvert.ToUInt16(obj);
                case DataType.UInt32:
                    return DbConvert.ToUInt32(obj);
                case DataType.UInt64:
                    return DbConvert.ToUInt64(obj);
                case DataType.Single:
                    return DbConvert.ToSingle(obj);
                case DataType.Double:
                    return DbConvert.ToDouble(obj);
                case DataType.Currency:
                case DataType.Decimal:
                case DataType.VarNumeric:
                    return DbConvert.ToDecimal(obj);
                case DataType.Object:
                    return obj;
                case DataType.Date:
                case DataType.DateTime:
                case DataType.DateTime2:
                case DataType.Time:
                    return DbConvert.ToDateTime(obj);
                case DataType.DateTimeOffset:
                    return DbConvert.ToDateTimeOffset(obj);
                case DataType.Guid:
                    return DbConvert.ToGuid(obj);
                case DataType.AnsiString:
                case DataType.AnsiStringFixedLength:
                case DataType.String:
                case DataType.StringFixedLength:
                case DataType.Xml:
                    return DbConvert.ToString(obj);
                default:
                    return obj;
            }
        }

        /// <summary>
        /// 获取指定数据类型的默认值
        /// </summary>
        /// <param name="dataType">数据类型</param>
        /// <returns>指定数据类型的默认值</returns>
        public static Object GetDefaultValue(DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Binary:
                    return default(Byte[]);
                case DataType.Boolean:
                    return default(Boolean);
                case DataType.Byte:
                    return default(Byte);
                case DataType.SByte:
                    return default(SByte);
                case DataType.Int16:
                    return default(Int16);
                case DataType.Int32:
                    return default(Int32);
                case DataType.Int64:
                    return default(Int64);
                case DataType.UInt16:
                    return default(UInt16);
                case DataType.UInt32:
                    return default(UInt32);
                case DataType.UInt64:
                    return default(UInt64);
                case DataType.Single:
                    return default(Single);
                case DataType.Double:
                    return default(Double);
                case DataType.Currency:
                case DataType.Decimal:
                case DataType.VarNumeric:
                    return default(Decimal);
                case DataType.Object:
                    return default(Object);
                case DataType.Date:
                case DataType.DateTime:
                case DataType.DateTime2:
                case DataType.Time:
                    return default(DateTime);
                case DataType.DateTimeOffset:
                    return default(DateTimeOffset);
                case DataType.Guid:
                    return default(Guid);
                case DataType.AnsiString:
                case DataType.AnsiStringFixedLength:
                case DataType.String:
                case DataType.StringFixedLength:
                case DataType.Xml:
                    return default(String);
                default:
                    return default(Object);
            }
        }
    }
}