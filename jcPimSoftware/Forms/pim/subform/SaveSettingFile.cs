using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace jcPimSoftware
{
    public partial class SaveSettingFile : Form
    {
        //欲保存的文件名称
        private string _FileName;
        internal string FileName
        {
            get { return _FileName + ".ini"; }
        }

        #region 构造方法
        public SaveSettingFile()
        {
            InitializeComponent();
        }
        #endregion

        #region 检查文件名称是否符合格式
        /// <summary>
        /// 检查文件名称是否符合格式
        /// </summary>
        /// <param name="txt"></param>
        private bool CheckFileName(string txt)
        {
            string[] str = new string[] { "\\", "/", ":", "*", "?", "<", ">", "|" };

            if (txt != "")
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (txt.IndexOf(str[i]) != -1)
                    {
                        //MessageBox.Show(this, "文件名不能包含以下字符之一: \n    \\  /  :  *  ?  <  >  | ", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                return true;
            }
            else
            {
                //MessageBox.Show(this, "名称不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

        }
        #endregion

        #region 按钮事件
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CheckFileName(tbxFileName.Text))
            {
                _FileName = tbxFileName.Text;

                this.DialogResult = DialogResult.OK;

                this.Close();

            } else {
                lblInfo.Text = "File Name is invalid!";
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void tbxFileName_MouseClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref tbxFileName, tbxFileName.Text.Trim());
            testTouchPad.ShowDialog();
        }
        #endregion
    }
}