using CML.ToolKit.SocketEx;
using System.Threading;

namespace CML.ToolKit.ToolTest
{
    /// <summary>
    /// Socket客户端测试类
    /// </summary>
    class SocketClientTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "SocketClient";

        /// <summary>
        /// 执行测试
        /// </summary>
        public override void ExecuteTest()
        {
            //测试时间
            int testTimeSecs = 60 * 60;

            SocketClient client = new SocketClient()
            {
                CP_HeartBeatTime = 5,
                CP_ReSendTimes = 5,
                CP_RestartTime = 5,
                CP_ConnectTime = 5,
                CP_IsAutoReConnect = true
            };

            client.CE_ReceiveMessage += Client_ReceiveMessage;
            client.CF_InitClient("127.0.0.1", 9696);
            client.CF_StartConnection();

            PrintLn(MsgType.INFO, $"等待连接服务器！");
            Thread.Sleep(2000);

            PrintLn(MsgType.INFO, $"测试时间{testTimeSecs}秒！");
            while (testTimeSecs-- > 0 && client.CP_IsConnected)
            {
                client.CF_SendMessage("Send To Server");
                Thread.Sleep(1000);
            }

            client.CF_StopConnection();
        }

        private void Client_ReceiveMessage(ModClientMessage msg)
        {
            switch (msg.MsgType)
            {
                case EMsgType.Infomation:
                    PrintLn(MsgType.INFO, $"{msg.Message}");
                    break;
                case EMsgType.System:
                    PrintLn(MsgType.WARN, $"{msg.Message}");
                    break;
                case EMsgType.Error:
                    PrintLn(MsgType.ERROR, $"{msg.Message}");
                    break;
            }
        }
    }
}
