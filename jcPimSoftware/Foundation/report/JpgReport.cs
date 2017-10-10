using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace jcPimSoftware
{
    class JpgReport
    {
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDC(
         string lpszDriver,        // driver name������
         string lpszDevice,        // device name�豸��
         string lpszOutput,        // not used; should be NULL
         IntPtr lpInitData  // optional printer data
         );
        [DllImport("gdi32.dll")]
        public static extern int BitBlt(
         IntPtr hdcDest, // handle to destination DCĿ���豸�ľ��
         int nXDest,  // x-coord of destination upper-left cornerĿ���������Ͻǵ�X����
         int nYDest,  // y-coord of destination upper-left cornerĿ���������Ͻǵ�Y����
         int nWidth,  // width of destination rectangleĿ�����ľ��ο��
         int nHeight, // height of destination rectangleĿ�����ľ��γ���
         IntPtr hdcSrc,  // handle to source DCԴ�豸�ľ��
         int nXSrc,   // x-coordinate of source upper-left cornerԴ��������Ͻǵ�X����
         int nYSrc,   // y-coordinate of source upper-left cornerԴ��������Ͻǵ�Y����
         UInt32 dwRop  // raster operation code��դ�Ĳ���ֵ
         );

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(
         IntPtr hdc // handle to DC
         );

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(
         IntPtr hdc,        // handle to DC
         int nWidth,     // width of bitmap, in pixels
         int nHeight     // height of bitmap, in pixels
         );

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(
         IntPtr hdc,          // handle to DC
         IntPtr hgdiobj   // handle to object
         );

        [DllImport("gdi32.dll")]
        public static extern int DeleteDC(
         IntPtr hdc          // handle to DC
         );

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(
         IntPtr hwnd,               // Window to copy,Handle to the window that will be copied. 
         IntPtr hdcBlt,             // HDC to print into,Handle to the device context. 
         UInt32 nFlags              // Optional flags,Specifies the drawing options. It can be one of the following values. 
         );

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(
         IntPtr hwnd
         );


        public static Bitmap GetWindow(IntPtr hWnd)
        {
            IntPtr hscrdc = GetWindowDC(hWnd);
            Control control = Control.FromHandle(hWnd);
            IntPtr hbitmap = CreateCompatibleBitmap(hscrdc, control.Width, control.Height);
            IntPtr hmemdc = CreateCompatibleDC(hscrdc);
            SelectObject(hmemdc, hbitmap);
            PrintWindow(hWnd, hmemdc, 0);
            Bitmap bmp = Bitmap.FromHbitmap(hbitmap);
            DeleteDC(hscrdc);//ɾ���ù��Ķ���
            DeleteDC(hmemdc);//ɾ���ù��Ķ���
            return bmp;
        }
    }
}
