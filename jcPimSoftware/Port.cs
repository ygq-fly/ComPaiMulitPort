using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace jcPimSoftware
{
    public partial class Port : Form
    {
        int sel = 0;
        public Port(int num)
        {
            InitializeComponent();
            sel = num;
        }
        public int selectindex = 1;
        private void SetPort(int num)
        {
            switch (num)
            {
                case 0: radioButton1.Checked = true; break;
                case 1: radioButton2.Checked = true; break;
                case 2: radioButton3.Checked = true; break;
                case 3: radioButton4.Checked = true; break;
                case 4: radioButton5.Checked = true; break;
                case 5: radioButton6.Checked = true; break;
                case 6: radioButton7.Checked = true; break;
                case 7: radioButton8.Checked = true; break;
            }
        }
        private void SetControl(int num)
        {
            switch (num)
            {
                case 0: radioButton1.Enabled = true; break;
                case 1: radioButton2.Enabled = true; break;
                case 2: radioButton3.Enabled = true; break;
                case 3: radioButton4.Enabled = true; break;
                case 4: radioButton5.Enabled = true; break;
                case 5: radioButton6.Enabled = true; break;
                case 6: radioButton7.Enabled = true; break;
                case 7: radioButton8.Enabled = true; break;
            }
        }
        private void ControlEnable()
        {
            for (int i = 0; i <Convert.ToInt32(App_Configure.Cnfgs.Ms_switch_port_count); i++)
            {
                SetControl(i);
            }
        }

        private void  GetPort()
        {
            if (radioButton1.Checked)
                selectindex = 0;
            else if (radioButton2.Checked)
                selectindex = 1;
            else if (radioButton3.Checked)
                selectindex = 2;
            else if (radioButton4.Checked)
                selectindex = 3;
            else if (radioButton5.Checked)
                selectindex = 4;
            else if (radioButton6.Checked)
                selectindex = 5;
            else if (radioButton7.Checked)
                selectindex = 6;
            else 
                selectindex = 7;      
        }
        private void Port_Load(object sender, EventArgs e)
        {
            SetPort(sel);
            ControlEnable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetPort();
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {


        }

       
    }
}
