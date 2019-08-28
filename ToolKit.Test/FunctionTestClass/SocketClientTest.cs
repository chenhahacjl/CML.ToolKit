using CML.SocketEx;
using System.Threading;

namespace ToolKit.Test
{
    /// <summary>
    /// Socket客户端测试类
    /// </summary>
    internal class SocketClientTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "SocketClient";

        /// <summary>
        /// 版本信息
        /// </summary>
        public override void CF_GetVersionInfo()
        {
            PrintMsgLn(MsgType.Success, "⊙日志信息⊙");
            PrintMsgLn(MsgType.Info, new VersionInfo().CF_GetVersionInfo());
        }

        /// <summary>
        /// 执行测试
        /// </summary>
        public override void ExecuteTest()
        {
            //测试时间
            int testTimeSecs = 60;

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

            PrintLogLn(MsgType.Info, $"等待连接服务器！");
            Thread.Sleep(2000);

            PrintLogLn(MsgType.Info, $"测试时间{testTimeSecs}秒！");
            while (testTimeSecs-- > 0)
            {
                if (client.CP_IsConnected)
                {
                    client.CF_SendMessage("Send To Server");
                }
                else
                {
                    PrintLogLn(MsgType.Error, $"未连接到服务端！");
                }

                Thread.Sleep(1000);
            }

            client.CF_StopConnection();
            Thread.Sleep(2000);
        }

        private void Client_ReceiveMessage(ModClientMessage msg)
        {
            switch (msg.MsgType)
            {
                case EMsgType.Infomation:
                    PrintLogLn(MsgType.Info, $"{msg.Message}");
                    break;
                case EMsgType.System:
                    PrintLogLn(MsgType.Warn, $"{msg.Message}");
                    break;
                case EMsgType.Error:
                    PrintLogLn(MsgType.Error, $"{msg.Message}");
                    break;
            }
        }
    }
}
