using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 菱形图形控件
    /// </summary>
    [ToolboxItem(true)]
    [DefaultBindingProperty("Text"), DefaultProperty("Text")]
    public class ShapeDiamond : ShapeBase
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ShapeDiamond()
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
            //创建路径
            m_shapePath = new GraphicsPath();
            m_shapePath.AddLine(Width / 2, 0, Width, Height / 2 - 1);
            m_shapePath.AddLine(Width, Height / 2 - 1, Width / 2, Height - 1);
            m_shapePath.AddLine(Width / 2, Height - 1, 0, Height / 2);
            m_shapePath.AddLine(0, Height / 2, Width / 2, 0);

            base.OnPaint(e);
        }
        #endregion
    }
}
