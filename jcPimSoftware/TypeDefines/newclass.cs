using System;
using System.Collections.Generic;
using System.Text;

namespace jcPimSoftware
{
    public class newclass
    {
        #region
        /// <summary>
        /// 出厂日期
        /// </summary>
        private string dates;
        /// <summary>
        /// 授权日期
        /// </summary>
        private string datee;
        /// <summary>
        /// 频谱仪型号
        /// </summary>
        private string type;
        /// <summary>
        /// 试用总天数
        /// </summary>
        private string  days;

        /// <summary>
        /// 实际用的天数
        /// </summary>
        private string day;
        /// <summary>
        /// 上次记录的时间
        /// </summary>
        private string daynow;
        /// <summary>
        /// 是否需要授权
        /// </summary>
        private string needcheck;

        #endregion

        #region
        /// <summary>
        /// 出厂日期
        /// </summary>
        public string Dates
        {
            get { return dates; }
            set { dates = value; }
        }
        /// <summary>
        /// 授权日期
        /// </summary>
        public string Datee
        {
            get { return datee; }
            set { datee = value; }
        }
        /// <summary>
        /// 频谱仪型号
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// 试用总天数
        /// </summary>
        public string Days
        {
            get { return days; }
            set { days = value; }
        }

        /// <summary>
        /// 实际用的天数
        /// </summary>
        public string Day
        {
            get { return day; }
            set { day = value; }
        }
        /// <summary>
        /// 上次记录的时间
        /// </summary>
        public string Daynow
        {
            get { return daynow; }
            set { daynow = value; }
        }
        /// <summary>
        /// 是否需要授权
        /// </summary>
        public string Needcheck
        {
            get { return needcheck; }
            set { needcheck = value; }
        }
        #endregion
    }
}
