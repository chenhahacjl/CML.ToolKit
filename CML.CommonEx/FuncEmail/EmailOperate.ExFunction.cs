namespace CML.CommonEx.EmailEx.ExFunction
{
    /// <summary>
    /// Email操作类（扩展方法）
    /// </summary>
    public static class EmailOperateEF
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="sendInfo">发送信息</param>
        /// <param name="emailInfo">邮件信息</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool CF_SendEmail(this ModServerInfo sendInfo, ModEmailInfo emailInfo, out string errMsg)
        {
            return EmailOperate.CF_SendEmail(sendInfo, emailInfo, out errMsg);
        }
    }
}
