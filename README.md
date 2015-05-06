DotMaysWind.Data
================

非常轻量级的数据库访问工具库，链式代码编写，封装了SQL语句以及各个数据库中常用的函数，支持Access、SQLite、SQL Server CE、SQL Server、MySQL以及Oracle数据库。

自己和小伙伴们的项目常使用该框架实现数据库操作，目前处于并将长期处于alpha或beta版。

在Web.config或App.config中配置好连接字符串：

    <connectionStrings>
    <add name="MainDatabase" connectionString="Data Source=SERVER_ADDRESS;Initial Catalog=DATABASE_NAME;Pooling=true;User ID='USERNAME';Password='PASSWORD';" providerName="System.Data.SqlClient"/>
    </connectionStrings>

DotMaysWind.Data支持以下两种方式：

1、伪·Orm风格：

    using System;
    using System.Collections.Generic;

    using DotMaysWind.Data;
    using DotMaysWind.Data.Orm;

    public class User
    {
        public Int32 UserID { get; set; }
        public String UserName { get; set; }
    }

    public class UserDataRepository : AbstractDatabaseTable<User>
    {
        private const String UserIDColumn = "UserID";
        private const String UserNameColumn = "UserName";

        public UserDataRepository()
            : base(MainDatabase.Instance) { }

        public override String TableName
        {
            get { return "tbl_Users"; }
        }

        protected override User CreateEntity(Object sender, EntityCreatingArgs args)
        {
            User entity = new User();

            entity.UserID = this.LoadInt32(args, UserIDColumn);
            entity.UserName = this.LoadString(args, UserNameColumn);

            return entity;
        }

        public Boolean InsertEntity(User user)
        {
            return this.Insert()
                .Set(UserIDColumn, user.UserID)
                .Set(UserNameColumn, user.UserName)
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

        public List<User> GetEntities(Int32 pageSize, Int32 pageIndex)
        {
            return this.Select()
                .Querys(UserIDColumn, UserNameColumn)
                .OrderByAsc(UserNameColumn)
                .Paged(pageSize, pageIndex)
                .ToEntityList<User>(this);
        }
        
        public Int32 CountAllEntities()
        {
            return this.Select().Count();
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

2、真·Orm风格：

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

    public class UserDataRepository : DatabaseTable<User>
    {
        public UserDataRepository()
            : base(MainDatabase.Instance) { }

        public Boolean InsertEntity(User user)
        {
            return this.Insert()
                .Set(user)
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

        public List<User> GetEntities(Int32 pageSize, Int32 pageIndex)
        {
            return this.Select()
                .Querys<User>(c => new { c.UserID, c.UserName })
                .OrderByAsc(c => c.UserName)
                .Paged(pageSize, pageIndex)
                .ToEntityList<User>(this);
        }
        
        public Int32 CountAllEntities()
        {
            return this.Select().Count();
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
