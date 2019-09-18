using System.Threading;
using System.Windows.Forms;

namespace CML.CommonEx.DebugEx
{
    /// <summary>
    /// 通用调试模块
    /// </summary>
    public class CommonDebugModel : IDebugDev
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModelName => "通用调试工具";

        /// <summary>
        /// 模块描述
        /// </summary>
        public string ModelDesc => "通用的C#调试工具";

        /// <summary>
        /// 模块识别码
        /// </summary>
        public string ModelID => null;

        /// <summary>
        /// 项目实例
        /// </summary>
        public object ProjectObject { get; set; }

        /// <summary>
        /// 执行调试
        /// </summary>
        public void ExecuteDebug()
        {
            DialogResult dr = MessageBox.Show("使用模态对话框显示通用调试模块窗体？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                new FormCommonDebugModel(ProjectObject).ShowDialog();
            }
            else
            {
                new FormCommonDebugModel(ProjectObject).Show();
            }
        }
    }
}
