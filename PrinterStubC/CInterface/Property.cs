/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using PrinterStubC.Common;

//using DemoTreeView;
namespace BYHXPrinterManager
{
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct SPrintAmendProperty
    {
        public uint bUseful;//0x19ED5500(有效)
        public uint uSize;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] pRipColorOrder;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 38)]
        public byte[] pRetainWrod;
        public SPrintAmendProperty(bool bInit)
        {
            if (bInit)
            {
                bUseful = CoreConst.BeEnableConstMark;// 0x19ED5500;
            }
            else
            {
                bUseful = 0;
            }
            uSize = 44;
            //uPrintSense = 0;
            //uRasterSense = 0;
            pRipColorOrder = new byte[16];
            pRetainWrod = new byte[38];
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct USER_SET_INFORMATION 
    {
        /// <summary>
        /// //4byte,设置标志
        /// </summary>
        public uint  Flag;   
        /// <summary>
        /// 4byte,左加速距离
        /// </summary>
        public uint  AccSpaceL;  
        /// <summary>
        /// 4byte,右加速距离
        /// </summary>
        public uint AccSpaceR;  
        /// <summary>
        /// byte,平台距离x
        /// </summary>
        public uint  FlatSpace;
        /// <summary>
        /// 光栅分辨率
        /// </summary>
        public uint  uRasterSense ;

        public ushort PumpType;                //2byte "BM"
       
        /// <summary>
        /// 头板个数
        /// </summary>
        public byte HeadBoardNum;
		/// <summary>
		/// 使用指定的端口则相应位置1
		/// </summary>
        public byte HeadPortMask;
        /// <summary>
        /// 打印分辨率
        /// </summary>
        public uint uPrintSense;
        /// <summary>
        /// 对于scorpion和aojet机器,当x轴回原点时,z轴回到指定的位置
        /// </summary>
        public uint zDefault; //

        /// <summary>
        ///拓扑类型。
        ///00：表示1条光纤连接到一个扩展板；
        ///01：表示2条光纤连接到同一个扩展板；
        ///10：表示2条光纤连接到不同扩展板。
        /// </summary>
        public byte Topology;

        /// <summary>
        /// 转接板1头板数据MAP, 光纤1传输低4位对应头板数据，光纤2传输高4位对应头板数据
        /// </summary>
        public byte B1HbDataMap;

        /// <summary>
        /// 转接板2头板数据MAP, 光纤1传输低4位对应头板数据，光纤2传输高4位对应头板数据
        /// </summary>
        public byte B2HbDataMap;

        /// <summary>
        /// 头板数据宽度，0为1byte,1为2byte,依次累加。
        /// </summary>
        public byte HeadBoardDataByteWidth  ;
        [MarshalAs(UnmanagedType.I1)]
        public bool bSupportZendPointSensor; //是否支持z重点传感器

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] rev1;

        /// <summary>
        /// Z轴最大行程
        /// </summary>
        public uint zMaxRoute;

        public float yMaxLen; //Y方向最大行程

        public float fVOffset;

        public ushort SamplePoint;

        public byte motorDebug;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public byte[] rev;
        public USER_SET_INFORMATION(bool bInit)
        {
            if (bInit)
            {
                Flag = CoreConst.BeEnableConstMark;// 0x19ED5500;
            }
            else
            {
                Flag = 0;
            }
            AccSpaceL = 0;
            AccSpaceR = 0;
            FlatSpace = 0;
            uRasterSense = 720;
            uPrintSense = 720;
            rev = new byte[7];
            PumpType = CoreConst.ENABLE_CONTINU_PUMP_INK;
            //Pack = 0;
            zDefault = 0;
            HeadPortMask = 0xff;
            HeadBoardNum = 0;
            Topology = 0;
            B1HbDataMap = 0;
            B2HbDataMap = 0;
            HeadBoardDataByteWidth = 0;
            rev1 = new byte[3];
            zMaxRoute = 0;
            bSupportZendPointSensor = false;
            yMaxLen = 0;
            fVOffset = 0;
            SamplePoint = 0;
            motorDebug = 0;
        }
    };



    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct SPrinterProperty
    {
        public uint bSupportBit1; //1: HardPanel 2: Head1Color2Y 
        public float fQepPerInchY;
        [MarshalAs(UnmanagedType.I1)] public bool bSupportFeather;
        [MarshalAs(UnmanagedType.I1)] public bool bSupportHeadHeat;
        /// <summary>
        /// 是否为卷板俩用机型
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] public bool bSupportDoubleMachine;
        [MarshalAs(UnmanagedType.I1)] public bool bSupportYEncoder;
        [MarshalAs(UnmanagedType.I1)] public bool bSupportWhiteInkYoffset;
        public byte nWhiteInkNum;
        public byte nOneHeadDivider;
        public byte nCarriageReturnNum;


        //Common Printer Property	
        [MarshalAs(UnmanagedType.I4)] public PrinterHeadEnum ePrinterHead;
        [MarshalAs(UnmanagedType.I4)] public SingleCleanEnum eSingleClean;
        public byte nColorNum;
        //[Obsolete("使用nHeadNum属性代替")]//Obsolete特性标记会导致不序列化
        public byte nHeadNumOld;
        public byte nHeadNumPerColor;
        public byte nHeadNumPerGroupY;
        public byte nHeadNumPerRow;
        public byte nHeadHeightNum; //0x80 表示双面喷
        public byte nElectricNum;
        public byte nResNum;
        public byte nMediaType;
        public byte nPassListNum;

        //One bit Property
        public byte nRev1;
        [MarshalAs(UnmanagedType.I1)] public bool bSupportAutoClean;
        [MarshalAs(UnmanagedType.I1)] public bool bSupportPaperSensor;
        [MarshalAs(UnmanagedType.I1)] public bool bSupportWhiteInk;
        [MarshalAs(UnmanagedType.I1)] public bool bSupportUV;
        [MarshalAs(UnmanagedType.I1)] public bool bSupportHandFlash;
        public byte nDspInfo;
        [MarshalAs(UnmanagedType.I1)] public bool bSupportMilling;
        [MarshalAs(UnmanagedType.I1)] public bool bSupportZMotion;

        [MarshalAs(UnmanagedType.I1)] public bool bHeadInLeft;
        [MarshalAs(UnmanagedType.I1)] public bool bPowerOnRenewProperty;
        [MarshalAs(UnmanagedType.I1)] public bool bHeadElectricReverse;
        [MarshalAs(UnmanagedType.I1)] public bool bHeadSerialReverse;
        [MarshalAs(UnmanagedType.I1)] public bool bInternalMap;
        [MarshalAs(UnmanagedType.I1)] public bool bElectricMap;

        //Clip Setting  
        public float fMaxPaperWidth;
        public float fMaxPaperHeight;
        //Arrange as mechaical 
        public float fHeadAngle;
        public float fHeadYSpace;
        public float fHeadXColorSpace;
        public float fHeadXGroupSpace;

        public int nResX;
        public int nResY;
        public int nStepPerHead;

        ///????????????????? this value should put some place that user can Setting, this include Clean Pos 
        public float fPulsePerInchX;

        /// <summary>
        /// 不应直接使用此值,请使用fPulsePerInchY属性替代
        /// </summary>
        //[Obsolete("使用属性fPulsePerInchY代替")] //Obsolete特性标记会导致不序列化
        public float _fPulsePerInchY;

        public float fPulsePerInchY
        {
            get
            {
#if USE_PROPERTY
                return CoreInterface.GetfPulsePerInchY(1);
#else
                return CoreInterface.GetfPulsePerInchY(0);
#endif
            }
        }

        public float fPulsePerInchZ;


		public uint    Version;
        public int SettingOnOff;
        //struct user_setting_on_off{
        //    unsigned int	LoadMap : 1;
        //    unsigned int	LoadXOffset : 1;
        //    unsigned int	LoadYOffset : 1;
        //    unsigned int	rev : 29;
        //}SettingOnOff;
        //struct machine_type{
        public uint machine_type_0;//			unsigned int    StepOneBand			: 1;
            //unsigned int    SmallFlatfrom       : 1;
            //unsigned int    JobQuickRestart : 1;
            //unsigned int    SendJobNoWait : 1;
            //unsigned int	rev					: 28;
        public uint machine_type_1;//
        public short  nElectricNumNew;
		public byte   nHbNum;
        public byte SSysterm;
		public ushort  nHeadNumNew;
        public ushort HeadResY;
        /// <summary>
        /// 第四轴的编码器分辨率
        /// </summary>
        public float fPulsePerInchAxis4;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
		public byte[]	rev2;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_COLOR_NUM)] 
        public byte[] eColorOrder;
        public byte rev41;//dll eColorOrder按字符串处理
        [MarshalAs(UnmanagedType.I1)]
        public bool bSupportZendPointSensor; //是否支持z终点传感器
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] 
		public byte[]    rev42;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int) SpeedEnum.CustomSpeed)] 
        public byte[] eSpeedMap;

        public ushort nHeadNum
        {
            get 
            {
                if (IsKyocera(ePrinterHead) || CoreInterface.IsS_system())
                    return Math.Max(nHeadNumNew, nHeadNumOld);
                return nHeadNumOld;
            }
        }

        public float[] GetXOffset()
        {
            int phyHeadnum = GetPhyHeadNum();
            float[] xoffset = new float[phyHeadnum];
            int groupnum = phyHeadnum / nColorNum;
            int phylinenum = nColorNum;
            int phygroupnum = phyHeadnum / phylinenum;
            for (int k = 0; k < phygroupnum; k++)
            {
                double group0_X = fHeadXGroupSpace*k;
                if (fHeadXGroupSpace < 0)
                    group0_X = (- phygroupnum + 1 + k)*fHeadXGroupSpace;
                for (int i = 0; i < phylinenum; i++)
                {
                    int id = k*phylinenum + i;
                    double color_X = fHeadXColorSpace*i;
                    if (fHeadXColorSpace < 0)
                        color_X = fHeadXColorSpace*(i - phylinenum + 1);
                    xoffset[id] =
                        (float) (group0_X + color_X);
                }
            }

//			for (int j=0; j< groupnum;j++)
//			{
//				for (int i=0;i<nColorNum;i++)
//				{
//					if(fHeadXGroupSpace>0)
//						xoffset[j*nColorNum +i] = fHeadXColorSpace *i + fHeadXGroupSpace *j;
//					else
//						xoffset[j*nColorNum +i] = fHeadXColorSpace *i + fHeadXGroupSpace *(groupnum-1-j);
//
//				}
//			}
            return xoffset;
        }

        public float[] get_YOffset()
        {
            float[] yoffset = new float[nHeadNum];
            for (int i = 0; i < nColorNum; i++)
            {
                if (fHeadYSpace >= 0)
                    yoffset[i] = i*fHeadYSpace;
                else
                    yoffset[i] = (nColorNum - 1 - i)*fHeadYSpace;

            }
            return yoffset;
        }

        public bool IsDocanShading()
        {
#if DOCAN_SHADING
			return true;
#else
            return false;
#endif
        }
        /// <summary>
        /// 根据颜色索引获取颜色名称
        /// </summary>
        /// <param name="index">颜色索引</param>
        /// <param name="bCari">是否是校准界面调用</param>
        /// <returns></returns>
        public string Get_ColorIndex(int index,bool bCari=false)
        {
            try
            {
                int colornum = GetRealColorNum();
                if(IsMirrorArrangement()&&!bCari)
                    index = (index / colornum)%2 > 0 ? (colornum-1 - index % colornum) : index % colornum;
                else
                {
                    index = index%colornum;
                }
                if (IsDocanShading())
                {
                    if (colornum == 8)
                        index = index%4;
                }
                return ((ColorEnum_Short) eColorOrder[index]).ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public ColorEnum Get_ColorEnum(int index)
        {
            try
            {
                int colornum = GetRealColorNum();
                index = index%colornum;
                if (IsDocanShading())
                {
                    if (colornum == 8)
                        index = index%4;
                }
                return ((ColorEnum) eColorOrder[index]);
            }
            catch (Exception)
            {
                return ColorEnum.Black;
            }
        }
        /// <summary>
        /// 是否是基于二部系统的Epson系列
        /// </summary>
        /// <param name="cPrinterHead"></param>
        /// <returns></returns>
        public static bool IsEpson(PrinterHeadEnum cPrinterHead)
        {
            return (cPrinterHead == PrinterHeadEnum.RICOH_GEN4_7pl
                    || cPrinterHead == PrinterHeadEnum.RICOH_GEN4P_7pl
                    || cPrinterHead == PrinterHeadEnum.RICOH_GEN4L_15pl
                    || cPrinterHead == PrinterHeadEnum.EGen5
                   );
        }

        /// <summary>
        /// 是否是基于一部系统的Epson系列
        /// </summary>
        /// <param name="cPrinterHead"></param>
        /// <returns></returns>
        public static bool IsY1Epson(PrinterHeadEnum cPrinterHead)
        {
            return (cPrinterHead == PrinterHeadEnum.Ricoh_Gen6
                    || cPrinterHead == PrinterHeadEnum.Epson_5113
                    || cPrinterHead == PrinterHeadEnum.EPSON_I3200
                    || cPrinterHead == PrinterHeadEnum.XAAR_1201_Y1
                );
        }
        public static bool IsKonica512i(PrinterHeadEnum cPrinterHead)
        {
            if (
                          cPrinterHead == PrinterHeadEnum.Konica_KM512i_MHB_12pl
                        || cPrinterHead == PrinterHeadEnum.Konica_KM512i_LHB_30pl
                        || cPrinterHead == PrinterHeadEnum.Konica_KM512i_MAB_C_15pl
                        || cPrinterHead == PrinterHeadEnum.Konica_KM512i_SH_6pl
                        || cPrinterHead == PrinterHeadEnum.Konica_KM512i_SAB_6pl
                        || cPrinterHead == PrinterHeadEnum.Konica_KM512i_LNB_30pl
                )
                return true;
            else
                return false;

        }
        public static bool IsXAAR1201(PrinterHeadEnum cPrinterHead)
        {
            return (cPrinterHead == PrinterHeadEnum.XAAR_1201_Y1);
        }
        public static bool IsSG1024(PrinterHeadEnum cPrinterHead)
        {
            if (   cPrinterHead == PrinterHeadEnum.Spectra_SG1024MA_25pl
                || cPrinterHead == PrinterHeadEnum.Spectra_SG1024SA_12pl
                || cPrinterHead == PrinterHeadEnum.Spectra_SG1024XSA_7pl
                || cPrinterHead == PrinterHeadEnum.Spectra_SG1024LA_80pl
                )
                return true;
            else
                return false;
        }
        public static bool IsKonica1024i(PrinterHeadEnum cPrinterHead)
        {
            if (cPrinterHead == PrinterHeadEnum.Konica_KM1024i_MHE_13pl
                || cPrinterHead == PrinterHeadEnum.Konica_KM1024i_MAE_13pl
                || cPrinterHead == PrinterHeadEnum.Konica_KM1024i_LHE_30pl
                || cPrinterHead == PrinterHeadEnum.Konica_KM1024i_SHE_6pl
                || cPrinterHead == PrinterHeadEnum.Konica_KM1024i_SAE_6pl
                )
                return true;
            else
                return false;

        }
        public static bool IsKyocera300(PrinterHeadEnum cPrinterHead)
        {
            if (cPrinterHead == PrinterHeadEnum.Kyocera_KJ4B_0300_5pl_1h2c ||
                cPrinterHead == PrinterHeadEnum.Kyocera_KJ4A_0300_5pl_1h2c
                )
                return true;
            return false;
        }
        public static bool IsKyocera600(PrinterHeadEnum cPrinterHead)
        {
            if (cPrinterHead == PrinterHeadEnum.Kyocera_KJ4B_QA06_5pl ||
                cPrinterHead == PrinterHeadEnum.Kyocera_KJ4B_YH06_5pl ||
                cPrinterHead == PrinterHeadEnum.Kyocera_KJ4A_TA06_6pl ||
                cPrinterHead == PrinterHeadEnum.Kyocera_KJ4A_AA06_3pl ||
                cPrinterHead == PrinterHeadEnum.Kyocera_KJ4A_RH06)
                return true;
            return false;
        }
        public static bool IsKyocera1200(PrinterHeadEnum cPrinterHead)
        {
            if (cPrinterHead == PrinterHeadEnum.Kyocera_KJ4B_1200_1p5pl ||
                cPrinterHead == PrinterHeadEnum.Kyocera_KJ4A_1200_1p5pl)
                return true;
            return false;
        }
        public static bool IsKyocera300And600(PrinterHeadEnum cPrinterHead)
        {
            if (IsKyocera300(cPrinterHead) ||
                IsKyocera600(cPrinterHead))
                return true;
            return false;
        }
        public static bool IsKyocera(PrinterHeadEnum cPrinterHead)
        {
            if (IsKyocera300(cPrinterHead) ||
                IsKyocera600(cPrinterHead) ||
                IsKyocera1200(cPrinterHead))
                return true;
            return false;
        }

        public static bool IsPolarisOneHead4Color(PrinterHeadEnum cPrinterHead)
        {
            if (cPrinterHead == PrinterHeadEnum.Spectra_PolarisColor4_15pl
                || cPrinterHead == PrinterHeadEnum.Spectra_PolarisColor4_35pl
                || cPrinterHead == PrinterHeadEnum.Spectra_PolarisColor4_80pl
                )
                return true;
            else
                return false;
        }

        public static bool IsPolaris(PrinterHeadEnum cPrinterHead)
        {
            return (cPrinterHead == PrinterHeadEnum.Spectra_Polaris_15pl
                    || cPrinterHead == PrinterHeadEnum.Spectra_Polaris_35pl
                    || cPrinterHead == PrinterHeadEnum.Spectra_Polaris_80pl
                    || IsPolarisOneHead4Color(cPrinterHead)
                   );
        }

        public static bool IsSpectra(PrinterHeadEnum cPrinterHead)
        {
            if (
                cPrinterHead == PrinterHeadEnum.Spectra_S_128 ||
                cPrinterHead == PrinterHeadEnum.Spectra_GALAXY_256 ||
                cPrinterHead == PrinterHeadEnum.Spectra_NOVA_256 ||
                cPrinterHead == PrinterHeadEnum.Spectra_Polaris_15pl ||
                cPrinterHead == PrinterHeadEnum.Spectra_Polaris_35pl ||
                cPrinterHead == PrinterHeadEnum.Spectra_Polaris_80pl ||

                cPrinterHead == PrinterHeadEnum.Spectra_PolarisColor4_15pl ||
                cPrinterHead == PrinterHeadEnum.Spectra_PolarisColor4_35pl ||
                cPrinterHead == PrinterHeadEnum.Spectra_PolarisColor4_80pl ||
                SPrinterProperty.IsSG1024(cPrinterHead)
                )
                return true;
            else
                return false;
        }

        public static bool IsSurpportVolumeConvert(PrinterHeadEnum cPrinterHead)
        {
            if (cPrinterHead == PrinterHeadEnum.Konica_KM512M_14pl
                || cPrinterHead == PrinterHeadEnum.Konica_KM512MAX_14pl
                || cPrinterHead == PrinterHeadEnum.Konica_KM1024S_6pl
                || cPrinterHead == PrinterHeadEnum.Konica_KM3688_6pl
                || cPrinterHead == PrinterHeadEnum.Konica_KM1024M_14pl
                || cPrinterHead == PrinterHeadEnum.Konica_KM512MAX_14pl
                || cPrinterHead == PrinterHeadEnum.Konica_KM1024i_SHE_6pl
                || cPrinterHead == PrinterHeadEnum.Konica_KM1024i_SAE_6pl
                )
                return true;
            else
                return false;
        }

        public static bool IsKonica512(PrinterHeadEnum cPrinterHead)
		{
			if(cPrinterHead ==  PrinterHeadEnum.Konica_KM512L_42pl
				|| cPrinterHead == PrinterHeadEnum.Konica_KM512M_14pl
				|| cPrinterHead == PrinterHeadEnum.Konica_KM512LNX_35pl
                || cPrinterHead == PrinterHeadEnum.Konica_KM512_SH_4pl

                || cPrinterHead == PrinterHeadEnum.Konica_KM1024S_6pl
                || cPrinterHead == PrinterHeadEnum.Konica_KM3688_6pl
				|| cPrinterHead == PrinterHeadEnum.Konica_KM1024M_14pl
				|| cPrinterHead == PrinterHeadEnum.Konica_KM1024L_42pl

				|| cPrinterHead == PrinterHeadEnum.Konica_KM512MAX_14pl
				|| cPrinterHead == PrinterHeadEnum.Konica_KM512LAX_30pl

                || SPrinterProperty.IsKonica512i(cPrinterHead)
				)
				return true;
			else
				return false;
		}
		public static bool IsNewXaar382(PrinterHeadEnum cPrinterHead)
		{
			bool bNewHBVirion = false;
			SBoardInfo sBoardInfo = new SBoardInfo();
			if( CoreInterface.GetBoardInfo(0,ref sBoardInfo) != 0)
			{
				SFWVersion fwv= new SFWVersion(sBoardInfo.m_nHBBoardVersion);
				bNewHBVirion = fwv.m_nMainVersion>=2&&fwv.m_nSubVersion>=2;
			}
			bool m_bNewXaar382 = (cPrinterHead == PrinterHeadEnum.Xaar_Proton382_35pl
				||cPrinterHead == PrinterHeadEnum.Xaar_Proton382_60pl
				||cPrinterHead == PrinterHeadEnum.Xaar_Proton382_15pl
				)&& bNewHBVirion;
			return m_bNewXaar382;
		}
		public bool EPSONLCD_DEFINED
		{
		    get
		    {
		        bool bsupportLcd = (this.bSupportBit1 & (byte) INTBIT.Bit_2) != 0;
		        return (SPrinterProperty.IsEpson(ePrinterHead)) && bsupportLcd;
		    }
		}
        /// <summary>
        /// 是否支持z轴测量
        /// </summary>
        public bool IsZMeasurSupport
        {
            get
            {
                return (this.bSupportBit1 & (uint)INTBIT.Bit_9) != 0; ;
                //return true;
            }
        }

        public bool IsMirrorArrangement()
        {
            bool bMirror = (bSupportBit1 & (byte)INTBIT.Bit_6) != 0;
            return bMirror;
        }	

		public int GetSpotColorNum()
		{
			return (nWhiteInkNum&0xf) + ((nWhiteInkNum>>4)&0xF);
		}
        public int GetNozzleNum()
        {
            return CoreInterface.GetOneHeadNozzleNum() * this.nHeadNumPerColor;
        }

        /// <summary>
        /// 获取物理喷头个数
        /// </summary>
        /// <returns></returns>
        public int GetPhyHeadNum()
        {
            int rowNumPerHead = nHeadNumPerColor * nOneHeadDivider;
            if (IsMirrorArrangement())
                rowNumPerHead /= 2;
            return nHeadNum/rowNumPerHead;
        }

		public bool IsALLWIN_FLAT()
		{
#if !LIYUUSB
			ushort pid,vid ;
			pid = vid = 0;
			int ret = CoreInterface.GetProductID(ref vid, ref pid);
			if(ret != 0)
			{
				if(vid == (ushort)VenderID.ALLWIN_FLAT_UV) 
					return true;
				else
					return false;
			}
			else
			{
				string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.GetHWSettingFail);
				MessageBox.Show(info,ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Information);
				return false;
			}
#else
		    return false;
#endif
		}
        /// <summary>
        /// 奥威 512i 高速印染机
        /// </summary>
        /// <returns></returns>
        public bool IsALLWIN_512i_HighSpeed()
        {
#if !LIYUUSB
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if(vid == (ushort)VenderID.ALLWIN && 
                    (this.ePrinterHead == PrinterHeadEnum.Konica_KM512i_MAB_C_15pl
                    || this.ePrinterHead == PrinterHeadEnum.Konica_KM512i_LNB_30pl)
					)  
					return true;
                return false;
            }
            string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.GetHWSettingFail);
            MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
#else
		    return false;
#endif
        }

        public static bool IsDocanPrintMode()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if ((vid == (ushort)VenderID.DOCAN_FLAT_UV)
                    || vid == (ushort)VenderID.DOCAN_ROLL_UV
                    )
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public static bool IsGZ_GMA_PrintMode()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (((vid == (ushort)VenderID.GONGZENG_FLAT_UV) && pid == 0x1300 )
                    || ((vid == (ushort)VenderID.GONGZENG_ROLL_UV) && pid == 0x1323))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public static bool IsLingFeng_RollUV_16H()
        {
            //return true;
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.LINGFENG_RollUV_16H)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public static bool IsHuman()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == 0x85)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// 是否为Docan
        /// </summary>
        /// <returns></returns>
        public bool IsDocan()
        {
#if !LIYUUSB
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if ((vid &0x807f )== ((ushort)VenderID.DOCAN &0x807f) 
                    //|| vid == (ushort)VenderID.DOCAN_FLAT_UV
                    //|| vid == (ushort)VenderID.DOCAN_ROLL_UV
                    //|| vid == (ushort)VenderID.DOCAN_BELT
                    )
                    return true;
                return false;
            }
            //string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.GetHWSettingFail);
            //MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
#else
		    return false;
#endif
        }

        /// <summary>
        /// 判断厂商Id是否为世导
        /// </summary>
        /// <returns></returns>
        public bool IsKEDITEC()
        {
#if !LIYUUSB
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.KEDITEC || vid == (ushort)VenderID.KEDITEC_ROLL_TEXTILE
                    || vid == (ushort)VenderID.KEDITEC_FLAT_TEXTILE)
                    return true;
                return false;
            }
            string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.GetHWSettingFail);
            MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
#else
		    return false;
#endif
        }

        /// <summary>
        /// 是否是傲威 JetRix这个厂家
        /// </summary>
        /// <returns></returns>
        public bool IsAllWin_JetRix()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.ALLWIN)
                    return true;
                return false;
            }
            //string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.GetHWSettingFail);
            //MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

        /// <summary>
        /// 功能和IsZMeasurSupport一致 gzw20150519
        /// </summary>
        /// <returns></returns>
		public bool ZMeasurSensorSupport()
		{
#if !LIYUUSB
#if true
            return IsZMeasurSupport;
#else
			ushort pid,vid ;
			pid = vid = 0;
			int ret = CoreInterface.GetProductID(ref vid, ref pid);
			if(ret != 0)
			{
			    if(vid == (ushort)VenderID.DOCAN || vid == (ushort)VenderID.DOCAN_FLAT_UV  //Docan
					||vid == (ushort)VenderID.ALLWIN_FLAT_UV
                    //|| vid == (ushort)VenderID.WITCOLOR_FLAT_UV
					) 
					return true;
			    return false;
			}
		    string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.GetHWSettingFail);
		    MessageBox.Show(info,ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Information);
		    return false;
#endif
#else
		    return false;
#endif
        }

        public bool IsAllWinUV()
        {
#if !LIYUUSB
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (
                    vid == (ushort)VenderID.ALLWIN_FLAT_UV
                    
                    || vid == (ushort)VenderID.ALLWIN_ROLL_UV
                    
                    )
                    return true;
                else
                    return false;
            }
            else
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.GetHWSettingFail);
                MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
#else
		    return false;
#endif
        }

		public bool IsDisplayForm()
		{
#if false// !LIYUUSB
			ushort pid,vid ;
			pid = vid = 0;
			int ret = CoreInterface.GetProductID(ref vid, ref pid);
			if(ret != 0)
			{
				if(vid == (ushort)VenderID.ALLWIN
					||vid == (ushort)VenderID.ALLWIN_EPSON4
					||vid == (ushort)VenderID.ALLWIN_FLAT_UV
                    || vid == (ushort)VenderID.ALLWIN_TEXTILE4
					||vid == (ushort)VenderID.ALLWIN_ROLL_UV
					||vid == (ushort)VenderID.ALLWIN_TEXTILE2
					||vid == (ushort)VenderID.ALLWIN_UV_EPSON4
					)  
					return true;
				else
					return false;
			}
			else
			{
				string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.GetHWSettingFail);
				MessageBox.Show(info,ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Information);
				return false;
			}
#else
		    return false;
#endif
		}
        public static bool IsTateKey()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)0x218)
                    return true;
                else
                    return false;
            }
            else
            {
                //string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.GetHWSettingFail);
                //MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

		public bool IsTATE()
		{
				ushort pid,vid ;
				pid = vid = 0;
				int ret = CoreInterface.GetProductID(ref vid, ref pid);
				if(ret != 0)
				{
                if ((vid & 0x807F) == (ushort)VenderID.TATE)
						return true;
					else
						return false;
				}
				else
				{
                //string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.GetHWSettingFail);
                //MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
					return false;
				}
		}
        /// <summary>
        /// 是否为锐智厂商
        /// </summary>
        /// <returns></returns>
        public static bool IsRuiZhi()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (
                    vid == (ushort)VenderID.RuiZhi ||
                    vid == (ushort)0x8082
                    )
                    return true;
                else
                    return false;
            }
            return false;
        }
        /// <summary>
        /// 是否为工正厂商,模糊匹配
        /// </summary>
        /// <returns></returns>
        public static bool IsGongZeng()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if ((vid & 0xF) == (ushort)VenderID.GONGZENG)
                    return true;
                else
                    return false;
            }
            return false;
        }
        /// <summary>
        /// 工正一个机型，008b0300
        /// </summary>
        /// <returns></returns>
        public static bool IsGongZengMeasureBeforPrint()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (
                    vid == (ushort)VenderID.GONGZENG_FLAT_UV && pid == 0x0300)
                    return true;
                else
                    return false;
            }
            return false;
        }
        /// <summary>
        /// 工正一个机型，000b0100
        /// </summary>
        /// <returns></returns>
        public static bool IsGongZengNew()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (
                    vid == (ushort)VenderID.GONGZENG && pid==0x0100)
                    return true;
                else
                    return false;
            }
            return false;
        }
        public static bool IsGongZengUv()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (
                    vid == (ushort)VenderID.GONGZENG_FLAT_UV
                    || vid == (ushort)VenderID.GONGZENG_ROLL_UV
                    || vid == (ushort)VenderID.GONGZENG_DOUBLE_SIDE
                    )
                    return true;
                else
                    return false;
            }
            return false;
        }

        public static bool IsGONGZENG_DOUBLE_SIDE()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.GONGZENG_DOUBLE_SIDE)
                    return true;
                else
                    return false;
            }
            return false;
        }

		public bool IsGongZengEpson()
		{
	#if !LIYUUSB
			if(SPrinterProperty.IsEpson(this.ePrinterHead))
			{
				ushort pid,vid ;
				pid = vid = 0;
				int ret = CoreInterface.GetProductID(ref vid, ref pid);
				if(ret != 0)
				{
					if(vid == (ushort)VenderID.GONGZENG || vid ==  (ushort)VenderID.GONGZENG_FLAT_UV) 
						return true;
					else
						return false;
				}
				else
				{
					string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.GetHWSettingFail);
					MessageBox.Show(info,ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Information);
					return false;
				}
			}
			else
				return false;
#else
            return false;
#endif
		}

        public bool IsAllWinEpson()
		{
#if !LIYUUSB
        	if(SPrinterProperty.IsEpson(this.ePrinterHead))
			{
				ushort pid,vid ;
				pid = vid = 0;
				int ret = CoreInterface.GetProductID(ref vid, ref pid);
				if(ret != 0)
				{
					if(vid == (ushort)VenderID.ALLWIN || vid ==  (ushort)VenderID.ALLWIN_FLAT_UV ) 
						return true;
					else
						return false;
				}
				else
				{
					string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.GetHWSettingFail);
					MessageBox.Show(info,ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Information);
					return false;
				}
			}
			else
				return false;
#else
		    return false;
#endif
		}

		public bool IsSurportAutoSpray()
		{
#if !LIYUUSB
			if(SPrinterProperty.IsEpson(this.ePrinterHead))
			{
				ushort pid,vid ;
				pid = vid = 0;
				int ret = CoreInterface.GetProductID(ref vid, ref pid);
				if(ret != 0)
				{
					if(vid == (ushort)VenderID.MICOLOR || vid ==  (ushort)VenderID.MICOLOR_FLAT_UV 
						||vid == (ushort)VenderID.BEMAJET || vid ==  (ushort)VenderID.BEMAJET_FLAT_UV) 
						return true;
					else
						return false;
				}
				else
				{
					string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.GetHWSettingFail);
					MessageBox.Show(info,ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Information);
					return false;
				}
			}
			else
				return false;
#else
		    return false;
#endif
		}

        /// <summary>
        /// 是否支持喷头保湿/会维护站功能
        /// </summary>
        /// <returns></returns>
        public static bool IsSurportCapping()
        {
            if (PubFunc.Is3DPrintMachine() || UIFunctionOnOff.SupportCapping) 
                return true;
#if !LIYUUSB
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort) VenderID.COLORJET || vid == (ushort) VenderID.COLORJET_BELT_TEXTILE
                    || vid == (ushort) VenderID.COLORJET_FLAT_UV || vid == (ushort) VenderID.COLORJET_ROLL_TEXTILE

                    || vid == (ushort)VenderID.SAISHUN_BELT_TEXTILE || vid == (ushort)VenderID.SAISHUN || vid == (ushort)VenderID.SAISHUN_FLAT_TEXTILE
                    || vid == (ushort)VenderID.FLORA_FLAT_TEXTILE || vid == (ushort)VenderID.FLORA_BELT_TEXTILE
                    || vid == (ushort)VenderID.BluePrint_ROLL_UV || vid == (ushort)VenderID.BluePrint_BELT_UV
                    || vid == (ushort)VenderID.ALLPRINT || vid == (ushort)VenderID.ALLPRINT_FLAT_TEXTILE || vid == (ushort)VenderID.ALLPRINT_DOUBLE_FLAT
                    || vid == (ushort)VenderID.ALLWIN_ROLL_TEXTILE || vid == (ushort)VenderID.ALLWIN_BELT_TEXTILE
                    || vid == (ushort)VenderID.NKT || vid == (ushort)VenderID.NKT_Onepass || vid == (ushort)VenderID.NKT_FLAT_SLOVENT
                    || vid == (ushort)VenderID.FangDa || vid == (ushort)VenderID.FangDa_BELT_TEXTILE
                    || vid == (ushort)VenderID.DXSC_FLAT_GLASS
                    || vid == (ushort)VenderID.AUDLEY_BELT_TEXTILE
                    || vid == (ushort)VenderID.DOCAN_BELT_TEXTILE
                    || vid == (ushort)VenderID.BIHONG_FLAT_TEXTILE
                    || vid == (ushort)VenderID.HuiLiCai
                    || vid == (ushort)VenderID.PuQi
                    || vid == (ushort)0x255
                    )
                    return true;
                return false;
            }
            string info = ResString.GetEnumDisplayName(typeof (UIError), UIError.GetHWSettingFail);
            MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
#else
		    return false;
#endif
        }

        public static bool IsHighResolution(PrinterHeadEnum nHeadType)
        {
            if (nHeadType == PrinterHeadEnum.Konica_KM256M_14pl
                || nHeadType == PrinterHeadEnum.Konica_KM1024S_6pl
                || nHeadType == PrinterHeadEnum.Konica_KM3688_6pl
                || nHeadType == PrinterHeadEnum.Konica_KM1024M_14pl
                || nHeadType == PrinterHeadEnum.Konica_KM512M_14pl
                || nHeadType == PrinterHeadEnum.Konica_KM512_SH_4pl
                || SPrinterProperty.IsKonica512i(nHeadType)
                || nHeadType == PrinterHeadEnum.Konica_KM512MAX_14pl
                || nHeadType == PrinterHeadEnum.Spectra_GALAXY_256
                || nHeadType == PrinterHeadEnum.Spectra_Polaris_15pl
                || nHeadType == PrinterHeadEnum.Spectra_Polaris_35pl
                || SPrinterProperty.IsSG1024(nHeadType)
                || nHeadType == PrinterHeadEnum.Spectra_PolarisColor4_15pl
                || nHeadType == PrinterHeadEnum.Spectra_PolarisColor4_35pl

                || nHeadType == PrinterHeadEnum.Konica_KM512LNX_35pl
                || nHeadType == PrinterHeadEnum.Xaar_Proton382_35pl
                || nHeadType == PrinterHeadEnum.Xaar_Proton382_15pl
                || SPrinterProperty.IsEpson(nHeadType)

                || nHeadType == PrinterHeadEnum.Spectra_Emerald_10pl
                || nHeadType == PrinterHeadEnum.Spectra_Emerald_30pl
                || SPrinterProperty.IsKyocera(nHeadType)

                || IsKonica1024i(nHeadType)

                || nHeadType == PrinterHeadEnum.Konica_KM1800i_3P5pl
                || nHeadType == PrinterHeadEnum.Konica_M600SH_2C

                || nHeadType == PrinterHeadEnum.Fujifilm_GMA3305300_5pl
                || nHeadType == PrinterHeadEnum.Fujifilm_GMA9905300_5pl

                || nHeadType == PrinterHeadEnum.Epson_S2840
                || nHeadType == PrinterHeadEnum.Epson_S2840_WaterInk
                || nHeadType == PrinterHeadEnum.EPSON_S1600_RC_UV
                || nHeadType == PrinterHeadEnum.Konica_KM1024A_6_26pl
                || nHeadType == PrinterHeadEnum.Epson_5113
                || nHeadType == PrinterHeadEnum.EPSON_I3200
                || nHeadType == PrinterHeadEnum.Ricoh_Gen6
                )
                return true;
            else
                return false;
        }

		public int GetFeatherPercent()
		{
		    return CoreConst.MAX_PASS_NUM*100;
		}

		public void SynchronousCalibrationSettings(ref SPrinterSetting ss)
		{
			if(EPSONLCD_DEFINED)
				EpsonLCD.GetCalibrationSetting(ref ss.sCalibrationSetting,false);
			CoreInterface.SetPrinterSetting(ref ss);
		}

		public int GetRealColorNum()
		{
			return Math.Min(this.nColorNum,this.eColorOrder.Length);
		}
		public Color GetButtonColor(int iIndex)
		{
			int colornum = GetRealColorNum();
			iIndex %= colornum;
			ColorEnum colorEnum = (ColorEnum)eColorOrder[iIndex];
			Color color = new Color();
			switch(colorEnum)
			{
				case ColorEnum.Cyan:
					color = Color.Cyan;
					break;
				case ColorEnum.Magenta:
					color = Color.Magenta;
					break;
				case ColorEnum.Yellow:
					color = Color.Yellow;
					break;
				case ColorEnum.Black:
					color = Color.Black;
					break;
				case ColorEnum.LightCyan:
					color = Color.LightCyan;
					break;
				case ColorEnum.LightMagenta:
					color = Color.LightPink;
					break;
				case ColorEnum.LightYellow:
					color = Color.LightYellow;
					break;
				case ColorEnum.LightBlack:
					color = Color.LightGray;
					break;
				case ColorEnum.Red:
					color = Color.Red;
					break;
				case ColorEnum.Blue:
					color = Color.Blue;
					break;
				
				case ColorEnum.Orange:
					color = Color.Orange;
					break;
				case ColorEnum.Green:
					color = Color.Green;
					break;

				case ColorEnum.White:
					color = Color.White;
					break;
				case ColorEnum.Vanish:
					color = Color.White;
					break;
                case ColorEnum.SkyBlue:
			        color = Color.SkyBlue;
			        break;
                case  ColorEnum.Gray:
                    color = Color.Gray;
                    break;
                default:
					color = Color.Gray;
					break;
			}
			return color;
					
		}


        public static bool IsKonic1800i(PrinterHeadEnum nHeadType)
        {
            return nHeadType == PrinterHeadEnum.Konica_KM1800i_3P5pl;
        }


        public bool IsKonicHead()
        {
            return this.ePrinterHead == PrinterHeadEnum.Konica_KM1024L_42pl
                   || this.ePrinterHead == PrinterHeadEnum.Konica_KM1024M_14pl
                   || this.ePrinterHead == PrinterHeadEnum.Konica_KM1024S_6pl
                   || this.ePrinterHead == PrinterHeadEnum.Konica_KM3688_6pl
                   || this.ePrinterHead == PrinterHeadEnum.Konica_KM128L_42pl
                   || this.ePrinterHead == PrinterHeadEnum.Konica_KM128M_14pl
                   || this.ePrinterHead == PrinterHeadEnum.Konica_KM256L_42pl
                   || this.ePrinterHead == PrinterHeadEnum.Konica_KM256M_14pl
                   || this.ePrinterHead == PrinterHeadEnum.Konica_KM512LAX_30pl
                   || this.ePrinterHead == PrinterHeadEnum.Konica_KM512LNX_35pl
                   || this.ePrinterHead == PrinterHeadEnum.Konica_KM512L_42pl
                   || this.ePrinterHead == PrinterHeadEnum.Konica_KM512MAX_14pl
                   || this.ePrinterHead == PrinterHeadEnum.Konica_KM512M_14pl
                   || this.ePrinterHead == PrinterHeadEnum.Konica_KM512_SH_4pl
                   || SPrinterProperty.IsKonica512i(this.ePrinterHead)
                   || SPrinterProperty.IsKonica1024i(this.ePrinterHead)
                    ;
        }


        public bool IsAOJET_UV()
        {
#if !LIYUUSB
			ushort pid,vid ;
			pid = vid = 0;
			int ret = CoreInterface.GetProductID(ref vid, ref pid);
			if(ret != 0)
			{
                if (vid == (ushort)VenderID.AOJET || vid == (ushort)VenderID.AOJET_UV) 
					return true;
			    return false;
			}
			else
			{
				string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.GetHWSettingFail);
				MessageBox.Show(info,ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Information);
				return false;
			}
#else
            return false;
#endif
        }

        public bool IsTLHG()
        {
#if !LIYUUSB
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.TLHG || vid == (ushort)VenderID.TLHG_BELT_TEXTILE)
                    return true;
                return false;
            }
            else
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.GetHWSettingFail);
                MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
#else
            return false;
#endif
        }

        public bool IsCYQF()
        {
#if !LIYUUSB
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)0x8091 || vid == (ushort)1100)
                    return true;
                return false;
            }
            else
            {
                //string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.GetHWSettingFail);
                //MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
#else
            return false;
#endif
        }

        public static bool IsHanTuo()
        {
#if !LIYUUSB
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.HanTuo)
                    return true;
                return false;
            }
            else
            {
                //string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.GetHWSettingFail);
                //MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
#else
            return false;
#endif
        }
        /// <summary>
        /// y回原点是否由软件控制
        /// </summary>
        /// <returns></returns>
        public bool Y_BackToOrgControlBySw()
        {
            return (IsAllPrint() && nMediaType != 0) || IsYINKELI_FLAT_TEXTILE_ID() || IsBiHong() || UIFunctionOnOff.SupportBackToOrgYControlBySw;
        }

        public static bool IsAllPrint()
        {
#if !LIYUUSB
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.ALLPRINT 
                    || vid == (ushort)VenderID.ALLPRINT_BELT_TEXTILE
                    || vid == (ushort)VenderID.ALLPRINT_FLAT_TEXTILE
                    || vid == (ushort)VenderID.ALLPRINT_DOUBLE_FLAT
                    )
                    return true;
                return false;
            }
            return false;
#else
            return false;
#endif
        }
        public static bool IsAllPrintDoubleFlat()
        {
#if !LIYUUSB
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.ALLPRINT_DOUBLE_FLAT || vid == (ushort)VenderID.ALLPRINT_FLAT_TEXTILE)
                    return true;
                return false;
            }
            return false;
#else
            return false;
#endif
        }

        /// <summary>
        /// 是否为印可丽椭圆数码机
        /// </summary>
        /// <returns></returns>
        public static bool IsYINKELI_FLAT_TEXTILE_ID()
        {
#if !LIYUUSB
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.YINKELI_FLAT_TEXTILE)
                    return true;
                return false;
            }
            return false;
#else
            return false;
#endif
        }
        /// <summary>
        /// 是否为彩神印染
        /// </summary>
        /// <returns></returns>
        public static bool IsFloraT50()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.FLORA_FLAT_TEXTILE)
                    return true;
                return false;
            }
            return false;
        }
        /// <summary>
        /// 是否支持双平台
        /// </summary>
        /// <returns></returns>
        public static bool SurpportDoublePlatform()
        {
            if (DoublePlatformDebugMode()
                || PubFunc.IsZhuoZhan()
                || IsFloraT50OrT180()
                )
                return true;
            return false;
        }


        /// <summary>
        /// 是否为彩神uv
        /// </summary>
        /// <returns></returns>
        public static bool IsFloraUv()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.FLORA_FLAT_UV)
                    return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 是否为彩神
        /// </summary>
        /// <returns></returns>
        public static bool IsFlora()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.FLORA_FLAT_UV
                    || vid==(ushort)VenderID.FLORA
                    || vid == (ushort)VenderID.FLORA_BELT_TEXTILE
                    || vid == (ushort)VenderID.FLORA_FLAT_TEXTILE)
                    return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 是否为彩神直喷机
        /// </summary>
        /// <returns></returns>
        public static bool IsFloraBeltTEXTILE()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.FLORA_BELT_TEXTILE)
                    return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 是否为彩神T50/T180
        /// </summary>
        /// <returns></returns>
        public static bool IsFloraT50OrT180()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.FLORA_FLAT_TEXTILE || vid == (ushort)VenderID.FLORA_BELT_TEXTILE)
                    return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 丽彩5113机器
        /// </summary>
        /// <returns></returns>
        public static bool IsLiCai5113()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)0x8205)
                    return true;
                return false;
            }
            return false;
        }
        /// <summary>
        /// 是否为彩神 034b1526
        /// </summary>
        /// <returns></returns>
        public static bool IsFlora_BELT_TEXTILE1526()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.FLORA_BELT_TEXTILE && pid == 0x1526)
                    return true;
                return false;
            }
            return false;
        }

        public static bool DoublePlatformDebugMode()
        {
            if (File.Exists(Path.Combine(Application.StartupPath, "DoublePlatform.bin")))
                return true;
            return false;
        }
        /// <summary>
        /// 是否为瓷砖机id
        /// </summary>
        /// <returns></returns>
        public static bool IsTILE_PRINT_ID()
        {
            ushort Vid, Pid;
            Vid = Pid = 0;
            bool isNkt = false;
            if (CoreInterface.GetProductID(ref Vid, ref Pid) != 0)
            {
                if ((Vid > 0x400 && Vid < 0x500) || Vid == (ushort)VenderID.NKT || Vid == (ushort)VenderID.BoHui_FLAT_TEXTILE)
                {
                    isNkt = true;
                }
            }
            return isNkt;
        }

        /// <summary>
        /// 判断是否为英威KM512i_MAB_C
        /// </summary>
        /// <returns></returns>
        public bool IsInwearKm512iMab_c()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.INWEAR&&ePrinterHead == PrinterHeadEnum.Konica_KM512i_MAB_C_15pl)
                    return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 是否显示gz压墨 负压信息
        /// 工正的uv 机型或者是gama喷头才显示
        /// </summary>
        /// <returns></returns>
        public bool ShowGzPurgeControl()
        {
            return IsGongZengUv()
                || (IsGongZeng()
                && (ePrinterHead == PrinterHeadEnum.Fujifilm_GMA3305300_5pl || ePrinterHead == PrinterHeadEnum.Fujifilm_GMA9905300_5pl));
        }

        public static bool IsSimpleUV()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.FLORA_FLAT_UV || vid == (ushort)0xDDDD)
                    return true;
                if ((vid & 0x807f) == ((ushort)VenderID.DOCAN & 0x807f) && vid != (ushort)VenderID.DOCAN_BELT_TEXTILE)
                    return true;

                return false;
            }
            return false;
        }

        /// <summary>
        /// 第一次就绪时不显示
        /// </summary>
        /// <returns></returns>
        public static bool NoShowMeasureMsgWhenFirstReady()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (
                    vid == (ushort) VenderID.NKT || vid == (ushort) VenderID.NKT_Onepass ||
                    vid == (ushort) VenderID.NKT_FLAT_SLOVENT
                    )
                    return true;
            }
            return IsSimpleUV();
        }
        /// <summary>
        /// 卷轴机类似平板应用
        /// 作业间距作为y原点[卷轴本来没有y原点概念的]
        /// </summary>
        /// <returns></returns>
        public bool SurpportJobSpaceAsOriginY()
        {
            // 目前越达和彩神支持
            return (IsYUEDA_BELT_UV() || IsFloraUv() || UIFunctionOnOff.SurpportJobSpaceAsOriginY) && nMediaType == 0;
        }

        /// <summary>
        /// 判断是否为越达京瓷导带机
        /// </summary>
        /// <returns></returns>
        public static bool IsYUEDA_BELT_UV()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.YUEDA_BELT_UV)
                    return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 是否越达厂家
        /// </summary>
        /// <returns></returns>
        public static bool IsYUEDA()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)0x43 || vid == (ushort)0xC3 || vid == (ushort)0x1C3 || vid == (ushort)0x843 || vid == (ushort)0x343 || vid == (ushort)0x743)
                    return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 判断是否为英威特殊应用简化界面[KIP机器]
        /// </summary>
        /// <returns></returns>
        public static bool IsInwearSimpleUi()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if ((vid == (ushort)VenderID.INWEAR_ROLL_UV && pid == 0x0120) || vid == (ushort)0xDDDD)
                    return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 是否为colorjet rool uv
        /// </summary>
        /// <returns></returns>
        public bool IsColorJetRoolUv()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.COLORJET_ROLL_UV
                    || vid == (ushort)VenderID.COLORJET_BELT_TEXTILE
                    )
                    return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 是否为碧宏
        /// </summary>
        /// <returns></returns>
        public static bool IsBiHong()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.BIHONG || vid == (ushort)VenderID.BIHONG_FLAT_TEXTILE)
                    return true;
                return false;
            }
            return false;
        }
        public static bool IsBIHONG_FLAT_TEXTILE()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.BIHONG_FLAT_TEXTILE)
                    return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 是否是聆风京瓷-belt-textile
        /// </summary>
        /// <returns></returns>
        public static bool IsLingFengJingCiBeltTextile()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.COLORJET_BELT_TEXTILE && pid == 0x1500)
                    return true;
                return false;
            }
            return false;
        }

        public static bool SurpportCurJobIndex()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)0x0307)
                    return true;
                return false;
            }

            return false;
        }

        public static bool IsHengYin()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.HengYin)
                    return true;
                return false;
            }

            return false;
        }
        /// <summary>
        /// 北京博联20190429_e6ec9bb8-3d64-4bbc-8fbc-b5861f097c39
        /// </summary>
        /// <returns></returns>
        public static bool IsBoLian3D()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == 0x00EF && pid == 0x0100)
                    return true;
                return false;
            }

            return false;
        }

        public static bool IsColorJetWet()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == 0x308)
                    return true;
                return false;
            }

            return false;
        }


        /// <summary>
        /// 东川理光g6-flat-uv
        /// </summary>
        /// <returns></returns>
        public static bool IsDongChuan_Rich_G6_Flat_UV()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.DOCAN_FLAT_UV && pid == 0x1641)
                    return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 坚锐打瓶子
        /// </summary>
        /// <returns></returns>
        public static bool IsJianRui()
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.JianRui_FLAT_UV)
                    return true;
            }
            return false;
        }
    }

}
