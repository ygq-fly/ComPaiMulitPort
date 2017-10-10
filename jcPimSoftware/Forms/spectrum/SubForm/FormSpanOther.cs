// ===============================================================================
// 文件名：FormSpanOther
// 创建人：倪骞
// 日  期：2011-4-29 
//
// 描  述：SPAN快捷菜单子窗体
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
    public partial class FormSpanOther : Form
    {
        #region 变量定义

        /// <summary>
        /// 当前扫描带宽
        /// </summary>
        private int _inputSpan = 0;
        public int InputSpan
        {
            set { _inputSpan = value; }
        }

        /// <summary>
        /// 新扫描带宽
        /// </summary>
        private int _outputSpan = 0;
        public int OutputSpan
        {
            get { return _outputSpan; }
        }

        #endregion


        #region 窗体构造
        /// <summary>
        /// 窗体构造
        /// </summary>
        public FormSpanOther()
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
        private void FormSpanOther_Load(object sender, EventArgs e)
        {
            double currentSpan = _inputSpan / 1000.0;
            txtSpan.Text = currentSpan.ToString("0.000");
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
                double Span = double.Parse(txtSpan.Text.Trim());
                _outputSpan = (int)(Span * 1000);
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
                freq = double.Parse(txtSpan.Text.Trim());
                if (freq < 0 || freq > 3000)
                {
                    MessageBox.Show(this, "Scanning band is out of its range!");
                    rev = false;
                }
            }
            catch
            {
                MessageBox.Show(this, "Scanning band setup error!");
                rev = false;
            }

            return rev;
        }

        #endregion

        private void txtSpan_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref txtSpan, txtSpan.Text.Trim());
            testTouchPad.ShowDialog();
        }

        #endregion

    }
}