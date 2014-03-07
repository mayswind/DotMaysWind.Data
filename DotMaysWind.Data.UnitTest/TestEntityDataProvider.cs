using System;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Linq;
using DotMaysWind.Data.Orm;

namespace DotMaysWind.Data.UnitTest
{
    public class TestEntityDataProvider : DatabaseTable<TestEntity>
    {
        public TestEntityDataProvider(Database db)
            : base(db) { }

        public void EntityInsert(TestEntity entity, out String sql, out SqlParameter[] parameters)
        {
            InsertCommand cmd = this.Insert().Add(entity);

            sql = cmd.GetSqlCommand().Trim();
            parameters = cmd.GetAllParameters().ToArray();
        }

        public void LinqInsert(TestEntity entity, out String sql, out SqlParameter[] parameters)
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

        public void EntityUpdate(TestEntity entity, out String sql, out SqlParameter[] parameters)
        {
            UpdateCommand cmd = this.Update().Set(entity);

            sql = cmd.GetSqlCommand().Trim();
            parameters = cmd.GetAllParameters().ToArray();
        }

        public void LinqUpdate(TestEntity entity, out String sql, out SqlParameter[] parameters)
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

        public void LinqIncrease(TestEntity entity, out String sql, out SqlParameter[] parameters)
        {
            UpdateCommand cmd = this.Update().Increase<TestEntity>(c => c.Test2);

            sql = cmd.GetSqlCommand().Trim();
            parameters = cmd.GetAllParameters().ToArray();
        }

        public void LinqDecrease(TestEntity entity, out String sql, out SqlParameter[] parameters)
        {
            UpdateCommand cmd = this.Update().Decrease<TestEntity>(c => c.Test2);

            sql = cmd.GetSqlCommand().Trim();
            parameters = cmd.GetAllParameters().ToArray();
        }
    }
}