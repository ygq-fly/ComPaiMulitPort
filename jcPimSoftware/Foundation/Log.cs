using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace jcPimSoftware
{
    class Log
    {
        /// <summary>
        /// 功能模块枚举
        /// </summary>
        internal enum EFunctionType
        {
            PIM = 0,
            SPECTRUM = 1,
            ISOLATION = 2,
            VSWR = 3,
            HARMONIC = 4,
            TestMode = 5
        }

        private const string strPIM_LogPath = "C:\\PIM_Log.txt";
        private const string strSPE_LogPath = "C:\\SPE_Log.txt";
        private const string strISO_LogPath = "C:\\ISO_Log.txt";
        private const string strVSW_LogPath = "C:\\VSW_Log.txt";
        private const string strHAR_LogPath = "C:\\HAR_Log.txt";
        private const string strTEST_LogPath = "C:\\TEST_Log.txt";

        /// <summary>
        /// 记录日志文件
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="type">日志类型</param>
        public static void WriteLog(string msg, EFunctionType type)
        {
            string strFilePath = "";
            switch (type)
            {
                case EFunctionType.PIM:
                    strFilePath = strPIM_LogPath;
                    break;
                case EFunctionType.SPECTRUM:
                    strFilePath = strSPE_LogPath;
                    break;
                case EFunctionType.ISOLATION:
                    strFilePath = strISO_LogPath;
                    break;
                case EFunctionType.VSWR:
                    strFilePath = strVSW_LogPath;
                    break;
                case EFunctionType.HARMONIC:
                    strFilePath = strHAR_LogPath;
                    break;
                case EFunctionType.TestMode:
                    strFilePath = strTEST_LogPath;
                    break;
                default:
                    break;
            }

            FileStream fs = new FileStream(strFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("====>" + DateTime.Now.ToString() + ";  " + msg.ToString());
            sw.Close();
            fs.Close();
        }
    }
}
