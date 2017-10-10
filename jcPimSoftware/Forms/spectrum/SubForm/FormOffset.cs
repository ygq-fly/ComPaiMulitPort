// ===============================================================================
// 文件名：FormOffset
// 创建人：倪骞
// 日  期：2011-4-29 
//
// 描  述：Offset Option快捷菜单子窗体
//         
//
// 版  本： 1.0.0.0
//
// 更新记录 
// ===============================================================================
// 时  间： 2011-4-29   	   创建该文件
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
        #region 变量定义

        /// <summary>
        /// 确定启用补偿标识
        /// </summary>
        private bool _bOn = false;
        public bool bOn
        {
            get { return _bOn; }
            set { _bOn = value; }
        }

        /// <summary>
        /// 传出补偿数据结构
        /// </summary>
        private string[] _outOffsetData;
        public string[] OutOffsetData
        {
            get { return _outOffsetData; }
            set { _outOffsetData = value; }
        }

        /// <summary>
        /// 补偿标识 true启用 false禁用
        /// </summary>
        private bool _bEnableOffset = false;
        public bool bEnableOffset
        {
            get { return _bEnableOffset; }
            set { _bEnableOffset = value; }
        }

        /// <summary>
        /// 当前扫描步进
        /// </summary>
        private int _intRbw = 1;
        public int intRbw
        {
            get { return _intRbw; }
            set { _intRbw = value; }
        }

        /// <summary>
        /// 当前通带 true宽带 false窄带
        /// </summary>
        private int _bBand = 0;
        public int bBand
        {
            get { return _bBand; }
            set { _bBand = value; }
        }

        /// <summary>
        /// 补偿文件路径
        /// </summary>
        private static string filePath = "";
        public static string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        #endregion


        #region 窗体构造
        /// <summary>
        /// 窗体构造
        /// </summary>
        public FormOffset()
        {
            InitializeComponent();
            plotOffset.Resume();
        }

        #endregion


        #region 窗体事件

        #region 窗体加载
        /// <summary>
        /// 窗体加载
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


        #region 按钮事件

        #region 浏览
        /// <summary>
        /// 浏览
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

        #region 启用
        /// <summary>
        /// 启用
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

        #region 禁用
        /// <summary>
        /// 禁用
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


        #region 方法

        #region 根据窄宽带、rbw获取相应的补偿文件路径
        /// <summary>
        /// 根据窄宽带、rbw获取相应的补偿文件路径
        /// </summary>
        /// <returns>补偿文件路径</returns>
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

        #region 封装绘图数据
        /// <summary>
        /// 封装绘图数据
        /// </summary>
        /// <param name="OffsetDataArray">补偿数据</param>
        /// <returns>绘图点集</returns>
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

            //若补偿起始频率不是从0MHz，补充第一个绘图点
            if (revPoints[0].X > 0)
            {
                PointF pf = new PointF(0, revPoints[0].Y);
                listPointF.Add(pf);
            }
            for (int i = 0; i < revPoints.Length; i++)
            {
                listPointF.Add(revPoints[i]);
            }
            //若补偿结束频率不是3000MHz，补充最后一个绘图点
            if (revPoints[revPoints.Length - 1].X < 3000)
            {
                PointF pf = new PointF(3000, revPoints[revPoints.Length - 1].Y);
                listPointF.Add(pf);
            }

            return listPointF.ToArray();
        }

        #endregion

        #region 绘制补偿曲线
        /// <summary>
        /// 绘制补偿曲线
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