// ===============================================================================
// �ļ�����FormRF
// �����ˣ����
// ��  �ڣ�2011-6-22 
//
// ��  ����FormRF��ݲ˵��Ӵ���
//         
//
// ��  ���� 1.0.0.0
//
// ���¼�¼ 
// ===============================================================================
// ʱ  �䣺 2011-6-22   	   �������ļ�
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
    public partial class FormRF : Form
    {
        #region ��������

        /// <summary>
        /// ���ű��
        /// </summary>
        private int _num = 1;

        /// <summary>
        /// RF1Ƶ��
        /// </summary>
        public static float FreqRF_1 = App_Settings.sgn_1.Min_Freq;

        /// <summary>
        /// RF1����
        /// </summary>
        public static float TxRF_1 = 30;

        /// <summary>
        /// RF1���ñ�ʶ
        /// </summary>
        public static bool EnableRF_1 = false;

        /// <summary>
        /// RF2Ƶ��
        /// </summary>
        public static float FreqRF_2 = App_Settings.sgn_2.Min_Freq;

        /// <summary>
        /// RF2����
        /// </summary>
        public static float TxRF_2 = 30;

        /// <summary>
        /// RF2���ñ�ʶ
        /// </summary>
        public static bool EnableRF_2 = false;

        #endregion


        #region ���幹��
        /// <summary>
        /// ���幹��
        /// </summary>
        public FormRF(int num)
        {
            InitializeComponent();

            _num = num;
        }

        #endregion


        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormRF_Load(object sender, EventArgs e)
        {
            if (_num == 1)
            {
                groupBox1.Text = "Channel: RF_1";
                numericUpDownFreq.Maximum = (decimal)App_Settings.sgn_1.Max_Freq;
                numericUpDownFreq.Minimum = (decimal)App_Settings.sgn_1.Min_Freq;
                numericUpDownTx.Maximum = (decimal)App_Settings.sgn_1.Max_Power;
                numericUpDownTx.Minimum = (decimal)App_Settings.sgn_1.Min_Power;

                numericUpDownFreq.Value = (decimal)FreqRF_1;
                numericUpDownTx.Value = (decimal)TxRF_1;
                chkEnable.Checked = EnableRF_1;
            }
            else
            {
                groupBox1.Text = "Channe2: RF_2";
                numericUpDownFreq.Maximum = (decimal)App_Settings.sgn_2.Max_Freq;
                numericUpDownFreq.Minimum = (decimal)App_Settings.sgn_2.Min_Freq;
                numericUpDownTx.Maximum = (decimal)App_Settings.sgn_2.Max_Power;
                numericUpDownTx.Minimum = (decimal)App_Settings.sgn_2.Min_Power;

                numericUpDownFreq.Value = (decimal)FreqRF_2;
                numericUpDownTx.Value = (decimal)TxRF_2;
                chkEnable.Checked = EnableRF_2;
            }
        }

        #endregion


        #region ��ť�¼�

        #region O K
        /// <summary>
        /// O K
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_num == 1)
            {
                FreqRF_1 = (float)numericUpDownFreq.Value;
                TxRF_1 = (float)numericUpDownTx.Value;
                EnableRF_1 = chkEnable.Checked;
            }
            else
            {
                FreqRF_2 = (float)numericUpDownFreq.Value;
                TxRF_2 = (float)numericUpDownTx.Value;
                EnableRF_2 = chkEnable.Checked;
            }

            this.Close();
        }

        #endregion

        #endregion


        #region TouchPad

        private void numericUpDownFreq_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref numericUpDownFreq, numericUpDownFreq.Value.ToString().Trim());
            testTouchPad.ShowDialog();
        }

        private void numericUpDownTx_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref numericUpDownTx, numericUpDownTx.Value.ToString().Trim());
            testTouchPad.ShowDialog();
        }

        #endregion
    }
}