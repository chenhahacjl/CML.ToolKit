using CML.CommonEx.VersionEx;
using System;

namespace CML.ToolTest.FunctionTestClass
{
    internal class VersionTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "VersionTest";

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
            PrintMsgLn(MsgType.Info, new TestVersion().CF_GetVersionInfo());
        }

        /// <summary>
        /// 版本控制测试类
        /// </summary>
        public class TestVersion : VersionBase
        {
            #region 版本信息
            /// <summary>
            /// 主版本号
            /// </summary>
            public override string CP_VerMain => "9.6";
            /// <summary>
            /// 研发版本号
            /// </summary>
            public override string CP_VerDev => "96Y001R001";
            /// <summary>
            /// 更新时间
            /// </summary>
            public override string CP_VerDate => DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            #endregion
        }
    }
}
