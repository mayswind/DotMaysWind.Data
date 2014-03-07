using System;

namespace DotMaysWind.Data.Orm
{
    /// <summary>
    /// 无特性异常
    /// </summary>
    [Serializable]
    public class NullAttributeException : NullReferenceException
    {
        #region 构造方法
        /// <summary>
        /// 初始化新的无特性异常
        /// </summary>
        public NullAttributeException() { }

        /// <summary>
        /// 初始化新的无特性异常
        /// </summary>
        /// <param name="message">指定错误信息</param>
		public NullAttributeException(String message)
            : base(message) { }

        /// <summary>
        /// 初始化新的无特性异常
        /// </summary>
        /// <param name="message">指定错误信息</param>
        /// <param name="args">格式化参数组</param>
        public NullAttributeException(String message, params Object[] args)
            : base(String.Format(message, args)) { }

        /// <summary>
        /// 初始化新的无特性异常
        /// </summary>
        /// <param name="message">指定错误信息</param>
        /// <param name="innerException">导致当前异常的异常</param>
        public NullAttributeException(String message, Exception innerException)
            : base(message, innerException) { }
        #endregion
    }
}