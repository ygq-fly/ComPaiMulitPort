namespace jcPimSoftware
{
    partial class FormVswrSave
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
            this.lblPath = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btn_Root = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this.txtPDF = new System.Windows.Forms.TextBox();
            this.txtJpg = new System.Windows.Forms.TextBox();
            this.txtCsv = new System.Windows.Forms.TextBox();
            this.groupBoxType = new System.Windows.Forms.GroupBox();
            this.chkPDF = new System.Windows.Forms.CheckBox();
            this.chkJpg = new System.Windows.Forms.CheckBox();
            this.chkCsv = new System.Windows.Forms.CheckBox();
            this.groupBoxFile.SuspendLayout();
            this.groupBoxType.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(21, 14);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(88, 16);
            this.lblPath.TabIndex = 70;
            this.lblPath.Text = "文件路径：";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(134, 190);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(201, 26);
            this.textBox3.TabIndex = 68;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(134, 229);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(201, 26);
            this.textBox2.TabIndex = 67;
            // 
            // btn_Root
            // 
            this.btn_Root.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Root.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Root.Location = new System.Drawing.Point(360, 7);
            this.btn_Root.Name = "btn_Root";
            this.btn_Root.Size = new System.Drawing.Size(106, 30);
            this.btn_Root.TabIndex = 69;
            this.btn_Root.Text = "Change Menu";
            this.btn_Root.UseVisualStyleBackColor = true;
            this.btn_Root.Click += new System.EventHandler(this.btn_Root_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(36, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 66;
            this.label3.Text = "modno :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(35, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 65;
            this.label2.Text = "serno :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(134, 268);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(201, 26);
            this.textBox1.TabIndex = 64;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(35, 271);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 63;
            this.label1.Text = "opeor :";
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(366, 264);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 62;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(366, 213);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 61;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // groupBoxFile
            // 
            this.groupBoxFile.Controls.Add(this.txtPDF);
            this.groupBoxFile.Controls.Add(this.txtJpg);
            this.groupBoxFile.Controls.Add(this.txtCsv);
            this.groupBoxFile.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.groupBoxFile.Location = new System.Drawing.Point(127, 46);
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.Size = new System.Drawing.Size(334, 138);
            this.groupBoxFile.TabIndex = 60;
            this.groupBoxFile.TabStop = false;
            this.groupBoxFile.Text = "File Name";
            // 
            // txtPDF
            // 
            this.txtPDF.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPDF.Location = new System.Drawing.Point(8, 103);
            this.txtPDF.Name = "txtPDF";
            this.txtPDF.Size = new System.Drawing.Size(311, 26);
            this.txtPDF.TabIndex = 10;
            // 
            // txtJpg
            // 
            this.txtJpg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtJpg.Location = new System.Drawing.Point(8, 63);
            this.txtJpg.Name = "txtJpg";
            this.txtJpg.Size = new System.Drawing.Size(311, 26);
            this.txtJpg.TabIndex = 9;
            // 
            // txtCsv
            // 
            this.txtCsv.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCsv.Location = new System.Drawing.Point(8, 23);
            this.txtCsv.Name = "txtCsv";
            this.txtCsv.Size = new System.Drawing.Size(311, 26);
            this.txtCsv.TabIndex = 8;
            // 
            // groupBoxType
            // 
            this.groupBoxType.Controls.Add(this.chkPDF);
            this.groupBoxType.Controls.Add(this.chkJpg);
            this.groupBoxType.Controls.Add(this.chkCsv);
            this.groupBoxType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.groupBoxType.Location = new System.Drawing.Point(10, 46);
            this.groupBoxType.Name = "groupBoxType";
            this.groupBoxType.Size = new System.Drawing.Size(111, 138);
            this.groupBoxType.TabIndex = 59;
            this.groupBoxType.TabStop = false;
            this.groupBoxType.Text = "File Type";
            // 
            // chkPDF
            // 
            this.chkPDF.AutoSize = true;
            this.chkPDF.Location = new System.Drawing.Point(28, 111);
            this.chkPDF.Name = "chkPDF";
            this.chkPDF.Size = new System.Drawing.Size(54, 20);
            this.chkPDF.TabIndex = 2;
            this.chkPDF.Text = "PDF";
            this.chkPDF.UseVisualStyleBackColor = true;
            // 
            // chkJpg
            // 
            this.chkJpg.AutoSize = true;
            this.chkJpg.Location = new System.Drawing.Point(28, 68);
            this.chkJpg.Name = "chkJpg";
            this.chkJpg.Size = new System.Drawing.Size(54, 20);
            this.chkJpg.TabIndex = 1;
            this.chkJpg.Text = "JPG";
            this.chkJpg.UseVisualStyleBackColor = true;
            // 
            // chkCsv
            // 
            this.chkCsv.AutoSize = true;
            this.chkCsv.Location = new System.Drawing.Point(28, 25);
            this.chkCsv.Name = "chkCsv";
            this.chkCsv.Size = new System.Drawing.Size(54, 20);
            this.chkCsv.TabIndex = 0;
            this.chkCsv.Text = "CSV";
            this.chkCsv.UseVisualStyleBackColor = true;
            // 
            // FormVswrSave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 309);
            this.ControlBox = false;
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.btn_Root);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBoxFile);
            this.Controls.Add(this.groupBoxType);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormVswrSave";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SAVE";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormVswrSave_Load);
            this.groupBoxFile.ResumeLayout(false);
            this.groupBoxFile.PerformLayout();
            this.groupBoxType.ResumeLayout(false);
            this.groupBoxType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPath;
        public System.Windows.Forms.TextBox textBox3;
        public System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btn_Root;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBoxFile;
        private System.Windows.Forms.TextBox txtPDF;
        private System.Windows.Forms.TextBox txtJpg;
        private System.Windows.Forms.TextBox txtCsv;
        private System.Windows.Forms.GroupBox groupBoxType;
        private System.Windows.Forms.CheckBox chkPDF;
        private System.Windows.Forms.CheckBox chkJpg;
        private System.Windows.Forms.CheckBox chkCsv;

    }
}