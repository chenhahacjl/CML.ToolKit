using CML.CommonEx.DebugEx;

namespace ToolKit.Test
{
    /// <summary>
    /// 枚举操作测试类
    /// </summary>
    internal class DebugTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "DebugTest";

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
            var result = DebugOperate.OpenDebugDll("CML.CommonEx.dll", "", this, out string errMsg);
            if (result)
            {
                PrintLogLn(MsgType.Success, "调试模块调用成功！");
            }
            else
            {
                PrintLogLn(MsgType.Error, errMsg);
            }
        }

        #region 测试内容
        public int m_testField1 = 9696;
        private bool m_testField2 = false;
        internal string m_testField3 = "TestField";
        public char m_testField4 = 'T';
        public short m_testField5 = 96;
        public long m_testField6 = 969696;
        public byte m_testField7 = 128;
        public decimal m_testField8 = 96969696;
        public double m_testField9 = 9696.9696;
        public float m_testField10 = 9696.96f;
        public sbyte m_testField11 = -96;
        public uint m_testField12 = 969696;
        public ulong m_testField13 = 969696;
        public ushort m_testField14 = 9696;

        public int TestProperty1
        {
            get
            {
                PrintLogLn(MsgType.Info, $"获取TestProperty1属性值: {m_testField1}");
                return m_testField1;
            }
            set
            {
                PrintLogLn(MsgType.Info, $"设置TestProperty1属性值: {m_testField1} -> {value}");
                m_testField1 = value;
            }
        }

        private bool TestProperty2
        {
            get
            {
                PrintLogLn(MsgType.Info, $"获取TestProperty2属性值: {m_testField2}");
                return m_testField2;
            }
            set
            {
                PrintLogLn(MsgType.Info, $"设置TestProperty2属性值: {m_testField2} -> {value}");
                m_testField2 = value;
            }
        }

        internal string TestProperty3
        {
            get
            {
                PrintLogLn(MsgType.Info, $"获取TestProperty3属性值: {m_testField3}");
                return m_testField3;
            }
            set
            {
                PrintLogLn(MsgType.Info, $"设置TestProperty3属性值: {m_testField3} -> {value}");
                m_testField3 = value;
            }
        }
        #endregion
    }
}
