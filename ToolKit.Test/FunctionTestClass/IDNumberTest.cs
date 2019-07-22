using CML.CommonEx.IDNumberEx;
using CML.CommonEx.IDNumberEx.ExFunction;
using CML.CommonEx.EnumEx.ExFunction;

namespace CML.ToolTest
{
    /// <summary>
    /// 身份证号操作测试类
    /// </summary>
    internal class IDNumberTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "IDNumberTest";

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
            ModIDNumber modelIDNumber = new ModIDNumber("33152120000101001X");

            PrintLogLn(MsgType.Info, $"校验位: {modelIDNumber.CF_GetSpecialCode()}");
            PrintLogLn(MsgType.Info, $"籍贯: {modelIDNumber.CF_GetDomicile()}");
            PrintLogLn(MsgType.Info, $"发卡机构: {modelIDNumber.CF_GetCardIssuer()}");
            PrintLogLn(MsgType.Info, $"性别: {modelIDNumber.CF_GetGender().CF_GetDescription()}");
            PrintLogLn(MsgType.Info, $"年龄: {modelIDNumber.CF_GetAge()}");
            PrintLogLn(MsgType.Info, $"生日: {modelIDNumber.CF_GetBirthday().ToString("yyyy/MM/dd")}");
            PrintLogLn(MsgType.Info, $"生肖: {modelIDNumber.CF_GetZodiac()}");
            PrintLogLn(MsgType.Info, $"星座: {modelIDNumber.CF_GetConstellation()}");
        }
    }
}
