using System;

namespace DotMaysWind.Data.Command.Condition
{
    /// <summary>
    /// Sql条件语句抽象类
    /// </summary>
    public abstract class AbstractSqlCondition : ISqlCondition
    {
        #region 字段
        private AbstractSqlCommand _baseCommand;
        #endregion

        #region 属性
        /// <summary>
        /// 获取语句类型
        /// </summary>
        public abstract SqlConditionType ConditionType { get; }

        /// <summary>
        /// 获取所在Sql语句
        /// </summary>
        public ISqlCommand SqlCommand
        {
            get { return this._baseCommand; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的Sql抽象条件语句
        /// </summary>
        /// <param name="baseCommand">源Sql语句</param>
        internal AbstractSqlCondition(AbstractSqlCommand baseCommand)
        {
            this._baseCommand = baseCommand;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取条件语句参数集合
        /// </summary>
        /// <returns>条件语句参数集合</returns>
        public abstract DataParameter[] GetAllParameters();

        /// <summary>
        /// 输出条件语句内容
        /// </summary>
        /// <returns>条件语句内容</returns>
        public abstract String GetClauseText();

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

        /// <summary>
        /// 与指定Sql条件执行与操作
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>条件列表</returns>
        public SqlConditionList And(AbstractSqlCondition condition)
        {
            return SqlCondition.And(this._baseCommand, this, condition);
        }

        /// <summary>
        /// 与指定Sql条件执行或操作
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>条件列表</returns>
        public SqlConditionList Or(AbstractSqlCondition condition)
        {
            return SqlCondition.Or(this._baseCommand, this, condition);
        }
        #endregion

        #region 重载方法和运算符
        /// <summary>
        /// Sql条件语句执行非操作
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>条件列表</returns>
        public static SqlNotCondition operator !(AbstractSqlCondition condition)
        {
            if (condition == null)
            {
                return null;
            }
            else
            {
                return SqlCondition.Not(condition._baseCommand, condition);
            }
        }

        /// <summary>
        /// 两个Sql条件语句执行与操作
        /// </summary>
        /// <param name="condition1">条件1</param>
        /// <param name="condition2">条件2</param>
        /// <returns>条件列表</returns>
        public static AbstractSqlCondition operator &(AbstractSqlCondition condition1, AbstractSqlCondition condition2)
        {
            if (condition1 == null && condition2 == null)
            {
                return null;
            }
            else if (condition1 != null && condition2 == null)
            {
                return condition1;
            }
            else if (condition1 == null && condition2 != null)
            {
                return condition2;
            }

            if (condition1.ConditionType == SqlConditionType.ConditionList)
            {
                SqlConditionList list = condition1 as SqlConditionList;
                list.Add(condition2);

                return list;
            }
            else if (condition2.ConditionType == SqlConditionType.ConditionList)
            {
                SqlConditionList list = condition2 as SqlConditionList;
                list.Add(condition1);

                return list;
            }
            else
            {
                return SqlCondition.And(condition1._baseCommand, condition1, condition2);
            }
        }

        /// <summary>
        /// 两个Sql条件语句执行或操作
        /// </summary>
        /// <param name="condition1">条件1</param>
        /// <param name="condition2">条件2</param>
        /// <returns>条件列表</returns>
        public static AbstractSqlCondition operator |(AbstractSqlCondition condition1, AbstractSqlCondition condition2)
        {
            if (condition1 == null && condition2 == null)
            {
                return null;
            }
            else if (condition1 != null && condition2 == null)
            {
                return condition1;
            }
            else if (condition1 == null && condition2 != null)
            {
                return condition2;
            }

            if (condition1.ConditionType == SqlConditionType.ConditionList)
            {
                SqlConditionList list = condition1 as SqlConditionList;
                list.Add(condition2);

                return list;
            }
            else if (condition2.ConditionType == SqlConditionType.ConditionList)
            {
                SqlConditionList list = condition2 as SqlConditionList;
                list.Add(condition1);

                return list;
            }
            else
            {
                return SqlCondition.Or(condition1._baseCommand, condition1, condition2);
            }
        }

        /// <summary>
        /// 返回当前对象的信息
        /// </summary>
        /// <returns>当前对象的信息</returns>
        public override String ToString()
        {
            return String.Format("{0}, {1}", base.ToString(), this.GetClauseText());
        }
        #endregion
    }
}