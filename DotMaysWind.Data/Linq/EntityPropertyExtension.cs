using System;

using DotMaysWind.Data.Command;

namespace DotMaysWind.Data.Linq
{
    /// <summary>
    /// 实体属性扩展类
    /// </summary>
    public static class EntityPropertyExtension
    {
        #region IsNull
        /// <summary>
        /// 判断属性所指字段内容是否为空
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="key">实体属性</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>内容是否为空</returns>
        public static Boolean IsNull<T>(this T key) where T : struct
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断属性所指字段内容是否为空
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="key">实体属性</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>内容是否为空</returns>
        public static Boolean IsNull<T>(this T? key) where T : struct
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断属性所指字段内容是否为空
        /// </summary>
        /// <param name="key">实体属性</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>内容是否为空</returns>
        public static Boolean IsNull(this String key)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IsNotNull
        /// <summary>
        /// 判断属性所指字段内容是否不为空
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="key">实体属性</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>内容是否不为空</returns>
        public static Boolean IsNotNull<T>(this T key) where T : struct
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Boolean IsNotNull<T>(this T? key) where T : struct
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Boolean IsNotNull(this String key)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Like
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
        #endregion

        #region NotLike
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

        #region Between
        /// <summary>
        /// 判断属性所指字段是否在指定范围内
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="key">实体属性</param>
        /// <param name="start">开始值</param>
        /// <param name="end">结束值</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否在指定范围内</returns>
        public static Boolean Between<T>(this T key, T start, T end) where T : struct
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断属性所指字段是否在指定范围内
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="key">实体属性</param>
        /// <param name="start">开始值</param>
        /// <param name="end">结束值</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否在指定范围内</returns>
        public static Boolean Between<T>(this T? key, T start, T end) where T : struct
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断属性所指字段是否在指定范围内
        /// </summary>
        /// <param name="key">实体属性</param>
        /// <param name="start">开始值</param>
        /// <param name="end">结束值</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否在指定范围内</returns>
        public static Boolean Between(this String key, String start, String end)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Between
        /// <summary>
        /// 判断属性所指字段是否不在指定范围内
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="key">实体属性</param>
        /// <param name="start">开始值</param>
        /// <param name="end">结束值</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否不在指定范围内</returns>
        public static Boolean NotBetween<T>(this T key, T start, T end) where T : struct
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断属性所指字段是否不在指定范围内
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="key">实体属性</param>
        /// <param name="start">开始值</param>
        /// <param name="end">结束值</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否不在指定范围内</returns>
        public static Boolean NotBetween<T>(this T? key, T start, T end) where T : struct
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
        public static Boolean NotBetween(this String key, String start, String end)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region In
        /// <summary>
        /// 判断属性所指字段是否包含指定内容
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="key">实体属性</param>
        /// <param name="values">指定内容集合</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否包含指定内容</returns>
        public static Boolean In<T>(this T key, params T[] values) where T : struct
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断属性所指字段是否包含指定内容
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="key">实体属性</param>
        /// <param name="values">指定内容集合</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否包含指定内容</returns>
        public static Boolean In<T>(this T? key, params T[] values) where T : struct
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断属性所指字段是否包含指定内容
        /// </summary>
        /// <param name="key">实体属性</param>
        /// <param name="values">指定内容集合</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否包含指定内容</returns>
        public static Boolean In(this String key, params String[] values)
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
        public static Boolean In<T>(this T key, SelectCommand command) where T : struct
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
        public static Boolean In<T>(this T? key, SelectCommand command) where T : struct
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
        public static Boolean In(this String key, SelectCommand command)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region NotIn
        /// <summary>
        /// 判断属性所指字段是否不包含指定内容
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="key">实体属性</param>
        /// <param name="values">指定内容集合</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否不包含指定内容</returns>
        public static Boolean NotIn<T>(this T key, params T[] values) where T : struct
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 判断属性所指字段是否不包含指定内容
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="key">实体属性</param>
        /// <param name="values">指定内容集合</param>
        /// <exception cref="NotImplementedException">该方法只用于Linq表达式，没有具体实现</exception>
        /// <returns>是否不包含指定内容</returns>
        public static Boolean NotIn<T>(this T? key, params T[] values) where T : struct
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
        public static Boolean NotIn(this String key, params String[] values)
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
        public static Boolean NotIn<T>(this T key, SelectCommand command) where T : struct
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
        public static Boolean NotIn<T>(this T? key, SelectCommand command) where T : struct
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
        public static Boolean NotIn(this String key, SelectCommand command)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}