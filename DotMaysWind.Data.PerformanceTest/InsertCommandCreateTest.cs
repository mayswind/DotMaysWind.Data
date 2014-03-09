using System;
using System.Data;
using System.Data.Common;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Command.Condition;
using DotMaysWind.Data.Linq;
using DotMaysWind.Data.Orm;

namespace DotMaysWind.Data.PerformanceTest
{
    /// <summary>
    /// 插入语句创建
    /// </summary>
    internal static class InsertCommandCreateTest
    {
        #region 字段
        private static Database _fakeDb;
        private static TestEntityDataProvider _provider;
        private static TestEntity _entity;
        #endregion

        #region 构造方法
        static InsertCommandCreateTest()
        {
            _fakeDb = DatabaseFactory.CreateDatabase("", "System.Data.SqlClient");
            _provider = new TestEntityDataProvider(_fakeDb);

            _entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };
        }
        #endregion

        #region 测试方法
        internal static void DatabaseNormalCreateInsertCommand()
        {
            SqlParameter[] insertParameters = new SqlParameter[8]
            {
                SqlParameter.Create("TestColumn1", "NEW_TestColumn1", _entity.Test1),
                SqlParameter.Create("TestColumn2", "NEW_TestColumn2", _entity.Test2),
                SqlParameter.Create("TestColumn3", "NEW_TestColumn3", _entity.Test3),
                SqlParameter.Create("TestColumn4", "NEW_TestColumn4", _entity.Test4),
                SqlParameter.Create("TestColumn5", "NEW_TestColumn5", DbType.Int32, _entity.Test5),
                SqlParameter.Create("TestColumn6", "NEW_TestColumn6", DbType.Double, _entity.Test6),
                SqlParameter.Create("TestColumn7", "NEW_TestColumn7", DbType.DateTime, _entity.Test7),
                SqlParameter.Create("TestColumn8", "NEW_TestColumn8", DbType.Int16, _entity.Test8),
            };

            DbCommand dbCommand = _fakeDb.CreateInsertCommand(_provider.TableName).Add(insertParameters).ToDbCommand();
        }

        internal static void DatabaseEntityCreateInsertCommand()
        {
            DbCommand dbCommand = _fakeDb.CreateInsertCommand(_provider.TableName).Add(_entity).ToDbCommand();
        }

        internal static void ProviderEntityCreateInsertCommand()
        {
            DbCommand dbCommand = _provider.InsertTestByEntity(_entity);
        }

        internal static void DatabaseLinqCreateInsertCommand()
        {
            InsertCommand command = _fakeDb.CreateInsertCommand(_provider.TableName)
                .Add<TestEntity>(c => c.Test1, _entity.Test1)
                .Add<TestEntity>(c => c.Test2, _entity.Test2)
                .Add<TestEntity>(c => c.Test3, _entity.Test3)
                .Add<TestEntity>(c => c.Test4, _entity.Test4)
                .Add<TestEntity>(c => c.Test5, _entity.Test5)
                .Add<TestEntity>(c => c.Test6, _entity.Test6)
                .Add<TestEntity>(c => c.Test7, _entity.Test7)
                .Add<TestEntity>(c => c.Test8, _entity.Test8);

            DbCommand dbCommand = command.ToDbCommand();
        }

        internal static void ProviderLinqCreateInsertCommand()
        {
            DbCommand dbCommand = _provider.InsertTestByLinq(_entity);
        }
        #endregion
    }
}