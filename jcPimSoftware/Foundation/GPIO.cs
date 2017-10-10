using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Msg_App_APP;
using susiGpio;
using System.Windows.Forms;

namespace jcPimSoftware
{
    internal class GPIO
    {
        #region DLL导入

        [DllImport("winio.dll", SetLastError = true, ThrowOnUnmappableChar = true)]
        public static extern bool InitializeWinIo();
        [DllImport("winio.dll", SetLastError = true, ThrowOnUnmappableChar = true)]
        public static extern void ShutdownWinIo();
        [DllImport("winio.dll", SetLastError = true, ThrowOnUnmappableChar = true)]
        public static extern bool SetPortVal(int wPortAddr, int dwPortVal, byte bSize);
        [DllImport("winio.dll", SetLastError = true, ThrowOnUnmappableChar = true)]
        public static extern bool GetPortVal(int wPortAddr, int[] pdwPortVal, byte bSize);

        #endregion


        #region 变量定义

        static  int comAddr = 1;

        /// <summary>
        /// GPIO状态 true正常 false不正常
        /// </summary>
        private static bool gpioSucc = false;

        /// <summary>
        /// 电池应用程序标志位 0不用 1启用
        /// </summary>
        private static int EnableBattery = App_Configure.Cnfgs.Battery;

        /// <summary>
        /// 设置带宽的管脚
        /// </summary>
        private static int Width_pinNum = App_Configure.Cnfgs.Width_pinNum;
        /// <summary>
        /// 获取市电的管脚
        /// </summary>
        private static int Power_pinNum = App_Configure.Cnfgs.Power_pinNum;

        #endregion


        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public GPIO()
        {

        }

        #endregion


        #region 方法

        #region 初始化WinIO
        /// <summary>
        /// 初始化WinIO
        /// </summary>
        /// <returns>true成功 false失败</returns>
        public static bool InitWinIo()
        {
            try
            {
                return InitializeWinIo();
            }
            catch { return false; }
        }

        #endregion

        #region 关闭WinIO
        /// <summary>
        /// 关闭WinIO
        /// </summary>
        /// <returns></returns>
        public static void CloseWinIo()
        {
            ShutdownWinIo();
        }

        #endregion


        #region 初始化GPIO
        /// <summary>
        /// 初始化GPIO
        /// </summary>
        public static bool InitGpio()
        {
            bool rev = false;
            if (susiGpio.susiGpio.InitGpio())
            {
                susiGpio.susiGpio.SetMark();
                gpioSucc = true;
                rev = true;
            }

            return rev;
        }

        #endregion

        #region 关闭GPIO
        /// <summary>
        /// 关闭GPIO
        /// </summary>
        public static bool CloseGpio()
        {
            return susiGpio.susiGpio.UninitGpio();
        }

        #endregion


        #region 切换窄带
        /// <summary>
        /// 切换窄带
        /// </summary>
        /// <returns>true成功 false失败</returns>
        public static bool Narrowband()
        {
            bool rev = false;

            if (App_Configure.Cnfgs.Gpio == 0)
            {
                if (SetPortVal(0x084d, 0x00, 1))
                {
                    App_Configure.Cnfgs.Channel = 0;
                    rev = true;
                }
            }
            else
            {
                byte pinNum = Convert.ToByte(Width_pinNum.ToString(), 10);
                if (gpioSucc)
                {
                    if (susiGpio.susiGpio.WriteExLow(pinNum))
                    {
                        App_Configure.Cnfgs.Channel = 0;
                        rev = true;
                    }
                }
            }

            return rev;
        }

        #endregion

        #region 切换宽带
        /// <summary>
        /// 切换宽带
        /// </summary>
        /// <returns>true成功 false失败</returns>
        public static bool Wideband()
        {
            bool rev = false;

            if (App_Configure.Cnfgs.Gpio == 0)
            {
                if (SetPortVal(0x084d, 0x01, 1))
                {
                    App_Configure.Cnfgs.Channel = 1;
                    rev = true;
                }
            }
            else
            {
                byte pinNum = Convert.ToByte(Width_pinNum.ToString(), 10);
                if (gpioSucc)
                {
                    if (susiGpio.susiGpio.WriteExHigh(pinNum))
                    {
                        App_Configure.Cnfgs.Channel = 1;
                        rev = true;
                    }
                }
            }

            return rev;
        }

        #endregion


        #region Rev
        /// <summary>
        /// Rev
        /// </summary>
        /// <returns>true成功 false失败</returns>
        public static bool Rev()
        {
            bool rev = false;
            int[] value = new int[1];

			if (App_Configure.Cnfgs.Gpio == 0)
			{
				GetPortVal(0x084d, value, 1);
				if (SetPortVal(0x084d, value[0] & 0xfe, 1))
				{
					App_Configure.Cnfgs.Channel = 0;
					rev = true;
				}


			}
			else if (App_Configure.Cnfgs.Gpio == 4)
			{
				int[] original = new int[1];

				if (GetPortVal(0x0a00, original, 1) == false)
					return false;
				rev =  SetPortVal(0x0a00, original[0] & 0xfe, 1);
                if (App_Configure.Cnfgs.Ms_switch_port_count >= 1)
                {
                    RFSignal.RFClear(comAddr, RFPriority.LvlTwo);
                    RFSignal.RFAssistGpo(comAddr, RFPriority.LvlTwo, 0);
                    RFSignal.RFStart(comAddr);
                }
			}
            if (App_Configure.Cnfgs.Ms_switch_port_count >= 2 && App_Configure.Cnfgs.Gpio == 0)
            {
                GetPortVal(0x084d, value, 1);
                if (SetPortVal(0x084d, value[0] & 0xfe, 1))
                {
                    App_Configure.Cnfgs.Channel = 0;
                    rev = true;
                }
                if (App_Configure.Cnfgs.Ms_switch_port_count >= 1)
                {
                    RFSignal.RFClear(comAddr, RFPriority.LvlTwo);
                    RFSignal.RFAssistGpo(comAddr, RFPriority.LvlTwo, 0);
                    RFSignal.RFStart(comAddr);
                }

            }

          
       

			return rev;
        }

        public static bool Rev2()
        {
            bool rev = false;
            int[] value = new int[1];

            //if (App_Configure.Cnfgs.Gpio == 0)
            //{
            GetPortVal(0x084d, value, 1);
            if (SetPortVal(0x084d, value[0] & 0xffdf, 1))
            {
                App_Configure.Cnfgs.Channel = 0;
                rev = true;
            }


            return rev;
        }

        public static bool Rev(int num)
        {
            bool rev = false;
            int[] value = new int[1];

            //if (App_Configure.Cnfgs.Gpio == 0)
            //{
            if (num == 1)
            {
                // GPO 1= 0，2=0
                //GetPortVal(0x084d, value, 1);
                if (SetPortVal(0x084d,0, 1))
                {
                    App_Configure.Cnfgs.Channel = 0;
                    rev = true;
                }
                //GetPortVal(0x084d, value, 1);
                //if (SetPortVal(0x084d, 0x01, 1))
                //{
                //    App_Configure.Cnfgs.Channel = 0;
                //    rev = true;
                //}         
            }
            else if (num == 2)
            {
                // GPO 1= 1，2=0
                //GetPortVal(0x084d, value, 1);
                if (SetPortVal(0x084d, 0x01, 1))
                {
                    App_Configure.Cnfgs.Channel = 1;
                    rev = true;
                }
                //GetPortVal(0x084d, value, 1);
                //if (SetPortVal(0x084d, (value[0] & 0xffbf), 1))
                //{
                //    App_Configure.Cnfgs.Channel = 1;
                //    rev = true;
                //}
            }
            //}
            //else
            //{
            //    byte pinNum = Convert.ToByte(Width_pinNum.ToString(), 10);
            //    if (gpioSucc)
            //    {
            //        if (susiGpio.susiGpio.WriteExLow(pinNum))
            //        {
            //            App_Configure.Cnfgs.Channel = 0;
            //            rev = true;
            //        }
            //    }
            //}

            return rev;
        }

        #endregion

        #region Fwd
        /// <summary>
        /// Fwd
        /// </summary>
        /// <returns>true成功 false失败</returns>
        public static bool Fwd()
        {
            bool rev = false;
            int[] value = new int[1];

			if (App_Configure.Cnfgs.Gpio == 0)
			{
				GetPortVal(0x084d, value, 1);
				if (SetPortVal(0x084d, value[0] | 0x01, 1))
				{
					App_Configure.Cnfgs.Channel = 1;
					rev = true;
				}

              
				//}
				//else
				//{
				//    byte pinNum = Convert.ToByte(Width_pinNum.ToString(), 10);
				//    if (gpioSucc)
				//    {
				//        if (susiGpio.susiGpio.WriteExHigh(pinNum))
				//        {
				//            App_Configure.Cnfgs.Channel = 1;
				//            rev = true;
				//        }
				//    }
				//}
			}
			else if (App_Configure.Cnfgs.Gpio == 4)
			{
				int[] original = new int[1];

				if (GetPortVal(0x0a00, original, 1) == false)
					return false;
				rev =  SetPortVal(0x0a00, original[0] | 0x01, 1);
                if (App_Configure.Cnfgs.Ms_switch_port_count >= 1)
                {
                    RFSignal.RFClear(comAddr, RFPriority.LvlTwo);
                    RFSignal.RFAssistGpo(comAddr, RFPriority.LvlTwo, 1);
                    RFSignal.RFStart(comAddr);
                }
			}

			return rev;
        }
        public static bool Fwd2()
        {
            bool rev = false;
            int[] value = new int[1];

            //if (App_Configure.Cnfgs.Gpio == 0)
            //{
            GetPortVal(0x084d, value, 1);
            if (SetPortVal(0x084d, value[0] | 0x40, 1))
            {
                App_Configure.Cnfgs.Channel = 1;
                rev = true;
            }
            


            return rev;
        }

        #endregion
        public static bool Fwd(int num)
        {
            bool rev = false;
            int[] value = new int[1];

            //if (App_Configure.Cnfgs.Gpio == 0)
            //{
            if (num == 1)
            {                
                // GPO 1= 0，2=1
                //GetPortVal(0x084d, value, 1);
                if (SetPortVal(0x084d, 0x80, 1))
                {
                    App_Configure.Cnfgs.Channel = 0;
                    rev = true;
                }
                //GetPortVal(0x084d, value, 1);
                //if (SetPortVal(0x084d, (value[0] | 0x40), 1))
                //{
                //    App_Configure.Cnfgs.Channel = 0;
                //    rev = true;
                //}
            }
            else if (num == 2)
            {
                // GPO 1= 1，2=1
                //GetPortVal(0x084d, value, 1);
                if (SetPortVal(0x084d, 0x81, 1))
                {
                    App_Configure.Cnfgs.Channel = 1;
                    rev = true;
                }
                //GetPortVal(0x084d, value, 1);
                //if (SetPortVal(0x084d, 0x40, 1))
                //{
                //    App_Configure.Cnfgs.Channel = 1;
                //    rev = true;
                //}
            }
            //
            if (App_Configure.Cnfgs.Ms_switch_port_count >= 2 && App_Configure.Cnfgs.Gpio == 0)
            {
                int[] original = new int[1];

                GetPortVal(0x084d, value, 1);
                if (SetPortVal(0x084d, value[0] | 0x01, 1))
                {
                    App_Configure.Cnfgs.Channel = 1;
                    rev = true;
                }
                if (App_Configure.Cnfgs.Ms_switch_port_count >= 1)
                {
                    RFSignal.RFClear(comAddr, RFPriority.LvlTwo);
                    RFSignal.RFAssistGpo(comAddr, RFPriority.LvlTwo, 1);
                    RFSignal.RFStart(comAddr);
                }
            }

            return rev;
        }

        #region High
        /// <summary>
        /// High
        /// </summary>
        /// <returns></returns>
        public static bool SetHigh()
        {
            bool rev = false;
            int[] value = new int[1];
            byte pinNum = Convert.ToByte(Width_pinNum.ToString(), 10);

            GetPortVal(0x084d, value, 1);
            rev = SetPortVal(0x084d, value[0] | 0x80, 1);
            return rev;
        }

        #endregion

        #region Low
        /// <summary>
        /// Low
        /// </summary>
        /// <returns></returns>
        public static bool SetLow()
        {
            bool rev = false;
            int[] value = new int[1];
            byte pinNum = Convert.ToByte(Width_pinNum.ToString(), 10);

            GetPortVal(0x084d, value, 1);
            rev = SetPortVal(0x084d, value[0] & 0x7f, 1);
            return rev;
        }

        #endregion


        #region 获取当前供电情况

        public static int PowerState()
        {
            int rev = -1;

            //WINIO检测市电
            if (EnableBattery == 0)
            {
                if (App_Configure.Cnfgs.Gpio == 0)
                {
                    int[] value = new int[1];
                    if (GetPortVal(0x084E, value, 1))
                    {
                        if ((value[0] & 0x40) != 0)
                        {
                            rev = 1;
                        }
                        else
                        {
                            rev = 0;
                        }
                    }
                }
                else
                {
                    int v;
                    byte pinNum = Convert.ToByte(Power_pinNum.ToString(), 10);
                    if (gpioSucc)
                    {
                        susiGpio.susiGpio.ReadEx(pinNum, out v);
                        rev = v;
                    }
                }
            }
            //电池服务
            else
            {
                uint v = CMessage.IsACin("Battery_flag");

                if (v == CMessage.ACIN)
                    rev = 1;

                if (v == CMessage.ACOUT)
                    rev = 0;
            }

            return 1;
        }

        #endregion

        #endregion

    }
}
