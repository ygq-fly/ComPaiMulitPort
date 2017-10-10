namespace jcPimSoftware
{
    partial class FormSaveData
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
            this.groupBoxType = new System.Windows.Forms.GroupBox();
            this.chkJpg = new System.Windows.Forms.CheckBox();
            this.chkCsv = new System.Windows.Forms.CheckBox();
            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this.txtJpg = new System.Windows.Forms.TextBox();
            this.txtCsv = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            this.btn_Root = new System.Windows.Forms.Button();
            this.groupBoxType.SuspendLayout();
            this.groupBoxFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxType
            // 
            this.groupBoxType.Controls.Add(this.chkJpg);
            this.groupBoxType.Controls.Add(this.chkCsv);
            this.groupBoxType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.groupBoxType.Location = new System.Drawing.Point(12, 53);
            this.groupBoxType.Name = "groupBoxType";
            this.groupBoxType.Size = new System.Drawing.Size(111, 100);
            this.groupBoxType.TabIndex = 0;
            this.groupBoxType.TabStop = false;
            this.groupBoxType.Text = "File Type";
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
            this.chkJpg.CheckedChanged += new System.EventHandler(this.chkJpg_CheckedChanged);
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
            this.chkCsv.CheckedChanged += new System.EventHandler(this.chkCsv_CheckedChanged);
            // 
            // groupBoxFile
            // 
            this.groupBoxFile.Controls.Add(this.txtJpg);
            this.groupBoxFile.Controls.Add(this.txtCsv);
            this.groupBoxFile.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.groupBoxFile.Location = new System.Drawing.Point(129, 53);
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.Size = new System.Drawing.Size(245, 100);
            this.groupBoxFile.TabIndex = 1;
            this.groupBoxFile.TabStop = false;
            this.groupBoxFile.Text = "File Name";
            // 
            // txtJpg
            // 
            this.txtJpg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtJpg.Location = new System.Drawing.Point(8, 66);
            this.txtJpg.Name = "txtJpg";
            this.txtJpg.Size = new System.Drawing.Size(229, 26);
            this.txtJpg.TabIndex = 9;
            this.txtJpg.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtJpg_MouseDoubleClick);
            // 
            // txtCsv
            // 
            this.txtCsv.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCsv.Location = new System.Drawing.Point(8, 23);
            this.txtCsv.Name = "txtCsv";
            this.txtCsv.Size = new System.Drawing.Size(229, 26);
            this.txtCsv.TabIndex = 8;
            this.txtCsv.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtCsv_MouseDoubleClick);
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(380, 71);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 34);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(380, 115);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 34);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(21, 12);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(88, 16);
            this.lblPath.TabIndex = 52;
            this.lblPath.Text = "文件路径：";
            // 
            // btn_Root
            // 
            this.btn_Root.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Root.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Root.Location = new System.Drawing.Point(351, 5);
            this.btn_Root.Name = "btn_Root";
            this.btn_Root.Size = new System.Drawing.Size(106, 30);
            this.btn_Root.TabIndex = 51;
            this.btn_Root.Text = "Change Menu";
            this.btn_Root.UseVisualStyleBackColor = true;
            this.btn_Root.Click += new System.EventHandler(this.btn_Root_Click);
            // 
            // FormSaveData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 162);
            this.ControlBox = false;
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.btn_Root);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBoxFile);
            this.Controls.Add(this.groupBoxType);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSaveData";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SAVEDATA";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormSaveData_Load);
            this.groupBoxType.ResumeLayout(false);
            this.groupBoxType.PerformLayout();
            this.groupBoxFile.ResumeLayout(false);
            this.groupBoxFile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxType;
        private System.Windows.Forms.GroupBox groupBoxFile;
        private System.Windows.Forms.CheckBox chkJpg;
        private System.Windows.Forms.CheckBox chkCsv;
        private System.Windows.Forms.TextBox txtJpg;
        private System.Windows.Forms.TextBox txtCsv;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Button btn_Root;
    }
}