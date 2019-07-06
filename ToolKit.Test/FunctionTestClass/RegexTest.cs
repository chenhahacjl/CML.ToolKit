using CML.CommonEx.RegexEx;
using CML.CommonEx.RegexEx.ExFunction;

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
            PrintResult("9696", "9696".CF_IsInterger());
            PrintResult("-9696", "-9696".CF_IsInterger(ENumberVerifyType.Nagtive));
            PrintResult("-1.233", "-1.233".CF_IsFloat());
            PrintResult("-1.233", "-1.233".CF_IsFloat(ENumberVerifyType.Nagtive));
            PrintResult("-1.23456", "-1.23456".CF_IsDouble());
            PrintResult("-1.23456", "-1.23456".CF_IsDouble(ENumberVerifyType.NotPositive));
            PrintResult("2019/1/1", "2019/1/1".CF_IsDateTime());
            PrintResult("ABC@DEF.COM", "ABC@DEF.COM".CF_IsEmail());
            PrintResult("01086551122", "01086551122".CF_IsTelePhoneNumber());
            PrintResult("+8613900000000", "+8613900000000".CF_IsMobilePhoneNumber());
            PrintResult("01086551122", "01086551122".CF_IsPhoneNumber());
            PrintResult("310000", "310000".CF_IsZipCode());
            PrintResult("192.168.20.9", "192.168.20.9".CF_IsIPv4());
            PrintResult("fe80::3c14:32ce:2221:1305%14", "fe80::3c14:32ce:2221:1305%14".CF_IsIPv6());
            PrintResult("115.70800", "115.70800".CF_IsLongitude());
            PrintResult("41.54147", "41.54147".CF_IsLatitude());
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
                PrintLogLn(MsgType.Success, $"验证通过: {input}");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"验证失败: {input}");
            }
        }
    }
}
