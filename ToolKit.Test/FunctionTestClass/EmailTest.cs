using CML.CommonEx.EmailEx;
using CML.CommonEx.EmailEx.ExFunction;
using System.Collections.Generic;
using System.Net.Mail;

namespace ToolKit.Test
{
    /// <summary>
    /// 邮件操作测试类
    /// </summary>
    internal class EmailTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "EmailTest";

        /// <summary>
        /// 版本信息
        /// </summary>
        public override void CF_GetVersionInfo()
        {
            PrintMsgLn(MsgType.Success, "⊙日志信息⊙");
            PrintMsgLn(MsgType.Info, new VersionInfo().CF_GetVersionInfo());
        }

        /// <summary>
        /// 执行测试
        /// </summary>
        public override void ExecuteTest()
        {
            ModServerInfo sendInfo = new ModServerInfo()
            {
                SmtpHost = "smtp.host.com",
                SmtpPort = 9696,
                SmtpUser = "example@host.com",
                SmtpPwd = "password",
                EnableSsl = true,
            };

            ModEmailInfo emailInfo = new ModEmailInfo()
            {
                FromEmail = "example@host.com",
                ToEmail = new List<string> { "example@host.com" },
                AttachmentList = new List<Attachment>()
                  { new Attachment(@"X:\FilePath")},
            };

            bool result = sendInfo.CF_SendEmail(emailInfo, out string errMsg);

            if (result)
            {
                PrintLogLn(MsgType.Success, "邮件发送成功！");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"邮件发送失败: {errMsg}");
            }
        }
    }
}
