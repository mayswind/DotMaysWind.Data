using System;

namespace DotMaysWind.Data.Command.Condition
{
    /// <summary>
    /// Sql简单条件语句类
    /// </summary>
    public sealed class SqlBasicParameterCondition : AbstractSqlCondition
    {
        #region 字段
        private SqlParameter _parameterOne;
        private SqlParameter _parameterTwo;
        private SqlOperator _operator;
        #endregion

        #region 属性
        /// <summary>
        /// 获取语句类型
        /// </summary>
        public override SqlConditionType ConditionType
        {
            get { return SqlConditionType.BasicParameterCondition; }
        }

        /// <summary>
        /// 获取字段名称
        /// </summary>
        public String ColumnName
        {
            get { return (this._parameterOne != null ? this._parameterOne.ColumnName : String.Empty); }  
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
        /// <param name="baseCommand">源Sql语句</param>
        /// <param name="parameter">参数</param>
        /// <param name="op">条件运算符</param>
        internal SqlBasicParameterCondition(AbstractSqlCommand baseCommand, SqlParameter parameter, SqlOperator op)
            : base(baseCommand)
        {
            this._parameterOne = parameter;
            this._parameterTwo = null;
            this._operator = op;
        }

        /// <summary>
        /// 初始化Sql查询语句类
        /// </summary>
        /// <param name="baseCommand">源Sql语句</param>
        /// <param name="parameterOne">参数一</param>
        /// <param name="parameterTwo">参数二</param>
        /// <param name="op">条件运算符</param>
        internal SqlBasicParameterCondition(AbstractSqlCommand baseCommand, SqlParameter parameterOne, SqlParameter parameterTwo, SqlOperator op)
            : base(baseCommand)
        {
            this._parameterOne = parameterOne;
            this._parameterTwo = parameterTwo;
            this._operator = op;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取条件语句包含的参数集合
        /// </summary>
        /// <returns>条件语句参数集合</returns>
        public override SqlParameter[] GetAllParameters()
        {
            Int32 paramCount = ((Byte)this._operator) / 100;

            if (paramCount == 1)
            {
                return new SqlParameter[] { this._parameterOne };
            }
            else if (paramCount == 2)
            {
                return new SqlParameter[] { this._parameterOne, this._parameterTwo };
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 输出条件语句
        /// </summary>
        /// <returns>条件语句</returns>
        public override String GetClauseText()
        {
            String format = String.Format("({0})", SqlOperators.InternalGetOperatorFormat(this._operator));
            Int32 paramCount = ((Byte)this._operator) / 100;

            if (paramCount == 0 && this._parameterOne != null)
            {
                return String.Format(format, this._parameterOne.ColumnName);
            }
            else if (paramCount == 1 && this._parameterOne != null)
            {
                return String.Format(format, this._parameterOne.ColumnName, 
                    (this._parameterOne.IsUseParameter ? this._parameterOne.ParameterName : this._parameterOne.Value.ToString()));
            }
            else if (paramCount == 2 && this._parameterOne != null && this._parameterTwo != null)
            {
                return String.Format(format, this._parameterOne.ColumnName, 
                    (this._parameterOne.IsUseParameter ? this._parameterOne.ParameterName : this._parameterOne.Value.ToString()), 
                    (this._parameterTwo.IsUseParameter ? this._parameterTwo.ParameterName : this._parameterTwo.Value.ToString()));
            }
            else
            {
                return String.Empty;
            }
        }
        #endregion

        #region 重载方法和运算符
        /// <summary>
        /// 获取当前参数的哈希值
        /// </summary>
        /// <returns>当前参数的哈希值</returns>
        public override Int32 GetHashCode()
        {
            return this._parameterOne.GetHashCode();
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

            SqlBasicParameterCondition condition = obj as SqlBasicParameterCondition;

            if (condition == null)
            {
                return false;
            }

            if (this._operator != condition._operator)
            {
                return false;
            }

            Int32 paramCount = ((Byte)this._operator) / 100;

            if (paramCount >= 1)
            {
                if ((this._parameterOne != null && condition._parameterOne == null) || (this._parameterOne == null && condition._parameterOne != null))
                {
                    return false;
                }

                if (this._parameterOne != null && condition._parameterOne != null && !this._parameterOne.Equals(condition._parameterOne))
                {
                    return false;
                }
            }
            
            if (paramCount >= 2)
            {
                if ((this._parameterTwo != null && condition._parameterTwo == null) || (this._parameterTwo == null && condition._parameterTwo != null))
                {
                    return false;
                }

                if (this._parameterTwo != null && condition._parameterTwo != null && !this._parameterTwo.Equals(condition._parameterTwo))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 判断两个Sql简单条件语句是否相同
        /// </summary>
        /// <param name="obj">待比较的Sql简单条件语句</param>
        /// <param name="obj2">待比较的第二个Sql简单条件语句</param>
        /// <returns>两个Sql简单条件语句是否相同</returns>
        public static Boolean operator ==(SqlBasicParameterCondition obj, SqlBasicParameterCondition obj2)
        {
            return Object.Equals(obj, obj2);
        }

        /// <summary>
        /// 判断两个Sql简单条件语句是否不同
        /// </summary>
        /// <param name="obj">待比较的Sql简单条件语句</param>
        /// <param name="obj2">待比较的第二个Sql简单条件语句</param>
        /// <returns>两个Sql简单条件语句是否不同</returns>
        public static Boolean operator !=(SqlBasicParameterCondition obj, SqlBasicParameterCondition obj2)
        {
            return !Object.Equals(obj, obj2);
        }
        #endregion
    }
}