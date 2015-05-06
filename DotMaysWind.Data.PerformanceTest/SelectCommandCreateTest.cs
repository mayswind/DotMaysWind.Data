using System;
using System.Data;
using System.Data.Common;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Linq;

namespace DotMaysWind.Data.PerformanceTest
{
    /// <summary>
    /// 选择语句创建
    /// </summary>
    internal static class SelectCommandCreateTest
    {
        #region 字段
        private static TestEntityRepository _repository;
        #endregion

        #region 构造方法
        static SelectCommandCreateTest()
        {
            _repository = new TestEntityRepository();
        }
        #endregion

        #region 测试方法
        internal static void BaseCreateSelectCommand()
        {
            String sql = "SELECT TestColumn1,TestColumn2,TestColumn5,TestColumn8,TestColumn3 AS TTTT,MAX(TestColumn4) AS MMMM FROM TestTable INNER JOIN AnotherTable ON TestTable.TestColumn2 = AnotherTable.TestColumn2 WHERE ((TestColumn2 >= @PN_TestColumn2_GreaterThanOrEqual) OR ((TestColumn5 > @PN_TestColumn5_GreaterThan) AND (TestColumn5 < @PN_TestColumn5_LessThan))) GROUP BY TestColumn3 ORDER BY TestColumn6 ASC";

            DbCommand dbCommand = DbHelper.FakeDb.CreateDbCommand();
            dbCommand.CommandText = sql;
            dbCommand.Parameters.Add(DbHelper.InternalCreateDbParameter("TestColumn2", "@PN_TestColumn2_GreaterThanOrEqual", DbType.String, 123));
            dbCommand.Parameters.Add(DbHelper.InternalCreateDbParameter("TestColumn5", "@PN_TestColumn5_GreaterThan", DbType.Int32, 1));
            dbCommand.Parameters.Add(DbHelper.InternalCreateDbParameter("TestColumn5", "@PN_TestColumn5_LessThan", DbType.Double, 10));
        }

        internal static void DatabaseNormalCreateSelectCommand()
        {
            SelectCommand command = DbHelper.FakeDb.CreateSelectCommand(_repository.TableName)
                .Querys("TestColumn1", "TestColumn2", "TestColumn5", "TestColumn8")
                .Query("TestColumn3", "TTTT")
                .Query(SqlAggregateFunction.Max, "TestColumn4", "MMMM")
                .Where(c => c.GreaterThanOrEqual("TestColumn2", 123) | (c.GreaterThan("TestColumn5", 1) & c.LessThan("TestColumn5", 10)))
                .GroupBy("TestColumn3")
                .InnerJoin("TestColumn2", "TestTable", "TestColumn2")
                .OrderBy("TestColumn6", SqlOrderType.Asc);

            DbCommand dbCommand = command.ToDbCommand();
        }

        internal static void DatabaseLinqCreateSelectCommand()
        {
            SelectCommand command = DbHelper.FakeDb.CreateSelectCommand(_repository.TableName)
                .Querys<TestEntity>(c => new { c.Test1, c.Test2, c.Test5, c.Test8 })
                .Query<TestEntity>(c => c.Test3, "TTTT")
                .Query<TestEntity>(c => c.Test4, SqlAggregateFunction.Max, "MMMM")
                .Where<TestEntity>(c => c.Test2 >= 123 || (c.Test5 > 1 && c.Test5 < 10))
                .GroupBy<TestEntity>(c => c.Test3)
                .InnerJoin<TestEntity, TestEntity>(c => c.Test2, d => d.Test2)
                .OrderBy<TestEntity>(c => c.Test6, SqlOrderType.Asc);

            DbCommand dbCommand = command.ToDbCommand();
        }

        internal static void RepositoryLinqCreateSelectCommand()
        {
            DbCommand dbCommand = _repository.SelectTest();
        }
        #endregion
    }
}