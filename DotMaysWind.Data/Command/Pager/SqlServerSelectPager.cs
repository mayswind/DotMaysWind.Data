using System;

using DotMaysWind.Data.Command.Condition;

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
        /// <param name="orderReverse">是否反转排序</param>
        /// <returns>选择语句内容</returns>
        internal static String InternalGetPagerCommand(SelectCommand baseCommand, Boolean orderReverse)
        {
            SqlCommandBuilder sb = new SqlCommandBuilder(baseCommand.Database);
            sb.AppendSelectPrefix();

            if (baseCommand.PageSize > 0 && baseCommand.RecordStart > 0)//分页模式
            {
                /*
                    SELECT * FROM
                    (
                        SELECT TOP 30 *, ROW_NUMBER() OVER(ORDER BY ID ASC) AS RN
                        FROM TABLE_NAME
                    ) AS T
                    WHERE RN > 20
                */

                sb.AppendSelectDistinct(baseCommand.UseDistinct).AppendAllColumnNames(baseCommand.QueryFields);

                SqlCommandBuilder innerBuilder = new SqlCommandBuilder(baseCommand.Database);
                innerBuilder.AppendSelectOrderBys(baseCommand.SqlOrders, false);

                SelectCommand innerCommand = new SelectCommand(baseCommand.Database, baseCommand.TableName);
                innerCommand.InternalQuerys(baseCommand.QueryFields.ToArray());
                innerCommand.InternalQuerys(SqlQueryField.InternalCreateFromFunction(baseCommand, "ROW_NUMBER() OVER( " + innerBuilder.ToString() + ")", "RN"));
                innerCommand.PageSize = baseCommand.RecordStart + baseCommand.PageSize;
                innerCommand.SqlJoins = baseCommand.SqlJoins;
                innerCommand.SqlWhere = baseCommand.SqlWhere;
                innerCommand.GroupByColumns = baseCommand.GroupByColumns;
                innerCommand.SqlHaving = baseCommand.SqlHaving;

                sb.AppendSelectFrom(innerCommand.GetCommandText("T"), true);
                sb.AppendWhere(SqlCondition.InternalCreateAction(baseCommand, "RN", SqlOperator.GreaterThan, baseCommand.RecordStart.ToString()));
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
    }
}