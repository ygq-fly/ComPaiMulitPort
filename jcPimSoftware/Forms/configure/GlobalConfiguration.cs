// ===============================================================================
// �ļ�����GlobalConfiguration
// �����ˣ����
// ��  �ڣ�2011-6-7
//
// ��  ����CONFIGģ��
//         
//
// ��  ���� 1.0.0.0
//
// ���¼�¼ 
// ===============================================================================
// ʱ  �䣺 2011-6-7   	   �������ļ�
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
        #region ���幹��
        /// <summary>
        /// ���幹��
        /// </summary>
        public GlobalConfiguration()
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
        private void GlobalConfiguration_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        #endregion

        #endregion

        
        #region ��ť�¼�

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


        #region ����

        #region  �������ò���
        /// <summary>
        /// �������ò���
        /// </summary>
        private void LoadConfig()
        {
            numericUpDownVswr.Value = (decimal)App_Configure.Cnfgs.Max_Vswr;
            numericUpDownTemp.Value = (decimal)App_Configure.Cnfgs.Max_Temp;

            //խREV
            if (App_Configure.Cnfgs.Channel == 0)
            {
                //����REV
                //GPIO.Rev();

                radioNarrow.Checked = true;
                radioBroad.Checked = false;
            }
            else
            {
                //����FWD
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

            //��ط���
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

        #region  ���浱ǰ����
        /// <summary>
        /// ���浱ǰ����
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
                //����խ��
                GPIO.Rev();

            }
            if (radioBroad.Checked)
            {
                //���ÿ��
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