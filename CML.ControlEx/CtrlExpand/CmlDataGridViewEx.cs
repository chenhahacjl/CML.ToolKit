using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace CML.ControlEx
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
        public string CP_ConfigPath { get; set; } = "";
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

            base.OnCreateControl();
        }

        /// <summary>
        /// 表格数据行添加事件
        /// </summary>
        /// <param name="e">事件数据</param>
        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                for (int i = 0; i < e.RowCount; i++)
                {
                    Rows[e.RowIndex + i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Rows[e.RowIndex + i].HeaderCell.Value = (e.RowIndex + i + 1).ToString();
                }

                for (int i = e.RowIndex + e.RowCount; i < Rows.Count; i++)
                {
                    Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Rows[i].HeaderCell.Value = (i + 1).ToString();
                }
            }
            catch { }

            base.OnRowsAdded(e);
        }

        /// <summary>
        /// 表格数据行删除事件
        /// </summary>
        /// <param name="e">事件数据</param>
        protected override void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                for (int i = 0; i < e.RowCount; i++)
                {
                    Rows[e.RowIndex + i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Rows[e.RowIndex + i].HeaderCell.Value = (e.RowIndex + i + 1).ToString();
                }

                for (int i = e.RowIndex + e.RowCount; i < Rows.Count; i++)
                {
                    Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Rows[i].HeaderCell.Value = (i + 1).ToString();
                }
            }
            catch { }

            base.OnRowsRemoved(e);
        }

        /// <summary>
        /// 表格排序事件
        /// </summary>
        /// <param name="e">事件数据</param>
        protected override void OnSorted(EventArgs e)
        {
            try
            {
                for (int i = 0; i < Rows.Count; i++)
                {
                    Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Rows[i].HeaderCell.Value = (i + 1).ToString();
                }
            }
            catch { }

            base.OnSorted(e);
        }

        /// <summary>
        /// 单元格鼠标单击事件
        /// </summary>
        /// <param name="e">事件数据</param>
        protected override void OnCellMouseClick(DataGridViewCellMouseEventArgs e)
        {
            //右键事件
            if (e.Button == MouseButtons.Right)
            {
                //标题行
                if (e.RowIndex == -1)
                {
                    ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                    contextMenuStrip.Items.Add("设置表格显示列", null, (X, Y) => new FormDgvColumnVisibility(CP_ConfigPath, this).ShowDialog());
                    contextMenuStrip.Show(this, PointToClient(Cursor.Position));
                }
                else if (e.ColumnIndex != -1)
                {
                    object obj = Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    string value = obj?.ToString();

                    if (!string.IsNullOrEmpty(value))
                    {
                        //将单元格内容设置到剪贴板
                        Thread thread = new Thread(() =>
                        {
                            Clipboard.SetText(value);
                        })
                        { IsBackground = true };

                        //将当前线程设置为单个线程单元(STA)模式方可进行OLE调用。
                        _ = thread.TrySetApartmentState(ApartmentState.STA);
                        thread.Start();
                    }
                }
            }
            else if (e.RowIndex != -1 && e.ColumnIndex != -1 && Columns[e.ColumnIndex].CellType == typeof(DataGridViewCheckBoxCell))
            {
                if (Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || Rows[e.RowIndex].Cells[e.ColumnIndex].Value == DBNull.Value)
                {
                    Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                }
                else
                {
                    Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !(bool)Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                }

                EndEdit();
            }

            base.OnCellMouseClick(e);
        }

        /// <summary>
        /// 单元格鼠标双击事件
        /// </summary>
        /// <param name="e">事件数据</param>
        protected override void OnCellMouseDoubleClick(DataGridViewCellMouseEventArgs e)
        {
            //判断行数与列数不为空、是否为标题行的第一列、第一列类型是否为CheckBoxCell
            if ((RowCount != 0 && ColumnCount != 0) && (e.RowIndex == -1 && e.ColumnIndex == 0) && (Columns[0].CellType == typeof(DataGridViewCheckBoxCell)))
            {
                FormDgvRowCheckSelect checkSelect = new FormDgvRowCheckSelect()
                {
                    RowCount = RowCount
                };
                if (checkSelect.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < Rows.Count; i++)
                    {
                        Rows[i].Cells[0].Value = i >= checkSelect.RowIndexMin && i <= checkSelect.RowIndexMax;
                    }
                }

                EndEdit();
            }

            base.OnCellMouseDoubleClick(e);
        }
        #endregion
    }
}
