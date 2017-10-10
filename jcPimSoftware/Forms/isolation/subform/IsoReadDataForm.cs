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
    public partial class IsoReadDataForm : Form
    {
        public IsoReadDataForm()
        {
            InitializeComponent();
        }

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
                if (fs[i].Extension.ToLower() == ".csv")
                    lbxFiles.Items.Add("[" + fs[i].Name + "] " + fs[i].CreationTime.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            lbxFiles.ResumeLayout(true);
            if (lbxFiles.Items.Count > 0)
                lbxFiles.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //欲加载文件的名称
        private string _FileName;
        internal string FileName
        {
            get { return _FileName; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string s;

            int iStart, iEnd;

            if (lbxFiles.SelectedItem != null)
            {
                s = lbxFiles.SelectedItem.ToString();

                iStart = s.IndexOf('[');

                iEnd = s.IndexOf(']');

                if ((iStart >= 0) && (iStart < s.Length) && (iEnd > iStart) && (iEnd < s.Length))
                    _FileName = s.Substring((iStart + 1), ((iEnd - 1) - (iStart + 1) + 1));
            }

            this.Close();
        }

              
    }
}