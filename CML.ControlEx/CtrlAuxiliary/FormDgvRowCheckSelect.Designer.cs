namespace CML.ControlEx
{
    partial class FormDgvRowCheckSelect
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRange = new System.Windows.Forms.Label();
            this.txtIndexMax = new CML.ControlEx.CmlTextBoxEx();
            this.txtIndexMin = new CML.ControlEx.CmlTextBoxEx();
            this.btnCancel = new CML.ControlEx.CmlButtonEx();
            this.btnEnter = new CML.ControlEx.CmlButtonEx();
            this.btnAll = new CML.ControlEx.CmlButtonEx();
            this.btnClear = new CML.ControlEx.CmlButtonEx();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(494, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入选择范围";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(44, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "从";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(167, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "到";
            // 
            // lblRange
            // 
            this.lblRange.AutoSize = true;
            this.lblRange.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRange.Location = new System.Drawing.Point(304, 93);
            this.lblRange.Name = "lblRange";
            this.lblRange.Size = new System.Drawing.Size(122, 19);
            this.lblRange.TabIndex = 5;
            this.lblRange.Text = "（范围1-N）";
            // 
            // txtIndexMax
            // 
            this.txtIndexMax.BackColor = System.Drawing.Color.White;
            this.txtIndexMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIndexMax.CP_DisableBackColor = System.Drawing.Color.LightGray;
            this.txtIndexMax.CP_InputType = CML.ControlEx.EInputTypes.Int;
            this.txtIndexMax.CP_Maximum = 1D;
            this.txtIndexMax.CP_Minimum = 1D;
            this.txtIndexMax.CP_PasswordChar = '\0';
            this.txtIndexMax.CP_ReadOnlyBackColor = System.Drawing.Color.LightGray;
            this.txtIndexMax.CP_Title = "";
            this.txtIndexMax.CP_TitleAlignment = CML.ControlEx.ETextAlignment.Center;
            this.txtIndexMax.CP_TitleBackColor = System.Drawing.Color.LightGray;
            this.txtIndexMax.CP_TitleFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIndexMax.CP_TitleForeColor = System.Drawing.Color.Black;
            this.txtIndexMax.CP_Unit = "";
            this.txtIndexMax.CP_UnitAlignment = CML.ControlEx.ETextAlignment.Center;
            this.txtIndexMax.CP_UnitBackColor = System.Drawing.Color.LightGray;
            this.txtIndexMax.CP_UnitFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIndexMax.CP_UnitForeColor = System.Drawing.Color.Black;
            this.txtIndexMax.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIndexMax.ForeColor = System.Drawing.Color.Black;
            this.txtIndexMax.Location = new System.Drawing.Point(203, 87);
            this.txtIndexMax.Margin = new System.Windows.Forms.Padding(4);
            this.txtIndexMax.Name = "txtIndexMax";
            this.txtIndexMax.Size = new System.Drawing.Size(81, 30);
            this.txtIndexMax.TabIndex = 4;
            // 
            // txtIndexMin
            // 
            this.txtIndexMin.BackColor = System.Drawing.Color.White;
            this.txtIndexMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIndexMin.CP_DisableBackColor = System.Drawing.Color.LightGray;
            this.txtIndexMin.CP_InputType = CML.ControlEx.EInputTypes.Int;
            this.txtIndexMin.CP_Maximum = 1D;
            this.txtIndexMin.CP_Minimum = 1D;
            this.txtIndexMin.CP_PasswordChar = '\0';
            this.txtIndexMin.CP_ReadOnlyBackColor = System.Drawing.Color.LightGray;
            this.txtIndexMin.CP_Title = "";
            this.txtIndexMin.CP_TitleAlignment = CML.ControlEx.ETextAlignment.Center;
            this.txtIndexMin.CP_TitleBackColor = System.Drawing.Color.LightGray;
            this.txtIndexMin.CP_TitleFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIndexMin.CP_TitleForeColor = System.Drawing.Color.Black;
            this.txtIndexMin.CP_Unit = "";
            this.txtIndexMin.CP_UnitAlignment = CML.ControlEx.ETextAlignment.Center;
            this.txtIndexMin.CP_UnitBackColor = System.Drawing.Color.LightGray;
            this.txtIndexMin.CP_UnitFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIndexMin.CP_UnitForeColor = System.Drawing.Color.Black;
            this.txtIndexMin.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIndexMin.ForeColor = System.Drawing.Color.Black;
            this.txtIndexMin.Location = new System.Drawing.Point(80, 87);
            this.txtIndexMin.Margin = new System.Windows.Forms.Padding(4);
            this.txtIndexMin.Name = "txtIndexMin";
            this.txtIndexMin.Size = new System.Drawing.Size(81, 30);
            this.txtIndexMin.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.CP_CustomerInformation = null;
            this.btnCancel.CP_NewLineChar = '@';
            this.btnCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.Maroon;
            this.btnCancel.Location = new System.Drawing.Point(402, 153);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 46);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnter.CP_CustomerInformation = null;
            this.btnEnter.CP_NewLineChar = '@';
            this.btnEnter.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEnter.ForeColor = System.Drawing.Color.Blue;
            this.btnEnter.Location = new System.Drawing.Point(316, 153);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(80, 46);
            this.btnEnter.TabIndex = 8;
            this.btnEnter.Text = "确认";
            this.btnEnter.Click += new System.EventHandler(this.BtnEnter_Click);
            // 
            // btnAll
            // 
            this.btnAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAll.CP_CustomerInformation = null;
            this.btnAll.CP_NewLineChar = '@';
            this.btnAll.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAll.ForeColor = System.Drawing.Color.Blue;
            this.btnAll.Location = new System.Drawing.Point(98, 153);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(80, 46);
            this.btnAll.TabIndex = 7;
            this.btnAll.Text = "全选";
            this.btnAll.Click += new System.EventHandler(this.BtnAll_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.CP_CustomerInformation = null;
            this.btnClear.CP_NewLineChar = '@';
            this.btnClear.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.ForeColor = System.Drawing.Color.Blue;
            this.btnClear.Location = new System.Drawing.Point(12, 153);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 46);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "清空";
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // FormDgvRowCheckSelect
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(494, 211);
            this.Controls.Add(this.lblRange);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIndexMax);
            this.Controls.Add(this.txtIndexMin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.btnEnter);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(510, 250);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(510, 250);
            this.Name = "FormDgvRowCheckSelect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据行范围选择";
            this.Shown += new System.EventHandler(this.FormDgvRowCheckSelect_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CmlButtonEx btnCancel;
        private CmlButtonEx btnEnter;
        private System.Windows.Forms.Label label1;
        private CmlTextBoxEx txtIndexMin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private CmlTextBoxEx txtIndexMax;
        private System.Windows.Forms.Label lblRange;
        private CmlButtonEx btnAll;
        private CmlButtonEx btnClear;
    }
}