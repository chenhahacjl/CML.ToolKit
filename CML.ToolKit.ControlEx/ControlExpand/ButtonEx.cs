using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 按钮控件
    /// </summary>
    [ToolboxItem(true)]
    [DefaultEvent("Click")]
    [DefaultBindingProperty("Text"), DefaultProperty("Text")]
    public partial class ButtonEx : UserControl
    {
        #region 私有变量
        /// <summary>
        /// 字符串格式
        /// </summary>
        private readonly StringFormat m_stringFormat = null;
        /// <summary>
        /// 换行字符
        /// </summary>
        private char m_cNewLine = '@';
        /// <summary>
        /// 前景颜色、背景颜色、启用状态颜色、激活状态颜色
        /// </summary>
        private Color m_colorFore = Color.Black;
        private Color m_colorBack = Color.Gainsboro;
        private Color m_colorEnable = Color.FromArgb(190, 190, 190);
        private Color m_colorActive = Color.AliceBlue;
        /// <summary>
        /// 圆角半径
        /// </summary>
        private int m_nRoundCorner = 4;
        /// <summary>
        /// 选中状态
        /// </summary>
        private bool m_isSelected = false;
        /// <summary>
        /// 边框显示状态
        /// </summary>
        private bool m_isBorderVisiable = true;
        /// <summary>
        /// 鼠标覆盖状态
        /// </summary>
        private bool m_isMouseHorve = false;
        /// <summary>
        /// 是否为左键单击
        /// </summary>
        private bool m_isLeftMouseDown = false;
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
            get => base.BackColor;
            set => base.BackColor = value;
        }

        /// <summary>
        /// 获取或设置当前控件的文本的颜色
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true), DefaultValue(typeof(Color), "Black")]
        [Category("Appearance"), Description("获取或设置当前控件的文本的颜色")]
        public override Color ForeColor
        {
            get => m_colorFore;
            set
            {
                m_colorFore = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置当前控件的文本
        /// </summary>
        [Browsable(true), Bindable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance"), Description("获取或设置当前控件的文本")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                base.Invalidate();
            }
        }

        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置按钮文本换行符
        /// </summary>
        [DefaultValue(typeof(Color), "@")]
        [Category("CMLAttribute"), Description("获取或设置按钮文本换行符")]
        public char CP_NewLineChar
        {
            get => m_cNewLine;
            set
            {
                m_cNewLine = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置按钮的背景色
        /// </summary>
        [DefaultValue(typeof(Color), "Gainsboro")]
        [Category("CMLAttribute"), Description("获取或设置按钮的背景色")]
        public Color CP_OriginalColor
        {
            get => m_colorBack;
            set
            {
                m_colorBack = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置按钮的启用色
        /// </summary>
        [DefaultValue(typeof(Color), "0xBEBEBE")]
        [Category("CMLAttribute"), Description("获取或设置按钮的启用色")]
        public Color CP_EnableColor
        {
            get => m_colorEnable;
            set
            {
                m_colorEnable = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置按钮的活动色
        /// </summary>
        [DefaultValue(typeof(Color), "AliceBlue")]
        [Category("CMLAttribute"), Description("获取或设置按钮的活动色")]
        public Color CP_ActiveColor
        {
            get => m_colorActive;
            set
            {
                m_colorActive = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置按钮圆角半径
        /// </summary>
        [DefaultValue(4)]
        [Category("CMLAttribute"), Description("获取或设置按钮圆角半径")]
        public int CP_CornerRadius
        {
            get => m_nRoundCorner;
            set
            {
                m_nRoundCorner = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 设置按钮的边框可见性
        /// </summary>
        [Browsable(true), DefaultValue(true)]
        [Category("CMLAttribute"), Description("设置按钮的边框可见性")]
        public bool CP_BorderVisiable
        {
            get => m_isBorderVisiable;
            set
            {
                m_isBorderVisiable = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置按钮的选中状态
        /// </summary>
        [DefaultValue(false)]
        [Category("CMLAttribute"), Description("获取或设置按钮的选中状态")]
        public bool CP_Selected
        {
            get => m_isSelected;
            set
            {
                m_isSelected = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置额外的信息
        /// </summary>
        [Browsable(false), DefaultValue(false)]
        [Category("CMLAttribute"), Description("获取或设置额外的信息")]
        public string CP_CustomerInformation { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ButtonEx()
        {
            InitializeComponent();

            // 初始化文本布局信息
            m_stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            //设置控件样式
            base.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint,
                true
            );
        }
        #endregion

        #region 重写事件
        /// <summary>
        /// 重绘界面的逻辑功能
        /// </summary>
        /// <param name="e">重绘的事件</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            //边框路径
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddLine(CP_CornerRadius, 0, base.Width - CP_CornerRadius - 1, 0);
            graphicsPath.AddArc(base.Width - CP_CornerRadius * 2 - 1, 0, CP_CornerRadius * 2, CP_CornerRadius * 2, 270f, 90f);
            graphicsPath.AddLine(base.Width - 1, CP_CornerRadius, base.Width - 1, base.Height - CP_CornerRadius - 1);
            graphicsPath.AddArc(base.Width - CP_CornerRadius * 2 - 1, base.Height - CP_CornerRadius * 2 - 1, CP_CornerRadius * 2, CP_CornerRadius * 2, 0f, 90f);
            graphicsPath.AddLine(base.Width - CP_CornerRadius - 1, base.Height - 1, CP_CornerRadius, base.Height - 1);
            graphicsPath.AddArc(0, base.Height - CP_CornerRadius * 2 - 1, CP_CornerRadius * 2, CP_CornerRadius * 2, 90f, 90f);
            graphicsPath.AddLine(0, base.Height - CP_CornerRadius - 1, 0, CP_CornerRadius);
            graphicsPath.AddArc(0, 0, CP_CornerRadius * 2, CP_CornerRadius * 2, 180f, 90f);

            Rectangle rect = new Rectangle(base.ClientRectangle.X, base.ClientRectangle.Y, base.ClientRectangle.Width, base.ClientRectangle.Height);

            Brush brushFore, brushBack;
            if (base.Enabled)
            {
                brushFore = new SolidBrush(ForeColor);

                if (CP_Selected)
                {
                    brushBack = new SolidBrush(Color.DodgerBlue);
                }
                else
                {
                    if (m_isMouseHorve)
                    {
                        brushBack = new SolidBrush(CP_ActiveColor);
                    }
                    else
                    {
                        brushBack = new SolidBrush(m_colorBack);
                    }
                }

                if (m_isLeftMouseDown)
                {
                    rect.Offset(1, 1);
                }
            }
            else
            {
                brushFore = new SolidBrush(Color.Gray);
                brushBack = new SolidBrush(CP_EnableColor);
            }

            //绘制背景
            e.Graphics.FillPath(brushBack, graphicsPath);

            //绘制边框
            if (CP_BorderVisiable)
            {
                using (Pen pen = new Pen(Color.FromArgb(170, 170, 170)))
                {
                    e.Graphics.DrawPath(pen, graphicsPath);
                }
            }

            //绘制文字
            e.Graphics.DrawString(Text.Replace(m_cNewLine.ToString(), "\n"), Font, brushFore, rect, m_stringFormat);

            //释放资源
            brushFore.Dispose();
            brushBack.Dispose();
            graphicsPath.Dispose();

            base.OnPaint(e);
        }

        /// <summary>
        /// 引发MouseEnter事件
        /// </summary>
        /// <param name="e">事件</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            m_isMouseHorve = true;
            base.Invalidate();
        }

        /// <summary>
        /// 引发MouseLeave事件
        /// </summary>
        /// <param name="e">事件</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            m_isMouseHorve = false;
            base.Invalidate();
        }

        /// <summary>
        /// 引发MouseDown事件
        /// </summary>
        /// <param name="e">事件</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            bool flag = e.Button == MouseButtons.Left;
            if (flag)
            {
                m_isLeftMouseDown = true;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 引发MouseUp事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            bool flag = e.Button == MouseButtons.Left;
            if (flag)
            {
                m_isLeftMouseDown = false;
                base.Invalidate();
            }
        }
        #endregion
    }
}
