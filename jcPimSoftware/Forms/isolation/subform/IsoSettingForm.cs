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
    public partial class IsoSettingForm : Form
    {
        #region 实例变量
        /// <summary>
        /// 隔离度功能模块的配置项
        /// </summary>
        private Settings_Iso settings;

        /// <summary>
        /// 隔离度扫描例程对象
        /// </summary>
        private Iso_Sweep SweepObj;

        /// <summary>
        /// 功放一校准标识
        /// </summary>
        public static bool bEnableCAL_RF1 = false;

        /// <summary>
        /// 功放二校准标识
        /// </summary>
        public static bool bEnableCAL_RF2 = false;

        #endregion


        #region 构造函数
        internal IsoSettingForm(Iso_Sweep SweepObj, Settings_Iso settings)
        {
            this.SweepObj = SweepObj;

            this.settings = settings;

            InitializeComponent();
        }
        #endregion 


        #region 窗体加载

        private void IsoSettingForm_Load(object sender, EventArgs e)
        {
            nudFrq.Maximum = Convert.ToDecimal(App_Settings.sgn_2.Max_Freq);
            nudFrq.Minimum = Convert.ToDecimal(App_Settings.sgn_1.Min_Freq);
            nudTx.Maximum = Convert.ToDecimal(App_Settings.sgn_1.Max_Power);
            nudTx.Minimum = Convert.ToDecimal(App_Settings.sgn_2.Min_Power);

            cbxCarrier.SelectedIndex = 0;

            GetIsoSettings();

            nudMaxIso.ValueChanged += new EventHandler(nudMaxIso_ValueChanged);
            nudMinIso.ValueChanged += new EventHandler(nudMinIso_ValueChanged);
            nudMinIso.Maximum = nudMaxIso.Value - 1;
        }

        #endregion


        #region 按钮事件
        private void btnCal_Click(object sender, EventArgs e)
        {
            bool power_too_large = false;

            IsoCALForm calform = new IsoCALForm(this.SweepObj, this.settings);

            if (cbxCarrier.SelectedIndex == 0)
                power_too_large = calform.Do_CAL(RFInvolved.Rf_1);
            else
                power_too_large = calform.Do_CAL(RFInvolved.Rf_2);

            if (!power_too_large)
            {
                if (calform.ShowDialog() == DialogResult.OK)
                {
                    if (cbxCarrier.SelectedIndex == 0)
                        bEnableCAL_RF1 = true;
                    else
                        bEnableCAL_RF2 = true;
                }
                else
                {
                    if (cbxCarrier.SelectedIndex == 0)
                        bEnableCAL_RF1 = false;
                    else
                        bEnableCAL_RF2 = false;
                }
            }
                
            calform.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SetIsoSettings();

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            IsoSaveSettingFileForm ssfm = new IsoSaveSettingFileForm();

            if (ssfm.ShowDialog() == DialogResult.OK)
            {
                //App_Configure.Cnfgs.File_Usr_Iso = ssfm.FileName;

                this.settings.Save2File(App_Configure.Cnfgs.Path_Def + "\\Settings_Iso.ini",
                                        App_Configure.Cnfgs.Path_Usr_Iso + "\\" + ssfm.FileName);               
            }

            ssfm.Dispose();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            App_Settings.iso.Clone(this.settings);

            GetIsoSettings();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            IsoReadSettingFileForm rsfm = new IsoReadSettingFileForm();

            rsfm.FillFiles(App_Configure.Cnfgs.Path_Usr_Iso);

            if (rsfm.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(App_Configure.Cnfgs.Path_Usr_Iso + "\\" + rsfm.FileName))
                {
                    //App_Configure.Cnfgs.File_Usr_Iso = rsfm.FileName;

                    Settings_Iso iso = new Settings_Iso(App_Configure.Cnfgs.Path_Usr_Iso + "\\" +
                                                        rsfm.FileName);

                    iso.LoadSettings();

                    iso.Clone(this.settings);

                    GetIsoSettings();
                }
            }

            rsfm.Dispose();
        }
        #endregion 


        #region 其他事件

        void nudMinIso_ValueChanged(object sender, EventArgs e)
        {
            if (nudMinIso.Value < nudMaxIso.Value)
                nudMaxIso.Minimum = nudMinIso.Value + 1;
            else
                nudMinIso.Maximum = nudMaxIso.Value - 1;
        }

        void nudMaxIso_ValueChanged(object sender, EventArgs e)
        {
            if (nudMaxIso.Value > nudMinIso.Value)
                nudMinIso.Maximum = nudMaxIso.Value - 1;
            else
                nudMaxIso.Minimum = nudMinIso.Value + 1;
        }

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


        #region 实例函数
        /// <summary>
        ///  将settings对象中的值填充到设置界面
        /// </summary>
        /// <param name="getIso"></param>
        private void GetIsoSettings()
        {
            try
            {
                nudFrq.Value = Convert.ToDecimal(settings.F);
                nudTx.Value = Convert.ToDecimal(settings.Tx);
                nudLimit.Value = Convert.ToDecimal(settings.Limit);
                nudAtt.Value = Convert.ToDecimal(settings.Att_Spc);
                nudTimePoints.Value = Convert.ToDecimal(settings.Time_Points);
                nudFreqStep.Value = Convert.ToDecimal(settings.Freq_Step);
                nudMinIso.Value = Convert.ToDecimal(settings.Min_Iso);
                nudMaxIso.Value = Convert.ToDecimal(settings.Max_Iso);
            }
            catch { }
        }

        /// <summary>
        /// 将设置界面的值保存到settings对象
        /// </summary>
        /// <param name="setIso"></param>
        private void SetIsoSettings()
        {
            float value = 0.0f;

            value = Convert.ToSingle(nudFrq.Value);

            if (((value >= App_Settings.sgn_1.Min_Freq) && (value <= App_Settings.sgn_1.Max_Freq)) ||

                ((value >= App_Settings.sgn_2.Min_Freq) && (value <= App_Settings.sgn_2.Max_Freq)))
            {
                settings.F = value;
            }

            value = Convert.ToSingle(nudTx.Value);

            if ((value >= App_Settings.sgn_1.Min_Power) &&
                (value <= App_Settings.sgn_1.Max_Power))
            {
                settings.Tx = value;
            }

            settings.Time_Points = Convert.ToInt32(nudTimePoints.Value);
            settings.Freq_Step = Convert.ToSingle(nudFreqStep.Value);
            settings.Limit = Convert.ToSingle(nudLimit.Value);
            settings.Att_Spc = Convert.ToInt32(nudAtt.Value);
            settings.Max_Iso = Convert.ToSingle(nudMaxIso.Value);
            settings.Min_Iso = Convert.ToSingle(nudMinIso.Value);
        }
        #endregion       
    }

}