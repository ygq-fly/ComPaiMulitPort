using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace jcPimSoftware
{
    public partial class SpecificsForm : Form
    {

        #region 构造函数
        public SpecificsForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 窗体事件
        private void SpecificsForm_Load(object sender, EventArgs e)
        {
            App_Settings.spfc.LoadSettings();
            GetSpecifics(App_Settings.spfc);
        }
        #endregion

        #region 获取规格配置值
        /// <summary>
        /// 获取规格配置值
        /// </summary>
        /// <param name="sf"></param>
        private void GetSpecifics(Specifics sf)
        {

            tbxF1UpS_3.Text = sf.ims[0].F1UpS.ToString();
            tbxF1UpE_3.Text = sf.ims[0].F1UpE.ToString();
            tbxF1fixed_3.Text = sf.ims[0].F1fixed.ToString();
            tbxF2UpS_3.Text = sf.ims[0].F2DnE.ToString();
            tbxF2UpE_3.Text = sf.ims[0].F2DnS.ToString();
            tbxF2fixed_3.Text = sf.ims[0].F2fixed.ToString();
            tbxImS_3.Text = sf.ims[0].ImS.ToString();
            tbxImE_3.Text = sf.ims[0].ImE.ToString();

            tbxF1UpS_5.Text = sf.ims[1].F1UpS.ToString();
            tbxF1UpE_5.Text = sf.ims[1].F1UpE.ToString();
            tbxF1fixed_5.Text = sf.ims[1].F1fixed.ToString();
            tbxF2UpS_5.Text = sf.ims[1].F2DnE.ToString();
            tbxF2UpE_5.Text = sf.ims[1].F2DnS.ToString();
            tbxF2fixed_5.Text = sf.ims[1].F2fixed.ToString();
            tbxImS_5.Text = sf.ims[1].ImS.ToString();
            tbxImE_5.Text = sf.ims[1].ImE.ToString();

            tbxF1UpS_7.Text = sf.ims[2].F1UpS.ToString();
            tbxF1UpE_7.Text = sf.ims[2].F1UpE.ToString();
            tbxF1fixed_7.Text = sf.ims[2].F1fixed.ToString();
            tbxF2UpS_7.Text = sf.ims[2].F2DnE.ToString();
            tbxF2UpE_7.Text = sf.ims[2].F2DnS.ToString();
            tbxF2fixed_7.Text = sf.ims[2].F2fixed.ToString();
            tbxImS_7.Text = sf.ims[2].ImS.ToString();
            tbxImE_7.Text = sf.ims[2].ImE.ToString();

            tbxF1UpS_9.Text = sf.ims[3].F1UpS.ToString();
            tbxF1UpE_9.Text = sf.ims[3].F1UpE.ToString();
            tbxF1fixed_9.Text = sf.ims[3].F1fixed.ToString();
            tbxF2UpS_9.Text = sf.ims[3].F2DnE.ToString();
            tbxF2UpE_9.Text = sf.ims[3].F2DnS.ToString();
            tbxF2fixed_9.Text = sf.ims[3].F2fixed.ToString();
            tbxImS_9.Text = sf.ims[3].ImS.ToString();
            tbxImE_9.Text = sf.ims[3].ImE.ToString();

            tbxCbn1F1S.Text = sf.cbn.Cbn1F1S.ToString();
            tbxCbn1F1E.Text = sf.cbn.Cbn1F1E.ToString();

            tbxCbn1F2S.Text = sf.cbn.Cbn1F2S.ToString();
            tbxCbn1F2E.Text = sf.cbn.Cbn1F2E.ToString();

            tbxCbn1RxS.Text = sf.cbn.Cbn1RxS.ToString();
            tbxCbn1RxE.Text = sf.cbn.Cbn1RxE.ToString();

            tbxCbn2TxS.Text = sf.cbn.Cbn2TxS.ToString();
            tbxCbn2TxE.Text = sf.cbn.Cbn2TxE.ToString();

            tbxCbn2RxS.Text = sf.cbn.Cbn2RxS.ToString();
            tbxCbn2RxE.Text = sf.cbn.Cbn2RxE.ToString();

            tbxTxS.Text = sf.cbn.TxS.ToString();
            tbxTxE.Text = sf.cbn.TxE.ToString();

            tbxRxS.Text = sf.cbn.RxS.ToString();
            tbxRxE.Text = sf.cbn.RxE.ToString();

        }
        #endregion

        #region 设置规格配置值
        /// <summary>
        /// 设置规格配置值
        /// </summary>
        /// <param name="sf"></param>
        private void SetSpecifics(Specifics sf)
        {
            sf.ims[0].F1UpS = float.Parse(tbxF1UpS_3.Text.Trim());
            sf.ims[0].F1UpE = float.Parse(tbxF1UpE_3.Text.Trim());
            sf.ims[0].F1fixed = float.Parse(tbxF1fixed_3.Text.Trim());
            sf.ims[0].F2DnE = float.Parse(tbxF2UpS_3.Text.Trim());
            sf.ims[0].F2DnS = float.Parse(tbxF2UpE_3.Text.Trim());
            sf.ims[0].F2fixed = float.Parse(tbxF2fixed_3.Text.Trim());
            sf.ims[0].ImS = float.Parse(tbxImS_3.Text.Trim());
            sf.ims[0].ImE = float.Parse(tbxImE_3.Text.Trim());

            sf.ims[1].F1UpS = float.Parse(tbxF1UpS_3.Text.Trim());
            sf.ims[1].F1UpE = float.Parse(tbxF1UpE_3.Text.Trim());
            sf.ims[1].F1fixed = float.Parse(tbxF1fixed_3.Text.Trim());
            sf.ims[1].F2DnE = float.Parse(tbxF2UpS_3.Text.Trim());
            sf.ims[1].F2DnS = float.Parse(tbxF2UpE_3.Text.Trim());
            sf.ims[1].F2fixed = float.Parse(tbxF2fixed_3.Text.Trim());
            sf.ims[1].ImS = float.Parse(tbxImS_3.Text.Trim());
            sf.ims[1].ImE = float.Parse(tbxImE_3.Text.Trim());

            sf.ims[2].F1UpS = float.Parse(tbxF1UpS_3.Text.Trim());
            sf.ims[2].F1UpE = float.Parse(tbxF1UpE_3.Text.Trim());
            sf.ims[2].F1fixed = float.Parse(tbxF1fixed_3.Text.Trim());
            sf.ims[2].F2DnE = float.Parse(tbxF2UpS_3.Text.Trim());
            sf.ims[2].F2DnS = float.Parse(tbxF2UpE_3.Text.Trim());
            sf.ims[2].F2fixed = float.Parse(tbxF2fixed_3.Text.Trim());
            sf.ims[2].ImS = float.Parse(tbxImS_3.Text.Trim());
            sf.ims[2].ImE = float.Parse(tbxImE_3.Text.Trim());

            sf.ims[3].F1UpS = float.Parse(tbxF1UpS_3.Text.Trim());
            sf.ims[3].F1UpE = float.Parse(tbxF1UpE_3.Text.Trim());
            sf.ims[3].F1fixed = float.Parse(tbxF1fixed_3.Text.Trim());
            sf.ims[3].F2DnE = float.Parse(tbxF2UpS_3.Text.Trim());
            sf.ims[3].F2DnS = float.Parse(tbxF2UpE_3.Text.Trim());
            sf.ims[3].F2fixed = float.Parse(tbxF2fixed_3.Text.Trim());
            sf.ims[3].ImS = float.Parse(tbxImS_3.Text.Trim());
            sf.ims[3].ImE = float.Parse(tbxImE_3.Text.Trim());

            sf.cbn.Cbn1F1S = float.Parse(tbxCbn1F1S.Text.Trim());
            sf.cbn.Cbn1F1E = float.Parse(tbxCbn1F1E.Text.Trim());

            sf.cbn.Cbn1F2S = float.Parse(tbxCbn1F2S.Text.Trim());
            sf.cbn.Cbn1F2E = float.Parse(tbxCbn1F2E.Text.Trim());

            sf.cbn.Cbn1RxS = float.Parse(tbxCbn1RxS.Text.Trim());
            sf.cbn.Cbn1RxE = float.Parse(tbxCbn1RxE.Text.Trim());

            sf.cbn.Cbn2TxS = float.Parse(tbxCbn2TxS.Text.Trim());
            sf.cbn.Cbn2TxE = float.Parse(tbxCbn2TxE.Text.Trim());

            sf.cbn.Cbn2RxS = float.Parse(tbxCbn2RxS.Text.Trim());
            sf.cbn.Cbn2RxE = float.Parse(tbxCbn2RxE.Text.Trim());

            sf.cbn.TxS = float.Parse(tbxTxS.Text.Trim());
            sf.cbn.TxE = float.Parse(tbxTxE.Text.Trim());

            sf.cbn.RxS = float.Parse(tbxRxS.Text.Trim());
            sf.cbn.RxE = float.Parse(tbxRxE.Text.Trim());
        }
        #endregion
    }
}