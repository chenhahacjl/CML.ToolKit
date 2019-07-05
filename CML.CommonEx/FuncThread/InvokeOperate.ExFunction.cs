using System;
using System.Windows.Forms;

namespace CML.CommonEx.ThreadEx.ExFunction
{
    /// <summary>
    /// 委托操作类(扩展方法)
    /// </summary>
    public static class InvokeOperateEF
    {
        /// <summary>
        /// 多线程更新UI
        /// </summary>
        /// <param name="control">委托控件</param>
        /// <param name="action">委托操作</param>
        /// <param name="isThrowException">是否抛出异常</param>
        public static void CF_InvokeUI(this Control control, Action action, bool isThrowException = false)
        {
            InvokeOperate.CF_InvokeUI(control, action, isThrowException);
        }
    }
}
