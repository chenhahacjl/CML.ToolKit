namespace CML.ToolKit.SocketEx
{
    /// <summary>
    /// 正则变量
    /// </summary>
    internal static class ISRegex
    {
        /// <summary>
        /// 普通消息解析正则
        /// </summary>
        public static string RegMsgNormal => "\\[_MSG_START_\\](.+?)\\[_MSG_END_\\]";

        /// <summary>
        /// 计算机名称消息解析正则
        /// </summary>
        public static string RegMsgPcName => "\\[_PC_NAME_START_\\](.+?)\\[_PC_NAME_END_\\]";

    }
}
