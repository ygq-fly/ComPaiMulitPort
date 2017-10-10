// ===============================================================================
// 文件名：FormVswrRead
// 创建人：倪骞
// 日  期：2011-5-19 
//
// 描  述：READ快捷菜单子窗体
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
    public partial class FormVswrRead : Form
    {
        #region 变量定义

        /// <summary>
        /// 文件路径
        /// </summary>
        private string _FilePath = "";
        public string FilePath
        {
            get { return _FilePath; }
        }

        #endregion


        #region 窗体构造
        /// <summary>
        /// 窗体构造
        /// </summary>
        public FormVswrRead()
        {
            InitializeComponent();
        }

        #endregion


        #region 按钮事件

        #region 浏览
        /// <summary>
        /// 浏览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = App_Configure.Cnfgs.Path_Rpt_Vsw;
            openFile.Filter = "CSV File(*.csv)|*.csv";

            if (openFile.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                txtFilePath.Text = openFile.FileName;
            }
        }

        #endregion

        #region 确定
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            _FilePath = txtFilePath.Text.Trim();
            this.DialogResult = DialogResult.OK;
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
    }
}