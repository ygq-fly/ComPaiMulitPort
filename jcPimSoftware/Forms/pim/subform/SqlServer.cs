using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace jcPimSoftware
{
    public partial class SqlServer : Form
    {
        internal SqlServer(ProductInfo pi, SqlInfo si, delegate_SavePDF d_SavePdf)
        {
            InitializeComponent();
            this.pi = pi;
            this.si = si;
            this.del_SavePdf = d_SavePdf;
        }

        string band;
        SqlInfo si;
        ProductInfo pi;
        delegate_SavePDF del_SavePdf;

        private void SqlServer_Load(object sender, EventArgs e)
        {
            tB_Tester.Text = pi.tester;
            tB_JcID.Text = pi.device;
            tB_Type.Text = pi.type;
            try
            {
                band = pi.device.Substring(2, 4);
            }
            catch
            {
                band = pi.device;
            }

            lbl_Peak.Text = "Peak: " + pi.pim + "dBm/" + pi.result;
            lbl_Value.Text = "@" + pi.fpim + "MHz/"
                + pi.power + "/"
                + pi.mode + "/"
                + pi.order + "/"
                + band + "\r\n"
                + "Limit:" + pi.limit + "dBm";
            if (pi.savepdf == "False")
                cb_UpLoad.Checked = false;
            else
                cb_UpLoad.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tB_Serial.Text == "")
            {
                MessageBox.Show("产品序列号不能为空!");
                return;
            }

            pi.sn = tB_Serial.Text;
            pi.type = tB_Type.Text;
            pi.time = DateTime.Now.ToString();

            if (cb_UpLoad.Checked)
            {
                try
                {
                    string path = App_Configure.Cnfgs.Path_Rpt_Pim + "\\" + "pdf\\" + pi.sn + "_" + pi.device + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".pdf";
                    if (!del_SavePdf(path))
                    {
                        MessageBox.Show("save faile");
                        return;
                    }
                    FtpWeb ftp = new FtpWeb(si.ftpaddr, "", si.ftpuser, si.ftppw);
                    ftp.Upload(path);
                    pi.savepdf = "True";
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }  

            try
            {
                SqlConnection conn = new SqlConnection();
                //conn.ConnectionString = "Data Source=SAN-PC;Initial Catalog=Jointcom;User Id=sa;Password=sa";
                conn.ConnectionString = "Data Source=" + si.sqladdr + ";Initial Catalog=" + si.sqlname + ";User Id=" + si.sqluser + ";Password=" + si.sqlpw;
                conn.Open();

                string sql = "insert into " + pi.device + " (SN,Type,Op,Pim,Result,Fpim,\"Power\",Mode,\"Order\",Band,Limit,Time,Remark) "
                        + "values('"
                        + pi.sn + "','"
                        + pi.type + "','"
                        + pi.tester + "','"
                        + pi.pim + "','"
                        + pi.result + "','"
                        + pi.fpim + "','"
                        + pi.power + "','"
                        + pi.mode + "','"
                        + pi.order + "','"
                        + band + "','"
                        + pi.limit + "','"
                        + pi.time + "','"
                        + pi.savepdf + "')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                int n = cmd.ExecuteNonQuery();

                conn.Close();               
                MessageBox.Show("Success!");
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接失败：" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void tB_Serial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }

    }

    public delegate bool delegate_SavePDF(string str);

    public class ProductInfo
    {
        public string sn = "Test123";
        public string type = "Null";
        public string tester = "TestName";        

        public string pim = "-165.0";
        public string result = "PASS";
        public string fpim = "960.0";
        public string power = "43";
        public string mode = "";
        public string order = "";
        public string limit = "";
        public string device = "JcXXXX";
        public string savepdf = "False";
        public string time = "0000";
    }
}