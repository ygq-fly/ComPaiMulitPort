using System;
using System.Collections.Generic;
using System.Text;

namespace jcPimSoftware
{
    public class FileClass
    {

        #region///FileClass

        //文件名称
        private string _fileName;

        //文件类型
        private string _fileType;

        //文件大小
        private long _fileSize;


        //文件访问时间
        private DateTime _fileTime;


        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        } 


        public DateTime FileTime
        {
            get { return _fileTime; }
            set { _fileTime = value; }
        }

        public long FileSize
        {
            get { return _fileSize; }
            set { _fileSize = value; }
        }
        public string FileType
        {
            get { return _fileType; }
            set { _fileType = value; }
        }

        #endregion
    }
}
