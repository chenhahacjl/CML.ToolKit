using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CML.ControlEx
{
    /// <summary>
    /// 窗体移动工具
    /// </summary>
    [ToolboxItem(true)]
    public partial class CmlFormMoveTool : UserControl
    {
        #region 私有常量
        //透明样式
        private const int WS_EX_TRANSPARENT = 0x0020;
        #endregion

        #region 私有变量
        private Point m_ptLocation = new Point(0, 0);
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置按下移动状态
        /// </summary>
        [Browsable(true), DefaultValue(true)]
        [Category("CMLAttribute"), Description("获取或设置按下移动状态")]
        public bool CP_Moveable { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public CmlFormMoveTool()
        {
            InitializeComponent();

            //控件透明支持
            SetStyle(
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.Opaque,
                true
            );

            CP_Moveable = true;
        }
        #endregion

        #region 重写方法
        /// <summary>
        /// 控件透明支持
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                //控件透明支持
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TRANSPARENT;

                return cp;
            }
        }
        #endregion

        #region 窗体移动事件
        private void CmlFormMoveTool_MouseDown(object sender, MouseEventArgs e)
        {
            if (CP_Moveable)
            {
                m_ptLocation = new Point(e.X, e.Y);
            }
        }

        private void CmlFormMoveTool_MouseMove(object sender, MouseEventArgs e)
        {
            if (CP_Moveable && ParentForm != null && e.Button == MouseButtons.Left)
            {
                int nNewX = ParentForm.Location.X + e.X - m_ptLocation.X;
                int nNewY = ParentForm.Location.Y + e.Y - m_ptLocation.Y;

                ParentForm.Location = new Point(nNewX, nNewY);
            }
        }
        #endregion
    }
}
