using CML.CommonEx.NetworkEx;
using CML.CommonEx.NetworkEx.ExFunction;

namespace ToolKit.Test
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
            ModWebRequest modelWebRequest = new ModWebRequest()
            {
                RequestUrl = "https://www.baidu.com",
                TimeOut = 5000,
            };

            string html = modelWebRequest.CF_GetHtmlCode(out string errMsg);
            if (string.IsNullOrEmpty(errMsg))
            {
                PrintLogLn(MsgType.Success, $"HTML代码获取成功: {html.Length}");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"HTML代码获取失败: {errMsg}");
            }

            modelWebRequest = new ModWebRequest()
            {
                RequestUrl = "http://192.168.40.161:8001/TEST.txt",
                TimeOut = 5000,
            };

            bool rlt = modelWebRequest.CF_DownloadFile(@"E:\Test_1.txt", out errMsg);
            if (rlt)
            {
                PrintLogLn(MsgType.Success, $"文件下载成功！");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"文件下载失败: {errMsg}");
            }

            modelWebRequest = new ModWebRequest()
            {
                RequestUrl = "http://localhost:64489/SaveForm.aspx",
                TimeOut = 5000
            };
            modelWebRequest.Headers.Add("SAVEPATH", @"\");
            modelWebRequest.Headers.Add("FILENAME", @"HELP.txt");

            rlt = modelWebRequest.CF_UploadFile(
                @"E:\TEST_2.txt",
                out string retMsg,
                out errMsg
            );
            if (rlt)
            {
                PrintLogLn(MsgType.Success, $"文件上传成功: {retMsg.Substring(0, retMsg.IndexOf('\r'))}");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"文件上传失败: {errMsg}");
            }
        }
    }
}
