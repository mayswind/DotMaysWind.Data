using System;

using DotMaysWind.Data.Orm;

namespace DotMaysWind.Data.UnitTest
{
    public class TestEntityRepository : DatabaseTable<TestEntity>
    {
        public TestEntityRepository(IDatabase db)
            : base(db) { }
    }
}