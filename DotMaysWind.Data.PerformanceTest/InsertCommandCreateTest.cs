using System;
using System.Data;
using System.Data.Common;

using DotMaysWind.Data.Command;
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
        private static TestEntityDataProvider _provider;
        private static TestEntity _entity;
        #endregion

        #region 构造方法
        static InsertCommandCreateTest()
        {
            _provider = new TestEntityDataProvider();
            _entity = new TestEntity() { Test1 = "1", Test2 = 2, Test3 = 3.0, Test4 = DateTime.Now, Test8 = 8 };
        }
        #endregion

        #region 测试方法
        internal static void BaseCreateInsertCommand()
        {
            String sql = "INSERT INTO TestTable (TestColumn1,TestColumn2,TestColumn3,TestColumn4,TestColumn5,TestColumn6,TestColumn7,TestColumn8) VALUES (@PN_NEW_TestColumn1,@PN_NEW_TestColumn2,@PN_NEW_TestColumn3,@PN_NEW_TestColumn4,@PN_NEW_TestColumn5,@PN_NEW_TestColumn6,@PN_NEW_TestColumn7,@PN_NEW_TestColumn8)";

            DbCommand dbCommand = DbHelper.FakeDb.CreateDbCommand();
            dbCommand.CommandText = sql;
            dbCommand.Parameters.Add(DbHelper.InternalCreateDbParameter("TestColumn1", "@PN_NEW_TestColumn1", DbType.String, _entity.Test1));
            dbCommand.Parameters.Add(DbHelper.InternalCreateDbParameter("TestColumn2", "@PN_NEW_TestColumn2", DbType.Int32, _entity.Test2));
            dbCommand.Parameters.Add(DbHelper.InternalCreateDbParameter("TestColumn3", "@PN_NEW_TestColumn3", DbType.Double, _entity.Test3));
            dbCommand.Parameters.Add(DbHelper.InternalCreateDbParameter("TestColumn4", "@PN_NEW_TestColumn4", DbType.DateTime, _entity.Test4));
            dbCommand.Parameters.Add(DbHelper.InternalCreateDbParameter("TestColumn5", "@PN_NEW_TestColumn5", DbType.Int32, _entity.Test5));
            dbCommand.Parameters.Add(DbHelper.InternalCreateDbParameter("TestColumn6", "@PN_NEW_TestColumn6", DbType.Double, _entity.Test6));
            dbCommand.Parameters.Add(DbHelper.InternalCreateDbParameter("TestColumn7", "@PN_NEW_TestColumn7", DbType.DateTime, _entity.Test7));
            dbCommand.Parameters.Add(DbHelper.InternalCreateDbParameter("TestColumn8", "@PN_NEW_TestColumn8", DbType.Int16, _entity.Test8));
        }

        internal static void DatabaseNormalCreateInsertCommand()
        {
            InsertCommand command = DbHelper.FakeDb.CreateInsertCommand(_provider.TableName)
                .Set("TestColumn1", _entity.Test1)
                .Set("TestColumn2", _entity.Test2)
                .Set("TestColumn3", _entity.Test3)
                .Set("TestColumn4", _entity.Test4)
                .Set("TestColumn5", _entity.Test5)
                .Set("TestColumn6", _entity.Test6)
                .Set("TestColumn7", _entity.Test7)
                .Set("TestColumn8", DataType.Int16, _entity.Test8);

            DbCommand dbCommand = command.ToDbCommand();
        }

        internal static void DatabaseEntityCreateInsertCommand()
        {
            DbCommand dbCommand = DbHelper.FakeDb.CreateInsertCommand(_provider.TableName).Set(_entity).ToDbCommand();
        }

        internal static void ProviderEntityCreateInsertCommand()
        {
            DbCommand dbCommand = _provider.InsertTestByEntity(_entity);
        }

        internal static void DatabaseLinqCreateInsertCommand()
        {
            InsertCommand command = DbHelper.FakeDb.CreateInsertCommand(_provider.TableName)
                .Set<TestEntity>(c => c.Test1, _entity.Test1)
                .Set<TestEntity>(c => c.Test2, _entity.Test2)
                .Set<TestEntity>(c => c.Test3, _entity.Test3)
                .Set<TestEntity>(c => c.Test4, _entity.Test4)
                .Set<TestEntity>(c => c.Test5, _entity.Test5)
                .Set<TestEntity>(c => c.Test6, _entity.Test6)
                .Set<TestEntity>(c => c.Test7, _entity.Test7)
                .Set<TestEntity>(c => c.Test8, _entity.Test8);

            DbCommand dbCommand = command.ToDbCommand();
        }

        internal static void ProviderLinqCreateInsertCommand()
        {
            DbCommand dbCommand = _provider.InsertTestByLinq(_entity);
        }
        #endregion
    }
}