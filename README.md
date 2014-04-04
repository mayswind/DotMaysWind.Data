DotMaysWind.Data
================

非常轻量级的数据库访问工具库，封装了Access、SQLite、SQL Server CE、SQL Server、MySQL以及Oracle，使用统一的代码构建SQL命令实现数据库无缝切换，提高开发效率。

自己参与和负责的项目一直使用该类库操作数据库，目前并将长期处于alpha或beta版。

在Web.config或App.config中配置好连接字符串：
    <connectionStrings>
    <add name="MainDatabase" connectionString="Data Source=SERVER_ADDRESS;Initial Catalog=DATABASE_NAME;Pooling=true;User ID='USERNAME';Password='PASSWORD';" providerName="System.Data.SqlClient"/>
    </connectionStrings>

Orm支持两种风格：
1、手写代码风格：
    using System;
    using System.Collections.Generic;
    using System.Data;

    using DotMaysWind.Data;
    using DotMaysWind.Data.Orm;

    public class User
    {
        public Int32 UserID { get; set; }
        public String UserName { get; set; }
    }

    public class UserDataProvider : AbstractDatabaseTable<User>
    {
        private const String UserIDColumn = "UserID";
        private const String UserNameColumn = "UserName";

        public UserDataProvider()
            : base(MainDatabase.Instance) { }

        public override String TableName
        {
            get { return "tbl_Users"; }
        }

        protected override User CreateEntity(DataRow row, DataColumnCollection columns)
        {
            User entity = new User();

            entity.UserID = this.LoadInt32(row, columns, UserIDColumn);
            entity.UserName = this.LoadString(row, columns, UserNameColumn);

            return entity;
        }

        public Boolean InsertEntity(User user)
        {
            return this.Insert()
                .Add(UserIDColumn, user.UserID)
                .Add(UserNameColumn, user.UserName)
                .Result() > 0;
        }

        public Boolean UpdateEntity(User user)
        {
            return this.Update()
                .Set(UserNameColumn, user.UserName)
                .Where(c => c.Equal(UserIDColumn, user.UserID))
                .Result() > 0;
        }

        public Boolean DeleteEntity(Int32 userID)
        {
            return this.Delete()
                .Where(c => c.Equal(UserIDColumn, userID))
                .Result() > 0;
        }

        public List<User> GetAllEntities()
        {
            return this.Select()
                .Querys(UserIDColumn, UserNameColumn)
                .ToEntityList<User>(this);
        }
    }

    internal static class MainDatabase
    {
        private static IDatabase _database;

        internal static IDatabase Instance
        {
            get { return _database; }
        }

        static MainDatabase()
        {
            _database = DatabaseFactory.CreateDatabase("MainDatabase");
        }
    }

2、特性代码风格：
    using System;
    using System.Collections.Generic;

    using DotMaysWind.Data;
    using DotMaysWind.Data.Linq;
    using DotMaysWind.Data.Orm;

    [DatabaseTable("tbl_Users")]
    public class User
    {
        [DatabaseColumn("UserID")]
        public Int32 UserID { get; set; }

        [DatabaseColumn("UserName")]
        public String UserName { get; set; }
    }

    public class UserDataProvider : DatabaseTable<User>
    {
        public UserDataProvider()
            : base(MainDatabase.Instance) { }

        public Boolean InsertEntity(User user)
        {
            return this.Insert()
                .Add(user)
                .Result() > 0;
        }

        public Boolean UpdateEntity(User user)
        {
            return this.Update()
                .Set<User>(c => user.UserName, user.UserName)
                .Where<User>(c => c.UserID == user.UserID)
                .Result() > 0;
        }

        public Boolean DeleteEntity(Int32 userID)
        {
            return this.Delete()
                .Where<User>(c => c.UserID == userID)
                .Result() > 0;
        }

        public List<User> GetAllEntities()
        {
            return this.Select()
                .Querys<User>(c => new { c.UserID, c.UserName })
                .ToEntityList<User>(this);
        }
    }

    internal static class MainDatabase
    {
        private static IDatabase _database;

        internal static IDatabase Instance
        {
            get { return _database; }
        }

        static MainDatabase()
        {
            _database = DatabaseFactory.CreateDatabase("MainDatabase");
        }
    }
