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
        /// 函数参数
        /// </summary>
        protected String _parameter;
        #endregion

        #region 属性
        /// <summary>
        /// 获取是否需要提交参数
        /// </summary>
        public Boolean HasParameters
        {
            get { return false; }
        }

        /// <summary>
        /// 获取函数参数
        /// </summary>
        public String Parameter
        {
            get { return this._parameter; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化Sql基本函数抽象类
        /// </summary>
        /// <param name="parameter">函数参数</param>
        protected AbstractSqlBasicFunction(String parameter)
        {
            this._parameter = parameter;
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
        /// <param name="dbType">数据库类型</param>
        /// <returns>函数拼接后字符串</returns>
        public abstract String GetSqlFunction(DatabaseType dbType);
        #endregion
    }
}