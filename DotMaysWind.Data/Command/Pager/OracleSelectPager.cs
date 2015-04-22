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
            sb.AppendSelectDistinct(baseCommand.UseDistinct).AppendAllColumnNames(baseCommand.UseDistinct, baseCommand.InternalGetQueryFieldList());

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

                SelectCommand innestCommand = baseCommand.Database.InternalCreateSelectCommand(baseCommand, baseCommand.TableName);
                innestCommand.InternalSetQueryFieldList(baseCommand);
                innestCommand.InternalSetJoinList(baseCommand);
                innestCommand.InternalSetWhereCondition(baseCommand);
                innestCommand.InternalSetGroupByColumnList(baseCommand);
                innestCommand.InternalSetHavingCondition(baseCommand);
                innestCommand.InternalSetOrderList(baseCommand);

                SelectCommand innerCommand = baseCommand.Database.InternalCreateSelectCommand(baseCommand, innestCommand, "");
                innerCommand.InternalSetWhereCondition(innerCommand.ConditionBuilder.LessThanOrEqualColumn("ROWNUM", (baseCommand.RecordStart + baseCommand.PageSize).ToString()));
                innerCommand.InternalQuerys(baseCommand.QueryFields);
                innerCommand.InternalQuerys(SqlQueryField.InternalCreateFromFunction(baseCommand, "ROWNUM", "RN"));

                sb.AppendSelectFrom(innerCommand.GetCommandText(), true);
                sb.AppendWhere(baseCommand.ConditionBuilder.GreaterThanColumn("RN", baseCommand.RecordStart.ToString()));
            }
            else//正常模式
            {
                sb.AppendSelectFromAndJoins(baseCommand.TableName, baseCommand.IsFromSql, baseCommand.InternalGetJoinList());

                ISqlCondition where = baseCommand.WhereCondition;

                if (baseCommand.PageSize > 0)
                {
                    where = baseCommand.ConditionBuilder.And(where, baseCommand.ConditionBuilder.LessThanOrEqualColumn("ROWNUM", baseCommand.PageSize.ToString()));
                }

                sb.AppendWhere(where);
                sb.AppendSelectGroupBys(baseCommand.InternalGetGroupByColumnList());
                sb.AppendHaving(baseCommand.InternalGetHavingCondition());
                sb.AppendSelectOrderBys(baseCommand.InternalGetOrderList(), orderReverse);
            }

            return sb.ToString();
        }
    }
}