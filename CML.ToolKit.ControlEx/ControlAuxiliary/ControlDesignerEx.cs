using System.ComponentModel;
using System.Windows.Forms.Design;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 支持设计控件设计器
    /// </summary>
    internal class ControlDesignerEx : ControlDesigner
    {
        /// <summary>
        /// 支持设计控件
        /// </summary>
        private IDesignerControl m_designerControl;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="component">组件</param>
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            m_designerControl = (IDesignerControl)component;
            _ = EnableDesignMode(m_designerControl.DesignerControl, "DesignerControl");
        }
    }
}
