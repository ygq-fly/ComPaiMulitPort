// ===============================================================================
// 文件名：TestForm
// 创建人：倪骞
// 日  期：2011-6-8
//
// 描  述：TEST MODE模块
//         
//
// 版  本： 1.0.0.0
//
// 更新记录 
// ===============================================================================
// 时  间： 2011-6-8   	   创建该文件
//
// ===============================================================================



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SpectrumLib.Models;
using SpectrumLib.Defines;
using System.Threading;

namespace jcPimSoftware
{
    public partial class TestForm : Form
    {
        #region 变量定义

        /// <summary>
        /// 执行扫描参数，在扫描函数中，应该使用此参数
        /// 它从usr_sweeps复制过来
        /// </summary>
        private SweepParams exe_params;

        /// <summary>
        /// 频谱分析线程
        /// </summary>
        private Thread thdAnalysis;

        /// <summary>
        /// 功放1测试线程
        /// </summary>
        private Thread thdRF1;

        /// <summary>
        /// 功放2测试线程
        /// </summary>
        private Thread thdRF2; 

        /// <summary>
        /// 频谱仪接口对象
        /// </summary>
        private SpectrumLib.ISpectrum ISpectrumObj;

        /// <summary>
        /// 频谱仪类型 0 NEC 1 Bird
        /// </summary>
        int type = 0;

        /// <summary>
        /// 功放1等待句柄
        /// </summary>
        private ManualResetEvent power1_Handle;

        /// <summary>
        /// 功放2等待句柄
        /// </summary>
        private ManualResetEvent power2_Handle;

        /// <summary>
        /// 功放1异常信息对象
        /// </summary>
        private RFErrors rfErrors_1;

        /// <summary>
        /// 功放2异常信息对象
        /// </summary>
        private RFErrors rfErrors_2;

        /// <summary>
        /// 功放1状态信息对象
        /// </summary>
        private PowerStatus rfStatus_1;

        /// <summary>
        /// 功放2状态信息对象
        /// </summary>
        private PowerStatus rfStatus_2;

        /// <summary>
        /// 窗体句柄
        /// </summary>
        private IntPtr _handle = IntPtr.Zero;

        /// <summary>
        /// 功放协议类型(0紫光功放，1韩国功放)
        /// </summary>
        private readonly int RF_Type = 0;

        /// <summary>
        /// 开启功放1等待稳定输出的时间
        /// </summary>
        private int Wait_time1 = 3000;

        /// <summary>
        /// 开启功放2等待稳定输出的时间
        /// </summary>
        private int Wait_time2 = 3000;

        #endregion


        #region 窗体构造
        /// <summary>
        /// 窗体构造
        /// </summary>
        public TestForm()
        {
            InitializeComponent();
        }

        #endregion


        #region 窗体事件

        #region 窗体加载
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestForm_Load(object sender, EventArgs e)
        {
            _handle = this.Handle;

            //频谱类型
            type = App_Configure.Cnfgs.Spectrum;
            switch (type)
            {
                case 0:
                    ISpectrumObj = new SpectrumLib.Spectrums.SpeCat2(this.Handle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
                    break;
                case 1:
                    ISpectrumObj = new SpectrumLib.Spectrums.BirdSh(this.Handle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
                    break;
                case 2:
                    ISpectrumObj = new SpectrumLib.Spectrums.Deli(this.Handle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
                    break;
                case 3:
                    ISpectrumObj = new SpectrumLib.Spectrums.FanShuang(this.Handle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
                    break;
            }

            ISpectrumObj.EnableLog();

            //绑定RBW
            Bind(comboBoxRbw);

            if (type == 0)
            {
                comboBoxVbw.Enabled = false;
                comboBoxVbw.Visible = false;
                label19.Visible = false;
            }
            else if (type == 1)
            {
                //绑定VBW
                Bind(comboBoxVbw);
            }
            else
            {
                comboBoxVbw.Enabled = false;
                comboBoxVbw.Visible = false;
                label19.Visible = false;
            }

            //绑定Fun
            BindFun();

            //扫描对象
            exe_params = new SweepParams();
            exe_params.SweepType = SweepType.Time_Sweep;
            exe_params.WndHandle = this.Handle;
            exe_params.DevInfo = new DeviceInfo();
            exe_params.TmeParam = new TimeSweepParam();
            exe_params.WndHandle = _handle;
            power1_Handle = new ManualResetEvent(false);
            power2_Handle = new ManualResetEvent(false);
            //建立射频功放层
            RFSignal.SetWndHandle(exe_params.WndHandle);

            btnRF1_On.Enabled = true;
            btnRF1_Off.Enabled = false;

            btnRF2_On.Enabled = true;
            btnRF2_Off.Enabled = false;

            btnSpe_On.Enabled = true;
            btnSpe_Off.Enabled = false;

            //范围
            numericUpDownFreq1.Minimum = (decimal)App_Settings.sgn_1.Min_Freq;
            numericUpDownFreq1.Maximum = (decimal)App_Settings.sgn_1.Max_Freq;
            numericUpDownFreq2.Minimum = (decimal)App_Settings.sgn_2.Min_Freq;
            numericUpDownFreq2.Maximum = (decimal)App_Settings.sgn_2.Max_Freq;
            numericUpDownRF1.Minimum = (decimal)App_Settings.sgn_1.Min_Power;
            numericUpDownRF1.Maximum = (decimal)App_Settings.sgn_1.Max_Power;
            numericUpDownRF2.Minimum = (decimal)App_Settings.sgn_2.Min_Power;
            numericUpDownRF2.Maximum = (decimal)App_Settings.sgn_2.Max_Power;

            //初值
            numericUpDownFreq1.Value = (decimal)App_Settings.sgn_1.Min_Freq;
            numericUpDownFreq2.Value = (decimal)App_Settings.sgn_2.Min_Freq;
            numericUpDownRF1.Value = (decimal)App_Settings.sgn_1.Min_Power;
            numericUpDownRF2.Value = (decimal)App_Settings.sgn_2.Min_Power;
            numericUpDownSpe.Value = (decimal)App_Settings.sgn_1.Min_Freq;

            if (App_Configure.Cnfgs.Cal_Use_Table)
            {
                btnOffset.Enabled = false;
                chkTable.Checked = true;
                rad_frd.Enabled = true;
                rad_rev.Enabled = true;
            }
            else
            {
                btnOffset.Enabled = true;
                chkTable.Checked = false;
                rad_frd.Enabled = false;
                rad_rev.Enabled = false;
            }
        }

        #endregion

        #region 窗体关闭
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (thdRF1 != null && thdRF1.IsAlive)
            {
                thdRF1.Abort();
            }
            StopRF(RFInvolved.Rf_1);
            if (thdRF2 != null && thdRF2.IsAlive)
            {
                thdRF2.Abort();
            }
            StopRF(RFInvolved.Rf_2);

            StopScan();
        }

        #endregion

        #endregion


        #region 按钮事件

        #region RF1 ON

        private void btnRF1_On_Click(object sender, EventArgs e)
        {
            numericUpDownFreq1.Enabled = false;
            numericUpDownRF1.Enabled = false;
            btnRF1_On.Enabled = false;
            btnRF1_Off.Enabled = true;

            float freq_1 = (float)numericUpDownFreq1.Value;
            float power_1 = (float)numericUpDownRF1.Value;
            exe_params.TmeParam.F1 = freq_1;
            exe_params.TmeParam.P1 = OffsetPower(freq_1, power_1, 1);

            exe_params.DevInfo.RF_Addr1 = App_Configure.Cnfgs.ComAddr1;
            exe_params.RFInvolved = RFInvolved.Rf_1;
            rfStatus_1 = new PowerStatus();
            rfErrors_1 = new RFErrors();

            thdRF1 = new Thread(StartRF);
            thdRF1.IsBackground = true;
            thdRF1.Start(RFInvolved.Rf_1);
        }

        #endregion

        #region RF1 OFF

        private void btnRF1_Off_Click(object sender, EventArgs e)
        {
            btnRF1_On.Enabled = true;
            btnRF1_Off.Enabled = false;

            if (thdRF1 != null && thdRF1.IsAlive)
            {
                thdRF1.Abort();
            }

            StopRF(RFInvolved.Rf_1);
        }

        #endregion

        #region RF2 ON

        private void btnRF2_On_Click(object sender, EventArgs e)
        {
            numericUpDownFreq2.Enabled = false;
            numericUpDownRF2.Enabled = false;
            btnRF2_On.Enabled = false;
            btnRF2_Off.Enabled = true;

            float freq_2 = (float)numericUpDownFreq2.Value;
            float power_2 = (float)numericUpDownRF2.Value;
            exe_params.TmeParam.F2 = freq_2;
            exe_params.TmeParam.P2 = OffsetPower(freq_2, power_2, 2);

            exe_params.DevInfo.RF_Addr2 = App_Configure.Cnfgs.ComAddr2;
            exe_params.RFInvolved = RFInvolved.Rf_2;
            rfStatus_2 = new PowerStatus();
            rfErrors_2 = new RFErrors();

            thdRF2 = new Thread(StartRF);
            thdRF2.IsBackground = true;
            thdRF2.Start(RFInvolved.Rf_2);
        }

        #endregion

        #region RF2 OFF

        private void btnRF2_Off_Click(object sender, EventArgs e)
        {
            btnRF2_On.Enabled = true;
            btnRF2_Off.Enabled = false;

            if (thdRF2 != null && thdRF2.IsAlive)
            {
                thdRF2.Abort();
            }

            StopRF(RFInvolved.Rf_2);
        }

        #endregion

        #region Spe On

        private void btnSpe_On_Click(object sender, EventArgs e)
        {
            btnSpe_On.Enabled = false;
            btnSpe_Off.Enabled = true;

            StartScan();
        }

        #endregion

        #region Spe Off

        private void btnSpe_Off_Click(object sender, EventArgs e)
        {
            StopScan();
        }

        #endregion

        #region Offset

        private void btnOffset_Click(object sender, EventArgs e)
        {
            Config cf = new Config();
            cf.TabOffsetIndex = (int)comboBoxFun.SelectedValue;
            cf.ShowDialog();
        }

        #endregion

        #endregion


        #region 其他事件

        #region TAB切换事件
        /// <summary>
        /// TAB切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControlTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlTest.SelectedTab.Name == "tabPageRF")
            {
                StopScan();
                MessageBox.Show(this, "Please check the external attenuator!");
            }
            else
            {
                if (thdRF1 != null && thdRF1.IsAlive)
                {
                    thdRF1.Abort();
                }
                StopRF(RFInvolved.Rf_1);

                if (thdRF2 != null && thdRF2.IsAlive)
                {
                    thdRF2.Abort();
                }
                StopRF(RFInvolved.Rf_2);
            }
        }

        #endregion

        #region 切换表格补偿
        /// <summary>
        /// 切换表格补偿
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTable_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTable.Checked)
            {
                btnOffset.Enabled = false;
                rad_frd.Enabled = true;
                rad_rev.Enabled = true;
            }
            else
            {
                btnOffset.Enabled = true;
                rad_frd.Enabled = false;
                rad_rev.Enabled = false;
            }
        }

        #endregion

        #region 前、反向切换
        /// <summary>
        /// 反向
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rad_rev_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_rev.Checked)
            {
                rad_frd.Checked = false;
            }
        }

        /// <summary>
        /// 前向
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rad_frd_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_frd.Checked)
            {
                rad_rev.Checked = false;
            }
        }

        #endregion

        #endregion


        #region TouchPad

        private void numericUpDownFreq1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad tp = new TouchPad(ref numericUpDownFreq1, numericUpDownFreq1.Value.ToString());
            tp.ShowDialog();
        }

        private void numericUpDownRF1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad tp = new TouchPad(ref numericUpDownRF1, numericUpDownRF1.Value.ToString());
            tp.ShowDialog();
        }

        private void numericUpDownFreq2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad tp = new TouchPad(ref numericUpDownFreq2, numericUpDownFreq2.Value.ToString());
            tp.ShowDialog();
        }

        private void numericUpDownRF2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad tp = new TouchPad(ref numericUpDownRF2, numericUpDownRF2.Value.ToString());
            tp.ShowDialog();
        }

        private void numericUpDownSpe_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad tp = new TouchPad(ref numericUpDownSpe, numericUpDownSpe.Value.ToString());
            tp.ShowDialog();
        }

        #endregion


        #region 辅助方法

        #region 绑定功能模块
        /// <summary>
        /// 绑定功能模块
        /// </summary>
        private void BindFun()
        {
            DataTable dt_fun = new DataTable();
            dt_fun.Columns.Add("name", typeof(string));
            dt_fun.Columns.Add("number", typeof(int));

            DataRow dr = dt_fun.NewRow();
            dr["name"] = "PIM";
            dr["number"] = 0;
            dt_fun.Rows.Add(dr);

            //dr = dt_fun.NewRow();
            //dr["name"] = "ISOLATION";
            //dr["number"] = 1;
            //dt_fun.Rows.Add(dr);

            //dr = dt_fun.NewRow();
            //dr["name"] = "VSWR";
            //dr["number"] = 2;
            //dt_fun.Rows.Add(dr);

            //dr = dt_fun.NewRow();
            //dr["name"] = "HARMONIC";
            //dr["number"] = 3;
            //dt_fun.Rows.Add(dr);

            comboBoxFun.DataSource = dt_fun;
            comboBoxFun.DisplayMember = "name";
            comboBoxFun.ValueMember = "number";
        }

        #endregion

        #region 绑定分析步进
        /// <summary>
        /// 绑定分析步进
        /// </summary>
        private void Bind(ComboBox cmbx)
        {
            if (type == 0)
            {
                DataTable dt_nec = new DataTable();
                dt_nec.Columns.Add("rbw", typeof(string));
                dt_nec.Columns.Add("value", typeof(int));

                DataRow dr = dt_nec.NewRow();
                dr["rbw"] = "1 KHz";
                dr["value"] = 1;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "4 KHz";
                dr["value"] = 4;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "8 KHz";
                dr["value"] = 8;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "20 KHz";
                dr["value"] = 20;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "40 KHz";
                dr["value"] = 40;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "100 KHz";
                dr["value"] = 100;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "250 KHz";
                dr["value"] = 250;
                dt_nec.Rows.Add(dr);

                cmbx.DataSource = dt_nec;
                cmbx.DisplayMember = "rbw";
                cmbx.ValueMember = "value";
            }
            else if(type==1)
            {
                DataTable dt_bird = new DataTable();
                dt_bird.Columns.Add("rbw", typeof(string));
                dt_bird.Columns.Add("value", typeof(int));

                DataRow dr = dt_bird.NewRow();
                dr["rbw"] = "1 KHz";
                dr["value"] = 1000;
                dt_bird.Rows.Add(dr);

                dr = dt_bird.NewRow();
                dr["rbw"] = "3 KHz";
                dr["value"] = 3000;
                dt_bird.Rows.Add(dr);

                dr = dt_bird.NewRow();
                dr["rbw"] = "10 KHz";
                dr["value"] = 10000;
                dt_bird.Rows.Add(dr);

                dr = dt_bird.NewRow();
                dr["rbw"] = "30 KHz";
                dr["value"] = 30000;
                dt_bird.Rows.Add(dr);

                dr = dt_bird.NewRow();
                dr["rbw"] = "100 KHz";
                dr["value"] = 100000;
                dt_bird.Rows.Add(dr);

                dr = dt_bird.NewRow();
                dr["rbw"] = "300 KHz";
                dr["value"] = 300000;
                dt_bird.Rows.Add(dr);

                dr = dt_bird.NewRow();
                dr["rbw"] = "1000 KHz";
                dr["value"] = 1000000;
                dt_bird.Rows.Add(dr);

                cmbx.DataSource = dt_bird;
                cmbx.DisplayMember = "rbw";
                cmbx.ValueMember = "value";
            }
            else if (type == 2)
            {
                DataTable dt_nec = new DataTable();
                dt_nec.Columns.Add("rbw", typeof(string));
                dt_nec.Columns.Add("value", typeof(int));

                DataRow dr = dt_nec.NewRow();
                dr["rbw"] = "1 KHz";
                dr["value"] = 1;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "10 KHz";
                dr["value"] = 10;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "100 KHz";
                dr["value"] = 100;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "1000 KHz";
                dr["value"] = 1000;
                dt_nec.Rows.Add(dr);

                cmbx.DataSource = dt_nec;
                cmbx.DisplayMember = "rbw";
                cmbx.ValueMember = "value";
            }
            else if (type == 3)
            {
                DataTable dt_nec = new DataTable();
                dt_nec.Columns.Add("rbw", typeof(string));
                dt_nec.Columns.Add("value", typeof(int));

                DataRow dr = dt_nec.NewRow();
                dr["rbw"] = "1 KHz";
                dr["value"] = -1000;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "10 KHz";
                dr["value"] = 10;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "100 KHz";
                dr["value"] = 100;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "1000 KHz";
                dr["value"] = 1000;
                dt_nec.Rows.Add(dr);

                cmbx.DataSource = dt_nec;
                cmbx.DisplayMember = "rbw";
                cmbx.ValueMember = "value";
            }
        }

        #endregion

        #region 计算功放补偿
        /// <summary>
        /// 计算功放补偿
        /// </summary>
        /// <param name="freq">功放频率</param>
        /// <param name="p">输出功率</param>
        /// <param name="num">功放编号</param>
        /// <returns>补偿后的输出功率</returns>
        private float OffsetPower(float freq, float p, int num)
        {
            float revP = p;

            if (chkTable.Checked)
            {
                if (rad_rev.Checked)
                {
                    if ((int)comboBoxFun.SelectedValue == 0)
                    {
                        if (num == 1)
                        {
                            
                            //revP = Tx_Tables.pim_rev_tx1.Offset(freq, p, Tx_Tables.pim_rev_offset1) + p;
                            if (App_Configure.Cnfgs.Mode >=2)
                            {
                                if (PimForm.port1_rev_fwd == 1 || PimForm.port1_rev_fwd == 2)
                                    revP = Tx_Tables.pim_rev_tx1.Offset(freq, p, Tx_Tables.pim_rev_offset1) + p ;
                                else
                                    revP = Tx_Tables.pim_rev2_tx1.Offset(freq, p, Tx_Tables.pim_rev2_offset1) + p ;
                            }
                            else
                                revP = Tx_Tables.pim_rev_tx1.Offset(freq, p, Tx_Tables.pim_rev_offset1) + p ;
                        }
                        else
                        {
                            //revP = Tx_Tables.pim_rev_tx2.Offset(freq, p, Tx_Tables.pim_rev_offset2) + p;
                            if (App_Configure.Cnfgs.Mode >=2)
                            {
                                if (PimForm.port1_rev_fwd == 1 || PimForm.port1_rev_fwd == 2)
                                    revP = Tx_Tables.pim_rev_tx2.Offset(freq, p, Tx_Tables.pim_rev_offset2) + p ;
                                else
                                    revP = Tx_Tables.pim_rev2_tx2.Offset(freq, p, Tx_Tables.pim_rev2_offset2) + p ;
                            }
                            else
                                revP = Tx_Tables.pim_rev_tx2.Offset(freq, p, Tx_Tables.pim_rev_offset2) + p ;
                        }
                    }
                    //else if ((int)comboBoxFun.SelectedValue == 1)
                    //{
                    //    if (num == 1)
                    //    {
                    //        revP = Tx_Tables.iso_tx1.Offset(freq, p, Tx_Tables.iso_offset1) + p;
                    //    }
                    //    else
                    //    {
                    //        revP = Tx_Tables.iso_tx2.Offset(freq, p, Tx_Tables.iso_offset2) + p;
                    //    }
                    //}
                    //else if ((int)comboBoxFun.SelectedValue == 2)
                    //{
                    //    if (num == 1)
                    //    {
                    //        revP = Tx_Tables.vsw_tx1.Offset(freq, p, Tx_Tables.vsw_offset1) + p;
                    //    }
                    //    else
                    //    {
                    //        revP = Tx_Tables.vsw_tx2.Offset(freq, p, Tx_Tables.vsw_offset2) + p;
                    //    }
                    //}
                    //else
                    //{
                    //    if (num == 1)
                    //    {
                    //        revP = Tx_Tables.har_tx1.Offset(freq, p, Tx_Tables.har_offset1) + p;
                    //    }
                    //    else
                    //    {
                    //        revP = Tx_Tables.har_tx2.Offset(freq, p, Tx_Tables.har_offset2) + p;
                    //    }
                    //}
                }
                else
                {
                    if ((int)comboBoxFun.SelectedValue == 0)
                    {
                        if (num == 1)
                        {
                            //revP = Tx_Tables.pim_rev_tx1.Offset(freq, p, Tx_Tables.pim_rev_offset1) + p;
                            
                            if(PimForm.port1_rev_fwd==1||PimForm.port1_rev_fwd==2)
                                revP = Tx_Tables.pim_rev_tx1.Offset(freq, p, Tx_Tables.pim_rev_offset1) + p ;
                            else
                                revP = Tx_Tables.pim_rev2_tx1.Offset(freq, p, Tx_Tables.pim_rev2_offset1) + p ;
                        }
                        else
                        {
                            //revP = Tx_Tables.pim_rev_tx2.Offset(freq, p, Tx_Tables.pim_rev_offset2) + p;

                            if (PimForm.port1_rev_fwd == 1 || PimForm.port1_rev_fwd == 2)
                                revP = Tx_Tables.pim_rev_tx2.Offset(freq, p, Tx_Tables.pim_rev_offset2) + p;
                            else
                                revP = Tx_Tables.pim_rev2_tx2.Offset(freq, p, Tx_Tables.pim_rev2_offset2) + p ;
                        }

                        //if (num == 1)
                        //{
                        //    revP = Tx_Tables.pim_frd_tx1.Offset(freq, p, Tx_Tables.pim_frd_offset1) + p;
                        //}
                        //else
                        //{
                        //    revP = Tx_Tables.pim_frd_tx2.Offset(freq, p, Tx_Tables.pim_frd_offset2) + p;
                        //}
                    }
                    //else if ((int)comboBoxFun.SelectedValue == 1)
                    //{
                    //    if (num == 1)
                    //    {
                    //        revP = Tx_Tables.iso_tx1.Offset(freq, p, Tx_Tables.iso_offset1) + p;
                    //    }
                    //    else
                    //    {
                    //        revP = Tx_Tables.iso_tx2.Offset(freq, p, Tx_Tables.iso_offset2) + p;
                    //    }
                    //}
                    //else if ((int)comboBoxFun.SelectedValue == 2)
                    //{
                    //    if (num == 1)
                    //    {
                    //        revP = Tx_Tables.vsw_tx1.Offset(freq, p, Tx_Tables.vsw_offset1) + p;
                    //    }
                    //    else
                    //    {
                    //        revP = Tx_Tables.vsw_tx2.Offset(freq, p, Tx_Tables.vsw_offset2) + p;
                    //    }
                    //}
                    //else
                    //{
                    //    if (num == 1)
                    //    {
                    //        revP = Tx_Tables.har_tx1.Offset(freq, p, Tx_Tables.har_offset1) + p;
                    //    }
                    //    else
                    //    {
                    //        revP = Tx_Tables.har_tx2.Offset(freq, p, Tx_Tables.har_offset2) + p;
                    //    }
                    //}
                }
            }
            else
            {
                if ((int)comboBoxFun.SelectedValue == 0)
                {
                    if (num == 1)
                    {
                        revP = (float)App_Factors.pim_tx1.ValueWithOffset(freq, p);
                    }
                    else
                    {
                        revP = (float)App_Factors.pim_tx2.ValueWithOffset(freq, p);
                    }
                }
                else if ((int)comboBoxFun.SelectedValue == 1)
                {
                    if (num == 1)
                    {
                        revP = (float)App_Factors.iso_tx1.ValueWithOffset(freq, p);
                    }
                    else
                    {
                        revP = (float)App_Factors.iso_tx2.ValueWithOffset(freq, p);
                    }
                }
                else if ((int)comboBoxFun.SelectedValue == 2)
                {
                    if (num == 1)
                    {
                        revP = (float)App_Factors.vsw_tx1.ValueWithOffset(freq, p);
                    }
                    else
                    {
                        revP = (float)App_Factors.vsw_tx2.ValueWithOffset(freq, p);
                    }
                }
                else
                {
                    if (num == 1)
                    {
                        revP = (float)App_Factors.har_tx1.ValueWithOffset(freq, p);
                    }
                    else
                    {
                        revP = (float)App_Factors.har_tx2.ValueWithOffset(freq, p);
                    }
                }
            }

            return revP;
        }

        #endregion

        #region 计算频谱补偿
        /// <summary>
        /// 计算频谱补偿
        /// </summary>
        /// <param name="freq">频率</param>
        /// <param name="p">接收到的功率</param>
        /// <returns>补偿后的功率</returns>
        private float OffsetSpec(float freq, float p)
        {
            float revP = p;

            if (App_Configure.Cnfgs.Cal_Use_Table)
            {
                if (rad_rev.Checked)
                {
                    if ((int)comboBoxFun.SelectedValue == 0)
                    {
                        revP = Rx_Tables.Offset(freq, FuncModule.PIM, true) + p;
                    }
                    //else if ((int)comboBoxFun.SelectedValue == 1)
                    //{
                    //    revP = Rx_Tables.Offset(freq, FuncModule.ISO) + p;
                    //}
                    //else if ((int)comboBoxFun.SelectedValue == 2)
                    //{
                    //    revP = Rx_Tables.Offset(freq, FuncModule.VSW) + p;
                    //}
                    //else
                    //{
                    //    revP = Rx_Tables.Offset(freq, FuncModule.HAR) + p;
                    //}
                }
                else
                {
                    if ((int)comboBoxFun.SelectedValue == 0)
                    {
                        revP = Rx_Tables.Offset(freq, FuncModule.PIM, false) + p;
                    }
                }
            }
            else
            {
                if ((int)comboBoxFun.SelectedValue == 0)
                {
                    if (App_Configure.Cnfgs.Channel == 0)
                        revP = (float)App_Factors.pim_rx1.ValueWithOffset(freq, p);
                    else if (App_Configure.Cnfgs.Channel == 1)
                        revP = (float)App_Factors.pim_rx2.ValueWithOffset(freq, p);
                }
                else if ((int)comboBoxFun.SelectedValue == 1)
                {
                    //revP = (float)App_Factors.iso_rx1.ValueWithOffset(freq, p);
                }
                else if ((int)comboBoxFun.SelectedValue == 2)
                {
                    //revP = (float)App_Factors.vsw_rx1.ValueWithOffset(freq, p);
                }
                else
                {
                    //revP = (float)App_Factors.har_rx1.ValueWithOffset(freq, p);
                }
            }

            return (float)Math.Round(revP, 2);
        }

        #endregion

        #region 启动频谱分析
        /// <summary>
        /// 启动频谱分析
        /// </summary>
        private void StartScan()
        {
            numericUpDownSpe.Enabled = false;
            comboBoxRbw.Enabled = false;

            float centerFreq = (float)numericUpDownSpe.Value;
            int rbw = (int)comboBoxRbw.SelectedValue;
            int vbw = rbw;
            if (type == 1)
                vbw = (int)comboBoxVbw.SelectedValue;
            object o;

            SpectrumLib.Models.ScanModel ScanModel;
            ScanModel = new ScanModel();
            ScanModel.StartFreq = centerFreq - App_Settings.pim.Scanband;
            ScanModel.EndFreq = centerFreq + App_Settings.pim.Scanband;
            ScanModel.Unit = CommonDef.EFreqUnit.MHz;
            ScanModel.Att = 0;
            ScanModel.Rbw = rbw;
            ScanModel.Vbw = vbw;
            ScanModel.EnableTimer = true;
            ScanModel.Continued = true;
            ScanModel.TimeSpan = App_Settings.spc.SampleSpan;
            if (type == 1)
            {
                ScanModel.FullPoints = true;
            }
            else
            {
                ScanModel.FullPoints = false  ;
            }
           
            ScanModel.Deli_averagecount = 6;
            ScanModel.Deli_detector = "AVERage";
            ScanModel.Deli_ref = -50;//REF
            ScanModel.Deli_refoffset = 0;
            ScanModel.Deli_startspe = 1;
            ScanModel.DeliSpe = CommonDef.SpectrumType.Deli_SPECTRUM;
            ScanModel.Deli_isSpectrum = true;

            o = ScanModel;
            thdAnalysis = new Thread(ISpectrumObj.StartAnalysis);
            thdAnalysis.IsBackground = true;
            thdAnalysis.Start(o);
        }

        #endregion

        #region 停止频谱分析
        /// <summary>
        /// 停止频谱分析
        /// </summary>
        private void StopScan()
        {
            ISpectrumObj.StopAnalysis();
            if (thdAnalysis != null)
            {
                if (thdAnalysis.IsAlive)
                {
                    thdAnalysis.Abort();
                }
            }

            numericUpDownSpe.Enabled = true;
            comboBoxRbw.Enabled = true;

            btnSpe_On.Enabled = true;
            btnSpe_Off.Enabled = false;
        }

        #endregion

        #region 获取频谱数据
        /// <summary>
        /// 获取频谱数据
        /// </summary>
        /// <returns></returns>
        private float GetSpeRev()
        {
            int maxIndex = 0;
            float max = float.MinValue;
            PointF[] PaintPointFs = (PointF[])ISpectrumObj.GetSpectrumData();
            for (int i = 0; i < PaintPointFs.Length; i++)
            {
                if (PaintPointFs[i].Y > max)
                {
                    max = PaintPointFs[i].Y;
                    maxIndex = i;
                }
            }

            max = OffsetSpec(PaintPointFs[maxIndex].X, PaintPointFs[maxIndex].Y);
            return max;
        }

        #endregion

        #region 启动功放测试
        /// <summary>
        /// 启动功放测试
        /// </summary>
        /// <param name="Num">功放编号</param>
        private void StartRF(object RF)
        {
            RFInvolved Num;
            Num = (RFInvolved)RF;
            bool bErrors = false;

            if (Num == RFInvolved.Rf_1)
            {
                bErrors = RF_Set(exe_params.DevInfo.RF_Addr1,
                      exe_params.RFPriority,
                      exe_params.TmeParam.P1, exe_params.TmeParam.F1,
                      true, false, true, true);

                //if(bErrors)
                //    NativeMessage.PostMessage(_handle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);

                for (int i = 0; i < 60; i++)
                {
                    bErrors |= RF_Sample(exe_params.DevInfo.RF_Addr1,
                                      exe_params.RFPriority,
                                      ref rfStatus_1);

                    //检查功放异常现象，包括功放通信超时
                    bErrors = CheckRF_1(bErrors);
                    if (bErrors)
                    {
                        NativeMessage.PostMessage(_handle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
                        return;
                    }

                    Thread.Sleep(1000);
                }
            }
            else
            {
                bErrors = RF_Set(exe_params.DevInfo.RF_Addr2,
                     exe_params.RFPriority,
                     exe_params.TmeParam.P2, exe_params.TmeParam.F2,
                     true, false, true, true);

                //if (bErrors)
                //    NativeMessage.PostMessage(_handle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);

                for (int i = 0; i < 60; i++)
                {
                    bErrors |= RF_Sample(exe_params.DevInfo.RF_Addr2,
                                      exe_params.RFPriority,
                                      ref rfStatus_2);

                    //检查功放异常现象，包括功放通信超时
                    bErrors = CheckRF_2(bErrors);
                    if (bErrors)
                    {
                        NativeMessage.PostMessage(_handle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                        return;
                    }

                    Thread.Sleep(1000);
                }
            }
        }

        #endregion

        #region 停止功放测试
        /// <summary>
        /// 停止功放测试
        /// </summary>
        /// <param name="Num">功放编号</param>
        private void StopRF(RFInvolved Num)
        {
            if (Num == RFInvolved.Rf_1)
            {
                RF_Set(exe_params.DevInfo.RF_Addr1,
                      exe_params.RFPriority,
                      exe_params.TmeParam.P1, exe_params.TmeParam.F1,
                      false, false, false, false);

                numericUpDownFreq1.Enabled = true;
                numericUpDownRF1.Enabled = true;

                btnRF1_On.Enabled = true;
                btnRF1_Off.Enabled = false;
            }
            else
            {
                RF_Set(exe_params.DevInfo.RF_Addr2,
                     exe_params.RFPriority,
                     exe_params.TmeParam.P2, exe_params.TmeParam.F2,
                     false, false, false, false);

                numericUpDownFreq2.Enabled = true;
                numericUpDownRF2.Enabled = true;

                btnRF2_On.Enabled = true;
                btnRF2_Off.Enabled = false;
            }
        }

        #endregion

        #region 功放异常检测
        /// <summary>
        /// 依据全局静态的功放设备限制条件进行异常检查
        /// </summary>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        private bool CheckRF_1(bool timeOut)
        {
            bool errors = false;

            rfErrors_1.RF_TimeOut = timeOut;
            if (timeOut)
                Log.WriteLog("RF1通讯超时!", Log.EFunctionType.TestMode);

            else
            {
                if ((rfStatus_1.Status2.Current < App_Configure.Cnfgs.Min_Curr) ||
                    (rfStatus_1.Status2.Current > App_Configure.Cnfgs.Max_Curr))
                {
                    errors = true;
                    rfErrors_1.RF_CurrError = true;
                    rfErrors_1.RF_CurrValue = rfStatus_1.Status2.Current;
                    Log.WriteLog("RF1电流异常!  I1=" + rfStatus_1.Status2.Current.ToString(), Log.EFunctionType.TestMode);
                }

                if ((rfStatus_1.Status2.Temp < App_Configure.Cnfgs.Min_Temp) ||
                    (rfStatus_1.Status2.Temp > App_Configure.Cnfgs.Max_Temp))
                {
                    errors = true;
                    rfErrors_1.RF_TempError = true;
                    rfErrors_1.RF_TempValue = rfStatus_1.Status2.Temp;
                    Log.WriteLog("RF1温度异常!  Temp1=" + rfStatus_1.Status2.Temp.ToString(), Log.EFunctionType.TestMode);
                }

                if ((rfStatus_1.Status2.Vswr > App_Configure.Cnfgs.Max_Vswr))
                {
                    errors = true;
                    rfErrors_1.RF_VswrError = true;
                    rfErrors_1.RF_VswrValue = rfStatus_1.Status2.Vswr;
                    Log.WriteLog("RF1驻波异常!  VSWR1=" + rfStatus_1.Status2.Vswr.ToString(), Log.EFunctionType.TestMode);
                }
            }

            return (errors || timeOut);
        }

        /// <summary>
        /// 依据全局静态的功放设备限制条件进行异常检查
        /// </summary>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        private bool CheckRF_2(bool timeOut)
        {
            bool errors = false;

            rfErrors_2.RF_TimeOut = timeOut;
            if (timeOut)
                Log.WriteLog("RF2通讯超时!", Log.EFunctionType.TestMode);

            else
            {
                if ((rfStatus_2.Status2.Current < App_Configure.Cnfgs.Min_Curr) ||
                    (rfStatus_2.Status2.Current > App_Configure.Cnfgs.Max_Curr))
                {
                    errors = true;
                    rfErrors_2.RF_CurrError = true;
                    rfErrors_2.RF_CurrValue = rfStatus_2.Status2.Current;
                    Log.WriteLog("RF2电流异常!  I2=" + rfStatus_2.Status2.Current.ToString(), Log.EFunctionType.TestMode);
                }

                if ((rfStatus_2.Status2.Temp < App_Configure.Cnfgs.Min_Temp) ||
                    (rfStatus_2.Status2.Temp > App_Configure.Cnfgs.Max_Temp))
                {
                    errors = true;
                    rfErrors_2.RF_TempError = true;
                    rfErrors_2.RF_TempValue = rfStatus_2.Status2.Temp;
                    Log.WriteLog("RF2温度异常!  Temp1=" + rfStatus_2.Status2.Temp.ToString(), Log.EFunctionType.TestMode);
                }

                if (rfStatus_2.Status2.Vswr > App_Configure.Cnfgs.Max_Vswr)
                {
                    errors = true;
                    rfErrors_2.RF_VswrError = true;
                    rfErrors_2.RF_VswrValue = rfStatus_2.Status2.Vswr;
                    Log.WriteLog("RF2驻波异常!  VSWR2=" + rfStatus_2.Status2.Vswr.ToString(), Log.EFunctionType.TestMode);
                }
            }

            return (errors || timeOut);
        }

        #endregion

        #region 发送功放命令
        #region RF_Set
        /// <summary>
        /// RF_Set
        /// </summary>
        /// <param name="Addr">功放地址</param>
        /// <param name="Lvl">命令等级</param>
        /// <param name="P">功率</param>
        /// <param name="F">频率</param>
        /// <param name="RFon">开启功放标识</param>
        /// <param name="ignoreRFon">忽略开功放标识</param>
        /// <param name="useP">设置功率标识</param>
        /// <param name="useF">设置频率标识</param>
        /// <returns>true成功 false超时</returns>
        private bool RF_Set(int Addr,
                           int Lvl,
                           float P,
                           float F,
                           bool RFon,
                           bool ignoreRFon,
                           bool useP,
                           bool useF)
        {
            bool RF_Succ = true;

            RFSignal.RFClear(Addr, Lvl);

            //紫光功放改变频率会影响功率，需先设置频率；韩国功放改变功率会影响频率，需先设置功率
            if (RF_Type == 0)
            {
                if (useF)
                    RFSignal.RFFreq(Addr, Lvl, F);

                if (useP)
                    RFSignal.RFPower(Addr, Lvl, P);
            }
            else
            {
                if (useP)
                    RFSignal.RFPower(Addr, Lvl, P);

                if (useF)
                    RFSignal.RFFreq(Addr, Lvl, F);
            }

            if (!ignoreRFon)
            {
                if (RFon)
                    RFSignal.RFOn(Addr, Lvl);
                else
                    RFSignal.RFOff(Addr, Lvl);
            }

            RFSignal.RFStart(Addr);


            //等待功放
            if (Addr == exe_params.DevInfo.RF_Addr1)
            {
                RF_Succ = power1_Handle.WaitOne(1000, true);
                power1_Handle.Reset();
            }
            else
            {
                RF_Succ = power2_Handle.WaitOne(1000, true);
                power2_Handle.Reset();
            }

            if (RFon == true && ignoreRFon == false)
            {
                if (RF_Type == 0)
                {
                    if (Addr == exe_params.DevInfo.RF_Addr1)
                        Thread.Sleep(Wait_time1);
                    else
                        Thread.Sleep(Wait_time2);
                }
            }
            else
            {
                if (RF_Type == 0)
                    Thread.Sleep(50);
                else
                    Thread.Sleep(150);
            }

            //返回通信超时的情况
            return (!RF_Succ);
        }


        #endregion

        #region RF_Sample
        /// <summary>
        /// RF_Sample
        /// </summary>
        /// <param name="Addr">功放地址</param>
        /// <param name="Lvl">命令等级</param>
        /// <param name="status">功放状态对象</param>
        /// <returns>true成功 false超时</returns>
        private bool RF_Sample(int Addr,
                              int Lvl,
                              ref PowerStatus status)
        {
            bool RF_Succ = true;

            RFSignal.RFClear(Addr, Lvl);

            RFSignal.RFSample(Addr, Lvl);

            RFSignal.RFStart(Addr);

            if (Addr == exe_params.DevInfo.RF_Addr1)
            {
                RF_Succ = power1_Handle.WaitOne(1000, true);
                power1_Handle.Reset();
            }
            else
            {
                RF_Succ = power2_Handle.WaitOne(1000, true);
                power2_Handle.Reset();
            }

            //没有发生功放通信超时，则获取功放状态信息
            if (RF_Succ)
                RFSignal.RFStatus(Addr, ref status);

            //返回通信超时的情况
            return (!RF_Succ);
        }

        #endregion 
        #endregion

        #endregion


        #region 窗体消息

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case MessageID.SPECTRUEME_SUCCED:
                    lblRevSpe.Text = GetSpeRev().ToString();
                    break;
                case MessageID.SPECTRUM_ERROR:
                    MessageBox.Show(this, "Spectrum analysis failed. It may be caused by the spectrum device does not connect or scanning failed!");
                    StopScan();
                    break;
                case MessageID.RF_SUCCED_ALL:
                    if (m.WParam.ToInt32() == exe_params.DevInfo.RF_Addr1)
                    {
                        power1_Handle.Set();
                    }
                    if (m.WParam.ToInt32() == exe_params.DevInfo.RF_Addr2)
                    {
                        power2_Handle.Set();
                    }
                    break;
                case MessageID.RF_ERROR:
                    if (m.WParam.ToInt32() == exe_params.DevInfo.RF_Addr1)
                    {
                        if (thdRF1 != null && thdRF1.IsAlive)
                        {
                            thdRF1.Abort();
                        }
                        StopRF(RFInvolved.Rf_1);
                        MessageBox.Show(this, "PA1 operation failed!");
                    }
                    if (m.WParam.ToInt32() == exe_params.DevInfo.RF_Addr2)
                    {
                        if (thdRF2 != null && thdRF2.IsAlive)
                        {
                            thdRF2.Abort();
                        }
                        StopRF(RFInvolved.Rf_2);
                        MessageBox.Show(this, "PA2 operation failed!");
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        #endregion

    }
}