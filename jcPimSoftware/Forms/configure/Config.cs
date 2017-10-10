using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace jcPimSoftware
{
    public partial class Config : Form
    {

        #region ��������
        TxRxCal t = null;

        public int TabOffsetIndex
        {
            get { return tclConfig.SelectedIndex; }
            set { tclConfig.SelectedIndex = value; }
        }
        #endregion

        #region ���캯��
        public Config()
        {
            InitializeComponent();
        }
        #endregion

        #region �����¼�
        private void Config_Load(object sender, EventArgs e)
        {   
            LoadConfig();
        }
        #endregion

        #region ������ʾPIM,ISO,VSWR,HARMONIC������������
        /// <summary>
        /// ������ʾPIM,ISO,VSWR������������
        /// </summary>
        private void LoadConfig()
        {
            //PIM
            TxRxConfig(t, App_Factors.pim_tx1,
              App_Factors.pim_tx1_disp,
              App_Factors.pim_tx2,
              App_Factors.pim_tx2_disp,
              App_Factors.pim_rx1,
              App_Factors.pim_rx2,0);

            ////ISO
            //TxRxConfig(t, App_Factors.iso_tx1,
            //   App_Factors.iso_tx1_disp,
            //   App_Factors.iso_tx2,
            //   App_Factors.iso_tx2_disp,
            //   App_Factors.iso_rx1, 1);

            ////VSWR
            //TxRxConfig(t, App_Factors.vsw_tx1,
            //   App_Factors.vsw_tx1_disp,
            //   App_Factors.vsw_tx2,
            //   App_Factors.vsw_tx2_disp,
            //   App_Factors.vsw_rx1, 2);

            ////HARMONIC
            //TxRxConfig(t, App_Factors.har_tx1,
            //   App_Factors.har_tx1_disp,
            //   App_Factors.har_tx2,
            //   App_Factors.har_tx2_disp,
            //   App_Factors.har_rx1, 3);
        }
        #endregion 

        #region ���ز�������
        /// <summary>
        /// ���ز�������
        /// </summary>
        /// <param name="t"></param>
        /// <param name="factorsTx1"></param>
        /// <param name="factorsTx1Disp"></param>
        /// <param name="factorsTx2"></param>
        /// <param name="factorsTx2Disp"></param>
        /// <param name="factorsRx"></param>
        /// <param name="i"></param>
        private void TxRxConfig(TxRxCal t,
              Offset_Fators factorsTx1,
              Offset_Fators factorsTx1Disp,
              Offset_Fators factorsTx2,
              Offset_Fators factorsTx2Disp,
              Offset_Fators factorsRx,
            Offset_Fators factorsRx1,
                                int i)
        {
            factorsTx1.LoadOffsets();
            factorsTx1Disp.LoadOffsets();
            factorsTx2.LoadOffsets();
            factorsTx2Disp.LoadOffsets();
            factorsRx.LoadOffsets();

            t = new TxRxCal(factorsTx1, factorsTx1Disp,
                            factorsTx2, factorsTx2Disp,
                            factorsRx, factorsRx1);

            t.TopLevel = false;
            t.Parent = tclConfig.TabPages[i];
            tclConfig.TabPages[i].Controls.Add(t);
            t.Dock = DockStyle.Fill;
            t.Show();
        }
        #endregion

    }
}