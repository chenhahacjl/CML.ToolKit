namespace CML.ToolKit.ControlEx
{
    partial class CmlTextBoxEx
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
            this.pnlValue = new System.Windows.Forms.Panel();
            this.txtlimit = new CML.ToolKit.ControlEx.TextBoxBase();
            this.lblUnit = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUnitSplit = new System.Windows.Forms.Label();
            this.lblTitleSplit = new System.Windows.Forms.Label();
            this.pnlValue.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlValue
            // 
            this.pnlValue.BackColor = System.Drawing.Color.White;
            this.pnlValue.Controls.Add(this.txtlimit);
            this.pnlValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlValue.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlValue.Location = new System.Drawing.Point(1, 0);
            this.pnlValue.Margin = new System.Windows.Forms.Padding(5);
            this.pnlValue.Name = "pnlValue";
            this.pnlValue.Size = new System.Drawing.Size(179, 24);
            this.pnlValue.TabIndex = 4;
            this.pnlValue.Click += new System.EventHandler(this.PnlValue_Click);
            // 
            // txtlimit
            // 
            this.txtlimit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtlimit.BackColor = System.Drawing.Color.White;
            this.txtlimit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtlimit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtlimit.Location = new System.Drawing.Point(2, 3);
            this.txtlimit.Margin = new System.Windows.Forms.Padding(5);
            this.txtlimit.CP_Maximum = 65535D;
            this.txtlimit.CP_Minimum = -65535D;
            this.txtlimit.Name = "txtlimit";
            this.txtlimit.Size = new System.Drawing.Size(175, 19);
            this.txtlimit.TabIndex = 0;
            this.txtlimit.Text = "";
            this.txtlimit.ReadOnlyChanged += new System.EventHandler(this.Txtlimit_ReadOnlyChanged);
            this.txtlimit.EnabledChanged += new System.EventHandler(this.Txtlimit_EnabledChanged);
            this.txtlimit.FontChanged += new System.EventHandler(this.Txtlimit_FontChanged);
            this.txtlimit.TextChanged += new System.EventHandler(this.Txtlimit_TextChanged);
            this.txtlimit.Enter += new System.EventHandler(this.Txtlimit_Enter);
            this.txtlimit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txtlimit_KeyDown);
            this.txtlimit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Txtlimit_KeyUp);
            // 
            // lblUnit
            // 
            this.lblUnit.BackColor = System.Drawing.Color.LightGray;
            this.lblUnit.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblUnit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUnit.Location = new System.Drawing.Point(181, 0);
            this.lblUnit.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(0, 24);
            this.lblUnit.TabIndex = 3;
            this.lblUnit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUnit.Click += new System.EventHandler(this.LblUnit_Click);
            this.lblUnit.MouseEnter += new System.EventHandler(this.LblUnit_MouseEnter);
            this.lblUnit.MouseLeave += new System.EventHandler(this.LblUnit_MouseLeave);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.LightGray;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(0, 24);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUnitSplit
            // 
            this.lblUnitSplit.BackColor = System.Drawing.Color.LightGray;
            this.lblUnitSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUnitSplit.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblUnitSplit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUnitSplit.Location = new System.Drawing.Point(180, 0);
            this.lblUnitSplit.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblUnitSplit.Name = "lblUnitSplit";
            this.lblUnitSplit.Size = new System.Drawing.Size(1, 24);
            this.lblUnitSplit.TabIndex = 6;
            this.lblUnitSplit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUnitSplit.Visible = false;
            // 
            // lblTitleSplit
            // 
            this.lblTitleSplit.BackColor = System.Drawing.Color.LightGray;
            this.lblTitleSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitleSplit.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitleSplit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitleSplit.Location = new System.Drawing.Point(0, 0);
            this.lblTitleSplit.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTitleSplit.Name = "lblTitleSplit";
            this.lblTitleSplit.Size = new System.Drawing.Size(1, 24);
            this.lblTitleSplit.TabIndex = 7;
            this.lblTitleSplit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitleSplit.Visible = false;
            // 
            // CmlTextBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnlValue);
            this.Controls.Add(this.lblUnitSplit);
            this.Controls.Add(this.lblUnit);
            this.Controls.Add(this.lblTitleSplit);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CmlTextBox";
            this.Size = new System.Drawing.Size(181, 24);
            this.Click += new System.EventHandler(this.UnitTextBox_Click);
            this.Resize += new System.EventHandler(this.UnitTextBox_Resize);
            this.pnlValue.ResumeLayout(false);
            this.pnlValue.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlValue;
        private System.Windows.Forms.Label lblUnit;
        private TextBoxBase txtlimit;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUnitSplit;
        private System.Windows.Forms.Label lblTitleSplit;
    }
}
