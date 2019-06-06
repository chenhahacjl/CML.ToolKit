using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 选项卡控件
    /// </summary>
    [ToolboxItem(true)]
    [DefaultEvent("SelectedIndexChanged")]
    [DefaultBindingProperty("TabPages"), DefaultProperty("TabPages")]
    public class TabControlEx : TabControl
    {
        #region 私有变量
        /// <summary>
        /// 换行符
        /// </summary>
        private char m_cNewLine = '@';
        /// <summary>
        /// 选中项背景颜色
        /// </summary>
        private Color m_colorSelectedBack = Color.LimeGreen;
        /// <summary>
        /// 选中项边框颜色
        /// </summary>
        private Color m_colorSelectedBorder = Color.Red;
        /// <summary>
        /// 标签页字体颜色
        /// </summary>
        private Color[] m_colorsPageFore = new Color[] { Color.Black };
        /// <summary>
        /// 标签页背景颜色
        /// </summary>
        private Color[] m_colorsPageBack = new Color[] { Color.LightGray };
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置选项卡标题换行符
        /// </summary>
        [Browsable(true), DefaultValue('@')]
        [Category("CMLAttribute"), Description("选项卡标题换行符")]
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
        /// 获取或设置被选中选项卡边框颜色
        /// </summary>
        [Browsable(true)]
        [Category("CMLAttribute"), Description("被选中选项卡边框颜色")]
        public Color CP_SelectedBorderColor
        {
            get => m_colorSelectedBorder;
            set
            {
                m_colorSelectedBorder = value;

                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置被选中选项卡背景颜色
        /// </summary>
        [Browsable(true)]
        [Category("CMLAttribute"), Description("被选中选项卡背景颜色")]
        public Color CP_SelectedBackColor
        {
            get => m_colorSelectedBack;
            set
            {
                m_colorSelectedBack = value;

                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置选项卡标题背景颜色列表
        /// </summary>
        [Browsable(true)]
        [Category("CMLAttribute"), Description("选项卡标题颜色列表")]
        public Color[] CP_TabPageBackColors
        {
            get => m_colorsPageBack;
            set
            {
                m_colorsPageBack = value;

                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置选项卡标题字体颜色列表
        /// </summary>
        [Browsable(true)]
        [Category("CMLAttribute"), Description("选项卡标题字体颜色列表")]
        public Color[] CP_TabPageForeColors
        {
            get => m_colorsPageFore;
            set
            {
                m_colorsPageFore = value;

                base.Invalidate();
            }
        }

        /// <summary>
        /// 隐藏DrawMode属性
        /// </summary>
        [Browsable(false)]
        public new TabDrawMode DrawMode
        {
            get => base.DrawMode;
            set => base.DrawMode = value;
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public TabControlEx()
        {
            //固定大小
            DrawMode = TabDrawMode.OwnerDrawFixed;

            //设置控件样式
            base.SetStyle(
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint,
                true
            );
        }
        #endregion

        #region 重写事件
        /// <summary>
        /// 引发 System.Windows.Forms.TabControl.DrawItem 事件。
        /// </summary>
        /// <param name="e">为 DrawItem 事件提供数据。</param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            DrawTabItem(e.Graphics);
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 绘制选项卡标题
        /// </summary>
        /// <param name="g"></param>
        private void DrawTabItem(Graphics g)
        {
            int pageCount = TabPages.Count;
            int foreColorCount = m_colorsPageFore.Length;
            int backColorCount = m_colorsPageBack.Length;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            for (int i = 0; i < pageCount; i++)
            {
                Rectangle tpRect = GetTabRect(i);
                g.FillRectangle(new SolidBrush(m_colorsPageBack[i % backColorCount]), tpRect);

                if (SelectedIndex == i)
                {
                    g.FillRectangle(new SolidBrush(m_colorSelectedBack), tpRect);
                    g.DrawRectangle(new Pen(m_colorSelectedBorder, 1), tpRect.X, tpRect.Y, tpRect.Width - 2, tpRect.Height);
                }

                g.DrawString(
                    TabPages[i].Text.Replace(m_cNewLine.ToString(), "\n"),
                    Font,
                    new SolidBrush(m_colorsPageFore[i % foreColorCount]),
                    tpRect,
                    new StringFormat()
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    }
                );
            }
        }
        #endregion
    }
}
