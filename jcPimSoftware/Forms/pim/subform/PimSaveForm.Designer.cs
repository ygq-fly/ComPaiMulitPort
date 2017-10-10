namespace jcPimSoftware
{
    partial class PimSaveForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblInstrumentNo = new System.Windows.Forms.Label();
            this.lblImSchema = new System.Windows.Forms.Label();
            this.lblImMode = new System.Windows.Forms.Label();
            this.lblImUint = new System.Windows.Forms.Label();
            this.lblOrder = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxInstrument = new System.Windows.Forms.TextBox();
            this.tbxImMode = new System.Windows.Forms.TextBox();
            this.tbxImUint = new System.Windows.Forms.TextBox();
            this.tbxImOrder = new System.Windows.Forms.TextBox();
            this.tbxImSchema = new System.Windows.Forms.TextBox();
            this.tbxTx1 = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tbxTx2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxF1 = new System.Windows.Forms.TextBox();
            this.lblF1 = new System.Windows.Forms.Label();
            this.tbxF2 = new System.Windows.Forms.TextBox();
            this.lblF2 = new System.Windows.Forms.Label();
            this.tbxOffset1 = new System.Windows.Forms.TextBox();
            this.lblOffset1 = new System.Windows.Forms.Label();
            this.tbxOffset2 = new System.Windows.Forms.TextBox();
            this.lblOffset2 = new System.Windows.Forms.Label();
            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this.txtPDF = new System.Windows.Forms.TextBox();
            this.txtJpg = new System.Windows.Forms.TextBox();
            this.txtCsv = new System.Windows.Forms.TextBox();
            this.groupBoxType = new System.Windows.Forms.GroupBox();
            this.chkPDF = new System.Windows.Forms.CheckBox();
            this.chkJpg = new System.Windows.Forms.CheckBox();
            this.chkCsv = new System.Windows.Forms.CheckBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_Root = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.groupBoxFile.SuspendLayout();
            this.groupBoxType.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dgvData.Location = new System.Drawing.Point(0, 447);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(745, 119);
            this.dgvData.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column1.HeaderText = "No.";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.Width = 57;
            // 
            // Column2
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F);
            this.Column2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column2.HeaderText = "Tx1 (dBm)";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.Width = 110;
            // 
            // Column3
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 12F);
            this.Column3.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column3.HeaderText = "F1 (MHz)";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.Width = 110;
            // 
            // Column4
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 12F);
            this.Column4.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column4.HeaderText = "Tx2 (dBm)";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.Width = 110;
            // 
            // Column5
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 12F);
            this.Column5.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column5.HeaderText = "F2 (MHz)";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column5.Width = 110;
            // 
            // Column6
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 12F);
            this.Column6.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column6.HeaderText = "Im Value";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column6.Width = 110;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Im at F (MHz)";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column7.Width = 135;
            // 
            // lblInstrumentNo
            // 
            this.lblInstrumentNo.AutoSize = true;
            this.lblInstrumentNo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInstrumentNo.Location = new System.Drawing.Point(15, 341);
            this.lblInstrumentNo.Name = "lblInstrumentNo";
            this.lblInstrumentNo.Size = new System.Drawing.Size(104, 16);
            this.lblInstrumentNo.TabIndex = 1;
            this.lblInstrumentNo.Text = "InstrumentNo";
            // 
            // lblImSchema
            // 
            this.lblImSchema.AutoSize = true;
            this.lblImSchema.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblImSchema.Location = new System.Drawing.Point(15, 380);
            this.lblImSchema.Name = "lblImSchema";
            this.lblImSchema.Size = new System.Drawing.Size(72, 16);
            this.lblImSchema.TabIndex = 2;
            this.lblImSchema.Text = "ImSchema";
            // 
            // lblImMode
            // 
            this.lblImMode.AutoSize = true;
            this.lblImMode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblImMode.Location = new System.Drawing.Point(353, 348);
            this.lblImMode.Name = "lblImMode";
            this.lblImMode.Size = new System.Drawing.Size(56, 16);
            this.lblImMode.TabIndex = 3;
            this.lblImMode.Text = "ImMode";
            // 
            // lblImUint
            // 
            this.lblImUint.AutoSize = true;
            this.lblImUint.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblImUint.Location = new System.Drawing.Point(15, 418);
            this.lblImUint.Name = "lblImUint";
            this.lblImUint.Size = new System.Drawing.Size(56, 16);
            this.lblImUint.TabIndex = 4;
            this.lblImUint.Text = "ImUint";
            // 
            // lblOrder
            // 
            this.lblOrder.AutoSize = true;
            this.lblOrder.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOrder.Location = new System.Drawing.Point(531, 347);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new System.Drawing.Size(64, 16);
            this.lblOrder.TabIndex = 5;
            this.lblOrder.Text = "ImOrder";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(228, 383);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "Tx1";
            // 
            // tbxInstrument
            // 
            this.tbxInstrument.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxInstrument.Location = new System.Drawing.Point(122, 338);
            this.tbxInstrument.Name = "tbxInstrument";
            this.tbxInstrument.Size = new System.Drawing.Size(189, 26);
            this.tbxInstrument.TabIndex = 7;
            // 
            // tbxImMode
            // 
            this.tbxImMode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxImMode.Location = new System.Drawing.Point(415, 342);
            this.tbxImMode.Name = "tbxImMode";
            this.tbxImMode.Size = new System.Drawing.Size(100, 26);
            this.tbxImMode.TabIndex = 9;
            // 
            // tbxImUint
            // 
            this.tbxImUint.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxImUint.Location = new System.Drawing.Point(122, 413);
            this.tbxImUint.Name = "tbxImUint";
            this.tbxImUint.Size = new System.Drawing.Size(100, 26);
            this.tbxImUint.TabIndex = 10;
            // 
            // tbxImOrder
            // 
            this.tbxImOrder.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxImOrder.Location = new System.Drawing.Point(601, 344);
            this.tbxImOrder.Name = "tbxImOrder";
            this.tbxImOrder.Size = new System.Drawing.Size(100, 26);
            this.tbxImOrder.TabIndex = 11;
            // 
            // tbxImSchema
            // 
            this.tbxImSchema.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxImSchema.Location = new System.Drawing.Point(122, 379);
            this.tbxImSchema.Name = "tbxImSchema";
            this.tbxImSchema.Size = new System.Drawing.Size(100, 26);
            this.tbxImSchema.TabIndex = 12;
            // 
            // tbxTx1
            // 
            this.tbxTx1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxTx1.Location = new System.Drawing.Point(270, 377);
            this.tbxTx1.Name = "tbxTx1";
            this.tbxTx1.Size = new System.Drawing.Size(100, 26);
            this.tbxTx1.TabIndex = 13;
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(381, 185);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(93, 30);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Location = new System.Drawing.Point(381, 243);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(93, 30);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tbxTx2
            // 
            this.tbxTx2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxTx2.Location = new System.Drawing.Point(270, 411);
            this.tbxTx2.Name = "tbxTx2";
            this.tbxTx2.Size = new System.Drawing.Size(100, 26);
            this.tbxTx2.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(228, 418);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 16);
            this.label7.TabIndex = 20;
            this.label7.Text = "Tx2";
            // 
            // tbxF1
            // 
            this.tbxF1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxF1.Location = new System.Drawing.Point(415, 380);
            this.tbxF1.Name = "tbxF1";
            this.tbxF1.Size = new System.Drawing.Size(100, 26);
            this.tbxF1.TabIndex = 23;
            // 
            // lblF1
            // 
            this.lblF1.AutoSize = true;
            this.lblF1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblF1.Location = new System.Drawing.Point(385, 382);
            this.lblF1.Name = "lblF1";
            this.lblF1.Size = new System.Drawing.Size(24, 16);
            this.lblF1.TabIndex = 22;
            this.lblF1.Text = "F1";
            // 
            // tbxF2
            // 
            this.tbxF2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxF2.Location = new System.Drawing.Point(415, 415);
            this.tbxF2.Name = "tbxF2";
            this.tbxF2.Size = new System.Drawing.Size(100, 26);
            this.tbxF2.TabIndex = 25;
            // 
            // lblF2
            // 
            this.lblF2.AutoSize = true;
            this.lblF2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblF2.Location = new System.Drawing.Point(385, 416);
            this.lblF2.Name = "lblF2";
            this.lblF2.Size = new System.Drawing.Size(24, 16);
            this.lblF2.TabIndex = 24;
            this.lblF2.Text = "F2";
            // 
            // tbxOffset1
            // 
            this.tbxOffset1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxOffset1.Location = new System.Drawing.Point(601, 380);
            this.tbxOffset1.Name = "tbxOffset1";
            this.tbxOffset1.Size = new System.Drawing.Size(100, 26);
            this.tbxOffset1.TabIndex = 27;
            // 
            // lblOffset1
            // 
            this.lblOffset1.AutoSize = true;
            this.lblOffset1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOffset1.Location = new System.Drawing.Point(531, 383);
            this.lblOffset1.Name = "lblOffset1";
            this.lblOffset1.Size = new System.Drawing.Size(64, 16);
            this.lblOffset1.TabIndex = 26;
            this.lblOffset1.Text = "Offset1";
            // 
            // tbxOffset2
            // 
            this.tbxOffset2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxOffset2.Location = new System.Drawing.Point(601, 412);
            this.tbxOffset2.Name = "tbxOffset2";
            this.tbxOffset2.Size = new System.Drawing.Size(100, 26);
            this.tbxOffset2.TabIndex = 29;
            // 
            // lblOffset2
            // 
            this.lblOffset2.AutoSize = true;
            this.lblOffset2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOffset2.Location = new System.Drawing.Point(531, 415);
            this.lblOffset2.Name = "lblOffset2";
            this.lblOffset2.Size = new System.Drawing.Size(64, 16);
            this.lblOffset2.TabIndex = 28;
            this.lblOffset2.Text = "Offset2";
            // 
            // groupBoxFile
            // 
            this.groupBoxFile.Controls.Add(this.txtPDF);
            this.groupBoxFile.Controls.Add(this.txtJpg);
            this.groupBoxFile.Controls.Add(this.txtCsv);
            this.groupBoxFile.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.groupBoxFile.Location = new System.Drawing.Point(138, 35);
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.Size = new System.Drawing.Size(346, 138);
            this.groupBoxFile.TabIndex = 32;
            this.groupBoxFile.TabStop = false;
            this.groupBoxFile.Text = "File Name";
            // 
            // txtPDF
            // 
            this.txtPDF.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPDF.Location = new System.Drawing.Point(10, 103);
            this.txtPDF.Name = "txtPDF";
            this.txtPDF.Size = new System.Drawing.Size(326, 26);
            this.txtPDF.TabIndex = 10;
            this.txtPDF.TextChanged += new System.EventHandler(this.txtPDF_TextChanged);
            this.txtPDF.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txt_MouseDoubleClick);
            // 
            // txtJpg
            // 
            this.txtJpg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtJpg.Location = new System.Drawing.Point(10, 65);
            this.txtJpg.Name = "txtJpg";
            this.txtJpg.Size = new System.Drawing.Size(326, 26);
            this.txtJpg.TabIndex = 9;
            this.txtJpg.TextChanged += new System.EventHandler(this.txtJpg_TextChanged);
            this.txtJpg.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txt_MouseDoubleClick);
            // 
            // txtCsv
            // 
            this.txtCsv.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCsv.Location = new System.Drawing.Point(10, 27);
            this.txtCsv.Name = "txtCsv";
            this.txtCsv.Size = new System.Drawing.Size(326, 26);
            this.txtCsv.TabIndex = 8;
            this.txtCsv.TextChanged += new System.EventHandler(this.txtCsv_TextChanged);
            this.txtCsv.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txt_MouseDoubleClick);
            // 
            // groupBoxType
            // 
            this.groupBoxType.Controls.Add(this.chkPDF);
            this.groupBoxType.Controls.Add(this.chkJpg);
            this.groupBoxType.Controls.Add(this.chkCsv);
            this.groupBoxType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.groupBoxType.Location = new System.Drawing.Point(12, 35);
            this.groupBoxType.Name = "groupBoxType";
            this.groupBoxType.Size = new System.Drawing.Size(120, 138);
            this.groupBoxType.TabIndex = 31;
            this.groupBoxType.TabStop = false;
            this.groupBoxType.Text = "File Type";
            this.groupBoxType.Enter += new System.EventHandler(this.groupBoxType_Enter);
            // 
            // chkPDF
            // 
            this.chkPDF.AutoSize = true;
            this.chkPDF.Location = new System.Drawing.Point(28, 106);
            this.chkPDF.Name = "chkPDF";
            this.chkPDF.Size = new System.Drawing.Size(54, 20);
            this.chkPDF.TabIndex = 2;
            this.chkPDF.Text = "PDF";
            this.chkPDF.UseVisualStyleBackColor = true;
            this.chkPDF.CheckedChanged += new System.EventHandler(this.chkPDF_CheckedChanged);
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
            this.chkCsv.Location = new System.Drawing.Point(28, 30);
            this.chkCsv.Name = "chkCsv";
            this.chkCsv.Size = new System.Drawing.Size(54, 20);
            this.chkCsv.TabIndex = 0;
            this.chkCsv.Text = "CSV";
            this.chkCsv.UseVisualStyleBackColor = true;
            this.chkCsv.CheckedChanged += new System.EventHandler(this.chkCsv_CheckedChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(148, 220);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(212, 26);
            this.textBox3.TabIndex = 44;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(148, 185);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(212, 26);
            this.textBox2.TabIndex = 43;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(46, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 42;
            this.label3.Text = "serno :";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(46, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 41;
            this.label2.Text = "modno :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(46, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 40;
            this.label1.Text = "opeor :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(148, 257);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(212, 26);
            this.textBox1.TabIndex = 39;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btn_Root
            // 
            this.btn_Root.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Root.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Root.Location = new System.Drawing.Point(361, 6);
            this.btn_Root.Name = "btn_Root";
            this.btn_Root.Size = new System.Drawing.Size(106, 30);
            this.btn_Root.TabIndex = 45;
            this.btn_Root.Text = "Change Menu";
            this.btn_Root.UseVisualStyleBackColor = true;
            this.btn_Root.Click += new System.EventHandler(this.btn_Root_Click);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(20, 13);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(88, 16);
            this.lblPath.TabIndex = 49;
            this.lblPath.Text = "文件路径：";
            // 
            // PimSaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 301);
            this.ControlBox = false;
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.btn_Root);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBoxFile);
            this.Controls.Add(this.groupBoxType);
            this.Controls.Add(this.tbxOffset2);
            this.Controls.Add(this.lblOffset2);
            this.Controls.Add(this.tbxOffset1);
            this.Controls.Add(this.lblOffset1);
            this.Controls.Add(this.tbxF2);
            this.Controls.Add(this.lblF2);
            this.Controls.Add(this.tbxF1);
            this.Controls.Add(this.lblF1);
            this.Controls.Add(this.tbxTx2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbxTx1);
            this.Controls.Add(this.tbxImSchema);
            this.Controls.Add(this.tbxImOrder);
            this.Controls.Add(this.tbxImUint);
            this.Controls.Add(this.tbxImMode);
            this.Controls.Add(this.tbxInstrument);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblOrder);
            this.Controls.Add(this.lblImUint);
            this.Controls.Add(this.lblImMode);
            this.Controls.Add(this.lblImSchema);
            this.Controls.Add(this.lblInstrumentNo);
            this.Controls.Add(this.dgvData);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PimSaveForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " ";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PimSaveForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.groupBoxFile.ResumeLayout(false);
            this.groupBoxFile.PerformLayout();
            this.groupBoxType.ResumeLayout(false);
            this.groupBoxType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label lblInstrumentNo;
        private System.Windows.Forms.Label lblImSchema;
        private System.Windows.Forms.Label lblImMode;
        private System.Windows.Forms.Label lblImUint;
        private System.Windows.Forms.Label lblOrder;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxInstrument;
        private System.Windows.Forms.TextBox tbxImMode;
        private System.Windows.Forms.TextBox tbxImUint;
        private System.Windows.Forms.TextBox tbxImOrder;
        private System.Windows.Forms.TextBox tbxImSchema;
        private System.Windows.Forms.TextBox tbxTx1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox tbxTx2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxF1;
        private System.Windows.Forms.Label lblF1;
        private System.Windows.Forms.TextBox tbxF2;
        private System.Windows.Forms.Label lblF2;
        private System.Windows.Forms.TextBox tbxOffset1;
        private System.Windows.Forms.Label lblOffset1;
        private System.Windows.Forms.TextBox tbxOffset2;
        private System.Windows.Forms.Label lblOffset2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.GroupBox groupBoxFile;
        private System.Windows.Forms.GroupBox groupBoxType;
        private System.Windows.Forms.CheckBox chkPDF;
        private System.Windows.Forms.CheckBox chkJpg;
        private System.Windows.Forms.CheckBox chkCsv;
        public System.Windows.Forms.TextBox txtPDF;
        public System.Windows.Forms.TextBox txtJpg;
        public System.Windows.Forms.TextBox txtCsv;
        public System.Windows.Forms.TextBox textBox3;
        public System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_Root;
        private System.Windows.Forms.Label lblPath;
    }
}