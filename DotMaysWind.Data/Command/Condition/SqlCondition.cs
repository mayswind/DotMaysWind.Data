using System;
using System.Collections.Generic;

using DotMaysWind.Data.Helper;

namespace DotMaysWind.Data.Command.Condition
{
    /// <summary>
    /// Sql条件语句类
    /// </summary>
    public static class SqlCondition
    {
        #region 常量
        /// <summary>
        /// 创建永远为真的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition True(AbstractSqlCommand cmd)
        {
            return SqlCondition.InternalCreateAction(cmd, "1", SqlOperator.Equal, "1");
        }

        /// <summary>
        /// 创建永远为假的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition False(AbstractSqlCommand cmd)
        {
            return SqlCondition.InternalCreateAction(cmd, "1", SqlOperator.Equal, "0");
        }
        #endregion

        #region 基本参数条件
        #region Internal
        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="op">条件运算符</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreate(AbstractSqlCommand cmd, String columnName, SqlOperator op)
        {
            return new SqlBasicParameterCondition(cmd, cmd.CreateDataParameter(columnName, null), op);
        }

        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreate(AbstractSqlCommand cmd, String columnName, SqlOperator op, Object value)
        {
            return new SqlBasicParameterCondition(cmd, cmd.CreateDataParameter(columnName, value), op);
        }

        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreate(AbstractSqlCommand cmd, String columnName, SqlOperator op, DataType dataType, Object value)
        {
            return new SqlBasicParameterCondition(cmd, cmd.CreateDataParameter(columnName, dataType, value), op);
        }

        /// <summary>
        /// 创建双参数新的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="valueOne">数据一</param>
        /// <param name="valueTwo">数据二</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreate(AbstractSqlCommand cmd, String columnName, SqlOperator op, Object valueOne, Object valueTwo)
        {
            return new SqlBasicParameterCondition(cmd, cmd.CreateDataParameter(columnName, valueOne), cmd.CreateDataParameter(columnName, valueTwo), op);
        }

        /// <summary>
        /// 创建双参数新的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="valueOne">数据一</param>
        /// <param name="valueTwo">数据二</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreate(AbstractSqlCommand cmd, String columnName, SqlOperator op, DataType dataType, Object valueOne, Object valueTwo)
        {
            return new SqlBasicParameterCondition(cmd, cmd.CreateDataParameter(columnName, dataType, valueOne), cmd.CreateDataParameter(columnName, dataType, valueTwo), op);
        }

        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="action">赋值操作</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreateAction(AbstractSqlCommand cmd, String columnName, SqlOperator op, String action)
        {
            return new SqlBasicParameterCondition(cmd, cmd.CreateDataParameterCustomAction(columnName, action), op);
        }

        /// <summary>
        /// 创建双参数新的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreateColumn(AbstractSqlCommand cmd, String columnName, SqlOperator op, String tableNameTwo, String columnNameTwo)
        {
            return new SqlBasicParameterCondition(cmd, cmd.CreateDataParameterCustomAction(columnName, GetFieldName(tableNameTwo, columnNameTwo)), op);
        }

        /// <summary>
        /// 创建双参数新的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreateColumn(AbstractSqlCommand cmd, String columnName, SqlOperator op, String columnNameTwo)
        {
            return new SqlBasicParameterCondition(cmd, cmd.CreateDataParameterCustomAction(columnName, GetFieldName(String.Empty, columnNameTwo)), op);
        }
        #endregion

        #region SqlAggregateFunction
        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="function">合计函数类型</param>
        /// <param name="columnName">要查询的字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Create(AbstractSqlCommand cmd, SqlAggregateFunction function, String columnName, SqlOperator op, Object value)
        {
            return SqlCondition.InternalCreate(cmd, String.Format("{0}({1})", function.ToString().ToUpperInvariant(), columnName), op, value);
        }

        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="function">合计函数类型</param>
        /// <param name="columnName">要查询的字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Create(AbstractSqlCommand cmd, SqlAggregateFunction function, String columnName, SqlOperator op, DataType dataType, Object value)
        {
            return SqlCondition.InternalCreate(cmd, String.Format("{0}({1})", function.ToString().ToUpperInvariant(), columnName), op, dataType, value);
        }

        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="function">合计函数类型</param>
        /// <param name="op">条件运算符</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Create(AbstractSqlCommand cmd, SqlAggregateFunction function, SqlOperator op, Object value)
        {
            return SqlCondition.Create(cmd, function, "*", op, value);
        }
        #endregion

        #region SqlFunction
        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="function">Sql函数</param>
        /// <param name="op">条件运算符</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Create(AbstractSqlCommand cmd, ISqlFunction function, SqlOperator op, Object value)
        {
            return SqlCondition.InternalCreate(cmd, function.GetCommandText(), op, value);
        }

        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="function">Sql函数</param>
        /// <param name="op">条件运算符</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Create(AbstractSqlCommand cmd, ISqlFunction function, SqlOperator op, DataType dataType, Object value)
        {
            return SqlCondition.InternalCreate(cmd, function.GetCommandText(), op, dataType, value);
        }
        #endregion

        #region IsNull/IsNotNull
        /// <summary>
        /// 创建判断是否为空的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNot">是否非空</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalIsNull(AbstractSqlCommand cmd, String columnName, Boolean isNot)
        {
            return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.IsNotNull : SqlOperator.IsNull));
        }

        /// <summary>
        /// 创建判断是否为空的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition IsNull(AbstractSqlCommand cmd, String columnName)
        {
            return SqlCondition.InternalIsNull(cmd, columnName, false);
        }

        /// <summary>
        /// 创建判断是否非空的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition IsNotNull(AbstractSqlCommand cmd, String columnName)
        {
            return SqlCondition.InternalIsNull(cmd, columnName, true);
        }
        #endregion

        #region Equal/NotEqual
        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Equal(AbstractSqlCommand cmd, String columnName, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, SqlOperator.Equal, value);
        }

        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Equal(AbstractSqlCommand cmd, String columnName, DataType dataType, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, SqlOperator.Equal, dataType, value);
        }

        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition EqualColumn(AbstractSqlCommand cmd, String columnName, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(cmd, columnName, SqlOperator.Equal, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition EqualColumn(AbstractSqlCommand cmd, String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(cmd, columnName, SqlOperator.Equal, tableNameTwo, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotEqual(AbstractSqlCommand cmd, String columnName, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, SqlOperator.NotEqual, value);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotEqual(AbstractSqlCommand cmd, String columnName, DataType dataType, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, SqlOperator.NotEqual, dataType, value);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotEqualColumn(AbstractSqlCommand cmd, String columnName, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(cmd, columnName, SqlOperator.NotEqual, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotEqualColumn(AbstractSqlCommand cmd, String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(cmd, columnName, SqlOperator.NotEqual, tableNameTwo, columnNameTwo);
        }
        #endregion

        #region GreaterThan/LessThan
        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThan(AbstractSqlCommand cmd, String columnName, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, SqlOperator.GreaterThan, value);
        }

        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThan(AbstractSqlCommand cmd, String columnName, DataType dataType, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, SqlOperator.GreaterThan, dataType, value);
        }

        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThanColumn(AbstractSqlCommand cmd, String columnName, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(cmd, columnName, SqlOperator.GreaterThan, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThanColumn(AbstractSqlCommand cmd, String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(cmd, columnName, SqlOperator.GreaterThan, tableNameTwo, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LessThan(AbstractSqlCommand cmd, String columnName, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, SqlOperator.LessThan, value);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LessThan(AbstractSqlCommand cmd, String columnName, DataType dataType, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, SqlOperator.LessThan, dataType, value);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LessThanColumn(AbstractSqlCommand cmd, String columnName, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(cmd, columnName, SqlOperator.LessThan, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LessThanColumn(AbstractSqlCommand cmd, String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(cmd, columnName, SqlOperator.LessThan, tableNameTwo, columnNameTwo);
        }
        #endregion

        #region GreaterThanOrEqual/LessThanOrEqual
        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThanOrEqual(AbstractSqlCommand cmd, String columnName, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, SqlOperator.GreaterThanOrEqual, value);
        }

        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThanOrEqual(AbstractSqlCommand cmd, String columnName, DataType dataType, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, SqlOperator.GreaterThanOrEqual, dataType, value);
        }

        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThanOrEqualColumn(AbstractSqlCommand cmd, String columnName, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(cmd, columnName, SqlOperator.GreaterThanOrEqual, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThanOrEqualColumn(AbstractSqlCommand cmd, String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(cmd, columnName, SqlOperator.GreaterThanOrEqual, tableNameTwo, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LessThanOrEqual(AbstractSqlCommand cmd, String columnName, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, SqlOperator.LessThanOrEqual, value);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LessThanOrEqual(AbstractSqlCommand cmd, String columnName, DataType dataType, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, SqlOperator.LessThanOrEqual, dataType, value);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LessThanOrEqualColumn(AbstractSqlCommand cmd, String columnName, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(cmd, columnName, SqlOperator.LessThanOrEqual, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LessThanOrEqualColumn(AbstractSqlCommand cmd, String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(cmd, columnName, SqlOperator.LessThanOrEqual, tableNameTwo, columnNameTwo);
        }
        #endregion

        #region Like/NotLike
        #region Internal
        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNot">是否不相似</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalLike(AbstractSqlCommand cmd, String columnName, Boolean isNot, String value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.NotLike : SqlOperator.Like), value);
        }

        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNot">是否不相似</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalLike(AbstractSqlCommand cmd, String columnName, Boolean isNot, DataType dataType, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.NotLike : SqlOperator.Like), dataType, value);
        }

        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNot">是否不相似</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalLikeColumn(AbstractSqlCommand cmd, String columnName, Boolean isNot, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(cmd, columnName, (isNot ? SqlOperator.NotLike : SqlOperator.Like), columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNot">是否不相似</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalLikeColumn(AbstractSqlCommand cmd, String columnName, Boolean isNot, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(cmd, columnName, (isNot ? SqlOperator.NotLike : SqlOperator.Like), tableNameTwo, columnNameTwo);
        }
        #endregion

        #region General
        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Like(AbstractSqlCommand cmd, String columnName, String value)
        {
            return SqlCondition.InternalLike(cmd, columnName, false, value);
        }

        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Like(AbstractSqlCommand cmd, String columnName, DataType dataType, Object value)
        {
            return SqlCondition.InternalLike(cmd, columnName, false, dataType, value);
        }

        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeColumn(AbstractSqlCommand cmd, String columnName, String columnNameTwo)
        {
            return SqlCondition.InternalLikeColumn(cmd, columnName, false, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeColumn(AbstractSqlCommand cmd, String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.InternalLikeColumn(cmd, columnName, false, tableNameTwo, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否不相似的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLike(AbstractSqlCommand cmd, String columnName, String value)
        {
            return SqlCondition.InternalLike(cmd, columnName, true, value);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLike(AbstractSqlCommand cmd, String columnName, DataType dataType, Object value)
        {
            return SqlCondition.InternalLike(cmd, columnName, true, dataType, value);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLikeColumn(AbstractSqlCommand cmd, String columnName, String columnNameTwo)
        {
            return SqlCondition.InternalLikeColumn(cmd, columnName, true, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLikeColumn(AbstractSqlCommand cmd, String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.InternalLikeColumn(cmd, columnName, true, tableNameTwo, columnNameTwo);
        }
        #endregion

        #region LikeAll
        /// <summary>
        /// 创建判断是否包含的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeAll(AbstractSqlCommand cmd, String columnName, String value)
        {
            return SqlCondition.Like(cmd, columnName, "%" + value + "%");
        }

        /// <summary>
        /// 创建判断是否包含的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeAll(AbstractSqlCommand cmd, String columnName, DataType dataType, Object value)
        {
            return SqlCondition.Like(cmd, columnName, dataType, "%" + value + "%");
        }

        /// <summary>
        /// 创建判断是否不包含的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLikeAll(AbstractSqlCommand cmd, String columnName, String value)
        {
            return SqlCondition.NotLike(cmd, columnName, "%" + value + "%");
        }

        /// <summary>
        /// 创建判断是否不包含的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLikeAll(AbstractSqlCommand cmd, String columnName, DataType dataType, Object value)
        {
            return SqlCondition.NotLike(cmd, columnName, dataType, "%" + value + "%");
        }
        #endregion

        #region LikeStartWith
        /// <summary>
        /// 创建判断是否开头包含的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeStartWith(AbstractSqlCommand cmd, String columnName, String value)
        {
            return SqlCondition.Like(cmd, columnName, value + "%");
        }

        /// <summary>
        /// 创建判断是否开头包含的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeStartWith(AbstractSqlCommand cmd, String columnName, DataType dataType, Object value)
        {
            return SqlCondition.Like(cmd, columnName, dataType, value + "%");
        }

        /// <summary>
        /// 创建判断是否开头不包含的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLikeStartWith(AbstractSqlCommand cmd, String columnName, String value)
        {
            return SqlCondition.NotLike(cmd, columnName, value + "%");
        }

        /// <summary>
        /// 创建判断是否开头不包含的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLikeStartWith(AbstractSqlCommand cmd, String columnName, DataType dataType, Object value)
        {
            return SqlCondition.NotLike(cmd, columnName, dataType, value + "%");
        }
        #endregion

        #region LikeEndWith
        /// <summary>
        /// 创建判断是否结尾包含的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeEndWith(AbstractSqlCommand cmd, String columnName, String value)
        {
            return SqlCondition.Like(cmd, columnName, "%" + value);
        }

        /// <summary>
        /// 创建判断是否结尾包含的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeEndWith(AbstractSqlCommand cmd, String columnName, DataType dataType, Object value)
        {
            return SqlCondition.Like(cmd, columnName, dataType, "%" + value);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLikeEndWith(AbstractSqlCommand cmd, String columnName, String value)
        {
            return SqlCondition.NotLike(cmd, columnName, "%" + value);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLikeEndWith(AbstractSqlCommand cmd, String columnName, DataType dataType, Object value)
        {
            return SqlCondition.NotLike(cmd, columnName, dataType, "%" + value);
        }
        #endregion
        #endregion

        #region Between/NotBetween
        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNot">是否不在范围内</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalBetween(AbstractSqlCommand cmd, String columnName, Boolean isNot, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.NotBetween : SqlOperator.Between), valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNot">是否不在范围内</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalBetween(AbstractSqlCommand cmd, String columnName, Boolean isNot, DataType dataType, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.NotBetween : SqlOperator.Between), dataType, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Between(AbstractSqlCommand cmd, String columnName, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalBetween(cmd, columnName, false, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Between(AbstractSqlCommand cmd, String columnName, DataType dataType, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalBetween(cmd, columnName, false, dataType, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否不在范围内的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotBetween(AbstractSqlCommand cmd, String columnName, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalBetween(cmd, columnName, true, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否不在范围内的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotBetween(AbstractSqlCommand cmd, String columnName, DataType dataType, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalBetween(cmd, columnName, true, dataType, valueOne, valueTwo);
        }
        #endregion

        #region BetweenNullable/NotBetweenNullable
        #region Internal
        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNot">是否不在范围内</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalBetweenNullable(AbstractSqlCommand cmd, String columnName, Boolean isNot, Object valueOne, Object valueTwo)
        {
            if (valueOne != null && valueTwo != null)
            {
                return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.NotBetween : SqlOperator.Between), valueOne, valueTwo);
            }
            else if (valueOne != null && valueTwo == null)
            {
                return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.LessThan : SqlOperator.GreaterThanOrEqual), valueOne);
            }
            else if (valueOne == null && valueTwo != null)
            {
                return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.GreaterThan : SqlOperator.LessThanOrEqual), valueTwo);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNot">是否不在范围内</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalBetweenNullable(AbstractSqlCommand cmd, String columnName, Boolean isNot, DataType dataType, Object valueOne, Object valueTwo)
        {
            if (valueOne != null && valueTwo != null)
            {
                return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.NotBetween : SqlOperator.Between), dataType, valueOne, valueTwo);
            }
            else if (valueOne != null && valueTwo == null)
            {
                return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.LessThan : SqlOperator.GreaterThanOrEqual), dataType, valueOne);
            }
            else if (valueOne == null && valueTwo != null)
            {
                return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.GreaterThan : SqlOperator.LessThanOrEqual), dataType, valueTwo);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNot">是否不在范围内</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalBetweenNullable<T>(AbstractSqlCommand cmd, String columnName, Boolean isNot, T? valueOne, T? valueTwo) where T : struct
        {
            if (valueOne.HasValue && valueTwo.HasValue)
            {
                return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.NotBetween : SqlOperator.Between), valueOne, valueTwo);
            }
            else if (valueOne.HasValue && !valueTwo.HasValue)
            {
                return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.LessThan : SqlOperator.GreaterThanOrEqual), valueOne);
            }
            else if (!valueOne.HasValue && valueTwo.HasValue)
            {
                return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.GreaterThan : SqlOperator.LessThanOrEqual), valueTwo);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNot">是否不在范围内</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalBetweenNullable<T>(AbstractSqlCommand cmd, String columnName, Boolean isNot, DataType dataType, T? valueOne, T? valueTwo) where T : struct
        {
            if (valueOne.HasValue && valueTwo.HasValue)
            {
                return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.NotBetween : SqlOperator.Between), dataType, valueOne, valueTwo);
            }
            else if (valueOne.HasValue && !valueTwo.HasValue)
            {
                return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.LessThan : SqlOperator.GreaterThanOrEqual), dataType, valueOne);
            }
            else if (!valueOne.HasValue && valueTwo.HasValue)
            {
                return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.GreaterThan : SqlOperator.LessThanOrEqual), dataType, valueTwo);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region BetweenNullable
        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="valueOne">可空开始值</param>
        /// <param name="valueTwo">可空结束值</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition BetweenNullable(AbstractSqlCommand cmd, String columnName, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalBetweenNullable(cmd, columnName, false, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="valueOne">可空开始值</param>
        /// <param name="valueTwo">可空结束值</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition BetweenNullable(AbstractSqlCommand cmd, String columnName, DataType dataType, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalBetweenNullable(cmd, columnName, false, dataType, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition BetweenNullable<T>(AbstractSqlCommand cmd, String columnName, T? valueOne, T? valueTwo) where T : struct
        {
            return SqlCondition.InternalBetweenNullable<T>(cmd, columnName, false, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition BetweenNullable<T>(AbstractSqlCommand cmd, String columnName, DataType dataType, T? valueOne, T? valueTwo) where T : struct
        {
            return SqlCondition.InternalBetweenNullable<T>(cmd, columnName, false, dataType, valueOne, valueTwo);
        }
        #endregion

        #region NotBetweenNullable
        /// <summary>
        /// 创建判断是否不在范围内的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="valueOne">可空开始值</param>
        /// <param name="valueTwo">可空结束值</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotBetweenNullable(AbstractSqlCommand cmd, String columnName, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalBetweenNullable(cmd, columnName, true, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否不在范围内的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="valueOne">可空开始值</param>
        /// <param name="valueTwo">可空结束值</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotBetweenNullable(AbstractSqlCommand cmd, String columnName, DataType dataType, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalBetweenNullable(cmd, columnName, true, dataType, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否不在范围内的Sql条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotBetweenNullable<T>(AbstractSqlCommand cmd, String columnName, T? valueOne, T? valueTwo) where T : struct
        {
            return SqlCondition.InternalBetweenNullable<T>(cmd, columnName, true, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否不在范围内的Sql条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotBetweenNullable<T>(AbstractSqlCommand cmd, String columnName, DataType dataType, T? valueOne, T? valueTwo) where T : struct
        {
            return SqlCondition.InternalBetweenNullable<T>(cmd, columnName, true, dataType, valueOne, valueTwo);
        }
        #endregion
        #endregion
        #endregion

        #region 基本语句条件
        #region General
        /// <summary>
        /// 创建新的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="op">条件运算符</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicCommandCondition Create(AbstractSqlCommand cmd, String columnName, SqlOperator op, String tableName, Action<SelectCommand> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            return new SqlBasicCommandCondition(cmd, columnName, op, tableName, action);
        }

        /// <summary>
        /// 创建新的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="op">条件运算符</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicCommandCondition Create(AbstractSqlCommand cmd, String columnName, SqlOperator op, Action<SelectCommand> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            return new SqlBasicCommandCondition(cmd, columnName, op, cmd.TableName, action);
        }
        #endregion

        #region Equal/NotEqual
        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition Equal(AbstractSqlCommand cmd, String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.Equal, tableName, action);
        }

        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition Equal(AbstractSqlCommand cmd, String columnName, Action<SelectCommand> action)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.Equal, action);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition NotEqual(AbstractSqlCommand cmd, String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.NotEqual, tableName, action);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition NotEqual(AbstractSqlCommand cmd, String columnName, Action<SelectCommand> action)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.NotEqual, action);
        }
        #endregion

        #region GreaterThan/LessThan
        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition GreaterThan(AbstractSqlCommand cmd, String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.GreaterThan, tableName, action);
        }

        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition GreaterThan(AbstractSqlCommand cmd, String columnName, Action<SelectCommand> action)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.GreaterThan, action);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition LessThan(AbstractSqlCommand cmd, String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.LessThan, tableName, action);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition LessThan(AbstractSqlCommand cmd, String columnName, Action<SelectCommand> action)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.LessThan, action);
        }
        #endregion

        #region GreaterThanOrEqual/LessThanOrEqual
        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition GreaterThanOrEqual(AbstractSqlCommand cmd, String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.GreaterThanOrEqual, tableName, action);
        }

        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition GreaterThanOrEqual(AbstractSqlCommand cmd, String columnName, Action<SelectCommand> action)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.GreaterThanOrEqual, action);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition LessThanOrEqual(AbstractSqlCommand cmd, String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.LessThanOrEqual, tableName, action);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition LessThanOrEqual(AbstractSqlCommand cmd, String columnName, Action<SelectCommand> action)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.LessThanOrEqual, action);
        }
        #endregion

        #region Like/NotLike
        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition Like(AbstractSqlCommand cmd, String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.Like, tableName, action);
        }

        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition Like(AbstractSqlCommand cmd, String columnName, Action<SelectCommand> action)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.Like, action);
        }

        /// <summary>
        /// 创建判断是否不相似的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition NotLike(AbstractSqlCommand cmd, String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.NotLike, tableName, action);
        }

        /// <summary>
        /// 创建判断是否不相似的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition NotLike(AbstractSqlCommand cmd, String columnName, Action<SelectCommand> action)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.NotLike, action);
        }
        #endregion
        #endregion

        #region In参数条件
        #region Internal
        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="parameters">参数集合</param>
        /// <exception cref="ArgumentNullException">参数集合不能为空</exception>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalIn(AbstractSqlCommand cmd, Boolean isNotIn, params DataParameter[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            return new SqlInsideParametersCondition(cmd, isNotIn, parameters);
        }

        /// <summary>
        /// 创建新的Sql IN参数条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalIn(AbstractSqlCommand cmd, String columnName, Boolean isNotIn, DataType dataType, IEnumerable<Object> values)
        {
            List<DataParameter> parameters = new List<DataParameter>();

            if (values != null)
            {
                foreach (Object value in values)
                {
                    parameters.Add(cmd.CreateDataParameter(columnName, dataType, value));
                }
            }

            return new SqlInsideParametersCondition(cmd, isNotIn, parameters.ToArray());
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalIn(AbstractSqlCommand cmd, String columnName, Boolean isNotIn, IEnumerable<Object> values)
        {
            List<DataParameter> parameters = new List<DataParameter>();

            if (values != null)
            {
                DataType? dataType = null;

                foreach (Object value in values)
                {
                    if (!dataType.HasValue)
                    {
                        dataType = DataTypeHelper.InternalGetDataType(value);
                    }

                    parameters.Add(cmd.CreateDataParameter(columnName, dataType.Value, value));
                }
            }

            return new SqlInsideParametersCondition(cmd, isNotIn, parameters.ToArray());
        }
        
        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalIn(AbstractSqlCommand cmd, String columnName, Boolean isNotIn, DataType dataType, Array values)
        {
            List<DataParameter> parameters = new List<DataParameter>();

            if (values != null)
            {
                foreach (Object obj in values)
                {
                    parameters.Add(cmd.CreateDataParameter(columnName, dataType, obj));
                }
            }

            return new SqlInsideParametersCondition(cmd, isNotIn, parameters.ToArray());
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalIn(AbstractSqlCommand cmd, String columnName, Boolean isNotIn, Array values)
        {
            List<DataParameter> parameters = new List<DataParameter>();

            if (values != null)
            {
                DataType? dataType = null;

                foreach (Object obj in values)
                {
                    if (!dataType.HasValue)
                    {
                        dataType = DataTypeHelper.InternalGetDataType(obj);
                    }

                    parameters.Add(cmd.CreateDataParameter(columnName, dataType.Value, obj));
                }
            }

            return new SqlInsideParametersCondition(cmd, isNotIn, parameters.ToArray());
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalIn(AbstractSqlCommand cmd, String columnName, Boolean isNotIn, DataType dataType, String values, Char separator)
        {
            List<DataParameter> parameters = new List<DataParameter>();

            if (!String.IsNullOrEmpty(values))
            {
                String[] valuesArray = values.Split(separator);

                for (Int32 i = 0; i < valuesArray.Length; i++)
                {
                    parameters.Add(cmd.CreateDataParameter(columnName, dataType, valuesArray[i].Trim()));
                }
            }

            return new SqlInsideParametersCondition(cmd, isNotIn, parameters.ToArray());
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalIn<T>(AbstractSqlCommand cmd, String columnName, Boolean isNotIn, DataType dataType, IEnumerable<T> values)
        {
            List<DataParameter> parameters = new List<DataParameter>();

            if (values != null)
            {
                foreach (T value in values)
                {
                    parameters.Add(cmd.CreateDataParameter(columnName, dataType, value));
                }
            }

            return new SqlInsideParametersCondition(cmd, isNotIn, parameters.ToArray());
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalIn<T>(AbstractSqlCommand cmd, String columnName, Boolean isNotIn, IEnumerable<T> values)
        {
            List<DataParameter> parameters = new List<DataParameter>();

            if (values != null)
            {
                DataType? dataType = null;

                foreach (T value in values)
                {
                    if (!dataType.HasValue)
                    {
                        dataType = DataTypeHelper.InternalGetDataType(value);
                    }

                    parameters.Add(cmd.CreateDataParameter(columnName, dataType.Value, value));
                }
            }

            return new SqlInsideParametersCondition(cmd, isNotIn, parameters.ToArray());
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalIn<T>(AbstractSqlCommand cmd, String columnName, Boolean isNotIn, DataType dataType, String values, Char separator) where T : IConvertible
        {
            List<DataParameter> parameters = new List<DataParameter>();

            if (!String.IsNullOrEmpty(values))
            {
                String[] valuesArray = values.Split(separator);
                Type t = typeof(T);

                for (Int32 i = 0; i < valuesArray.Length; i++)
                {
                    if (String.IsNullOrEmpty(valuesArray[i]))
                    {
                        continue;
                    }

                    Object value = Convert.ChangeType(valuesArray[i].Trim(), t);
                    parameters.Add(cmd.CreateDataParameter(columnName, dataType, value));
                }
            }

            return new SqlInsideParametersCondition(cmd, isNotIn, parameters.ToArray());
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalInInt32(AbstractSqlCommand cmd, String columnName, Boolean isNotIn, String values, Char separator)
        {
            List<DataParameter> parameters = new List<DataParameter>();

            if (!String.IsNullOrEmpty(values))
            {
                String[] valuesArray = values.Split(separator);

                for (Int32 i = 0; i < valuesArray.Length; i++)
                {
                    if (String.IsNullOrEmpty(valuesArray[i]))
                    {
                        continue;
                    }

                    Int32 value = Convert.ToInt32(valuesArray[i].Trim());
                    parameters.Add(cmd.CreateDataParameter(columnName, DataType.Int32, value));
                }
            }

            return new SqlInsideParametersCondition(cmd, isNotIn, parameters.ToArray());
        }
        #endregion

        #region In
        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="parameters">参数集合</param>
        /// <exception cref="ArgumentNullException">参数集合不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In(AbstractSqlCommand cmd, params DataParameter[] parameters)
        {
            return SqlCondition.InternalIn(cmd, false, parameters);
        }

        /// <summary>
        /// 创建新的Sql IN参数条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In(AbstractSqlCommand cmd, String columnName, DataType dataType, IEnumerable<Object> values)
        {
            return SqlCondition.InternalIn(cmd, columnName, false, dataType, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In(AbstractSqlCommand cmd, String columnName, IEnumerable<Object> values)
        {
            return SqlCondition.InternalIn(cmd, columnName, false, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In(AbstractSqlCommand cmd, String columnName, DataType dataType, Array values)
        {
            return SqlCondition.InternalIn(cmd, columnName, false, dataType, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In(AbstractSqlCommand cmd, String columnName, Array values)
        {
            return SqlCondition.InternalIn(cmd, columnName, false, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In(AbstractSqlCommand cmd, String columnName, DataType dataType, String values, Char separator)
        {
            return SqlCondition.InternalIn(cmd, columnName, false, dataType, values, separator);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition InThese<T>(AbstractSqlCommand cmd, String columnName, DataType dataType, params T[] values)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, false, dataType, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition InThese<T>(AbstractSqlCommand cmd, String columnName, params T[] values)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, false, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In<T>(AbstractSqlCommand cmd, String columnName, DataType dataType, IEnumerable<T> values)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, false, dataType, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In<T>(AbstractSqlCommand cmd, String columnName, IEnumerable<T> values)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, false, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="func">操作方法</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In<T>(AbstractSqlCommand cmd, String columnName, DataType dataType, Func<IEnumerable<T>> func)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, false, dataType, func());
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="func">操作方法</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In<T>(AbstractSqlCommand cmd, String columnName, Func<IEnumerable<T>> func)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, false, func());
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In<T>(AbstractSqlCommand cmd, String columnName, DataType dataType, String values, Char separator) where T : IConvertible
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, false, dataType, values, separator);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition InInt32(AbstractSqlCommand cmd, String columnName, String values, Char separator)
        {
            return SqlCondition.InternalInInt32(cmd, columnName, false, values, separator);
        }
        #endregion

        #region NotIn
        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="parameters">参数集合</param>
        /// <exception cref="ArgumentNullException">参数集合不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn(AbstractSqlCommand cmd, params DataParameter[] parameters)
        {
            return SqlCondition.InternalIn(cmd, true, parameters);
        }

        /// <summary>
        /// 创建新的Sql NOT IN参数条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn(AbstractSqlCommand cmd, String columnName, DataType dataType, IEnumerable<Object> values)
        {
            return SqlCondition.InternalIn(cmd, columnName, true, dataType, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn(AbstractSqlCommand cmd, String columnName, IEnumerable<Object> values)
        {
            return SqlCondition.InternalIn(cmd, columnName, true, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn(AbstractSqlCommand cmd, String columnName, DataType dataType, Array values)
        {
            return SqlCondition.InternalIn(cmd, columnName, true, dataType, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn(AbstractSqlCommand cmd, String columnName, Array values)
        {
            return SqlCondition.InternalIn(cmd, columnName, true, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn(AbstractSqlCommand cmd, String columnName, DataType dataType, String values, Char separator)
        {
            return SqlCondition.InternalIn(cmd, columnName, true, dataType, values, separator);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotInThese<T>(AbstractSqlCommand cmd, String columnName, DataType dataType, params T[] values)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, true, dataType, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotInThese<T>(AbstractSqlCommand cmd, String columnName, params T[] values)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, true, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn<T>(AbstractSqlCommand cmd, String columnName, DataType dataType, IEnumerable<T> values)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, true, dataType, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn<T>(AbstractSqlCommand cmd, String columnName, IEnumerable<T> values)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, true, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="func">操作方法</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn<T>(AbstractSqlCommand cmd, String columnName, DataType dataType, Func<IEnumerable<T>> func)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, true, dataType, func());
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="func">操作方法</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn<T>(AbstractSqlCommand cmd, String columnName, Func<IEnumerable<T>> func)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, true, func());
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn<T>(AbstractSqlCommand cmd, String columnName, DataType dataType, String values, Char separator) where T : IConvertible
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, true, dataType, values, separator);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotInInt32(AbstractSqlCommand cmd, String columnName, String values, Char separator)
        {
            return SqlCondition.InternalInInt32(cmd, columnName, true, values, separator);
        }
        #endregion
        #endregion

        #region In语句条件
        #region Internal
        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideCommandCondition InternalIn(AbstractSqlCommand cmd, String columnName, Boolean isNotIn, String tableName, Action<SelectCommand> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            return new SqlInsideCommandCondition(cmd, columnName, isNotIn, tableName, action);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideCommandCondition InternalIn(AbstractSqlCommand cmd, String columnName, Boolean isNotIn, Action<SelectCommand> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            return new SqlInsideCommandCondition(cmd, columnName, isNotIn, cmd.TableName, action);
        }
        #endregion

        #region In
        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideCommandCondition In(AbstractSqlCommand cmd, String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlCondition.InternalIn(cmd, columnName, false, tableName, action);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideCommandCondition In(AbstractSqlCommand cmd, String columnName, Action<SelectCommand> action)
        {
            return SqlCondition.InternalIn(cmd, columnName, false, action);
        }
        #endregion

        #region NotIn
        /// <summary>
        /// 创建新的Sql Not IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideCommandCondition NotIn(AbstractSqlCommand cmd, String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlCondition.InternalIn(cmd, columnName, true, tableName, action);
        }

        /// <summary>
        /// 创建新的Sql Not IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideCommandCondition NotIn(AbstractSqlCommand cmd, String columnName, Action<SelectCommand> action)
        {
            return SqlCondition.InternalIn(cmd, columnName, true, action);
        }
        #endregion
        #endregion

        #region And/Or/Not
        #region General
        /// <summary>
        /// 创建新的Sql条件语句集合
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="concatType">连接类型</param>
        /// <param name="conditions">条件语句集合</param>
        /// <returns>Sql条件语句集合</returns>
        internal static SqlConditionList InternalCreateList(AbstractSqlCommand cmd, SqlWhereConcatType concatType, IEnumerable<ISqlCondition> conditions)
        {
            SqlConditionList list = new SqlConditionList(cmd, concatType);

            if (conditions != null)
            {
                foreach (ISqlCondition condition in conditions)
                {
                    list.Add(condition);
                }
            }
            
            return list;
        }
        #endregion

        #region And
        /// <summary>
        /// 创建与连接的Sql条件语句集合
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="conditions">条件语句集合</param>
        /// <returns>Sql条件语句集合</returns>
        public static SqlConditionList And(AbstractSqlCommand cmd, params ISqlCondition[] conditions)
        {
            return SqlCondition.InternalCreateList(cmd, SqlWhereConcatType.And, conditions);
        }

        /// <summary>
        /// 创建与连接的Sql条件语句集合
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="conditions">条件语句集合</param>
        /// <returns>Sql条件语句集合</returns>
        public static SqlConditionList And(AbstractSqlCommand cmd, IEnumerable<ISqlCondition> conditions)
        {
            return SqlCondition.InternalCreateList(cmd, SqlWhereConcatType.And, conditions);
        }
        #endregion

        #region Or
        /// <summary>
        /// 创建或连接的Sql条件语句集合
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="conditions">条件语句集合</param>
        /// <returns>Sql条件语句集合</returns>
        public static SqlConditionList Or(AbstractSqlCommand cmd, params ISqlCondition[] conditions)
        {
            return SqlCondition.InternalCreateList(cmd, SqlWhereConcatType.Or, conditions);
        }

        /// <summary>
        /// 创建或连接的Sql条件语句集合
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="conditions">条件语句集合</param>
        /// <returns>Sql条件语句集合</returns>
        public static SqlConditionList Or(AbstractSqlCommand cmd, IEnumerable<ISqlCondition> conditions)
        {
            return SqlCondition.InternalCreateList(cmd, SqlWhereConcatType.Or, conditions);
        }
        #endregion

        #region Not
        /// <summary>
        /// 创建新的Sql Not条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="condition">条件语句</param>
        /// <exception cref="ArgumentNullException">Sql条件语句不能为空</exception>
        /// <returns>Sql Not条件语句</returns>
        public static SqlNotCondition Not(AbstractSqlCommand cmd, ISqlCondition condition)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            return new SqlNotCondition(cmd, condition);
        }
        #endregion
        #endregion

        #region 私有方法
        private static String GetFieldName(String tableName, String columnName)
        {
            return (String.IsNullOrEmpty(tableName) ? columnName : tableName + '.' + columnName);
        }
        #endregion
    }
}