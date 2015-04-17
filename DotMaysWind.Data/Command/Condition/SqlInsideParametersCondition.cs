using System;
using System.Collections.Generic;
using System.Text;

namespace DotMaysWind.Data.Command.Condition
{
    /// <summary>
    /// Sql IN参数条件语句类
    /// </summary>
    public sealed class SqlInsideParametersCondition : AbstractSqlCondition
    {
        #region 字段
        private Boolean _isNotIn;
        private List<DataParameter> _parameters;
        #endregion

        #region 属性
        /// <summary>
        /// 获取语句类型
        /// </summary>
        public override SqlConditionType ConditionType
        {
            get { return SqlConditionType.InsideParametersCondition; }
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
            get { return (this._parameters.Count > 0 ? this._parameters[0].ColumnName : String.Empty); }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化Sql IN参数条件语句类
        /// </summary>
        /// <param name="baseCommand">源Sql语句</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="parameters">参数集合</param>
        internal SqlInsideParametersCondition(AbstractSqlCommand baseCommand, Boolean isNotIn, params DataParameter[] parameters)
            : base(baseCommand)
        {
            this._isNotIn = isNotIn;
            this._parameters = new List<DataParameter>();

            if (parameters != null)
            {
                this._parameters.AddRange(parameters);
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取条件语句包含的参数集合
        /// </summary>
        /// <returns>条件语句参数集合</returns>
        public override DataParameter[] GetAllParameters()
        {
            if (this._parameters == null)
            {
                return null;
            }

            return this._parameters.ToArray();
        }

        /// <summary>
        /// 输出条件语句
        /// </summary>
        /// <returns>条件语句</returns>
        public override String GetClauseText()
        {
            if (this._parameters == null || this._parameters.Count <= 0)
            {
                return String.Empty;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append('(').Append(this._parameters[0].ColumnName).Append((this._isNotIn ? " NOT IN (" : " IN ("));

            for (Int32 i = 0; i < this._parameters.Count; i++)
            {
                if (i > 0) sb.Append(",");

                if (this._parameters[i].IsUseParameter)
                {
                    sb.Append(this._parameters[i].ParameterName);
                }
                else
                {
                    sb.Append(this._parameters[i].Value.ToString());
                }
            }

            sb.Append("))");

            return sb.ToString();
        }
        #endregion

        #region 重载方法和运算符
        /// <summary>
        /// 获取当前参数的哈希值
        /// </summary>
        /// <returns>当前参数的哈希值</returns>
        public override Int32 GetHashCode()
        {
            return this._parameters.GetHashCode();
        }

        /// <summary>
        /// 判断两个Sql IN参数条件语句是否相同
        /// </summary>
        /// <param name="obj">待比较的Sql IN参数条件语句</param>
        /// <returns>两个Sql IN参数条件语句是否相同</returns>
        public override Boolean Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            SqlInsideParametersCondition condition = obj as SqlInsideParametersCondition;

            if (condition == null)
            {
                return false;
            }

            if (this._isNotIn != condition._isNotIn)
            {
                return false;
            }

            if (this._parameters.Count != condition._parameters.Count)
            {
                return false;
            }

            for (Int32 i = 0; i < this._parameters.Count; i++)
            {
                if ((this._parameters[i] != null && condition._parameters[i] == null) || (this._parameters[i] == null && condition._parameters[i] != null))
                {
                    return false;
                }

                if (!this._parameters[i].Equals(condition._parameters[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 判断两个Sql IN参数条件语句是否相同
        /// </summary>
        /// <param name="obj">待比较的Sql IN参数条件语句</param>
        /// <param name="obj2">待比较的第二个Sql IN参数条件语句</param>
        /// <returns>两个Sql IN参数条件语句是否相同</returns>
        public static Boolean operator ==(SqlInsideParametersCondition obj, SqlInsideParametersCondition obj2)
        {
            return Object.Equals(obj, obj2);
        }

        /// <summary>
        /// 判断两个Sql IN参数条件语句是否不同
        /// </summary>
        /// <param name="obj">待比较的Sql IN参数条件语句</param>
        /// <param name="obj2">待比较的第二个Sql IN参数条件语句</param>
        /// <returns>两个Sql IN参数条件语句是否不同</returns>
        public static Boolean operator !=(SqlInsideParametersCondition obj, SqlInsideParametersCondition obj2)
        {
            return !Object.Equals(obj, obj2);
        }
        #endregion
    }
}