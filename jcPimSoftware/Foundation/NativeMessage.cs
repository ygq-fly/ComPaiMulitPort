using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace jcPimSoftware
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
        public Point__ pt;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct HWND__
    {
        /// int
        public int unused;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct Point__
    {
        /// LONG->int
        public int x;

        /// LONG->int
        public int y;
    }

    
    class NativeMessage
    {
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
    }
}
