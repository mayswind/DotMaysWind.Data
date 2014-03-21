using System;
using System.Collections.Generic;
using System.Text;

namespace DotMaysWind.Data.Command.Function
{
    /// <summary>
    /// Sql复杂函数类
    /// </summary>
    public class SqlComplexFunction : ISqlFunction
    {
        #region 字段
        private AbstractSqlBasicFunction _basicFunc;
        private String _leftMethod;
        private String _rightMethod;
        #endregion

        #region 属性
        /// <summary>
        /// 获取是否需要提交参数
        /// </summary>
        public Boolean HasParameters
        {
            get { return this._basicFunc.HasParameters; }
        }

        /// <summary>
        /// 获取Sql基础函数
        /// </summary>
        public AbstractSqlBasicFunction Function
        {
            get { return this._basicFunc; }
        }

        /// <summary>
        /// 获取左侧增加的内容
        /// </summary>
        public String LeftMethod
        {
            get { return this._leftMethod; }
        }

        /// <summary>
        /// 获取右侧增加的内容
        /// </summary>
        public String RightMethod
        {
            get { return this._rightMethod; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的Sql复杂函数
        /// </summary>
        /// <param name="basicFunc">Sql基础函数</param>
        /// <param name="leftMethod">左边增加的内容</param>
        /// <param name="rightMethod">右边增加的内容</param>
        public SqlComplexFunction(AbstractSqlBasicFunction basicFunc, String leftMethod, String rightMethod)
        {
            this._basicFunc = basicFunc;
            this._leftMethod = leftMethod;
            this._rightMethod = rightMethod;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取查询函数参数集合
        /// </summary>
        /// <returns>查询函数参数集合</returns>
        public SqlParameter[] GetAllParameters()
        {
            return this._basicFunc.GetAllParameters();
        }

        /// <summary>
        /// 获取函数拼接后字符串
        /// </summary>
        /// <returns>函数拼接后字符串</returns>
        public String GetSqlText()
        {
            return String.Format("({0}{1}{2})", this._leftMethod, this._basicFunc.GetSqlText(), this._rightMethod);
        }
        #endregion
    }
}