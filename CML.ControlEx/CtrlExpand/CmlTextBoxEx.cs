using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CML.ControlEx
{
    /// <summary>
    /// 单位显示文本框
    /// </summary>
    [ToolboxItem(true)]
    [DefaultBindingProperty("Text"), DefaultProperty("Text")]
    public partial class CmlTextBoxEx : UserControl
    {
        #region 私有变量
        private int m_nUnitWidth = 0;
        private int m_nTitleWidth = 0;
        private Color m_colorNormalBack = Color.LightGray;
        private Color m_colorDisableBack = Color.LightGray;
        private Color m_colorReadOnlyBack = Color.LightGray;
        #endregion

        #region 注册事件
        /// <summary>
        /// 在文本框按键释放时发生
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CmlEvent"), Description("在文本框按键释放时发生")]
        public event KeyEventHandler CE_KeyUp;

        /// <summary>
        /// 在文本框按键按下时发生
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CmlEvent"), Description("在文本框按键按下时发生")]
        public event KeyEventHandler CE_KeyDown;

        /// <summary>
        /// 在内容改变时发生
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CmlEvent"), Description("在内容改变时发生")]
        public event EventHandler CE_TextChanged;

        /// <summary>
        /// 在单位单击时发生
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CmlEvent"), Description("在单位单击时发生")]
        public event EventHandler CE_UnitClick;
        #endregion

        #region 重写属性
        /// <summary>
        /// 获取或设置与此关联的文本
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("获取或设置与此关联的文本")]
        public new string Text
        {
            get => txtlimit.Text;
            set => txtlimit.Text = value;
        }

        /// <summary>
        /// 设置或获取控件的背景色
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("Appearance"), Description("设置或获取控件的背景色")]
        public new Color BackColor
        {
            get => m_colorNormalBack;
            set
            {
                m_colorNormalBack = value;

                ReColoredControl();
            }
        }

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
                txtlimit.ForeColor = value;
            }
        }

        /// <summary>
        /// 设置或获取控件显示的文字的字体
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("Appearance"), Description("设置或获取控件显示的文字的字体")]
        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                txtlimit.Font = value;
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置文本框获得焦点状态
        /// </summary>
        [Browsable(true), DefaultValue(true)]
        [Category("CMLAttribute"), Description("获取或设置文本框获得焦点状态")]
        public bool CP_CanTextFocus { get; set; }

        /// <summary>
        /// 获取或设置控件显示的文本的对齐方式
        /// </summary>
        [Browsable(true), DefaultValue(HorizontalAlignment.Left)]
        [Category("CMLAttribute"), Description("获取或设置控件显示的文本的对齐方式")]
        public HorizontalAlignment CP_Alignment
        {
            get => txtlimit.TextAlign;
            set => txtlimit.TextAlign = value;
        }

        /// <summary>
        /// 获取或设置输入数据长度
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        [Category("CMLAttribute"), Description("获取或设置输入数据长度")]
        public int CP_Length
        {
            get => txtlimit.CP_Length;
            set => txtlimit.CP_Length = value;
        }

        /// <summary>
        /// 获取或设置与此关联的密码掩码字符
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("获取或设置与此关联的密码掩码字符")]
        public char CP_PasswordChar
        {
            get => txtlimit.PasswordChar;
            set => txtlimit.PasswordChar = value;
        }

        /// <summary>
        /// 设置或获取禁用时的背景色
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("设置或获取禁用时的背景色")]
        public Color CP_DisableBackColor
        {
            get => m_colorDisableBack;
            set
            {
                m_colorDisableBack = value;

                ReColoredControl();
            }
        }

        /// <summary>
        /// 设置或获取只读时的背景色
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("设置或获取只读时的背景色")]
        public Color CP_ReadOnlyBackColor
        {
            get => m_colorReadOnlyBack;
            set
            {
                m_colorReadOnlyBack = value;

                ReColoredControl();
            }
        }

        /// <summary>
        /// 获取或设置控件显示的标题
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("获取或设置控件显示的标题")]
        public string CP_Title
        {
            get => lblTitle.Text;
            set
            {
                lblTitle.Text = value;

                ReInitControl();
            }
        }

        /// <summary>
        /// 获取或设置控件显示的标题的对齐方式
        /// </summary>
        [Browsable(true), DefaultValue(ContentAlignment.MiddleCenter)]
        [Category("CMLAttribute"), Description("获取或设置控件显示的标题的对齐方式")]
        public ETextAlignment CP_TitleAlignment
        {
            get
            {
                if (lblTitle.TextAlign == ContentAlignment.MiddleLeft)
                {
                    return ETextAlignment.Left;
                }
                else if (lblTitle.TextAlign == ContentAlignment.MiddleCenter)
                {
                    return ETextAlignment.Center;
                }
                else
                {
                    return ETextAlignment.Right;
                }
            }
            set
            {
                if (value == ETextAlignment.Left)
                {
                    lblTitle.TextAlign = ContentAlignment.MiddleLeft;
                }
                else if (value == ETextAlignment.Center)
                {
                    lblTitle.TextAlign = ContentAlignment.MiddleCenter;
                }
                else
                {
                    lblTitle.TextAlign = ContentAlignment.MiddleRight;
                }
            }
        }

        /// <summary>
        /// 获取或设置控件显示的标题宽度
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        [Category("CMLAttribute"), Description("获取或设置控件显示的标题宽度")]
        public int CP_TitleWidth
        {
            get => m_nTitleWidth;
            set
            {
                m_nTitleWidth = value;

                ReInitControl();
            }
        }

        /// <summary>
        /// 获取或设置控件显示的标题字体
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("获取或设置控件显示的标题字体")]
        public Font CP_TitleFont
        {
            get => lblTitle.Font;
            set
            {
                lblTitle.Font = value;

                ReInitControl();
            }
        }

        /// <summary>
        /// 设置或获取标题的前景色
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("设置或获取标题的前景色")]
        public Color CP_TitleForeColor
        {
            get => lblTitle.ForeColor;
            set => lblTitle.ForeColor = value;
        }

        /// <summary>
        /// 设置或获取标题的背景色
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("设置或获取标题的背景色")]
        public Color CP_TitleBackColor
        {
            get => lblTitle.BackColor;
            set => lblTitle.BackColor = value;
        }

        /// <summary>
        /// 获取或设置控件显示的单位
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("获取或设置控件显示的单位")]
        public string CP_Unit
        {
            get => lblUnit.Text;
            set
            {
                lblUnit.Text = value;

                ReInitControl();
            }
        }

        /// <summary>
        /// 获取或设置控件显示的单位的对齐方式
        /// </summary>
        [Browsable(true), DefaultValue(ContentAlignment.MiddleCenter)]
        [Category("CMLAttribute"), Description("获取或设置控件显示的单位的对齐方式")]
        public ETextAlignment CP_UnitAlignment
        {
            get
            {
                if (lblUnit.TextAlign == ContentAlignment.MiddleLeft)
                {
                    return ETextAlignment.Left;
                }
                else if (lblUnit.TextAlign == ContentAlignment.MiddleCenter)
                {
                    return ETextAlignment.Center;
                }
                else
                {
                    return ETextAlignment.Right;
                }
            }
            set
            {
                if (value == ETextAlignment.Left)
                {
                    lblUnit.TextAlign = ContentAlignment.MiddleLeft;
                }
                else if (value == ETextAlignment.Center)
                {
                    lblUnit.TextAlign = ContentAlignment.MiddleCenter;
                }
                else
                {
                    lblUnit.TextAlign = ContentAlignment.MiddleRight;
                }
            }
        }

        /// <summary>
        /// 获取或设置控件显示的单位宽度
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        [Category("CMLAttribute"), Description("获取或设置控件显示的单位宽度")]
        public int CP_UnitWidth
        {
            get => m_nUnitWidth;
            set
            {
                m_nUnitWidth = value;

                ReInitControl();
            }
        }

        /// <summary>
        /// 获取或设置控件显示的单位字体
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("获取或设置控件显示的单位字体")]
        public Font CP_UnitFont
        {
            get => lblUnit.Font;
            set
            {
                lblUnit.Font = value;

                ReInitControl();
            }
        }

        /// <summary>
        /// 设置或获取单位的前景色
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("设置或获取单位的前景色")]
        public Color CP_UnitForeColor
        {
            get => lblUnit.ForeColor;
            set => lblUnit.ForeColor = value;
        }

        /// <summary>
        /// 设置或获取单位的背景色
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("设置或获取单位的背景色")]
        public Color CP_UnitBackColor
        {
            get => lblUnit.BackColor;
            set => lblUnit.BackColor = value;
        }

        /// <summary>
        /// 获取或设置数据输入类型
        /// </summary>
        [Browsable(true), DefaultValue(EInputTypes.String)]
        [Category("CMLAttribute"), Description("获取或设置数据输入类型")]
        public EInputTypes CP_InputType
        {
            get => txtlimit.CP_InputType;
            set => txtlimit.CP_InputType = value;
        }

        /// <summary>
        /// 获取或设置数值型数据最大值
        /// </summary>
        [Browsable(true), DefaultValue(double.MaxValue)]
        [Category("CMLAttribute"), Description("获取或设置数值型数据最大值")]
        public double CP_Maximum
        {
            get => txtlimit.CP_Maximum;
            set => txtlimit.CP_Maximum = value;
        }

        /// <summary>
        /// 获取或设置数值型数据最小值
        /// </summary>
        [Browsable(true), DefaultValue(double.MinValue)]
        [Category("CMLAttribute"), Description("获取或设置数值型数据最小值")]
        public double CP_Minimum
        {
            get => txtlimit.CP_Minimum;
            set => txtlimit.CP_Minimum = value;
        }

        /// <summary>
        /// 设置或获取控件文本只读状态
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("设置或获取控件文本只读状态")]
        public bool CP_ReadOnly
        {
            get => txtlimit.ReadOnly;
            set => txtlimit.ReadOnly = value;
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public CmlTextBoxEx()
        {
            InitializeComponent();

            //初始化属性
            Text = string.Empty;
            BackColor = Color.White;
            ForeColor = Color.Black;
            CP_Unit = string.Empty;
            CP_CanTextFocus = true;

            ReInitControl();
            ReColoredControl();

            base.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint,
                true
            );
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 重新初始化控件
        /// </summary>
        private void ReInitControl()
        {
            //控件高度
            SizeF sim = CreateGraphics().MeasureString("0123456789", base.Font);
            Height = (int)sim.Height + 9;

            //标题属性
            lblTitleSplit.Visible = !string.IsNullOrEmpty(lblTitle.Text);

            if (string.IsNullOrEmpty(lblTitle.Text))
            {
                lblTitle.Width = 0;
            }
            else if (m_nTitleWidth != 0)
            {
                lblTitle.Width = m_nTitleWidth;
            }
            else
            {
                sim = CreateGraphics().MeasureString(lblTitle.Text.Replace(' ', '_'), lblTitle.Font);
                lblTitle.Width = (int)sim.Width + 10;
            }

            //单位属性
            lblUnitSplit.Visible = !string.IsNullOrEmpty(lblUnit.Text);

            if (string.IsNullOrEmpty(lblUnit.Text))
            {
                lblUnit.Width = 0;
            }
            else if (m_nUnitWidth != 0)
            {
                lblUnit.Width = m_nUnitWidth;
            }
            else
            {
                sim = CreateGraphics().MeasureString(lblUnit.Text.Replace(' ', '_'), lblUnit.Font);
                lblUnit.Width = (int)sim.Width + 10;
            }
        }

        private void ReColoredControl()
        {
            if (!Enabled)
            {
                base.BackColor = m_colorDisableBack;
                pnlValue.BackColor = m_colorDisableBack;
                txtlimit.BackColor = m_colorDisableBack;
            }
            else if (CP_ReadOnly)
            {
                base.BackColor = m_colorReadOnlyBack;
                pnlValue.BackColor = m_colorReadOnlyBack;
                txtlimit.BackColor = m_colorReadOnlyBack;
            }
            else
            {
                base.BackColor = m_colorNormalBack;
                pnlValue.BackColor = m_colorNormalBack;
                txtlimit.BackColor = m_colorNormalBack;
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 为控件设置输入焦点
        /// </summary>
        public void CF_Focus()
        {
            txtlimit.Focus();
        }

        /// <summary>
        /// 清除文本框控件中的所有字符
        /// </summary>
        public void CF_Clear()
        {
            txtlimit.Clear();
        }
        #endregion

        #region 控件事件
        private void Txtlimit_Enter(object sender, EventArgs e)
        {
            if (!CP_CanTextFocus)
            {
                lblTitle.Focus();
            }
        }

        private void Txtlimit_FontChanged(object sender, EventArgs e)
        {
            ReInitControl();
        }

        private void UnitTextBox_Resize(object sender, EventArgs e)
        {
            ReInitControl();
        }

        private void Txtlimit_ReadOnlyChanged(object sender, EventArgs e)
        {
            ReColoredControl();
        }

        private void Txtlimit_EnabledChanged(object sender, EventArgs e)
        {
            ReColoredControl();
        }

        private void UnitTextBox_Click(object sender, EventArgs e)
        {
            txtlimit.Focus();
        }

        private void PnlValue_Click(object sender, EventArgs e)
        {
            txtlimit.Focus();
        }

        private void LblUnit_MouseEnter(object sender, EventArgs e)
        {
            Cursor curTemp = Cursors.Default;

            if (CE_UnitClick != null)
            {
                curTemp = Cursors.Hand;
            }

            if (Cursor != curTemp)
            {
                Cursor = curTemp;
            }
        }

        private void LblUnit_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
        #endregion

        #region 自定义事件响应
        private void Txtlimit_KeyUp(object sender, KeyEventArgs e)
        {
            CE_KeyUp?.Invoke(sender, e);
        }

        private void Txtlimit_KeyDown(object sender, KeyEventArgs e)
        {
            CE_KeyDown?.Invoke(sender, e);
        }

        private void LblUnit_Click(object sender, EventArgs e)
        {
            CE_UnitClick?.Invoke(sender, e);
        }

        private void Txtlimit_TextChanged(object sender, EventArgs e)
        {
            CE_TextChanged?.Invoke(sender, e);
        }
        #endregion
    }
}
