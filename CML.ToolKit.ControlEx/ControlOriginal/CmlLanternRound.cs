using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 圆形指示灯控件
    /// </summary>
    [ToolboxItem(true)]
    [DefaultBindingProperty("CP_LanternColor"), DefaultProperty("CP_LanternColor")]
    public partial class CmlLanternRound : UserControl
    {
        #region 私有变量
        private readonly StringFormat m_stringFormat = null;
        private Color m_colorCenter = Color.White;
        private Color m_colorBackground = Color.LimeGreen;
        private bool m_isUseGradientColor = false;
        #endregion

        #region 重写属性
        /// <summary>
        /// 隐藏Text属性
        /// </summary>
        [Browsable(false)]
        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

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
        /// 获取或设置灯信号的前景色
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Color), "LimeGreen")]
        [Category("CMLAttribute"), Description("获取或设置信号灯的背景色")]
        public Color CP_LanternColor
        {
            get
            {
                return m_colorBackground;
            }
            set
            {
                m_colorBackground = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置中心点的颜色，当且仅当UseGradientColor属性为True时生效
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Color), "White")]
        [Category("CMLAttribute"), Description("获取或设置中心点的颜色，当且仅当UseGradientColor属性为True时生效")]
        public Color CP_CenterColor
        {
            get
            {
                return m_colorCenter;
            }
            set
            {
                m_colorCenter = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置当前的灯信号是否启用渐变色的画刷
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("获取或设置当前的灯信号是否启用渐变色的画刷")]
        public bool CP_UseGradientColor
        {
            get
            {
                return m_isUseGradientColor;
            }
            set
            {
                m_isUseGradientColor = value;
                base.Invalidate();
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public CmlLanternRound()
        {
            InitializeComponent();

            m_stringFormat = new StringFormat();
            m_stringFormat.Alignment = StringAlignment.Center;
            m_stringFormat.LineAlignment = StringAlignment.Center;

            base.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.SupportsTransparentBackColor,
                true
            );
        }
        #endregion

        #region 重写方法
        /// <summary>
        /// 重绘控件的界面信息
        /// </summary>
        /// <param name="e">重绘事件</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //高宽最小值
            int nMin = Math.Min(base.Width, base.Height) - 1;

            //中点
            Point ptMid = new Point(nMin / 2, nMin / 2);
            e.Graphics.TranslateTransform((float)ptMid.X, (float)ptMid.Y);

            float num2 = (float)(ptMid.X * 17) / 20f;
            float num3 = (float)ptMid.X / 20f;

            if (num2 >= 2f)
            {
                //内部的圆外接矩形
                RectangleF rectInner = new RectangleF(-num2, -num2, 2f * num2, 2f * num2);
                //外部圆的外接矩形
                RectangleF rectExternal = new RectangleF(-num2 - 2f * num3, -num2 - 2f * num3, 2f * num2 + 4f * num3, 2f * num2 + 4f * num3);

                //绘制外部圆形
                using (Pen pen = new Pen(m_colorBackground, num3))
                {
                    e.Graphics.DrawEllipse(pen, rectExternal);
                }

                //绘制内部圆形
                if (!m_isUseGradientColor)
                {
                    using (Brush brushBackground = new SolidBrush(m_colorBackground))
                    {
                        e.Graphics.FillEllipse(brushBackground, rectInner);
                    }
                }
                else
                {
                    GraphicsPath graphicsPath = new GraphicsPath();
                    graphicsPath.AddEllipse(-num2, -num2, 2f * num2, 2f * num2);

                    PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath);
                    pathGradientBrush.CenterPoint = new Point(0, 0);
                    pathGradientBrush.InterpolationColors = new ColorBlend
                    {
                        Positions = new float[]
                        {
                            0f,
                            1f
                        },
                        Colors = new Color[]
                        {
                            m_colorBackground,
                            m_colorCenter
                        }
                    };

                    e.Graphics.FillEllipse(pathGradientBrush, rectInner);

                    using (Pen pen2 = new Pen(m_colorBackground))
                    {
                        e.Graphics.DrawEllipse(pen2, -num2, -num2, 2f * num2, 2f * num2);
                    }

                    //释放资源
                    pathGradientBrush.Dispose();
                    graphicsPath.Dispose();
                }

                e.Graphics.ResetTransform();

                base.OnPaint(e);
            }
        }
        #endregion
    }
}