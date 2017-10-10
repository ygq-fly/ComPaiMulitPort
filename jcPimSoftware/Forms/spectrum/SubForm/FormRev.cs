using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace jcPimSoftware
{
    public partial class FormRev : Form
    {
        #region ��������

        /// <summary>
        /// ���˥����ֵ
        /// </summary>
        private float _rev = 0f;
        public float Rev
        {
            get { return _rev; }
            set { _rev = value; }
        }

        #endregion


        #region ���幹��
        /// <summary>
        /// ���幹��
        /// </summary>
        public FormRev()
        {
            InitializeComponent();
        }

        #endregion


        #region �����¼�

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormRev_Load(object sender, EventArgs e)
        {
            txtRev.Text = _rev.ToString("0.#");
        }

        #endregion

        #endregion


        #region ȷ��
        /// <summary>
        /// ȷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                _rev = float.Parse(txtRev.Text.Trim());
                this.DialogResult = DialogResult.OK;
            }
            catch
            {
                MessageBox.Show(this,"REV setup error!");
            }
        }

        #endregion


        #region ȡ��
        /// <summary>
        /// ȡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        #endregion


        #region  TouchPad
        /// <summary>
        /// TouchPad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRev_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref txtRev, txtRev.Text.Trim());
            testTouchPad.ShowDialog();
        }

        #endregion
    }
}