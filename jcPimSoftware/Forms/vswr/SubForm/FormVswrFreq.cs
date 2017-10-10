// ===============================================================================
// �ļ�����FormVswrFreq
// �����ˣ����
// ��  �ڣ�2011-5-19 
//
// ��  ����FREQ��ݲ˵��Ӵ���
//         
//
// ��  ���� 1.0.0.0
//
// ���¼�¼ 
// ===============================================================================
// ʱ  �䣺 2011-5-19    	   �������ļ�
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
    public partial class FormVswrFreq : Form
    {
        #region ��������

        /// <summary>
        /// פ�����ö���
        /// </summary>
        private Settings_Vsw settings;

        /// <summary>
        /// פ������Ƶ��(MHz)
        /// </summary>
        private float _vswrFreq = 935;
        public float VswrFreq
        {
            get { return _vswrFreq; }
            set { _vswrFreq = value; }
        }

        #endregion


        #region ���幹��
        /// <summary>
        /// ���幹��
        /// </summary>
        internal FormVswrFreq(Settings_Vsw vsw_settings)
        {
            InitializeComponent();
            this.settings = vsw_settings;
        }

        #endregion


        #region �����¼�

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormVswrFreq_Load(object sender, EventArgs e)
        {
            txtFreq.Text = settings.F.ToString("0.000");
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
                _vswrFreq = float.Parse(txtFreq.Text.Trim());
                this.DialogResult = DialogResult.OK;
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

        #region TouchPad

        private void txtFreq_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref txtFreq, txtFreq.Text.Trim());
            testTouchPad.ShowDialog();
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
                freq = float.Parse(txtFreq.Text.Trim());
                if (freq < App_Settings.sgn_1.Min_Freq || freq > App_Settings.sgn_2.Max_Freq)
                {
                    MessageBox.Show(this, "Frequency setup is out of its range!");
                    rev = false;
                }
            }
            catch
            {
                MessageBox.Show(this, "Frequency setup error!");
                rev = false;
            }

            return rev;
        }

        #endregion

        #endregion
    }
}