using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveFormTool
{
    class EditCycle
    {
        UInt16 m_OldCycle;
        public UInt16 OldCycle
        {
            get { return m_OldCycle; }
        }

        UInt16 m_NewStartCycle;
        public UInt16 NewStartCycle
        {
            get { return m_NewStartCycle; }
        }

        UInt16 m_NewEndCycle;
        public UInt16 NewEndCycle
        {
            get { return m_NewEndCycle; }
        }

        UInt16 m_NewCycleStep;
        public UInt16 NewCycleStep
        {
            get { return m_NewCycleStep; }
        }

        public EditCycle(UInt16 OldCycle)
        {
            m_OldCycle = OldCycle;

            m_NewStartCycle = OldCycle;
            m_NewEndCycle = OldCycle;
            m_NewCycleStep = 0;
        }

        public void SetNewValue(UInt16 NewStartCycle, UInt16 NewEndCycle, UInt16 CycleStep)
        {
            if (NewStartCycle > NewEndCycle)
            {
                throw new Exception("Start Value > EndValue");
            }

            if (CycleStep > 0)
            {
                if ((NewEndCycle - NewStartCycle) % CycleStep != 0)
                {
                    throw new Exception("The step is invalid");
                }
            }
            else if(0 == CycleStep)
            {
                if (NewEndCycle != NewStartCycle)
                {
                    throw new Exception("The step is invalid");
                }
            }

            m_NewStartCycle = NewStartCycle;
            m_NewEndCycle = NewEndCycle;
            m_NewCycleStep = CycleStep;
        }

        public string GetNewValue()
        {
            return string.Format("[{0},{1}]({2})", m_NewStartCycle, m_NewEndCycle, m_NewCycleStep);
        }

        public bool IsChanged()
        { 
            bool ret = true;
            if (m_NewStartCycle == m_NewEndCycle && 0 == m_NewCycleStep)
            {
                if (m_NewStartCycle == m_OldCycle)
                {
                    ret = false;
                }
            }

            return ret;
        }
    }

    class WaveDataEx
    {
        private WaveData m_WaveData;
        public WaveData Wave
        {
            get
            {
                return m_WaveData;
            }
        }

        private List<IAmplitude> m_Amplitudes = new List<IAmplitude> ();
        public List<IAmplitude> Amplitudes
        {
            get
            {
                return m_Amplitudes;
            }
        }

        public WaveDataEx(WaveData WaveData)
        {
            m_WaveData = WaveData;
        }

        public void AddAmplitude(IAmplitude Amlitude)
        {
            m_Amplitudes.Add(Amlitude);
        }
    }
}
