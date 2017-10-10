using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace jcPimSoftware
{
    class SaveCsv
    {


        private bool SaveT(string csvFileName, CsvReport_Pim_Entry[] cp,float limit,ImSchema isc,ImOrder imoder)
        {
            try
            {
                if (!File.Exists(csvFileName))
                {


                    SaveTxt(csvFileName, cp, limit, isc, imoder, true);

                    return true;
                }
                else
                {
                    MessageBox.Show( "The TXT file name has already existed!");
                    return false;
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("保存TXT文件异常：" + e.ToString(), Log.EFunctionType.PIM);
                return false;
            }
        }



        public bool SaveTxt(string path, CsvReport_Pim_Entry[] entries, float limits,ImSchema isc,ImOrder imoder, bool isdbm)
        {


            double limit = limits;
            string unit = "dBm";
            float max = float.MinValue;
            for (int i = 0; i < entries.Length; i++)
            {
                if (max <= entries[i].Im_V) max = entries[i].Im_V;
            }
            //if (!isdbm)
            //{
            //    unit = "dBc";
            //    max = max - cjt.pow1;
            //}
            string mesure = "REV";
            string port = "Port 1";
            if (isc == ImSchema.FWD)
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
                        "Swept IM" + blank +
                        //Test Description
                        "[Enter Test Description]" + blank +
                        //Model Number
                        "[Enter Model Number]" + blank +
                        //Serial Number
                        "" + blank +
                        //Operator
                        "[Enter Operator]" + blank +
                        //Carrier 1 Freq, MHz
                        entries[i].F1.ToString("0.0") + blank +
                        //Carrier 2 Freq, MHz
                        entries[i].F2.ToString("0.0") + blank +
                        //Carrier 1 Power
                        //"ON" + blank +  //功率1
                        entries[i].P1.ToString("0.0") + blank +  //功率1
                        //Carrier 2 Power
                        //"ON" + blank +//功率2
                        entries[i].P2.ToString("0.0") + blank +  //功率1
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
                        ((int)imoder).ToString() + "rd" + blank +
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
    }
}
