using System;

namespace DotMaysWind.Data.Command
{
    /// <summary>
    /// Sql排序语句类
    /// </summary>
    public class SqlOrder
    {
        #region 字段
        private String _tableName;
        private String _columnName;
        private SqlOrderType _orderType;

        private Boolean _useFunction;
        private Boolean _useDistinct;
        private String _function;
        #endregion

        #region 属性
        /// <summary>
        /// 获取表格名称
        /// </summary>
        public String TableName
        {
            get { return this._tableName; }
        }

        /// <summary>
        /// 获取或设置字段名
        /// </summary>
        public String FieldName
        {
            get { return this._columnName; }
        }

        /// <summary>
        /// 获取或设置排序类型
        /// </summary>
        public SqlOrderType OrderType
        {
            get { return this._orderType; }
        }

        /// <summary>
        /// 获取是否使用函数
        /// </summary>
        internal Boolean UseFunction
        {
            get { return this._useFunction; }
        }

        /// <summary>
        /// 获取是否保证记录唯一
        /// </summary>
        internal Boolean UseDistinct
        {
            get { return this._useDistinct; }
        }

        /// <summary>
        /// 获取函数名称
        /// </summary>
        internal String Function
        {
            get { return this._function; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的Sql语句排序类
        /// </summary>
        /// <param name="baseCommand">选择语句</param>
        /// <param name="tableName">表格名称</param>
        /// <param name="columnName">字段名</param>
        /// <param name="orderType">排序方式</param>
        private SqlOrder(SelectCommand baseCommand, String tableName, String columnName, SqlOrderType orderType)
        {
            this._tableName = tableName;
            this._columnName = columnName;
            this._orderType = orderType;
            this._useFunction = false;
            this._useDistinct = false;
            this._function = String.Empty;
        }

        /// <summary>
        /// 初始化新的Sql语句排序类
        /// </summary>
        /// <param name="baseCommand">选择语句</param>
        /// <param name="tableName">表格名称</param>
        /// <param name="function">合计函数</param>
        /// <param name="columnName">字段名</param>
        /// <param name="useDistinct">是否保证记录唯一</param>
        /// <param name="orderType">排序方式</param>
        private SqlOrder(SelectCommand baseCommand, String tableName, SqlAggregateFunction function, String columnName, Boolean useDistinct, SqlOrderType orderType)
        {
            this._tableName = tableName;
            this._columnName = columnName;
            this._orderType = orderType;
            this._useFunction = true;
            this._useDistinct = useDistinct;
            this._function = function.ToString().ToUpperInvariant();
        }

        /// <summary>
        /// 初始化新的Sql语句排序类
        /// </summary>
        /// <param name="baseCommand">选择语句</param>
        /// <param name="function">合计函数</param>
        /// <param name="orderType">排序方式</param>
        private SqlOrder(SelectCommand baseCommand, String function, SqlOrderType orderType)
        {
            this._tableName = String.Empty;
            this._columnName = String.Empty;
            this._orderType = orderType;
            this._useFunction = true;
            this._useDistinct = false;
            this._function = function;
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
        #region InternalCreate
        /// <summary>
        /// 创建新的Sql语句排序类
        /// </summary>
        /// <param name="cmd">选择语句</param>
        /// <param name="tableName">表格名称</param>
        /// <param name="columnName">字段名</param>
        /// <param name="orderType">排序方式</param>
        internal static SqlOrder InternalCreate(SelectCommand cmd, String tableName, String columnName, SqlOrderType orderType)
        {
            return new SqlOrder(cmd, tableName, columnName, orderType);
        }

        /// <summary>
        /// 创建新的Sql语句排序类
        /// </summary>
        /// <param name="cmd">选择语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="orderType">排序方式</param>
        internal static SqlOrder InternalCreate(SelectCommand cmd, String columnName, SqlOrderType orderType)
        {
            return new SqlOrder(cmd, String.Empty, columnName, orderType);
        }
        #endregion

        #region InternalCreateFromAggregateFunction
        /// <summary>
        /// 创建新的Sql语句排序类
        /// </summary>
        /// <param name="cmd">选择语句</param>
        /// <param name="tableName">表格名称</param>
        /// <param name="function">合计函数</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="useDistinct">是否保证记录唯一</param>
        /// <param name="orderType">排序方式</param>
        internal static SqlOrder InternalCreateFromAggregateFunction(SelectCommand cmd, String tableName, SqlAggregateFunction function, String columnName, Boolean useDistinct, SqlOrderType orderType)
        {
            return new SqlOrder(cmd, tableName, function, columnName, useDistinct, orderType);
        }

        /// <summary>
        /// 创建新的Sql语句排序类
        /// </summary>
        /// <param name="cmd">选择语句</param>
        /// <param name="tableName">表格名称</param>
        /// <param name="function">合计函数</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="orderType">排序方式</param>
        internal static SqlOrder InternalCreateFromAggregateFunction(SelectCommand cmd, String tableName, SqlAggregateFunction function, String columnName, SqlOrderType orderType)
        {
            return new SqlOrder(cmd, tableName, function, columnName, false, orderType);
        }

        /// <summary>
        /// 创建新的Sql语句排序类
        /// </summary>
        /// <param name="cmd">选择语句</param>
        /// <param name="tableName">表格名称</param>
        /// <param name="function">合计函数</param>
        /// <param name="useDistinct">是否保证记录唯一</param>
        /// <param name="orderType">排序方式</param>
        internal static SqlOrder InternalCreateFromAggregateFunction(SelectCommand cmd, String tableName, SqlAggregateFunction function, Boolean useDistinct, SqlOrderType orderType)
        {
            return new SqlOrder(cmd, tableName, function, "*", useDistinct, orderType);
        }

        /// <summary>
        /// 创建新的Sql语句排序类
        /// </summary>
        /// <param name="cmd">选择语句</param>
        /// <param name="tableName">表格名称</param>
        /// <param name="function">合计函数</param>
        /// <param name="orderType">排序方式</param>
        internal static SqlOrder InternalCreateFromAggregateFunction(SelectCommand cmd, String tableName, SqlAggregateFunction function, SqlOrderType orderType)
        {
            return new SqlOrder(cmd, tableName, function, "*", false, orderType);
        }

        /// <summary>
        /// 创建新的Sql语句排序类
        /// </summary>
        /// <param name="cmd">选择语句</param>
        /// <param name="function">合计函数</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="useDistinct">是否保证记录唯一</param>
        /// <param name="orderType">排序方式</param>
        internal static SqlOrder InternalCreateFromAggregateFunction(SelectCommand cmd, SqlAggregateFunction function, String columnName, Boolean useDistinct, SqlOrderType orderType)
        {
            return new SqlOrder(cmd, String.Empty, function, columnName, useDistinct, orderType);
        }

        /// <summary>
        /// 创建新的Sql语句排序类
        /// </summary>
        /// <param name="cmd">选择语句</param>
        /// <param name="function">合计函数</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="orderType">排序方式</param>
        internal static SqlOrder InternalCreateFromAggregateFunction(SelectCommand cmd, SqlAggregateFunction function, String columnName, SqlOrderType orderType)
        {
            return new SqlOrder(cmd, String.Empty, function, columnName, false, orderType);
        }

        /// <summary>
        /// 创建新的Sql语句排序类
        /// </summary>
        /// <param name="cmd">选择语句</param>
        /// <param name="function">合计函数</param>
        /// <param name="useDistinct">是否保证记录唯一</param>
        /// <param name="orderType">排序方式</param>
        internal static SqlOrder InternalCreateFromAggregateFunction(SelectCommand cmd, SqlAggregateFunction function, Boolean useDistinct, SqlOrderType orderType)
        {
            return new SqlOrder(cmd, String.Empty, function, "*", useDistinct, orderType);
        }

        /// <summary>
        /// 创建新的Sql语句排序类
        /// </summary>
        /// <param name="cmd">选择语句</param>
        /// <param name="function">合计函数</param>
        /// <param name="orderType">排序方式</param>
        internal static SqlOrder InternalCreateFromAggregateFunction(SelectCommand cmd, SqlAggregateFunction function, SqlOrderType orderType)
        {
            return new SqlOrder(cmd, String.Empty, function, "*", false, orderType);
        }
        #endregion

        #region InternalCreateFromFunction
        /// <summary>
        /// 创建新的Sql语句排序类
        /// </summary>
        /// <param name="cmd">源选择语句</param>
        /// <param name="function">函数内容</param>
        /// <param name="orderType">排序方式</param>
        internal static SqlOrder InternalCreateFromFunction(SelectCommand cmd, String function, SqlOrderType orderType)
        {
            return new SqlOrder(cmd, function, orderType);
        }
        #endregion
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

            if (!String.Equals(this._tableName, order._tableName))
            {
                return false;
            }

            if (!String.Equals(this._columnName, order._columnName))
            {
                return false;
            }

            if (_useFunction != order._useFunction)
            {
                return false;
            }

            if (!String.Equals(this._function, order._function))
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
            return String.Format("{0}, {1} {2}", base.ToString(), this._function + this._columnName, this._orderType.ToString());
        }
        #endregion
    }
}