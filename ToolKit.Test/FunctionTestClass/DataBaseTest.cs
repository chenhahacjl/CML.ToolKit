using CML.CommonEx.DataBaseEx;
using System;

namespace ToolKit.Test
{
    /// <summary>
    /// 数据库测试类
    /// </summary>
    internal class DataBaseTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "DataBaseTest";

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
            DataBase dataBase = new DataBase()
            {
                CP_ConnectionType = EDataBaseType.ORACLE,
                CP_ConnectionString = "DATA SOURCE=192.168.20.15:1521/orcl; User ID=SCOTT; Password=123456;"
            };

            try
            {
                dataBase.CF_InitDataBase();

                object count = dataBase.CF_GetSingleObject("SELECT COUNT(*) FROM T_AGING_RESULT_INFO");
                PrintLogLn(MsgType.Info, $"{count}");
            }
            catch (Exception ex)
            {
                PrintLogLn(MsgType.Error, ex.Message);
            }
        }
    }
}
