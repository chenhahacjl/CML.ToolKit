using CML.ToolKit.ConfigurationEx;
using System;
using System.Collections.Generic;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 数据表控件帮助类
    /// </summary>
    internal class DataGridViewOperate
    {
        /// <summary>
        /// 读取数据列可见性
        /// </summary>
        /// <param name="cfgFile">配置文件</param>
        /// <param name="dgvName">数据表名</param>
        /// <returns>数据列可见性</returns>
        public static Dictionary<string, bool> LoadColumnVisibility(string cfgFile, string dgvName)
        {
            //读取配置
            string strStatu = IniOperate.CF_ReadConfig(cfgFile, "DataGridView", dgvName).Trim();

            //转换为状态字典
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            try
            {
                string[] columns = strStatu.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string column in columns)
                {
                    result.Add(
                        column.Split('|')[0].Trim(),
                        column.Split('|')[1].ToUpper().Trim() == "TRUE"
                    );
                }
            }
            catch
            {
                result.Clear();
            }

            return result;
        }

        /// <summary>
        /// 保存数据列可见性
        /// </summary>
        /// <param name="cfgFile">配置文件</param>
        /// <param name="dgvName">数据表名</param>
        /// <param name="visibility">数据列可见性</param>
        public static void SaveColumnVisibility(string cfgFile, string dgvName, Dictionary<string, bool> visibility)
        {
            //转换为字符串
            string result = "";
            foreach (string key in visibility.Keys)
            {
                result += key + "|" + visibility[key].ToString().ToUpper() + ";";
            }

            //保存配置
            IniOperate.CF_WriteConfig(cfgFile, "DataGridView", dgvName, result);
        }
    }
}
