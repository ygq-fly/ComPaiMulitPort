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
        //��Ǳ���
        private bool mBoTimeout;
        public DoHandler Do;
        public object obj;

        public FunTimeout()
        {
            //  ��ʼ״̬Ϊ ֹͣ
            this.mTimeoutObject = new ManualResetEvent(true);
        }
        /// <summary>
        /// ָ����ʱʱ�� �첽ִ��ĳ������
        /// </summary>
        /// <returns>ִ�� �Ƿ�ʱ</returns>
        public bool DoWithTimeout(TimeSpan timeSpan)
        {
            if (this.Do == null)
            {
                return false;
            }
            this.mTimeoutObject.Reset();
            this.mBoTimeout = true; //���
            this.Do.BeginInvoke(obj, DoAsyncCallBack, null);
            // �ȴ� �ź�Set
            if (!this.mTimeoutObject.WaitOne(timeSpan, true))
            {
                this.mBoTimeout = true;
            }
            return this.mBoTimeout;
        }
        /// <summary>
        /// �첽ί�� �ص�����
        /// </summary>
        /// <param name="result"></param>
        private void DoAsyncCallBack(IAsyncResult result)
        {
            try
            {
                this.Do.EndInvoke(result);
                // ָʾ������ִ��δ��ʱ
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
