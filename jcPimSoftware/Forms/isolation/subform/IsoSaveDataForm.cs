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
    public partial class IsoSaveDataForm : Form
    {
        public bool _csvFlag = false;
        public bool _jpgFlag = false;
        public bool _pdfFlag = false;
        public IsoSaveDataForm()
        {
            InitializeComponent();
        }

        private void IsoSaveDataForm_Load(object sender, EventArgs e)
        {
            chkCsv.Checked = Convert.ToBoolean(App_Configure.Cnfgs.Csv_checked);
            chkPdf.Checked = Convert.ToBoolean(App_Configure.Cnfgs.Pdf_checked);
            chkJpg.Checked = Convert.ToBoolean(App_Configure.Cnfgs.Jpg_checked);
            string s = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            txtCsv.Text = s;
            txtPdf.Text = s;
            txtJpg.Text = s;
            SaveDatats();
            textBox3.Text = App_Configure.Cnfgs.Modno;
            textBox2.Text = App_Configure.Cnfgs.Serno;
            textBox1.Text = App_Configure.Cnfgs.Opeor;
            lblPath.Text = "文件路径:" + App_Configure.Cnfgs.Path_Rpt_Iso;
        }

        private void chkCsv_CheckedChanged(object sender, EventArgs e)
        {
            SaveDatats();
        }

        private void chkJpg_CheckedChanged(object sender, EventArgs e)
        {
            SaveDatats();
        }

        private void chkPdf_CheckedChanged(object sender, EventArgs e)
        {
            SaveDatats();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CheckFileExists();
        }

        private void SaveDatats()
        {
            if (chkCsv.Checked)
            {
                txtCsv.Enabled = true;
                _csvFlag = true;
            }
            else
            {
                txtCsv.Enabled = false;
                _csvFlag = false;
            }

            if (chkJpg.Checked)
            {
                txtJpg.Enabled = true;
                _jpgFlag = true;
            }
            else
            {
                txtJpg.Enabled = false;
                _jpgFlag = false;
            }

            if (chkPdf.Checked)
            {
                txtPdf.Enabled = true;
                _pdfFlag = true;
            }
            else
            {
                txtPdf.Enabled = false;
                _pdfFlag = false;
            }
        }

        /// <summary>
        /// 返回CSV报表文件全路径名称
        /// </summary>
        public string Csv_FileName
        {
            get
            {
                string s = "";

                if (this.chkCsv.Checked)
                    s = App_Configure.Cnfgs.Path_Rpt_Iso + "\\csv\\" + this.txtCsv.Text + ".csv";

                return s;
            }
        }

        /// <summary>
        /// 返回JPG报表文件全路径名称
        /// </summary>
        public string Jpg_FileName
        {
            get
            {
                string s = "";

                if (this.chkJpg.Checked)
                    s = App_Configure.Cnfgs.Path_Rpt_Iso + "\\jpg\\" + this.txtJpg.Text + ".jpg";

                return s;
            }
        }

        /// <summary>
        /// 返回PDF报表文件全路径名称
        /// </summary>
        public string Pdf_FileName
        {
            get
            {
                string s = "";

                if (this.chkPdf.Checked)
                    s = App_Configure.Cnfgs.Path_Rpt_Iso + "\\pdf\\" + this.txtPdf.Text + ".pdf";

                return s;
            }
        }

        private bool ValidateFileName(string txt)
        {
            string[] str = new string[] { "\\", "/", ":", "*", "?", "<", ">", "|" };

            if (txt != "")
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (txt.IndexOf(str[i]) != -1)
                        return false;
                }
                return true;
            }
            else
                return false;
        }

        private void CheckFileExists()
        {
            bool bExists;

            if (chkCsv.Checked)
            {
                if (!ValidateFileName(txtCsv.Text))
                {
                    MessageBox.Show(this,"file name invalid or is null!");

                    return;
                }

                bExists = File.Exists(App_Configure.Cnfgs.Path_Rpt_Iso + "\\csv\\" + txtCsv.Text + ".csv");

                if (bExists)
                {
                    MessageBox.Show(this,"The CSV file name has already existed!");

                    return;
                }
            }

            if (chkJpg.Checked)
            {
                if (!ValidateFileName(txtJpg.Text))
                {
                    MessageBox.Show(this,"file name invalid or is null!");

                    return;
                }

                bExists = File.Exists(App_Configure.Cnfgs.Path_Rpt_Iso + "\\jpg\\" + txtJpg.Text + ".jpg");

                if (bExists)
                {
                    MessageBox.Show(this,"The jpg file name has already existed!");

                    return;
                }
            }

            if (chkPdf.Checked)
            {
                if (!ValidateFileName(txtPdf.Text))
                {
                    MessageBox.Show(this,"file name invalid or is null!");

                    return;
                }

                bExists = File.Exists(App_Configure.Cnfgs.Path_Rpt_Iso + "\\pdf\\" + txtPdf.Text + ".pdf");

                if (bExists)
                {
                    MessageBox.Show(this,"The PDF file name has already existed!");

                    return;
                }
            }

            //告知文件名称检验成功
            DialogResult = DialogResult.OK;
        }


        #region TouchPad
        private void tbxTxt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string[] txt = sender.ToString().Split(char.Parse(":"));
            TouchPad((TextBox)sender, txt[1]);
        }

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
                App_Configure.Cnfgs.Path_Rpt_Iso = fbd.SelectedPath + "\\iso";

                if (!Directory.Exists(App_Configure.Cnfgs.Path_Rpt_Iso))
                    Directory.CreateDirectory(App_Configure.Cnfgs.Path_Rpt_Iso);

                App_Configure.CreateReportSubFolder(App_Configure.Cnfgs.Path_Rpt_Iso);
                lblPath.Text = "文件路径:" + App_Configure.Cnfgs.Path_Rpt_Iso;
            }
            fbd.Dispose();
        }
    }
}