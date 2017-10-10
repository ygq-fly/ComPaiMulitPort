namespace jcPimSoftware
{
    partial class FileManager
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileManager));
            this.lv = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.cm = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tm = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.img1 = new System.Windows.Forms.ImageList(this.components);
            this.img2 = new System.Windows.Forms.ImageList(this.components);
            this.cb = new System.Windows.Forms.ComboBox();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.lb = new System.Windows.Forms.Label();
            this.lb1 = new System.Windows.Forms.Label();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btn_new = new System.Windows.Forms.Button();
            this.cm.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv
            // 
            this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lv.ContextMenuStrip = this.cm;
            this.lv.LargeImageList = this.img1;
            this.lv.Location = new System.Drawing.Point(4, 110);
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(563, 391);
            this.lv.SmallImageList = this.img2;
            this.lv.TabIndex = 0;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            this.lv.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lv_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File";
            this.columnHeader1.Width = 104;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 99;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Type";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 86;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "LastTime";
            this.columnHeader4.Width = 378;
            // 
            // cm
            // 
            this.cm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.tm,
            this.CopyToolStripMenuItem,
            this.ToolStripMenuItem,
            this.SendToolStripMenuItem});
            this.cm.Name = "cm";
            this.cm.Size = new System.Drawing.Size(153, 136);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.打开ToolStripMenuItem.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.打开ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("打开ToolStripMenuItem.Image")));
            this.打开ToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.打开ToolStripMenuItem.Text = "Open";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tm
            // 
            this.tm.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tm.Image = ((System.Drawing.Image)(resources.GetObject("tm.Image")));
            this.tm.Name = "tm";
            this.tm.Size = new System.Drawing.Size(152, 22);
            this.tm.Text = "Delete";
            this.tm.Click += new System.EventHandler(this.tm_Click);
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.CopyToolStripMenuItem.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CopyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("CopyToolStripMenuItem.Image")));
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CopyToolStripMenuItem.Text = "Copy";
            this.CopyToolStripMenuItem.Visible = false;
            this.CopyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem.Image")));
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItem.Text = "Paste";
            this.ToolStripMenuItem.Visible = false;
            this.ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click_1);
            // 
            // SendToolStripMenuItem
            // 
            this.SendToolStripMenuItem.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SendToolStripMenuItem.Name = "SendToolStripMenuItem";
            this.SendToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.SendToolStripMenuItem.Text = "Send to";
            // 
            // img1
            // 
            this.img1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("img1.ImageStream")));
            this.img1.TransparentColor = System.Drawing.Color.Transparent;
            this.img1.Images.SetKeyName(0, "shell32_4.ico");
            this.img1.Images.SetKeyName(1, "shell32_152.ico");
            this.img1.Images.SetKeyName(2, "shimgvw_2.ico");
            this.img1.Images.SetKeyName(3, "shimgvw_4.ico");
            this.img1.Images.SetKeyName(4, "Foxit Reader_129.ico");
            this.img1.Images.SetKeyName(5, "xlicons_266.ico");
            this.img1.Images.SetKeyName(6, "shell32_1.ico");
            // 
            // img2
            // 
            this.img2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("img2.ImageStream")));
            this.img2.TransparentColor = System.Drawing.Color.Transparent;
            this.img2.Images.SetKeyName(0, "shell32_4.ico");
            this.img2.Images.SetKeyName(1, "shell32_152.ico");
            this.img2.Images.SetKeyName(2, "shimgvw_2.ico");
            this.img2.Images.SetKeyName(3, "shimgvw_4.ico");
            this.img2.Images.SetKeyName(4, "Foxit Reader_129.ico");
            this.img2.Images.SetKeyName(5, "xlicons_266.ico");
            this.img2.Images.SetKeyName(6, "shell32_1.ico");
            // 
            // cb
            // 
            this.cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb.FormattingEnabled = true;
            this.cb.Location = new System.Drawing.Point(353, 23);
            this.cb.Name = "cb";
            this.cb.Size = new System.Drawing.Size(105, 24);
            this.cb.TabIndex = 2;
            this.cb.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            // 
            // tbPath
            // 
            this.tbPath.Enabled = false;
            this.tbPath.Location = new System.Drawing.Point(47, 73);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(390, 26);
            this.tbPath.TabIndex = 4;
            // 
            // lb
            // 
            this.lb.AutoSize = true;
            this.lb.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb.Location = new System.Drawing.Point(263, 26);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(72, 16);
            this.lb.TabIndex = 5;
            this.lb.Text = "Display:";
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb1.Location = new System.Drawing.Point(4, 76);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(40, 16);
            this.lb1.TabIndex = 8;
            this.lb1.Text = "Addr";
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.SystemColors.Control;
            this.btnDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDel.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.Image")));
            this.btnDel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDel.Location = new System.Drawing.Point(154, 12);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(89, 44);
            this.btnDel.TabIndex = 7;
            this.btnDel.Text = "Delete";
            this.btnDel.UseVisualStyleBackColor = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Image = ((System.Drawing.Image)(resources.GetObject("btnReturn.Image")));
            this.btnReturn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReturn.Location = new System.Drawing.Point(443, 70);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(115, 30);
            this.btnReturn.TabIndex = 6;
            this.btnReturn.Text = "Back    ";
            this.btnReturn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btn_new
            // 
            this.btn_new.BackColor = System.Drawing.SystemColors.Control;
            this.btn_new.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_new.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_new.Image = ((System.Drawing.Image)(resources.GetObject("btn_new.Image")));
            this.btn_new.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_new.Location = new System.Drawing.Point(7, 12);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(119, 44);
            this.btn_new.TabIndex = 1;
            this.btn_new.Text = "New Folder";
            this.btn_new.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_new.UseVisualStyleBackColor = false;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // FileManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 506);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.lb1);
            this.Controls.Add(this.lv);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.lb);
            this.Controls.Add(this.cb);
            this.Controls.Add(this.btn_new);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Operations";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.demo_Load);
            this.cm.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.ComboBox cb;
        private System.Windows.Forms.ContextMenuStrip cm;
        private System.Windows.Forms.ToolStripMenuItem tm;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
        private System.Windows.Forms.Label lb;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SendToolStripMenuItem;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.ImageList img1;
        private System.Windows.Forms.ImageList img2;

    }
}

