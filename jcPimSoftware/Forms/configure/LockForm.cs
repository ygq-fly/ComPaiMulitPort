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
    public partial class LockForm : Form
    {
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, int bRevert);
        [DllImport("User32.dll")]
        public static extern bool EnableMenuItem(IntPtr hMenu, int uIDEnableItem, int uEnable);

        #region 变量定义

        // 自定义的密钥
        private const string strPublicKey = "0BDFC73BC56346CC";

        // 配置文件保存的加密字符串
        private string strCoder = string.Empty;

        private const int SC_CLOSE = 0xF060;
        private const int MF_ENABLED = 0x00000000;
        private const int MF_GRAYED = 0x00000001;
        private const int MF_DISABLED = 0x00000002;

        #endregion


        #region 窗体构造
        /// <summary>
        /// 窗体构造
        /// </summary>
        public LockForm()
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
        private void LockForm_Load(object sender, EventArgs e)
        {
            strCoder = App_Configure.Cnfgs.Password;
            IntPtr hMenu = GetSystemMenu(this.Handle, 0);
            EnableMenuItem(hMenu, SC_CLOSE, MF_DISABLED | MF_GRAYED);
        }

        #endregion


        #region 事件
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            string strtbx;
            string strDecoder;

            strtbx = tbxPassWord.Text.Trim();
            strDecoder = DecryptStr(strCoder);

            if (strtbx.Equals(strDecoder))
            {
                this.Close();
            }
            else
            {
                lblInfo.Text = "Password error!";
            }
        }

        #endregion


        #region 方法

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

        private void tbxPassWord_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TouchPad tp = new TouchPad(ref tbxPassWord, tbxPassWord.Text.Trim(), true);
            tp.StartPosition = FormStartPosition.CenterScreen;
            tp.ShowDialog();
        }

        #endregion 


        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_NOCLOSE = 0x200;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CS_NOCLOSE;
                return cp;
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x84 && m.Result == (IntPtr)2) // 不让拖动标题栏
            {
                m.Result = (IntPtr)1;
            }
            if (m.Msg == 0xA3)                         // 双击标题栏无反应
            {
                m.WParam = System.IntPtr.Zero;
            }
        }
    }
}