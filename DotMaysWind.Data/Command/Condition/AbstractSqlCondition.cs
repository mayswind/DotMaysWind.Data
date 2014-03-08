using System;

namespace DotMaysWind.Data.Command.Condition
{
    /// <summary>
    /// Sql条件语句抽象类
    /// </summary>
    public abstract class AbstractSqlCondition : ISqlCondition
    {
        #region 属性
        /// <summary>
        /// 获取语句类型
        /// </summary>
        public abstract SqlConditionType ConditionType { get; }
        #endregion

        #region 方法
        /// <summary>
        /// Sql条件语句执行非操作
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>条件列表</returns>
        public static SqlNotCondition operator !(AbstractSqlCondition condition)
        {
            return SqlCondition.Not(condition);
        }

        /// <summary>
        /// 两个Sql条件语句执行与操作
        /// </summary>
        /// <param name="condition1">条件1</param>
        /// <param name="condition2">条件2</param>
        /// <returns>条件列表</returns>
        public static SqlConditionList operator &(AbstractSqlCondition condition1, AbstractSqlCondition condition2)
        {
            return SqlCondition.And(condition1, condition2);
        }

        /// <summary>
        /// 两个Sql条件语句执行与操作
        /// </summary>
        /// <param name="condition1">条件1</param>
        /// <param name="condition2">条件2</param>
        /// <returns>条件列表</returns>
        public static SqlConditionList operator &(SqlConditionList condition1, AbstractSqlCondition condition2)
        {
            if (condition1.ConcatType == SqlWhereConcatType.And)
            {
                condition1.Add(condition2);
                return condition1;
            }
            else
            {
                return SqlCondition.And(condition1, condition2);
            }
        }

        /// <summary>
        /// 两个Sql条件语句执行与操作
        /// </summary>
        /// <param name="condition1">条件1</param>
        /// <param name="condition2">条件2</param>
        /// <returns>条件列表</returns>
        public static SqlConditionList operator &(AbstractSqlCondition condition1, SqlConditionList condition2)
        {
            if (condition2.ConcatType == SqlWhereConcatType.And)
            {
                condition2.Add(condition2);
                return condition2;
            }
            else
            {
                return SqlCondition.And(condition1, condition2);
            }
        }

        /// <summary>
        /// 两个Sql条件语句执行或操作
        /// </summary>
        /// <param name="condition1">条件1</param>
        /// <param name="condition2">条件2</param>
        /// <returns>条件列表</returns>
        public static SqlConditionList operator |(AbstractSqlCondition condition1, AbstractSqlCondition condition2)
        {
            return SqlCondition.Or(condition1, condition2);
        }

        /// <summary>
        /// 两个Sql条件语句执行或操作
        /// </summary>
        /// <param name="condition1">条件1</param>
        /// <param name="condition2">条件2</param>
        /// <returns>条件列表</returns>
        public static SqlConditionList operator |(SqlConditionList condition1, AbstractSqlCondition condition2)
        {
            if (condition1.ConcatType == SqlWhereConcatType.Or)
            {
                condition1.Add(condition2);
                return condition1;
            }
            else
            {
                return SqlCondition.Or(condition1, condition2);
            }
        }

        /// <summary>
        /// 两个Sql条件语句执行或操作
        /// </summary>
        /// <param name="condition1">条件1</param>
        /// <param name="condition2">条件2</param>
        /// <returns>条件列表</returns>
        public static SqlConditionList operator |(AbstractSqlCondition condition1, SqlConditionList condition2)
        {
            if (condition2.ConcatType == SqlWhereConcatType.Or)
            {
                condition2.Add(condition2);
                return condition2;
            }
            else
            {
                return SqlCondition.Or(condition1, condition2);
            }
        }

        /// <summary>
        /// 获取条件语句参数集合
        /// </summary>
        /// <returns>条件语句参数集合</returns>
        public abstract SqlParameter[] GetAllParameters();

        /// <summary>
        /// 输出条件语句内容
        /// </summary>
        /// <returns>条件语句内容</returns>
        public abstract String GetSqlClause();

        /// <summary>
        /// 获取当前参数的哈希值
        /// </summary>
        /// <returns>当前参数的哈希值</returns>
        public abstract override Int32 GetHashCode();

        /// <summary>
        /// 判断两个Sql条件语句是否相同
        /// </summary>
        /// <param name="obj">待比较的Sql条件语句</param>
        /// <returns>两个Sql条件语句是否相同</returns>
        public abstract override Boolean Equals(Object obj);
        #endregion

        #region 重载方法
        /// <summary>
        /// 返回当前对象的信息
        /// </summary>
        /// <returns>当前对象的信息</returns>
        public override String ToString()
        {
            return String.Format("{0}, {1}", base.ToString(), this.GetSqlClause());
        }
        #endregion
    }
}