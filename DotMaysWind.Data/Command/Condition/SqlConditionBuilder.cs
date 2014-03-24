﻿using System;
using System.Collections.Generic;
using System.Data;

namespace DotMaysWind.Data.Command.Condition
{
    /// <summary>
    /// Sql条件语句生成类
    /// </summary>
    public class SqlConditionBuilder
    {
        #region 字段
        private AbstractSqlCommand _baseCommand;
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的Sql语句条件类
        /// </summary>
        /// <param name="baseCommand">源语句</param>
        internal SqlConditionBuilder(AbstractSqlCommand baseCommand)
        {
            this._baseCommand = baseCommand;
        }
        #endregion

        #region 基本参数条件
        #region SqlAggregateFunction
        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="function">合计函数类型</param>
        /// <param name="columnName">要查询的字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition Create(SqlAggregateFunction function, String columnName, SqlOperator op, Object value)
        {
            return SqlCondition.Create(this._baseCommand, function, columnName, op, value);
        }

        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="function">合计函数类型</param>
        /// <param name="columnName">要查询的字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition Create(SqlAggregateFunction function, String columnName, SqlOperator op, DbType dbType, Object value)
        {
            return SqlCondition.Create(this._baseCommand, function, columnName, op, dbType, value);
        }

        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="function">合计函数类型</param>
        /// <param name="op">条件运算符</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition Create(SqlAggregateFunction function, SqlOperator op, Object value)
        {
            return SqlCondition.Create(this._baseCommand, function, op, value);
        }
        #endregion

        #region SqlFunction
        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="function">Sql函数</param>
        /// <param name="op">条件运算符</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition Create(ISqlFunction function, SqlOperator op, Object value)
        {
            return SqlCondition.Create(this._baseCommand, function, op, value);
        }

        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="function">Sql函数</param>
        /// <param name="op">条件运算符</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition Create(ISqlFunction function, SqlOperator op, DbType dbType, Object value)
        {
            return SqlCondition.Create(this._baseCommand, function, op, dbType, value);
        }
        #endregion

        #region IsNull/IsNotNull
        /// <summary>
        /// 创建判断是否为空的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition IsNull(String columnName)
        {
            return SqlCondition.IsNull(this._baseCommand, columnName);
        }

        /// <summary>
        /// 创建判断是否非空的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition IsNotNull(String columnName)
        {
            return SqlCondition.IsNotNull(this._baseCommand, columnName);
        }
        #endregion

        #region Equal/NotEqual
        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition Equal(String columnName, Object value)
        {
            return SqlCondition.Equal(this._baseCommand, columnName, value);
        }

        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition Equal(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.Equal(this._baseCommand, columnName, dbType, value);
        }

        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition EqualColumn(String columnName, String columnNameTwo)
        {
            return SqlCondition.EqualColumn(this._baseCommand, columnName, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition EqualColumn(String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.EqualColumn(this._baseCommand, columnName, tableNameTwo, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotEqual(String columnName, Object value)
        {
            return SqlCondition.NotEqual(this._baseCommand, columnName, value);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotEqual(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.NotEqual(this._baseCommand, columnName, dbType, value);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotEqualColumn(String columnName, String columnNameTwo)
        {
            return SqlCondition.NotEqualColumn(this._baseCommand, columnName, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotEqualColumn(String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.NotEqualColumn(this._baseCommand, columnName, tableNameTwo, columnNameTwo);
        }
        #endregion

        #region GreaterThan/LessThan
        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition GreaterThan(String columnName, Object value)
        {
            return SqlCondition.GreaterThan(this._baseCommand, columnName, value);
        }

        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition GreaterThan(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.GreaterThan(this._baseCommand, columnName, dbType, value);
        }

        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition GreaterThanColumn(String columnName, String columnNameTwo)
        {
            return SqlCondition.GreaterThanColumn(this._baseCommand, columnName, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition GreaterThanColumn(String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.GreaterThanColumn(this._baseCommand, columnName, tableNameTwo, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LessThan(String columnName, Object value)
        {
            return SqlCondition.LessThan(this._baseCommand, columnName, value);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LessThan(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.LessThan(this._baseCommand, columnName, dbType, value);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LessThanColumn(String columnName, String columnNameTwo)
        {
            return SqlCondition.LessThanColumn(this._baseCommand, columnName, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LessThanColumn(String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.LessThanColumn(this._baseCommand, columnName, tableNameTwo, columnNameTwo);
        }
        #endregion

        #region GreaterThanOrEqual/LessThanOrEqual
        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition GreaterThanOrEqual(String columnName, Object value)
        {
            return SqlCondition.GreaterThanOrEqual(this._baseCommand, columnName,value);
        }

        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition GreaterThanOrEqual(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.GreaterThanOrEqual(this._baseCommand, columnName, dbType, value);
        }

        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition GreaterThanOrEqualColumn(String columnName, String columnNameTwo)
        {
            return SqlCondition.GreaterThanOrEqualColumn(this._baseCommand, columnName, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition GreaterThanOrEqualColumn(String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.GreaterThanOrEqualColumn(this._baseCommand, columnName, tableNameTwo, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LessThanOrEqual(String columnName, Object value)
        {
            return SqlCondition.LessThanOrEqual(this._baseCommand, columnName, value);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LessThanOrEqual(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.LessThanOrEqual(this._baseCommand, columnName, dbType, value);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LessThanOrEqualColumn(String columnName, String columnNameTwo)
        {
            return SqlCondition.LessThanOrEqualColumn(this._baseCommand, columnName, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LessThanOrEqualColumn(String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.LessThanOrEqualColumn(this._baseCommand, columnName, tableNameTwo, columnNameTwo);
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
        public SqlBasicParameterCondition Like(String columnName, String value)
        {
            return SqlCondition.Like(this._baseCommand, columnName, value);
        }

        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition Like(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.Like(this._baseCommand, columnName, dbType, value);
        }

        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LikeColumn(String columnName, String columnNameTwo)
        {
            return SqlCondition.LikeColumn(this._baseCommand, columnName, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LikeColumn(String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.LikeColumn(this._baseCommand, columnName, tableNameTwo, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否不相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotLike(String columnName, String value)
        {
            return SqlCondition.NotLike(this._baseCommand, columnName, value);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotLike(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.NotLike(this._baseCommand, columnName, dbType, value);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotLikeColumn(String columnName, String columnNameTwo)
        {
            return SqlCondition.NotLikeColumn(this._baseCommand, columnName, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="tableNameTwo">数据表名二</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotLikeColumn(String columnName, String tableNameTwo, String columnNameTwo)
        {
            return SqlCondition.NotLikeColumn(this._baseCommand, columnName, tableNameTwo, columnNameTwo);
        }
        #endregion

        #region LikeAll
        /// <summary>
        /// 创建判断是否包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LikeAll(String columnName, String value)
        {
            return SqlCondition.LikeAll(this._baseCommand, columnName, value);
        }

        /// <summary>
        /// 创建判断是否包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LikeAll(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.LikeAll(this._baseCommand, columnName, dbType, value);
        }

        /// <summary>
        /// 创建判断是否不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotLikeAll(String columnName, String value)
        {
            return SqlCondition.NotLikeAll(this._baseCommand, columnName, value);
        }

        /// <summary>
        /// 创建判断是否不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotLikeAll(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.NotLikeAll(this._baseCommand, columnName, dbType, value);
        }
        #endregion

        #region LikeStartWith
        /// <summary>
        /// 创建判断是否开头包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LikeStartWith(String columnName, String value)
        {
            return SqlCondition.LikeStartWith(this._baseCommand, columnName, value);
        }

        /// <summary>
        /// 创建判断是否开头包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LikeStartWith(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.LikeStartWith(this._baseCommand, columnName, dbType, value);
        }

        /// <summary>
        /// 创建判断是否开头不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotLikeStartWith(String columnName, String value)
        {
            return SqlCondition.NotLikeStartWith(this._baseCommand, columnName, value);
        }

        /// <summary>
        /// 创建判断是否开头不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotLikeStartWith(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.NotLikeStartWith(this._baseCommand, columnName, dbType, value);
        }
        #endregion

        #region LikeEndWith
        /// <summary>
        /// 创建判断是否结尾包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LikeEndWith(String columnName, String value)
        {
            return SqlCondition.LikeEndWith(this._baseCommand, columnName, value);
        }

        /// <summary>
        /// 创建判断是否结尾包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LikeEndWith(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.LikeEndWith(this._baseCommand, columnName, dbType, value);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotLikeEndWith(String columnName, String value)
        {
            return SqlCondition.NotLikeEndWith(this._baseCommand, columnName, value);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotLikeEndWith(String columnName, DbType dbType, Object value)
        {
            return SqlCondition.NotLikeEndWith(this._baseCommand, columnName, dbType, value);
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
        public SqlBasicParameterCondition Between(String columnName, Object valueOne, Object valueTwo)
        {
            return SqlCondition.Between(this._baseCommand, columnName, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="valueOne">数据一</param>
        /// <param name="valueTwo">数据二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition Between(String columnName, DbType dbType, Object valueOne, Object valueTwo)
        {
            return SqlCondition.Between(this._baseCommand, columnName, dbType, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否不在范围内的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="valueOne">数据一</param>
        /// <param name="valueTwo">数据二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotBetween(String columnName, Object valueOne, Object valueTwo)
        {
            return SqlCondition.NotBetween(this._baseCommand, columnName, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否不在范围内的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="valueOne">数据一</param>
        /// <param name="valueTwo">数据二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotBetween(String columnName, DbType dbType, Object valueOne, Object valueTwo)
        {
            return SqlCondition.NotBetween(this._baseCommand, columnName, dbType, valueOne, valueTwo);
        }
        #endregion
        #endregion

        #region 基本语句条件
        #region Equal/NotEqual
        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition Equal(String columnName, SelectCommand command)
        {
            return SqlCondition.Equal(this._baseCommand, columnName, command);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition NotEqual(String columnName, SelectCommand command)
        {
            return SqlCondition.NotEqual(this._baseCommand, columnName, command);
        }
        #endregion

        #region GreaterThan/LessThan
        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition GreaterThan(String columnName, SelectCommand command)
        {
            return SqlCondition.GreaterThan(this._baseCommand, columnName, command);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition LessThan(String columnName, SelectCommand command)
        {
            return SqlCondition.LessThan(this._baseCommand, columnName, command);
        }
        #endregion

        #region GreaterThanOrEqual/LessThanOrEqual
        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition GreaterThanOrEqual(String columnName, SelectCommand command)
        {
            return SqlCondition.GreaterThanOrEqual(this._baseCommand, columnName, command);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition LessThanOrEqual(String columnName, SelectCommand command)
        {
            return SqlCondition.LessThanOrEqual(this._baseCommand, columnName, command);
        }
        #endregion

        #region Like/NotLike
        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition Like(String columnName, SelectCommand command)
        {
            return SqlCondition.Like(this._baseCommand, columnName, command);
        }

        /// <summary>
        /// 创建判断是否不相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition NotLike(String columnName, SelectCommand command)
        {
            return SqlCondition.NotLike(this._baseCommand, columnName, command);
        }
        #endregion
        #endregion

        #region In参数条件
        #region In
        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="parameters">参数集合</param>
        /// <exception cref="ArgumentNullException">参数集合不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition In(params SqlParameter[] parameters)
        {
            return SqlCondition.In(this._baseCommand, parameters);
        }

        /// <summary>
        /// 创建新的Sql IN参数条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition In(String columnName, DbType dbType, params Object[] values)
        {
            return SqlCondition.In(this._baseCommand, columnName, dbType, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition In(String columnName, params Object[] values)
        {
            return SqlCondition.In(this._baseCommand, columnName, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition In(String columnName, DbType dbType, Array values)
        {
            return SqlCondition.In(this._baseCommand, columnName, dbType, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition In(String columnName, Array values)
        {
            return SqlCondition.In(this._baseCommand, columnName, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition In(String columnName, DbType dbType, String values, Char separator)
        {
            return SqlCondition.In(this._baseCommand, columnName, dbType, values, separator);
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
        public SqlInsideParametersCondition In<T>(String columnName, DbType dbType, String values, Char separator) where T : IConvertible
        {
            return SqlCondition.In<T>(this._baseCommand, columnName, dbType, values, separator);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition InInt32(String columnName, String values, Char separator)
        {
            return SqlCondition.InInt32(this._baseCommand, columnName, values, separator);
        }
        #endregion

        #region NotIn
        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="parameters">参数集合</param>
        /// <exception cref="ArgumentNullException">参数集合不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotIn(params SqlParameter[] parameters)
        {
            return SqlCondition.NotIn(this._baseCommand, parameters);
        }

        /// <summary>
        /// 创建新的Sql NOT IN参数条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotIn(String columnName, DbType dbType, params Object[] values)
        {
            return SqlCondition.NotIn(this._baseCommand, columnName, dbType, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotIn(String columnName, params Object[] values)
        {
            return SqlCondition.NotIn(this._baseCommand, columnName, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotIn(String columnName, DbType dbType, Array values)
        {
            return SqlCondition.NotIn(this._baseCommand, columnName, dbType, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotIn(String columnName, Array values)
        {
            return SqlCondition.NotIn(this._baseCommand, columnName, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotIn(String columnName, DbType dbType, String values, Char separator)
        {
            return SqlCondition.NotIn(this._baseCommand, columnName, dbType, values, separator);
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
        public SqlInsideParametersCondition NotIn<T>(String columnName, DbType dbType, String values, Char separator) where T : IConvertible
        {
            return SqlCondition.NotIn<T>(this._baseCommand, columnName, dbType, values, separator);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotInInt32(String columnName, String values, Char separator)
        {
            return SqlCondition.NotInInt32(this._baseCommand, columnName, values, separator);
        }
        #endregion
        #endregion

        #region In语句条件
        #region In
        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="command">选择语句</param>
        /// <exception cref="ArgumentNullException">选择语句不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlInsideCommandCondition In(String columnName, SelectCommand command)
        {
            return SqlCondition.In(this._baseCommand, columnName, command);
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
        public SqlInsideCommandCondition NotIn(String columnName, SelectCommand command)
        {
            return SqlCondition.NotIn(this._baseCommand, columnName, command);
        }
        #endregion
        #endregion

        #region And/Or/Not
        #region And
        /// <summary>
        /// 创建与连接的Sql条件语句集合
        /// </summary>
        /// <param name="conditions">条件语句集合</param>
        /// <returns>Sql条件语句集合</returns>
        public SqlConditionList And(params ISqlCondition[] conditions)
        {
            return SqlCondition.And(this._baseCommand, conditions);
        }

        /// <summary>
        /// 创建与连接的Sql条件语句集合
        /// </summary>
        /// <param name="conditions">条件语句集合</param>
        /// <returns>Sql条件语句集合</returns>
        public SqlConditionList And(List<ISqlCondition> conditions)
        {
            return SqlCondition.And(this._baseCommand, conditions.ToArray());
        }
        #endregion

        #region Or
        /// <summary>
        /// 创建或连接的Sql条件语句集合
        /// </summary>
        /// <param name="conditions">条件语句集合</param>
        /// <returns>Sql条件语句集合</returns>
        public SqlConditionList Or(params ISqlCondition[] conditions)
        {
            return SqlCondition.Or(this._baseCommand, conditions);
        }

        /// <summary>
        /// 创建或连接的Sql条件语句集合
        /// </summary>
        /// <param name="conditions">条件语句集合</param>
        /// <returns>Sql条件语句集合</returns>
        public SqlConditionList Or(List<ISqlCondition> conditions)
        {
            return SqlCondition.Or(this._baseCommand, conditions.ToArray());
        }
        #endregion

        #region Not
        /// <summary>
        /// 创建新的Sql Not条件语句
        /// </summary>
        /// <param name="condition">条件语句</param>
        /// <exception cref="ArgumentNullException">Sql条件语句不能为空</exception>
        /// <returns>Sql Not条件语句</returns>
        public SqlNotCondition Not(ISqlCondition condition)
        {
            return SqlCondition.Not(this._baseCommand, condition);
        }
        #endregion
        #endregion
    }
}