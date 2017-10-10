namespace jcPimSoftware
{
    partial class IsoSaveDataForm
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
            this.btn_Root = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this.txtPdf = new System.Windows.Forms.TextBox();
            this.txtJpg = new System.Windows.Forms.TextBox();
            this.txtCsv = new System.Windows.Forms.TextBox();
            this.groupBoxType = new System.Windows.Forms.GroupBox();
            this.chkPdf = new System.Windows.Forms.CheckBox();
            this.chkJpg = new System.Windows.Forms.CheckBox();
            this.chkCsv = new System.Windows.Forms.CheckBox();
            this.groupBoxFile.SuspendLayout();
            this.groupBoxType.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(15, 15);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(88, 16);
            this.lblPath.TabIndex = 68;
            this.lblPath.Text = "文件路径：";
            // 
            // btn_Root
            // 
            this.btn_Root.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Root.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Root.Location = new System.Drawing.Point(357, 8);
            this.btn_Root.Name = "btn_Root";
            this.btn_Root.Size = new System.Drawing.Size(106, 30);
            this.btn_Root.TabIndex = 67;
            this.btn_Root.Text = "Change Menu";
            this.btn_Root.UseVisualStyleBackColor = true;
            this.btn_Root.Click += new System.EventHandler(this.btn_Root_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(130, 196);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(210, 26);
            this.textBox3.TabIndex = 66;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(130, 228);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(210, 26);
            this.textBox2.TabIndex = 65;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(32, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 64;
            this.label3.Text = "modno :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(31, 231);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 63;
            this.label2.Text = "serno :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(31, 264);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 60;
            this.label1.Text = "opeor :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(130, 261);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(210, 26);
            this.textBox1.TabIndex = 62;
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(372, 257);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 61;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(372, 199);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 59;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // groupBoxFile
            // 
            this.groupBoxFile.Controls.Add(this.txtPdf);
            this.groupBoxFile.Controls.Add(this.txtJpg);
            this.groupBoxFile.Controls.Add(this.txtCsv);
            this.groupBoxFile.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.groupBoxFile.Location = new System.Drawing.Point(115, 49);
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.Size = new System.Drawing.Size(337, 138);
            this.groupBoxFile.TabIndex = 58;
            this.groupBoxFile.TabStop = false;
            this.groupBoxFile.Text = "File Name";
            // 
            // txtPdf
            // 
            this.txtPdf.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPdf.Location = new System.Drawing.Point(15, 103);
            this.txtPdf.Name = "txtPdf";
            this.txtPdf.Size = new System.Drawing.Size(308, 26);
            this.txtPdf.TabIndex = 10;
            // 
            // txtJpg
            // 
            this.txtJpg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtJpg.Location = new System.Drawing.Point(15, 65);
            this.txtJpg.Name = "txtJpg";
            this.txtJpg.Size = new System.Drawing.Size(308, 26);
            this.txtJpg.TabIndex = 9;
            // 
            // txtCsv
            // 
            this.txtCsv.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCsv.Location = new System.Drawing.Point(15, 27);
            this.txtCsv.Name = "txtCsv";
            this.txtCsv.Size = new System.Drawing.Size(308, 26);
            this.txtCsv.TabIndex = 8;
            // 
            // groupBoxType
            // 
            this.groupBoxType.Controls.Add(this.chkPdf);
            this.groupBoxType.Controls.Add(this.chkJpg);
            this.groupBoxType.Controls.Add(this.chkCsv);
            this.groupBoxType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.groupBoxType.Location = new System.Drawing.Point(6, 49);
            this.groupBoxType.Name = "groupBoxType";
            this.groupBoxType.Size = new System.Drawing.Size(104, 138);
            this.groupBoxType.TabIndex = 57;
            this.groupBoxType.TabStop = false;
            this.groupBoxType.Text = "File Type";
            // 
            // chkPdf
            // 
            this.chkPdf.AutoSize = true;
            this.chkPdf.Location = new System.Drawing.Point(28, 106);
            this.chkPdf.Name = "chkPdf";
            this.chkPdf.Size = new System.Drawing.Size(54, 20);
            this.chkPdf.TabIndex = 2;
            this.chkPdf.Text = "PDF";
            this.chkPdf.UseVisualStyleBackColor = true;
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
            this.chkCsv.Location = new System.Drawing.Point(28, 30);
            this.chkCsv.Name = "chkCsv";
            this.chkCsv.Size = new System.Drawing.Size(54, 20);
            this.chkCsv.TabIndex = 0;
            this.chkCsv.Text = "CSV";
            this.chkCsv.UseVisualStyleBackColor = true;
            // 
            // IsoSaveDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 306);
            this.ControlBox = false;
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.btn_Root);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBoxFile);
            this.Controls.Add(this.groupBoxType);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "IsoSaveDataForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Save Test Data As Reprot";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.IsoSaveDataForm_Load);
            this.groupBoxFile.ResumeLayout(false);
            this.groupBoxFile.PerformLayout();
            this.groupBoxType.ResumeLayout(false);
            this.groupBoxType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Button btn_Root;
        public System.Windows.Forms.TextBox textBox3;
        public System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBoxFile;
        private System.Windows.Forms.TextBox txtPdf;
        private System.Windows.Forms.TextBox txtJpg;
        private System.Windows.Forms.TextBox txtCsv;
        private System.Windows.Forms.GroupBox groupBoxType;
        private System.Windows.Forms.CheckBox chkPdf;
        private System.Windows.Forms.CheckBox chkJpg;
        private System.Windows.Forms.CheckBox chkCsv;

    }
}