// ===============================================================================
// �ļ�����FormSpanOther
// �����ˣ����
// ��  �ڣ�2011-4-29 
//
// ��  ����SPAN��ݲ˵��Ӵ���
//         
//
// ��  ���� 1.0.0.0
//
// ���¼�¼ 
// ===============================================================================
// ʱ  �䣺 2011-4-29   	   �������ļ�
//
// ===============================================================================



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace jcPimSoftware
{
    public partial class FormSpanOther : Form
    {
        #region ��������

        /// <summary>
        /// ��ǰɨ�����
        /// </summary>
        private int _inputSpan = 0;
        public int InputSpan
        {
            set { _inputSpan = value; }
        }

        /// <summary>
        /// ��ɨ�����
        /// </summary>
        private int _outputSpan = 0;
        public int OutputSpan
        {
            get { return _outputSpan; }
        }

        #endregion


        #region ���幹��
        /// <summary>
        /// ���幹��
        /// </summary>
        public FormSpanOther()
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
        private void FormSpanOther_Load(object sender, EventArgs e)
        {
            double currentSpan = _inputSpan / 1000.0;
            txtSpan.Text = currentSpan.ToString("0.000");
        }

        #endregion

        #endregion


        #region ��ť�¼�

        #region ȷ��
        /// <summary>
        /// ȷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                double Span = double.Parse(txtSpan.Text.Trim());
                _outputSpan = (int)(Span * 1000);
                this.DialogResult = DialogResult.OK;
                this.Close();
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
            this.Close();
        }

        #endregion

        #endregion


        #region ����

        #region ����У��
        /// <summary>
        /// ����У��
        /// </summary>
        /// <returns>true�ɹ� falseʧ��</returns>
        private bool CheckInput()
        {
            bool rev = true;
            double freq = 0;

            try
            {
                freq = double.Parse(txtSpan.Text.Trim());
                if (freq < 0 || freq > 3000)
                {
                    MessageBox.Show(this, "Scanning band is out of its range!");
                    rev = false;
                }
            }
            catch
            {
                MessageBox.Show(this, "Scanning band setup error!");
                rev = false;
            }

            return rev;
        }

        #endregion

        private void txtSpan_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref txtSpan, txtSpan.Text.Trim());
            testTouchPad.ShowDialog();
        }

        #endregion

    }
}