using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace jcPimSoftware.Foundation
{
    public partial class FormProgress : Form
    {
        private string msg = "";
        public int status = 0;
        public FormProgress(string info)
        {
            InitializeComponent();
            this.msg = info;
            this.lblAc.Text = msg + "\r\n";
        }
        
        private void FormProgress_Load(object sender, EventArgs e)
        {
            if (msg.Equals("Closeing......"))
            {
                this.pgbBar.Step = 2;
                this.timer.Interval = 50;
            }
            timer.Start();
        }
        public void GetInfoMation(string info)
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.lblInfo.Text += info + " \r\n";
            });
            Thread.Sleep(600);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.pgbBar.PerformStep();
            if (this.pgbBar.Value == this.pgbBar.Maximum)
            {
                if (status == 2)
                    lblErrorInfo.Text = "Some hardware error,Press 'ok' to continue or restart system to try again!";
                if (!msg.Equals("Closeing......"))
                {
                    btnOK.Visible = true;
                }
                else
                {
                    timer.Stop();
                    this.Close();
                }
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            timer.Stop();
            this.Close();
        }
    }
}