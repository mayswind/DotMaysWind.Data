using System;
using System.Collections.Generic;

namespace DotMaysWind.Data.Command.Condition
{
    /// <summary>
    /// Sql条件语句生成类
    /// </summary>
    public sealed class SqlConditionBuilder
    {
        #region 字段
        private AbstractSqlCommandWithWhere _baseCommand;
        #endregion

        #region 属性
        /// <summary>
        /// 获取当前的语句
        /// </summary>
        public AbstractSqlCommandWithWhere Command
        {
            get { return this._baseCommand; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的Sql语句条件类
        /// </summary>
        /// <param name="baseCommand">源语句</param>
        internal SqlConditionBuilder(AbstractSqlCommandWithWhere baseCommand)
        {
            this._baseCommand = baseCommand;
        }
        #endregion

        #region 常量方法
        /// <summary>
        /// 创建永远为真的Sql条件语句
        /// </summary>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition True()
        {
            return SqlBasicParameterCondition.InternalCreateAction(this._baseCommand, "1", SqlOperator.Equal, "1");
        }

        /// <summary>
        /// 创建永远为假的Sql条件语句
        /// </summary>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition False()
        {
            return SqlBasicParameterCondition.InternalCreateAction(this._baseCommand, "1", SqlOperator.Equal, "0");
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
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, String.Format("{0}({1})", function.ToString().ToUpperInvariant(), columnName), op, value);
        }

        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="function">合计函数类型</param>
        /// <param name="columnName">要查询的字段名</param>
        /// <param name="op">条件运算符</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition Create(SqlAggregateFunction function, String columnName, SqlOperator op, DataType dataType, Object value)
        {
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, String.Format("{0}({1})", function.ToString().ToUpperInvariant(), columnName), op, dataType, value);
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
            return this.Create(function, "*", op, value);
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
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, function.GetCommandText(), op, value);
        }

        /// <summary>
        /// 创建单参数新的Sql条件语句
        /// </summary>
        /// <param name="function">Sql函数</param>
        /// <param name="op">条件运算符</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition Create(ISqlFunction function, SqlOperator op, DataType dataType, Object value)
        {
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, function.GetCommandText(), op, dataType, value);
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
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.IsNull);
        }

        /// <summary>
        /// 创建判断是否非空的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition IsNotNull(String columnName)
        {
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.IsNotNull);
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
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.Equal, value);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotEqual(String columnName, Object value)
        {
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.NotEqual, value);
        }

        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition Equal(String columnName, DataType dataType, Object value)
        {
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.Equal, dataType, value);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotEqual(String columnName, DataType dataType, Object value)
        {
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.NotEqual, dataType, value);
        }

        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition EqualColumn(String columnName, String columnNameTwo)
        {
            return SqlBasicParameterCondition.InternalCreateColumn(this._baseCommand, columnName, SqlOperator.Equal, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotEqualColumn(String columnName, String columnNameTwo)
        {
            return SqlBasicParameterCondition.InternalCreateColumn(this._baseCommand, columnName, SqlOperator.NotEqual, columnNameTwo);
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
            return SqlBasicParameterCondition.InternalCreateColumn(this._baseCommand, columnName, SqlOperator.Equal, tableNameTwo, columnNameTwo);
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
            return SqlBasicParameterCondition.InternalCreateColumn(this._baseCommand, columnName, SqlOperator.NotEqual, tableNameTwo, columnNameTwo);
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
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.GreaterThan, value);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LessThan(String columnName, Object value)
        {
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.LessThan, value);
        }

        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition GreaterThan(String columnName, DataType dataType, Object value)
        {
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.GreaterThan, dataType, value);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LessThan(String columnName, DataType dataType, Object value)
        {
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.LessThan, dataType, value);
        }

        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition GreaterThanColumn(String columnName, String columnNameTwo)
        {
            return SqlBasicParameterCondition.InternalCreateColumn(this._baseCommand, columnName, SqlOperator.GreaterThan, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LessThanColumn(String columnName, String columnNameTwo)
        {
            return SqlBasicParameterCondition.InternalCreateColumn(this._baseCommand, columnName, SqlOperator.LessThan, columnNameTwo);
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
            return SqlBasicParameterCondition.InternalCreateColumn(this._baseCommand, columnName, SqlOperator.GreaterThan, tableNameTwo, columnNameTwo);
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
            return SqlBasicParameterCondition.InternalCreateColumn(this._baseCommand, columnName, SqlOperator.LessThan, tableNameTwo, columnNameTwo);
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
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.GreaterThanOrEqual, value);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LessThanOrEqual(String columnName, Object value)
        {
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.LessThanOrEqual, value);
        }

        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition GreaterThanOrEqual(String columnName, DataType dataType, Object value)
        {
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.GreaterThanOrEqual, dataType, value);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LessThanOrEqual(String columnName, DataType dataType, Object value)
        {
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.LessThanOrEqual, dataType, value);
        }

        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition GreaterThanOrEqualColumn(String columnName, String columnNameTwo)
        {
            return SqlBasicParameterCondition.InternalCreateColumn(this._baseCommand, columnName, SqlOperator.GreaterThanOrEqual, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LessThanOrEqualColumn(String columnName, String columnNameTwo)
        {
            return SqlBasicParameterCondition.InternalCreateColumn(this._baseCommand, columnName, SqlOperator.LessThanOrEqual, columnNameTwo);
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
            return SqlBasicParameterCondition.InternalCreateColumn(this._baseCommand, columnName, SqlOperator.GreaterThanOrEqual, tableNameTwo, columnNameTwo);
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
            return SqlBasicParameterCondition.InternalCreateColumn(this._baseCommand, columnName, SqlOperator.LessThanOrEqual, tableNameTwo, columnNameTwo);
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
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.Like, value);
        }

        /// <summary>
        /// 创建判断是否不相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotLike(String columnName, String value)
        {
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.NotLike, value);
        }

        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition Like(String columnName, DataType dataType, Object value)
        {
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.Like, dataType, value);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotLike(String columnName, DataType dataType, Object value)
        {
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.NotLike, dataType, value);
        }

        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LikeColumn(String columnName, String columnNameTwo)
        {
            return SqlBasicParameterCondition.InternalCreateColumn(this._baseCommand, columnName, SqlOperator.Like, columnNameTwo);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="columnNameTwo">字段名二</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotLikeColumn(String columnName, String columnNameTwo)
        {
            return SqlBasicParameterCondition.InternalCreateColumn(this._baseCommand, columnName, SqlOperator.NotLike, columnNameTwo);
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
            return SqlBasicParameterCondition.InternalCreateColumn(this._baseCommand, columnName, SqlOperator.Like, tableNameTwo, columnNameTwo);
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
            return SqlBasicParameterCondition.InternalCreateColumn(this._baseCommand, columnName, SqlOperator.NotLike, tableNameTwo, columnNameTwo);
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
            return this.Like(columnName, "%" + value + "%");
        }

        /// <summary>
        /// 创建判断是否不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotLikeAll(String columnName, String value)
        {
            return this.NotLike(columnName, "%" + value + "%");
        }

        /// <summary>
        /// 创建判断是否包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LikeAll(String columnName, DataType dataType, Object value)
        {
            return this.Like(columnName, dataType, "%" + value + "%");
        }

        /// <summary>
        /// 创建判断是否不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotLikeAll(String columnName, DataType dataType, Object value)
        {
            return this.NotLike(columnName, dataType, "%" + value + "%");
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
            return this.Like(columnName, value + "%");
        }

        /// <summary>
        /// 创建判断是否开头不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotLikeStartWith(String columnName, String value)
        {
            return this.NotLike(columnName, value + "%");
        }

        /// <summary>
        /// 创建判断是否开头包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LikeStartWith(String columnName, DataType dataType, Object value)
        {
            return this.Like(columnName, dataType, value + "%");
        }

        /// <summary>
        /// 创建判断是否开头不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotLikeStartWith(String columnName, DataType dataType, Object value)
        {
            return this.NotLike(columnName, dataType, value + "%");
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
            return this.Like(columnName, "%" + value);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotLikeEndWith(String columnName, String value)
        {
            return this.NotLike(columnName, "%" + value);
        }

        /// <summary>
        /// 创建判断是否结尾包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition LikeEndWith(String columnName, DataType dataType, Object value)
        {
            return this.Like(columnName, dataType, "%" + value);
        }

        /// <summary>
        /// 创建判断是否结尾不包含的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="value">数据</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotLikeEndWith(String columnName, DataType dataType, Object value)
        {
            return this.NotLike(columnName, dataType, "%" + value);
        }
        #endregion
        #endregion

        #region Between/NotBetween
        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition Between(String columnName, Object valueOne, Object valueTwo)
        {
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.Between, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否不在范围内的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotBetween(String columnName, Object valueOne, Object valueTwo)
        {
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.NotBetween, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition Between(String columnName, DataType dataType, Object valueOne, Object valueTwo)
        {
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.Between, dataType, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否不在范围内的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotBetween(String columnName, DataType dataType, Object valueOne, Object valueTwo)
        {
            return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.NotBetween, dataType, valueOne, valueTwo);
        }
        #endregion

        #region BetweenNullable/NotBetweenNullable
        #region General
        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="isNot">是否不在范围内</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        private SqlBasicParameterCondition BetweenNullable(String columnName, Boolean isNot, Object valueOne, Object valueTwo)
        {
            if (valueOne != null && valueTwo != null)
            {
                return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, (isNot ? SqlOperator.NotBetween : SqlOperator.Between), valueOne, valueTwo);
            }
            else if (valueOne != null && valueTwo == null)
            {
                return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, (isNot ? SqlOperator.LessThan : SqlOperator.GreaterThanOrEqual), valueOne);
            }
            else if (valueOne == null && valueTwo != null)
            {
                return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, (isNot ? SqlOperator.GreaterThan : SqlOperator.LessThanOrEqual), valueTwo);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="isNot">是否不在范围内</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        private SqlBasicParameterCondition BetweenNullable(String columnName, Boolean isNot, DataType dataType, Object valueOne, Object valueTwo)
        {
            if (valueOne != null && valueTwo != null)
            {
                return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, (isNot ? SqlOperator.NotBetween : SqlOperator.Between), dataType, valueOne, valueTwo);
            }
            else if (valueOne != null && valueTwo == null)
            {
                return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, (isNot ? SqlOperator.LessThan : SqlOperator.GreaterThanOrEqual), dataType, valueOne);
            }
            else if (valueOne == null && valueTwo != null)
            {
                return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, (isNot ? SqlOperator.GreaterThan : SqlOperator.LessThanOrEqual), dataType, valueTwo);
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
        /// <param name="columnName">字段名</param>
        /// <param name="isNot">是否不在范围内</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        private SqlBasicParameterCondition BetweenNullable<T>(String columnName, Boolean isNot, T? valueOne, T? valueTwo) where T : struct
        {
            if (valueOne.HasValue && valueTwo.HasValue)
            {
                return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, (isNot ? SqlOperator.NotBetween : SqlOperator.Between), valueOne, valueTwo);
            }
            else if (valueOne.HasValue && !valueTwo.HasValue)
            {
                return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, (isNot ? SqlOperator.LessThan : SqlOperator.GreaterThanOrEqual), valueOne);
            }
            else if (!valueOne.HasValue && valueTwo.HasValue)
            {
                return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, (isNot ? SqlOperator.GreaterThan : SqlOperator.LessThanOrEqual), valueTwo);
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
        /// <param name="columnName">字段名</param>
        /// <param name="isNot">是否不在范围内</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        private SqlBasicParameterCondition BetweenNullable<T>(String columnName, Boolean isNot, DataType dataType, T? valueOne, T? valueTwo) where T : struct
        {
            if (valueOne.HasValue && valueTwo.HasValue)
            {
                return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, (isNot ? SqlOperator.NotBetween : SqlOperator.Between), dataType, valueOne, valueTwo);
            }
            else if (valueOne.HasValue && !valueTwo.HasValue)
            {
                return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, (isNot ? SqlOperator.LessThan : SqlOperator.GreaterThanOrEqual), dataType, valueOne);
            }
            else if (!valueOne.HasValue && valueTwo.HasValue)
            {
                return SqlBasicParameterCondition.InternalCreate(this._baseCommand, columnName, (isNot ? SqlOperator.GreaterThan : SqlOperator.LessThanOrEqual), dataType, valueTwo);
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
        /// <param name="columnName">字段名</param>
        /// <param name="valueOne">可空开始值</param>
        /// <param name="valueTwo">可空结束值</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition BetweenNullable(String columnName, Object valueOne, Object valueTwo)
        {
            return this.BetweenNullable(columnName, false, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="valueOne">可空开始值</param>
        /// <param name="valueTwo">可空结束值</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition BetweenNullable(String columnName, DataType dataType, Object valueOne, Object valueTwo)
        {
            return this.BetweenNullable(columnName, false, dataType, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition BetweenNullable<T>(String columnName, T? valueOne, T? valueTwo) where T : struct
        {
            return this.BetweenNullable<T>(columnName, false, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否在范围内的Sql条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition BetweenNullable<T>(String columnName, DataType dataType, T? valueOne, T? valueTwo) where T : struct
        {
            return this.BetweenNullable<T>(columnName, false, dataType, valueOne, valueTwo);
        }
        #endregion

        #region NotBetweenNullable
        /// <summary>
        /// 创建判断是否不在范围内的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="valueOne">可空开始值</param>
        /// <param name="valueTwo">可空结束值</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotBetweenNullable(String columnName, Object valueOne, Object valueTwo)
        {
            return this.BetweenNullable(columnName, true, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否不在范围内的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="valueOne">可空开始值</param>
        /// <param name="valueTwo">可空结束值</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotBetweenNullable(String columnName, DataType dataType, Object valueOne, Object valueTwo)
        {
            return this.BetweenNullable(columnName, true, dataType, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否不在范围内的Sql条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotBetweenNullable<T>(String columnName, T? valueOne, T? valueTwo) where T : struct
        {
            return this.BetweenNullable<T>(columnName, true, valueOne, valueTwo);
        }

        /// <summary>
        /// 创建判断是否不在范围内的Sql条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="valueOne">开始值</param>
        /// <param name="valueTwo">结束值</param>
        /// <returns>Sql条件语句</returns>
        public SqlBasicParameterCondition NotBetweenNullable<T>(String columnName, DataType dataType, T? valueOne, T? valueTwo) where T : struct
        {
            return this.BetweenNullable<T>(columnName, true, dataType, valueOne, valueTwo);
        }
        #endregion
        #endregion
        #endregion

        #region 基本语句条件
        #region Equal/NotEqual
        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition Equal(String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlBasicCommandCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.Equal, tableName, action);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition NotEqual(String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlBasicCommandCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.NotEqual, tableName, action);
        }

        /// <summary>
        /// 创建判断是否相等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition Equal(String columnName, Action<SelectCommand> action)
        {
            return SqlBasicCommandCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.Equal, action);
        }

        /// <summary>
        /// 创建判断是否不等的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition NotEqual(String columnName, Action<SelectCommand> action)
        {
            return SqlBasicCommandCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.NotEqual, action);
        }
        #endregion

        #region GreaterThan/LessThan
        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition GreaterThan(String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlBasicCommandCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.GreaterThan, tableName, action);
        }

        /// <summary>
        /// 创建判断是否大于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition GreaterThan(String columnName, Action<SelectCommand> action)
        {
            return SqlBasicCommandCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.GreaterThan, action);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition LessThan(String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlBasicCommandCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.LessThan, tableName, action);
        }

        /// <summary>
        /// 创建判断是否小于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition LessThan(String columnName, Action<SelectCommand> action)
        {
            return SqlBasicCommandCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.LessThan, action);
        }
        #endregion

        #region GreaterThanOrEqual/LessThanOrEqual
        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition GreaterThanOrEqual(String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlBasicCommandCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.GreaterThanOrEqual, tableName, action);
        }

        /// <summary>
        /// 创建判断是否大于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition GreaterThanOrEqual(String columnName, Action<SelectCommand> action)
        {
            return SqlBasicCommandCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.GreaterThanOrEqual, action);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition LessThanOrEqual(String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlBasicCommandCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.LessThanOrEqual, tableName, action);
        }

        /// <summary>
        /// 创建判断是否小于等于的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition LessThanOrEqual(String columnName, Action<SelectCommand> action)
        {
            return SqlBasicCommandCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.LessThanOrEqual, action);
        }
        #endregion

        #region Like/NotLike
        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition Like(String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlBasicCommandCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.Like, tableName, action);
        }

        /// <summary>
        /// 创建判断是否相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition Like(String columnName, Action<SelectCommand> action)
        {
            return SqlBasicCommandCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.Like, action);
        }

        /// <summary>
        /// 创建判断是否不相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition NotLike(String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlBasicCommandCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.NotLike, tableName, action);
        }

        /// <summary>
        /// 创建判断是否不相似的Sql条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlBasicCommandCondition NotLike(String columnName, Action<SelectCommand> action)
        {
            return SqlBasicCommandCondition.InternalCreate(this._baseCommand, columnName, SqlOperator.NotLike, action);
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
        public SqlInsideParametersCondition In(params DataParameter[] parameters)
        {
            return SqlInsideParametersCondition.InternalCreate(this._baseCommand, false, parameters);
        }

        /// <summary>
        /// 创建新的Sql IN参数条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition In(String columnName, DataType dataType, IEnumerable<Object> values)
        {
            return SqlInsideParametersCondition.InternalCreate(this._baseCommand, columnName, false, dataType, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition In(String columnName, IEnumerable<Object> values)
        {
            return SqlInsideParametersCondition.InternalCreate(this._baseCommand, columnName, false, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition In(String columnName, DataType dataType, Array values)
        {
            return SqlInsideParametersCondition.InternalCreate(this._baseCommand, columnName, false, dataType, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition In(String columnName, Array values)
        {
            return SqlInsideParametersCondition.InternalCreate(this._baseCommand, columnName, false, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition In<T>(String columnName, DataType dataType, IEnumerable<T> values)
        {
            return SqlInsideParametersCondition.InternalCreate<T>(this._baseCommand, columnName, false, dataType, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition In<T>(String columnName, IEnumerable<T> values)
        {
            return SqlInsideParametersCondition.InternalCreate<T>(this._baseCommand, columnName, false, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="func">操作方法</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition In<T>(String columnName, DataType dataType, Func<IEnumerable<T>> func)
        {
            return SqlInsideParametersCondition.InternalCreate<T>(this._baseCommand, columnName, false, dataType, func());
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="func">操作方法</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition In<T>(String columnName, Func<IEnumerable<T>> func)
        {
            return SqlInsideParametersCondition.InternalCreate<T>(this._baseCommand, columnName, false, func());
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition In<T>(String columnName, DataType dataType, String values, Char separator) where T : IConvertible
        {
            return SqlInsideParametersCondition.InternalCreate<T>(this._baseCommand, columnName, false, dataType, values, separator);
        }
        #endregion

        #region NotIn
        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="parameters">参数集合</param>
        /// <exception cref="ArgumentNullException">参数集合不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotIn(params DataParameter[] parameters)
        {
            return SqlInsideParametersCondition.InternalCreate(this._baseCommand, true, parameters);
        }

        /// <summary>
        /// 创建新的Sql NOT IN参数条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotIn(String columnName, DataType dataType, IEnumerable<Object> values)
        {
            return SqlInsideParametersCondition.InternalCreate(this._baseCommand, columnName, true, dataType, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotIn(String columnName, IEnumerable<Object> values)
        {
            return SqlInsideParametersCondition.InternalCreate(this._baseCommand, columnName, true, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotIn(String columnName, DataType dataType, Array values)
        {
            return SqlInsideParametersCondition.InternalCreate(this._baseCommand, columnName, true, dataType, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotIn(String columnName, Array values)
        {
            return SqlInsideParametersCondition.InternalCreate(this._baseCommand, columnName, true, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotIn<T>(String columnName, DataType dataType, IEnumerable<T> values)
        {
            return SqlInsideParametersCondition.InternalCreate<T>(this._baseCommand, columnName, true, dataType, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotIn<T>(String columnName, IEnumerable<T> values)
        {
            return SqlInsideParametersCondition.InternalCreate<T>(this._baseCommand, columnName, true, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="func">操作方法</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotIn<T>(String columnName, DataType dataType, Func<IEnumerable<T>> func)
        {
            return SqlInsideParametersCondition.InternalCreate<T>(this._baseCommand, columnName, true, dataType, func());
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="func">操作方法</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotIn<T>(String columnName, Func<IEnumerable<T>> func)
        {
            return SqlInsideParametersCondition.InternalCreate<T>(this._baseCommand, columnName, true, func());
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotIn<T>(String columnName, DataType dataType, String values, Char separator) where T : IConvertible
        {
            return SqlInsideParametersCondition.InternalCreate<T>(this._baseCommand, columnName, true, dataType, values, separator);
        }
        #endregion

        #region InThese/NotInThese
        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition InThese<T>(String columnName, DataType dataType, params T[] values)
        {
            return SqlInsideParametersCondition.InternalCreate<T>(this._baseCommand, columnName, false, dataType, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotInThese<T>(String columnName, DataType dataType, params T[] values)
        {
            return SqlInsideParametersCondition.InternalCreate<T>(this._baseCommand, columnName, true, dataType, values);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition InThese<T>(String columnName, params T[] values)
        {
            return SqlInsideParametersCondition.InternalCreate<T>(this._baseCommand, columnName, false, values);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="columnName">字段名</param>
        /// <param name="values">数据集合</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotInThese<T>(String columnName, params T[] values)
        {
            return SqlInsideParametersCondition.InternalCreate<T>(this._baseCommand, columnName, true, values);
        }
        #endregion

        #region In/NotIn 专用类型
        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition InString(String columnName, String values, Char separator)
        {
            return SqlInsideParametersCondition.InternalCreateWithStringList(this._baseCommand, columnName, false, values, separator);
        }

        /// <summary>
        /// 创建新的Sql NOT IN条件语句
        /// </summary>
        /// <param name="columnName">字段名</param>
        /// <param name="values">分隔符号分隔的数据集合</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>Sql条件语句</returns>
        public SqlInsideParametersCondition NotInString(String columnName, String values, Char separator)
        {
            return SqlInsideParametersCondition.InternalCreateWithStringList(this._baseCommand, columnName, true, values, separator);
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
            return SqlInsideParametersCondition.InternalCreateWithInt32List(this._baseCommand, columnName, false, values, separator);
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
            return SqlInsideParametersCondition.InternalCreateWithInt32List(this._baseCommand, columnName, true, values, separator);
        }
        #endregion
        #endregion

        #region In语句条件
        #region In
        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlInsideCommandCondition In(String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlInsideCommandCondition.InternalCreate(this._baseCommand, columnName, false, tableName, action);
        }

        /// <summary>
        /// 创建新的Sql IN条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlInsideCommandCondition In(String columnName, Action<SelectCommand> action)
        {
            return SqlInsideCommandCondition.InternalCreate(this._baseCommand, columnName, false, action);
        }
        #endregion

        #region NotIn
        /// <summary>
        /// 创建新的Sql Not IN条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="tableName">查询的表名</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlInsideCommandCondition NotIn(String columnName, String tableName, Action<SelectCommand> action)
        {
            return SqlInsideCommandCondition.InternalCreate(this._baseCommand, columnName, true, tableName, action);
        }

        /// <summary>
        /// 创建新的Sql Not IN条件语句
        /// </summary>
        /// <param name="columnName">字段名称</param>
        /// <param name="action">设置选择语句的方法</param>
        /// <exception cref="ArgumentNullException">设置语句的方法不能为空</exception>
        /// <returns>Sql条件语句</returns>
        public SqlInsideCommandCondition NotIn(String columnName, Action<SelectCommand> action)
        {
            return SqlInsideCommandCondition.InternalCreate(this._baseCommand, columnName, true, action);
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
            return SqlConditionList.InternalCreate(this._baseCommand, SqlWhereConcatType.And, conditions);
        }

        /// <summary>
        /// 创建与连接的Sql条件语句集合
        /// </summary>
        /// <param name="conditions">条件语句集合</param>
        /// <returns>Sql条件语句集合</returns>
        public SqlConditionList And(IEnumerable<ISqlCondition> conditions)
        {
            return SqlConditionList.InternalCreate(this._baseCommand, SqlWhereConcatType.And, conditions);
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
            return SqlConditionList.InternalCreate(this._baseCommand, SqlWhereConcatType.Or, conditions);
        }

        /// <summary>
        /// 创建或连接的Sql条件语句集合
        /// </summary>
        /// <param name="conditions">条件语句集合</param>
        /// <returns>Sql条件语句集合</returns>
        public SqlConditionList Or(IEnumerable<ISqlCondition> conditions)
        {
            return SqlConditionList.InternalCreate(this._baseCommand, SqlWhereConcatType.Or, conditions);
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
            return SqlNotCondition.Create(this._baseCommand, condition);
        }
        #endregion
        #endregion
    }
}