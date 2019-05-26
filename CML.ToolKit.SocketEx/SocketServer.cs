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
        private int m_restartSecs = 5;
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
        #endregion

        #region 公共属性
        /// <summary>
        /// 服务状态
        /// </summary>
        public bool IsServerOpen { get; private set; }

        /// <summary>
        /// 服务重启间隔[默认值:5|最小值:1|单位:秒]
        /// </summary>
        public int RestartTime
        {
            get => m_restartSecs;
            set => m_restartSecs = value > 1 ? value : 1;
        }

        /// <summary>
        /// 发送失败重发次数[默认值:3|范围:0-10|单位:次]
        /// </summary>
        public int ReSendTimes
        {
            get => m_reSendTimes;
            set => m_reSendTimes = value < 0 ? 0 : value > 10 ? 10 : value;
        }

        /// <summary>
        /// 客户端列表
        /// </summary>
        public ModClient[] ClientList
        {
            get
            {
                ModClient[] clients = new ModClient[m_clientList.Count];
                m_clientList.CopyTo(0, clients, 0, clients.Length);
                return clients;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造服务端对象
        /// </summary>
        public SocketServer() { }

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
        public delegate void ReceiveMessageHandle(ModMessage message);
        /// <summary>
        /// 获取消息事件
        /// </summary>
        public event ReceiveMessageHandle ReceiveMessage;
        #endregion

        #region 公共方法
        /// <summary>
        /// 初始化服务
        /// </summary>
        /// <param name="ip">服务端IP地址</param>
        /// <param name="port">服务占用端口</param>
        /// <returns>执行结果</returns>
        public bool InitServer(string ip, int port)
        {
            if (!IPAddress.TryParse(ip, out IPAddress address))
            {
                ReceiveMessage?.Invoke(new ModMessage(null, EMsgType.Error, "IP格式错误"));
                return false;
            }
            if (port < 0 || port > 65535)
            {
                ReceiveMessage?.Invoke(new ModMessage(null, EMsgType.Error, "端口范围错误"));
                return false;
            }

            m_ipEndPoint = new IPEndPoint(address, port);

            ReceiveMessage?.Invoke(new ModMessage(null, EMsgType.Infomation, "服务初始化成功"));
            return true;
        }

        /// <summary>
        /// 开启服务
        /// </summary>
        /// <returns>执行结果</returns>
        public bool StartServer()
        {
            //判断服务是否打开
            if (IsServerOpen)
            {
                ReceiveMessage?.Invoke(new ModMessage(null, EMsgType.Error, "服务已运行"));
                return false;
            }

            //释放资源
            CloseSocketConnect();

            //是否第一次启动
            if (m_lastCloseTime != ISDefault.DefDateTime)
            {
                while (m_lastCloseTime.AddSeconds(m_restartSecs) > DateTime.Now)
                {
                    int waitTime = (int)(m_lastCloseTime.AddSeconds(m_restartSecs) - DateTime.Now).TotalSeconds + 1;

                    ReceiveMessage?.Invoke(new ModMessage(null, EMsgType.System, $"服务重启倒数: {waitTime}秒"));

                    Thread.Sleep(1000);
                }
            }

            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                m_socket.Bind(m_ipEndPoint);
                IsServerOpen = true;
            }
            catch (Exception ex)
            {
                ReceiveMessage?.Invoke(new ModMessage(null, EMsgType.Error, "服务启动失败: " + ex.Message));
                return false;
            }

            m_socket.Listen(30);
            ReceiveMessage?.Invoke(new ModMessage(null, EMsgType.Infomation, "服务打开成功"));

            ThreadPool.QueueUserWorkItem(new WaitCallback(this.AcceptClientConnect), m_socket);
            ReceiveMessage?.Invoke(new ModMessage(null, EMsgType.Infomation, "开启监听服务"));

            return true;
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <returns>执行结果</returns>
        public bool StopServer()
        {
            if (!IsServerOpen)
            {
                ReceiveMessage?.Invoke(new ModMessage(null, EMsgType.Error, "服务未开启"));
                return false;
            }

            //关闭Socket连接
            CloseSocketConnect();

            //记录关闭信息
            IsServerOpen = false;
            m_lastCloseTime = DateTime.Now;

            ReceiveMessage?.Invoke(new ModMessage(null, EMsgType.Infomation, "服务关闭成功"));
            return true;
        }

        /// <summary>
        /// 群体发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <returns>执行结果</returns>
        public bool SendMessage(string message)
        {
            string strErrMsg = string.Empty;

            foreach (ModClient client in ClientList)
            {
                ModResult<string> result = SendMsg(client.Socket, message, m_reSendTimes);
                if (!result.IsSuccess)
                {
                    strErrMsg += $"向{client.Name}{result.Result}";
                }
            }

            if (string.IsNullOrEmpty(strErrMsg))
            {
                ReceiveMessage?.Invoke(new ModMessage(null, EMsgType.Infomation, "消息群发成功"));
                return true;
            }
            else
            {
                ReceiveMessage?.Invoke(new ModMessage(null, EMsgType.Error, strErrMsg));
                return false;
            }
        }

        /// <summary>
        /// 单独发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="guid">客户端ID</param>
        /// <returns>执行结果</returns>
        public bool SendMessage(string message, Guid guid)
        {
            ModResult<string> result = SendMsg(m_clientList.Find(e => e.GUID == guid).Socket, message, m_reSendTimes);

            if (result.IsSuccess)
            {
                ReceiveMessage?.Invoke(new ModMessage(null, EMsgType.Infomation, "消息发送成功"));
                return true;
            }
            else
            {
                ReceiveMessage?.Invoke(new ModMessage(null, EMsgType.Error, result.Result));
                return false;
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 接受用户端连接
        /// </summary>
        /// <param name="socket">套接字</param>
        private void AcceptClientConnect(object socket)
        {
            Socket socketServer = socket as Socket;

            while (true)
            {
                if (!IsServerOpen) break;

                try
                {
                    Socket proxSocket = null;

                    try
                    {
                        proxSocket = socketServer.Accept();
                    }
                    catch { continue; }

                    if (SendMsg(proxSocket, ISCommand.CmdGetPcName, m_reSendTimes, true).IsSuccess)
                    {
                        ModClient client = new ModClient(proxSocket);
                        m_clientList.Add(client);
                        ReceiveMessage?.Invoke(new ModMessage(null, EMsgType.Infomation, $"<{client.GUID}>新客户端连接"));

                        ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveMsg), client);
                    }
                }
                catch { }
            }
        }

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
            if (socket == null) { return new ModResult<string>(false, "Socket为空"); }

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
                if (!IsServerOpen) break;

                int dataLength;
                try
                {
                    dataLength = client.Socket.Receive(data, 0, data.Length, SocketFlags.None);
                }
                catch
                {
                    //客户端异常退出
                    ReceiveMessage?.Invoke(new ModMessage(client, EMsgType.Error, "客户端异常退出"));
                    return;
                }

                if (dataLength <= 0) { return; }

                string message = Encoding.UTF8.GetString(data, 0, dataLength);

                if (!AnalyseMsg(client, message))
                    break;
            }
        }

        /// <summary>
        /// 解析消息内容
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="message">消息内容</param>
        private bool AnalyseMsg(ModClient client, String message)
        {
            //心跳包
            if (message.Contains(ISCommand.CmdClientHB))
            {
                ReceiveMessage?.Invoke(new ModMessage(client, EMsgType.Infomation, "收到心跳包"));

                if (SendMsg(client.Socket, ISCommand.CmdServerHB, m_reSendTimes, true).IsSuccess)
                {
                    ReceiveMessage?.Invoke(new ModMessage(client, EMsgType.Infomation, "回应心跳包成功"));
                    client.SetHBTime();
                }
                else
                {
                    ReceiveMessage?.Invoke(new ModMessage(client, EMsgType.Error, "回应心跳包失败"));
                    m_clientList.Remove(client);
                    return false;
                }
            }

            //计算机名称
            if (message.Contains(ISMessage.MsgPcNameStart) && message.Contains(ISMessage.MsgPcNameEnd))
            {
                Regex regex = new Regex(ISRegex.RegMsgPcName);
                Match match = regex.Match(message);
                if (match.Success)
                {
                    ReceiveMessage?.Invoke(new ModMessage(null, EMsgType.Infomation, $"<{client.GUID}>获得客户端名称: {match.Groups[1].Value}"));
                    client.Name = match.Groups[1].Value;
                }

                message = message.Replace(match.Groups[0].Value, "");
            }

            //消息
            if (message.Contains(ISMessage.MsgNormalStart) && message.Contains(ISMessage.MsgNormalEnd))
            {
                Regex regex = new Regex(ISRegex.RegMsgNormal);
                Match match = regex.Match(message);

                while (match.Success)
                {
                    ReceiveMessage?.Invoke(new ModMessage(client, EMsgType.Infomation, $"收到客户端消息: {match.Groups[1].Value}"));
                    match = match.NextMatch();
                }
            }

            return true;
        }
        #endregion
    }
}
