using CML.CommonEx.EncodeEx;
using System;
using System.Reflection;

namespace CML.ToolTest
{
    /// <summary>
    /// 编码测试类
    /// </summary>
    internal class EncodeTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "EncodeTest";

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
            PrintLog(MsgType.Warn, "是否执行DES加密解密测试(Y/N):");
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
            {
                PrintMsgLn(MsgType.Warn, "Y");
                PrintLogLn(MsgType.Info, "开始DES加密解密测试！");
                DESEncryptTest();
            }
            else
            {
                PrintMsgLn(MsgType.Warn, "N");
            }

            PrintLog(MsgType.Warn, "是否执行MD5加密测试(Y/N):");
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
            {
                PrintMsgLn(MsgType.Warn, "Y");
                PrintLogLn(MsgType.Info, "开始MD5加密测试！");
                MD5EncodeTest();
            }
            else
            {
                PrintMsgLn(MsgType.Warn, "N");
            }
        }

        /// <summary>
        /// DES加密解密测试
        /// </summary>
        private void DESEncryptTest()
        {
            string input = GetRandomString(10);
            PrintLogLn(MsgType.Info, $"待加密字符串: {input}");
            string key = GetRandomString(8);
            PrintLogLn(MsgType.Info, $"加密密钥: {key}");
            string iv = GetRandomString(8);
            PrintLogLn(MsgType.Info, $"加密向量: {iv}");

            string encode = DESEncrypt.CF_Encrypt(input, key, iv);
            if (encode.StartsWith("ERROR:"))
            {
                PrintLogLn(MsgType.Error, $"加密字符串失败: {encode.Substring(6)}");
            }
            else
            {
                PrintLogLn(MsgType.Success, $"加密字符串成功: {encode}");
            }

            string decode = DESEncrypt.CF_Decrypt(encode, key, iv);
            if (decode.StartsWith("ERROR:"))
            {
                PrintLogLn(MsgType.Error, $"解密字符串失败: {decode.Substring(6)}");
            }
            else
            {
                PrintLogLn(MsgType.Success, $"解密字符串成功: {decode}");
            }

            if (input == decode)
            {
                PrintLogLn(MsgType.Success, "原字符串与解密字符串比对成功！");
            }
            else
            {
                PrintLogLn(MsgType.Error, "原字符串与解密字符串比对失败！");
            }
        }

        /// <summary>
        /// MD5编码测试
        /// </summary>
        private void MD5EncodeTest()
        {
            PrintLogLn(MsgType.Info, "16位MD5（大写）: " + MD5Encrypt.CF_MD5Encrypt16("MD5Tester"));
            PrintLogLn(MsgType.Info, "16位MD5（小写）: " + MD5Encrypt.CF_MD5Encrypt16("MD5Tester", false));

            PrintLogLn(MsgType.Info, "32位MD5（大写）: " + MD5Encrypt.CF_MD5Encrypt32("MD5Tester"));
            PrintLogLn(MsgType.Info, "32位MD5（小写）: " + MD5Encrypt.CF_MD5Encrypt32("MD5Tester", false));

            PrintLogLn(MsgType.Info, "文件MD5（大写）: " + MD5Encrypt.CF_MD5EncryptFile(Assembly.GetExecutingAssembly().Location));
            PrintLogLn(MsgType.Info, "文件MD5（小写）: " + MD5Encrypt.CF_MD5EncryptFile(Assembly.GetExecutingAssembly().Location, false));
        }
    }
}
