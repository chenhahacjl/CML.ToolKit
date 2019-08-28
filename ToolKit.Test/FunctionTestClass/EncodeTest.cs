using CML.CommonEx.EncodeEx;
using CML.CommonEx.EncodeEx.ExFunction;
using System;
using System.Reflection;
using System.Text;

namespace ToolKit.Test
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

            PrintLog(MsgType.Warn, "是否执行3DES加密解密测试(Y/N):");
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
            {
                PrintMsgLn(MsgType.Warn, "Y");
                PrintLogLn(MsgType.Info, "开始3DES加密解密测试！");
                DESTripleEncryptTest();
            }
            else
            {
                PrintMsgLn(MsgType.Warn, "N");
            }

            PrintLog(MsgType.Warn, "是否执行AES加密解密测试(Y/N):");
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
            {
                PrintMsgLn(MsgType.Warn, "Y");
                PrintLogLn(MsgType.Info, "开始AES加密解密测试！");
                AESEncryptTest();
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
            ModDESParameter desParameter = new ModDESParameter()
            {
                Key = GetRandomString(10),
                IV = GetRandomString(6),
                Encode = Encoding.ASCII,
                PaddingChar = 'X',
            };

            string input = GetRandomString(10);
            PrintLogLn(MsgType.Info, $"待加密字符串: {input}");
            PrintLogLn(MsgType.Info, $"加密密钥: {desParameter.Key}");
            PrintLogLn(MsgType.Info, $"加密向量: {desParameter.IV}");

            if (desParameter.CF_EncryptString(input, out string encode, out string errMsg))
            {
                PrintLogLn(MsgType.Success, $"加密字符串成功: {encode}");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"加密字符串失败: {errMsg}");
            }

            if (desParameter.CF_DecryptString(encode, out string decode, out errMsg))
            {
                PrintLogLn(MsgType.Success, $"解密字符串成功: {decode}");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"解密字符串失败: {errMsg}");
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
        /// 3DES加密解密测试
        /// </summary>
        private void DESTripleEncryptTest()
        {
            ModDESTripleParameter desParameter = new ModDESTripleParameter()
            {
                Key = GetRandomString(26),
                IV = GetRandomString(6),
                Encode = Encoding.ASCII,
                PaddingChar = 'X',
            };

            string input = GetRandomString(10);
            PrintLogLn(MsgType.Info, $"待加密字符串: {input}");
            PrintLogLn(MsgType.Info, $"加密密钥: {desParameter.Key}");
            PrintLogLn(MsgType.Info, $"加密向量: {desParameter.IV}");

            if (desParameter.CF_EncryptString(input, out string encode, out string errMsg))
            {
                PrintLogLn(MsgType.Success, $"加密字符串成功: {encode}");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"加密字符串失败: {errMsg}");
            }

            if (desParameter.CF_DecryptString(encode, out string decode, out errMsg))
            {
                PrintLogLn(MsgType.Success, $"解密字符串成功: {decode}");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"解密字符串失败: {errMsg}");
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
        /// AES加密解密测试
        /// </summary>
        private void AESEncryptTest()
        {
            ModAESParameter aesParameter = new ModAESParameter()
            {
                Key = GetRandomString(18),
                IV = GetRandomString(14),
                Encode = Encoding.UTF8,
                PaddingChar = 'a',
            };

            string input = GetRandomString(10);
            PrintLogLn(MsgType.Info, $"待加密字符串: {input}");
            PrintLogLn(MsgType.Info, $"加密密钥: {aesParameter.Key}");
            PrintLogLn(MsgType.Info, $"加密向量: {aesParameter.IV}");

            if (aesParameter.CF_EncryptString(input, out string encode, out string errMsg))
            {
                PrintLogLn(MsgType.Success, $"加密字符串成功: {encode}");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"加密字符串失败: {errMsg}");
            }

            if (aesParameter.CF_DecryptString(encode, out string decode, out errMsg))
            {
                PrintLogLn(MsgType.Success, $"解密字符串成功: {decode}");
            }
            else
            {
                PrintLogLn(MsgType.Error, $"解密字符串失败: {errMsg}");
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
            ModMD5Parameter md5Parameter = new ModMD5Parameter()
            {
                IsUppercase = true,
                Encode = Encoding.UTF8,
                MD5Length = EMD5Length.L32,
            };

            if (md5Parameter.CF_EncryptString("MD5Tester", out string encode, out string errMsg))
            {
                PrintLogLn(MsgType.Success, "加密字符串成功: " + encode);
            }
            else
            {
                PrintLogLn(MsgType.Error, "加密字符串失败: " + errMsg);
            }

            if (md5Parameter.CF_EncryptFile(Assembly.GetExecutingAssembly().Location, out  encode, out  errMsg))
            {
                PrintLogLn(MsgType.Success, "加密文件成功: " + encode);
            }
            else
            {
                PrintLogLn(MsgType.Error, "加密文件失败: " + errMsg);
            }
        }
    }
}
