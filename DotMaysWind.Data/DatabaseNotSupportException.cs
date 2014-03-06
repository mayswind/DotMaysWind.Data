using System;

namespace DotMaysWind.Data
{
    /// <summary>
    /// 数据库不支持调用方法异常
    /// </summary>
    [Serializable]
    public class DatabaseNotSupportException : NotSupportedException
    {
        #region 构造方法
        /// <summary>
        /// 初始化新的数据库不支持调用方法异常
        /// </summary>
        public DatabaseNotSupportException() { }

        /// <summary>
        /// 初始化新的数据库不支持调用方法异常
        /// </summary>
        /// <param name="message">指定错误信息</param>
		public DatabaseNotSupportException(String message)
            : base(message) { }

        /// <summary>
        /// 初始化新的数据库不支持调用方法异常
        /// </summary>
        /// <param name="message">指定错误信息</param>
        /// <param name="args">格式化参数组</param>
        public DatabaseNotSupportException(String message, params Object[] args)
            : base(String.Format(message, args)) { }

        /// <summary>
        /// 初始化新的数据库不支持调用方法异常
        /// </summary>
        /// <param name="message">指定错误信息</param>
        /// <param name="innerException">导致当前异常的异常</param>
        public DatabaseNotSupportException(String message, Exception innerException)
            : base(message, innerException) { }
        #endregion
    }
}