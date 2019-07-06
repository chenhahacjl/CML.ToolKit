using CML.CommonEx.NetworkEx;
using CML.CommonEx.NetworkEx.ExFunction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace CML.ToolTest.FunctionTestClass
{
    /// <summary>
    /// 网络操作测试类
    /// </summary>
    internal class NetworkTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "NetworkTest";

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
            ModelWebRequest modelWebRequest = new ModelWebRequest()
            {
                RequestUrl = "https://www.baidu.com",
                TimeOut = 5000
            };

            string html = modelWebRequest.CF_GetHtmlCode(out string errMsg);
            if (string.IsNullOrEmpty(errMsg))
            {
                PrintMsgLn(MsgType.Info, html);
                PrintLogLn(MsgType.Success, $"HTML代码获取成功！");
            }
            else
            {
                PrintLogLn(MsgType.Success, $"HTML代码获取失败: {errMsg}");
            }
        }
    }
}
