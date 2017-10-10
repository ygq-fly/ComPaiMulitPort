using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace jcPimSoftware
{
    partial class PimTestProject : Form
    {
        public PimTestProject(List<PimTestScript> listScript, int selected)
        {
            InitializeComponent();
            m_listScript = listScript;
            m_selected = selected;
            listBoxProject.SelectionMode = SelectionMode.One;

            cbxPimMode.Items.Clear();
            cbxPimMode.Items.Add(SweepType.Freq_Sweep);
            cbxPimMode.Items.Add(SweepType.Time_Sweep);

            cbxPimOrder.Items.Clear();
            cbxPimOrder.Items.Add(ImOrder.Im3);
            cbxPimOrder.Items.Add(ImOrder.Im5);
            cbxPimOrder.Items.Add(ImOrder.Im7);
            cbxPimOrder.Items.Add(ImOrder.Im9);

            cbxPimSchema.Items.Clear();
            //cbxPimSchema.Items.Add(ImSchema.REV);
            //cbxPimSchema.Items.Add(ImSchema.FWD);
            //ygq
            cbxPimSchema.Items.Add(ImSchema.REV);
            cbxPimSchema.Items.Add(ImSchema.FWD);
            //ygq

            nudTx.Maximum = Convert.ToDecimal(App_Settings.sgn_1.Max_Power);
            nudTx.Minimum = Convert.ToDecimal(App_Settings.sgn_1.Min_Power);

            nudFre.Maximum = Convert.ToDecimal(App_Settings.sgn_1.Max_Freq);
            nudFreE.Maximum = Convert.ToDecimal(App_Settings.sgn_1.Max_Freq);
            nudFre2.Maximum = Convert.ToDecimal(App_Settings.sgn_2.Max_Freq);
            nudFreE2.Maximum = Convert.ToDecimal(App_Settings.sgn_2.Max_Freq);

            nudFre.Minimum = Convert.ToDecimal(App_Settings.sgn_1.Min_Freq);
            nudFreE.Minimum = Convert.ToDecimal(App_Settings.sgn_1.Min_Freq);
            nudFre2.Minimum = Convert.ToDecimal(App_Settings.sgn_2.Min_Freq);
            nudFreE2.Minimum = Convert.ToDecimal(App_Settings.sgn_2.Min_Freq);

            lblFre.Text = "("+App_Settings.sgn_1.Min_Freq.ToString("0.0") + "MHz-" +
                            App_Settings.sgn_1.Max_Freq.ToString("0.0") + "MHz)";
            lblFre2.Text = "(" + App_Settings.sgn_2.Min_Freq.ToString("0.0") + "MHz-" +
                            App_Settings.sgn_2.Max_Freq.ToString("0.0") + "MHz)";
        }

        //方案列表
        List<PimTestScript> m_listScript;
        //当前选择的方案
        public int m_selected;
        //添加状态标识
        bool m_isAdd = false;
        //当前方案是否被删除
        bool m_isNowProjectDel = false;
        //修改状态标识
        bool m_isMod = false;
        //是否需要保存
        bool m_isSave = false;
        private void PimTestProjectManage_Load(object sender, EventArgs e)
        {
            groupSettings.Enabled = false;
            tbxName.Enabled = false;
            btnModCancel.Enabled = false;
            listBoxProject.Items.Clear();
            for (int i = 0; i < m_listScript.Count;i++ )
            {
                listBoxProject.Items.Add(m_listScript[i].settings.projectName);              
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if(m_isAdd || m_isMod)
                e.Cancel = true;
            if (m_isNowProjectDel)
            {
                MessageBox.Show("当前方案已删除 请重新 选择！");
                e.Cancel = true;
            }
            
            base.OnClosing(e);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (listBoxProject.SelectedIndex == -1)
            {
                MessageBox.Show("请选择！");
                return;
            }
            m_selected = listBoxProject.SelectedIndex;
            m_isNowProjectDel = false;
            this.DialogResult = DialogResult.OK;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {//添加
            m_isAdd = true;
            btnAdd.Enabled = false;
            btnSelect.Enabled = false;

            //初始化方案
            Settings_Pim sp = new Settings_Pim(m_listScript[0].settings.fileName);
            m_listScript[0].settings.Clone(sp);
            m_listScript.Add(new PimTestScript(sp));
            int n = m_listScript.Count - 1;
            sp.projectName = "BAND-" + n.ToString();
            //刷新列表
            listBoxProject.Items.Add(sp.projectName);
            listBoxProject.SelectedIndex = m_listScript.Count - 1;

            btnDel.Enabled = false;
            listBoxProject.Enabled = false;
            groupSettings.Enabled = true;
            tbxName.Enabled = true;
            btnModOk.Text = "确认";
            btnModCancel.Enabled = true;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {//删除方案
            DelScript();
        }

        private void listBoxProject_SelectedIndexChanged(object sender, EventArgs e)
        {//方案选择事件
            int n = listBoxProject.SelectedIndex;
            if (n < 0) return;
            cbxPimMode.SelectedIndex = (int)m_listScript[n].settings.SweepType;
            cbxPimSchema.SelectedIndex = (int)m_listScript[n].settings.PimSchema;
            cbxPimOrder.SelectedItem = m_listScript[n].settings.PimOrder;
            nudTx.Value = Convert.ToDecimal(m_listScript[n].settings.Tx);
            nudFre.Value = Convert.ToDecimal(m_listScript[n].settings.F1s);
            nudFreE.Value = Convert.ToDecimal(m_listScript[n].settings.F1e);
            nudFre2.Value = Convert.ToDecimal(m_listScript[n].settings.F2s);
            nudFreE2.Value = Convert.ToDecimal(m_listScript[n].settings.F2e);
            nudStep.Value = Convert.ToDecimal(m_listScript[n].settings.Setp1);
            nudTime.Value = Convert.ToDecimal(m_listScript[n].settings.TimeSweepPoints / 60.0);
            tbxName.Text = m_listScript[n].settings.projectName;
            if (n == 0)
            {
                btnModOk.Enabled = false;
                btnModCancel.Enabled = false;
                btnDel.Enabled = false;
            }
            else
            {
                btnModOk.Enabled = true;
                btnDel.Enabled = true;
            }
        }

        private void btnModOk_Click(object sender, EventArgs e)
        {//修改按钮
            if (m_isAdd)//确认添加
                AddScript(true);
            else
            {
                if(!m_isMod)
                {//开启修改
                    m_isMod = true;
                    groupSettings.Enabled = true;
                    tbxName.Enabled = true;
                    btnModOk.Text = "确认";
                    btnModCancel.Enabled = true;

                    btnSelect.Enabled = false;
                    btnAdd.Enabled = false;
                    btnDel.Enabled = false;
                    listBoxProject.Enabled = false;
                }
                else
                {//确认修改
                    m_isMod = false;
                    groupSettings.Enabled = false;
                    tbxName.Enabled = false;
                    btnModOk.Text = "修改";
                    btnModCancel.Enabled = false;

                    ModScript();

                    btnSelect.Enabled = true;
                    btnAdd.Enabled = true;
                    btnDel.Enabled = true;
                    listBoxProject.Enabled = true;
                }
            }
        }
        private void btnModCancel_Click(object sender, EventArgs e)
        {//取消按钮
            if (m_isAdd)//取消添加
                AddScript(false);
            else
            {
                if(m_isMod)
                {//取消修改
                    if (MessageBox.Show("确认取消?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                        return;  

                    m_isMod = false;
                    groupSettings.Enabled = false;
                    tbxName.Enabled = false;
                    btnModOk.Text = "修改";
                    btnModCancel.Enabled = false;

                    btnSelect.Enabled = true;
                    btnAdd.Enabled = true;
                    btnDel.Enabled = true;
                    listBoxProject.Enabled = true;

                    listBoxProject_SelectedIndexChanged(null, null);
                }
            }
        }

        void AddScript(bool isOk)
        {//添加方案
            if (isOk)
            {
                if (MessageBox.Show("确认添加?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
            }
            m_isAdd = false;
            btnAdd.Enabled = true;
            btnSelect.Enabled = true;
            btnDel.Enabled = true;
            listBoxProject.Enabled = true;
            tbxName.Enabled = false;
            if (isOk)
                ModScript();
            else
                DelScript();
            groupSettings.Enabled = false;
            btnModOk.Text = "修改";
            btnModCancel.Enabled = false;
        }

        void DelScript()
        {//删除方案
            int n = listBoxProject.SelectedIndex;
            if (n <= 0) return;
            if (MessageBox.Show("确认删除?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;     
            m_listScript.RemoveAt(n);
            listBoxProject.Items.RemoveAt(n);  
            if (m_selected == n)
            {
                listBoxProject.SelectedIndex = n - 1;
                m_isNowProjectDel = true;
            }
            m_isSave = true;
        }

        void ModScript()
        {
            int n = listBoxProject.SelectedIndex;
            m_listScript[n].settings.projectName = tbxName.Text;
            m_listScript[n].settings.SweepType = (SweepType)cbxPimMode.SelectedItem;
            m_listScript[n].settings.PimSchema = (ImSchema)cbxPimSchema.SelectedItem;
            m_listScript[n].settings.PimOrder = (ImOrder)cbxPimOrder.SelectedItem;
            m_listScript[n].settings.F1s = Convert.ToSingle(nudFre.Value);
            m_listScript[n].settings.F1e = Convert.ToSingle(nudFreE.Value);
            m_listScript[n].settings.F2s = Convert.ToSingle(nudFre2.Value);
            m_listScript[n].settings.F2e = Convert.ToSingle(nudFreE2.Value);
            m_listScript[n].settings.Setp1 = Convert.ToSingle(nudStep.Value);
            m_listScript[n].settings.Setp2 = Convert.ToSingle(nudStep.Value);
            m_listScript[n].settings.TimeSweepPoints = Convert.ToInt32(nudTime.Value * 60);
            m_listScript[n].settings.Tx = Convert.ToSingle(nudTx.Value);
            listBoxProject.Items[n] = m_listScript[n].settings.projectName;
            m_isSave = true;
        }

        private void cbxPimMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbxPimMode.SelectedIndex == 0)
            {
                nudFre.Enabled = true;
                nudFreE.Enabled = true;
                nudFre2.Enabled = true;
                nudFreE2.Enabled = true;
                nudTime.Enabled = false;
                nudStep.Enabled = true;
            }
            else if (cbxPimMode.SelectedIndex == 1)
            {
                nudFre.Enabled = true;
                nudFreE.Enabled = false;
                nudFre2.Enabled = false;
                nudFreE2.Enabled = true;
                nudTime.Enabled = true;
                nudStep.Enabled = false;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            if (m_isSave)
                SaveScript();
            base.OnClosed(e);
        }

        void SaveScript()
        {
            string strPath = Application.StartupPath + "\\settings\\JcScript.ini";
            //清空
            int count = int.Parse(IniFile.GetString("Script", "count", "0", strPath));
            for (int i = 1; i <= count; i++)
                IniFile.SetString("Script", "N" + i.ToString(), "", strPath);
            //开始SAVE
            int n = m_listScript.Count;
            IniFile.SetString("Script", "count", (n - 1).ToString(), strPath);
            for (int i = 1; i < n; i++)
            {//列表序号0为默认配置，不保存
                string strValue = m_listScript[i].settings.projectName + "," +
                                  ((int)m_listScript[i].settings.SweepType).ToString() + "," +
                                  ((int)m_listScript[i].settings.PimSchema).ToString() + "," +
                                  ((int)m_listScript[i].settings.PimOrder).ToString() + "," +
                                  m_listScript[i].settings.F1s.ToString() + "," +
                                  m_listScript[i].settings.F1e.ToString() + "," +
                                  m_listScript[i].settings.F2s.ToString() + "," +
                                  m_listScript[i].settings.F2e.ToString() + "," +
                                  m_listScript[i].settings.Setp1.ToString() + "," +
                                  m_listScript[i].settings.TimeSweepPoints.ToString() + "," +
                                  m_listScript[i].settings.Tx;
                IniFile.SetString("Script", "N" + i.ToString(), strValue, strPath);
            }
        }
    }

    class PimTestScript
    {
        public PimTestScript(Settings_Pim sp)
        {
            this.settings = sp;
            result = "";
        }
        public Settings_Pim settings;
        //预留
        public string result;
    }
}
