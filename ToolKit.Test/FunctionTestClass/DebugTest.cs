using CML.CommonEx.DebugEx;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

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
        private bool m_testField1 = false;

        internal string m_testField2 = "TestField";

        public char m_testField3_1 = 'T';
        public byte m_testField3_2 = 128;
        public sbyte m_testField3_3 = -128;
        public short m_testField3_4 = -128;
        public ushort m_testField13_5 = 128;
        public int m_testField3_6 = -1024;
        public uint m_testField3_7 = 1024;
        public long m_testField3_8 = -40960;
        public ulong m_testField3_9 = 40960;
        public float m_testField3_10 = 1024.96F;
        public double m_testField3_11 = 1024.9696;
        public decimal m_testField3_12 = 96969696;
        public IntPtr m_testField13_13 = new IntPtr(9696);
        public UIntPtr m_testField3_14 = new UIntPtr(9696);

        public DateTime m_testField4 = DateTime.Now;

        public Size m_testField5_1 = new Size(96, 96);
        public SizeF m_testField5_2 = new SizeF(9.6F, 6.9F);
        public Point m_testField5_3 = new Point(96, 96);
        public PointF m_testField5_4 = new PointF(9.6F, 9.6F);

        public Rectangle m_testField6_1 = new Rectangle(9, 6, 9, 6);
        public RectangleF m_testField6_2 = new RectangleF(9.6F, 6.9F, 9.6F, 6.9F);
        public Padding m_testField6_3 = new Padding(9, 6, 9, 6);

        public Color m_testField7 = Color.FromKnownColor(KnownColor.MenuHighlight);

        public ETest m_testField8 = ETest.A;

        public Font m_testField9 = new Font("宋体", 12);

        public bool TestProperty1
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

        private string TestProperty2
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

        internal int TestProperty3
        {
            get
            {
                PrintLogLn(MsgType.Info, $"获取TestProperty3属性值: {m_testField3_6}");
                return m_testField3_6;
            }
            set
            {
                PrintLogLn(MsgType.Info, $"设置TestProperty3属性值: {m_testField3_6} -> {value}");
                m_testField3_6 = value;
            }
        }

        public enum ETest
        {
            [Description("AA")]
            A,
            [Description("BB")]
            B,
            [Description("CC")]
            C,
            [Description("DD")]
            D,
        }
        #endregion
    }
}
