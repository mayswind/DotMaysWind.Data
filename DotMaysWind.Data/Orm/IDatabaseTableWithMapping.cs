using System;

namespace DotMaysWind.Data.Orm
{
    /// <summary>
    /// 支持映射的数据库表格接口
    /// </summary>
    internal interface IDatabaseTableWithMapping
    {
        /// <summary>
        /// 获取数据表名
        /// </summary>
        String TableName { get; }

        /// <summary>
        /// 获取实体类型
        /// </summary>
        Type EntityType { get; }

        /// <summary>
        /// 获取实体类属性名称对应的字段特性
        /// </summary>
        /// <param name="propertyName">实体类属性名称</param>
        /// <returns>字段特性</returns>
        DatabaseColumnAtrribute this[String propertyName] { get; }
    }
}