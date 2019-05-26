using System;

namespace CML.ToolKit.ToolTest
{
    /// <summary>
    /// 测试类基类
    /// 【请重载 ExecuteTest() 方法】
    /// </summary>
    internal class ToolKitTestBase
    {
        /// <summary>
        /// 执行测试【请重载本方法】
        /// </summary>
        public virtual void ExecuteTest()
        {
            PrintLn(MsgType.WARN, $"请重载 ToolKitTestBase 类 ExecuteTest() 方法");
        }

        /// <summary>
        /// 控制台打印消息
        /// </summary>
        /// <param name="type">消息类型</param>
        /// <param name="msg">消息内容</param>
        protected void PrintLn(MsgType type, string msg)
        {
            Console.ForegroundColor =
                type == MsgType.INFO ? ConsoleColor.White :
                type == MsgType.WARN ? ConsoleColor.Yellow :
                ConsoleColor.Red;
            Console.WriteLine($"[{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}]{msg}");
            Console.ResetColor();
        }

        /// <summary>
        /// 消息类型
        /// </summary>
        protected enum MsgType
        {
            /// <summary>
            /// 信息
            /// </summary>
            INFO,
            /// <summary>
            /// 警告
            /// </summary>
            WARN,
            /// <summary>
            /// 错误
            /// </summary>
            ERROR
        }
    }
}
