using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace jcPimSoftware
{
    public partial class FormScanCount : Form
    {
        #region 变量

        private int scancount;

        public int Scancount
        {
            get { return scancount; }
            set { scancount = value; }
        }

        #endregion


        public FormScanCount()
        {
            InitializeComponent();
        }

        /// <summary>OK
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                int count = int.Parse(txtcount.Text.Trim());
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        #region 输入校验
        /// <summary>
        /// 输入校验
        /// </summary>
        /// <returns>true成功 false失败</returns>
        private bool CheckInput()
        {
            bool rev = true;
            int count  = 0;

            try
            {
                count = int.Parse(txtcount.Text.Trim());
                if (count < 1 || count > 30)
                {
                    MessageBox.Show(this, "ScanCount is out of its range!");
                    rev = false;
                }
            }
            catch
            {
                MessageBox.Show(this, "ScanCount setup error!");
                rev = false;
            }

            return rev;
        }
        #endregion

        /// <summary>Cancel
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormScanCount_Load(object sender, EventArgs e)
        {
            txtcount.Text = scancount.ToString();
        }

        private void txtcount_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref txtcount, txtcount.Text.Trim());
            testTouchPad.ShowDialog();
        }

    }
}