using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CML.SocketEx
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum EMsgType
    {
        /// <summary>
        /// 接收消息
        /// </summary>
        Infomation,
        /// <summary>
        /// 系统消息
        /// </summary>
        System,
        /// <summary>
        /// 错误信息
        /// </summary>
        Error
    }
}
