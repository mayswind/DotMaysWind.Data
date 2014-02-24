using System;

namespace DotMaysWind.Data.Command.Condition
{
    /// <summary>
    /// Sql查询语句类型
    /// </summary>
    public enum SqlConditionType : byte
    {
        /// <summary>
        /// 基于参数的基础条件
        /// </summary>
        BasicParameterCondition = 0,

        /// <summary>
        /// 基于查询的基础条件
        /// </summary>
        BasicCommandCondition = 1,

        /// <summary>
        /// 基于参数的IN条件
        /// </summary>
        InsideParametersCondition = 2,

        /// <summary>
        /// 基于查询的IN条件
        /// </summary>
        InsideCommandCondition = 3,

        /// <summary>
        /// 条件列表
        /// </summary>
        ConditionList = 4
    }
}