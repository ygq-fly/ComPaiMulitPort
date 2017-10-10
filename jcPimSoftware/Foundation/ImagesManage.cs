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
        /// 加载图片资源所在的DLL程序集，该程序集不应该是强签名的
        /// 名称中不应包含后缀.dll
        /// </summary>
        /// <param name="dllName"></param>
        /// <returns></returns>
        public static Boolean LoadImageDLL(string dllName)
        {
            Boolean result = false;

            //仅实例化一次
            if (asms == null)
                asms = new List<Assembly>();

            //加载包含图片资源的程序集
            Assembly asm = null;

            asm = Assembly.Load(dllName);

            //若图片资源程序集加载成功，则将其添加到列表
            //并更新当前活动程序集的索引
            if (asm != null)
            {
                asms.Add(asm);

                activeIndex = (asms.Count - 1);

                result = true;
            }

            return result;
        }

        /// <summary>
        /// 获取活动资源程序集中图片，必须提供文件夹名称和文件名称
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Image GetImage(string folderName, string fileName)
        {
            Bitmap bmp  = null;
            Stream strm = null;

            
            //名称中有空字符串，则还回空
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
        /// 设置当前活动的资源程序集，它包含了图片资源
        /// </summary>
        /// <param name="index"></param>
        public static void SetActiveAssembly(int index)
        {
            activeIndex = index;
        }
    }

}
