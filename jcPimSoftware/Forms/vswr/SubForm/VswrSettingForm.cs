using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;



namespace jcPimSoftware
{
    public partial class VswrSettingForm : Form
    {
        #region 变量定义

        /// <summary>
        /// 用户配置信息
        /// </summary>
        private Settings_Vsw settings;


        /// <summary>
        /// 功放一驻波校准对象集合
        /// </summary>
        public static List<jcPimSoftware.VswrForm.CalibrationObj> listCurrentCAL_1 = new List<jcPimSoftware.VswrForm.CalibrationObj>();

        /// <summary>
        /// 功放二驻波校准对象集合
        /// </summary>
        public static List<jcPimSoftware.VswrForm.CalibrationObj> listCurrentCAL_2 = new List<jcPimSoftware.VswrForm.CalibrationObj>();


        /// <summary>
        /// 功放一校准标识
        /// </summary>
        public static bool bEnableCAL_RF1 = false;

        /// <summary>
        /// 功放二校准标识
        /// </summary>
        public static bool bEnableCAL_RF2 = false;

        #endregion


        #region 窗体构造
        /// <summary>
        /// 窗体构造
        /// </summary>
        internal VswrSettingForm(Settings_Vsw vsw_settings)
        {
            InitializeComponent();
            this.settings = vsw_settings;
            cbxCarrier.SelectedIndex = 0;
        }

        #endregion


        #region 窗体加载
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VswrSettingForm_Load(object sender, EventArgs e)
        {
            nudFreq.Maximum = Convert.ToDecimal(App_Settings.sgn_2.Max_Freq);
            nudFreq.Minimum = Convert.ToDecimal(App_Settings.sgn_1.Min_Freq);
            nudTx.Maximum = Convert.ToDecimal(App_Settings.sgn_1.Max_Power);
            nudTx.Minimum = Convert.ToDecimal(App_Settings.sgn_2.Min_Power);

            GetVswrSettings();

            if (App_Configure.Cnfgs.Spectrum == 0)
            {
                lblRbw.Text = "(KHz)";
                lblVbw.Text = "(KHz)";
            }
            else
            {
                lblRbw.Text = "(Hz)";
                lblVbw.Text = "(Hz)";
            }

            nudMinRls.ValueChanged += new EventHandler(nudMinRls_ValueChanged);
            nudMaxRls.ValueChanged += new EventHandler(nudMaxRls_ValueChanged);
            nudMinRls.Maximum = nudMaxRls.Value - 1;

            nudMinVsw.ValueChanged += new EventHandler(nudMinVsw_ValueChanged);
            nudMaxVsw.ValueChanged += new EventHandler(nudMaxVsw_ValueChanged);
            nudMinVsw.Maximum = nudMaxVsw.Value - 1;
        }

        #endregion


        #region 按钮事件

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckConfig())
            {
                SetVswrSettings();
                MessageBox.Show(this, "OK!");
                this.DialogResult = DialogResult.OK;
            }
        }

        #endregion

        #region 取消
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Calibration
        /// <summary>
        /// Calibration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCal_Click(object sender, EventArgs e)
        {
            FormCalProgress frmCal = new FormCalProgress(this.settings);
            if (cbxCarrier.SelectedIndex == 0)
            {
                frmCal.Rf_num = RFInvolved.Rf_1;
            }
            else
            {
                frmCal.Rf_num = RFInvolved.Rf_2;
            }
            if (frmCal.ShowDialog() == DialogResult.OK)
            {
                if (cbxCarrier.SelectedIndex == 0)
                {
                    listCurrentCAL_1 = frmCal.ListCurrentCAL_1;
                    bEnableCAL_RF1 = true;

                    FileStream fs = new FileStream("c:\\vswr_cal_carrier1.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("Carrier1");
                    for (int i = 0; i < listCurrentCAL_1.Count; i++)
                    {
                        sw.WriteLine("Freq: " + listCurrentCAL_1[i].Freq.ToString() + "MHz, RL0: " + listCurrentCAL_1[i].RL0.ToString() + "dB, TX0: "
                                    + listCurrentCAL_1[i].Tx0.ToString() + "dBm, RX0: " + listCurrentCAL_1[i].Rx0.ToString() + "dBm");
                    }
                    sw.WriteLine();
                    sw.Close();
                    fs.Close();
                }
                else
                {
                    listCurrentCAL_2 = frmCal.ListCurrentCAL_2;
                    bEnableCAL_RF2 = true;

                    FileStream fs = new FileStream("c:\\vswr_cal_carrier2.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("Carrier2");
                    for (int i = 0; i < listCurrentCAL_2.Count; i++)
                    {
                        sw.WriteLine("Freq: " + listCurrentCAL_2[i].Freq.ToString() + "MHz, RL0: " + listCurrentCAL_2[i].RL0.ToString() + "dB, TX0: "
                                    + listCurrentCAL_2[i].Tx0.ToString() + "dBm, RX0: " + listCurrentCAL_2[i].Rx0.ToString() + "dBm");
                    }
                    sw.WriteLine();
                    sw.Close();
                    fs.Close();
                }
            }
            else
            {
                if (cbxCarrier.SelectedIndex == 0)
                {
                    bEnableCAL_RF1 = false;
                }
                else
                {
                    bEnableCAL_RF2 = false;
                }
                MessageBox.Show(this, "Automatic calibration failed!");
            }
        }

        #endregion

        #region Defalult
        /// <summary>
        /// Defalult
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDefault_Click(object sender, EventArgs e)
        {
            App_Settings.vsw.Clone(this.settings);
            GetVswrSettings();
        }

        #endregion

        #region Save as
        /// <summary>
        /// Save as
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            IsoSaveSettingFileForm ssfm = new IsoSaveSettingFileForm();

            if (ssfm.ShowDialog() == DialogResult.OK)
            {
                //App_Configure.Cnfgs.File_Usr_Vsw = ssfm.FileName;

                this.settings.Save2File(App_Configure.Cnfgs.Path_Def + "\\Settings_Vsw.ini",
                                       App_Configure.Cnfgs.Path_Usr_Vsw + "\\" + ssfm.FileName);
            }

            ssfm.Dispose();
        }

        #endregion

        #region Load
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            IsoReadSettingFileForm rsfm = new IsoReadSettingFileForm();

            rsfm.FillFiles(App_Configure.Cnfgs.Path_Usr_Vsw);

            if (rsfm.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(App_Configure.Cnfgs.Path_Usr_Vsw + "\\" + rsfm.FileName))
                {
                    //App_Configure.Cnfgs.File_Usr_Vsw = rsfm.FileName;

                    Settings_Vsw vsw = new Settings_Vsw(App_Configure.Cnfgs.Path_Usr_Vsw + "\\" +
                                                        rsfm.FileName);

                    vsw.LoadSettings();

                    vsw.Clone(this.settings);

                    GetVswrSettings();
                }
            }

            rsfm.Dispose();
        }

        #endregion

        #endregion


        #region 其他事件

        private void nudMinRls_ValueChanged(object sender, EventArgs e)
        {
            if (nudMinRls.Value < nudMaxRls.Value)
                nudMaxRls.Minimum = nudMinRls.Value + 1;
            else
                nudMinRls.Maximum = nudMaxRls.Value - 1;
        }

        private void nudMaxRls_ValueChanged(object sender, EventArgs e)
        {
            if (nudMaxRls.Value > nudMinRls.Value)
                nudMinRls.Maximum = nudMaxRls.Value - 1;
            else
                nudMaxRls.Minimum = nudMinRls.Value + 1;
        }

        void nudMinVsw_ValueChanged(object sender, EventArgs e)
        {
            if (nudMinVsw.Value < nudMaxVsw.Value)
                nudMaxVsw.Minimum = nudMinVsw.Value + 1;
            else
                nudMinVsw.Maximum = nudMaxVsw.Value - 1;
        }

        void nudMaxVsw_ValueChanged(object sender, EventArgs e)
        {
            if (nudMaxVsw.Value > nudMinVsw.Value)
                nudMinVsw.Maximum = nudMaxVsw.Value - 1;
            else
                nudMaxVsw.Minimum = nudMinVsw.Value + 1;
        }

        #endregion


        #region  辅助方法

        #region 获取VSWR对象值
        /// <summary>
        /// 获取VSWR对象值
        /// </summary>
        /// <param name="getVswr"></param>
        private void GetVswrSettings()
        {
            try
            {
                nudFreq.Value = Convert.ToDecimal(settings.F);
                nudFreqStep.Value = Convert.ToDecimal(this.settings.Freq_Step);
                nudLimit.Value = Convert.ToDecimal(this.settings.Limit_Vsw);
                nudVbw.Value = Convert.ToDecimal(this.settings.Vbw_Spc);
                nudRbw.Value = Convert.ToDecimal(this.settings.Rbw_Spc);
                nudTx.Value = Convert.ToDecimal(this.settings.Tx);
                nudVbw.Value = Convert.ToDecimal(this.settings.Vbw_Spc);
                nudMaxVsw.Value = Convert.ToDecimal(this.settings.Max_Vsw);
                nudMinVsw.Value = Convert.ToDecimal(this.settings.Min_Vsw);
                nudAtt.Value = Convert.ToDecimal(this.settings.Att_Spc);
                nudMaxRls.Value = Convert.ToDecimal(this.settings.Max_Rls);
                nudMinRls.Value = Convert.ToDecimal(this.settings.Min_Rls);
                nudCount.Value = Convert.ToDecimal(this.settings.Count);

                numericUpDownAttor.Value = Convert.ToDecimal(this.settings.Attenuator);
                numericUpDownOffset.Value = Convert.ToDecimal(this.settings.Offset);
            }
            catch { }
        }

        #endregion

        #region 设置VSWR对象值
        /// <summary>
        /// 设置VSWR对象值
        /// </summary>
        private void SetVswrSettings()
        {
            this.settings.F = Convert.ToSingle(nudFreq.Value);
            this.settings.Freq_Step = Convert.ToSingle(nudFreqStep.Value);
            this.settings.Limit_Vsw = Convert.ToSingle(nudLimit.Value);
            this.settings.Count = Convert.ToInt32(nudCount.Value);
            this.settings.Rbw_Spc = Convert.ToInt32(nudRbw.Value);
            this.settings.Tx = Convert.ToSingle(nudTx.Value);
            this.settings.Vbw_Spc = Convert.ToInt32(nudVbw.Value);
            this.settings.Max_Vsw = Convert.ToSingle(nudMaxVsw.Value);
            this.settings.Min_Vsw = Convert.ToSingle(nudMinVsw.Value);
            this.settings.Att_Spc = Convert.ToInt32(nudAtt.Value);
            this.settings.Max_Rls = Convert.ToSingle(nudMaxRls.Value);
            this.settings.Min_Rls = Convert.ToSingle(nudMinRls.Value);
            this.settings.Attenuator = Convert.ToSingle(numericUpDownAttor.Value);
            this.settings.Offset = Convert.ToSingle(numericUpDownOffset.Value);
        }

        #endregion 

        #region VSWR设置校验
        /// <summary>
        /// VSWR设置校验
        /// </summary>
        /// <returns></returns>
        private bool CheckConfig()
        {
            bool rev = true;

            try
            {
                float F = Convert.ToSingle(nudFreq.Value);
                float Freq_Step = Convert.ToSingle(nudFreqStep.Value);
                float Limit_Vsw = Convert.ToSingle(nudLimit.Value);
                int Vbw_Spc = Convert.ToInt32(nudVbw.Value);
                int Rbw_Spc = Convert.ToInt32(nudRbw.Value);
                float Tx = Convert.ToSingle(nudTx.Value);
                float Max_Vsw = Convert.ToSingle(nudMaxVsw.Value);
                float Min_Vsw = Convert.ToSingle(nudMinVsw.Value);
                int Att_Spc = Convert.ToInt32(nudAtt.Value);
                float Max_Rls = Convert.ToSingle(nudMaxRls.Value);
                float Min_Rls = Convert.ToSingle(nudMinRls.Value);
                int Count = Convert.ToInt32(nudCount.Text.Trim());

                if (F < App_Settings.sgn_1.Min_Freq || F > App_Settings.sgn_2.Max_Freq)
                {
                    MessageBox.Show(this, "Frequency setup is out of its range!");
                    rev = false;
                }
                if (Tx < App_Settings.sgn_1.Min_Power || Tx > App_Settings.sgn_2.Max_Power)
                {
                    MessageBox.Show(this, "The carrier frequency setup is out of its range!");
                    rev = false;
                }
            }
            catch
            {
                rev = false;
                MessageBox.Show(this, "Save failed,param error!");
            }

            return rev;
        }

        #endregion

        #endregion


        #region TouchPad

        #region MouseDoubleClick
        private void nudValue_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            NumericUpDown number = (NumericUpDown)sender;
            TouchPad((NumericUpDown)sender, number.Value.ToString());
        }
        #endregion

        private void TouchPad(NumericUpDown n, string text)
        {
            TouchPad testTouchPad = new TouchPad(ref n, text);
            testTouchPad.ShowDialog();
        }

        #endregion

    }
}