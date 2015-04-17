using System;

namespace DotMaysWind.Data
{
    /// <summary>
    /// 数据类型
    /// </summary>
    /// <remarks>
    /// 与 System.Data.DataType 完全对应
    /// </remarks>
    public enum DataType : int
    {
        /// <summary>
        /// 非 Unicode 字符串
        /// </summary>
        AnsiString = 0,

        /// <summary>
        /// 二进制数据
        /// </summary>
        Binary = 1,

        /// <summary>
        /// 8 位无符号整型
        /// </summary>
        Byte = 2,

        /// <summary>
        /// 布尔型
        /// </summary>
        Boolean = 3,

        /// <summary>
        /// 货币型
        /// </summary>
        Currency = 4,

        /// <summary>
        /// 日期型
        /// </summary>
        Date = 5,

        /// <summary>
        /// 日期和时间型
        /// </summary>
        DateTime = 6,

        /// <summary>
        /// 数值类型
        /// </summary>
        Decimal = 7,

        /// <summary>
        /// 双精度浮点型
        /// </summary>
        Double = 8,

        /// <summary>
        /// 全局唯一标识符类型
        /// </summary>
        Guid = 9,

        /// <summary>
        /// 16 位有符号整型
        /// </summary>
        Int16 = 10,
        
        /// <summary>
        /// 32 位有符号整型
        /// </summary>
        Int32 = 11,

        /// <summary>
        /// 64 位有符号整型
        /// </summary>
        Int64 = 12,

        /// <summary>
        /// 常规类型，表示任何没有由其他 DataType 值显式表示的引用或值类型
        /// </summary>
        Object = 13,

        /// <summary>
        /// 8 位有符号整型
        /// </summary>
        SByte = 14,

        /// <summary>
        /// 单精度浮点型
        /// </summary>
        Single = 15,

        /// <summary>
        /// Unicode 字符串
        /// </summary>
        String = 16,

        /// <summary>
        /// 时间型
        /// </summary>
        Time = 17,

        /// <summary>
        /// 16 位无符号整型
        /// </summary>
        UInt16 = 18,

        /// <summary>
        /// 32 位无符号整型
        /// </summary>
        UInt32 = 19,

        /// <summary>
        /// 64 位无符号整型
        /// </summary>
        UInt64 = 20,

        /// <summary>
        /// 可变长数值
        /// </summary>
        VarNumeric = 21,

        /// <summary>
        /// 非 Unicode 固定长度字符串
        /// </summary>
        AnsiStringFixedLength = 22,

        /// <summary>
        /// Unicode 固定长度字符串
        /// </summary>
        StringFixedLength = 23,

        /// <summary>
        /// XML 文档
        /// </summary>
        Xml = 25,

        /// <summary>
        /// 日期和时间型
        /// </summary>
        DateTime2 = 26,

        /// <summary>
        /// 显示时区的日期和时间型
        /// </summary>
        DateTimeOffset = 27,
    }
}