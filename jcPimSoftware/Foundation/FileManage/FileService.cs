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

        #region 参数定义
      
        FileClass fc;

        /// <summary>
        /// 源目录 
        /// </summary>
        private DirectoryInfo _Source;

        /// <summary>
        /// 目标目录
        /// </summary>
        private DirectoryInfo _Target;

        private FileInfo _fileInfo;


        /// <summary> 
        /// 文件复制完成 
        /// </summary> 
        /// <param name="FileCount">文件数量合计</param> 
        /// <param name="CopyCount">复制完成的数量</param> 
        /// <param name="FileSize">文件大小合计</param> 
        /// <param name="CopySize">复制完成的大小</param> 
        /// <param name="FileName">复制完成的文件名</param> 
        public delegate void CopyRun(int FileCount,long _folderSize, int CopyCount,string FileName);

        public event CopyRun MyCopyRun;

        public delegate void CloseRun();

        public event CloseRun MyCloseRun;

        /// <summary>
        /// 要复制的文件总数
        /// </summary>
        private int _FileCount = 0;

        /// <summary>
        /// 要复制的文件和文件夹总数
        /// </summary>
        private long _folderSize = 0;

        /// <summary>
        /// 已经复制的文件个数
        /// </summary>
        private int _CopyCount = 0;

        #endregion

        #region 复制目录包含文件
        /// <summary>
        /// 复制目录包含文件 
        /// </summary>
        /// <param name="p_SourceDirectory">源目录</param> 
        /// <param name="p_TargetDirectory">目标目录</param> 
        public void CopyDirectory(string p_SourceDirectory, string p_TargetDirectory)
        {
            _Source = new DirectoryInfo(p_SourceDirectory);
            string strTemp = p_TargetDirectory + p_SourceDirectory.Substring(p_SourceDirectory.LastIndexOf(@"\") + 1);
            _Target = new DirectoryInfo(strTemp);
        }
        #endregion

        #region 开始复制
        /// <summary>  
        /// 开始复制
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

        #region 获取文件个数
        /// <summary>
        /// 获取文件个数
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

        #region 复制文件
        /// <summary>
        /// 复制文件
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

        #region 获取文件夹下的文件个数和文件夹大小
        /// <summary>
        /// 获取文件夹下的文件个数和文件夹大小
        /// </summary>
        /// <param name="MySiurceDirectory">目录文件夹</param>
        public void GetFileCount(DirectoryInfo MySiurceDirectory)
        {
            if (MySiurceDirectory != null)
            {
                //循环文件
                foreach (FileInfo _File in MySiurceDirectory.GetFiles())        
                {
                    _FileCount++;
                    _folderSize += _File.Length;
                }
                //循环子目录 
                foreach (DirectoryInfo _SourceSubDir in MySiurceDirectory.GetDirectories())  
                {
                    GetFileCount(_SourceSubDir);
                }
            }
        }
        #endregion

        #region 复制目录到指定目录
        /// <summary>
        /// 复制目录到指定目录
        /// </summary>
        /// <param name="source">源目录</param> 
        /// <param name="target">目标目录</param> 
        public void Copy(DirectoryInfo p_Source, DirectoryInfo p_Target)
        {
            if (!Directory.Exists(p_Target.FullName))
                Directory.CreateDirectory(p_Target.FullName);

            //循环文件 
            foreach (FileInfo _File in p_Source.GetFiles())
            {
                _File.CopyTo(Path.Combine(p_Target.ToString(), _File.Name), true);
                _CopyCount++;
                if (MyCopyRun != null)
                    MyCopyRun(_FileCount,_folderSize, _CopyCount, _File.Name);
            }

            //循环子目录 
            foreach (DirectoryInfo _SourceSubDir in p_Source.GetDirectories())
            {
                DirectoryInfo _NextDir = p_Target.CreateSubdirectory(_SourceSubDir.Name);
                Copy(_SourceSubDir, _NextDir);
            }

        }
        #endregion

        #region 新建文件夹
        /// <summary>
        /// 新建文件夹
        /// </summary>
        /// <param name="fileName">文件夹名称</param>
        /// <param name="path">路径</param>
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

        #region 获取指定路径下的所有文件夹和文件
        /// <summary>
        /// 获取指定路径下的所有文件夹和文件
        /// </summary>
        /// <param name="path">路径参数</param>
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

        #region 删除文件和文件夹
        /// <summary>
        /// 删除文件和文件夹
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

        #region 获取文件大小
        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件大小</returns>
        public long FileSize(string filePath)
        {
            long temp = 0;

            //判断当前路径所指向的是否为文件
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

        #region 获取文件后缀名
        /// <summary>
        /// 获取文件后缀名
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>后缀名</returns>
        public string GetFex(string fileName)
        {
            FileInfo info = new FileInfo(fileName);
            return info.Extension;
        }
        #endregion

        #region 判断文件的类型
        /// <summary>
        /// 判断文件的类型
        /// </summary>
        /// <param name="extension">后缀名</param>
        /// <returns>类型编号</returns>
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

        #region 判断文件类型名称
        /// <summary>
        /// 判断文件类型名称
        /// </summary>
        /// <param name="num">文件类型编号</param>
        /// <returns>文件类型名称</returns>
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

        #region 获取上一目录路径
        /// <summary>
        /// 获取上一目录路径
        /// </summary>
        /// <param name="path">当前路径</param>
        /// <returns>上一路径</returns>
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
        /// 错误框
        /// </summary>
        /// <param name="info">标题名称</param>
        /// <param name="labTxt">内容</param>
        /// <param name="btnTxt">按钮名称</param>
        /// <returns></returns>
        public DialogResult ErrorMessageBox(string info, string labTxt, string btnTxt)
        {
            Error error = new Error(info, labTxt, btnTxt);
            return error.ShowDialog();
        }
        #endregion

        #region 消息框
        /// <summary>
        /// 消息框
        /// </summary>
        /// <param name="info">标题名称</param>
        /// <param name="labTxt">内容</param>
        /// <param name="btnTxt">按钮名称</param>
        public void InfoMessageBox(string info, string labTxt, string btnTxt)
        {
            Ok o = new Ok(info,labTxt,btnTxt);
            o.Show();
        }
        #endregion

        #region OkCancel警告框
        /// <summary>
        /// OkCancel警告框
        /// </summary>
        /// <param name="info">标题名称</param>
        /// <param name="labTxt">内容</param>
        /// <param name="btnOkTxt">Ok按钮名称</param>
        /// <param name="btnCancelTxt">Cancel按钮名称</param>
        /// <returns></returns>
        public DialogResult OkCancleMessageBox(string info, string labTxt, string btnOkTxt,string btnCancelTxt)
        {
            OkCancel oc = new OkCancel(info,labTxt,btnOkTxt,btnCancelTxt);
            return oc.ShowDialog();
        }
        #endregion

        #region YesNo警告框
        /// <summary>
        /// YesNo警告框
        /// </summary>
        /// <param name="info">标题名称</param>
        /// <param name="labTxt">内容</param>
        /// <param name="btnYesTxt">Yes按钮名称</param>
        /// <param name="btnNoTxt">No按钮名称</param>
        /// <returns></returns>
        public DialogResult YesNoMessageBox(string info, string labTxt, string btnYesTxt, string btnNoTxt)
        {
            YesNo yn = new YesNo(info, labTxt, btnYesTxt, btnNoTxt);
            return yn.ShowDialog();
        }
        #endregion

        /// <summary>
        /// YesNoCancel警告框
        /// </summary>
        /// <param name="info">标题名称</param>
        /// <param name="labTxt">内容</param>
        /// <param name="btnYesTxt">Yes按钮名称</param>
        /// <param name="btnNoTxt">No按钮名称</param>
        /// <param name="btnCancelTxt">Cancel按钮名称</param>
        /// <returns></returns>
        public DialogResult YesNoCancelMessageBox(string info, string labTxt, string btnYesTxt, string btnNoTxt, string btnCancelTxt)
        {
            YesNoCancel ync = new YesNoCancel(info, labTxt, btnYesTxt, btnNoTxt, btnCancelTxt);
            return ync.ShowDialog();
        }

    }
}
