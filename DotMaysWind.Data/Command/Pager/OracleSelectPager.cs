using System;

using DotMaysWind.Data.Command.Condition;

namespace DotMaysWind.Data.Command.Pager
{
    /// <summary>
    /// Oracle选择语句分页器
    /// </summary>
    internal static class OracleSelectPager
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
            sb.AppendSelectDistinct(baseCommand.UseDistinct).AppendAllColumnNames(baseCommand.QueryFields);

            if (baseCommand.PageSize > 0 && baseCommand.RecordStart > 0)//分页模式
            {
                /*
                    SELECT * FROM
                    (
                        SELECT *, ROWNUM RN
                        FROM (SELECT * FROM TABLE_NAME)
                        WHERE ROWNUM <= 30
                    )
                    WHERE RN > 20
                */

                SelectCommand innestCommand = new SelectCommand(baseCommand.Database, baseCommand.TableName);
                innestCommand.QueryFields = baseCommand.QueryFields;
                innestCommand.SqlJoins = baseCommand.SqlJoins;
                innestCommand.SqlWhere = baseCommand.SqlWhere;
                innestCommand.GroupByColumns = baseCommand.GroupByColumns;
                innestCommand.SqlHaving = baseCommand.SqlHaving;
                innestCommand.SqlOrders = baseCommand.SqlOrders;

                SelectCommand innerCommand = new SelectCommand(baseCommand.Database, innestCommand, "");
                innerCommand.SqlWhere = SqlCondition.LessThanOrEqualColumn(innerCommand, "ROWNUM", (baseCommand.RecordStart + baseCommand.PageSize).ToString());
                innerCommand.InternalQuerys(baseCommand.QueryFields.ToArray());
                innerCommand.InternalQuerys(SqlQueryField.InternalCreateFromFunction(baseCommand, "ROWNUM", "RN"));

                sb.AppendSelectFrom(innerCommand.GetCommandText(), true);
                sb.AppendWhere(SqlCondition.GreaterThanColumn(baseCommand, "RN", baseCommand.RecordStart.ToString()));
            }
            else//正常模式
            {
                sb.AppendSelectFromAndJoins(baseCommand.TableName, baseCommand.IsFromSql, baseCommand.SqlJoins);

                ISqlCondition where = baseCommand.SqlWhere;

                if (baseCommand.PageSize > 0)
                {
                    where = SqlCondition.And(baseCommand, where, SqlCondition.LessThanOrEqualColumn(baseCommand, "ROWNUM", baseCommand.PageSize.ToString()));
                }

                sb.AppendWhere(where);
                sb.AppendSelectGroupBys(baseCommand.GroupByColumns);
                sb.AppendHaving(baseCommand.SqlHaving);
                sb.AppendSelectOrderBys(baseCommand.SqlOrders, orderReverse);
            }

            return sb.ToString();
        }
    }
}