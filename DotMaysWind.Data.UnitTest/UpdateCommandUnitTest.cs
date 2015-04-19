using System;

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
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            TestEntityDataProvider provider = new TestEntityDataProvider(fakeDb);
            TestEntity entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };

            String expectedSql = "UPDATE TestTable SET TestColumn1=@PN_IDX_0,TestColumn2=@PN_IDX_1,TestColumn3=@PN_IDX_2,TestColumn4=@PN_IDX_3,TestColumn5=@PN_IDX_4,TestColumn6=@PN_IDX_5,TestColumn7=@PN_IDX_6,TestColumn8=@PN_IDX_7";
            DataParameter[] expectedParameter = new DataParameter[8]
            {
                DataParameter.InternalCreate(fakeDb, "TestColumn1", "0", entity.Test1),
                DataParameter.InternalCreate(fakeDb, "TestColumn2", "1", entity.Test2),
                DataParameter.InternalCreate(fakeDb, "TestColumn3", "2", entity.Test3),
                DataParameter.InternalCreate(fakeDb, "TestColumn4", "3", entity.Test4),
                DataParameter.InternalCreate(fakeDb, "TestColumn5", "4", DataType.Int32, entity.Test5),
                DataParameter.InternalCreate(fakeDb, "TestColumn6", "5", DataType.Double, entity.Test6),
                DataParameter.InternalCreate(fakeDb, "TestColumn7", "6", DataType.DateTime, entity.Test7),
                DataParameter.InternalCreate(fakeDb, "TestColumn8", "7", DataType.Int16, entity.Test8)
            };

            UpdateCommand cmd = fakeDb.CreateUpdateCommand(provider.TableName).Set(entity);
            String actualSql = cmd.GetCommandText().Trim();
            DataParameter[] actualParameter = cmd.GetAllParameters();

            Assert.AreEqual(expectedSql, actualSql);

            for (Int32 i = 0; i < actualParameter.Length; i++)
            {
                Assert.AreEqual(expectedParameter[i], actualParameter[i]);
            }
        }

        [TestMethod()]
        public void LinqUpdateTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            TestEntityDataProvider provider = new TestEntityDataProvider(fakeDb);
            TestEntity entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };

            String expectedSql = "UPDATE TestTable SET TestColumn1=@PN_IDX_0,TestColumn2=@PN_IDX_1,TestColumn3=@PN_IDX_2,TestColumn4=@PN_IDX_3,TestColumn5=@PN_IDX_4,TestColumn6=@PN_IDX_5,TestColumn7=@PN_IDX_6,TestColumn8=@PN_IDX_7";
            DataParameter[] expectedParameter = new DataParameter[8]
            {
                DataParameter.InternalCreate(fakeDb, "TestColumn1", "0", entity.Test1),
                DataParameter.InternalCreate(fakeDb, "TestColumn2", "1", entity.Test2),
                DataParameter.InternalCreate(fakeDb, "TestColumn3", "2", entity.Test3),
                DataParameter.InternalCreate(fakeDb, "TestColumn4", "3", entity.Test4),
                DataParameter.InternalCreate(fakeDb, "TestColumn5", "4", DataType.Int32, entity.Test5),
                DataParameter.InternalCreate(fakeDb, "TestColumn6", "5", DataType.Double, entity.Test6),
                DataParameter.InternalCreate(fakeDb, "TestColumn7", "6", DataType.DateTime, entity.Test7),
                DataParameter.InternalCreate(fakeDb, "TestColumn8", "7", DataType.Int16, entity.Test8)
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

            String actualSql = cmd.GetCommandText().Trim();
            DataParameter[] actualParameter = cmd.GetAllParameters();

            Assert.AreEqual(expectedSql, actualSql);

            for (Int32 i = 0; i < actualParameter.Length; i++)
            {
                Assert.AreEqual(expectedParameter[i], actualParameter[i]);
            }
        }

        [TestMethod()]
        public void LinqIncreaseTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            TestEntityDataProvider provider = new TestEntityDataProvider(fakeDb);
            TestEntity entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };

            String expectedSql = "UPDATE TestTable SET TestColumn2=(TestColumn2+1)";
            DataParameter[] expectedParameter = new DataParameter[0];

            UpdateCommand cmd = fakeDb.CreateUpdateCommand(provider.TableName).Increase<TestEntity>(c => c.Test2);
            String actualSql = cmd.GetCommandText().Trim();
            DataParameter[] actualParameter = cmd.GetAllParameters();

            Assert.AreEqual(expectedSql, actualSql);
            Assert.AreEqual(expectedParameter.Length, actualParameter.Length);
        }

        [TestMethod()]
        public void LinqDecreaseTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            TestEntityDataProvider provider = new TestEntityDataProvider(fakeDb);
            TestEntity entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };

            String expectedSql = "UPDATE TestTable SET TestColumn2=(TestColumn2-1)";
            DataParameter[] expectedParameter = new DataParameter[0];

            UpdateCommand cmd = fakeDb.CreateUpdateCommand(provider.TableName).Decrease<TestEntity>(c => c.Test2);
            String actualSql = cmd.GetCommandText().Trim();
            DataParameter[] actualParameter = cmd.GetAllParameters();

            Assert.AreEqual(expectedSql, actualSql);
            Assert.AreEqual(expectedParameter.Length, actualParameter.Length);
        }
    }
}
