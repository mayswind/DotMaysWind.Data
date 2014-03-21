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
            SelectCommand expectedCommand = new SelectCommand(fakeDb, "");
            SelectCommand actualCommand = new SelectCommand(fakeDb, "");

            SqlBasicParameterCondition expectedCondition = SqlCondition.Equal(expectedCommand, "TestColumn2", 123);
            SqlBasicParameterCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2 == 123) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlBasicParameterCondition expectedCondition2 = SqlCondition.NotEqual(expectedCommand, "TestColumn2", 123);
            SqlBasicParameterCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2 != 123) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            SqlBasicParameterCondition expectedCondition3 = SqlCondition.GreaterThan(expectedCommand, "TestColumn2", 123);
            SqlBasicParameterCondition actualCondition3 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2 > 123) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);

            SqlBasicParameterCondition expectedCondition4 = SqlCondition.LessThan(expectedCommand, "TestColumn2", 123);
            SqlBasicParameterCondition actualCondition4 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2 < 123) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition4, actualCondition4);

            SqlBasicParameterCondition expectedCondition5 = SqlCondition.GreaterThanOrEqual(expectedCommand, "TestColumn2", 123);
            SqlBasicParameterCondition actualCondition5 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2 >= 123) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition5, actualCondition5);

            SqlBasicParameterCondition expectedCondition6 = SqlCondition.LessThanOrEqual(expectedCommand, "TestColumn2", 123);
            SqlBasicParameterCondition actualCondition6 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2 <= 123) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition6, actualCondition6);

            SqlBasicParameterCondition expectedCondition7 = SqlCondition.IsNotNull(expectedCommand, "TestColumn1");
            SqlBasicParameterCondition actualCondition7 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1 != null) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition7, actualCondition7);

            SqlBasicParameterCondition expectedCondition8 = SqlCondition.IsNull(expectedCommand, "TestColumn3");
            SqlBasicParameterCondition actualCondition8 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test3 == null) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition8, actualCondition8);

            SqlBasicParameterCondition expectedCondition10 = SqlCondition.EqualColumn(expectedCommand, "TestColumn2", "TestColumn3");
            SqlBasicParameterCondition actualCondition10 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2 == c.Test3) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition10, actualCondition10);
        }
        #endregion

        #region 判空条件
        [TestMethod]
        public void LinqCreateIsNullConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = new SelectCommand(fakeDb, "");
            SelectCommand actualCommand = new SelectCommand(fakeDb, "");

            SqlBasicParameterCondition expectedCondition = SqlCondition.IsNull(expectedCommand, "TestColumn1");
            SqlBasicParameterCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.IsNull()) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlBasicParameterCondition expectedCondition2 = SqlCondition.IsNull(expectedCommand, "TestColumn2");
            SqlBasicParameterCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2.IsNull()) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            SqlBasicParameterCondition expectedCondition3 = SqlCondition.IsNull(expectedCommand, "TestColumn5");
            SqlBasicParameterCondition actualCondition3 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test5.IsNull()) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);
        }

        [TestMethod]
        public void LinqCreateIsNotNullConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = new SelectCommand(fakeDb, "");
            SelectCommand actualCommand = new SelectCommand(fakeDb, "");

            SqlBasicParameterCondition expectedCondition = SqlCondition.IsNotNull(expectedCommand, "TestColumn1");
            SqlBasicParameterCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.IsNotNull()) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlBasicParameterCondition expectedCondition2 = SqlCondition.IsNotNull(expectedCommand, "TestColumn2");
            SqlBasicParameterCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2.IsNotNull()) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            SqlBasicParameterCondition expectedCondition3 = SqlCondition.IsNotNull(expectedCommand, "TestColumn5");
            SqlBasicParameterCondition actualCondition3 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test5.IsNotNull()) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);
        }
        #endregion

        #region 包含条件
        [TestMethod]
        public void LinqCreateInConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = new SelectCommand(fakeDb, "");
            SelectCommand actualCommand = new SelectCommand(fakeDb, "");

            SqlInsideParametersCondition expectedCondition = SqlCondition.In(expectedCommand, "TestColumn1", "1");
            SqlInsideParametersCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.In("1")) as SqlInsideParametersCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlInsideParametersCondition expectedCondition2 = SqlCondition.In(expectedCommand, "TestColumn1", "1", "2", "3", "4", "5");
            SqlInsideParametersCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.In("1", "2", "3", "4", "5")) as SqlInsideParametersCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            SqlInsideParametersCondition expectedCondition3 = SqlCondition.In(expectedCommand, "TestColumn2", 1, 2, 3, 4, 5);
            SqlInsideParametersCondition actualCondition3 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2.In(1, 2, 3, 4, 5)) as SqlInsideParametersCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);

            SqlInsideParametersCondition expectedCondition4 = SqlCondition.In(expectedCommand, "TestColumn5", 1, 2, 3, 4, 5);
            SqlInsideParametersCondition actualCondition4 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test5.In(1, 2, 3, 4, 5)) as SqlInsideParametersCondition;

            Assert.AreEqual(expectedCondition4, actualCondition4);

            TestEntityDataProvider provider = new TestEntityDataProvider(fakeDb);
            TestEntity entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };
            SelectCommand cmd = fakeDb.CreateSelectCommand(provider.TableName, 10, 2, 55)
                .Query("TestColumn4")
                .Where<TestEntity>(c => c.Test1 == "test" && c.Test2 != 222 && c.Test4 < DateTime.Now)
                .OrderBy<TestEntity>(c => c.Test3, SqlOrderType.Desc);

            SqlInsideCommandCondition expectedCondition5 = SqlCondition.In(expectedCommand, "TestColumn4", cmd);
            SqlInsideCommandCondition actualCondition5 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test4.In(cmd)) as SqlInsideCommandCondition;

            Assert.AreEqual(expectedCondition5, actualCondition5);
        }

        [TestMethod]
        public void LinqCreateNotInConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = new SelectCommand(fakeDb, "");
            SelectCommand actualCommand = new SelectCommand(fakeDb, "");

            SqlInsideParametersCondition expectedCondition = SqlCondition.NotIn(expectedCommand, "TestColumn1", "1");
            SqlInsideParametersCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.NotIn("1")) as SqlInsideParametersCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlInsideParametersCondition expectedCondition2 = SqlCondition.NotIn(expectedCommand, "TestColumn1", "1", "2", "3", "4", "5");
            SqlInsideParametersCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.NotIn("1", "2", "3", "4", "5")) as SqlInsideParametersCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            SqlInsideParametersCondition expectedCondition3 = SqlCondition.NotIn(expectedCommand, "TestColumn2", 1, 2, 3, 4, 5);
            SqlInsideParametersCondition actualCondition3 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2.NotIn(1, 2, 3, 4, 5)) as SqlInsideParametersCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);

            SqlInsideParametersCondition expectedCondition4 = SqlCondition.NotIn(expectedCommand, "TestColumn5", 1, 2, 3, 4, 5);
            SqlInsideParametersCondition actualCondition4 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test5.NotIn(1, 2, 3, 4, 5)) as SqlInsideParametersCondition;

            Assert.AreEqual(expectedCondition4, actualCondition4);

            TestEntityDataProvider provider = new TestEntityDataProvider(fakeDb);
            TestEntity entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };
            SelectCommand cmd = fakeDb.CreateSelectCommand(provider.TableName, 10, 2, 55)
                .Query("TestColumn4")
                .Where<TestEntity>(c => c.Test1 == "test" && c.Test2 != 222 && c.Test4 < DateTime.Now)
                .OrderBy<TestEntity>(c => c.Test3, SqlOrderType.Desc);

            SqlInsideCommandCondition expectedCondition5 = SqlCondition.NotIn(expectedCommand, "TestColumn4", cmd);
            SqlInsideCommandCondition actualCondition5 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test4.NotIn(cmd)) as SqlInsideCommandCondition;

            Assert.AreEqual(expectedCondition5, actualCondition5);
        }
        #endregion

        #region 范围条件
        [TestMethod]
        public void LinqCreateBetweenConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = new SelectCommand(fakeDb, "");
            SelectCommand actualCommand = new SelectCommand(fakeDb, "");

            SqlBasicParameterCondition expectedCondition = SqlCondition.Between(expectedCommand, "TestColumn1", "1", "2");
            SqlBasicParameterCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.Between("1", "2")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlBasicParameterCondition expectedCondition2 = SqlCondition.Between(expectedCommand, "TestColumn2", 1, 2);
            SqlBasicParameterCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2.Between(1, 2)) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            SqlBasicParameterCondition expectedCondition3 = SqlCondition.Between(expectedCommand, "TestColumn5", 1, 2);
            SqlBasicParameterCondition actualCondition3 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test5.Between(1, 2)) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);
        }

        [TestMethod]
        public void LinqCreateNotBetweenConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = new SelectCommand(fakeDb, "");
            SelectCommand actualCommand = new SelectCommand(fakeDb, "");

            SqlBasicParameterCondition expectedCondition = SqlCondition.NotBetween(expectedCommand, "TestColumn1", "1", "2");
            SqlBasicParameterCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.NotBetween("1", "2")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlBasicParameterCondition expectedCondition2 = SqlCondition.NotBetween(expectedCommand, "TestColumn2", 1, 2);
            SqlBasicParameterCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2.NotBetween(1, 2)) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            SqlBasicParameterCondition expectedCondition3 = SqlCondition.NotBetween(expectedCommand, "TestColumn5", 1, 2);
            SqlBasicParameterCondition actualCondition3 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test5.NotBetween(1, 2)) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);
        }
        #endregion

        #region 相似条件
        [TestMethod]
        public void LinqCreateLikeConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = new SelectCommand(fakeDb, "");
            SelectCommand actualCommand = new SelectCommand(fakeDb, "");

            SqlBasicParameterCondition expectedCondition = SqlCondition.Like(expectedCommand, "TestColumn1", "test1");
            SqlBasicParameterCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.Like("test1")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlBasicParameterCondition expectedCondition2 = SqlCondition.LikeAll(expectedCommand, "TestColumn1", "test2");
            SqlBasicParameterCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.LikeAll("test2")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            SqlBasicParameterCondition expectedCondition3 = SqlCondition.LikeStartWith(expectedCommand, "TestColumn1", "test3");
            SqlBasicParameterCondition actualCondition3 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.LikeStartWith("test3")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);

            SqlBasicParameterCondition expectedCondition4 = SqlCondition.LikeEndWith(expectedCommand, "TestColumn1", "test4");
            SqlBasicParameterCondition actualCondition4 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.LikeEndWith("test4")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition4, actualCondition4);
        }

        [TestMethod]
        public void LinqCreateNotLikeConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = new SelectCommand(fakeDb, "");
            SelectCommand actualCommand = new SelectCommand(fakeDb, "");

            SqlBasicParameterCondition expectedCondition = SqlCondition.NotLike(expectedCommand, "TestColumn1", "test1");
            SqlBasicParameterCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.NotLike("test1")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlBasicParameterCondition expectedCondition2 = SqlCondition.NotLikeAll(expectedCommand, "TestColumn1", "test2");
            SqlBasicParameterCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.NotLikeAll("test2")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            SqlBasicParameterCondition expectedCondition3 = SqlCondition.NotLikeStartWith(expectedCommand, "TestColumn1", "test3");
            SqlBasicParameterCondition actualCondition3 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.NotLikeStartWith("test3")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);

            SqlBasicParameterCondition expectedCondition4 = SqlCondition.NotLikeEndWith(expectedCommand, "TestColumn1", "test4");
            SqlBasicParameterCondition actualCondition4 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test1.NotLikeEndWith("test4")) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition4, actualCondition4);
        }
        #endregion

        #region 条件列表
        [TestMethod]
        public void LinqCreateConditionListTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = new SelectCommand(fakeDb, "");
            SelectCommand actualCommand = new SelectCommand(fakeDb, "");

            SqlConditionList expectedCondition = SqlCondition.GreaterThanOrEqual(expectedCommand, "TestColumn2", 123) & SqlCondition.LessThan(expectedCommand, "TestColumn2", 456);
            SqlConditionList actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2 >= 123 && c.Test2 < 456) as SqlConditionList;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlConditionList expectedCondition2 = SqlCondition.GreaterThanOrEqual(expectedCommand, "TestColumn2", 123) | (SqlCondition.GreaterThan(expectedCommand, "TestColumn4", DateTime.Now) & SqlCondition.LessThan(expectedCommand, "TestColumn7", DateTime.Now.AddDays(7)));
            SqlConditionList actualCondition2 = SqlLinqCondition.Create<TestEntity>(actualCommand, c => c.Test2 >= 123 || (c.Test4 > DateTime.Now && c.Test7 < DateTime.Now.AddDays(7))) as SqlConditionList;

            Assert.AreEqual(expectedCondition2, actualCondition2);
        }
        #endregion

        #region Not
        [TestMethod]
        public void CreateNotConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = new SelectCommand(fakeDb, "");
            SelectCommand actualCommand = new SelectCommand(fakeDb, "");

            SqlBasicParameterCondition baseCondition = SqlCondition.Equal(expectedCommand, "TestColumn2", 1);
            SqlNotCondition expectedCondition = !baseCondition;
            SqlNotCondition actualCondition = SqlLinqCondition.Create<TestEntity>(actualCommand, c => !(c.Test2 == 1)) as SqlNotCondition;

            Assert.AreEqual(expectedCondition, actualCondition);
        }
        #endregion
    }
}