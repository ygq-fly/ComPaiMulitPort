// ===============================================================================
// �ļ�����FormEntry
// �����ˣ����
// ��  �ڣ�2011-4-29 
//
// ��  ����ENTRY��ݲ˵��Ӵ���
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
    public partial class FormEntry : Form
    {
        #region ��������

        /// <summary>
        /// ����EntryƵ��
        /// </summary>
        private int _inputEntry = 0;
        public int InputEntry
        {
            set { _inputEntry = value; }
        }

        /// <summary>
        /// ���EntryƵ��
        /// </summary>
        private int _outputEntry = 0;
        public int OutputEntry
        {
            get { return _outputEntry; }
        }

        /// <summary>
        /// ��ǰ��ͼ���Ƶ��
        /// </summary>
        private int _maxFreq = 0;
        public int MaxFreq
        {
            get { return _maxFreq; }
            set { _maxFreq = value; }
        }

        /// <summary>
        /// ��ǰ��ͼ��СƵ��
        /// </summary>
        private int _minFreq = 0;
        public int MinFreq
        {
            get { return _minFreq; }
            set { _minFreq = value; }
        }


        #endregion


        #region ���幹��
        /// <summary>
        /// ���幹��
        /// </summary>
        public FormEntry()
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
        private void FormEntry_Load(object sender, EventArgs e)
        {
            double EntryFreq = _inputEntry / 1000.0;
            txtEntry.Text = EntryFreq.ToString("0.000");
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
                _outputEntry = Convert.ToInt32(double.Parse(txtEntry.Text.Trim()) * 1000);
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
                freq = double.Parse(txtEntry.Text.Trim());
                if (_minFreq < freq / 1000.0 || freq > _maxFreq / 1000.0)
                {
                    rev = false;
                }
            }
            catch
            {
                MessageBox.Show(this, "Frequency setup is out of its range!");
                rev = false;
            }

            return rev;
        }

        #endregion

        #endregion


        #region TouchPad

        private void txtEntry_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref txtEntry, txtEntry.Text.Trim());
            testTouchPad.ShowDialog();
        }

        #endregion 
    }
}