using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            SqlInsideParametersCondition expectedCondition = SqlCondition.In("TestColumn2", 1, 2, 3, 4, 5);
            SqlInsideParametersCondition actualCondition = SqlCondition.InInt32("TestColumn2", "1, 2, 3, 4, 5", ',');

            Assert.AreEqual(expectedCondition, actualCondition);
        }

        [TestMethod]
        public void CreateNotInConditionTest()
        {
            SqlInsideParametersCondition expectedCondition = SqlCondition.NotIn("TestColumn2", 1, 2, 3, 4, 5);
            SqlInsideParametersCondition actualCondition = SqlCondition.NotInInt32("TestColumn2", "1, 2, 3, 4, 5", ',');

            Assert.AreEqual(expectedCondition, actualCondition);
        }
        #endregion

        #region Not
        [TestMethod]
        public void CreateNotConditionTest()
        {
            SqlBasicParameterCondition baseCondition = SqlCondition.Equal("TestColumn2", 1);
            SqlNotCondition actualCondition = !baseCondition;

            String expectedConditionClause = "(NOT((TestColumn2 = @PN_TestColumn2_Equal)))";
            String actualConditionClause = actualCondition.GetSqlClause();

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