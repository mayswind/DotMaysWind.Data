using System;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql自定义语句类
    /// </summary>
    public sealed class CustomCommand : AbstractSqlCommand
    {
        #region 字段
        private SqlCommandType _commandType;
        private String _commandText;
        #endregion

        #region 属性
        /// <summary>
        /// 获取语句类型
        /// </summary>
        public override SqlCommandType CommandType
        {
            get { return this._commandType; }
        }

        /// <summary>
        /// 不支持获取或设置数据表名
        /// </summary>
        /// <exception cref="NotSupportedException">不支持获取或设置数据表名</exception>
        public new String TableName
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的Sql自定义语句类
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="commandType">语句类型</param>
        /// <param name="commandString">语句内容</param>
        internal CustomCommand(AbstractDatabase database, SqlCommandType commandType, String commandString)
            : base(database, null, String.Empty)
        {
            this._commandType = commandType;
            this._commandText = commandString;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 增加指定参数组并返回当前语句
        /// </summary>
        /// <param name="parameters">要增加的参数组</param>
        /// <returns>当前语句</returns>
        public CustomCommand AddParameters(params DataParameter[] parameters)
        {
            if (parameters != null)
            {
                this._parameters.AddRange(parameters);
            }

            return this;
        }

        /// <summary>
        /// 设置Sql语句并返回当前语句
        /// </summary>
        /// <param name="commandText">语句内容</param>
        /// <returns>当前语句</returns>
        public CustomCommand SetCommandText(String commandText)
        {
            this._commandText = commandText;
            return this;
        }

        /// <summary>
        /// 获取Sql语句内容
        /// </summary>
        /// <returns>Sql语句内容</returns>
        public override String GetCommandText()
        {
            return this._commandText;
        }
        #endregion
    }
}