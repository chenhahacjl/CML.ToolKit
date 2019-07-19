using CML.CommonEx.EverythingEx;
using System;

namespace CML.ToolTest.FunctionTestClass
{
    /// <summary>
    /// Everything软件搜索测试类
    /// </summary>
    internal class EverythingTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "EverythingTest";

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
            PrintMsg(MsgType.Info, "请输入关键字: ");
            string keyword = Console.ReadLine();

            try
            {
                //设置搜索条件
                EverythingOperate.CF_DetectPlatform();
                EverythingOperate.CF_SetSearch(keyword);
                EverythingOperate.CF_SetRequestFlags(ERequest.FILE_NAME);
                EverythingOperate.CF_SetSort(ESortMode.DATE_MODIFIED_ASCENDING);

                //执行搜索
                EverythingOperate.CF_Query(true);

                //获取结果
                for (uint i = 0; i < EverythingOperate.CF_GetNumResults(); i++)
                {
                    PrintMsgLn(MsgType.Info, EverythingOperate.CF_GetResultFileName(i));
                }

                PrintLogLn(MsgType.Info, $"搜索到: {EverythingOperate.CF_GetNumResults()} 个文件（夹）");
            }
            catch (Exception ex)
            {
                PrintLogLn(MsgType.Error, ex.Message);
            }
        }
    }
}
