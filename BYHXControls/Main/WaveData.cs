using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BYHXPrinterManager;

namespace WaveFormTool
{
    public class CommonInfo
    {
        public static string oldWaveFileName = "Wave.csv";
        public static string oldNMFileName = "NM.csv";

        public static string newWaveFileName = "Wave_V1.csv";
        public static string newNMFileName = "NM_V1.csv";
        public static string newWaveAnalogFileName = "WaveAnalog_V1.csv";
    }

	class WaveDataSnip
	{
		//以下都是暂时的命名
		public byte m_Snip1;
		public byte m_Snip2;
		public ushort m_Snip3;
	}

	class WaveNMData
	{
		public string[] m_NM = new string[8];
		public int m_Cycle;
	}

	class WaveDataSnipPercentage
	{
		public double m_StandardVoltage;
		public double m_Snip1_Per;
		public double m_Snip2_Per;
	}

	//V7 start
	class NewWaveData
	{
		public double m_StandardVoltage; //平台电压
		public ushort m_VoltageStart;
		public ushort m_VoltageEnd;
        public float  m_AnalogStart;
        public float  m_AnalogEnd;
		public double m_VoltageStart_Percentage;
		public double m_VoltageEnd_Percentage;
		public double m_dVoltage_Cycle; //时间(us)
		public ushort m_Voltage_Cycle;
	}

	class NewWaveNMData
	{
		public string[] m_NM = new string[8];
		public int m_Cycle;
		public double m_dCycle;
	}

    struct VDData
    {
        public float D;
        public float V;
    }

    struct VVData
    {
        public float NewV;
        public float OldV;
    }

	//V7 End
	class WaveData
	{
		byte[] m_OriginalWaveData;//byte型数组，存放波形数据
		int m_SegmentCount = 0;//整形,段数
		ushort m_usTotalCycle = 0;//总周期数
        public float MaxCycle = 0;
        public List<VDData> VDDataList = new List<VDData>();
        public List<VVData> VVDataList = new List<VVData>();

		Dictionary<string, byte> m_WaveSummary = new System.Collections.Generic.Dictionary<string, byte>();//定义名为m_WaveSummary的字典，key是string，value是byte
		public Dictionary<string, byte> WaveSummary   //定义名为WaveSummary、类型为字典的“方法”；该方法的功能是返回m_WaveSummary字典中的内容。
		{
			get
			{
				return m_WaveSummary;
			}
		}

		List<WaveDataSnip> m_WaveDatas = new System.Collections.Generic.List<WaveDataSnip>();//定义名为m_WaveDatas的List
		public List<WaveDataSnip> WaveDatas  //定义名为WaveDatas的方法
		{
			get
			{
				return m_WaveDatas;
			}
			set
			{
				m_WaveDatas = value;
			}
		}

		List<WaveNMData> m_WaveNMDatas = new System.Collections.Generic.List<WaveNMData>();//定义名为m_WaveNMDatas的List
		public List<WaveNMData> WaveNMDatas  //定义名为WaveNMDatas的方法
		{
			get
			{
				return m_WaveNMDatas;
			}
			set
			{
				m_WaveNMDatas = value;
			}
		}

		List<WaveDataSnipPercentage> m_WaveDatasPercentage = new System.Collections.Generic.List<WaveDataSnipPercentage>();//定义名为m_WaveDatasPercentage的List
		public List<WaveDataSnipPercentage> WaveDatasPercentage  //定义名为WaveDatasPercentage的方法
		{
			get
			{
				return m_WaveDatasPercentage;
			}
			set
			{
				m_WaveDatasPercentage = value;
			}
		}

		//V7 Start
		List<NewWaveData> m_NewWaveDatas = new System.Collections.Generic.List<NewWaveData>();//定义名为m_NewWaveDatas的List
		public List<NewWaveData> NewWaveDatas  //定义名为NewWaveNMDatas的方法
		{
			get
			{
				return m_NewWaveDatas;
			}
			set
			{
				m_NewWaveDatas = value;
			}
		}

		public Dictionary<int, double> m_Wave2Correction = new System.Collections.Generic.Dictionary<int, double>();//每段波形对应的周期修正值

		List<NewWaveNMData> m_NewWaveNMDatas = new System.Collections.Generic.List<NewWaveNMData>();//定义名为m_NewWaveNMDatas的List
		public List<NewWaveNMData> NewWaveNMDatas  //定义名为NewWaveNMDatas的方法
		{
			get
			{
				return m_NewWaveNMDatas;
			}
			set
			{
				m_NewWaveNMDatas = value;
			}
		}
		//V7 End
		List<UInt32> m_NM = new System.Collections.Generic.List<UInt32>();
		public List<UInt32> NM
		{
			get
			{
				return m_NM;
			}
			set
			{
				m_NM = value;
			}
		}

		public WaveData()
		{
		}

		public WaveData(int SegmentCount, int NMCount)
		{ }

		public WaveData(int SegmentCount, int NMCount, double CycleValue)
		{
			//int BufferIndex = 0;
			m_WaveSummary.Add("Summary1", Convert.ToByte(SegmentCount));  //向m_WaveSummary字典中添加“Summary1”key,value为SegmentCount
			m_WaveSummary.Add("Summary2", Convert.ToByte(4));  //将4传给"Summary2"
			//m_WaveSummary.Add("Summary3", m_OriginalWaveData[BufferIndex++]);
			//m_WaveSummary.Add("Summary4", m_OriginalWaveData[BufferIndex++]);

			for (int i = 0; i < SegmentCount; i++)
			{
				WaveDataSnip Snip = new WaveDataSnip();
				m_WaveDatas.Add(Snip);
			}

			for (int i = 0; i < NMCount; i++)
			{
				m_NM.Add(0);
			}

			for (int i = 0; i < SegmentCount; i++)
			{
				WaveDataSnipPercentage SnipPercentage = new WaveDataSnipPercentage();
				m_WaveDatasPercentage.Add(SnipPercentage);
			}

			//V7 Start
			for (int i = 0; i < SegmentCount; i++)
			{
				NewWaveData newWaveData = new NewWaveData();
				m_NewWaveDatas.Add(newWaveData);
			}
			for (int i = 0; i < NMCount; i++)
			{
				NewWaveNMData newNMData = new NewWaveNMData();
				m_NewWaveNMDatas.Add(newNMData);
			}
			//V7 End

			//获取NM周期修正
			UpdateNMCorrect(CycleValue, false);
		}

		public bool Parse(byte[] WaveData)
		{
			m_WaveSummary.Clear();
			m_WaveDatas.Clear();
			m_NM.Clear();
			m_OriginalWaveData = WaveData;

			m_SegmentCount = (int)m_OriginalWaveData[0];

			int BufferIndex = 0;
			m_WaveSummary.Add("Summary1", m_OriginalWaveData[BufferIndex++]);
			m_WaveSummary.Add("Summary2", m_OriginalWaveData[BufferIndex++]);
			m_WaveSummary.Add("Summary3", m_OriginalWaveData[BufferIndex++]);
			m_WaveSummary.Add("Summary4", m_OriginalWaveData[BufferIndex++]);

			for (int i = 0; i < m_SegmentCount; i++)
			{
				WaveDataSnip Snip = new WaveDataSnip();
				Snip.m_Snip1 = m_OriginalWaveData[BufferIndex++];
				Snip.m_Snip2 = m_OriginalWaveData[BufferIndex++];

				byte HighByte = m_OriginalWaveData[BufferIndex++];
				byte LowByte = m_OriginalWaveData[BufferIndex++];
				byte[] Data = new byte[2];
				Data[0] = HighByte;
				Data[1] = LowByte;
				Snip.m_Snip3 = BitConverter.ToUInt16(Data, 0);

				m_WaveDatas.Add(Snip);
			}

			int ByteLeft = m_OriginalWaveData.Length - 4 - m_SegmentCount * 4;
			if (ByteLeft % 4 != 0)
			{
				return false;
			}
			int NMDataCount = ByteLeft / 4;
			for (int i = 0; i < NMDataCount; i++)
			{
				UInt32 Data = BitConverter.ToUInt32(m_OriginalWaveData, BufferIndex);
				m_NM.Add(Data);
				BufferIndex += 4;
			}

			return true;
		}

		public byte[] GetBytes()
		{
			int TotalLength = 1 * m_WaveSummary.Count + 4 * m_WaveDatas.Count + 4 * m_NM.Count;
			byte[] Bytes = new byte[TotalLength];

			int BufferIndex = 0;

			//Summary
			foreach (KeyValuePair<string, byte> kv in m_WaveSummary)
			{
				Bytes[BufferIndex++] = kv.Value;
			}

			//Wave
			for (int i = 0; i < m_WaveDatas.Count; i++)
			{
				WaveDataSnip Snip = m_WaveDatas[i];
				Bytes[BufferIndex++] = Snip.m_Snip1;
				Bytes[BufferIndex++] = Snip.m_Snip2;

				byte[] Data = BitConverter.GetBytes(Snip.m_Snip3);
				Array.Copy(Data, 0, Bytes, BufferIndex, 2);

				BufferIndex += 2;
			}

            //CoreInterface.WriteLogNormal("Before Write NM");

			//NM
			for (int i = 0; i < m_NM.Count; i++)
			{
				byte[] Data = BitConverter.GetBytes(m_NM[i]);
				Array.Copy(Data, 0, Bytes, BufferIndex, 4);

				BufferIndex += 4;
			}

			return Bytes;
		}

		public void ParseWaveDataFromFile(string strPath = null)
		{
			m_SegmentCount = 0;

			if (strPath == null) strPath = "Wave.csv";

			List<List<string>> CsvData = new List<List<string>>();
			using (StreamReader sr = new StreamReader(strPath))
			{
				while (!sr.EndOfStream)
				{
					string DebugData = sr.ReadLine();
					List<string> DebugRow = new List<string>(DebugData.Split(','));

					if (0 == m_SegmentCount)
					{
						m_SegmentCount = DebugRow.Count;
					}
					else
					{
						if (m_SegmentCount != DebugRow.Count)
						{
							throw new Exception("The column number is not the same in csv.");
						}
					}
					CsvData.Add(DebugRow);
				}
			}

			m_WaveDatas.Clear();
			m_WaveDatasPercentage.Clear();
			for (int i = 1; i < m_SegmentCount; i++)//从1开始是因为第一列是文字 
			{
				WaveDataSnip Snip = new WaveDataSnip();

				Snip.m_Snip1 = Convert.ToByte(CsvData[0 + 1][i], 10);//在波形信息中添加SN后，需要的信息都下移一行，这是做出修改的地方以支持添加SN后的格式变化。这里原来转换的是16进制。10-16
				Snip.m_Snip2 = Convert.ToByte(CsvData[1 + 1][i], 10);
				Snip.m_Snip3 = Convert.ToByte(CsvData[2 + 1][i], 10);

				m_WaveDatas.Add(Snip);

				//设定标准电压及百分比
				WaveDataSnipPercentage SnipPercentage = new WaveDataSnipPercentage();
				SnipPercentage.m_StandardVoltage = Convert.ToDouble(CsvData[1][1]);
				if ((int)SnipPercentage.m_StandardVoltage != 0)
				{
					SnipPercentage.m_Snip1_Per = Snip.m_Snip1 / SnipPercentage.m_StandardVoltage;
					SnipPercentage.m_Snip2_Per = Snip.m_Snip2 / SnipPercentage.m_StandardVoltage;
				}
				else
				{
					SnipPercentage.m_Snip1_Per = 0;
					SnipPercentage.m_Snip2_Per = 0;
				}
				m_WaveDatasPercentage.Add(SnipPercentage);
			}
		}

		public void ParseNMDataFromFile(string strPath = null)
		{
			int NMDataCount = 0;

			if (strPath == null) strPath = "NM.csv";

			List<List<string>> CsvData = new List<List<string>>();
			using (StreamReader sr = new StreamReader(strPath))
			{
				while (!sr.EndOfStream)
				{
					string DebugData = sr.ReadLine();
					List<string> DebugRow = new List<string>(DebugData.Split(','));

					if (0 == NMDataCount)
					{
						NMDataCount = DebugRow.Count;
					}
					else
					{
						if (NMDataCount != DebugRow.Count)
						{
							throw new Exception("The column number is not the same in csv.");
						}
					}
					CsvData.Add(DebugRow);
				}
			}

			m_WaveNMDatas.Clear();
			for (int i = 1; i < NMDataCount; i++)
			{
				byte[] OldNMData = BitConverter.GetBytes(m_NM[i]);//先取原来的值，因为第一个字节是照搬

				string SecondByteHexString = CsvData[0][i] + CsvData[1][i] + CsvData[2][i] + CsvData[3][i];
				byte SecondByte = Convert.ToByte(SecondByteHexString, 2);

				byte[] TheLeftTwoBytes = BitConverter.GetBytes(Convert.ToUInt16(CsvData[4][i]));

				OldNMData[1] = SecondByte;
				Array.Copy(TheLeftTwoBytes, 0, OldNMData, 2, 2);

				m_NM[i] = BitConverter.ToUInt32(OldNMData, 0);

				//设置NM数据
				WaveNMData waveData = new WaveNMData();
				waveData.m_NM[0] = Convert.ToString(CsvData[0][i]);
				waveData.m_NM[1] = Convert.ToString(CsvData[1][i]);
				waveData.m_NM[2] = Convert.ToString(CsvData[2][i]);
				waveData.m_NM[3] = Convert.ToString(CsvData[3][i]);
				waveData.m_Cycle = Convert.ToInt32(CsvData[4][i]);
				m_WaveNMDatas.Add(waveData);
			}
		}

		public void SaveWaveDataToFile(List<List<string>> CsvData)
		{
			SaveDataToFile(
				string.Format("Wave_{0:D4}-{1:D2}-{2:D2}-{3:D2}-{4:D2}-{5:D2}.csv", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
				CsvData);
		}

		public void SaveNMDataToFile(List<List<string>> CsvData)
		{
			SaveDataToFile(
				string.Format("NM_{0:D4}-{1:D2}-{2:D2}-{3:D2}-{4:D2}-{5:D2}.csv", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
				CsvData);
		}

		private void SaveDataToFile(string FileName, List<List<string>> CsvData)
		{
			using (StreamWriter sw = new StreamWriter(FileName))
			{
				foreach (List<string> DebugInfo in CsvData)
				{
					int Count = DebugInfo.Count;
					int Index = 0;
					foreach (string DebugRowData in DebugInfo)
					{
						sw.Write(DebugRowData);
						Index++;

						if (Index < Count)
						{
							sw.Write(",");
						}
					}
					sw.WriteLine();
				}
			}
		}
		public void SaveWaveDataToCsvFile(List<List<string>> CsvData, string strPath)
		{
			SaveDataToFile(strPath, CsvData);
		}

		public void SaveNMDataToCsvFile(List<List<string>> CsvData, string strPath)
		{
			SaveDataToFile(strPath, CsvData);
		}

		//V7 start
		/// <summary>
		/// 从本地导入波形数据
		/// </summary>
		/// <param name="strPath"></param>
		public void ParseWaveDataFromLocalFile(string strHBTypeName, string strPath = null)
		{
			m_SegmentCount = 0;

			int LocalSegmentCount = 0;

			if (strPath == null) strPath = "Wave.csv";

			List<List<string>> CsvData = new List<List<string>>();
			using (StreamReader sr = new StreamReader(strPath))
			{
				while (!sr.EndOfStream)
				{
					string DebugData = sr.ReadLine();
					List<string> DebugRow = new List<string>(DebugData.Split(','));

					if (0 == LocalSegmentCount)
					{
						LocalSegmentCount = DebugRow.Count;
					}
					//else
					//{
					//    if (m_SegmentCount != DebugRow.Count)
					//    {
					//        throw new Exception("The column number is not the same in csv.");
					//    }
					//}
					CsvData.Add(DebugRow);
				}
			}

			m_WaveDatas.Clear();
			m_NewWaveDatas.Clear();
			int nStandardVoltage = Convert.ToInt32(CsvData[1][1]);				//平台电压（数字量）
			float fStandardVoltage = 0.0f;										//平台电压（模拟电压）
			fStandardVoltage = G4_DigitalValue2Voltage(nStandardVoltage,false);

			for (int i = 1; i < LocalSegmentCount; i++)//从1开始是因为第一列是文字 
			{
				NewWaveData newdata = new NewWaveData();

				newdata.m_VoltageStart = (ushort)Convert.ToInt16(CsvData[0 + 1][i], 10);
				newdata.m_VoltageEnd = (ushort)Convert.ToInt16(CsvData[1 + 1][i], 10);
				newdata.m_Voltage_Cycle = (ushort)Convert.ToInt16(CsvData[2 + 1][i], 10);

				if (newdata.m_Voltage_Cycle != 0) m_SegmentCount++;

				//设定标准电压及百分比
				newdata.m_StandardVoltage = fStandardVoltage;
				if ((int)newdata.m_StandardVoltage != 0)
				{
					{
						newdata.m_VoltageStart_Percentage = G4_DigitalValue2Voltage(newdata.m_VoltageStart,false) / fStandardVoltage * 100;
						newdata.m_VoltageEnd_Percentage = G4_DigitalValue2Voltage(newdata.m_VoltageEnd,false) / fStandardVoltage * 100;
					}
				}
				else
				{
					newdata.m_VoltageStart_Percentage = 0;
					newdata.m_VoltageEnd_Percentage = 0;
				}
				m_NewWaveDatas.Add(newdata);
			}
		}

        public void ParseWaveDataFromLocalFile_Analog(string strHBTypeName, string strPath = null)
        {
            m_SegmentCount = 0;

            int LocalSegmentCount = 0;

            if (strPath == null) strPath = CommonInfo.newWaveAnalogFileName;

            List<List<string>> CsvData = new List<List<string>>();
            using (StreamReader sr = new StreamReader(strPath))
            {
                while (!sr.EndOfStream)
                {
                    string DebugData = sr.ReadLine();
                    List<string> DebugRow = new List<string>(DebugData.Split(','));

                    if (0 == LocalSegmentCount)
                    {
                        LocalSegmentCount = DebugRow.Count;
                    }
                    //else
                    //{
                    //    if (m_SegmentCount != DebugRow.Count)
                    //    {
                    //        throw new Exception("The column number is not the same in csv.");
                    //    }
                    //}
                    CsvData.Add(DebugRow);
                }
            }

            m_WaveDatas.Clear();
            m_NewWaveDatas.Clear();
            //int nStandardVoltage = Convert.ToInt32(CsvData[1][1]);				//平台电压（数字量）
            float fStandardVoltage = 0.0f;										//平台电压（模拟电压）
            fStandardVoltage = Convert.ToSingle(CsvData[1][1]);

            for (int i = 1; i < LocalSegmentCount; i++)//从1开始是因为第一列是文字 
            {
                NewWaveData newdata = new NewWaveData();

                newdata.m_AnalogStart = Convert.ToSingle(CsvData[0 + 1][i]);
                newdata.m_AnalogEnd = Convert.ToSingle(CsvData[1 + 1][i]);
                newdata.m_Voltage_Cycle = (ushort)Convert.ToInt16(CsvData[2 + 1][i], 10);

                newdata.m_VoltageStart = G4_Voltage2DigitalValue(newdata.m_AnalogStart);
                newdata.m_VoltageEnd = G4_Voltage2DigitalValue(newdata.m_AnalogEnd);

                if (newdata.m_Voltage_Cycle != 0) m_SegmentCount++;

                //设定标准电压及百分比
                newdata.m_StandardVoltage = fStandardVoltage;
                if ((int)newdata.m_StandardVoltage != 0)
                {
                    {
                        newdata.m_VoltageStart_Percentage = newdata.m_AnalogStart / fStandardVoltage * 100;
                        newdata.m_VoltageEnd_Percentage = newdata.m_AnalogEnd / fStandardVoltage * 100;
                    }
                }
                else
                {
                    newdata.m_VoltageStart_Percentage = 0;
                    newdata.m_VoltageEnd_Percentage = 0;
                }
                m_NewWaveDatas.Add(newdata);
            }
        }

		public void ParseNMDataFromLocalFile(string strPath = null)
		{
			int NMDataCount = 0;

			if (strPath == null) strPath = "NM.csv";

			List<List<string>> CsvData = new List<List<string>>();
			using (StreamReader sr = new StreamReader(strPath))
			{
				while (!sr.EndOfStream)
				{
					string DebugData = sr.ReadLine();
					List<string> DebugRow = new List<string>(DebugData.Split(','));

					if (0 == NMDataCount)
					{
						NMDataCount = DebugRow.Count;
					}
					//else
					//{
					//    if (NMDataCount != DebugRow.Count)
					//    {
					//        throw new Exception("The column number is not the same in csv.");
					//    }
					//}
					CsvData.Add(DebugRow);
				}
			}

			m_NewWaveNMDatas.Clear();
			for (int i = 1; i < NMDataCount; i++)
			{
				//设置NM数据
				NewWaveNMData newData = new NewWaveNMData();
				newData.m_NM[0] = Convert.ToString(CsvData[0][i]);
				newData.m_NM[1] = Convert.ToString(CsvData[1][i]);
				newData.m_NM[2] = Convert.ToString(CsvData[2][i]);
				newData.m_NM[3] = Convert.ToString(CsvData[3][i]);
				newData.m_Cycle = Convert.ToInt32(CsvData[4][i]);
				m_NewWaveNMDatas.Add(newData);
			}
		}

		public bool NewParse(string strHBTypeName, byte[] WaveData)
		{
			m_WaveSummary.Clear();
			m_NewWaveDatas.Clear();
			m_NM.Clear();
			m_OriginalWaveData = WaveData;

			m_SegmentCount = (int)m_OriginalWaveData[0];

			int BufferIndex = 0;
			m_WaveSummary.Add("Summary1", m_OriginalWaveData[BufferIndex++]);
			BufferIndex++;
			m_WaveSummary.Add("Summary2", m_OriginalWaveData[BufferIndex++]);
			BufferIndex++;
			//总周期取得
			byte HByte = m_OriginalWaveData[BufferIndex++];
			byte LByte = m_OriginalWaveData[BufferIndex++];
			byte[] DataCycle = new byte[2];
			DataCycle[0] = HByte;
			DataCycle[1] = LByte;
			m_usTotalCycle = BitConverter.ToUInt16(DataCycle, 0);

			m_WaveSummary.Add("Summary3", HByte);
			m_WaveSummary.Add("Summary4", Convert.ToByte(LByte & 0x7f));


			// 各段长度解析
			//第1 byte：0~3位表示波1所包含的段数
			//			4~7位表示波2所包含的段数
			//。。。
			//第4 byte：0~3位表示波7所包含的段数
			//			4~7位表示波8所包含的段数
			//第5byte，第6 byte 预留
			const int m_nWaveTotal = 8;

			int[] m_nWavePhase = new int[m_nWaveTotal] { 0, 0, 0, 0, 0, 0, 0, 0 };
			int[] m_nWaveIndex = new int[m_nWaveTotal] { 0, 0, 0, 0, 0, 0, 0, 0 };
			{
				//取得各通道数据
				m_nWavePhase[0] = Convert.ToInt32((m_OriginalWaveData[BufferIndex] & 0xf0) >> 4);
				m_nWavePhase[1] = Convert.ToInt32(m_OriginalWaveData[BufferIndex] & 0x0f);
				BufferIndex++;
				m_nWavePhase[2] = Convert.ToInt32((m_OriginalWaveData[BufferIndex] & 0xf0) >> 4);
				m_nWavePhase[3] = Convert.ToInt32(m_OriginalWaveData[BufferIndex] & 0x0f);
				BufferIndex++;
				m_nWavePhase[4] = Convert.ToInt32((m_OriginalWaveData[BufferIndex] & 0xf0) >> 4);
				m_nWavePhase[5] = Convert.ToInt32(m_OriginalWaveData[BufferIndex] & 0x0f);
				BufferIndex++;
				m_nWavePhase[6] = Convert.ToInt32((m_OriginalWaveData[BufferIndex] & 0xf0) >> 4);
				m_nWavePhase[7] = Convert.ToInt32(m_OriginalWaveData[BufferIndex] & 0x0f);
				BufferIndex += 3;
			}

			//数字量设定到元Wave结构中
			ushort uStandardVoltage = 0;	//平台电压（数字量）
			float fStandardVoltage = 0.0f;	//平台电压（模拟电压）
			ushort uStartVoltage = 0;		//开始电压
			ushort uEndVoltage = 0;			//终了电压
			ushort uCycle = 0;				//电压周期
			int m_PhaseCount = 0;			//NewWaveDatas的计数器
			int nWaveIndex = 0;				//波段标示
			int nTotalIs0Flag = 0;			//波段是否全为0标志
			int nTotalSum = 0;				//各波段之和
			bool bLoopOverFlg = false;		//循环结束标志
			for (int i = 0; i < m_SegmentCount; i++)
			{
				byte HighByte, LowByte;
				byte[] Data = new byte[2];

				//元Wave结构转换成新Wave结构（WaveDataSnip -> NewWaveData）
				NewWaveData newWaveData = new NewWaveData();

				//开始电压
				HighByte = m_OriginalWaveData[BufferIndex++];
				LowByte = m_OriginalWaveData[BufferIndex++];
				Data[0] = HighByte;
				Data[1] = LowByte;
				uStartVoltage = BitConverter.ToUInt16(Data, 0);
				if (0 == i)
				{
					//平台电压设定
					uStandardVoltage = uStartVoltage;
					fStandardVoltage = G4_DigitalValue2Voltage(uStandardVoltage);

					newWaveData.m_StandardVoltage = fStandardVoltage;
					for (int n = 0; n < m_nWaveTotal; n++) { nTotalIs0Flag |= m_nWavePhase[n]; nTotalSum += m_nWavePhase[n]; }
				}
				//终了电压
				HighByte = m_OriginalWaveData[BufferIndex++];
				LowByte = m_OriginalWaveData[BufferIndex++];
				Data[0] = HighByte;
				Data[1] = LowByte;
				uEndVoltage = BitConverter.ToUInt16(Data, 0);

				//持续周期
				HighByte = m_OriginalWaveData[BufferIndex++];
				LowByte = m_OriginalWaveData[BufferIndex++];
				Data[0] = HighByte;
				Data[1] = LowByte;
				uCycle = (ushort)(BitConverter.ToUInt16(Data, 0) & 0x1FFF);

				if (nTotalIs0Flag == 0)	//原格式波形导入
				{
					//波形划分（每两个相邻的平台电压作为一个波）
					if (uStartVoltage == uEndVoltage && uStartVoltage == uStandardVoltage && i != 0 && m_PhaseCount % m_nWaveTotal != 0)
					{
						//填充至8段，开始终了电压都为平台电压，周期为0
						int nRemainder = m_PhaseCount % m_nWaveTotal;
						for (int nIndex = 0; nIndex < m_nWaveTotal - nRemainder; nIndex++)
						{
							NullDataForWave(ref  m_NewWaveDatas, uStandardVoltage);
							m_PhaseCount++;
							if (m_PhaseCount == 64)
							{
								bLoopOverFlg = true;
								break;
							}
						}
					}
					//64段已满，不能再填充数据
					if (bLoopOverFlg == false)
					{
						newWaveData.m_VoltageStart = uStartVoltage;
						newWaveData.m_VoltageEnd = uEndVoltage;
						newWaveData.m_Voltage_Cycle = uCycle;

						//开始终了电压百分比设定
						if (uStandardVoltage != 0)
						{
							{
								newWaveData.m_VoltageStart_Percentage = G4_DigitalValue2Voltage(newWaveData.m_VoltageStart) / fStandardVoltage * 100;
								newWaveData.m_VoltageEnd_Percentage = G4_DigitalValue2Voltage(newWaveData.m_VoltageEnd) / fStandardVoltage * 100;
							}
						}
						else
						{
							newWaveData.m_VoltageStart_Percentage = 0;
							newWaveData.m_VoltageEnd_Percentage = 0;
						}
						//Wave数据保存
						m_NewWaveDatas.Add(newWaveData);
						m_PhaseCount++;
						if (m_PhaseCount == 64) bLoopOverFlg = true;
					}
				}
				else
				{		//新格式波形导入

					//					if (nTotalSum != m_SegmentCount || m_nWavePhase[0] == 0 ) return false;

					//数据构成
					newWaveData.m_VoltageStart = uStartVoltage;
					newWaveData.m_VoltageEnd = uEndVoltage;
					newWaveData.m_Voltage_Cycle = uCycle;

					//开始终了电压百分比设定
					if (uStandardVoltage != 0)
					{
						{
							newWaveData.m_VoltageStart_Percentage = G4_DigitalValue2Voltage(newWaveData.m_VoltageStart) / fStandardVoltage * 100;
							newWaveData.m_VoltageEnd_Percentage = G4_DigitalValue2Voltage(newWaveData.m_VoltageEnd) / fStandardVoltage * 100;
						}
					}
					else
					{
						newWaveData.m_VoltageStart_Percentage = 0;
						newWaveData.m_VoltageEnd_Percentage = 0;
					}

					if (nWaveIndex < m_nWaveTotal)
					{
						//Wave
						if (m_nWavePhase[nWaveIndex] - (++m_nWaveIndex[nWaveIndex]) > 0)
						{
							//Wave数据保存
							m_NewWaveDatas.Add(newWaveData);
							m_PhaseCount++;
							if (m_PhaseCount == 64) bLoopOverFlg = true;
						}
						else if (m_nWavePhase[nWaveIndex] == m_nWaveIndex[nWaveIndex])
						{
							//当前的Wave数据保存
							m_NewWaveDatas.Add(newWaveData);
							m_PhaseCount++;
							if (m_PhaseCount == 64) bLoopOverFlg = true;

							if (bLoopOverFlg == false)
							{
								//8段数据补齐
								for (int k = 0; k < m_nWaveTotal - m_nWavePhase[nWaveIndex]; k++)
								{
									NullDataForWave(ref  m_NewWaveDatas, uStandardVoltage);
									m_PhaseCount++;
									if (m_PhaseCount == 64)
									{
										bLoopOverFlg = true;
										break;
									}
								}
								nWaveIndex++;

								if (bLoopOverFlg == false)
								{
									if (nWaveIndex < m_nWaveTotal)
									{
										//如果下一个波段没数据
										if (m_nWavePhase[nWaveIndex] == 0)
										{
											//8段数据补齐
											for (int k = 0; k < m_nWaveTotal - m_nWavePhase[nWaveIndex]; k++)
											{
												NullDataForWave(ref  m_NewWaveDatas, uStandardVoltage);
												m_PhaseCount++;
												if (m_PhaseCount == 64)
												{
													bLoopOverFlg = true;
													break;
												}
											}
											nWaveIndex++;
										}
									}
								}
							}
						}
					}
				}
				if (bLoopOverFlg == true) break;
			}
			for (int nRemainder = 0; nRemainder < 64 - m_PhaseCount; nRemainder++)
			{
				//补足64段
				NullDataForWave(ref  m_NewWaveDatas, uStandardVoltage);
			}

			//剩余NM长度 = 数据总长度 - Summary长度 - 段数标示长度 - Wave长度(段数*每段长度)
			int ByteLeft = m_OriginalWaveData.Length - 6 - 6 - m_SegmentCount * 6;
			if (ByteLeft % 4 != 0)
			{
				return false;
			}
			int NMDataCount = ByteLeft / 4;
			int nBufferCounter = 0;
			int nNMPhase = 0;
			BufferIndex = m_OriginalWaveData.Length - ByteLeft;
			m_NewWaveNMDatas.Clear();
			for (int i = 0; i < NMDataCount; i++)
			{
				nBufferCounter = BufferIndex;
				UInt32 Data = BitConverter.ToUInt32(m_OriginalWaveData, BufferIndex);
				m_NM.Add(Data);
				BufferIndex += 4;

				//设置到新NMData结构中（NewWaveNMData）
				NewWaveNMData newNMData = new NewWaveNMData();
				if (0 == i)
				{
					byte byNMphase = m_OriginalWaveData[nBufferCounter++];		//段数
					nNMPhase = byNMphase >> 4;

					nBufferCounter += 1;
					byte byStartCycle = m_OriginalWaveData[nBufferCounter++];	//起始周期
					nBufferCounter++;											//0x80固定
				}
				else
				{
					nBufferCounter += 1;
					byte byteNM = m_OriginalWaveData[nBufferCounter++];
					{//NM3电平取得
						byteNM = Convert.ToByte(byteNM);
						newNMData.m_NM[3] = GetPowerLevelInfo(byteNM);
						if (newNMData.m_NM[3] == "10")
						{
							if (i == 1) newNMData.m_NM[3] = "01";					//强制赋成高电平
							else newNMData.m_NM[3] = m_NewWaveNMDatas[i - 2].m_NM[3];
						}
					}
					{//NM2电平取得
						byteNM = Convert.ToByte(byteNM >> 2);
						newNMData.m_NM[2] = GetPowerLevelInfo(byteNM);
						if (newNMData.m_NM[2] == "10")
						{
							if (i == 1) newNMData.m_NM[2] = "01";					//强制赋成高电平
							else newNMData.m_NM[2] = m_NewWaveNMDatas[i - 2].m_NM[2];
						}
					}
					{//NM1电平取得
						byteNM = Convert.ToByte(byteNM >> 2);
						newNMData.m_NM[1] = GetPowerLevelInfo(byteNM);
						if (newNMData.m_NM[1] == "10")
						{
							if (i == 1) newNMData.m_NM[1] = "01";					//强制赋成高电平
							else newNMData.m_NM[1] = m_NewWaveNMDatas[i - 2].m_NM[1];
						}
					}
					{//NM0电平取得
						byteNM = Convert.ToByte(byteNM >> 2);
						newNMData.m_NM[0] = GetPowerLevelInfo(byteNM);
						if (newNMData.m_NM[0] == "10")
						{
							if (i == 1) newNMData.m_NM[0] = "01";					//强制赋成高电平		
							else newNMData.m_NM[0] = m_NewWaveNMDatas[i - 2].m_NM[0];
						}
					}
					//预备
					newNMData.m_NM[4] = "01";
					newNMData.m_NM[5] = "01";
					newNMData.m_NM[6] = "01";
					newNMData.m_NM[7] = "01";

					//周期设置
					byte LowByte = m_OriginalWaveData[nBufferCounter++];
					byte HighByte = m_OriginalWaveData[nBufferCounter++];
					byte[] DataNM = new byte[2];
					DataNM[0] = LowByte;
					DataNM[1] = HighByte;
					newNMData.m_Cycle = BitConverter.ToUInt16(DataNM, 0);

					//未使用的NM设定
					if (i > nNMPhase)
					{
						newNMData.m_NM[0] = "01";
						newNMData.m_NM[1] = "01";
						newNMData.m_NM[2] = "01";
						newNMData.m_NM[3] = "01";
						newNMData.m_NM[4] = "01";
						newNMData.m_NM[5] = "01";
						newNMData.m_NM[6] = "01";
						newNMData.m_NM[7] = "01";
						newNMData.m_Cycle = m_NewWaveNMDatas[i - 2].m_Cycle;
						m_NewWaveNMDatas.Add(newNMData);
					}
					else
					{
						m_NewWaveNMDatas.Add(newNMData);
					}
				}
			}
			//NM数据填充
			for (int n = m_NewWaveNMDatas.Count; n < 9; n++)
			{
				NewWaveNMData newNMData = new NewWaveNMData();
				newNMData.m_NM[0] = "01";
				newNMData.m_NM[1] = "01";
				newNMData.m_NM[2] = "01";
				newNMData.m_NM[3] = "01";
				newNMData.m_NM[4] = "01";
				newNMData.m_NM[5] = "01";
				newNMData.m_NM[6] = "01";
				newNMData.m_NM[7] = "01";
				newNMData.m_Cycle = m_NewWaveNMDatas[m_NewWaveNMDatas.Count - 1].m_Cycle;
				m_NewWaveNMDatas.Add(newNMData);
			}

			return true;
		}

		/// <summary>
		/// 预读波形数据解析
		/// </summary>
		public bool NewParse2(string strHBTypeName, byte[] WaveData, double CycleValue)
		{
			m_WaveSummary.Clear();
			m_NewWaveDatas.Clear();
			m_NM.Clear();
			m_OriginalWaveData = WaveData;

			m_SegmentCount = (int)m_OriginalWaveData[0];

			int BufferIndex = 0;
			m_WaveSummary.Add("Summary1", m_OriginalWaveData[BufferIndex++]);
			BufferIndex++;
			m_WaveSummary.Add("Summary2", m_OriginalWaveData[BufferIndex++]);
			BufferIndex++;
			//总周期取得
			byte HByte = m_OriginalWaveData[BufferIndex++];
			byte LByte = m_OriginalWaveData[BufferIndex++];
			byte[] DataCycle = new byte[2];
			DataCycle[0] = HByte;
			DataCycle[1] = LByte;
			m_usTotalCycle = BitConverter.ToUInt16(DataCycle, 0);

			m_WaveSummary.Add("Summary3", HByte);
			m_WaveSummary.Add("Summary4", Convert.ToByte(LByte & 0x7f));


			// 各段长度解析
			//第1 byte：0~3位表示波1所包含的段数
			//			4~7位表示波2所包含的段数
			//。。。
			//第4 byte：0~3位表示波7所包含的段数
			//			4~7位表示波8所包含的段数
			//第5byte，第6 byte 预留
			const int m_nWaveTotal = 8;

			int[] m_nWavePhase = new int[m_nWaveTotal] { 0, 0, 0, 0, 0, 0, 0, 0 };
			int[] m_nWaveIndex = new int[m_nWaveTotal] { 0, 0, 0, 0, 0, 0, 0, 0 };
			{
				//取得各通道数据
				m_nWavePhase[0] = Convert.ToInt32((m_OriginalWaveData[BufferIndex] & 0xf0) >> 4);
				m_nWavePhase[1] = Convert.ToInt32(m_OriginalWaveData[BufferIndex] & 0x0f);
				BufferIndex++;
				m_nWavePhase[2] = Convert.ToInt32((m_OriginalWaveData[BufferIndex] & 0xf0) >> 4);
				m_nWavePhase[3] = Convert.ToInt32(m_OriginalWaveData[BufferIndex] & 0x0f);
				BufferIndex++;
				m_nWavePhase[4] = Convert.ToInt32((m_OriginalWaveData[BufferIndex] & 0xf0) >> 4);
				m_nWavePhase[5] = Convert.ToInt32(m_OriginalWaveData[BufferIndex] & 0x0f);
				BufferIndex++;
				m_nWavePhase[6] = Convert.ToInt32((m_OriginalWaveData[BufferIndex] & 0xf0) >> 4);
				m_nWavePhase[7] = Convert.ToInt32(m_OriginalWaveData[BufferIndex] & 0x0f);
				BufferIndex += 3;
			}

			//数字量设定到元Wave结构中
			ushort uStandardVoltage = 0;	//平台电压（数字量）
			float fStandardVoltage = 0.0f;	//平台电压（模拟电压）
			ushort uStartVoltage = 0;		//开始电压
			ushort uEndVoltage = 0;			//终了电压
			ushort uCycle = 0;				//电压周期
			int m_PhaseCount = 0;			//NewWaveDatas的计数器
			int nWaveIndex = 0;				//波段标示
			int nTotalIs0Flag = 0;			//波段是否全为0标志
			int nTotalSum = 0;				//各波段之和
			bool bLoopOverFlg = false;		//循环结束标志
			for (int i = 0; i < m_SegmentCount; i++)
			{
				byte HighByte, LowByte;
				byte[] Data = new byte[2];

				//元Wave结构转换成新Wave结构（WaveDataSnip -> NewWaveData）
				NewWaveData newWaveData = new NewWaveData();

				//开始电压
				HighByte = m_OriginalWaveData[BufferIndex++];
				LowByte = m_OriginalWaveData[BufferIndex++];
				Data[0] = HighByte;
				Data[1] = LowByte;
				uStartVoltage = BitConverter.ToUInt16(Data, 0);
				if (0 == i)
				{
					//平台电压设定
					uStandardVoltage = uStartVoltage;
					fStandardVoltage = G4_DigitalValue2Voltage(uStandardVoltage);

					newWaveData.m_StandardVoltage = fStandardVoltage;
					for (int n = 0; n < m_nWaveTotal; n++) { nTotalIs0Flag |= m_nWavePhase[n]; nTotalSum += m_nWavePhase[n]; }
				}
				//终了电压
				HighByte = m_OriginalWaveData[BufferIndex++];
				LowByte = m_OriginalWaveData[BufferIndex++];
				Data[0] = HighByte;
				Data[1] = LowByte;
				uEndVoltage = BitConverter.ToUInt16(Data, 0);

				//持续周期
				HighByte = m_OriginalWaveData[BufferIndex++];
				LowByte = m_OriginalWaveData[BufferIndex++];
				Data[0] = HighByte;
				Data[1] = LowByte;
				uCycle = (ushort)(BitConverter.ToUInt16(Data, 0) & 0x1FFF);

				if (nTotalIs0Flag == 0)	//原格式波形导入
				{
					//波形划分（每两个相邻的平台电压作为一个波）
					if (uStartVoltage == uEndVoltage && uStartVoltage == uStandardVoltage && i != 0 && m_PhaseCount % m_nWaveTotal != 0)
					{
						//填充至8段，开始终了电压都为平台电压，周期为0
						int nRemainder = m_PhaseCount % m_nWaveTotal;
						for (int nIndex = 0; nIndex < m_nWaveTotal - nRemainder; nIndex++)
						{
							NullDataForWave(ref  m_NewWaveDatas, uStandardVoltage);
							m_PhaseCount++;
							if (m_PhaseCount == 64)
							{
								bLoopOverFlg = true;
								break;
							}
						}
					}
					//64段已满，不能再填充数据
					if (bLoopOverFlg == false)
					{
						newWaveData.m_VoltageStart = uStartVoltage;
						newWaveData.m_VoltageEnd = uEndVoltage;
						newWaveData.m_Voltage_Cycle = uCycle;

						//开始终了电压百分比设定
						if (uStandardVoltage != 0)
						{
							{
								newWaveData.m_VoltageStart_Percentage = G4_DigitalValue2Voltage(newWaveData.m_VoltageStart) / fStandardVoltage * 100;
								newWaveData.m_VoltageEnd_Percentage = G4_DigitalValue2Voltage(newWaveData.m_VoltageEnd) / fStandardVoltage * 100;
							}
						}
						else
						{
							newWaveData.m_VoltageStart_Percentage = 0;
							newWaveData.m_VoltageEnd_Percentage = 0;
						}
						//Wave数据保存
						m_NewWaveDatas.Add(newWaveData);
						m_PhaseCount++;
						if (m_PhaseCount == 64) bLoopOverFlg = true;
					}
				}
				else
				{		//新格式波形导入

					//					if (nTotalSum != m_SegmentCount || m_nWavePhase[0] == 0 ) return false;

					//数据构成
					newWaveData.m_VoltageStart = uStartVoltage;
					newWaveData.m_VoltageEnd = uEndVoltage;
					newWaveData.m_Voltage_Cycle = uCycle;

					//开始终了电压百分比设定
					if (uStandardVoltage != 0)
					{
						{
							newWaveData.m_VoltageStart_Percentage = G4_DigitalValue2Voltage(newWaveData.m_VoltageStart) / fStandardVoltage * 100;
							newWaveData.m_VoltageEnd_Percentage = G4_DigitalValue2Voltage(newWaveData.m_VoltageEnd) / fStandardVoltage * 100;
						}
					}
					else
					{
						newWaveData.m_VoltageStart_Percentage = 0;
						newWaveData.m_VoltageEnd_Percentage = 0;
					}

					if (nWaveIndex < m_nWaveTotal)
					{
						//Wave
						if (m_nWavePhase[nWaveIndex] - (++m_nWaveIndex[nWaveIndex]) > 0)
						{
							//Wave数据保存
							m_NewWaveDatas.Add(newWaveData);
							m_PhaseCount++;
							if (m_PhaseCount == 64) bLoopOverFlg = true;
						}
						else if (m_nWavePhase[nWaveIndex] == m_nWaveIndex[nWaveIndex])
						{
							//当前的Wave数据保存
							m_NewWaveDatas.Add(newWaveData);
							m_PhaseCount++;
							if (m_PhaseCount == 64) bLoopOverFlg = true;

							if (bLoopOverFlg == false)
							{
								//8段数据补齐
								for (int k = 0; k < m_nWaveTotal - m_nWavePhase[nWaveIndex]; k++)
								{
									NullDataForWave(ref  m_NewWaveDatas, uStandardVoltage);
									m_PhaseCount++;
									if (m_PhaseCount == 64)
									{
										bLoopOverFlg = true;
										break;
									}
								}
								nWaveIndex++;

								if (bLoopOverFlg == false)
								{
									if (nWaveIndex < m_nWaveTotal)
									{
										//如果下一个波段没数据
										if (m_nWavePhase[nWaveIndex] == 0)
										{
											//8段数据补齐
											for (int k = 0; k < m_nWaveTotal - m_nWavePhase[nWaveIndex]; k++)
											{
												NullDataForWave(ref  m_NewWaveDatas, uStandardVoltage);
												m_PhaseCount++;
												if (m_PhaseCount == 64)
												{
													bLoopOverFlg = true;
													break;
												}
											}
											nWaveIndex++;
										}
									}
								}
							}
						}
					}
				}
				if (bLoopOverFlg == true) break;
			}
			for (int nRemainder = 0; nRemainder < 64 - m_PhaseCount; nRemainder++)
			{
				//补足64段
				NullDataForWave(ref  m_NewWaveDatas, uStandardVoltage);
			}

			//剩余NM长度 = 数据总长度 - Summary长度 - 段数标示长度 - Wave长度(段数*每段长度)
			int ByteLeft = m_OriginalWaveData.Length - 6 - 6 - m_SegmentCount * 6;
			if (ByteLeft % 4 != 0)
			{
				return false;
			}
			int NMDataCount = ByteLeft / 4;
			int nBufferCounter = 0;
			int nNMPhase = 0;
			BufferIndex = m_OriginalWaveData.Length - ByteLeft;
			m_NewWaveNMDatas.Clear();
			for (int i = 0; i < NMDataCount; i++)
			{
				nBufferCounter = BufferIndex;
				UInt32 Data = BitConverter.ToUInt32(m_OriginalWaveData, BufferIndex);
				m_NM.Add(Data);
				BufferIndex += 4;

				//设置到新NMData结构中（NewWaveNMData）
				NewWaveNMData newNMData = new NewWaveNMData();
				if (0 == i)
				{
					byte byNMphase = m_OriginalWaveData[nBufferCounter++];		//段数
					nNMPhase = byNMphase >> 4;

					nBufferCounter += 1;
					byte byStartCycle = m_OriginalWaveData[nBufferCounter++];	//起始周期
					nBufferCounter++;											//0x80固定
				}
				else
				{
					nBufferCounter += 1;
					byte byteNM = m_OriginalWaveData[nBufferCounter++];
					{//NM3电平取得
						byteNM = Convert.ToByte(byteNM);
						newNMData.m_NM[3] = GetPowerLevelInfo(byteNM);
						if (newNMData.m_NM[3] == "10")
						{
							if (i == 1) newNMData.m_NM[3] = "01";					//强制赋成高电平
							else newNMData.m_NM[3] = m_NewWaveNMDatas[i - 2].m_NM[3];
						}
					}
					{//NM2电平取得
						byteNM = Convert.ToByte(byteNM >> 2);
						newNMData.m_NM[2] = GetPowerLevelInfo(byteNM);
						if (newNMData.m_NM[2] == "10")
						{
							if (i == 1) newNMData.m_NM[2] = "01";					//强制赋成高电平
							else newNMData.m_NM[2] = m_NewWaveNMDatas[i - 2].m_NM[2];
						}
					}
					{//NM1电平取得
						byteNM = Convert.ToByte(byteNM >> 2);
						newNMData.m_NM[1] = GetPowerLevelInfo(byteNM);
						if (newNMData.m_NM[1] == "10")
						{
							if (i == 1) newNMData.m_NM[1] = "01";					//强制赋成高电平
							else newNMData.m_NM[1] = m_NewWaveNMDatas[i - 2].m_NM[1];
						}
					}
					{//NM0电平取得
						byteNM = Convert.ToByte(byteNM >> 2);
						newNMData.m_NM[0] = GetPowerLevelInfo(byteNM);
						if (newNMData.m_NM[0] == "10")
						{
							if (i == 1) newNMData.m_NM[0] = "01";					//强制赋成高电平		
							else newNMData.m_NM[0] = m_NewWaveNMDatas[i - 2].m_NM[0];
						}
					}
					//预备
					newNMData.m_NM[4] = "01";
					newNMData.m_NM[5] = "01";
					newNMData.m_NM[6] = "01";
					newNMData.m_NM[7] = "01";

					//周期设置
					byte LowByte = m_OriginalWaveData[nBufferCounter++];
					byte HighByte = m_OriginalWaveData[nBufferCounter++];
					byte[] DataNM = new byte[2];
					DataNM[0] = LowByte;
					DataNM[1] = HighByte;
					newNMData.m_Cycle = BitConverter.ToUInt16(DataNM, 0);

					//未使用的NM设定
					if (i > nNMPhase)
					{
						newNMData.m_NM[0] = "01";
						newNMData.m_NM[1] = "01";
						newNMData.m_NM[2] = "01";
						newNMData.m_NM[3] = "01";
						newNMData.m_NM[4] = "01";
						newNMData.m_NM[5] = "01";
						newNMData.m_NM[6] = "01";
						newNMData.m_NM[7] = "01";
						newNMData.m_Cycle = m_NewWaveNMDatas[i - 2].m_Cycle;
						m_NewWaveNMDatas.Add(newNMData);
					}
					else
					{
						m_NewWaveNMDatas.Add(newNMData);
					}
				}
			}

            //MaxCycle = m_NewWaveNMDatas[m_NewWaveNMDatas.Count - 1].m_Cycle;

			//NM数据填充
			for (int n = m_NewWaveNMDatas.Count; n < 9; n++)
			{
				NewWaveNMData newNMData = new NewWaveNMData();
				newNMData.m_NM[0] = "01";
				newNMData.m_NM[1] = "01";
				newNMData.m_NM[2] = "01";
				newNMData.m_NM[3] = "01";
				newNMData.m_NM[4] = "01";
				newNMData.m_NM[5] = "01";
				newNMData.m_NM[6] = "01";
				newNMData.m_NM[7] = "01";
				newNMData.m_Cycle = m_NewWaveNMDatas[m_NewWaveNMDatas.Count - 1].m_Cycle;
				m_NewWaveNMDatas.Add(newNMData);
			}

			//获取NM周期修正
			UpdateNMCorrect(CycleValue, false);

			return true;
		}

		/// <summary>
		/// 填充平台数据
		/// </summary>
		/// <param name="m_NewWaveDatas"></param>
		/// <param name="uVoltage"></param>
		private void NullDataForWave(ref System.Collections.Generic.List<NewWaveData> m_NewWaveDatas, ushort uVoltage)
		{
			NewWaveData nullWaveData = new NewWaveData();
			nullWaveData.m_VoltageStart = uVoltage;			//开始电压为平台电压
			nullWaveData.m_VoltageEnd = uVoltage;			//终了电压为平台电压
			nullWaveData.m_Voltage_Cycle = 0;				//周期为0
			nullWaveData.m_VoltageStart_Percentage = 100;	//开始电压百分比为100%
			nullWaveData.m_VoltageEnd_Percentage = 100;		//终了电压百分比为100%
			m_NewWaveDatas.Add(nullWaveData);
		}

		/// <summary>
		/// 获取最新的Wave和NM数据
		/// </summary>
		/// <returns></returns>
		public byte[] NewGetBytes(bool bImportPreFlag, bool bWriteFlag, int nVSD, int nWaveName, ushort[] nWaveNum, ushort nTotalCount)
		{
			//数据总长度 = VSD序号 + WaveName序号 + Summary长度 + Wave各段长度 + Wave长度(段数*每段长度) + NM长度（段数*每段长度）
			int TotalLength = 1 + 1 + 6 + 6 + 6 * nTotalCount + 4 * m_NM.Count;
			byte[] Bytes = new byte[TotalLength];

			int BufferIndex = 0;

			//WaveName
			Bytes[BufferIndex++] = Convert.ToByte(nWaveName);


			//VSD
			Bytes[BufferIndex++] = bWriteFlag ? Convert.ToByte(nVSD | 0x80) : Convert.ToByte(nVSD);

			//Summary
			byte[] bySummary = new byte[4];
			int nSummary = 0;
			foreach (KeyValuePair<string, byte> kv in m_WaveSummary)
			{
				bySummary[nSummary++] = kv.Value;
			}
			//用户设定总段数
			int nSettingPhase = bySummary[0];
			//用户设定总周期数
			ushort usSettingCycle = BitConverter.ToUInt16(bySummary, 2);

			if (bImportPreFlag == false)
			{
				Bytes[BufferIndex++] = bySummary[0];
				BufferIndex++;
				Bytes[BufferIndex++] = bySummary[1];
				BufferIndex++;
				Bytes[BufferIndex++] = bySummary[2];
				Bytes[BufferIndex++] = bySummary[3];

				m_usTotalCycle = usSettingCycle;
			}
			else
			{
				//预读总段数回设
				Bytes[BufferIndex++] = Convert.ToByte(nTotalCount);
				BufferIndex++;
				Bytes[BufferIndex++] = bySummary[1];
				BufferIndex++;

				//总周期数设定为预读总周期数
				byte[] byTotalCycle = BitConverter.GetBytes(m_usTotalCycle);
				Bytes[BufferIndex++] = byTotalCycle[0];
				Bytes[BufferIndex++] = byTotalCycle[1];
			}

			//各波段数追加
			Bytes[BufferIndex++] = Convert.ToByte((nWaveNum[0] << 4) | (nWaveNum[1] & 0x0F));
			Bytes[BufferIndex++] = Convert.ToByte((nWaveNum[2] << 4) | (nWaveNum[3] & 0x0F));
			Bytes[BufferIndex++] = Convert.ToByte((nWaveNum[4] << 4) | (nWaveNum[5] & 0x0F));
			Bytes[BufferIndex++] = Convert.ToByte((nWaveNum[6] << 4) | (nWaveNum[7] & 0x0F));
			BufferIndex += 2;
			//Wave
			for (int i = 0; i < m_NewWaveDatas.Count; i++)
			{
				byte[] Data = new byte[2];
				NewWaveData newWaveData = m_NewWaveDatas[i];

				if (newWaveData.m_Voltage_Cycle == 0) continue;
				//开始电压
				Data = BitConverter.GetBytes(newWaveData.m_VoltageStart);
				Array.Copy(Data, 0, Bytes, BufferIndex, 2);
				BufferIndex += 2;

				//终了电压
				Data = BitConverter.GetBytes(newWaveData.m_VoltageEnd);
				Array.Copy(Data, 0, Bytes, BufferIndex, 2);
				BufferIndex += 2;

				//周期
				Data = BitConverter.GetBytes(newWaveData.m_Voltage_Cycle);
				Array.Copy(Data, 0, Bytes, BufferIndex, 2);
				BufferIndex += 2;
			}
			////段数不足，平台电压补足，剩余周期数平分
			//ushort nCyclePer = 0;
			//if (m_SegmentCount > nTotalCount)
			//{
			//    nCyclePer =(ushort)((m_usTotalCycle - usSettingCycle) / (m_SegmentCount - nTotalCount));
			//}
			//for (int nRemaider = 0; nRemaider < m_SegmentCount - nTotalCount; nRemaider++)
			//{
			//    byte[] DataDummy = new byte[2];
			//    //开始电压 = 平台电压
			//    DataDummy = BitConverter.GetBytes(m_NewWaveDatas[0].m_VoltageStart);
			//    Array.Copy(DataDummy, 0, Bytes, BufferIndex, 2);
			//    BufferIndex += 2;

			//    //终了电压 = 平台电压
			//    DataDummy = BitConverter.GetBytes(m_NewWaveDatas[0].m_VoltageStart);
			//    Array.Copy(DataDummy, 0, Bytes, BufferIndex, 2);
			//    BufferIndex += 2;

			//    if (nRemaider == m_SegmentCount - nTotalCount - 1) nCyclePer += (ushort)((m_usTotalCycle - usSettingCycle) % (m_SegmentCount - nTotalCount));
			//    //周期
			//    DataDummy = BitConverter.GetBytes(nCyclePer);
			//    Array.Copy(DataDummy, 0, Bytes, BufferIndex, 2);
			//    BufferIndex += 2;
			//}

			//NM
			for (int i = 0; i < m_NM.Count; i++)
			{
				if (0 == i)
				{
					byte[] Data = BitConverter.GetBytes(m_NM[i]);
					Array.Copy(Data, 0, Bytes, BufferIndex, 4);

					BufferIndex += 4;
				}
				else
				{
					NewWaveNMData newNMData = m_NewWaveNMDatas[i - 1];
					byte byteNM = 0x00;
					BufferIndex++;
					//NM0电平设置
					byte byteNM0 = SetPowerLevelInfo(newNMData.m_NM[0]);
					byte byteNM1 = SetPowerLevelInfo(newNMData.m_NM[1]);
					byte byteNM2 = SetPowerLevelInfo(newNMData.m_NM[2]);
					byte byteNM3 = SetPowerLevelInfo(newNMData.m_NM[3]);

					Bytes[BufferIndex] = Convert.ToByte(byteNM | byteNM0 | (byteNM1 >> 2) | (byteNM2 >> 4) | (byteNM3 >> 6));
					BufferIndex++;

					//周期设定
					byte[] Data = new byte[2];
					Data = BitConverter.GetBytes(newNMData.m_Cycle);
					Array.Copy(Data, 0, Bytes, BufferIndex, 2);
					BufferIndex += 2;
				}
			}

            //波形总周期修正，波形总周期大于NM总周期
            if (Bytes.Length > 10)
            {
                byte wave_Hbyte = Bytes[6];
                byte wave_Lbyte = Bytes[7];
                byte[] wave_Data = new byte[2];
                wave_Data[0] = wave_Hbyte;
                wave_Data[1] = (byte)(wave_Lbyte & 0x7F);
                ushort wave_Sum = BitConverter.ToUInt16(wave_Data, 0);

                byte nm_Hbyte = Bytes[Bytes.Length - 2];
                byte nm_Lbyte = Bytes[Bytes.Length - 1];
                byte[] nm_Data = new byte[2];
                nm_Data[0] = nm_Hbyte;
                nm_Data[1] = (byte)(nm_Lbyte & 0x7F);
                ushort nm_Sum = BitConverter.ToUInt16(nm_Data, 0);

                if (wave_Sum <= nm_Sum)
                {
                    wave_Sum = (ushort)(nm_Sum + 6);
                    wave_Data = BitConverter.GetBytes(wave_Sum);
                    Bytes[6] = wave_Data[0];
                    Bytes[7] = (byte)(wave_Data[1] | 0x80);
                }
            }

			return Bytes;
		}

		/// <summary>
		/// 从1byte数据中NM0~NM3的高低电平值
		/// </summary>
		/// <param name="byteNM"></param>
		/// <returns></returns>
		public string GetPowerLevelInfo(byte byteNM)
		{
			string strNM = "";
			int nBit = byteNM & 0x03;
			if (nBit == 0x00) strNM = "00";
			else if (nBit == 0x01) strNM = "01";
			else if (nBit == 0x02) strNM = "10";
			return strNM;
		}

		public byte SetPowerLevelInfo(string strNM)
		{
			byte byteNM = 0x00;
			if (strNM == "00") byteNM = 0x00;
			else if (strNM == "01") byteNM = 0x40;
			else if (strNM == "10") byteNM = 0x80;
			return byteNM;
		}

		/// <summary>
		/// G4 模拟电压转换成数字量,最小数字量为4确保
		/// </summary>
		/// <param name="fVoltage"></param>
		/// <returns></returns>
		public ushort G4_Voltage2DigitalValue(float fVoltage)
		{
			ushort usDigitalValue = 4;

			//if (fVoltage > 1.43) usDigitalValue = (ushort)((fVoltage - 1.43) / 0.036875 + 0.5);

            if (VDDataList.Count == 0)
                SetVDdataList();

            float A = 0;
            float B = 0;

            GetVDdataAB(1, fVoltage, ref A, ref B);

            usDigitalValue = (ushort)((fVoltage - B) / A);

            if (usDigitalValue < 0)
                usDigitalValue = 0;

			return usDigitalValue;
		}
		/// <summary>
		/// G4 数字量转换成模拟电压
		/// </summary>
		/// <param name="nDigitalValue"></param>
		/// <returns></returns>
		public float G4_DigitalValue2Voltage(int nDigitalValue, bool isNew = true)
		{
			float fVoltage = 0.0f;
            float A = 0;
            float B = 0;

            if (!isNew)
            {
                fVoltage = (float)(0.036875 * nDigitalValue + 1.43);

                //转换
                if (VVDataList.Count == 0)
                    SetVVdataList();

                GetVVdataAB(1, fVoltage, ref A, ref B);

                //fVoltage = A * fVoltage + B;
                fVoltage = (fVoltage - B) / A;

                if (fVoltage < 0)
                    fVoltage = 0;

            }
            else
            {
                if (VDDataList.Count == 0)
                    SetVDdataList();

                GetVDdataAB(2, nDigitalValue, ref A, ref B);

                fVoltage = A * nDigitalValue + B;

                if (fVoltage < 0)
                    fVoltage = 0;
            }

			return fVoltage;
		}
		//V7 End

		public void UpdateNMCorrect(double CycleValue, bool enableNMCorrect)
		{
			//获取NM周期修正
			{
				//根据波形信息计算出期望的NM周期
				int[] CaculateNMCycle = new int[9];
				for (int n = 0; n < 8; n++)//此处代码是从UserControl8里拷贝出来的，8对应m_WavePhase_Number，以后不应该写死
				{
					CaculateNMCycle[1] += NewWaveDatas[n].m_Voltage_Cycle;
					CaculateNMCycle[2] += NewWaveDatas[1 * 8 + n].m_Voltage_Cycle;
					CaculateNMCycle[3] += NewWaveDatas[2 * 8 + n].m_Voltage_Cycle;
					CaculateNMCycle[4] += NewWaveDatas[3 * 8 + n].m_Voltage_Cycle;
					CaculateNMCycle[5] += NewWaveDatas[4 * 8 + n].m_Voltage_Cycle;
					CaculateNMCycle[6] += NewWaveDatas[5 * 8 + n].m_Voltage_Cycle;
					CaculateNMCycle[7] += NewWaveDatas[6 * 8 + n].m_Voltage_Cycle;
					CaculateNMCycle[8] += NewWaveDatas[7 * 8 + n].m_Voltage_Cycle;
				}

				CaculateNMCycle[2] += CaculateNMCycle[1];
				CaculateNMCycle[3] += CaculateNMCycle[2];
				CaculateNMCycle[4] += CaculateNMCycle[3];
				CaculateNMCycle[5] += CaculateNMCycle[4];
				CaculateNMCycle[6] += CaculateNMCycle[5];
				CaculateNMCycle[7] += CaculateNMCycle[6];
				CaculateNMCycle[8] += CaculateNMCycle[7];

				//计算出期望值与实际值之间的差距
				for (int n = 0; n < 8; n++)
				{
					if (enableNMCorrect)
					{
						m_Wave2Correction[n] = (CaculateNMCycle[n + 1] - m_NewWaveNMDatas[n + 1].m_Cycle) / CycleValue;
					}
					else
					{
						m_Wave2Correction[n] = 0.0f;
					}
				}
			}
		}

		public int AvailableWaveCount
		{
			get
			{
				int count = 0;
				foreach (NewWaveData waveData in m_NewWaveDatas)
				{
					if (waveData.m_Voltage_Cycle != 0)
					{
						count++;
					}
				}

				return count;
			}
		}

		public bool Validation(int vsd)
		{
#warning "VSD信息应该放到波形数据里"

			bool valid = true;

			int availableWaveCount = AvailableWaveCount;
			if (1 == vsd)
			{
				if (availableWaveCount > 43)
				{
					valid = false;
					m_currentErrorCode = 0;
					goto EXIT;
				}
			}
			else if (2 == vsd)
			{
				if (availableWaveCount > 25)
				{
					valid = false;
					m_currentErrorCode = 0;
					goto EXIT;
				}
			}
			else
			{
				if (availableWaveCount > 29)
				{
					valid = false;
					m_currentErrorCode = 0;
					goto EXIT;
				}

			}


		EXIT:
			return valid;
		}

		private Dictionary<int, string> m_errorMsg = new System.Collections.Generic.Dictionary<int, string>();
		private void InitErrorList()
		{
			int errorNo = 0;
			m_errorMsg.Add(errorNo++, "实际段数超出当前VSD允许的最大段数");
		}

		private int m_currentErrorCode;
		public string LatestErrorMsg
		{
			get
			{
				return m_errorMsg[m_currentErrorCode];
			}
		}

        public void GetVDdataAB(int type,float value,ref float A,ref float B)
        {
            VDData data1 = new VDData();
            VDData data2 = new VDData();

            if (VDDataList.Count == 0)
                SetVDdataList();
            if (type == 1)
            {
                if (value < VDDataList[0].V)
                    value = VDDataList[0].V;
                else if (value > VDDataList[VDDataList.Count - 1].V)
                    value = VDDataList[VDDataList.Count - 1].V;

                for (int i = 0; i < VDDataList.Count - 1; i++)
                {
                    if (value >= VDDataList[i].V && value <= VDDataList[i + 1].V)
                    {
                        data1 = VDDataList[i];
                        data2 = VDDataList[i + 1];
                       
                        break;
                    }
                }
            }
            else
            {
                if (value < VDDataList[0].D)
                    value = VDDataList[0].D;
                if (value > VDDataList[VDDataList.Count - 1].D)
                    value = VDDataList[VDDataList.Count - 1].D;

                for (int i = 0; i < VDDataList.Count - 1; i++)
                {
                    if (value >= VDDataList[i].D && value <= VDDataList[i + 1].D)
                    {
                        data1 = VDDataList[i];
                        data2 = VDDataList[i + 1];

                        break;
                    }
                }
            }

            A = (data2.V - data1.V) / (data2.D - data1.D);
            B = (data1.D * data2.V - data2.D * data1.V) / (data1.D - data2.D);
        }

        public void GetVVdataAB(int type, float value, ref float A, ref float B)
        {
            VVData data1 = new VVData();
            VVData data2 = new VVData();

            if (VVDataList.Count == 0)
                SetVVdataList();

            if (value < VVDataList[0].OldV)
            {
                value = VVDataList[0].OldV;
            }
            else if (value > VVDataList[VVDataList.Count - 1].OldV)
            {
                value = VVDataList[VVDataList.Count - 1].OldV;
            }

            for (int i = 0; i < VVDataList.Count - 1; i++)
            {
                if (value >= VVDataList[i].OldV && value <= VVDataList[i + 1].OldV)
                {
                    data1 = VVDataList[i];
                    data2 = VVDataList[i + 1];

                    break;
                }
            }

            A = (data2.OldV - data1.OldV) / (data2.NewV - data1.NewV);
            B = (data1.NewV * data2.OldV - data2.NewV * data1.OldV) / (data1.NewV - data2.NewV);
        }


        public void SetVDdataList()
        {
            //{20,0.0058}, {129,4.447},{281,10.887},{412,16.1157},{539,21.5393},{672,26.978},{761,30.675}
            //{17,0.4511},{123,4.6737},{273,10.8347},{401,15.8632},{529,21.0430},{659,26.3613},{747,29.9932}   //160918

            VDDataList.Clear();

            VDData vd = new VDData();
            vd.D = 17; vd.V = 0.4511f;
            VDDataList.Add(vd);

            vd = new VDData();
            vd.D = 123; vd.V = 4.6737f;
            VDDataList.Add(vd);

            vd = new VDData();
            vd.D = 273; vd.V = 10.8347f;
            VDDataList.Add(vd);

            vd = new VDData();
            vd.D = 401; vd.V = 15.8632f;
            VDDataList.Add(vd);

            vd = new VDData();
            vd.D = 529; vd.V = 21.043f;
            VDDataList.Add(vd);

            vd = new VDData();
            vd.D = 659; vd.V = 26.3613f;
            VDDataList.Add(vd);

            vd = new VDData();
            vd.D = 747; vd.V = 29.9932f;
            VDDataList.Add(vd);

        }

        public void SetVVdataList()
        {
            //{0.4528,1.9912},
            //{4.68,5.8175},
            //{10.8565,11.2525},
            //{15.8690,15.8862},
            //{21.0524,20.5206},
            //{26.3788,25.2525},
            //{30.0224,28.44}

            VVDataList.Clear();

            VVData vv = new VVData();
            vv.NewV = 0.4528f; vv.OldV = 1.9912f;
            VVDataList.Add(vv);

            vv = new VVData();
            vv.NewV = 4.68f; vv.OldV = 5.8175f;
            VVDataList.Add(vv);

            vv = new VVData();
            vv.NewV = 10.8565f; vv.OldV = 11.2525f;
            VVDataList.Add(vv);

            vv = new VVData();
            vv.NewV = 15.8690f; vv.OldV = 15.8862f;
            VVDataList.Add(vv);

            vv = new VVData();
            vv.NewV = 21.0524f; vv.OldV = 20.5206f;
            VVDataList.Add(vv);

            vv = new VVData();
            vv.NewV = 26.3788f; vv.OldV = 25.2525f;
            VVDataList.Add(vv);

            vv = new VVData();
            vv.NewV = 30.0224f; vv.OldV = 28.44f;
            VVDataList.Add(vv);
        }
	}


}
