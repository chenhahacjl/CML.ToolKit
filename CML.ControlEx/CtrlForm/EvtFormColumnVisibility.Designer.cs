namespace HZEVT.TestSystem.Controls
{
    partial class EvtFormColumnVisibility
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
            this.btn_Enter = new HZEVT.TestSystem.Controls.EvtButton();
            this.pnlOperate = new System.Windows.Forms.Panel();
            this.btnSelectNone = new HZEVT.TestSystem.Controls.EvtButton();
            this.btnSelectAll = new HZEVT.TestSystem.Controls.EvtButton();
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
            this.chksColumns.Size = new System.Drawing.Size(268, 295);
            this.chksColumns.TabIndex = 0;
            this.chksColumns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ChksColumns_ItemCheck);
            // 
            // btn_Enter
            // 
            this.btn_Enter.CustomerInformation = null;
            this.btn_Enter.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Enter.ForeColor = System.Drawing.Color.Blue;
            this.btn_Enter.Location = new System.Drawing.Point(8, 6);
            this.btn_Enter.Name = "btn_Enter";
            this.btn_Enter.Size = new System.Drawing.Size(80, 46);
            this.btn_Enter.TabIndex = 0;
            this.btn_Enter.Text = "确认";
            this.btn_Enter.Click += new System.EventHandler(this.Btn_Enter_Click);
            // 
            // pnlOperate
            // 
            this.pnlOperate.Controls.Add(this.btnSelectNone);
            this.pnlOperate.Controls.Add(this.btnSelectAll);
            this.pnlOperate.Controls.Add(this.btn_Enter);
            this.pnlOperate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOperate.Location = new System.Drawing.Point(0, 295);
            this.pnlOperate.Name = "pnlOperate";
            this.pnlOperate.Size = new System.Drawing.Size(268, 59);
            this.pnlOperate.TabIndex = 1;
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.CustomerInformation = null;
            this.btnSelectNone.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelectNone.Location = new System.Drawing.Point(180, 6);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(80, 46);
            this.btnSelectNone.TabIndex = 3;
            this.btnSelectNone.Text = "清空";
            this.btnSelectNone.Click += new System.EventHandler(this.BtnSelectNone_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.CustomerInformation = null;
            this.btnSelectAll.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelectAll.Location = new System.Drawing.Point(94, 6);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(80, 46);
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new System.EventHandler(this.BtnSelectAll_Click);
            // 
            // EvtFormColumnVisibility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 354);
            this.Controls.Add(this.chksColumns);
            this.Controls.Add(this.pnlOperate);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EvtFormColumnVisibility";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据列可见性选择";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EvtFormColumnVisibility_FormClosing);
            this.Shown += new System.EventHandler(this.FormColumnVisibility_Shown);
            this.pnlOperate.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chksColumns;
        private EvtButton btn_Enter;
        private System.Windows.Forms.Panel pnlOperate;
        private EvtButton btnSelectNone;
        private EvtButton btnSelectAll;
    }
}