using CML.ToolKit.EncodeEx;
using System.Reflection;

namespace CML.ToolKit.ToolTest
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
        /// 执行测试
        /// </summary>
        public override void ExecuteTest()
        {
            PrintLn(MsgType.Info, "开始DES加密解密测试！");
            DESEncryptTest();

            PrintLn(MsgType.Info, "开始MD5加密测试！");
            MD5EncodeTest();
        }

        /// <summary>
        /// DES加密解密测试
        /// </summary>
        private void DESEncryptTest()
        {
            string input = GetRandomString(10);
            PrintLn(MsgType.Info, $"待加密字符串: {input}");
            string key = GetRandomString(8);
            PrintLn(MsgType.Info, $"加密密钥: {key}");
            string iv = GetRandomString(8);
            PrintLn(MsgType.Info, $"加密向量: {iv}");

            string encode = DESEncrypt.CF_Encrypt(input, key, iv);
            if (encode.StartsWith("ERROR:"))
            {
                PrintLn(MsgType.Error, $"加密字符串失败: {encode.Substring(6)}");
            }
            else
            {
                PrintLn(MsgType.Success, $"加密字符串成功: {encode}");
            }

            string decode = DESEncrypt.CF_Decrypt(encode, key, iv);
            if (decode.StartsWith("ERROR:"))
            {
                PrintLn(MsgType.Error, $"解密字符串失败: {decode.Substring(6)}");
            }
            else
            {
                PrintLn(MsgType.Success, $"解密字符串成功: {decode}");
            }

            if (input == decode)
            {
                PrintLn(MsgType.Success, "原字符串与解密字符串比对成功！");
            }
            else
            {
                PrintLn(MsgType.Error, "原字符串与解密字符串比对失败！");
            }
        }

        /// <summary>
        /// MD5编码测试
        /// </summary>
        private void MD5EncodeTest()
        {
            PrintLn(MsgType.Info, "16位MD5（大写）: " + MD5Encrypt.MD5Encrypt16("MD5Tester"));
            PrintLn(MsgType.Info, "16位MD5（小写）: " + MD5Encrypt.MD5Encrypt16("MD5Tester", false));

            PrintLn(MsgType.Info, "32位MD5（大写）: " + MD5Encrypt.MD5Encrypt32("MD5Tester"));
            PrintLn(MsgType.Info, "32位MD5（小写）: " + MD5Encrypt.MD5Encrypt32("MD5Tester", false));

            PrintLn(MsgType.Info, "文件MD5（大写）: " + MD5Encrypt.MD5EncryptFile(Assembly.GetExecutingAssembly().Location));
            PrintLn(MsgType.Info, "文件MD5（小写）: " + MD5Encrypt.MD5EncryptFile(Assembly.GetExecutingAssembly().Location, false));
        }
    }
}
