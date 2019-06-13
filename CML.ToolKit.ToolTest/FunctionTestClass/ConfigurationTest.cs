using CML.ToolKit.ConfigurationEx;
using System;
using System.IO;

namespace CML.ToolKit.ToolTest
{
    /// <summary>
    /// 配置操作测试类
    /// </summary>
    internal class ConfigurationTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "ConfigurationTest";

        /// <summary>
        /// 执行测试
        /// </summary>
        public override void ExecuteTest()
        {
            PrintLog(MsgType.Warn, "是否执行INI文件操作测试(Y/N):");
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
            {
                PrintMsgLn(MsgType.Warn, "Y");
                PrintLogLn(MsgType.Info, "开始INI文件操作测试！");
                IniFileOperateTest();
            }
            else
            {
                PrintMsgLn(MsgType.Warn, "N");
            }

            PrintLog(MsgType.Warn, "是否执行注册表操作测试(Y/N):");
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
            {
                PrintMsgLn(MsgType.Warn, "Y");
                PrintLogLn(MsgType.Info, "开始注册表操作测试！");
                try
                {
                    RegeditOperateTest();
                }
                catch (Exception ex)
                {
                    PrintLogLn(MsgType.Error, ex.Message);
                }
            }
            else
            {
                PrintMsgLn(MsgType.Warn, "N");
            }
        }

        /// <summary>
        /// INI文件操作测试
        /// </summary>
        private void IniFileOperateTest()
        {
            string iniPath = Path.Combine(Environment.CurrentDirectory, "TestConfig.ini");

            PrintLogLn(MsgType.Info, $"INI文件路径: {iniPath}");

            PrintLogLn(MsgType.Warn, "静态方法测试！");

            string value0 = GetRandomString(10);
            PrintLogLn(MsgType.Info, $"生成随机字符串: {value0}");

            IniOperate.CF_WriteConfig(iniPath, "TestSection", "StringKey", value0.ToString());
            PrintLogLn(MsgType.Info, $"写入配置: TestSection->StringKey -> {value0}");

            string result0 = IniOperate.CF_ReadConfig(iniPath, "TestSection", "StringKey");
            PrintLogLn(MsgType.Info, $"读取配置: TestSection->StringKey -> {result0}");

            if (value0 == result0)
            {
                PrintLogLn(MsgType.Success, $"写入值与读取值比对成功！");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"写入值与读取值比对失败！");
            }

            double value1 = Math.Round(new Random().NextDouble(), 10);
            PrintLogLn(MsgType.Info, $"生成随机小数: {value1}");

            IniOperate.CF_WriteConfig<double>(iniPath, "TestSection", "DoubleKey", value1);
            PrintLogLn(MsgType.Info, $"写入配置: TestSection->DoubleKey -> {value1}");

            double result1 = IniOperate.CF_ReadConfig<double>(iniPath, "TestSection", "DoubleKey");
            PrintLogLn(MsgType.Info, $"读取配置: TestSection->DoubleKey -> {result1}");

            if (value1 == result1)
            {
                PrintLogLn(MsgType.Success, $"写入值与读取值比对成功！");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"写入值与读取值比对失败！");
            }

            ETest value2 = (ETest)new Random().Next(1, 6);
            PrintLogLn(MsgType.Info, $"生成随机枚举值: {value2}");

            IniOperate.CF_WriteConfig<ETest>(iniPath, "TestSection", "EnumKey", value2);
            PrintLogLn(MsgType.Info, $"写入配置: TestSection->EnumKey -> {value2}");

            ETest result2 = IniOperate.CF_ReadConfig<ETest>(iniPath, "TestSection", "EnumKey");
            PrintLogLn(MsgType.Info, $"读取配置: TestSection->EnumKey -> {result2}");

            if (value2 == result2)
            {
                PrintLogLn(MsgType.Success, $"写入值与读取值比对成功！");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"写入值与读取值比对失败！");
            }

            PrintLogLn(MsgType.Warn, "动态方法测试！");

            IniOperate iniOperate = new IniOperate(iniPath);

            value0 = GetRandomString(10);
            PrintLogLn(MsgType.Info, $"生成随机字符串: {value0}");

            iniOperate.CF_WriteConfig("TestSection", "StringKey", value0.ToString());
            PrintLogLn(MsgType.Info, $"写入配置: TestSection->StringKey -> {value0}");

            result0 = iniOperate.CF_ReadConfig("TestSection", "StringKey");
            PrintLogLn(MsgType.Info, $"读取配置: TestSection->StringKey -> {result0}");

            if (value0 == result0)
            {
                PrintLogLn(MsgType.Success, $"写入值与读取值比对成功！");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"写入值与读取值比对失败！");
            }

            value1 = Math.Round(new Random().NextDouble(), 10);
            PrintLogLn(MsgType.Info, $"生成随机小数: {value1}");

            iniOperate.CF_WriteConfig("TestSection", "DoubleKey", value1);
            PrintLogLn(MsgType.Info, $"写入配置: TestSection->DoubleKey -> {value1}");

            result1 = iniOperate.CF_ReadConfig<double>("TestSection", "DoubleKey");
            PrintLogLn(MsgType.Info, $"读取配置: TestSection->DoubleKey -> {result1}");

            if (value1 == result1)
            {
                PrintLogLn(MsgType.Success, $"写入值与读取值比对成功！");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"写入值与读取值比对失败！");
            }

            value2 = (ETest)new Random().Next(1, 6);
            PrintLogLn(MsgType.Info, $"生成随机枚举值: {value2}");

            iniOperate.CF_WriteConfig("TestSection", "EnumKey", value2);
            PrintLogLn(MsgType.Info, $"写入配置: TestSection->EnumKey -> {value2}");

            result2 = iniOperate.CF_ReadConfig<ETest>("TestSection", "EnumKey");
            PrintLogLn(MsgType.Info, $"读取配置: TestSection->EnumKey -> {result2}");

            if (value2 == result2)
            {
                PrintLogLn(MsgType.Success, $"写入值与读取值比对成功！");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"写入值与读取值比对失败！");
            }

            File.Delete(iniPath);
        }

        /// <summary>
        /// 注册表操作测试
        /// </summary>
        private void RegeditOperateTest()
        {
            PrintLogLn(MsgType.Info, "检查注册表项存在: HKEY_LOCAL_MACHINE\\SOFTWARE\\ItemTester\\");
            if (!RegOperate.CF_ExistSubItem(ERegDomain.LocalMachine, "SOFTWARE\\ItemTester\\"))
            {
                PrintLogLn(MsgType.Info, "不存在注册表项: HKEY_LOCAL_MACHINE\\SOFTWARE\\ItemTester\\");

                if (RegOperate.CF_CreateSubItem(ERegDomain.LocalMachine, "SOFTWARE\\ItemTester\\"))
                {
                    PrintLogLn(MsgType.Success, "创建注册表项成功: HKEY_LOCAL_MACHINE\\SOFTWARE\\ItemTester\\");
                }
                else
                {
                    PrintLogLn(MsgType.Error, "创建注册表项失败: HKEY_LOCAL_MACHINE\\SOFTWARE\\ItemTester\\");
                }
            }
            else
            {
                PrintLogLn(MsgType.Info, "存在注册表项: HKEY_LOCAL_MACHINE\\SOFTWARE\\ItemTester\\");
            }

            PrintLogLn(MsgType.Info, "检查注册表键值名称存在: HKEY_LOCAL_MACHINE\\SOFTWARE\\ItemTester\\RegTester");
            if (!RegOperate.CF_ExistRegeditKey(ERegDomain.LocalMachine, "SOFTWARE\\ItemTester\\", "RegTester"))
            {
                PrintLogLn(MsgType.Info, "不存在注册表键值名称: HKEY_LOCAL_MACHINE\\SOFTWARE\\ItemTester\\RegTester");
            }
            else
            {
                PrintLogLn(MsgType.Info, "存在注册表键值名称: HKEY_LOCAL_MACHINE\\SOFTWARE\\ItemTester\\");
            }

            string value = GetRandomString(10);
            PrintLogLn(MsgType.Info, $"生成随机字符串: {value}");

            if (RegOperate.CF_WriteRegeditKey(ERegDomain.LocalMachine, "SOFTWARE\\ItemTester\\", "RegTester", ERegValueKind.String, value))
            {
                PrintLogLn(MsgType.Success, $"写入注册表键值成功: HKEY_LOCAL_MACHINE\\SOFTWARE\\ItemTester\\RegTester -> {value}");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"写入注册表键值失败: HKEY_LOCAL_MACHINE\\SOFTWARE\\ItemTester\\RegTester -> {value}");
            }

            string result = RegOperate.CF_ReadRegeditKey(ERegDomain.LocalMachine, "SOFTWARE\\ItemTester\\", "RegTester") as string;
            PrintLogLn(MsgType.Info, $"读取注册表键值: HKEY_LOCAL_MACHINE\\SOFTWARE\\ItemTester\\RegTester -> {result}");

            if (value == result)
            {
                PrintLogLn(MsgType.Success, $"写入值与读取值比对成功！");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"写入值与读取值比对失败！");
            }

            if (RegOperate.CF_DeleteRegeditKey(ERegDomain.LocalMachine, "SOFTWARE\\ItemTester\\", "RegTester"))
            {
                PrintLogLn(MsgType.Success, "删除注册表键值成功: HKEY_LOCAL_MACHINE\\SOFTWARE\\ItemTester\\RegTester");
            }
            else
            {
                PrintLogLn(MsgType.Error, "删除注册表键值失败: HKEY_LOCAL_MACHINE\\SOFTWARE\\ItemTester\\RegTester");
            }

            if (RegOperate.CF_DeleteSubItem(ERegDomain.LocalMachine, "SOFTWARE\\ItemTester\\"))
            {
                PrintLogLn(MsgType.Success, "删除注册表项成功: HKEY_LOCAL_MACHINE\\SOFTWARE\\ItemTester\\");
            }
            else
            {
                PrintLogLn(MsgType.Error, "删除注册表项失败: HKEY_LOCAL_MACHINE\\SOFTWARE\\ItemTester\\");
            }
        }

        /// <summary>
        /// 测试枚举
        /// </summary>
        private enum ETest { One, Two, Three, Four, Five }
    }
}
