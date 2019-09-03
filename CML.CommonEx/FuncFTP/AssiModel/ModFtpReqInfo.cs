using System.Collections;

namespace CML.CommonEx.FTPEx
{
    /// <summary>
    /// FTP请求信息模型
    /// </summary>
    public class ModFtpReqInfo
    {
        /// <summary>
        /// 备份的请求路径
        /// </summary>
        private readonly Stack m_bakRequestPath = new Stack();

        /// <summary>
        /// 请求路径（默认为根目录）
        /// </summary>
        public string RequestPath { get; set; } = "";

        /// <summary>
        /// 请求方式（默认ListDirectory）
        /// </summary>
        public EFtpRequestMethod Method { get; set; } = EFtpRequestMethod.ListDirectory;

        /// <summary>
        /// 目标文件名（重命名时使用）
        /// </summary>
        public string RenameTo { get; set; } = "";

        /// <summary>
        /// 上传限速（上传时使用）
        /// </summary>
        public ModTransmissionSpeed UploadSpeed { get; set; } = new ModTransmissionSpeed();

        /// <summary>
        /// 下载限速（下载时使用）
        /// </summary>
        public ModTransmissionSpeed DownloadSpeed { get; set; } = new ModTransmissionSpeed();

        /// <summary>
        /// 构造函数（默认构造）
        /// </summary>
        public ModFtpReqInfo() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestPath">请求路径</param>
        public ModFtpReqInfo(string requestPath)
        {
            RequestPath = requestPath;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="method">请求方式</param>
        public ModFtpReqInfo(EFtpRequestMethod method)
        {
            Method = method;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestPath">请求路径</param>
        /// <param name="method">请求方式</param>
        public ModFtpReqInfo(string requestPath, EFtpRequestMethod method)
        {
            RequestPath = requestPath;
            Method = method;
        }

        /// <summary>
        /// 备份请求路径
        /// </summary>
        public void CF_PushRequestPath()
        {
            m_bakRequestPath.Push(RequestPath);
        }

        /// <summary>
        /// 还原请求路径
        /// </summary>
        public void CF_PopRequestPath()
        {
            if (m_bakRequestPath.Count != 0)
            {
                RequestPath = m_bakRequestPath.Pop() as string;
            }
        }
    }
}
