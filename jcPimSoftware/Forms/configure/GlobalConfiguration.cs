// ===============================================================================
// 文件名：GlobalConfiguration
// 创建人：倪骞
// 日  期：2011-6-7
//
// 描  述：CONFIG模块
//         
//
// 版  本： 1.0.0.0
//
// 更新记录 
// ===============================================================================
// 时  间： 2011-6-7   	   创建该文件
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
    public partial class GlobalConfiguration : Form
    {
        #region 窗体构造
        /// <summary>
        /// 窗体构造
        /// </summary>
        public GlobalConfiguration()
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
        private void GlobalConfiguration_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        #endregion

        #endregion

        
        #region 按钮事件

        #region Test Mode

        private void btnTest_Click(object sender, EventArgs e)
        {
            //this.Close();
            TestForm tf = new TestForm();
            tf.ShowDialog();
        }

        #endregion

        #region Compensation

        private void btnCompensation_Click(object sender, EventArgs e)
        {
            //this.Close();
            Config cf = new Config();
            cf.ShowDialog();
        }

        #endregion

        #region Parameters

        private void btnParameters_Click(object sender, EventArgs e)
        {
            //this.Close();
            SpecificsForm sf = new SpecificsForm();
            sf.ShowDialog();
        }

        #endregion

        #region File Manage

        private void btnFileManage_Click(object sender, EventArgs e)
        {
            //this.Close();
            FileManager fm = new FileManager();
            fm.LoadAllData(App_Configure.Cnfgs.Path_Rpt_Pim + "\\");
            fm.ShowDialog();
        }

        #endregion

        #region Save

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveConfig();
            MessageBox.Show(this,"OK!");
            this.Close();
        }

        #endregion

        #region Cancel

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #endregion


        #region TouchPad

        private void numericUpDownVswr_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad tp = new TouchPad(ref numericUpDownVswr, numericUpDownVswr.Value.ToString());
            tp.ShowDialog();
        }

        private void numericUpDownTemp_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad tp = new TouchPad(ref numericUpDownTemp, numericUpDownTemp.Value.ToString());
            tp.ShowDialog();
        }

        #endregion


        #region 方法

        #region  加载配置参数
        /// <summary>
        /// 加载配置参数
        /// </summary>
        private void LoadConfig()
        {
            numericUpDownVswr.Value = (decimal)App_Configure.Cnfgs.Max_Vswr;
            numericUpDownTemp.Value = (decimal)App_Configure.Cnfgs.Max_Temp;

            //窄REV
            if (App_Configure.Cnfgs.Channel == 0)
            {
                //设置REV
                //GPIO.Rev();

                radioNarrow.Checked = true;
                radioBroad.Checked = false;
            }
            else
            {
                //设置FWD
                //GPIO.Fwd();

                radioNarrow.Checked = false;
                radioBroad.Checked = true;
            }

            //GPIO
            if (App_Configure.Cnfgs.Gpio == 0)
            {
                radio_gpio_old.Checked = true;
                radio_gpio_new.Checked = false;
            }
            else
            {
                radio_gpio_old.Checked = false;
                radio_gpio_new.Checked = true;
            }

            //电池服务
            if (App_Configure.Cnfgs.Battery == 0)
            {
                chk_battary.Checked = false;
            }
            else
            {
                chk_battary.Checked = true;
            }

            if (App_Configure.Cnfgs.EnableSuperConfig == 0)
            {
                btnTest.Visible = false;
                btnCompensation.Visible = false;
            }
            else
            {
                btnTest.Visible = true;
                btnCompensation.Visible = true;
            }

            if (App_Configure.Cnfgs.Cal_Use_Table)
                btnCompensation.Enabled = false;
            else
                btnCompensation.Enabled = true;
        }

        #endregion

        #region  保存当前配置
        /// <summary>
        /// 保存当前配置
        /// </summary>
        private void SaveConfig()
        {
            try
            {
                App_Configure.Cnfgs.Max_Vswr = Convert.ToSingle(numericUpDownVswr.Value);
                App_Configure.Cnfgs.Max_Temp = Convert.ToSingle(numericUpDownTemp.Value);
            }
            catch
            {
                MessageBox.Show("VSWR Limit or Temp Limit is wrong!");
                return;
            }

            if (radio_gpio_old.Checked)
            {
                App_Configure.Cnfgs.Gpio = 0;
            }
            if (radio_gpio_new.Checked)
            {
                App_Configure.Cnfgs.Gpio = 1;
            }

            if (radioNarrow.Checked)
            {
                //设置窄带
                GPIO.Rev();

            }
            if (radioBroad.Checked)
            {
                //设置宽带
                GPIO.Fwd();

            }

            if (chk_battary.Checked)
            {
                App_Configure.Cnfgs.Battery = 1;
            }
            else
            {
                App_Configure.Cnfgs.Battery = 0;
            }
        }

        #endregion

        #endregion
    }
}