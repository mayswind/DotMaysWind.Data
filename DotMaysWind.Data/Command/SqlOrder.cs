using System;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql排序语句类
    /// </summary>
    public class SqlOrder
    {
        #region 字段
        private String _fieldName;
        private SqlOrderType _orderType;
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置字段名
        /// </summary>
        public String FieldName
        {
            get { return this._fieldName; }
            set { this._fieldName = value; }
        }

        /// <summary>
        /// 获取或设置排序类型
        /// </summary>
        public SqlOrderType OrderType
        {
            get { return this._orderType; }
            set { this._orderType = value; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的Sql语句排序类
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="orderType">排序方式</param>
        private SqlOrder(String fieldName, SqlOrderType orderType)
        {
            this._fieldName = fieldName;
            this._orderType = orderType;
        }
        #endregion
        
        #region 方法
        /// <summary>
        /// 获取排序类型
        /// </summary>
        /// <param name="orderReverse">是否取反</param>
        /// <returns>排序类型</returns>
        public SqlOrderType GetOrderType(Boolean orderReverse)
        {
            if (orderReverse)
            {
                return (this._orderType == SqlOrderType.Asc ? SqlOrderType.Desc : SqlOrderType.Asc);
            }
            else
            {
                return this._orderType;
            }
        }
        #endregion

        #region 静态方法
        /// <summary>
        /// 初始化新的Sql语句排序类
        /// </summary>
        /// <param name="fieldName">字段名</param>
        public static SqlOrder Create(String fieldName)
        {
            return new SqlOrder(fieldName, SqlOrderType.Asc);
        }

        /// <summary>
        /// 初始化新的Sql语句排序类
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="orderType">排序方式</param>
        public static SqlOrder Create(String fieldName, SqlOrderType orderType)
        {
            return new SqlOrder(fieldName, orderType);
        }

        /// <summary>
        /// 初始化新的Sql语句排序类
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="isAscending">是否升序</param>
        public static SqlOrder Create(String fieldName, Boolean isAscending)
        {
            return new SqlOrder(fieldName, (isAscending ? SqlOrderType.Asc : SqlOrderType.Desc));
        }
        #endregion

        #region 重载方法和运算符
        /// <summary>
        /// 获取当前参数的哈希值
        /// </summary>
        /// <returns>当前参数的哈希值</returns>
        public override Int32 GetHashCode()
        {
            return this._fieldName.GetHashCode();
        }

        /// <summary>
        /// 判断两个Sql排序语句是否相同
        /// </summary>
        /// <param name="obj">待比较的Sql排序语句</param>
        /// <returns>两个Sql排序语句是否相同</returns>
        public override Boolean Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            SqlOrder order = obj as SqlOrder;

            if (order == null)
            {
                return false;
            }

            if (this._orderType != order._orderType)
            {
                return false;
            }

            if (!String.Equals(this._fieldName, order._fieldName))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 判断两个Sql排序语句是否相同
        /// </summary>
        /// <param name="obj">待比较的Sql排序语句</param>
        /// <param name="obj2">待比较的第二个Sql排序语句</param>
        /// <returns>两个Sql排序语句是否相同</returns>
        public static Boolean operator ==(SqlOrder obj, SqlOrder obj2)
        {
            return Object.Equals(obj, obj2);
        }

        /// <summary>
        /// 判断两个Sql排序语句是否不同
        /// </summary>
        /// <param name="obj">待比较的Sql排序语句</param>
        /// <param name="obj2">待比较的第二个Sql排序语句</param>
        /// <returns>两个Sql排序语句是否不同</returns>
        public static Boolean operator !=(SqlOrder obj, SqlOrder obj2)
        {
            return !Object.Equals(obj, obj2);
        }

        /// <summary>
        /// 返回当前对象的信息
        /// </summary>
        /// <returns>当前对象的信息</returns>
        public override String ToString()
        {
            return String.Format("{0}, {1} {2}", base.ToString(), this._fieldName, this._orderType.ToString());
        }
        #endregion
    }
}