using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;

namespace CML.CommonEx.UIAutomationEx.ExFunction
{
    /// <summary>
    /// UI自动化操作类(扩展方法)
    /// 【引用】UIAutomationClient
    /// 【引用】UIAutomationTypes
    /// 【引用】UIAutomationProvider
    /// </summary>
    public static class UIAutomationOperateEF
    {
        #region 启动应用程序
        /// <summary>
        /// 启动应用程序
        /// </summary>
        /// <param name="progPath">应用程序路径</param>
        /// <returns>进程变量</returns>
        public static Process CF_StartProgram(this string progPath)
        {
            return UIAutomationOperate.CF_StartProgram(progPath);
        }

        /// <summary>
        /// 启动应用程序
        /// </summary>
        /// <param name="progPath">应用程序路径</param>
        /// <param name="args">启动参数</param>
        /// <returns>进程变量</returns>
        public static Process CF_StartProgram(this string progPath, string args)
        {
            return UIAutomationOperate.CF_StartProgram(progPath, args);
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
            return UIAutomationOperate.CF_StartProgram(progPath, args, workPath);
        }
        #endregion

        #region 获取元素合集
        /// <summary>
        /// 获得根元素下的所有元素
        /// </summary>
        /// <returns>元素合集</returns>
        public static AutomationElementCollection CF_GetAllElements()
        {
            return UIAutomationOperate.CF_GetAllElements();
        }

        /// <summary>
        /// 获取窗体元素下的元素合集
        /// </summary>
        /// <param name="element">窗体元素</param>
        /// <param name="treeScope">遍历模式</param>
        /// <returns>元素合集</returns>
        public static AutomationElementCollection CF_GetElementCollection(this AutomationElement element, TreeScope treeScope)
        {
            return UIAutomationOperate.CF_GetElementCollection(element, treeScope);
        }
        #endregion

        #region 获取元素
        /// <summary>
        /// 获取根元素
        /// </summary>
        /// <returns>根元素</returns>
        public static AutomationElement CF_GetRootElement()
        {
            return UIAutomationOperate.CF_GetRootElement();
        }

        /// <summary>
        /// 通过进程获取元素
        /// </summary>
        /// <param name="process">进程</param>
        /// <returns>元素</returns>
        public static AutomationElement CF_GetElementByProcess(this Process process)
        {
            return UIAutomationOperate.CF_GetElementByProcess(process);
        }

        /// <summary>
        /// 通过进程ID获取元素
        /// </summary>
        /// <param name="pid">进程ID</param>
        /// <returns>元素</returns>
        public static AutomationElement CF_GetElementByPId(this int pid)
        {
            return UIAutomationOperate.CF_GetElementByPId(pid);
        }

        /// <summary>
        /// 获取窗体元素下的第一个元素
        /// </summary>
        /// <param name="element">窗体元素</param>
        /// <returns>第一个元素</returns>
        public static AutomationElement CF_GetFirstElement(this AutomationElement element)
        {
            return UIAutomationOperate.CF_GetFirstElement(element);
        }

        /// <summary>
        /// 获取窗体元素下的第一个元素
        /// </summary>
        /// <param name="element">窗体元素</param>
        /// <param name="treeScope">遍历模式</param>
        /// <returns>第一个元素</returns>
        public static AutomationElement CF_GetFirstElement(this AutomationElement element, TreeScope treeScope)
        {
            return UIAutomationOperate.CF_GetFirstElement(element, treeScope);
        }

        /// <summary>
        /// 通过序列ID获取窗体元素下的元素
        /// </summary>
        /// <param name="element">窗体元素</param>
        /// <param name="index">序列ID</param>
        /// <returns>元素</returns>
        public static AutomationElement CF_GetElementByIndex(this AutomationElement element, int index)
        {
            return UIAutomationOperate.CF_GetElementByIndex(element, index);
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
            return UIAutomationOperate.CF_GetElementByIndex(element, treeScope, index);
        }

        /// <summary>
        /// 通过内部名称获取窗体元素下的元素
        /// </summary>
        /// <param name="element">窗体元素</param>
        /// <param name="automationId">内部名称</param>
        /// <returns>元素</returns>
        public static AutomationElement CF_GetElementByAutomationId(this AutomationElement element, string automationId)
        {
            return UIAutomationOperate.CF_GetElementByAutomationId(element, automationId);
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
            return UIAutomationOperate.CF_GetElementByAutomationId(element, treeScope, automationId);
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
            return UIAutomationOperate.CF_GetElementByTypeName(element, controlType, elementName);
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
            return UIAutomationOperate.CF_GetElementByTypeName(element, treeScope, controlType, elementName);
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
            return UIAutomationOperate.CF_GetNameContent(element);
        }

        /// <summary>
        /// 获取文本框元素文本内容
        /// </summary>
        /// <param name="element">文本框元素</param>
        /// <returns>文本内容</returns>
        public static string CF_GetTextContent(this AutomationElement element)
        {
            return UIAutomationOperate.CF_GetTextContent(element);
        }

        /// <summary>
        /// 设置文本框元素文本内容
        /// </summary>
        /// <param name="element">文本框元素</param>
        /// <param name="content">文本内容</param>
        /// <param name="delay">Focus延时[单位:MS|默认:100MS]</param>
        public static void CF_SetTextContent(this AutomationElement element, string content, int delay = 100)
        {
            UIAutomationOperate.CF_SetTextContent(element, content, delay);
        }

        /// <summary>
        /// 获取复选框元素选择状态
        /// </summary>
        /// <param name="element">复选框元素</param>
        /// <returns>选择状态</returns>
        public static ECheckBoxStatus CF_GetCheckBoxStatu(this AutomationElement element)
        {
            return UIAutomationOperate.CF_GetCheckBoxStatu(element);
        }

        /// <summary>
        /// 设置复选框元素选择状态
        /// </summary>
        /// <param name="element">复选框元素</param>
        /// <param name="statu">选择状态</param>
        public static void CF_SetCheckBoxStatu(this AutomationElement element, bool statu)
        {
            UIAutomationOperate.CF_SetCheckBoxStatu(element, statu);
        }

        /// <summary>
        /// 获取下拉列表选择项
        /// </summary>
        /// <param name="element">下拉列表元素</param>
        /// <returns>列表项元素</returns>
        public static AutomationElement CF_GetSelectListItem(this AutomationElement element)
        {
            return UIAutomationOperate.CF_GetSelectListItem(element);
        }

        /// <summary>
        /// 设置下拉列表选择项
        /// </summary>
        /// <param name="element">列表项元素</param>
        public static void CF_SetSelectListItem(this AutomationElement element)
        {
            UIAutomationOperate.CF_SetSelectListItem(element);
        }

        /// <summary>
        /// 单击按钮
        /// </summary>
        /// <param name="element">按钮元素</param>
        public static void CF_ClickButton(this AutomationElement element)
        {
            UIAutomationOperate.CF_ClickButton(element);
        }

        /// <summary>
        /// 对指定元素发送命令
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="command">命令</param>
        /// <param name="delay">Focus延时[单位:MS|默认:100MS]</param>
        public static void CF_SendCommand(this AutomationElement element, string command, int delay = 100)
        {
            UIAutomationOperate.CF_SendCommand(element, command, delay);
        }
        #endregion

        #region 延时操作
        /// <summary>
        /// 延时等待（毫秒）
        /// </summary>
        /// <param name="millisecond">延时毫秒数</param>
        public static void CF_DelayMillisecond(this int millisecond)
        {
            UIAutomationOperate.CF_DelayMillisecond(millisecond);
        }

        /// <summary>
        /// 延时等待（秒）
        /// </summary>
        /// <param name="second">延时秒数</param>
        public static void CF_DelaySecond(this int second)
        {
            UIAutomationOperate.CF_DelaySecond(second);
        }

        /// <summary>
        /// 延时等待（分钟）
        /// </summary>
        /// <param name="minute">延时分钟数</param>
        public static void CF_DelayMinute(this int minute)
        {
            UIAutomationOperate.CF_DelayMinute(minute);
        }

        /// <summary>
        /// 延时等待（小时）
        /// </summary>
        /// <param name="hour">延时小时数</param>
        public static void CF_DelayHour(this int hour)
        {
            UIAutomationOperate.CF_DelayHour(hour);
        }
        #endregion
    }
}
