using System;
using System.Data;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Linq;
using DotMaysWind.Data.Orm;

namespace DotMaysWind.Data.UnitTest
{
    /// <summary>
    /// Orm单元测试
    ///</summary>
    [TestClass()]
    public class OrmTest
    {
        [DatabaseTableAtrribute("TestTable")]
        private class TestEntity
        {
            [DatabaseColumnAtrribute("Test1")]
            public String Test1 { get; set; }

            [DatabaseColumnAtrribute("Test2")]
            public Int32 Test2 { get; set; }

            [DatabaseColumnAtrribute("Test3")]
            public Double Test3 { get; set; }

            [DatabaseColumnAtrribute("Test4")]
            public DateTime Test4 { get; set; }

            [DatabaseColumnAtrribute("Test5")]
            public Int32? Test5 { get; set; }

            [DatabaseColumnAtrribute("Test6")]
            public Double? Test6 { get; set; }

            [DatabaseColumnAtrribute("Test7")]
            public DateTime? Test7 { get; set; }

            [DatabaseColumnAtrribute("Test8", DbType.Int16)]
            public Int32 Test8 { get; set; }
        }

        private class TestEntityDataProvider : DatabaseTable<TestEntity>
        {
            public TestEntityDataProvider(Database db)
                : base(db) { }

            public void TestInsert(TestEntity entity, out String sql, out SqlParameter[] parameters)
            {
                InsertCommand cmd = this.Insert().Add(entity);

                sql = cmd.GetSqlCommand().Trim();
                parameters = cmd.GetAllParameters().ToArray();
            }

            public void TestInsert2(TestEntity entity, out String sql, out SqlParameter[] parameters)
            {
                InsertCommand cmd = this.Insert()
                    .Add<TestEntity>(c => c.Test1, entity.Test1)
                    .Add<TestEntity>(c => c.Test2, entity.Test2)
                    .Add<TestEntity>(c => c.Test3, entity.Test3)
                    .Add<TestEntity>(c => c.Test4, entity.Test4)
                    .Add<TestEntity>(c => c.Test5, entity.Test5)
                    .Add<TestEntity>(c => c.Test6, entity.Test6)
                    .Add<TestEntity>(c => c.Test7, entity.Test7)
                    .Add<TestEntity>(c => c.Test8, entity.Test8);

                sql = cmd.GetSqlCommand().Trim();
                parameters = cmd.GetAllParameters().ToArray();
            }

            public void TestUpdate(TestEntity entity, out String sql, out SqlParameter[] parameters)
            {
                UpdateCommand cmd = this.Update().Set(entity);

                sql = cmd.GetSqlCommand().Trim();
                parameters = cmd.GetAllParameters().ToArray();
            }

            public void TestUpdate2(TestEntity entity, out String sql, out SqlParameter[] parameters)
            {
                UpdateCommand cmd = this.Update()
                    .Set<TestEntity>(c => c.Test1, entity.Test1)
                    .Set<TestEntity>(c => c.Test2, entity.Test2)
                    .Set<TestEntity>(c => c.Test3, entity.Test3)
                    .Set<TestEntity>(c => c.Test4, entity.Test4)
                    .Set<TestEntity>(c => c.Test5, entity.Test5)
                    .Set<TestEntity>(c => c.Test6, entity.Test6)
                    .Set<TestEntity>(c => c.Test7, entity.Test7)
                    .Set<TestEntity>(c => c.Test8, entity.Test8);

                sql = cmd.GetSqlCommand().Trim();
                parameters = cmd.GetAllParameters().ToArray();
            }
        }

        [TestMethod()]
        public void InsertCommandTest()
        {
            Database fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient");
            TestEntityDataProvider provider = new TestEntityDataProvider(fakeDb);
            TestEntity entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };

            String expectedSql = "INSERT INTO TestTable ( Test1,Test2,Test3,Test4,Test5,Test6,Test7,Test8 ) " +
                "VALUES ( @PN_NEW_Test1,@PN_NEW_Test2,@PN_NEW_Test3,@PN_NEW_Test4,@PN_NEW_Test5,@PN_NEW_Test6,@PN_NEW_Test7,@PN_NEW_Test8 )";
            SqlParameter[] expectedParameter = new SqlParameter[8]
            {
                SqlParameter.Create("Test1", "NEW_Test1", entity.Test1),
                SqlParameter.Create("Test2", "NEW_Test2", entity.Test2),
                SqlParameter.Create("Test3", "NEW_Test3", entity.Test3),
                SqlParameter.Create("Test4", "NEW_Test4", entity.Test4),
                SqlParameter.Create("Test5", "NEW_Test5", entity.Test5),
                SqlParameter.Create("Test6", "NEW_Test6", entity.Test6),
                SqlParameter.Create("Test7", "NEW_Test7", entity.Test7),
                SqlParameter.Create("Test8", "NEW_Test8", DbType.Int16, entity.Test8),
            };

            String actualSql = "";
            SqlParameter[] actualParameter = null;
            provider.TestInsert(entity, out actualSql, out actualParameter);

            String actualSql2 = "";
            SqlParameter[] actualParameter2 = null;
            provider.TestInsert2(entity, out actualSql2, out actualParameter2);

            Assert.AreEqual(expectedSql, actualSql);
            Assert.AreEqual(expectedSql, actualSql2);

            for (Int32 i = 0; i < actualParameter.Length; i++)
            {
                Assert.AreEqual(expectedParameter[i], actualParameter[i]);
                Assert.AreEqual(expectedParameter[i], actualParameter2[i]);
            }
        }

        [TestMethod()]
        public void UpdateCommandTest()
        {
            Database fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient");
            TestEntityDataProvider provider = new TestEntityDataProvider(fakeDb);
            TestEntity entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };

            String expectedSql = "UPDATE TestTable SET Test1=@PN_OLD_Test1,Test2=@PN_OLD_Test2,Test3=@PN_OLD_Test3,Test4=@PN_OLD_Test4,Test5=@PN_OLD_Test5,Test6=@PN_OLD_Test6,Test7=@PN_OLD_Test7,Test8=@PN_OLD_Test8";
            SqlParameter[] expectedParameter = new SqlParameter[8]
            {
                SqlParameter.Create("Test1", "OLD_Test1", entity.Test1),
                SqlParameter.Create("Test2", "OLD_Test2", entity.Test2),
                SqlParameter.Create("Test3", "OLD_Test3", entity.Test3),
                SqlParameter.Create("Test4", "OLD_Test4", entity.Test4),
                SqlParameter.Create("Test5", "OLD_Test5", entity.Test5),
                SqlParameter.Create("Test6", "OLD_Test6", entity.Test6),
                SqlParameter.Create("Test7", "OLD_Test7", entity.Test7),
                SqlParameter.Create("Test8", "OLD_Test8", DbType.Int16, entity.Test8),
            };

            String actualSql = "";
            SqlParameter[] actualParameter = null;
            provider.TestUpdate(entity, out actualSql, out actualParameter);

            String actualSql2 = "";
            SqlParameter[] actualParameter2 = null;
            provider.TestUpdate2(entity, out actualSql2, out actualParameter2);

            Assert.AreEqual(expectedSql, actualSql);
            Assert.AreEqual(expectedSql, actualSql2);

            for (Int32 i = 0; i < actualParameter.Length; i++)
            {
                Assert.AreEqual(expectedParameter[i], actualParameter[i]);
                Assert.AreEqual(expectedParameter[i], actualParameter2[i]);
            }
        }
    }
}