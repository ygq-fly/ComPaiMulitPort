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
    public partial class ReadSettingFiles : Form
    {

        //欲加载文件的名称
        private string _FileName;
        internal string FileName
        {
            get { return _FileName; }
        }

        public ReadSettingFiles()
        {
            InitializeComponent();
        }


        #region 将用户配置文件夹下的所有文件填充到列表
        /// <summary>
        /// 将用户配置文件夹下的所有文件填充到列表
        /// </summary>
        /// <param name="path">路径参数</param>
        internal void FillFiles(string path)
        {
            if (!Directory.Exists(path))
                return;

            DirectoryInfo info = new DirectoryInfo(path);
            FileSystemInfo[] fs = info.GetFileSystemInfos();
            
            lbxFiles.SuspendLayout();

            lbxFiles.Items.Clear();

            for (int i = 0; i < fs.Length; i++)
            {
                if (fs[i].Extension.ToLower() == ".ini")
                    lbxFiles.Items.Add(fs[i].Name);
            }

            lbxFiles.ResumeLayout(true);
        }
        #endregion

        #region 按钮事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lbxFiles.SelectedItem != null)  
                _FileName = lbxFiles.SelectedItem.ToString();

            this.Close();
        }

        #endregion
    }
}