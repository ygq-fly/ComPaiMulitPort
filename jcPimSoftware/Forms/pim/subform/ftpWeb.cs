using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Globalization;
 
namespace jcPimSoftware
{
    public class FtpWeb
    {
        string ftpServerIP;
        string ftpRemotePath;
        string ftpUserID;
        string ftpPassword;
        string ftpURI;
 
        /// <summary>
        /// ����FTP
        /// </summary>
        /// <param name="FtpServerIP">FTP���ӵ�ַ</param>
        /// <param name="FtpRemotePath">ָ��FTP���ӳɹ���ĵ�ǰĿ¼, �����ָ����Ĭ��Ϊ��Ŀ¼</param>
        /// <param name="FtpUserID">�û���</param>
        /// <param name="FtpPassword">����</param>
        public FtpWeb(string FtpServerIP, string FtpRemotePath, string FtpUserID, string FtpPassword)
        {
            ftpServerIP = FtpServerIP;
            ftpRemotePath = FtpRemotePath;
            ftpUserID = FtpUserID;
            ftpPassword = FtpPassword;
            ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";
        }
 
        /// <summary>
        /// �ϴ�
        /// </summary>
        /// <param name="filename"></param>
        public void Upload(string filename)
        {
            FileInfo fileInf = new FileInfo(filename);
            string uri = ftpURI + fileInf.Name;
            FtpWebRequest reqFTP;
 
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();
            try
            {
                Stream strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Insert_Standard_ErrorLog.Insert("FtpWeb", "Upload Error --> " + ex.Message);
            }
        }
 
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        public void Download(string filePath, string fileName)
        {
            FtpWebRequest reqFTP;
            try
            {
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
 
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + fileName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
 
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
 
                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Insert_Standard_ErrorLog.Insert("FtpWeb", "Download Error --> " + ex.Message);
            }
        }
 
 
        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="fileName"></param>
        public void Delete(string fileName)
        {
            try
            {
                string uri = ftpURI + fileName;
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
 
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
 
                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Insert_Standard_ErrorLog.Insert("FtpWeb", "Delete Error --> " + ex.Message + "  �ļ���:" + fileName);
            }
        }
 
        /// <summary>
        /// ɾ���ļ���
        /// </summary>
        /// <param name="folderName"></param>
        public void RemoveDirectory(string folderName)
        {
            try
            {
                string uri = ftpURI + folderName;
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
 
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;
 
                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Insert_Standard_ErrorLog.Insert("FtpWeb", "Delete Error --> " + ex.Message + "  �ļ���:" + folderName);
            }
        }
 
        /// <summary>
        /// ��ȡ��ǰĿ¼����ϸ(�����ļ����ļ���)
        /// </summary>
        /// <returns></returns>
        public string[] GetFilesDetailList()
        {
            string[] downloadFiles;
            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI));
                ftp.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
 
                //while (reader.Read() > 0)
                //{
 
                //}
                string line = reader.ReadLine();
                //line = reader.ReadLine();
                //line = reader.ReadLine();
 
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf("\n"), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                downloadFiles = null;
                Insert_Standard_ErrorLog.Insert("FtpWeb", "GetFilesDetailList Error --> " + ex.Message);
                return downloadFiles;
            }
        }
 
        /// <summary>
        /// ��ȡ��ǰĿ¼���ļ��б�(���ļ�)
        /// </summary>
        /// <returns></returns>
        public string[] GetFileList(string mask)
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
 
                string line = reader.ReadLine();
                while (line != null)
                {
                    if (mask.Trim() != string.Empty && mask.Trim() != "*.*")
                    {
 
                        string mask_ = mask.Substring(0, mask.IndexOf("*"));
                        if (line.Substring(0, mask_.Length) == mask_)
                        {
                            result.Append(line);
                            result.Append("\n");
                        }
                    }
                    else
                    {
                        result.Append(line);
                        result.Append("\n");
                    }
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                downloadFiles = null;
                if (ex.Message.Trim() != "Զ�̷��������ش���: (550) �ļ�������(���磬δ�ҵ��ļ����޷������ļ�)��")
                {
                    Insert_Standard_ErrorLog.Insert("FtpWeb", "GetFileList Error --> " + ex.Message.ToString());
                }
                return downloadFiles;
            }
        }
 
        /// <summary>
        /// ��ȡ��ǰĿ¼�����е��ļ����б�(���ļ���)
        /// </summary>
        /// <returns></returns>
        public string[] GetDirectoryList()
        {
            string[] drectory = GetFilesDetailList();
            string m = string.Empty;
            foreach (string str in drectory)
            {
                int dirPos = str.IndexOf("<DIR>");
                if (dirPos>0)
                {
                    /*�ж� Windows ���*/
                    m += str.Substring(dirPos + 5).Trim() + "\n";
                }
                else if (str.Trim().Substring(0, 1).ToUpper() == "D")
                {
                    /*�ж� Unix ���*/
                    string dir = str.Substring(54).Trim();
                    if (dir != "." && dir != "..")
                    {
                        m += dir + "\n"; 
                    }
                }
            }
 
            char[] n = new char[] { '\n' };
            return m.Split(n);
        }
 
        /// <summary>
        /// �жϵ�ǰĿ¼��ָ������Ŀ¼�Ƿ����
        /// </summary>
        /// <param name="RemoteDirectoryName">ָ����Ŀ¼��</param>
        public bool DirectoryExist(string RemoteDirectoryName)
        {
            string[] dirList = GetDirectoryList();
            foreach (string str in dirList)
            {
                if (str.Trim() == RemoteDirectoryName.Trim())
                {
                    return true;
                }
            }
            return false;
        }
 
        /// <summary>
        /// �жϵ�ǰĿ¼��ָ�����ļ��Ƿ����
        /// </summary>
        /// <param name="RemoteFileName">Զ���ļ���</param>
        public bool FileExist(string RemoteFileName)
        {
            string[] fileList = GetFileList("*.*");
            foreach (string str in fileList)
            {
                if (str.Trim() == RemoteFileName.Trim())
                {
                    return true;
                }
            }
            return false;
        }
 
        /// <summary>
        /// �����ļ���
        /// </summary>
        /// <param name="dirName"></param>
        public void MakeDir(string dirName)
        {
            FtpWebRequest reqFTP;
            try
            {
                // dirName = name of the directory to create.
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + dirName));
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
 
                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Insert_Standard_ErrorLog.Insert("FtpWeb", "MakeDir Error --> " + ex.Message);
            }
        }
 
        /// <summary>
        /// ��ȡָ���ļ���С
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public long GetFileSize(string filename)
        {
            FtpWebRequest reqFTP;
            long fileSize = 0;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + filename));
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                fileSize = response.ContentLength;
 
                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Insert_Standard_ErrorLog.Insert("FtpWeb", "GetFileSize Error --> " + ex.Message);
            }
            return fileSize;
        }
 
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="currentFilename"></param>
        /// <param name="newFilename"></param>
        public void ReName(string currentFilename, string newFilename)
        {
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + currentFilename));
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = newFilename;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
 
                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Insert_Standard_ErrorLog.Insert("FtpWeb", "ReName Error --> " + ex.Message);
            }
        }
 
        /// <summary>
        /// �ƶ��ļ�
        /// </summary>
        /// <param name="currentFilename"></param>
        /// <param name="newFilename"></param>
        public void MovieFile(string currentFilename, string newDirectory)
        {
            ReName(currentFilename, newDirectory);
        }
 
        /// <summary>
        /// �л���ǰĿ¼
        /// </summary>
        /// <param name="DirectoryName"></param>
        /// <param name="IsRoot">true ����·��   false ���·��</param>
        public void GotoDirectory(string DirectoryName, bool IsRoot)
        {
            if (IsRoot)
            {
                ftpRemotePath = DirectoryName;
            }
            else
            {
                ftpRemotePath += DirectoryName + "/";
            }
            ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";
        }
 
        /// <summary>
        /// ɾ������Ŀ¼
        /// </summary>
        /// <param name="ftpServerIP">FTP ������ַ</param>
        /// <param name="folderToDelete">FTP �û���</param>
        /// <param name="ftpUserID">FTP �û���</param>
        /// <param name="ftpPassword">FTP ����</param>
        public static void DeleteOrderDirectory(string ftpServerIP, string folderToDelete, string ftpUserID, string ftpPassword)
        {
            try{
                if (!string.IsNullOrEmpty(ftpServerIP) && !string.IsNullOrEmpty(folderToDelete) && !string.IsNullOrEmpty(ftpUserID) && !string.IsNullOrEmpty(ftpPassword))
                {
                    FtpWeb fw = new FtpWeb(ftpServerIP, folderToDelete, ftpUserID, ftpPassword);
                    //���붩��Ŀ¼
                    fw.GotoDirectory(folderToDelete, true);
                    //��ȡ���Ŀ¼
                    string[] folders = fw.GetDirectoryList();
                    foreach (string folder in folders)
                    { 
                        if (!string.IsNullOrEmpty(folder) || folder != "")
                        {
                            //���붩��Ŀ¼
                            string subFolder = folderToDelete + "/" + folder;
                            fw.GotoDirectory(subFolder, true);
                            //��ȡ�ļ��б�
                            string[] files = fw.GetFileList("*.*");
                            if (files != null)
                            {
                                //ɾ���ļ�
                                foreach (string file in files)
                                {
                                    fw.Delete(file);
                                }
                            }
                            //ɾ����ӡ����ļ���
                            fw.GotoDirectory(folderToDelete, true);
                            fw.RemoveDirectory(folder);
                        }
                    }
 
                    //ɾ�������ļ���
                    string parentFolder = folderToDelete.Remove(folderToDelete.LastIndexOf('/'));
                    string orderFolder = folderToDelete.Substring(folderToDelete.LastIndexOf('/') + 1);
                    fw.GotoDirectory(parentFolder, true);
                    fw.RemoveDirectory(orderFolder);
                }
                else
                {
                    throw new Exception("FTP ��·������Ϊ�գ�");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("ɾ������ʱ�������󣬴�����ϢΪ��" + ex.Message);
            }
        }
    }
 
 
    public class Insert_Standard_ErrorLog
    {
        public static void Insert(string x, string y)
        {
            MessageBox.Show(y);
        }
    }
 
 
}