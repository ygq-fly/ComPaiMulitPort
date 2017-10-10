// ===============================================================================
// 文件名：FormFreq
// 创建人：倪骞
// 日  期：2011-4-29 
//
// 描  述：FREQ快捷菜单子窗体
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
    public partial class FormFreq : Form
    {
        #region 变量定义

        /// <summary>
        /// 0起始频率，1结束频率，2中心频率
        /// </summary>
        private int _intType = 0;
        public int intType
        {
            get { return _intType; }
            set { _intType = value; }
        }

        /// <summary>
        /// 当前传入频率
        /// </summary>
        private int _intCurrentFreq = 0;
        public int IntCurrentFreq
        {
            set { _intCurrentFreq = value; }
        }
    
        /// <summary>
        /// 起始频率
        /// </summary>
        private int _intStartFreq = 0;
        public int IntStartFreq
        {
            get { return _intStartFreq; }
        }

        /// <summary>
        /// 结束频率
        /// </summary>
        private int _intEndFreq = 0;
        public int IntEndFreq
        {
            get { return _intEndFreq; }
        }

        /// <summary>
        /// 中心频率
        /// </summary>
        private int _intCenterFreq = 0;
        public int IntCenterFreq
        {
            get { return _intCenterFreq; }
        }

        #endregion


        #region 窗体构造
        /// <summary>
        /// 窗体构造
        /// </summary>
        public FormFreq()
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
        private void FormFreq_Load(object sender, EventArgs e)
        {
            double doubleCurrentFreq = _intCurrentFreq / 1000.0;
            switch (_intType)
            {
                case 0:
                    this.Text = "StartFreq";
                    txtFreq.Text = doubleCurrentFreq.ToString("0.000");
                    break;
                case 1:
                    this.Text = "EndFreq";
                    txtFreq.Text = doubleCurrentFreq.ToString("0.000");
                    break;
                case 2:
                    this.Text = "CenterFreq";
                    txtFreq.Text = doubleCurrentFreq.ToString("0.000");
                    break;
                default:
                    _intType = 0;
                    this.Text = "StartFreq";
                    txtFreq.Text = doubleCurrentFreq.ToString("0.000");
                    break;
            }
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
                double freq = double.Parse(txtFreq.Text.Trim());
                switch (_intType)
                {
                    case 0:
                        _intStartFreq = (int)(freq * 1000);
                        break;
                    case 1:
                        _intEndFreq = (int)(freq * 1000);
                        break;
                    case 2:
                        _intCenterFreq = (int)(freq * 1000);
                        break;
                    default:
                        _intStartFreq = (int)(freq * 1000);
                        break;
                }
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
                freq = double.Parse(txtFreq.Text.Trim());
                if (freq < 0 || freq > 3000)
                {
                    MessageBox.Show(this,"Frequency setup is out of its range!");
                    rev = false;
                }
            }
            catch
            {
                MessageBox.Show(this,"Frequency setup error!");
                rev = false;
            }

            return rev;
        }

        #endregion

        #endregion


        #region TouchPad

        private void txtFreq_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref txtFreq, txtFreq.Text.Trim());
            testTouchPad.ShowDialog();
        }

        #endregion 
    }
}