using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

using DotMaysWind.Data.Helper;

namespace DotMaysWind.Data.Orm.Helper
{
    /// <summary>
    /// 实体类辅助类
    /// </summary>
    internal static class EntityHelper
    {
        /// <summary>
        /// 获取实体类映射表格名称
        /// </summary>
        /// <returns>表格名称</returns>
        internal static String InternalGetTableName(Type entityType)
        {
            Object[] objs = entityType.GetCustomAttributes(typeof(DatabaseTableAtrribute), true);

            foreach (Object obj in objs)
            {
                DatabaseTableAtrribute attr = obj as DatabaseTableAtrribute;

                if (attr != null)
                {
                    return attr.TableName;
                }
            }

            return String.Empty;
        }

        /// <summary>
        /// 获取实体列特性集合
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <returns>实体列特性集合</returns>
        internal static Dictionary<String, DatabaseColumnAtrribute> InternalGetTableColumns(Type entityType)
        {
            Dictionary<String, DatabaseColumnAtrribute> dict = new Dictionary<String, DatabaseColumnAtrribute>();
            PropertyInfo[] props = entityType.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                Object[] objs = prop.GetCustomAttributes(typeof(DatabaseColumnAtrribute), true);

                foreach (Object obj in objs)
                {
                    DatabaseColumnAtrribute attr = obj as DatabaseColumnAtrribute;

                    if (attr != null)
                    {
                        dict[prop.Name] = attr;
                        break;
                    }
                }
            }

            return dict;
        }

        /// <summary>
        /// 获取指定实体类的Sql参数集合
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="paramPrefix">参数名前缀</param>
        /// <returns>Sql参数集合</returns>
        internal static SqlParameter[] InternalGetSqlParameters(Object entity, String paramPrefix)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (entity == null)
            {
                return parameters.ToArray();
            }

            Type entityType = entity.GetType();
            PropertyInfo[] props = entityType.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                DatabaseColumnAtrribute attr = null;
                SqlParameter parameter = null;

                Object[] objs = prop.GetCustomAttributes(typeof(DatabaseColumnAtrribute), true);

                foreach (Object obj in objs)
                {
                    if ((attr = obj as DatabaseColumnAtrribute) != null)
                    {
                        break;
                    }
                }

                if (attr.DbType.HasValue)
                {
                    parameter = SqlParameter.Create(attr.ColumnName, paramPrefix + attr.ColumnName, attr.DbType.Value, prop.GetValue(entity, null));
                }
                else
                {
                    parameter = SqlParameter.Create(attr.ColumnName, paramPrefix + attr.ColumnName, prop.GetValue(entity, null));
                }

                parameters.Add(parameter);
            }

            return parameters.ToArray();
        }

        /// <summary>
        /// 获取指定类型是否为可空类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否为可空类型</returns>
        internal static Boolean InternalIsNullableType(Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            return (type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}