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
        /// 更新指定实体所有参数并返回当前语句
        /// </summary>
        /// <param name="cmd">更新语句</param>
        /// <param name="entity">实体</param>
        /// <returns>当前语句</returns>
        /// <example>
        /// <code lang="C#">
        /// <![CDATA[
        /// public class UserDataProvider : DatabaseTable<User>
        /// {
        ///     //other necessary code
        ///     
        ///     public Boolean UpdateEntity(User user)
        ///     {
        ///         return this.Update()
        ///             .Set(user)
        ///             .Where<User>(c => c.UserID == user.UserID)
        ///             .Result() > 0;
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static UpdateCommand Set(this UpdateCommand cmd, Object entity)
        {
            DataParameter[] parameters = EntityHelper.InternalGetSqlParameters(cmd, entity);

            if (parameters != null)
            {
                cmd.Set(parameters);
            }

            return cmd;
        }
        #endregion
    }
}