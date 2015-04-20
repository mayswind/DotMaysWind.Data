using System;
using System.Data;

namespace DotMaysWind.Data.Orm
{
    /// <summary>
    /// 实体创建参数
    /// </summary>
    public class EntityCreatingArgs
    {
        #region 字段
        private Int32 _index;
        private DataRow _row;
        private DataColumnCollection _columns;
        private Object _extraArg;
        #endregion

        #region 属性
        /// <summary>
        /// 获取当前索引
        /// </summary>
        public Int32 Index
        {
            get { return this._index; }
        }

        /// <summary>
        /// 获取当前数据行
        /// </summary>
        public DataRow Row
        {
            get { return this._row; }
        }

        /// <summary>
        /// 获取数据列集合
        /// </summary>
        public DataColumnCollection Columns
        {
            get { return this._columns; }
        }

        /// <summary>
        /// 获取额外的参数
        /// </summary>
        public Object ExtraArgument
        {
            get { return this._extraArg; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的实体创建参数
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="row">当前数据行</param>
        /// <param name="columns">数据列集合</param>
        /// <param name="extraArg">额外参数</param>
        internal EntityCreatingArgs(Int32 index, DataRow row, DataColumnCollection columns, Object extraArg)
        {
            this._index = index;
            this._row = row;
            this._columns = columns;
            this._extraArg = extraArg;
        }
        #endregion
    }
}