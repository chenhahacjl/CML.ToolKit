using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CML.ToolKit.SocketEx
{
    public struct ModClientMessage
    {
        #region 公共属性
        /// <summary>
        /// 消息类型
        /// </summary>
        public EMsgType MsgType { get; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 空消息标记
        /// </summary>
        public bool IsEmptyMsg { get => Message == ISDefault.DefMessage; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造Socket信息对象
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="message">消息内容</param>
        public ModClientMessage(EMsgType msgType, string message)
        {
            MsgType = msgType;
            Message = string.IsNullOrEmpty(message) ? ISDefault.DefMessage : message;
        }
        #endregion
    }
}
