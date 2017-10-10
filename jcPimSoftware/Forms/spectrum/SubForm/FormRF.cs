// ===============================================================================
// 文件名：FormRF
// 创建人：倪骞
// 日  期：2011-6-22 
//
// 描  述：FormRF快捷菜单子窗体
//         
//
// 版  本： 1.0.0.0
//
// 更新记录 
// ===============================================================================
// 时  间： 2011-6-22   	   创建该文件
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
    public partial class FormRF : Form
    {
        #region 变量定义

        /// <summary>
        /// 功放编号
        /// </summary>
        private int _num = 1;

        /// <summary>
        /// RF1频率
        /// </summary>
        public static float FreqRF_1 = App_Settings.sgn_1.Min_Freq;

        /// <summary>
        /// RF1功率
        /// </summary>
        public static float TxRF_1 = 30;

        /// <summary>
        /// RF1启用标识
        /// </summary>
        public static bool EnableRF_1 = false;

        /// <summary>
        /// RF2频率
        /// </summary>
        public static float FreqRF_2 = App_Settings.sgn_2.Min_Freq;

        /// <summary>
        /// RF2功率
        /// </summary>
        public static float TxRF_2 = 30;

        /// <summary>
        /// RF2启用标识
        /// </summary>
        public static bool EnableRF_2 = false;

        #endregion


        #region 窗体构造
        /// <summary>
        /// 窗体构造
        /// </summary>
        public FormRF(int num)
        {
            InitializeComponent();

            _num = num;
        }

        #endregion


        #region 窗体加载
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormRF_Load(object sender, EventArgs e)
        {
            if (_num == 1)
            {
                groupBox1.Text = "Channel: RF_1";
                numericUpDownFreq.Maximum = (decimal)App_Settings.sgn_1.Max_Freq;
                numericUpDownFreq.Minimum = (decimal)App_Settings.sgn_1.Min_Freq;
                numericUpDownTx.Maximum = (decimal)App_Settings.sgn_1.Max_Power;
                numericUpDownTx.Minimum = (decimal)App_Settings.sgn_1.Min_Power;

                numericUpDownFreq.Value = (decimal)FreqRF_1;
                numericUpDownTx.Value = (decimal)TxRF_1;
                chkEnable.Checked = EnableRF_1;
            }
            else
            {
                groupBox1.Text = "Channe2: RF_2";
                numericUpDownFreq.Maximum = (decimal)App_Settings.sgn_2.Max_Freq;
                numericUpDownFreq.Minimum = (decimal)App_Settings.sgn_2.Min_Freq;
                numericUpDownTx.Maximum = (decimal)App_Settings.sgn_2.Max_Power;
                numericUpDownTx.Minimum = (decimal)App_Settings.sgn_2.Min_Power;

                numericUpDownFreq.Value = (decimal)FreqRF_2;
                numericUpDownTx.Value = (decimal)TxRF_2;
                chkEnable.Checked = EnableRF_2;
            }
        }

        #endregion


        #region 按钮事件

        #region O K
        /// <summary>
        /// O K
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_num == 1)
            {
                FreqRF_1 = (float)numericUpDownFreq.Value;
                TxRF_1 = (float)numericUpDownTx.Value;
                EnableRF_1 = chkEnable.Checked;
            }
            else
            {
                FreqRF_2 = (float)numericUpDownFreq.Value;
                TxRF_2 = (float)numericUpDownTx.Value;
                EnableRF_2 = chkEnable.Checked;
            }

            this.Close();
        }

        #endregion

        #endregion


        #region TouchPad

        private void numericUpDownFreq_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref numericUpDownFreq, numericUpDownFreq.Value.ToString().Trim());
            testTouchPad.ShowDialog();
        }

        private void numericUpDownTx_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref numericUpDownTx, numericUpDownTx.Value.ToString().Trim());
            testTouchPad.ShowDialog();
        }

        #endregion
    }
}