using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 圆形指示灯控件
    /// </summary>
    [ToolboxItem(true)]
    [Designer(typeof(ControlDesignerEx))]
    [DefaultBindingProperty("CP_Title"), DefaultProperty("CP_Title")]
    public class CmlPanelTitle : Control, IDesignerControl
    {
        #region 私有变量
        /// <summary>
        /// 标题Label控件
        /// </summary>
        private Label m_ctlTitleLabel = new Label();
        /// <summary>
        /// 容器Panel控件
        /// </summary>
        private Panel m_ctlContainerPanel = new Panel();
        /// <summary>
        /// 外层Panel控件
        /// </summary>
        private Panel m_ctlExternalPanel = new Panel();
        #endregion

        #region 重写属性
        /// <summary>
        /// 获取或设置当前控件的文本（隐藏属性）
        /// </summary>
        [Browsable(false)]
        public override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        /// <summary>
        /// 获取或设置控件显示的文字的字体（隐藏属性）
        /// </summary>
        [Browsable(false)]
        public override Font Font
        {
            get => base.Font;
            set => base.Font = value;
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取支持设计控件
        /// </summary>
        [Browsable(false)]
        public Control DesignerControl => m_ctlContainerPanel;

        /// <summary>
        /// 获取或设置标题内容
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("获取或设置标题内容")]
        public string CP_Title
        {
            get => m_ctlTitleLabel.Text;
            set => m_ctlTitleLabel.Text = value;
        }

        /// <summary>
        /// 获取或设置标题高度（最小为10）
        /// </summary>
        [Browsable(true), DefaultValue(30)]
        [Category("CMLAttribute"), Description("获取或设置标题高度（最小为10）")]
        public int CP_TitleHeight
        {
            get => m_ctlTitleLabel.Height;
            set => m_ctlTitleLabel.Height = value < 10 ? 10 : value;
        }

        /// <summary>
        /// 获取或设置标题字体
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Font), "宋体, 12pt")]
        [Category("CMLAttribute"), Description("获取或设置标题字体")]
        public Font CP_TitleFont
        {
            get => m_ctlTitleLabel.Font;
            set => m_ctlTitleLabel.Font = value;
        }

        /// <summary>
        /// 获取或设置标题文字对齐方式
        /// </summary>
        [Browsable(true), DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        [Category("CMLAttribute"), Description("获取或设置标题文字对齐方式")]
        public ContentAlignment CP_TitleAlignment
        {
            get => m_ctlTitleLabel.TextAlign;
            set => m_ctlTitleLabel.TextAlign = value;
        }

        /// <summary>
        /// 获取或设置标题背景颜色
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Color), "Lavender")]
        [Category("CMLAttribute"), Description("获取或设置标题背景颜色")]
        public Color CP_TitleBackColor
        {
            get => m_ctlTitleLabel.BackColor;
            set => m_ctlTitleLabel.BackColor = value;
        }

        /// <summary>
        /// 获取或设置标题字体颜色
        /// </summary>
        [Browsable(true), DefaultValue(typeof(Color), "Black")]
        [Category("CMLAttribute"), Description("获取或设置标题字体颜色")]
        public Color CP_TitleForeColor
        {
            get => m_ctlTitleLabel.ForeColor;
            set => m_ctlTitleLabel.ForeColor = value;
        }

        /// <summary>
        /// 获取或设置边框样式
        /// </summary>
        [Browsable(true), DefaultValue(typeof(BorderStyle), "FixedSingle")]
        [Category("CMLAttribute"), Description("获取或设置边框样式")]
        public BorderStyle CP_BorderStyle
        {
            get => m_ctlExternalPanel.BorderStyle;
            set => m_ctlExternalPanel.BorderStyle = value;
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public CmlPanelTitle()
        {
            Size = new Size(200, 150);

            //初始化外层Panel控件
            m_ctlExternalPanel.BorderStyle = BorderStyle.FixedSingle;
            m_ctlExternalPanel.Dock = DockStyle.Fill;
            Controls.Add(m_ctlExternalPanel);

            //初始化容器Panel控件
            m_ctlContainerPanel.BackColor = Color.White;
            m_ctlContainerPanel.Dock = DockStyle.Fill;
            m_ctlExternalPanel.Controls.Add(m_ctlContainerPanel);

            //初始化标题Label控件
            m_ctlTitleLabel.Height = 30;
            m_ctlTitleLabel.AutoSize = false;
            m_ctlTitleLabel.ForeColor = Color.Black;
            m_ctlTitleLabel.BackColor = Color.Lavender;
            m_ctlTitleLabel.Font = new Font("宋体", 12);
            m_ctlTitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            m_ctlTitleLabel.Dock = DockStyle.Top;
            m_ctlExternalPanel.Controls.Add(m_ctlTitleLabel);
        }
    }
}
