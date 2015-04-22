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

                sb.AppendSelectDistinct(baseCommand.UseDistinct).AppendAllColumnNames(baseCommand.UseDistinct, baseCommand.InternalGetQueryFieldList());

                SqlCommandBuilder innerBuilder = new SqlCommandBuilder(baseCommand.Database);
                innerBuilder.AppendSelectOrderBys(baseCommand.InternalGetOrderList(), false);

                SelectCommand innerCommand = baseCommand.Database.InternalCreateSelectCommand(baseCommand, baseCommand.TableName);
                innerCommand.InternalQuerys(baseCommand.QueryFields);
                innerCommand.InternalQuerys(SqlQueryField.InternalCreateFromFunction(baseCommand, "ROW_NUMBER() OVER( " + innerBuilder.ToString() + ")", "RN"));
                innerCommand.PageSize = baseCommand.RecordStart + baseCommand.PageSize;
                innerCommand.InternalSetJoinList(baseCommand);
                innerCommand.InternalSetWhereCondition(baseCommand);
                innerCommand.InternalSetGroupByColumnList(baseCommand);
                innerCommand.InternalSetHavingCondition(baseCommand);

                sb.AppendSelectFrom(innerCommand.GetCommandText("T"), true);
                sb.AppendWhere(baseCommand.ConditionBuilder.GreaterThanColumn("RN", baseCommand.RecordStart.ToString()));
            }
            else//正常模式
            {
                if (baseCommand.PageSize > 0)
                {
                    sb.AppendSelectTop(baseCommand.PageSize);
                }

                sb.AppendSelectDistinct(baseCommand.UseDistinct).AppendAllColumnNames(baseCommand.UseDistinct, baseCommand.InternalGetQueryFieldList());
                sb.AppendSelectFromAndJoins(baseCommand.TableName, baseCommand.IsFromSql, baseCommand.InternalGetJoinList());

                sb.AppendWhere(baseCommand.WhereCondition);
                sb.AppendSelectGroupBys(baseCommand.InternalGetGroupByColumnList());
                sb.AppendHaving(baseCommand.InternalGetHavingCondition());
                sb.AppendSelectOrderBys(baseCommand.InternalGetOrderList(), orderReverse);
            }

            return sb.ToString();
        }
    }
}