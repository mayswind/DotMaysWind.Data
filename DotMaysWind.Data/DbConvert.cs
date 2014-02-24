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
        public static DateTime? ToNullableDateTime(Object obj)
        {
            if (obj == null || Convert.IsDBNull(obj))
            {
                return null;
            }
            else if (obj is DateTime)
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
        /// 将数据库字段转换为字符串
        /// </summary>
        /// <param name="obj">数据库字段</param>
        /// <returns>字符串</returns>
        public static String ToString(Object obj)
        {
            if (obj != null && obj != DBNull.Value)
            {
                return obj.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}