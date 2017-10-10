// ===============================================================================
// 文件名：FormAttOther
// 创建人：倪骞
// 日  期：2011-4-29 
//
// 描  述：ATT快捷菜单子窗体
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
    public partial class FormAttOther : Form
    {
        #region 变量定义

        /// <summary>
        /// 传入ATT
        /// </summary>
        private int _inputAtt = 0;
        public int InputAtt
        {
            set { _inputAtt = value - 40; }
        }

        /// <summary>
        /// 传出ATT
        /// </summary>
        private int _outputAtt = 0;
        public int OutputAtt
        {
            get { return _outputAtt + 40; }
        }

        #endregion


        #region 窗体构造
        /// <summary>
        /// 窗体构造
        /// </summary>
        public FormAttOther()
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
        private void FormAttOther_Load(object sender, EventArgs e)
        {
            txtAtt.Text = _inputAtt.ToString();
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
            double double_att = 0;
            int int_att = 0;
            if (CheckInput())
            {
                double_att = double.Parse(txtAtt.Text.Trim());
                int_att = (int)Math.Floor(double_att);

                int_att = int_att / 2 * 2;

                if (int_att < -40)
                {
                    int_att = -40;
                }
                if (int_att > 20)
                {
                    int_att = 20;
                }

                _outputAtt = int_att;

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
            double att = 0;

            try
            {
                att = double.Parse(txtAtt.Text.Trim());
            }
            catch
            {
                MessageBox.Show(this, "ATT setup error!");
                rev = false;
            }

            return rev;
        }

        #endregion

        #endregion


        #region TouchPad

        private void txtAtt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref txtAtt, txtAtt.Text.Trim());
            testTouchPad.ShowDialog();
        }

        #endregion 
    }
}