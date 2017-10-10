using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Globalization;

namespace jcPimSoftware
{
    public partial class FileManager : Form
    {

        #region 构造函数
        public FileManager()
        {
            InitializeComponent();
            LoadItems();
        }
        #endregion

        #region 变量

        FileService c;

        /// <summary>
        /// 地址栏路径
        /// </summary>
        string urlPath= string.Empty;

        /// <summary>
        /// 移动存储设备可存储大小
        /// </summary>
        long _usbSize = 0;

        /// <summary>
        /// 保存第一次传进来的值
        /// </summary>
        string _path = string.Empty;

        NewFolder nf;

        ArrayList arrayList;

        /// <summary>
        /// 当设备被插入/拔出的时候，WINDOWS会向每个窗体发送WM_DEVICECHANGE 消息
        /// </summary>
        private const int WM_DEVICECHANGE = 0x0219;

        /// <summary>
        /// 如果wParam值等于DBT_DEVICEREMOVECOMPLETE，表示设备已经被移出。
        /// </summary>
        private const int DBT_DEVICEREMOVECOMPLETE = 0x8004; 

        /// <summary>
        /// 当消息的wParam 值等于 DBT_DEVICEARRIVAL 时，表示设备被插入并且已经可用
        /// </summary>
        private const int DBT_DEVICEARRIVAL = 0x8000;

        #endregion

        #region WndProc消息
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_DEVICECHANGE)
            {
                if (m.WParam.ToInt32() == DBT_DEVICEARRIVAL)
                {
                    LoadItems();
                }
                if (m.WParam.ToInt32() == DBT_DEVICEREMOVECOMPLETE)
                {
                    try
                    {
                        //当设备拔出时删掉相应的右键菜单
                        foreach (ToolStripItem item in SendToolStripMenuItem.DropDownItems)
                        {
                            SendToolStripMenuItem.DropDownItems.Remove(item);
                        }
                    }
                    catch
                    {
                        SendToolStripMenuItem.DropDownItems.Clear();
                    }
                }
            }

            base.WndProc(ref m);
        }
        #endregion

        #region 窗体事件
        private void demo_Load(object sender, EventArgs e)
        {
            //LanguageHelp.IsPreprocessID = false;

            //LanguageHelp.Load(App_Configure.Cnfgs.Lang_Pack_Path);

            //GetLanguage_STR();
        }
        #endregion

        #region 获取语言包配置
        /// <summary>
        /// 获取语言包配置
        /// </summary>
        private void GetLanguage_STR()
        {
            this.Text = LanguageHelp.GetDlgItem("010000", "NOthing");
            ItemNode[] items = LanguageHelp.GetSubdlgItems("010000");
            btn_new.Text = items[0].label;
            btnDel.Text = items[1].label;
            lb.Text = items[2].label;
            lb1.Text = items[3].label;
            btnReturn.Text = items[4].label;
            ItemNode[] subItems = LanguageHelp.GetSsubdlgItems("010000", items[5].id);
            
            lv.Columns[0].Text = subItems[0].label;
            lv.Columns[1].Text = subItems[1].label;
            lv.Columns[2].Text = subItems[2].label;
            lv.Columns[3].Text = subItems[3].label;

        }
        #endregion

        #region 检测可存储设备
        /// <summary>
        /// 检测可存储设备
        /// </summary>
        private void LoadItems()
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.Removable)
                {
                    _usbSize=drive.AvailableFreeSpace;
                    SendToolStripMenuItem.DropDownItems.Add(drive.Name);
                    SendToolStripMenuItem.DropDownItemClicked += new ToolStripItemClickedEventHandler(SendToolStripMenuItem_DropDownItemClicked);
                    foreach (ToolStripItem item in SendToolStripMenuItem.DropDownItems)
                    {
                        item.Font = new Font("宋体", 12F);
                        item.Image = ImagesManage.GetImage("resources", "usb.png");
                    }
                }
            }
        }
        #endregion

        #region SendToolStripMenuItem
        private void SendToolStripMenuItem_DropDownItemClicked(object sender, EventArgs e)
        {
            string txt = SendToolStripMenuItem.DropDownItems[0].Text;
            int a = lv.SelectedItems.Count;
            if (a > 0)
            {
                Copy();
                Paste(txt);
            }
        }
        #endregion

        #region 按钮事件
        #region cb
        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
             string value = cb.Text;
             View v=(View)Enum.Parse(typeof(View),value);
             lv.View = v;
         }
        #endregion

        #region lv
         private void lv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string[] str = urlPath.Split(char.Parse("\\"));
            if (str.Length > 4)
                btn_new.Enabled = false;
            else
                btn_new.Enabled = true;
            ListViewItem lvht = lv.GetItemAt(e.X, e.Y);
            if (Directory.Exists(urlPath + lvht.Text))
            {
                if (lvht != null)
                {
                    urlPath += lvht.Text + "\\";
                    tbPath.Text = urlPath;
                    try
                    {
                        GetAllFiles(urlPath);
                    }
                    catch { }
                }
            }
            else
            {
                try
                {
                    Process.Start(urlPath + lvht.Text);
                }
                catch { }
            }

        }
        #endregion

        #region btn_new
        private void btn_new_Click(object sender, EventArgs e)
        {
            nf = new NewFolder();
            DialogResult dr = nf.ShowDialog();
            if (dr == DialogResult.Yes)
            {
                c = new FileService();
                if (tbPath.Text != "")
                {
                    if (c.NewFolder(tbPath.Text, nf.tb1.Text) == true)
                    {
                        GetAllFiles(tbPath.Text);
                    }
                    else
                    {

                        MessageBox.Show(this, "Filename exist The file name has already existed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(this, "The saving path is error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
 
                }
            }
        }
        #endregion

        #region tm
        private void tm_Click(object sender, EventArgs e)
        {
            Del();
        }
        #endregion

        #region btnReturn
        private void btnReturn_Click(object sender, EventArgs e)
        {
            string rfPath = tbPath.Text.Trim();
            if (rfPath != "")
            {
                if (!rfPath.Equals(_path))
                {
                    if (rfPath != "" && rfPath != null)
                    {
                        rfPath = c.Returnf(tbPath.Text.Trim());
                        tbPath.Text = rfPath;
                        urlPath = rfPath;
                        string[] str = urlPath.Split(char.Parse("\\"));
                        if (str.Length - 1 > 4)
                            btn_new.Enabled = false;
                        else
                            btn_new.Enabled = true;
                    }
                }
                GetAllFiles(rfPath);
            }
        }
        #endregion

        #region ToolStripMenuItem
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lv.SelectedItems.Count > 0)
            {
                if (Directory.Exists(urlPath + lv.SelectedItems[0].Text))
                {
                    if (lv.SelectedItems != null)
                    {
                        urlPath += lv.SelectedItems[0].Text + "\\";
                        tbPath.Text = urlPath;
                        try
                        {
                            GetAllFiles(urlPath);
                        }
                        catch { }
                    }
                }
                else
                {
                    try
                    {
                        Process.Start(urlPath + lv.SelectedItems[0].Text);
                    }
                    catch { }
                }
            }
        }
        #endregion

        #region btnDel
        private void btnDel_Click(object sender, EventArgs e)
        {
            Del();
        }
        #endregion

        #region CopyToolStripMenuItem
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copy();
        }
        #endregion

        #region ToolStripMenuItem
        private void ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (lv.SelectedItems != null)
            {
                Paste(tbPath.Text);
                GetAllFiles(tbPath.Text);
            }
        }
        #endregion
        #endregion

        #region 初始化获取路径下的所有数据
        /// <summary>
        /// 初始化获取路径下的所有数据
        /// </summary>
        /// <param name="path">路径</param>
        public void LoadAllData(string path)
        {
            _path = path;//记录第一次传进来的路径
            if (path== "")
            {
                btn_new.Enabled = false;
            }
            else
            {
                btn_new.Enabled = true;
            }

            //cb.Items.AddRange(Enum.GetNames(typeof(View)));
            if (Directory.Exists(path))
            {
                cb.Items.Add(View.LargeIcon);
                cb.Items.Add(View.Details);
                cb.SelectedIndex = 0;
                c = new FileService();
                GetAllFiles(path);
                tbPath.Text = path;
                urlPath = path;
            }
            else
            {
                MessageBox.Show(this, "The path initialization failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 复制文件
        /// <summary>
        /// 复制文件
        /// </summary>
        private void Copy()
        {
            int a = lv.SelectedItems.Count;
            string value=tbPath.Text;
            arrayList = new ArrayList();
            if (a > 1)
            {
                arrayList.Clear();
                for (int i = 0; i < a; i++)
                {
                    arrayList.Add(value + lv.SelectedItems[i].Text);
                }
            }
            else if (a > 0)
            {
                arrayList.Add(value + lv.SelectedItems[0].Text);
            }

        }
        #endregion

        #region 粘贴文件
        /// <summary>
        /// 粘贴文件
        /// </summary>
        /// <param name="destinationFileName">目标路径</param>
        private void Paste(string destinationFileName)
        {
            if (arrayList != null)
            {
                CopyProgress cp = new CopyProgress(arrayList,destinationFileName,_usbSize);
                DialogResult dr= cp.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    cp.Close();
                    arrayList = null;
                }
            }
        }
        #endregion

        #region 删除所选的文件和文件夹
        /// <summary>
        /// 删除所选的文件和文件夹
        /// </summary>
        private void Del()
        {
            int a = lv.SelectedItems.Count;
            ArrayList al = new ArrayList();
            if (a > 1)
            {
                al.Clear();
                for (int i = 0; i < a; i++)
                {
                    al.Add(lv.SelectedItems[i].Text);
                }
                DelFiles df = new DelFiles(al, tbPath.Text);
                DialogResult result = df.ShowDialog();
                if (result == DialogResult.No)
                {
                    GetAllFiles(tbPath.Text);
                }
            }
            else if (a > 0)
            {
                //调用删除当前选中文件或文件夹的方法
                //string txt = LanguageHelp.GetStrItem("010000", "Nothing");
                //ItemNode[] node = LanguageHelp.GetSubstrItems("010000");
                DialogResult dr = c.YesNoMessageBox("Warning", "The information cannot be retrieved \r\nafter deletion, are you sure you want \r\nto delete it?", "Yes", "No");
                if (dr == DialogResult.Yes)
                {
                    c.DeleteFolder(tbPath.Text + lv.SelectedItems[0].Text);
                    GetAllFiles(tbPath.Text);
                }
            }
        }

        #endregion

        #region 获取指定路径下的所有文件
        /// <summary>
        /// 获取指定路径下的所有文件
        /// </summary>
        /// <param name="path"></param>
        private void GetAllFiles(string path)
        {
            lv.Items.Clear();
            int num = 0;
            string extension = string.Empty;
            string size = string.Empty;

            //获取文件集合
            IList<FileClass> list = c.GetAll(path);

            for (int i=0; i < list.Count; i++)
            {
                extension = c.GetFex(list[i].FileName);

                num = c.CheckFileType(extension);
                if (num == 0)
                    size = "";
                else if (list[i].FileSize == 0)
                    size = "0 KB";
                else
                    size = list[i].FileSize.ToString("###,### KB");

                //绑定数据到ListView
                lv.Items.Add(list[i].FileName);
                lv.Items[i].SubItems.Add(size);
                lv.Items[i].SubItems.Add(c.FileType(num));
                lv.Items[i].SubItems.Add(list[i].FileTime.ToString());
                lv.Items[i].ImageIndex = num;
            }
        }
        #endregion

    }
}