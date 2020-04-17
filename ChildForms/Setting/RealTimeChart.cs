using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.InteropServices;

namespace BYHXPrinterManager.Setting
{
    public partial class RealTimeChart : Form
    {
        private SPrinterProperty m_rsPrinterPropery;//Only read for color order

        private int Voltage_LR_Num = 0;
        private int m_HeadNum = 0;
        private int m_TempNum = 0;
        private int m_HeadVoltageNum = 0;
        private int m_HeadPulseWidthNum = 0;
        private int m_InkBaoTempNum = 0;
        private int m_InkVolageNum = 0;
        private int m_VolageOffsetNum = 0;

        private float ADJUST_VOL_PER_HEAD = 8;
        private float VOL_COUNT_PER_HEAD = 1;
        private float Plus_COUNT_PER_HEAD = 1;
        //private const int VOL_COUNT_PER_POLARIS_HEAD = 2;
        //private const int VOL_COUNT_PER_SG1024_GRAY = 3;
        //private const int VOL_COUNT_PER_KM1024I_GRAY = 6;
        private byte m_StartHeadIndex = 0;
        private byte[] m_pMap;
        private bool m_bSpectra = false;
        private bool m_bKonic512 = false;
        private bool m_bKonic1800i = false;
        private bool m_bKonicM600 = false;
        private bool m_bSupportHeadHeat = false;
        private bool m_bXaar382 = false;
        private bool m_bPolaris = false;
        /// <summary>
        /// 是否是Polaris_GZ.
        /// </summary>
        private bool m_bExcept = false;
        private bool m_Konic512_1head2color = false;
        private bool m_bPolaris_V5_8 = false;
        private bool m_bPolaris_V5_8_Emerald = false;
        private bool m_bPolaris_V7_16 = false;
        private bool m_bSpectra_SG1024_Gray = false;
        private bool m_bKonic1024i_Gray = false;
        private bool m_bPolaris_V7_16_Emerald = false;
        private bool m_bPolaris_V7_16_Polaris = false;
        private bool m_bRicoHead = false;
        private bool m_bXaar501 = false;
        private bool m_bKyocera = false;
        private bool m_bGma990 = false;
        /// <summary>
        /// 是否为垂直排列
        /// </summary>
        private bool m_bVerArrangement = false;
        private bool m_bMirrorArrangement = false;
        private bool m_b1head2color = false;
        private bool isDirty = false;


        private List<string> HeadTextList = new List<string>();
        RealTimeDataOneHead[] infoList = new RealTimeDataOneHead[4];

        public RealTimeChart()
        {
            InitializeComponent();
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
             m_rsPrinterPropery = sp;
#if true
            uint uiHtype = 0;
            CoreInterface.GetUIHeadType(ref uiHtype);
            m_bKonic512 = (uiHtype & 0x01) != 0;
            m_bXaar382 = (uiHtype & 0x02) != 0;
            m_bSpectra = (uiHtype & 0x04) != 0;
            m_bPolaris = (uiHtype & 0x08) != 0;
            m_bPolaris_V5_8 = (uiHtype & 0x10) != 0; ;
            m_bExcept = (uiHtype & 0x20) != 0;
            m_bPolaris_V7_16 = (uiHtype & 0x40) != 0;
            m_bKonic1024i_Gray = (uiHtype & 0x80) != 0;
            m_bSpectra_SG1024_Gray = (uiHtype & 0x100) != 0;
            m_bXaar501 = (uiHtype & 0x200) != 0;//pan dan Xaar501?
#else
			m_bSpectra = SPrinterProperty.IsSpectra(sp.ePrinterHead);
			m_bKonic512 = SPrinterProperty.IsKonica512 (sp.ePrinterHead);
			m_bXaar382 = (sp.ePrinterHead == PrinterHeadEnum.Xaar_Proton382_35pl||sp.ePrinterHead == PrinterHeadEnum.Xaar_Proton382_60pl);			
			m_bPolaris = SPrinterProperty.IsPolaris(sp.ePrinterHead);
			//			m_bPolaris_V5_8 = SPrinterProperty.IsPolaris(sp.ePrinterHead);
			SBoardInfo sBoardInfo = new SBoardInfo();
			if( CoreInterface.GetBoardInfo(0,ref sBoardInfo) != 0)
			{
				m_bExcept = (sBoardInfo.m_nBoardManufatureID == 0xB || sBoardInfo.m_nBoardManufatureID == 0x8b);
			}
#endif

            m_bVerArrangement = ((sp.bSupportBit1 & 2) != 0);
            m_bMirrorArrangement = m_rsPrinterPropery.IsMirrorArrangement();
            m_b1head2color = (m_rsPrinterPropery.nOneHeadDivider == 2);
            m_Konic512_1head2color = m_b1head2color && m_bKonic512;
            m_bPolaris_V7_16_Emerald = m_bPolaris_V7_16 &&
                (sp.ePrinterHead == PrinterHeadEnum.Spectra_Emerald_10pl
                                || sp.ePrinterHead == PrinterHeadEnum.Spectra_Emerald_30pl);
            m_bPolaris_V7_16_Polaris = m_bPolaris_V7_16 &&
                (sp.ePrinterHead == PrinterHeadEnum.Spectra_PolarisColor4_15pl
                                || sp.ePrinterHead == PrinterHeadEnum.Spectra_PolarisColor4_35pl
                                || sp.ePrinterHead == PrinterHeadEnum.Spectra_PolarisColor4_80pl
                                || sp.ePrinterHead == PrinterHeadEnum.Spectra_Polaris_15pl
                                || sp.ePrinterHead == PrinterHeadEnum.Spectra_Polaris_35pl
                                || sp.ePrinterHead == PrinterHeadEnum.Spectra_Polaris_80pl);
            m_bPolaris_V5_8_Emerald = m_bPolaris_V5_8 &&
                (sp.ePrinterHead == PrinterHeadEnum.Spectra_Emerald_10pl
                || sp.ePrinterHead == PrinterHeadEnum.Spectra_Emerald_30pl);
            m_bRicoHead = sp.ePrinterHead == PrinterHeadEnum.RICOH_GEN4_7pl
                || sp.ePrinterHead == PrinterHeadEnum.RICOH_GEN4L_15pl
                || sp.ePrinterHead == PrinterHeadEnum.RICOH_GEN4P_7pl;
            m_bKyocera = SPrinterProperty.IsKyocera(sp.ePrinterHead);
            m_bKonic1800i = SPrinterProperty.IsKonic1800i(sp.ePrinterHead);
            m_bKonicM600 = sp.ePrinterHead == PrinterHeadEnum.Konica_M600SH_2C;
            m_bGma990 = sp.ePrinterHead == PrinterHeadEnum.Fujifilm_GMA9905300_5pl || sp.ePrinterHead == PrinterHeadEnum.Fujifilm_GMA3305300_5pl;

            m_HeadNum = NewLayoutFun.GetHeadNum();

            if (m_bKonic1024i_Gray)
            {
                m_TempNum = m_HeadNum/2;
            }
            else if (m_bSpectra_SG1024_Gray)
            {
                if (!CoreInterface.IsSG1024_AS_8_HEAD())
                {
                    if (m_b1head2color)
                    {
                        m_TempNum /= 2;
                    }
                }
            }

            m_StartHeadIndex = 0;

            //int imax = Math.Max(m_HeadNum, m_TempNum);
            m_pMap = new byte[m_HeadNum];
            for (int i = 0; i < m_HeadNum; i++)
            {
                m_pMap[i] = (byte)i;
            }

            if (GetRealTimeInfo2())
            {
                InitHeadTextList();
                InitChart();
                InitCheckedListBox();
            }
        }
        public void OnPrinterSettingChange(SPrinterSetting ss)
        {

        }
        public void OnPreferenceChange(UIPreference up)
        {

        }

        private bool GetRealTimeInfo2()
        {
            bool ret = false;
            infoList = new RealTimeDataOneHead[m_HeadNum];
            uint rmask = 0;
            rmask |= 1 << (int)EnumVoltageTemp.TemperatureCur;
            int headNum = 0;

            int size = Marshal.SizeOf(typeof(RealTimeDataOneHead));
            byte[] InfosBytes = new byte[m_HeadNum * size];

            IntPtr infosIntptr = Marshal.AllocHGlobal(size * m_HeadNum);

            try
            {
                if (CoreInterface.GetRealTimeInfo2(infosIntptr, ref headNum, rmask) != 0)
                {
                    for (int i = 0; i < m_HeadNum; i++)
                    {
                        IntPtr ptr = (IntPtr)(infosIntptr.ToInt64() + i * size);
                        infoList[i] = (RealTimeDataOneHead)Marshal.PtrToStructure(ptr, typeof(RealTimeDataOneHead));
                    }

                }
                else
                {
                    MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.GetRealTimeInfoFail), ResString.GetProductName());
                    return false;
                }
                ret = true;
            }
            finally
            {
                Marshal.FreeHGlobal(infosIntptr);
            }

            return ret;
        }

        private void InitHeadTextList()
        {
            int j = 0;
            for (int i = 0; i < m_HeadNum; i++)
            {
                string strHeadName = System.Text.Encoding.Default.GetString(infoList[i].sName).Trim().Replace("\0", "").Replace(" ", "").Replace("", "").Replace("?", "");

                string label;
                if (m_bKonic1024i_Gray || m_bKonic1800i)
                {
                    //label = string.Format("{0}{1} ({2})", i/ 2, i % 2 == 0 ? "R" : "L", strHeadName);
                    if (i % 2 != 0) continue;
                    label = string.Format("{0}({1})", i / 2, strHeadName);
                }
                else
                {
                    //if (m_b1head2color)
                    //{
                    //    if (m_bVerArrangement)
                    //    {
                    //        label = string.Format("{0} ({1}/{2})", (i + m_StartHeadIndex), m_rsPrinterPropery.Get_ColorIndex(j + m_StartHeadIndex), m_rsPrinterPropery.Get_ColorIndex(j + 1 + m_StartHeadIndex));
                    //        j += 2;
                    //    }
                    //    else
                    //    {
                    //        if (m_bMirrorArrangement)
                    //        {
                    //            int cIndex1 = j + m_StartHeadIndex;
                    //            int cIndex2 = j + 1 + m_StartHeadIndex;
                    //            //if ((j / m_rsPrinterPropery.nColorNum) % 2 != 0)
                    //            //{
                    //            //    cIndex1 = m_rsPrinterPropery.nColorNum - cIndex1 % m_rsPrinterPropery.nColorNum - 1;
                    //            //    cIndex2 = m_rsPrinterPropery.nColorNum - cIndex2 % m_rsPrinterPropery.nColorNum - 1;
                    //            //}
                    //            label = string.Format("{0} ({1}/{2})", (i + m_StartHeadIndex), m_rsPrinterPropery.Get_ColorIndex(cIndex1), m_rsPrinterPropery.Get_ColorIndex(cIndex2));
                    //            j += 2;
                    //        }
                    //        else
                    //        {
                    //            label = string.Format("{0} ({1}/{2})", (i + m_StartHeadIndex), m_rsPrinterPropery.Get_ColorIndex(j + m_StartHeadIndex), m_rsPrinterPropery.Get_ColorIndex(j + 1 + m_StartHeadIndex));
                    //            j += 2;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    label = (i + m_StartHeadIndex).ToString()
                    //        + "  (" + m_rsPrinterPropery.Get_ColorIndex(i + m_StartHeadIndex) + ")";
                    //}

                    label = "Color" + (i + 1).ToString() + ":" + strHeadName;
                }
                HeadTextList.Add(label);
            }
        }

        private void InitChart()
        {
            chart_Temperature.Series.Clear();
            for (int i = 0; i < m_TempNum; i++)
            {
                Series series = new Series(HeadTextList[i])
                {
                    ChartArea = "ChartArea1",
                    ChartType = SeriesChartType.Spline,
                    BorderWidth = 2,
                };
                chart_Temperature.Series.Add(series);
            }
        }

        private void InitCheckedListBox()
        {
            for (int i = 0; i < m_TempNum; i++)
            {
                int index = checkedListBox1.Items.Add(HeadTextList[i]);
                checkedListBox1.SetItemChecked(index, true);
            }
        }
        private void RealTimeChart_Load(object sender, EventArgs e)
        {
            this.Text = ResString.GetProductName();
        }

        private void timer_RealTime_Tick(object sender, EventArgs e)
        {
            OnRealTimeChange();
        }

        private void OnRealTimeChange()
        {
            //SRealTimeCurrentInfo info = new SRealTimeCurrentInfo();
            //uint rmask = 0;
            //rmask |= 1 << (int)EnumVoltageTemp.TemperatureCur;
            if (GetRealTimeInfo2())
            {
                if (_infos.Count >= chart_Temperature.ChartAreas[0].AxisX.Maximum)
                {
                    _infos.RemoveAt(0);
                }
                _infos.Add(infoList);
                ChartTemperatureReload();
            }
            else
            {
                timer_RealTime.Enabled = false;
                MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.GetRealTimeInfoFail), ResString.GetProductName());
            }
        }
        private readonly List<RealTimeDataOneHead[]> _infos = new List<RealTimeDataOneHead[]>();

        private void ChartTemperatureReload()
        {
            chart_Temperature.Series.Clear();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemCheckState(i) == CheckState.Checked)
                {
                    Series series = new Series(HeadTextList[i])
                    {
                        ChartArea = "ChartArea1",
                        ChartType = SeriesChartType.Spline,
                        BorderWidth = 2,
                    };
                    for (int j = 0; j < _infos.Count; j++)
                    {
                        RealTimeDataOneHead[] temp = _infos[j];

                        series.Points.AddY(temp[i].cTemperatureCur[0]);
                    }
                    chart_Temperature.Series.Add(series);
                }
            }
        }

        private void RealTimeChart_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer_RealTime.Enabled = false;
        }
    }
}
