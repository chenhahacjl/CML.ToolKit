using System;
using System.Windows.Forms;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 控件展示窗体
    /// </summary>
    public partial class FormShowControl : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FormShowControl()
        {
            InitializeComponent();
        }

        private void ButtonEx1_Click(object sender, EventArgs e)
        {
            FormLogin login = new FormLogin();
            login.CE_LoginEvent += (Username, Password) =>
            {
                return Username == Password ? ELoginResult.Success : ELoginResult.WrongUsernameOrPassword;
            };
            login.ShowDialog();
        }

        private void ButtonEx2_Click(object sender, EventArgs e)
        {
            _ = new FormValueInput()
            {
                CP_Title = "这个时输入框标题",
                CP_Description = "描述信息(输入范围0-1000的整数): ",
                CP_Unit = "单位",
                CP_InputType = EInputTypes.Int,
                CP_Minimum = 0,
                CP_Maximum = 1000
            }.ShowDialog();
        }
    }
}
