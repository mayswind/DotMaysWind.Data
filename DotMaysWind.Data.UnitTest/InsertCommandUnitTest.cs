using System;
using System.Data;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotMaysWind.Data.UnitTest
{
    /// <summary>
    /// 插入语句单元测试
    /// </summary>
    [TestClass]
    public class InsertCommandUnitTest
    {
        [TestMethod()]
        public void EntityInsertTest()
        {
            Database fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient");
            TestEntityDataProvider provider = new TestEntityDataProvider(fakeDb);
            TestEntity entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };

            String expectedSql = "INSERT INTO TestTable ( TestColumn1,TestColumn2,TestColumn3,TestColumn4,TestColumn5,TestColumn6,TestColumn7,TestColumn8 ) " +
                "VALUES ( @PN_NEW_TestColumn1,@PN_NEW_TestColumn2,@PN_NEW_TestColumn3,@PN_NEW_TestColumn4,@PN_NEW_TestColumn5,@PN_NEW_TestColumn6,@PN_NEW_TestColumn7,@PN_NEW_TestColumn8 )";
            SqlParameter[] expectedParameter = new SqlParameter[8]
            {
                SqlParameter.Create("TestColumn1", "NEW_TestColumn1", entity.Test1),
                SqlParameter.Create("TestColumn2", "NEW_TestColumn2", entity.Test2),
                SqlParameter.Create("TestColumn3", "NEW_TestColumn3", entity.Test3),
                SqlParameter.Create("TestColumn4", "NEW_TestColumn4", entity.Test4),
                SqlParameter.Create("TestColumn5", "NEW_TestColumn5", entity.Test5),
                SqlParameter.Create("TestColumn6", "NEW_TestColumn6", entity.Test6),
                SqlParameter.Create("TestColumn7", "NEW_TestColumn7", entity.Test7),
                SqlParameter.Create("TestColumn8", "NEW_TestColumn8", DbType.Int16, entity.Test8),
            };

            String actualSql = "";
            SqlParameter[] actualParameter = null;
            provider.EntityInsert(entity, out actualSql, out actualParameter);

            Assert.AreEqual(expectedSql, actualSql);

            for (Int32 i = 0; i < actualParameter.Length; i++)
            {
                Assert.AreEqual(expectedParameter[i], actualParameter[i]);
            }
        }

        [TestMethod()]
        public void LinqInsertTest()
        {
            Database fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient");
            TestEntityDataProvider provider = new TestEntityDataProvider(fakeDb);
            TestEntity entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };

            String expectedSql = "INSERT INTO TestTable ( TestColumn1,TestColumn2,TestColumn3,TestColumn4,TestColumn5,TestColumn6,TestColumn7,TestColumn8 ) " +
                "VALUES ( @PN_NEW_TestColumn1,@PN_NEW_TestColumn2,@PN_NEW_TestColumn3,@PN_NEW_TestColumn4,@PN_NEW_TestColumn5,@PN_NEW_TestColumn6,@PN_NEW_TestColumn7,@PN_NEW_TestColumn8 )";
            SqlParameter[] expectedParameter = new SqlParameter[8]
            {
                SqlParameter.Create("TestColumn1", "NEW_TestColumn1", entity.Test1),
                SqlParameter.Create("TestColumn2", "NEW_TestColumn2", entity.Test2),
                SqlParameter.Create("TestColumn3", "NEW_TestColumn3", entity.Test3),
                SqlParameter.Create("TestColumn4", "NEW_TestColumn4", entity.Test4),
                SqlParameter.Create("TestColumn5", "NEW_TestColumn5", entity.Test5),
                SqlParameter.Create("TestColumn6", "NEW_TestColumn6", entity.Test6),
                SqlParameter.Create("TestColumn7", "NEW_TestColumn7", entity.Test7),
                SqlParameter.Create("TestColumn8", "NEW_TestColumn8", DbType.Int16, entity.Test8),
            };

            String actualSql = "";
            SqlParameter[] actualParameter = null;
            provider.LinqInsert(entity, out actualSql, out actualParameter);

            Assert.AreEqual(expectedSql, actualSql);

            for (Int32 i = 0; i < actualParameter.Length; i++)
            {
                Assert.AreEqual(expectedParameter[i], actualParameter[i]);
            }
        }
    }
}
