using System;

namespace DotMaysWind.Data.Command.Helper
{
    /// <summary>
    /// Sql Server选择语句分页辅助类
    /// </summary>
    internal static class SqlServerSelectPagerHelper
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

            if (realPageIndex == 1)//第一页
            {
                if (cmd.PageSize > 0)
                {
                    sb.AppendSelectTop(cmd.PageSize);
                }

                sb.AppendSelectDistinct(cmd.UseDistinct).AppendAllColumnNames(cmd.QueryFields);
                sb.AppendSelectFromAndJoins(cmd.TableName, cmd.IsFromSql, cmd.SqlJoins);

                sb.AppendWhere(cmd.SqlWhere);
                sb.AppendSelectGroupBys(cmd.GroupByColumns);
                sb.AppendHaving(cmd.SqlHaving);
                sb.AppendSelectOrderBys(cmd.SqlOrders, orderReverse);
            }
            else if (realPageIndex == realPageCount)//最后一页
            {
                sb.AppendSelectDistinct(cmd.UseDistinct).AppendAllColumnNames(cmd.QueryFields);

                SelectCommand innerCommand = new SelectCommand(cmd.Database, cmd.TableName);
                innerCommand.QueryFields = cmd.QueryFields;
                innerCommand.PageSize = cmd.RecordCount - cmd.PageSize * (realPageIndex - 1);
                innerCommand.SqlJoins = cmd.SqlJoins;
                innerCommand.SqlWhere = cmd.SqlWhere;
                innerCommand.GroupByColumns = cmd.GroupByColumns;
                innerCommand.SqlHaving = cmd.SqlHaving;
                innerCommand.SqlOrders = cmd.SqlOrders;

                sb.AppendSelectFrom(innerCommand.GetSqlCommand("T", !orderReverse), true);
                sb.AppendSelectOrderBys(cmd.SqlOrders, orderReverse);
            }
            else if (realPageIndex < (realPageCount / 2 + realPageCount % 2))//前1/2部分页
            {
                sb.AppendSelectDistinct(cmd.UseDistinct).AppendAllColumnNames(cmd.QueryFields);

                SelectCommand innestCommand = new SelectCommand(cmd.Database, cmd.TableName);
                innestCommand.QueryFields = cmd.QueryFields;
                innestCommand.PageSize = cmd.PageSize * realPageIndex;
                innestCommand.SqlJoins = cmd.SqlJoins;
                innestCommand.SqlWhere = cmd.SqlWhere;
                innestCommand.GroupByColumns = cmd.GroupByColumns;
                innestCommand.SqlHaving = cmd.SqlHaving;
                innestCommand.SqlOrders = cmd.SqlOrders;

                SelectCommand innerCommand = new SelectCommand(cmd.Database, innestCommand, "T");
                innerCommand.QueryFields = cmd.QueryFields;
                innerCommand.PageSize = cmd.PageSize;
                innerCommand.SqlOrders = cmd.SqlOrders;

                sb.AppendSelectFrom(innerCommand.GetSqlCommand("T", !orderReverse), true);
                sb.AppendSelectOrderBys(cmd.SqlOrders, orderReverse);
            }
            else//后1/2部分页
            {
                sb.AppendSelectTop(cmd.PageSize);
                sb.AppendSelectDistinct(cmd.UseDistinct).AppendAllColumnNames(cmd.QueryFields);

                SelectCommand innerCommand = new SelectCommand(cmd.Database, cmd.TableName);
                innerCommand.QueryFields = cmd.QueryFields;
                innerCommand.PageSize = cmd.RecordCount - cmd.PageSize * (realPageIndex - 1);
                innerCommand.SqlJoins = cmd.SqlJoins;
                innerCommand.SqlWhere = cmd.SqlWhere;
                innerCommand.GroupByColumns = cmd.GroupByColumns;
                innerCommand.SqlHaving = cmd.SqlHaving;
                innerCommand.SqlOrders = cmd.SqlOrders;

                sb.AppendSelectFrom(innerCommand.GetSqlCommand("T", !orderReverse), true);
                sb.AppendSelectOrderBys(cmd.SqlOrders, orderReverse);
            }

            return sb.ToString();
        }
    }
}