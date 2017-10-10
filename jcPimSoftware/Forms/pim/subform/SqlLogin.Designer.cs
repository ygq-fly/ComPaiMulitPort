namespace jcPimSoftware
{
    partial class SqlLogin
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
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cB_sqlServerType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tB_sqlpassward = new System.Windows.Forms.TextBox();
            this.tB_sqluser = new System.Windows.Forms.TextBox();
            this.tB_sqldatabase = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tB_sqladdr = new System.Windows.Forms.TextBox();
            this.tB_ftpaddr = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tB_Tester = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tB_ftppw = new System.Windows.Forms.TextBox();
            this.tB_ftpuser = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(175, 369);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 46);
            this.button1.TabIndex = 0;
            this.button1.Text = "登入";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cB_sqlServerType);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tB_sqlpassward);
            this.groupBox2.Controls.Add(this.tB_sqluser);
            this.groupBox2.Controls.Add(this.tB_sqldatabase);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tB_sqladdr);
            this.groupBox2.Location = new System.Drawing.Point(12, 170);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(354, 191);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // cB_sqlServerType
            // 
            this.cB_sqlServerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_sqlServerType.FormattingEnabled = true;
            this.cB_sqlServerType.Items.AddRange(new object[] {
            "SQLServer2005"});
            this.cB_sqlServerType.Location = new System.Drawing.Point(139, 20);
            this.cB_sqlServerType.Name = "cB_sqlServerType";
            this.cB_sqlServerType.Size = new System.Drawing.Size(163, 20);
            this.cB_sqlServerType.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(40, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 14);
            this.label6.TabIndex = 8;
            this.label6.Text = "服务器类型：";
            // 
            // tB_sqlpassward
            // 
            this.tB_sqlpassward.Location = new System.Drawing.Point(140, 160);
            this.tB_sqlpassward.Name = "tB_sqlpassward";
            this.tB_sqlpassward.PasswordChar = '*';
            this.tB_sqlpassward.Size = new System.Drawing.Size(162, 21);
            this.tB_sqlpassward.TabIndex = 9;
            // 
            // tB_sqluser
            // 
            this.tB_sqluser.Location = new System.Drawing.Point(140, 125);
            this.tB_sqluser.Name = "tB_sqluser";
            this.tB_sqluser.Size = new System.Drawing.Size(162, 21);
            this.tB_sqluser.TabIndex = 8;
            // 
            // tB_sqldatabase
            // 
            this.tB_sqldatabase.Location = new System.Drawing.Point(139, 90);
            this.tB_sqldatabase.Name = "tB_sqldatabase";
            this.tB_sqldatabase.Size = new System.Drawing.Size(163, 21);
            this.tB_sqldatabase.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(40, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "密码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(40, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "用户：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(40, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "数据库名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(41, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "服务器地址：";
            // 
            // tB_sqladdr
            // 
            this.tB_sqladdr.Location = new System.Drawing.Point(140, 55);
            this.tB_sqladdr.Name = "tB_sqladdr";
            this.tB_sqladdr.Size = new System.Drawing.Size(162, 21);
            this.tB_sqladdr.TabIndex = 6;
            // 
            // tB_ftpaddr
            // 
            this.tB_ftpaddr.Location = new System.Drawing.Point(139, 15);
            this.tB_ftpaddr.Name = "tB_ftpaddr";
            this.tB_ftpaddr.Size = new System.Drawing.Size(162, 21);
            this.tB_ftpaddr.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(40, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 14);
            this.label7.TabIndex = 10;
            this.label7.Text = "FTP服务器：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tB_Tester);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 45);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(41, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "操作员：";
            // 
            // tB_Tester
            // 
            this.tB_Tester.Location = new System.Drawing.Point(139, 15);
            this.tB_Tester.Name = "tB_Tester";
            this.tB_Tester.Size = new System.Drawing.Size(163, 21);
            this.tB_Tester.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(309, 379);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(57, 27);
            this.button2.TabIndex = 10;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.tB_ftppw);
            this.groupBox3.Controls.Add(this.tB_ftpuser);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.tB_ftpaddr);
            this.groupBox3.Location = new System.Drawing.Point(12, 63);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(356, 101);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(41, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 14);
            this.label9.TabIndex = 13;
            this.label9.Text = "密码：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(40, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 14);
            this.label8.TabIndex = 12;
            this.label8.Text = "用户：";
            // 
            // tB_ftppw
            // 
            this.tB_ftppw.Location = new System.Drawing.Point(140, 71);
            this.tB_ftppw.Name = "tB_ftppw";
            this.tB_ftppw.PasswordChar = '*';
            this.tB_ftppw.Size = new System.Drawing.Size(162, 21);
            this.tB_ftppw.TabIndex = 4;
            // 
            // tB_ftpuser
            // 
            this.tB_ftpuser.Location = new System.Drawing.Point(139, 42);
            this.tB_ftpuser.Name = "tB_ftpuser";
            this.tB_ftpuser.Size = new System.Drawing.Size(162, 21);
            this.tB_ftpuser.TabIndex = 3;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(55, 385);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "保存密码";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // SqlLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 418);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(396, 456);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(396, 456);
            this.Name = "SqlLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "登陆";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SqlLogin_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tB_sqlpassward;
        private System.Windows.Forms.TextBox tB_sqluser;
        private System.Windows.Forms.TextBox tB_sqldatabase;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tB_sqladdr;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tB_Tester;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cB_sqlServerType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tB_ftpaddr;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tB_ftppw;
        private System.Windows.Forms.TextBox tB_ftpuser;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}