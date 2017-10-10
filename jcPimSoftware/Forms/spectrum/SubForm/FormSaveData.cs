// ===============================================================================
// 文件名：FormSaveData
// 创建人：倪骞
// 日  期：2011-4-29 
//
// 描  述：DATASAVE快捷菜单子窗体
//         
//
// 版  本： 1.0.0.0
//
// 更新记录 
// ===============================================================================
// 时  间： 2011-5-18   	   创建该文件
//
// ===============================================================================



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace jcPimSoftware
{
    public partial class FormSaveData : Form
    {
        #region 变量定义

        /// <summary>
        /// 存放文件的根路径
        /// </summary>
        private string RootPath;

        /// <summary>
        /// CSV文件路径
        /// </summary>
        private string _csvFileName;
        public string CsvFileName
        {
            get { return _csvFileName; }
        }

        /// <summary>
        /// JPG文件路径
        /// </summary>
        private string _jpgFileName;
        public string JpgFileName
        {
            get { return _jpgFileName; }
        }

        /// <summary>
        /// 启用CSV保存
        /// </summary>
        private bool _bEnableCsv;
        public bool bEnableCsv
        {
            get { return _bEnableCsv; }
        }

        /// <summary>
        /// 启用JPG保存
        /// </summary>
        private bool _bEnableJpg;
        public bool bEnableJpg
        {
            get { return _bEnableJpg; }
        }

        #endregion


        #region 窗体构造
        /// <summary>
        /// 窗体构造
        /// </summary>
        public FormSaveData()
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
        private void FormSaveData_Load(object sender, EventArgs e)
        {
            RootPath = App_Configure.Cnfgs.Path_Rpt_Spc + "\\";

            chkCsv.Checked = Convert.ToBoolean(App_Configure.Cnfgs.Csv_checked);
            chkJpg.Checked = Convert.ToBoolean(App_Configure.Cnfgs.Jpg_checked);
            SaveDatas();

            DateTime dt_now=DateTime.Now;
            string strDate = dt_now.ToString("yyyy-MM-dd hh-mm-ss");
            _csvFileName = RootPath + "csv\\" + strDate + ".csv";
            _jpgFileName = RootPath + "jpg\\" + strDate + ".jpg";
            txtCsv.Text = strDate;
            txtJpg.Text = strDate;
            lblPath.Text = "文件路径:" + App_Configure.Cnfgs.Path_Rpt_Spc;
        }

        #endregion

        #endregion


        #region 事件

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            _bEnableCsv = chkCsv.Checked;
            _bEnableJpg = chkJpg.Checked;
            _csvFileName = RootPath + "csv\\" + txtCsv.Text.Trim() + ".csv";
            _jpgFileName = RootPath + "jpg\\" + txtJpg.Text.Trim() + ".jpg";
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

        #region CSV
        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCsv_CheckedChanged(object sender, EventArgs e)
        {
            SaveDatas();
        }

        #endregion

        #region JPG
        /// <summary>
        /// JPG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkJpg_CheckedChanged(object sender, EventArgs e)
        {
            SaveDatas();
        }

        #endregion

        private void SaveDatas()
        {
            if (chkJpg.Checked)
            {
                txtJpg.Enabled = true;
                _bEnableJpg = true;
            }
            else
            {
                txtJpg.Enabled = false;
                _bEnableJpg = false;
            }

            if (chkCsv.Checked)
            {
                txtCsv.Enabled = true;
                _bEnableCsv = true;
            }
            else
            {
                txtCsv.Enabled = false;
                _bEnableCsv = false;
            }
        }


        #endregion


        #region TouchPad

        private void txtCsv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref txtCsv, txtCsv.Text.Trim());
            testTouchPad.ShowDialog();
        }

        private void txtJpg_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad testTouchPad = new TouchPad(ref txtJpg, txtJpg.Text.Trim());
            testTouchPad.ShowDialog();

        }
        #endregion

        private void btn_Root_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                App_Configure.Cnfgs.Path_Rpt_Spc = fbd.SelectedPath + "\\spc";

                if (!Directory.Exists(App_Configure.Cnfgs.Path_Rpt_Spc))
                    Directory.CreateDirectory(App_Configure.Cnfgs.Path_Rpt_Spc);

                App_Configure.CreateReportSubFolder(App_Configure.Cnfgs.Path_Rpt_Spc);
                RootPath = App_Configure.Cnfgs.Path_Rpt_Spc + "\\";
                lblPath.Text = "文件路径:" + App_Configure.Cnfgs.Path_Rpt_Spc;
            }
            fbd.Dispose();
        }


        #region 调用方法



        #endregion
    }
}