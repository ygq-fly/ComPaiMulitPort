namespace jcPimSoftware
{
    partial class CopyProgress
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CopyProgress));
            this.pb = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lb1 = new System.Windows.Forms.Label();
            this.labSize = new System.Windows.Forms.Label();
            this.Cancle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(7, 93);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(323, 16);
            this.pb.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(33, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(260, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Location = new System.Drawing.Point(6, 67);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(112, 16);
            this.lb1.TabIndex = 2;
            this.lb1.Text = "Ready to copy";
            // 
            // labSize
            // 
            this.labSize.AutoSize = true;
            this.labSize.Location = new System.Drawing.Point(219, 68);
            this.labSize.Name = "labSize";
            this.labSize.Size = new System.Drawing.Size(0, 16);
            this.labSize.TabIndex = 3;
            // 
            // Cancle
            // 
            this.Cancle.Location = new System.Drawing.Point(341, 86);
            this.Cancle.Name = "Cancle";
            this.Cancle.Size = new System.Drawing.Size(75, 30);
            this.Cancle.TabIndex = 4;
            this.Cancle.Text = "取消";
            this.Cancle.UseVisualStyleBackColor = true;
            this.Cancle.Click += new System.EventHandler(this.Cancle_Click);
            // 
            // CopyProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 144);
            this.ControlBox = false;
            this.Controls.Add(this.Cancle);
            this.Controls.Add(this.labSize);
            this.Controls.Add(this.lb1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pb);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CopyProgress";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Copying...";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.CopyProgress_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.Label labSize;
        private System.Windows.Forms.Button Cancle;
    }
}