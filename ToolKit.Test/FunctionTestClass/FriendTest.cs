using CML.EntertainmentEx.FriendEx;

namespace ToolKit.Test
{
    /// <summary>
    /// 朋友游戏测试类
    /// </summary>
    internal class FriendTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "FriendTest";

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
