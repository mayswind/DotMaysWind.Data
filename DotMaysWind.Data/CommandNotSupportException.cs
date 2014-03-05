using System;

namespace DotMaysWind.Data
{
    /// <summary>
    /// Sql命令不支持调用方法异常
    /// </summary>
    [Serializable]
    public class CommandNotSupportException : NotSupportedException
    {
        #region 构造方法
        /// <summary>
        /// 初始化新的Sql命令不支持调用方法异常
        /// </summary>
        public CommandNotSupportException() { }

        /// <summary>
        /// 初始化新的Sql命令不支持调用方法异常
        /// </summary>
        /// <param name="message">指定错误信息</param>
		public CommandNotSupportException(String message)
            : base(message) { }

        /// <summary>
        /// 初始化新的Sql命令不支持调用方法异常
        /// </summary>
        /// <param name="message">指定错误信息</param>
        /// <param name="args">格式化参数组</param>
        public CommandNotSupportException(String message, params Object[] args)
            : base(String.Format(message, args)) { }

        /// <summary>
        /// 初始化新的Sql命令不支持调用方法异常
        /// </summary>
        /// <param name="message">指定错误信息</param>
        /// <param name="innerException">导致当前异常的异常</param>
        public CommandNotSupportException(String message, Exception innerException)
            : base(message, innerException) { }
        #endregion
    }
}