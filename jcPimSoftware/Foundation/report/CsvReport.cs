using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace jcPimSoftware
{
    /// <summary>
    /// CSV�����ļ���ͷ����Ϣ
    /// ������פ���ȣ�����ȣ�г��ʹ����ͬ�ı�ͷ
    /// P: Pim��I��Isolation��V��vswr��H��harmoinc
    /// </summary>
    class CsvReport_PIVH_Header
    {
        string mac_desc;
        SweepType swp_type;
        ImSchema im_schema;
        ImOrder im_order;
        int point_num;
        float limit_value;
        string date_time;
        float sweep_start;
        float sweep_stop;
        float y_Max_RL;
        float y_Min_RL;
        float y_Max_VSWR;
        float y_Min_VSWR;
        int n;

        /// <summary>
        /// ����1�ĵ���
        /// </summary>
        public int N
        {
            get { return n; }
            set { n = value; }
        }


        /// <summary>
        /// �Ǳ��ͺŵ�������Ϣ
        /// </summary>
        public string Mac_Desc
        {
            get { return mac_desc; }
            set { mac_desc = value; }
        }

        /// <summary>
        /// ɨ������ָʾ��
        /// </summary>
        internal SweepType Swp_Type
        {
            get { return swp_type; }
            set { swp_type = value; }
        }

        /// <summary>
        /// ɨ�跽��ָʾ��
        /// </summary>
        internal ImSchema Im_Schema
        {
            get { return im_schema; }
            set { im_schema = value; }
        }

        /// <summary>
        /// ��������ָʾ��
        /// </summary>
        internal ImOrder Im_Order
        {
            get { return im_order; }
            set { im_order = value; }
        }

        /// <summary>
        /// ɨ�����
        /// </summary>
        public int Point_Num
        {
            get { return point_num; }
            set { point_num = value; }
        }

        /// <summary>
        /// PASS/FAIL���жϷ�ֵ
        /// </summary>
        public float Limit_Value
        {
            get { return limit_value; }
            set { limit_value = value; }
        }

        /// <summary>
        /// �������������ʱ��
        /// </summary>
        public string Date_Time
        {
            get { return date_time; }
            set { date_time = value; }
        }

        /// <summary>
        /// ɨ�迪ʼƵ��
        /// </summary>
        public float Sweep_Start
        {
            get { return sweep_start; }
            set { sweep_start = value; }
        }

        /// <summary>
        /// ɨ��ֹͣƵ��
        /// </summary>
        public float Sweep_Stop
        {
            get { return sweep_stop; }
            set { sweep_stop = value; }
        }

        /// <summary>
        /// Y��RL�������ֵ
        /// </summary>
        public float Y_Max_RL
        {
            get { return y_Max_RL; }
            set { y_Max_RL = value; }
        }

        /// <summary>
        /// Y��RL������Сֵ
        /// </summary>
        public float Y_Min_RL
        {
            get { return y_Min_RL; }
            set { y_Min_RL = value; }
        }

        /// <summary>
        /// Y��VSWR�������ֵ
        /// </summary>
        public float Y_Max_VSWR
        {
            get { return y_Max_VSWR; }
            set { y_Max_VSWR = value; }
        }

        /// <summary>
        /// Y��RL������Сֵ
        /// </summary>
        public float Y_Min_VSWR
        {
            get { return y_Min_VSWR; }
            set { y_Min_VSWR = value; }
        }
    }


    /// <summary>
    /// CSV�����ļ���ͷ����Ϣ    
    /// Ƶ�׷���ģ��ר��
    /// </summary>
    class CsvReport_Spctrum_Header
    {
        float start;
        float stop;
        int rbw;
        int att;
        string mac_desc;
        string date_time;

        /// <summary>
        /// Ƶ�׷����Ŀ�ʼƵ��
        /// </summary>
        public float Start
        {
            get { return start; }
            set { start = value; }
        }

        /// <summary>
        /// Ƶ�׷����Ľ���Ƶ��
        /// </summary>
        public float Stop
        {
            get { return stop; }
            set { stop = value; }
        }

        /// <summary>
        /// Ƶ�׷�����RBW
        /// </summary>
        public int RBW
        {
            get { return rbw; }
            set { rbw = value; }
        }

        /// <summary>
        /// Ƶ�׷�����ATT
        /// </summary>
        public int ATT
        {
            get { return att; }
            set { att = value; }
        }

        /// <summary>
        /// �Ǳ��ͺŵ�������Ϣ
        /// </summary>
        public string Mac_Desc
        {
            get { return mac_desc; }
            set { mac_desc = value; }
        }

        /// <summary>
        /// �������������ʱ��
        /// </summary>
        public string Date_Time
        {
            get { return date_time; }
            set { date_time = value; }
        }
    }

    /// <summary>
    /// ����ɨ����
    /// </summary>
    class CsvReport_Pim_Entry
    {
        int no;
        float p1;
        float f1;
        float p2;
        float f2;
        float im_f;
        float im_v;

        /// <summary>
        /// ɨ�������
        /// </summary>
        public int No
        {
            get { return no; }
            set { no = value; }
        }

        /// <summary>
        /// ����1������ʣ���λ dBm
        /// </summary>
        public float P1
        {
            get { return p1; }
            set { p1 = value; }
        }
  
        /// <summary>
        /// ����1����Ƶ�ʣ���λ MHz
        /// </summary>
        public float F1
        {
            get { return f1; }
            set { f1 = value; }
        }

        /// <summary>
        /// ����2������ʣ���λ dBm
        /// </summary>
        public float P2
        {
            get { return p2; }
            set { p2 = value; }
        }

        /// <summary>
        /// ����2����Ƶ�ʣ���λ MHz
        /// </summary>
        public float F2
        {
            get { return f2; }
            set { f2 = value; }
        }

        /// <summary>
        /// ɨ����Ƶ�ʣ���λ MHz
        /// </summary>
        public float Im_F
        {
            get { return im_f; }
            set { im_f = value; }
        }

        /// <summary>
        /// ɨ���Ļ���ֵ����λ dBc
        /// </summary>
        public float Im_V
        {
            get { return im_v; }
            set { im_v = value; }
        }

    }

    /// <summary>
    /// פ���ȣ�����ȣ�г����ɨ������ʽ��ͬ
    /// I��Isolation��V��vswr��H��harmoinc
    /// </summary>
    class CsvReport_IVH_Entry
    {
        int no;
        float p;
        float f;
        float ivh_value;
        float noise;
        float rl;

        /// <summary>
        /// ɨ�������
        /// </summary>
        public int No
        {
            get { return no; }
            set { no = value; }
        }

        /// <summary>
        /// ����������ʣ���λ dBm
        /// </summary>
        public float P
        {
            get { return p; }
            set { p = value; }
        }

        /// <summary>
        /// ��������Ƶ�ʣ���λ MHz
        /// </summary>
        public float F
        {
            get { return f; }
            set { f = value; }
        }

        /// <summary>
        /// ɨ����ֵ����λ dB
        /// </summary>
        public float IVH_Value
        {
            get { return ivh_value; }
            set { ivh_value = value; }
        }

        /// <summary>
        /// ɨ����ƽ������ֵ����λ dBm
        /// </summary>
        public float Noise
        {
            get { return noise; }
            set { noise = value; }
        }

        /// <summary>
        /// RLֵ
        /// </summary>
        public float Rl
        {
            get { return rl; }
            set { rl = value; }
        }
    }


    class CsvReport
    {
        /// <summary>
        /// ������ɨ�����header������ɨ�����б�entries���浽�ļ�fileName
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="entries"></param>
        /// <param name="header"></param>
        internal static void Save_Csv_Pim(string fileName, CsvReport_Pim_Entry[] entries, CsvReport_PIVH_Header header)
        {
            StreamWriter sw = null;

            try
            {
                sw = new StreamWriter(fileName, false, Encoding.ASCII);

                string s1;
                if (header.Swp_Type == SweepType.Freq_Sweep)
                    s1 = "Frequency Sweep ";
                else
                    s1 = "Time Sweep ";

                string s2;
                if (header.Im_Schema == ImSchema.FWD)
                    s2 = "FWD ";
                else 
                    s2 = "REV ";

                string s3 = "Start " + header.Sweep_Start.ToString("0.#") + " " +
                            "Stop " + header.Sweep_Stop.ToString("0.#") + " " +
                            "Point Number " + header.Point_Num.ToString() + " " +
                            "Limit Value " + header.Limit_Value.ToString("0.#");

                string sparams = "params " +
                                 ((int)header.Swp_Type).ToString() + " " +
                                 ((int)header.Im_Schema).ToString() + " " +
                                 ((int)header.Im_Order).ToString() + " " +
                                 header.Sweep_Start.ToString("0.#") + " " +
                                 header.Sweep_Stop.ToString("0.#") + " " +
                                 header.Point_Num.ToString() + " " +
                                 header.Limit_Value.ToString("0.#") + " " +
                                 header.N.ToString();

                sw.WriteLine(header.Mac_Desc);
                sw.WriteLine(header.Date_Time);
                sw.WriteLine(s1 + s2 + "Im Order " + ((int)header.Im_Order).ToString());
                sw.WriteLine(s3);
                sw.WriteLine(sparams);
                sw.WriteLine("Tx Out(dbm) Im Value(dbc) Frequency(MHz)");
                sw.WriteLine("NO., P1, F1, P2, F2, Im_V, Im_F");

                string s4;
                for (int i = 0; i < entries.Length; i++)
                {
                    s4 = entries[i].No.ToString() + ", " +
                         entries[i].P1.ToString("0.0000000") + ", " +
                         entries[i].F1.ToString("0.0000000") + ", " +
                         entries[i].P2.ToString("0.0000000") + ", " +
                         entries[i].F2.ToString("0.0000000") + ", " +
                         entries[i].Im_V.ToString("0.0000000") + ", " +
                         entries[i].Im_F.ToString("0.0000000");

                    sw.WriteLine(s4);
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();

            } catch {
                if (sw != null) {
                    sw.Close();
                    sw.Dispose();
                }
            }
        }


        public static bool SaveTxt(string path, CsvReport_Pim_Entry[] entries,  CsvReport_PIVH_Header header,bool isdbm)
        {
            string col = "Instrument  Date	                Time	                 Content	Test Description	 Model Number	                Serial Number	                Operator	        Carrier 1 Freq MHz	 Carrier 2 Freq, MHz 	Carrier 1 Power	    Carrier 2 Power	    Carrier Power Units	    Carrier 1 Offset	Carrier 2 Offset	Carrier Offset Units	ALC	Averaging	Settling Time,     msec	       IM Measurement	Stimulus Port	IM Order	IM Freq, MHz	IM Power	Reference Value	    IM Peak Power	IM Units";

            string val = "         26/07/2016	        12:16:28 PM	               Swept IM	                     [Enter Test Description]	    [Enter Model Number]		    [Enter Operator]	925.0	             960.0	                    OFF	                OFF	                dBm	                    0.0	                0.0	                dB	                    ON	            Normal	        0	       REV	Port        1	            3rd	        890.0	        -111.3	    -119.000000	        -135.380000	        dBm";

            double limit = header.Limit_Value;
            string unit = "dBm";
            float max = float.MinValue;
            for (int i = 0; i < entries.Length; i++)
            {
                if (max<= entries[i].Im_V) max = entries[i].Im_V;
            }
            //if (!isdbm)
            //{
            //    unit = "dBc";
            //    max = max - cjt.pow1;
            //}
            string mesure = "REV";
            string port = "Port 1";
            if (header.Im_Schema == ImSchema.FWD)
            {
                mesure = "FWD";
                port = "Port 2";
            }
            if (!Directory.Exists(App_Configure.Cnfgs.Path_Rpt_Pim + "\\csv"))
                Directory.CreateDirectory(App_Configure.Cnfgs.Path_Rpt_Pim + "\\csv");

            bool exists = File.Exists(path + ".txt");
            FileStream fs = new FileStream(path + ".txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            bool result = true;
            try
            {
             
                    //    sw.WriteLine("Instrument    Date    Time    Content    Test Description    Model Number    Serial Number    Operator" +
                    //"    Carrier 1 Freq, MHz     Carrier 2 Freq, MHz    Carrier 1 Power    Carrier 2 Power    Carrier Power    Units    Carrier 1 Offset    Carrier 2 Offset" +
                    //"    Carrier Offset Units    ALC Averaging    Settling Time, msec    IM Measurement    Stimulus Port    IM Order    IM Freq, MHz    IM Power    Reference Value    IM Peak Power    IM Units");
                    sw.WriteLine(
                                    "Instrument" + "\t" +
                                    "Date " + "\t" +
                                    "Time" + "\t" +
                                    "Content" + "\t" +
                                    "Test Description" + "\t" +
                                    "Model Number" + "\t" +
                                    "Serial Number" + "\t" +
                                    "Operator" + "\t" +
                                    "Carrier 1 Freq, MHz" + "\t" +
                                    "Carrier 2 Freq, MHz" + "\t" +
                                    "Carrier 1 Power" + "\t" +
                                    "Carrier 2 Power" + "\t" +
                                    "Carrier Power Units" + "\t" +
                                    "Carrier 1 Offset" + "\t" +
                                    "Carrier 2 Offset" + "\t" +
                                    "Carrier Offset Units" + "\t" +
                                    "ALC" + "\t" +
                                    "Averaging" + "\t" +
                                    "Settling Time, msec" + "\t" +
                                    "IM Measurement" + "\t" +
                                    "Stimulus Port" + "\t" +
                                    "IM Order" + "\t" +
                                    "IM Freq, MHz" + "\t" +
                                    "IM Power" + "\t" +
                                    "Reference Value" + "\t" +
                                    "IM Peak Power" + "\t" +
                                    "IM Units");
                string blank = "\t";
                   for (int i = 0; i < entries.Length; i++)
                {
                string s =
                    //Instrument
                    //"[Enter Instrument]" + blank +
                    "" + blank +
                    //Date
                    DateTime.Now.ToString("yyyy-MM-dd") + blank +
                    //Time
                    DateTime.Now.ToString("HH:mm:ss") + blank +
                    //Content
                    "Swept IM"+blank+
                    //Test Description
                    "[Enter Test Description]" + blank +
                    //Model Number
                    "[Enter Model Number]" + blank +
                    //Serial Number
                    ""+blank+
                    //Operator
                    "[Enter Operator]" + blank +
                    //Carrier 1 Freq, MHz
                    entries[i].F1.ToString("0.0") + blank +
                    //Carrier 2 Freq, MHz
                    entries[i].F2.ToString("0.0") + blank +
                    //Carrier 1 Power
                    //"ON" + blank +  //����1
                    entries[i].P1.ToString("0.0") + blank +  //����1
                    //Carrier 2 Power
                    //"ON" + blank +//����2
                    entries[i].P2.ToString("0.0") + blank +  //����1
                    //Carrier Power Units
                    "dBm" + blank +
                    //Carrier 1 Offset
                    "0.0" + blank +
                    //Carrier 2 Offset
                    "0.0" + blank +
                    //Carrier  Offset Uints
                    "dB" + blank + 
                    //ALC
                    "ON" + blank +
                    //Averaging
                    "Normal" + blank +
                    //Settling Time, msec
                    "0" + blank +
                    //IM Measurement
                    mesure + blank +
                    //Stimulus Port
                    port + blank +
                    //IM Oder
                    ((int)header.Im_Order).ToString() + "rd" + blank +
                    //IM Freq, MHz
                    entries[i].Im_F.ToString("0.0") + blank +
                    //IM Power
                    entries[i].Im_V.ToString("0.0") + blank +
                    //Reference Value
                    limit.ToString("0.000000") + blank +
                    //IM Peak Power
                    max.ToString("0.000000") + blank +
                    //IM units
                    unit;
                sw.WriteLine(s);
                   }

            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
            return result;
        }


    

        /// <summary>
        /// �ӻ���CSV�ļ�fileName��ȡ��Ϣ������ͷ����Ϣ��䵽header����ɨ������䵽�б�entries
        /// ���ɹ��򷵻�TRUE��
        /// �������ļ����ƻ�����ʽ�������߶��ļ��쳣��ת���쳣�ȣ��򷵻�FALSE
        /// </summary>
        /// <param name="entries"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        internal static bool Read_Csv_Pim(string fileName, out List<CsvReport_Pim_Entry> entries, out CsvReport_PIVH_Header header)
        {
            int v, ln;
            string sLine;
            char[] chars;
            string[] sArray;
            CsvReport_Pim_Entry pim_entry;

            StreamReader sr = null;

            //�ļ���ָʾ
            bool bCrashed = false;

            try
            {
                //����CSV�ļ���ͷ������
                header = new CsvReport_PIVH_Header();

                //������
                sr = new StreamReader(fileName, Encoding.ASCII);

                //�Թ���һ��
                sr.ReadLine();

                //��ȡ�ڶ��е�������ʱ��ֵ
                header.Date_Time = sr.ReadLine();

                //�Թ�����������
                sr.ReadLine();
                sr.ReadLine();

                //��ȡ�����У����Բ���
                //�����ң���params��ͷ��ÿ���������Կո����
                //����Ϊɨ������ָʾ�֡���������ָʾ�֡�ɨ�������ɨ��ο�ֵ
                chars = new char[1];
                chars[0] = ' ';
                sArray = sr.ReadLine().Split(chars);

                v = int.Parse(sArray[1]);
                if (v == (int)SweepType.Time_Sweep)
                    header.Swp_Type = SweepType.Time_Sweep;
                else
                    header.Swp_Type = SweepType.Freq_Sweep;

                v = int.Parse(sArray[2]);
                if (v == 0)
                    header.Im_Schema = ImSchema.REV;
                else
                    header.Im_Schema = ImSchema.FWD;

                header.Im_Order = (ImOrder)Enum.Parse(typeof(ImOrder),
                                                       Enum.GetName(typeof(ImOrder), int.Parse(sArray[3]))); 

                header.Sweep_Start = float.Parse(sArray[4]);

                header.Sweep_Stop = float.Parse(sArray[5]);

                header.Point_Num = int.Parse(sArray[6]);

                header.Limit_Value = float.Parse(sArray[7]);

                header.N = int.Parse(sArray[8]);

                //�Թ�����������
                sr.ReadLine();
                sr.ReadLine();
                
                //һ���ı�������ɨ��������
                ln = 7;               
                
                chars[0] = ',';
                entries = new List<CsvReport_Pim_Entry>();

                //�ӵڰ��п�ʼ����ȡɨ��������
                sLine = sr.ReadLine();
                while (!String.IsNullOrEmpty(sLine))
                {
                    sArray = sLine.Split(chars);

                    if (sArray.Length != ln)
                    {
                        bCrashed = true;
                        break;
                    }

                    pim_entry = new CsvReport_Pim_Entry();

                    pim_entry.No = int.Parse(sArray[0]);
                    pim_entry.P1 = float.Parse(sArray[1]);
                    pim_entry.F1 = float.Parse(sArray[2]);
                    pim_entry.P2 = float.Parse(sArray[3]);
                    pim_entry.F2 = float.Parse(sArray[4]);
                    pim_entry.Im_V = float.Parse(sArray[5]);
                    pim_entry.Im_F = float.Parse(sArray[6]);

                    entries.Add(pim_entry);

                    //��ȡ��һ�У�ֱ���ļ�����
                    sLine = sr.ReadLine();
                }

                sr.Close();
                sr.Dispose();

            } catch  {
                entries = null;
                header = null;
                bCrashed = true;
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }

            //����ֵ
            return (!bCrashed);
        }


        /// <summary>
        /// ��IVHɨ�����header��ɨ�����б�entries���浽�ļ�fileName
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="entries"></param>
        /// <param name="header"></param>
        internal static void Save_Csv_IVH(string fileName, CsvReport_IVH_Entry[] entries, CsvReport_PIVH_Header header)
        {
            StreamWriter sw = null;

            try
            {
                sw = new StreamWriter(fileName, false, Encoding.ASCII);

                string s1;
                if (header.Swp_Type == SweepType.Freq_Sweep)
                    s1 = "Frequency Sweep ";
                else
                    s1 = "Time Sweep ";

                string s2 =  "Start " + header.Sweep_Start.ToString("0.#") + " " +
                             "Stop " + header.Sweep_Stop.ToString("0.#") + " " +
                             "Point Number " + header.Point_Num.ToString() + " " +
                             "Limit Value " + header.Limit_Value.ToString("0.#");

                string sparams = "params " +
                                 ((int)header.Swp_Type).ToString() + " " +
                                 "0 " + 
                                 "3 " +
                                 header.Sweep_Start.ToString("0.#") + " " +
                                 header.Sweep_Stop.ToString("0.#") + " " +
                                 header.Point_Num.ToString() + " " +
                                 header.Limit_Value.ToString("0.#") + " " +
                                 header.Y_Max_RL.ToString("0.#") + " " +
                                 header.Y_Min_RL.ToString("0.#") + " " +
                                 header.Y_Max_VSWR.ToString("0.#") + " " +
                                 header.Y_Min_VSWR.ToString("0.#");                

                sw.WriteLine(header.Mac_Desc);
                sw.WriteLine(header.Date_Time);
                sw.WriteLine(s1);
                sw.WriteLine(s2);
                sw.WriteLine(sparams);
                sw.WriteLine("Tx Out(dbm) Im Value(dbc) Frequency(MHz)");
                sw.WriteLine("NO., P, F, Value, Noise, RL");
                string s3;
                for (int i = 0; i < entries.Length; i++)
                {
                    s3 = entries[i].No.ToString() + ", " +
                         entries[i].P.ToString("0.0000000") + ", " +
                         entries[i].F.ToString("0.0000000") + ", " +
                         entries[i].IVH_Value.ToString("0.0000000") + ", " +
                         entries[i].Noise.ToString("0.0000000") + ", " +
                         entries[i].Rl.ToString("0.0000000");

                    sw.WriteLine(s3);
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();

            } catch {
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                }
            }
        }


        /// <summary>
        /// ��IVH��CSV�ļ�fileName��ȡ��Ϣ������ͷ����Ϣ��䵽header����ɨ������䵽�б�entries
        /// ���ɹ��򷵻�TRUE��
        /// �������ļ����ƻ�����ʽ�������߶��ļ��쳣��ת���쳣�ȣ��򷵻�FALSE
        /// </summary>
        /// <param name="entries"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        internal static bool Read_Csv_IVH(string fileName, out List<CsvReport_IVH_Entry> entries, out CsvReport_PIVH_Header header)
        {
            int v, ln;
            string sLine;
            char[] chars;
            string[] sArray;
            CsvReport_IVH_Entry ivh_entry;

            StreamReader sr = null;

            //�ļ���ָʾ
            bool bCrashed = false;

            try
            {
                //����CSV�ļ���ͷ������
                header = new CsvReport_PIVH_Header();

                //������
                sr = new StreamReader(fileName, Encoding.ASCII);

                //�Թ���һ��
                sr.ReadLine();

                //��ȡ�ڶ��е�������ʱ��ֵ
                header.Date_Time = sr.ReadLine();

                //�Թ�����������
                sr.ReadLine();
                sr.ReadLine();

                //��ȡ�����У����Բ���
                //�����ң���params��ͷ��ÿ���������Կո����
                //����Ϊɨ������ָʾ�֡���������ָʾ�֡�ɨ�������ɨ��ο�ֵ
                chars = new char[1];
                chars[0] = ' ';
                sArray = sr.ReadLine().Split(chars);

                v = int.Parse(sArray[1]);
                if (v == 0)
                    header.Swp_Type = SweepType.Time_Sweep;
                else
                    header.Swp_Type = SweepType.Freq_Sweep;

                v = int.Parse(sArray[2]);
                if (v == 0)
                    header.Im_Schema = ImSchema.REV;
                else
                    header.Im_Schema = ImSchema.FWD;

                header.Im_Order = (ImOrder)Enum.Parse(typeof(ImOrder),
                                                      Enum.GetName(typeof(ImOrder), int.Parse(sArray[3]))); 

                header.Sweep_Start = float.Parse(sArray[4]);

                header.Sweep_Stop = float.Parse(sArray[5]);

                header.Point_Num = int.Parse(sArray[6]);

                header.Limit_Value = float.Parse(sArray[7]);

                header.Y_Max_RL = float.Parse(sArray[8]);

                header.Y_Min_RL = float.Parse(sArray[9]);

                header.Y_Max_VSWR = float.Parse(sArray[10]);

                header.Y_Min_VSWR = float.Parse(sArray[11]);

                //�Թ�����������
                sr.ReadLine();
                sr.ReadLine();

                //һ���ı�������ɨ��������
                ln = 6;

                chars[0] = ',';
                entries = new List<CsvReport_IVH_Entry>();

                //�ӵڰ��п�ʼ����ȡɨ��������
                sLine = sr.ReadLine();
                while (!String.IsNullOrEmpty(sLine))
                {
                    sArray = sLine.Split(chars);

                    if (sArray.Length != ln)
                    {
                        bCrashed = true;
                        break;
                    }

                    ivh_entry = new CsvReport_IVH_Entry();

                    ivh_entry.No = int.Parse(sArray[0]);
                    ivh_entry.P = float.Parse(sArray[1]);
                    ivh_entry.F = float.Parse(sArray[2]);
                    ivh_entry.IVH_Value = float.Parse(sArray[3]);
                    ivh_entry.Noise = float.Parse(sArray[4]);
                    ivh_entry.Rl = float.Parse(sArray[5]);

                    entries.Add(ivh_entry);

                    //��ȡ��һ�У�ֱ���ļ�����
                    sLine = sr.ReadLine();
                }

                sr.Close();
                sr.Dispose();

            }
            catch
            {
                entries = null;
                header = null;
                bCrashed = true;
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }

            //����ֵ
            return (!bCrashed);
        }

        
        /// <summary>
        /// ��Ƶ��ɨ�����header��ɨ�����б�entries���浽�ļ�fileName
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="entries"></param>
        /// <param name="header"></param>
        internal static void Save_Csv_Spectrum(string fileName, PointF[] entries, CsvReport_Spctrum_Header header)
        {
            StreamWriter sw = null;

            try
            {
                sw = new StreamWriter(fileName, false, Encoding.ASCII);

                string s1;
                s1 = "params " + 
                     header.Start.ToString("0.#") + " " +
                     header.Stop.ToString("0.#") + " " +
                     header.RBW.ToString() + " " +
                     header.ATT.ToString();

                sw.WriteLine(header.Mac_Desc);
                sw.WriteLine(header.Date_Time);
                sw.WriteLine(s1);

                sw.WriteLine("Frequency(MHz), Field_Value(dBm)");
    
                for (int i = 0; i < entries.Length; i++)               
                    sw.WriteLine(entries[i].X.ToString("0.0000000") + ", " + entries[i].Y.ToString("0.0000000"));

                sw.Flush();
                sw.Close();
                sw.Dispose();

            } catch {
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                }
            }
        }


        /// <summary>
        /// ��Ƶ�׷�����CSV�ļ�fileName��ȡ��Ϣ������ͷ����Ϣ��䵽header����ɨ������䵽�б�entries
        /// ���ɹ��򷵻�TRUE��
        /// �������ļ����ƻ�����ʽ�������߶��ļ��쳣��ת���쳣�ȣ��򷵻�FALSE
        /// </summary>
        /// <param name="entries"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        internal static bool Read_Csv_Spectrum(string fileName, out List<PointF> entries, out CsvReport_Spctrum_Header header)
        {
            int ln;
            string sLine;
            char[] chars;
            string[] sArray;
            PointF spc_entry;

            StreamReader sr = null;

            //�ļ���ָʾ
            bool bCrashed = false;

            try
            {
                //����CSV�ļ���ͷ������
                header = new CsvReport_Spctrum_Header();

                //������
                sr = new StreamReader(fileName, Encoding.ASCII);

                //�Թ���һ��
                sr.ReadLine();

                //��ȡ�ڶ��е�������ʱ��ֵ
                header.Date_Time = sr.ReadLine();
                
                //��ȡ�����У����Բ���
                //�����ң���params��ͷ��ÿ���������Կո����
                //����Ϊ��ʼƵ�ʡ�����Ƶ�ʡ�RBW��ATT
                chars = new char[1];
                chars[0] = ' ';
                sArray = sr.ReadLine().Split(chars);

                header.Start = float.Parse(sArray[1]);
                header.Stop  = float.Parse(sArray[2]);
                header.RBW   = int.Parse(sArray[3]);
                header.ATT   = int.Parse(sArray[4]);
                
                //�Թ�����
                sr.ReadLine();

                //һ���ı�������ɨ��������
                ln = 2;

                chars[0] = ',';
                entries = new List<PointF>();

                //�ӵ����п�ʼ����ȡɨ��������
                sLine = sr.ReadLine();
                while (!String.IsNullOrEmpty(sLine))
                {
                    sArray = sLine.Split(chars);

                    if (sArray.Length != ln)
                    {
                        bCrashed = true;
                        break;
                    }

                    spc_entry = new PointF();
                    spc_entry.X = float.Parse(sArray[0]);
                    spc_entry.Y = float.Parse(sArray[1]);

                    entries.Add(spc_entry);

                    //��ȡ��һ�У�ֱ���ļ�����
                    sLine = sr.ReadLine();
                }

                sr.Close();
                sr.Dispose();

            }
            catch
            {
                entries = null;
                header = null;
                bCrashed = true;
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }

            //����ֵ
            return (!bCrashed);
        }
    }
}
