namespace jcPimSoftware
{
    partial class YesNo
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
            this.b_Yes = new System.Windows.Forms.Button();
            this.b_No = new System.Windows.Forms.Button();
            this.lb_Txt = new System.Windows.Forms.Label();
            this.picBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // b_Yes
            // 
            this.b_Yes.Location = new System.Drawing.Point(66, 89);
            this.b_Yes.Name = "b_Yes";
            this.b_Yes.Size = new System.Drawing.Size(77, 30);
            this.b_Yes.TabIndex = 0;
            this.b_Yes.Text = "是";
            this.b_Yes.UseVisualStyleBackColor = true;
            this.b_Yes.Click += new System.EventHandler(this.b_Yes_Click);
            // 
            // b_No
            // 
            this.b_No.Location = new System.Drawing.Point(204, 89);
            this.b_No.Name = "b_No";
            this.b_No.Size = new System.Drawing.Size(77, 30);
            this.b_No.TabIndex = 1;
            this.b_No.Text = "否";
            this.b_No.UseVisualStyleBackColor = true;
            this.b_No.Click += new System.EventHandler(this.b_No_Click);
            // 
            // lb_Txt
            // 
            this.lb_Txt.AutoSize = true;
            this.lb_Txt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_Txt.Location = new System.Drawing.Point(63, 16);
            this.lb_Txt.Name = "lb_Txt";
            this.lb_Txt.Size = new System.Drawing.Size(264, 16);
            this.lb_Txt.TabIndex = 2;
            this.lb_Txt.Text = "删除后将不能恢复，确定要删除吗？";
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(24, 18);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(32, 32);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox.TabIndex = 3;
            this.picBox.TabStop = false;
            // 
            // YesNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 143);
            this.ControlBox = false;
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.lb_Txt);
            this.Controls.Add(this.b_No);
            this.Controls.Add(this.b_Yes);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "YesNo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "警告";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.YesNo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        public System.Windows.Forms.Button b_Yes;
        public System.Windows.Forms.Button b_No;
        public System.Windows.Forms.Label lb_Txt;
    }
}