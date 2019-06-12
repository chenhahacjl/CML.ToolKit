using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 美化登录窗体
    /// </summary>
    public partial class CmlFormLogin : Form
    {
        #region 私有变量
        private bool isFinishTimeEvent = false;
        private Point m_ptLocation = new Point(0, 0);
        private DialogResult m_drDialogResult = DialogResult.Cancel;
        #endregion

        #region 公共属性
        /// <summary>
        /// 用户信息
        /// </summary>
        public ModLoginInfo CP_UserInfo { get; private set; } = new ModLoginInfo("", "");

        /// <summary>
        /// 背景图片
        /// </summary>
        public Image CP_BackgroundImage { set => pnlLogo.BackgroundImage = value; }

        /// <summary>
        /// 头像图片
        /// </summary>
        public Image CP_HeadImage { set => picHead.Image = value; }
        #endregion

        #region 登录委托事件
        /// <summary>
        /// 登录事件委托
        /// </summary>
        /// <param name="Username">用户名</param>
        /// <param name="Password">密码</param>
        /// <returns>登录结果</returns>
        public delegate ELoginResult LoginEventHandler(string Username, string Password);

        /// <summary>
        /// 登录事件
        /// </summary>
        public event LoginEventHandler CE_LoginEvent;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数，请添加登录事件（LoginEvent）
        /// </summary>
        public CmlFormLogin()
        {
            InitializeComponent();

            Icon = FindForm().Icon;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 动画关闭窗体
        /// </summary>
        private void CloseForm()
        {
            isFinishTimeEvent = false;
            timerFormClose.Enabled = true;
            timerFormClose.Start();

            new Thread(() =>
            {
                while (!isFinishTimeEvent)
                {
                    Thread.Sleep(100);
                }

                InvokeOperate.InvokeUI(this, new Action(() =>
                {
                    Close();
                }));
            })
            { IsBackground = true }.Start();
        }
        #endregion

        #region 窗体事件
        private void FormLogin_Load(object sender, EventArgs e)
        {
            timerFormShow.Enabled = true;
            timerFormShow.Start();
        }

        private void FormLogin_Shown(object sender, EventArgs e)
        {
            if (CE_LoginEvent == null)
            {
                InvokeOperate.InvokeUI(this, new Action(() =>
                {
                    MessageBox.Show("未定义登录事件，无法登录！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CloseForm();
                }));
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = m_drDialogResult;
        }
        #endregion

        #region 窗体移动事件
        private void WindowMove_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_ptLocation = e.Location;
            }
        }

        private void WindowMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.Location.X - m_ptLocation.X;
                Top += e.Location.Y - m_ptLocation.Y;
            }
        }
        #endregion

        #region 按钮事件
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("请输入用户名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtUserName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("请输入登录密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtPassword.Focus();
                return;
            }

            txtUserName.Visible = false;
            txtPassword.Visible = false;
            btnLogin.Visible = false;
            btnExit.Visible = false;

            isFinishTimeEvent = false;
            timerLoginIn.Enabled = true;
            timerLoginIn.Start();

            new Thread(() =>
            {
                while (!isFinishTimeEvent)
                {
                    Thread.Sleep(100);
                }

                ELoginResult result = CE_LoginEvent.Invoke(txtUserName.Text, txtPassword.Text);
                if (result == ELoginResult.Success)
                {
                    m_drDialogResult = DialogResult.OK;
                    CP_UserInfo = new ModLoginInfo(txtUserName.Text, txtPassword.Text);

                    InvokeOperate.InvokeUI(this, new Action(() =>
                    {
                        lblLoginTip.Text = "登录成功！";
                        CloseForm();
                    }));
                }
                else
                {
                    InvokeOperate.InvokeUI(this, new Action(() =>
                    {
                        picHead.Left = 37;
                        txtUserName.Visible = true;
                        txtPassword.Visible = true;
                        btnLogin.Visible = true;
                        btnExit.Visible = true;
                        lblLoginTip.Visible = false;

                        txtPassword.CF_Clear();
                        txtPassword.Focus();

                        MessageBox.Show(
                            result == ELoginResult.WrongUsernameOrPassword ? "用户名或密码输入错误！" :
                            result == ELoginResult.NetworkError ? "登录失败，请检查网络！" :
                            "用户权限不足，无法登录！",
                            "提示", MessageBoxButtons.OK, MessageBoxIcon.Information
                        );
                    }));
                }
            })
            { IsBackground = true }.Start();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }
        #endregion

        #region 计时器事件
        private void TimerFormShow_Tick(object sender, EventArgs e)
        {
            if (Opacity < 0.95)
            {
                Opacity += 0.03;
            }
            else
            {
                timerFormShow.Stop();
            }
        }

        private void TimerFormClose_Tick(object sender, EventArgs e)
        {
            if (Opacity > 0)
            {
                Opacity -= 0.03;
            }
            else
            {
                isFinishTimeEvent = true;
                timerFormClose.Stop();
            }
        }

        private void TimerLoginIn_Tick(object sender, EventArgs e)
        {
            int nNewLeft = (Width - picHead.Width) / 2;

            if (picHead.Left != nNewLeft)
            {
                if (Math.Abs(nNewLeft - picHead.Left) > 10)
                {
                    picHead.Left += 10;
                }
                else
                {
                    picHead.Left = nNewLeft;
                }
            }
            else
            {
                timerLoginIn.Stop();

                lblLoginTip.Text = "　登录中，请稍候...";
                lblLoginTip.Visible = true;
                isFinishTimeEvent = true;
            }
        }
        #endregion

        #region 文本框回车响应
        private void TxtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txtUserName.Text))
            {
                txtPassword.Focus();
            }
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txtPassword.Text))
            {
                BtnLogin_Click(null, null);
            }
        }
        #endregion
    }
}
