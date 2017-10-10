// ===============================================================================
// �ļ�����FormSaveData
// �����ˣ����
// ��  �ڣ�2011-4-29 
//
// ��  ����DATASAVE��ݲ˵��Ӵ���
//         
//
// ��  ���� 1.0.0.0
//
// ���¼�¼ 
// ===============================================================================
// ʱ  �䣺 2011-5-18   	   �������ļ�
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
    public partial class FormSaveData : Form
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

        #endregion


        #region ���幹��
        /// <summary>
        /// ���幹��
        /// </summary>
        public FormSaveData()
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
        private void FormSaveData_Load(object sender, EventArgs e)
        {
            RootPath = App_Configure.Cnfgs.Path_Rpt_Spc + "\\";

            chkCsv.Checked = Convert.ToBoolean(App_Configure.Cnfgs.Csv_checked);
            chkJpg.Checked = Convert.ToBoolean(App_Configure.Cnfgs.Jpg_checked);
            SaveDatas();

            DateTime dt_now=DateTime.Now;
            string strDate = dt_now.ToString("yyyy-MM-dd hh-mm-ss");
            _csvFileName = RootPath + "csv\\" + strDate + ".csv";
            _jpgFileName = RootPath + "jpg\\" + strDate + ".jpg";
            txtCsv.Text = strDate;
            txtJpg.Text = strDate;
            lblPath.Text = "�ļ�·��:" + App_Configure.Cnfgs.Path_Rpt_Spc;
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
            _csvFileName = RootPath + "csv\\" + txtCsv.Text.Trim() + ".csv";
            _jpgFileName = RootPath + "jpg\\" + txtJpg.Text.Trim() + ".jpg";
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
            this.Close();
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
            SaveDatas();
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
            SaveDatas();
        }

        #endregion

        private void SaveDatas()
        {
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
        #endregion

        private void btn_Root_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                App_Configure.Cnfgs.Path_Rpt_Spc = fbd.SelectedPath + "\\spc";

                if (!Directory.Exists(App_Configure.Cnfgs.Path_Rpt_Spc))
                    Directory.CreateDirectory(App_Configure.Cnfgs.Path_Rpt_Spc);

                App_Configure.CreateReportSubFolder(App_Configure.Cnfgs.Path_Rpt_Spc);
                RootPath = App_Configure.Cnfgs.Path_Rpt_Spc + "\\";
                lblPath.Text = "�ļ�·��:" + App_Configure.Cnfgs.Path_Rpt_Spc;
            }
            fbd.Dispose();
        }


        #region ���÷���



        #endregion
    }
}