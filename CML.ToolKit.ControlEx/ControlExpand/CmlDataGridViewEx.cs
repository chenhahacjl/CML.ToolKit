using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 表格控件
    /// </summary>
    [ToolboxItem(true)]
    [DefaultEvent("CellContentClick")]
    [DefaultBindingProperty("CP_ConfigPath"), DefaultProperty("CP_ConfigPath")]
    public class CmlDataGridViewEx : DataGridView
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置配置文件路径
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("CMLAttribute"), Description("获取或设置配置文件路径")]
        public string CP_ConfigPath { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public CmlDataGridViewEx()
        {
            DefaultCellStyle.SelectionBackColor = Color.LightGray;
            DefaultCellStyle.SelectionForeColor = Color.Black;
        }
        #endregion

        #region 重写事件
        /// <summary>
        /// 控件创建事件
        /// </summary>
        protected override void OnCreateControl()
        {
            //判断配置文件是否存在
            if (!File.Exists(CP_ConfigPath))
            {
                return;
            }

            //读取配置
            Dictionary<string, bool> columns = DataGridViewOperate.LoadColumnVisibility(CP_ConfigPath, Name);

            //应用配置
            foreach (string key in columns.Keys)
            {
                if (Columns.Contains(key))
                {
                    Columns[key].Visible = columns[key];
                }
            }
        }

        /// <summary>
        /// 单元格鼠标单击事件
        /// </summary>
        /// <param name="e">事件数据</param>
        protected override void OnCellMouseClick(DataGridViewCellMouseEventArgs e)
        {
            //标题右键
            if (e.RowIndex == -1 && e.Button == MouseButtons.Right)
            {
                //菜单
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                contextMenuStrip.Items.Add("设置表格显示列", null, (X, Y) => new FormDgvColumnVisibility(CP_ConfigPath, this).ShowDialog());
                contextMenuStrip.Show(this, PointToClient(Cursor.Position));
            }

            base.OnCellMouseClick(e);
        }

        /// <summary>
        /// 单元格鼠标双击事件
        /// </summary>
        /// <param name="e">事件数据</param>
        protected override void OnCellMouseDoubleClick(DataGridViewCellMouseEventArgs e)
        {
            //判断第一列是否为CheckBoxCell
            if (e.RowIndex != -1 || e.ColumnIndex != 0 ||
                RowCount == 0 || ColumnCount == 0 ||
                Columns[0].CellType != typeof(DataGridViewCheckBoxCell))
            {
                return;
            }

            //Check选择
            FormDgvRowCheckSelect checkSelect = new FormDgvRowCheckSelect()
            {
                RowCount = RowCount
            };
            if (checkSelect.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            for (int i = 0; i < Rows.Count; i++)
            {
                Rows[i].Cells[0].Value = i >= checkSelect.RowIndexMin && i <= checkSelect.RowIndexMax;
            }

            EndEdit();
        }
        #endregion
    }
}
