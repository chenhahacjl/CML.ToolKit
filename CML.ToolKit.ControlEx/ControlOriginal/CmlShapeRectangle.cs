using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 长方形图形控件
    /// </summary>
    [ToolboxItem(true)]
    [DefaultBindingProperty("Text"), DefaultProperty("Text")]
    public class CmlShapeRectangle : ShapeBase
    {
        #region 私有变量
        private int m_nRoundCorner = 4;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置圆角半径
        /// </summary>
        [Browsable(true), DefaultValue(4)]
        [Category("CMLAttribute"), Description("获取或设置圆角半径")]
        public int CP_CornerRadius
        {
            get => m_nRoundCorner;
            set
            {

                m_nRoundCorner = value < 0 ? 0 : value;
                Invalidate();
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public CmlShapeRectangle()
        {
            //控件大小
            Width = 160;
            Height = 80;
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
            if (m_nRoundCorner == 0)
            {
                m_shapePath.AddRectangle(new Rectangle(0, 0, Width - 1, Height - 1));
            }
            else
            {
                m_shapePath.AddLine(m_nRoundCorner, 0, Width - m_nRoundCorner - 1, 0);
                m_shapePath.AddArc(Width - m_nRoundCorner * 2 - 1, 0, m_nRoundCorner * 2, m_nRoundCorner * 2, 270f, 90f);
                m_shapePath.AddLine(Width - 1, m_nRoundCorner, Width - 1, Height - m_nRoundCorner - 1);
                m_shapePath.AddArc(Width - m_nRoundCorner * 2 - 1, Height - m_nRoundCorner * 2 - 1, m_nRoundCorner * 2, m_nRoundCorner * 2, 0f, 90f);
                m_shapePath.AddLine(Width - m_nRoundCorner - 1, Height - 1, m_nRoundCorner, Height - 1);
                m_shapePath.AddArc(0, Height - m_nRoundCorner * 2 - 1, m_nRoundCorner * 2, m_nRoundCorner * 2, 90f, 90f);
                m_shapePath.AddLine(0, Height - m_nRoundCorner - 1, 0, m_nRoundCorner);
                m_shapePath.AddArc(0, 0, m_nRoundCorner * 2, m_nRoundCorner * 2, 180f, 90f);
            }

            base.OnPaint(e);
        }
        #endregion
    }
}
