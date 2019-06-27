using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CML.ControlEx
{
    /// <summary>
    /// 复选框控件
    /// </summary>
    [ToolboxItem(true)]
    [DefaultEvent("CheckedChanged")]
    [DefaultBindingProperty("Text"), DefaultProperty("Text")]
    public class CmlCheckBoxEx : CheckBox
    {
        #region 私有变量
        /// <summary>
        /// 勾选时控件显示属性
        /// </summary>
        private string m_strCheckText = "";
        private Font m_colorCheckedFont = new Font("宋体", 9F);
        private Color m_colorCheckedFore = Color.Black;
        private Color m_colorCheckedBack = SystemColors.Control;
        /// <summary>
        /// 未勾选时控件显示属性
        /// </summary>
        private string m_strUnCheckedText = "";
        private Font m_colorUnCheckedFont = new Font("宋体", 9F);
        private Color m_colorUnCheckedFore = Color.Black;
        private Color m_colorUnCheckedBack = SystemColors.Control;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置选中状态下显示的文字
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("获取或设置选中状态下显示的文字")]
        public string CP_CheckedText
        {
            get => m_strCheckText;
            set
            {
                m_strCheckText = value;

                if (Checked)
                {
                    base.Text = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置选中状态下显示的字体
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("获取或设置选中状态下显示的字体")]
        public Font CP_CheckedFont
        {
            get => m_colorCheckedFont;
            set
            {
                m_colorCheckedFont = value;

                if (Checked)
                {
                    base.Font = value;
                }
            }
        }

        /// <summary>
        /// 设置或获取选中状态下的前景色
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("设置或获取选中状态下的前景色")]
        public Color CP_CheckedForeColor
        {
            get => m_colorCheckedFore;
            set
            {
                m_colorCheckedFore = value;


                if (Checked)
                {
                    base.ForeColor = value;
                }
            }
        }

        /// <summary>
        /// 设置或获取选中状态下的背景色
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("设置或获取选中状态下的背景色")]
        public Color CP_CheckedBackColor
        {
            get => m_colorCheckedBack;
            set
            {
                m_colorCheckedBack = value;

                if (Checked)
                {
                    base.BackColor = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置未选中状态下显示的文字
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("获取或设置未选中状态下显示的文字")]
        public string CP_UnCheckedText
        {
            get => m_strUnCheckedText;
            set
            {
                m_strUnCheckedText = value;

                if (!Checked)
                {
                    base.Text = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置未选中状态下显示的字体
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("获取或设置未选中状态下显示的字体")]
        public Font CP_UnCheckedFont
        {
            get => m_colorUnCheckedFont;
            set
            {
                m_colorUnCheckedFont = value;

                if (!Checked)
                {
                    base.Font = value;
                }
            }
        }

        /// <summary>
        /// 设置或获取未选中状态下的前景色
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("设置或获取未选中状态下的前景色")]
        public Color CP_UnCheckedForeColor
        {
            get => m_colorUnCheckedFore;
            set
            {
                m_colorUnCheckedFore = value;


                if (!Checked)
                {
                    base.ForeColor = value;
                }
            }
        }

        /// <summary>
        /// 设置或获取未选中状态下的背景色
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("设置或获取未选中状态下的背景色")]
        public Color CP_UnCheckedBackColor
        {
            get => m_colorUnCheckedBack;
            set
            {
                m_colorUnCheckedBack = value;

                if (!Checked)
                {
                    base.BackColor = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置与此控件关联的文本(隐藏属性)
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false), DefaultValue(false)]
        [Category("Appearance"), Description("获取或设置与此控件关联的文本")]
        public new string Text => base.Text;

        /// <summary>
        /// 获取或设置显示的文字的字体(隐藏属性)
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false), DefaultValue(false)]
        [Category("Appearance"), Description("获取或设置显示的文字的字体(隐藏属性)")]
        public new Font Font => base.Font;

        /// <summary>
        /// 设置或获取控件的前景色(隐藏属性)
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false), DefaultValue(false)]
        [Category("Appearance"), Description("设置或获取控件的前景色(隐藏属性)")]
        public new Color ForeColor => base.ForeColor;

        /// <summary>
        /// 设置或获取控件的背景色(隐藏属性)
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false), DefaultValue(false)]
        [Category("Appearance"), Description("设置或获取控件的背景色(隐藏属性)")]
        public new Color BackColor => base.BackColor;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public CmlCheckBoxEx()
        {
            base.Text = string.Empty;
        }
        #endregion

        #region 重写事件
        /// <summary>
        /// 引发 System.Windows.Forms.CheckBox.CheckedChanged 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs。</param>
        protected override void OnCheckedChanged(EventArgs e)
        {
            base.Text = Checked ? m_strCheckText : m_strUnCheckedText;
            base.Font = Checked ? m_colorCheckedFont : m_colorUnCheckedFont;
            base.ForeColor = Checked ? m_colorCheckedFore : m_colorUnCheckedFore;
            base.BackColor = Checked ? m_colorCheckedBack : m_colorUnCheckedBack;

            base.OnCheckedChanged(e);
        }
        #endregion
    }
}
