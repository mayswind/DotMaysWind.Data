using System;
using System.Data;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Command.Condition;
using DotMaysWind.Data.Linq;
using DotMaysWind.Data.Orm;

namespace DotMaysWind.Data.UnitTest
{
    /// <summary>
    /// 使用Linq的Sql条件单元测试
    /// </summary>
    [TestClass]
    public class ConditionForLinqUnitTest
    {
        #region 基本条件
        [TestMethod]
        public void LinqCreateBasicConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = fakeDb.CreateSelectCommand("");
            SelectCommand actualCommand = fakeDb.CreateSelectCommand("");

            SqlConditionBuilder expectedConditionBuilder = expectedCommand.ConditionBuilder;

            SqlBasicParameterCondition expectedCondition = expectedConditionBuilder.Equal("TestColumn2", 123);
            SqlBasicParameterCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2 == 123) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlBasicParameterCondition expectedCondition2 = expectedConditionBuilder.NotEqual("TestColumn2", 123);
            SqlBasicParameterCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2 != 123) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            SqlBasicParameterCondition expectedCondition3 = expectedConditionBuilder.GreaterThan("TestColumn2", 123);
            SqlBasicParameterCondition actualCondition3 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2 > 123) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);

            SqlBasicParameterCondition expectedCondition4 = expectedConditionBuilder.LessThan("TestColumn2", 123);
            SqlBasicParameterCondition actualCondition4 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2 < 123) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition4, actualCondition4);

            SqlBasicParameterCondition expectedCondition5 = expectedConditionBuilder.GreaterThanOrEqual("TestColumn2", 123);
            SqlBasicParameterCondition actualCondition5 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2 >= 123) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition5, actualCondition5);

            SqlBasicParameterCondition expectedCondition6 = expectedConditionBuilder.LessThanOrEqual("TestColumn2", 123);
            SqlBasicParameterCondition actualCondition6 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2 <= 123) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition6, actualCondition6);

            SqlBasicParameterCondition expectedCondition7 = expectedConditionBuilder.IsNotNull("TestColumn1");
            SqlBasicParameterCondition actualCondition7 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1 != null) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition7, actualCondition7);

            SqlBasicParameterCondition expectedCondition8 = expectedConditionBuilder.IsNull("TestColumn3");
            SqlBasicParameterCondition actualCondition8 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test3 == null) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition8, actualCondition8);

            SqlBasicParameterCondition expectedCondition10 = expectedConditionBuilder.EqualColumn("TestColumn2", "TestColumn3");
            SqlBasicParameterCondition actualCondition10 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2 == c.Test3) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition10, actualCondition10);
        }
        #endregion

        #region 判空条件
        [TestMethod]
        public void LinqCreateIsNullConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = fakeDb.CreateSelectCommand("");
            SelectCommand actualCommand = fakeDb.CreateSelectCommand("");

            SqlConditionBuilder expectedConditionBuilder = expectedCommand.ConditionBuilder;

            SqlBasicParameterCondition expectedCondition = expectedConditionBuilder.IsNull("TestColumn1");
            SqlBasicParameterCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.IsNull()) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlBasicParameterCondition expectedCondition2 = expectedConditionBuilder.IsNull("TestColumn2");
            SqlBasicParameterCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2.IsNull()) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            SqlBasicParameterCondition expectedCondition3 = expectedConditionBuilder.IsNull("TestColumn5");
            SqlBasicParameterCondition actualCondition3 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test5.IsNull()) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);
        }

        [TestMethod]
        public void LinqCreateIsNotNullConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = fakeDb.CreateSelectCommand("");
            SelectCommand actualCommand = fakeDb.CreateSelectCommand("");

            SqlConditionBuilder expectedConditionBuilder = expectedCommand.ConditionBuilder;

            SqlBasicParameterCondition expectedCondition = expectedConditionBuilder.IsNotNull("TestColumn1");
            SqlBasicParameterCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.IsNotNull()) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlBasicParameterCondition expectedCondition2 = expectedConditionBuilder.IsNotNull("TestColumn2");
            SqlBasicParameterCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2.IsNotNull()) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            SqlBasicParameterCondition expectedCondition3 = expectedConditionBuilder.IsNotNull("TestColumn5");
            SqlBasicParameterCondition actualCondition3 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test5.IsNotNull()) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);
        }
        #endregion

        #region 包含条件
        [TestMethod]
        public void LinqCreateInConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = fakeDb.CreateSelectCommand("");
            SelectCommand actualCommand = fakeDb.CreateSelectCommand("");

            SqlConditionBuilder expectedConditionBuilder = expectedCommand.ConditionBuilder;

            SqlInsideParametersCondition expectedCondition = expectedConditionBuilder.InThese("TestColumn1", "1");
            SqlInsideParametersCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.InThese("1")) as SqlInsideParametersCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlInsideParametersCondition expectedCondition2 = expectedConditionBuilder.InThese("TestColumn1", "1", "2", "3", "4", "5");
            SqlInsideParametersCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.InThese("1", "2", "3", "4", "5")) as SqlInsideParametersCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            SqlInsideParametersCondition expectedCondition3 = expectedConditionBuilder.InThese("TestColumn2", 1, 2, 3, 4, 5);
            SqlInsideParametersCondition actualCondition3 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2.InThese(1, 2, 3, 4, 5)) as SqlInsideParametersCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);

            SqlInsideParametersCondition expectedCondition4 = expectedConditionBuilder.InThese("TestColumn5", 1, 2, 3, 4, 5);
            SqlInsideParametersCondition actualCondition4 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test5.InThese(1, 2, 3, 4, 5)) as SqlInsideParametersCondition;

            Assert.AreEqual(expectedCondition4, actualCondition4);

            TestEntityDataProvider provider = new TestEntityDataProvider(fakeDb);
            TestEntity entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };

            SqlInsideCommandCondition expectedCondition5 = expectedConditionBuilder.In("TestColumn4", provider.TableName, s => 
            {
                s.Query("TestColumn4")
                    .Paged(10, 2)
                    .Where<TestEntity>(c => c.Test1 == "test" && c.Test2 != 222 && c.Test4 < DateTime.Now)
                    .OrderBy<TestEntity>(c => c.Test3, SqlOrderType.Desc);
            });

            Action<SelectCommand> createAnotherSelect = s =>
            {
                s.Query("TestColumn4")
                       .Paged(10, 2)
                       .Where<TestEntity>(sc => sc.Test1 == "test" && sc.Test2 != 222 && sc.Test4 < DateTime.Now)
                       .OrderBy<TestEntity>(sc => sc.Test3, SqlOrderType.Desc);
            };

            SqlInsideCommandCondition actualCondition5 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test4.In<TestEntity>(createAnotherSelect)) as SqlInsideCommandCondition;

            Assert.AreEqual(expectedCondition5, actualCondition5);
        }

        [TestMethod]
        public void LinqCreateNotInConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = fakeDb.CreateSelectCommand("");
            SelectCommand actualCommand = fakeDb.CreateSelectCommand("");

            SqlConditionBuilder expectedConditionBuilder = expectedCommand.ConditionBuilder;

            SqlInsideParametersCondition expectedCondition = expectedConditionBuilder.NotInThese("TestColumn1", "1");
            SqlInsideParametersCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.NotInThese("1")) as SqlInsideParametersCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlInsideParametersCondition expectedCondition2 = expectedConditionBuilder.NotInThese("TestColumn1", "1", "2", "3", "4", "5");
            SqlInsideParametersCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.NotInThese("1", "2", "3", "4", "5")) as SqlInsideParametersCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            SqlInsideParametersCondition expectedCondition3 = expectedConditionBuilder.NotInThese("TestColumn2", 1, 2, 3, 4, 5);
            SqlInsideParametersCondition actualCondition3 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2.NotInThese(1, 2, 3, 4, 5)) as SqlInsideParametersCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);

            SqlInsideParametersCondition expectedCondition4 = expectedConditionBuilder.NotInThese("TestColumn5", 1, 2, 3, 4, 5);
            SqlInsideParametersCondition actualCondition4 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test5.NotInThese(1, 2, 3, 4, 5)) as SqlInsideParametersCondition;

            Assert.AreEqual(expectedCondition4, actualCondition4);

            TestEntityDataProvider provider = new TestEntityDataProvider(fakeDb);
            TestEntity entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };

            SqlInsideCommandCondition expectedCondition5 = expectedConditionBuilder.NotIn("TestColumn4", provider.TableName, s =>
            {
                s.Query("TestColumn4")
                    .Paged(10, 2)
                    .Where<TestEntity>(c => c.Test1 == "test" && c.Test2 != 222 && c.Test4 < DateTime.Now)
                    .OrderBy<TestEntity>(c => c.Test3, SqlOrderType.Desc);
            });

            Action<SelectCommand> createAnotherSelect = s =>
            {
                s.Query("TestColumn4")
                       .Paged(10, 2)
                       .Where<TestEntity>(sc => sc.Test1 == "test" && sc.Test2 != 222 && sc.Test4 < DateTime.Now)
                       .OrderBy<TestEntity>(sc => sc.Test3, SqlOrderType.Desc);
            };

            SqlInsideCommandCondition actualCondition5 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test4.NotIn<TestEntity>(createAnotherSelect)) as SqlInsideCommandCondition;

            Assert.AreEqual(expectedCondition5, actualCondition5);
        }
        #endregion

        #region 范围条件
        [TestMethod]
        public void LinqCreateBetweenConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = fakeDb.CreateSelectCommand("");
            SelectCommand actualCommand = fakeDb.CreateSelectCommand("");

            SqlConditionBuilder expectedConditionBuilder = expectedCommand.ConditionBuilder;

            SqlBasicParameterCondition expectedCondition = expectedConditionBuilder.Between("TestColumn1", "1", "2");
            SqlBasicParameterCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.Between("1", "2")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlBasicParameterCondition expectedCondition2 = expectedConditionBuilder.Between("TestColumn2", 1, 2);
            SqlBasicParameterCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2.Between(1, 2)) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            SqlBasicParameterCondition expectedCondition3 = expectedConditionBuilder.Between("TestColumn5", 1, 2);
            SqlBasicParameterCondition actualCondition3 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test5.Between(1, 2)) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);
        }

        [TestMethod]
        public void LinqCreateNotBetweenConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = fakeDb.CreateSelectCommand("");
            SelectCommand actualCommand = fakeDb.CreateSelectCommand("");

            SqlConditionBuilder expectedConditionBuilder = expectedCommand.ConditionBuilder;

            SqlBasicParameterCondition expectedCondition = expectedConditionBuilder.NotBetween("TestColumn1", "1", "2");
            SqlBasicParameterCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.NotBetween("1", "2")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlBasicParameterCondition expectedCondition2 = expectedConditionBuilder.NotBetween("TestColumn2", 1, 2);
            SqlBasicParameterCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2.NotBetween(1, 2)) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            SqlBasicParameterCondition expectedCondition3 = expectedConditionBuilder.NotBetween("TestColumn5", 1, 2);
            SqlBasicParameterCondition actualCondition3 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test5.NotBetween(1, 2)) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);
        }
        #endregion

        #region 相似条件
        [TestMethod]
        public void LinqCreateLikeConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = fakeDb.CreateSelectCommand("");
            SelectCommand actualCommand = fakeDb.CreateSelectCommand("");

            SqlConditionBuilder expectedConditionBuilder = expectedCommand.ConditionBuilder;

            SqlBasicParameterCondition expectedCondition = expectedConditionBuilder.Like("TestColumn1", "test1");
            SqlBasicParameterCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.Like("test1")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlBasicParameterCondition expectedCondition2 = expectedConditionBuilder.LikeAll("TestColumn1", "test2");
            SqlBasicParameterCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.LikeAll("test2")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            SqlBasicParameterCondition expectedCondition3 = expectedConditionBuilder.LikeStartWith("TestColumn1", "test3");
            SqlBasicParameterCondition actualCondition3 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.LikeStartWith("test3")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);

            SqlBasicParameterCondition expectedCondition4 = expectedConditionBuilder.LikeEndWith("TestColumn1", "test4");
            SqlBasicParameterCondition actualCondition4 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.LikeEndWith("test4")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition4, actualCondition4);
        }

        [TestMethod]
        public void LinqCreateNotLikeConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = fakeDb.CreateSelectCommand("");
            SelectCommand actualCommand = fakeDb.CreateSelectCommand("");

            SqlConditionBuilder expectedConditionBuilder = expectedCommand.ConditionBuilder;

            SqlBasicParameterCondition expectedCondition = expectedConditionBuilder.NotLike("TestColumn1", "test1");
            SqlBasicParameterCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.NotLike("test1")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlBasicParameterCondition expectedCondition2 = expectedConditionBuilder.NotLikeAll("TestColumn1", "test2");
            SqlBasicParameterCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.NotLikeAll("test2")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            SqlBasicParameterCondition expectedCondition3 = expectedConditionBuilder.NotLikeStartWith("TestColumn1", "test3");
            SqlBasicParameterCondition actualCondition3 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.NotLikeStartWith("test3")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);

            SqlBasicParameterCondition expectedCondition4 = expectedConditionBuilder.NotLikeEndWith("TestColumn1", "test4");
            SqlBasicParameterCondition actualCondition4 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.NotLikeEndWith("test4")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition4, actualCondition4);
        }
        #endregion

        #region 条件列表
        [TestMethod]
        public void LinqCreateConditionListTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = fakeDb.CreateSelectCommand("");
            SelectCommand actualCommand = fakeDb.CreateSelectCommand("");

            SqlConditionBuilder expectedConditionBuilder = expectedCommand.ConditionBuilder;

            AbstractSqlCondition expectedCondition = expectedConditionBuilder.GreaterThanOrEqual("TestColumn2", 123) & expectedConditionBuilder.LessThan("TestColumn2", 456);
            AbstractSqlCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2 >= 123 && c.Test2 < 456) as SqlConditionList;

            Assert.AreEqual(expectedCondition, actualCondition);

            AbstractSqlCondition expectedCondition2 = expectedConditionBuilder.GreaterThanOrEqual("TestColumn2", 123) | (expectedConditionBuilder.GreaterThan("TestColumn4", DateTime.Now) & expectedConditionBuilder.LessThan("TestColumn7", DateTime.Now.AddDays(7)));
            AbstractSqlCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2 >= 123 || (c.Test4 > DateTime.Now && c.Test7 < DateTime.Now.AddDays(7))) as SqlConditionList;

            Assert.AreEqual(expectedCondition2, actualCondition2);
        }
        #endregion

        #region Not
        [TestMethod]
        public void CreateNotConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = fakeDb.CreateSelectCommand("");
            SelectCommand actualCommand = fakeDb.CreateSelectCommand("");

            SqlConditionBuilder expectedConditionBuilder = expectedCommand.ConditionBuilder;
            SqlConditionBuilder actualConditionBuilder = expectedCommand.ConditionBuilder;

            SqlBasicParameterCondition baseCondition = expectedConditionBuilder.Equal("TestColumn2", 1);
            SqlNotCondition expectedCondition = !baseCondition;
            SqlNotCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => !(c.Test2 == 1)) as SqlNotCondition;

            Assert.AreEqual(expectedCondition, actualCondition);
        }
        #endregion
    }
}