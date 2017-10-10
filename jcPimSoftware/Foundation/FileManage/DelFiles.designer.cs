namespace jcPimSoftware
{
    partial class DelFiles
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
            this.lb = new System.Windows.Forms.Label();
            this.prob = new System.Windows.Forms.ProgressBar();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.listb = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lb
            // 
            this.lb.AutoSize = true;
            this.lb.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb.Location = new System.Drawing.Point(80, 9);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(294, 19);
            this.lb.TabIndex = 0;
            this.lb.Text = "确定要删除所选，请点确认按钮！";
            // 
            // prob
            // 
            this.prob.Location = new System.Drawing.Point(4, 47);
            this.prob.Name = "prob";
            this.prob.Size = new System.Drawing.Size(468, 15);
            this.prob.TabIndex = 1;
            // 
            // btn1
            // 
            this.btn1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn1.Location = new System.Drawing.Point(46, 75);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(89, 30);
            this.btn1.TabIndex = 2;
            this.btn1.Text = "确认";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn2
            // 
            this.btn2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn2.Location = new System.Drawing.Point(180, 75);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(89, 30);
            this.btn2.TabIndex = 3;
            this.btn2.Text = "关闭";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // btn3
            // 
            this.btn3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn3.Location = new System.Drawing.Point(314, 75);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(89, 30);
            this.btn3.TabIndex = 4;
            this.btn3.Text = "详细>>";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.Click += new System.EventHandler(this.btn3_Click);
            // 
            // listb
            // 
            this.listb.FormattingEnabled = true;
            this.listb.HorizontalScrollbar = true;
            this.listb.ItemHeight = 16;
            this.listb.Location = new System.Drawing.Point(5, 128);
            this.listb.Name = "listb";
            this.listb.Size = new System.Drawing.Size(466, 244);
            this.listb.TabIndex = 5;
            // 
            // DelFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 120);
            this.ControlBox = false;
            this.Controls.Add(this.listb);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.prob);
            this.Controls.Add(this.lb);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DelFiles";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "删除";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.DelFiles_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DelFiles_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb;
        private System.Windows.Forms.ProgressBar prob;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.ListBox listb;
    }
}