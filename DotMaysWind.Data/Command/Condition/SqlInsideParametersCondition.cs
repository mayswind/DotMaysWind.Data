using System;
using System.Collections.Generic;
using System.Text;

namespace DotMaysWind.Data.Command.Condition
{
    /// <summary>
    /// Sql IN参数条件语句类
    /// </summary>
    public class SqlInsideParametersCondition : AbstractSqlCondition
    {
        #region 字段
        private List<SqlParameter> _parameters;
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
        /// <param name="parameters">参数集合</param>
        internal SqlInsideParametersCondition(List<SqlParameter> parameters)
        {
            this._parameters = parameters;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取条件语句包含的参数集合
        /// </summary>
        public override SqlParameter[] GetAllParameters()
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
        public override String ToString()
        {
            if (this._parameters == null || this._parameters.Count <= 0)
            {
                return String.Empty;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append('(').Append(this._parameters[0].ColumnName).Append(" IN (");

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
    }
}