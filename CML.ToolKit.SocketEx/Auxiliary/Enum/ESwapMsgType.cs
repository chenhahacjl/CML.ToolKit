using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CML.ToolKit.SocketEx
{
    /// <summary>
    /// 交换消息类型
    /// </summary>
    internal enum ESwapMsgType
    {
        /// <summary>
        /// 错误消息
        /// </summary>
        BadMessage,
        /// <summary>
        /// 未知消息
        /// </summary>
        UnknowMessage,
        /// <summary>
        /// 普通消息
        /// </summary>
        NormalMessage,
        /// <summary>
        /// 心跳包
        /// </summary>
        HeartBeat,
        /// <summary>
        /// 计算机名称
        /// </summary>
        ComputerName,
        /// <summary>
        /// 下线命令
        /// </summary>
        ShutdownCommand,
    }
}
