using CML.ToolKit.SocketEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CML.ToolKit.ToolTest
{
    /// <summary>
    /// Socket工具包测试类
    /// </summary>
    internal class SocketExTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类实例
        /// </summary>
        public static ToolKitTestBase Instance => new SocketExTest();

        /// <summary>
        /// 执行测试
        /// </summary>
        public override void ExecuteTest()
        {
            SocketServer server = new SocketServer()
            {
                ReSendTimes = 5,
                RestartTime = 5
            };

            server.ReceiveMessage += Server_ReceiveMessage;
            server.InitServer("127.0.0.1", 9696);
            server.StartServer();
        }

        /// <summary>
        /// 接受服务端消息
        /// </summary>
        /// <param name="msg"></param>
        private void Server_ReceiveMessage(ModMessage msg)
        {
            switch (msg.MsgType)
            {
                case EMsgType.Infomation:
                    if (msg.Client == null)
                        PrintLn(MsgType.INFO, $"{msg.Message}");
                    else
                        PrintLn(MsgType.INFO, $"<{msg.Client.Name}/{msg.Client.Socket.RemoteEndPoint}>{msg.Message}");
                    break;
                case EMsgType.System:
                    if (msg.Client == null)
                        PrintLn(MsgType.WARN, $"{msg.Message}");
                    else
                        PrintLn(MsgType.WARN, $"<{msg.Client.Name}/{msg.Client.Socket.RemoteEndPoint}>{msg.Message}");
                    break;
                case EMsgType.Error:
                    if (msg.Client == null)
                        PrintLn(MsgType.ERROR, $"{msg.Message}");
                    else
                        PrintLn(MsgType.ERROR, $"<{msg.Client.Name}/{msg.Client.Socket.RemoteEndPoint}>{msg.Message}");
                    break;
            }
        }
    }
}
