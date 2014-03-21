using System;

namespace DotMaysWind.Data.Command.Pager
{
    /// <summary>
    /// MySQL选择语句分页器
    /// </summary>
    internal static class MySQLSelectPager
    {
        /// <summary>
        /// 获取选择语句内容
        /// </summary>
        /// <param name="baseCommand">源选择语句</param>
        /// <param name="realPageIndex">实际页索引</param>
        /// <param name="realPageCount">实际页数量</param>
        /// <param name="orderReverse">是否反转排序</param>
        /// <returns>选择语句内容</returns>
        internal static String InternalGetPagerCommand(SelectCommand baseCommand, Int32 realPageIndex, Int32 realPageCount, Boolean orderReverse)
        {
            SqlCommandBuilder sb = new SqlCommandBuilder(baseCommand.Database);
            sb.AppendSelectPrefix();
            
            sb.AppendSelectDistinct(baseCommand.UseDistinct).AppendAllColumnNames(baseCommand.QueryFields);
            sb.AppendSelectFromAndJoins(baseCommand.TableName, baseCommand.IsFromSql, baseCommand.SqlJoins);

            sb.AppendWhere(baseCommand.SqlWhere);
            sb.AppendSelectGroupBys(baseCommand.GroupByColumns);
            sb.AppendHaving(baseCommand.SqlHaving);
            sb.AppendSelectOrderBys(baseCommand.SqlOrders, orderReverse);

            if (baseCommand.PageSize > 0 && baseCommand.PageIndex == 1)
            {
                sb.AppendSelectLimit(baseCommand.PageSize);
            }
            else if (baseCommand.PageSize > 0 && baseCommand.PageIndex > 1)
            {
                sb.AppendSelectLimit(baseCommand.PageSize * (realPageIndex - 1), baseCommand.PageSize);
            }

            return sb.ToString();
        }
    }
}