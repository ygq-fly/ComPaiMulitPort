using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace jcPimSoftware
{
    public partial class Ok : Form
    {
        #region ¹¹Ôìº¯Êý
        public Ok(string info,string txt,string btnTxt)
        {
            InitializeComponent();
            this.Text = info;
            la_Txt.Text = txt;
            bt_Ok.Text = btnTxt;
        }
        #endregion

        private void bt_Ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void Ok_Load(object sender, EventArgs e)
        {
            pbxOk.Image = ImagesManage.GetImage("ico", "info.ico");
        }
    }
}