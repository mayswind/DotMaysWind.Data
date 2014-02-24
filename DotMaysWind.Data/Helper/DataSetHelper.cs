using System;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace DotMaysWind.Data.Helper
{
    /// <summary>
    /// 数据集辅助类
    /// </summary>
    internal static class DataSetHelper
    {
        /// <summary>
        /// 创建数据集
        /// </summary>
        /// <param name="dbProvider">数据库提供者</param>
        /// <param name="dbCommand">数据库命令</param>
        /// <param name="tableNames">数据表名称</param>
        /// <returns>数据集</returns>
        internal static DataSet InternalCreateDataSet(DbProviderFactory dbProvider, DbCommand dbCommand, String[] tableNames)
        {
            DataSet dataSet = new DataSet();
            dataSet.Locale = CultureInfo.InvariantCulture;

            using (DbDataAdapter dataAdapter = dbProvider.CreateDataAdapter())
            {
                ((IDbDataAdapter)dataAdapter).SelectCommand = dbCommand;
                String text = "Table";

                for (Int32 j = 0; j < tableNames.Length; j++)
                {
                    String sourceTable = (j == 0) ? text : (text + j);
                    dataAdapter.TableMappings.Add(sourceTable, tableNames[j]);
                }

                dataAdapter.Fill(dataSet);
            }

            return dataSet;
        }

        /// <summary>
        /// 创建数据集
        /// </summary>
        /// <param name="dbProvider">数据库提供者</param>
        /// <param name="dbCommand">数据库命令</param>
        /// <returns>数据集</returns>
        internal static DataSet InternalCreateDataSet(DbProviderFactory dbProvider, DbCommand dbCommand)
        {
            return DataSetHelper.InternalCreateDataSet(dbProvider, dbCommand, new String[] { "Table" });
        }
    }
}