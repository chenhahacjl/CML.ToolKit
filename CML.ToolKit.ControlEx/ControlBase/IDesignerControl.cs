using System.Windows.Forms;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 支持设计控件接口
    /// </summary>
    internal interface IDesignerControl
    {
        /// <summary>
        /// 获取支持设计控件
        /// </summary>
        Control DesignerControl { get; }
    }
}
