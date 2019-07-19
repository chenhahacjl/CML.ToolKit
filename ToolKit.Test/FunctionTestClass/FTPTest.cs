using CML.CommonEx.FTPEx;
using CML.CommonEx.FTPEx.ExFunction;

namespace CML.ToolTest.FunctionTestClass
{
    /// <summary>
    /// FTP测试类
    /// </summary>
    internal class FTPTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "FTPTest";

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
            ModelFtpInfomation ftpInfomation = new ModelFtpInfomation("192.168.20.15")
            {
                Port = 9695,
                FtpReqInfo = new ModelFtpReqInfo()
                {
                    TransmissionSpeed = new ModelTransmissionSpeed()
                    {
                        Speed = 3,
                        Unit = ESpeedUnit.MB,
                        Delay = 950,
                    }
                },
                FtpExtendInfo = new ModelFtpExtendInfo()
                {

                },
            };

            bool result = ftpInfomation.CF_UploadFile("1.rar", @"1.rar", true, out string errMsg);
            if (result)
            {
                PrintLogLn(MsgType.Success, "执行成功: " + result);
            }
            else
            {
                PrintLogLn(MsgType.Error, $"执行失败: {errMsg}");
            }
        }
    }
}
