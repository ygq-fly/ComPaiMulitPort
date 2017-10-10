// ===============================================================================
// �ļ�����SpectrumDef
// �����ˣ����
// ��  �ڣ�2011-4-29 
//
// ��  ����Ƶ���ǹ�������
//         
//
// ��  ���� 1.0.0.0
//
// ���¼�¼ 
// ===============================================================================
// ʱ  �䣺 2011-4-29   	   �������ļ�
//
// ===============================================================================



using System;
using System.Collections.Generic;
using System.Text;

namespace jcPimSoftware
{
    class SpectrumDef
    {
        /// <summary>
        /// Ƶ��������ö��
        /// </summary>
        public enum ESpectrumType
        {
            SpeCat2 = 0,
            Bird = 1,
            Deli=2
        }

        /// <summary>
        /// ��ݲ˵�ö��
        /// </summary>
        public enum EShortcutMenu
        {
            REF_LEVEL = 0,
            SPAN = 1,
            RBW = 2,
            VBW = 3,
            SCALE = 4,
            UNIT = 5,
            REF_POSITION = 6,
            HOLD_MENU = 7,
            MARK_SELECT = 8,
            PEAK_HOLD = 9,
            TRACE_SELECT = 10
        }

        /// <summary>
        /// Ƶ�׷������������ṹ
        /// </summary>
        public struct OffsetObj
        {
            /// <summary>
            /// �ֶβ����Ľ���Ƶ��
            /// </summary>
            public double endFreq;
            /// <summary>
            /// ������һ����ϵ��
            /// </summary>
            public double paramA;
            /// <summary>
            /// �����ĳ�����
            /// </summary>
            public double paramB;
        }
    }
}
