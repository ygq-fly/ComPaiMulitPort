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
        #region ����

        // �Զ������Կ
        private const string strPublicKey = "0BDFC73BC56346CC";

        // �����ļ�����ļ����ַ���
        private string strCoder = string.Empty;

        #endregion


        #region ���幹��
        /// <summary>
        /// ���幹��
        /// </summary>
        public SystemLock()
        {
            InitializeComponent();
        }

        #endregion


        #region �������
        /// <summary>
        /// �������
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


        #region �¼�

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


        #region ����

        /// <summary>
        /// ���ܺ�����ʹ�ù�����Կ
        /// </summary>
        /// <param name="_source">��Ҫ���ܵ��ַ���</param>
        /// <returns>���ؼ��ܺõĴ�</returns>
        public string EncryptStr(string _source)
        {
            return EncryptStr(_source, strPublicKey);
        }

        /// <summary>
        /// ���ܺ�����ʹ�ù�����Կ
        /// </summary>
        /// <param name="password">Ҫ���ܵ�����</param>
        /// <returns>���ؽ��ܺ���ַ���</returns>
        public string DecryptStr(string _source)
        {
            return DecryptStr(_source, strPublicKey);
        }

        /// <summary>
        /// ���ܺ�����ʹ���Զ�����Կ
        /// </summary>
        /// <param name="password">��Ҫ���ܵ��ַ���</param>
        /// <param name="keyiv">��Կ��16λ�ַ���</param>
        /// <returns>���ؼ��ܺõĴ�</returns>
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
        /// ���ܺ�����ʹ���Զ�����Կ
        /// </summary>
        /// <param name="password">Ҫ���ܵ�����</param>
        /// <param name="keyiv">��Կ��16λ�ַ���</param>
        /// <returns>���ؽ��ܺ���ַ���</returns>
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