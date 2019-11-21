namespace CML.ControlEx
{
    partial class FormDgvColumnVisibility
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
            this.chksColumns = new System.Windows.Forms.CheckedListBox();
            this.btnEnter = new CML.ControlEx.CmlButtonEx();
            this.pnlOperate = new System.Windows.Forms.Panel();
            this.btnSelectNone = new CML.ControlEx.CmlButtonEx();
            this.btnSelectAll = new CML.ControlEx.CmlButtonEx();
            this.pnlOperate.SuspendLayout();
            this.SuspendLayout();
            // 
            // chksColumns
            // 
            this.chksColumns.BackColor = System.Drawing.Color.White;
            this.chksColumns.CheckOnClick = true;
            this.chksColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chksColumns.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chksColumns.FormattingEnabled = true;
            this.chksColumns.IntegralHeight = false;
            this.chksColumns.Location = new System.Drawing.Point(0, 0);
            this.chksColumns.Margin = new System.Windows.Forms.Padding(5);
            this.chksColumns.Name = "chksColumns";
            this.chksColumns.Size = new System.Drawing.Size(268, 242);
            this.chksColumns.TabIndex = 0;
            this.chksColumns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ChksColumns_ItemCheck);
            // 
            // btnEnter
            // 
            this.btnEnter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEnter.CP_CustomerInformation = null;
            this.btnEnter.CP_NewLineChar = '@';
            this.btnEnter.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEnter.ForeColor = System.Drawing.Color.Blue;
            this.btnEnter.Location = new System.Drawing.Point(10, 8);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(162, 96);
            this.btnEnter.TabIndex = 0;
            this.btnEnter.Text = "确 认";
            this.btnEnter.Click += new System.EventHandler(this.BtnEnter_Click);
            // 
            // pnlOperate
            // 
            this.pnlOperate.Controls.Add(this.btnSelectNone);
            this.pnlOperate.Controls.Add(this.btnSelectAll);
            this.pnlOperate.Controls.Add(this.btnEnter);
            this.pnlOperate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOperate.Location = new System.Drawing.Point(0, 242);
            this.pnlOperate.Name = "pnlOperate";
            this.pnlOperate.Size = new System.Drawing.Size(268, 112);
            this.pnlOperate.TabIndex = 1;
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSelectNone.CP_CustomerInformation = null;
            this.btnSelectNone.CP_NewLineChar = '@';
            this.btnSelectNone.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelectNone.Location = new System.Drawing.Point(178, 58);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(80, 46);
            this.btnSelectNone.TabIndex = 3;
            this.btnSelectNone.Text = "清空";
            this.btnSelectNone.Click += new System.EventHandler(this.BtnSelectNone_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSelectAll.CP_CustomerInformation = null;
            this.btnSelectAll.CP_NewLineChar = '@';
            this.btnSelectAll.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelectAll.Location = new System.Drawing.Point(178, 8);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(80, 46);
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new System.EventHandler(this.BtnSelectAll_Click);
            // 
            // FormDgvColumnVisibility
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(268, 354);
            this.Controls.Add(this.chksColumns);
            this.Controls.Add(this.pnlOperate);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDgvColumnVisibility";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据列可见性选择";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDgvColumnVisibility_FormClosing);
            this.Shown += new System.EventHandler(this.FormDgvColumnVisibility_Shown);
            this.pnlOperate.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chksColumns;
        private CmlButtonEx btnEnter;
        private System.Windows.Forms.Panel pnlOperate;
        private CmlButtonEx btnSelectNone;
        private CmlButtonEx btnSelectAll;
    }
}