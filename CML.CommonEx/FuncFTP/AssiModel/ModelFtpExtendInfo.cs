namespace CML.CommonEx.FTPEx
{
    /// <summary>
    /// FTP扩展信息模型
    /// </summary>
    public class ModelFtpExtendInfo
    {
        /// <summary>
        /// 请求超时（毫秒，默认5秒）
        /// </summary>
        public int Timeout { get; set; } = 5000;

        /// <summary>
        /// 读写超时（毫秒，默认5秒）
        /// </summary>
        public int ReadWriteTimeout { get; set; } = 5000;

        /// <summary>
        /// 是否启用SSL连接（默认false）
        /// </summary>
        public bool EnableSsl { get; set; } = false;

        /// <summary>
        /// 是否启用二进制传输（默认true）
        /// </summary>
        public bool UseBinary { get; set; } = true;

        /// <summary>
        /// 是否为被动模式请求（默认false）
        /// </summary>
        public bool UsePassive { get; set; } = false;

        /// <summary>
        /// 请求完成之后是否保持连接（默认false）
        /// </summary>
        public bool KeepAlive { get; set; } = false;

        /// <summary>
        /// 代理（默认不开启）
        /// </summary>
        public ModelProxy Proxy { get; set; } = new ModelProxy();
    }
}
