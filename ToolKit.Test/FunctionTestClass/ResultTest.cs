using CML.CommonEx.ResultEx;
using System;
using System.ComponentModel;

namespace ToolKit.Test
{
    /// <summary>
    /// 身份证号操作测试类
    /// </summary>
    internal class ResultTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "ResultTest";

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
            TResult<string> result = new TResult<string>("Test String 1");
            if (!result)
            {
                PrintLogLn(MsgType.Error, result.ToString());
            }
            else
            {
                PrintLogLn(MsgType.Success, result.Result);
            }

            result = new TResult<string>(null, TestEnum.TestItem);
            if (!result)
            {
                PrintLogLn(MsgType.Error, result.ToString());
            }
            else
            {
                PrintLogLn(MsgType.Success, result.Result);
            }

            try { int zero = 0; zero /= zero; }
            catch (Exception ex) { result = new TResult<string>(null, ex); }
            if (!result)
            {
                PrintLogLn(MsgType.Error, result.ToString());
            }
            else
            {
                PrintLogLn(MsgType.Success, result.Result);
            }

            result = new TResult<string>(null, "Error String 1");
            if (!result)
            {
                PrintLogLn(MsgType.Error, result.ToString());
            }
            else
            {
                PrintLogLn(MsgType.Success, result.Result);
            }

            result = new TResult<string>("Test String 3", 0, null);
            if (!result)
            {
                PrintLogLn(MsgType.Error, result.ToString());
            }
            else
            {
                PrintLogLn(MsgType.Success, result.Result);
            }
        }

        private enum TestEnum
        {
            [Description("Enum Test String")]
            TestItem = 9696,
        }
    }
}
