using CML.CommonEx.ConfigurationEx;
using CML.CommonEx.SingletonEx;

namespace CML.ToolTest.FunctionTestClass
{
    /// <summary>
    /// 朋友游戏测试类
    /// </summary>
    internal class SingletonTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "SingletonTest";

        /// <summary>
        /// 版本信息
        /// </summary>
        public override void GetVersionInfo()
        {
            PrintMsgLn(MsgType.Success, "⊙日志信息⊙");
            PrintMsgLn(MsgType.Info, new CommonEx.SingletonEx.VersionInfo().GetVersionInfo());
        }

        /// <summary>
        /// 执行测试
        /// </summary>
        public override void ExecuteTest()
        {
            PrintLogLn(MsgType.Info, $"传参单例模式验证: ");

            string strRamdom = GetRandomString(10);
            PrintLogLn(MsgType.Info, $"传参单例模式验证字符串: {strRamdom}");

            SingletonBase<TestClass1>.CP_Instance.ID = strRamdom;

            string result = SingletonBase<TestClass1>.CP_Instance.ID;
            PrintLogLn(MsgType.Info, $"传参单例模式结果字符串: {result}");

            if (strRamdom == result)
            {
                PrintLogLn(MsgType.Success, $"传参单例模式验证成功！");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"传参单例模式验证失败！");
            }

            PrintLogLn(MsgType.Info, $"继承单例模式验证: ");

            strRamdom = GetRandomString(10);
            PrintLogLn(MsgType.Info, $"继承单例模式验证字符串: {strRamdom}");

            TestClass2.CP_Instance.ID = strRamdom;

            result = TestClass2.CP_Instance.ID;
            PrintLogLn(MsgType.Info, $"继承单例模式结果字符串: {result}");

            if (strRamdom == result)
            {
                PrintLogLn(MsgType.Success, $"继承单例模式验证成功！");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"继承单例模式验证失败！");
            }
        }
    }

    /// <summary>
    /// 测试类1
    /// </summary>
    public class TestClass1
    {
        public string ID { get; set; }
    }

    /// <summary>
    /// 测试类2
    /// </summary>
    public class TestClass2 : SingletonBase<TestClass2>
    {
        public string ID { get; set; }
    }
}
