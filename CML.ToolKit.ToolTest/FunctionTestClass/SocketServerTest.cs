using CML.ToolKit.SocketEx;
using System.Threading;

namespace CML.ToolKit.ToolTest
{
    /// <summary>
    /// Socket服务端测试类
    /// </summary>
    internal class SocketServerTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "SocketServer";

        /// <summary>
        /// 执行测试
        /// </summary>
        public override void ExecuteTest()
        {
            //测试时间
            int testTimeSecs = 60 * 60;

            SocketServer server = new SocketServer()
            {
                CP_HeartBeatCheckTime = 5,
                CP_ReSendTimes = 5,
                CP_RestartTime = 5,
                CP_IsOpenHeartBeatCheck = false
            };

            server.CE_ReceiveMessage += Server_ReceiveMessage;
            server.CF_InitServer("127.0.0.1", 9696);
            server.CF_StartService();

            PrintLn(MsgType.Info, $"等待服务启动！");
            Thread.Sleep(2000);

            PrintLn(MsgType.Info, $"测试时间{testTimeSecs}秒！");
            while (testTimeSecs-- > 0 && server.CP_IsServerOpen)
            {
                //server.CF_SendMessage("Send To Client");
                Thread.Sleep(1000);
            }

            server.CF_StopService();
        }

        /// <summary>
        /// 接受服务端消息
        /// </summary>
        /// <param name="msg"></param>
        private void Server_ReceiveMessage(ModServerMessage msg)
        {
            switch (msg.MsgType)
            {
                case EMsgType.Infomation:
                    if (msg.Client == null || string.IsNullOrEmpty(msg.Client.Name))
                        PrintLn(MsgType.Info, $"{msg.Message}");
                    else
                        PrintLn(MsgType.Info, $"<{msg.Client.Name}/{msg.Client.Socket.RemoteEndPoint}>{msg.Message}");
                    break;
                case EMsgType.System:
                    if (msg.Client == null || string.IsNullOrEmpty(msg.Client.Name))
                        PrintLn(MsgType.Warn, $"{msg.Message}");
                    else
                        PrintLn(MsgType.Warn, $"<{msg.Client.Name}/{msg.Client.Socket.RemoteEndPoint}>{msg.Message}");
                    break;
                case EMsgType.Error:
                    if (msg.Client == null || string.IsNullOrEmpty(msg.Client.Name))
                        PrintLn(MsgType.Error, $"{msg.Message}");
                    else
                        PrintLn(MsgType.Error, $"<{msg.Client.Name}/{msg.Client.Socket.RemoteEndPoint}>{msg.Message}");
                    break;
            }
        }
    }
}
