using System;
using System.Collections.Generic;
using System.Data;

using DotMaysWind.Data.Command.Function;
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
            return new SqlBasicParameterCondition(cmd, cmd.CreateSqlParameter(columnName, null), op);
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
            return new SqlBasicParameterCondition(cmd, cmd.CreateSqlParameter(columnName, value), op);
        }

        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreate(AbstractSqlCommand cmd, String columnName, SqlOperator op, DbType dbType, Object value)
        {
            return new SqlBasicParameterCondition(cmd, cmd.CreateSqlParameter(columnName, dbType, value), op);
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
            return new SqlBasicParameterCondition(cmd, cmd.CreateSqlParameter(columnName, valueOne), cmd.CreateSqlParameter(columnName, valueTwo), op);
        }

        /// <summary>
        /// 创建双参数新的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="valueOne">数据一</param>
        /// <param name="valueTwo">数据二</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalCreate(AbstractSqlCommand cmd, String columnName, SqlOperator op, DbType dbType, Object valueOne, Object valueTwo)
        {
            return new SqlBasicParameterCondition(cmd, cmd.CreateSqlParameter(columnName, dbType, valueOne), cmd.CreateSqlParameter(columnName, dbType, valueTwo), op);
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
            return new SqlBasicParameterCondition(cmd, cmd.CreateSqlParameterCustomAction(columnName, action), op);
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
            return new SqlBasicParameterCondition(cmd, cmd.CreateSqlParameterCustomAction(columnName, GetFieldName(tableNameTwo, columnNameTwo)), op);
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
            return new SqlBasicParameterCondition(cmd, cmd.CreateSqlParameterCustomAction(columnName, GetFieldName(String.Empty, columnNameTwo)), op);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Create(AbstractSqlCommand cmd, SqlAggregateFunction function, String columnName, SqlOperator op, DbType dbType, Object value)
        {
            return SqlCondition.InternalCreate(cmd, String.Format("{0}({1})", function.ToString().ToUpperInvariant(), columnName), op, dbType, value);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Create(AbstractSqlCommand cmd, ISqlFunction function, SqlOperator op, DbType dbType, Object value)
        {
            return SqlCondition.InternalCreate(cmd, function.GetCommandText(), op, dbType, value);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Equal(AbstractSqlCommand cmd, String columnName, DbType dbType, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, SqlOperator.Equal, dbType, value);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotEqual(AbstractSqlCommand cmd, String columnName, DbType dbType, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, SqlOperator.NotEqual, dbType, value);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThan(AbstractSqlCommand cmd, String columnName, DbType dbType, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, SqlOperator.GreaterThan, dbType, value);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LessThan(AbstractSqlCommand cmd, String columnName, DbType dbType, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, SqlOperator.LessThan, dbType, value);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition GreaterThanOrEqual(AbstractSqlCommand cmd, String columnName, DbType dbType, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, SqlOperator.GreaterThanOrEqual, dbType, value);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LessThanOrEqual(AbstractSqlCommand cmd, String columnName, DbType dbType, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, SqlOperator.LessThanOrEqual, dbType, value);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalLike(AbstractSqlCommand cmd, String columnName, Boolean isNot, DbType dbType, Object value)
        {
            return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.NotLike : SqlOperator.Like), dbType, value);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Like(AbstractSqlCommand cmd, String columnName, DbType dbType, Object value)
        {
            return SqlCondition.InternalLike(cmd, columnName, false, dbType, value);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLike(AbstractSqlCommand cmd, String columnName, DbType dbType, Object value)
        {
            return SqlCondition.InternalLike(cmd, columnName, true, dbType, value);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeAll(AbstractSqlCommand cmd, String columnName, DbType dbType, Object value)
        {
            return SqlCondition.Like(cmd, columnName, dbType, "%" + value + "%");
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
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLikeAll(AbstractSqlCommand cmd, String columnName, DbType dbType, Object value)
        {
            return SqlCondition.NotLike(cmd, columnName, dbType, "%" + value + "%");
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
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeStartWith(AbstractSqlCommand cmd, String columnName, DbType dbType, Object value)
        {
            return SqlCondition.Like(cmd, columnName, dbType, value + "%");
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
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLikeStartWith(AbstractSqlCommand cmd, String columnName, DbType dbType, Object value)
        {
            return SqlCondition.NotLike(cmd, columnName, dbType, value + "%");
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
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition LikeEndWith(AbstractSqlCommand cmd, String columnName, DbType dbType, Object value)
        {
            return SqlCondition.Like(cmd, columnName, dbType, "%" + value);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotLikeEndWith(AbstractSqlCommand cmd, String columnName, DbType dbType, Object value)
        {
            return SqlCondition.NotLike(cmd, columnName, dbType, "%" + value);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalBetween(AbstractSqlCommand cmd, String columnName, Boolean isNot, DbType dbType, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.NotBetween : SqlOperator.Between), dbType, valueOne, valueTwo);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition Between(AbstractSqlCommand cmd, String columnName, DbType dbType, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalBetween(cmd, columnName, false, dbType, valueOne, valueTwo);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotBetween(AbstractSqlCommand cmd, String columnName, DbType dbType, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalBetween(cmd, columnName, true, dbType, valueOne, valueTwo);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalBetweenNullable(AbstractSqlCommand cmd, String columnName, Boolean isNot, DbType dbType, Object valueOne, Object valueTwo)
        {
            if (valueOne != null && valueTwo != null)
            {
                return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.NotBetween : SqlOperator.Between), dbType, valueOne, valueTwo);
            }
            else if (valueOne != null && valueTwo == null)
            {
                return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.LessThan : SqlOperator.GreaterThanOrEqual), dbType, valueOne);
            }
            else if (valueOne == null && valueTwo != null)
            {
                return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.GreaterThan : SqlOperator.LessThanOrEqual), dbType, valueTwo);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicParameterCondition InternalBetweenNullable<T>(AbstractSqlCommand cmd, String columnName, Boolean isNot, DbType dbType, T? valueOne, T? valueTwo) where T : struct
        {
            if (valueOne.HasValue && valueTwo.HasValue)
            {
                return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.NotBetween : SqlOperator.Between), dbType, valueOne, valueTwo);
            }
            else if (valueOne.HasValue && !valueTwo.HasValue)
            {
                return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.LessThan : SqlOperator.GreaterThanOrEqual), dbType, valueOne);
            }
            else if (!valueOne.HasValue && valueTwo.HasValue)
            {
                return SqlCondition.InternalCreate(cmd, columnName, (isNot ? SqlOperator.GreaterThan : SqlOperator.LessThanOrEqual), dbType, valueTwo);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="valueOne">可空开始值</param>
        /// <param name="valueTwo">可空结束值</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition BetweenNullable(AbstractSqlCommand cmd, String columnName, DbType dbType, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalBetweenNullable(cmd, columnName, false, dbType, valueOne, valueTwo);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition BetweenNullable<T>(AbstractSqlCommand cmd, String columnName, DbType dbType, T? valueOne, T? valueTwo) where T : struct
        {
            return SqlCondition.InternalBetweenNullable<T>(cmd, columnName, false, dbType, valueOne, valueTwo);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="valueOne">可空开始值</param>
        /// <param name="valueTwo">可空结束值</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotBetweenNullable(AbstractSqlCommand cmd, String columnName, DbType dbType, Object valueOne, Object valueTwo)
        {
            return SqlCondition.InternalBetweenNullable(cmd, columnName, true, dbType, valueOne, valueTwo);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicParameterCondition NotBetweenNullable<T>(AbstractSqlCommand cmd, String columnName, DbType dbType, T? valueOne, T? valueTwo) where T : struct
        {
            return SqlCondition.InternalBetweenNullable<T>(cmd, columnName, true, dbType, valueOne, valueTwo);
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
        /// <param name="command">选择语句</param>
        /// <exception cref="ArgumentNullException">选择语句不能为空</exception>
        /// <returns>Sql条件语句</returns>
        internal static SqlBasicCommandCondition Create(AbstractSqlCommand cmd, String columnName, SqlOperator op, SelectCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            return new SqlBasicCommandCondition(cmd, columnName, op, command);
        }
        #endregion

        #region Equal/NotEqual
        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition Equal(AbstractSqlCommand cmd, String columnName, SelectCommand command)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.Equal, command);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition NotEqual(AbstractSqlCommand cmd, String columnName, SelectCommand command)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.NotEqual, command);
        }
        #endregion

        #region GreaterThan/LessThan
        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition GreaterThan(AbstractSqlCommand cmd, String columnName, SelectCommand command)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.GreaterThan, command);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition LessThan(AbstractSqlCommand cmd, String columnName, SelectCommand command)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.LessThan, command);
        }
        #endregion

        #region GreaterThanOrEqual/LessThanOrEqual
        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition GreaterThanOrEqual(AbstractSqlCommand cmd, String columnName, SelectCommand command)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.GreaterThanOrEqual, command);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition LessThanOrEqual(AbstractSqlCommand cmd, String columnName, SelectCommand command)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.LessThanOrEqual, command);
        }
        #endregion

        #region Like/NotLike
        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition Like(AbstractSqlCommand cmd, String columnName, SelectCommand command)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.Like, command);
        }

        /// <summary>
        /// 创建判断是否不相似的Sql条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public static SqlBasicCommandCondition NotLike(AbstractSqlCommand cmd, String columnName, SelectCommand command)
        {
            return SqlCondition.Create(cmd, columnName, SqlOperator.NotLike, command);
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
        internal static SqlInsideParametersCondition InternalIn(AbstractSqlCommand cmd, Boolean isNotIn, params SqlParameter[] parameters)
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
        /// <param name="dbType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalIn(AbstractSqlCommand cmd, String columnName, Boolean isNotIn, DbType dbType, params Object[] values)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (values != null)
            {
                for (Int32 i = 0; i < values.Length; i++)
                {
                    parameters.Add(cmd.CreateSqlParameter(columnName, dbType, values[i]));
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
        internal static SqlInsideParametersCondition InternalIn(AbstractSqlCommand cmd, String columnName, Boolean isNotIn, params Object[] values)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (values != null)
            {
                DbType dbType = DbTypeHelper.InternalGetDbType(values[0]);

                for (Int32 i = 0; i < values.Length; i++)
                {
                    parameters.Add(cmd.CreateSqlParameter(columnName, dbType, values[i]));
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
        /// <param name="dbType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalIn(AbstractSqlCommand cmd, String columnName, Boolean isNotIn, DbType dbType, Array values)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (values != null)
            {
                foreach (Object obj in values)
                {
                    parameters.Add(cmd.CreateSqlParameter(columnName, dbType, obj));
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
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (values != null)
            {
                DbType? dbType = null;

                foreach (Object obj in values)
                {
                    if (!dbType.HasValue)
                    {
                        dbType = DbTypeHelper.InternalGetDbType(obj);
                    }

                    parameters.Add(cmd.CreateSqlParameter(columnName, dbType.Value, obj));
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
        /// <param name="dbType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalIn(AbstractSqlCommand cmd, String columnName, Boolean isNotIn, DbType dbType, String values, Char separator)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!String.IsNullOrEmpty(values))
            {
                String[] valuesArray = values.Split(separator);

                for (Int32 i = 0; i < valuesArray.Length; i++)
                {
                    parameters.Add(cmd.CreateSqlParameter(columnName, dbType, valuesArray[i].Trim()));
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
        /// <param name="dbType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalIn<T>(AbstractSqlCommand cmd, String columnName, Boolean isNotIn, DbType dbType, params T[] values)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (values != null)
            {
                for (Int32 i = 0; i < values.Length; i++)
                {
                    parameters.Add(cmd.CreateSqlParameter(columnName, dbType, values[i]));
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
        internal static SqlInsideParametersCondition InternalIn<T>(AbstractSqlCommand cmd, String columnName, Boolean isNotIn, params T[] values)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (values != null)
            {
                DbType dbType = DbTypeHelper.InternalGetDbType(values[0]);

                for (Int32 i = 0; i < values.Length; i++)
                {
                    parameters.Add(cmd.CreateSqlParameter(columnName, dbType, values[i]));
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
        /// <param name="dbType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideParametersCondition InternalIn<T>(AbstractSqlCommand cmd, String columnName, Boolean isNotIn, DbType dbType, String values, Char separator) where T : IConvertible
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!String.IsNullOrEmpty(values))
            {
                String[] valuesArray = values.Split(separator);
                Type t = typeof(T);

                for (Int32 i = 0; i < valuesArray.Length; i++)
                {
                    Object value = Convert.ChangeType(valuesArray[i].Trim(), t);
                    parameters.Add(cmd.CreateSqlParameter(columnName, dbType, value));
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
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!String.IsNullOrEmpty(values))
            {
                String[] valuesArray = values.Split(separator);

                for (Int32 i = 0; i < valuesArray.Length; i++)
                {
                    parameters.Add(cmd.CreateSqlParameter(columnName, DbType.Int32, Convert.ToInt32(valuesArray[i].Trim())));
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
        public static SqlInsideParametersCondition In(AbstractSqlCommand cmd, params SqlParameter[] parameters)
        {
            return SqlCondition.InternalIn(cmd, false, parameters);
        }

        /// <summary>
        /// 创建新的Sql IN参数条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In(AbstractSqlCommand cmd, String columnName, DbType dbType, params Object[] values)
        {
            return SqlCondition.InternalIn(cmd, columnName, false, dbType, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In(AbstractSqlCommand cmd, String columnName, params Object[] values)
        {
            return SqlCondition.InternalIn(cmd, columnName, false, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In(AbstractSqlCommand cmd, String columnName, DbType dbType, Array values)
        {
            return SqlCondition.InternalIn(cmd, columnName, false, dbType, values);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In(AbstractSqlCommand cmd, String columnName, DbType dbType, String values, Char separator)
        {
            return SqlCondition.InternalIn(cmd, columnName, false, dbType, values, separator);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In<T>(AbstractSqlCommand cmd, String columnName, DbType dbType, params T[] values)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, false, dbType, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In<T>(AbstractSqlCommand cmd, String columnName, params T[] values)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, false, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="func">操作方法</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In<T>(AbstractSqlCommand cmd, String columnName, DbType dbType, Func<T[]> func)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, false, dbType, func());
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="func">操作方法</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In<T>(AbstractSqlCommand cmd, String columnName, Func<T[]> func)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, false, func());
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition In<T>(AbstractSqlCommand cmd, String columnName, DbType dbType, String values, Char separator) where T : IConvertible
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, false, dbType, values, separator);
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
        public static SqlInsideParametersCondition NotIn(AbstractSqlCommand cmd, params SqlParameter[] parameters)
        {
            return SqlCondition.InternalIn(cmd, true, parameters);
        }

        /// <summary>
        /// 创建新的Sql NOT IN参数条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn(AbstractSqlCommand cmd, String columnName, DbType dbType, params Object[] values)
        {
            return SqlCondition.InternalIn(cmd, columnName, true, dbType, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn(AbstractSqlCommand cmd, String columnName, params Object[] values)
        {
            return SqlCondition.InternalIn(cmd, columnName, true, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn(AbstractSqlCommand cmd, String columnName, DbType dbType, Array values)
        {
            return SqlCondition.InternalIn(cmd, columnName, true, dbType, values);
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
        /// <param name="dbType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn(AbstractSqlCommand cmd, String columnName, DbType dbType, String values, Char separator)
        {
            return SqlCondition.InternalIn(cmd, columnName, true, dbType, values, separator);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn<T>(AbstractSqlCommand cmd, String columnName, DbType dbType, params T[] values)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, true, dbType, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn<T>(AbstractSqlCommand cmd, String columnName, params T[] values)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, true, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="func">操作方法</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn<T>(AbstractSqlCommand cmd, String columnName, DbType dbType, Func<T[]> func)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, true, dbType, func());
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="func">操作方法</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn<T>(AbstractSqlCommand cmd, String columnName, Func<T[]> func)
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, true, func());
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideParametersCondition NotIn<T>(AbstractSqlCommand cmd, String columnName, DbType dbType, String values, Char separator) where T : IConvertible
        {
            return SqlCondition.InternalIn<T>(cmd, columnName, true, dbType, values, separator);
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
        /// <param name="command">选择语句</param>
        /// <exception cref="ArgumentNullException">选择语句不能为空</exception>
        /// <returns>Sql条件语句</returns>
        internal static SqlInsideCommandCondition InternalIn(AbstractSqlCommand cmd, String columnName, Boolean isNotIn, SelectCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            return new SqlInsideCommandCondition(cmd, columnName, isNotIn, command);
        }
        #endregion

        #region In
        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <exception cref="ArgumentNullException">选择语句不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideCommandCondition In(AbstractSqlCommand cmd, String columnName, SelectCommand command)
        {
            return SqlCondition.InternalIn(cmd, columnName, false, command);
        }
        #endregion

        #region NotIn
        /// <summary>
        /// 创建新的Sql Not IN条件语句
        /// </summary>
        /// <param name="cmd">Sql语句</param>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <exception cref="ArgumentNullException">选择语句不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public static SqlInsideCommandCondition NotIn(AbstractSqlCommand cmd, String columnName, SelectCommand command)
        {
            return SqlCondition.InternalIn(cmd, columnName, true, command);
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
        internal static SqlConditionList InternalCreateList(AbstractSqlCommand cmd, SqlWhereConcatType concatType, params ISqlCondition[] conditions)
        {
            SqlConditionList list = new SqlConditionList(cmd, concatType);

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
        public static SqlConditionList And(AbstractSqlCommand cmd, List<ISqlCondition> conditions)
        {
            return SqlCondition.InternalCreateList(cmd, SqlWhereConcatType.And, conditions.ToArray());
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
        public static SqlConditionList Or(AbstractSqlCommand cmd, List<ISqlCondition> conditions)
        {
            return SqlCondition.InternalCreateList(cmd, SqlWhereConcatType.Or, conditions.ToArray());
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