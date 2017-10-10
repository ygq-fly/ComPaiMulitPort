using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using jcPimSoftware;

namespace jcPimSoftware
{
    public partial class DelFiles : Form
    {

        #region

        ArrayList arrayList;
        string filePath = string.Empty;
        FileService mc;
        bool flag = false;

        #endregion

        #region 构造函数
        public DelFiles(ArrayList list,string path)
        {
            InitializeComponent();
            filePath = path;
            arrayList = list;
        }
        #endregion

        #region 按钮事件
        #region btn1
        private void btn1_Click(object sender, EventArgs e)
        {
                listb.Items.Clear();
                string item = string.Empty;
                int num = 0;
                if (arrayList != null)
                {
                    if (100 % arrayList.Count != 0)
                    {
                        num = 100 / arrayList.Count;
                    }
                    for (int i = 0; i < arrayList.Count; i++)
                    {
                        item = filePath + arrayList[i].ToString();
                        mc.DeleteFolder(item);
                        listb.Items.Add(item);
                        prob.Value += num;
                    }
                    this.prob.Value = this.prob.Maximum;
                    MessageBox.Show(this, "Deleted successfully!");
                }
                arrayList = null;
            }
        #endregion

        #region btn2
        private void btn2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
        #endregion

        #region btn3
        private void btn3_Click(object sender, EventArgs e)
        {
            if (flag == false)
            {
                this.Height = 414;
                flag = true;
            }
            else
            {
                this.Height = 152;
                flag = false;
            }
        }
        #endregion
        #endregion

        #region 窗体事件
        private void DelFiles_Load(object sender, EventArgs e)
        {
            mc = new FileService();
        }

        private void DelFiles_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
        #endregion
    }
}