using CML.ToolKit.ControlEx;
using System.Windows.Forms;

namespace CML.ToolKit.ToolTest
{
    /// <summary>
    /// 控件测试类
    /// </summary>
    internal class ControlTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "ControlTest";

        /// <summary>
        /// 执行测试
        /// </summary>
        public override void ExecuteTest()
        {
            Application.EnableVisualStyles();
            _ = new FormShowControl().ShowDialog();
        }
    }
}
