using System;

namespace CML.ToolKit.SocketEx
{
    /// <summary>
    /// 默认变量
    /// </summary>
    internal static class ISDefault
    {
        /// <summary>
        /// 默认消息
        /// </summary>
        public static string DefMessage => "[_DEFAULT_MSG_]";

        /// <summary>
        /// 默认时间
        /// </summary>
        public static DateTime DefDateTime => new DateTime(9696, 01, 01);

        /// <summary>
        /// 默认GUID
        /// </summary>
        public static Guid DefGuid => Guid.NewGuid();
    }
}
