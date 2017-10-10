using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Msg_App_APP
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct tagMSG
    {
        /// HWND->HWND__*
        public IntPtr hwnd;

        /// UINT->unsigned int
        public uint message;

        /// WPARAM->UINT_PTR->unsigned int
        public uint wParam;

        /// LPARAM->LONG_PTR->int
        public int lParam;

        /// DWORD->unsigned int
        public uint time;

        /// POINT->tagPOINT
        public Point pt;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct HWND__
    {
        /// int
        public int unused;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct Point
    {
        /// LONG->int
        public int x;

        /// LONG->int
        public int y;
    }


    class CMessage
    {
        public const uint HWND_BROADCAST = 0xFFFF;
        public const uint ACIN = 0xBB;
        public const uint ACOUT = 0x33;
        public const uint WM_ASKACISIN = 0x0400 + 1023;
        public const uint WM_ACKACISIN = 0x0400 + 1024;

        //BOOL GetMessage(LPMSG lpMsg,
        //                HWND hWnd,
        //                UINT wMsgFilterMin,
        //                UINT wMsgFilterMax
        //                );
        [DllImport("User32.dll", CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetMessage([OutAttribute()] out tagMSG lpMsg,
                                               [InAttribute()] IntPtr hWnd,
                                               uint wMsgFilterMin,
                                               uint wMsgFilterMax);

        //BOOL PostMessage(HWND hWnd,
        //                UINT Msg,
        //                WPARAM wParam,
        //                LPARAM lParam
        //            );
        [DllImport("User32.dll", CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PostMessage([InAttribute] IntPtr hWnd,
                                                      uint Msg,
                                                      uint wParam,
                                                      int lParam);

        //HWND FindWindow(LPCTSTR lpClassName,
        //LPCTSTR lpWindowName
        //);
        [DllImport("User32.dll", CharSet = CharSet.Ansi)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindoNname);


        internal static uint IsACin(string WindowName)
        {
            tagMSG MSG;
            IntPtr hwndTarget = FindWindow(null, WindowName);

            if (hwndTarget.ToInt32() != 0)
            {
                PostMessage(hwndTarget, WM_ASKACISIN, 0, 0);

                GetMessage(out MSG, IntPtr.Zero, WM_ACKACISIN, WM_ACKACISIN);

                return MSG.wParam;

            }
            else
                return 0;
        }

        internal static void ACin(string WindowName)
        {
            IntPtr hwndTarget = FindWindow(null, WindowName);

            if (hwndTarget.ToInt32() != 0)
                PostMessage(hwndTarget, WM_ACKACISIN, ACIN, 0);
        }

        internal static void ACout(string WindowName)
        {
            IntPtr hwndTarget = FindWindow(null, WindowName);

            if (hwndTarget.ToInt32() != 0)
                CMessage.PostMessage(hwndTarget, WM_ACKACISIN, ACOUT, 0);
        }

    }
}
