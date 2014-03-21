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
        /// <param name="realPageIndex">实际页索引</param>
        /// <param name="realPageCount">实际页数量</param>
        /// <param name="orderReverse">是否反转排序</param>
        /// <returns>选择语句内容</returns>
        internal static String InternalGetPagerCommand(SelectCommand baseCommand, Int32 realPageIndex, Int32 realPageCount, Boolean orderReverse)
        {
            SqlCommandBuilder sb = new SqlCommandBuilder(baseCommand.Database);
            sb.AppendSelectPrefix();
            sb.AppendSelectDistinct(baseCommand.UseDistinct).AppendAllColumnNames(baseCommand.QueryFields);

            if (baseCommand.PageSize == 0)//正常模式
            {
                sb.AppendSelectFromAndJoins(baseCommand.TableName, baseCommand.IsFromSql, baseCommand.SqlJoins);

                sb.AppendWhere(baseCommand.SqlWhere);
                sb.AppendSelectGroupBys(baseCommand.GroupByColumns);
                sb.AppendHaving(baseCommand.SqlHaving);
                sb.AppendSelectOrderBys(baseCommand.SqlOrders, orderReverse);
            }
            else//分页模式
            {
                /*
                    SELECT * FROM
                    (
                        SELECT A.*, ROWNUM RN
                        FROM (SELECT * FROM TABLE_NAME) A
                        WHERE ROWNUM <= 40
                    )
                    WHERE RN >= 21 
                */
                SelectCommand innestCommand = new SelectCommand(baseCommand.Database, baseCommand.TableName);
                innestCommand.QueryFields = baseCommand.QueryFields;
                innestCommand.SqlJoins = baseCommand.SqlJoins;
                innestCommand.SqlWhere = baseCommand.SqlWhere;
                innestCommand.GroupByColumns = baseCommand.GroupByColumns;
                innestCommand.SqlHaving = baseCommand.SqlHaving;
                innestCommand.SqlOrders = baseCommand.SqlOrders;
                
                SelectCommand innerCommand = new SelectCommand(baseCommand.Database, innestCommand, "");
                innerCommand.SqlWhere = SqlCondition.InternalCreateAction(innerCommand, "ROWNUM", SqlOperator.LessThanOrEqual, (realPageIndex * baseCommand.PageSize).ToString());
                innerCommand.InternalQuerys(baseCommand.QueryFields.ToArray());
                innerCommand.InternalQuerys(SqlQueryField.InternalCreateFromFunction(baseCommand, "ROWNUM", "RN"));

                sb.AppendSelectFrom(innerCommand.GetSqlCommand(), true);
                sb.AppendWhere(SqlCondition.InternalCreateAction(baseCommand, "RN", SqlOperator.GreaterThanOrEqual, ((realPageIndex - 1) * baseCommand.PageSize + 1).ToString()));
            }

            return sb.ToString();
        }
    }
}