using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace jcPimSoftware
{
    public delegate void DoHandler(object o);
    class FunTimeout
    {
        private ManualResetEvent mTimeoutObject;
        //标记变量
        private bool mBoTimeout;
        public DoHandler Do;
        public object obj;

        public FunTimeout()
        {
            //  初始状态为 停止
            this.mTimeoutObject = new ManualResetEvent(true);
        }
        /// <summary>
        /// 指定超时时间 异步执行某个方法
        /// </summary>
        /// <returns>执行 是否超时</returns>
        public bool DoWithTimeout(TimeSpan timeSpan)
        {
            if (this.Do == null)
            {
                return false;
            }
            this.mTimeoutObject.Reset();
            this.mBoTimeout = true; //标记
            this.Do.BeginInvoke(obj, DoAsyncCallBack, null);
            // 等待 信号Set
            if (!this.mTimeoutObject.WaitOne(timeSpan, true))
            {
                this.mBoTimeout = true;
            }
            return this.mBoTimeout;
        }
        /// <summary>
        /// 异步委托 回调函数
        /// </summary>
        /// <param name="result"></param>
        private void DoAsyncCallBack(IAsyncResult result)
        {
            try
            {
                this.Do.EndInvoke(result);
                // 指示方法的执行未超时
                this.mBoTimeout = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.mBoTimeout = true;
            }
            finally
            {
                this.mTimeoutObject.Set();
            }
        }
    }
}
