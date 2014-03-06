using System;

namespace DotMaysWind.Data.Linq
{
    /// <summary>
    /// Linq操作不支持调用方法异常
    /// </summary>
    [Serializable]
    public class LinqNotSupportedException : NotSupportedException
    {
        #region 构造方法
        /// <summary>
        /// 初始化新的Linq操作调用方法异常
        /// </summary>
        public LinqNotSupportedException() { }

        /// <summary>
        /// 初始化新的Linq操作调用方法异常
        /// </summary>
        /// <param name="message">指定错误信息</param>
		public LinqNotSupportedException(String message)
            : base(message) { }

        /// <summary>
        /// 初始化新的Linq操作调用方法异常
        /// </summary>
        /// <param name="message">指定错误信息</param>
        /// <param name="args">格式化参数组</param>
        public LinqNotSupportedException(String message, params Object[] args)
            : base(String.Format(message, args)) { }

        /// <summary>
        /// 初始化新的Linq操作调用方法异常
        /// </summary>
        /// <param name="message">指定错误信息</param>
        /// <param name="innerException">导致当前异常的异常</param>
        public LinqNotSupportedException(String message, Exception innerException)
            : base(message, innerException) { }
        #endregion
    }
}