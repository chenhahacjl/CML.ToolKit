using System;
using System.Net.Sockets;

namespace CML.ToolKit.SocketEx
{
    /// <summary>
    /// 信息模型
    /// </summary>
    public struct ModServerMessage
    {
        #region 公共属性
        /// <summary>
        /// 客户端
        /// </summary>
        public ModClient Client { get; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public EMsgType MsgType { get; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 信息源
        /// </summary>
        public string Source
        {
            get
            {
                string source = "";
                try
                {
                    if (Client.Socket != null) { source = Client.Socket.RemoteEndPoint.ToString(); }
                }
                catch (Exception) { }

                return source;
            }
        }

        /// <summary>
        /// 空消息标记
        /// </summary>
        public bool IsEmptyMsg { get => Message == ISDefault.DefMessage; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造Socket信息对象
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="msgType">消息类型</param>
        /// <param name="message">消息内容</param>
        public ModServerMessage(ModClient client, EMsgType msgType, string message)
        {
            Client = client;
            MsgType = msgType;
            Message = string.IsNullOrEmpty(message) ? ISDefault.DefMessage : message;
        }
        #endregion
    }
}
