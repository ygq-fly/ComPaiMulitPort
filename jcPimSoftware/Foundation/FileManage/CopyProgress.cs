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

        #region ��������

        /// <summary>
        /// �������ݼ���
        /// </summary>
        ArrayList al;

        /// <summary>
        /// Ŀ��·��
        /// </summary>
        string targetPath = string.Empty;

        FileService mc;

        Thread Th;

        /// <summary>
        /// �洢�豸�Ŀ��ô�С
        /// </summary>
        long _usbSize = 0;

        #endregion

        #region ���캯��
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

        #region ��ʼ�����ļ��л����ļ�
        /// <summary>
        /// ��ʼ�����ļ��л����ļ�
        /// </summary>
        private void StartCopy()
        {
            Th = new Thread(new ThreadStart(Start));
            Th.Start();
        }
        #endregion

        #region �߳��������Ʒ���
        /// <summary>
        /// �߳��������Ʒ���
        /// </summary>
        private void Start()
        {
            mc = new FileService();
            DirectoryInfo direcInfo;
            mc.MyCloseRun+=new FileService.CloseRun(mc_MyCloseRun);

            //��ȡ�ļ�����
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
            //ѭ������
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

            //�������
            this.Invoke((MethodInvoker)delegate
            {
                pb.Value = this.pb.Maximum;
                MessageBox.Show(this, "OK!");
                Th.Abort();
                this.DialogResult = DialogResult.OK;
            });

        }
        #endregion

        #region  ���������õ��¼�����
        /// <summary>
        /// ���������õ��¼�����
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

        #region �洢�ռ䲻���������¼�����
        /// <summary>
        /// �洢�ռ䲻���������¼�����
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
    