// ===============================================================================
// 文件名：FormVswrFreq
// 创建人：倪骞
// 日  期：2011-5-19 
//
// 描  述：FREQ快捷菜单子窗体
//         
//
// 版  本： 1.0.0.0
//
// 更新记录 
// ===============================================================================
// 时  间： 2011-5-19    	   创建该文件
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
    public partial class FormVswrFreq : Form
    {
        #region 变量定义

        /// <summary>
        /// 驻波配置对象
        /// </summary>
        private Settings_Vsw settings;

        /// <summary>
        /// 驻波分析频率(MHz)
        /// </summary>
        private float _vswrFreq = 935;
        public float VswrFreq
        {
            get { return _vswrFreq; }
            set { _vswrFreq = value; }
        }

        #endregion


        #region 窗体构造
        /// <summary>
        /// 窗体构造
        /// </summary>
        internal FormVswrFreq(Settings_Vsw vsw_settings)
        {
            InitializeComponent();
            this.settings = vsw_settings;
        }

        #endregion


        #region 窗体事件

        #region 窗体加载
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormVswrFreq_Load(object sender, EventArgs e)
        {
            txtFreq.Text = settings.F.ToString("0.000");
        }

        #endregion

        #endregion


        #region 按钮事件

        #region 确定
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                _vswrFreq = float.Parse(txtFreq.Text.Trim());
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

        #region TouchPad

        private void txtFreq_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref txtFreq, txtFreq.Text.Trim());
            testTouchPad.ShowDialog();
        }

        #endregion

        #endregion


        #region 方法

        #region 输入校验
        /// <summary>
        /// 输入校验
        /// </summary>
        /// <returns>true成功 false失败</returns>
        private bool CheckInput()
        {
            bool rev = true;
            double freq = 0;

            try
            {
                freq = float.Parse(txtFreq.Text.Trim());
                if (freq < App_Settings.sgn_1.Min_Freq || freq > App_Settings.sgn_2.Max_Freq)
                {
                    MessageBox.Show(this, "Frequency setup is out of its range!");
                    rev = false;
                }
            }
            catch
            {
                MessageBox.Show(this, "Frequency setup error!");
                rev = false;
            }

            return rev;
        }

        #endregion

        #endregion
    }
}