using System;
using System.Data.Common;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Linq;
using DotMaysWind.Data.Orm;

namespace DotMaysWind.Data.PerformanceTest
{
    public class TestEntityRepository : DatabaseTable<TestEntity>
    {
        public TestEntityRepository()
            : base(DbHelper.FakeDb) { }

        public DbCommand SelectTest()
        {
            SelectCommand command = this.Select()
                .Querys<TestEntity>(c => new { c.Test1, c.Test2, c.Test5, c.Test8 })
                .Query<TestEntity>(c => c.Test3, "TTTT")
                .Query<TestEntity>(c => c.Test4, SqlAggregateFunction.Max, "MMMM")
                .Where<TestEntity>(c => c.Test2 >= 123 || (c.Test5 > 1 && c.Test5 < 10))
                .GroupBy<TestEntity>(c => c.Test3)
                .InnerJoin<TestEntity, TestEntity>(c => c.Test2, d => d.Test2)
                .OrderBy<TestEntity>(c => c.Test6, SqlOrderType.Asc);

            return command.ToDbCommand();
        }

        public DbCommand InsertTestByEntity(TestEntity entity)
        {
            InsertCommand command = this.Insert().Set(entity);

            return command.ToDbCommand();
        }

        public DbCommand InsertTestByLinq(TestEntity entity)
        {
            InsertCommand command = this.Insert()
                .Set<TestEntity>(c => c.Test1, entity.Test1)
                .Set<TestEntity>(c => c.Test2, entity.Test2)
                .Set<TestEntity>(c => c.Test3, entity.Test3)
                .Set<TestEntity>(c => c.Test4, entity.Test4)
                .Set<TestEntity>(c => c.Test5, entity.Test5)
                .Set<TestEntity>(c => c.Test6, entity.Test6)
                .Set<TestEntity>(c => c.Test7, entity.Test7)
                .Set<TestEntity>(c => c.Test8, entity.Test8);

            return command.ToDbCommand();
        }
    }
}