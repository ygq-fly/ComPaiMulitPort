using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;

namespace jcPimSoftware
{
    
    public partial class SqlLogin : Form
    {
        SqlInfo si;
        private static byte[] Keys = { 0x01, 0x09, 0x08, 0x07, 0x05, 0x04, 0xAB, 0xCD };
        private static string encryptKey = "jointcom"; 

        public SqlLogin(SqlInfo si)
        {
            this.si = si;
            InitializeComponent();
        }

        private void SqlLogin_Load(object sender, EventArgs e)
        {
            cB_sqlServerType.SelectedIndex = 0;
            tB_Tester.Text = IniFile.GetString("sqlinfo", "operator", "test", Application.StartupPath + "\\SqlInfo.ini");
            tB_sqladdr.Text = IniFile.GetString("sqlinfo", "sqladdr", "0.0.0.0", Application.StartupPath + "\\SqlInfo.ini");
            tB_sqldatabase.Text = IniFile.GetString("sqlinfo", "sqlname", "pim-datebase", Application.StartupPath + "\\SqlInfo.ini");
            tB_sqluser.Text = IniFile.GetString("sqlinfo", "sqluser", "sa", Application.StartupPath + "\\SqlInfo.ini");

            if (IniFile.GetString("sqlinfo", "deson", "0", Application.StartupPath + "\\SqlInfo.ini") == "1")
            {
                checkBox1.Checked = true;
                string str_sql = IniFile.GetString("sqlinfo", "sqlpw", "123", Application.StartupPath + "\\SqlInfo.ini");
                str_sql = DecryptDES(str_sql);
                if (str_sql != "Error")
                    tB_sqlpassward.Text = str_sql;

                string str_ftp = IniFile.GetString("sqlinfo", "ftppw", "123", Application.StartupPath + "\\SqlInfo.ini");
                str_ftp = DecryptDES(str_ftp);
                if (str_ftp != "Error")
                    tB_ftppw.Text = str_ftp;
            }

            tB_ftpaddr.Text = IniFile.GetString("sqlinfo", "ftpaddr", "0.0.0.0:21", Application.StartupPath + "\\SqlInfo.ini");
            tB_ftpuser.Text = IniFile.GetString("sqlinfo", "ftpuser", "san", Application.StartupPath + "\\SqlInfo.ini");
            
        }

        string device = "jc" + App_Configure.Cnfgs.SN;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                si.ftpaddr = tB_ftpaddr.Text;
                si.ftpuser = tB_ftpuser.Text;
                si.ftppw = tB_ftppw.Text;

                si.sqladdr = tB_sqladdr.Text;
                si.sqlname = tB_sqldatabase.Text;
                si.sqluser = tB_sqluser.Text;
                si.sqlpw = tB_sqlpassward.Text;
                si.PimOperator = tB_Tester.Text;
                si.ftpaddr = tB_ftpaddr.Text;

                #region 尝试连接数据库，并检查数据库
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=" + tB_sqladdr.Text + ";Initial Catalog=" + tB_sqldatabase.Text + ";User Id=" + tB_sqluser.Text + ";Password=" + tB_sqlpassward.Text;
                conn.Open();

                string sql = "select count(1) from sys.objects where name='" + device + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                int n = Convert.ToInt32(cmd.ExecuteScalar());
                if (n == 0)//不存在表
                {
                    sql = "create table " + device + "("
                    + "SN char(20) not null,"
                    + "Type nchar(20),"
                    + "Op nchar(20) not null,"
                    + "Pim char(20) not null,"
                    + "Result char(20) not null,"
                    + "Fpim char(20) not null,"
                    + "\"Power\" char(20) not null,"
                    + "Mode char(20) not null,"
                    + "\"Order\" char(20) not null,"
                    + "Band char(20) not null,"
                    + "Limit char(20) not null,"
                    + "Time char(20) not null,"
                    + "Remark char(20) not null)";
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteScalar();

                    //MessageBox.Show("新建数据库:" + device);
                }
                else
                {
                    sql = "select count(name) from syscolumns where id=object_id('" + device + "')";
                    cmd = new SqlCommand(sql, conn);
                    int i = Convert.ToInt32(cmd.ExecuteScalar());
                    if (i == 11)
                    {
                        sql = "alter table " + device + " add Time char(20)";
                        cmd = new SqlCommand(sql, conn);
                        cmd.ExecuteScalar();
                        sql = "alter table " + device + " add Remark char(20)";
                        cmd = new SqlCommand(sql, conn);
                        cmd.ExecuteScalar();
                    }
                }

                conn.Close();
                #endregion

                IniFile.SetString("sqlinfo", "sqladdr", si.sqladdr, Application.StartupPath + "\\SqlInfo.ini");
                IniFile.SetString("sqlinfo", "sqlname", si.sqlname, Application.StartupPath + "\\SqlInfo.ini");
                IniFile.SetString("sqlinfo", "sqluser", si.sqluser, Application.StartupPath + "\\SqlInfo.ini");
                IniFile.SetString("sqlinfo", "operator", si.PimOperator, Application.StartupPath + "\\SqlInfo.ini");
                IniFile.SetString("sqlinfo", "ftpaddr", si.ftpaddr, Application.StartupPath + "\\SqlInfo.ini");
                IniFile.SetString("sqlinfo", "ftpuser", si.ftpuser, Application.StartupPath + "\\SqlInfo.ini");
                if (checkBox1.Checked)
                {
                    IniFile.SetString("sqlinfo", "deson", "1", Application.StartupPath + "\\SqlInfo.ini");
                    string mn = EncryptDES(si.sqlpw);
                    if (mn != "Error")
                        IniFile.SetString("sqlinfo", "sqlpw", mn, Application.StartupPath + "\\SqlInfo.ini");

                    string mn2 = EncryptDES(si.ftppw);
                    if(mn2!="Error")
                        IniFile.SetString("sqlinfo", "ftppw", mn2, Application.StartupPath + "\\SqlInfo.ini");
                }
                else
                {
                    IniFile.SetString("sqlinfo", "deson", "0", Application.StartupPath + "\\SqlInfo.ini");
                }
                    

                MessageBox.Show("连接成功!");

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接失败: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public static string EncryptDES(string encryptString)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return "Error";
            }
        }

        public static string DecryptDES(string decryptString)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey);
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
                return "Error";
            }
        }
        
    }

    public class SqlInfo
    {
        public string PimOperator = "test";
        public string sqladdr = "127.0.0.1,1433";
        public string sqlname = "Jointcom";
        public string sqluser = "sa";
        public string sqlpw = "";
        public string ftpaddr = "";
        public string ftpuser = "";
        public string ftppw = "";
    }
}