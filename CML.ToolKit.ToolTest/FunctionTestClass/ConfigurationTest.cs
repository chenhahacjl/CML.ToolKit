using CML.ToolKit.ConfigurationEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CML.ToolKit.ToolTest
{
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
            IniFileOperateTest();
        }

        /// <summary>
        /// INI文件测试
        /// </summary>
        private void IniFileOperateTest()
        {
            PrintLn(MsgType.Info, "开始INI文件操作测试！");

            string iniPath = Path.Combine(Environment.CurrentDirectory, "TestConfig.ini");
            PrintLn(MsgType.Info, $"INI文件路径: {iniPath}");

            PrintLn(MsgType.Warn, "静态方法测试！");

            string value0 = GetRandomString(10, true, true, true, true, "");
            PrintLn(MsgType.Info, $"生成随机字符串: {value0}");

            IniOperate.CF_WriteConfig(iniPath, "TestSection", "StringKey", value0.ToString());
            PrintLn(MsgType.Info, $"写入配置: TestSection->StringKey->{value0}");

            string result0 = IniOperate.CF_ReadConfig(iniPath, "TestSection", "StringKey");
            PrintLn(MsgType.Info, $"读取配置: TestSection->StringKey->{result0}");

            if (value0 == result0)
            {
                PrintLn(MsgType.Success, $"写入值与读取值比对成功！");
            }
            else
            {
                PrintLn(MsgType.Error, $"写入值与读取值比对失败！");
            }

            double value1 = Math.Round(new Random().NextDouble(), 10);
            PrintLn(MsgType.Info, $"生成随机小数: {value1}");

            IniOperate.CF_WriteConfig<double>(iniPath, "TestSection", "DoubleKey", value1);
            PrintLn(MsgType.Info, $"写入配置: TestSection->DoubleKey->{value1}");

            double result1 = IniOperate.CF_ReadConfig<double>(iniPath, "TestSection", "DoubleKey");
            PrintLn(MsgType.Info, $"读取配置: TestSection->DoubleKey->{result1}");

            if (value1 == result1)
            {
                PrintLn(MsgType.Success, $"写入值与读取值比对成功！");
            }
            else
            {
                PrintLn(MsgType.Error, $"写入值与读取值比对失败！");
            }

            ETest value2 = (ETest)new Random().Next(1, 6);
            PrintLn(MsgType.Info, $"生成随机枚举值: {value2}");

            IniOperate.CF_WriteConfig<ETest>(iniPath, "TestSection", "EnumKey", value2);
            PrintLn(MsgType.Info, $"写入配置: TestSection->EnumKey->{value2}");

            ETest result2 = IniOperate.CF_ReadConfig<ETest>(iniPath, "TestSection", "EnumKey");
            PrintLn(MsgType.Info, $"读取配置: TestSection->EnumKey->{result2}");

            if (value2 == result2)
            {
                PrintLn(MsgType.Success, $"写入值与读取值比对成功！");
            }
            else
            {
                PrintLn(MsgType.Error, $"写入值与读取值比对失败！");
            }

            PrintLn(MsgType.Warn, "动态方法测试！");

            IniOperate iniOperate = new IniOperate(iniPath);

            value0 = GetRandomString(10, true, true, true, true, "");
            PrintLn(MsgType.Info, $"生成随机字符串: {value0}");

            iniOperate.CF_WriteConfig("TestSection", "StringKey", value0.ToString());
            PrintLn(MsgType.Info, $"写入配置: TestSection->StringKey->{value0}");

            result0 = iniOperate.CF_ReadConfig("TestSection", "StringKey");
            PrintLn(MsgType.Info, $"读取配置: TestSection->StringKey->{result0}");

            if (value0 == result0)
            {
                PrintLn(MsgType.Success, $"写入值与读取值比对成功！");
            }
            else
            {
                PrintLn(MsgType.Error, $"写入值与读取值比对失败！");
            }

            value1 = Math.Round(new Random().NextDouble(), 10);
            PrintLn(MsgType.Info, $"生成随机小数: {value1}");

            iniOperate.CF_WriteConfig("TestSection", "DoubleKey", value1);
            PrintLn(MsgType.Info, $"写入配置: TestSection->DoubleKey->{value1}");

            result1 = iniOperate.CF_ReadConfig<double>("TestSection", "DoubleKey");
            PrintLn(MsgType.Info, $"读取配置: TestSection->DoubleKey->{result1}");

            if (value1 == result1)
            {
                PrintLn(MsgType.Success, $"写入值与读取值比对成功！");
            }
            else
            {
                PrintLn(MsgType.Error, $"写入值与读取值比对失败！");
            }

            value2 = (ETest)new Random().Next(1, 6);
            PrintLn(MsgType.Info, $"生成随机枚举值: {value2}");

            iniOperate.CF_WriteConfig("TestSection", "EnumKey", value2);
            PrintLn(MsgType.Info, $"写入配置: TestSection->EnumKey->{value2}");

            result2 = iniOperate.CF_ReadConfig<ETest>("TestSection", "EnumKey");
            PrintLn(MsgType.Info, $"读取配置: TestSection->EnumKey->{result2}");

            if (value2 == result2)
            {
                PrintLn(MsgType.Success, $"写入值与读取值比对成功！");
            }
            else
            {
                PrintLn(MsgType.Error, $"写入值与读取值比对失败！");
            }
        }

        ///<summary>
        ///生成随机字符串 
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<param name="useNum">是否包含数字</param>
        ///<param name="useLow">是否包含小写字母</param>
        ///<param name="useUpp">是否包含大写字母</param>
        ///<param name="useSpe">是否包含特殊字符</param>
        ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        ///<returns>指定长度的随机字符串</returns>
        public string GetRandomString(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));

            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }

            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }

            return s;
        }

        /// <summary>
        /// 测试枚举
        /// </summary>
        private enum ETest { One, Two, Three, Four, Five }
    }
}
