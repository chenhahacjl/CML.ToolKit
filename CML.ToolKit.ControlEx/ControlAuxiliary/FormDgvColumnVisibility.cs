using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 显示数据列选择窗体（内部使用）
    /// </summary>
    internal partial class FormDgvColumnVisibility : Form
    {
        #region 私有变量
        /// <summary>
        /// 配置文件
        /// </summary>
        private readonly string m_ConfigFile = "";
        /// <summary>
        /// 表格对象
        /// </summary>
        private readonly DataGridView m_DataGridView = null;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cfgFile">配置文件</param>
        /// <param name="dataGridView">表格控件</param>
        public FormDgvColumnVisibility(string cfgFile, DataGridView dataGridView)
        {
            InitializeComponent();

            Icon = FindForm().Icon;
            m_ConfigFile = cfgFile;
            m_DataGridView = dataGridView;
        }
        #endregion

        #region 窗体事件
        private void FormDgvColumnVisibility_Shown(object sender, EventArgs e)
        {
            if (m_DataGridView == null) return;

            for (int i = 0; i < m_DataGridView.Columns.Count; i++)
            {
                chksColumns.Items.Add(m_DataGridView.Columns[i].HeaderText);
                chksColumns.SetItemChecked(i, m_DataGridView.Columns[i].Visible);
            }
        }

        private void FormDgvColumnVisibility_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!m_DataGridView.RowHeadersVisible && m_DataGridView.DisplayedColumnCount(true) == 0)
            {
                //判断配置文件是否存在
                if (!File.Exists(m_ConfigFile))
                {
                    for (int i = 0; i < m_DataGridView.Columns.Count; i++)
                    {
                        m_DataGridView.Columns[i].Visible = true;
                    }
                }
                else
                {
                    //读取配置
                    Dictionary<string, bool> columns = DataGridViewOperate.LoadColumnVisibility(m_ConfigFile, m_DataGridView.Name);

                    //应用配置
                    foreach (string key in columns.Keys)
                    {
                        if (m_DataGridView.Columns.Contains(key))
                        {
                            m_DataGridView.Columns[key].Visible = columns[key];
                        }
                    }
                }
            }

            //保存
            Dictionary<string, bool> visibility = new Dictionary<string, bool>();
            for (int i = 0; i < m_DataGridView.Columns.Count; i++)
            {
                visibility.Add(m_DataGridView.Columns[i].Name, m_DataGridView.Columns[i].Visible);
            }
            DataGridViewOperate.SaveColumnVisibility(m_ConfigFile, m_DataGridView.Name, visibility);
        }
        #endregion

        #region 控件事件
        private void ChksColumns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            m_DataGridView.Columns[e.Index].Visible = e.NewValue == CheckState.Checked;
        }

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否显示全部数据列？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                return;
            }

            for (int i = 0; i < chksColumns.Items.Count; i++)
            {
                chksColumns.SetItemChecked(i, true);
            }
        }

        private void BtnSelectNone_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否隐藏全部数据列？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                return;
            }

            for (int i = 0; i < chksColumns.Items.Count; i++)
            {
                chksColumns.SetItemChecked(i, false);
            }
        }

        private void BtnEnter_Click(object sender, EventArgs e)
        {
            if (!m_DataGridView.RowHeadersVisible && m_DataGridView.DisplayedColumnCount(true) == 0)
            {
                MessageBox.Show("行标题不可见时无法隐藏所有行！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Close();
        }
        #endregion
    }
}
