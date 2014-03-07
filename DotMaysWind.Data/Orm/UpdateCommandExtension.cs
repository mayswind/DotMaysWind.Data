using System;

using DotMaysWind.Data.Command;
using DotMaysWind.Data.Orm.Helper;

namespace DotMaysWind.Data.Orm
{
    /// <summary>
    /// 更新语句扩展方法类
    /// </summary>
    public static class UpdateCommandExtension
    {
        #region 扩展方法
        /// <summary>
        /// 插入更新实体所有参数并返回当前语句
        /// </summary>
        /// <param name="cmd">插入语句</param>
        /// <param name="entity">实体</param>
        /// <returns>当前语句</returns>
        public static UpdateCommand Set(this UpdateCommand cmd, Object entity)
        {
            SqlParameter[] parameters = EntityHelper.InternalGetSqlParameters(entity, Constants.UpdateOldParameterNamePrefix);
            cmd.Set(parameters);

            return cmd;
        }
        #endregion
    }
}