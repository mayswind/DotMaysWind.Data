using System;

namespace DotMaysWind.Data.Linq
{
    /// <summary>
    /// 表达式无效异常
    /// </summary>
    [Serializable]
    public class ExpressionInvalidException : Exception
    {
        #region 构造方法
        /// <summary>
        /// 初始化新的表达式无效异常
        /// </summary>
        public ExpressionInvalidException() { }

        /// <summary>
        /// 初始化新的表达式无效异常
        /// </summary>
        /// <param name="message">指定错误信息</param>
		public ExpressionInvalidException(String message)
            : base(message) { }

        /// <summary>
        /// 初始化新的表达式无效异常
        /// </summary>
        /// <param name="message">指定错误信息</param>
        /// <param name="args">格式化参数组</param>
        public ExpressionInvalidException(String message, params Object[] args)
            : base(String.Format(message, args)) { }

        /// <summary>
        /// 初始化新的表达式无效异常
        /// </summary>
        /// <param name="message">指定错误信息</param>
        /// <param name="innerException">导致当前异常的异常</param>
        public ExpressionInvalidException(String message, Exception innerException)
            : base(message, innerException) { }
        #endregion
    }
}