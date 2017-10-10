using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace jcPimSoftware
{
    public partial class YesNo : Form
    {

        #region 构造函数
        public YesNo(string info,string labTxt,string btnYesTxt,string btnNoTxt)
        {
            InitializeComponent();
            this.Text = info;
            lb_Txt.Text = labTxt;
            b_Yes.Text = btnYesTxt;
            b_No.Text = btnNoTxt;
        }
        #endregion
         
        #region 按钮事件
        #region b_Yes
        private void b_Yes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }
        #endregion

        #region b_No
        private void b_No_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
        #endregion

        private void YesNo_Load(object sender, EventArgs e)
        {
            picBox.Image = ImagesManage.GetImage("ico", "warning.ico");
        }
        #endregion

    }
}