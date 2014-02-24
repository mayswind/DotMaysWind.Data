using System;
using System.Collections.Generic;

namespace DotMaysWind.Data.Command.Condition
{
    /// <summary>
    /// Sql简单条件语句类
    /// </summary>
    public class SqlBasicCommandCondition : AbstractSqlCondition
    {
        #region 字段
        private String _columnName;
        private SqlOperator _operator;
        private SelectCommand _command;
        #endregion

        #region 属性
        /// <summary>
        /// 获取语句类型
        /// </summary>
        public override SqlConditionType ConditionType
        {
            get { return SqlConditionType.BasicCommandCondition; }
        }

        /// <summary>
        /// 获取字段名称
        /// </summary>
        public String ColumnName
        {
            get { return this._columnName; }
        }
        
        /// <summary>
        /// 获取Sql查询类型
        /// </summary>
        public SqlOperator Operator
        {
            get { return this._operator; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化Sql查询语句类
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="op">条件运算符</param>
        /// <param name="command">选择语句</param>
        internal SqlBasicCommandCondition(String columnName, SqlOperator op, SelectCommand command)
        {
            this._columnName = columnName;
            this._operator = op;
            this._command = command;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取条件语句包含的参数集合
        /// </summary>
        public override SqlParameter[] GetAllParameters()
        {
            if (this._command == null)
            {
                return null;
            }

            List<SqlParameter> parameters = this._command.GetAllParameters();

            return parameters.ToArray();
        }

        /// <summary>
        /// 输出条件语句
        /// </summary>
        /// <returns>条件语句</returns>
        public override String ToString()
        {
            if (this._command == null)
            {
                return String.Empty;
            }

            String format = String.Format("({0})", SqlOperators.InternalGetOperatorFormat(this._operator));

            return String.Format(format, this._columnName, '(' + this._command.ToString() + ')');
        }
        #endregion
    }
}