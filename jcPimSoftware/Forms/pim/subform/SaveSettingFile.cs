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
        //��������ļ�����
        private string _FileName;
        internal string FileName
        {
            get { return _FileName + ".ini"; }
        }

        #region ���췽��
        public SaveSettingFile()
        {
            InitializeComponent();
        }
        #endregion

        #region ����ļ������Ƿ���ϸ�ʽ
        /// <summary>
        /// ����ļ������Ƿ���ϸ�ʽ
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
                        //MessageBox.Show(this, "�ļ������ܰ��������ַ�֮һ: \n    \\  /  :  *  ?  <  >  | ", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                return true;
            }
            else
            {
                //MessageBox.Show(this, "���Ʋ���Ϊ��!", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

        }
        #endregion

        #region ��ť�¼�
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