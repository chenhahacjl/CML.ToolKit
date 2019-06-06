using System.Runtime.InteropServices;
using System.Text;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// INI操作帮助类
    /// </summary>
    internal class IniFileOperate
    {
        #region DLL引用
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        #endregion

        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="iniPath">文件路径</param>
        /// <param name="section">节名</param>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        public static void IniWriteValue(string iniPath, string section, string key, string value)
        {
            _ = WritePrivateProfileString(section, key, value, iniPath);
        }

        /// <summary>
        /// 读出INI文件
        /// </summary>
        /// <param name="iniPath">文件路径</param>
        /// <param name="Section">节名</param>
        /// <param name="Key">键名</param> 
        public static string IniReadValue(string iniPath, string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            _ = GetPrivateProfileString(Section, Key, "", temp, 500, iniPath);

            return temp.ToString();
        }
    }
}
