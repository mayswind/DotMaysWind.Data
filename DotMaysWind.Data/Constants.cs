using System;

namespace DotMaysWind.Data
{
    /// <summary>
    /// 常量类
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// 参数名称前缀
        /// </summary>
        internal const String GeneralParameterNamePrefix = "@PN_";

        /// <summary>
        /// 参数名称前缀
        /// </summary>
        internal const String OracleParameterNamePrefix = ":PN_";

        /// <summary>
        /// 添加新参数名称前缀
        /// </summary>
        internal const String InsertNewParameterNamePrefix = "NEW_";

        /// <summary>
        /// 更新旧参数名称前缀
        /// </summary>
        internal const String UpdateOldParameterNamePrefix = "OLD_";
    }
}