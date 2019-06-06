using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 图形控件基类
    /// </summary>
    [ToolboxItem(false)]
    public class ShapeBase : Control
    {
        #region 私有变量
        /// <summary>
        /// 形状边缘路径
        /// </summary>
        protected GraphicsPath m_shapePath = new GraphicsPath();
        /// <summary>
        /// 样式类型
        /// </summary>
        private EShapeStyleType m_eStyleType = EShapeStyleType.PresetStyle;
        /// <summary>
        /// 预置样色主题
        /// </summary>
        private EShapePresetStyle m_eShapeColorStyle = EShapePresetStyle.Green;
        /// <summary>
        /// 自定义样色主题
        /// </summary>
        private Color m_colorUserStyleShape = Color.Green;
        private Color m_colorUserStyleBorder = Color.Black;
        /// <summary>
        /// 换行符
        /// </summary>
        private char m_cNewLine = '@';
        /// <summary>
        /// 边框宽度
        /// </summary>
        private int m_nBorderWidth = 0;
        #endregion

        #region 重写属性
        /// <summary>
        /// 设置或获取控件的前景色
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("Appearance"), Description("设置或获取控件的前景色")]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置当前控件的文本
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("Appearance"), Description("获取或设置当前控件的文本")]
        public override string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                Invalidate();
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 形状样式类型
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Color), "PresetStyle")]
        [Category("CMLAttribute"), Description("形状样式类型")]
        public EShapeStyleType CP_ShapeStyle
        {
            get => m_eStyleType;
            set
            {
                m_eStyleType = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置形状预置样式
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Color), "Green")]
        [Category("CMLAttribute"), Description("获取或设置预置形状样式")]
        public EShapePresetStyle CP_PresetStyle
        {
            get => m_eShapeColorStyle;
            set
            {
                m_eShapeColorStyle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置形状用户样式颜色
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Color), "Green")]
        [Category("CMLAttribute"), Description("获取或设置用户样式形状颜色")]
        public Color CP_UserStyleShapeColor
        {
            get => m_colorUserStyleShape;
            set
            {
                m_colorUserStyleShape = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置用户样式边框颜色
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Color), "Black")]
        [Category("CMLAttribute"), Description("获取或设置用户样式边框颜色")]
        public Color CP_UserStyleBorderColor
        {
            get => m_colorUserStyleBorder;
            set
            {
                m_colorUserStyleBorder = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置提示文字换行符
        /// </summary>
        [Browsable(true), DefaultValue('@')]
        [Category("CMLAttribute"), Description("获取或设置提示文字换行符")]
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
        /// 获取或设置边框宽度
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        [Category("CMLAttribute"), Description("获取或设置边框宽度")]
        public int CP_BorderWidth
        {
            get => m_nBorderWidth;
            set
            {
                m_nBorderWidth = value < 0 ? 0 : value;
                Invalidate();
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ShapeBase()
        {
            //控件风格
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
        }
        #endregion

        #region  重写事件
        /// <summary>
        /// System.Windows.Forms.Control.Paint 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.Windows.Forms.PaintEventArgs。</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //抗锯齿
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Color colorShape, colorBorder, colorFont;

            if (m_eStyleType == EShapeStyleType.UserStyle)
            {
                colorShape = m_colorUserStyleShape;
                colorBorder = m_colorUserStyleBorder;
                colorFont = ForeColor;
            }
            else if (m_eShapeColorStyle == EShapePresetStyle.Green)
            {
                colorShape = Color.LimeGreen;
                colorBorder = colorFont = Color.Black;
            }
            else if (m_eShapeColorStyle == EShapePresetStyle.Yellow)
            {
                colorShape = Color.Gold;
                colorBorder = colorFont = Color.Black;
            }
            else if (m_eShapeColorStyle == EShapePresetStyle.Red)
            {
                colorShape = Color.Tomato;
                colorBorder = colorFont = Color.Black;
            }
            else
            {
                colorShape = Color.LightGray;
                colorBorder = colorFont = Color.Black;
            }

            //绘制图形
            using (Brush brush = new SolidBrush(colorShape))
            {
                e.Graphics.FillPath(brush, m_shapePath);
            }

            //绘制边框
            if (m_nBorderWidth != 0)
            {
                using (Brush brush = new SolidBrush(colorBorder))
                {
                    using (Pen pen = new Pen(brush, m_nBorderWidth))
                    {
                        e.Graphics.DrawPath(pen, m_shapePath);
                    }
                }
            }

            //绘制文字
            using (Brush brush = new SolidBrush(colorFont))
            {
                //换行
                string strText = Text.Replace(m_cNewLine, '\n');
                SizeF size = e.Graphics.MeasureString(strText, Font);
                float x = (Width - size.Width) / 2;
                float y = (Height - size.Height) / 2;

                e.Graphics.DrawString(strText, Font, brush, x, y);
            }
        }

        /// <summary>
        /// 引发 System.Windows.Forms.Control.Resize 事件。
        /// </summary>
        /// <param name="eventargs">包含事件数据的 System.EventArgs。</param>
        protected override void OnResize(EventArgs eventargs)
        {
            base.Invalidate();
        }
        #endregion
    }
}
