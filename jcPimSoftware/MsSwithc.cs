using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace jcPimSoftware
{
    internal  class MsSwithc
    {
       public static   com_io_ctl.com_io_ctl cic;
        
        public static  bool  ClientCom()
        {
            try
            {
                string addCom = IniFile.GetString("GPIO_Board", "addrCOm", "", Application.StartupPath + "\\Configures.ini");
                string addLan = IniFile.GetString("GPIO_Board", "addrLan", "", Application.StartupPath + "\\Configures.ini");
                cic = new com_io_ctl.com_io_ctl(Application.StartupPath + "\\io_mobi2_6.ini");
                //cic.OpenCom("COM" + App_Configure.Cnfgs.Comaddr_switch);
                if (addCom != "")
                    cic.OpenCom(addCom);
                else if (addLan != "")
                {
                    cic.TcpConnect(addLan, 4001);
                }
                else
                    cic.TcpConnect("192.168.1.178", 4001);
                return true;
            }
            catch
            {
                return false;
            }
          
        }
        public static  bool CheckPort(int num)
        {
            bool result = cic.BaseStateWrite(num);
            Thread.Sleep(100);
            return result;
        }
    }
}
