using System;
using System.Collections.Generic;
using System.Data;

using DotMaysWind.Data.Helper;

namespace DotMaysWind.Data.Command.Condition
{
    /// <summary>
    /// Sql条件语句类
    /// </summary>
    public static class SqlCondition
    {
        #region 基本参数条件
        #region General
        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="op">条件运算符</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreate(String columnName, SqlOperator op)
        {
            return new SqlBasicParameterCondition(SqlParameter.Create(columnName, columnName + "_" + op.ToString(), null), op);
        }

        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreate(String columnName, SqlOperator op, Object value)
        {
            return new SqlBasicParameterCondition(SqlParameter.Create(columnName, columnName + "_" + op.ToString(), value), op);
        }

        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreate(String columnName, String paramName, SqlOperator op, Object value)
        {
            return new SqlBasicParameterCondition(SqlParameter.Create(columnName, paramName, value), op);
        }

        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreate(String columnName, SqlOperator op, DbType dbType, Object value)
        {
            return new SqlBasicParameterCondition(SqlParameter.Create(columnName, columnName + "_" + op.ToString(), dbType, value), op);
        }

        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreate(String columnName, String paramName, SqlOperator op, DbType dbType, Object value)
        {
            return new SqlBasicParameterCondition(SqlParameter.Create(columnName, paramName, dbType, value), op);
        }

        /// <summary>
        /// 创建双参数新的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="valueOne">数据一</param>
        /// <param name="valueTwo">数据二</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreate(String columnName, SqlOperator op, Object valueOne, Object valueTwo)
        {
            return new SqlBasicParameterCondition(SqlParameter.Create(columnName, columnName + "_" + op.ToString() + "_One", valueOne), SqlParameter.Create(columnName, columnName + "_" + op.ToString() + "_Two", valueTwo), op);
        }

        /// <summary>
        /// 创建双参数新的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="valueOne">数据一</param>
        /// <param name="valueTwo">数据二</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreate(String columnName, SqlOperator op, DbType dbType, Object valueOne, Object valueTwo)
        {
            return new SqlBasicParameterCondition(SqlParameter.Create(columnName, columnName + "_" + op.ToString() + "_One", dbType, valueOne), SqlParameter.Create(columnName, columnName + "_" + op.ToString() + "_Two", dbType, valueTwo), op);
        }

        /// <summary>
        /// 创建双参数新的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="valueOne">数据一</param>
        /// <param name="valueTwo">数据二</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreate(String columnName, String paramName, SqlOperator op, Object valueOne, Object valueTwo)
        {
            return new SqlBasicParameterCondition(SqlParameter.Create(columnName, paramName + "_One", valueOne), SqlParameter.Create(columnName, paramName + "_Two", valueTwo), op);
        }

        /// <summary>
        /// 创建双参数新的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="valueOne">数据一</param>
        /// <param name="valueTwo">数据二</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreate(String columnName, String paramName, SqlOperator op, DbType dbType, Object valueOne, Object valueTwo)
        {
            return new SqlBasicParameterCondition(SqlParameter.Create(columnName, paramName + "_One", dbType, valueOne), SqlParameter.Create(columnName, paramName + "_Two", dbType, valueTwo), op);
        }

        /// <summary>
        /// 创建双参数新的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreateColumn(String columnName, SqlOperator op, String tableNameTwo, String columnNameTwo)
        {
            return new SqlBasicParameterCondition(SqlParameter.CreateCustomAction(columnName, GetFieldName(tableNameTwo, columnNameTwo)), op);
        }

        /// <summary>
        /// 创建双参数新的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreateColumn(String columnName, SqlOperator op, String columnNameTwo)
        {
            return new SqlBasicParameterCondition(SqlParameter.CreateCustomAction(columnName, GetFieldName(String.Empty, columnNameTwo)), op);
        }
        #endregion

        #region SqlAggregateFunction
        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="function">合计函数类型</param>
        /// <param name="columnName">要查询的字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Create(SqlAggregateFunction function, String columnName, SqlOperator op, Object value)
        {
            return SqlCondition.InternalCreate(String.Format("{0}({1})", function.ToString().ToUpperInvariant(), columnName), op, value);
        }

        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="function">合计函数类型</param>
        /// <param name="op">条件运算符</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Create(SqlAggregateFunction function, SqlOperator op, Object value)
        {
            return SqlCondition.Create(function, "*", op, value);
        }
        #endregion

        #region IsNull/IsNotNull
        /// <summary>
        /// 创建判断是否为空的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition IsNull(String columnName)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.IsNull);
        }

        /// <summary>
        /// 创建判断是否非空的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition IsNotNull(String columnName)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.IsNotNull);
        }
        #endregion

        #region Equal/NotEqual
        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Equal(String columnName, Object value)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.Equal, value);
        }

        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Equal(String columnName, String paramName, Object value)
        {
            return SqlCondition.InternalCreate(columnName, paramName, SqlOperator.Equal, value);
        }

        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Equal(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.Equal, dbType, value);
        }

        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition EqualColumn(String columnName, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(columnName, SqlOperator.Equal, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition EqualColumn(String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(columnName, SqlOperator.Equal, tableNameTwo, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotEqual(String columnName, Object value)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.NotEqual, value);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql查询语句类</returns>
        public static SqlBasicParameterCondition NotEqual(String columnName, String paramName, Object value)
        {
            return SqlCondition.InternalCreate(columnName, paramName, SqlOperator.NotEqual, value);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotEqual(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.NotEqual, dbType, value);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotEqualColumn(String columnName, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(columnName, SqlOperator.NotEqual, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotEqualColumn(String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(columnName, SqlOperator.NotEqual, tableNameTwo, columnNameTwo);
        }
        #endregion

        #region GreaterThan/LessThan
        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThan(String columnName, Object value)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.GreaterThan, value);
        }

        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThan(String columnName, String paramName, Object value)
        {
            return SqlCondition.InternalCreate(columnName, paramName, SqlOperator.GreaterThan, value);
        }

        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThan(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.GreaterThan, dbType, value);
        }

        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThanColumn(String columnName, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(columnName, SqlOperator.GreaterThan, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThanColumn(String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(columnName, SqlOperator.GreaterThan, tableNameTwo, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LessThan(String columnName, Object value)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.LessThan, value);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql查询语句类</returns>
        public static SqlBasicParameterCondition LessThan(String columnName, String paramName, Object value)
        {
            return SqlCondition.InternalCreate(columnName, paramName, SqlOperator.LessThan, value);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LessThan(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.LessThan, dbType, value);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LessThanColumn(String columnName, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(columnName, SqlOperator.LessThan, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LessThanColumn(String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(columnName, SqlOperator.LessThan, tableNameTwo, columnNameTwo);
        }
        #endregion

        #region GreaterThanOrEqual/LessThanOrEqual
        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThanOrEqual(String columnName, Object value)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.GreaterThanOrEqual, value);
        }

        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThanOrEqual(String columnName, String paramName, Object value)
        {
            return SqlCondition.InternalCreate(columnName, paramName, SqlOperator.GreaterThanOrEqual, value);
        }

        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThanOrEqual(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.GreaterThanOrEqual, dbType, value);
        }

        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThanOrEqualColumn(String columnName, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(columnName, SqlOperator.GreaterThanOrEqual, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThanOrEqualColumn(String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(columnName, SqlOperator.GreaterThanOrEqual, tableNameTwo, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LessThanOrEqual(String columnName, Object value)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.LessThanOrEqual, value);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql查询语句类</returns>
        public static SqlBasicParameterCondition LessThanOrEqual(String columnName, String paramName, Object value)
        {
            return SqlCondition.InternalCreate(columnName, paramName, SqlOperator.LessThanOrEqual, value);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LessThanOrEqual(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.LessThanOrEqual, dbType, value);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LessThanOrEqualColumn(String columnName, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(columnName, SqlOperator.LessThanOrEqual, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LessThanOrEqualColumn(String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(columnName, SqlOperator.LessThanOrEqual, tableNameTwo, columnNameTwo);
        }
        #endregion

        #region Like/NotLike
        #region General
        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Like(String columnName, String value)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.Like, value);
        }

        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Like(String columnName, String paramName, String value)
        {
            return SqlCondition.InternalCreate(columnName, paramName, SqlOperator.Like, value);
        }

        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Like(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.Like, dbType, value);
        }

        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeColumn(String columnName, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(columnName, SqlOperator.Like, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeColumn(String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(columnName, SqlOperator.Like, tableNameTwo, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否不相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLike(String columnName, String value)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.NotLike, value);
        }

        /// <summary>
        /// 创建判断是否不相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql查询语句类</returns>
        public static SqlBasicParameterCondition NotLike(String columnName, String paramName, String value)
        {
            return SqlCondition.InternalCreate(columnName, paramName, SqlOperator.NotLike, value);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLike(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.NotLike, dbType, value);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLikeColumn(String columnName, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(columnName, SqlOperator.NotLike, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLikeColumn(String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.InternalCreateColumn(columnName, SqlOperator.NotLike, tableNameTwo, columnNameTwo);
        }
        #endregion

        #region LikeAll
        /// <summary>
        /// 创建判断是否包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeAll(String columnName, String value)
        {
            return SqlCondition.Like(columnName, "%" + value + "%");
        }

        /// <summary>
        /// 创建判断是否包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeAll(String columnName, String paramName, String value)
        {
            return SqlCondition.Like(columnName, paramName, "%" + value + "%");
        }

        /// <summary>
        /// 创建判断是否包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeAll(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.Like(columnName, dbType, "%" + value + "%");
        }

        /// <summary>
        /// 创建判断是否不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLikeAll(String columnName, String value)
        {
            return SqlCondition.NotLike(columnName, "%" + value + "%");
        }

        /// <summary>
        /// 创建判断是否不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql查询语句类</returns>
        public static SqlBasicParameterCondition NotLikeAll(String columnName, String paramName, String value)
        {
            return SqlCondition.NotLike(columnName, paramName, "%" + value + "%");
        }

        /// <summary>
        /// 创建判断是否不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLikeAll(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.NotLike(columnName, dbType, "%" + value + "%");
        }
        #endregion

        #region LikeStartWith
        /// <summary>
        /// 创建判断是否开头包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeStartWith(String columnName, String value)
        {
            return SqlCondition.Like(columnName, value + "%");
        }

        /// <summary>
        /// 创建判断是否开头包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeStartWith(String columnName, String paramName, String value)
        {
            return SqlCondition.Like(columnName, paramName, value + "%");
        }

        /// <summary>
        /// 创建判断是否开头包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeStartWith(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.Like(columnName, dbType, value + "%");
        }

        /// <summary>
        /// 创建判断是否开头不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLikeStartWith(String columnName, String value)
        {
            return SqlCondition.NotLike(columnName, value + "%");
        }

        /// <summary>
        /// 创建判断是否开头不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql查询语句类</returns>
        public static SqlBasicParameterCondition NotLikeStartWith(String columnName, String paramName, String value)
        {
            return SqlCondition.NotLike(columnName, paramName, value + "%");
        }

        /// <summary>
        /// 创建判断是否开头不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLikeStartWith(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.NotLike(columnName, dbType, value + "%");
        }
        #endregion

        #region LikeEndWith
        /// <summary>
        /// 创建判断是否结尾包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeEndWith(String columnName, String value)
        {
            return SqlCondition.Like(columnName, "%" + value);
        }

        /// <summary>
        /// 创建判断是否结尾包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeEndWith(String columnName, String paramName, String value)
        {
            return SqlCondition.Like(columnName, paramName, "%" + value);
        }

        /// <summary>
        /// 创建判断是否结尾包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeEndWith(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.Like(columnName, dbType, "%" + value);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLikeEndWith(String columnName, String value)
        {
            return SqlCondition.NotLike(columnName, "%" + value);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql查询语句类</returns>
        public static SqlBasicParameterCondition NotLikeEndWith(String columnName, String paramName, String value)
        {
            return SqlCondition.NotLike(columnName, paramName, "%" + value);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLikeEndWith(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.NotLike(columnName, dbType, "%" + value);
        }
        #endregion
        #endregion

        #region Between/NotBetween
        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="valueOne">数据一</param>
        /// <param name="valueTwo">数据二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Between(String columnName, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.Between, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="valueOne">数据一</param>
        /// <param name="valueTwo">数据二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Between(String columnName, String paramName, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalCreate(columnName, paramName, SqlOperator.Between, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="valueOne">数据一</param>
        /// <param name="valueTwo">数据二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Between(String columnName, DbType dbType, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.Between, dbType, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否不在范围内的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="valueOne">数据一</param>
        /// <param name="valueTwo">数据二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotBetween(String columnName, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.NotBetween, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否不在范围内的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="paramName">参数名</param>
        /// <param name="valueOne">数据一</param>
        /// <param name="valueTwo">数据二</param>
        /// <returns>Sql查询语句类</returns>
        public static SqlBasicParameterCondition NotBetween(String columnName, String paramName, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalCreate(columnName, paramName, SqlOperator.NotBetween, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否不在范围内的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="valueOne">数据一</param>
        /// <param name="valueTwo">数据二</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotBetween(String columnName, DbType dbType, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalCreate(columnName, SqlOperator.NotBetween, dbType, valueOne, valueTwo);
        }
        #endregion
        #endregion

        #region 基本语句条件
        #region General
        /// <summary>
        /// 创建新的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="op">条件运算符</param>
        /// <param name="command">选择语句</param>
        /// <exception cref="ArgumentNullException">选择语句不能为空</exception>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicCommandCondition Create(String columnName, SqlOperator op, SelectCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            return new SqlBasicCommandCondition(columnName, op, command);
        }
        #endregion

        #region Equal/NotEqual
        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition Equal(String columnName, SelectCommand command)
        {
            return SqlCondition.Create(columnName, SqlOperator.Equal, command);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition NotEqual(String columnName, SelectCommand command)
        {
            return SqlCondition.Create(columnName, SqlOperator.NotEqual, command);
        }
        #endregion

        #region GreaterThan/LessThan
        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition GreaterThan(String columnName, SelectCommand command)
        {
            return SqlCondition.Create(columnName, SqlOperator.GreaterThan, command);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition LessThan(String columnName, SelectCommand command)
        {
            return SqlCondition.Create(columnName, SqlOperator.LessThan, command);
        }
        #endregion

        #region GreaterThanOrEqual/LessThanOrEqual
        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition GreaterThanOrEqual(String columnName, SelectCommand command)
        {
            return SqlCondition.Create(columnName, SqlOperator.GreaterThanOrEqual, command);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition LessThanOrEqual(String columnName, SelectCommand command)
        {
            return SqlCondition.Create(columnName, SqlOperator.LessThanOrEqual, command);
        }
        #endregion

        #region Like/NotLike
        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition Like(String columnName, SelectCommand command)
        {
            return SqlCondition.Create(columnName, SqlOperator.Like, command);
        }

        /// <summary>
        /// 创建判断是否不相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition NotLike(String columnName, SelectCommand command)
        {
            return SqlCondition.Create(columnName, SqlOperator.NotLike, command);
        }
        #endregion
        #endregion

        #region In参数条件
        #region General
        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="parameters">参数集合</param>
        /// <exception cref="ArgumentNullException">参数集合不能为空</exception>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalCreateIn(Boolean isNotIn, params SqlParameter[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            return new SqlInsideParametersCondition(isNotIn, parameters);
        }

        /// <summary>
        /// 创建新的Sql IN参数条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalCreateIn(String columnName, Boolean isNotIn, DbType dbType, params Object[] values)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (values != null)
            {
                for (Int32 i = 0; i < values.Length; i++)
                {
                    parameters.Add(SqlParameter.Create(columnName, columnName + (isNotIn ? "_NotIn_" : "_In_") + i.ToString(), dbType, values[i]));
                }
            }

            return new SqlInsideParametersCondition(isNotIn, parameters.ToArray());
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalCreateIn(String columnName, Boolean isNotIn, params Object[] values)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (values != null)
            {
                DbType dbType = DbTypeHelper.InternalGetDbType(values[0]);

                for (Int32 i = 0; i < values.Length; i++)
                {
                    parameters.Add(SqlParameter.Create(columnName, columnName + (isNotIn ? "_NotIn_" : "_In_") + i.ToString(), dbType, values[i]));
                }
            }

            return new SqlInsideParametersCondition(isNotIn, parameters.ToArray());
        }
        
        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalCreateIn(String columnName, Boolean isNotIn, DbType dbType, Array values)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (values != null)
            {
                Int32 i = 0;

                foreach (Object obj in values)
                {
                    parameters.Add(SqlParameter.Create(columnName, columnName + (isNotIn ? "_NotIn_" : "_In_") + (i++).ToString(), dbType, obj));
                }
            }

            return new SqlInsideParametersCondition(isNotIn, parameters.ToArray());
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalCreateIn(String columnName, Boolean isNotIn, Array values)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (values != null)
            {
                DbType? dbType = null;
                Int32 i = 0;

                foreach (Object obj in values)
                {
                    if (!dbType.HasValue)
                    {
                        dbType = DbTypeHelper.InternalGetDbType(obj);
                    }

                    parameters.Add(SqlParameter.Create(columnName, columnName + (isNotIn ? "_NotIn_" : "_In_") + (i++).ToString(), dbType.Value, obj));
                }
            }

            return new SqlInsideParametersCondition(isNotIn, parameters.ToArray());
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalCreateIn(String columnName, Boolean isNotIn, DbType dbType, String values, Char separator)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!String.IsNullOrEmpty(values))
            {
                String[] valuesArray = values.Split(separator);

                for (Int32 i = 0; i < valuesArray.Length; i++)
                {
                    parameters.Add(SqlParameter.Create(columnName, columnName + (isNotIn ? "_NotIn_" : "_In_") + i.ToString(), dbType, valuesArray[i].Trim()));
                }
            }

            return new SqlInsideParametersCondition(isNotIn, parameters.ToArray());
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalCreateIn<T>(String columnName, Boolean isNotIn, DbType dbType, String values, Char separator) where T : IConvertible
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!String.IsNullOrEmpty(values))
            {
                String[] valuesArray = values.Split(separator);
                Type t = typeof(T);

                for (Int32 i = 0; i < valuesArray.Length; i++)
                {
                    Object value = Convert.ChangeType(valuesArray[i].Trim(), t);
                    parameters.Add(SqlParameter.Create(columnName, columnName + (isNotIn ? "_NotIn_" : "_In_") + i.ToString(), dbType, value));
                }
            }

            return new SqlInsideParametersCondition(isNotIn, parameters.ToArray());
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalCreateInInt32(String columnName, Boolean isNotIn, String values, Char separator)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!String.IsNullOrEmpty(values))
            {
                String[] valuesArray = values.Split(separator);

                for (Int32 i = 0; i < valuesArray.Length; i++)
                {
                    parameters.Add(SqlParameter.Create(columnName, columnName + (isNotIn ? "_NotIn_" : "_In_") + i.ToString(), DbType.Int32, Convert.ToInt32(valuesArray[i].Trim())));
                }
            }

            return new SqlInsideParametersCondition(isNotIn, parameters.ToArray());
        }
        #endregion

        #region In
        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="parameters">参数集合</param>
        /// <exception cref="ArgumentNullException">参数集合不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In(params SqlParameter[] parameters)
        {
            return SqlCondition.InternalCreateIn(false, parameters);
        }

        /// <summary>
        /// 创建新的Sql IN参数条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In(String columnName, DbType dbType, params Object[] values)
        {
            return SqlCondition.InternalCreateIn(columnName, false, dbType, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In(String columnName, params Object[] values)
        {
            return SqlCondition.InternalCreateIn(columnName, false, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In(String columnName, DbType dbType, Array values)
        {
            return SqlCondition.InternalCreateIn(columnName, true, dbType, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In(String columnName, Array values)
        {
            return SqlCondition.InternalCreateIn(columnName, true, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In(String columnName, DbType dbType, String values, Char separator)
        {
            return SqlCondition.InternalCreateIn(columnName, false, dbType, values, separator);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In<T>(String columnName, DbType dbType, String values, Char separator) where T : IConvertible
        {
            return SqlCondition.InternalCreateIn<T>(columnName, false, dbType, values, separator);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition InInt32(String columnName, String values, Char separator)
        {
            return SqlCondition.InternalCreateInInt32(columnName, false, values, separator);
        }
        #endregion

        #region NotIn
        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="parameters">参数集合</param>
        /// <exception cref="ArgumentNullException">参数集合不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn(params SqlParameter[] parameters)
        {
            return SqlCondition.InternalCreateIn(true, parameters);
        }

        /// <summary>
        /// 创建新的Sql NOT IN参数条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn(String columnName, DbType dbType, params Object[] values)
        {
            return SqlCondition.InternalCreateIn(columnName, true, dbType, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn(String columnName, params Object[] values)
        {
            return SqlCondition.InternalCreateIn(columnName, true, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn(String columnName, DbType dbType, Array values)
        {
            return SqlCondition.InternalCreateIn(columnName, true, dbType, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn(String columnName, Array values)
        {
            return SqlCondition.InternalCreateIn(columnName, true, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn(String columnName, DbType dbType, String values, Char separator)
        {
            return SqlCondition.InternalCreateIn(columnName, true, dbType, values, separator);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn<T>(String columnName, DbType dbType, String values, Char separator) where T : IConvertible
        {
            return SqlCondition.InternalCreateIn<T>(columnName, true, dbType, values, separator);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotInInt32(String columnName, String values, Char separator)
        {
            return SqlCondition.InternalCreateInInt32(columnName, true, values, separator);
        }
        #endregion
        #endregion

        #region In语句条件
        #region General
        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="isNotIn">是否不在范围内</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <exception cref="ArgumentNullException">选择语句不能为空</exception>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideCommandCondition InternalCreateIn(String columnName, Boolean isNotIn, SelectCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            return new SqlInsideCommandCondition(columnName, isNotIn, command);
        }
        #endregion

        #region In
        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <exception cref="ArgumentNullException">选择语句不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideCommandCondition In(String columnName, SelectCommand command)
        {
            return SqlCondition.InternalCreateIn(columnName, false, command);
        }
        #endregion

        #region NotIn
        /// <summary>
        /// 创建新的Sql Not IN条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <exception cref="ArgumentNullException">选择语句不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideCommandCondition NotIn(String columnName, SelectCommand command)
        {
            return SqlCondition.InternalCreateIn(columnName, true, command);
        }
        #endregion
        #endregion

        #region And/Or/Not
        #region General
        /// <summary>
        /// 创建新的Sql条件语句集合
        /// </summary>
        /// <param name="concatType">连接类型</param>
        /// <param name="conditions">条件语句集合</param>
        /// <returns>Sql条件语句集合</returns>
        internal static SqlConditionList InternalCreateList(SqlWhereConcatType concatType, params ISqlCondition[] conditions)
        {
            SqlConditionList list = new SqlConditionList(concatType);

            if (conditions != null)
            {
                for (Int32 i = 0; i < conditions.Length; i++)
                {
                    list.Add(conditions[i]);
                }
            }
            
            return list;
        }
        #endregion

        #region And
        /// <summary>
        /// 创建与连接的Sql条件语句集合
        /// </summary>
        /// <param name="conditions">条件语句集合</param>
        /// <returns>Sql条件语句集合</returns>
        public static SqlConditionList And(params ISqlCondition[] conditions)
        {
            return SqlCondition.InternalCreateList(SqlWhereConcatType.And, conditions);
        }

        /// <summary>
        /// 创建与连接的Sql条件语句集合
        /// </summary>
        /// <param name="conditions">条件语句集合</param>
        /// <returns>Sql条件语句集合</returns>
        public static SqlConditionList And(List<ISqlCondition> conditions)
        {
            return SqlCondition.InternalCreateList(SqlWhereConcatType.And, conditions.ToArray());
        }
        #endregion

        #region Or
        /// <summary>
        /// 创建或连接的Sql条件语句集合
        /// </summary>
        /// <param name="conditions">条件语句集合</param>
        /// <returns>Sql条件语句集合</returns>
        public static SqlConditionList Or(params ISqlCondition[] conditions)
        {
            return SqlCondition.InternalCreateList(SqlWhereConcatType.Or, conditions);
        }

        /// <summary>
        /// 创建或连接的Sql条件语句集合
        /// </summary>
        /// <param name="conditions">条件语句集合</param>
        /// <returns>Sql条件语句集合</returns>
        public static SqlConditionList Or(List<ISqlCondition> conditions)
        {
            return SqlCondition.InternalCreateList(SqlWhereConcatType.Or, conditions.ToArray());
        }
        #endregion

        #region Not
        /// <summary>
        /// 创建新的Sql Not条件语句
        /// </summary>
        /// <param name="condition">条件语句</param>
        /// <exception cref="ArgumentNullException">Sql条件语句不能为空</exception>
        /// <returns>Sql Not条件语句</returns>
        public static SqlNotCondition Not(ISqlCondition condition)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            return new SqlNotCondition(condition);
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