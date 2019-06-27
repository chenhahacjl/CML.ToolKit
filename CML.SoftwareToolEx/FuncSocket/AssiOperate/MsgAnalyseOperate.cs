using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CML.SocketEx
{
    /// <summary>
    /// 客户端消息解析类
    /// </summary>
    internal class MsgAnalyseOperate
    {
        /// <summary>
        /// 未处理消息
        /// </summary>
        private Dictionary<string, string> m_strUnProcessMsg = new Dictionary<string, string>();

        /// <summary>
        /// 交换信息上报委托
        /// </summary>
        /// <param name="swapMsg">客户端交换信息</param>
        public delegate void SwapMessageNotifyHandler(ModSwapMessage swapMsg);

        /// <summary>
        /// 交换信息上报事件
        /// </summary>
        public event SwapMessageNotifyHandler MessageNotify;

        /// <summary>
        /// 消息解析
        /// </summary>
        /// <param name="id">消息ID</param>
        /// <param name="message">消息内容</param>
        /// <param name="client">客户端（服务端使用时写）</param>
        public void AnalyseMsg(string id, string message, ModClient client)
        {
            //判断ID是否存在
            if (!m_strUnProcessMsg.ContainsKey(id))
            {
                m_strUnProcessMsg.Add(id, "");
            }

            //分解消息
            string[] msgs = (m_strUnProcessMsg[id] + message).Split(new string[] { "[_" }, StringSplitOptions.RemoveEmptyEntries);

            //清空未处理消息
            m_strUnProcessMsg[id] = "";

            for (int i = 0; i < msgs.Length; i++)
            {
                msgs[i] = "[_" + msgs[i];
            }

            for (int i = 0; i < msgs.Length; i++)
            {
                //不完整消息
                if (!msgs[i].EndsWith("_]"))
                {
                    //判断是否为最后一组
                    if (i == msgs.Length - 1)
                    {
                        //等下次再处理
                        m_strUnProcessMsg[id] = msgs[i];
                    }
                    else
                    {
                        MessageNotify?.Invoke(new ModSwapMessage(ESwapMsgType.BadMessage, msgs[i], client));
                    }

                    break;
                }

                //普通消息
                if (msgs[i].StartsWith(ISMessage.MsgNormalStart) && msgs[i].EndsWith(ISMessage.MsgNormalEnd))
                {
                    string revMsg = msgs[i].Substring(
                        ISMessage.MsgNormalStart.Length,
                        msgs[i].Length - ISMessage.MsgNormalStart.Length - ISMessage.MsgNormalEnd.Length);
                    MessageNotify?.Invoke(new ModSwapMessage(ESwapMsgType.NormalMessage, revMsg, client));

                    break;
                }

                //心跳包
                if (msgs[i] == ISCommand.CmdClientHB || msgs[i] == ISCommand.CmdServerHB)
                {
                    MessageNotify?.Invoke(new ModSwapMessage(ESwapMsgType.HeartBeat, null, client));

                    break;
                }

                //请求计算机名称
                if (msgs[i] == ISCommand.CmdGetPcName)
                {
                    string PCInfo = ISMessage.MsgPcNameStart + Environment.UserDomainName + "/" + Environment.UserName + ISMessage.MsgPcNameEnd;
                    MessageNotify?.Invoke(new ModSwapMessage(ESwapMsgType.ComputerName, PCInfo, client));

                    break;
                }
                //收到计算机名称
                if (msgs[i].StartsWith(ISMessage.MsgPcNameStart) && msgs[i].EndsWith(ISMessage.MsgPcNameEnd))
                {
                    string revMsg = msgs[i].Substring(
                        ISMessage.MsgPcNameStart.Length,
                        msgs[i].Length - ISMessage.MsgPcNameStart.Length - ISMessage.MsgPcNameEnd.Length);
                    MessageNotify?.Invoke(new ModSwapMessage(ESwapMsgType.ComputerName, revMsg, client));

                    break;
                }

                //强制下线命令
                if (msgs[i] == ISCommand.CmdClientRequShutdown)
                {
                    MessageNotify?.Invoke(new ModSwapMessage(ESwapMsgType.ShutdownCommand, null, client));

                    break;
                }
                //请求下线命令
                if (msgs[i] == ISCommand.CmdClientNeedShutdown)
                {
                    MessageNotify?.Invoke(new ModSwapMessage(ESwapMsgType.ShutdownCommand, null, client));

                    break;
                }

                MessageNotify?.Invoke(new ModSwapMessage(ESwapMsgType.UnknowMessage, msgs[i], client));
            }
        }
    }
}
