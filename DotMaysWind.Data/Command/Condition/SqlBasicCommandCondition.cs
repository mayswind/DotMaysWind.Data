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

            return this._command.GetAllParameters();
        }

        /// <summary>
        /// 输出条件语句
        /// </summary>
        /// <returns>条件语句</returns>
        public override String GetSqlClause()
        {
            if (this._command == null)
            {
                return String.Empty;
            }

            String format = String.Format("({0})", SqlOperators.InternalGetOperatorFormat(this._operator));

            return String.Format(format, this._columnName, '(' + this._command.GetSqlCommand() + ')');
        }
        #endregion

        #region 重载方法和运算符
        /// <summary>
        /// 获取当前参数的哈希值
        /// </summary>
        /// <returns>当前参数的哈希值</returns>
        public override Int32 GetHashCode()
        {
            return this._columnName.GetHashCode();
        }

        /// <summary>
        /// 判断两个Sql简单条件语句是否相同
        /// </summary>
        /// <param name="obj">待比较的Sql简单条件语句</param>
        /// <returns>两个Sql简单条件语句是否相同</returns>
        public override Boolean Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            SqlBasicCommandCondition condition = obj as SqlBasicCommandCondition;

            if (condition == null)
            {
                return false;
            }

            if (this._operator != condition._operator)
            {
                return false;
            }

            if (!String.Equals(this._columnName, condition._columnName))
            {
                return false;
            }

            if ((this._command != null && condition._command == null) || (this._command == null && condition._command != null))
            {
                return false;
            }

            if (this._command != null && condition._command != null && !this._command.Equals(condition._command))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 判断两个Sql简单条件语句是否相同
        /// </summary>
        /// <param name="obj">待比较的Sql简单条件语句</param>
        /// <param name="obj2">待比较的第二个Sql简单条件语句</param>
        /// <returns>两个Sql简单条件语句是否相同</returns>
        public static Boolean operator ==(SqlBasicCommandCondition obj, SqlBasicCommandCondition obj2)
        {
            return Object.Equals(obj, obj2);
        }

        /// <summary>
        /// 判断两个Sql简单条件语句是否不同
        /// </summary>
        /// <param name="obj">待比较的Sql简单条件语句</param>
        /// <param name="obj2">待比较的第二个Sql简单条件语句</param>
        /// <returns>两个Sql简单条件语句是否不同</returns>
        public static Boolean operator !=(SqlBasicCommandCondition obj, SqlBasicCommandCondition obj2)
        {
            return !Object.Equals(obj, obj2);
        }
        #endregion
    }
}