using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace CML.CommonEx.EmailEx
{
    /// <summary>
    /// 邮件信息
    /// </summary>
    public class ModEmailInfo
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public string FromEmail { get; set; } = string.Empty;

        /// <summary>
        /// 发送者显示名称
        /// </summary>
        public string FromName { get; set; } = "CML.Email.Sender";

        /// <summary>
        /// 收件者列表
        /// </summary>
        public List<string> ToEmail { get; set; } = null;

        /// <summary>
        /// 抄送者列表
        /// </summary>
        public List<string> CCEmail { get; set; } = null;

        /// <summary>
        /// 密送者列表
        /// </summary>
        public List<string> BCCEmail { get; set; } = null;

        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Subject { get; set; } = "CML.Email.DefaultSubject";

        /// <summary>
        /// 邮件正文
        /// </summary>
        public string Body { get; set; } = "CML.Email.DefaultBody";

        /// <summary>
        /// 邮件正文是否为HTML格式（默认为True）
        /// </summary>
        public bool IsBodyHtml { get; set; } = true;

        /// <summary>
        /// 附件列表
        /// </summary>
        public List<Attachment> AttachmentList { get; set; } = null;

        /// <summary>
        /// 文本编码格式
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 邮件优先级
        /// </summary>
        public MailPriority MailPriority { get; set; } = MailPriority.Normal;
    }
}
