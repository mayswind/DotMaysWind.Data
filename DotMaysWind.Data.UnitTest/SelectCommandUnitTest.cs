using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Command.Condition;
using DotMaysWind.Data.Linq;

namespace DotMaysWind.Data.UnitTest
{
    /// <summary>
    /// 选择语句单元测试
    /// </summary>
    [TestClass]
    public class SelectCommandUnitTest
    {
        [TestMethod]
        public void LinqSelectTest()
        {
            IDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient");
            TestEntityDataProvider provider = new TestEntityDataProvider(fakeDb);

            SelectCommand expectedCommand = fakeDb.CreateSelectCommand(provider.TableName)
                .Querys("TestColumn1", "TestColumn2", "TestColumn5", "TestColumn8")
                .Query("TestColumn3", "TTTT")
                .Query(SqlAggregateFunction.Max, "TestColumn4", "MMMM")
                .Where(c => c.GreaterThanOrEqual("TestColumn2", 123) | (c.GreaterThan("TestColumn4", DateTime.Now) & c.LessThan("TestColumn7", DateTime.Now.AddDays(7))))
                .GroupBy("TestColumn3")
                .InnerJoin("TestColumn2", "AnotherTable", "TestColumn2")
                .OrderBy("TestColumn6", SqlOrderType.Asc);

            SelectCommand actualCommand = fakeDb.CreateSelectCommand(provider.TableName)
                .Querys<TestEntity>(c => new { c.Test1, c.Test2, c.Test5, c.Test8 })
                .Query<TestEntity>(c => c.Test3, "TTTT")
                .Query<TestEntity>(c => c.Test4, SqlAggregateFunction.Max, "MMMM")
                .Where<TestEntity>(c => c.Test2 >= 123 || (c.Test4 > DateTime.Now && c.Test7 < DateTime.Now.AddDays(7)))
                .GroupBy<TestEntity>(c => c.Test3)
                .InnerJoin<TestEntity, TestEntity>(c => c.Test2, "AnotherTable", d => d.Test2)
                .OrderBy<TestEntity>(c => c.Test6, SqlOrderType.Asc);

            Assert.AreEqual(expectedCommand, actualCommand);
        }
    }
}