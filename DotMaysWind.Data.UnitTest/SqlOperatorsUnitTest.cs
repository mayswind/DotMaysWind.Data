using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DotMaysWind.Data.Command.Condition;

namespace DotMaysWind.Data.UnitTest
{
    /// <summary>
    /// Sql运算符测试
    ///</summary>
    [TestClass()]
    public class SqlOperatorsUnitTest
    {
        [TestMethod()]
        public void GetOperatorFormatTest()
        {
            String[] OperatorFormats = new String[] {
                "{0} IS NULL",
                "{0} IS NOT NULL",

                "{0} = {1}",
                "{0} <> {1}",
                "{0} > {1}",
                "{0} < {1}",
                "{0} >= {1}",
                "{0} <= {1}",
                "{0} LIKE {1}",
                "{0} NOT LIKE {1}",

                "{0} BETWEEN {1} AND {2}",
                "{0} NOT BETWEEN {1} AND {2}"
            };

            SqlOperator[] Operators = new SqlOperator[] {
                SqlOperator.IsNull,//IS NULL
                SqlOperator.IsNotNull,//IS NOT NULL

                SqlOperator.Equal,//=
                SqlOperator.NotEqual,//<>
                SqlOperator.GreaterThan,//>
                SqlOperator.LessThan,//<
                SqlOperator.GreaterThanOrEqual,//>=
                SqlOperator.LessThanOrEqual,//<=
                SqlOperator.Like,//LIKE
                SqlOperator.NotLike,//NOT LIKE
                
                SqlOperator.Between,//BETWEEN
                SqlOperator.NotBetween,//NOT BETWEEN
            };

            SqlOperator op = new SqlOperator();
            String expected = String.Empty;
            String actual = String.Empty;

            for (Byte i = 0; i < 12; i++)
            {
                op = Operators[i];
                expected = OperatorFormats[i];
                actual = SqlOperators.InternalGetOperatorFormat(op);

                Assert.AreEqual(expected, actual);
            }
        }
    }
}