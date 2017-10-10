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
    public partial class ReadFiles : Form
    {

        //�������ļ�������
        private string _FileName;
        internal string FileName
        {
            get { return _FileName; }
        }

        public ReadFiles()
        {
            InitializeComponent();
        }


        #region ���û������ļ����µ������ļ���䵽�б�
        /// <summary>
        /// ���û������ļ����µ������ļ���䵽�б�
        /// </summary>
        /// <param name="path">·������</param>
        internal void FillFiles(string path)
        {
            if (!Directory.Exists(path))
                return;

            DirectoryInfo info = new DirectoryInfo(path);
            //FileSystemInfo[] fs = info.GetFileSystemInfos();
            DirectoryInfo[] fs = info.GetDirectories();
            lbxFiles.SuspendLayout();

            lbxFiles.Items.Clear();

            for (int i = 0; i < fs.Length; i++)
            {
                //if (fs[i].Extension.ToLower() == ".csv")
                    lbxFiles.Items.Add(fs[i].Name);
            }
           
            lbxFiles.ResumeLayout(true);
        }
        #endregion

        #region ��ť�¼�
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lbxFiles.SelectedItem != null)  
                _FileName = lbxFiles.SelectedItem.ToString();

            this.DialogResult = DialogResult.OK;
        }

        #endregion
    }
}