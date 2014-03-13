using System;

using DotMaysWind.Data.Command.Condition;

namespace DotMaysWind.Data.Command.Helper
{
    /// <summary>
    /// Oracle选择语句分页辅助类
    /// </summary>
    internal static class OracleSelectPagerHelper
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

            if (cmd.PageSize == 0)//正常模式
            {
                sb.AppendSelectFromAndJoins(cmd.TableName, cmd.IsFromSql, cmd.SqlJoins);

                sb.AppendWhere(cmd.SqlWhere);
                sb.AppendSelectGroupBys(cmd.GroupByColumns);
                sb.AppendHaving(cmd.SqlHaving);
                sb.AppendSelectOrderBys(cmd.SqlOrders, orderReverse);
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
                SelectCommand innestCommand = new SelectCommand(cmd.Database, cmd.TableName);
                innestCommand.QueryFields = cmd.QueryFields;
                innestCommand.SqlJoins = cmd.SqlJoins;
                innestCommand.SqlWhere = cmd.SqlWhere;
                innestCommand.GroupByColumns = cmd.GroupByColumns;
                innestCommand.SqlHaving = cmd.SqlHaving;
                innestCommand.SqlOrders = cmd.SqlOrders;
                
                SelectCommand innerCommand = new SelectCommand(cmd.Database, innestCommand, "");
                innerCommand.SqlWhere = SqlCondition.InternalCreate(SqlParameter.CreateCustomAction("ROWNUM", (realPageIndex * cmd.PageSize).ToString()), SqlOperator.LessThanOrEqual);
                innerCommand.InternalQuerys(cmd.QueryFields.ToArray());
                innerCommand.InternalQuerys(SqlQueryField.InternalCreateFromFunction("ROWNUM", "RN"));

                sb.AppendSelectFrom(innerCommand.GetSqlCommand(), true);
                sb.AppendWhere(SqlCondition.InternalCreate(SqlParameter.CreateCustomAction("RN", ((realPageIndex - 1) * cmd.PageSize + 1).ToString()), SqlOperator.GreaterThanOrEqual));
            }

            return sb.ToString();
        }
    }
}