using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Text;
using System.Windows.Data;
using PrinterStubC.Utility;

namespace BYHXPrinterManager
{
	public enum XCoordinatesDirection 
	{
		//		Default = 0,
		YcontinueYinterleave,// 在X方向的坐标，先Ycontinue group，再Yinterleave group。
		YinterleaveYcontinue, // 在X方向的坐标，先Yinterleave group，再Ycontinue group。
	}

	public enum SymmetricType 
	{
		Type0 = 0, // 0，以最外层为对称单位（在不同的头上颜色是对称的）方式0，在消除因覆盖关系导致的色差，效果应该更好。
		Type1,  //1，以最内层为对称单位（在一个头内部颜色是对称的）。方式1，更容易拼插，两个颜色之间的距离比较窄。
	}


	public enum Cali_Pattern_Type
	{
		DotCheck = 0,
		NozzleCheck,
		StepCheck,
		StepCheck_Micro,
		StepVerify,
		LeftCheck,
		RightCheck,
		BiDirCheck,
		VerticalCheck,
		InterleaveCheck,
		YIntervalCheck,
		AngleLeftCheck,
		AngleRightCheck
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 0)]
	public struct CaliPrintSetting
	{
		public Cali_Pattern_Type type;
 
		public byte VSDModel; // 第二个Byte是VSD Model,合法值是1-4。default是3。
		public byte DotSetting; // 第三个Byte是点大小,合法值是0-1-2-3。含义为自动，小中大。default是大。
		public byte DPIModel; // 第四个Byte是分辨率,合法值是1-2-3-4-5。含义为720,360,540,270,1440。default是360。
		public ushort StartPos; // 第五六个Byte是起始位置,单位是上面DPI的一个点。
    
		//only for Y Step calibration.
		public byte MediaType;//第七个Byte是介质类型，暂时定义为,  Index: 0:GlossPaper, 1:Vinyl, 2:PP, 3:Film, 4:Other1, 5:Other2, 6:Other3.
		public byte PassNum;//第八个Byte是Pass number.从1~32.
 
		public byte Option;           //第九个Byte是一个额外参数。可以灵活使用。default是0. 
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 0)]
	public struct CaliConfig
	{
		public byte len;
		public byte version;
		public byte BaseColor; //color code. it define the title color. 
		//and, for most calibration, it define calibration base color. current is K.
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=2)]
		public byte[] YStepColor; //current is K,M
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=59)]
		public 	byte[] reserved;
	}

	//	EPSON need the more factory data. So, FactoryWrite and WriteBoardConfig tools must change.
	//#define COLORCODE_K 'K'
	//#define COLORCODE_C 'C'
	//#define COLORCODE_M 'M'
	//#define COLORCODE_Y 'Y'
	//
	//#define MAX_NAME_LEN 16
	//#define FACTORYDATA_EX_VER 0x1
	//#define FACTORYDATA_EX_MASK_HEADDIR_IS_POS  0x1
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 0)]
	public struct EPR_FactoryData_Ex
	{
		public 	byte len;  //current len is sizeof(struct EPR_FactoryData_Ex)
		public 	byte version;   //current version is 0x02
 
		public 	ushort m_nXEncoderDPI;  //720/600 DPI.
		public 	int m_nBitFlagEx; //Bit0 : bHeadDirIsPositive, current dir is positive. Bit1 : bIsWeakSoventPrinter, default is 0.
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=8)]
		public 	byte[] m_nColorOrder; //content is color code.
		//有可能超过喷头自身的拼插数。
		// 存在三种情况。
		// YInterleaveNum 小于 喷头自身的拼插数。是一头两色的例子，如，EPSON 1头八色。
		// YInterleaveNum 等于 喷头自身的拼插数。是普通的例子，如 EPSON 2头八色（前四色，后四色）。
		// YInterleaveNum 大于 喷头自身的拼插数。是喷头拼插的例子，如 EPSON 2头4色，Y向拼插成720DPI。
		public 	byte YInterleaveNum; //Ｙ向拼插的数目。对于EPSON，双四色打印机为2; 8色机器为1. 对于ricoh，应该为2.
		//factoryDataEx.LayoutType bit0 == 0, 在X方向的坐标，先Ycontinue，再Yinterleave。
		//factoryDataEx.LayoutType bit0 == 1, 在X方向的坐标，先Yinterleave，再Ycontinue。
		//  内部喷头编号总是，先Ycontinue，再Yinterleave。
		//factoryDataEx.LayoutType bit1 == 1, 对称色序。Y interleave 必须是2的倍数。2012-7-2: still not support fully.
		//factoryDataEx.LayoutType bit2，对称色序的对称方式。当允许多种对称方式的时候（如，EPSON Y 拼插成720DPI)，
		//  0，以最外层为对称单位（在不同的头上颜色是对称的）。1，以最内层为对称单位（在一个头内部颜色是对称的）。
		//  方式0，在消除因覆盖关系导致的色差，效果应该更好。方式1，更容易拼插，两个颜色之间的距离比较窄。
		//factoryDataEx.LayoutType bit3 == 0, Ycontinue group 在X方向是回绕。通常，回绕更节省空间。
		// 例如，EPSON4H/4C，在X向位置只有两个位置，13在一个位置，24在一个位置。34的位置回绕了。
		public 	byte LayoutType;   //reserved for special layout. default is 0. 0 means PrintHead is Y continue. 
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_NAME_LEN)]
		public 	byte[] ManufacturerName;
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_NAME_LEN)]
		public 	byte[] PrinterName;

		//for ricoh. 打印机的最大的组数。可以由用户关闭其中的若干组。
		public 	byte MaxGroupNumber; 
		public 	byte Vsd2ToVsd3;      //VSD mode
		public 	byte Vsd2ToVsd3_ColorDeep; //ColorDeep
		public 	byte Only_Used_1head; //是否开启单头打印
		public 	byte Mask_head_used;//那个头是打印头，1: head1，2: head2,  4: head3, 8: head4, 等等

		public 	byte reserved;

		public EPR_FactoryData_Ex(SPrinterProperty sp)
		{
			len = (byte)Marshal.SizeOf(typeof(EPR_FactoryData_Ex));
			version = 0x01;
			m_nXEncoderDPI = 720;
			m_nBitFlagEx = 0;
			m_nColorOrder = new byte[8];
			byte[] deforder = new byte[4];
            if (sp.EPSONLCD_DEFINED)
			{
				deforder = new byte[]{75,67,77,89};	//kcmy		
			}
			else
			{
				deforder = new byte[]{89,77,67,75};	//ymck		
			}
			Buffer.BlockCopy(deforder,0,m_nColorOrder,0,deforder.Length);
			YInterleaveNum =2;
			LayoutType = 0;
			ManufacturerName = new byte[CoreConst.MAX_NAME_LEN];
			PrinterName = new byte[CoreConst.MAX_NAME_LEN];
			reserved = 0;
			MaxGroupNumber = 2;
			Vsd2ToVsd3 = 2;
			Vsd2ToVsd3_ColorDeep = 3;
			Only_Used_1head = 0;
			Mask_head_used = 1;
		}
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 0)]
	public struct CLEANPARA
	{
		public 	byte structsize;
		public 	byte flash;    // 1: 是否自动闪喷，0/1=否/是
		public 	byte pause_gohome; // 2: 暂停后回原点,0/1=否/是
		public 	ushort flash_interval;  // 3-4: 闪喷间隔，以毫秒为单位
		public 	byte longflash_beforeprint;  // 5: 打印前回原点后是否猛喷一下
		public 	byte autoClean_passInterval;     // 6: 每多少个 Pass 自动清洗一次喷头
		public 	byte autoCleanTimes;     // 7: 每次清洗喷头时清洗几下，最小值2
		public 	byte manualCleanTimes;  //8: 手动清洗次数 
		public 	byte longflash_passInterval;     // 9: 每多少个 Pass 自动猛喷一次
		public 	byte blowInk_passInterval;    // 10: 每多少个 Pass 自动压墨一次
		public 	ushort flashTimes;             //11,12 闪喷的次数；B 系统用这个, A不用
 
		public 	ushort pauseIntervalAfterClean;    //13, 14清洗后小车停顿时间
		public 	ushort pauseIntervalAfterBlowInk;  //15, 16猛喷的时间
		public 	byte paraFlag;  //17: 0 means the follwoing 2 parameters will use FW setting 
//#if HEAD_EPSON_GEN5
		public 	byte autoClean_way; //0~5:default/customized/strong/normal/weak/refill.  only for AllWIn epson
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 45)]
        public byte[] reserve;//[64 - 19];   // 19-63: 保留

#if false    
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=46)]
		public 	byte reserve;//[64 - 18];   // 19-63: 保留
#endif
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 0)]
	public struct USB_RPT_MainUI_Param
	{
		public float PrintOrigin; //unit: inch
 
		//the following is valid only when PM send to FW.
		public int PassNum; //指明步进修正对应的PASS数。
		public int StepModify; //用于主UI上的步进修正。
	};

	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 0)]
	public struct USB_Print_Quality
	{
		//Index: 0, OFF; 1, ECLOSION type1(gradient); 2, ECLOSION type2(Wave). 具体羽化参数由PM决定。这是唯一的由PM保存的参数。
		public int PrintQuality;
	};

	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 0)]
	public struct USB_RPT_Media_Info
	{
		public float MediaOrigin;//unit: inch
		public float MediaWidth;//unit: inch
		public float Margin;//unit: inch
	};

	public class EpsonExAllParam
	{
		private string m_TagheadParameterPercent = "headParameterPercent";
//		private string m_TagFeatherPercent = "FeatherPercent";
		private string m_TagCaliConfig = "CaliConfig";
		private string m_TagEPR_FactoryData_Ex = "EPR_FactoryData_Ex";
		private string m_TagCLEANPARA ="CLEANPARA";
		private string m_TagUSB_RPT_MainUI_Param = "USB_RPT_MainUI_Param";
		private string m_TagUSB_Print_Quality = "USB_Print_Quality";
		private string m_TagUSB_RPT_Media_Info ="USB_RPT_Media_Info";

		[MarshalAs(UnmanagedType.ByValArray,SizeConst=4)]
		public sbyte[] headParameterPercent;//对应KCMY。顺序是K,C,M,Y.
//		public int nFeatherPercent;  //具体羽化参数由PM决定。这是唯一的由PM保存的参数。
//		public CaliPrintSetting sCaliPrintSetting;
		public CaliConfig sCaliConfig;
		public EPR_FactoryData_Ex sEPR_FactoryData_Ex;
		public CLEANPARA sCLEANPARA;
		public USB_RPT_MainUI_Param sUSB_RPT_MainUI_Param;
		public USB_Print_Quality sUSB_Print_Quality;
		public USB_RPT_Media_Info sUSB_RPT_Media_Info;

		public EpsonExAllParam()
		{
			headParameterPercent = new sbyte[4];//对应KCMY。顺序是K,C,M,Y.
//			nFeatherPercent = 0;  //具体羽化参数由PM决定。这是唯一的由PM保存的参数。
			sCaliConfig = new CaliConfig();
			sEPR_FactoryData_Ex = new EPR_FactoryData_Ex(new SPrinterProperty());
			sCLEANPARA = new CLEANPARA();
			sUSB_RPT_MainUI_Param = new USB_RPT_MainUI_Param();
			sUSB_Print_Quality = new USB_Print_Quality();
			sUSB_RPT_Media_Info = new USB_RPT_Media_Info();
		}

		unsafe public void LoadFromXml(XmlElement elem_AllParam)
		{
			XmlElement root = elem_AllParam;
			XmlElement elem_i;
			elem_i = PubFunc.GetFirstChildByName(root,m_TagheadParameterPercent);
			headParameterPercent = (sbyte[])PubFunc.SystemConvertFromXml(elem_i.OuterXml,typeof(sbyte[]));
			elem_i = PubFunc.GetFirstChildByName(root,m_TagCaliConfig);
			sCaliConfig = (CaliConfig)PubFunc.SystemConvertFromXml(elem_i.OuterXml,typeof(CaliConfig));
			elem_i = PubFunc.GetFirstChildByName(root,m_TagEPR_FactoryData_Ex);
			sEPR_FactoryData_Ex = (EPR_FactoryData_Ex)PubFunc.SystemConvertFromXml(elem_i.OuterXml,typeof(EPR_FactoryData_Ex));
			elem_i = PubFunc.GetFirstChildByName(root,m_TagCLEANPARA);
			sCLEANPARA = (CLEANPARA)PubFunc.SystemConvertFromXml(elem_i.OuterXml,typeof(CLEANPARA));
			elem_i = PubFunc.GetFirstChildByName(root,m_TagUSB_RPT_MainUI_Param);
			sUSB_RPT_MainUI_Param = (USB_RPT_MainUI_Param)PubFunc.SystemConvertFromXml(elem_i.OuterXml,typeof(USB_RPT_MainUI_Param));
			elem_i = PubFunc.GetFirstChildByName(root,m_TagUSB_Print_Quality);
			sUSB_Print_Quality = (USB_Print_Quality)PubFunc.SystemConvertFromXml(elem_i.OuterXml,typeof(USB_Print_Quality));
			elem_i = PubFunc.GetFirstChildByName(root,m_TagUSB_RPT_Media_Info);
			sUSB_RPT_Media_Info = (USB_RPT_Media_Info)PubFunc.SystemConvertFromXml(elem_i.OuterXml,typeof(USB_RPT_Media_Info));
		}
		public void SaveToXml(SelfcheckXmlDocument doc)
		{
			XmlElement root;
			root = doc.CreateElement("","EpsonExAllParam","");
			doc.AppendChild(root);
			string xml = PubFunc.SystemConvertToXml(sCaliConfig,sCaliConfig.GetType());
			xml += PubFunc.SystemConvertToXml(sEPR_FactoryData_Ex,sEPR_FactoryData_Ex.GetType());
			xml += PubFunc.SystemConvertToXml(sCLEANPARA,sCLEANPARA.GetType());
			xml += PubFunc.SystemConvertToXml(sUSB_RPT_MainUI_Param,sUSB_RPT_MainUI_Param.GetType());
			xml += PubFunc.SystemConvertToXml(sUSB_Print_Quality,sUSB_Print_Quality.GetType());
			xml += PubFunc.SystemConvertToXml(sUSB_RPT_Media_Info,sUSB_RPT_Media_Info.GetType());
			root.InnerXml = xml;
		}
	}

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct FloraParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; //"T50" "T180"
        public uint moistYPlace; //执行清洗时Y的位置，只有T50用(要求只有T50机型才显示，即0x7cb)
        public uint moistZPlace; //保湿时Z位置
        public uint cleanZPlace; //清洗时Z位置
        public uint purgeInkTime; //压墨时间
        public uint purgeInkRecoverTime; //压墨恢复时间
        public uint suckStartPlace; //吸风开始位置，结束位置在原点(T50为Y,T180为X)
        public uint suckEndPlace; //吸风结束位置，只有T50用(要求只有T50机型才显示，即0x7cb)
        public byte bIsNeedPrepare; //是否需要喷预处理液, 0 不需要，1需要
        public byte cleanSlotNum; // 清洗槽数量,一般就是喷头排数
        public ushort doWetDelay; 			// 空闲多久，系统自动进入保湿(unit:s)
        public float speed; //喷预处理液速度 英寸/秒
        public uint preOff;					//喷预处理液位置到打印原点偏移(只有T50机型才显示)
        public uint cleanSlotSpace; // 清洗槽间距,一般为喷头间距
        public uint cleanMotorSpeed;			// T180, 清洗马达速度
        public float speedBack;  //喷完处理液回退速度 英寸/秒
        public uint scraperStart1;			// 第一排刮片起始位置
        public uint scraperStart2;			// 第二排刮片起始位置

        public FloraParam(object oo)
        {
            Flag = new char[4];
            moistYPlace = 0;
            moistZPlace = 0;
            cleanZPlace = 0;
            purgeInkTime = 0;
            purgeInkRecoverTime = 0;
            suckStartPlace = 0;
            suckEndPlace = 0;
            bIsNeedPrepare = 0;
            doWetDelay =120;
            speed = 0;
            preOff = 0;
            cleanSlotNum = 0;
            cleanSlotSpace = 0;
            cleanMotorSpeed = 0;
            speedBack = 0;
            scraperStart1 = 0;
            scraperStart2 = 0;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct WetParam
    {
        /// <summary>
        /// 'WETP'
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public char[] Flag; //!< 'WETP'

        /// <summary>
        ///  此域之后有效字节数(不包括此域与尾部预留字节
        /// </summary>
        public ushort ActiveLen; //!< 此域之后有效字节数(不包括此域与尾部预留字节)
        #region 自动保湿
        /// <summary>
        ///  自动保湿使能
        /// </summary>
        public byte Enable; //!< 自动保湿使能

        /// <summary>
        ///  bit0~2 分别表示X Y Z位置有效性，置1有效
        /// </summary>
        public byte PosMask; //!< bit0~2 分别表示X Y Z位置有效性，置1有效

        public int XPos; //!< 
        public int YPos; //!< 
        public int ZPos; //!< 

        /// <summary>
        /// 等待时间,MS
        /// </summary>
        public uint WaitTime; //!< 等待时间,MS
        #endregion

        #region 自动回缺省位置
        /// <summary>
        /// 回缺省位置延时使能
        /// </summary>
        public byte DefaultBackDelayEnable;
        /// <summary>
        ///  bit0~2 分别表示X Y Z位置有效性，置1有效
        /// </summary>
        public byte DefaultBackPosMask;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] rev;
        public int DefaultBackXPos; //!< 
        public int DefaultBackYPos; //!< 

        /// <summary>
        /// 等待时间,MS
        /// </summary>
        public uint BackDefaultPosWaitTime; //!< 等待时间,MS
        #endregion

        public int cover4Place; //封头动作封头轴移动位置//奥德利
    }

    /// <summary>
    /// 卓展光油专用参数
    /// </summary>
    public struct ZhuoZhanParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; //
        public uint zWorkPos; //!< Z2上升的位置
    }

    public struct YStepSpeedParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; //
        public int ySpeed;   
    }

    public struct LingFengParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; //
        //public byte ZEnable;
        public byte WetEnable;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 57)]
        public byte[] rev;
    }

    /// <summary>
    /// 普奇喷头清洗参数
    /// </summary>
    public struct PQCleanParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; ////!< 清洗相关('P', 'Q', 'C', '\0')
        public int PressInkTime;		//!< 压墨时间 IO切换正压
        public int XStartPos;		//!< x 清洗起始位置
        public int YDistance;		//!< x 清洗轴行程
        public int XAdjust;		//!< Z清洗高度
        public byte CleanNum;		//!< 清洗排数
        public byte bAutoCap; //是否自动保湿
        public byte ScrapTimes; //刮墨次数
        public byte rev;
        public int CleanZPos; //墨栈清洗位置
        public int WetZPos; //墨栈保湿位置
        public int AutoCapWaitTime;		//!< 自动保湿等待时间(s)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (62-36))]
        public byte[] Reserve;
    }

    /// <summary>
    /// Gma喷头清洗参数
    /// </summary>
    public struct GmaCleanParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; ////!< 清洗相关 GZ_CLOTH_M_CLEAN	MAKE_4CHAR_CONST_BE('C', 'L', 'C', 'N')
        public int XStartPos;		//!< x 清洗起始位置
        public int XDistance;		//!< x 清洗每排间距
        public int ZCleanPos;		//!< Z清洗高度
        public int ScraperPos;		//!< 刮片距离
        public byte CleanRowNum;		//!< 清洗排数
        public byte CleanTime;		//!< 清洗次数
        public ushort rev_54;
        public int ZCarryCleanPos;//下车Z清洗高度
        public int YCarryCleanPos;//下车Y清洗位置
    }

    /// <summary>
    /// 深圳润天智 自动/手动上料
    /// </summary>
    public struct FloraFlatParam_tag {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag;            // 'FFUF'
        public UInt32 Yadjust_Auto;    //自动上下料时机械臂Y偏移（脉冲）
        public UInt32 Yadjust_Manual;  //手动上下料时机械臂Y偏移（脉冲）
        public byte BAutoLoadDeal;             //是否打印时自动上下料，0否1是
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] rev;
    }

    public struct HlcCleanParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; ///MAKE_4CHAR_CONST_BE('H', 'L', 'C', 'N')
        public int PressInkTime;		//!< 压墨时间 IO切换正压
        public int RecoveryInkTime;		//!< 压墨后恢复时间 IO切换负压
        public int XPressInkPos;		//!< 清洗压墨X位置
        public int ZCleanPos;			//!< 清洗Z高度
        public int YCleanPos;			//!< 清洗Y行程
        public int YCleanSpeed;			//!< 清洗Y速度

    }
    /// <summary>
    /// 喷头布局（白，彩，亮）
    /// </summary>
    public enum WhiteVarnishLayout
    {
        WFCMVN,		//白远彩中亮近
        WNCMVF,		//白近彩中亮远
        WFCMVF,		//白远彩中亮远
        WNCFVN,		//白近彩远亮近
        CWC,        //彩白彩
        WCC,        //白彩彩
    }

    public class EpsonLCD
	{
		public const int MaxGroupNumber = 8;
				
		/// <summary>
		/// 打印DotCheck
		///通过USB EP0。方向是OUT。
		///reqCode是0x7F。index是2，value是0。
		///第一个Byte是VSD Model,合法值是1-4。
		///第二个Byte是分辨率,合法值是1-2-3。含义为720,360,240。default是360。
		///第三四个Byte是起始位置,单位是上面DPI的一个点。
		/// </summary>
		public static void PrintDotCheck(CaliPrintSetting cps)
		{
#if false
			byte[] subval = new byte[9];
			uint bufsize = (uint)subval.Length;
			subval[0] =(byte)(cps.type);
			subval[1] =(cps.VSDModel);
			subval[2] =cps.DotSetting;
			subval[3] =(cps.DPIModel);
			byte[] startp = BitConverter.GetBytes(cps.StartPos);
			Buffer.BlockCopy(startp, 0, subval, 4, 2);
			subval[6] = cps.MediaType;
			subval[7] = (cps.PassNum);
			subval[8] = cps.Option;
			if(CoreInterface.SetEpsonEP0Cmd(0x7F,subval,ref bufsize,3,0) == 0)
			{
				Debug.Assert(false,"SetPrint_Quality fialed!");
			}
#endif
		}

		public static void GetCalibrationSetting(ref SCalibrationSetting scali,bool isForDisplay)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(SCalibrationSetting))+4];

			uint tatolLen = (uint)val.Length;
			ushort adr = 0;
			uint maxLenonetime = 64;
			while (tatolLen > 0)
			{
				byte[] subval = new byte[maxLenonetime];
				uint readlen = maxLenonetime;
				if (tatolLen < maxLenonetime)
					readlen = tatolLen;
				subval = new byte[readlen];
				
				bool bfiald = false;
				if(isForDisplay)
					bfiald = CoreInterface.GetEpsonEP0Cmd(0x7F,subval,ref readlen,12,adr) == 0; 
				else
					bfiald = CoreInterface.GetEpsonEP0Cmd(0x7F,subval,ref readlen,8,adr) == 0; 

				if(bfiald)
				{
					Debug.Assert(false,"GetCalibrationSetting fialed!");
					break;
				}				
//				Thread.Sleep(1000);
//				subval = new byte[readlen];
				Buffer.BlockCopy(subval, 0, val, adr, subval.Length);
				adr += (ushort)subval.Length;
				tatolLen -= (ushort)subval.Length;
			}

			byte[] subval1 = new byte[Marshal.SizeOf(typeof(SCalibrationSetting))];
			Buffer.BlockCopy(val,4,subval1,0,subval1.Length);
            scali = (SCalibrationSetting)SerializationUnit.BytesToStruct(subval1, typeof(SCalibrationSetting));
		}

		public static void SetCalibrationSetting(SCalibrationSetting scali)
		{
#if false
			byte[] val = new byte[Marshal.SizeOf(typeof(SCalibrationSetting))+4];
			uint bufsize = (uint)val.Length;
			byte[] lenbuf = BitConverter.GetBytes((ushort)bufsize);
			Buffer.BlockCopy(lenbuf,0,val,0,lenbuf.Length);
			byte[] versionbuf = BitConverter.GetBytes((ushort)0x101);
			Buffer.BlockCopy(versionbuf,0,val,2,versionbuf.Length);
			byte[] subval = SerializationUnit.StructToBytes(scali);
			Buffer.BlockCopy(subval,0,val,4,subval.Length);

			uint totalsize = bufsize;
			ushort addr = 0;
			while (totalsize > 0)
			{
				uint size = 64;
				if (totalsize < size)
					size = totalsize;
				totalsize -= size;

				byte[] Buf = new byte[size];
				Buffer.BlockCopy(val, (int)addr, Buf, 0, (int)size);
				if (CoreInterface.SetEpsonEP0Cmd(0x7F,Buf,ref size,12,addr)==0)
				{
					Debug.Assert(false,"SetCalibrationSetting fialed!");
					return;
				}
//				Thread.Sleep(1000);
				addr += (ushort)(size);
			}
#endif
		}

		public static void GetCaliConfig(ref CaliConfig cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(CaliConfig))];
			uint bufsize = (uint)val.Length;
			if(CoreInterface.GetEpsonEP0Cmd(0x7F,val,ref bufsize,7,0) == 0)
			{
				Debug.Assert(false,"GetCaliConfig fialed!");
			}
            cc = (CaliConfig)SerializationUnit.BytesToStruct(val, typeof(CaliConfig));
		}

        /// <summary>
        /// 工正针对【KM512_8H_600_Roll_solvent_5.1_VE_0x0B】改变了热文件的原有逻辑，只有检测到有纸才打印,其他的还走之前的逻辑
        /// </summary>
        /// <returns></returns>
        public static bool CheckHasPaper()
        {
            if (SPrinterProperty.IsGongZengNew())
            {
                byte[] val=new byte[3];
                uint bufsize=(uint)val.Length;
                if (CoreInterface.GetEpsonEP0Cmd(0x82, val, ref bufsize, 0, 6) == 0)
                {
                    return true;
                }
                try
                {
                    BitArray bit = new BitArray(BitConverter.GetBytes(val[2]));
                    return !bit.Get(4);
                }
                catch 
                {
                    return true;
                }
            }
            //其他的走之前的热文件逻辑
            return true;
        }


		public static void SetCaliConfig(CaliConfig cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(CaliConfig))];
			uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
			if(CoreInterface.SetEpsonEP0Cmd(0x7F,val,ref bufsize,7,0) == 0)
			{
				Debug.Assert(false,"SetCaliConfig fialed!");
			}
		}

		public static int GetEPR_FactoryData_Ex(ref EPR_FactoryData_Ex cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(EPR_FactoryData_Ex))];
			uint bufsize = (uint)val.Length;
			int ret =CoreInterface.GetEpsonEP0Cmd(0x7F,val,ref bufsize,6,0);
			if( ret == 0)
			{
				Debug.Assert(false,"GetEPR_FactoryData_Ex fialed!");
			}
            cc = (EPR_FactoryData_Ex)SerializationUnit.BytesToStruct(val, typeof(EPR_FactoryData_Ex));
			return ret;
		}

		public static int SetEPR_FactoryData_Ex(EPR_FactoryData_Ex cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(EPR_FactoryData_Ex))];
			uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
			int ret = CoreInterface.SetEpsonEP0Cmd(0x7F,val,ref bufsize,6,0);
			if(ret == 0)
			{
				Debug.Assert(false,"SetEPR_FactoryData_Ex fialed!");
			}
			return ret;
		}
		
		/// <summary>
		/// cmd = 0x64;value = 0;index = 0; 
		/// </summary>
		/// <param name="cc"></param>
		/// 
		public static void GetCleaningOption(ref CLEANPARA cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(CLEANPARA))];
			uint bufsize = (uint)val.Length;
			if(CoreInterface.GetEpsonEP0Cmd(0x64,val,ref bufsize,0,0) == 0)
			{
				Debug.Assert(false,"GetCleaningOption fialed!");
			}
            cc = (CLEANPARA)SerializationUnit.BytesToStruct(val, typeof(CLEANPARA));
		}

		/// <summary>
		/// cmd = 0x64;value = 0;index = 0; 
		/// </summary>
		/// <param name="cc"></param>
		public static void SetCleaningOption(CLEANPARA cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(CLEANPARA))];
			uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
			if(CoreInterface.SetEpsonEP0Cmd(0x64,val,ref bufsize,0,0) == 0)
			{
				Debug.Assert(false,"SetCleaningOption fialed!");
			}
		}

		/// <summary>
		/// read by EP6:
		///通过USB EP6。方向是IN。
		///#define EP6_CMD_T_MAINUI        0x2 //see struct USB_RPT_MainUI_Param
		///read by EP0:
		///通过USB EP0。方向是IN。
		///reqCode是0x7F。value是9。
		/// </summary>
		/// <param name="cc"></param>
		public static void GetMainUI_Param(ref USB_RPT_MainUI_Param cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(USB_RPT_MainUI_Param))];
			uint bufsize = (uint)val.Length;
			if(CoreInterface.GetEpsonEP0Cmd(0x7F,val,ref bufsize,9,0) == 0)
			{
				Debug.Assert(false,"GetMainUI_Param fialed!");
			}
			cc = (USB_RPT_MainUI_Param)SerializationUnit.BytesToStruct(val,typeof(USB_RPT_MainUI_Param));
		}

		/// <summary>
		/// two items at mainUI need refresh: Print Origin and step modify.
		///Set by EP0:
		///通过USB EP0。方向是OUT。
		///reqCode是0x7F。value是9。
		/// </summary>
		/// <param name="cc"></param>
		public static void SetMainUI_Param(USB_RPT_MainUI_Param cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(USB_RPT_MainUI_Param))];
			uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
			if(CoreInterface.SetEpsonEP0Cmd(0x7F,val,ref bufsize,9,0) == 0)
			{
				Debug.Assert(false,"SetMainUI_Param fialed!");
			}

		}

		/// <summary>
		///read by EP0:
		///通过USB EP0。方向是IN。
		///reqCode是0x7F。value是10
		/// </summary>
		/// <param name="cc"></param>
		public static void GetPrint_Quality(ref USB_Print_Quality cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(USB_Print_Quality))];
			uint bufsize = (uint)val.Length;
			if(CoreInterface.GetEpsonEP0Cmd(0x7F,val,ref bufsize,10,0) == 0)
			{
				Debug.Assert(false,"GetPrint_Quality fialed!");
			}
			cc = (USB_Print_Quality)SerializationUnit.BytesToStruct(val,typeof(USB_Print_Quality));
		}

		/// <summary>
		/// Set by EP0:
		///通过USB EP0。方向是OUT。
		///reqCode是0x7F。value是10。
		/// </summary>
		/// <param name="cc"></param>
		public static void SetPrint_Quality(USB_Print_Quality cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(USB_Print_Quality))];
			uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
			if(CoreInterface.SetEpsonEP0Cmd(0x7F,val,ref bufsize,10,0) == 0)
			{
				Debug.Assert(false,"SetPrint_Quality fialed!");
			}
			
		}
		
		/// <summary>
		/// 		read by EP6: 
		///通过USB EP6。方向是IN。
		///read by EP0:
		///通过USB EP0。方向是IN。
		///reqCode是0x7F。value是11。
		/// </summary>
		/// <param name="cc"></param>
		public static void GetMedia_Info(ref USB_RPT_Media_Info cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(USB_RPT_Media_Info))];
			uint bufsize = (uint)val.Length;
			if(CoreInterface.GetEpsonEP0Cmd(0x7F,val,ref bufsize,11,0) == 0)
			{
				Debug.Assert(false,"GetMedia_Info fialed!");
			}
			cc = (USB_RPT_Media_Info)SerializationUnit.BytesToStruct(val,typeof(USB_RPT_Media_Info));
		}

		/// <summary>
		/// Set by EP0:
		///通过USB EP0。方向是OUT。
		///reqCode是0x7F。value是11。
		/// </summary>
		/// <param name="cc"></param>
		public static void SetMedia_Info(USB_RPT_Media_Info cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(USB_RPT_Media_Info))];
			uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
			if(CoreInterface.SetEpsonEP0Cmd(0x7F,val,ref bufsize,11,0) == 0)
			{
				Debug.Assert(false,"SetMedia_Info fialed!");
			}
		}

		/// <summary>
		/// 在ＰＭ中，ＥＰＳＯＮ有自己专门的ｈｅａｄparameter. 它类似别的喷头的电压调整。只不过是一个调整百分比，取值范围从-50到+50.
		///b. Get EPSON head parameter.
		///reqCode是0x5C。index是8，长度是6字节。内容是，
		///第一个字节是0；
		///第二个字节是0x5C;
		///接下来是，
		///INT8S headParameterPercent[4];  //对应KCMY。顺序是K,C,M,Y.
		/// </summary>
		/// <param name="cc"></param>
		public static void GetHeadparameter(ref sbyte[] cc)
		{
			cc = new sbyte[4];
			byte[] val = new byte[6];
			uint bufsize = (uint)val.Length;
			if(CoreInterface.GetEpsonEP0Cmd(0x5C,val,ref bufsize,0,8) != 0)
				Buffer.BlockCopy(val,2,cc,0,cc.Length);
		}

		/// <summary>
		/// a. set  EPSON head parameter. 这个参数会保存在EPSON头板上。
		///reqCode是0x5C。index是8，长度是4字节。内容是
		///INT8S headParameterPercent[4];  //对应KCMY。顺序是K,C,M,Y.
		/// </summary>
		/// <param name="cc"></param>
		public static void SetHeadparameter(sbyte[] cc)
		{
			uint bufsize = (uint)cc.Length;
			byte[] val = new byte[bufsize];
			Buffer.BlockCopy(cc,0,val,0,cc.Length);
			if(CoreInterface.SetEpsonEP0Cmd(0x5C,val,ref bufsize,0,8) == 0)
			{
				Debug.Assert(false,"SetHeadparameter fialed!");
			}
		}

		public static void GetSTEP(ref int nstep,ref int npass)
		{
			byte[] val = new byte[3];
			uint bufsize = (uint)val.Length;
			if(CoreInterface.GetEpsonEP0Cmd(0x7D,val,ref bufsize,0,0) != 0)
			{
				nstep = (int)BitConverter.ToInt16(val,0);
				npass = (int)val[2];
			}
		}

		public static void SaveEpsonCaliParaToFile(string path)
		{
			string msginfo = string.Empty;
			try
			{
				byte[] len = new byte[2];
				uint rlen = (uint)len.Length;
				int ret = CoreInterface.GetEpsonEP0Cmd(0x7F,len,ref rlen,15,0);
				if(ret != 0)
				{
					rlen = (uint)BitConverter.ToInt16(len,0);
					byte[] val = new byte[rlen];

					uint tatolLen = (uint)val.Length;
					ushort adr = 0;
					uint maxLenonetime = 64;
					while (tatolLen > 0)
					{
						byte[] subval = new byte[maxLenonetime];
						uint readlen = maxLenonetime;
						if (tatolLen < maxLenonetime)
							readlen = tatolLen;
						subval = new byte[readlen];
				
						bool bfiald = CoreInterface.GetEpsonEP0Cmd(0x7F,subval,ref readlen,16,adr) == 0; 

						if(bfiald)
						{
							msginfo = ResString.GetResString("EpsonLoadCali_Faild");
							return;
						}				
						//				Thread.Sleep(1000);
						//				subval = new byte[readlen];
						Buffer.BlockCopy(subval, 0, val, adr, subval.Length);
						adr += (ushort)subval.Length;
						tatolLen -= (ushort)subval.Length;
					}

					FileStream fs = new FileStream(path,FileMode.Create,FileAccess.Write,FileShare.Read);
					fs.Write(val,0,val.Length);
					fs.Flush();
					fs.Close();
					msginfo = ResString.GetResString("EpsonSaveCali_Ok");
				}
				else
				{
					msginfo = ResString.GetResString("EpsonLoadCali_GetSizeFaild");
				}
			}
			catch(Exception ex)
			{
				msginfo = ex.Message;
			}
			finally
			{
				MessageBox.Show(msginfo,ResString.GetProductName(),MessageBoxButtons.OK);				
			}
		}
        public static bool IsNewDurationInkCmd() {
            byte[] val = new byte[64];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x54, val, ref bufsize, 0, 2) == 0) {
                return false;
            }
            return ((val[4] & 0x40) > 0);
        }

        public static bool GetDurationInk(ref long[] result, int mbid = CoreConst.DefaultMbid) {
            byte[] val = new byte[66];
            uint bufsize = (uint)val.Length;
            result = new long[8];
            if (CoreInterface.GetEpsonEP0Cmd(0x52, val, ref bufsize, 2, 1) == 1) {
                for (int i = 0; i < 8; i++) {
                    result[i] = BitConverter.ToInt64(val, 2 + 8 * i);
                }
                return true;
            } else {
                return false;
            }
        }

		public static void LoadEpsonCaliParaFromFile(string path)
		{
			
			string msginfo = string.Empty;
			try
			{
				byte[] len = new byte[2];
				uint rlen = (uint)len.Length;
				int ret = CoreInterface.GetEpsonEP0Cmd(0x7F,len,ref rlen,15,0);
				if(ret != 0)
				{
					rlen = (uint)BitConverter.ToInt16(len,0);
					FileStream fs = new FileStream(path,FileMode.Open,FileAccess.ReadWrite,FileShare.ReadWrite);
					//				if( fs.Length != rlen)
					//				{
					//					MessageBox.Show("file format diamatch with current setting.");
					//					fs.Close();
					//					return;
					//				}
					byte[] val = new byte[rlen];
					fs.Read(val,0,(int)fs.Length);
					fs.Close();
					uint bufsize = (uint)val.Length;
					uint totalsize = bufsize;
					ushort addr = 0;
					while (totalsize > 0)
					{
						uint size = 64;
						if (totalsize < size)
							size = totalsize;
						totalsize -= size;

						byte[] Buf = new byte[size];
						Buffer.BlockCopy(val, (int)addr, Buf, 0, (int)size);
						if (CoreInterface.SetEpsonEP0Cmd(0x7F,Buf,ref size,16,addr)==0)
						{
							msginfo = ResString.GetResString("EpsonLoadCali_Faild");
							return;
						}
						//				Thread.Sleep(1000);
						addr += (ushort)(size);
					}
					byte[] retbuf = new byte[64];
					uint buflen = (uint)retbuf.Length;
					if(CoreInterface.GetEpsonEP0Cmd(0x7F,retbuf,ref buflen,15,2)!=0)
					{
						/*
						 *  接收到 0  传输成功
			1   升级的版本低于基础版本
			2   升级的版本不支持，高于当前最高版本       
			3    setuplength错误 
	*/
						byte retsult = retbuf[0];
						switch(retsult)
						{
							case 0:
								msginfo = ResString.GetResString("EpsonLoadCali_Ok");
								break;
							case 1:
								msginfo = ResString.GetResString("EpsonLoadCali_VerTooOld");
								break;
							case 2:
								msginfo = ResString.GetResString("EpsonLoadCali_VerTooLatest");
								break;
							case 3:
								msginfo = ResString.GetResString("EpsonLoadCali_SizeError");
								break;
							case 4:
								msginfo = ResString.GetResString("EpsonLoadCali_FlagError");//flag错误 
								break;
						}
					}
					else
					{
						msginfo = ResString.GetResString("EpsonLoadCali_GetRetFaild");
					}
				}
				else
				{
					msginfo = ResString.GetResString("EpsonLoadCali_GetSizeFaild");
				}
			}
			catch(Exception ex)
			{
				msginfo = ex.Message;
			}
			finally
			{
				MessageBox.Show(msginfo,ResString.GetProductName(),MessageBoxButtons.OK);				
			}
		}
		public static int PrepareReadBaiscWave(ushort id)
		{
			byte[] val = new byte[1];
			uint bufsize = (uint)val.Length;
			int ret = CoreInterface.SetEpsonEP0Cmd(0x7F, val, ref bufsize, 25, id);
			if (ret == 0)
			{
				throw new Exception("Prepare read baisc wave fialed! cmd is 0x7F, value is 25");
			}
			return ret;
		}

		public static int GetWaveDataTotalLength()
		{
			return GetLength(22);
		}
		/// <summary>
		/// 读取波形数据总长度
		/// </summary>
		public static int GetWaveNameTotalLength()
		{
			return GetLength(34);
		}
		/// <summary>
		/// 读取通道名字总长度
		/// </summary>
		public static int GetChannelNameTotalLength()
		{
			return GetLength(35);
		}
		/// <summary>
		/// 读取映射表总长度
		/// </summary>
		public static int GetWaveMappingTotalLength()
		{
			return GetLength(36);
		}

		private static int GetLength(ushort Cmd)
		{
			byte[] subval = new byte[4];
			uint bufsize = (uint)subval.Length;
			ushort index = 0;
			if (CoreInterface.GetEpsonEP0Cmd(0x7F, subval, ref bufsize, Cmd, index) == 0)
			{
				//throw new Exception(string.Format("GetEpsonEP0Cmd Failed. cmd is 0x7F, value is {0}", Cmd));
                MessageBox.Show(string.Format("GetEpsonEP0Cmd Failed. cmd is 0x7F, value is {0}", Cmd),ResString.GetProductName(), MessageBoxButtons.YesNo, MessageBoxIcon.Error);
			}
			return BitConverter.ToInt32(subval, 0);
		}


		public static byte[] GetWaveData(int Length)
		{
			return GetWaveInfo(Length, 22);
		}
		/// <summary>
		/// 读取波形数据
		/// </summary>
		public static byte[] GetWaveNameData(int Length)
		{
			return GetWaveInfo(Length, 34);
		}
		/// <summary>
		/// 读取通道数据
		/// </summary>
		public static byte[] GetChannelNameData(ushort index)
		{
			byte[] subval = new byte[20];
			uint bufsize = (uint)subval.Length;
			if (CoreInterface.GetEpsonEP0Cmd(0x7F, subval, ref bufsize, 35, index) == 0)
			{
				throw new Exception("GetEpsonEP0Cmd Failed. cmd is 0x7F, value is 35");
			}

			return subval;
		}
		/// <summary>
		/// 读取映射表数据
		/// </summary>
		public static byte[] GetWaveMappingData(int Length)
		{
			return GetWaveInfo(Length, 36);
		}

		private static byte[] GetWaveInfo(int Length, ushort Cmd)
		{
			byte[] WaveData = new byte[Length];

			int PacketCount = (int)Math.Ceiling((double)(float)Length / 25.0f);

			int ByteLeft = Length;
			int ByteReceive = 0;
			for (ushort i = 1; i <= PacketCount; i++)
			{
				int ReceiveCount = ByteLeft >= 25 ? 25 : ByteLeft;
				byte[] subval = new byte[ReceiveCount];

				uint bufsize = (uint)subval.Length;
				if (CoreInterface.GetEpsonEP0Cmd(0x7F, subval, ref bufsize, Cmd, i) == 0)
				{
					throw new Exception(string.Format("GetEpsonEP0Cmd Failed. cmd is 0x7F, value is {0}", Cmd));
				}
				else
				{
					Array.Copy(subval, 0, WaveData, ByteReceive, subval.Length);

					ByteLeft -= ReceiveCount;
					ByteReceive += ReceiveCount;
				}
			}

			return WaveData;
		}

		public static int SetWaveDataTotalLength(int Length)
		{
			return SetLength(Length, 26);
		}

		public static int SetAmplitudeTotalLength(int Length)
		{
			return SetLength(Length, 27);
		}

		public static int SetNewAddNodeTotalLength(int Length)
		{
			return SetLength(Length, 28);
		}
		/// <summary>
		///  波形名字修改总长度设定
		/// </summary>
		public static int SetWaveNameTotalLength(int Length)
		{
			return SetLength(Length, 29);
		}
		/// <summary>
		/// 波形映射表总长度设定
		/// </summary>
		public static int SetWaveMappingTotalLength(int Length)
		{
			return SetLength(Length, 43);
		}

		public static int WriteWaveData(byte[] WaveData)
		{
			return WriteWaveInfo(WaveData, 26);
		}

		public static int WriteAmplitudeData(byte[] AmplitudeData)
		{
			return WriteWaveInfo(AmplitudeData, 27);
		}

		public static int WriteNewAddNodeData(byte[] Data)
		{
			return WriteWaveInfo(Data, 28);
		}

		/// <summary>
		/// 波形名字修改数据下发
		/// </summary>
		public static int WriteWaveNameData(byte[] Data)
		{
			return WriteWaveInfo(Data, 29);
		}
		/// <summary>
		/// 波形映射表数据下发
		/// </summary>
		public static int WriteWaveMappingData(byte[] Data)
		{
			return WriteWaveInfo(Data, 43);
		}

		private static int WriteWaveInfo(byte[] Info, ushort Cmd)
		{
			int PacketCount = (int)Math.Ceiling((double)(float)Info.Length / 25.0f);

			int ByteLeft = Info.Length;
			int ByteSent = 0;
			for (ushort i = 1; i <= PacketCount; i++)
			{
				int SendDataCount = ByteLeft >= 25 ? 25 : ByteLeft;
				byte[] subval = new byte[SendDataCount];

				Array.Copy(Info, ByteSent, subval, 0, SendDataCount);

				uint bufsize = (uint)subval.Length;
				if (CoreInterface.SetEpsonEP0Cmd(0x7F, subval, ref bufsize, Cmd, i) == 0)
				{
					throw new Exception(string.Format("SetEpsonEP0Cmd Failed. cmd is 0x7F, value is {0}", Cmd));
				}
				else
				{
					ByteLeft -= SendDataCount;
					ByteSent += SendDataCount;
				}
			}

			return Info.Length;
		}

		private static int SetLength(int Length, ushort Cmd)
		{
			byte[] subval = BitConverter.GetBytes(Length);
			uint bufsize = (uint)subval.Length;
			ushort index = 0;

			int ret = CoreInterface.SetEpsonEP0Cmd(0x7F, subval, ref bufsize, Cmd, index);
			if (ret == 0)
			{
				throw new Exception(string.Format("SetEpsonEP0Cmd failed. cmd is 0x7F, value is {0}", Cmd));
			}

			return ret;
		}
		/// <summary>
		/// 预读波形名字
		/// </summary>
		public static void PrepareReadWaveName()
		{
			PrepareReadCommand(42);
		}
		/// <summary>
		/// 预读波形映射表
		/// </summary>
		public static void PrepareReadWaveMapping()
		{
			PrepareReadCommand(44);
		}

		private static void PrepareReadCommand(ushort Cmd)
		{
			byte[] val = new byte[1];
			uint bufsize = (uint)val.Length;
			ushort index = 0;
			if (CoreInterface.SetEpsonEP0Cmd(0x7F, val, ref bufsize, Cmd, index) == 0)
			{
				throw new Exception(string.Format("SetEpsonEP0Cmd Failed. cmd is 0x7F, value is {0}", Cmd));
			}
			return;
		}


        /// <summary>
        /// 读取双轴属性
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetDoubleYAxis_Info(ref DOUBLE_YAXIS cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(DOUBLE_YAXIS))+2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x78, val, ref bufsize, 0, 1) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(DOUBLE_YAXIS))];
            DOUBLE_YAXIS myStruct = new DOUBLE_YAXIS();
            Array.Copy(val, 2, structData, 0, structData.Length);
            cc = (DOUBLE_YAXIS)SerializationUnit.BytesToStruct(structData, typeof(DOUBLE_YAXIS));
            return true;
        }

        /// <summary>
        /// 设置双轴属性
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetDoubleYAxis_Info(DOUBLE_YAXIS cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(DOUBLE_YAXIS))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x78, val, ref bufsize, 0, 1) == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Get FW Version
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
	    public static bool GetFWVersionInfo(ref byte[] cc)
	    {
            cc = new byte[16];
            byte[] val = new byte[64];
	        uint valsize = (uint) val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x54, val, ref valsize, 0, 1) == 0)
            {
                return false;
            }
            Buffer.BlockCopy(val, 2, cc, 0, cc.Length);
	        return true;
	    }

	    /// <summary>
        /// 获取新景泰打样机参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetNktParam(ref NKTParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(NKTParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x90) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(NKTParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (NKTParam)SerializationUnit.BytesToStruct(structData, typeof(NKTParam));

            bool flagCheck = true;
            char[] flag = new char[] { 'N', 'K', 'T', '\0' };
            for (int i = 0; i < cc.Flag.Length; i++)
            {
                if (cc.Flag[i] != flag[i])
                {
                    flagCheck = false;
                    break;
                }
            }
            return flagCheck;
        }

        /// <summary>
        /// 设置新景泰打样机参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetNktParam(NKTParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(NKTParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x90) == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取天龙化工清洗位置参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetTlhgParam(ref TlhgParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(TlhgParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x40) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(TlhgParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (TlhgParam)SerializationUnit.BytesToStruct(structData, typeof(TlhgParam));
            bool flagCheck = true;
            char[] flag = new char[] { 'T', 'L', 'H', 'G' };
            for (int i = 0; i < cc.Flag.Length; i++)
            {
                if (cc.Flag[i] != flag[i])
                {
                    flagCheck = false;
                    break;
                }
            }
            return flagCheck;
        }

        /// <summary>
        /// 设置天龙化工清洗位置参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetTlhgParam(TlhgParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(TlhgParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x40) == 0)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// 获取3D打印设置参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool Get3DPrint_Info(ref S_3DPrint cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(S_3DPrint)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x10) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(S_3DPrint))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (S_3DPrint)SerializationUnit.BytesToStruct(structData, typeof(S_3DPrint));

            byte[] byteflag = BitConverter.GetBytes(cc.Flag);
            if (Encoding.ASCII.GetString(byteflag) != "FeH\0")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置3D打印设置参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool Set3DPrint_Info(S_3DPrint cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(S_3DPrint))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x10) == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public static bool GetGZClothMotionParam(ref GZClothMotionParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(GZClothMotionParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0xE3) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(GZClothMotionParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (GZClothMotionParam)SerializationUnit.BytesToStruct(structData, typeof(GZClothMotionParam));

            string flag=new string(cc.Flag);
            if (flag != "CLOT")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取工正纸板机参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetGZCardboardParam(ref GzCardboardParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(GzCardboardParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 2) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(GzCardboardParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (GzCardboardParam)SerializationUnit.BytesToStruct(structData, typeof(GzCardboardParam));
            string flag = new string(cc.Flag);
            if (flag != "GZCB")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置工正纸板机参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetGZCardboardParam(GzCardboardParam cc)
        {
            cc.Flag =new char[]{'G','Z','C','B'};
            byte[] val = new byte[Marshal.SizeOf(typeof(GzCardboardParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 2) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetGZClothMotionParam(GZClothMotionParam cc)
        {
            cc.Flag = new char[] { 'C', 'L', 'O', 'T' };
            byte[] val = new byte[Marshal.SizeOf(typeof(GZClothMotionParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0xE3) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 东川、Colorjet 板卷一体机：设置成flat
        /// </summary>
        /// <returns></returns>
        public static bool SetFlat()
        {
            byte[] val = new byte[] { 0x01 };
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x82, val, ref bufsize, 0, 0x04) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 东川、Colorjet 板卷一体机：设置成roll
        /// </summary>
        /// <returns></returns>
        public static bool SetRoll()
        {
            byte[] val = new byte[] { 0x02 };
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x82, val, ref bufsize, 0, 0x04) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 东川、Colorjet 板卷一体机：获取当前模式
        /// </summary>
        /// <returns></returns>
        public static bool GetRollOrFlat(ref byte selectedType)
        {
            byte[] val = new byte[3];
            uint bufsize = (uint)val.Length;
            int ret = CoreInterface.GetEpsonEP0Cmd(0x82, val, ref bufsize, 0, 0x04);
            if (ret != 0)
            {
                if (bufsize == 3)
                {
                    selectedType = val[2];
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 收放布控制开
        /// </summary>
        /// <returns></returns>
        public static bool RetractableClothBegin()
       {
           byte[] val = new byte[] { (byte) 'B' };
           uint bufsize = (uint)val.Length;
           if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0xE4) == 0)
           {
               return false;
           }
           return true;
       }
        /// <summary>
        /// 收放布控制关
        /// </summary>
        /// <returns></returns>
       public static bool RetractableClothEnd()
       {
           byte[] val = new byte[] { (byte)'E' };
           uint bufsize = (uint)val.Length;
           if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0xE4) == 0)
           {
               return false;
           }
           return true;
       }
        /// <summary>
        /// 获取全印参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetAllprintParam(ref AllprintParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(AllprintParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x80) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(AllprintParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (AllprintParam)SerializationUnit.BytesToStruct(structData, typeof(AllprintParam));
            string flag = new string(cc.Flag);
            if (flag != "APRT")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置全印参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetAllprintParam(AllprintParam cc)
        {
            cc.Flag = new char[] { 'A', 'P', 'R', 'T' };
            byte[] val = new byte[Marshal.SizeOf(typeof(AllprintParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x80) == 0)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// 铺沙
        /// </summary>
        /// <param name="cc"></param>
        public static bool LayingSand()
        {
            byte[] val = new byte[2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x11) == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 下沙完成
        /// </summary>
        public static bool XiaShaFinish()
        {
            byte[] val = new byte[2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x12) == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取手动清洗参数for colorjet
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetManualCleanParam(ref ManualCleanParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(ManualCleanParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x20) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(ManualCleanParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (ManualCleanParam)SerializationUnit.BytesToStruct(structData, typeof(ManualCleanParam));
            string flag = Encoding.ASCII.GetString(BitConverter.GetBytes(cc.nFlag));
            if (CoreInterface.IsS_system())
            {
                if (flag != "CJCS")
                {
                    return false;
                }
                return true;
            }
            else
            {
                if (flag != "CJCA")
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 设置手动清洗参数for colorjet
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetManualCleanParam(ManualCleanParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(ManualCleanParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x20) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置手动清洗参数for colorjet ZCenter
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetManualCleanParamZCenter()
        {
            byte[] val = { 0x00 };
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x22) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置手动清洗参数for colorjet ZBottom
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetManualCleanParamZBottom()
        {
            byte[] val = {0x00};
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x23) == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 开始清洗皮带,持续一段时间输出24v
        /// </summary>
        public static bool StartCleanBelt()
        {
            byte[] val = { 0x00 };
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x25) == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 中止清洗皮带,停止输出24v
        /// </summary>
        public static bool StopCleanBelt()
        {
            byte[] val = { 0x00 };
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x26) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        ///  东川textile 初始化Z位移计参数，动作触发
        /// </summary>
        public static bool DOCANTEXTILE_SENSOR_INIT()
        {
            byte[] val = { 0x00 };
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0131) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取东川清洗机清洗参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetDocanTextileCleanParam(ref DocanTextileParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(DocanTextileParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0130) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(DocanTextileParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (DocanTextileParam)SerializationUnit.BytesToStruct(structData, typeof(DocanTextileParam));
            string flag = new string(cc.Flag);
            if (flag != "DCT\0")
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置东川清洗机清洗参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetDocanTextileCleanParam(DocanTextileParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(DocanTextileParam))];
            uint bufsize = (uint)val.Length;
            cc.Flag = new char[] { 'D', 'C', 'T', '\0' };
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0130) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取博昊清洗机清洗参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetBoHaoCleanParam(ref BoHaoParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(BoHaoParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0150) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(BoHaoParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (BoHaoParam)SerializationUnit.BytesToStruct(structData, typeof(BoHaoParam));
            string flag = Encoding.ASCII.GetString(BitConverter.GetBytes(cc.Flag));
            if (flag != "BH\0\0")
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置东川清洗机清洗参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetBoHaoCleanParam(BoHaoParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(BoHaoParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0150) == 0)
            {
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// 获取奥威清洗机清洗参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetALLWINCleanParam(ref ALLWINCleanParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(ALLWINCleanParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0110) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(ALLWINCleanParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (ALLWINCleanParam)SerializationUnit.BytesToStruct(structData, typeof(ALLWINCleanParam));
            string flag = new string(cc.Flag);
            if (flag != "AWC\0")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置奥威清洗机清洗参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetALLWINCleanParam(ALLWINCleanParam cc)
        {
            cc.Flag = new char[] { 'A', 'W', 'C', '\0' };
            cc.rev=new byte[6];
            byte[] val = new byte[Marshal.SizeOf(typeof(ALLWINCleanParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0110) == 0)
            {
                return false;
            }
            return true;
        }
        ///// <summary>
        ///// 获取乐彩清洗机清洗参数
        ///// </summary>
        ///// <param name="cc"></param>
        //public static bool GetLECAICleanParam(ref LeCaiCleanParam cc)
        //{
        //    byte[] val = new byte[Marshal.SizeOf(typeof(LeCaiCleanParam)) + 2];
        //    uint bufsize = (uint)val.Length;
        //    if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0031) == 0)
        //    {
        //        return false;
        //    }
        //    byte[] structData = new byte[Marshal.SizeOf(typeof(LeCaiCleanParam))];
        //    Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
        //    cc = (LeCaiCleanParam)SerializationUnit.BytesToStruct(structData, typeof(LeCaiCleanParam));
        //    return true;
        //}

        ///// <summary>
        ///// 设置乐彩清洗机清洗参数
        ///// </summary>
        ///// <param name="cc"></param>
        //public static bool SetLECAICleanParam(LeCaiCleanParam cc)
        //{
        //    cc.Flag = new char[] { 'L', 'C', 'C', '\0' };
        //    cc.rev = new byte[49];
        //    byte[] val = new byte[Marshal.SizeOf(typeof(LeCaiCleanParam))];
        //    uint bufsize = (uint)val.Length;
        //    val = SerializationUnit.StructToBytes(cc);
        //    if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0031) == 0)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        /// <summary>
        /// 开始乐彩清洗
        /// </summary>
        /// <param name="cc"></param>
        public static bool StartLECAIClean()
        {
            byte[] val = new byte[0];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0032) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 手动清洗
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetManualCleanCmd(byte value,byte? level=null)
        {
            if (level.HasValue)
            {
                // for 印可丽
                byte[] val = new byte[] { value,level.Value};
                uint bufsize = (uint)val.Length;
                if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x60) == 0)
                {
                    return false;
                }
            }
            else
            {
                // colorjet
                byte[] val = new byte[] { value };
                uint bufsize = (uint)val.Length;
                if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x21) == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 浙江普崎手动清洗
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetManualCleanCmd_PQ(byte value)
        {
            byte[] val = new byte[] { value };
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x141) == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 终止手动清洗
        /// </summary>
        public static bool StopManualClean()
        {
                byte[] val = new byte[0];
                uint bufsize = (uint)val.Length;
                if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x24) == 0)
                {
                    return false;
                }
            return true;
        }

        /// <summary>
        /// 获取乐彩位置参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetPosition_Info(ref PositionSetting_LeCai cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(PositionSetting_LeCai)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x30) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(PositionSetting_LeCai))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (PositionSetting_LeCai)SerializationUnit.BytesToStruct(structData, typeof(PositionSetting_LeCai));
            string flag = Encoding.ASCII.GetString(BitConverter.GetBytes(cc.nFlag));
            if (flag != "LEC\0")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置乐彩位置参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetPosition_Info(PositionSetting_LeCai cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(PositionSetting_LeCai))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x30) == 0)
            {
                return false;
            }
            return true;
        }

	    /// <summary>
	    ///     //：req = 0x7A, index = 0;只能写不能读，主板不存
	    /// 全局/外设参数
	    /// </summary>
	    /// <param name="settings"></param>
	    /// <returns></returns>
	    public static bool SetPeripheralExtendedSettings(AllParam settings)
	    {
	        PrintingParaType para = new PrintingParaType(null);
	        para.YAdjustDistance =
	            (uint) (settings.ExtendedSettings.BackAndForthLen*settings.PrinterProperty.fPulsePerInchY);
	        byte[] val = new byte[Marshal.SizeOf(typeof (PrintingParaType))];
	        uint bufsize = (uint) val.Length;
	        val = SerializationUnit.StructToBytes(para);
	        return CoreInterface.SetEpsonEP0Cmd(0x7a, val, ref bufsize, 0, 0) != 0;
	    }

	    //////////////////////////////////////////////
	    /// <summary>
	    /// 设置彩神参数
	    /// </summary>
	    /// <param name="settings"></param>
	    /// <returns></returns>
        public static bool SetFloraParam(AllParam settings)
        {
            FloraParamUI paramUi = settings.ExtendedSettings.FloraParam;
            FloraParam para = new FloraParam(null);
            ushort pid, vid;
            pid = vid = 0;
	        ushort index = 0xA0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.FLORA_FLAT_TEXTILE)
                {
                    para.Flag = new char[] {'0', 'T', '5', '0' };
                    index = 0xA0;
                    para.suckStartPlace = (uint)(paramUi.suckStartPlace * settings.PrinterProperty.fPulsePerInchY);
                }
                if (vid == (ushort)VenderID.FLORA_BELT_TEXTILE)
                {
                    para.Flag = new char[] {  'T', '1', '8', '0'};
                    index = 0xA1;
                    para.suckStartPlace = (uint)(paramUi.suckStartPlace * settings.PrinterProperty.fPulsePerInchX);
                }
            }
            para.cleanZPlace = (uint)(paramUi.cleanZPlace * settings.PrinterProperty.fPulsePerInchZ);
            para.moistYPlace = (uint)(paramUi.cappingYPlace * settings.PrinterProperty.fPulsePerInchY);
            para.moistZPlace = (uint)(paramUi.cappingZPlace * settings.PrinterProperty.fPulsePerInchZ);
            para.suckEndPlace = (uint)(paramUi.suckEndPlace * settings.PrinterProperty.fPulsePerInchY);
            para.purgeInkRecoverTime = paramUi.purgeInkRecoverTime;
            para.purgeInkTime = paramUi.purgeInkTime;

            byte bitFlag = 0;

            if (paramUi.bIsNeedPrepare)
            {
                bitFlag |= 0x1;
            }
            if (paramUi.bIsNeedCleanFlash)
            {
                bitFlag |= 0x2;
            }

            para.bIsNeedPrepare = bitFlag; // (byte)(paramUi.bIsNeedPrepare ? 1 : 0);
            para.speed = paramUi.speed;
            para.preOff = (uint)(paramUi.preOffset * settings.PrinterProperty.fPulsePerInchX);
	        para.cleanSlotNum = paramUi.cleanSlotNum;
	        para.cleanSlotSpace = (uint) (paramUi.cleanSlotSpace*settings.PrinterProperty.fPulsePerInchX);
	        para.cleanMotorSpeed = paramUi.cleanMotorSpeed;
	        para.doWetDelay = paramUi.doWetDelay;
	        para.speedBack = paramUi.speedBack;
            para.scraperStart1 = (uint)(paramUi.scraperStart1 * settings.PrinterProperty.fPulsePerInchX);
            para.scraperStart2 = (uint)(paramUi.scraperStart2 * settings.PrinterProperty.fPulsePerInchX);
            byte[] val = new byte[Marshal.SizeOf(typeof(FloraParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(para);
            return CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, index) != 0;
        }

        //////////////////////////////////////////////
        /// <summary>
        /// 设置彩神参数
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static bool GetFloraParam(ref AllParam settings)
        {
            FloraParamUI paramUi = settings.ExtendedSettings.FloraParam;
            FloraParam para = new FloraParam(null);
            ushort pid, vid;
            pid = vid = 0;
            ushort index = 0xA0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.FLORA_FLAT_TEXTILE)
                {
                    index = 0xA0;
                }
                if (vid == (ushort)VenderID.FLORA_BELT_TEXTILE)
                {
                    index = 0xA1;
                }
            }
            byte[] val = new byte[Marshal.SizeOf(typeof(FloraParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, index) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(FloraParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            para = (FloraParam)SerializationUnit.BytesToStruct(structData, typeof(FloraParam));
            if (vid == (ushort)VenderID.FLORA_FLAT_TEXTILE)
            {
                string flag = new string(para.Flag);
                if (flag != "0T50")
                {
                    return false;
                }
            }
            if (vid == (ushort)VenderID.FLORA_BELT_TEXTILE)
            {
                string flag = new string(para.Flag);
                if (flag != "T180")
                {
                    return false;
                }
            }
            if (vid == (ushort)VenderID.FLORA_BELT_TEXTILE)
            {
                paramUi.suckStartPlace = para.suckStartPlace / settings.PrinterProperty.fPulsePerInchX;
            }
            if (settings.PrinterProperty.fPulsePerInchZ != 0)
            {
                paramUi.cleanZPlace = para.cleanZPlace / settings.PrinterProperty.fPulsePerInchZ;
                paramUi.cappingZPlace = para.moistZPlace / settings.PrinterProperty.fPulsePerInchZ;                
            }
            if (settings.PrinterProperty.fPulsePerInchY != 0)
            {
                if (vid == (ushort)VenderID.FLORA_FLAT_TEXTILE)
                {
                    paramUi.suckStartPlace = para.suckStartPlace / settings.PrinterProperty.fPulsePerInchY;
                }
                paramUi.cappingYPlace = para.moistYPlace / settings.PrinterProperty.fPulsePerInchY;
                paramUi.suckEndPlace = para.suckEndPlace/settings.PrinterProperty.fPulsePerInchY;
            }
            paramUi.purgeInkRecoverTime = para.purgeInkRecoverTime;
            paramUi.purgeInkTime = para.purgeInkTime;
            paramUi.bIsNeedPrepare = (para.bIsNeedPrepare & 0x1) > 0;
            paramUi.bIsNeedCleanFlash = (para.bIsNeedPrepare & 0x2) > 0;
            paramUi.speed = para.speed;
            paramUi.preOffset = (para.preOff /settings.PrinterProperty.fPulsePerInchX);
            paramUi.doWetDelay = para.doWetDelay;
            settings.ExtendedSettings.FloraParam = paramUi;
            
            return true;
        }

        /// <summary>
        /// 直通fw的作业相关设置
        /// </summary>
        /// <param name="jobSettings"></param>
        /// <returns></returns>
        public static bool SetFwJobSettings(FwJobSettings jobSettings)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(FwJobSettings))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(jobSettings);
            return CoreInterface.SetEpsonEP0Cmd(0x83, val, ref bufsize, 0, 1) != 0;
        }
        /// <summary>
        /// 喷头混装喷头类型定义
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static bool GetPrintHeadOrder(ref PrintHeadOrder cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(PrintHeadOrder)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x78, val, ref bufsize, 0, 2) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(PrintHeadOrder))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (PrintHeadOrder)SerializationUnit.BytesToStruct(structData, typeof(PrintHeadOrder));
            return true;
        }

        public static bool SetPrintHeadOrder(PrintHeadOrder para)
        {
            para.Flag = new char[] { 'P', 'H', 'O', 'r' };
            byte[] val = new byte[Marshal.SizeOf(typeof(PrintHeadOrder))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(para);
            return CoreInterface.SetEpsonEP0Cmd(0x78, val, ref bufsize, 0, 2) != 0;
        }
        /// <summary>
        /// 获取保湿参数
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static bool GetWetParam(ref WetParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(WetParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x7f, val, ref bufsize, 0, 2) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(WetParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (WetParam)SerializationUnit.BytesToStruct(structData, typeof(WetParam));
            return true;
        }
        /// <summary>
        /// 设置保湿参数
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public static bool SetWetParam(WetParam para)
        {
            para.Flag = new char[] { 'W', 'E', 'T', 'P' };
            byte[] val = new byte[Marshal.SizeOf(typeof(WetParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(para);
            return CoreInterface.SetEpsonEP0Cmd(0x7f, val, ref bufsize, 0, 2) != 0;
        }

        /// <summary>
        /// 获取卓展专用参数
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static bool GetZhuoZhanParam(ref ZhuoZhanParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(ZhuoZhanParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x130) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(ZhuoZhanParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (ZhuoZhanParam)SerializationUnit.BytesToStruct(structData, typeof(ZhuoZhanParam));
            string flag = new string(cc.Flag);
            if (flag != "ZZGY")
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置卓展专用参数
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public static bool SetZhuoZhanParam(ZhuoZhanParam para)
        {
            para.Flag = new char[] { 'Z', 'Z', 'G', 'Y' };
            byte[] val = new byte[Marshal.SizeOf(typeof(ZhuoZhanParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(para);
            return CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x130) != 0;
        }

        /// <summary>
        /// 震荡设置
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public static bool SetShakeParam(PhShake_tag cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(PhShake_tag))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            return CoreInterface.SetEpsonEP0Cmd(0x83, val, ref bufsize, 0, 0x05) != 0;
        }

        /// <summary>
        /// 获取震荡设置
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public static bool GetShakeParam(ref PhShake_tag cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(PhShake_tag)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x83, val, ref bufsize, 0, 0x05) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(PhShake_tag))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (PhShake_tag)SerializationUnit.BytesToStruct(structData, typeof(PhShake_tag));
            return true;
        }
        /// <summary>
        /// 获取板卡标志 BYHX
        /// </summary>
        /// <returns></returns>
        public static bool GetFlagOfBoard(byte[] val)
        {
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x86, val, ref bufsize, 0, 0x11) != 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 设置板卡标志 BYHX
        /// </summary>
        /// <returns></returns>
        public static bool SetFlagOfBoard()
        {
            string flag = "BYHX";
            byte[] val = System.Text.Encoding.Default.GetBytes(flag);
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x86, val, ref bufsize, 0, 0x11) != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取普奇喷头清洗参数
        /// </summary>
        /// <returns></returns>
        public static bool GetPQCleanParam(ref PQCleanParam para)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(PQCleanParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x140) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(PQCleanParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            para = (PQCleanParam)SerializationUnit.BytesToStruct(structData, typeof(PQCleanParam));

            bool flagCheck = true;
            char[] flag = new char[] { 'P', 'Q', 'C', '\0' };
            for (int i = 0; i < para.Flag.Length; i++)
            {
                if (para.Flag[i] != flag[i])
                {
                    flagCheck = false;
                    break;
                }
            }
            return flagCheck;
        }
        /// <summary>
        /// 设置普奇喷头清洗参数
        /// </summary>
        /// <returns></returns>
        public static bool SetPQCleanParam(PQCleanParam para)
        {
            para.Flag = new char[] { 'P', 'Q', 'C', '\0' };
            byte[] val = new byte[Marshal.SizeOf(typeof(PQCleanParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(para);
            return CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x140) != 0;
        }

        /// <summary>
        /// 获取Gma喷头清洗参数
        /// </summary>
        /// <returns></returns>
        public static bool GetGmaCleanParam(ref GmaCleanParam para)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(GmaCleanParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0xe5) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(GmaCleanParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            para = (GmaCleanParam)SerializationUnit.BytesToStruct(structData, typeof(GmaCleanParam));

            bool flagCheck = true;
            char[] flag = new char[] { 'C', 'L', 'C', 'N' };
            for (int i = 0; i < para.Flag.Length; i++)
            {
                if (para.Flag[i] != flag[i])
                {
                    flagCheck = false;
                    break;
                }
            }
            return flagCheck;
        }
        /// <summary>
        /// 设置Gma喷头清洗参数
        /// </summary>
        /// <returns></returns>
        public static bool SetGmaCleanParam(GmaCleanParam para)
        {
            para.Flag = new char[] { 'C', 'L', 'C', 'N' };
            byte[] val = new byte[Marshal.SizeOf(typeof(GmaCleanParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(para);
            return CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0xE5) != 0;
        }

        /// <summary>
        /// 获取惠丽彩喷头清洗参数
        /// </summary>
        /// <returns></returns>
        public static bool GetHlcCleanParam(ref HlcCleanParam para)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(HlcCleanParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x140) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(HlcCleanParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            para = (HlcCleanParam)SerializationUnit.BytesToStruct(structData, typeof(HlcCleanParam));

            bool flagCheck = true;
            char[] flag = new char[] { 'H', 'L', 'C', (char)0 };
            for (int i = 0; i < para.Flag.Length; i++)
            {
                if (para.Flag[i] != flag[i])
                {
                    flagCheck = false;
                    break;
                }
            }
            return flagCheck;
        }
        /// <summary>
        /// 设置惠丽彩喷头清洗参数
        /// </summary>
        /// <returns></returns>
        public static bool SetHlcCleanParam(HlcCleanParam para)
        {
            para.Flag = new char[] { 'H', 'L', 'C', (char)0 };
            byte[] val = new byte[Marshal.SizeOf(typeof(HlcCleanParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(para);
            return CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x140) != 0;
        }

        //////////////////////////////////////////////
        /// <summary>
        /// 获取ldp参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetLDPParam(ref LDPParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(LDPParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x120) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(LDPParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (LDPParam)SerializationUnit.BytesToStruct(structData, typeof(LDPParam));
            string flag = new string(cc.Flag);
            if (flag != "LDP\0")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置ldp参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetLDPParam(LDPParam cc)
        {
            cc.Flag = new char[] { 'L', 'D', 'P', (char)0 };
            byte[] val = new byte[Marshal.SizeOf(typeof(LDPParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x120) == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// hapond电机模式切换
        /// </summary>
        /// <param name="cc"></param>
        public static void SetHapondControlMode(HapondMotorParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(HapondMotorParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0100) == 0)
            {
                Debug.Assert(false, "SetHapondControlMode fialed!");
            }
        }

        /// <summary>
        /// hapond电机模式切换
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetHapondControlMode(ref HapondMotorParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(HapondMotorParam))+2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0100) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(HapondMotorParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (HapondMotorParam)SerializationUnit.BytesToStruct(structData, typeof(HapondMotorParam));
            string flag = new string(cc.Flag);
            if (flag != "HAPD")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 通用双Y电机模式切换
        /// </summary>
        /// <param name="cc"></param>
        public static void SetMotionSetting(HapondMotorParam_Auto cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(HapondMotorParam_Auto))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x8B, val, ref bufsize, 0, 0x1) == 0)
            {
                Debug.Assert(false, "SetMotionSetting fialed!");
            }
        }

        /// <summary>
        /// 通用双Y电机模式切换
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetMotionSetting(ref HapondMotorParam_Auto cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(HapondMotorParam_Auto)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x8B, val, ref bufsize, 0, 0x1) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(HapondMotorParam_Auto))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (HapondMotorParam_Auto)SerializationUnit.BytesToStruct(structData, typeof(HapondMotorParam_Auto));
            string flag = new string(cc.Flag);
            if (flag != "YTOQ")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取白墨循环数据参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetWhiteInkCycleParam(ref WhiteInkCycle cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(WhiteInkCycle)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x82, val, ref bufsize, 0, 5) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(WhiteInkCycle))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (WhiteInkCycle)SerializationUnit.BytesToStruct(structData, typeof(WhiteInkCycle));
            return true;
        }

        /// <summary>
        /// 白墨循环数据参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetWhiteInkCycleParam(WhiteInkCycle cc)
        {
            cc.Flag = new char[] { 'W', 'I', 'C', 'F' };
            byte[] val = new byte[Marshal.SizeOf(typeof(WhiteInkCycle))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x82, val, ref bufsize, 0, 5) == 0)
            {
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// 设置UV灯X2参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetGZUVX2Param(ref GZUVX2Param cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(GZUVX2Param)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0xE8) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(GZUVX2Param))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (GZUVX2Param)SerializationUnit.BytesToStruct(structData, typeof(GZUVX2Param));
            string flag = Encoding.ASCII.GetString(BitConverter.GetBytes(cc.Flag));
            if (flag != "GZX\0")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取UV灯X2参数
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetGZUVX2Param(GZUVX2Param cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(GZUVX2Param))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0xE8) == 0)
            {
                return false;
            }
            return true;
        }
        //////////////////////////////////////////////

        /// <summary>
        /// 是否支持京瓷头板设置电压
        /// </summary>
        /// <returns></returns>
        public static bool IsSupportVoltage_KY()
        {
            byte[] val = new byte[64];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x54, val, ref bufsize, 0, 0x2) != 0)
            {
                
                if ((val[4] & 0x8) > 0)
                    return true;
            }

            return false;
        }


        /// <summary>
        /// 获取Y步进速度
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static bool GetYStepSpeed(ref int ySpeed)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(YStepSpeedParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x82, val, ref bufsize, 0, 7) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(YStepSpeedParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            YStepSpeedParam cc = (YStepSpeedParam)SerializationUnit.BytesToStruct(structData, typeof(YStepSpeedParam));
            string flag = new string(cc.Flag);
            if (flag == "STSP")
            {
                ySpeed = cc.ySpeed;
            }
            else
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置Y步进速度
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public static bool SetYStepSpeed(int ySpeed)
        {
            YStepSpeedParam cc = new YStepSpeedParam();
            cc.Flag = new char[] { 'S', 'T', 'S', 'P' };
            cc.ySpeed = ySpeed;
            byte[] val = new byte[Marshal.SizeOf(typeof(YStepSpeedParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            return CoreInterface.SetEpsonEP0Cmd(0x82, val, ref bufsize, 0, 7) != 0;
        }


        public static bool GetLingFengPara(ref LingFengParam param)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(LingFengParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x27) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(LingFengParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            param = (LingFengParam)SerializationUnit.BytesToStruct(structData, typeof(LingFengParam));
            string flag = new string(param.Flag);
            if (flag != "CJR\0")
            {
                return false;
            }
            return true;
        }

        public static bool SetLingFengParam(LingFengParam cc)
        {
            cc.Flag = new char[] { 'C', 'J', 'R', (char)0 };
            byte[] val = new byte[Marshal.SizeOf(typeof(LingFengParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x27) == 0)
            {
                return false;
            }
            return true;
        }

        public static bool SetKeepwat(int flag)
        {
            byte[] subval = new byte[1];
            uint bufsize = (uint)subval.Length;
            subval[0] = (byte)flag;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x7C, subval, ref bufsize, 5, (ushort)0);
            return (ret != 0);
        }

        public static int GetKeepwat()
        {
            int ret = 0;
            byte[] subval = new byte[3];
            uint bufsize = (uint)subval.Length;
            subval[0] = 0x7C;
            subval[1] = 5;
            if (CoreInterface.GetEpsonEP0Cmd(0x7C, subval, ref bufsize, 5, (ushort)0) != 0)
            {
                ret = subval[2];
            }

            return ret;
        }

        public static byte[] GetChanneParamValue_Y2(int channelNum)
        {
            byte[] val = new byte[channelNum];
            uint readlen = (uint)val.Length;

            CoreInterface.GetEpsonEP0Cmd(0x7F, val, ref readlen, 25, 0);

            return val;
        }

        public static void GetAllChannelsFromBoard_Y2(int channelNum, List<string> NameList, ref  List<ChannelInfo> channels)
        {
            byte[] channelParamValues = EpsonLCD.GetChanneParamValue_Y2(channelNum);

            for (int i = 1; i <= channelNum; i++)
            {
                ChannelInfo channel = new ChannelInfo();
                channel.Id = i;
                channel.Name = NameList[i - 1];//EpsonLCD.GetChannelName(i);
                channel.OriginalName = ""; //EpsonLCD.GetChannelNameDebug(i);
                channel.OriginalValue = BitConverter.ToString(channelParamValues, i - 1, 1);

                channel.Value = channelParamValues[i - 1];
                channels.Add(channel);
            }
        }

        public static int SetChannelParamValue_Y2(byte[] raios)
        {
            uint uSize = (uint)raios.Length;
            return CoreInterface.SetEpsonEP0Cmd(0x7F, raios, ref uSize, 33, 0);
        }

        /// <summary>
        /// UV灯偏移距离设置
        /// </summary>
        /// <returns></returns>
        public static bool SetUVOffsetDistToFw(UVOffsetDistanceUI uvOffset, float fPulsePerInchX)
        {
            byte[] buf = new byte[uvOffset.OffsetDistArray.Length * 4];
            uint bufsize = (uint)buf.Length;

            for (int i = 0; i < uvOffset.OffsetDistArray.Length; i++)
            {
                Buffer.BlockCopy(BitConverter.GetBytes((int)(uvOffset.OffsetDistArray[i] * fPulsePerInchX)), 0, buf, i * 4, 4);
            }


            int ret = CoreInterface.SetEpsonEP0Cmd(0x82, buf, ref bufsize, 0, 0x0A);
            return ret == 1;
        }

        public static bool GetHumanParam(ref HumanParam param)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(HumanParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x160) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(HumanParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            param = (HumanParam)SerializationUnit.BytesToStruct(structData, typeof(HumanParam));
            string flag = Encoding.ASCII.GetString(BitConverter.GetBytes(param.Flag));
            if (flag != "HUMA")
            {
                return false;
            }
            return true;
        }

        public static bool SetHumanParam(HumanParam param)
        {
            param.Flag = BitConverter.ToUInt32(Encoding.ASCII.GetBytes("HUMA"),0);
            byte[] val = new byte[Marshal.SizeOf(typeof(HumanParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(param);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x160) == 0)
            {
                return false;
            }
            return true;
        }

        public static bool SetTemperaturProfil(bool flag)
        {
            uint select = (uint)(flag ? 0x00000100 : 0x00000120);
            List<byte> value = new List<byte>();
            value.Add(0xC5);
            value.Add(0xDE);
            value.AddRange(BitConverter.GetBytes(select));
            uint bufsize = (uint)value.Count;
            if (CoreInterface.SetEpsonEP0Cmd(0x80, value.ToArray(), ref bufsize, 0, 0) != 0)
            {
                return true;
            }
            return false;
        }

        public static bool GetTemperaturProfileState()
        {
            byte[] val = new byte[8];
            uint bufsize = (uint)val.Length;
            val[0] = 0xC5;
            val[1] = 0xE3;
            if (CoreInterface.SetEpsonEP0Cmd(0x80, val, ref bufsize, 0, 0) != 0)
            {
                return true;
            }
            return false;
        }
        public static bool SetWorkPosInfo(SPrinterSetting ss)
        {
            uint workPos1 = 0xffffffff;
            uint workPos2 = 0xffffffff;

            if((ss.sExtensionSetting.WorkPosEnable & 1) == 1 )
            {
                workPos1 = (uint)ss.sExtensionSetting.WorkPosList[0];
            } 
            if((ss.sExtensionSetting.WorkPosEnable >> 1 & 1) == 1)
            {
                workPos2 = (uint)ss.sExtensionSetting.WorkPosList[1];
            }

            byte[] val = new byte[8];
            uint bufsize = (uint)val.Length;
            byte[] temp = new byte[4];
            temp = BitConverter.GetBytes(workPos1);
            Array.Copy(temp, 0, val, 0, 4);
            temp = BitConverter.GetBytes(workPos2);
            Array.Copy(temp, 0, val, 4, 4);

            if (CoreInterface.SetEpsonEP0Cmd(0x7F, val, ref bufsize, 92, 0) != 0)
            {
                return true;
            }
            return false;
        }
    }

    public static class InkAreaStaticsHelper
    {
        /// <summary>
        /// 设置当前日期
        /// </summary>
        /// <returns></returns>
        public static bool SetCurrentDate(DateTime dataTimeNow)
        {
            ushort Year = (ushort)dataTimeNow.Date.Year;
            byte Month = (byte)dataTimeNow.Date.Month;
            byte Day = (byte)dataTimeNow.Date.Day;
            byte[] valYear = BitConverter.GetBytes(Year);
            byte[] val = new byte[4];
            val[0] = valYear[0];
            val[1] = valYear[1];
            val[2] = Month;
            val[3] = Day;
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x86, val, ref bufsize, 0, 0x12) != 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 月记录墨量清零
        /// </summary>
        /// <returns></returns>
        public static bool ClearInkQuantity()
        {
            byte[] val = new byte[0];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x86, val, ref bufsize, 0, 0x13) != 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 月记录打印面积清零
        /// </summary>
        /// <returns></returns>
        public static bool ClearPrintArea()
        {
            byte[] val = new byte[0];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x86, val, ref bufsize, 0, 0x14) != 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获得月记录墨量“IVPM” + 32bit,12个月*8个颜色，ml单位
        /// </summary>
        /// <returns></returns>
        public static bool GetInkQuantity(byte[] val)
        {
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x86, val, ref bufsize, 0, 0x15) != 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 设置月记录墨量“IVPM” + 32bit,12个月*8个颜色，ml单位
        /// </summary>
        /// <returns></returns>
        public static bool SetInkQuantity(byte[] val)
        {
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x86, val, ref bufsize, 0, 0x15) != 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获得月记录打印面积”PAPM“ + double，12个月，单位同总打印面积。
        /// </summary>
        /// <returns></returns>
        public static bool GetPrintArea(byte[] val)
        {
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x86, val, ref bufsize, 0, 0x16) != 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 设置月记录打印面积”PAPM“ + double，12个月，单位同总打印面积。
        /// </summary>
        /// <returns></returns>
        public static bool SetPrintArea(byte[] val)
        {
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x86, val, ref bufsize, 0, 0x16) != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 把板卡的墨量统计信息同步到本地磁盘
        /// 跨年时清空上一年记录,重新开始记录
        /// 如果月份不同,则清空去年当前月的数据,重新计数
        /// </summary>
        /// <returns></returns>
        public static bool SynchronizeInkAndArea(DateTime dt)
        {
            SBoardInfo sBoardInfo = new SBoardInfo();
            if (CoreInterface.GetBoardInfo(0, ref sBoardInfo) != 0)
            {
                string filePath = GetInkStaticsFileName(sBoardInfo.m_nBoardSerialNum);
                FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                byte[] ink = new byte[Marshal.SizeOf(typeof(InkOfMonths))];
                byte[] area = new byte[Marshal.SizeOf(typeof(AreaOfMonths))];
                BinaryReader br = new BinaryReader(fs);
                byte[] yearBytes = BitConverter.GetBytes(dt.ToBinary());
                DateTime lastYear = DateTime.Now;
                // 从上次同步的文件中读取出同步的年份
                if (fs.Length > ink.Length + area.Length)
                {
                    br.Read(yearBytes, ink.Length + area.Length, yearBytes.Length);
                    lastYear = DateTime.FromBinary(BitConverter.ToInt64(yearBytes,0));
                }

                BinaryWriter bw = new BinaryWriter(fs);
                if (GetInkQuantity(ink) && GetPrintArea(area))
                {
                    // 如果月份不同,则清空数据,重新计数
                    if (dt.Month != lastYear.Month)
                    {
                        //清空去年当前月的数据,InkOfColors
                        byte[] zoreBytes = new byte[Marshal.SizeOf(typeof(InkOfColors))];//一个月各颜色的数据长度InkOfColors
                        Buffer.BlockCopy(zoreBytes, 0, ink, 4 + (dt.Month-1) * zoreBytes.Length, zoreBytes.Length);
                        //
                        zoreBytes = new byte[4];//面积用的double类型长度是4
                        Buffer.BlockCopy(zoreBytes, 0, area, 4 + (dt.Month - 1) * zoreBytes.Length, zoreBytes.Length);
                      
                        if (SetInkQuantity(ink) && SetPrintArea(area))
                        {
                            //MessageBox.Show(ResString.GetResString("SynchronizeInkAndAreaSuccess"),
                            //    ResString.GetProductName());
                        }
                    }
                    fs.Seek(0, SeekOrigin.Begin);
                    bw.Write(ink);
                    bw.Write(area);
                    bw.Write(dt.ToBinary());
                    fs.Close();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 获取板卡是否支持同步墨量功能
        /// </summary>
        /// <returns></returns>
        public static bool CanSynchronizeInkAndArea()
        {
            byte[] val = new byte[64];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x54, val, ref bufsize, 0, 0x2) != 0)
            {
                if ((val[4] & 1) == 1)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是否更换了板卡
        /// </summary>
        /// <returns></returns>
        public static bool BoardHasChanged()
        {
            string[] files = GetAllInkStaticsFiles();
            if (files.Length == 1) // 有且只有一个文件时,才认为发生了板卡更换;多个文件时无法确认导入哪个文件的数据,客户决定这种情况放弃导入;
            {
                SBoardInfo sBoardInfo = new SBoardInfo();
                if (CoreInterface.GetBoardInfo(0, ref sBoardInfo) != 0)
                {
                    string filePath =GetInkStaticsFileName(sBoardInfo.m_nBoardSerialNum);
                    if (!File.Exists(filePath))
                    {
                        return true;// 本地存在墨量数据文件,但和当前板卡不匹配,则认为更换了板卡
                    }
                    return false;
                }
            }
            return false;
        }

        private static string strInkStaticsFilePre = "InkS";
        /// <summary>
        /// 按板卡id获取带统一前缀的文件名称
        /// </summary>
        /// <param name="boardid"></param>
        /// <returns></returns>
        public static string GetInkStaticsFileName(uint boardid)
        {
            return Path.Combine(Application.StartupPath, strInkStaticsFilePre + boardid + ".dll");
        }
        /// <summary>
        /// 获取根目录下所有墨量数据文件,最多应只有一个
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllInkStaticsFiles()
        {
            string[] files = Directory.GetFiles(Application.StartupPath, strInkStaticsFilePre + "*.dll");
            return files;
        }

        /// <summary>
        /// 清除除当前连接主板外的其他墨量数据文件
        /// </summary>
        public static void ClearInkStaticsFiles()
        {
            string curFilePath = string.Empty;
            SBoardInfo sBoardInfo = new SBoardInfo();
            if (CoreInterface.GetBoardInfo(0, ref sBoardInfo) != 0)
            {
                curFilePath = GetInkStaticsFileName(sBoardInfo.m_nBoardSerialNum);
            }
            string[] files = GetAllInkStaticsFiles();
            for (int i = 0; i < files.Length; i++)
            {
                if (curFilePath != files[i])
                    File.Delete(files[i]);
            }
        }

        
    }

}