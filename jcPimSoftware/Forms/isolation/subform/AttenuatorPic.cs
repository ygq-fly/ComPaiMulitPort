using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using jcPimSoftware;

namespace AnalyzerApparatus.Gui.Isolation
{
    public partial class AttenuatorPic : Form
    {
        public AttenuatorPic()
        {
            InitializeComponent();
            ChangeBtnPic(pictureBox1, "warnning_att.bmp");
            //panel1.
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 通用按钮图片切换
        /// <summary>
        /// 通用按钮图片切换
        /// </summary>
        /// <param name="pb">PictureBox对象</param>
        /// <param name="picName">图片名称</param>
        private void ChangeBtnPic(PictureBox pb, string picName)
        {
            pictureBox1.Image = ImagesManage.GetImage("Other", picName);
        }

        #endregion
    }
}