using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Reflection;


namespace jcPimSoftware
{
    
    internal class ImagesManage
    {
        private static int activeIndex = -1;
        private static List<Assembly> asms = null;

        private ImagesManage()
        {
           //
        }

        /// <summary>
        /// ����ͼƬ��Դ���ڵ�DLL���򼯣��ó��򼯲�Ӧ����ǿǩ����
        /// �����в�Ӧ������׺.dll
        /// </summary>
        /// <param name="dllName"></param>
        /// <returns></returns>
        public static Boolean LoadImageDLL(string dllName)
        {
            Boolean result = false;

            //��ʵ����һ��
            if (asms == null)
                asms = new List<Assembly>();

            //���ذ���ͼƬ��Դ�ĳ���
            Assembly asm = null;

            asm = Assembly.Load(dllName);

            //��ͼƬ��Դ���򼯼��سɹ���������ӵ��б�
            //�����µ�ǰ����򼯵�����
            if (asm != null)
            {
                asms.Add(asm);

                activeIndex = (asms.Count - 1);

                result = true;
            }

            return result;
        }

        /// <summary>
        /// ��ȡ���Դ������ͼƬ�������ṩ�ļ������ƺ��ļ�����
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Image GetImage(string folderName, string fileName)
        {
            Bitmap bmp  = null;
            Stream strm = null;

            
            //�������п��ַ������򻹻ؿ�
            if (String.IsNullOrEmpty(folderName.Trim()) ||
                String.IsNullOrEmpty(fileName.Trim()))                 
                return bmp;

            if ((activeIndex >= 0) && (activeIndex < asms.Count))
            {
                Assembly asm = asms[activeIndex];
                strm = asm.GetManifestResourceStream(asm.GetName().Name + ".images." + 
                                                     folderName.ToLower()+ "." +  
                                                     fileName.ToLower());
                if (strm == null)
                    bmp = null;
                else 
                    bmp = new System.Drawing.Bitmap(strm);
            }

            return bmp;
        }

        /// <summary>
        /// ���õ�ǰ�����Դ���򼯣���������ͼƬ��Դ
        /// </summary>
        /// <param name="index"></param>
        public static void SetActiveAssembly(int index)
        {
            activeIndex = index;
        }
    }

}
