using CML.ToolKit.ControlEx;

namespace CML.ToolKit.ToolTest
{
    internal class ControlTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "ControlTest";

        /// <summary>
        /// 执行测试
        /// </summary>
        public override void ExecuteTest() => new FormShowControl().Show();
    }
}
