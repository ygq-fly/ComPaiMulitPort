using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace jcPimSoftware
{
    public partial class F1Reset : Form
    {

        int n = 0;
        float f1s;
        float f1e;
        float step;
        float p1;
        #region 构造函数
        public F1Reset(int i,float f1s,float f1e,float p1,float step)
        {
            n = i;
            this.f1s = f1s;
            this.f1e = f1e;
            this.p1 = p1;
            this.step = step;
            InitializeComponent();
        }
        #endregion

        #region 窗体事件
        private void F1Reset_Load(object sender, EventArgs e)
        {
            if (n == (int)SweepType.Freq_Sweep)
            {
                nudFreE.Enabled = true;
                nudStep.Enabled = true;
            }
            else
            {
                nudFreE.Enabled = false;
                nudStep.Enabled = false;
            }
            nudFre.Value = Convert.ToDecimal(f1s);
            nudFreE.Value = Convert.ToDecimal(f1e);
            nudStep.Value = Convert.ToDecimal(step);
            nudPower.Value = Convert.ToDecimal(p1);

            lblFre.Text = App_Settings.sgn_1.Min_Freq.ToString("0.0") + "MHz-" +
                            App_Settings.sgn_1.Max_Freq.ToString("0.0") + "MHz";
            lblPower.Text = App_Settings.sgn_1.Min_Power.ToString("0.0") + "dBm-" +
                            App_Settings.sgn_1.Max_Power.ToString("0.0") + "dBm";
        }
        #endregion

        #region 按钮事件
        private void nub_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            NumericUpDown number = (NumericUpDown)sender;
            TouchPad((NumericUpDown)sender, number.Value.ToString());
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (nudFre.Value > nudFreE.Value && n == 1)
            {
                MessageBox.Show(this, "End range value cannot be less than the starting range value!");
                nudFre.Value = Convert.ToDecimal(f1s);
                nudFreE.Value = Convert.ToDecimal(f1e);
                nudStep.Value = Convert.ToDecimal(step);
                nudPower.Value = Convert.ToDecimal(p1);
            }
            else
            {
                string str = string.Empty;
                if (n != 1)
                {
                    if (nudFre.Value < Convert.ToDecimal(App_Settings.sgn_1.Min_Freq) || nudFre.Value > Convert.ToDecimal(App_Settings.sgn_2.Max_Freq))
                    {
                        str += "Frequency setting error!\r\n";
                    }
                }
                else
                {
                    if (nudFre.Value < Convert.ToDecimal(App_Settings.sgn_1.Min_Freq) || nudFreE.Value > Convert.ToDecimal(App_Settings.sgn_1.Max_Freq))
                    {
                        str += "Frequency setting error!\r\n";
                    }
                }

                if (nudPower.Value < Convert.ToDecimal(App_Settings.sgn_1.Min_Power) || nudPower.Value > Convert.ToDecimal(App_Settings.sgn_1.Max_Power))
                {
                    str += "Power setting error!\r\n";
                }
                if (Convert.ToDecimal(nudFreE.Value - nudFre.Value) < nudStep.Value && n == 0)
                {
                    str += "Step setting error!\r\n";
                }
                if (str == string.Empty)
                    this.DialogResult = DialogResult.OK;
                else
                {
                    MessageBox.Show(this, str);
                    nudFre.Value = Convert.ToDecimal(f1s);
                    nudFreE.Value = Convert.ToDecimal(f1e);
                    nudStep.Value = Convert.ToDecimal(step);
                    nudPower.Value = Convert.ToDecimal(p1);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion

        #region TouchPad
        /// <summary>
        /// TouchPad
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="text"></param>
        private void TouchPad(NumericUpDown n, string text)
        {
            TouchPad testTouchPad = new TouchPad(ref n, text);
            testTouchPad.ShowDialog();
        }
        #endregion
    }
}