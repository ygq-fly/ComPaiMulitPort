using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Threading;
using System.IO;
using System.Windows.Forms;


namespace jcPimSoftware
{
    public class Code
    {
        #region 变量

        /// <summary>
        /// 路径
        /// </summary>
        public static readonly string filePath = @"D:\key";
        public static readonly string strFilePath =  @"D:\key\key";
        public static readonly string strLogPath =   @"D:\key\Log.txt";
        public static readonly string strErrPath =   @"D:\key\Err.txt";
        /// <summary>
        /// DES密钥
        /// </summary>
        public static readonly string DESkeys = "jointcom";
        public static readonly string UnDefine = "zjn934";
        /// <summary>
        /// 分隔字符
        /// </summary>
        public const string str = "#";

        #endregion

        #region  DES加密，解密
        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>DES加密字符串
        /// 
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));//加密密钥
                byte[] rgbIV = Keys;//密钥向量
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);//待加密的字符串
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>DES解密字符串
        /// 
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
        #endregion

        #region 把生成的key变成乱码
        /// <summary>把生成的key变成乱码
        /// 
        /// </summary>
        /// <param name="Str"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string StringXor(string Str, string Key)
        {
            int vKeyLen = Key.Length;
            char[] StrChars = Str.ToCharArray();
            char[] KeyChars = Key.ToCharArray();
            int j = 0;
            for (int i = 0; i < Str.Length; i++)
            {
                StrChars[i] ^= KeyChars[i++ % Key.Length];
            }
            return new string(StrChars);
        }
        #endregion

        #region 文件判断
        /// <summary>读取文件信息
        /// 
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        public string[] ReadFile(string strFilePath)
        {
            try
            {
                string textall;
                FileStream fs = new FileStream(strFilePath, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                textall = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
                fs.Close();
                fs.Dispose();
                textall = textall.Replace("\r\n","");
                textall = StringXor(textall, UnDefine);
                textall = DecryptDES(textall, DESkeys);
                string[] arr = textall.Split('#');
                return arr;
            }
            catch { return null; }


        }

        /// <summary>判断时间大小,后面的时间大于前面的时间，返回true，反之，返回false
        /// 
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public bool CheckDate(string date1, string date2)
        {
            bool booldate=false;
            //date1
            DateTime dts = Convert.ToDateTime(date1);
            //date2
            DateTime dte = Convert.ToDateTime(date2);
            //date2-date1
            TimeSpan dteTOdts = dte.Subtract(dts);
            if (dteTOdts.Days >= 0)
            {
                booldate = true;
            }
            return booldate;
        }

        /// <summary>返回使用天数
        /// 
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public int Usedday(string date1, string date2)
        {
            //date1
            DateTime dts = Convert.ToDateTime(date1);
            //date2
            DateTime dte = Convert.ToDateTime(date2);
            //date2-date1
            TimeSpan dteTOdts = dte.Subtract(dts);
            return dteTOdts.Days+1;
        }

       /// <summary>更新文件信息
       /// 
       /// </summary>
       /// <param name="strfilepath"></param>
       /// <param name="textall"></param>
        public void WriterFile(string strfilepath, string textall)
        {
            FileStream fs = new FileStream(strfilepath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(textall);
            sw.Close();
            sw.Dispose();
            fs.Close();
            fs.Dispose();
        }

        /// <summary>记录操作LOG
        /// 
        /// </summary>
        /// <param name="strfilepath"></param>
        /// <param name="day"></param>
        public void WriterLog(int day)
        {
            FileStream fs = new FileStream(strLogPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            string str = EncryptDES(DateTime.Now.ToShortDateString() + ";  " + day.ToString(), DESkeys);
            sw.WriteLine(str);
            sw.Close();
            sw.Dispose();
            fs.Close();
            fs.Dispose();
        }

        /// <summary>记录错误记录
        /// 
        /// </summary>
        /// <param name="str"></param>
        public void WriterErrLog(string str)
        {
            FileStream fs = new FileStream(strErrPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(str);
            sw.Close();
            sw.Dispose();
            fs.Close();
            fs.Dispose();
        }

        /// <summary>检查授权文件
        /// 
        /// </summary>
        /// <param name="msno"></param>
        /// <returns></returns>
        public bool CheckFile(string msno)
        {
            bool flag = false;
            string datenow = DateTime.Now.ToShortDateString();
            string textall = string.Empty;
            string strDate = DateTime.Now.ToShortDateString();
            string[] arr = ReadFile(strFilePath);
            newclass nc = new newclass();
            nc.Dates = arr[0];
            nc.Datee = arr[1];
            nc.Type = arr[2];
            nc.Days = arr[3];
            nc.Day = arr[4];
            nc.Needcheck = arr[5];
            //判断是否是授权文件
            if (nc.Needcheck.Equals("1"))
            {
                //判断型号是否对
                if (nc.Type.ToLower().Equals(msno))
                {
                    //判断系统时间是否>=出厂时间
                    if (CheckDate(nc.Dates, datenow))
                    {
                        //判断系统时间是否<=授权时间
                        if (CheckDate(datenow, nc.Datee))
                        {
                            //判断时间调整是否是往前调
                            if (Usedday(nc.Dates, datenow) >= int.Parse(nc.Day))
                            {
                                nc.Day = Convert.ToString(Usedday(nc.Dates, datenow));
                                textall = nc.Dates + str + nc.Datee + str + nc.Type + str + nc.Days + str + nc.Day + str + nc.Needcheck;
                                textall = EncryptDES(textall, DESkeys);
                                textall = StringXor(textall, UnDefine);
                                WriterFile(strFilePath, textall);
                                WriterLog(int.Parse(nc.Day));
                                flag = true;
                                
                            }
                            else
                            {
                                strDate = strDate + ":时间不能往前调";
                                WriterErrLog(strDate);
                            }
                        }
                        else
                        {
                            nc.Day = Convert.ToString(Usedday(nc.Dates, datenow));
                            textall = nc.Dates + str + nc.Datee + str + nc.Type + str + nc.Days + str + nc.Day + str + nc.Needcheck;
                            textall = EncryptDES(textall, DESkeys);
                            textall = StringXor(textall, UnDefine);
                            WriterFile(strFilePath, textall);
                            strDate = strDate + ":系统时间超出授权时间";
                            WriterErrLog(strDate);
                        }
                    }
                    else
                    {
                        strDate = strDate + ":系统时间错误，不在出厂日期跟授权日期内";
                        WriterErrLog(strDate);
                    }
                }
                else
                {
                    strDate = strDate + ":互调仪型号不对";
                    WriterErrLog(strDate);
                }  
            }
            else
            {
                if (nc.Type.ToLower().Equals(msno))
                {
                    flag = true;
                }
                else
                {
                    strDate = strDate + ":互调仪型号不对";
                    WriterErrLog(strDate);
                }
            }
            return flag;
        }

        #endregion

        #region 创建KEY文件夹
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool CreatFolder()
        {
            bool rev = false;
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
                FileStream fs = new FileStream(filePath + "\\Err.txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
                FileStream fs1 = new FileStream(filePath + "\\Log.txt", FileMode.Create);
                StreamWriter sw1 = new StreamWriter(fs1);
                sw1.Close();
                sw1.Dispose();
                fs1.Close();
                fs1.Dispose();
                rev = true;
            }
            else
            {
                if (File.Exists(filePath + "\\key"))
                {
                    File.Delete(filePath + "\\key");
                }
                if (File.Exists(filePath + "\\Err.txt"))
                {
                    File.Delete(filePath + "\\Err.txt");
                }
                FileStream fs = new FileStream(filePath + "\\Err.txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();

                if (File.Exists(filePath + "\\Log.txt"))
                {
                    File.Delete(filePath + "\\Log.txt");
                }
                FileStream fs1 = new FileStream(filePath + "\\Log.txt", FileMode.Create);
                StreamWriter sw1 = new StreamWriter(fs1);
                sw1.Close();
                sw1.Dispose();
                fs1.Close();
                fs1.Dispose();
                rev = true;
            }
            return rev;
        }
        #endregion

    }
}
