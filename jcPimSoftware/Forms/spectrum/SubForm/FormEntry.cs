// ===============================================================================
// 文件名：FormEntry
// 创建人：倪骞
// 日  期：2011-4-29 
//
// 描  述：ENTRY快捷菜单子窗体
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

namespace jcPimSoftware
{
    public partial class FormEntry : Form
    {
        #region 变量定义

        /// <summary>
        /// 输入Entry频率
        /// </summary>
        private int _inputEntry = 0;
        public int InputEntry
        {
            set { _inputEntry = value; }
        }

        /// <summary>
        /// 输出Entry频率
        /// </summary>
        private int _outputEntry = 0;
        public int OutputEntry
        {
            get { return _outputEntry; }
        }

        /// <summary>
        /// 当前绘图最大频率
        /// </summary>
        private int _maxFreq = 0;
        public int MaxFreq
        {
            get { return _maxFreq; }
            set { _maxFreq = value; }
        }

        /// <summary>
        /// 当前绘图最小频率
        /// </summary>
        private int _minFreq = 0;
        public int MinFreq
        {
            get { return _minFreq; }
            set { _minFreq = value; }
        }


        #endregion


        #region 窗体构造
        /// <summary>
        /// 窗体构造
        /// </summary>
        public FormEntry()
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
        private void FormEntry_Load(object sender, EventArgs e)
        {
            double EntryFreq = _inputEntry / 1000.0;
            txtEntry.Text = EntryFreq.ToString("0.000");
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
                _outputEntry = Convert.ToInt32(double.Parse(txtEntry.Text.Trim()) * 1000);
                this.DialogResult = DialogResult.OK;
                this.Close();
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
                freq = double.Parse(txtEntry.Text.Trim());
                if (_minFreq < freq / 1000.0 || freq > _maxFreq / 1000.0)
                {
                    rev = false;
                }
            }
            catch
            {
                MessageBox.Show(this, "Frequency setup is out of its range!");
                rev = false;
            }

            return rev;
        }

        #endregion

        #endregion


        #region TouchPad

        private void txtEntry_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref txtEntry, txtEntry.Text.Trim());
            testTouchPad.ShowDialog();
        }

        #endregion 
    }
}