using CML.CommonEx.RegexEx;
using System.ComponentModel;

namespace CML.ToolTest
{
    /// <summary>
    /// 正则表达式测试类
    /// </summary>
    internal class RegexTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "RegexTest";

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
            PrintResult("-1.23456", "-1.23456".CF_IsDouble());
            PrintResult("ABC@DEF.COM", "ABC@DEF.COM".CF_IsEmail());
            PrintResult("192.168.20.9", "192.168.20.9".CF_IsIPv4());
        }

        /// <summary>
        /// 打印结果
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="result">结果</param>
        private void PrintResult(string input, bool result)
        {
            if (result)
            {
                PrintLogLn(MsgType.Info, $"验证通过: {input}");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"验证失败: {input}");
            }
        }
    }
}
