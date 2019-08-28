using CML.CommonEx.ThreadEx;

namespace ToolKit.Test
{
    /// <summary>
    /// 线程操作测试类
    /// </summary>
    internal class ThreadTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "ThreadTest";

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
            PrintLogLn(MsgType.Info, "没有测试类");
        }
    }
}
