using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace jcPimSoftware
{
    public partial class OkCancel : Form
    {

        #region 构造函数
        public OkCancel(string info,string labTxt,string btnOkTxt,string btnCancelTxt)
        {
            InitializeComponent();
            this.Text = info;
            l_Txt.Text = labTxt;
            b_Ok.Text = btnOkTxt;
            b_Cancel.Text = btnCancelTxt;
        }
        #endregion

        #region 按钮事件
        #region b_Ok
        private void b_Ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        #endregion

        #region b_Cancel
        private void b_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion

        private void OkCancel_Load(object sender, EventArgs e)
        {
            pbxOkCancel.Image = ImagesManage.GetImage("ico", "info.ico");
        }
        #endregion
    }
}