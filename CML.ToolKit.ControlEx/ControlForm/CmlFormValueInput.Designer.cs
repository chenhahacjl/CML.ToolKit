namespace CML.ToolKit.ControlEx
{
    partial class CmlFormValueInput
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CmlFormValueInput));
            this.lnlDescription = new System.Windows.Forms.Label();
            this.btnCancel = new CML.ToolKit.ControlEx.CmlButtonEx();
            this.btnEnter = new CML.ToolKit.ControlEx.CmlButtonEx();
            this.utxtValue = new CML.ToolKit.ControlEx.CmlTextBoxEx();
            this.SuspendLayout();
            // 
            // lnlDescription
            // 
            this.lnlDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lnlDescription.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lnlDescription.Location = new System.Drawing.Point(59, 26);
            this.lnlDescription.Name = "lnlDescription";
            this.lnlDescription.Size = new System.Drawing.Size(287, 30);
            this.lnlDescription.TabIndex = 1;
            this.lnlDescription.Text = "请输入:";
            this.lnlDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.CP_CustomerInformation = null;
            this.btnCancel.CP_NewLineChar = '@';
            this.btnCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.Red;
            this.btnCancel.Location = new System.Drawing.Point(292, 129);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 38);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnter.CP_CustomerInformation = null;
            this.btnEnter.CP_NewLineChar = '@';
            this.btnEnter.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEnter.ForeColor = System.Drawing.Color.Blue;
            this.btnEnter.Location = new System.Drawing.Point(185, 129);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(89, 38);
            this.btnEnter.TabIndex = 1;
            this.btnEnter.Text = "确定";
            this.btnEnter.Click += new System.EventHandler(this.BtnEnter_Click);
            // 
            // utxtValue
            // 
            this.utxtValue.BackColor = System.Drawing.Color.White;
            this.utxtValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.utxtValue.CP_DisableBackColor = System.Drawing.Color.LightGray;
            this.utxtValue.CP_Maximum = 65535D;
            this.utxtValue.CP_Minimum = -65535D;
            this.utxtValue.CP_PasswordChar = '\0';
            this.utxtValue.CP_ReadOnlyBackColor = System.Drawing.Color.LightGray;
            this.utxtValue.CP_Title = "";
            this.utxtValue.CP_TitleAlignment = CML.ToolKit.ControlEx.ETextAlignment.Center;
            this.utxtValue.CP_TitleBackColor = System.Drawing.Color.LightGray;
            this.utxtValue.CP_TitleFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.utxtValue.CP_TitleForeColor = System.Drawing.Color.Black;
            this.utxtValue.CP_Unit = "";
            this.utxtValue.CP_UnitAlignment = CML.ToolKit.ControlEx.ETextAlignment.Center;
            this.utxtValue.CP_UnitBackColor = System.Drawing.Color.LightGray;
            this.utxtValue.CP_UnitFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.utxtValue.CP_UnitForeColor = System.Drawing.Color.Black;
            this.utxtValue.Font = new System.Drawing.Font("宋体", 15F);
            this.utxtValue.ForeColor = System.Drawing.Color.Black;
            this.utxtValue.Location = new System.Drawing.Point(59, 77);
            this.utxtValue.Margin = new System.Windows.Forms.Padding(4);
            this.utxtValue.Name = "utxtValue";
            this.utxtValue.Size = new System.Drawing.Size(287, 31);
            this.utxtValue.TabIndex = 3;
            // 
            // FormValueInput
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(404, 191);
            this.Controls.Add(this.utxtValue);
            this.Controls.Add(this.lnlDescription);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnEnter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(420, 230);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 230);
            this.Name = "FormValueInput";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "请输入数据";
            this.Shown += new System.EventHandler(this.ValueInputForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ValueInputForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ValueInputForm_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private CmlButtonEx btnEnter;
        private CmlButtonEx btnCancel;
        private System.Windows.Forms.Label lnlDescription;
        private CmlTextBoxEx utxtValue;
    }
}
