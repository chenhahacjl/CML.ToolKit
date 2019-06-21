using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace CML.ToolKit.SocketEx
{
    /// <summary>
    /// Socket服务端类
    /// </summary>
    public class SocketServer
    {
        #region 私有变量
        /// <summary>
        /// 服务端网络终端
        /// </summary>
        private IPEndPoint m_ipEndPoint = null;
        /// <summary>
        /// 服务端套接字
        /// </summary>
        private Socket m_socket = null;
        /// <summary>
        /// 服务重启间隔
        /// </summary>
        private int m_reStartSecs = 5;
        /// <summary>
        /// 心跳检测间隔时间
        /// </summary>
        private int m_heartBeatCheckSecs = 60;
        /// <summary>
        /// 发送失败重发次数
        /// </summary>
        private int m_reSendTimes = 3;
        /// <summary>
        /// 上次服务结束时间
        /// </summary>
        private DateTime m_lastCloseTime = ISDefault.DefDateTime;
        /// <summary>
        /// 客户端列表
        /// </summary>
        private readonly List<ModClient> m_clientList = new List<ModClient>();
        /// <summary>
        /// 线程停止标志
        /// </summary>
        private bool m_isThreadStop = true;
        /// <summary>
        /// 信息解析
        /// </summary>
        private readonly MsgAnalyseOperate m_analyseOperate = new MsgAnalyseOperate();
        #endregion

        #region 公共属性
        /// <summary>
        /// 服务状态
        /// </summary>
        public bool CP_IsServerOpen { get; private set; }

        /// <summary>
        /// 服务重启间隔[默认值:5|最小值:1|单位:秒]
        /// </summary>
        public int CP_RestartTime
        {
            get => m_reStartSecs;
            set => m_reStartSecs = value > 1 ? value : 1;
        }

        /// <summary>
        /// 发送失败重发次数[默认值:3|范围:0-10|单位:次]
        /// </summary>
        public int CP_ReSendTimes
        {
            get => m_reSendTimes;
            set => m_reSendTimes = value < 0 ? 0 : value > 10 ? 10 : value;
        }

        /// <summary>
        /// 是否开启心跳间隔检测
        /// </summary>
        public bool CP_IsOpenHeartBeatCheck { get; set; } = false;

        /// <summary>
        /// 心跳间隔检测时间[默认值:60|最小值:1|单位:秒]
        /// </summary>
        public int CP_HeartBeatCheckTime
        {
            get => m_heartBeatCheckSecs;
            set => m_heartBeatCheckSecs = value > 1 ? value : 1;
        }

        /// <summary>
        /// 客户端列表
        /// </summary>
        public ModClient[] CP_ClientList
        {
            get
            {
                ModClient[] clients = new ModClient[m_clientList.Count];
                m_clientList.CopyTo(0, clients, 0, clients.Length);
                return clients;
            }
        }
        #endregion

        #region 构造/析构函数
        /// <summary>
        /// 构造服务端对象
        /// </summary>
        public SocketServer()
        {
            m_analyseOperate.MessageNotify += MessageNotify;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        ~SocketServer()
        {
            CloseSocketConnect();
        }
        #endregion

        #region 委托、事件
        /// <summary>
        /// 获取消息委托
        /// </summary>
        /// <param name="message"></param>
        public delegate void ReceiveMessageHandle(ModServerMessage message);
        /// <summary>
        /// 获取消息事件
        /// </summary>
        public event ReceiveMessageHandle CE_ReceiveMessage;
        #endregion

        #region 公共方法
        /// <summary>
        /// 初始化服务
        /// </summary>
        /// <param name="ip">服务端IP地址</param>
        /// <param name="port">服务占用端口</param>
        /// <returns>执行结果</returns>
        public bool CF_InitServer(string ip, int port)
        {
            if (!IPAddress.TryParse(ip, out IPAddress address))
            {
                CE_ReceiveMessage?.Invoke(new ModServerMessage(null, EMsgType.Error, "IP格式错误"));
                return false;
            }
            if (port < 0 || port > 65535)
            {
                CE_ReceiveMessage?.Invoke(new ModServerMessage(null, EMsgType.Error, "端口范围错误"));
                return false;
            }

            m_ipEndPoint = new IPEndPoint(address, port);

            CE_ReceiveMessage?.Invoke(new ModServerMessage(null, EMsgType.System, "服务初始化成功"));
            return true;
        }

        /// <summary>
        /// 开启服务
        /// </summary>
        /// <returns>执行结果</returns>
        public bool CF_StartService()
        {
            //判断服务是否打开
            if (CP_IsServerOpen)
            {
                CE_ReceiveMessage?.Invoke(new ModServerMessage(null, EMsgType.Error, "服务已运行"));
                return false;
            }

            //是否第一次启动
            if (m_lastCloseTime != ISDefault.DefDateTime)
            {
                //释放资源
                CloseSocketConnect();

                //是否第一次启动
                while (m_lastCloseTime.AddSeconds(m_reStartSecs) > DateTime.Now)
                {
                    int waitTime = (int)(m_lastCloseTime.AddSeconds(m_reStartSecs) - DateTime.Now).TotalSeconds + 1;

                    CE_ReceiveMessage?.Invoke(new ModServerMessage(null, EMsgType.System, $"服务重启倒数: {waitTime}秒"));

                    Thread.Sleep(1000);
                }
            }

            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                m_socket.Bind(m_ipEndPoint);
                CP_IsServerOpen = true;
            }
            catch (Exception ex)
            {
                CE_ReceiveMessage?.Invoke(new ModServerMessage(null, EMsgType.Error, "服务启动失败: " + ex.Message));
                return false;
            }

            m_socket.Listen(30);
            CE_ReceiveMessage?.Invoke(new ModServerMessage(null, EMsgType.System, "服务打开成功"));

            m_isThreadStop = false;
            ThreadPool.QueueUserWorkItem(new WaitCallback(MonitorThread), m_socket);
            CE_ReceiveMessage?.Invoke(new ModServerMessage(null, EMsgType.System, "开启监听服务"));

            new Thread(HeartBeatThread) { IsBackground = true }.Start();
            CE_ReceiveMessage?.Invoke(new ModServerMessage(null, EMsgType.System, "成功开启心跳监测线程"));

            return true;
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <returns>执行结果</returns>
        public bool CF_StopService()
        {
            if (!CP_IsServerOpen && m_isThreadStop)
            {
                CE_ReceiveMessage?.Invoke(new ModServerMessage(null, EMsgType.Error, "服务未开启"));
                return false;
            }

            CE_ReceiveMessage?.Invoke(new ModServerMessage(null, EMsgType.System, "关闭服务端连接"));

            int count = 5;
            while (count-- > 0 && m_clientList.Count != 0)
            {
                //发送客户端关闭连接指令
                for (int i = m_clientList.Count - 1; i >= 0; i--)
                {
                    ExitClient(m_clientList[i]);
                }

                Thread.Sleep(1000);
            }

            //通知线程停止
            m_isThreadStop = true;

            //关闭Socket连接
            CloseSocketConnect();

            return true;
        }

        /// <summary>
        /// 群体发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <returns>执行结果</returns>
        public bool CF_SendMessage(string message)
        {
            string strErrMsg = string.Empty;

            foreach (ModClient client in CP_ClientList)
            {
                ModResult<string> result = SendMsg(client.Socket, message, m_reSendTimes);
                if (!result.IsSuccess)
                {
                    strErrMsg += $"向{client.Name}{result.Result}";
                }
            }

            if (string.IsNullOrEmpty(strErrMsg))
            {
                CE_ReceiveMessage?.Invoke(new ModServerMessage(null, EMsgType.System, "消息群发成功: " + message));
                return true;
            }
            else
            {
                CE_ReceiveMessage?.Invoke(new ModServerMessage(null, EMsgType.Error, strErrMsg));
                return false;
            }
        }

        /// <summary>
        /// 单独发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="guid">客户端ID</param>
        /// <returns>执行结果</returns>
        public bool CF_SendMessage(string message, Guid guid)
        {
            ModResult<string> result = SendMsg(m_clientList.Find(e => e.GUID == guid).Socket, message, m_reSendTimes);

            if (result.IsSuccess)
            {
                CE_ReceiveMessage?.Invoke(new ModServerMessage(null, EMsgType.System, "消息发送成功"));
                return true;
            }
            else
            {
                CE_ReceiveMessage?.Invoke(new ModServerMessage(null, EMsgType.Error, result.Result));
                return false;
            }
        }
        #endregion

        #region 线程方法
        private void MonitorThread(object socket)
        {
            Socket socketServer = socket as Socket;

            while (true)
            {
                //是否接收到停止标志
                if (m_isThreadStop) { break; }

                try
                {
                    Socket proxSocket = null;

                    try
                    {
                        //创建新连接
                        proxSocket = socketServer.Accept();
                    }
                    catch { continue; }

                    if (SendMsg(proxSocket, ISCommand.CmdGetPcName, m_reSendTimes, true).IsSuccess)
                    {
                        ModClient client = new ModClient(proxSocket);
                        client.SetHBTime();
                        m_clientList.Add(client);
                        CE_ReceiveMessage?.Invoke(new ModServerMessage(null, EMsgType.System, $"<{client.GUID}>新客户端连接"));

                        ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveMsg), client);
                    }
                }
                catch { }
            }

            //记录关闭信息
            CP_IsServerOpen = false;
            m_lastCloseTime = DateTime.Now;

            CE_ReceiveMessage?.Invoke(new ModServerMessage(null, EMsgType.System, "关闭监听服务"));
        }

        /// <summary>
        /// 心跳包监控
        /// </summary>
        private void HeartBeatThread()
        {
            while (true)
            {
                //是否接收到停止标志
                if (m_isThreadStop) { break; }
                //是否失去连接
                if (!CP_IsServerOpen) { break; }

                //是否开启心跳检测
                if (CP_IsOpenHeartBeatCheck)
                {
                    //循环判断心跳是否超时
                    try
                    {
                        for (int i = m_clientList.Count - 1; i >= 0; i--)
                        {
                            if (m_clientList[i].CheckHBTimeOut(CP_HeartBeatCheckTime))
                            {
                                CE_ReceiveMessage?.Invoke(new ModServerMessage(m_clientList[i], EMsgType.Error, "心跳检测超时，关闭连接"));
                                ExitClient(m_clientList[i]);
                                m_clientList.RemoveAt(i);
                            }
                        }
                    }
                    catch { }
                }

                Thread.Sleep(1000);
            }

            CE_ReceiveMessage?.Invoke(new ModServerMessage(null, EMsgType.System, "退出心跳监测线程"));
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
        /// <param name="socket">套接字</param>
        /// <param name="message">消息内容</param>
        /// <param name="reSend">重发次数</param>
        /// <param name="innerMsg">内部消息标志</param>
        /// <returns></returns>
        private ModResult<string> SendMsg(Socket socket, string message, int reSend, bool innerMsg = false)
        {
            //Socket是否为空
            if (socket == null) { return new ModResult<string>(false, "Socket为空"); }
            //是否启动服务器
            if (!CP_IsServerOpen) { return new ModResult<string>(false, "服务器未启动"); }

            string sendMsg = message;

            if (!innerMsg)
            {
                sendMsg = ISMessage.MsgNormalStart + message + ISMessage.MsgNormalEnd;
            }

            byte[] sendData = Encoding.UTF8.GetBytes(sendMsg);

            ModResult<string> result = new ModResult<string>();
            try
            {
                if (socket.Send(sendData) != sendData.Length)
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
                result = SendMsg(socket, message, reSend, innerMsg);
            }

            return result;
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="obj"></param>
        private void ReceiveMsg(object obj)
        {
            ModClient client = (ModClient)obj;
            byte[] data = new byte[1024 * 1024];

            while (true)
            {
                if (!CP_IsServerOpen) break;

                int dataLength;
                try
                {
                    dataLength = client.Socket.Receive(data, 0, data.Length, SocketFlags.None);
                }
                catch
                {
                    //客户端异常退出
                    m_clientList.Remove(client);
                    CE_ReceiveMessage?.Invoke(new ModServerMessage(client, EMsgType.Error, "客户端异常退出"));
                    break;
                }

                if (dataLength <= 0) { return; }

                string message = Encoding.UTF8.GetString(data, 0, dataLength);

                m_analyseOperate.AnalyseMsg(client.GUID.ToString(), message, client);
            }
        }

        /// <summary>
        /// 关闭客户端连接
        /// </summary>
        /// <param name="client">客户端</param>
        /// <returns>执行结果</returns>
        private void ExitClient(ModClient client)
        {
            try
            {
                SendMsg(client.Socket, ISCommand.CmdClientRequShutdown, m_reSendTimes, true);
                CE_ReceiveMessage?.Invoke(new ModServerMessage(client, EMsgType.System, "发送强制下线命令"));

            }
            catch { }
        }

        /// <summary>
        /// 交换信息上报
        /// </summary>
        /// <param name="swapMsg">交换消息</param>
        private void MessageNotify(ModSwapMessage swapMsg)
        {
            switch (swapMsg.SwapMsgType)
            {
                case ESwapMsgType.BadMessage:
                {
                    CE_ReceiveMessage?.Invoke(new ModServerMessage(swapMsg.Client, EMsgType.Error, $"收到异常消息: {swapMsg.SwapMsg}"));

                    break;
                }
                case ESwapMsgType.UnknowMessage:
                {
                    CE_ReceiveMessage?.Invoke(new ModServerMessage(swapMsg.Client, EMsgType.Error, $"收到未知消息: { swapMsg.SwapMsgType}|{swapMsg.SwapMsg}"));

                    break;
                }
                case ESwapMsgType.NormalMessage:
                {
                    CE_ReceiveMessage?.Invoke(new ModServerMessage(swapMsg.Client, EMsgType.Infomation, $"收到客户端消息: {swapMsg.SwapMsg}"));

                    break;
                }
                case ESwapMsgType.HeartBeat:
                {
                    CE_ReceiveMessage?.Invoke(new ModServerMessage(swapMsg.Client, EMsgType.System, "收到心跳包"));
                    if (SendMsg(swapMsg.Client.Socket, ISCommand.CmdServerHB, m_reSendTimes, true).IsSuccess)
                    {
                        CE_ReceiveMessage?.Invoke(new ModServerMessage(swapMsg.Client, EMsgType.System, "回应心跳包成功"));
                        swapMsg.Client.SetHBTime();
                    }
                    else
                    {
                        CE_ReceiveMessage?.Invoke(new ModServerMessage(swapMsg.Client, EMsgType.Error, "回应心跳包失败"));
                        m_clientList.Remove(swapMsg.Client);
                    }

                    break;
                }
                case ESwapMsgType.ComputerName:
                {
                    CE_ReceiveMessage?.Invoke(new ModServerMessage(null, EMsgType.System, $"<{swapMsg.Client.GUID}>获得客户端名称: {swapMsg.SwapMsg}"));

                    break;
                }
                case ESwapMsgType.ShutdownCommand:
                {
                    CE_ReceiveMessage?.Invoke(new ModServerMessage(swapMsg.Client, EMsgType.System, "收到客户端下线命令"));
                    m_clientList.Remove(swapMsg.Client);
                    CE_ReceiveMessage?.Invoke(new ModServerMessage(swapMsg.Client, EMsgType.System, "客户端下线成功"));

                    break;
                }
            }
        }
        #endregion
    }
}
