using System;
using System.Data;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Linq;
using DotMaysWind.Data.Orm;

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
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            TestEntityDataProvider provider = new TestEntityDataProvider(fakeDb);
            TestEntity entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };

            String expectedSql = "INSERT INTO TestTable ( TestColumn1,TestColumn2,TestColumn3,TestColumn4,TestColumn5,TestColumn6,TestColumn7,TestColumn8 ) " +
                "VALUES ( @PN_IDX_0,@PN_IDX_1,@PN_IDX_2,@PN_IDX_3,@PN_IDX_4,@PN_IDX_5,@PN_IDX_6,@PN_IDX_7 )";
            DataParameter[] expectedParameter = new DataParameter[8]
            {
                DataParameter.InternalCreate(fakeDb, "TestColumn1", 0, entity.Test1),
                DataParameter.InternalCreate(fakeDb, "TestColumn2", 1, entity.Test2),
                DataParameter.InternalCreate(fakeDb, "TestColumn3", 2, entity.Test3),
                DataParameter.InternalCreate(fakeDb, "TestColumn4", 3, entity.Test4),
                DataParameter.InternalCreate(fakeDb, "TestColumn5", 4, DbType.Int32, entity.Test5),
                DataParameter.InternalCreate(fakeDb, "TestColumn6", 5, DbType.Double, entity.Test6),
                DataParameter.InternalCreate(fakeDb, "TestColumn7", 6, DbType.DateTime, entity.Test7),
                DataParameter.InternalCreate(fakeDb, "TestColumn8", 7, DbType.Int16, entity.Test8)
            };

            InsertCommand cmd = fakeDb.CreateInsertCommand(provider.TableName).Set(entity);
            String actualSql = cmd.GetCommandText().Trim();
            DataParameter[] actualParameter = cmd.GetAllParameters();

            Assert.AreEqual(expectedSql, actualSql);

            for (Int32 i = 0; i < actualParameter.Length; i++)
            {
                Assert.AreEqual(expectedParameter[i], actualParameter[i]);
            }
        }

        [TestMethod()]
        public void LinqInsertTest()
        {
            AbstractDatabase fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient") as AbstractDatabase;
            TestEntityDataProvider provider = new TestEntityDataProvider(fakeDb);
            TestEntity entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };

            String expectedSql = "INSERT INTO TestTable ( TestColumn1,TestColumn2,TestColumn3,TestColumn4,TestColumn5,TestColumn6,TestColumn7,TestColumn8 ) " +
                "VALUES ( @PN_IDX_0,@PN_IDX_1,@PN_IDX_2,@PN_IDX_3,@PN_IDX_4,@PN_IDX_5,@PN_IDX_6,@PN_IDX_7 )";
            DataParameter[] expectedParameter = new DataParameter[8]
            {
                DataParameter.InternalCreate(fakeDb, "TestColumn1", 0, entity.Test1),
                DataParameter.InternalCreate(fakeDb, "TestColumn2", 1, entity.Test2),
                DataParameter.InternalCreate(fakeDb, "TestColumn3", 2, entity.Test3),
                DataParameter.InternalCreate(fakeDb, "TestColumn4", 3, entity.Test4),
                DataParameter.InternalCreate(fakeDb, "TestColumn5", 4, DbType.Int32, entity.Test5),
                DataParameter.InternalCreate(fakeDb, "TestColumn6", 5, DbType.Double, entity.Test6),
                DataParameter.InternalCreate(fakeDb, "TestColumn7", 6, DbType.DateTime, entity.Test7),
                DataParameter.InternalCreate(fakeDb, "TestColumn8", 7, DbType.Int16, entity.Test8)
            };

            InsertCommand cmd = fakeDb.CreateInsertCommand(provider.TableName)
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
    }
}
