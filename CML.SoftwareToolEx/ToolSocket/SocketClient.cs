using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace CML.SocketEx
{
    /// <summary>
    /// Socket客户端
    /// </summary>
    public class SocketClient
    {
        #region 私有变量
        /// <summary>
        /// 服务端网络终端
        /// </summary>
        private IPEndPoint m_ipEndPoint = null;
        /// <summary>
        /// 客户端套接字
        /// </summary>
        private Socket m_socket = null;
        /// <summary>
        /// 重连间隔时间
        /// </summary>
        private int m_reStartSecs = 5;
        /// <summary>
        /// 连接间隔时间
        /// </summary>
        private int m_connectSecs = 5;
        /// <summary>
        /// 心跳间隔时间
        /// </summary>
        private int m_heartBeatSecs = 60;
        /// <summary>
        /// 心跳开始跳过次数
        /// </summary>
        private int m_heartBeatSkipTimes = 5;
        /// <summary>
        /// 发送失败重发次数
        /// </summary>
        private int m_reSendTimes = 3;
        /// <summary>
        /// 上次连接结束时间
        /// </summary>
        private DateTime m_lastCloseTime = ISDefault.DefDateTime;
        /// <summary>
        /// 发送心跳时间
        /// </summary>
        private DateTime m_hbSendTime = ISDefault.DefDateTime;
        /// <summary>
        /// 接收心跳时间
        /// </summary>
        private DateTime m_hbReceiveTime = ISDefault.DefDateTime;
        /// <summary>
        /// 通讯线程
        /// </summary>
        private Thread m_thread = null;
        /// <summary>
        /// 线程停止标志
        /// </summary>
        private bool m_isThreadStop = true;
        /// <summary>
        /// 正在连接标志
        /// </summary>
        private bool m_isConnectiong = false;
        /// <summary>
        /// 解析ID
        /// </summary>
        private string m_analyseID = "";
        /// <summary>
        /// 信息解析
        /// </summary>
        private readonly MsgAnalyseOperate m_analyseOperate = new MsgAnalyseOperate();
        #endregion

        #region 公共属性
        /// <summary>
        /// 是否已经连接
        /// </summary>
        public bool CP_IsConnected { get; private set; }

        /// <summary>
        /// 是否自动重连接
        /// </summary>
        public bool CP_IsAutoReConnect { get; set; } = true;

        /// <summary>
        /// 重连间隔时间[默认值:5|最小值:1|单位:秒]
        /// </summary>
        public int CP_RestartTime
        {
            get => m_reStartSecs;
            set => m_reStartSecs = value > 1 ? value : 1;
        }

        /// <summary>
        /// 连接间隔时间(≥重连间隔时间)[默认值:5|最小值:1|单位:秒]
        /// </summary>
        public int CP_ConnectTime
        {
            get => m_connectSecs;
            set => m_connectSecs = value < m_reStartSecs ? value : value > 1 ? value : 1;
        }

        /// <summary>
        /// 心跳间隔时间[默认值:60|最小值:1|单位:秒]
        /// </summary>
        public int CP_HeartBeatTime
        {
            get => m_heartBeatSecs;
            set => m_heartBeatSecs = value > 1 ? value : 1;
        }

        /// <summary>
        /// 心跳开始跳过次数[默认值:5|最小值:1|单位:秒]
        /// </summary>
        public int CP_HeartBeatSkipTimes
        {
            get => m_heartBeatSkipTimes;
            set => m_heartBeatSkipTimes = value > 1 ? value : 1;
        }

        /// <summary>
        /// 发送失败重发次数[默认值:3|范围:0-10|单位:次]
        /// </summary>
        public int CP_ReSendTimes
        {
            get => m_reSendTimes;
            set => m_reSendTimes = value < 0 ? 0 : value > 10 ? 10 : value;
        }
        #endregion

        #region 构造/析构函数
        /// <summary>
        /// 构造服务端对象
        /// </summary>
        public SocketClient()
        {
            m_analyseOperate.MessageNotify += MessageNotify;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        ~SocketClient()
        {
            if (CP_IsConnected)
            {
                //发送下线命令
                SendMsg(ISCommand.CmdClientNeedShutdown, m_reSendTimes, true);
            }

            //通知线程停止
            m_isThreadStop = true;

            CloseSocketConnect();
        }
        #endregion

        #region 委托、事件
        /// <summary>
        /// 获取消息委托
        /// </summary>
        /// <param name="message"></param>
        public delegate void ReceiveMessageHandle(ModClientMessage message);
        /// <summary>
        /// 获取消息事件
        /// </summary>
        public event ReceiveMessageHandle CE_ReceiveMessage;
        #endregion

        #region 公共方法
        /// <summary>
        /// 初始化客户端
        /// </summary>
        /// <param name="ip">服务端IP地址</param>
        /// <param name="port">服务占用端口</param>
        /// <returns>执行结果</returns>
        public bool CF_InitClient(string ip, int port)
        {
            if (!IPAddress.TryParse(ip, out IPAddress address))
            {
                CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.Error, "IP格式错误"));
                return false;
            }
            if (port < 0 || port > 65535)
            {
                CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.Error, "端口范围错误"));
                return false;
            }

            m_analyseID = Guid.NewGuid().ToString();
            m_ipEndPoint = new IPEndPoint(address, port);

            CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.System, "客户端初始化成功"));
            return true;
        }

        /// <summary>
        /// 打开连接
        /// </summary>
        /// <returns>执行结果</returns>
        public bool CF_StartConnection()
        {
            //判断服务是否打开
            if (CP_IsConnected)
            {
                CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.Error, "已连接到服务器"));
                return false;
            }

            //是否正在连接
            if (m_isConnectiong)
            {
                CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.Error, "正在连接到服务器"));
                return false;
            }

            m_isThreadStop = false;
            m_thread = new Thread(ConnectThread) { IsBackground = true };
            m_thread.Start();

            CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.System, "开启连接线程"));
            return true;
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <returns>执行结果</returns>
        public bool CF_StopConnection()
        {
            if (!CP_IsConnected && !m_isConnectiong && m_isThreadStop)
            {
                CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.Error, "未连接到服务器"));
                return false;
            }

            //发送下线命令
            SendMsg(ISCommand.CmdClientNeedShutdown, m_reSendTimes, true);

            //通知线程停止
            m_isThreadStop = true;

            //关闭Socket连接
            CloseSocketConnect();

            return true;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <returns>执行结果</returns>
        public bool CF_SendMessage(string message)
        {
            ModResult<string> result = SendMsg(message, m_reSendTimes);

            if (result.IsSuccess)
            {
                CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.System, "消息发送成功"));
                return true;
            }
            else
            {
                CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.Error, result.Result));
                return false;
            }
        }
        #endregion

        #region 线程方法
        /// <summary>
        /// 服务器连接线程
        /// </summary>
        private void ConnectThread()
        {
            while (true)
            {
                //是否接收到停止标志
                if (m_isThreadStop) { break; }

                //是否第一次启动
                if (m_lastCloseTime != ISDefault.DefDateTime)
                {
                    //释放资源
                    CloseSocketConnect();

                    //等待重启
                    while (m_lastCloseTime.AddSeconds(m_reStartSecs) > DateTime.Now)
                    {
                        int waitTime = (int)(m_lastCloseTime.AddSeconds(m_reStartSecs) - DateTime.Now).TotalSeconds + 1;

                        CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.System, $"服务重启倒数: {waitTime}秒"));

                        Thread.Sleep(1000);
                    }
                }

                //开始连接服务器
                m_isConnectiong = true;
                m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    //连接服务器
                    m_socket.Connect(m_ipEndPoint);

                    CP_IsConnected = true;
                    CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.System, "成功连接到服务端"));

                    //消息接收线程
                    new Thread(ReceiveThread) { IsBackground = true }.Start();
                    CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.System, "成功开启消息接收线程"));

                    //心跳线程
                    new Thread(HeartBeatThread) { IsBackground = true }.Start();
                    CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.System, "成功开启心跳线程"));

                    m_isConnectiong = false;
                }
                catch
                {
                    CP_IsConnected = false;
                    m_isConnectiong = false;

                    CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.System, "服务端连接失败"));
                }

                //等待失去连接
                while (CP_IsConnected) { Thread.Sleep(1000); }

                //自动重连
                if (CP_IsAutoReConnect)
                {
                    for (int i = 0; i < m_connectSecs; i++)
                    {
                        //是否接收到停止标志
                        if (m_isThreadStop)
                        {
                            break;
                        }

                        //每次等待1秒
                        CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.System, $"等待{m_connectSecs - i}秒重新连接"));
                        Thread.Sleep(1000);
                    }
                }
                else { break; }
            }

            CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.System, "已关闭客户端连接"));
        }

        /// <summary>
        /// 消息接收线程
        /// </summary>
        private void ReceiveThread()
        {
            while (true)
            {
                //是否接收到停止标志
                if (m_isThreadStop) { break; }
                //是否失去连接
                if (!CP_IsConnected) { break; }

                try
                {
                    byte[] receiveData = new byte[1024 * 1024];
                    int dataLength = m_socket.Receive(receiveData);

                    if (dataLength > 0)
                    {
                        string message = Encoding.UTF8.GetString(receiveData, 0, dataLength);
                        m_analyseOperate.AnalyseMsg(m_analyseID, message, null);
                    }
                }
                catch
                {
                    if (CP_IsConnected)
                    {
                        CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.Error, "与服务端失去连接"));
                    }

                    break;
                }
            }

            //记录关闭信息
            m_lastCloseTime = DateTime.Now;

            //设置标志位
            CP_IsConnected = false;
            m_isConnectiong = false;

            CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.System, "退出消息接收线程"));
        }

        /// <summary>
        /// 心跳线程
        /// </summary>
        private void HeartBeatThread()
        {
            //跳过次数
            int count = m_heartBeatSkipTimes;

            while (true)
            {
                //是否接收到停止标志
                if (m_isThreadStop) { break; }
                //是否失去连接
                if (!CP_IsConnected) { break; }

                if (CP_IsConnected && SendMsg(ISCommand.CmdClientHB, m_reSendTimes, true).IsSuccess)
                {
                    m_hbSendTime = DateTime.Now;

                    CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.System, "成功发送心跳包"));
                }
                else
                {
                    CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.Error, "心跳包发送失败"));
                    break;
                }

                int times = m_heartBeatSecs;
                while (times-- > 0)
                {
                    //是否接收到停止标志
                    if (m_isThreadStop) { break; }
                    //是否失去连接
                    if (!CP_IsConnected) { break; }

                    Thread.Sleep(1000);
                }

                if (--count < 0)
                {
                    if ((m_hbReceiveTime >= m_hbSendTime && (m_hbReceiveTime - m_hbSendTime).TotalSeconds > m_heartBeatSecs) ||
                        m_hbReceiveTime < m_hbSendTime)
                    {
                        CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.Error, "未收到服务端心跳回复"));
                        break;
                    }
                }
                else
                {
                    CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.System, "跳过心跳检测"));
                }
            }

            //设置标志位
            CP_IsConnected = false;
            m_isConnectiong = false;

            CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.System, "退出心跳线程"));
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 关闭套接字连接
        /// </summary>
        /// <returns>执行结果</returns>
        private ModResult<string> CloseSocketConnect()
        {
            if (m_socket != null)
            {
                try
                {
                    if (m_socket.Connected)
                    {
                        m_socket.Shutdown(SocketShutdown.Both);
                    }

                    m_socket.Close();
                    m_socket.Dispose();
                    m_socket = null;
                }
                catch (Exception ex)
                {
                    return new ModResult<string>(false, "连接关闭失败: " + ex.Message);
                }
            }

            return new ModResult<string>();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="reSend">重发次数</param>
        /// <param name="innerMsg">内部消息标志</param>
        /// <returns></returns>
        private ModResult<string> SendMsg(string message, int reSend, bool innerMsg = false)
        {
            //是否连接服务器
            if (!CP_IsConnected) { return new ModResult<string>(false, "未连接到服务器"); }
            //Socket是否为空
            if (m_socket == null) { return new ModResult<string>(false, "Socket为空"); }

            string sendMsg = message;

            if (!innerMsg)
            {
                sendMsg = ISMessage.MsgNormalStart + message + ISMessage.MsgNormalEnd;
            }

            byte[] sendData = Encoding.UTF8.GetBytes(sendMsg);

            ModResult<string> result = new ModResult<string>();
            try
            {
                if (m_socket.Send(sendData) != sendData.Length)
                {
                    result = new ModResult<string>(false, "消息发送失败");
                }
            }
            catch (Exception ex)
            {
                result = new ModResult<string>(false, "消息发送错误: " + ex.Message);
            }

            if (!result.IsSuccess && --reSend > 0)
            {
                result = SendMsg(message, reSend, innerMsg);
            }

            return result;
        }

        /// <summary>
        /// 客户端交换信息上报
        /// </summary>
        /// <param name="swapMsg">客户端交换消息</param>
        private void MessageNotify(ModSwapMessage swapMsg)
        {
            switch (swapMsg.SwapMsgType)
            {
                case ESwapMsgType.BadMessage:
                {
                    CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.Error, $"收到异常消息: {swapMsg.SwapMsg}"));

                    break;
                }
                case ESwapMsgType.UnknowMessage:
                {
                    CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.Error, $"收到未知消息: {swapMsg.SwapMsgType}|{swapMsg.SwapMsg}"));

                    break;
                }
                case ESwapMsgType.NormalMessage:
                {
                    CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.Infomation, $"收到服务端消息: {swapMsg.SwapMsg}"));

                    break;
                }
                case ESwapMsgType.HeartBeat:
                {
                    m_hbReceiveTime = DateTime.Now;
                    CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.System, "收到心跳包"));

                    break;
                }
                case ESwapMsgType.ComputerName:
                {
                    if (SendMsg(swapMsg.SwapMsg, CP_ReSendTimes, true).IsSuccess)
                    {
                        CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.System, "回复本机名称成功"));
                    }
                    else
                    {
                        CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.System, "回复本机名称失败"));
                    }

                    break;
                }
                case ESwapMsgType.ShutdownCommand:
                {
                    CE_ReceiveMessage?.Invoke(new ModClientMessage(EMsgType.System, "收到服务端强制下线命令"));
                    CF_StopConnection();

                    break;
                }
            }
        }
        #endregion
    }
}
