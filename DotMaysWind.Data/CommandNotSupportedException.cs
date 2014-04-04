using System;

namespace DotMaysWind.Data
{
    /// <summary>
    /// 操作不被数据库支持异常
    /// </summary>
    [Serializable]
    public class CommandNotSupportedException : NotSupportedException
    {
        #region 构造方法
        /// <summary>
        /// 初始化新的操作不被数据库支持异常
        /// </summary>
        public CommandNotSupportedException() { }

        /// <summary>
        /// 初始化新的操作不被数据库支持异常
        /// </summary>
        /// <param name="message">指定错误信息</param>
		public CommandNotSupportedException(String message)
            : base(message) { }

        /// <summary>
        /// 初始化新的操作不被数据库支持异常
        /// </summary>
        /// <param name="message">指定错误信息</param>
        /// <param name="args">格式化参数组</param>
        public CommandNotSupportedException(String message, params Object[] args)
            : base(String.Format(message, args)) { }

        /// <summary>
        /// 初始化新的操作不被数据库支持异常
        /// </summary>
        /// <param name="message">指定错误信息</param>
        /// <param name="innerException">导致当前异常的异常</param>
        public CommandNotSupportedException(String message, Exception innerException)
            : base(message, innerException) { }
        #endregion
    }
}