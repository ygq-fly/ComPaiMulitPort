// ===============================================================================
// �ļ�����FormAttOther
// �����ˣ����
// ��  �ڣ�2011-4-29 
//
// ��  ����ATT��ݲ˵��Ӵ���
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
    public partial class FormAttOther : Form
    {
        #region ��������

        /// <summary>
        /// ����ATT
        /// </summary>
        private int _inputAtt = 0;
        public int InputAtt
        {
            set { _inputAtt = value - 40; }
        }

        /// <summary>
        /// ����ATT
        /// </summary>
        private int _outputAtt = 0;
        public int OutputAtt
        {
            get { return _outputAtt + 40; }
        }

        #endregion


        #region ���幹��
        /// <summary>
        /// ���幹��
        /// </summary>
        public FormAttOther()
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
        private void FormAttOther_Load(object sender, EventArgs e)
        {
            txtAtt.Text = _inputAtt.ToString();
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
            double double_att = 0;
            int int_att = 0;
            if (CheckInput())
            {
                double_att = double.Parse(txtAtt.Text.Trim());
                int_att = (int)Math.Floor(double_att);

                int_att = int_att / 2 * 2;

                if (int_att < -40)
                {
                    int_att = -40;
                }
                if (int_att > 20)
                {
                    int_att = 20;
                }

                _outputAtt = int_att;

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
            double att = 0;

            try
            {
                att = double.Parse(txtAtt.Text.Trim());
            }
            catch
            {
                MessageBox.Show(this, "ATT setup error!");
                rev = false;
            }

            return rev;
        }

        #endregion

        #endregion


        #region TouchPad

        private void txtAtt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref txtAtt, txtAtt.Text.Trim());
            testTouchPad.ShowDialog();
        }

        #endregion 
    }
}