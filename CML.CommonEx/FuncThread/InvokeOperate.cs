using System;
using System.Windows.Forms;

namespace CML.CommonEx.ThreadEx
{
    /// <summary>
    /// 委托操作类
    /// </summary>
    public class InvokeOperate
    {
        /// <summary>
        /// 多线程更新UI
        /// </summary>
        /// <param name="control">委托控件</param>
        /// <param name="action">委托操作</param>
        /// <param name="isThrowException">是否抛出异常</param>
        public static void CF_InvokeUI(Control control, Action action, bool isThrowException = false)
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
