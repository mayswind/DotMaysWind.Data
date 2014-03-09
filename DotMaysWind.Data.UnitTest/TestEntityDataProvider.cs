using System;

using DotMaysWind.Data.Orm;

namespace DotMaysWind.Data.UnitTest
{
    public class TestEntityDataProvider : DatabaseTable<TestEntity>
    {
        public TestEntityDataProvider(Database db)
            : base(db) { }
    }
}