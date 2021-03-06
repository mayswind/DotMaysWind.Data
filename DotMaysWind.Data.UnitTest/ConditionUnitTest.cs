﻿using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Command.Condition;

namespace DotMaysWind.Data.UnitTest
{
    /// <summary>
    /// Sql条件单元测试
    /// </summary>
    [TestClass]
    public class ConditionUnitTest
    {
        #region 包含条件
        [TestMethod]
        public void CreateInConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = fakeDb.CreateSelectCommand("");
            SelectCommand actualCommand = fakeDb.CreateSelectCommand("");

            SqlConditionBuilder expectedConditionBuilder = expectedCommand.ConditionBuilder;
            SqlConditionBuilder actualConditionBuilder = actualCommand.ConditionBuilder;

            SqlInsideParametersCondition expectedCondition = expectedConditionBuilder.InThese("TestColumn2", 1, 2, 3, 4, 5);
            SqlInsideParametersCondition actualCondition = actualConditionBuilder.InInt32("TestColumn2", "1, 2, 3, 4, 5", ',');

            Assert.AreEqual(expectedCondition, actualCondition);
        }

        [TestMethod]
        public void CreateNotInConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = fakeDb.CreateSelectCommand("");
            SelectCommand actualCommand = fakeDb.CreateSelectCommand("");

            SqlConditionBuilder expectedConditionBuilder = expectedCommand.ConditionBuilder;
            SqlConditionBuilder actualConditionBuilder = actualCommand.ConditionBuilder;

            SqlInsideParametersCondition expectedCondition = expectedConditionBuilder.NotInThese("TestColumn2", 1, 2, 3, 4, 5);
            SqlInsideParametersCondition actualCondition = actualConditionBuilder.NotInInt32("TestColumn2", "1, 2, 3, 4, 5", ',');

            Assert.AreEqual(expectedCondition, actualCondition);
        }
        #endregion

        #region Not
        [TestMethod]
        public void CreateNotConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;

            SelectCommand baseCommand = fakeDb.CreateSelectCommand("");
            SqlConditionBuilder conditionBuilder = baseCommand.ConditionBuilder;

            SqlBasicParameterCondition baseCondition = conditionBuilder.Equal("TestColumn2", 1);
            SqlNotCondition actualCondition = !baseCondition;

            String expectedConditionClause = "(NOT((TestColumn2 = @PN_IDX_0)))";
            String actualConditionClause = actualCondition.GetClauseText();

            DataParameter[] expectedParameters = baseCondition.GetAllParameters();
            DataParameter[] actualParameters = baseCondition.GetAllParameters();

            Assert.AreEqual(expectedConditionClause, actualConditionClause);

            for (Int32 i = 0; i < expectedParameters.Length; i++)
            {
                Assert.AreEqual(expectedParameters[i], actualParameters[i]);
            }
        }
        #endregion

        #region Nullable
        [TestMethod]
        public void CreateNullableConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;

            SelectCommand baseCommand = fakeDb.CreateSelectCommand("");
            SqlConditionBuilder conditionBuilder = baseCommand.ConditionBuilder;

            AbstractSqlCondition baseCondition = conditionBuilder.Equal("TestColumn2", 1);

            AbstractSqlCondition expectedCondition = baseCondition & null;
            AbstractSqlCondition actualCondition = baseCondition;

            Assert.AreEqual(expectedCondition, actualCondition);

            AbstractSqlCondition expectedCondition2 = null & baseCondition;
            AbstractSqlCondition actualCondition2 = baseCondition;

            Assert.AreEqual(expectedCondition2, actualCondition2);

            AbstractSqlCondition expectedCondition3 = baseCondition | null;
            AbstractSqlCondition actualCondition3 = baseCondition;

            Assert.AreEqual(expectedCondition3, actualCondition3);

            AbstractSqlCondition expectedCondition4 = null | baseCondition;
            AbstractSqlCondition actualCondition4 = baseCondition;

            Assert.AreEqual(expectedCondition4, actualCondition4);
        }
        #endregion
    }
}