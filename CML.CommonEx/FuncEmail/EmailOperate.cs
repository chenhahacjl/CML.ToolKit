using System;
using System.Net;
using System.Net.Mail;

namespace CML.CommonEx.EmailEx
{
    /// <summary>
    /// Email操作类
    /// </summary>
    public class EmailOperate
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="sendInfo">发送信息</param>
        /// <param name="emailInfo">邮件信息</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_SendEmail(ModServerInfo sendInfo, ModEmailInfo emailInfo, out string errMsg)
        {
            bool result;

            try
            {
                if (string.IsNullOrEmpty(sendInfo?.SmtpHost))
                {
                    errMsg = "请填写SMTP服务器！";
                    return false;
                }

                if (sendInfo.SmtpPort < 1 || sendInfo.SmtpPort > 65535)
                {
                    errMsg = "SMTP服务器端口填写错误！";
                    return false;
                }

                if (emailInfo.ToEmail == null || emailInfo.ToEmail.Count == 0)
                {
                    errMsg = "请填写收件人！";
                    return false;
                }

                //构造邮件
                MailMessage mailMsg = new MailMessage
                {
                    From = new MailAddress(emailInfo.FromEmail, emailInfo.FromName, emailInfo.Encoding),
                    Priority = emailInfo.MailPriority,
                    SubjectEncoding = emailInfo.Encoding,
                    BodyEncoding = emailInfo.Encoding,
                };

                //收件人
                emailInfo.ToEmail?.ForEach(item => mailMsg.To.Add(item));
                //抄送人
                emailInfo.CCEmail?.ForEach(item => mailMsg.CC.Add(item));
                //密送人
                emailInfo.BCCEmail?.ForEach(item => mailMsg.Bcc.Add(item));
                //邮件标题
                mailMsg.Subject = emailInfo.Subject;
                // 邮件正文
                mailMsg.Body = emailInfo.Body;
                //邮件正文是否是HTML格式
                mailMsg.IsBodyHtml = emailInfo.IsBodyHtml;
                //邮件附件
                emailInfo.AttachmentList?.ForEach(item => mailMsg.Attachments.Add(item));

                //构造SMTP协议
                SmtpClient smtpClient = new SmtpClient
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = sendInfo.EnableSsl,
                    Host = sendInfo.SmtpHost,
                    Port = sendInfo.SmtpPort,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(sendInfo.SmtpUser, sendInfo.SmtpPwd)
                };

                //发送邮件
                smtpClient.Send(mailMsg);

                errMsg = "";
                result = true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                result = false;
            }

            return result;
        }
    }
}
