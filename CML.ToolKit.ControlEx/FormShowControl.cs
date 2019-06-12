using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        private void FormShowControl_Shown(object sender, EventArgs e)
        {
            string[] arrDate = new string[1000];
            List<float> arrData = new List<float>();

            for (int i = 0; i < 1000; i++)
            {
                arrDate[i] = Convert.ToString(i + 1);
                Random random = new Random(Guid.NewGuid().GetHashCode());
                arrData.Add(Convert.ToSingle(random.NextDouble() * 1000));
            }

            chartCurve1.CF_SetDateCustomer(arrDate);
            chartCurve1.CF_SetCurve("DATA", true, arrData.ToArray(), Color.Red, 1, false);
            chartCurve1.CP_ValueMaxLeft = arrData.Max();
            chartCurve1.CP_ValueMinLeft = arrData.Min();
            chartCurve1.CF_RenderCurveUI();
        }

        private void ButtonEx1_Click(object sender, EventArgs e)
        {
            CmlFormLogin login = new CmlFormLogin();
            login.CE_LoginEvent += (Username, Password) =>
            {
                return Username == Password ? ELoginResult.Success : ELoginResult.WrongUsernameOrPassword;
            };
            login.ShowDialog();
        }

        private void ButtonEx2_Click(object sender, EventArgs e)
        {
            _ = new CmlFormValueInput()
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
