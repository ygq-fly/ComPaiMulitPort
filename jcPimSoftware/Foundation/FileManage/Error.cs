using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace jcPimSoftware
{
    public partial class Error : Form
    {
        public Error(string info, string labTxt, string btnOkTxt)
        {
            InitializeComponent();
            this.Text = info;
            lab.Text = labTxt;
            error_Btn.Text = btnOkTxt;
        }

        private void error_Btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void Error_Load(object sender, EventArgs e)
        {
            pbxError.Image = ImagesManage.GetImage("ico", "error.ico");
        }
    }
}