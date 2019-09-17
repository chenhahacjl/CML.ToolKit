using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CML.CommonEx.DebugEx
{
    /// <summary>
    /// 调试模块选择窗体
    /// </summary>
    internal partial class FormModelSelect : Form
    {
        /// <summary>
        /// 模块列表
        /// </summary>
        private List<object> m_lstDebugModel = null;

        /// <summary>
        /// 选择模块序号
        /// </summary>
        public int SelectedIndex { get; private set; } = -1;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FormModelSelect(List<object> lstDebugModel)
        {
            InitializeComponent();

            m_lstDebugModel = lstDebugModel ?? new List<object>();
        }

        private void FormModelSelect_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < m_lstDebugModel.Count; i++)
            {
                dgvFunction.Rows.Add(
                    DebugOperate.GetProperty(m_lstDebugModel[i], "ModelName"),
                    DebugOperate.GetProperty(m_lstDebugModel[i], "ModelDesc")
                );
            }

            lblModelCount.Text = $"模块数量: {m_lstDebugModel.Count}";
            btnSelect.Enabled = m_lstDebugModel.Count != 0;
        }

        private void DgvFunction_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFunction.SelectedRows.Count == 0)
            {
                return;
            }

            SelectedIndex = dgvFunction.SelectedRows[0].Index;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            if (dgvFunction.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择模块！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SelectedIndex = dgvFunction.SelectedRows[0].Index;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
