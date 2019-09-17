using CML.CommonEx.RegexEx.ExFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CML.CommonEx.DebugEx
{
    /// <summary>
    /// 通用调试模块窗体
    /// </summary>
    internal partial class FormCommonDebugModel : Form
    {
        #region 私有变量
        /// <summary>
        /// 项目实例
        /// </summary>
        private readonly object m_projectObject = null;
        #endregion

        #region 私有方法
        /// <summary>
        /// 初始化信息
        /// </summary>
        private void InitInfomation()
        {
            //判断项目实例是否为空
            if (m_projectObject == null) { return; }

            //初始化表格
            DataTable dtControl = new DataTable();
            dtControl.Columns.Add("Name", Type.GetType("System.String"));
            dtControl.Columns.Add("Text", Type.GetType("System.String"));
            dtControl.Columns.Add("Object", Type.GetType("System.Object"));

            //判断是否为控件
            if (m_projectObject is Control)
            {
                //设置表格内容
                LoadControl(dtControl, m_projectObject as Control);
            }

            //设置显示格式
            dgvControl.DataSource = dtControl;
            dgvControl.Columns["Name"].MinimumWidth = 100;
            dgvControl.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvControl.Columns["Text"].MinimumWidth = 100;
            dgvControl.Columns["Text"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvControl.Columns["Object"].Visible = false;

            //初始化表格
            DataTable dtFieldProperty = new DataTable();
            dtFieldProperty.Columns.Add("类型", Type.GetType("System.String"));
            dtFieldProperty.Columns.Add("数据类型", Type.GetType("System.String"));
            dtFieldProperty.Columns.Add("Name", Type.GetType("System.String"));
            dtFieldProperty.Columns.Add("Value", Type.GetType("System.Object"));

            //加载变量
            FieldInfo[] fields = m_projectObject.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var item in fields)
            {
                if (!typeof(Control).IsAssignableFrom(item.FieldType))
                {
                    dtFieldProperty.Rows.Add("变量", item.FieldType.Name, item.Name, item.GetValue(m_projectObject));
                }
            }

            //加载属性
            PropertyInfo[] properties = m_projectObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var item in properties)
            {
                dtFieldProperty.Rows.Add("属性", item.PropertyType.Name, item.Name, item.GetValue(m_projectObject, null));
            }
            properties = m_projectObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var item in properties)
            {
                dtFieldProperty.Rows.Add("属性", item.PropertyType.Name, item.Name, item.GetValue(m_projectObject, null));
            }

            //设置显示格式
            dgvFieldProperty.DataSource = dtFieldProperty;
            dgvFieldProperty.Columns["类型"].MinimumWidth = 100;
            dgvFieldProperty.Columns["类型"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvFieldProperty.Columns["数据类型"].MinimumWidth = 100;
            dgvFieldProperty.Columns["数据类型"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvFieldProperty.Columns["Name"].MinimumWidth = 100;
            dgvFieldProperty.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvFieldProperty.Columns["Value"].MinimumWidth = 100;
            dgvFieldProperty.Columns["Value"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        /// <summary>
        /// 加载控件
        /// </summary>
        private void LoadControl(DataTable dataTable, Control control)
        {
            dataTable.Rows.Add(control.Name, control.Text, control);

            foreach (Control item in control.Controls)
            {
                if (item is Panel || item is GroupBox || item is TabControl || item is ContainerControl)
                {
                    LoadControl(dataTable, item);
                }
                else
                {
                    dataTable.Rows.Add(item.Name, item.Text, item);
                }
            }
        }

        /// <summary>
        /// 加载变量
        /// </summary>
        /// <param name="name"></param>
        private void LoadField(string name)
        {
            FieldInfo field = m_projectObject.GetType().GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (field == null)
            {
                txtFieldName.Clear();
                txtFieldFullName.Clear();
                txtFieldStatic.Clear();
                txtFieldPublic.Clear();
                txtFieldPrivate.Clear();
                txtFieldReadOnly.Clear();
                txtFieldDataType.Clear();
                txtFieldToken.Clear();
                txtFieldValue.Clear();
            }
            else
            {
                txtFieldName.Text = field.Name;
                txtFieldFullName.Text = field.DeclaringType.FullName + "." + field.Name;
                txtFieldStatic.Text = field.IsStatic.ToString();
                txtFieldPublic.Text = field.IsPrivate.ToString();
                txtFieldPrivate.Text = field.IsPublic.ToString();
                txtFieldReadOnly.Text = field.IsInitOnly.ToString();
                txtFieldDataType.Text = field.FieldType.FullName;
                txtFieldToken.Text = field.MetadataToken.ToString();
                txtFieldValue.Text = field.GetValue(m_projectObject)?.ToString();
            }
        }

        /// <summary>
        /// 加载属性
        /// </summary>
        /// <param name="name"></param>
        private void LoadProperty(string name)
        {
            bool isPublic = true;
            PropertyInfo property = m_projectObject.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public);
            if (property == null)
            {
                isPublic = false;
                property = m_projectObject.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.NonPublic);
            }

            if (property == null)
            {
                txtPropertyName.Clear();
                txtPropertyFullName.Clear();
                txtPropertyPublic.Clear();
                txtPropertyPGet.Clear();
                txtPropertyNPGet.Clear();
                txtPropertyPSet.Clear();
                txtPropertyNPSet.Clear();
                txtPropertyCanRead.Clear();
                txtPropertyCanWrite.Clear();
                txtPropertyDataType.Clear();
                txtPropertyToken.Clear();
                txtPropertyValue.Clear();
            }
            else
            {
                txtPropertyName.Text = property.Name;
                txtPropertyFullName.Text = property.DeclaringType.FullName + "." + property.Name;
                txtPropertyPublic.Text = isPublic ? "Public" : "NonPublic";
                txtPropertyPGet.Text = CreateFunctionString(property.GetGetMethod());
                txtPropertyNPGet.Text = CreateFunctionString(property.GetGetMethod(true));
                txtPropertyPSet.Text = CreateFunctionString(property.GetSetMethod());
                txtPropertyNPSet.Text = CreateFunctionString(property.GetSetMethod(true));
                txtPropertyCanRead.Text = property.CanRead.ToString();
                txtPropertyCanWrite.Text = property.CanWrite.ToString();
                txtPropertyDataType.Text = property.PropertyType.FullName;
                txtPropertyToken.Text = property.MetadataToken.ToString();
                txtPropertyValue.Text = property.GetValue(m_projectObject, null)?.ToString();
            }
        }

        /// <summary>
        /// 创建方法字符串
        /// </summary>
        /// <param name="methodInfo">方法变量</param>
        /// <returns>方法字符串</returns>
        private string CreateFunctionString(MethodInfo methodInfo)
        {
            if (methodInfo == null) { return "Null"; }

            string function = methodInfo.IsPublic ? "public " : methodInfo.IsPrivate ? "private " : "internal ";
            function += methodInfo.IsStatic ? "static " : "";
            function += methodInfo.ReturnType.Name + " ";
            function += methodInfo.Name + "(";

            List<ParameterInfo> parameters = new List<ParameterInfo>(methodInfo.GetParameters());
            for (int i = 0; i < parameters.Count; i++)
            {
                ParameterInfo parameter = parameters.FirstOrDefault(item => item.Position == i);
                if (parameter != null)
                {
                    function += parameter.ParameterType.Name + " " + parameter.Name + ",";
                }
            }

            function = function.TrimEnd(',') + ")";

            return function;
        }

        /// <summary>
        /// 设置变量/属性值
        /// </summary>
        /// <param name="type">变量/属性</param>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <returns>执行结果</returns>
        private bool SetFieldPropertyValue(string type, string name, object value)
        {
            string errMsg;
            if (type == "变量")
            {
                DebugOperate.SetField(m_projectObject, name, value, out errMsg);
            }
            else
            {
                DebugOperate.SetProperty(m_projectObject, name, value, out errMsg);
            }

            if (string.IsNullOrEmpty(errMsg))
            {
                dgvFieldProperty.SelectedRows[0].Cells["Value"].Value = value;
                return true;
            }
            else
            {
                MessageBox.Show($"数值修改失败: {errMsg}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region 构造函数
        public FormCommonDebugModel(object projectObject)
        {
            InitializeComponent();

            m_projectObject = projectObject;
        }
        #endregion

        #region 窗体事件
        private void FormCommonDebugModel_Load(object sender, EventArgs e)
        {
            InitInfomation();
        }
        #endregion

        #region 控件标签页事件
        private void TxtControlSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtControlSearch.Text) && e.KeyCode == Keys.Enter)
            {
                int index = 969696;
                for (int i = 0; i < dgvControl.Rows.Count; i++)
                {
                    string name = dgvControl.Rows[i].Cells["Name"].Value as string;
                    if (name.ToUpper().Contains(txtControlSearch.Text.ToUpper()) ||
                        RegexOperateEF.CF_IsMatch(name, txtControlSearch.Text))
                    {
                        if (index > i) index = i;
                        dgvControl.Rows[i].Cells["Name"].Style.BackColor = Color.PaleGreen;
                    }
                    else
                    {
                        dgvControl.Rows[i].Cells["Name"].Style.BackColor = Color.White;
                    }
                }
                if (index == 969696)
                {
                    dgvControl.ClearSelection();
                }
                else
                {
                    dgvControl.Rows[index].Cells[0].Selected = true;
                    dgvControl.CurrentCell = dgvControl.Rows[index].Cells[0];
                }
            }
        }

        private void DgvControl_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvControl.Rows.Count == 0 || dgvControl.SelectedRows.Count == 0)
            {
                propertyGrid.SelectedObject = null;
                return;
            }

            propertyGrid.SelectedObject = dgvControl.SelectedRows[0].Cells[2].Value;
        }
        #endregion

        #region 变量/属性标签页事件
        private void TxtFieldPropertySearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFieldPropertySearch.Text) && e.KeyCode == Keys.Enter)
            {
                int index = 969696;
                for (int i = 0; i < dgvFieldProperty.Rows.Count; i++)
                {
                    string name = dgvFieldProperty.Rows[i].Cells["Name"].Value as string;
                    if (name.ToUpper().Contains(txtFieldPropertySearch.Text.ToUpper()) ||
                        RegexOperateEF.CF_IsMatch(name, txtFieldPropertySearch.Text))
                    {
                        if (index > i) index = i;
                        dgvFieldProperty.Rows[i].Cells["Name"].Style.BackColor = Color.PaleGreen;
                    }
                    else
                    {
                        dgvFieldProperty.Rows[i].Cells["Name"].Style.BackColor = Color.White;
                    }
                }
                if (index == 969696)
                {
                    dgvFieldProperty.ClearSelection();
                }
                else
                {
                    dgvFieldProperty.Rows[index].Cells[0].Selected = true;
                    dgvFieldProperty.CurrentCell = dgvControl.Rows[index].Cells[0];
                }
            }
        }

        private void DgvFieldProperty_SelectionChanged(object sender, EventArgs e)
        {
            //隐藏所有窗体
            pnlNoSelect.Visible = false;
            pnlField.Visible = false;
            pnlProperty.Visible = false;
            pnlMdfFPOther.Visible = false;
            pnlMdfFPBoolean.Visible = false;
            pnlMdfFPString.Visible = false;
            pnlMdfFPNumber.Visible = false;
            pnlMdfFPDateTime.Visible = false;

            //判断是否选择行
            if (dgvFieldProperty.SelectedRows.Count == 0)
            {
                pnlNoSelect.Visible = true;
                pnlMdfFPOther.Visible = true;
                return;
            }

            //类型判断
            if (dgvFieldProperty.SelectedRows[0].Cells["类型"].Value as string == "变量")
            {
                LoadField(dgvFieldProperty.SelectedRows[0].Cells["Name"].Value as string);
                pnlField.Visible = true;
            }
            else
            {
                LoadProperty(dgvFieldProperty.SelectedRows[0].Cells["Name"].Value as string);
                pnlProperty.Visible = true;
            }

            //数据
            Type dataType = dgvFieldProperty.SelectedRows[0].Cells["Value"].Value.GetType();
            object value = dgvFieldProperty.SelectedRows[0].Cells["Value"].Value;

            //根据不同类型做不同操作
            switch (dataType.Name)
            {
                case "Boolean":
                {
                    pnlMdfFPBoolean.Visible = true;
                    rbMdfFPBooleanT.Checked = Convert.ToBoolean(value);
                    rbMdfFPBooleanF.Checked = !Convert.ToBoolean(value);
                    break;
                }
                case "String":
                {
                    pnlMdfFPString.Visible = true;
                    txtMdfFPString.Text = value as string;
                    chkMdfFPStringNull.Checked = value is null;
                    break;
                }
                case "Char":
                case "Byte":
                case "SByte":
                case "Int16":
                case "Int32":
                case "Int64":
                case "UInt16":
                case "UInt32":
                case "UInt64":
                case "Single":
                case "Double":
                case "Decimal":
                {
                    pnlMdfFPNumber.Visible = true;
                    txtMdfFPNumber.Tag = dataType.Name;
                    txtMdfFPNumber.Text = value.ToString();
                    break;
                }
                case "DateTime":
                {
                    pnlMdfFPDateTime.Visible = true;
                    txtMdfFPDateTime.Text = Convert.ToDateTime(value).ToString("yyyy/MM/dd HH:mm:ss");
                    if (Convert.ToDateTime(value) < dtpMdfFPDateTimeDate.MinDate || Convert.ToDateTime(value) > dtpMdfFPDateTimeDate.MaxDate)
                    {
                        dtpMdfFPDateTimeDate.Value = Convert.ToDateTime(DateTime.Now);
                        dtpMdfFPDateTimeTime.Value = Convert.ToDateTime(DateTime.Now);
                    }
                    else
                    {
                        dtpMdfFPDateTimeDate.Value = Convert.ToDateTime(value);
                        dtpMdfFPDateTimeTime.Value = Convert.ToDateTime(value);
                    }
                    break;
                }
                default:
                {
                    pnlMdfFPOther.Visible = true;
                    break;
                }
            }
        }

        private void BtnMdfFPBoolean_Click(object sender, EventArgs e)
        {
            object value = rbMdfFPBooleanT.Checked;
            string type = dgvFieldProperty.SelectedRows[0].Cells["类型"].Value as string;
            string name = dgvFieldProperty.SelectedRows[0].Cells["Name"].Value as string;

            SetFieldPropertyValue(type, name, value);
        }

        private void BtnMdfFPStringMaxSize_Click(object sender, EventArgs e)
        {
            using (Form form = new Form()
            {
                Text = "数值修改",
                ShowIcon = false,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.CenterScreen,
                Font = new Font(Font.FontFamily, 12),
                Size = new Size(500, 500),
            })
            {
                TextBox textBox = new TextBox
                {
                    Multiline = true,
                    Dock = DockStyle.Fill,
                    Text = txtMdfFPString.Text,
                    ScrollBars = ScrollBars.Both,
                };

                Button btnOK = new Button()
                {
                    Height = 30,
                    Text = "确定",
                    Dock = DockStyle.Bottom,
                };
                btnOK.Click += (__, _) =>
                {
                    txtMdfFPString.Text = textBox.Text;
                    form.Close();
                };

                Button btnCancel = new Button()
                {
                    Height = 30,
                    Text = "取消",
                    Dock = DockStyle.Bottom,
                };
                btnCancel.Click += (__, _) => form.Close();

                form.Shown += (__, _) => btnCancel.Focus();
                form.Controls.Add(textBox);
                form.Controls.Add(btnOK);
                form.Controls.Add(btnCancel);
                form.ShowDialog();
            }
        }

        private void ChkMdfFPStringNull_CheckedChanged(object sender, EventArgs e)
        {
            txtMdfFPString.Enabled = !chkMdfFPStringNull.Checked;
            btnMdfFPStringMaxSize.Enabled = !chkMdfFPStringNull.Checked;
        }

        private void BtnMdfFPString_Click(object sender, EventArgs e)
        {
            object value = chkMdfFPStringNull.Checked ? null : txtMdfFPString.Text;
            string type = dgvFieldProperty.SelectedRows[0].Cells["类型"].Value as string;
            string name = dgvFieldProperty.SelectedRows[0].Cells["Name"].Value as string;

            SetFieldPropertyValue(type, name, value);
        }

        private void BtnMdfFPMumber_Click(object sender, EventArgs e)
        {
            object value = null;

            switch (txtMdfFPNumber.Tag as string)
            {
                case "Char":
                {
                    value = char.TryParse(txtMdfFPNumber.Text, out char o) ? o as object : null;
                    break;
                }
                case "Byte":
                {
                    value = byte.TryParse(txtMdfFPNumber.Text, out byte o) ? o as object : null;
                    break;
                }
                case "SByte":
                {
                    value = sbyte.TryParse(txtMdfFPNumber.Text, out sbyte o) ? o as object : null;
                    break;
                }
                case "Int16":
                {
                    value = short.TryParse(txtMdfFPNumber.Text, out short o) ? o as object : null;
                    break;
                }
                case "Int32":
                {
                    value = int.TryParse(txtMdfFPNumber.Text, out int o) ? o as object : null;
                    break;
                }
                case "Int64":
                {
                    value = long.TryParse(txtMdfFPNumber.Text, out long o) ? o as object : null;
                    break;
                }
                case "UInt16":
                {
                    value = ushort.TryParse(txtMdfFPNumber.Text, out ushort o) ? o as object : null;
                    break;
                }
                case "UInt32":
                {
                    value = uint.TryParse(txtMdfFPNumber.Text, out uint o) ? o as object : null;
                    break;
                }
                case "UInt64":
                {
                    value = ulong.TryParse(txtMdfFPNumber.Text, out ulong o) ? o as object : null;
                    break;
                }
                case "Single":
                {
                    value = float.TryParse(txtMdfFPNumber.Text, out float o) ? o as object : null;
                    break;
                }
                case "Double":
                {
                    value = double.TryParse(txtMdfFPNumber.Text, out double o) ? o as object : null;
                    break;
                }
                case "Decimal":
                {
                    value = decimal.TryParse(txtMdfFPNumber.Text, out decimal o) ? o as object : null;
                    break;
                }
                default:
                {
                    break;
                }
            }

            if (value == null)
            {
                MessageBox.Show("数值输入错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string type = dgvFieldProperty.SelectedRows[0].Cells["类型"].Value as string;
            string name = dgvFieldProperty.SelectedRows[0].Cells["Name"].Value as string;

            SetFieldPropertyValue(type, name, value);
        }

        private void BtnMdfFPDateTime_Click(object sender, EventArgs e)
        {
            object value = new DateTime(
                dtpMdfFPDateTimeDate.Value.Year,
                dtpMdfFPDateTimeDate.Value.Month,
                dtpMdfFPDateTimeDate.Value.Day,
                dtpMdfFPDateTimeTime.Value.Hour,
                dtpMdfFPDateTimeTime.Value.Minute,
                dtpMdfFPDateTimeTime.Value.Second
            );
            string type = dgvFieldProperty.SelectedRows[0].Cells["类型"].Value as string;
            string name = dgvFieldProperty.SelectedRows[0].Cells["Name"].Value as string;

            if (SetFieldPropertyValue(type, name, value))
            {
                txtMdfFPDateTime.Text = Convert.ToDateTime(value).ToString("yyyy/MM/dd HH:mm:ss");
            }
        }
        #endregion

        #region 操作区
        private void BtnTopMost_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
            btnTopMost.BackColor = TopMost ? Color.FromArgb(192, 255, 192) : Color.FromArgb(255, 192, 192);
        }

        private void BtnRefreshAll_Click(object sender, EventArgs e)
        {
            txtControlSearch.Clear();
            txtFieldPropertySearch.Clear();

            InitInfomation();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            //刷新控件信息
            for (int i = 0; i < dgvControl.Rows.Count; i++)
            {
                dgvControl.Rows[i].Cells["Text"].Value = (dgvControl.Rows[i].Cells["Object"].Value as Control).Text;
            }

            //刷新变量属性数值
            for (int i = 0; i < dgvFieldProperty.Rows.Count; i++)
            {
                string type = dgvFieldProperty.Rows[i].Cells["类型"].Value as string;
                string name = dgvFieldProperty.Rows[i].Cells["Name"].Value as string;

                if (type == "变量")
                {
                    dgvFieldProperty.Rows[i].Cells["Value"].Value = DebugOperate.GetField(m_projectObject, name);
                }
                else
                {
                    dgvFieldProperty.Rows[i].Cells["Value"].Value = DebugOperate.GetProperty(m_projectObject, name);
                }
            }
        }
        #endregion
    }
}
