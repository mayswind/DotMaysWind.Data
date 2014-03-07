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
        /// <param name="entityType">实体类型</param>
        /// <returns>表格名称</returns>
        internal static String InternalGetTableName(Type entityType)
        {
            Object[] objs = entityType.GetCustomAttributes(typeof(DatabaseTableAttribute), true);

            foreach (Object obj in objs)
            {
                DatabaseTableAttribute attr = obj as DatabaseTableAttribute;

                if (attr != null)
                {
                    return attr.TableName;
                }
            }

            return String.Empty;
        }

        /// <summary>
        /// 获取实体类映射字段特性
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="propertyName">实体属性名称</param>
        /// <returns>字段特性</returns>
        internal static DatabaseColumnAttribute InternalGetColumnAttribute(Type entityType, String propertyName)
        {
            PropertyInfo[] props = entityType.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                if (!String.Equals(prop.Name, propertyName))
                {
                    continue;
                }

                DatabaseColumnAttribute attr = null;
                Object[] objs = prop.GetCustomAttributes(typeof(DatabaseColumnAttribute), true);

                foreach (Object obj in objs)
                {
                    if ((attr = obj as DatabaseColumnAttribute) != null)
                    {
                        return attr;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 获取实体列特性集合
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <returns>实体列特性集合</returns>
        internal static Dictionary<String, DatabaseColumnAttribute> InternalGetTableColumns(Type entityType)
        {
            Dictionary<String, DatabaseColumnAttribute> dict = new Dictionary<String, DatabaseColumnAttribute>();
            PropertyInfo[] props = entityType.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                Object[] objs = prop.GetCustomAttributes(typeof(DatabaseColumnAttribute), true);

                foreach (Object obj in objs)
                {
                    DatabaseColumnAttribute attr = obj as DatabaseColumnAttribute;

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
        /// 获取指定Sql参数
        /// </summary>
        /// <param name="attr">实体属性特性</param>
        /// <param name="value">属性值</param>
        /// <returns>Sql参数集合</returns>
        internal static SqlParameter InternalGetSqlParameter(DatabaseColumnAttribute attr, Object value)
        {
            SqlParameter parameter = null;

            if (attr.DbType.HasValue)
            {
                parameter = SqlParameter.Create(attr.ColumnName, attr.DbType.Value, value);
            }
            else
            {
                parameter = SqlParameter.Create(attr.ColumnName, value);
            }

            return parameter;
        }

        /// <summary>
        /// 获取指定实体类的Sql参数集合
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns>Sql参数集合</returns>
        internal static SqlParameter[] InternalGetSqlParameters(Object entity)
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
                DatabaseColumnAttribute attr = null;
                Object[] objs = prop.GetCustomAttributes(typeof(DatabaseColumnAttribute), true);

                foreach (Object obj in objs)
                {
                    if ((attr = obj as DatabaseColumnAttribute) != null)
                    {
                        SqlParameter parameter = EntityHelper.InternalGetSqlParameter(attr, prop.GetValue(entity, null));
                        parameters.Add(parameter);
                        break;
                    }
                }
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