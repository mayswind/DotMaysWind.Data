using System;

using DotMaysWind.Data.Command;

namespace DotMaysWind.Data.Linq
{
    /// <summary>
    /// 实体属性扩展类
    /// </summary>
    public static class EntityPropertyExtension
    {
        #region IsNull/IsNotNull
        /// <summary>
        /// 判断属性所指字段内容是否为空
        /// </summary>
        /// <param name="key">实体属性</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>内容是否为空</returns>
        public static Boolean IsNull(this Object key)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// 判断属性所指字段内容是否不为空
        /// </summary>
        /// <param name="key">实体属性</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>内容是否为空</returns>
        public static Boolean IsNotNull(this Object key)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Like/NotLike
        /// <summary>
        /// 判断属性所指字段是否与指定内容相似
        /// </summary>
        /// <param name="key">实体属性</param>
        /// <param name="value">指定内容</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否与指定内容相似</returns>
        public static Boolean Like(this String key, String value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断属性所指字段是否包含指定内容
        /// </summary>
        /// <param name="key">实体属性</param>
        /// <param name="value">指定内容</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否包含指定内容</returns>
        public static Boolean LikeAll(this String key, String value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断属性所指字段是否开头包含指定内容
        /// </summary>
        /// <param name="key">实体属性</param>
        /// <param name="value">指定内容</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否开头包含指定内容</returns>
        public static Boolean LikeStartWith(this String key, String value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断属性所指字段是否结尾包含指定内容
        /// </summary>
        /// <param name="key">实体属性</param>
        /// <param name="value">指定内容</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否包含指定内容</returns>
        public static Boolean LikeEndWith(this String key, String value)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// 判断属性所指字段是否与指定内容不相似
        /// </summary>
        /// <param name="key">实体属性</param>
        /// <param name="value">指定内容</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否与指定内容不相似</returns>
        public static Boolean NotLike(this String key, String value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断属性所指字段是否不包含指定内容
        /// </summary>
        /// <param name="key">实体属性</param>
        /// <param name="value">指定内容</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否不包含指定内容</returns>
        public static Boolean NotLikeAll(this String key, String value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断属性所指字段是否开头不包含指定内容
        /// </summary>
        /// <param name="key">实体属性</param>
        /// <param name="value">指定内容</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否开头不包含指定内容</returns>
        public static Boolean NotLikeStartWith(this String key, String value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断属性所指字段是否结尾不包含指定内容
        /// </summary>
        /// <param name="key">实体属性</param>
        /// <param name="value">指定内容</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否不包含指定内容</returns>
        public static Boolean NotLikeEndWith(this String key, String value)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Between/NotBetween
        /// <summary>
        /// 判断属性所指字段是否在指定范围内
        /// </summary>
        /// <param name="key">实体属性</param>
        /// <param name="start">开始值</param>
        /// <param name="end">结束值</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否在指定范围内</returns>
        public static Boolean Between(this Object key, Object start, Object end)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// 判断属性所指字段是否不在指定范围内
        /// </summary>
        /// <param name="key">实体属性</param>
        /// <param name="start">开始值</param>
        /// <param name="end">结束值</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否不在指定范围内</returns>
        public static Boolean NotBetween(this Object key, Object start, Object end)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region In/NotIn
        /// <summary>
        /// 判断属性所指字段是否包含指定内容
        /// </summary>
        /// <param name="key">实体属性</param>
        /// <param name="values">指定内容集合</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否包含指定内容</returns>
        public static Boolean InThese(this Object key, params Object[] values)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断属性所指字段是否包含指定选择语句查询结果
        /// </summary>
        /// <param name="key">实体属性</param>
        /// <param name="command">选择语句</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否不包含指定选择语句查询结果</returns>
        public static Boolean In(this Object key, SelectCommand command)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断属性所指字段是否不包含指定内容
        /// </summary>
        /// <param name="key">实体属性</param>
        /// <param name="values">指定内容集合</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否不包含指定内容</returns>
        public static Boolean NotInThese(this Object key, params Object[] values)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断属性所指字段是否不包含指定选择语句查询结果
        /// </summary>
        /// <param name="key">实体属性</param>
        /// <param name="command">选择语句</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否不包含指定选择语句查询结果</returns>
        public static Boolean NotIn(this Object key, SelectCommand command)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}