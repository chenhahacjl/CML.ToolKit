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
        /// 写入配置文件
        /// </summary>
        /// <param name="section">节点名称（比如: [<see langword="CONFIG"/>]）</param>
        /// <param name="key">键名（若此参数为 <see langword="null"/>，则删除整节点）（比如: <see langword="KEY"/>=VALUE）</param>
        /// <param name="value">键值（若此参数为 <see langword="null"/>，则删除整个键值）（比如: KEY=<see langword="VALUE"/>）</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>返回值（若执行成功，则返回非 0 值）</returns>
        [DllImport("kernel32", EntryPoint = "WritePrivateProfileString")]
        public static extern int WritePrivateProfileString(byte[] section, byte[] key, byte[] value, string filePath);

        /// <summary>
        /// 读取配置文件，获取键值字符串
        /// </summary>
        /// <param name="section">节点名称（比如: [<see langword="CONFIG"/>]）</param>
        /// <param name="key">键名（若此参数为 <see langword="null"/>，则删除整节点）（比如: <see langword="KEY"/>=VALUE）</param>
        /// <param name="defValue">默认值</param>
        /// <param name="retValue">[OUT] 缓冲区（接收读取值）</param>
        /// <param name="size">缓冲区大小</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>复制到缓冲区的字符数</returns>
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        public static extern int GetPrivateProfileString(byte[] section, byte[] key, byte[] defValue, byte[] retValue, int size, string filePath);

        #endregion

        #region 公共属性
        /// <summary>
        /// INI文件路径
        /// </summary>
        public string CP_FilePath { get; }
        /// <summary>
        /// 编码方式
        /// </summary>
        public Encoding CP_Encoding { get; } = Encoding.Default;
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
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filePath">INI文件路径</param>
        /// <param name="encoding">编码方式</param>
        public IniFile(string filePath, Encoding encoding)
        {
            CP_FilePath = filePath;
            CP_Encoding = encoding;
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
            int length = GetPrivateProfileString(null, null, null, buffer, buffer.Length, CP_FilePath);

            int start = 0;
            List<string> result = new List<string>();
            for (int i = 0; i < length; i++)
            {
                if (buffer[i] == 0)
                {
                    result.Add(CP_Encoding.GetString(buffer, start, i - start));
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
            int length = GetPrivateProfileString(CP_Encoding.GetBytes(section), null, null, buffer, buffer.Length, CP_FilePath);

            int start = 0;
            List<string> result = new List<string>();
            for (int i = 0; i < length; i++)
            {
                if (buffer[i] == 0)
                {
                    result.Add(CP_Encoding.GetString(buffer, start, i - start));
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
            _ = WritePrivateProfileString(
                section is null ? null : CP_Encoding.GetBytes(section),
                key is null ? null : CP_Encoding.GetBytes(key),
                value is null ? null : CP_Encoding.GetBytes(value)
                , CP_FilePath
            );
        }

        /// <summary>
        /// 读出INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键名</param>
        public string CF_ReadValue(string section, string key)
        {
            byte[] temp = new byte[65535];

            int length = GetPrivateProfileString(
                section is null ? null : CP_Encoding.GetBytes(section),
                key is null ? null : CP_Encoding.GetBytes(key),
                null,
                temp,
                65535,
                CP_FilePath
            );

            return CP_Encoding.GetString(temp, 0, length);
        }
        #endregion
    }
}
