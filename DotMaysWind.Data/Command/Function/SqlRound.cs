using System;

namespace DotMaysWind.Data.Command.Function
{
    /// <summary>
    /// Sql取整函数类
    /// </summary>
    public class SqlRound : AbstractSqlBasicFunction
    {
        #region 字段
        private Int32 _decimals;
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化Sql取整函数类
        /// </summary>
        /// <param name="parameter">函数参数</param>
        /// <param name="decimals">小数位数</param>
        public SqlRound(String parameter, Int32 decimals)
            : base(parameter)
        {
            this._decimals = decimals;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取函数拼接后字符串
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns>函数拼接后字符串</returns>
        public override String ToString(DatabaseType dbType)
        {
            return String.Format("LEN({0}, {1})", this._parameter, this._decimals.ToString());
        }
        #endregion
    }
}