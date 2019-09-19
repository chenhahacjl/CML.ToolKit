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
            dgvControl.Columns["Text"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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
                if (typeof(Control).IsAssignableFrom(item.FieldType))
                {
                    dtFieldProperty.Rows.Add("变量(控件)", item.FieldType.Name, item.Name, item.GetValue(m_projectObject));
                }
                else
                {
                    dtFieldProperty.Rows.Add("变量", item.FieldType.Name, item.Name, item.GetValue(m_projectObject));
                }
            }

            //加载属性
            PropertyInfo[] properties = m_projectObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var item in properties)
            {
                if (typeof(Control).IsAssignableFrom(item.PropertyType))
                {
                    dtFieldProperty.Rows.Add("属性（控件）", item.PropertyType.Name, item.Name, item.GetValue(m_projectObject, null));
                }
                else
                {
                    dtFieldProperty.Rows.Add("属性", item.PropertyType.Name, item.Name, item.GetValue(m_projectObject, null));
                }
            }

            //设置显示格式
            dgvFP.DataSource = dtFieldProperty;
            dgvFP.Columns["类型"].MinimumWidth = 100;
            dgvFP.Columns["类型"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvFP.Columns["数据类型"].MinimumWidth = 100;
            dgvFP.Columns["数据类型"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvFP.Columns["Name"].MinimumWidth = 100;
            dgvFP.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvFP.Columns["Value"].MinimumWidth = 100;
            dgvFP.Columns["Value"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataTable dtFunction = new DataTable();
            dtFunction.Columns.Add("类型", Type.GetType("System.String"));
            dtFunction.Columns.Add("数据类型", Type.GetType("System.String"));
            dtFunction.Columns.Add("Name", Type.GetType("System.String"));
            dtFunction.Columns.Add("Parameter", Type.GetType("System.String"));
            dtFunction.Columns.Add("Types", Type.GetType("System.Object"));

            //加载构造函数
            ConstructorInfo[] constructors = m_projectObject.GetType().GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var constructor in constructors)
            {
                dtFunction.Rows.Add("构造函数", "Void", m_projectObject.GetType().Name, CreateParasString(constructor.GetParameters(), out Type[] types), types);
            }

            //加载事件
            EventInfo[] events = m_projectObject.GetType().GetEvents(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var @event in events)
            {
                dtFunction.Rows.Add("事件", @event.EventHandlerType.Name, @event.Name, "", null);
            }

            //加载方法
            MethodInfo[] methods = m_projectObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var method in methods)
            {
                dtFunction.Rows.Add("方法", method.ReturnType.Name, method.Name, CreateParasString(method.GetParameters(), out Type[] types), types);
            }

            dgvCEM.DataSource = dtFunction;
            dgvCEM.Columns["类型"].MinimumWidth = 100;
            dgvCEM.Columns["类型"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvCEM.Columns["数据类型"].MinimumWidth = 100;
            dgvCEM.Columns["数据类型"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvCEM.Columns["Name"].MinimumWidth = 100;
            dgvCEM.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvCEM.Columns["Parameter"].MinimumWidth = 100;
            dgvCEM.Columns["Parameter"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvCEM.Columns["Types"].Visible = false;
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
        /// <param name="name">变量名称</param>
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
        /// <param name="name">属性名称</param>
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
        /// 加载构造函数
        /// </summary>
        /// <param name="type">参数类型</param>
        private void LoadConstructor(Type[] type)
        {
            ConstructorInfo constructor = m_projectObject.GetType().GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, type, null);

            if (constructor == null)
            {
                txtCtorName.Clear();
                txtCtorFullName.Clear();
                txtCtorStatic.Clear();
                txtCtorAbstract.Clear();
                txtCtorVirtual.Clear();
                txtCtorFinal.Clear();
                txtCtorPublic.Clear();
                txtCtorPrivate.Clear();
                txtCtorToken.Clear();
            }
            else
            {
                txtCtorName.Text = constructor.DeclaringType.Name;
                txtCtorFullName.Text = constructor.DeclaringType.FullName;
                txtCtorStatic.Text = constructor.IsStatic.ToString();
                txtCtorAbstract.Text = constructor.IsAbstract.ToString();
                txtCtorVirtual.Text = constructor.IsVirtual.ToString();
                txtCtorFinal.Text = constructor.IsFinal.ToString();
                txtCtorPublic.Text = constructor.IsPublic.ToString();
                txtCtorPrivate.Text = constructor.IsPrivate.ToString();
                txtCtorParas.Text = CreateParasString(constructor.GetParameters(), out _);
                txtCtorToken.Text = constructor.MetadataToken.ToString();
            }
        }

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="name">事件名称</param>
        private void LoadEvent(string name)
        {
            bool isPublic = true;
            EventInfo @event = m_projectObject.GetType().GetEvent(name, BindingFlags.Instance | BindingFlags.Public);
            if (@event == null)
            {
                isPublic = false;
                @event = m_projectObject.GetType().GetEvent(name, BindingFlags.Instance | BindingFlags.NonPublic);
            }

            if (@event == null)
            {
                txtEventName.Clear();
                txtEventFullName.Clear();
                txtEventMulticast.Clear();
                txtEventPublic.Clear();
                txtEventEventType.Clear();
                txtEventToken.Clear();
            }
            else
            {
                txtEventName.Text = @event.Name;
                txtEventFullName.Text = @event.DeclaringType.FullName + "." + @event.Name;
                txtEventMulticast.Text = @event.IsMulticast.ToString();
                txtEventPublic.Text = isPublic ? "Public" : "NonPublic";
                txtEventEventType.Text = @event.EventHandlerType.FullName;
                txtEventToken.Text = @event.MetadataToken.ToString();
            }
        }

        /// <summary>
        /// 加载方法
        /// </summary>
        /// <param name="name">方法名称</param>
        /// <param name="type">参数类型</param>
        private void LoadMethod(string name, Type[] type)
        {
            var method = m_projectObject.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, type, null);

            txtMethodName.Text = method.Name;
            txtMethodFullName.Text = method.DeclaringType.FullName + "." + method.Name;
            txtMethodStatic.Text = method.IsStatic.ToString();
            txtMethodAbstract.Text = method.IsAbstract.ToString();
            txtMethodVirtual.Text = method.IsVirtual.ToString();
            txtMethodFinal.Text = method.IsFinal.ToString();
            txtMethodPublic.Text = method.IsPublic.ToString();
            txtMethodPrivate.Text = method.IsPrivate.ToString();
            txtMethodReturn.Text = method.ReturnType.FullName;
            txtMethodParas.Text = CreateParasString(method.GetParameters(), out _);
            txtMethodToken.Text = method.MetadataToken.ToString();
        }

        /// <summary>
        /// 创建方法字符串
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        /// <returns>方法字符串</returns>
        private string CreateFunctionString(MethodInfo methodInfo)
        {
            if (methodInfo == null) { return "Null"; }

            string function = methodInfo.IsPublic ? "public " : methodInfo.IsPrivate ? "private " : "protected/internal ";
            function += methodInfo.IsStatic ? "static " : "";
            function += methodInfo.ReturnType.Name + " ";
            function += methodInfo.Name + "(";
            function += CreateParasString(methodInfo.GetParameters(), out _);
            function += ")";

            return function;
        }

        /// <summary>
        /// 创建参数字符串
        /// </summary>
        /// <param name="arrParaInfo">参数信息</param>
        /// <param name="types">参数类型</param>
        /// <returns>参数字符串</returns>
        private string CreateParasString(ParameterInfo[] arrParaInfo, out Type[] types)
        {
            string paras = "";
            List<Type> lstType = new List<Type>();

            List<ParameterInfo> parameters = new List<ParameterInfo>(arrParaInfo);
            for (int i = 0; i < parameters.Count; i++)
            {
                ParameterInfo parameter = parameters.FirstOrDefault(item => item.Position == i);
                if (parameter != null)
                {
                    paras += parameter.ParameterType.Name + " " + parameter.Name + ", ";
                    lstType.Add(parameter.ParameterType);
                }
            }

            types = lstType.ToArray();
            return paras.TrimEnd(' ').TrimEnd(',');
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
                dgvFP.SelectedRows[0].Cells["Value"].Value = value;
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
        private void TxtFPSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFPSearch.Text) && e.KeyCode == Keys.Enter)
            {
                int index = 969696;
                for (int i = 0; i < dgvFP.Rows.Count; i++)
                {
                    string name = dgvFP.Rows[i].Cells["Name"].Value as string;
                    if (name.ToUpper().Contains(txtFPSearch.Text.ToUpper()) ||
                        RegexOperateEF.CF_IsMatch(name, txtFPSearch.Text))
                    {
                        if (index > i) index = i;
                        dgvFP.Rows[i].Cells["Name"].Style.BackColor = Color.PaleGreen;
                    }
                    else
                    {
                        dgvFP.Rows[i].Cells["Name"].Style.BackColor = Color.White;
                    }
                }
                if (index == 969696)
                {
                    dgvFP.ClearSelection();
                }
                else
                {
                    dgvFP.Rows[index].Cells[0].Selected = true;
                    dgvFP.CurrentCell = dgvFP.Rows[index].Cells[0];
                }
            }
        }

        private void DgvFP_SelectionChanged(object sender, EventArgs e)
        {
            //隐藏所有窗体
            foreach (Control control in pnlTypeFP.Controls)
            {
                control.Visible = false;
            }
            foreach (Control control in pnlMdfFP.Controls)
            {
                control.Visible = false;
            }

            //判断是否选择行
            if (dgvFP.SelectedRows.Count == 0)
            {
                pnlNoSelectFP.Visible = true;
                pnlMdfFPOther.Visible = true;
                return;
            }

            //类型判断
            if ((dgvFP.SelectedRows[0].Cells["类型"].Value as string).Substring(0, 2) == "变量")
            {
                LoadField(dgvFP.SelectedRows[0].Cells["Name"].Value as string);
                pnlField.Visible = true;
            }
            else if ((dgvFP.SelectedRows[0].Cells["类型"].Value as string).Substring(0, 2) == "属性")
            {
                LoadProperty(dgvFP.SelectedRows[0].Cells["Name"].Value as string);
                pnlProperty.Visible = true;
            }
            else
            {
                pnlNoSelectFP.Visible = true;
            }

            //数据
            Type dataType = dgvFP.SelectedRows[0].Cells["Value"].Value.GetType();
            object value = dgvFP.SelectedRows[0].Cells["Value"].Value;

            //根据不同类型做不同操作
            if (value is Enum)
            {
                pnlMdfFPEnum.Visible = true;
                cmbMdfFPEnum.Items.Clear();

                int index = -1;
                string[] arrNames = Enum.GetNames(dataType);
                for (int i = 0; i < arrNames.Length; i++)
                {
                    if (value.ToString() == arrNames[i]) { index = i; }
                    string desc = EnumEx.EnumOperate.CF_GetDescription(Enum.Parse(dataType, arrNames[i]) as Enum);
                    cmbMdfFPEnum.Items.Add(arrNames[i] == desc ? arrNames[i] : $"{arrNames[i]} [{desc}]");
                }
                cmbMdfFPEnum.SelectedIndex = index;
                btnMdfFPEnum.Enabled = cmbMdfFPEnum.Items.Count != 0;
            }
            else
            {
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
                    case "UInt16":
                    case "Int32":
                    case "UInt32":
                    case "Int64":
                    case "UInt64":
                    case "Single":
                    case "Double":
                    case "Decimal":
                    case "IntPtr":
                    case "UIntPtr":
                    {
                        pnlMdfFPNumber.Visible = true;
                        txtMdfFPNumber.Text = value.ToString();
                        btnMdfFPNumber.Tag = dataType.Name;
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
                    case "Size":
                    {
                        pnlMdfFPSize.Visible = true;
                        lblMdfFPSizeOne.Text = "W";
                        txtMdfFPSizeOne.Text = ((Size)value).Width.ToString();
                        lblMdfFPSizeTwo.Text = "H";
                        txtMdfFPSizeTwo.Text = ((Size)value).Height.ToString();
                        btnMdfFPSize.Tag = dataType.Name;
                        break;
                    }
                    case "SizeF":
                    {
                        pnlMdfFPSize.Visible = true;
                        lblMdfFPSizeOne.Text = "W";
                        txtMdfFPSizeOne.Text = ((SizeF)value).Width.ToString();
                        lblMdfFPSizeTwo.Text = "H";
                        txtMdfFPSizeTwo.Text = ((SizeF)value).Height.ToString();
                        btnMdfFPSize.Tag = dataType.Name;
                        break;
                    }
                    case "Point":
                    {
                        pnlMdfFPSize.Visible = true;
                        lblMdfFPSizeOne.Text = "X";
                        txtMdfFPSizeOne.Text = ((Point)value).X.ToString();
                        lblMdfFPSizeTwo.Text = "Y";
                        txtMdfFPSizeTwo.Text = ((Point)value).Y.ToString();
                        btnMdfFPSize.Tag = dataType.Name;
                        break;
                    }
                    case "PointF":
                    {
                        pnlMdfFPSize.Visible = true;
                        lblMdfFPSizeOne.Text = "X";
                        txtMdfFPSizeOne.Text = ((PointF)value).X.ToString();
                        lblMdfFPSizeTwo.Text = "Y";
                        txtMdfFPSizeTwo.Text = ((PointF)value).Y.ToString();
                        btnMdfFPSize.Tag = dataType.Name;
                        break;
                    }
                    case "Rectangle":
                    {
                        pnlMdfFPRectangle.Visible = true;
                        lblMdfFPRectangleOne.Text = "X";
                        txtMdfFPRectangleOne.Text = ((Rectangle)value).X.ToString();
                        lblMdfFPRectangleTwo.Text = "Y";
                        txtMdfFPRectangleTwo.Text = ((Rectangle)value).Y.ToString();
                        lblMdfFPRectangleThree.Text = "W";
                        txtMdfFPRectangleThree.Text = ((Rectangle)value).Width.ToString();
                        lblMdfFPRectangleFour.Text = "H";
                        txtMdfFPRectangleFour.Text = ((Rectangle)value).Height.ToString();
                        btnMdfFPRectangle.Tag = dataType.Name;
                        break;
                    }
                    case "RectangleF":
                    {
                        pnlMdfFPRectangle.Visible = true;
                        lblMdfFPRectangleOne.Text = "X";
                        txtMdfFPRectangleOne.Text = ((RectangleF)value).X.ToString();
                        lblMdfFPRectangleTwo.Text = "Y";
                        txtMdfFPRectangleTwo.Text = ((RectangleF)value).Y.ToString();
                        lblMdfFPRectangleThree.Text = "W";
                        txtMdfFPRectangleThree.Text = ((RectangleF)value).Width.ToString();
                        lblMdfFPRectangleFour.Text = "H";
                        txtMdfFPRectangleFour.Text = ((RectangleF)value).Height.ToString();
                        btnMdfFPRectangle.Tag = dataType.Name;
                        break;
                    }
                    case "Padding":
                    {
                        pnlMdfFPRectangle.Visible = true;
                        lblMdfFPRectangleOne.Text = "L";
                        txtMdfFPRectangleOne.Text = ((Padding)value).Left.ToString();
                        lblMdfFPRectangleTwo.Text = "T";
                        txtMdfFPRectangleTwo.Text = ((Padding)value).Top.ToString();
                        lblMdfFPRectangleThree.Text = "R";
                        txtMdfFPRectangleThree.Text = ((Padding)value).Right.ToString();
                        lblMdfFPRectangleFour.Text = "B";
                        txtMdfFPRectangleFour.Text = ((Padding)value).Bottom.ToString();
                        btnMdfFPRectangle.Tag = dataType.Name;
                        break;
                    }
                    case "Color":
                    {
                        pnlMdfFPColor.Visible = true;

                        int index = -1;
                        string[] arrNames = Enum.GetNames(typeof(KnownColor));
                        for (int i = 0; i < arrNames.Length; i++)
                        {
                            if (value.ToString().Contains(arrNames[i])) { index = i; }
                            cmbMdfFPNameSelected.Items.Add(arrNames[i]);
                        }

                        if (index == -1)
                        {
                            rbMdfFPColor.Checked = true;
                            rbMdfFPName.Checked = false;
                            btnMdfFPColorSelected.BackColor = (Color)value;
                            cmbMdfFPNameSelected.SelectedIndex = 0;
                        }
                        else
                        {
                            rbMdfFPColor.Checked = false;
                            rbMdfFPName.Checked = true;
                            btnMdfFPColorSelected.BackColor = Color.FromKnownColor(KnownColor.Transparent);
                            cmbMdfFPNameSelected.SelectedIndex = index;
                        }
                        break;
                    }
                    case "Font":
                    {
                        pnlMdfFPFont.Visible = true;
                        btnMdfFPFontSelect.Font = (Font)value;
                        break;
                    }
                    default:
                    {
                        pnlMdfFPOther.Visible = true;
                        break;
                    }
                }
            }
        }

        private void BtnMdfFPBoolean_Click(object sender, EventArgs e)
        {
            object value = rbMdfFPBooleanT.Checked;
            string type = (dgvFP.SelectedRows[0].Cells["类型"].Value as string).Substring(0, 2);
            string name = dgvFP.SelectedRows[0].Cells["Name"].Value as string;

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
            string type = (dgvFP.SelectedRows[0].Cells["类型"].Value as string).Substring(0, 2);
            string name = dgvFP.SelectedRows[0].Cells["Name"].Value as string;

            SetFieldPropertyValue(type, name, value);
        }

        private void BtnMdfFPNumber_Click(object sender, EventArgs e)
        {
            object value = null;

            switch ((sender as Button).Tag as string)
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
                case "UInt16":
                {
                    value = ushort.TryParse(txtMdfFPNumber.Text, out ushort o) ? o as object : null;
                    break;
                }
                case "Int32":
                {
                    value = int.TryParse(txtMdfFPNumber.Text, out int o) ? o as object : null;
                    break;
                }
                case "UInt32":
                {
                    value = uint.TryParse(txtMdfFPNumber.Text, out uint o) ? o as object : null;
                    break;
                }
                case "Int64":
                {
                    value = long.TryParse(txtMdfFPNumber.Text, out long o) ? o as object : null;
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
                case "IntPtr":
                {
                    value = long.TryParse(txtMdfFPNumber.Text, out long o) ? new IntPtr(o) as object : null;
                    break;
                }
                case "UIntPtr":
                {
                    value = ulong.TryParse(txtMdfFPNumber.Text, out ulong o) ? new UIntPtr(o) as object : null;
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

            string type = (dgvFP.SelectedRows[0].Cells["类型"].Value as string).Substring(0, 2);
            string name = dgvFP.SelectedRows[0].Cells["Name"].Value as string;

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
            string type = (dgvFP.SelectedRows[0].Cells["类型"].Value as string).Substring(0, 2);
            string name = dgvFP.SelectedRows[0].Cells["Name"].Value as string;

            if (SetFieldPropertyValue(type, name, value))
            {
                txtMdfFPDateTime.Text = Convert.ToDateTime(value).ToString("yyyy/MM/dd HH:mm:ss");
            }
        }

        private void BtnMdfFPSize_Click(object sender, EventArgs e)
        {
            object value = null;

            switch ((sender as Button).Tag as string)
            {
                case "Size":
                {
                    value =
                        int.TryParse(txtMdfFPSizeOne.Text, out int o) ?
                        int.TryParse(txtMdfFPSizeTwo.Text, out int t) ?
                        new Size(o, t) as object : null : null;
                    break;
                }
                case "SizeF":
                {
                    value =
                        float.TryParse(txtMdfFPSizeOne.Text, out float o) ?
                        float.TryParse(txtMdfFPSizeTwo.Text, out float t) ?
                        new SizeF(o, t) as object : null : null;
                    break;
                }
                case "Point":
                {
                    value =
                        int.TryParse(txtMdfFPSizeOne.Text, out int o) ?
                        int.TryParse(txtMdfFPSizeTwo.Text, out int t) ?
                        new Point(o, t) as object : null : null;
                    break;
                }
                case "PointF":
                {
                    value =
                        float.TryParse(txtMdfFPSizeOne.Text, out float o) ?
                        float.TryParse(txtMdfFPSizeTwo.Text, out float t) ?
                        new PointF(o, t) as object : null : null;
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

            string type = (dgvFP.SelectedRows[0].Cells["类型"].Value as string).Substring(0, 2);
            string name = dgvFP.SelectedRows[0].Cells["Name"].Value as string;

            SetFieldPropertyValue(type, name, value);
        }

        private void BtnMdfFPRectangle_Click(object sender, EventArgs e)
        {
            object value = null;

            switch ((sender as Button).Tag as string)
            {
                case "Rectangle":
                {
                    value =
                        int.TryParse(txtMdfFPRectangleOne.Text, out int o) ?
                        int.TryParse(txtMdfFPRectangleTwo.Text, out int t) ?
                        int.TryParse(txtMdfFPRectangleTwo.Text, out int th) ?
                        int.TryParse(txtMdfFPRectangleTwo.Text, out int f) ?
                        new Rectangle(o, t, th, f) as object : null : null : null : null;
                    break;
                }
                case "RectangleF":
                {
                    value =
                        float.TryParse(txtMdfFPRectangleOne.Text, out float o) ?
                        float.TryParse(txtMdfFPRectangleTwo.Text, out float t) ?
                        float.TryParse(txtMdfFPRectangleTwo.Text, out float th) ?
                        float.TryParse(txtMdfFPRectangleTwo.Text, out float f) ?
                        new RectangleF(o, t, th, f) as object : null : null : null : null;
                    break;
                }
                case "Padding":
                {
                    value =
                        int.TryParse(txtMdfFPRectangleOne.Text, out int o) ?
                        int.TryParse(txtMdfFPRectangleTwo.Text, out int t) ?
                        int.TryParse(txtMdfFPRectangleTwo.Text, out int th) ?
                        int.TryParse(txtMdfFPRectangleTwo.Text, out int f) ?
                        new Padding(o, t, th, f) as object : null : null : null : null;
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

            string type = (dgvFP.SelectedRows[0].Cells["类型"].Value as string).Substring(0, 2);
            string name = dgvFP.SelectedRows[0].Cells["Name"].Value as string;

            SetFieldPropertyValue(type, name, value);
        }

        private void RbMdfFPColor_CheckedChanged(object sender, EventArgs e)
        {
            btnMdfFPColorSelected.Enabled = rbMdfFPColor.Checked;
            cmbMdfFPNameSelected.Enabled = rbMdfFPName.Checked;
        }

        private void BtnMdfFPColorSelected_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog()
            {
                Color = (sender as Button).BackColor,
            })
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    (sender as Button).BackColor = colorDialog.Color;
                }
            }
        }

        private void CmbMdfFPNameSelected_SelectedIndexChanged(object sender, EventArgs e)
        {
            picMdfFPNameColor.BackColor = cmbMdfFPNameSelected.SelectedIndex == -1 ?
                Color.Transparent : Color.FromName(cmbMdfFPNameSelected.SelectedItem.ToString());
        }

        private void BtnMdfFPColor_Click(object sender, EventArgs e)
        {
            object value = rbMdfFPColor.Checked ?
                btnMdfFPColorSelected.BackColor as object :
                Color.FromName(cmbMdfFPNameSelected.SelectedItem.ToString()) as object;
            string type = (dgvFP.SelectedRows[0].Cells["类型"].Value as string).Substring(0, 2);
            string name = dgvFP.SelectedRows[0].Cells["Name"].Value as string;

            SetFieldPropertyValue(type, name, value);
        }

        private void BtnMdfFPEnum_Click(object sender, EventArgs e)
        {
            string select = cmbMdfFPEnum.SelectedItem.ToString();
            select = select.Contains('[') ? select.Substring(0, select.IndexOf('[') - 1) : select;

            object value = Enum.Parse(dgvFP.SelectedRows[0].Cells["Value"].Value.GetType(), select);
            string type = (dgvFP.SelectedRows[0].Cells["类型"].Value as string).Substring(0, 2);
            string name = dgvFP.SelectedRows[0].Cells["Name"].Value as string;

            SetFieldPropertyValue(type, name, value);
        }

        private void BtnMdfFPFontSelect_Click(object sender, EventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog()
            {
                Font = (sender as Button).Font,
            })
            {
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    (sender as Button).Font = fontDialog.Font;
                }
            }
        }

        private void BtnMdfFPFont_Click(object sender, EventArgs e)
        {
            object value = btnMdfFPFontSelect.Font;
            string type = (dgvFP.SelectedRows[0].Cells["类型"].Value as string).Substring(0, 2);
            string name = dgvFP.SelectedRows[0].Cells["Name"].Value as string;

            SetFieldPropertyValue(type, name, value);
        }
        #endregion

        #region 构造函数/事件/方法标签页事件
        private void TxtCEMSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCEMSearch.Text) && e.KeyCode == Keys.Enter)
            {
                int index = 969696;
                for (int i = 0; i < dgvCEM.Rows.Count; i++)
                {
                    string name = dgvCEM.Rows[i].Cells["Name"].Value as string;
                    if (name.ToUpper().Contains(txtCEMSearch.Text.ToUpper()) ||
                        RegexOperateEF.CF_IsMatch(name, txtCEMSearch.Text))
                    {
                        if (index > i) index = i;
                        dgvCEM.Rows[i].Cells["Name"].Style.BackColor = Color.PaleGreen;
                    }
                    else
                    {
                        dgvCEM.Rows[i].Cells["Name"].Style.BackColor = Color.White;
                    }
                }
                if (index == 969696)
                {
                    dgvCEM.ClearSelection();
                }
                else
                {
                    dgvCEM.Rows[index].Cells[0].Selected = true;
                    dgvCEM.CurrentCell = dgvCEM.Rows[index].Cells[0];
                }
            }
        }

        private void DgvCEM_SelectionChanged(object sender, EventArgs e)
        {
            //隐藏所有窗体
            foreach (Control control in pnlTypeCEF.Controls)
            {
                control.Visible = false;
            }

            //判断是否选择行
            if (dgvCEM.SelectedRows.Count == 0)
            {
                pnlNoSelectCEF.Visible = true;
                return;
            }

            //类型判断
            if (dgvCEM.SelectedRows[0].Cells["类型"].Value as string == "构造函数")
            {
                LoadConstructor(dgvCEM.SelectedRows[0].Cells["Types"].Value as Type[]);
                pnlConstructor.Visible = true;
            }
            else if (dgvCEM.SelectedRows[0].Cells["类型"].Value as string == "事件")
            {
                LoadEvent(dgvCEM.SelectedRows[0].Cells["Name"].Value as string);
                pnlEvent.Visible = true;
            }
            else if (dgvCEM.SelectedRows[0].Cells["类型"].Value as string == "方法")
            {
                LoadMethod(dgvCEM.SelectedRows[0].Cells["Name"].Value as string, dgvCEM.SelectedRows[0].Cells["Types"].Value as Type[]);
                pnlMethod.Visible = true;
            }
            else
            {
                pnlNoSelectCEF.Visible = true;
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
            txtFPSearch.Clear();

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
            for (int i = 0; i < dgvFP.Rows.Count; i++)
            {
                string type = (dgvFP.Rows[i].Cells["类型"].Value as string).Substring(0, 2);
                string name = dgvFP.Rows[i].Cells["Name"].Value as string;

                if (type == "变量")
                {
                    dgvFP.Rows[i].Cells["Value"].Value = DebugOperate.GetField(m_projectObject, name);
                }
                else
                {
                    dgvFP.Rows[i].Cells["Value"].Value = DebugOperate.GetProperty(m_projectObject, name);
                }
            }
        }
        #endregion
    }
}
