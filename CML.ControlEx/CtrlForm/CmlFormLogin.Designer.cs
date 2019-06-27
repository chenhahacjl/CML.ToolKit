namespace CML.ControlEx
{
    partial class CmlFormLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CmlFormLogin));
            this.picHead = new System.Windows.Forms.PictureBox();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.timerLoginIn = new System.Windows.Forms.Timer(this.components);
            this.timerFormShow = new System.Windows.Forms.Timer(this.components);
            this.timerFormClose = new System.Windows.Forms.Timer(this.components);
            this.lblLoginTip = new System.Windows.Forms.Label();
            this.txtPassword = new CML.ControlEx.CmlTextBoxEx();
            this.txtUserName = new CML.ControlEx.CmlTextBoxEx();
            this.btnExit = new CML.ControlEx.CmlButtonEx();
            this.btnLogin = new CML.ControlEx.CmlButtonEx();
            ((System.ComponentModel.ISupportInitialize)(this.picHead)).BeginInit();
            this.SuspendLayout();
            // 
            // picHead
            // 
            this.picHead.BackColor = System.Drawing.Color.Transparent;
            this.picHead.Image = ((System.Drawing.Image)(resources.GetObject("picHead.Image")));
            this.picHead.Location = new System.Drawing.Point(37, 171);
            this.picHead.Name = "picHead";
            this.picHead.Size = new System.Drawing.Size(95, 92);
            this.picHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHead.TabIndex = 21;
            this.picHead.TabStop = false;
            this.picHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WindowMove_MouseDown);
            this.picHead.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WindowMove_MouseMove);
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlLogo.BackgroundImage")));
            this.pnlLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(424, 151);
            this.pnlLogo.TabIndex = 22;
            this.pnlLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WindowMove_MouseDown);
            this.pnlLogo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WindowMove_MouseMove);
            // 
            // timerLoginIn
            // 
            this.timerLoginIn.Interval = 10;
            this.timerLoginIn.Tick += new System.EventHandler(this.TimerLoginIn_Tick);
            // 
            // timerFormShow
            // 
            this.timerFormShow.Interval = 10;
            this.timerFormShow.Tick += new System.EventHandler(this.TimerFormShow_Tick);
            // 
            // timerFormClose
            // 
            this.timerFormClose.Interval = 10;
            this.timerFormClose.Tick += new System.EventHandler(this.TimerFormClose_Tick);
            // 
            // lblLoginTip
            // 
            this.lblLoginTip.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLoginTip.ForeColor = System.Drawing.Color.Black;
            this.lblLoginTip.Location = new System.Drawing.Point(37, 273);
            this.lblLoginTip.Name = "lblLoginTip";
            this.lblLoginTip.Size = new System.Drawing.Size(351, 45);
            this.lblLoginTip.TabIndex = 23;
            this.lblLoginTip.Text = "　登录中，请稍候...";
            this.lblLoginTip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLoginTip.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.CP_DisableBackColor = System.Drawing.Color.LightGray;
            this.txtPassword.CP_Maximum = 65535D;
            this.txtPassword.CP_Minimum = -65535D;
            this.txtPassword.CP_PasswordChar = '*';
            this.txtPassword.CP_ReadOnlyBackColor = System.Drawing.Color.LightGray;
            this.txtPassword.CP_Title = "密码";
            this.txtPassword.CP_TitleAlignment = CML.ControlEx.ETextAlignment.Center;
            this.txtPassword.CP_TitleBackColor = System.Drawing.Color.DeepSkyBlue;
            this.txtPassword.CP_TitleFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPassword.CP_TitleForeColor = System.Drawing.Color.White;
            this.txtPassword.CP_Unit = "";
            this.txtPassword.CP_UnitAlignment = CML.ControlEx.ETextAlignment.Center;
            this.txtPassword.CP_UnitBackColor = System.Drawing.Color.LightGray;
            this.txtPassword.CP_UnitFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPassword.CP_UnitForeColor = System.Drawing.Color.Black;
            this.txtPassword.Font = new System.Drawing.Font("微软雅黑", 14.5F);
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Location = new System.Drawing.Point(139, 222);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(249, 36);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.CE_KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPassword_KeyDown);
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.Color.White;
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.CP_DisableBackColor = System.Drawing.Color.LightGray;
            this.txtUserName.CP_Maximum = 65535D;
            this.txtUserName.CP_Minimum = -65535D;
            this.txtUserName.CP_PasswordChar = '\0';
            this.txtUserName.CP_ReadOnlyBackColor = System.Drawing.Color.LightGray;
            this.txtUserName.CP_Title = "用户";
            this.txtUserName.CP_TitleAlignment = CML.ControlEx.ETextAlignment.Center;
            this.txtUserName.CP_TitleBackColor = System.Drawing.Color.DeepSkyBlue;
            this.txtUserName.CP_TitleFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserName.CP_TitleForeColor = System.Drawing.Color.White;
            this.txtUserName.CP_Unit = "";
            this.txtUserName.CP_UnitAlignment = CML.ControlEx.ETextAlignment.Center;
            this.txtUserName.CP_UnitBackColor = System.Drawing.Color.Red;
            this.txtUserName.CP_UnitFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserName.CP_UnitForeColor = System.Drawing.Color.Black;
            this.txtUserName.Font = new System.Drawing.Font("微软雅黑", 14.5F);
            this.txtUserName.ForeColor = System.Drawing.Color.Black;
            this.txtUserName.Location = new System.Drawing.Point(139, 176);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(249, 36);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.CE_KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtUserName_KeyDown);
            // 
            // btnExit
            // 
            this.btnExit.CP_ActiveColor = System.Drawing.Color.LightSkyBlue;
            this.btnExit.CP_CustomerInformation = null;
            this.btnExit.CP_NewLineChar = '@';
            this.btnExit.CP_OriginalColor = System.Drawing.Color.DeepSkyBlue;
            this.btnExit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(226, 283);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(135, 35);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "退   出";
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.CP_ActiveColor = System.Drawing.Color.LightSkyBlue;
            this.btnLogin.CP_CustomerInformation = null;
            this.btnLogin.CP_NewLineChar = '@';
            this.btnLogin.CP_OriginalColor = System.Drawing.Color.DeepSkyBlue;
            this.btnLogin.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(64, 283);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(135, 35);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "登   录";
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // CmlFormLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(424, 332);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.picHead);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.pnlLogo);
            this.Controls.Add(this.lblLoginTip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLogin";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CmlFormLogin";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLogin_FormClosing);
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.Shown += new System.EventHandler(this.FormLogin_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WindowMove_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WindowMove_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.picHead)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox picHead;
        private System.Windows.Forms.Panel pnlLogo;
        private CmlButtonEx btnLogin;
        private CmlButtonEx btnExit;
        private CmlTextBoxEx txtUserName;
        private CmlTextBoxEx txtPassword;
        private System.Windows.Forms.Timer timerLoginIn;
        private System.Windows.Forms.Timer timerFormShow;
        private System.Windows.Forms.Timer timerFormClose;
        private System.Windows.Forms.Label lblLoginTip;
    }
}