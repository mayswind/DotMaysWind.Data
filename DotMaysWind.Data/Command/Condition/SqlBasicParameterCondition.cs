using System;

namespace DotMaysWind.Data.Command.Condition
{
    /// <summary>
    /// Sql简单条件语句类
    /// </summary>
    public class SqlBasicParameterCondition : AbstractSqlCondition
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
        /// <param name="parameter">参数</param>
        /// <param name="op">条件运算符</param>
        internal SqlBasicParameterCondition(SqlParameter parameter, SqlOperator op)
        {
            this._parameterOne = parameter;
            this._parameterTwo = null;
            this._operator = op;
        }

        /// <summary>
        /// 初始化Sql查询语句类
        /// </summary>
        /// <param name="parameterOne">参数一</param>
        /// <param name="parameterTwo">参数二</param>
        /// <param name="op">条件运算符</param>
        internal SqlBasicParameterCondition(SqlParameter parameterOne, SqlParameter parameterTwo, SqlOperator op)
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
        public override String GetSqlClause()
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
    }
}