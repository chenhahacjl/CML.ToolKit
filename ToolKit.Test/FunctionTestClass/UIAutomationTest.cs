using CML.CommonEx.UIAutomationEx;
using CML.CommonEx.UIAutomationEx.ExFunction;
using System.Diagnostics;
using System.Windows.Automation;

namespace CML.ToolTest
{
    /// <summary>
    /// UI自动化操作测试类
    /// </summary>
    internal class UIAutomationTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "UIAutomationTest";

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
            Process process = @"C:\Windows\Notepad.exe".CF_StartProgram();
            1.CF_DelaySecond();

            AutomationElement app = process.CF_GetElementByProcess();
            AutomationElement element = app.CF_GetElementByAutomationId("15");
            element.CF_SetTextContent(
                "Hello World!\n\n" +
                "H\n" +
                "   E\n" +
                "      L\n" +
                "         L\n" +
                "            O\n" +
                "            W\n" +
                "         O\n" +
                "      R\n" +
                "   L\n" +
                "D\n\n" +
                "                 ------Cmile_96"
            );

            2.CF_DelaySecond();
            process.Kill();
        }
    }
}
