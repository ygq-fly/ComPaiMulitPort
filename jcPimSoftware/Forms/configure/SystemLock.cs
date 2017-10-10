using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.InteropServices;

namespace jcPimSoftware
{
    public partial class SystemLock : Form
    {
        #region 定义

        // 自定义的密钥
        private const string strPublicKey = "0BDFC73BC56346CC";

        // 配置文件保存的加密字符串
        private string strCoder = string.Empty;

        #endregion


        #region 窗体构造
        /// <summary>
        /// 窗体构造
        /// </summary>
        public SystemLock()
        {
            InitializeComponent();
        }

        #endregion


        #region 窗体加载
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemLock_Load(object sender, EventArgs e)
        {
            strCoder = App_Configure.Cnfgs.Password;
            lblwarnning.Text = "";
            groupReset.Enabled = false;
        }

        #endregion


        #region 事件

        private void btnLock_Click(object sender, EventArgs e)
        {
            string strdecoder;
            string strpsd;
            strdecoder = tbxLockPsd.Text.Trim();
            strpsd = DecryptStr(strCoder);

            if (strdecoder.Equals(strpsd))
            {
                this.Hide();
                LockForm lockfrm = new LockForm();
                lockfrm.ShowDialog();
                this.Close();
            }
            else
            {
                lblInfo.Text = "Password error!";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkReset_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReset.Checked)
                groupReset.Enabled = true;
            else
                groupReset.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string strdecoder;
            string strold;
            string strnew;
            string stragain;

            strdecoder = DecryptStr(strCoder);
            strold = tbxOld.Text.Trim();
            strnew = tbxNew.Text.Trim();
            stragain = tbxAgain.Text.Trim();

            if (strnew != stragain)
            {
                lblwarnning.Text = "Please enter the same password twice";
                return;
            }
            if (strold != strdecoder)
            {
                lblwarnning.Text = "The password is incorrect!";
                return;
            }

            lblwarnning.Text = "The password is changed!";

            App_Configure.Cnfgs.Password = EncryptStr(strnew);
            App_Configure.Cnfgs.StoreSettings();
            strCoder = App_Configure.Cnfgs.Password;
        }

        #endregion


        #region 方法

        /// <summary>
        /// 加密函数，使用公共密钥
        /// </summary>
        /// <param name="_source">需要加密的字符串</param>
        /// <returns>返回加密好的串</returns>
        public string EncryptStr(string _source)
        {
            return EncryptStr(_source, strPublicKey);
        }

        /// <summary>
        /// 解密函数，使用公共密钥
        /// </summary>
        /// <param name="password">要解密的内容</param>
        /// <returns>返回解密后的字符串</returns>
        public string DecryptStr(string _source)
        {
            return DecryptStr(_source, strPublicKey);
        }

        /// <summary>
        /// 加密函数，使用自定义密钥
        /// </summary>
        /// <param name="password">需要加密的字符串</param>
        /// <param name="keyiv">密钥，16位字符串</param>
        /// <returns>返回加密好的串</returns>
        public string EncryptStr(string _source, string _key)
        {
            string strSource, strKey;
            strKey = _key.Substring(0, 8);
            Byte[] byKey = ASCIIEncoding.ASCII.GetBytes(strKey);
            strKey = _key.Substring(8, 8);
            Byte[] byIV = ASCIIEncoding.ASCII.GetBytes(strKey);
            DES objDES = new DESCryptoServiceProvider();
            objDES.Key = byKey;
            objDES.IV = byIV;
            byte[] byInput = Encoding.Default.GetBytes(_source);
            MemoryStream objMemoryStream = new MemoryStream();
            CryptoStream objCryptoStream = new CryptoStream(objMemoryStream, objDES.CreateEncryptor(), CryptoStreamMode.Write);
            objCryptoStream.Write(byInput, 0, byInput.Length);
            objCryptoStream.FlushFinalBlock();
            StringBuilder objStringBuilder = new StringBuilder();
            foreach (byte b in objMemoryStream.ToArray())
            {
                objStringBuilder.AppendFormat("{0:X2}", b);
            }
            strSource = objStringBuilder.ToString();
            objCryptoStream.Close();
            objMemoryStream.Close();
            return strSource.ToString();
        }

        /// <summary>
        /// 解密函数，使用自定义密钥
        /// </summary>
        /// <param name="password">要解密的内容</param>
        /// <param name="keyiv">密钥，16位字符串</param>
        /// <returns>返回解密后的字符串</returns>
        public string DecryptStr(string _source, string _key)
        {
            string strSource, strKey;
            try
            {
                strKey = _key.Substring(0, 8);
                Byte[] bytKey = ASCIIEncoding.ASCII.GetBytes(strKey);
                strKey = _key.Substring(8, 8);
                Byte[] bytIV = ASCIIEncoding.ASCII.GetBytes(strKey);
                DES objDES = new DESCryptoServiceProvider();
                objDES.Key = bytKey;
                objDES.IV = bytIV;
                byte[] bytInputByteArray = new byte[_source.Length / 2];
                for (int x = 0; x < (_source.Length / 2); x++)
                {
                    int i = (Convert.ToInt32(_source.Substring(x * 2, 2), 16));
                    bytInputByteArray[x] = (byte)i;
                }
                MemoryStream objMemoryStream = new MemoryStream();
                CryptoStream objCryptoStream = new CryptoStream(objMemoryStream, objDES.CreateDecryptor(), CryptoStreamMode.Write);
                objCryptoStream.Write(bytInputByteArray, 0, bytInputByteArray.Length);
                objCryptoStream.FlushFinalBlock();
                strSource = Encoding.Default.GetString(objMemoryStream.ToArray());
                objMemoryStream.Close();
            }
            catch
            {
                strSource = "Key Error...";
            }
            return strSource;
        }

        #endregion


        #region TouchPad

        private void tbxLockPsd_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad tp = new TouchPad(ref tbxLockPsd, tbxLockPsd.Text.Trim(), true);
            tp.ShowDialog();
        }

        private void tbxOld_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad tp = new TouchPad(ref tbxOld, tbxOld.Text.Trim(), true);
            tp.ShowDialog();
        }

        private void tbxNew_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad tp = new TouchPad(ref tbxNew, tbxNew.Text.Trim(), true);
            tp.ShowDialog();
        }

        private void tbxAgain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad tp = new TouchPad(ref tbxAgain, tbxAgain.Text.Trim(), true);
            tp.ShowDialog();
        }

        #endregion
    }
}