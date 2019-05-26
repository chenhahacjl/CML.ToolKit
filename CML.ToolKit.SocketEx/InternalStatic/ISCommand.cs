namespace CML.ToolKit.SocketEx
{
    /// <summary>
    /// 指令变量
    /// </summary>
    internal static class ISCommand
    {
        /// <summary>
        /// 获取客户端PC名称命令
        /// </summary>
        public static string CmdGetPcName => "[_GET_PC_NAME_]";

        /// <summary>
        /// 客户端发送心跳包
        /// </summary>
        public static string CmdClientHB => "[_CLIENT_HEART_BEAT_]";

        /// <summary>
        /// 服务端发送心跳包
        /// </summary>
        public static string CmdServerHB => "[_SERVER_HEART_BEAT_]";
    }
}
