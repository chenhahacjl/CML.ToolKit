namespace CML.SocketEx
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
        /// 客户端心跳包命令
        /// </summary>
        public static string CmdClientHB => "[_CLIENT_HEART_BEAT_]";

        /// <summary>
        /// 服务端心跳包命令
        /// </summary>
        public static string CmdServerHB => "[_SERVER_HEART_BEAT_]";

        /// <summary>
        /// 客户端自主下线命令
        /// </summary>
        public static string CmdClientNeedShutdown => "[_CLIENT_NEED_SHUTDOWN_]";

        /// <summary>
        /// 客户端强制下线命令
        /// </summary>
        public static string CmdClientRequShutdown => "[_CLIENT_REQUIR_SHUTDOWN_]";
    }
}
