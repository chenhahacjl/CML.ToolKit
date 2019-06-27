using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CML.CommonEx.VersionEx
{
    /// <summary>
    /// 版本控制基类
    /// </summary>
    public class VersionBase
    {
        #region 版本信息
        /// <summary>
        /// 主版本号
        /// </summary>
        public virtual string VerMain => "1.0";
        /// <summary>
        /// 研发版本号
        /// </summary>
        public virtual string VerDev => "19Y001A001";
        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual string VerDate => "2019年01月01日 00:00";
        /// <summary>
        /// 当前程序集 
        /// </summary>
        protected virtual Assembly RunAssembly => Assembly.GetExecutingAssembly();
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取升级记录
        /// </summary>
        /// <param name="file">嵌入资源路径</param>
        /// <returns>更新记录</returns>
        private string GetUpdateInfo(string file)
        {
            string result = "";
            try
            {
                //版本信息
                if (new List<string>(RunAssembly.GetManifestResourceNames()).Contains(file))
                {
                    using (Stream stream = RunAssembly.GetManifestResourceStream(file))
                    {
                        using (StreamReader sr = new StreamReader(stream, Encoding.Default))
                        {
                            result = sr.ReadToEnd();
                        }
                    }
                }
            }
            catch { }

            return result;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获得版本信息
        /// </summary>
        /// <param name="file">嵌入资源路径</param>
        /// <returns>版本信息</returns>
        public virtual string GetVersionInfo(string file = "")
        {
            string strVersion =
                "[主版本号]\r\n" + VerMain + "\r\n\r\n" +
                "[研发版本号]\r\n" + VerDev + "\r\n\r\n" +
                "[更新时间]\r\n" + VerDate;

            if (!string.IsNullOrEmpty(file))
            {
                strVersion += "\r\n\r\n[更新记录]\r\n" + GetUpdateInfo(file);
            }

            return strVersion;
        }
        #endregion
    }
}
