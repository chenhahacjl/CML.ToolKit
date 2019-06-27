using CML.CommonEx.EnumEx;
using System.ComponentModel;

namespace CML.ToolTest
{
    internal class EnumTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "EnumTest";

        /// <summary>
        /// 版本信息
        /// </summary>
        public override void GetVersionInfo()
        {
            PrintMsgLn(MsgType.Success, "⊙日志信息⊙");
            PrintMsgLn(MsgType.Info, new VersionInfo().GetVersionInfo());
        }

        /// <summary>
        /// 执行测试
        /// </summary>
        public override void ExecuteTest()
        {
            PrintLogLn(MsgType.Info, "枚举描述信息: " + EnumOperate.CF_GetDescription(ETest.EnumTestItem));
        }

        private enum ETest
        {
            [Description("枚举测试")]
            EnumTestItem
        }
    }
}
