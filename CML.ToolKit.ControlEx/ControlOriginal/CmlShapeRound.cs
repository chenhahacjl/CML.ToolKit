using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 圆形图形控件
    /// </summary>
    [ToolboxItem(true)]
    [DefaultBindingProperty("Text"), DefaultProperty("Text")]
    public class CmlShapeRound : ShapeBase
    {
        #region 私有变量
        private int m_nRadius = 50;
        #endregion

        #region 隐藏属性
        /// <summary>
        /// 获取或设置控件的高度和宽度
        /// </summary>
        [Browsable(false)]
        [Category("CMLAttribute"), Description("获取或设置控件的高度")]
        public new Size Size
        {
            get => base.Size;
            set => base.Size = value;
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置圆形半径
        /// </summary>
        [Browsable(true), DefaultValue(50)]
        [Category("CMLAttribute"), Description("获取或设置圆形半径")]
        public int CP_Radius
        {
            get => m_nRadius;
            set
            {
                m_nRadius = value < 1 ? 1 : value;
                Width = Height = m_nRadius * 2;
                Invalidate();
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public CmlShapeRound()
        {
            //控件大小
            Width = Height = 100;
        }
        #endregion

        #region  重写事件
        /// <summary>
        /// System.Windows.Forms.Control.Paint 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.Windows.Forms.PaintEventArgs。</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            m_shapePath = new GraphicsPath();
            m_shapePath.AddEllipse(new Rectangle(0, 0, m_nRadius * 2 - 1, m_nRadius * 2 - 1));

            base.OnPaint(e);
        }

        /// <summary>
        /// 引发 System.Windows.Forms.Control.Resize 事件。
        /// </summary>
        /// <param name="eventargs">包含事件数据的 System.EventArgs。</param>
        protected override void OnResize(EventArgs eventargs)
        {
            Width = Height;
            m_nRadius = Width / 2;
            Invalidate();
        }
        #endregion
    }
}
