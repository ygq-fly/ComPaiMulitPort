using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace jcPimSoftware
{
    //enum 

    public partial class TouchPad : Form
    {
        public TouchPad(string value)
        {
            InitializeComponent();
            this._value = value;
        }

        public TouchPad(ref TextBox tb, string defValue)
            : this(defValue)
        {
            //InitializeComponent();

            //this._value = defValue;

            _ObjTd = tb;
        }

        public TouchPad(ref TextBox tb, string defValue, bool flag)
            : this(defValue)
        {
            //InitializeComponent();

            //this._value = defValue;

            if (flag)
                txtBox.PasswordChar = '*';

            _ObjTd = tb;

        }

        public TouchPad(ref NumericUpDown Num, string defValue)
            : this(defValue)
        {
            //InitializeComponent();

            //this._value = defValue;

            _ObjNum = Num;
        }

        #region �ֶζ���

        //��ȡ��ǰֵ����
        public string _value = string.Empty; 

        public string _firTxt="a";  
        public string _sndTxt ="b";
        public string _thirdTxt ="c";
        StringBuilder sb;

        private TextBox _ObjTd;
        private NumericUpDown _ObjNum;
        
        #endregion

        private void ButtonOnClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string data = this.btnState.Text;
            string tag = btn.Tag.ToString();

            switch (tag)
            {
                //��ť��tag����
                //state��ʱ�������л�����ģʽ
                //back��ʱ��ִ��ɾ��
                //clear��ʱ��ִ�����
                
                #region
                case "state":
                    this.btnState.Text = changeState(data);
                    chooseState(this.btnState.Text);
                    break;
                case "back":
                    string strTxt = txtBox.Text.Trim();
                    if (strTxt != "")
                    {
                        string newStr = strTxt.Remove(strTxt.Length - 1, 1);
                        if (sb.Length > 0)
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }
                        txtBox.Text = newStr;
                    }
                    break;
                case "clear":
                    txtBox.Text = "";
                    sb.Remove(0, sb.Length);
                    break;
                default:
                    if (this.btnState.Text.Equals("123"))
                    {
                        if (tag.Equals("."))
                        {
                            #region �����룬�����ǰ�ı���Ϊ�վ�����" 0. ",�����Ϊ�վ��ж��Ƿ��Ѿ�����С�����ˣ����˾Ͳ������롣

                            if (txtBox.Text.Trim() == "")
                            {
                                sb.Append("0.");
                                txtBox.Text = sb.ToString();
                            }
                            else
                            {
                                //���ж��Ƿ���С����
                                sb.Remove(0, sb.Length);
                                string str = txtBox.Text.Trim();
                                sb.Append(str);
                                bool desc = false;
                                for (int i = 0; i < str.Length; i++)
                                {
                                    if (str.Substring(i, 1).Equals("."))
                                    {
                                        desc = true;
                                        break;
                                    }
                                }
                                //�����������С����
                                if (!desc)
                                {
                                    sb.Append(".");
                                    txtBox.Text = sb.ToString();
                                }
                            }

                            #endregion
                        }
                        else
                        {
                            if (!tag.Equals("v"))
                                sb.Append(btn.Text.Substring(0, 1));
                        }
                    }
                    else
                    {
                        //���ּ����ַ���ȡ��������
                        if (!tag.Equals("v"))
                            checkValue(btn.Text, data);
                        else
                            sb.Append(btn.Text);
                    }
                    this.txtBox.Text = sb.ToString();
                    break;

                #endregion
            }
        }

        /// <summary>
        /// �л����֡�Ӣ���Լ���С������
        /// </summary>
        /// <param name="state">״ָ̬ʾ�ַ�</param>
        /// <returns></returns>
        private string changeState(string state)
        {
            switch (state)
            {
                case "123": state = "abc"; break;
                case "abc": state = "ABC"; break;
                case "ABC": state = "123"; break;
            }
            return state;
        }

        /// <summary>
        /// �޸ĵ�����弸����ť��ֵ
        /// </summary>
        private void changeValue(string txtValue)
        {
            _firTxt = txtValue.Substring(txtValue.Length-3, 1);
            _sndTxt = txtValue.Substring(txtValue.Length-2, 1);
            _thirdTxt = txtValue.Substring(txtValue.Length-1, 1);

        }

        /// <summary>
        /// ���ò���ַ���ѡ��ģʽ����
        /// </summary>
        /// <param name="tagValue"></param>
        public void checkValue(string tagValue,string style)
        {
            changeValue(tagValue);
            chooseState(style);

        }

        /// <summary>
        /// ���ݵ�ǰ�л���״̬���и�ֵ
        /// </summary>
        /// <param name="value"></param>
        public void chooseState(string value)
        {
            switch (value)
            {
                case "abc":
                    this.btn1.Text = _firTxt;
                    this.btn2.Text = _sndTxt;
                    this.btn3.Text = _thirdTxt;
                    break;
                case "ABC":
                    this.btn1.Text = _firTxt.ToUpper();
                    this.btn2.Text = _sndTxt.ToUpper();
                    this.btn3.Text = _thirdTxt.ToUpper();
                    break;
            }
        }

        private void Demo_Load(object sender, EventArgs e)
        {
            picX.Image = ImagesManage.GetImage("ico", "x.gif");
            this.txtBox.Text = _value;
            sb = new StringBuilder();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void enterBtn_Click(object sender, EventArgs e)
        {

            try
            {
                if (_ObjTd != null)
                    this._ObjTd.Text = txtBox.Text.Trim();
                if (_ObjNum != null)
                    this._ObjNum.Value = Convert.ToDecimal(txtBox.Text.Trim());
            }
            catch { }

            this.DialogResult = DialogResult.OK;
        }

        private void btnJ_Click(object sender, EventArgs e)
        {
            //string s = btnState.Text.Trim();
            //changeState(s);
            //if (txtBox.Text.Trim() == "" && s.Equals("123"))
            //{
                sb.Append("-");
                this.txtBox.Text = sb.ToString();
            //}
        }

        private void picX_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}