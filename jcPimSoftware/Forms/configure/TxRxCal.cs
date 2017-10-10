using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace jcPimSoftware
{
    internal partial class TxRxCal : Form
    {
        #region

        private Offset_Fators _factorsTx1;
        private Offset_Fators _factorsTx1Disp;
        private Offset_Fators _factorsTx2;
        private Offset_Fators _factorsTx2Disp;
        private Offset_Fators _factorsRx;
        private Offset_Fators _factorsRx1;

        #endregion

        #region 构造函数
        public TxRxCal(Offset_Fators tx1,
            Offset_Fators tx1Disp,
            Offset_Fators tx2,
            Offset_Fators tx2Disp,
            Offset_Fators rx,
            Offset_Fators rx1)
        {
            InitializeComponent();

            _factorsTx1 = tx1;
            _factorsTx1Disp = tx1Disp;
            _factorsTx2 = tx2;
            _factorsTx2Disp = tx2Disp;
            _factorsRx = rx;
            _factorsRx1 = rx1;
        }
        #endregion

        #region 按钮事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void TxRxCal_Load(object sender, EventArgs e)
        {
            LoadFactors();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            StoreFactors();
        }
        #endregion

        #region 加载显示Tx、Tx_Disp、Rx
        /// <summary>
        /// 加载显示Tx、Tx_Disp、Rx
        /// </summary>
        public void LoadFactors()
        {
            LoadFactors_Tx();
            LoadFactors_Tx_Disp();
            LoadFactors_Rx();
            LoadFactors_Rx1();
        }
        #endregion

        #region 加载Tx
        /// <summary>
        /// 加载Tx
        /// </summary>
        public void LoadFactors_Tx()
        {
            nudTxF1fa.Value = Convert.ToDecimal(_factorsTx1.A1);
            nudTxF1fb.Value = Convert.ToDecimal(_factorsTx1.B1);
            nudTxF1fc.Value = Convert.ToDecimal(_factorsTx1.C1);

            nudTxF1pd.Value = Convert.ToDecimal(_factorsTx1.A2);
            nudTxF1pe.Value = Convert.ToDecimal(_factorsTx1.B2);
            nudTxF1pf.Value = Convert.ToDecimal(_factorsTx1.C2);

            nudTxF2fa.Value = Convert.ToDecimal(_factorsTx2.A1);
            nudTxF2fb.Value = Convert.ToDecimal(_factorsTx2.B1);
            nudTxF2fc.Value = Convert.ToDecimal(_factorsTx2.C1);

            nudTxF2pd.Value = Convert.ToDecimal(_factorsTx2.A2);
            nudTxF2pe.Value = Convert.ToDecimal(_factorsTx2.B2);
            nudTxF2pf.Value = Convert.ToDecimal(_factorsTx2.C2);
        }
        #endregion

        #region 加载Tx_Disp
        /// <summary>
        /// 加载Tx_Disp
        /// </summary>
        public void LoadFactors_Tx_Disp()
        {
            nudRxF1fa.Value = Convert.ToDecimal(_factorsTx1Disp.A1);
            nudRxF1fb.Value = Convert.ToDecimal(_factorsTx1Disp.B1);
            nudRxF1fc.Value = Convert.ToDecimal(_factorsTx1Disp.C1);

            nudRxF1pd.Value = Convert.ToDecimal(_factorsTx1Disp.A2);
            nudRxF1pe.Value = Convert.ToDecimal(_factorsTx1Disp.B2);
            nudRxF1pf.Value = Convert.ToDecimal(_factorsTx1Disp.C2);

            nudRxF2fa.Value = Convert.ToDecimal(_factorsTx2Disp.A1);
            nudRxF2fb.Value = Convert.ToDecimal(_factorsTx2Disp.B1);
            nudRxF2fc.Value = Convert.ToDecimal(_factorsTx2Disp.C1);

            nudRxF2pd.Value = Convert.ToDecimal(_factorsTx2Disp.A2);
            nudRxF2pe.Value = Convert.ToDecimal(_factorsTx2Disp.B2);
            nudRxF2pf.Value = Convert.ToDecimal(_factorsTx2Disp.C2);

        }
        #endregion

        #region 加载Rx
        /// <summary>
        /// 加载Rx
        /// </summary>
        public void LoadFactors_Rx()
        {
            nudRxRevfa.Value = Convert.ToDecimal(_factorsRx.A1);
            nudRxRevfb.Value = Convert.ToDecimal(_factorsRx.B1);
            nudRxRevfc.Value = Convert.ToDecimal(_factorsRx.C1);

            nudRxRevpd.Value = Convert.ToDecimal(_factorsRx.A2);
            nudRxRevpe.Value = Convert.ToDecimal(_factorsRx.B2);
            nudRxRevpf.Value = Convert.ToDecimal(_factorsRx.C2);
        }
        #endregion

        #region 加载Rx1
        /// <summary>
        /// 加载Rx1
        /// </summary>
        public void LoadFactors_Rx1()
        {
            nudRxFwdFa.Value = Convert.ToDecimal(_factorsRx1.A1);
            nudRxFwdFb.Value = Convert.ToDecimal(_factorsRx1.B1);
            nudRxFwdFc.Value = Convert.ToDecimal(_factorsRx1.C1);

            nudRxFwdPd.Value = Convert.ToDecimal(_factorsRx1.A2);
            nudRxFwdPe.Value = Convert.ToDecimal(_factorsRx1.B2);
            nudRxFwdPf.Value = Convert.ToDecimal(_factorsRx1.C2);
        }
        #endregion

        #region 设置保存Tx、Tx_Disp、Rx
        /// <summary>
        /// 设置保存Tx、Tx_Disp、Rx
        /// </summary>
        public void StoreFactors()
        {
            try
            {
                StoreFactors_Tx();
                StoreFactors_Tx_Disp();
                StoreFactors_Rx();
                StoreFactors_Rx1();

                _factorsTx1.StoreOffsets();
                _factorsTx1Disp.StoreOffsets();
                _factorsTx2.StoreOffsets();
                _factorsTx2Disp.StoreOffsets();
                _factorsRx.StoreOffsets();
                _factorsRx1.StoreOffsets();
                MessageBox.Show(this, "Settings saved successfully!");
            }
            catch { Log.WriteLog("补偿系数设置失败！",Log.EFunctionType.PIM); }
        }
        #endregion

        #region 设置Tx
        /// <summary>
        /// 设置Tx
        /// </summary>
        public void StoreFactors_Tx()
        {
            _factorsTx1.A1 = Convert.ToDouble(nudTxF1fa.Value);
            _factorsTx1.B1 = Convert.ToDouble(nudTxF1fb.Value);
            _factorsTx1.C1 = Convert.ToDouble(nudTxF1fc.Value);

            _factorsTx1.A2 = Convert.ToDouble(nudTxF1pd.Value);
            _factorsTx1.B2 = Convert.ToDouble(nudTxF1pe.Value);
            _factorsTx1.C2 = Convert.ToDouble(nudTxF1pf.Value);

            _factorsTx2.A1 = Convert.ToDouble(nudTxF2fa.Value);
            _factorsTx2.B1 = Convert.ToDouble(nudTxF2fb.Value);
            _factorsTx2.C1 = Convert.ToDouble(nudTxF2fc.Value);

            _factorsTx2.A2 = Convert.ToDouble(nudTxF2pd.Value);
            _factorsTx2.B2 = Convert.ToDouble(nudTxF2pe.Value);
            _factorsTx2.C2 = Convert.ToDouble(nudTxF2pf.Value);
        }
        #endregion

        #region 设置Tx_Disp
        /// <summary>
        /// 设置Tx_Disp
        /// </summary>
        public void StoreFactors_Tx_Disp()
        {
            _factorsTx1Disp.A1 = Convert.ToDouble(nudRxF1fa.Value);
            _factorsTx1Disp.B1 = Convert.ToDouble(nudRxF1fb.Value);
            _factorsTx1Disp.C1 = Convert.ToDouble(nudRxF1fc.Value);
            _factorsTx1Disp.A2 = Convert.ToDouble(nudRxF1pd.Value);
            _factorsTx1Disp.B2 = Convert.ToDouble(nudRxF1pe.Value);
            _factorsTx1Disp.C2 = Convert.ToDouble(nudRxF1pf.Value);


            _factorsTx2Disp.A1 = Convert.ToDouble(nudRxF2fa.Value);
            _factorsTx2Disp.B1 = Convert.ToDouble(nudRxF2fb.Value);
            _factorsTx2Disp.C1 = Convert.ToDouble(nudRxF2fc.Value);
            _factorsTx2Disp.A2 = Convert.ToDouble(nudRxF2pd.Value);
            _factorsTx2Disp.B2 = Convert.ToDouble(nudRxF2pe.Value);
            _factorsTx2Disp.C2 = Convert.ToDouble(nudRxF2pf.Value);
        }
        #endregion

        #region 设置Rx
        /// <summary>
        /// 设置Rx
        /// </summary>
        public void StoreFactors_Rx()
        {
            _factorsRx.A1 = Convert.ToDouble(nudRxRevfa.Value);
            _factorsRx.B1 = Convert.ToDouble(nudRxRevfb.Value);
            _factorsRx.C1 = Convert.ToDouble(nudRxRevfc.Value);
            _factorsRx.A2 = Convert.ToDouble(nudRxRevpd.Value);
            _factorsRx.B2 = Convert.ToDouble(nudRxRevpe.Value);
            _factorsRx.C2 = Convert.ToDouble(nudRxRevpf.Value);
        }
        #endregion

        #region 设置Rx1
        /// <summary>
        /// 设置Rx1
        /// </summary>
        public void StoreFactors_Rx1()
        {
            _factorsRx1.A1 = Convert.ToDouble(nudRxFwdFa.Value);
            _factorsRx1.B1 = Convert.ToDouble(nudRxFwdFb.Value);
            _factorsRx1.C1 = Convert.ToDouble(nudRxFwdFc.Value);
            _factorsRx1.A2 = Convert.ToDouble(nudRxFwdPd.Value);
            _factorsRx1.B2 = Convert.ToDouble(nudRxFwdPe.Value);
            _factorsRx1.C2 = Convert.ToDouble(nudRxFwdPf.Value);
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

        #region MouseDoubleClick
        private void nudValue_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            NumericUpDown number = (NumericUpDown)sender;
            TouchPad((NumericUpDown)sender, number.Value.ToString());
        }
        #endregion

     
    }
}