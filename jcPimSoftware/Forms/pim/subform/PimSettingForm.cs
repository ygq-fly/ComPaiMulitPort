using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;


namespace jcPimSoftware
{
    public partial class PimSettingForm : Form
    {
        #region 构造函数

        /// <summary>
        /// 隔离度功能模块的配置项
        /// </summary>
        internal Settings_Pim settings;
        public List<int> selectPort = new List<int>();
        List<CheckBox> cb_lsit = new List<CheckBox>();

        internal PimSettingForm(Settings_Pim settings,List<int> list_)
        {
            this.settings = settings;
            selectPort = list_;
            InitializeComponent();
            gbxFrequency.Visible = false;
        }
        #endregion

        #region 窗体事件
        private void PimSettingForm_Load(object sender, EventArgs e)
        {
            cb_lsit.AddRange(new List<CheckBox>() { checkBox1, checkBox2, checkBox3, checkBox4, checkBox8, checkBox7, checkBox5, checkBox6 });

            cbxPimMode.Items.Add(SweepType.Freq_Sweep);
            cbxPimMode.Items.Add(SweepType.Time_Sweep);

            cbxPimOrder.Items.Add(ImOrder.Im3);
            cbxPimOrder.Items.Add(ImOrder.Im5);
            cbxPimOrder.Items.Add(ImOrder.Im7);
            cbxPimOrder.Items.Add(ImOrder.Im9);
         
            cbxPimSchema.Items.Add(ImSchema.REV);
            cbxPimSchema.Items.Add(ImSchema.FWD);

            cbxPimUnit.Items.Add(ImUint.dBm);
            cbxPimUnit.Items.Add(ImUint.dBc);
            nudTx.Maximum = Convert.ToDecimal(App_Settings.sgn_1.Max_Power);
            nudTx.Minimum = Convert.ToDecimal(App_Settings.sgn_1.Min_Power);

            GetPimValues();

            numericUpDown1.Value = Convert.ToDecimal(App_Settings.spc.RxRef);
            numericUpDown2.Value = Convert.ToDecimal(App_Settings.spc.TxRef);

            for (int i = 0; i < selectPort.Count; i++)
            {  
                cb_lsit[selectPort[i] - 1].Checked = true;
            }
            for (int i = 0; i <App_Configure.Cnfgs.Ms_switch_port_count; i++)
            {
                cb_lsit[i].Enabled = true;
                comboBox1.Items.Add("PORT" + (i + 1).ToString());
            }
            comboBox1.SelectedIndex = 0;
        }
        #endregion

        #region 获取PIM对象值
        /// <summary>
        /// 获取PIM对象值
        /// </summary>
        private void GetPimValues()
        {
            cbxPimMode.SelectedItem = settings.SweepType;
            cKxEs.Checked = settings.EnableSquence;
            nudTimesF.Value = Convert.ToDecimal(settings.FreqSweepTimes);
            nudPointsF.Value = Convert.ToDecimal(settings.FreqSweepPoints);
            nudTimesT.Value = Convert.ToDecimal(settings.TimeSweepTimes);
            nudPintsT.Value = Convert.ToDecimal(settings.TimeSweepPoints);
            nudPimLimit.Value = Convert.ToDecimal(settings.Limit_Pim);
            cbxPimOrder.SelectedItem = settings.PimOrder;
            cbxPimSchema.SelectedItem = settings.PimSchema;
            cbxPimUnit.SelectedItem = settings.PimUnit;
            nudTx.Value = Convert.ToDecimal(settings.Tx);
            nudAtt.Value = Convert.ToDecimal(settings.Att_Spc);
        }
        #endregion

        #region 设置PIM对象值
        /// <summary>
        /// 设置PIM对象值
        /// </summary>
        private void SetPimValues()
        {
            settings.SweepType = (SweepType)cbxPimMode.SelectedItem;
            bool flag = false;
            if (cKxEs.Checked)
                flag = true;
            settings.EnableSquence = flag;
            settings.FreqSweepTimes = Convert.ToInt32(nudTimesF.Value);
            settings.FreqSweepPoints = Convert.ToInt32(nudPointsF.Value);
            settings.TimeSweepTimes = Convert.ToInt32(nudTimesT.Value);
            settings.TimeSweepPoints = Convert.ToInt32(nudPintsT.Value);
            settings.Tx = Convert.ToSingle(nudTx.Value);
            settings.Tx2 = Convert.ToSingle(nudTx.Value);
            settings.Att_Spc = Convert.ToInt32(nudAtt.Value);
            settings.Limit_Pim = (float)nudPimLimit.Value;
            settings.PimOrder = (ImOrder)cbxPimOrder.SelectedItem;
            settings.PimSchema = (ImSchema)cbxPimSchema.SelectedItem;
            settings.PimUnit = (ImUint)cbxPimUnit.SelectedItem;

            selectPort.Clear();
            for (int i = 0; i < cb_lsit.Count; i++)
            {

                if (cb_lsit[i].Checked)
                    selectPort.Add(i + 1);
            }
        }
        #endregion

        #region 按钮事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            //SaveSettingFile ssfm = new SaveSettingFile();

            //if (ssfm.ShowDialog() == DialogResult.OK)
            //{
               // string File_Usr_Pim = ssfm.FileName;

                //this.settings.Save2File(App_Configure.Cnfgs.Path_Def + "\\Settings_Pim.ini",
                //                        App_Configure.Cnfgs.Path_Usr_Pim + "\\" + ssfm.FileName);

              
            //}

            //ssfm.Dispose();


            if (MessageBox.Show("是否确认保存？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                IniFile.SetFileName("D:\\settings\\Settings_Pim.ini");
                IniFile.SetString("pim", "mode", cbxPimMode.SelectedIndex.ToString());
                IniFile.SetString("pim", "order", cbxPimOrder.Text.Substring(2));
               IniFile.SetString("pim", "unit", cbxPimUnit.SelectedIndex.ToString());
               IniFile.SetString("pim", "limit", nudPimLimit.Value.ToString());
               IniFile.SetString("pim", "att_spc", nudAtt.Value.ToString());
               IniFile.SetString("pim", "tx", nudTx.Value.ToString());
               IniFile.SetString("pim", "schema", cbxPimSchema.SelectedIndex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            //App_Settings.pim.Clone(this.settings);
            //GetPimValues();

            //ygq
            IniFile.SetFileName(Application.StartupPath+"\\settings\\Settings_Pim.ini");
            cbxPimMode.SelectedIndex = int.Parse(IniFile.GetString("pim", "mode", "0"));
            int order = int.Parse(IniFile.GetString("pim", "order", "0"));
            switch (order)
            {
                case 3:
                    cbxPimOrder.SelectedIndex = 0; break;
                case 5:
                    cbxPimOrder.SelectedIndex = 1; break;
                case 7:
                    cbxPimOrder.SelectedIndex = 2; break;
                case 9:
                    cbxPimOrder.SelectedIndex = 3; break;
            }
            cbxPimUnit.SelectedIndex = int.Parse(IniFile.GetString("pim", "unit", "0"));
            nudPimLimit.Value = Convert.ToDecimal(IniFile.GetString("pim", "limit", "-110"));
            nudAtt.Value = Convert.ToDecimal(IniFile.GetString("pim", "att_spc", "0"));
            nudTx.Value = Convert.ToDecimal(IniFile.GetString("pim", "tx", "43"));
            cbxPimSchema.SelectedIndex = int.Parse(IniFile.GetString("pim", "schema", "0"));
            //
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //ReadSettingFiles rsfm = new ReadSettingFiles();

            //rsfm.FillFiles(App_Configure.Cnfgs.Path_Usr_Pim);

            //if (rsfm.ShowDialog() == DialogResult.OK)
            //{
            //    if (File.Exists(App_Configure.Cnfgs.Path_Usr_Pim + "\\" + rsfm.FileName))
            //    {
            //       // string File_Usr_Pim = rsfm.FileName;

            //        Settings_Pim pim = new Settings_Pim(App_Configure.Cnfgs.Path_Usr_Pim + "\\" +
            //                                            rsfm.FileName);

            //        pim.LoadSettings();

            //        pim.Clone(this.settings);

            //        GetPimValues();
            //    }
            //}

            //rsfm.Dispose();

            //ygq
             IniFile.SetFileName("D:\\settings\\Settings_Pim.ini");
            cbxPimMode.SelectedIndex=  int.Parse(IniFile.GetString("pim", "mode", "0"));
            int order=int.Parse( IniFile.GetString("pim", "order","0"));
            switch (order)
            { 
                case 3:
                    cbxPimOrder.SelectedIndex = 0; break;
                case 5:
                    cbxPimOrder.SelectedIndex = 1; break;
                case 7:
                    cbxPimOrder.SelectedIndex = 2; break;
                case 9:
                    cbxPimOrder.SelectedIndex = 3; break;
            }
           cbxPimUnit.SelectedIndex=int.Parse(IniFile.GetString("pim", "unit", "0"));
           nudPimLimit.Value=Convert.ToDecimal(IniFile.GetString("pim", "limit", "-110"));
           nudAtt.Value=Convert.ToDecimal(IniFile.GetString("pim", "att_spc", "0"));
           nudTx.Value = Convert.ToDecimal(IniFile.GetString("pim", "tx", "43"));
           cbxPimSchema.SelectedIndex=int.Parse( IniFile.GetString("pim", "schema", "0"));
            //
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SetPimValues();

            //IniFile.SetFileName("d:\\settings\\Settings_Spc.ini");
            //App_Settings.spc.RxRef = Convert.ToSingle(numericUpDown1.Value);
            //IniFile.SetString("spectrum", "rxRef", App_Settings.spc.RxRef.ToString());
            //App_Settings.spc.TxRef = Convert.ToSingle(numericUpDown2.Value);
            //IniFile.SetString("spectrum", "txRef", App_Settings.spc.TxRef.ToString());

            IniFile.SetFileName("d:\\settings\\Settings_Spc.ini");
            string rx_s="";
            string tx_s="";
            for (int i = 0; i < 8; i++)
            {
                if(i!=0)
                {
                    rx_s += ",";
                    tx_s += ",";
                }
                rx_s += App_Settings.spc.List_rxRef[i];
                tx_s += App_Settings.spc.List_txRef[i];
            }
            IniFile.SetString("spectrum", "rxRefTable", rx_s);
            IniFile.SetString("spectrum", "txRefTable", tx_s);
          
            this.DialogResult = DialogResult.OK;
        }

        private void nudValue_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            NumericUpDown number=(NumericUpDown)sender;
            TouchPad((NumericUpDown)sender, number.Value.ToString());
        }

        #endregion

        #region TouchPad
        /// <summary>
        /// TouchPad
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="text"></param>
        private void TouchPad(NumericUpDown n, string text)
        {
            TouchPad testTouchPad = new TouchPad(ref n, text);
            testTouchPad.ShowDialog();
        }
        #endregion

        private void numericUpDown1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            NumericUpDown number = (NumericUpDown)sender;
            TouchPad((NumericUpDown)sender, number.Value.ToString());
        }

        private void numericUpDown2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            NumericUpDown number = (NumericUpDown)sender;
            TouchPad((NumericUpDown)sender, number.Value.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            App_Settings.spc.List_rxRef[comboBox1.SelectedIndex] = numericUpDown1.Value.ToString();
            App_Settings.spc.List_txRef[comboBox1.SelectedIndex] = numericUpDown2.Value.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDown1.Value = Convert.ToDecimal(App_Settings.spc.List_rxRef[comboBox1.SelectedIndex]);
            numericUpDown2.Value = Convert.ToDecimal(App_Settings.spc.List_txRef[comboBox1.SelectedIndex]);
        }
    }
}

