using System;
using System.Data.Common;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Command.Condition;
using DotMaysWind.Data.Linq;

namespace DotMaysWind.Data.PerformanceTest
{
    /// <summary>
    /// 选择语句创建
    /// </summary>
    internal static class SelectCommandCreateTest
    {
        #region 字段
        private static Database _fakeDb;
        private static TestEntityDataProvider _provider;
        #endregion

        #region 构造方法
        static SelectCommandCreateTest()
        {
            _fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient");
            _provider = new TestEntityDataProvider(_fakeDb);
        }
        #endregion

        #region 测试方法
        internal static void DatabaseNormalCreateSelectCommand()
        {
            SelectCommand command = _fakeDb.CreateSelectCommand(_provider.TableName)
                .Querys("TestColumn1", "TestColumn2", "TestColumn5", "TestColumn8")
                .Query("TestColumn3", "TTTT")
                .Query(SqlAggregateFunction.Max, "TestColumn4", "MMMM")
                .Where(SqlCondition.GreaterThanOrEqual("TestColumn2", 123) |
                    (SqlCondition.GreaterThan("TestColumn4", DateTime.Now) & SqlCondition.LessThan("TestColumn7", DateTime.Now.AddDays(7))))
                .GroupBy("TestColumn3")
                .InnerJoin("TestColumn2", "AnotherTable", "TestColumn2")
                .OrderBy("TestColumn6", SqlOrderType.Asc);

            DbCommand dbCommand = command.ToDbCommand();
        }

        internal static void DatabaseLinqCreateSelectCommand()
        {
            SelectCommand command = _fakeDb.CreateSelectCommand(_provider.TableName)
                .Querys<TestEntity>(c => new { c.Test1, c.Test2, c.Test5, c.Test8 })
                .Query<TestEntity>(c => c.Test3, "TTTT")
                .Query<TestEntity>(c => c.Test4, SqlAggregateFunction.Max, "MMMM")
                .Where<TestEntity>(c => c.Test2 >= 123 || (c.Test4 > DateTime.Now && c.Test7 < DateTime.Now.AddDays(7)))
                .GroupBy<TestEntity>(c => c.Test3)
                .InnerJoin<TestEntity, TestEntity>(c => c.Test2, "AnotherTable", d => d.Test2)
                .OrderBy<TestEntity>(c => c.Test6, SqlOrderType.Asc);

            DbCommand dbCommand = command.ToDbCommand();
        }

        internal static void ProviderLinqCreateSelectCommand()
        {
            DbCommand dbCommand = _provider.SelectTest();
        }
        #endregion
    }
}