using System;
using System.Collections.Generic;

namespace DotMaysWind.Data.Command.Condition
{
    /// <summary>
    /// Sql IN语句条件语句类
    /// </summary>
    public class SqlInsideCommandCondition : AbstractSqlCondition
    {
        #region 字段
        private String _columnName;
        private Boolean _isNotIn;
        private SelectCommand _command;
        #endregion

        #region 属性
        /// <summary>
        /// 获取语句类型
        /// </summary>
        public override SqlConditionType ConditionType
        {
            get { return SqlConditionType.InsideCommandCondition; }
        }

        /// <summary>
        /// 获取是否不在范围内
        /// </summary>
        public Boolean IsNotIn
        {
            get { return this._isNotIn; }
        }

        /// <summary>
        /// 获取字段名称
        /// </summary>
        public String ColumnName
        {
            get { return this._columnName; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化Sql IN语句条件语句类
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="command">选择语句</param>
        internal SqlInsideCommandCondition(String columnName, Boolean isNotIn, SelectCommand command)
        {
            this._columnName = columnName;
            this._isNotIn = isNotIn;
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
        public override String GetSqlClause()
        {
            if (this._command == null)
            {
                return String.Empty;
            }

            return String.Format("({0} {1} ({2}))", this._columnName, (this._isNotIn ? "NOT IN" : "IN"), this._command.GetSqlCommand());
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
        /// 判断两个Sql IN语句条件语句是否相同
        /// </summary>
        /// <param name="obj">待比较的Sql IN语句条件语句</param>
        /// <returns>两个Sql IN语句条件语句是否相同</returns>
        public override Boolean Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            SqlInsideCommandCondition condition = obj as SqlInsideCommandCondition;

            if (condition == null)
            {
                return false;
            }

            if (this._isNotIn != condition._isNotIn)
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
        /// 判断两个Sql IN语句条件语句是否相同
        /// </summary>
        /// <param name="obj">待比较的Sql IN语句条件语句</param>
        /// <param name="obj2">待比较的第二个Sql IN语句条件语句</param>
        /// <returns>两个Sql IN语句条件语句是否相同</returns>
        public static Boolean operator ==(SqlInsideCommandCondition obj, SqlInsideCommandCondition obj2)
        {
            return Object.Equals(obj, obj2);
        }

        /// <summary>
        /// 判断两个Sql IN语句条件语句是否不同
        /// </summary>
        /// <param name="obj">待比较的Sql IN语句条件语句</param>
        /// <param name="obj2">待比较的第二个Sql IN语句条件语句</param>
        /// <returns>两个Sql IN语句条件语句是否不同</returns>
        public static Boolean operator !=(SqlInsideCommandCondition obj, SqlInsideCommandCondition obj2)
        {
            return !Object.Equals(obj, obj2);
        }
        #endregion
    }
}