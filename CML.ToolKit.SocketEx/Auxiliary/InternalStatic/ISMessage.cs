namespace CML.ToolKit.SocketEx
{
    /// <summary>
    /// 消息变量
    /// </summary>
    internal static class ISMessage
    {
        /// <summary>
        /// 普通消息开始标记
        /// </summary>
        public static string MsgNormalStart => "[_MSG_START_]";
        /// <summary>
        /// 普通消息结束标记
        /// </summary>
        public static string MsgNormalEnd => "[_MSG_END_]";
        /// <summary>
        /// 计算机名称消息开始标记
        /// </summary>
        public static string MsgPcNameStart => "[_PC_NAME_START_]";
        /// <summary>
        /// 计算机名称消息结束标记
        /// </summary>
        public static string MsgPcNameEnd => "[_PC_NAME_END_]";
    }
}
