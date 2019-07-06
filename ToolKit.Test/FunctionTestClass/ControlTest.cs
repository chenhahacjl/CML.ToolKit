using CML.ControlEx;
using System.Windows.Forms;

namespace CML.ToolTest
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
            Application.EnableVisualStyles();
            _ = new FormShowControl().ShowDialog();
        }
    }
}
