using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;

namespace CML.CommonEx.UIAutomationEx
{
    /// <summary>
    /// UI自动化操作类
    /// 【引用】UIAutomationClient
    /// 【引用】UIAutomationTypes
    /// 【引用】UIAutomationProvider
    /// </summary>
    public static class UIAutomationOperate
    {
        #region 启动应用程序
        /// <summary>
        /// 启动应用程序
        /// </summary>
        /// <param name="progPath">应用程序路径</param>
        /// <returns>进程变量</returns>
        public static Process CF_StartProgram(this string progPath)
        {
            return CF_StartProgram(progPath, "");
        }

        /// <summary>
        /// 启动应用程序
        /// </summary>
        /// <param name="progPath">应用程序路径</param>
        /// <param name="args">启动参数</param>
        /// <returns>进程变量</returns>
        public static Process CF_StartProgram(this string progPath, string args)
        {
            return CF_StartProgram(progPath, args, new FileInfo(progPath).Directory.FullName);
        }

        /// <summary>
        /// 启动应用程序
        /// </summary>
        /// <param name="progPath">应用程序路径</param>
        /// <param name="args">启动参数</param>
        /// <param name="workPath">工装路径</param>
        /// <returns>进程变量</returns>
        public static Process CF_StartProgram(this string progPath, string args, string workPath)
        {
            if (!File.Exists(progPath))
            {
                return null;
            }

            //启动程序
            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo(progPath)
                {
                    Arguments = args,
                    WorkingDirectory = workPath
                }
            };

            process.Start();

            return process;
        }
        #endregion

        #region 获取元素合集
        /// <summary>
        /// 获得根元素下的所有元素
        /// </summary>
        /// <returns>元素合集</returns>
        public static AutomationElementCollection CF_GetAllElements()
        {
            AutomationElementCollection automationElementCollection = null;

            try
            {
                automationElementCollection = AutomationElement.RootElement.FindAll(
                    TreeScope.Subtree,
                    Condition.TrueCondition);
            }
            catch { }

            return automationElementCollection;
        }

        /// <summary>
        /// 获取窗体元素下的元素合集
        /// </summary>
        /// <param name="element">窗体元素</param>
        /// <param name="treeScope">遍历模式</param>
        /// <returns>元素合集</returns>
        public static AutomationElementCollection CF_GetElementCollection(this AutomationElement element, TreeScope treeScope)
        {
            AutomationElementCollection automationElementCollection = null;

            if (element != null)
            {
                try
                {
                    automationElementCollection = element.FindAll(
                        treeScope,
                        Condition.TrueCondition);
                }
                catch { }
            }

            return automationElementCollection;
        }
        #endregion

        #region 获取元素
        /// <summary>
        /// 获取根元素
        /// </summary>
        /// <returns>根元素</returns>
        public static AutomationElement CF_GetRootElement()
        {
            return AutomationElement.RootElement;
        }

        /// <summary>
        /// 通过进程获取元素
        /// </summary>
        /// <param name="process">进程</param>
        /// <returns>元素</returns>
        public static AutomationElement CF_GetElementByProcess(this Process process)
        {
            return CF_GetElementByPId(process.Id);
        }

        /// <summary>
        /// 通过进程ID获取元素
        /// </summary>
        /// <param name="pid">进程ID</param>
        /// <returns>元素</returns>
        public static AutomationElement CF_GetElementByPId(this int pid)
        {
            AutomationElement automationElement = null;

            try
            {
                automationElement = AutomationElement.RootElement.FindFirst(
                    TreeScope.Children,
                    new PropertyCondition(AutomationElement.ProcessIdProperty, pid));
            }
            catch { }

            return automationElement;
        }

        /// <summary>
        /// 获取窗体元素下的第一个元素
        /// </summary>
        /// <param name="element">窗体元素</param>
        /// <returns>第一个元素</returns>
        public static AutomationElement CF_GetFirstElement(this AutomationElement element)
        {
            return CF_GetFirstElement(element, TreeScope.Subtree);
        }

        /// <summary>
        /// 获取窗体元素下的第一个元素
        /// </summary>
        /// <param name="element">窗体元素</param>
        /// <param name="treeScope">遍历模式</param>
        /// <returns>第一个元素</returns>
        public static AutomationElement CF_GetFirstElement(this AutomationElement element, TreeScope treeScope)
        {
            AutomationElement automationElement = null;

            if (element != null)
            {
                try
                {
                    automationElement = element.FindAll(
                        treeScope,
                        Condition.TrueCondition)[0];
                }
                catch { }
            }

            return automationElement;
        }

        /// <summary>
        /// 通过序列ID获取窗体元素下的元素
        /// </summary>
        /// <param name="element">窗体元素</param>
        /// <param name="index">序列ID</param>
        /// <returns>元素</returns>
        public static AutomationElement CF_GetElementByIndex(this AutomationElement element, int index)
        {
            return CF_GetElementByIndex(element, TreeScope.Subtree, index);
        }

        /// <summary>
        /// 通过序列ID获取窗体元素下的元素
        /// </summary>
        /// <param name="element">窗体元素</param>
        /// <param name="treeScope">遍历模式</param>
        /// <param name="index">序列ID</param>
        /// <returns>元素</returns>
        public static AutomationElement CF_GetElementByIndex(this AutomationElement element, TreeScope treeScope, int index)
        {
            AutomationElement automationElement = null;

            if (element != null)
            {
                try
                {
                    automationElement = element.FindAll(
                        treeScope,
                        Condition.TrueCondition)[index];
                }
                catch { }
            }

            return automationElement;
        }

        /// <summary>
        /// 通过内部名称获取窗体元素下的元素
        /// </summary>
        /// <param name="element">窗体元素</param>
        /// <param name="automationId">内部名称</param>
        /// <returns>元素</returns>
        public static AutomationElement CF_GetElementByAutomationId(this AutomationElement element, string automationId)
        {
            return CF_GetElementByAutomationId(element, TreeScope.Subtree, automationId);
        }

        /// <summary>
        /// 通过内部名称获取窗体元素下的元素
        /// </summary>
        /// <param name="element">窗体元素</param>
        /// <param name="treeScope">遍历模式</param>
        /// <param name="automationId">内部名称</param>
        /// <returns>元素</returns>
        public static AutomationElement CF_GetElementByAutomationId(this AutomationElement element, TreeScope treeScope, string automationId)
        {
            AutomationElement automationElement = null;

            if (element != null)
            {
                try
                {
                    automationElement = element.FindFirst(
                        treeScope,
                        new PropertyCondition(AutomationElement.AutomationIdProperty, automationId));
                }
                catch { }
            }

            return automationElement;
        }

        /// <summary>
        /// 通过元素类型和元素名称获取元素
        /// </summary>
        /// <param name="element">窗体元素</param>
        /// <param name="controlType">元素类型</param>
        /// <param name="elementName">元素名称</param>
        /// <returns>元素</returns>
        public static AutomationElement CF_GetElementByTypeName(this AutomationElement element, ControlType controlType, string elementName)
        {
            return CF_GetElementByTypeName(element, TreeScope.Subtree, controlType, elementName);
        }

        /// <summary>
        /// 通过元素类型和元素名称获取元素
        /// </summary>
        /// <param name="element">窗体元素</param>
        /// <param name="treeScope">遍历模式</param>
        /// <param name="controlType">元素类型</param>
        /// <param name="elementName">元素名称</param>
        /// <returns>元素</returns>
        public static AutomationElement CF_GetElementByTypeName(this AutomationElement element, TreeScope treeScope, ControlType controlType, string elementName)
        {
            AutomationElement automationElement = null;

            if (element != null)
            {
                try
                {
                    AutomationElementCollection automationElementCollection = element.FindAll(
                        treeScope,
                        new PropertyCondition(AutomationElement.ControlTypeProperty, controlType));

                    for (int i = 0; i < automationElementCollection.Count; i++)
                    {
                        if (automationElementCollection[i].Current.Name == elementName)
                        {
                            automationElement = automationElementCollection[i];
                            break;
                        }
                    }
                }
                catch { }
            }

            return automationElement;
        }
        #endregion

        #region 元素操作
        /// <summary>
        /// 获取元素Name属性内容
        /// </summary>
        /// <param name="element">元素</param>
        /// <returns>Name属性</returns>
        public static string CF_GetNameContent(this AutomationElement element)
        {
            string elementName = "";

            if (element != null)
            {
                try
                {
                    elementName = element.Current.Name;
                }
                catch { }
            }

            return elementName;
        }

        /// <summary>
        /// 获取文本框元素文本内容
        /// </summary>
        /// <param name="element">文本框元素</param>
        /// <returns>文本内容</returns>
        public static string CF_GetTextContent(this AutomationElement element)
        {
            string elementText = "";

            if (element != null && element.Current.ControlType != ControlType.Edit)
            {
                try
                {
                    if (element.TryGetCurrentPattern(TextPattern.Pattern, out object pattern))
                    {
                        TextPattern txtPattern = pattern as TextPattern;
                        elementText = txtPattern.DocumentRange.GetText(-1);
                    }
                }
                catch { }
            }

            return elementText;
        }

        /// <summary>
        /// 设置文本框元素文本内容
        /// </summary>
        /// <param name="element">文本框元素</param>
        /// <param name="content">文本内容</param>
        /// <param name="delay">Focus延时[单位:MS|默认:100MS]</param>
        public static void CF_SetTextContent(this AutomationElement element, string content, int delay = 100)
        {
            if (element != null && element.Current.ControlType == ControlType.Edit)
            {
                try
                {
                    //设置焦点
                    element.SetFocus();

                    //等待设置焦点
                    Thread.Sleep(delay);

                    //是否支持直接写入
                    if (element.TryGetCurrentPattern(ValuePattern.Pattern, out object vPattern))
                    {
                        //指定类型
                        ValuePattern valuePattern = vPattern as ValuePattern;

                        //判断只读
                        if (!valuePattern.Current.IsReadOnly)
                        {
                            valuePattern.SetValue(content);
                        }
                    }
                    else
                    {
                        //模拟键入
                        SendKeys.SendWait("^{HOME}");
                        SendKeys.SendWait("^+{END}");
                        SendKeys.SendWait("{DEL}");
                        SendKeys.SendWait(content);
                    }
                }
                catch { }
            }
            else
            {
                try
                {
                    //设置焦点
                    element.SetFocus();

                    //模拟键入
                    SendKeys.SendWait("^{HOME}");
                    SendKeys.SendWait("^+{END}");
                    SendKeys.SendWait("{DEL}");
                    SendKeys.SendWait(content);
                }
                catch { }
            }
        }

        /// <summary>
        /// 获取复选框元素选择状态
        /// </summary>
        /// <param name="element">复选框元素</param>
        /// <returns>选择状态</returns>
        public static ECheckBoxStatus CF_GetCheckBoxStatu(this AutomationElement element)
        {
            ECheckBoxStatus checkBoxStatus = ECheckBoxStatus.Error;

            if (element != null && element.Current.ControlType == ControlType.CheckBox)
            {
                try
                {
                    if (element.TryGetCurrentPattern(TogglePattern.Pattern, out object pattern))
                    {
                        TogglePattern togglePattern = pattern as TogglePattern;
                        ToggleState toggleState = togglePattern.Current.ToggleState;

                        checkBoxStatus = toggleState == ToggleState.On ?
                            ECheckBoxStatus.On : toggleState == ToggleState.Off ?
                            ECheckBoxStatus.Off : ECheckBoxStatus.Indeterminate;
                    }
                }
                catch { }
            }

            return checkBoxStatus;
        }

        /// <summary>
        /// 设置复选框元素选择状态
        /// </summary>
        /// <param name="element">复选框元素</param>
        /// <param name="statu">选择状态</param>
        public static void CF_SetCheckBoxStatu(this AutomationElement element, bool statu)
        {
            if (element != null && element.Current.ControlType == ControlType.CheckBox)
            {
                try
                {
                    if (element.TryGetCurrentPattern(TogglePattern.Pattern, out object pattern))
                    {
                        TogglePattern togglePattern = pattern as TogglePattern;
                        ToggleState toggleState = statu ? ToggleState.On : ToggleState.Off;

                        if (togglePattern.Current.ToggleState != toggleState)
                        {
                            togglePattern.Toggle();
                        }
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// 获取下拉列表选择项
        /// </summary>
        /// <param name="element">下拉列表元素</param>
        /// <returns>列表项元素</returns>
        public static AutomationElement CF_GetSelectListItem(this AutomationElement element)
        {
            AutomationElement automationElement = null;

            if (element != null && element.Current.ControlType == ControlType.ComboBox)
            {
                try
                {
                    if (element.TryGetCurrentPattern(SelectionPattern.Pattern, out object pattern))
                    {
                        SelectionPattern selectionPattern = pattern as SelectionPattern;
                        automationElement = selectionPattern.Current.GetSelection()[0];
                    }
                }
                catch { }
            }

            return automationElement;
        }

        /// <summary>
        /// 设置下拉列表选择项
        /// </summary>
        /// <param name="element">列表项元素</param>
        public static void CF_SetSelectListItem(this AutomationElement element)
        {
            if (element != null && element.Current.ControlType == ControlType.ListItem)
            {
                try
                {
                    if (element.TryGetCurrentPattern(SelectionItemPattern.Pattern, out object pattern))
                    {
                        SelectionItemPattern selectionItemPattern = pattern as SelectionItemPattern;
                        selectionItemPattern.Select();
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// 单击按钮
        /// </summary>
        /// <param name="element">按钮元素</param>
        public static void CF_ClickButton(this AutomationElement element)
        {
            if (element != null && element.Current.ControlType == ControlType.Button)
            {
                try
                {
                    if (element.TryGetCurrentPattern(InvokePattern.Pattern, out object pattern))
                    {
                        InvokePattern invokePattern = pattern as InvokePattern;
                        invokePattern.Invoke();
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// 对指定元素发送命令
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="command">命令</param>
        /// <param name="delay">Focus延时[单位:MS|默认:100MS]</param>
        public static void CF_SendCommand(this AutomationElement element, string command, int delay = 100)
        {
            if (element != null)
            {
                try
                {
                    //设置焦点
                    element.SetFocus();
                    //等待设置焦点
                    Thread.Sleep(delay);
                    //发送命令
                    SendKeys.SendWait(command);
                }
                catch { }
            }
        }
        #endregion
    }
}
