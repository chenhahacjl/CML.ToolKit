using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CML.SocketEx
{
    /// <summary>
    /// 交换消息
    /// </summary>
    internal class ModSwapMessage
    {
        /// <summary>
        /// 交换信息类型
        /// </summary>
        public ESwapMsgType SwapMsgType { get; }

        /// <summary>
        /// 交换信息内容
        /// </summary>
        public string SwapMsg { get; }

        /// <summary>
        /// 交换信息内容
        /// </summary>
        public ModClient Client { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="swapMsgType">交换信息类型</param>
        /// <param name="swapMsg">交换信息内容</param>
        /// <param name="client">客户端（服务端使用时写）</param>
        public ModSwapMessage(ESwapMsgType swapMsgType, string swapMsg, ModClient client)
        {
            SwapMsgType = swapMsgType;
            SwapMsg = swapMsg;
            Client = client;
        }
    }
}
