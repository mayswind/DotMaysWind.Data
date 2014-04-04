using System;

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
            SelectCommand expectedCommand = new SelectCommand(fakeDb, "");
            SelectCommand actualCommand = new SelectCommand(fakeDb, "");

            SqlInsideParametersCondition expectedCondition = SqlCondition.In(expectedCommand, "TestColumn2", 1, 2, 3, 4, 5);
            SqlInsideParametersCondition actualCondition = SqlCondition.InInt32(actualCommand, "TestColumn2", "1, 2, 3, 4, 5", ',');

            Assert.AreEqual(expectedCondition, actualCondition);
        }

        [TestMethod]
        public void CreateNotInConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            SelectCommand expectedCommand = new SelectCommand(fakeDb, "");
            SelectCommand actualCommand = new SelectCommand(fakeDb, "");

            SqlInsideParametersCondition expectedCondition = SqlCondition.NotIn(expectedCommand, "TestColumn2", 1, 2, 3, 4, 5);
            SqlInsideParametersCondition actualCondition = SqlCondition.NotInInt32(actualCommand, "TestColumn2", "1, 2, 3, 4, 5", ',');

            Assert.AreEqual(expectedCondition, actualCondition);
        }
        #endregion

        #region Not
        [TestMethod]
        public void CreateNotConditionTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;

            SelectCommand baseCommand = new SelectCommand(fakeDb, "");
            SqlBasicParameterCondition baseCondition = SqlCondition.Equal(baseCommand, "TestColumn2", 1);
            SqlNotCondition actualCondition = !baseCondition;

            String expectedConditionClause = "(NOT((TestColumn2 = @PN_IDX_0)))";
            String actualConditionClause = actualCondition.GetClauseText();

            SqlParameter[] expectedParameters = baseCondition.GetAllParameters();
            SqlParameter[] actualParameters = baseCondition.GetAllParameters();

            Assert.AreEqual(expectedConditionClause, actualConditionClause);

            for (Int32 i = 0; i < expectedParameters.Length; i++)
            {
                Assert.AreEqual(expectedParameters[i], actualParameters[i]);
            }
        }
        #endregion
    }
}