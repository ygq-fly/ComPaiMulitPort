// ===============================================================================
// �ļ�����FormFreq
// �����ˣ����
// ��  �ڣ�2011-4-29 
//
// ��  ����FREQ��ݲ˵��Ӵ���
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
    public partial class FormFreq : Form
    {
        #region ��������

        /// <summary>
        /// 0��ʼƵ�ʣ�1����Ƶ�ʣ�2����Ƶ��
        /// </summary>
        private int _intType = 0;
        public int intType
        {
            get { return _intType; }
            set { _intType = value; }
        }

        /// <summary>
        /// ��ǰ����Ƶ��
        /// </summary>
        private int _intCurrentFreq = 0;
        public int IntCurrentFreq
        {
            set { _intCurrentFreq = value; }
        }
    
        /// <summary>
        /// ��ʼƵ��
        /// </summary>
        private int _intStartFreq = 0;
        public int IntStartFreq
        {
            get { return _intStartFreq; }
        }

        /// <summary>
        /// ����Ƶ��
        /// </summary>
        private int _intEndFreq = 0;
        public int IntEndFreq
        {
            get { return _intEndFreq; }
        }

        /// <summary>
        /// ����Ƶ��
        /// </summary>
        private int _intCenterFreq = 0;
        public int IntCenterFreq
        {
            get { return _intCenterFreq; }
        }

        #endregion


        #region ���幹��
        /// <summary>
        /// ���幹��
        /// </summary>
        public FormFreq()
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
        private void FormFreq_Load(object sender, EventArgs e)
        {
            double doubleCurrentFreq = _intCurrentFreq / 1000.0;
            switch (_intType)
            {
                case 0:
                    this.Text = "StartFreq";
                    txtFreq.Text = doubleCurrentFreq.ToString("0.000");
                    break;
                case 1:
                    this.Text = "EndFreq";
                    txtFreq.Text = doubleCurrentFreq.ToString("0.000");
                    break;
                case 2:
                    this.Text = "CenterFreq";
                    txtFreq.Text = doubleCurrentFreq.ToString("0.000");
                    break;
                default:
                    _intType = 0;
                    this.Text = "StartFreq";
                    txtFreq.Text = doubleCurrentFreq.ToString("0.000");
                    break;
            }
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
                double freq = double.Parse(txtFreq.Text.Trim());
                switch (_intType)
                {
                    case 0:
                        _intStartFreq = (int)(freq * 1000);
                        break;
                    case 1:
                        _intEndFreq = (int)(freq * 1000);
                        break;
                    case 2:
                        _intCenterFreq = (int)(freq * 1000);
                        break;
                    default:
                        _intStartFreq = (int)(freq * 1000);
                        break;
                }
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
                freq = double.Parse(txtFreq.Text.Trim());
                if (freq < 0 || freq > 3000)
                {
                    MessageBox.Show(this,"Frequency setup is out of its range!");
                    rev = false;
                }
            }
            catch
            {
                MessageBox.Show(this,"Frequency setup error!");
                rev = false;
            }

            return rev;
        }

        #endregion

        #endregion


        #region TouchPad

        private void txtFreq_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref txtFreq, txtFreq.Text.Trim());
            testTouchPad.ShowDialog();
        }

        #endregion 
    }
}