using System;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Orm.Helper;

namespace DotMaysWind.Data.Orm
{
    /// <summary>
    /// 插入语句扩展方法类
    /// </summary>
    public static class InsertCommandExtension
    {
        #region 扩展方法
        /// <summary>
        /// 插入指定实体所有参数并返回当前语句
        /// </summary>
        /// <param name="cmd">插入语句</param>
        /// <param name="entity">实体</param>
        /// <returns>当前语句</returns>
        public static InsertCommand Add(this InsertCommand cmd, Object entity)
        {
            SqlParameter[] parameters = EntityHelper.InternalGetSqlParameters(entity, Constants.InsertNewParameterNamePrefix);
            cmd.Add(parameters);

            return cmd;
        }
        #endregion
    }
}