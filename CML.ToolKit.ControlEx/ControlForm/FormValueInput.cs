using System;
using System.Windows.Forms;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 数值输入窗体
    /// </summary>
    public partial class FormValueInput : Form
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置控件显示的标题文本
        /// </summary>
        public string CP_Title
        {
            get => Text;
            set => Text = value;
        }

        /// <summary>
        /// 获取或设置控件显示的描述内容
        /// </summary>
        public string CP_Description
        {
            get => lnlDescription.Text;
            set => lnlDescription.Text = value;
        }

        /// <summary>
        /// 获取用户输入的内容
        /// </summary>
        public double CP_Value
        {
            get => double.TryParse(utxtValue.Text, out double dNum) ? dNum : 0;
            set => utxtValue.Text = value.ToString();
        }

        /// <summary>
        /// 获取或设置数据输入类型
        /// </summary>
        public EInputTypes CP_InputType
        {
            get => utxtValue.CP_InputType;
            set => utxtValue.CP_InputType = value;
        }

        /// <summary>
        /// 获取或设置数值型数据最大值
        /// </summary>
        public double CP_Maximum
        {
            get => utxtValue.CP_Maximum;
            set => utxtValue.CP_Maximum = value;
        }

        /// <summary>
        /// 获取或设置数值型数据最小值
        /// </summary>
        public double CP_Minimum
        {
            get => utxtValue.CP_Minimum;
            set => utxtValue.CP_Minimum = value;
        }

        /// <summary>
        /// 获取或设置控件显示的单位
        /// </summary>
        public string CP_Unit
        {
            get => utxtValue.CP_Unit;
            set => utxtValue.CP_Unit = value;
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public FormValueInput()
        {
            InitializeComponent();

            Icon = FindForm().Icon;
        }
        #endregion

        #region 窗体事件
        private void ValueInputForm_Shown(object sender, EventArgs e)
        {
            utxtValue.Focus();
            KeyPreview = true;
        }

        private void ValueInputForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BtnCancel_Click(null, null);
            }
        }
        #endregion

        #region 按钮事件
        private void BtnEnter_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ValueInputForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnEnter_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                BtnCancel_Click(null, null);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
    }
}
