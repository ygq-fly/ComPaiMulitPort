using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.IO;

namespace jcPimSoftware
{
    public partial class CopyProgress : Form
    {

        #region 参数定义

        /// <summary>
        /// 复制内容集合
        /// </summary>
        ArrayList al;

        /// <summary>
        /// 目标路径
        /// </summary>
        string targetPath = string.Empty;

        FileService mc;

        Thread Th;

        /// <summary>
        /// 存储设备的可用大小
        /// </summary>
        long _usbSize = 0;

        #endregion

        #region 构造函数
        public CopyProgress(ArrayList list, string destinationFileName,long usbSize)
        {
            InitializeComponent();
            al = list;
            targetPath = destinationFileName;
            _usbSize = usbSize;
        }
        #endregion

        #region CopyProgress
        private void CopyProgress_Load(object sender, EventArgs e)
        {
            StartCopy();
        }
        #endregion

        #region 开始复制文件夹或者文件
        /// <summary>
        /// 开始复制文件夹或者文件
        /// </summary>
        private void StartCopy()
        {
            Th = new Thread(new ThreadStart(Start));
            Th.Start();
        }
        #endregion

        #region 线程启动复制方法
        /// <summary>
        /// 线程启动复制方法
        /// </summary>
        private void Start()
        {
            mc = new FileService();
            DirectoryInfo direcInfo;
            mc.MyCloseRun+=new FileService.CloseRun(mc_MyCloseRun);

            //获取文件个数
            for (int i = 0; i < al.Count; i++)
            {
                if (Directory.Exists(al[i].ToString()))
                {
                    direcInfo = new DirectoryInfo(al[i].ToString());
                    mc.GetFileCount(direcInfo);
                }
                else
                {
                    mc.GetFile(al[i].ToString());
                }
            }
            //循环复制
            for (int i = 0; i < al.Count; i++)
            {
                if (Directory.Exists(al[i].ToString()))
                {
                    mc.CopyDirectory(al[i].ToString(), targetPath);
                    mc.Run(_usbSize);
                }
                else
                {
                    mc.CopyFile(al[i].ToString(), targetPath);
                }
                mc.MyCopyRun += new FileService.CopyRun(_Info_MyCopyRun);
            }

            //复制完成
            this.Invoke((MethodInvoker)delegate
            {
                pb.Value = this.pb.Maximum;
                MessageBox.Show(this, "OK!");
                Th.Abort();
                this.DialogResult = DialogResult.OK;
            });

        }
        #endregion

        #region  复制所调用的事件方法
        /// <summary>
        /// 复制所调用的事件方法
        /// </summary>
        void _Info_MyCopyRun(int FileCount,long folderSize, int CopyCount, string FileName)
        {
            this.Invoke((MethodInvoker)delegate
            {
                pb.Maximum = FileCount;
                pb.Value = CopyCount;
                lb1.Text = FileName;
            });
        }
        #endregion

        #region 存储空间不够触发的事件方法
        /// <summary>
        /// 存储空间不够触发的事件方法
        /// </summary>
        void mc_MyCloseRun()
        {
            this.Invoke((MethodInvoker)delegate
             {
                 Th.Abort();
                 this.Close();
             });
        }
        #endregion

        #region Cancle
        private void Cancle_Click(object sender, EventArgs e)
        {
            Th.Abort();
            this.Close();
        }
        #endregion
    }
}
    