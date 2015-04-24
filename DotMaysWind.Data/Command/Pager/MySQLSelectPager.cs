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
        /// <param name="orderReverse">是否反转排序</param>
        /// <returns>选择语句内容</returns>
        internal static String InternalGetPagerCommand(SelectCommand baseCommand, Boolean orderReverse)
        {
            SqlCommandBuilder sb = new SqlCommandBuilder(baseCommand.Database);
            sb.AppendSelectPrefix();

            sb.AppendSelectDistinct(baseCommand.UseDistinct).AppendAllColumnNames(baseCommand.UseDistinct, baseCommand.InternalGetQueryFieldList());
            sb.AppendSelectFromAndJoins(baseCommand.TableName, baseCommand.IsFromSql, baseCommand.InternalGetJoinList());

            sb.AppendWhere(baseCommand.WhereCondition);
            sb.AppendSelectGroupBys(baseCommand.InternalGetGroupByFieldList());
            sb.AppendHaving(baseCommand.InternalGetHavingCondition());
            sb.AppendSelectOrderBys(baseCommand.InternalGetOrderList(), orderReverse);

            if (baseCommand.PageSize > 0 && baseCommand.RecordStart <= 0)
            {
                sb.AppendSelectLimit(baseCommand.PageSize);
            }
            else if (baseCommand.PageSize > 0 && baseCommand.RecordStart > 0)
            {
                sb.AppendSelectLimit(baseCommand.RecordStart, baseCommand.PageSize);
            }

            return sb.ToString();
        }
    }
}