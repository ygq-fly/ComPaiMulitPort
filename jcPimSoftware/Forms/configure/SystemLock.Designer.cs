namespace jcPimSoftware
{
    partial class SystemLock
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
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxLockPsd = new System.Windows.Forms.TextBox();
            this.btnLock = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.groupReset = new System.Windows.Forms.GroupBox();
            this.lblwarnning = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.tbxAgain = new System.Windows.Forms.TextBox();
            this.tbxNew = new System.Windows.Forms.TextBox();
            this.tbxOld = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkReset = new System.Windows.Forms.CheckBox();
            this.groupReset.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Location = new System.Drawing.Point(305, 281);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lock psd:";
            // 
            // tbxLockPsd
            // 
            this.tbxLockPsd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxLockPsd.Location = new System.Drawing.Point(98, 27);
            this.tbxLockPsd.Name = "tbxLockPsd";
            this.tbxLockPsd.PasswordChar = '*';
            this.tbxLockPsd.Size = new System.Drawing.Size(201, 26);
            this.tbxLockPsd.TabIndex = 2;
            this.tbxLockPsd.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbxLockPsd_MouseDoubleClick);
            // 
            // btnLock
            // 
            this.btnLock.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLock.Location = new System.Drawing.Point(305, 30);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(75, 23);
            this.btnLock.TabIndex = 3;
            this.btnLock.Text = "Lock";
            this.btnLock.UseVisualStyleBackColor = true;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(95, 56);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(240, 16);
            this.lblInfo.TabIndex = 4;
            this.lblInfo.Text = "Please entry your password...";
            // 
            // groupReset
            // 
            this.groupReset.Controls.Add(this.lblwarnning);
            this.groupReset.Controls.Add(this.btnOK);
            this.groupReset.Controls.Add(this.tbxAgain);
            this.groupReset.Controls.Add(this.tbxNew);
            this.groupReset.Controls.Add(this.tbxOld);
            this.groupReset.Controls.Add(this.label4);
            this.groupReset.Controls.Add(this.label3);
            this.groupReset.Controls.Add(this.label2);
            this.groupReset.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupReset.Location = new System.Drawing.Point(7, 84);
            this.groupReset.Name = "groupReset";
            this.groupReset.Size = new System.Drawing.Size(378, 191);
            this.groupReset.TabIndex = 5;
            this.groupReset.TabStop = false;
            this.groupReset.Text = "Reset password";
            // 
            // lblwarnning
            // 
            this.lblwarnning.AutoSize = true;
            this.lblwarnning.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblwarnning.ForeColor = System.Drawing.Color.Red;
            this.lblwarnning.Location = new System.Drawing.Point(6, 156);
            this.lblwarnning.Name = "lblwarnning";
            this.lblwarnning.Size = new System.Drawing.Size(64, 16);
            this.lblwarnning.TabIndex = 9;
            this.lblwarnning.Text = "Info...";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(298, 121);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "O K";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tbxAgain
            // 
            this.tbxAgain.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxAgain.Location = new System.Drawing.Point(100, 118);
            this.tbxAgain.Name = "tbxAgain";
            this.tbxAgain.PasswordChar = '*';
            this.tbxAgain.Size = new System.Drawing.Size(187, 26);
            this.tbxAgain.TabIndex = 7;
            this.tbxAgain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbxAgain_MouseDoubleClick);
            // 
            // tbxNew
            // 
            this.tbxNew.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxNew.Location = new System.Drawing.Point(100, 74);
            this.tbxNew.Name = "tbxNew";
            this.tbxNew.PasswordChar = '*';
            this.tbxNew.Size = new System.Drawing.Size(187, 26);
            this.tbxNew.TabIndex = 6;
            this.tbxNew.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbxNew_MouseDoubleClick);
            // 
            // tbxOld
            // 
            this.tbxOld.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxOld.Location = new System.Drawing.Point(100, 25);
            this.tbxOld.Name = "tbxOld";
            this.tbxOld.PasswordChar = '*';
            this.tbxOld.Size = new System.Drawing.Size(187, 26);
            this.tbxOld.TabIndex = 5;
            this.tbxOld.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbxOld_MouseDoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(6, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Again psd:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(22, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "New psd:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(22, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Old psd:";
            // 
            // chkReset
            // 
            this.chkReset.AutoSize = true;
            this.chkReset.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkReset.Location = new System.Drawing.Point(12, 287);
            this.chkReset.Name = "chkReset";
            this.chkReset.Size = new System.Drawing.Size(67, 20);
            this.chkReset.TabIndex = 6;
            this.chkReset.Text = "Reset";
            this.chkReset.UseVisualStyleBackColor = true;
            this.chkReset.CheckedChanged += new System.EventHandler(this.chkReset_CheckedChanged);
            // 
            // SystemLock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 335);
            this.ControlBox = false;
            this.Controls.Add(this.chkReset);
            this.Controls.Add(this.groupReset);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnLock);
            this.Controls.Add(this.tbxLockPsd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SystemLock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SystemLock";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SystemLock_Load);
            this.groupReset.ResumeLayout(false);
            this.groupReset.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxLockPsd;
        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.GroupBox groupReset;
        private System.Windows.Forms.CheckBox chkReset;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxAgain;
        private System.Windows.Forms.TextBox tbxNew;
        private System.Windows.Forms.TextBox tbxOld;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblwarnning;
    }
}