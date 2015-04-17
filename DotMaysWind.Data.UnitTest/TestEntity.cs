using System;

using DotMaysWind.Data.Orm;

namespace DotMaysWind.Data.UnitTest
{
    [DatabaseTableAttribute("TestTable")]
    public class TestEntity
    {
        [DatabaseColumnAttribute("TestColumn1")]
        public String Test1 { get; set; }

        [DatabaseColumnAttribute("TestColumn2")]
        public Int32 Test2 { get; set; }

        [DatabaseColumnAttribute("TestColumn3")]
        public Double Test3 { get; set; }

        [DatabaseColumnAttribute("TestColumn4")]
        public DateTime Test4 { get; set; }

        [DatabaseColumnAttribute("TestColumn5")]
        public Int32? Test5 { get; set; }

        [DatabaseColumnAttribute("TestColumn6")]
        public Double? Test6 { get; set; }

        [DatabaseColumnAttribute("TestColumn7")]
        public DateTime? Test7 { get; set; }

        [DatabaseColumnAttribute("TestColumn8", DataType.Int16)]
        public Int32 Test8 { get; set; }
    }
}