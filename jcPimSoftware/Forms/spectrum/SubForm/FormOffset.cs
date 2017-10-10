// ===============================================================================
// �ļ�����FormOffset
// �����ˣ����
// ��  �ڣ�2011-4-29 
//
// ��  ����Offset Option��ݲ˵��Ӵ���
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
using System.IO;
using SpectrumLib;

namespace jcPimSoftware
{
    public partial class FormOffset : Form
    {
        #region ��������

        /// <summary>
        /// ȷ�����ò�����ʶ
        /// </summary>
        private bool _bOn = false;
        public bool bOn
        {
            get { return _bOn; }
            set { _bOn = value; }
        }

        /// <summary>
        /// �����������ݽṹ
        /// </summary>
        private string[] _outOffsetData;
        public string[] OutOffsetData
        {
            get { return _outOffsetData; }
            set { _outOffsetData = value; }
        }

        /// <summary>
        /// ������ʶ true���� false����
        /// </summary>
        private bool _bEnableOffset = false;
        public bool bEnableOffset
        {
            get { return _bEnableOffset; }
            set { _bEnableOffset = value; }
        }

        /// <summary>
        /// ��ǰɨ�貽��
        /// </summary>
        private int _intRbw = 1;
        public int intRbw
        {
            get { return _intRbw; }
            set { _intRbw = value; }
        }

        /// <summary>
        /// ��ǰͨ�� true��� falseխ��
        /// </summary>
        private int _bBand = 0;
        public int bBand
        {
            get { return _bBand; }
            set { _bBand = value; }
        }

        /// <summary>
        /// �����ļ�·��
        /// </summary>
        private static string filePath = "";
        public static string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        #endregion


        #region ���幹��
        /// <summary>
        /// ���幹��
        /// </summary>
        public FormOffset()
        {
            InitializeComponent();
            plotOffset.Resume();
        }

        #endregion


        #region �����¼�

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormOffset_Load(object sender, EventArgs e)
        {
            if (App_Configure.Cnfgs.Spectrum == 0)
            {
                lblRBW.Text = _intRbw.ToString() + "KHz";
            }
            else
            {
                if (_intRbw > 1000)
                    lblRBW.Text = (_intRbw / 1000).ToString() + "KHz";
                else
                    lblRBW.Text = _intRbw.ToString() + "Hz";
            }

            if (_bBand == 0)
            {
                lblBind.Text = "Rev";
            }
            else
            {
                lblBind.Text = "Frd";
            }

            if (!bEnableOffset)
            {
                //btnEnable.Enabled = false;
            }
            else
            {
                filePath = GetOffsetFilePath();
                txtFilePath.Text = App_Settings.spc.OffsetFilePath;
                DarwOffsetLine();
            }

            chkTest.Checked = false;
            groupBoxOffset.Enabled = false;
        }

        #endregion

        #endregion


        #region ��ť�¼�

        #region ���
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = "C:";
            openFile.Filter = "TXT File(*.txt)|*.txt";

            if (openFile.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                txtFilePath.Text = openFile.FileName;
                //btnEnable.Enabled = true;
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnable_Click(object sender, EventArgs e)
        {
            if (chkTest.Checked)
                filePath = txtFilePath.Text;
            else
                filePath = GetOffsetFilePath();
            App_Settings.spc.OffsetFilePath = filePath;
            _outOffsetData = SpectrumOffset.LoadOffsetFile(filePath);
            PointF[] PaintData = GetPaintData(_outOffsetData);

            plotOffset.Add(PaintData, 0, 0);
            _bEnableOffset = true;
            _bOn = true;
            DarwOffsetLine();
        }

        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDisable_Click(object sender, EventArgs e)
        {
            plotOffset.Clear();
            _bEnableOffset = false;
            _bOn = false;
        }

        #endregion

        #region TestMode

        private void chkTest_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTest.Checked)
            {
                groupBoxOffset.Enabled = true;
                filePath = App_Settings.spc.OffsetFilePath;
            }
            else
            {
                groupBoxOffset.Enabled = false;
                filePath = GetOffsetFilePath();
            }
        }

        #endregion

        #endregion


        #region ����

        #region ����խ�����rbw��ȡ��Ӧ�Ĳ����ļ�·��
        /// <summary>
        /// ����խ�����rbw��ȡ��Ӧ�Ĳ����ļ�·��
        /// </summary>
        /// <returns>�����ļ�·��</returns>
        private string GetOffsetFilePath()
        {
            string strFilePath = "";
            string RootPath = Application.StartupPath + "\\" + App_Configure.Cnfgs.Path_Def + "\\Spectrum_Tables";

            if (_bBand == 0)
            {
                if (_intRbw == 4 || _intRbw == 10 * 1000 || _intRbw == 1)
                {
                    strFilePath = RootPath + "\\Ch1_4KHz.txt";
                }
                else if (_intRbw == 20 || _intRbw == 100 * 1000 || _intRbw == 10)
                {
                    strFilePath = RootPath + "\\Ch1_20KHz.txt";
                }
                else if (_intRbw == 100 || _intRbw == 1000 * 1000)
                {
                    strFilePath = RootPath + "\\Ch1_100KHz.txt";
                }
                else
                {
                    strFilePath = RootPath + "\\Ch1_1000KHz.txt";
                }
            }
            else
            {
                if (_intRbw == 4 || _intRbw == 10 * 1000 || _intRbw == 1)
                {
                    strFilePath = RootPath + "\\Ch2_4KHz.txt";
                }
                else if (_intRbw == 20 || _intRbw == 100 * 1000 || _intRbw == 10)
                {
                    strFilePath = RootPath + "\\Ch2_20KHz.txt";
                }
                else if (_intRbw == 100 || _intRbw == 1000 * 1000)
                {
                    strFilePath = RootPath + "\\Ch2_100KHz.txt";
                }
                else
                {
                    strFilePath = RootPath + "\\Ch1_1000KHz.txt";
                }
            }

            return strFilePath;
        }

        #endregion

        #region ��װ��ͼ����
        /// <summary>
        /// ��װ��ͼ����
        /// </summary>
        /// <param name="OffsetDataArray">��������</param>
        /// <returns>��ͼ�㼯</returns>
        private PointF[] GetPaintData(string[] OffsetDataArray)
        {
            if (OffsetDataArray == null)
            {
                return null;
            }

            PointF[] revPoints = new PointF[OffsetDataArray.Length];
            List<PointF> listPointF = new List<PointF>();

            float Ymin = 0;
            float Ymax = 0;
            for (int i = 0; i < OffsetDataArray.Length; i++)
            {
                revPoints[i].X = float.Parse(OffsetDataArray[i].Split(',')[0]);
                revPoints[i].Y = float.Parse(OffsetDataArray[i].Split(',')[1]);

                if (i == 0)
                {
                    Ymin = revPoints[i].Y;
                    Ymax = revPoints[i].Y;
                }
                else
                {
                    if (revPoints[i].Y < Ymin)
                    {
                        Ymin = revPoints[i].Y;
                        continue;
                    }
                    if (revPoints[i].Y > Ymax)
                    {
                        Ymax = revPoints[i].Y;
                    }
                }
            }

            plotOffset.SetXStartStop(0, 3000f);
            plotOffset.SetYStartStop(Ymin, Ymax);

            //��������ʼƵ�ʲ��Ǵ�0MHz�������һ����ͼ��
            if (revPoints[0].X > 0)
            {
                PointF pf = new PointF(0, revPoints[0].Y);
                listPointF.Add(pf);
            }
            for (int i = 0; i < revPoints.Length; i++)
            {
                listPointF.Add(revPoints[i]);
            }
            //����������Ƶ�ʲ���3000MHz���������һ����ͼ��
            if (revPoints[revPoints.Length - 1].X < 3000)
            {
                PointF pf = new PointF(3000, revPoints[revPoints.Length - 1].Y);
                listPointF.Add(pf);
            }

            return listPointF.ToArray();
        }

        #endregion

        #region ���Ʋ�������
        /// <summary>
        /// ���Ʋ�������
        /// </summary>
        private void DarwOffsetLine()
        {
            _outOffsetData = SpectrumOffset.LoadOffsetFile(filePath);
            PointF[] PaintData = GetPaintData(_outOffsetData);
            plotOffset.Add(PaintData, 0, 0);
        }

        #endregion

        #endregion
    }
}