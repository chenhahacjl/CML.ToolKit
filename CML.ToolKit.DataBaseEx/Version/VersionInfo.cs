using System.IO;
using System.Reflection;
using System.Text;

namespace CML.ToolKit.DataBaseEx
{
    /// <summary>
    /// 数据库操作工具包版本信息
    /// </summary>
    public class VersionInfo
    {
        #region 版本信息
        /// <summary>
        /// 主版本号
        /// </summary>
        public string VerMain => "1.1";
        /// <summary>
        /// 研发版本号
        /// </summary>
        public string VerDev => "19Y002R001";
        /// <summary>
        /// 更新时间
        /// </summary>
        public string VerDate => "2019年06月19日 15:15";
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取升级记录
        /// </summary>
        /// <param name="file">嵌入资源路径</param>
        /// <returns>更新记录</returns>
        private string GetUpdateInfo(string file)
        {
            try
            {
                //版本信息
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(file))
                {
                    using (StreamReader sr = new StreamReader(stream, Encoding.Default))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获得版本信息
        /// </summary>
        /// <returns>版本信息</returns>
        public string GetVersionInfo()
        {
            string filePath = "CML.ToolKit.DataBaseEx.Version.UpdateInfo.LOG";
            return GetUpdateInfo(filePath);
        }
        #endregion
    }
}
