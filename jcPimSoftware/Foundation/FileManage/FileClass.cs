using System;
using System.Collections.Generic;
using System.Text;

namespace jcPimSoftware
{
    public class FileClass
    {

        #region///FileClass

        //�ļ�����
        private string _fileName;

        //�ļ�����
        private string _fileType;

        //�ļ���С
        private long _fileSize;


        //�ļ�����ʱ��
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
