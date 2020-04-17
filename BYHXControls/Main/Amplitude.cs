using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveFormTool
{
    enum AmplitudeDispatchType
    { 
        HeadParameter = 0,
        Voltage
    }

    class AmplitudeDispatch
    {
        private AmplitudeDispatchType m_Type;
        public AmplitudeDispatchType Type
        {
            get {return m_Type;}
        }

        private List<int> m_Voltages;
        public List<int> Voltages
        {
            get { return m_Voltages; }
        }

        //
        public AmplitudeDispatch(int Start, int End, int Step)
        {
            m_Type = AmplitudeDispatchType.HeadParameter;
            m_Voltages = new List<int>();

            //TODO 各种错误检查
            if (Step != 0)
            {
                do
                {
                    m_Voltages.Add(Start);
                    Start += Step;
                }
                while (Start <= End);
            }
            else
            {
                m_Voltages.Add(Start);
            }
        }

        public AmplitudeDispatch()
        {
            m_Type = AmplitudeDispatchType.Voltage;
        }
    }

    interface IAmplitude
    {
        byte[] GetBytes();
    }

    class AmplitudeHeadParameter : IAmplitude
    {
        int m_Voltage;

        public AmplitudeHeadParameter(int Voltage)
        {
            m_Voltage = Voltage;
        }

        public byte[] GetBytes()
        {
            byte[] Data;

            Data = new byte[2];
            Data[0] = 0;
            Data[1] = (byte)m_Voltage;

            return Data;
        }
    }

    class AmplitudeVoltageChange : IAmplitude
    {
        List<WaveDataSnip> m_OldData;
        List<WaveDataSnip> m_NewData;

        public AmplitudeVoltageChange(List<WaveDataSnip> OldData, List<WaveDataSnip> NewData)
        {
            m_OldData = OldData;
            m_NewData = NewData;
        }

        public byte[] GetBytes()
        {
            if (m_OldData.Count != m_NewData.Count)
            {
                throw new Exception("新旧波形段数不同，暂不支持");
            }

            Dictionary<int, WaveDataSnip> ModifedWaveSnip = new Dictionary<int, WaveDataSnip>();
            for (int i = 0; i < m_OldData.Count; i++)
            {
                WaveDataSnip OldSnip = m_OldData[i];
                WaveDataSnip NewSnip = m_NewData[i];

                if (OldSnip.m_Snip1 != NewSnip.m_Snip1 || OldSnip.m_Snip2 != NewSnip.m_Snip2)
                { 
                    //如果发生了修改，记录节点id和变化值
                    WaveDataSnip Snip = new WaveDataSnip();
                    Snip.m_Snip1 = (byte)(NewSnip.m_Snip1 - OldSnip.m_Snip1);
                    Snip.m_Snip2 = (byte)(NewSnip.m_Snip2 - OldSnip.m_Snip2);

                    ModifedWaveSnip.Add(i + 1, Snip);
                }
            }

            int BufferSize = 1 + 2 + 4 * ModifedWaveSnip.Count;
            byte[] Data = new byte[BufferSize];

            int BufferIndex = 0;
            Data[BufferIndex++] = 1;
            Array.Copy(BitConverter.GetBytes(Convert.ToUInt16(ModifedWaveSnip.Count)), 0, Data, BufferIndex, 2);
            BufferIndex += 2;

            foreach (KeyValuePair<int, WaveDataSnip> kv in ModifedWaveSnip)
            {
                Array.Copy(BitConverter.GetBytes(Convert.ToUInt16(kv.Key)), 0, Data, BufferIndex, 2);
                BufferIndex += 2;

                Data[BufferIndex++] = kv.Value.m_Snip1;
                Data[BufferIndex++] = kv.Value.m_Snip2;
            }

            return Data;
        }
    }
}
