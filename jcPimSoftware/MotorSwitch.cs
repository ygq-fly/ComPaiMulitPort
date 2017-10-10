using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace jcPimSoftware
{
    /// <summary>
    /// 电机开关类
    /// </summary>
    public  sealed class MotorSwitch
    {
        SerialPort __serialPort = null;
        string __comName;
        TcpClient __tcpClient = null;
        UdpClient __upPClient = null;
        IPEndPoint __endPoint = null;
        string __ipAddr = "127.0.0.1";
        int __ipPort = 4001;
        bool __needPush = false;
        List<string> __listError = new List<string>();

        /// <summary>
        /// 串口连接
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public bool Connect(string serial)
        {
            bool result = true;
            this.__comName = serial;
            __serialPort = new SerialPort(serial, 19200, Parity.None, 8, StopBits.One);
            __serialPort.ReceivedBytesThreshold = 12;

            try
            {
                __serialPort.Open();
            }
            catch
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// LAN连接
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool Connect(int type,string ip)
        {
            bool result = true;

            try
            {
                this.__ipAddr = ip;
                this.__ipPort = 4001;
                this.__endPoint = new IPEndPoint(IPAddress.Parse(ip), 4001);
                if (type == 0)
                {
                    __tcpClient = new TcpClient(this.__endPoint);
                __tcpClient.ReceiveTimeout = 10000;
            }
                else
                {
                    __upPClient = new UdpClient();
                }
            }
            catch(Exception ex)
            {
                __listError.Add(ex.Message);
                result = false;
            }

            return result;
        }
        public bool Connect(string ip, int port)
        {
            return Connect(1, ip);
        }
        /// <summary>
        /// 断开
        /// </summary>
        public void Disconnect()
        {
            if (this.__serialPort != null && this.__serialPort.IsOpen)
            {
                this.__serialPort.Close();
            }

            if (this.__tcpClient != null && this.__tcpClient.Connected)
            {
                this.__tcpClient.Close();
            }
            if (this.__upPClient != null)
            {
                this.__upPClient.Close();
                this.__upPClient = null;
            }
        }
        /// <summary>
        /// 获取错误代码
        /// </summary>
        /// <returns></returns>
        public string[] GetErrInfo()
        {
            string[] error = __listError.ToArray();
            __listError.Clear();
            return error;
        }
        /// <summary>
        /// 开关归零写
        /// </summary>
        /// <returns></returns>
        public bool SW_Work_Zero_Write()
        {
            bool result = true;
            Byte[] data = new Byte[4];

            result = TrxMotorSwitch(0x51, 1, new Byte[] { 0x01, 0x00, 0x00, 0x00 }, ref data);
            if (data[0] == 0xff)
                __listError.Add("PULL FAILED");
            return data[0] == 1 && result;
        }
        public bool SW_Work_Zero_Init()
        {
            bool result = true;
            Byte[] data = new Byte[4];
            result = TrxMotorSwitch(0x51, 1, new Byte[] { 0x02, 0x00, 0x00, 0x00 }, ref data);
            if (data[0] == 0xff)
                __listError.Add("PULL FAILED");

            return data[0] == 1 && result;
        }
        /// <summary>
        /// 开关归零读
        /// </summary>
        /// <returns></returns>
        public bool SW_Work_Zero_Read()
        {
            bool result = true;
            Byte[] data = new Byte[4];

            result = TrxMotorSwitch(0x51, 0, new Byte[] { 0x00, 0x00, 0x00, 0x00 }, ref data);

            return data[0] == 1 && result;
        }
        /// <summary>
        /// 工作模式（写）范围1~255
        /// </summary>
        public   bool SW_Work_Switch_Write(Byte port,bool push)
        {
            bool result = true;
            Byte[] data = new Byte[4];

            result = TrxMotorSwitch(0x52, 1, new Byte[] { port, (Byte)(push ? 0x01 : 0x00), 0x00, 0x00 }, ref data);

            return ((push ? 1 : 0) == data[1]) && result;
        }
        /// <summary>
        /// 工作模式（读）范围1~255
        /// </summary>
        /// <param name="port"></param>
        /// <param name="push"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool SW_Work_Switch_Read(Byte port, ref bool push, ref ushort position)
        {
            bool result = true;
            Byte[] data = new Byte[4];

            TrxMotorSwitch(0x52, 0, new Byte[] { port,0x00, 0x00, 0x00 }, ref data);
            position = BitConverter.ToUInt16(data, 2);

            return (push ? 1 : 0) == data[0] && result;
        }
        /// <summary>
        /// 工厂配置（读）
        /// </summary>
        /// <param name="port"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool SW_Fact_Config_Read(Byte port, ref ushort position)        
        {
            bool result = true;
            Byte[] data = new Byte[4];

            result = TrxMotorSwitch(0x53, 0, new Byte[] { port, 0x00, 0x00, 0x00 }, ref data);

            position = BitConverter.ToUInt16(data, 2);

            return result;
        }
        /// <summary>
        /// 工厂配置（写）
        /// </summary>
        /// <param name="port"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool SW_Fact_Config_Write(Byte port, ushort position)
        {
            bool result = true;
            Byte[] data = new Byte[4];

            result = TrxMotorSwitch(0x53, 1, new Byte[] { port, 0x00, (Byte)(position), (Byte)(position/256) }, ref data);

            return result;
        }
        /// <summary>
        /// 手动模式（写）
        /// </summary>
        /// <param name="port"></param>
        /// <param name="push"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool SW_Fact_User_Write(bool dir, bool push,ushort position)
        {
            bool result = true;
            Byte[] data = new Byte[4];

            result = TrxMotorSwitch(0x54, 1, new Byte[] { (Byte)(dir ? 1 : 0), (Byte)(push ? 1 : 0), (Byte)(position), (Byte)(position/256) }, ref data);

            return result;
        }
        /// <summary>
        /// 手动模式（读）
        /// </summary>
        /// <param name="port"></param>
        /// <param name="push"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool SW_Fact_User_Read(ref bool push, ref ushort position)
        {
            bool result = true;
            Byte[] data = new Byte[4];

            result = TrxMotorSwitch(0x54, 0, new Byte[] { 0x00, 0x00, 0x00, 0x00 }, ref data);

            push = (data[1] == 1);
            position = BitConverter.ToUInt16(data,2);

            return result;
        }
        /// <summary>
        /// 工作速度（写）单位HZ
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool SW_Fact_X_SpeedWork_Write(ushort speed)
        {
            bool result = true;
            Byte[] data = new Byte[4];

            result = TrxMotorSwitch(0x55, 1, new Byte[] { (Byte)(speed), (Byte)(speed >> 8) ,0x00,0x00}, ref data);

            return result;
        }
        /// <summary>
        /// 工作速度（读）单位HZ
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool SW_Fact_X_SpeedWork_Read(ref ushort speed)
        {
            bool result = true;
            Byte[] data = new Byte[4];

            result = TrxMotorSwitch(0x55, 0, new Byte[] { 0x00, 0x00, 0x00, 0x00 }, ref data);

            speed = BitConverter.ToUInt16(data, 0);

            return result;
        }
        /// <summary>
        /// 手动速度（写）单位HZ
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool SW_Fact_X_SpeedUser_Write(ushort speed)
        {
            bool result = true;
            Byte[] data = new Byte[4];

            result = TrxMotorSwitch(0x56, 1, new Byte[] { (Byte)(speed), (Byte)(speed >> 8), 0x00, 0x00 }, ref data);

            return result;
        }
        /// <summary>
        /// 手动速度（读）单位HZ
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool SW_Fact_X_SpeedUser_Read(ref ushort speed)
        {
            bool result = true;
            Byte[] data = new Byte[4];

            result = TrxMotorSwitch(0x56, 0, new Byte[] { 0x00, 0x00, 0x00, 0x00 }, ref data);

            speed = BitConverter.ToUInt16(data, 0);

            return result;
        }
        /// <summary>
        /// 回归速度（写）单位HZ
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool SW_Fact_X_SpeedReturn_Write(ushort speed)
        {
            bool result = true;
            Byte[] data = new Byte[4];

            result = TrxMotorSwitch(0x57, 1, new Byte[] { (Byte)(speed), (Byte)(speed >> 8), 0x00, 0x00 }, ref data);

            return result;
        }
        /// <summary>
        /// 回归速度（读）单位HZ
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool SW_Fact_X_SpeedReturn_Read(ref ushort speed)
        {
            bool result = true;
            Byte[] data = new Byte[4];

            result = TrxMotorSwitch(0x57, 0, new Byte[] { 0x00, 0x00, 0x00, 0x00 }, ref data);

            speed = BitConverter.ToUInt16(data, 0);

            return result;
        }
        /// <summary>
        /// 低速（写）单位HZ
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool SW_Fact_X_SpeedLow_Write(ushort speed)
        {
            bool result = true;
            Byte[] data = new Byte[4];

            result = TrxMotorSwitch(0x58, 1, new Byte[] { (Byte)(speed), (Byte)(speed >> 8), 0x00, 0x00 }, ref data);

            return result;
        }
        /// <summary>
        /// 低速（读）单位HZ
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool SW_Fact_X_SpeedLow_Read(ref ushort speed)
        {
            bool result = true;
            Byte[] data = new Byte[4];

            result = TrxMotorSwitch(0x58, 0, new Byte[] { 0x00, 0x00, 0x00, 0x00 }, ref data);

            speed = BitConverter.ToUInt16(data, 0);

            return result;
        }
        /// <summary>
        /// 占空比（写）单位HZ
        /// </summary>
        /// <param name="ratio"></param>
        /// <returns></returns>
        public bool SW_Fact_X_Space_Write(Byte ratio)
        {
            bool result = true;
            Byte[] data = new Byte[4];

            result = TrxMotorSwitch(0x59, 1, new Byte[] { ratio, 0x00, 0x00, 0x00 }, ref data);

            return result;
        }
        /// <summary>
        /// 占空比（读）单位HZ
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public bool SW_Fact_X_Space_Read(ref Byte ratio)
        {
            bool result = true;
            Byte[] data = new Byte[4];

            result = TrxMotorSwitch(0x59, 0, new Byte[] { 0x00, 0x00, 0x00, 0x00 }, ref data);

            ratio = data[0];

            return result;
        }
        /// <summary>
        /// 交互
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="dir"></param>
        /// <param name="data"></param>
        /// <param name="rcv"></param>
        /// <returns></returns>
        private   bool TrxMotorSwitch(Byte cmd, Byte dir, Byte[] data, ref Byte[] rcv)
        {
            Byte[] packet = new Byte[] { 0xF5, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            packet[4] = dir;
            packet[6] = cmd;
            Array.Copy(data, 0, packet, 7, 4);

            Byte check = 0;
            for (int i = 0; i < 11; i++)
            {
                check ^= packet[i];
            }
            packet[11] = check;

            bool result = false;

            if (this.__serialPort != null && this.__serialPort.IsOpen)
            {
                ManualResetEvent signal = new ManualResetEvent(false);

                SerialDataReceivedEventHandler handlerRcv = delegate(object sender, SerialDataReceivedEventArgs e)
                {
                    signal.Set();
                };

                this.__serialPort.DataReceived += handlerRcv;

                new Thread(new ThreadStart(delegate
                {
                    this.__serialPort.Write(packet, 0, packet.Length);
                })).Start();

                if (signal.WaitOne(10000))
                {
                    this.__serialPort.Read(packet, 0, packet.Length);
                    Array.Copy(packet, 7, rcv, 0, 4);
                    result = true;
                }
                else
                {
                    __listError.Add("Serial Read Timeout!");
                    result = false;
                }

                this.__serialPort.DataReceived -= handlerRcv;
            }

            if (this.__tcpClient != null && this.__tcpClient.Connected)
            {
                try
                {
                    ManualResetEvent signal = new ManualResetEvent(false);

                    this.__tcpClient.GetStream().Write(packet, 0, packet.Length);

                    IAsyncResult iar = this.__tcpClient.GetStream().BeginRead(packet, 0, packet.Length,
                        new AsyncCallback(delegate { signal.Set(); }), null);

                    result = signal.WaitOne(10000);

                    if (result)
                    {
                        Array.Copy(packet, 7, rcv, 0, 4);
                    }
                }
                catch
                {
                    __listError.Add("TCP Read Timeout!");
                    this.__tcpClient = null;
                    this.__tcpClient = new TcpClient(this.__endPoint);
                }
            }

            if (this.__upPClient != null )
            {
                try
                {
                    ManualResetEvent signal = new ManualResetEvent(false);

                    this.__upPClient.Send(packet, packet.Length,this.__endPoint);

                    IAsyncResult iar = this.__upPClient.BeginReceive(new AsyncCallback(delegate {
                        //闭包中更新标志
                        signal.Set();
                    }), null);

                    result = signal.WaitOne(10000);

                    if (result)
                    {
                        byte[] buf = this.__upPClient.EndReceive(iar, ref this.__endPoint);
                        Array.Copy(buf, 7, rcv, 0, 4); 
                    }
                }
                catch
                {
                    __listError.Add("UDP Read Timeout!");
                    result = false;
                }
            }

            if (result)
            {//CHECK
                check = 0;
                for (int i = 0; i < 11; i++)
                {
                    check ^= packet[i];
                }

                result = (packet[11] == check); 
             
                if(!result)
                    __listError.Add("Receive wrong check!");
            }

            return result;
        }
    }
}
