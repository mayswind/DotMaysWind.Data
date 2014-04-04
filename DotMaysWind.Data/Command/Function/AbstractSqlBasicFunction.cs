using System;

namespace DotMaysWind.Data.Command.Function
{
    /// <summary>
    /// Sql基本函数抽象类
    /// </summary>
    public abstract class AbstractSqlBasicFunction : ISqlFunction
    {
        #region 字段
        /// <summary>
        /// 源数据库
        /// </summary>
        protected AbstractDatabase _baseDatabase;
        #endregion

        #region 属性
        /// <summary>
        /// 获取是否需要提交参数
        /// </summary>
        public Boolean HasParameters
        {
            get { return false; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化Sql基本函数抽象
        /// </summary>
        /// <param name="baseDatabase">源数据库</param>
        protected AbstractSqlBasicFunction(AbstractDatabase baseDatabase)
        {
            this._baseDatabase = baseDatabase;
        }
        #endregion

        #region 运算符
        /// <summary>
        /// 对Sql基本函数增加内容
        /// </summary>
        /// <param name="function">Sql基本函数</param>
        /// <param name="extra">增加的内容</param>
        /// <returns>Sql复杂函数</returns>
        public static SqlComplexFunction operator +(AbstractSqlBasicFunction function, String extra)
        {
            return new SqlComplexFunction(function, String.Empty, extra);
        }

        /// <summary>
        /// 对Sql基本函数增加内容
        /// </summary>
        /// <param name="function">Sql基本函数</param>
        /// <param name="extra">增加的内容</param>
        /// <returns>Sql复杂函数</returns>
        public static SqlComplexFunction operator +(String extra, AbstractSqlBasicFunction function)
        {
            return new SqlComplexFunction(function, extra, String.Empty);
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取查询函数参数集合
        /// </summary>
        /// <returns>查询函数参数集合</returns>
        public SqlParameter[] GetAllParameters()
        {
            return null;
        }

        /// <summary>
        /// 获取函数拼接后字符串
        /// </summary>
        /// <returns>函数拼接后字符串</returns>
        public abstract String GetCommandText();
        #endregion
    }
}