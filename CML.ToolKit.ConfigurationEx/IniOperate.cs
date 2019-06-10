using System;
using System.IO;

namespace CML.ToolKit.ConfigurationEx
{
    /// <summary>
    /// INI操作帮助类
    /// </summary>
    public class IniOperate
    {
        #region 静态
        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="iniPath">文件</param>
        /// <param name="section">节名</param>
        /// <param name="key">键名</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static string CF_ReadConfig(string iniPath, string section, string key, string defValue = "")
        {
            if (File.Exists(iniPath))
            {
                IniFile iniFile = new IniFile(iniPath);
                string strValue = iniFile.CF_ReadValue(section, key);

                if (string.IsNullOrWhiteSpace(strValue))
                {
                    return defValue;
                }
                else
                {
                    return strValue;
                }
            }
            else
            {
                return defValue;
            }
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iniPath">文件</param>
        /// <param name="section">节名</param>
        /// <param name="key">键名</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static T CF_ReadConfig<T>(string iniPath, string section, string key, T defValue = default)
        {
            if (File.Exists(iniPath))
            {
                IniFile iniFile = new IniFile(iniPath);
                string value = iniFile.CF_ReadValue(section, key);

                if (string.IsNullOrWhiteSpace(value))
                {
                    return defValue;
                }
                else if (typeof(T).IsEnum)
                {
                    return (T)Enum.Parse(typeof(T), value, true);
                }
                else
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }
            else
            {
                return defValue;
            }
        }

        /// <summary>
        /// 写配置文件
        /// </summary>
        /// <param name="iniPath">文件</param>
        /// <param name="section">节名</param>
        /// <param name="key">键名</param>
        /// <param name="strValue">键值</param>
        public static void CF_WriteConfig(string iniPath, string section, string key, string strValue)
        {
            //创建文件夹
            if (!new FileInfo(iniPath).Directory.Exists)
            {
                new FileInfo(iniPath).Directory.Create();
            }

            IniFile iniFile = new IniFile(iniPath);
            iniFile.CF_WriteValue(section, key, strValue);
        }

        /// <summary>
        /// 写配置文件
        /// </summary>
        /// <param name="iniPath">文件</param>
        /// <param name="section">节名</param>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        public static void CF_WriteConfig<T>(string iniPath, string section, string key, T value)
        {
            //创建文件夹
            if (!new FileInfo(iniPath).Directory.Exists)
            {
                new FileInfo(iniPath).Directory.Create();
            }

            IniFile iniFile = new IniFile(iniPath);
            iniFile.CF_WriteValue(section, key, Convert.ToString(value));
        }
        #endregion

        #region 动态

        #region 私有变量
        /// <summary>
        /// INI文件操作基础类
        /// </summary>
        private readonly IniFile m_iniFile;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iniPath">INI文件路径</param>
        public IniOperate(string iniPath)
        {
            m_iniFile = new IniFile(iniPath);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="section">节名</param>
        /// <param name="key">键名</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public string CF_ReadConfig(string section, string key, string defValue = "")
        {
            if (File.Exists(m_iniFile.CP_FilePath))
            {
                string strValue = m_iniFile.CF_ReadValue(section, key);

                if (string.IsNullOrWhiteSpace(strValue))
                {
                    return defValue;
                }
                else
                {
                    return strValue;
                }
            }
            else
            {
                return defValue;
            }
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="section">节点名称</param>
        /// <param name="key">键名</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public T CF_ReadConfig<T>(string section, string key, T defValue = default)
        {
            if (File.Exists(m_iniFile.CP_FilePath))
            {
                string value = m_iniFile.CF_ReadValue(section, key);

                if (string.IsNullOrWhiteSpace(value))
                {
                    return defValue;
                }
                else if (typeof(T).IsEnum)
                {
                    return (T)Enum.Parse(typeof(T), value, true);
                }
                else
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }
            else
            {
                return defValue;
            }
        }

        /// <summary>
        /// 写配置文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键名</param>
        /// <param name="strValue">键值</param>
        public void CF_WriteConfig(string section, string key, string strValue)
        {
            //创建文件夹
            if (!new FileInfo(m_iniFile.CP_FilePath).Directory.Exists)
            {
                new FileInfo(m_iniFile.CP_FilePath).Directory.Create();
            }

            m_iniFile.CF_WriteValue(section, key, strValue);
        }

        /// <summary>
        /// 写配置文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        public void CF_WriteConfig<T>(string section, string key, T value)
        {
            //创建文件夹
            if (!new FileInfo(m_iniFile.CP_FilePath).Directory.Exists)
            {
                new FileInfo(m_iniFile.CP_FilePath).Directory.Create();
            }

            m_iniFile.CF_WriteValue(section, key, Convert.ToString(value));
        }
        #endregion

        #endregion
    }
}
