using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;


namespace jcPimSoftware
{
    public class FileService
    {

        #region ��������
      
        FileClass fc;

        /// <summary>
        /// ԴĿ¼ 
        /// </summary>
        private DirectoryInfo _Source;

        /// <summary>
        /// Ŀ��Ŀ¼
        /// </summary>
        private DirectoryInfo _Target;

        private FileInfo _fileInfo;


        /// <summary> 
        /// �ļ�������� 
        /// </summary> 
        /// <param name="FileCount">�ļ������ϼ�</param> 
        /// <param name="CopyCount">������ɵ�����</param> 
        /// <param name="FileSize">�ļ���С�ϼ�</param> 
        /// <param name="CopySize">������ɵĴ�С</param> 
        /// <param name="FileName">������ɵ��ļ���</param> 
        public delegate void CopyRun(int FileCount,long _folderSize, int CopyCount,string FileName);

        public event CopyRun MyCopyRun;

        public delegate void CloseRun();

        public event CloseRun MyCloseRun;

        /// <summary>
        /// Ҫ���Ƶ��ļ�����
        /// </summary>
        private int _FileCount = 0;

        /// <summary>
        /// Ҫ���Ƶ��ļ����ļ�������
        /// </summary>
        private long _folderSize = 0;

        /// <summary>
        /// �Ѿ����Ƶ��ļ�����
        /// </summary>
        private int _CopyCount = 0;

        #endregion

        #region ����Ŀ¼�����ļ�
        /// <summary>
        /// ����Ŀ¼�����ļ� 
        /// </summary>
        /// <param name="p_SourceDirectory">ԴĿ¼</param> 
        /// <param name="p_TargetDirectory">Ŀ��Ŀ¼</param> 
        public void CopyDirectory(string p_SourceDirectory, string p_TargetDirectory)
        {
            _Source = new DirectoryInfo(p_SourceDirectory);
            string strTemp = p_TargetDirectory + p_SourceDirectory.Substring(p_SourceDirectory.LastIndexOf(@"\") + 1);
            _Target = new DirectoryInfo(strTemp);
        }
        #endregion

        #region ��ʼ����
        /// <summary>  
        /// ��ʼ����
        /// </summary>
        public void Run(long usbSize)
        {
            if (usbSize <_folderSize)
            {
                System.Windows.Forms.MessageBox.Show("The files you want to copy are \r\nlarger than current storage capacity!", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                if (MyCloseRun != null)
                    MyCloseRun();
            }
            else
            {
                Copy(_Source, _Target);
            }
        }
        #endregion

        #region ��ȡ�ļ�����
        /// <summary>
        /// ��ȡ�ļ�����
        /// </summary>
        /// <param name="path"></param>
        public void GetFile(string path)
        {
            if (File.Exists(path))
            {
                _FileCount++;
            }
        }
        #endregion

        #region �����ļ�
        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="targetPath"></param>
        public void CopyFile(string filePath,string targetPath) 
        {
            _fileInfo = new FileInfo(filePath);
            _fileInfo.CopyTo(Path.Combine(targetPath, _fileInfo.Name), true);
            _CopyCount++;
            if (MyCopyRun != null)
                MyCopyRun(_FileCount,_folderSize, _CopyCount, _fileInfo.Name);
        }
        #endregion

        #region ��ȡ�ļ����µ��ļ��������ļ��д�С
        /// <summary>
        /// ��ȡ�ļ����µ��ļ��������ļ��д�С
        /// </summary>
        /// <param name="MySiurceDirectory">Ŀ¼�ļ���</param>
        public void GetFileCount(DirectoryInfo MySiurceDirectory)
        {
            if (MySiurceDirectory != null)
            {
                //ѭ���ļ�
                foreach (FileInfo _File in MySiurceDirectory.GetFiles())        
                {
                    _FileCount++;
                    _folderSize += _File.Length;
                }
                //ѭ����Ŀ¼ 
                foreach (DirectoryInfo _SourceSubDir in MySiurceDirectory.GetDirectories())  
                {
                    GetFileCount(_SourceSubDir);
                }
            }
        }
        #endregion

        #region ����Ŀ¼��ָ��Ŀ¼
        /// <summary>
        /// ����Ŀ¼��ָ��Ŀ¼
        /// </summary>
        /// <param name="source">ԴĿ¼</param> 
        /// <param name="target">Ŀ��Ŀ¼</param> 
        public void Copy(DirectoryInfo p_Source, DirectoryInfo p_Target)
        {
            if (!Directory.Exists(p_Target.FullName))
                Directory.CreateDirectory(p_Target.FullName);

            //ѭ���ļ� 
            foreach (FileInfo _File in p_Source.GetFiles())
            {
                _File.CopyTo(Path.Combine(p_Target.ToString(), _File.Name), true);
                _CopyCount++;
                if (MyCopyRun != null)
                    MyCopyRun(_FileCount,_folderSize, _CopyCount, _File.Name);
            }

            //ѭ����Ŀ¼ 
            foreach (DirectoryInfo _SourceSubDir in p_Source.GetDirectories())
            {
                DirectoryInfo _NextDir = p_Target.CreateSubdirectory(_SourceSubDir.Name);
                Copy(_SourceSubDir, _NextDir);
            }

        }
        #endregion

        #region �½��ļ���
        /// <summary>
        /// �½��ļ���
        /// </summary>
        /// <param name="fileName">�ļ�������</param>
        /// <param name="path">·��</param>
        public bool NewFolder(string path,string fileName)
        {
            bool flag = false;
            if (fileName != "")
            {
                string fullName = path + "\\" + fileName;
                if (!Directory.Exists(fullName))
                {
                    DirectoryInfo info = new DirectoryInfo(fullName);
                    info.Create();
                    flag = true;
                }
            }
            return flag;
        }
        #endregion

        #region ��ȡָ��·���µ������ļ��к��ļ�
        /// <summary>
        /// ��ȡָ��·���µ������ļ��к��ļ�
        /// </summary>
        /// <param name="path">·������</param>
        public IList<FileClass> GetAll(string path)
        {
            string extension=string.Empty;
            int i = 0;
            IList<FileClass> list = new List<FileClass>();
            DirectoryInfo info = new DirectoryInfo(path);
            FileSystemInfo[] fs = info.GetFileSystemInfos();
            for (i = 0; i < fs.Length; i++)
            {
                fc = new FileClass();
                fc.FileName = fs[i].Name;
                fc.FileSize = FileSize(path+fc.FileName);
                fc.FileTime = fs[i].LastWriteTime;
                list.Add(fc);
            }
            return list;
        }
        #endregion

        #region ɾ���ļ����ļ���
        /// <summary>
        /// ɾ���ļ����ļ���
        /// </summary>
        /// <param name="dir"></param>
        public void DeleteFolder(string dir)
        {
            if (File.Exists(dir))
            {
                try
                {
                    FileSystem.DeleteFile(dir, UIOption.OnlyErrorDialogs, 
                        RecycleOption.DeletePermanently, UICancelOption.DoNothing);
                }
                catch { }
            }
            else
            {
                try
                {
                    FileSystem.DeleteDirectory(dir,
                         UIOption.OnlyErrorDialogs,RecycleOption.DeletePermanently);
                }
                catch { }
            }
        }
        #endregion

        #region ��ȡ�ļ���С
        /// <summary>
        /// ��ȡ�ļ���С
        /// </summary>
        /// <param name="filePath">�ļ�·��</param>
        /// <returns>�ļ���С</returns>
        public long FileSize(string filePath)
        {
            long temp = 0;

            //�жϵ�ǰ·����ָ����Ƿ�Ϊ�ļ�
            if (File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Length % 1024 > 0)
                {
                    temp = fileInfo.Length / 1024 + 1; 
                }
                else
                {
                    temp = fileInfo.Length / 1024;
                }
            }
            return temp;
        }
        #endregion

        #region ��ȡ�ļ���׺��
        /// <summary>
        /// ��ȡ�ļ���׺��
        /// </summary>
        /// <param name="fileName">�ļ���</param>
        /// <returns>��׺��</returns>
        public string GetFex(string fileName)
        {
            FileInfo info = new FileInfo(fileName);
            return info.Extension;
        }
        #endregion

        #region �ж��ļ�������
        /// <summary>
        /// �ж��ļ�������
        /// </summary>
        /// <param name="extension">��׺��</param>
        /// <returns>���ͱ��</returns>
        public int CheckFileType(string extension)
        {
            int i = 0;
            switch (extension.ToLower())
            {
                case "": break;
                case ".txt": i = 1; break;
                case ".bmp": i = 2; break;
                case ".jpg": i = 3; break;
                case ".pdf": i = 4; break;
                case ".csv": i = 5; break;
                default: i = 6; break;
            }
            return i;
        }
        #endregion

        #region �ж��ļ���������
        /// <summary>
        /// �ж��ļ���������
        /// </summary>
        /// <param name="num">�ļ����ͱ��</param>
        /// <returns>�ļ���������</returns>
        public string FileType(int num)
        {
            string type = string.Empty;
            switch (num)
            {
                case 0: type = "Folder"; break;
                case 1: type = "Text Document"; break;
                case 2: type = "BMP Image"; break;
                case 3: type = "JEPG Image"; break;
                case 4: type = "PDF File"; break;
                case 5: type = "Microsoft Office Excel"; break;
                case 6: type = "Unknown file"; break;
            }
            return type;
        }
        #endregion

        #region ��ȡ��һĿ¼·��
        /// <summary>
        /// ��ȡ��һĿ¼·��
        /// </summary>
        /// <param name="path">��ǰ·��</param>
        /// <returns>��һ·��</returns>
        public string Returnf(string path)
        {
            string[] arr = path.Split(char.Parse("\\"));
            int num = arr.Length - 1;
            if (num == 1)
            {
                path = path.Substring(0, path.LastIndexOf("\\") + 1);
            }
            else if (num == 2)
            {
                path = path.Substring(0, path.IndexOf("\\") + 1);
            }
            else if (num > 1)
            {
                path = path.Substring(0, path.LastIndexOf("\\"));
            }
            if (num > 2)
            {
                path = path.Substring(0, path.LastIndexOf("\\") + 1);
            }
            return path;
        }
        #endregion

        #region
        /// <summary>
        /// �����
        /// </summary>
        /// <param name="info">��������</param>
        /// <param name="labTxt">����</param>
        /// <param name="btnTxt">��ť����</param>
        /// <returns></returns>
        public DialogResult ErrorMessageBox(string info, string labTxt, string btnTxt)
        {
            Error error = new Error(info, labTxt, btnTxt);
            return error.ShowDialog();
        }
        #endregion

        #region ��Ϣ��
        /// <summary>
        /// ��Ϣ��
        /// </summary>
        /// <param name="info">��������</param>
        /// <param name="labTxt">����</param>
        /// <param name="btnTxt">��ť����</param>
        public void InfoMessageBox(string info, string labTxt, string btnTxt)
        {
            Ok o = new Ok(info,labTxt,btnTxt);
            o.Show();
        }
        #endregion

        #region OkCancel�����
        /// <summary>
        /// OkCancel�����
        /// </summary>
        /// <param name="info">��������</param>
        /// <param name="labTxt">����</param>
        /// <param name="btnOkTxt">Ok��ť����</param>
        /// <param name="btnCancelTxt">Cancel��ť����</param>
        /// <returns></returns>
        public DialogResult OkCancleMessageBox(string info, string labTxt, string btnOkTxt,string btnCancelTxt)
        {
            OkCancel oc = new OkCancel(info,labTxt,btnOkTxt,btnCancelTxt);
            return oc.ShowDialog();
        }
        #endregion

        #region YesNo�����
        /// <summary>
        /// YesNo�����
        /// </summary>
        /// <param name="info">��������</param>
        /// <param name="labTxt">����</param>
        /// <param name="btnYesTxt">Yes��ť����</param>
        /// <param name="btnNoTxt">No��ť����</param>
        /// <returns></returns>
        public DialogResult YesNoMessageBox(string info, string labTxt, string btnYesTxt, string btnNoTxt)
        {
            YesNo yn = new YesNo(info, labTxt, btnYesTxt, btnNoTxt);
            return yn.ShowDialog();
        }
        #endregion

        /// <summary>
        /// YesNoCancel�����
        /// </summary>
        /// <param name="info">��������</param>
        /// <param name="labTxt">����</param>
        /// <param name="btnYesTxt">Yes��ť����</param>
        /// <param name="btnNoTxt">No��ť����</param>
        /// <param name="btnCancelTxt">Cancel��ť����</param>
        /// <returns></returns>
        public DialogResult YesNoCancelMessageBox(string info, string labTxt, string btnYesTxt, string btnNoTxt, string btnCancelTxt)
        {
            YesNoCancel ync = new YesNoCancel(info, labTxt, btnYesTxt, btnNoTxt, btnCancelTxt);
            return ync.ShowDialog();
        }

    }
}
