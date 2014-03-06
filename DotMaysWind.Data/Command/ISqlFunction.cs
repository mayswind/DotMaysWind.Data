using System;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql查询函数接口
    /// </summary>
    public interface ISqlFunction
    {
        /// <summary>
        /// 获取是否需要提交参数
        /// </summary>
        Boolean HasParameters { get; }

        /// <summary>
        /// 获取查询函数参数集合
        /// </summary>
        /// <returns>查询函数参数集合</returns>
        SqlParameter[] GetAllParameters();

        /// <summary>
        /// 获取函数拼接后字符串
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns>函数拼接后字符串</returns>
        String GetSqlFunction(DatabaseType dbType);
    }
}