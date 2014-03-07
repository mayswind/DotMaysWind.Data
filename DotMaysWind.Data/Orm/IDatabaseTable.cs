using System;

namespace DotMaysWind.Data.Orm
{
    /// <summary>
    /// 数据库表格接口
    /// </summary>
    public interface IDatabaseTable
    {
        /// <summary>
        /// 获取数据表名
        /// </summary>
        String TableName { get; }
    }
}