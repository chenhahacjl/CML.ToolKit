using System;
using System.Drawing;
using System.Windows.Forms;

namespace CML.ControlEx
{
    /// <summary>
    /// Check数据行选择窗体（内部使用）
    /// </summary>
    internal partial class FormDgvRowCheckSelect : Form
    {
        #region 私有变量
        private int m_nRowCount = 0;
        #endregion

        #region 公共属性
        /// <summary>
        /// 表格行数
        /// </summary>
        public int RowCount
        {
            get => m_nRowCount;
            set
            {
                m_nRowCount = value < 1 ? 1 : value;

                txtIndexMin.CP_Maximum = m_nRowCount;
                txtIndexMax.CP_Maximum = m_nRowCount;
                lblRange.Text = $"（范围1-{m_nRowCount}）";
            }
        }

        /// <summary>
        /// 选择行开始序号（0 -> N）
        /// </summary>
        public int RowIndexMin { get; private set; } = 0;

        /// <summary>
        /// 选择行结束序号（RowIndexMin -> N）
        /// </summary>
        public int RowIndexMax { get; private set; } = 0;
        #endregion

        #region 构造函数
        public FormDgvRowCheckSelect()
        {
            InitializeComponent();

            Icon = FindForm().Icon;
            DialogResult = DialogResult.Cancel;
        }
        #endregion

        #region 窗体事件
        private void FormDgvRowCheckSelect_Shown(object sender, EventArgs e)
        {
            txtIndexMin.Focus();
        }
        #endregion

        #region 按钮事件
        private void BtnClear_Click(object sender, EventArgs e)
        {
            RowIndexMin = -1;
            RowIndexMax = -1;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnAll_Click(object sender, EventArgs e)
        {
            RowIndexMin = 0;
            RowIndexMax = RowCount - 1;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnEnter_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(string.IsNullOrEmpty(txtIndexMin.Text) ? "1" : txtIndexMin.Text, out int indexMin) ||
                !int.TryParse(string.IsNullOrEmpty(txtIndexMax.Text) ? $"{RowCount}" : txtIndexMax.Text, out int indexMax))
            {
                MessageBox.Show("数值输入错误，请重新输入！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (indexMin > indexMax)
            {
                MessageBox.Show("下界范围大于上届范围，请重新输入！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RowIndexMin = indexMin - 1;
            RowIndexMax = indexMax - 1;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion
    }
}
