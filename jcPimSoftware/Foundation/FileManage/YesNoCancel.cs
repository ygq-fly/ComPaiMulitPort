using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace jcPimSoftware
{
    public partial class YesNoCancel : Form
    {
        public YesNoCancel(string info, string labTxt, string btnYesTxt, string btnNoTxt,string btnCancelTxt)
        {
            InitializeComponent();
            this.Text = info;
            labeInfo.Text = labTxt;
            btn_Yes.Text = btnYesTxt;
            btn_No.Text = btnNoTxt;
            btn_Cancel.Text = btnCancelTxt;
        }

        private void YesNoCancel_Load(object sender, EventArgs e)
        {
            pbxYesNo.Image = ImagesManage.GetImage("ico", "warning.ico");
        }

        private void btn_Yes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void btn_No_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}