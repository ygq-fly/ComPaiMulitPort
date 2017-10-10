using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace jcPimSoftware
{
    public partial class NewFolder : Form
    {
        FileService fs;

        #region 构造函数
        public NewFolder()
        {
            InitializeComponent();
        }
        #endregion

        #region 按钮事件
        #region btn1
        private void btn1_Click(object sender, EventArgs e)
        {
            if (CheckStr(tb1.Text.Trim()))
            {
                this.DialogResult = DialogResult.Yes;
            }
            else
            {
                tb1.Text = "";
            }
        }
        #endregion

        #region btn2
        private void btn2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
        #endregion
        #endregion

        #region 窗体事件
        private void NewFolder_Load(object sender, EventArgs e)
        {
            fs = new FileService();
           //LanguageHelp.IsPreprocessID = false;
           // GetLanguage_STR();
        }
        #endregion

        #region 检查新建文件夹名称是否符合格式
        /// <summary>
        /// 检查新建文件夹名称是否符合格式
        /// </summary>
        /// <param name="txt"></param>
        private bool CheckStr(string txt)
        {
            string [] str =new string[]{"\\","/",":","*","?","<",">","|"};

            if (txt != "")
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (txt.IndexOf(str[i]) != -1)
                    {
                        fs.ErrorMessageBox("Error", "The filename cannot contains any  of following characters:\n \\  /  :  *  ?  <  >  | ", "OK");
                        //MessageBox.Show(this, "文件名不能包含以下字符之一: \n    \\  /  :  *  ?  <  >  | ", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                return true;
            }
            else
            {
                fs.ErrorMessageBox("Error", "The filename cannot be blank!", "OK");
                //MessageBox.Show(this, "名称不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

        }
        #endregion

        #region 获取语言包配置
        /// <summary>
        /// 获取语言包配置
        /// </summary>
        private void GetLanguage_STR()
        {
            this.Text = LanguageHelp.GetDlgItem("020000", "Nothing");
            ItemNode[] items = LanguageHelp.GetSubdlgItems("020000");
            label1.Text = items[0].label;
            btn1.Text = items[1].label;
            btn2.Text = items[2].label;
        }
        #endregion

    }
}