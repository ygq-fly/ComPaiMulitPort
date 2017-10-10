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
    public partial class HarSettingForm : Form
    {
        /// <summary>
        /// 隔离度功能模块的配置项
        /// </summary>
        private Settings_Har settings;

        /// <summary>
        /// 隔离度扫描例程对象
        /// </summary>
        private Har_Sweep SweepObj;

        internal HarSettingForm(Har_Sweep SweepObj, Settings_Har settings)
        {
            this.SweepObj = SweepObj;

            this.settings = settings;

            InitializeComponent();
        }

        #region 按钮事件
        private void IsoSettingForm_Load(object sender, EventArgs e)
        {
            nudFrq.Maximum = Convert.ToDecimal(App_Settings.sgn_2.Max_Freq);
            nudFrq.Minimum = Convert.ToDecimal(App_Settings.sgn_1.Min_Freq);
            nudTx.Maximum = Convert.ToDecimal(App_Settings.sgn_1.Max_Power);
            nudTx.Minimum = Convert.ToDecimal(App_Settings.sgn_2.Min_Power);
            GetIsoSettings();

            nudMaxHar.ValueChanged += new EventHandler(nudMaxHar_ValueChanged);
            nudMinHar.ValueChanged += new EventHandler(nudMinHar_ValueChanged);
            nudMinHar.Maximum = nudMaxHar.Value - 1;
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
            HarSaveSettingFileForm ssfm = new HarSaveSettingFileForm();

            if (ssfm.ShowDialog() == DialogResult.OK)
            {
               // App_Configure.Cnfgs.File_Usr_Har = ssfm.FileName;

                this.settings.Save2File(App_Configure.Cnfgs.Path_Def  + "\\Settings_Har.ini",
                                        App_Configure.Cnfgs.Path_Usr_Har + "\\" + ssfm.FileName);               
            }

            ssfm.Dispose();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            App_Settings.har.Clone(this.settings);

            GetIsoSettings();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            HarReadSettingFileForm rsfm = new HarReadSettingFileForm();

            rsfm.FillFiles(App_Configure.Cnfgs.Path_Usr_Har);

            if (rsfm.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(App_Configure.Cnfgs.Path_Usr_Har + "\\" + rsfm.FileName))
                {
                   // App_Configure.Cnfgs.File_Usr_Har = rsfm.FileName;

                    Settings_Har har = new Settings_Har(App_Configure.Cnfgs.Path_Usr_Har + "\\" +
                                                        rsfm.FileName);

                    har.LoadSettings();

                    har.Clone(this.settings);

                    GetIsoSettings();
                }
            }

            rsfm.Dispose();
        }
        #endregion 

        #region 其他事件

        void nudMinHar_ValueChanged(object sender, EventArgs e)
        {
            if (nudMinHar.Value < nudMaxHar.Value)
                nudMaxHar.Minimum = nudMinHar.Value + 1;
            else
                nudMinHar.Maximum = nudMaxHar.Value - 1;
        }

        void nudMaxHar_ValueChanged(object sender, EventArgs e)
        {
            if (nudMaxHar.Value > nudMinHar.Value)
                nudMinHar.Maximum = nudMaxHar.Value - 1;
            else
                nudMaxHar.Minimum = nudMinHar.Value + 1;
        }

        #endregion

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
                nudMinHar.Value = Convert.ToDecimal(settings.Min_Har);
                nudMaxHar.Value = Convert.ToDecimal(settings.Max_Har);
                numericUpDownRev.Value = Convert.ToDecimal(settings.Rev);
            }
            catch { }
        }

        /// <summary>
        /// 将设置界面的值保存到settings对象
        /// </summary>
        /// <param name="setIso"></param>
        private void SetIsoSettings()
        {
            //float value = 0.0f;

            settings.F = Convert.ToSingle(nudFrq.Value);

            //if (((value >= App_Settings.sgn_1.Min_Freq) && (value <= App_Settings.sgn_1.Max_Freq)) ||

            //   ((value >= App_Settings.sgn_2.Min_Freq) && (value <= App_Settings.sgn_2.Max_Freq)))
            //{ 
            //    settings.F = value;
            //}

            settings.Tx = Convert.ToSingle(nudTx.Value);

            //if ((value >= App_Settings.sgn_1.Min_Power) &&
            //    (value <= App_Settings.sgn_1.Max_Power))
            //{
            //    settings.Tx = value;
            //}

            settings.Time_Points = Convert.ToInt32(nudTimePoints.Value);
            settings.Freq_Step = Convert.ToSingle(nudFreqStep.Value);
            settings.Limit = Convert.ToSingle(nudLimit.Value);
            settings.Att_Spc = Convert.ToInt32(nudAtt.Value);
            settings.Max_Har = Convert.ToSingle(nudMaxHar.Value);
            settings.Min_Har = Convert.ToSingle(nudMinHar.Value);
            settings.Rev = Convert.ToSingle(numericUpDownRev.Value);
        }


        #region TouchPad
        private void TouchPad(NumericUpDown n, string text)
        {
            TouchPad testTouchPad = new TouchPad(ref n, text);
            testTouchPad.ShowDialog();
        }

        #region MouseDoubleClick
        private void nudValue_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            NumericUpDown number = (NumericUpDown)sender;
            TouchPad((NumericUpDown)sender, number.Value.ToString());
        }
        #endregion

        #endregion
    }

}