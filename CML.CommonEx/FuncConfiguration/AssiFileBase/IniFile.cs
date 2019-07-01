using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CML.CommonEx.ConfigurationEx
{
    /// <summary>
    /// INI文件操作基础类
    /// </summary>
    internal class IniFile
    {
        #region DLL引用
        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section">节点名称[如[TypeName]]</param>
        /// <param name="key">键名</param>
        /// <param name="val">键值</param>
        /// <param name="filePath">文件路径</param>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键名</param>
        /// <param name="def">键值</param>
        /// <param name="retVal">StringBulider对象</param>
        /// <param name="size">字节大小</param>
        /// <param name="filePath">文件路径</param>
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键名</param>
        /// <param name="def">键值</param>
        /// <param name="retVal">字节数组</param>
        /// <param name="size">字节大小</param>
        /// <param name="filePath">文件路径</param>
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern uint GetPrivateProfileStringA(string section, string key, string def, byte[] retVal, int size, string filePath);
        #endregion

        #region 公共属性
        /// <summary>
        /// INI文件路径
        /// </summary>
        public string CP_FilePath { get; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filePath">INI文件路径</param>
        public IniFile(string filePath)
        {
            CP_FilePath = filePath;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 读取所有节点名称
        /// </summary>
        /// <returns>节点名称</returns>
        public List<string> CF_ReadSections()
        {
            byte[] buffer = new byte[65536];
            uint length = GetPrivateProfileStringA(null, null, null, buffer, buffer.Length, CP_FilePath);

            int start = 0;
            List<string> result = new List<string>();
            for (int i = 0; i < length; i++)
            {
                if (buffer[i] == 0)
                {
                    result.Add(Encoding.Default.GetString(buffer, start, i - start));
                    start = i + 1;
                }
            }

            return result;
        }

        /// <summary>
        /// 读取所有键名
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <returns>键名</returns>
        public List<string> CF_ReadKeys(string section)
        {
            byte[] buffer = new byte[65536];
            uint length = GetPrivateProfileStringA(section, null, null, buffer, buffer.Length, CP_FilePath);

            int start = 0;
            List<string> result = new List<string>();
            for (int i = 0; i < length; i++)
            {
                if (buffer[i] == 0)
                {
                    result.Add(Encoding.Default.GetString(buffer, start, i - start));
                    start = i + 1;
                }
            }

            return result;
        }

        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        public void CF_WriteValue(string section, string key, string value)
        {
            _ = WritePrivateProfileString(section, key, value, CP_FilePath);
        }

        /// <summary>
        /// 读出INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键名</param>
        public string CF_ReadValue(string section, string key)
        {
            StringBuilder temp = new StringBuilder(1024);
            _ = GetPrivateProfileString(section, key, "", temp, 65535, CP_FilePath);

            return temp.ToString();
        }
        #endregion
    }
}
