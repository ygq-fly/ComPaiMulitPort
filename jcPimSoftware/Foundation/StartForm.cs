using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace jcPimSoftware
{
    public partial class StartForm : Form
    {
        private string msg = "";
        public string infoMsg = "";
        public bool Complete = false;
        public int status = 1;
        public static ManualResetEvent mm = new ManualResetEvent(false);
        public SpectrumLib.ISpectrum ISpectrumObj;
        public StartForm(string info)
        {
            InitializeComponent();
            this.msg = info;
            this.lblAc.Text = msg + "\r\n";
        }
        
        private void FormProgress_Load(object sender, EventArgs e)
        {
            if (msg.Equals("Closing......"))
            {
                this.pgbBar.Step = 2;
            }
            timer.Start();
        }

        public void GetInfoMation(string info)
        {
            infoMsg += info + " \r\n";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.pgbBar.PerformStep();
            this.lblInfo.Text = infoMsg;
            if (this.pgbBar.Value == this.pgbBar.Maximum)
            {
                if (status == 0)
                {
                    lblErrorInfo.Text = "Some hardware error,Press 'ok' to continue or restart system to try again!";
                    btnOK.Visible = true;
                }
                else
                {
                    Program.mre.Set();
                    timer.Stop();
                    this.Close();
                   
                }
                if (!msg.Equals("Closing......"))
                    btnOK.Visible = true;
                else
                {
                    if (Complete)
                    {
                        Program.mre.Set();
                        timer.Stop();
                        this.Close();
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Program.mre.Set();
            timer.Stop();
            this.Close();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == MessageID.SF_WAIT)
            {
                timer.Interval = 200;
            }
            if (m.Msg == MessageID.SF_CONTINUTE)
            {
                timer.Interval = 40;
            }
            if (m.Msg == MessageID.SPECTRUEME_SUCCED)
            {
                ISpectrumObj.GetSpectrumData();
            }
        }
    }
}