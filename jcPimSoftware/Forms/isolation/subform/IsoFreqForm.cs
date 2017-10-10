using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace jcPimSoftware
{
    public partial class IsoFreqForm : Form
    {
        private float value;
        internal float Value
        {
            get { return value; }
        }

        public IsoFreqForm(float value)
        {
            InitializeComponent();

            tbxValue.Text = value.ToString("0.0");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            value = float.Parse(tbxValue.Text);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region TouchPad
        private void tbxTxt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string[] txt = sender.ToString().Split(char.Parse(":"));
            TouchPad((TextBox)sender, txt[1]);
        }

        /// <summary>
        /// TouchPad
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="text"></param>
        private void TouchPad(TextBox textBox, string text)
        {
            TouchPad testTouchPad = new TouchPad(ref textBox, text);
            testTouchPad.ShowDialog();
        }
        #endregion
    }
}