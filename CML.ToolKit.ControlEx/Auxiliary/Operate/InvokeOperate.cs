using System;
using System.Windows.Forms;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 委托帮助类
    /// </summary>
    internal static class InvokeOperate
    {
        /// <summary>
        /// 多线程更新UI
        /// </summary>
        /// <param name="control">委托控件</param>
        /// <param name="action">委托操作</param>
        /// <param name="isThrowException">是否抛出异常</param>
        public static void InvokeUI(this Control control, Action action, bool isThrowException = false)
        {
            if (control.InvokeRequired)
            {
                while (!control.IsHandleCreated)
                {
                    if (control.Disposing || control.IsDisposed)
                    {
                        return;
                    }
                }

                try
                {
                    _ = control.Invoke(action);
                }
                catch (Exception ex)
                {
                    if (isThrowException) { throw ex; }
                }
            }
            else
            {
                action();
            }
        }
    }
}
