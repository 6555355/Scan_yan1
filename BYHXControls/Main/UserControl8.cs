using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BYHXPrinterManager;
//using CyUSB;
using EpsonControlLibrary;
using System.IO;
using System.Diagnostics;
//using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;

using BYHXPrinterManager.Setting;

namespace WaveFormTool
{
	public partial class WaveMappingControl : UserControl
	{
		private WaveData m_WaveData = null;

		private List<WaveDataEx> m_Waves = new List<WaveDataEx>();
		private List<IAmplitude> m_AmplitudeToSends = new List<IAmplitude>();
		private List<WaveDataSnip> m_OriginalWaveSnips = null;//没有修改过的波形数据
		private List<string> ChannelNameList = new List<string>();				//通道名称列表
		private List<List<string>> WaveNameList = new List<List<string>>();		//波形名称列表
		private List<List<string>> WaveMappingList = new List<List<string>>();	//波形映射列表

		private int m_ChannelIndex = 0;
		private int m_VSDSelectIndex = 0;
		private int m_WaveSelectIndex = 0;
		private bool m_WriteWaveFlag = false;
		private int[] m_nNMArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
		private int WaveSegment = 0;
		private int NMSegment = 0;
		private int m_VSD_Number = 4;
		private int m_Wave_Number = 0;
		private int m_WaveName_Length = 8;			//波名字长度（7 byte + 0）
		private int m_WavePhase_Number = 8;			//波段总数
		private string strOpenFilePath = "";
		private float m_CycleValue = 0.0f;			//周期值 
		private int m_Channel_Number = 0;			//头板的通道数
		private bool m_ImportMappingFlag = false;	//波形映射导入结束标志
		private bool m_ImportOverFlag = false;		//波形导入结束标志
		private bool m_ImportPreFlag = false;		//预读波形/导入波形区分
		private string HBTypeName = "";				//头板类型
		private float m_xAxis = 0;				//X轴坐标
		private float m_yNM0Axis = 8;			//NM0的Y轴坐标(NM支持8段的场合，设为16)
		private float m_yNM1Axis = 6;			//NM1的Y轴坐标(NM支持8段的场合，设为14)
		private float m_yNM2Axis = 4;			//NM2的Y轴坐标(NM支持8段的场合，设为10)
		private float m_yNM3Axis = 2;			//NM3的Y轴坐标(NM支持8段的场合，设为8)
		private float m_yNM4Axis = 8;			//NM4的Y轴坐标
		private float m_yNM5Axis = 6;			//NM5的Y轴坐标
		private float m_yNM6Axis = 4;			//NM6的Y轴坐标
		private float m_yNM7Axis = 2;			//NM7的Y轴坐标

        private bool IsAutoStop = false;
        private bool IsAutoWave = false;
        private bool IsAutoPrint = false;
        private bool IsVoltage = false;

        int indexN = 0;
        int printCount = 0;
        double step = 0.2f;
        double maxStepValue = 1;
        double baseN = 0;
        int waveIdx = 0;
        int bandIdx = 0;

        double maxValue = 0;
        double currValue = 0;
        string currStr = "";

		public WaveMappingControl()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 头版类型获得，
		/// </summary>
		private void GetHBType()
		{
            HEAD_BOARD_TYPE Type = HEAD_BOARD_TYPE.KM256_12HEAD;
            Type = (HEAD_BOARD_TYPE)CoreInterface.get_HeadBoardType(true);

			//头板类型获得
            switch (Type)
			{
                //case HEAD_BOARD_TYPE.RICOH_GEN5_3H:
                //case HEAD_BOARD_TYPE.RICOH_GEN5_4H:
                //    HBTypeName = "G5";
                //    m_CycleValue = 12f;
                //    m_Wave_Number = 3;		//最多存3个波形
                //    break;
				
                //case HEAD_BOARD_TYPE.HEAD_BOARD_TYPE_RICOH_GEN4_64Pin_8H:
                //case HEAD_BOARD_TYPE.HEAD_BOARD_TYPE_RICOH_GEN4_64Pin_8H_V2:
                //case HEAD_BOARD_TYPE.HEAD_BOARD_TYPE_RICOH_GEN4_64Pin_8H_GH220:
				default:
					HBTypeName = "G4";
					m_CycleValue = 12f;
					m_Wave_Number = 3;		//最多存8个波形
					break;

                case HEAD_BOARD_TYPE.XAAR_1201_2H:
                //case HEAD_BOARD_TYPE.XAAR_1201_2HEAD_V2:
                case HEAD_BOARD_TYPE.XAAR_1201_4H:
                //case HEAD_BOARD_TYPE.XAAR_1201_4HEAD_V2:
                    HBTypeName = "XAAR";
					m_CycleValue = 12f;
					m_Wave_Number = 3;		//最多存3个波形
					break;
			}
		}

		/// <summary>
		/// 用户初始化控件
		/// </summary>
		private void CustomInitialize() 
		{
			//通道设置
			for (int nChannel = 0; nChannel < m_Channel_Number; nChannel++)
			{
				m_ComboBox_Channel.Items.Add(ChannelNameList[nChannel]);
			}

			for (int nVsd = 0; nVsd < 4; nVsd++)
			{
				string strVSD = "VSD" + (nVsd + 1).ToString();
				m_ComboBox_VSD.Items.Add(strVSD);
			}

            this.m_ComboBox_DPI.Items.Clear();
            foreach (XResDivMode place in Enum.GetValues(typeof(XResDivMode)))
            {
                string cmode = ResString.GetEnumDisplayName(typeof(XResDivMode), place);

                if (place < XResDivMode.PrintMode3)
                {
                    m_ComboBox_DPI.Items.Add(cmode);
                }
            }		

			//通道值设置为0
			m_ComboBox_Channel.SelectedIndex = 0;

			//VSD值为VSD1~VSD4,默认值为VDS1
			m_ComboBox_VSD.SelectedIndex = 0;

            if (m_ComboBox_DPI.Items.Count > 1)
            {
                m_ComboBox_DPI.SelectedIndex = 1;
            }

            ////波段初始化设置
            //CreateWaveArea();
            //m_TabControl_Wave.SelectedIndex = 0;

            ////绘图区域
            //m_GroupBox_Draw.Controls.Add(DrawChart());

			//更新UI
//			RefreshUI();
		}

		public void ProceedKernelMessage(IntPtr wParam, IntPtr lParam)
		{
            CoreMsgEnum kParam = (CoreMsgEnum)wParam.ToInt32();
            switch (kParam)
            {
                case CoreMsgEnum.HardPanelDirty:
                    {
                        Ep6Cmd cmd = (Ep6Cmd)lParam.ToInt32();
                        switch (cmd)
                        {
                            case Ep6Cmd.EP6_CMD_T_WAVE_READ_READY:
                                {
                                    FileLog("Ep6Cmd.EP6_CMD_T_WAVE_READ_READY");
                                    Debug.WriteLine("Ep6Cmd.EP6_CMD_T_WAVE_READ_READY 波形预读完成");

                                    int TotalLength = EpsonLCD.GetWaveDataTotalLength();
                                    m_WaveData.NewParse2(HBTypeName, EpsonLCD.GetWaveData(TotalLength), m_CycleValue);

                                    //保存刚刚获得的波形数据，在幅值方式为电压改变时有用
                                    m_OriginalWaveSnips = new List<WaveDataSnip>(m_WaveData.WaveDatas);

                                    ////导入Wave和NM数据
                                    //ImportWaveNMData();

                                    //btnIFSSave.Enabled = true;
                                    //m_Button_Import.Enabled = true;
                                }
                                break;
                            case Ep6Cmd.EP6_CMD_T_WAVE_SET_READY:
                                {
                                    //波形发送完成，发送幅值
                                    FileLog("Ep6Cmd.EP6_CMD_T_WAVE_SET_READY");
                                    Debug.WriteLine("Ep6Cmd.EP6_CMD_T_WAVE_SET_READY 波形发送完成，发送幅值");

                                    if (m_Waves.Count == 0)
                                        break;

                                    WaveDataEx WaveData = m_Waves[0];

                                    m_AmplitudeToSends.Clear();
                                    foreach (IAmplitude Amplitude in WaveData.Amplitudes)
                                    {
                                        m_AmplitudeToSends.Add(Amplitude);
                                    }

                                    if (0 == m_AmplitudeToSends.Count)
                                    {
                                        throw new Exception("0 == m_AmplitudeToSends.Count");
                                    }

                                    int Length = EpsonLCD.WriteAmplitudeData(m_AmplitudeToSends[0].GetBytes());
                                    EpsonLCD.SetAmplitudeTotalLength(Length);
                                }
                                break;
                            case Ep6Cmd.EP6_CMD_T_SWING_READY:
                                {
                                    FileLog("Ep6Cmd.EP6_CMD_T_SWING_READY");
                                    Debug.WriteLine("Ep6Cmd.EP6_CMD_T_SWING_READY 幅值下发完成");
                                    if (m_AmplitudeToSends.Count > 0)
                                    {
                                        m_AmplitudeToSends.RemoveAt(0);
                                    }
                                    if (m_AmplitudeToSends.Count > 0)
                                    {
                                        int Length = EpsonLCD.WriteAmplitudeData(m_AmplitudeToSends[0].GetBytes());
                                        EpsonLCD.SetAmplitudeTotalLength(Length);
                                    }
                                    else
                                    {
                                        //幅值发送完成，发送新增加节点信息，暂时为空
                                        byte[] NewNode = new byte[2];

                                        NewNode[0] = 0xFF;

                                        int Length = EpsonLCD.WriteNewAddNodeData(NewNode);

                                        EpsonLCD.SetNewAddNodeTotalLength(1);
                                    }
                                }
                                break;
                            case Ep6Cmd.EP6_CMD_T_WAVE_POINT_FINISH:
                                {
                                    //新添加节点数据发送完成命令
                                    FileLog("Ep6Cmd.EP6_CMD_T_WAVE_POINT_FINISH");
                                    Debug.WriteLine("Ep6Cmd.EP6_CMD_T_WAVE_POINT_FINISH 新添加节点数据发送完成命令");

                                    int Length = EpsonLCD.WriteWaveMappingData(ConstructWaveMapping());
                                    EpsonLCD.SetWaveMappingTotalLength(Length);
                                }
                                break;
                            case Ep6Cmd.EP6_CMD_T_WAVE_NAME_READY:
                                //预读波形名字就绪，开始读波形名字
                                {
                                    FileLog("Ep6Cmd.EP6_CMD_T_WAVE_NAME_READY");
                                    Debug.WriteLine("Ep6Cmd.EP6_CMD_T_WAVE_NAME_READY 预读波形命令下发完成");

                                    //取得波形名字总长度
                                    int TotalSize = EpsonLCD.GetWaveNameTotalLength();

                                    //取得波形名字数据
                                    ParseWaveName(EpsonLCD.GetWaveNameData(TotalSize), TotalSize);

                                    //预读映射表命令
                                    EpsonLCD.PrepareReadWaveMapping();
                                }
                                break;
                            case Ep6Cmd.EP6_CMD_T_WAVE_NAME_FINISH:
                                //波形名字发送完成命令
                                {
                                    FileLog("Ep6Cmd.EP6_CMD_T_WAVE_NAME_FINISH");
                                    Debug.WriteLine("Ep6Cmd.EP6_CMD_T_WAVE_NAME_FINISH 波形名字修改发送完成命令");
                                    MessageBox.Show(ResString.GetResString("Wave_Info_WaveNameUpdataFinish"), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ControlsEnable(true);
                                }
                                break;
                            case Ep6Cmd.EP6_CMD_T_WAVE_CHANNEL_FINISH:
                                //波形映射表完成命令
                                {
                                    FileLog("Ep6Cmd.EP6_CMD_T_WAVE_CHANNEL_FINISH");
                                    Debug.WriteLine("Ep6Cmd.EP6_CMD_T_WAVE_CHANNEL_FINISH 波形映射表完成命令");
                                    //获得波形映射总长度
                                    int TotalSize = EpsonLCD.GetWaveMappingTotalLength();
                                    if (0 == TotalSize)
                                    {
                                        MessageBox.Show(ResString.GetResString("Wave_Info_ReadWaveMapping0Length"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }

                                    //取得波形映射数据
                                    ParseWaveMapping(EpsonLCD.GetWaveMappingData(TotalSize), TotalSize);

                                    if (m_ImportMappingFlag == false)
                                    {
                                        //用户自定义
                                        CustomInitialize();
                                        //界面上所有控件显示
                                        this.Visible = true;
                                        //波形映射导入完成
                                        m_ImportMappingFlag = true;
                                    }
                                    else
                                    {
                                        MessageBox.Show(ResString.GetResString("Wave_Info_WaveMappingReadFinish"), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        ControlsEnable(true);
                                        m_ComboBox_Channel_SelectedIndexChanged(null, null);
                                        m_ComboBox_VSD_SelectedIndexChanged(null, null);
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case CoreMsgEnum.Status_Change:
                    {
                        int status = lParam.ToInt32();
                        if ((JetStatusEnum)status == JetStatusEnum.Ready && IsAutoWave && IsAutoPrint && !IsAutoStop)
                        {
                            IsAutoPrint = false;
                            AutoUpdateVave();
                        }
                        else if ((JetStatusEnum)status == JetStatusEnum.Busy && IsAutoWave && !IsAutoPrint && !IsAutoStop)
                        {
                            IsAutoPrint = true;
                        }
                        break;
                    }
            }
		}

		/// <summary>
		/// 通道选择
		/// </summary>
		private void m_ComboBox_Channel_SelectedIndexChanged(object sender, EventArgs e)
		{
            try
            {
                m_ChannelIndex = m_ComboBox_Channel.SelectedIndex;
                //通道变化默认VSD设置为1
                m_ComboBox_VSD.SelectedIndex = 0;
                m_ComboBox_Wave.SelectedIndex = Convert.ToInt32(WaveMappingList[m_ChannelIndex][m_ComboBox_VSD.SelectedIndex]);
            }
            catch
            { }
		}

		/// <summary>
		/// VSD选择
		/// </summary>
		private void m_ComboBox_VSD_SelectedIndexChanged(object sender, EventArgs e)
		{
            try
            {
                m_VSDSelectIndex = m_ComboBox_VSD.SelectedIndex;
                SetWaveAndNMInfo();

                //波形初始化设置
                m_ComboBox_Wave.Items.Clear();
                for (int nWaveName = 1; nWaveName <= m_Wave_Number; nWaveName++)
                {
                    string strWaveName = WaveNameList[m_VSDSelectIndex][nWaveName];
                    m_ComboBox_Wave.Items.Add(strWaveName);
                }

                //CoreInterface.WriteLogNormal(string.Format("m_ChannelIndex = {0}", m_ChannelIndex));
                //CoreInterface.WriteLogNormal(string.Format("m_VSDSelectIndex = {0}", m_VSDSelectIndex));
                //CoreInterface.WriteLogNormal(string.Format("WaveMappingList[m_ChannelIndex][m_VSDSelectIndex] = {0}", WaveMappingList[m_ChannelIndex][m_VSDSelectIndex]));
                int waveIdx = Convert.ToInt32(WaveMappingList[m_ChannelIndex][m_VSDSelectIndex]);
                if (waveIdx < m_ComboBox_Wave.Items.Count)
                {
                    m_ComboBox_Wave.SelectedIndex = waveIdx;
                }

            }
            catch { }
            //m_Button_Import.Enabled = false;
		}
		/// <summary>
		/// Wave和NM情报设定
		/// </summary>
		private void SetWaveAndNMInfo()
		{
			switch (m_VSDSelectIndex)
			{
				case 0:		//VSD1
					WaveSegment = 64;
					NMSegment = 9;
					break;
				case 1:		//VSD2
					WaveSegment = 64;
					NMSegment = 9;
					break;
				case 2:		//VSD3
					WaveSegment = 64;
					NMSegment = 9;
					break;
				case 3:		//VSD4
				default:
					break;
			}
            m_WaveData = new WaveData(WaveSegment, NMSegment, m_CycleValue);
			m_OriginalWaveSnips = new List<WaveDataSnip>(m_WaveData.WaveDatas);

			//初始化NM
			for (int n = 0; n < NMSegment; n++)
			{
				m_WaveData.NewWaveNMDatas[n].m_NM[0] = "01";
				m_WaveData.NewWaveNMDatas[n].m_NM[1] = "01";
				m_WaveData.NewWaveNMDatas[n].m_NM[2] = "01";
				m_WaveData.NewWaveNMDatas[n].m_NM[3] = "01";
				m_WaveData.NewWaveNMDatas[n].m_NM[4] = "01";
				m_WaveData.NewWaveNMDatas[n].m_NM[5] = "01";
				m_WaveData.NewWaveNMDatas[n].m_NM[6] = "01";
				m_WaveData.NewWaveNMDatas[n].m_NM[7] = "01";
				m_WaveData.NewWaveNMDatas[n].m_dCycle = 0;
				m_WaveData.NewWaveNMDatas[n].m_Cycle = 0;
			}
		}

		/// <summary>
		/// 波形名字选择变化函数
		/// </summary>
		private void m_ComboBox_Wave_SelectedIndexChanged(object sender, EventArgs e)
		{
			m_WaveSelectIndex = m_ComboBox_Wave.SelectedIndex;

            //m_TextBox_WaveNameDis.Text = WaveNameList[m_VSDSelectIndex][m_WaveSelectIndex+ 1];
		}

		/// <summary>
		/// NM周期更新
		/// </summary>
		public void NMDataUpdate()
		{
			if (m_ImportOverFlag == false) return;	//波形导入完了之前不能更新
			for (int n = 1; n < NMSegment; n++)
				m_WaveData.NewWaveNMDatas[n].m_dCycle = 0;
			for (int n = 0; n < m_WavePhase_Number; n++)
			{
				m_WaveData.NewWaveNMDatas[1].m_dCycle += m_WaveData.NewWaveDatas[n].m_dVoltage_Cycle;
				m_WaveData.NewWaveNMDatas[2].m_dCycle += m_WaveData.NewWaveDatas[1 * 8 + n].m_dVoltage_Cycle;
				m_WaveData.NewWaveNMDatas[3].m_dCycle += m_WaveData.NewWaveDatas[2 * 8 + n].m_dVoltage_Cycle;
				m_WaveData.NewWaveNMDatas[4].m_dCycle += m_WaveData.NewWaveDatas[3 * 8 + n].m_dVoltage_Cycle;
				m_WaveData.NewWaveNMDatas[5].m_dCycle += m_WaveData.NewWaveDatas[4 * 8 + n].m_dVoltage_Cycle;
				m_WaveData.NewWaveNMDatas[6].m_dCycle += m_WaveData.NewWaveDatas[5 * 8 + n].m_dVoltage_Cycle;
				m_WaveData.NewWaveNMDatas[7].m_dCycle += m_WaveData.NewWaveDatas[6 * 8 + n].m_dVoltage_Cycle;
				m_WaveData.NewWaveNMDatas[8].m_dCycle += m_WaveData.NewWaveDatas[7 * 8 + n].m_dVoltage_Cycle;
			}

			m_WaveData.NewWaveNMDatas[2].m_dCycle += m_WaveData.NewWaveNMDatas[1].m_dCycle;
			m_WaveData.NewWaveNMDatas[3].m_dCycle += m_WaveData.NewWaveNMDatas[2].m_dCycle;
			m_WaveData.NewWaveNMDatas[4].m_dCycle += m_WaveData.NewWaveNMDatas[3].m_dCycle;
			m_WaveData.NewWaveNMDatas[5].m_dCycle += m_WaveData.NewWaveNMDatas[4].m_dCycle;
			m_WaveData.NewWaveNMDatas[6].m_dCycle += m_WaveData.NewWaveNMDatas[5].m_dCycle;
			m_WaveData.NewWaveNMDatas[7].m_dCycle += m_WaveData.NewWaveNMDatas[6].m_dCycle;
			m_WaveData.NewWaveNMDatas[8].m_dCycle += m_WaveData.NewWaveNMDatas[7].m_dCycle;

            //周期修正
            for (int n = 0; n < m_WavePhase_Number; n++)
            {
                m_WaveData.NewWaveNMDatas[n + 1].m_dCycle -= m_WaveData.m_Wave2Correction[n];
            }
		}


		public void ConvertWaveCycle2NMCycle()
		{
			for (int n = 1; n < NMSegment; n++)
				m_WaveData.NewWaveNMDatas[n].m_Cycle = 0;
			for (int n = 0; n < m_WavePhase_Number; n++)
			{
				m_WaveData.NewWaveNMDatas[1].m_Cycle += m_WaveData.NewWaveDatas[n].m_Voltage_Cycle;
				m_WaveData.NewWaveNMDatas[2].m_Cycle += m_WaveData.NewWaveDatas[1 * 8 + n].m_Voltage_Cycle;
				m_WaveData.NewWaveNMDatas[3].m_Cycle += m_WaveData.NewWaveDatas[2 * 8 + n].m_Voltage_Cycle;
				m_WaveData.NewWaveNMDatas[4].m_Cycle += m_WaveData.NewWaveDatas[3 * 8 + n].m_Voltage_Cycle;
				m_WaveData.NewWaveNMDatas[5].m_Cycle += m_WaveData.NewWaveDatas[4 * 8 + n].m_Voltage_Cycle;
				m_WaveData.NewWaveNMDatas[6].m_Cycle += m_WaveData.NewWaveDatas[5 * 8 + n].m_Voltage_Cycle;
				m_WaveData.NewWaveNMDatas[7].m_Cycle += m_WaveData.NewWaveDatas[6 * 8 + n].m_Voltage_Cycle;
				m_WaveData.NewWaveNMDatas[8].m_Cycle += m_WaveData.NewWaveDatas[7 * 8 + n].m_Voltage_Cycle;
			}

			m_WaveData.NewWaveNMDatas[2].m_Cycle += m_WaveData.NewWaveNMDatas[1].m_Cycle;
			m_WaveData.NewWaveNMDatas[3].m_Cycle += m_WaveData.NewWaveNMDatas[2].m_Cycle;
			m_WaveData.NewWaveNMDatas[4].m_Cycle += m_WaveData.NewWaveNMDatas[3].m_Cycle;
			m_WaveData.NewWaveNMDatas[5].m_Cycle += m_WaveData.NewWaveNMDatas[4].m_Cycle;
			m_WaveData.NewWaveNMDatas[6].m_Cycle += m_WaveData.NewWaveNMDatas[5].m_Cycle;
			m_WaveData.NewWaveNMDatas[7].m_Cycle += m_WaveData.NewWaveNMDatas[6].m_Cycle;
			m_WaveData.NewWaveNMDatas[8].m_Cycle += m_WaveData.NewWaveNMDatas[7].m_Cycle;

            ////周期修正
            for (int n = 0; n < m_WavePhase_Number; n++)
            {
                m_WaveData.NewWaveNMDatas[n + 1].m_Cycle -= (int)(m_WaveData.m_Wave2Correction[n] * m_CycleValue + 0.5);
            }
		}


		/// <summary>
		/// 波形名字解析函数
		/// 波形名字buf结构是顺序存储的，一共4个vsd，每个vsd包含8个波形，即8个名字，每个名字最多占8个字节（有效7个字节 + 0位）共256个字节
		/// 例如：
		/// buf[0~7]:vsd1 波形1 名字
		/// buf[8~15]:vsd1 波形2 名字
		/// buf[64~71]:vsd2 波形1 名字
		/// buf[248~255]:vsd4 波形8 名字
		/// </summary>
		/// <param name="WaveData"></param>
		/// <returns></returns>
		private bool ParseWaveName(byte[] WaveNameData,int nTotalSize)
		{
			WaveNameList.Clear();
			for (int i = 0; i < m_VSD_Number; i++)
			{
				WaveNameList.Add(new List<string>());
			}

			for (int nVsd = 0; nVsd < m_VSD_Number; nVsd++)
			{
				for (int nWaveName = 0; nWaveName < m_Wave_Number; nWaveName++)
				{
					if( 0 == nWaveName )
						WaveNameList[nVsd].Add("VSD" + (nVsd + 1).ToString());

                    int beginIdx = nVsd * m_Wave_Number * m_WaveName_Length + nWaveName * m_WaveName_Length;

                    if (beginIdx + m_WaveName_Length > WaveNameData.Length)
                    {
                        //MessageBox.Show("超出波形名称数组,Len(WaveNameData)" + WaveNameData.Length.ToString());
                        break;
                    }

                    WaveNameList[nVsd].Add(Encoding.Default.GetString(WaveNameData, beginIdx, m_WaveName_Length));

					//WaveNameList[nVsd].Add(Encoding.Default.GetString(WaveNameData, nVsd * m_Wave_Number * m_WaveName_Length + nWaveName * m_WaveName_Length, m_WaveName_Length));
				}
			}
			return true;
		}

		/// <summary>
		/// 波形映射表解析函数
		/// 波形映射表结构，是一个短整型数组，长度为头板通道个数，按顺序存储通道对应的波形，短整型共16位
		/// 例如：
		/// 0~3位表示VSD1对应波形序号
		/// 4~7位表示VSD2对应波形序号
		/// 8~11位表示VSD3对应波形序号
		/// 12~15位表示VSD4对应波形序号
		/// </summary>
		/// <param name="WaveData"></param>
		/// <returns></returns>
		private bool ParseWaveMapping(byte[] WaveMappingData, int nTotalSize)
		{
			WaveMappingList.Clear();
			for (int i = 0; i < m_Channel_Number; i++)
			{
				WaveMappingList.Add(new List<string>());
			}

			int BufferIndex = 0;
			for (int nChannel = 0; nChannel < m_Channel_Number; nChannel++)
			{
                if (BufferIndex >= WaveMappingData.Length) break;

				//取得各通道数据
				WaveMappingList[nChannel].Add(Convert.ToString(WaveMappingData[BufferIndex] & 0x0f));			//VSD1
				WaveMappingList[nChannel].Add(Convert.ToString((WaveMappingData[BufferIndex] & 0xf0) >> 4));	//VSD2
				BufferIndex++;
				WaveMappingList[nChannel].Add(Convert.ToString(WaveMappingData[BufferIndex] & 0x0f));			//VSD3
				WaveMappingList[nChannel].Add(Convert.ToString((WaveMappingData[BufferIndex] & 0xf0) >> 4));	//VSD4
				BufferIndex++;
			}
			return true;
		}

		/// <summary>
		/// 波形映射数据构成
		/// </summary>
		/// <returns></returns>
		private byte[] ConstructWaveMapping()
		{
			byte[] byteChannel = new byte[m_Channel_Number * 2];	//每一个通道一个short
			int nIndex = 0;
			for (int nChannel = 0; nChannel < m_Channel_Number; nChannel++)
			{
				//取通道,VSD,波形序号
				if (nChannel == m_ChannelIndex)
				{
					switch (m_VSDSelectIndex)
					{
						case 0:			//VSD1
							WaveMappingList[nChannel][0] = m_WaveSelectIndex.ToString();
							break;
						case 1:			//VSD2
							WaveMappingList[nChannel][1] = m_WaveSelectIndex.ToString();
							break;
						case 2:			//VSD3
							WaveMappingList[nChannel][2] = m_WaveSelectIndex.ToString();
							break;
						case 3:			//VSD4
							WaveMappingList[nChannel][3] = m_WaveSelectIndex.ToString();
							break;
					}
				}

                if (WaveMappingList[nChannel].Count == 0) break;

				//VDS1和VSD2
				byteChannel[nIndex++] = Convert.ToByte(Convert.ToByte(WaveMappingList[nChannel][0]) | (Convert.ToByte(WaveMappingList[nChannel][1]) << 4));
				//VSD3和VSD4
				byteChannel[nIndex++] = Convert.ToByte(Convert.ToByte(WaveMappingList[nChannel][2]) | (Convert.ToByte(WaveMappingList[nChannel][3]) << 4));
			}
			return byteChannel;
		}

		/// <summary>
		/// 通道名称取得解析
		/// 通道名字最多19个字节 + 0位 共20字节通道序号（1开始）
		/// 例如 
		/// index == 1 命令读取的第1个通道的名字，返回20个字节
		/// </summary>
		/// <param name="ChannelNameData"></param>
		/// <param name="nTotalSize"></param>
		/// <returns></returns>
		private bool GetChannelName()
		{
			FileLog("Get Channel Name");
			//读取通道名称
			m_Channel_Number = EpsonLCD.GetChannelNameTotalLength();
            //m_Channel_Number = 8;
			for (int nIndex = 1; nIndex <= m_Channel_Number; nIndex++)
			{
				ChannelNameList.Add(Encoding.Default.GetString(EpsonLCD.GetChannelNameData((ushort)nIndex)));
			}
			return true;
		}

        public void CheckWaveBeginEnd()
        {
            if (m_WaveData == null)
                return;

            bool flagBegin = false;
            bool flagEnd = false;
            bool checkBegin = true;
            bool checkEnd = true;
            string strMessageInfo = "";

            for (int nmIdx = 0; nmIdx < 4; nmIdx++)
            {
                flagBegin = false;
                flagEnd = false;
                checkBegin = false;
                checkEnd = false;

                for (int i = 0; i < m_WaveData.NewWaveNMDatas.Count-1; i++)
                {
                    if (m_WaveData.NewWaveNMDatas[i].m_NM[nmIdx] == "00")
                    {
                        if (flagBegin == false && checkBegin == false)
                        {
                            flagBegin = true;
                        }

                        if (m_WaveData.NewWaveNMDatas[i + 1].m_NM[nmIdx] != "00")
                        {
                            flagEnd = true;
                        }
                    }

                    if (flagBegin)//检查开始
                    {
                        checkBegin = true;
                        flagBegin = false;
                        double beginCycle = 0;
                        double beginVoltage = 0;
                        for (int j = i * 8; j < (i + 1) * 8; j++)
                        {
                            if (m_WaveData.NewWaveDatas[j].m_dVoltage_Cycle > 0)
                            {
                                if (beginVoltage == 0)
                                {
                                    beginVoltage = m_WaveData.NewWaveDatas[j].m_VoltageStart_Percentage;
                                }

                                if (beginVoltage != m_WaveData.NewWaveDatas[j].m_VoltageEnd_Percentage)
                                {
                                    if (beginCycle < 0.5)
                                    {
                                        string info = ResString.GetResString("Wave_promptStartCycle");
                                        if (info == "")
                                        {
                                            info = "The wave start platform less than 0.5 us of NM" + nmIdx.ToString();
                                        }
                                        else
                                        {
                                            info = string.Format(info, nmIdx.ToString());
                                        }
                                        strMessageInfo += info + "\r\n";
                                    }
                                    break;
                                }
                                else
                                {
                                    beginCycle = m_WaveData.NewWaveDatas[j].m_dVoltage_Cycle;
                                }
                            }
                        }

                    }

                    if (flagEnd)//检查结束
                    {
                        checkBegin = false;
                        flagEnd = false;
                        double beginCycle = 0;
                        double endVoltage = 0;

                        for (int j = (i + 1) * 8 - 1; j >= i * 8; j--)
                        {
                            if (m_WaveData.NewWaveDatas[j].m_dVoltage_Cycle > 0)
                            {
                                if (endVoltage == 0)
                                {
                                    endVoltage = m_WaveData.NewWaveDatas[j].m_VoltageEnd_Percentage;
                                }

                                if (endVoltage != m_WaveData.NewWaveDatas[j].m_VoltageStart_Percentage)
                                {
                                    if (beginCycle < 2)
                                    {
                                        string info = ResString.GetResString("Wave_promptEndCycle");
                                        if (info == "")
                                        {
                                            info = "The wave end platform less than 2 us of NM" + nmIdx.ToString();
                                        }
                                        else
                                        {
                                            info = string.Format(info, nmIdx.ToString());
                                        }
                                        strMessageInfo += info + "\r\n";
                                    }
                                    break;
                                }
                                else
                                {
                                    beginCycle += m_WaveData.NewWaveDatas[j].m_dVoltage_Cycle;
                                }
                            }
                        }

                    }

                }

            }

            if (strMessageInfo != "")
            {
                MessageBox.Show(strMessageInfo, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

		/// <summary>
		/// 波形映射表读取
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_Button_ReadWaveMapping_Click(object sender, EventArgs e)
		{
			FileLog("Wave mapping Read");
			ControlsEnable(false);	//按钮无效化
			EpsonLCD.PrepareReadWaveMapping();
		}

		/// <summary>
		/// 波形映射表下发
		/// </summary>
		private void m_Button_WriteWaveMapping_Click(object sender, EventArgs e)
		{
			FileLog("Wave mapping Write");
			ControlsEnable(false);	//按钮无效化
			int Length = EpsonLCD.WriteWaveMappingData(ConstructWaveMapping());
			EpsonLCD.SetWaveMappingTotalLength(Length);
            MessageBox.Show(ResString.GetResString("Wave_Info_WaveMappingSengFinish"), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
			ControlsEnable(true);		//按钮有效化
		}

		/// <summary>
		/// 波形下发 
		/// </summary>
		private void m_Button_Update_Click(object sender, EventArgs e)
		{
			FileLog("Write wave to board");
			ControlsEnable(false);			//按钮无效化
			WaveDataEx WaveData = m_Waves[0];

			//遍历确定各波所含的段数
			ushort[] usCountArray = new ushort[]{0,0,0,0,0,0,0,0};
			for (int nIndex = 0; nIndex < m_WaveData.NewWaveDatas.Count; nIndex++)
			{
				{
					m_WaveData.NewWaveDatas[nIndex].m_VoltageStart = m_WaveData.G4_Voltage2DigitalValue((float)(m_WaveData.NewWaveDatas[0].m_StandardVoltage * m_WaveData.NewWaveDatas[nIndex].m_VoltageStart_Percentage / 100));
					m_WaveData.NewWaveDatas[nIndex].m_VoltageEnd = m_WaveData.G4_Voltage2DigitalValue((float)(m_WaveData.NewWaveDatas[0].m_StandardVoltage * m_WaveData.NewWaveDatas[nIndex].m_VoltageEnd_Percentage / 100));
				}
				if (m_WaveData.NewWaveDatas[nIndex].m_dVoltage_Cycle != 0)
				{
					usCountArray[nIndex / 8] += 1;
					m_WaveData.NewWaveDatas[nIndex].m_Voltage_Cycle = (ushort)(m_WaveData.NewWaveDatas[nIndex].m_dVoltage_Cycle * m_CycleValue + 0.5);
				}
				else 
				{
					m_WaveData.NewWaveDatas[nIndex].m_Voltage_Cycle = 0;
				}
			}

			//设定总段数，总周期数，开始周期不变
			//总段数
			ushort usTotalPhase = 0;
			for (int nIndex = 0; nIndex < usCountArray.Length; nIndex++)
			{
				usTotalPhase += usCountArray[nIndex]; 
			}
			byte[] byTotalPhase = BitConverter.GetBytes(usTotalPhase);
			if (m_WaveData.WaveSummary.ContainsKey("Summary1"))
			{
				m_WaveData.WaveSummary["Summary1"] = byTotalPhase[0];
			}
			else
			{
				m_WaveData.WaveSummary.Add("Summary1", byTotalPhase[0]);
			}

			ConvertWaveCycle2NMCycle();

			//总周期数
			ushort usTotalCycle = (ushort)(m_WaveData.NewWaveNMDatas[m_WaveData.NewWaveNMDatas.Count - 1].m_Cycle |0x8000);
			byte[] byTotalCycle = BitConverter.GetBytes(usTotalCycle);
			if (m_WaveData.WaveSummary.ContainsKey("Summary3"))
			{
				m_WaveData.WaveSummary["Summary3"] = byTotalCycle[0];
			}
			else
			{
				m_WaveData.WaveSummary.Add("Summary3", byTotalCycle[0]);
			}

			if (m_WaveData.WaveSummary.ContainsKey("Summary4"))
			{
				m_WaveData.WaveSummary["Summary4"] = byTotalCycle[1];
			}
			else
			{
				m_WaveData.WaveSummary.Add("Summary4", byTotalCycle[1]);
			}

			//int Length = EpsonLCD.WriteWaveData(WaveData.Wave.NewGetBytes(m_ImportPreFlag,m_WriteWaveFlag, m_VSDSelectIndex + 1, m_WaveSelectIndex + 1, usCountArray, usTotalPhase));
            int Length = EpsonLCD.WriteWaveData(m_WaveData.NewGetBytes(m_ImportPreFlag, m_WriteWaveFlag, m_VSDSelectIndex + 1, m_WaveSelectIndex + 1, usCountArray, usTotalPhase));
			EpsonLCD.SetWaveDataTotalLength(Length);
            //m_CheckBox_WriteWave.Checked = false;	//覆盖波形去除勾选
            //btnIFSSave.Enabled = true;
		}

		/// <summary>
		/// 运行日志记录
		/// </summary>
		/// <param name="ex"></param>
		private void FileLog(string strLog)
		{
			string path = System.Windows.Forms.Application.StartupPath + "\\WaveFormTool.log";
			// This text is added only once to the file.
			if (!File.Exists(path))
			{
				// Create a file to write to.
				using (StreamWriter sw = File.CreateText(path))
				{
					sw.WriteLine("WaveFormTool Start!");
				}
			}
			using (StreamWriter sw = File.AppendText(path))
			{
				sw.WriteLine("Time:" + DateTime.Now.ToString() + "    " + strLog);
			}
		}

		/// <summary>
		/// 防止按钮两次按下处理
		/// </summary>
		/// <param name="bEnable"></param>
		private void ControlsEnable(bool bEnable)
		{
			this.Enabled = bEnable;
			if (bEnable == true)
			{
                //m_Button_Update.Enabled = false;
                //m_Button_DotCheck.Enabled = false;
			}
		}
     


        private void AutoUpdateVave()
        {
            m_Waves.Clear();

            if (!m_WaveData.Validation(m_VSDSelectIndex + 1))
            {
                MessageBox.Show(ResString.GetResString("Wave_Info_NumberMoreThanMaxOfVSD"));
                return;
            }

            WaveDataEx WaveToSend = new WaveDataEx(m_WaveData);

            {
                AmplitudeHeadParameter Amplitude = new AmplitudeHeadParameter(Convert.ToInt32(0));
                WaveToSend.AddAmplitude(Amplitude);
            }

            {
                m_WaveData.WaveDatas = m_OriginalWaveSnips;
                AmplitudeVoltageChange Amplitude = new AmplitudeVoltageChange(m_OriginalWaveSnips, m_WaveData.WaveDatas);
                WaveToSend.AddAmplitude(Amplitude);
            }
            m_Waves.Add(WaveToSend);

            System.Threading.Thread.Sleep(500);

            if (IsVoltage)
            {
                m_WaveData.NewWaveDatas[0].m_StandardVoltage = Math.Round(currValue, 1);
            }
            else
            {
                m_WaveData.NewWaveDatas[indexN].m_dVoltage_Cycle = Math.Round(currValue, 1);
            }

            currStr = "(V)" + m_WaveData.NewWaveDatas[0].m_StandardVoltage.ToString() + " "
                + "(W)" + (waveIdx+1).ToString() + " "
                + "(1)" + m_WaveData.NewWaveDatas[waveIdx * m_WavePhase_Number].m_dVoltage_Cycle.ToString() + " "
                + "(2)" + m_WaveData.NewWaveDatas[waveIdx * m_WavePhase_Number + 1].m_dVoltage_Cycle.ToString() + " "
                + "(3)" + m_WaveData.NewWaveDatas[waveIdx * m_WavePhase_Number + 2].m_dVoltage_Cycle.ToString() + " "
                + "(4)" + m_WaveData.NewWaveDatas[waveIdx * m_WavePhase_Number + 3].m_dVoltage_Cycle.ToString() + " ";

            currValue = currValue + step;

            m_Button_Update_Click(null, null);
        }

        /// <summary>
        /// 初始化界面列表
        /// </summary>
        public void InitUiList()
        {
            //头版类型取得
            GetHBType();

            //通道名字取得
            GetChannelName();
            try
            {
                //波形名字预读命令
                EpsonLCD.PrepareReadWaveName();
            }
            catch
            {
                MessageBox.Show(ResString.GetResString("Wave_ReadWaveNameFailed"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FileLog("Prepare Read Wave Name");

        }
	}
}
