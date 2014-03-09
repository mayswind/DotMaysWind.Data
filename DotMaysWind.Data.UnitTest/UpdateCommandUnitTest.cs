using System;
using System.Data;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Linq;
using DotMaysWind.Data.Orm;

namespace DotMaysWind.Data.UnitTest
{
    /// <summary>
    /// 更新语句单元测试
    /// </summary>
    [TestClass]
    public class UpdateCommandUnitTest
    {
        [TestMethod()]
        public void EntityUpdateTest()
        {
            Database fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient");
            TestEntityDataProvider provider = new TestEntityDataProvider(fakeDb);
            TestEntity entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };

            String expectedSql = "UPDATE TestTable SET TestColumn1=@PN_OLD_TestColumn1,TestColumn2=@PN_OLD_TestColumn2,TestColumn3=@PN_OLD_TestColumn3,TestColumn4=@PN_OLD_TestColumn4,TestColumn5=@PN_OLD_TestColumn5,TestColumn6=@PN_OLD_TestColumn6,TestColumn7=@PN_OLD_TestColumn7,TestColumn8=@PN_OLD_TestColumn8";
            SqlParameter[] expectedParameter = new SqlParameter[8]
            {
                SqlParameter.Create("TestColumn1", "OLD_TestColumn1", entity.Test1),
                SqlParameter.Create("TestColumn2", "OLD_TestColumn2", entity.Test2),
                SqlParameter.Create("TestColumn3", "OLD_TestColumn3", entity.Test3),
                SqlParameter.Create("TestColumn4", "OLD_TestColumn4", entity.Test4),
                SqlParameter.Create("TestColumn5", "OLD_TestColumn5", DbType.Int32, entity.Test5),
                SqlParameter.Create("TestColumn6", "OLD_TestColumn6", DbType.Double, entity.Test6),
                SqlParameter.Create("TestColumn7", "OLD_TestColumn7", DbType.DateTime, entity.Test7),
                SqlParameter.Create("TestColumn8", "OLD_TestColumn8", DbType.Int16, entity.Test8),
            };

            UpdateCommand cmd = fakeDb.CreateUpdateCommand(provider.TableName).Set(entity);
            String actualSql = cmd.GetSqlCommand().Trim();
            SqlParameter[] actualParameter = cmd.GetAllParameters();

            Assert.AreEqual(expectedSql, actualSql);

            for (Int32 i = 0; i < actualParameter.Length; i++)
            {
                Assert.AreEqual(expectedParameter[i], actualParameter[i]);
            }
        }

        [TestMethod()]
        public void LinqUpdateTest()
        {
            Database fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient");
            TestEntityDataProvider provider = new TestEntityDataProvider(fakeDb);
            TestEntity entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };

            String expectedSql = "UPDATE TestTable SET TestColumn1=@PN_OLD_TestColumn1,TestColumn2=@PN_OLD_TestColumn2,TestColumn3=@PN_OLD_TestColumn3,TestColumn4=@PN_OLD_TestColumn4,TestColumn5=@PN_OLD_TestColumn5,TestColumn6=@PN_OLD_TestColumn6,TestColumn7=@PN_OLD_TestColumn7,TestColumn8=@PN_OLD_TestColumn8";
            SqlParameter[] expectedParameter = new SqlParameter[8]
            {
                SqlParameter.Create("TestColumn1", "OLD_TestColumn1", entity.Test1),
                SqlParameter.Create("TestColumn2", "OLD_TestColumn2", entity.Test2),
                SqlParameter.Create("TestColumn3", "OLD_TestColumn3", entity.Test3),
                SqlParameter.Create("TestColumn4", "OLD_TestColumn4", entity.Test4),
                SqlParameter.Create("TestColumn5", "OLD_TestColumn5", DbType.Int32, entity.Test5),
                SqlParameter.Create("TestColumn6", "OLD_TestColumn6", DbType.Double, entity.Test6),
                SqlParameter.Create("TestColumn7", "OLD_TestColumn7", DbType.DateTime, entity.Test7),
                SqlParameter.Create("TestColumn8", "OLD_TestColumn8", DbType.Int16, entity.Test8),
            };

            UpdateCommand cmd = fakeDb.CreateUpdateCommand(provider.TableName)
                .Set<TestEntity>(c => c.Test1, entity.Test1)
                .Set<TestEntity>(c => c.Test2, entity.Test2)
                .Set<TestEntity>(c => c.Test3, entity.Test3)
                .Set<TestEntity>(c => c.Test4, entity.Test4)
                .Set<TestEntity>(c => c.Test5, entity.Test5)
                .Set<TestEntity>(c => c.Test6, entity.Test6)
                .Set<TestEntity>(c => c.Test7, entity.Test7)
                .Set<TestEntity>(c => c.Test8, entity.Test8);

            String actualSql = cmd.GetSqlCommand().Trim();
            SqlParameter[] actualParameter = cmd.GetAllParameters();

            Assert.AreEqual(expectedSql, actualSql);

            for (Int32 i = 0; i < actualParameter.Length; i++)
            {
                Assert.AreEqual(expectedParameter[i], actualParameter[i]);
            }
        }

        [TestMethod()]
        public void LinqIncreaseTest()
        {
            Database fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient");
            TestEntityDataProvider provider = new TestEntityDataProvider(fakeDb);
            TestEntity entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };

            String expectedSql = "UPDATE TestTable SET TestColumn2=TestColumn2+1";
            SqlParameter[] expectedParameter = new SqlParameter[1] { SqlParameter.CreateCustomAction("TestColumn2", "TestColumn2+1") };

            UpdateCommand cmd = fakeDb.CreateUpdateCommand(provider.TableName).Increase<TestEntity>(c => c.Test2);
            String actualSql = cmd.GetSqlCommand().Trim();
            SqlParameter[] actualParameter = cmd.GetAllParameters();

            Assert.AreEqual(expectedSql, actualSql);
        }

        [TestMethod()]
        public void LinqDecreaseTest()
        {
            Database fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient");
            TestEntityDataProvider provider = new TestEntityDataProvider(fakeDb);
            TestEntity entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };

            String expectedSql = "UPDATE TestTable SET TestColumn2=TestColumn2-1";
            SqlParameter[] expectedParameter = new SqlParameter[1] { SqlParameter.CreateCustomAction("TestColumn2", "TestColumn2-1") };

            UpdateCommand cmd = fakeDb.CreateUpdateCommand(provider.TableName).Decrease<TestEntity>(c => c.Test2);
            String actualSql = cmd.GetSqlCommand().Trim();
            SqlParameter[] actualParameter = cmd.GetAllParameters();

            Assert.AreEqual(expectedSql, actualSql);
        }
    }
}
