using System;

namespace DotMaysWind.Data.Command.Helper
{
    /// <summary>
    /// MySQL选择语句分页辅助类
    /// </summary>
    internal static class MySQLSelectPagerHelper
    {
        /// <summary>
        /// 获取选择语句内容
        /// </summary>
        /// <param name="cmd">选择语句类</param>
        /// <param name="realPageIndex">实际页索引</param>
        /// <param name="realPageCount">实际页数量</param>
        /// <param name="orderReverse">是否反转排序</param>
        /// <returns>选择语句内容</returns>
        internal static String InternalGetSelectCommand(SelectCommand cmd, Int32 realPageIndex, Int32 realPageCount, Boolean orderReverse)
        {
            SqlCommandBuilder sb = new SqlCommandBuilder(cmd.DatabaseType);
            sb.AppendSelectPrefix();
            
            sb.AppendSelectDistinct(cmd.UseDistinct).AppendAllColumnNames(cmd.QueryFields);
            sb.AppendSelectFromAndJoins(cmd.TableName, cmd.IsFromSql, cmd.SqlJoins);

            sb.AppendWhere(cmd.SqlWhere);
            sb.AppendSelectGroupBys(cmd.GroupByColumns);
            sb.AppendHaving(cmd.SqlHaving);
            sb.AppendSelectOrderBys(cmd.SqlOrders, orderReverse);

            if (cmd.PageSize > 0 && cmd.PageIndex == 1)
            {
                sb.AppendSelectLimit(cmd.PageSize);
            }
            else if (cmd.PageSize > 0 && cmd.PageIndex > 1)
            {
                sb.AppendSelectLimit(cmd.PageSize * (realPageIndex - 1), cmd.PageSize);
            }

            return sb.ToString();
        }
    }
}