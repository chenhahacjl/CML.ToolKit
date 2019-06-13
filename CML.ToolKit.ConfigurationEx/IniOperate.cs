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
        /// <returns>读取结果</returns>
        public static string CF_ReadConfig(string iniPath, string section, string key, string defValue = "")
        {
            //读取结果
            string result = defValue;

            //路径是否为空
            if (!string.IsNullOrEmpty(iniPath))
            {
                try
                {
                    string strValue = new IniFile(iniPath).CF_ReadValue(section, key);

                    if (!string.IsNullOrWhiteSpace(strValue))
                    {
                        result = strValue;
                    }
                }
                catch { }
            }

            return result;
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iniPath">文件</param>
        /// <param name="section">节名</param>
        /// <param name="key">键名</param>
        /// <param name="defValue">默认值</param>
        /// <returns>读取结果</returns>
        public static T CF_ReadConfig<T>(string iniPath, string section, string key, T defValue = default)
        {
            //读取结果
            T result = defValue;

            //路径是否为空
            if (!string.IsNullOrEmpty(iniPath))
            {
                try
                {
                    string value = new IniFile(iniPath).CF_ReadValue(section, key);

                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        if (typeof(T).IsEnum)
                        {
                            result = (T)Enum.Parse(typeof(T), value, true);
                        }
                        else
                        {
                            result = (T)Convert.ChangeType(value, typeof(T));
                        }
                    }
                }
                catch { }
            }

            return result;
        }

        /// <summary>
        /// 写配置文件
        /// </summary>
        /// <param name="iniPath">文件</param>
        /// <param name="section">节名</param>
        /// <param name="key">键名</param>
        /// <param name="strValue">键值</param>
        /// <returns>执行结果</returns>
        public static bool CF_WriteConfig(string iniPath, string section, string key, string strValue)
        {
            //执行结果
            bool result = false;

            //路径是否为空
            if (!string.IsNullOrEmpty(iniPath))
            {
                try
                {
                    //创建文件夹
                    if (!new FileInfo(iniPath).Directory.Exists)
                    {
                        new FileInfo(iniPath).Directory.Create();
                    }

                    new IniFile(iniPath).CF_WriteValue(section, key, strValue);
                    result = true;
                }
                catch { }
            }

            return result;
        }

        /// <summary>
        /// 写配置文件
        /// </summary>
        /// <param name="iniPath">文件</param>
        /// <param name="section">节名</param>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        /// <returns>执行结果</returns>
        public static bool CF_WriteConfig<T>(string iniPath, string section, string key, T value)
        {
            //执行结果
            bool result = false;

            //路径是否为空
            if (!string.IsNullOrEmpty(iniPath))
            {
                try
                {
                    //创建文件夹
                    if (!new FileInfo(iniPath).Directory.Exists)
                    {
                        new FileInfo(iniPath).Directory.Create();
                    }

                    new IniFile(iniPath).CF_WriteValue(section, key, Convert.ToString(value));
                    result = true;
                }
                catch { }
            }

            return result;
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
        /// <returns>读取结果</returns>
        public string CF_ReadConfig(string section, string key, string defValue = "")
        {
            //读取结果
            string result = defValue;

            //路径是否为空
            if (!string.IsNullOrEmpty(m_iniFile.CP_FilePath))
            {
                try
                {
                    string strValue = m_iniFile.CF_ReadValue(section, key);

                    if (!string.IsNullOrWhiteSpace(strValue))
                    {
                        result = strValue;
                    }
                }
                catch { }
            }

            return result;
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="section">节点名称</param>
        /// <param name="key">键名</param>
        /// <param name="defValue">默认值</param>
        /// <returns>读取结果</returns>
        public T CF_ReadConfig<T>(string section, string key, T defValue = default)
        {
            //读取结果
            T result = defValue;

            //路径是否为空
            if (!string.IsNullOrEmpty(m_iniFile.CP_FilePath))
            {
                try
                {
                    string value = m_iniFile.CF_ReadValue(section, key);

                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        if (typeof(T).IsEnum)
                        {
                            result = (T)Enum.Parse(typeof(T), value, true);
                        }
                        else
                        {
                            result = (T)Convert.ChangeType(value, typeof(T));
                        }
                    }
                }
                catch { }
            }

            return result;
        }

        /// <summary>
        /// 写配置文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键名</param>
        /// <param name="strValue">键值</param>
        /// <returns>执行结果</returns>
        public bool CF_WriteConfig(string section, string key, string strValue)
        {
            //执行结果
            bool result = false;

            //路径是否为空
            if (!string.IsNullOrEmpty(m_iniFile.CP_FilePath))
            {
                try
                {
                    //创建文件夹
                    if (!new FileInfo(m_iniFile.CP_FilePath).Directory.Exists)
                    {
                        new FileInfo(m_iniFile.CP_FilePath).Directory.Create();
                    }

                    m_iniFile.CF_WriteValue(section, key, strValue);
                    result = true;
                }
                catch { }
            }

            return result;
        }

        /// <summary>
        /// 写配置文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        /// <returns>执行结果</returns>
        public bool CF_WriteConfig<T>(string section, string key, T value)
        {
            //执行结果
            bool result = false;

            //路径是否为空
            if (!string.IsNullOrEmpty(m_iniFile.CP_FilePath))
            {
                try
                {
                    //创建文件夹
                    if (!new FileInfo(m_iniFile.CP_FilePath).Directory.Exists)
                    {
                        new FileInfo(m_iniFile.CP_FilePath).Directory.Create();
                    }

                    m_iniFile.CF_WriteValue(section, key, Convert.ToString(value));
                    result = true;
                }
                catch { }
            }

            return result;
        }
        #endregion

        #endregion
    }
}
