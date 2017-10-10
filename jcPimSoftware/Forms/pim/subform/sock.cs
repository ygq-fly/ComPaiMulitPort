using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace jcPimSoftware
{
    class sock
    {
        public decimal f1;
        public decimal f2;
        public decimal pow;
        public decimal step;
        List<decimal> arr = new List<decimal>();
        public decimal f1_;
        public decimal f2_;
        public decimal pow_;
        public decimal step_;
        public decimal _fs;
        public decimal _fe;
        public decimal _pow;
        public decimal _step1;
        public decimal _step2;
        public decimal _time;
        delegate string del(Socket s);
        IntPtr i;
        public sock(IntPtr i)
        {
            Socket sockwatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string path = Application.StartupPath;
            this.i = i;
            string getip = IniFile.GetString("connServer", "addr", "0", Application.StartupPath + "\\SqlInfo.ini");
            if (getip == "0")
            {
                MessageBox.Show("读取ip错误");
                return;
            }
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(getip),6307);
            sockwatch.Bind(ip);
            sockwatch.Listen(10);
            Thread th2 = new Thread(SocketC);
            th2.IsBackground = true;
            th2.Start(sockwatch);
        }

        Dictionary<string, Socket> dicMe = new Dictionary<string, Socket>();
        string ippoint;
        Socket socksend;

      public   void SendMessage(string s)
        {
          
            byte[] buffer = Encoding.UTF8.GetBytes(s);
            socksend.Send(buffer);

        }

        void SocketC(object o)
        {

            Socket soc = o as Socket;

            try
            {
                while (true)
                {
                    if (dicMe.Count == 0)
                    {
                        socksend = soc.Accept();
                        //if (socksend.Connected )
                        //    break;
                        // }
                        MessageBox.Show(socksend.RemoteEndPoint.ToString() + "连接成功");
                        ippoint = socksend.RemoteEndPoint.ToString();
                        dicMe.Add(ippoint, socksend);

                        Thread th = new Thread(ReciveMes);
                        th.IsBackground = true;
                        th.Start(socksend);
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //socket 
        }
        void ReciveMes(object o)
        {
            string str = "";
            Socket sock = o as Socket;
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024];                   
                    int r = sock.Receive(buffer);
                    if (r != 0)
                    {
                        str = Encoding.UTF8.GetString(buffer, 0, r);
                        string[] s = str.Split(':');
                        switch (s[0])
                        {
                            case "@9":
                                try 
                                {
                                    if (s.Length == 7)
                                    {
                                        if (Convert.ToSingle(s[1]) < App_Settings.sgn_1.Min_Freq) s[1] = (App_Settings.sgn_1.Min_Freq).ToString();
                                        if (Convert.ToSingle(s[1]) > App_Settings.sgn_1.Max_Freq) s[1] = (App_Settings.sgn_1.Max_Freq).ToString();
                                        if (Convert.ToSingle(s[2]) < App_Settings.sgn_2.Min_Freq) s[2] = (App_Settings.sgn_2.Max_Freq).ToString();
                                        if (Convert.ToSingle(s[2]) > App_Settings.sgn_2.Max_Freq) s[2] = (App_Settings.sgn_2.Max_Freq).ToString();
                                        if (Convert.ToSingle(s[3]) < App_Settings.sgn_2.Min_Power) s[3] = (App_Settings.sgn_2.Min_Power).ToString();
                                        if (Convert.ToSingle(s[3]) > App_Settings.sgn_2.Max_Power) s[3] = (App_Settings.sgn_2.Max_Power).ToString();
                                        if (Convert.ToSingle(s[4]) < 0 || Convert.ToSingle(s[4]) > (Convert.ToSingle(s[2]) - Convert.ToSingle(s[1]))) s[4] = "0.1";
                                        if (Convert.ToSingle(s[5]) < 0 || Convert.ToSingle(s[5]) > (Convert.ToSingle(s[2]) - Convert.ToSingle(s[1]))) s[5] = "0.1";
                                        _fs = Convert.ToDecimal(s[1]);
                                        _fe = Convert.ToDecimal(s[2]);
                                        _pow = Convert.ToDecimal(s[3]);
                                        _step1 = Convert.ToDecimal(s[4]);
                                        _step2 = Convert.ToDecimal(s[5]);
                                        _time = Convert.ToDecimal(s[6]);
                                        NativeMessage.PostMessage(i, MessageID.Start_F1F2_ , 0, 0);
                                    }
                                }
                                    catch 
                                {
                                    NativeMessage.PostMessage(i, MessageID.Error_, 1, 0);
                                 }
                                break;
                            case "@2":
                                try
                                {
                                    if (s.Length == 5)
                                    {
                                        if (Convert.ToSingle(s[1]) < App_Settings.sgn_2.Min_Freq) s[1] =(App_Settings.sgn_2.Min_Freq).ToString();
                                        if (Convert.ToSingle(s[1]) > App_Settings.sgn_2.Max_Freq) s[1] =(App_Settings.sgn_2.Min_Freq).ToString();
                                        if (Convert.ToSingle(s[2]) < App_Settings.sgn_2.Min_Freq) s[2]= (App_Settings.sgn_2.Max_Freq).ToString();
                                        if (Convert.ToSingle(s[2]) > App_Settings.sgn_2.Max_Freq) s[2]= (App_Settings.sgn_2.Max_Freq).ToString();
                                        if (Convert.ToSingle(s[3]) < App_Settings.sgn_2.Min_Power) s[3] =(App_Settings.sgn_2.Min_Power).ToString();
                                        if (Convert.ToSingle(s[3]) > App_Settings.sgn_2.Max_Power) s[3] =(App_Settings.sgn_2.Max_Power).ToString();
                                        if (Convert.ToSingle(s[4]) < 0 || Convert.ToSingle(s[4]) > (Convert.ToSingle(s[2]) - Convert.ToSingle(s[1]))) s[4]="0.1";
                                        f1 = Convert.ToDecimal(s[1]);
                                        f2 = Convert.ToDecimal(s[2]);
                                        pow = Convert.ToDecimal(s[3]);
                                        step = Convert.ToDecimal(s[4]);
                                        NativeMessage.PostMessage(i, MessageID.Setfps_, 0, 0);
                                    }
                                }
                                catch
                                {
                                    NativeMessage.PostMessage(i, MessageID.Error_, 1, 0);
                                }
                                break;
                            case "@1":
                                try
                                {
                                    if (s.Length == 5)
                                    {
                                        if (Convert.ToSingle(s[1]) < App_Settings.sgn_1.Min_Freq) s[1] =(App_Settings.sgn_1.Min_Freq).ToString();
                                        if (Convert.ToSingle(s[1]) > App_Settings.sgn_1.Max_Freq) s[1] =(App_Settings.sgn_1.Min_Freq).ToString();
                                        if (Convert.ToSingle(s[2]) < App_Settings.sgn_1.Min_Freq) s[2] =(App_Settings.sgn_1.Max_Freq).ToString();
                                        if (Convert.ToSingle(s[2]) > App_Settings.sgn_1.Max_Freq) s[2]=(App_Settings.sgn_1.Max_Freq).ToString();
                                        if (Convert.ToSingle(s[3]) < App_Settings.sgn_1.Min_Power) s[3] =(App_Settings.sgn_1.Min_Power).ToString();
                                        if (Convert.ToSingle(s[3]) > App_Settings.sgn_1.Max_Power) s[3]= (App_Settings.sgn_1.Max_Power).ToString();
                                        if (Convert.ToSingle(s[4]) < 0 || Convert.ToSingle(s[4]) > (Convert.ToSingle(s[2]) - Convert.ToSingle(s[1]))) s[4] = "0.1";
                                        f1_ = Convert.ToDecimal(s[1]);
                                        f2_ = Convert.ToDecimal(s[2]);
                                        pow_ = Convert.ToDecimal(s[3]);
                                        step_ = Convert.ToDecimal(s[4]);

                                        NativeMessage.PostMessage(i, MessageID.Setfps_2, 0, 0);
                                    }
                                }
                                catch
                                {
                                    NativeMessage.PostMessage(i, MessageID.Error_, 1, 0);
                                }
                                break;
                            case "@3":
                                try
                                {
                                    if (s.Length == 2 && s[1] == "0")
                                    {
                                        NativeMessage.PostMessage(i, MessageID.Start__, 0, 0);
                                    }
                                }
                                catch
                                {
                                    NativeMessage.PostMessage(i, MessageID.Error_, 1, 0);
                                }
                                break;
                            case "@4":
                                try 
                                {
                                    if (s.Length == 2 && s[1] == "0")
                                    {
                                        NativeMessage.PostMessage(i, MessageID.Mode_, 0, 0);
                                    }
                                }
                                 catch
                                {
                                    NativeMessage.PostMessage(i, MessageID.Error_, 1, 0);
                                }
                                break;
                            case "@5":
                                try
                                {
                                    if (s.Length == 2 && s[1] == "3" || s[1] == "5" || s[1] == "7" || s[1] == "9")
                                    {
                                        NativeMessage.PostMessage(i, MessageID.Order__, 0, Convert.ToInt32(s[1]));
                                    }
                                    else
                                    {
                                        NativeMessage.PostMessage(i, MessageID.Error_, 1, 1);
                                    }
                                }
                                catch
                                {
                                    NativeMessage.PostMessage(i, MessageID.Error_, 1, 0);
                                }
                                break;
                            case "@6":
                                try
                                {
                                    if (s.Length == 2 && s[1] == "1" || s[1] == "2")
                                    {
                                        NativeMessage.PostMessage(i, MessageID.FWE_, 0, Convert.ToInt32(s[1]));
                                    }
                                    else
                                    {
                                        NativeMessage.PostMessage(i, MessageID.Error_, 1, 1);
                                    }
                                }
                                catch
                                {
                                    NativeMessage.PostMessage(i, MessageID.Error_, 1, 0);
                                }
                                break;
                            case "@7":
                                try
                                {
                                    if (s.Length == 2 && s[1] == "0")
                                    {
                                        NativeMessage.PostMessage(i, MessageID.Unit_, 0, 0);
                                    }
                                }
                                catch
                                {
                                    NativeMessage.PostMessage(i, MessageID.Error_, 1, 0);
                                }
                                break;
                            case "@8":
                                try
                                {
                                    if (s.Length == 2 && s[1] == "0")
                                    {
                                        dicMe[ippoint].Close();
                                        dicMe.Remove(ippoint);
                                    }
                                }
                                catch
                                {
                                    NativeMessage.PostMessage(i, MessageID.Error_, 1, 0);
                                }
                                break;
                            default:
                                NativeMessage.PostMessage(i, MessageID.Error_, 1, 0);
                                break;
                        }
                    }
                    else
                    {
                        NativeMessage.PostMessage(i, MessageID.Error_, 1, 0);
                    }

                }
            }
            catch
            {
                if (dicMe.Count != 0)
                {
                    dicMe[ippoint].Close();
                    dicMe.Clear();
                }

            }
        }
    }
}
