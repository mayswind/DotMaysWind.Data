using System;

namespace DotMaysWind.Data.Orm
{
    /// <summary>
    /// 数据库表特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class DatabaseTableAtrribute : Attribute
    {
        #region 字段
        private String _tableName;
        #endregion

        #region 属性
        /// <summary>
        /// 获取数据库表名
        /// </summary>
        public String TableName
        {
            get { return this._tableName; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化数据库表特性
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        public DatabaseTableAtrribute(String tableName)
        {
            this._tableName = tableName;
        }
        #endregion
    }
}