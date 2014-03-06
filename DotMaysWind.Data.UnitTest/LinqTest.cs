using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using DotMaysWind.Data.Command.Condition;
using DotMaysWind.Data.Linq;
using DotMaysWind.Data.Orm;

namespace DotMaysWind.Data.UnitTest
{
    /// <summary>
    /// Linq单元测试
    /// </summary>
    [TestClass]
    public class LinqTest
    {
        [DatabaseTableAtrribute("TestTable")]
        private class TestEntity
        {
            [DatabaseColumnAtrribute("TestColumn1")]
            public Int32 Test1 { get; set; }

            [DatabaseColumnAtrribute("TestColumn2")]
            public DateTime Test2 { get; set; }

            [DatabaseColumnAtrribute("TestColumn3")]
            public DateTime? Test3 { get; set; }
        }

        [TestMethod]
        public void LinqCreateBasicConditionTest()
        {
            SqlBasicParameterCondition expectedCondition = SqlCondition.Equal("TestColumn1", 123);
            SqlBasicParameterCondition actualCondition = SqlLinqCondition.Create<TestEntity>(c => c.Test1 == 123) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlBasicParameterCondition expectedCondition2 = SqlCondition.NotEqual("TestColumn1", 123);
            SqlBasicParameterCondition actualCondition2 = SqlLinqCondition.Create<TestEntity>(c => c.Test1 != 123) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            SqlBasicParameterCondition expectedCondition3 = SqlCondition.GreaterThan("TestColumn1", 123);
            SqlBasicParameterCondition actualCondition3 = SqlLinqCondition.Create<TestEntity>(c => c.Test1 > 123) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);

            SqlBasicParameterCondition expectedCondition4 = SqlCondition.LessThan("TestColumn1", 123);
            SqlBasicParameterCondition actualCondition4 = SqlLinqCondition.Create<TestEntity>(c => c.Test1 < 123) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition4, actualCondition4);

            SqlBasicParameterCondition expectedCondition5 = SqlCondition.GreaterThanOrEqual("TestColumn1", 123);
            SqlBasicParameterCondition actualCondition5 = SqlLinqCondition.Create<TestEntity>(c => c.Test1 >= 123) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition5, actualCondition5);

            SqlBasicParameterCondition expectedCondition6 = SqlCondition.LessThanOrEqual("TestColumn1", 123);
            SqlBasicParameterCondition actualCondition6 = SqlLinqCondition.Create<TestEntity>(c => c.Test1 <= 123) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition6, actualCondition6);

            SqlBasicParameterCondition expectedCondition7 = SqlCondition.IsNotNull("TestColumn3");
            SqlBasicParameterCondition actualCondition7 = SqlLinqCondition.Create<TestEntity>(c => c.Test3 != null) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition7, actualCondition7);

            SqlBasicParameterCondition expectedCondition8 = SqlCondition.IsNull("TestColumn3");
            SqlBasicParameterCondition actualCondition8 = SqlLinqCondition.Create<TestEntity>(c => c.Test3 == null) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition8, actualCondition8);

            SqlBasicParameterCondition expectedCondition9 = SqlCondition.EqualColumn("TestColumn2", "TestColumn3");
            SqlBasicParameterCondition actualCondition9 = SqlLinqCondition.Create<TestEntity>(c => c.Test2 == c.Test3) as SqlBasicParameterCondition;

            Assert.AreEqual(expectedCondition9, actualCondition9);
        }

        [TestMethod]
        public void LinqCreateConditionListTest()
        {
            SqlConditionList expectedCondition = SqlCondition.GreaterThanOrEqual("TestColumn1", 123) & SqlCondition.LessThan("TestColumn1", 456);
            SqlConditionList actualCondition = SqlLinqCondition.Create<TestEntity>(c => c.Test1 >= 123 && c.Test1 < 456) as SqlConditionList;

            Assert.AreEqual(expectedCondition, actualCondition);

            SqlConditionList expectedCondition2 = SqlCondition.GreaterThanOrEqual("TestColumn1", 123) | (SqlCondition.GreaterThan("TestColumn2", DateTime.Now) & SqlCondition.LessThan("TestColumn2", DateTime.Now.AddDays(7)));
            SqlConditionList actualCondition2 = SqlLinqCondition.Create<TestEntity>(c => c.Test1 >= 123 || (c.Test2 > DateTime.Now && c.Test2 < DateTime.Now.AddDays(7))) as SqlConditionList;

            Assert.AreEqual(expectedCondition2, actualCondition2);
        }
    }
}