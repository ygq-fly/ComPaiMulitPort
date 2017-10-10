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
    public partial class PimSaveForm : Form
    {
        #region 变量定义

        public bool _csvFlag = false;
        public bool _jpgFlag = false;
        public bool _pdfFlag = false;
        #endregion

        #region 构造方法
        public PimSaveForm( )
        {
            InitializeComponent();
        }
        #endregion

        #region 窗体事件
        private void PimSaveForm_Load(object sender, EventArgs e)
        {
            IniFile.SetFileName( "D:\\settings\\Specifics.ini");

            chkCsv.Checked = IniFile.GetString("Specifics", "csvC", "1")=="1"?true:false;
            chkPDF.Checked = IniFile.GetString("Specifics", "pdfC", "0") == "1" ? true : false;
            chkJpg.Checked = IniFile.GetString("Specifics", "jpgC", "0") == "1" ? true : false;
            //chkCsv.Checked = Convert.ToBoolean(App_Configure.Cnfgs.Csv_checked);
            //chkPDF.Checked = Convert.ToBoolean(App_Configure.Cnfgs.Pdf_checked);
            //chkJpg.Checked = Convert.ToBoolean(App_Configure.Cnfgs.Jpg_checked);
           
            string formate = "yyyy-MM-dd HH-mm-ss";
            txtCsv.Text = DateTime.Now.ToString(formate);
            txtJpg.Text = DateTime.Now.ToString(formate);
            txtPDF.Text = DateTime.Now.ToString(formate);
            textBox2.Text = App_Configure.Cnfgs.Modno;
            textBox3.Text = App_Configure.Cnfgs.Serno;
            textBox1.Text = App_Configure.Cnfgs.Opeor;
            SaveDatas();
            lblPath.Text = "文件路径:" + App_Configure.Cnfgs.Path_Rpt_Pim;
        }
        #endregion

        #region SaveDatas
        private void SaveDatas()
        {
            if (chkCsv.Checked)
            {
                _csvFlag = true;
                txtCsv.Enabled = true;
            }
            else
            {
                _csvFlag = false;
                txtCsv.Enabled = false;
            }
            if (chkJpg.Checked)
            {
                _jpgFlag = true;
                txtJpg.Enabled = true;
            }
            else
            {
                _jpgFlag = false;
                txtJpg.Enabled = false;
            }
            if (chkPDF.Checked)
            {
                _pdfFlag = true;
                txtPDF.Enabled = true;
            }
            else
            {
                _pdfFlag = false;
                txtPDF.Enabled = false;
            }
        }
        #endregion

        #region 按钮事件
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            IniFile.SetFileName("D:\\settings\\Specifics.ini");
            IniFile.SetString("Specifics", "csvC", _csvFlag?"1":"0");
            IniFile.SetString("Specifics", "pdfC", _pdfFlag?"1":"0");
            IniFile.SetString("Specifics", "jpgC", _jpgFlag?"1":"0");
            this.DialogResult = DialogResult.OK;
        }


        private void chkJpg_CheckedChanged(object sender, EventArgs e)
        {
            SaveDatas();
        }

        private void chkPDF_CheckedChanged(object sender, EventArgs e)
        {
            SaveDatas();
        }

        private void chkCsv_CheckedChanged(object sender, EventArgs e)
        {
            SaveDatas();
        }

        private void txt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string[] txt = sender.ToString().Split(char.Parse(":"));
            TouchPad((TextBox)sender, txt[1]);
        }
        #endregion

        #region TouchPad
        /// <summary>
        /// TouchPad
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="text"></param>
        private void TouchPad(TextBox textBox, string text)
        {
            TouchPad testTouchPad = new TouchPad(ref textBox, text);
            testTouchPad.ShowDialog();
        }
        #endregion

        private void btn_Root_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                App_Configure.Cnfgs.Path_Rpt_Pim = fbd.SelectedPath + "\\pim";
                if (!Directory.Exists(App_Configure.Cnfgs.Path_Rpt_Pim))
                    Directory.CreateDirectory(App_Configure.Cnfgs.Path_Rpt_Pim);

                App_Configure.CreateReportSubFolder(App_Configure.Cnfgs.Path_Rpt_Pim);
                lblPath.Text = "文件路径:" + App_Configure.Cnfgs.Path_Rpt_Pim;
            }
            fbd.Dispose();
        }

        private void groupBoxType_Enter(object sender, EventArgs e)
        {

        }

        private void txtPDF_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtJpg_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCsv_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

      
    }
}