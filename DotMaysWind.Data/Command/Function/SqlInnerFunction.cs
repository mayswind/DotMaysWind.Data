using System;
using System.Collections.Generic;

namespace DotMaysWind.Data.Command.Function
{
    /// <summary>
    /// Sql内置函数类
    /// </summary>
    public class SqlInnerFunction : AbstractSqlBasicFunction
    {
        #region 字段
        private SqlInnerFunctionType _functionType;
        private String[] _parameters;
        #endregion

        #region 属性
        /// <summary>
        /// 获取函数类型
        /// </summary>
        public SqlInnerFunctionType FunctionType
        {
            get { return this._functionType; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的Sql内置函数
        /// </summary>
        /// <param name="baseDatabase">源数据库</param>
        /// <param name="funcType">函数类型</param>
        /// <param name="parameters">函数参数列表</param>
        internal SqlInnerFunction(AbstractDatabase baseDatabase, SqlInnerFunctionType funcType, params String[] parameters)
            : base(baseDatabase)
        {
            this._parameters = parameters;
            this._functionType = funcType;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取函数拼接后字符串
        /// </summary>
        /// <returns>函数拼接后字符串</returns>
        /// <exception cref="CommandNotSupportedException">指定函数不受当前数据库支持</exception>
        public override String GetCommandText()
        {
            switch (this._functionType)
            {
                case SqlInnerFunctionType.IsNull:
                    return this._baseDatabase.InternalGetIsNullFunction(this._parameters[0], this._parameters[1]);
                case SqlInnerFunctionType.Upper:
                    return this._baseDatabase.InternalGetUpperFunction(this._parameters[0]);
                case SqlInnerFunctionType.Lower:
                    return this._baseDatabase.InternalGetLowerFunction(this._parameters[0]);
                case SqlInnerFunctionType.LTrim:
                    return this._baseDatabase.InternalGetLTrimFunction(this._parameters[0]);
                case SqlInnerFunctionType.RTrim:
                    return this._baseDatabase.InternalGetRTrimFunction(this._parameters[0]);
                case SqlInnerFunctionType.Trim:
                    return this._baseDatabase.InternalGetTrimFunction(this._parameters[0]);
                case SqlInnerFunctionType.Length:
                    return this._baseDatabase.InternalGetLengthFunction(this._parameters[0]);
                case SqlInnerFunctionType.Mid:
                    return this._baseDatabase.InternalGetMidFunction(this._parameters[0], this._parameters[1], this._parameters[2]);
                case SqlInnerFunctionType.Round:
                    return this._baseDatabase.InternalGetRoundFunction(this._parameters[0], this._parameters[1]);
                case SqlInnerFunctionType.Now:
                    return this._baseDatabase.InternalGetNowFunction();
                case SqlInnerFunctionType.DatePart:
                    return this._baseDatabase.InternalGetDatePartFunction(this._parameters[0], this._parameters[1]);
                default:
                    throw new CommandNotSupportedException("The database does not support this function.");
            }
        }
        #endregion
    }
}