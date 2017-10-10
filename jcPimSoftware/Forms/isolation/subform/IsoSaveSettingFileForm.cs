using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace jcPimSoftware
{
    public partial class IsoSaveSettingFileForm : Form
    {
        public IsoSaveSettingFileForm()
        {
            InitializeComponent();
        }

        //欲保存的文件名称
        private string _FileName;
        internal string FileName
        {
            get { return _FileName + ".ini"; }
        }

        /// <summary>
        /// 检查文件名称是否符合格式
        /// </summary>
        /// <param name="txt"></param>
        private bool CheckFileName(string txt)
        {
            string[] str = new string[] { "\\", "/", ":", "*", "?", "<", ">", "|" };

            if (txt != "")
            {
                for (int i = 0; i < str.Length; i++) {
                    if (txt.IndexOf(str[i]) != -1)                   
                        return false;
                }
                return true;
            }
            else            
                return false;
        }

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
    }
}