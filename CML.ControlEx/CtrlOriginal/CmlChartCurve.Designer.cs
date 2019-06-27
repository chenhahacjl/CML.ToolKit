namespace CML.ControlEx
{
    partial class CmlChartCurve
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
            this.pnlAxis = new System.Windows.Forms.Panel();
            this.picChart = new System.Windows.Forms.PictureBox();
            this.pnlAxis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picChart)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlAxis
            // 
            this.pnlAxis.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAxis.AutoScroll = true;
            this.pnlAxis.BackColor = System.Drawing.Color.Transparent;
            this.pnlAxis.Controls.Add(this.picChart);
            this.pnlAxis.Location = new System.Drawing.Point(43, 29);
            this.pnlAxis.Name = "pnlAxis";
            this.pnlAxis.Size = new System.Drawing.Size(766, 446);
            this.pnlAxis.TabIndex = 0;
            this.pnlAxis.Scroll += new System.Windows.Forms.ScrollEventHandler(this.PnlAxis_Scroll);
            // 
            // picChart
            // 
            this.picChart.Location = new System.Drawing.Point(0, 0);
            this.picChart.Name = "picChart";
            this.picChart.Size = new System.Drawing.Size(351, 209);
            this.picChart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picChart.TabIndex = 2;
            this.picChart.TabStop = false;
            this.picChart.Paint += new System.Windows.Forms.PaintEventHandler(this.PicChart_Paint);
            this.picChart.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PicChart_MouseDoubleClick);
            this.picChart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PicChart_MouseDown);
            this.picChart.MouseEnter += new System.EventHandler(this.PicChart_MouseEnter);
            this.picChart.MouseLeave += new System.EventHandler(this.PicChart_MouseLeave);
            this.picChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PicChart_MouseMove);
            this.picChart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PicChart_MouseUp);
            // 
            // CmlChartCurve
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.Controls.Add(this.pnlAxis);
            this.Name = "CmlChartCurve";
            this.Size = new System.Drawing.Size(852, 478);
            this.Load += new System.EventHandler(this.CurveChart_Load);
            this.SizeChanged += new System.EventHandler(this.CurveChart_SizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CurveChart_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CurveChart_MouseMove);
            this.pnlAxis.ResumeLayout(false);
            this.pnlAxis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlAxis;
        private System.Windows.Forms.PictureBox picChart;
    }
}
