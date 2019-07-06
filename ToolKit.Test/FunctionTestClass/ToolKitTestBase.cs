using System;
using System.Security.Cryptography;

namespace CML.ToolTest
{
    /// <summary>
    /// 测试类基类
    /// 【请重载 TestClassName 属性】
    /// 【请重载 ExecuteTest() 方法】
    /// 【请重载 VersionInfo() 方法】
    /// </summary>
    internal class ToolKitTestBase
    {
        /// <summary>
        /// 测试类名【请重载此属性】
        /// </summary>
        public virtual string TestClassName => $"请重载 ToolKitTestBase 类 TestClassName 属性";

        /// <summary>
        /// 版本信息【请重载此方法】
        /// </summary>
        public virtual void CF_GetVersionInfo()
        {
            PrintLogLn(MsgType.Error, $"请重载 ToolKitTestBase 类 VersionInfo() 方法");
        }

        /// <summary>
        /// 执行测试【请重载此方法】
        /// </summary>
        public virtual void ExecuteTest()
        {
            PrintLogLn(MsgType.Error, $"请重载 ToolKitTestBase 类 ExecuteTest() 方法");
        }

        /// <summary>
        /// 控制台打印消息
        /// </summary>
        /// <param name="type">消息类型</param>
        /// <param name="msg">消息内容</param>
        protected void PrintLog(MsgType type, string msg)
        {
            Console.ForegroundColor =
                type == MsgType.Info ? ConsoleColor.White :
                type == MsgType.Success ? ConsoleColor.Green :
                type == MsgType.Warn ? ConsoleColor.Yellow :
                ConsoleColor.Red;
            Console.Write($"[{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}]{msg}");
            Console.ResetColor();
        }

        /// <summary>
        /// 控制台打印消息（带换行）
        /// </summary>
        /// <param name="type">消息类型</param>
        /// <param name="msg">消息内容</param>
        protected void PrintLogLn(MsgType type, string msg)
        {
            Console.ForegroundColor =
                type == MsgType.Info ? ConsoleColor.White :
                type == MsgType.Success ? ConsoleColor.Green :
                type == MsgType.Warn ? ConsoleColor.Yellow :
                ConsoleColor.Red;
            Console.WriteLine($"[{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}]{msg}");
            Console.ResetColor();
        }

        /// <summary>
        /// 控制台打印消息
        /// </summary>
        /// <param name="type">消息类型</param>
        /// <param name="msg">消息内容</param>
        protected void PrintMsg(MsgType type, string msg)
        {
            Console.ForegroundColor =
                type == MsgType.Info ? ConsoleColor.White :
                type == MsgType.Success ? ConsoleColor.Green :
                type == MsgType.Warn ? ConsoleColor.Yellow :
                ConsoleColor.Red;
            Console.Write(msg);
            Console.ResetColor();
        }

        /// <summary>
        /// 控制台打印消息
        /// </summary>
        /// <param name="type">消息类型</param>
        /// <param name="msg">消息内容</param>
        protected void PrintMsgLn(MsgType type, string msg)
        {
            Console.ForegroundColor =
                type == MsgType.Info ? ConsoleColor.White :
                type == MsgType.Success ? ConsoleColor.Green :
                type == MsgType.Warn ? ConsoleColor.Yellow :
                ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        ///<summary>
        ///生成随机字符串 
        ///</summary>
        ///<param name="length">字符串的长度</param>
        ///<returns>随机字符串</returns>
        protected string GetRandomString(int length)
        {
            byte[] bts = new byte[4];
            new RNGCryptoServiceProvider().GetBytes(bts);
            Random r = new Random(BitConverter.ToInt32(bts, 0));

            string result = null;
            string model = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
            for (int i = 0; i < length; i++)
            {
                result += model.Substring(r.Next(0, model.Length - 1), 1);
            }

            return result;
        }

        /// <summary>
        /// 消息类型
        /// </summary>
        protected enum MsgType
        {
            /// <summary>
            /// 信息
            /// </summary>
            Info,
            /// <summary>
            /// 警告
            /// </summary>
            Success,
            /// <summary>
            /// 警告
            /// </summary>
            Warn,
            /// <summary>
            /// 错误
            /// </summary>
            Error
        }
    }
}
