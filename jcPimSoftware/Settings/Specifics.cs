using System;
using System.Collections.Generic;
using System.Text;

namespace jcPimSoftware
{
    //IM分析-频率范围参数
    class ImSpecifics {

        //扫频上行趟F1频率范围
        internal float F1UpS, F1UpE; 

        //扫频下行趟F2频率范围
        internal float F2DnS, F2DnE;    

        //扫频下行趟F1固定频率，扫频上行趟F2固定频率
        internal float F1fixed, F2fixed;

        // F1,F2的步进
        internal float F1Step, F2Step;

        //扫频互调频率范围
        internal float ImS, ImE; 

    }

    //仪表合路器频率指标
    class CbnSpecifics
    {
        //合路器1：F1的起始、截至频率
        internal float Cbn1F1S, Cbn1F1E;

        //合路器1：F2的起始、截至频率
        internal float Cbn1F2S, Cbn1F2E;

        //合路器1：Rx的起始、截至频率
        internal float Cbn1RxS, Cbn1RxE;

        //合路器2：Tx的起始、截至频率
        internal float Cbn2TxS, Cbn2TxE;

        //合路器2：Rx的起始、截至频率
        internal float Cbn2RxS, Cbn2RxE;

        //Tx的起始、截至频率
        internal float TxS, TxE;

        //Rx的起始、截至频率
        internal float RxS, RxE;
    }

    class Specifics
    {
        /// <summary>
        /// 配置文件名称，读取某个文件时，需要预先设置
        /// </summary>
        internal readonly string fileName;

        /// <summary>
        /// 能够支持最大的互调阶数
        /// </summary>
        internal readonly int maxImOrder;

        /// <summary>
        /// 互调规格参数数组
        /// </summary>
        internal ImSpecifics[] ims;

        /// <summary>
        /// 仪表相应合路器的规格参数
        /// </summary>
        internal CbnSpecifics cbn;
        
        /// <summary>
        /// 向构造函数传递最大的互调阶数
        /// </summary>
        /// <param name="fileName"></param>
        internal Specifics(string fileName)
        {
            this.fileName = fileName;

            this.maxImOrder =Enum.GetNames(typeof(ImOrder)).Length;

            ims = new ImSpecifics[maxImOrder];

            

            cbn = new CbnSpecifics();
            
        }

        /// <summary>
        /// 加载规格参数
        /// </summary>
        internal void LoadSettings()
        {
            IniFile.SetFileName(fileName);
            int n = 3;
            string pre;

            for (int i = 0; i < ims.Length; i++)
            {
                ims[i] = new ImSpecifics();
                
                pre = "ord" + n.ToString() + "_";

                ims[i].F1UpS = float.Parse(IniFile.GetString("Specifics", pre + "F1UpS", "869")); //F1: 869~871.5 
                ims[i].F1UpE = float.Parse(IniFile.GetString("Specifics", pre + "F1UpE", "871.5"));
                ims[i].F2DnS = float.Parse(IniFile.GetString("Specifics", pre + "F2DnS", "889")); //F2: 889~894
                ims[i].F2DnE = float.Parse(IniFile.GetString("Specifics", pre + "F2DnE", "894"));
                ims[i].F1fixed = float.Parse(IniFile.GetString("Specifics", pre + "F1fixed", "869")); //F1: 869 F2: 894
                ims[i].F2fixed = float.Parse(IniFile.GetString("Specifics", pre + "F2fixed", "894"));
                ims[i].F1Step = float.Parse(IniFile.GetString("Specifics", pre + "F1Step", "1")); //Step
                ims[i].F2Step = float.Parse(IniFile.GetString("Specifics", pre + "F2Step", "1"));
                ims[i].ImS = float.Parse(IniFile.GetString("Specifics", pre + "ImS", "844")); //Im3: 844~849
                ims[i].ImE = float.Parse(IniFile.GetString("Specifics", pre + "ImE", "849"));

                n = n + 2;
            }

            cbn.Cbn1F1S = float.Parse(IniFile.GetString("Specifics", "Cbn1F1S", "869")); //F1: 869~871.5
            cbn.Cbn1F1E = float.Parse(IniFile.GetString("Specifics", "Cbn1F1E", "871.5"));
            cbn.Cbn1F2S = float.Parse(IniFile.GetString("Specifics", "Cbn1F2S", "889")); //F2: 889~894
            cbn.Cbn1F2E = float.Parse(IniFile.GetString("Specifics", "Cbn1F2E", "894"));
            cbn.Cbn1RxS = float.Parse(IniFile.GetString("Specifics", "Cbn1RxS", "824")); //Rx: 824~849
            cbn.Cbn1RxE = float.Parse(IniFile.GetString("Specifics", "Cbn1RxE", "849"));

            cbn.Cbn2TxS = float.Parse(IniFile.GetString("Specifics", "Cbn2TxS", "869")); //Tx: 869~894
            cbn.Cbn2TxE = float.Parse(IniFile.GetString("Specifics", "Cbn2TxE", "894"));
            cbn.Cbn2RxS = float.Parse(IniFile.GetString("Specifics", "Cbn2RxS", "824")); //Rx: 824~849
            cbn.Cbn2RxE = float.Parse(IniFile.GetString("Specifics", "Cbn2RxE", "849"));

            cbn.TxS = float.Parse(IniFile.GetString("Specifics", "TxS", "869")); //Tx: 869~894
            cbn.TxE = float.Parse(IniFile.GetString("Specifics", "TxE", "894"));
            cbn.RxS = float.Parse(IniFile.GetString("Specifics", "RxS", "824")); //Rx: 824~849
            cbn.RxE = float.Parse(IniFile.GetString("Specifics", "RxE", "849"));           
        }

        /// <summary>
        /// 保存规格参数
        /// </summary>
        internal void StoreSettings()
        {
            int i;
            string pre;

            foreach (ImSpecifics a in ims)
            {
                i = 3;
                pre = "ord" + i.ToString() + "_";             
            
                IniFile.SetString("Specifics", pre + "F1UpS", a.F1UpS.ToString("0.###"));
                IniFile.SetString("Specifics", pre + "F1UpE", a.F1UpE.ToString("0.###"));
                IniFile.SetString("Specifics", pre + "F2DnS", a.F2DnS.ToString("0.###"));
                IniFile.SetString("Specifics", pre + "F2DnE", a.F2DnE.ToString("0.###"));
                IniFile.SetString("Specifics", pre + "F1fixed", a.F1fixed.ToString("0.###"));
                IniFile.SetString("Specifics", pre + "F2fixed", a.F2fixed.ToString("0.###"));
                IniFile.SetString("Specifics", pre + "F1Step", a.F1Step.ToString("0.###"));
                IniFile.SetString("Specifics", pre + "F2Step", a.F2Step.ToString("0.###"));
                IniFile.SetString("Specifics", pre + "ImS", a.ImS.ToString("0.###"));
                IniFile.SetString("Specifics", pre + "ImE", a.ImE.ToString("0.###"));

                i = i + 2;
            }

            IniFile.SetString("Specifics", "Cbn1F1S", cbn.Cbn1F1S.ToString("0.###"));
            IniFile.SetString("Specifics", "Cbn1F1E", cbn.Cbn1F1E.ToString("0.###"));
            IniFile.SetString("Specifics", "Cbn1F2S", cbn.Cbn1F2S.ToString("0.###"));
            IniFile.SetString("Specifics", "Cbn1F2E", cbn.Cbn1F2E.ToString("0.###"));
            IniFile.SetString("Specifics", "Cbn1RxS", cbn.Cbn1RxS.ToString("0.###"));
            IniFile.SetString("Specifics", "Cbn1RxE", cbn.Cbn1RxE.ToString("0.###"));

            IniFile.SetString("Specifics", "Cbn2TxS", cbn.Cbn2TxS.ToString("0.###"));
            IniFile.SetString("Specifics", "Cbn2TxE", cbn.Cbn2TxE.ToString("0.###"));
            IniFile.SetString("Specifics", "Cbn2RxS", cbn.Cbn2RxS.ToString("0.###"));
            IniFile.SetString("Specifics", "Cbn2RxE", cbn.Cbn2RxE.ToString("0.###"));

            IniFile.SetString("Specifics", "TxS", cbn.TxS.ToString("0.###"));
            IniFile.SetString("Specifics", "TxE", cbn.TxE.ToString("0.###"));
            IniFile.SetString("Specifics", "RxS", cbn.RxS.ToString("0.###"));
            IniFile.SetString("Specifics", "RxE", cbn.RxE.ToString("0.###"));
        }
    }
}
