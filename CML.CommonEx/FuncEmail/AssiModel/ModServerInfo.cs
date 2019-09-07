namespace CML.CommonEx.EmailEx
{
    /// <summary>
    /// 发送信息
    /// </summary>
    public class ModServerInfo
    {
        /// <summary>
        /// SMTP服务器
        /// </summary>
        public string SmtpHost { get; set; } = string.Empty;

        /// <summary>
        /// SMTP服务器端口(默认25)
        /// </summary>
        public int SmtpPort { get; set; } = 25;

        /// <summary>
        /// SMTP服务器用户名
        /// </summary>
        public string SmtpUser { get; set; } = string.Empty;

        /// <summary>
        /// SMTP服务器密码
        /// </summary>
        public string SmtpPwd { get; set; } = string.Empty;

        /// <summary>
        /// 是否启用SSL加密连接（默认为False）
        /// </summary>
        public bool EnableSsl { get; set; } = false;
    }
}
