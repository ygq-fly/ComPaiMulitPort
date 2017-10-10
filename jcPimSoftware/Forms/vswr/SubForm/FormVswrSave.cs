// ===============================================================================
// �ļ�����FormVswrSave
// �����ˣ����
// ��  �ڣ�2011-5-19 
//
// ��  ����SAVE��ݲ˵��Ӵ���
//         
//
// ��  ���� 1.0.0.0
//
// ���¼�¼ 
// ===============================================================================
// ʱ  �䣺 2011-5-19    	   �������ļ�
//
// ===============================================================================



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace jcPimSoftware
{
    public partial class FormVswrSave : Form
    {
        #region ��������

        /// <summary>
        /// ����ļ��ĸ�·��
        /// </summary>
        private string RootPath;

        /// <summary>
        /// CSV�ļ�·��
        /// </summary>
        private string _csvFileName;
        public string CsvFileName
        {
            get { return _csvFileName; }
        }

        /// <summary>
        /// JPG�ļ�·��
        /// </summary>
        private string _jpgFileName;
        public string JpgFileName
        {
            get { return _jpgFileName; }
        }

        /// <summary>
        /// PDF�ļ�·��
        /// </summary>
        private string _pdfFileName;
        public string PdfFileName
        {
            get { return _pdfFileName; }
            set { _pdfFileName = value; }
        }


        /// <summary>
        /// ����CSV����
        /// </summary>
        private bool _bEnableCsv;
        public bool bEnableCsv
        {
            get { return _bEnableCsv; }
        }

        /// <summary>
        /// ����JPG����
        /// </summary>
        private bool _bEnableJpg;
        public bool bEnableJpg
        {
            get { return _bEnableJpg; }
        }

        /// <summary>
        /// ����PDF����
        /// </summary>
        private bool _bEnablePdf;
        public bool bEnablePdf
        {
            get { return _bEnablePdf; }
            set { _bEnablePdf = value; }
        }


        #endregion


        #region ���幹��
        /// <summary>
        /// ���幹��
        /// </summary>
        public FormVswrSave()
        {
            InitializeComponent();
        }

        #endregion


        #region �����¼�

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormVswrSave_Load(object sender, EventArgs e)
        {
            RootPath = App_Configure.Cnfgs.Path_Rpt_Vsw + "\\";

            chkCsv.Checked = Convert.ToBoolean(App_Configure.Cnfgs.Csv_checked);
            chkPDF.Checked = Convert.ToBoolean(App_Configure.Cnfgs.Pdf_checked);
            chkJpg.Checked = Convert.ToBoolean(App_Configure.Cnfgs.Jpg_checked);
            SaveDatats();

            DateTime dt_now = DateTime.Now;
            string strDate = dt_now.ToString("yyyy-MM-dd hh-mm-ss");
            _csvFileName = RootPath + "csv\\" + strDate + ".csv";
            _jpgFileName = RootPath + "jpg\\" + strDate + ".jpg";
            _pdfFileName = RootPath + "pdf\\" + strDate + ".pdf";
            txtCsv.Text = strDate;
            txtJpg.Text = strDate;
            txtPDF.Text = strDate;
            textBox1.Text = App_Configure.Cnfgs.Opeor;
            textBox2.Text = App_Configure.Cnfgs.Serno;
            textBox3.Text = App_Configure.Cnfgs.Modno;
            lblPath.Text = "�ļ�·��:" + App_Configure.Cnfgs.Path_Rpt_Vsw;
        }

        #endregion

        #endregion


        #region �¼�

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            _bEnableCsv = chkCsv.Checked;
            _bEnableJpg = chkJpg.Checked;
            _bEnablePdf = chkPDF.Checked;
            _csvFileName = RootPath + "csv\\" + txtCsv.Text.Trim() + ".csv";
            _jpgFileName = RootPath + "jpg\\" + txtJpg.Text.Trim() + ".jpg";
            _pdfFileName = RootPath + "pdf\\" + txtPDF.Text.Trim() + ".pdf";
            this.DialogResult = DialogResult.OK;
        }

        #endregion

        #region ȡ��
        /// <summary>
        /// ȡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region CSV
        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCsv_CheckedChanged(object sender, EventArgs e)
        {
            SaveDatats();
        }

        #endregion

        #region JPG
        /// <summary>
        /// JPG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkJpg_CheckedChanged(object sender, EventArgs e)
        {
            SaveDatats();
        }

        #endregion

        #region PDF
        /// <summary>
        /// PDF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkPDF_CheckedChanged(object sender, EventArgs e)
        {
            SaveDatats();
        }

        #endregion

        private void SaveDatats()
        {
            if (chkCsv.Checked)
            {
                txtCsv.Enabled = true;
                _bEnableCsv = true;
            }
            else
            {
                txtCsv.Enabled = false;
                _bEnableCsv = false;
            }

            if (chkJpg.Checked)
            {
                txtJpg.Enabled = true;
                _bEnableJpg = true;
            }
            else
            {
                txtJpg.Enabled = false;
                _bEnableJpg = false;
            }

            if (chkPDF.Checked)
            {
                txtPDF.Enabled = true;
                bEnablePdf = true;
            }
            else
            {
                txtPDF.Enabled = false;
                bEnablePdf = false;
            }
        }

        #endregion


        #region TouchPad

        private void txtCsv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref txtCsv, txtCsv.Text.Trim());
            testTouchPad.ShowDialog();
        }

        private void txtJpg_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref txtJpg, txtJpg.Text.Trim());
            testTouchPad.ShowDialog();
        }

        private void txtPDF_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref txtPDF, txtPDF.Text.Trim());
            testTouchPad.ShowDialog();
        }

        #endregion 

        private void btn_Root_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                App_Configure.Cnfgs.Path_Rpt_Vsw = fbd.SelectedPath + "\\vsw";

                if (!Directory.Exists(App_Configure.Cnfgs.Path_Rpt_Vsw))
                    Directory.CreateDirectory(App_Configure.Cnfgs.Path_Rpt_Vsw);

                App_Configure.CreateReportSubFolder(App_Configure.Cnfgs.Path_Rpt_Vsw);
                RootPath = App_Configure.Cnfgs.Path_Rpt_Vsw + "\\";
                lblPath.Text = "�ļ�·��:" + App_Configure.Cnfgs.Path_Rpt_Vsw;
            }
            fbd.Dispose();
        }

    }
}