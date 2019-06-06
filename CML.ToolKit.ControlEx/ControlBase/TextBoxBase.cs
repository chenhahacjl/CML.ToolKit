using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 文本框控件基类
    /// </summary>
    [ToolboxItem(false)]
    [DefaultBindingProperty("Text"), DefaultProperty("Text")]
    internal class TextBoxBase : TextBox
    {
        #region 私有变量
        /// <summary>
        /// 输入最小值
        /// </summary>
        private double m_dMinimum = -65535;
        /// <summary>
        /// 输入最大值
        /// </summary>
        private double m_dMaximum = 65535;
        /// <summary>
        /// 输入长度
        /// </summary>
        private int m_nLength = 0;
        #endregion

        #region 重写属性
        /// <summary>
        /// 获取或设置与此关联的文本
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("Appearance"), Description("获取或设置与此关联的文本")]
        public override string Text
        {
            get => base.Text;
            set
            {
                if (IsValueValid(value, true))
                {
                    base.Text = value;
                }
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置数据输入类型
        /// </summary>
        [Browsable(true), DefaultValue(EInputTypes.String)]
        [Category("CMLAttribute"), Description("获取或设置数据输入类型")]
        public EInputTypes CP_InputType { get; set; }

        /// <summary>
        /// 获取或设置数值型数据最大值
        /// </summary>
        [Browsable(true), DefaultValue(65535D)]
        [Category("CMLAttribute"), Description("获取或设置数值型数据最大值")]
        public double CP_Maximum
        {
            get => m_dMaximum;
            set
            {
                if (value < m_dMinimum)
                {
                    m_dMaximum = m_dMinimum;
                }
                else
                {
                    m_dMaximum = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置数值型数据最小值
        /// </summary>
        [Browsable(true), DefaultValue(-65535D)]
        [Category("CMLAttribute"), Description("获取或设置数值型数据最小值")]
        public double CP_Minimum
        {
            get => m_dMinimum;
            set
            {
                if (value > m_dMaximum)
                {
                    m_dMinimum = m_dMaximum;
                }
                else
                {
                    m_dMinimum = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置输入数据长度
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        [Category("CMLAttribute"), Description("获取或设置输入数据长度")]
        public int CP_Length
        {
            get => m_nLength;
            set => m_nLength = value < 0 ? 0 : value;
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public TextBoxBase()
        {
            //去除右键菜单
            ContextMenu = new ContextMenu();

            //初始化属性值
            base.Text = string.Empty;
            CP_InputType = EInputTypes.String;
        }
        #endregion

        #region 重写方法
        /// <summary>
        /// 处理命令键
        /// </summary>
        /// <param name="m">通过引用传递的 System.Windows.Forms.Message，表示要处理的窗口消息。</param>
        /// <param name="keyData">System.Windows.Forms.Keys 值之一，表示要处理的快捷键。</param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message m, Keys keyData)
        {
            if (keyData == (Keys)Shortcut.CtrlV || keyData == (Keys)Shortcut.ShiftIns)
            {
                IDataObject iData = Clipboard.GetDataObject();

                string strNewText =
                    base.Text.Substring(0, base.SelectionStart) +
                    (string)iData.GetData(DataFormats.Text) +
                    base.Text.Substring(base.SelectionStart +
                    base.SelectionLength);

                if (!IsValueValid(strNewText, true))
                {
                    return true;
                }
            }

            return base.ProcessCmdKey(ref m, keyData);
        }

        /// <summary>
        /// 引发 System.Windows.Forms.Control.KeyPress 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.Windows.Forms.KeyPressEventArgs。</param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (e.KeyChar.ToString() == " ")
                {
                    e.Handled = true;
                    return;
                }

                string strNewText =
                    base.Text.Substring(0, base.SelectionStart) +
                    e.KeyChar.ToString() +
                    base.Text.Substring(base.SelectionStart + base.SelectionLength);

                if (!IsValueValid(strNewText, true))
                {
                    e.Handled = true;
                }
            }

            base.OnKeyPress(e);
        }

        /// <summary>
        /// 引发 System.Windows.Forms.Control.Leave 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs。</param>
        protected override void OnLeave(EventArgs e)
        {
            if (CP_InputType != EInputTypes.String && base.Text != string.Empty)
            {
                if (!IsValueValid(base.Text, false))
                {
                    base.Text = "";
                }
                else if (double.Parse(base.Text) == 0)
                {
                    base.Text = "0";
                }
            }

            base.OnLeave(e);
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 判断输入数值是否有效
        /// </summary>
        /// <param name="strValue">输入数值</param>
        /// <param name="bUse">是否被使用</param>
        /// <returns>判断结果</returns>
        private bool IsValueValid(string strValue, bool bUse)
        {
            bool bResult = true;

            if (string.IsNullOrEmpty(strValue))
            {
                return true;
            }

            if (strValue.Length > m_nLength && m_nLength != 0)
            {
                return false;
            }

            if (CP_InputType == EInputTypes.String)
            {
                return true;
            }

            if (bUse && strValue.Equals("-") && m_dMinimum < 0)
            {
                return true;
            }

            if (strValue.Contains(","))
            {
                return false;
            }

            try
            {
                if (CP_InputType == EInputTypes.Double)
                {
                    if (!double.TryParse(strValue, out double dNum))
                    {
                        bResult = false;
                    }
                    else if (dNum < m_dMinimum || dNum > m_dMaximum)
                    {
                        bResult = false;
                    }
                }
                else if (CP_InputType == EInputTypes.Int)
                {
                    if (!int.TryParse(strValue, out int nNum))
                    {
                        bResult = false;
                    }
                    else if (nNum < m_dMinimum || nNum > m_dMaximum)
                    {
                        bResult = false;
                    }
                }
            }
            catch { bResult = false; }

            return bResult;
        }
        #endregion
    }
}
