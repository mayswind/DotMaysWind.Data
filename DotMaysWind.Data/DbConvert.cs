using System;
using System.Data;

namespace DotMaysWind.Data
{
    /// <summary>
    /// 数据库转换器
    /// </summary>
    public static class DbConvert
    {
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
        /// <param name="obj">数据库字段</param>
        /// <returns>布尔型</returns>
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
        /// <param name="obj">数据库字段</param>
        /// <returns>字节型</returns>
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
        /// <param name="obj">数据库字段</param>
        /// <returns>字节型</returns>
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
        /// <param name="obj">数据库字段</param>
        /// <returns>有符号字节型</returns>
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
        /// <param name="obj">数据库字段</param>
        /// <returns>整型</returns>
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
        /// <param name="obj">数据库字段</param>
        /// <returns>无符号整型</returns>
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
        /// <param name="obj">数据库字段</param>
        /// <returns>整型</returns>
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
        /// <param name="obj">数据库字段</param>
        /// <returns>无符号整型</returns>
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
        /// <param name="obj">数据库字段</param>
        /// <returns>整型</returns>
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
        /// <param name="obj">数据库字段</param>
        /// <returns>无符号整型</returns>
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
        /// <param name="obj">数据库字段</param>
        /// <returns>单精度浮点型</returns>
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
        /// <param name="obj">数据库字段</param>
        /// <returns>双精度浮点型</returns>
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
        /// <param name="obj">数据库字段</param>
        /// <returns>十进制数</returns>
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
        /// <param name="obj">数据库字段</param>
        /// <returns>日期</returns>
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
        /// <param name="obj">数据库字段</param>
        /// <returns>日期</returns>
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
        /// 将数据库字段转换为Guid
        /// </summary>
        /// <param name="obj">数据库字段</param>
        /// <returns>Guid</returns>
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
        /// <param name="obj">数据库字段</param>
        /// <returns>字符串</returns>
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
        /// <param name="dbType">数据类型</param>
        /// <returns>转换后的内容</returns>
        public static Object ToValue(Object obj, DbType dbType)
        {
            switch (dbType)
            {
                case DbType.Binary:
                    return (Byte[])obj;
                case DbType.Boolean:
                    return DbConvert.ToBoolean(obj);
                case DbType.Byte:
                    return DbConvert.ToByte(obj);
                case DbType.SByte:
                    return DbConvert.ToSByte(obj);
                case DbType.Int16:
                    return DbConvert.ToInt16(obj);
                case DbType.Int32:
                    return DbConvert.ToInt32(obj);
                case DbType.Int64:
                    return DbConvert.ToInt64(obj);
                case DbType.UInt16:
                    return DbConvert.ToUInt16(obj);
                case DbType.UInt32:
                    return DbConvert.ToUInt32(obj);
                case DbType.UInt64:
                    return DbConvert.ToUInt64(obj);
                case DbType.Single:
                    return DbConvert.ToSingle(obj);
                case DbType.Double:
                    return DbConvert.ToDouble(obj);
                case DbType.Currency:
                case DbType.Decimal:
                case DbType.VarNumeric:
                    return DbConvert.ToDecimal(obj);
                case DbType.Object:
                    return obj;
                case DbType.Date:
                case DbType.DateTime:
                case DbType.DateTime2:
                case DbType.Time:
                    return DbConvert.ToDateTime(obj);
                case DbType.DateTimeOffset:
                    return DbConvert.ToDateTimeOffset(obj);
                case DbType.Guid:
                    return DbConvert.ToGuid(obj);
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                case DbType.StringFixedLength:
                case DbType.Xml:
                    return DbConvert.ToString(obj);
                default:
                    return obj;
            }
        }
    }
}