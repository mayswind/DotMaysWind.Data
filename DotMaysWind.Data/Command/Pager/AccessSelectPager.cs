using System;
using System.Collections.Generic;

using DotMaysWind.Data.Command.Condition;

namespace DotMaysWind.Data.Command.Pager
{
    /// <summary>
    /// Access选择语句分页器
    /// </summary>
    internal static class AccessSelectPager
    {
        /// <summary>
        /// 获取选择语句内容
        /// </summary>
        /// <param name="baseCommand">源选择语句</param>
        /// <param name="recordCount">记录数量</param>
        /// <param name="orderReverse">是否反转排序</param>
        /// <returns>选择语句内容</returns>
        internal static String InternalGetPagerCommand(SelectCommand baseCommand, Int32 recordCount, Boolean orderReverse)
        {
            SqlCommandBuilder sb = new SqlCommandBuilder(baseCommand.Database);
            sb.AppendSelectPrefix();

            if (baseCommand.PageSize > 0 && baseCommand.RecordStart > 0)//分页模式
            {
                Int32 realPageCount = (recordCount + baseCommand.PageSize - 1) / baseCommand.PageSize;
                Int32 realPageIndex = (baseCommand.RecordStart / baseCommand.PageSize) + 1;

                if (realPageIndex >= realPageCount)//最后一页
                {
                    sb.AppendSelectDistinct(baseCommand.UseDistinct).AppendAllColumnNames(baseCommand.QueryFields);

                    SelectCommand innerCommand = new SelectCommand(baseCommand.Database, baseCommand.TableName);
                    innerCommand.QueryFields = baseCommand.QueryFields;
                    innerCommand.PageSize = recordCount - baseCommand.RecordStart;
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
                    /*
                        SELECT * FROM
                        (
                            SELECT TOP 10 * FROM
                            (
                                SELECT TOP 30 * 
                                FROM TABLE_NAME
                                ORDER BY ID ASC
                            ) AS T1
                            ORDER BY ID DESC
                        ) AS T2
                        ORDER BY ID ASC
                    */

                    sb.AppendSelectDistinct(baseCommand.UseDistinct).AppendAllColumnNames(baseCommand.QueryFields);

                    SelectCommand innestCommand = new SelectCommand(baseCommand.Database, baseCommand.TableName);
                    innestCommand.QueryFields = baseCommand.QueryFields;
                    innestCommand.PageSize = baseCommand.RecordStart + baseCommand.PageSize;
                    innestCommand.SqlJoins = baseCommand.SqlJoins;
                    innestCommand.SqlWhere = baseCommand.SqlWhere;
                    innestCommand.GroupByColumns = baseCommand.GroupByColumns;
                    innestCommand.SqlHaving = baseCommand.SqlHaving;
                    innestCommand.SqlOrders = baseCommand.SqlOrders;

                    SelectCommand innerCommand = new SelectCommand(baseCommand.Database, innestCommand, "T1");
                    innerCommand.QueryFields = baseCommand.QueryFields;
                    innerCommand.PageSize = baseCommand.PageSize;
                    innerCommand.SqlOrders = baseCommand.SqlOrders;

                    sb.AppendSelectFrom(innerCommand.GetSqlCommand("T2", !orderReverse), true);
                    sb.AppendSelectOrderBys(baseCommand.SqlOrders, orderReverse);
                }
                else//后1/2部分页
                {
                    sb.AppendSelectTop(baseCommand.PageSize);
                    sb.AppendSelectDistinct(baseCommand.UseDistinct).AppendAllColumnNames(baseCommand.QueryFields);

                    SelectCommand innerCommand = new SelectCommand(baseCommand.Database, baseCommand.TableName);
                    innerCommand.QueryFields = baseCommand.QueryFields;
                    innerCommand.PageSize = recordCount - baseCommand.RecordStart;
                    innerCommand.SqlJoins = baseCommand.SqlJoins;
                    innerCommand.SqlWhere = baseCommand.SqlWhere;
                    innerCommand.GroupByColumns = baseCommand.GroupByColumns;
                    innerCommand.SqlHaving = baseCommand.SqlHaving;
                    innerCommand.SqlOrders = baseCommand.SqlOrders;

                    sb.AppendSelectFrom(innerCommand.GetSqlCommand("T", !orderReverse), true);
                    sb.AppendSelectOrderBys(baseCommand.SqlOrders, orderReverse);
                }
            }
            else//正常模式
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

            return sb.ToString();
        }

        /// <summary>
        /// 获取选择空内容语句
        /// </summary>
        /// <param name="baseCommand">源选择语句</param>
        /// <param name="orderReverse">是否反转排序</param>
        /// <returns>选择空内容语句</returns>
        internal static String InternalGetSelectNoneCommand(SelectCommand baseCommand, Boolean orderReverse)
        {
            SqlCommandBuilder sb = new SqlCommandBuilder(baseCommand.Database);

            sb.AppendSelectPrefix();
            sb.AppendSelectDistinct(baseCommand.UseDistinct).AppendAllColumnNames(baseCommand.QueryFields);
            sb.AppendSelectFromAndJoins(baseCommand.TableName, baseCommand.IsFromSql, baseCommand.SqlJoins);

            if (baseCommand.SqlWhere != null)
            {
                sb.AppendWhere(SqlCondition.And(baseCommand, SqlCondition.False(baseCommand), baseCommand.SqlWhere));
            }
            else
            {
                sb.AppendWhere(SqlCondition.False(baseCommand));
            }
            
            sb.AppendSelectGroupBys(baseCommand.GroupByColumns);
            sb.AppendHaving(baseCommand.SqlHaving);
            sb.AppendSelectOrderBys(baseCommand.SqlOrders, orderReverse);

            return sb.ToString();
        }

        /// <summary>
        /// 获取选择数量语句
        /// </summary>
        /// <param name="baseCommand">源选择语句</param>
        /// <returns>选择数量语句</returns>
        internal static String InternalGetCountCommand(SelectCommand baseCommand)
        {
            SqlCommandBuilder sb = new SqlCommandBuilder(baseCommand.Database);

            List<SqlQueryField> queryFields = new List<SqlQueryField>();
            queryFields.Add(SqlQueryField.InternalCreateFromAggregateFunction(baseCommand, SqlAggregateFunction.Count));

            sb.AppendSelectPrefix();
            sb.AppendSelectDistinct(baseCommand.UseDistinct).AppendAllColumnNames(queryFields);
            sb.AppendSelectFromAndJoins(baseCommand.TableName, baseCommand.IsFromSql, baseCommand.SqlJoins);

            sb.AppendWhere(baseCommand.SqlWhere);
            sb.AppendSelectGroupBys(baseCommand.GroupByColumns);
            sb.AppendHaving(baseCommand.SqlHaving);

            return sb.ToString();
        }
    }
}