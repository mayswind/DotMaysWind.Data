using System;

namespace DotMaysWind.Data.Command.Pager
{
    /// <summary>
    /// Sql Server选择语句分页器
    /// </summary>
    internal static class SqlServerSelectPager
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

            if (realPageIndex == 1)//第一页
            {
                if (baseCommand.PageSize > 0)
                {
                    sb.AppendSelectTop(baseCommand.PageSize);
                }

                sb.AppendSelectDistinct(baseCommand.UseDistinct).AppendAllColumnNames(baseCommand.QueryFields);
                sb.AppendSelectFromAndJoins(baseCommand.TableName, baseCommand.IsFromSql, baseCommand.SqlJoins);

                sb.AppendWhere(baseCommand.SqlWhere);
                sb.AppendSelectGroupBys(baseCommand.GroupByColumns);
                sb.AppendHaving(baseCommand.SqlHaving);
                sb.AppendSelectOrderBys(baseCommand.SqlOrders, orderReverse);
            }
            else if (realPageIndex == realPageCount)//最后一页
            {
                sb.AppendSelectDistinct(baseCommand.UseDistinct).AppendAllColumnNames(baseCommand.QueryFields);

                SelectCommand innerCommand = new SelectCommand(baseCommand.Database, baseCommand.TableName);
                innerCommand.QueryFields = baseCommand.QueryFields;
                innerCommand.PageSize = baseCommand.RecordCount - baseCommand.PageSize * (realPageIndex - 1);
                innerCommand.SqlJoins = baseCommand.SqlJoins;
                innerCommand.SqlWhere = baseCommand.SqlWhere;
                innerCommand.GroupByColumns = baseCommand.GroupByColumns;
                innerCommand.SqlHaving = baseCommand.SqlHaving;
                innerCommand.SqlOrders = baseCommand.SqlOrders;

                sb.AppendSelectFrom(innerCommand.GetSqlCommand("T", !orderReverse), true);
                sb.AppendSelectOrderBys(baseCommand.SqlOrders, orderReverse);
            }
            else if (realPageIndex < (realPageCount / 2 + realPageCount % 2))//前1/2部分页
            {
                sb.AppendSelectDistinct(baseCommand.UseDistinct).AppendAllColumnNames(baseCommand.QueryFields);

                SelectCommand innestCommand = new SelectCommand(baseCommand.Database, baseCommand.TableName);
                innestCommand.QueryFields = baseCommand.QueryFields;
                innestCommand.PageSize = baseCommand.PageSize * realPageIndex;
                innestCommand.SqlJoins = baseCommand.SqlJoins;
                innestCommand.SqlWhere = baseCommand.SqlWhere;
                innestCommand.GroupByColumns = baseCommand.GroupByColumns;
                innestCommand.SqlHaving = baseCommand.SqlHaving;
                innestCommand.SqlOrders = baseCommand.SqlOrders;

                SelectCommand innerCommand = new SelectCommand(baseCommand.Database, innestCommand, "T");
                innerCommand.QueryFields = baseCommand.QueryFields;
                innerCommand.PageSize = baseCommand.PageSize;
                innerCommand.SqlOrders = baseCommand.SqlOrders;

                sb.AppendSelectFrom(innerCommand.GetSqlCommand("T", !orderReverse), true);
                sb.AppendSelectOrderBys(baseCommand.SqlOrders, orderReverse);
            }
            else//后1/2部分页
            {
                sb.AppendSelectTop(baseCommand.PageSize);
                sb.AppendSelectDistinct(baseCommand.UseDistinct).AppendAllColumnNames(baseCommand.QueryFields);

                SelectCommand innerCommand = new SelectCommand(baseCommand.Database, baseCommand.TableName);
                innerCommand.QueryFields = baseCommand.QueryFields;
                innerCommand.PageSize = baseCommand.RecordCount - baseCommand.PageSize * (realPageIndex - 1);
                innerCommand.SqlJoins = baseCommand.SqlJoins;
                innerCommand.SqlWhere = baseCommand.SqlWhere;
                innerCommand.GroupByColumns = baseCommand.GroupByColumns;
                innerCommand.SqlHaving = baseCommand.SqlHaving;
                innerCommand.SqlOrders = baseCommand.SqlOrders;

                sb.AppendSelectFrom(innerCommand.GetSqlCommand("T", !orderReverse), true);
                sb.AppendSelectOrderBys(baseCommand.SqlOrders, orderReverse);
            }

            return sb.ToString();
        }
    }
}