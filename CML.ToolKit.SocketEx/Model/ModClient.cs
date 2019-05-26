using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace CML.ToolKit.SocketEx
{
    /// <summary>
    /// 客户端模型
    /// </summary>
    public class ModClient
    {
        /// <summary>
        /// 上次心跳时间
        /// </summary>
        private DateTime m_dtLastHBTime = DateTime.Now;

        /// <summary>
        /// 客户端ID
        /// </summary>
        public Guid GUID { get; }

        /// <summary>
        /// 套接字
        /// </summary>
        public Socket Socket { get; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// 构造客户端对象
        /// </summary>
        /// <param name="socket">套接字</param>
        public ModClient(Socket socket)
        {
            GUID = Guid.NewGuid();
            Socket = socket;
        }

        /// <summary>
        /// 设置心跳时间
        /// </summary>
        internal void SetHBTime()
        {
            m_dtLastHBTime = DateTime.Now;
        }

        /// <summary>
        /// 检查心跳是否超时
        /// </summary>
        /// <param name="timeOut">超时时间</param>
        /// <returns>是否超时</returns>
        internal bool CheckHBTimeOut(int timeOut)
        {
            return (DateTime.Now - m_dtLastHBTime).TotalSeconds > timeOut;
        }
    }
}
