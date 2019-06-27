using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace CML.ControlEx
{
    /// <summary>
    /// 告警指示灯控件
    /// </summary>
    [ToolboxItem(true)]
    [DefaultBindingProperty("CP_LightColor"), DefaultProperty("CP_LightColor")]
    public partial class CmlLanternAlarm : UserControl
    {
        #region 私有变量
        private Color m_colorBottom = Color.DimGray;
        private Color m_colorLight = Color.LimeGreen;
        private Color m_colorSave = Color.LimeGreen;
        private bool m_isFlash = false;
        private bool m_isLight = true;
        private readonly Timer m_timer = new Timer();
        private readonly StringFormat m_stringFormat = null;
        #endregion

        #region 重写属性
        /// <summary>
        /// 获取或设置控件的背景色
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true), DefaultValue(typeof(Color), "Transparent")]
        [Category("Appearance"), Description("获取或设置控件的背景色")]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置底座的背景色
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Color), "DimGray")]
        [Category("CMLAttribute"), Description("获取或设置底座的背景色")]
        public Color CP_BottomColor
        {
            get
            {
                return m_colorBottom;
            }
            set
            {
                m_colorBottom = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置报警灯颜色
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Color), "LimeGreen")]
        [Category("CMLAttribute"), Description("获取或设置报警灯颜色")]
        public Color CP_LightColor
        {
            get
            {
                return m_colorSave;
            }
            set
            {
                m_colorLight = value;
                m_colorSave = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置信号灯闪烁状态
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("获取或设置信号灯闪烁状态")]
        public bool CP_IsFlash
        {
            get
            {
                return m_isFlash;
            }
            set
            {
                m_isFlash = value;

                if (m_isFlash)
                {
                    m_timer.Start();
                }
                else
                {
                    m_timer.Stop();

                    if (!m_isLight)
                    {
                        m_isLight = true;
                        m_colorLight = m_colorSave;

                        Invalidate();
                    }
                }
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public CmlLanternAlarm()
        {
            InitializeComponent();

            m_stringFormat = new StringFormat();
            m_stringFormat.Alignment = StringAlignment.Center;
            m_stringFormat.LineAlignment = StringAlignment.Center;

            base.SetStyle(ControlStyles.UserPaint |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint,
                true
            );

            m_timer.Interval = 500;
            m_timer.Tick += Timer_Tick;
        }
        #endregion

        #region 重写事件
        /// <summary>
        /// 重绘控件的模型
        /// </summary>
        /// <param name="e">事件</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            int nBottom = 8;
            using (Brush brushBottom = new SolidBrush(m_colorBottom))
            {
                graphics.FillRectangle(brushBottom, new Rectangle(0, base.Height - 1 - nBottom, base.Width, nBottom));
                graphics.FillRectangle(brushBottom, new Rectangle(base.Width / 20, base.Height - 1 - nBottom * 2, base.Width - base.Width / 10, nBottom));
            }

            int nLight = base.Width * 4 / 10;
            using (Brush brushLight = new SolidBrush(m_colorLight))
            {
                if (base.Height - nLight - 2 * nBottom > 0)
                {
                    graphics.FillRectangle(brushLight, base.Width / 10, nLight, base.Width * 8 / 10, base.Height - nLight - 2 * nBottom);
                }
                graphics.FillPie(brushLight, base.Width / 10, 1, base.Width * 8 / 10, nLight * 2, 180, 180);
            }

            base.OnPaint(e);
        }
        #endregion

        #region 计时器事件
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (m_isFlash)
            {
                if (m_isLight)
                {
                    m_colorLight = Color.LightGray;
                }
                else
                {
                    m_colorLight = m_colorSave;
                }

                m_isLight = !m_isLight;
            }
            else
            {
                m_colorLight = m_colorSave;
            }

            Invalidate();
        }
        #endregion
    }
}
