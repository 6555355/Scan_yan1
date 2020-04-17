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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using PrinterStubC.Common;
using System.Reflection;

//using DemoTreeView;
namespace BYHXPrinterManager
{
	public struct CONSTANT
	{
		const string m_Dot = ".";
		public static CultureInfo CreateNativeCulture()
		{
			CultureInfo cultInfo = new CultureInfo("en-US");
			cultInfo.NumberFormat.NumberDecimalSeparator = m_Dot;
			return cultInfo;
		}
#if LIYUUSB
		public const int nReserveSizeConst = 29;
#else
		public const int nReserveSizeConst = 2;
#endif
	}

    /* JobPrint.dll:多介质排版接口
    * struct image_interface{
           double x;
           double y;
           const char * file;
       };
      int ImageTile(struct image_str argv[], int num, float height, float width,  char * file)
    */
    [Serializable]
    public struct ImageTileItem
    {
        public double X;//在拼贴图上该图所在位置X
        public double Y;//在拼贴图上该图所在位置Y
        public ImageTileItemClip ImageTileItemClip;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CoreConst.MAX_PATH)]
        public string File;//prt完整文件名
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct NoteInfo
    {
        public int FontSize;
        /// <summary>
        /// Regular=0;Bold=1;Italic=2;Underline=4;Strikeout=8
        /// 可以组合例如3=Bold|Italic，以此类推
        /// </summary>
        public int fontStyle;
        /// <summary>
        /// 注脚里图片的距离
        /// </summary>
        public int NoteMargin;
        /// <summary>
        /// 0:left;1:top;2:right;3:bottom
        /// </summary>
        public int NotePosition;
        /// <summary>
        /// 作业大小、分辨率、pass数、方向、文件路径
        /// </summary>
        public int AddtionInfoMask;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FontName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string NoteText;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string Pad;
    }

    [Serializable]
    public struct ImageUITileItem
    {
        public double X;
        public double Y;
        public ImageTileItemClip ImageTileItemClip;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CoreConst.MAX_PATH)]
        public string File;//prt完整文件名
        /// <summary>
        /// 保存拼贴前单个文件的独立预览图
        /// </summary>
        public string SourcePreviewFile;

        public static implicit operator ImageTileItem(ImageUITileItem item)
        {
            ImageTileItem tile = new ImageTileItem();
            tile.X = item.X;
            tile.Y = item.Y;
            tile.ImageTileItemClip = item.ImageTileItemClip;
            tile.File = item.File;
            return tile;
        }
    }

    public struct ImageTileItemClip
    {
        //对Prt剪切的起点以及剪切宽高
        public double ClipX;
        public double ClipY;
        public double ClipW;
        public double ClipH;
    }

    /// <summary>
    /// 多文件拼贴信息
    /// </summary>
    [Serializable]
    public struct MultiFileTileInfo
    {
        public List<ImageUITileItem> TileImages;
        public float Width;//单位英寸
        public float Height;//单位英寸
    }

    /*
     * typedef struct contrast_color{
	char Color;//图像层:'0';专色层:'K' 'C' 'M' 'Y' 'W'...
	unsigned char Rev[3];

	unsigned int Lay;//0,1,2...
	unsigned int Mode;//all:0/rip:1/image:2;
	
	unsigned int Gray;//for all mode:
	unsigned int Mask;//Bit7-Bit0:Color7-Color0;
	unsigned int SetType;//for image mode:or-0,and-1
	unsigned int Inverse;//for image mode:
	char * file;

    }ContrastColorTpye;

     */
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct ConstrastColor
    {
        public byte Color;
        public byte HeadIndex;
        public byte InitData;
        public byte DataSource;
        public byte ColorNum;
        public int Layer;
        public int Mode;//all:0/rip:1/image:2;
        public int Gray;//0-ff;界面上为0-100，这里需要转换（UIValue*255/100）
        public int Mask;
        public int SetType;//默认0为并集，1为交集
        public int Inverse;//是否勾选取反，若勾选则为1，否则为0
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
        public char[] FilePath;

        public ConstrastColor(object oo)
        {
            Color = (byte)'0';
            Layer = 0;
            Mode = 0;
            Gray = 255;
            Mask = 0;
            SetType = 0;
            Inverse = 0;
            FilePath = new char[255];//(' ',255);
            for (int i = 0; i < FilePath.Length; i++)
            {
                FilePath[i] = ' ';
            }
            HeadIndex = 0;
            InitData = 0;
            ColorNum = 0;
            DataSource = 0;
        }
    }


	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SPrtImageInfo
	{
		public int		nImageType;
		public int		nImageWidth;
		public int		nImageHeight;
		public int		nImageColorNum;
		public int		nImageColorDeep;
		public int		nImageResolutionX;
        public int      nImageResolutionY;
		public int		nImageDataSize;
        [XmlIgnore]
		public IntPtr   	nImageData;
	}
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SPrtImagePreview
	{
		public int		nImageType;
		public int		nImageWidth;
		public int		nImageHeight;
		public int		nImageColorNum;
		public int		nImageColorDeep;
		public int		nImageResolutionX;
		public int		nImageResolutionY;
		public int		nImageDataSize;
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_PREVIEW_BUFFER_SIZE)]
		public byte[]	nImageData;
	}


    [StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SPrtFileInfo
	{
		public SFrequencySetting	sFreSetting;
		public SPrtImageInfo		sImageInfo;
		public int					nVersion;
		[MarshalAs(UnmanagedType.ByValTStr,	SizeConst=CoreConst.MAX_NAME)]
		public string				sRipSource;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CoreConst.MAX_NAME)]
        public string sJobName;

	}
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SUSBDeviceInfo
	{
		public ushort  nProductID;
		public ushort  nVendorID;

		[MarshalAs(UnmanagedType.ByValTStr,SizeConst=CoreConst.MAX_USBINFO_STRINGLEN*2)]
		public string sProduct;
		[MarshalAs(UnmanagedType.ByValTStr,	SizeConst=CoreConst.MAX_USBINFO_STRINGLEN*2)]
		public string sManufacturer;
		[MarshalAs(UnmanagedType.ByValTStr,	SizeConst=CoreConst.MAX_USBINFO_STRINGLEN*2)]
		public string sSerialNumber;
	}

	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SSeviceSetting
	{
		public uint		unColorMask;
		public uint		unPassMask;
		public int		nCalibrationHeadIndex;
		public uint     nDirty;
		public int		nBit2Mode;

		public byte		Vsd2ToVsd3;      //VSD mode
		public byte		Vsd2ToVsd3_ColorDeep; //ColorDeep
        /// <summary>
        /// 扫描向电机，0x01第一个工作台(AXIS_X); 0x08第二个工作台(AXIS_4)
        /// 彩神t-shirt机
        /// </summary>
        public byte scanningAxis; //
        public byte nRev2;
		
		//public int		Reserve;
	    public SSeviceSetting(object oo)
	    {
	        unColorMask = 0;
	        unPassMask = 0;
	        nCalibrationHeadIndex = 3;
	        nDirty = 0;
	        nBit2Mode = 3;
	        Vsd2ToVsd3 = 2;
	        Vsd2ToVsd3_ColorDeep = 3;
	        scanningAxis = 1;
	        nRev2 = 0;
	    }
	}

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct SJobSetting
    {
        [MarshalAs(UnmanagedType.I1)] public bool bReversePrint;

        /// <summary>
        /// 打印完成后Y立即回原点[全印]
        /// 0:软件不控制;1:回原点; 2:不回原点
        /// </summary>
        public byte Yorg;

        /// <summary>
        /// 本次打印线程存活期间当前作业所在位置
        /// 与正反打印互斥
        /// </summary>
        public ushort nJobIndex;

        public int nJobID;

        /// <summary>
        /// 当前job是否需要等待开始打印信号
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] public bool bNeedWaitPrintStartSignal;

        /// <summary>
        /// 全印双平台
        /// 1：表示图像中只有彩色，2：表示作业中既有彩色也有白色//
        /// </summary>
        public byte ColorType; //

        /// <summary>
        /// 0 表示浓度100%，1 表示浓度降低1/255, 255 表示浓度为0 
        /// </summary>
        public byte cNegMaxGray; // for skyship

        /// <summary>
        /// 是否为单个prt分多层多次打印时的最后一层,印染应用,fw据此决定是否输出打印完成信号;
        /// 0:单层打印;1:多层打印中间层;2:多层打印的最后一层
        /// </summary>
        public byte bMultilayerCompleted; // 
     
  }

    [StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SJobSetting_UI
	{
		[MarshalAs(UnmanagedType.I1)]
		public bool      bReversePrint;
		[MarshalAs(UnmanagedType.I1)]
		public bool      bAlternatingPrint;
		[MarshalAs(UnmanagedType.I1)]
		public bool      bLevelLingPrint;
        [MarshalAs(UnmanagedType.I1)]
        public PrintMode pPrintMode;
        [MarshalAs(UnmanagedType.I4)]
        public SpeedEnum nSpeed;
        public byte nPass;
        public byte multipleInk;
        [MarshalAs(UnmanagedType.I1)]
        public bool bUseFileSetting;
        /// <summary>
        /// 扫描向电机，0x01第一个工作台(AXIS_X); 0x08第二个工作台(AXIS_4)
        /// 彩神t-shirt机
        /// </summary>
	    public byte scanningAxis;
	        [MarshalAs(UnmanagedType.I1)]
        public bool bIsDouble4CJob;
            /// <summary>
            /// 0 表示浓度100%，1 表示浓度降低1/255, 255 表示浓度为0 
            /// </summary>
            public byte cNegMaxGray; // for skyship

};
#if LIYUUSB
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SBoardInfo
	{
		public uint    m_nBoradVersion;
		public uint    m_nMTBoradVersion;
		public uint	    m_nHBBoardVersion;
		[MarshalAs(UnmanagedType.ByValTStr,	SizeConst=(CoreConst.BOARD_DATE_LEN))]
		public string	sProduceDateTime;
		[MarshalAs(UnmanagedType.ByValTStr,	SizeConst=(CoreConst.BOARD_DATE_LEN))]
		public string	sMTProduceDateTime;
		[MarshalAs(UnmanagedType.ByValTStr,	SizeConst=(CoreConst.BOARD_DATE_LEN))]
		public string	sReserveProduceDateTime;
		
		public uint	m_nBoardSerialNum;
		public ushort	m_nBoardManufatureID;
		public ushort	m_nBoardProductID;

		public byte m_nPrintHeadCategory;  //
		public byte m_nheadBoardType;
		public byte m_nouse1;
		public byte m_nouse;
	};
#else
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SBoardInfo
	{
		public ushort  m_nResultFlag;
		public ushort  m_nPkgVersion;
		public uint    m_nBoradVersion;
		public uint    m_nMTBoradVersion;
		public ushort	m_nBoardManufatureID;
		public ushort	m_nBoardProductID;
		public uint	m_nBoardSerialNum;
		[MarshalAs(UnmanagedType.ByValTStr,	SizeConst=(CoreConst.BOARD_DATE_LEN))]
		public string	sProduceDateTime;
		[MarshalAs(UnmanagedType.ByValTStr,	SizeConst=(CoreConst.BOARD_DATE_LEN))]
		public string	sMTProduceDateTime;
		[MarshalAs(UnmanagedType.ByValTStr,	SizeConst=(CoreConst.BOARD_DATE_LEN))]
		public string	sReserveProduceDateTime;
        public uint m_nHBBoardVersion;
        public uint m_nMapBoardVersion;
	    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (CoreConst.BOARD_DATE_LEN))]
        public string sMapBoardProduceDateTime;
	};
#endif

	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	struct SDetailError
	{
		public ushort	m_nWho;
		public ushort	m_nReserve;
		public uint	m_What;
	};
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct ToleranceBand
	{
		public int LeftTol_X; 
		public int LeftTol_Y;
		public int RightTol_X; 
		public int RightTol_Y;
	};
#if !LIYUUSB
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SFWFactoryData
	{
		public byte  m_nValidSize;
		public byte  m_nEncoder;
		public byte  m_nHeadType;
		public byte  m_nWidth;

		public byte m_nColorNum;
		public sbyte m_nGroupNum;
		public byte m_nReserve1;
		public byte m_nReserve2;

		public float m_fHeadXColorSpace;
		public float m_fHeadXGroupSpace;
		public float m_fHeadYSpace;
		public float m_fHeadAngle;

		public byte  m_nVersion;
		public byte  m_nCrc;
		
		public byte	 m_nWhiteInkNum;
		public byte	 m_nOverCoatInkNum;

        /// <summary>
        /// bit0:IsHeadLeft;bit1:mirror;bit2:surpportLcd;bit3:DualBank;
        /// bit4:COLORORDER;bit5:VerY;bit6:4ColorMirror;bit7:unkonwn;
        /// bit8:8ColorCompatibilityMode;bit9:ZMeasur;
        /// </summary>
		public uint	 m_nBitFlag;
		public byte m_nPaper_w_left; //unit : mm.  //the new element for EPSON.
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.OLD_MAX_COLOR_NUM)]
		public byte[] eColorOrder;
        public byte m_nTempCoff;			//温控系数
        public ushort m_usReserve;          //预备
        public byte m_xaar382_pixle_mode;   //45 pixle  or cycle 模式
        public byte m_PrintHeadCnt;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] rev;
	    public float ServePos;  // 服务站位置
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.OLD_MAX_COLOR_NUM)]
        public byte[] eColorOrderExt;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CONSTANT.nReserveSizeConst)]
        public byte  []m_nReserve;

	    public byte GetColorOrder(int index)
	    {
	        if (index < eColorOrder.Length)
	            return eColorOrder[index];
	        return eColorOrderExt[index - eColorOrder.Length];
	    }
	};
#else
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SFWFactoryData
	{
		public byte    m_nEncoder;
		public byte    m_nHeadType;

		public byte m_nColorNum;
		public sbyte m_nGroupNum;
		public float m_fHeadXColorSpace;
		public float m_fHeadXGroupSpace;

		public ushort  m_nWidth;
		public byte  m_nVersion;
		public byte  m_nCrc;
		public float		  m_fHeadYSpace;
		public float		  m_fHeadAngle;

		//[MarshalAs(UnmanagedType.ByValArray,SizeConst=10)]
		//public byte  []m_nReserve;

	};
#endif
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SMotionDebug
	{
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=64)]
		public byte  []m_nReserve;
	}

	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SSupportList
	{
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=16)]
		public byte  []m_nList;
	};
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SWriteHeadBoardInfo
	{
		public uint    m_nHeadFeature1; //XAAR = 0, Konica = 1, Spectra = 2 
		public uint    m_nHeadFeature2; //
	};
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SPwdInfo
	{
		public int nLimitTime;
		public int nDuration;
		public int nLang;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.OLD_MAX_COLOR_NUM)]
        public int[] nDurationInk;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.OLD_MAX_COLOR_NUM)]
		public int []nLimitInk;
	};
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SPwdInfo_UV
	{
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=2)]
		public uint []nDurationUV;
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=2)]
		public uint []nLimitUV;
	};
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct Curve382Header
	{
		public ushort m_cReserve; //  == 0
		public ushort m_cCrc;     // 字节的累加
		public byte   m_bUse;      //是否禁用
		public byte   m_nReserve2; 
	};
	[Serializable]
	public struct SFWVersion
	{
		public byte m_nHWVersion;
		public byte m_nMainVersion;
		public byte m_nSubVersion;
		public byte m_nBuildVersion;
		public SFWVersion(uint version)
		{
			m_nHWVersion = (byte)((version>>24)&0xff);
			m_nMainVersion = (byte)((version>>16)&0xff);
			m_nSubVersion = (byte)((version>>8)&0xff);
			m_nBuildVersion = (byte)((version>>0)&0xff);

		}
		public uint GetVersion()
		{
			uint version = 0;
			version = (version<<8)+m_nHWVersion;
			version = (version<<8)+m_nMainVersion;
			version = (version<<8)+m_nSubVersion;
			version = (version<<8)+m_nBuildVersion;
			return version;
		}
	}

	public struct ScorpionSenserStatus
	{
		//	 OUTPUT:
        public bool OutPldInkPumpBlackCtl1;//	b1  :PLD_INK_PUMP_BLACK_CTL1      
        public bool OutPldInkPumpBlackCtl2;//		 b2  :PLD_INK_PUMP_BLACK_CTL2      
        public bool OutPldInkPumpCyanCtl1;//			  b3  :PLD_INK_PUMP_CYAN_CTL1        
        public bool OutPldInkPumpCyanCtl2;//			   b4  :PLD_INK_PUMP_CYAN_CTL2         
        public bool OutPldInkPumpMagentaCtl1;//		b5  :PLD_INK_PUMP_MAGENTA_CTL1      
        public bool OutPldInkPumpMagentaCtl2;//					 b6  :PLD_INK_PUMP_MAGENTA_CTL2     
        public bool OutPldInkPumpYellowCtl1;//			  b7  :PLD_INK_PUMP_YELLOW_CTL1       
        public bool OutPldInkPumpYellowCtl2;//	   b8  :PLD_INK_PUMP_YELLOW_CTL2       
        public bool OutPldInkPumpLightBlackCtl;//	b9  :PLD_INK_PUMP_LIGHT_BLACK_CTL  
        public bool OutPldInkPumpLightCyanCtl;//		 b10  :PLD_INK_PUMP_LIGHT_CYAN_CTL    
        public bool OutPldInkPumpLightMagentaCtl;//   b11  :PLD_INK_PUMP_LIGHT_MAGENTA_CTL 
        public bool OutPldInkPumpLightYellowCtl;//	 b12  :PLD_INK_PUMP_LIGHT_YELLOW_CTL  
        public bool OutPldSolutionPumpCtl;//   b13  :PLD_Solution_pump_CTL         
        public bool OutPldExtraCtl9;// b14  :PLD_Extra_CTL9                 
        public bool OutPldExtraCtl8;//   b15  :PLD_Extra_CTL8      
           
        public bool OutPldLampNormalCtl;// b16  :PLD_LAMP_NORMAL_CTL           
        public bool OutPldLampEmergencyStopCtl;//   b17  : PLD_LAMP_EMERGENCY_STOP_CTL   
        public bool OutPldLeft3WaySolenoidCtl;//	  b18  : PLD_LEFT_3WAY_SOLENOID_CTL    
        public bool OutPldCleaningBoxShutterSolenoidCtl;// b19  :PLD_DRV_REV_CTL1               
        public bool OutPldCarriageFan;//	   b20  :PLD_CARRIAGE_FAN              
        public bool OutPldVacuumGateCtl1;//	 b21  :PLD_VACUUM_GATE_CTL1        
        public bool OutPldVacuumGateCtl2;//   b22  :PLD_VACUUM_GATE_CTL2          
        public bool OutPldVacuumGateCtl3;//	 b23  :PLD_VACUUM_GATE_CTL3           
        public bool OutPldSoundAlarmCtl1;//   b24  :PLD_SOUND_ALARM_CTL1           
        public bool OutPldSoundAlarmCtl2;//	 b25  :PLD_SOUND_ALARM_CTL2           
        public bool OutPldPrintStartCtl;//   b26 :PLD_SOUND_ALARM_CTL3           
        public bool OutPldSoundAlarmCtl4;//	b27  :PLD_SOUND_ALARM_CTL4           
        public bool OutPldWiperCySolenoidCtl;//  b28  :PLD_WIPER_CY_SOLENOID_CTL      
        public bool OutPldCapCySolenoidCtl;//	b29  :PLD_CAP_CY_SOLENOID_CTL        
        public bool OutPldSuckPumpCtl;//		  b30  :PLD_SUCK_PUMP_CTL              
        public bool OutPldPushinkCtl;//			b31  :PLD_PUSHINK_CTL                
        public bool OutPldSolutionAirSolenoidCtl;//	  b32  :PLD_SOLUTION_AIR_SOLENOID_CTL  
        public bool OutPldRightLeftUvLampOn;//			b33  :PLD_DRV_REV_CTL3      
        public bool OutPldReservedForCamera;//OutPldRightUvLampShutterOpenClose;//		  b34  :PLD_DRV_REV_CTL4             
        public bool OutPldReverseTakeup_CTL;//OutPldLeftUvLampShutterOpenClose;//b35  : PLD_DRV_REV_CTL5               
        public bool OutPldRearSetting_CTL;//OutPldUvControllerEmergencyStop;//		   b36  : PLD_DRV_REV_CTL6               
        public bool OutPldFrontSetting_CTL;//OutPldDrvRevCtl7;//		  b37  :PLD_DRV_REV_CTL7               
        public bool OutPldExtraCtl2;//	b38  :PLD_Extra_CTL2                
//		public bool ARM_Z_MOTOR_DIV_SEL;//		  b39  :ARM_Z_MOTOR_DIV_SEL            
//		public bool ARM_Z_MOTOR_HOLDOFF;//				b40  : ARM_Z_MOTOR_HOLDOFF            

		//		INPUT:
		public bool InPldSubInkTankWhite;//b1  :PLD_sub_ink_tank_white         
        public bool InPldCarriageLeftLimitSensor;//		 b2  :PLD_Carriage_left_limit_sensor  
        public bool InPldCarriageRightLimitSensor;// b3  :PLD_Carriage_right_limit_sensor  
        public bool InPldInkOverSensorIn;//b4  :PLD_INK_OVER_SENSOR_IN         
        public bool InPldCleaningBoxShutterOpened;//b5  :PLD_REV_SENSOR_IN1              
        public bool InPldCleaningBoxShutterClosed;//b6  :PLD_REV_SENSOR_IN2              
        public bool InPldLevelSensorIn23;//b7  :PLD_Level_sensor_IN23          
        public bool InPldZOriginIn;//b8  :PLD_Z_ORIGIN_IN               
        public bool InPldZAxisLowSensor;//b9  :PLD_Z_Axis_low_sensor            
        public bool InPldZAxisHeightLimitSensor;//b10  :PLD_Z_Axis_Height_Limit_sensor   
        public bool InPldRevSensorIn4;//b11  :PLD_REV_SENSOR_IN4             
        public bool InPldExtra_IN12;//InFrontMotorWorking;//b12  :PLD_MEDIA_SENSOR_IN5            
        public bool InPldExtra_IN13;//InFrontMotorStop;//b13  :PLD_MEDIA_SENSOR_IN6            
        public bool InPldExtra_IN14;//InRearMotorWorking;//b14  :PLD_MEDIA_SENSOR_IN2            
        public bool InPldExtra_IN15;//InRearMotorStop;//b15  :PLD_MEDIA_SENSOR_IN3            
        public bool InPldExtra_IN16;//InDirectionSetting;//b16  :PLD_MEDIA_SENSOR_IN4            
        public bool InPldMeshBar_IN;//InPldFrontManualSwitchCcw;//b17  :PLD_Front_Manual_switch_CCW      
        public bool InPldCoolingOff_IN;//InPldFrontManualSwitchCw;//b18  :PLD_Front_Manual_switch_CW       :
        public bool InPldFrontSettingOK_In;//InPldRearManualSwitchCcw;//	b19  :PLD_Rear_Manual_switch_CCW       
        public bool InPldRearSettingOK_In;//InPldRearManualSwitchCw;//	  b20  :PLD_Rear_Manual_switch_CW       
        public bool InPldXAxisOriginSensor;//	b21  :PLD_X_Axis_origin_sensor         
        public bool InPldFeedingRollerButton;//  b22  :PLD_Feeding_roller_button       
        public bool InPldMainRollerButton;//b23  :PLD_Main_roller_button          
        public bool InPldRightEmergencyButton;//  b24  :PLD_Right_emergency_button      
        public bool InPldXAxisRightLimitSensor;//	b25  :PLD_X_Axis_Right_Limit_sensor    
        public bool InPldRightWasteSensor;//  b26  :PLD_Right_waste_sensor           
        public bool InPldBlack;//	b27  :PLD_BLACK                      
        public bool InPldCyan;//	  b28  :PLD_CYAN                        
        public bool InPldMagenta;//	b29  :PLD_MAGENTA                      
        public bool InPldYellow;//  b30  :PLD_YELLOW                       
        public bool InPldLightBlack;//		b31  :PLD_LIGHT_BLACK                 
        public bool InPldLightCyan;//	  b32  :PLD_LIGHT_CYAN                  
        public bool InPldLightMagenta;//		b33  :PLD_LIGHT_MAGENTA               
        public bool InPldLightYellow;//	  b34  :PLD_LIGHT_YELLOW                
        public bool InPldWiperCyBackSensorIn;//	b35  :PLD_WIPER_CY_BACK_SENSOR_IN     
        public bool InPldWiperCyFrontSensorIn;//  b36  :PLD_WIPER_CY_FRONT_SENSOR_IN   
        public bool InPldMainTankWhite;//	b37  :PLD_Level_sensor_IN9            
        public bool InPldMainTankExtar;//	  b38  :PLD_Level_sensor_IIN10         
        public bool InPldMainTankSolvent;//	b39  :PLD_Level_sensor_IN11            
        public bool InPldLeftWasteSensor;//  b40  :PLD_Level_sensor_IN12           
        public bool InPldLeftEmergencyButton;//	  b41  :PLD_Left_emergency_button        
        public bool InPldXAxisLeftLimitSensor;//	b42  :PLD_X_Axis_left_limit_sensor     
        public bool InPldPressureAirSensor;//		  b43  :PLD_Pressure_air_sensor          
        public bool InPldMediaSensorIn1;//		b44  :PLD_MEDIA_SENSOR_IN1            
        public bool InPldUVReady;//  b45  :PLD_5M_Front_manual_button_CCW   
        public bool InPld_UvSystemError;//		b46  :PLD_REV_SENSOR_IN5              
        public bool InPldRevSensorIn6;//	  b47  :PLD_REV_SENSOR_IN6               
        public bool InPldRightUvLampShutterOpenClose ;//b48  :PLD_REV_SENSOR_IN7               
		public bool InPldLeftUvLampShutterOpenClose;//	  b49  :PLD_5M_Front_manual_button_CW    
		public bool InPldCappingPositionLowSensor;//b50  :PLD_Capping_position_low_sensor 
		public bool InDspEmergencyStop;//			  b51  : DSP_Emergency_stop
                    
	}

    public struct MotionDebugInfo
    {
        public int xPos;
        public int yPos;
        public int nDebugInt1;
        public int nDebugInt2;
        public int nDebugInt3;
        public int nDebugInt4;
    }

    public struct LiyuRipHEADER
    {
        public int nSignature; //must be 0x58485942
        public int nVersion; //must be 0x00000002
        public int nImageWidth; // RIP Image pixel width
        public int nImageHeight; // RIP Image pixel height

        public int nImageColorNum; //RIP image Color number include 4 (YMCK)
        //6 YMCKLcLm  8 YMCKLcLmOrGr
        public int nImageColorDeep; //1,2,4,8 RIP image output bitpercolor
        public int nImageResolutionX; //RIP image X resolution, 180,360,720 
        public int nImageResolutionY; //RIP Image Y resolution, 186,372,558,744

        public int nCompressMode; // First version should be 0 , no compress mode
        public int nBytePerLine; //First version used for no compress mode
        public int nBidirection; // Bidirection  for 1, Unidirection for 0 
        public int nPass; //1,2,3,4,6,8,12 Pass

        public int nSpeed; //High speed 0 Middle speed 1 Low Speed 2 

        ///////////////////////////////Version 2 Change
        public byte nExtraChannel;
        public byte nVSDMode; //Set VSD Mode for EPSON PrintHead driver 
        public byte nEpsonPrintMode; // High Qulity :0 ，High Speed: 1
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)] 
        public byte[] nReserve;
        public byte nAChannel; // prt1颜色数
        public byte nBChannel; // prt2颜色数
        public byte nCChannel; // prt1颜色数
        public byte nDChannel; // prt2颜色数
    }

    public struct CAISHEN_HEADER
    {
        public int uXResolution; //x resolution(dots/inch)
        public int uYResolution; //y resolution(dots/inch)
        public int uImageWidth; //image width(dots)
        public int uImageHeight; //image height(dots)
        public int uGrayBits; //gray level bits
        public int uColors; //colors
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public int[] uReserved; // uReserved 
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct ScorpionParam
    {
        public int WorkPos; // Z axis
        public int ServicePos; // unit mm;
        public int MeshHight; // Mesh Hight;
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct AojetParam
    {
        public uint Flag; // 'AJET'
        public uint zWorkPos;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
        public byte[] rev;
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct ByhxZMoveParam
    {
        public uint Flag; // 'BYHX'
        public uint HeadToPaper;//距离介质高度
        public uint PaperThick;//介质厚度
        public uint activeLen;// 用于版本兼容,表示此变量后的有效字节长度
        public int CleanPlace;
        public int WetPlace;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public byte[] rev;
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        //public byte[] rev;
    }
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct SBiSideSetting
    {
        /// <summary>
        /// //X 方向的调整
        /// </summary>
        public float fxTotalAdjust; 
        /// <summary>
        /// //Y 方向的调整
        /// </summary>
        public float fyTotalAdjust;
        /// <summary>
        /// //左面的调整
        /// </summary>
        public float fLeftTotalAdjust;
        /// <summary>
        /// //右面的调整
        /// </summary>
        public float fRightTotalAdjust; 
        /// <summary>
        /// //调整的步进
        /// </summary>
        public float fStepAdjust;    


        public bool IsEmpty
        {
            get
            {
                return fxTotalAdjust == 0 && fyTotalAdjust == 0 && fLeftTotalAdjust == 0 && fRightTotalAdjust==0;
            }
        }
    };

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct SDoubleSidePrint
    {
        public float PenWidth;
        public int CrossFlag;
        public float CrossWidth;
        public float CrossHeight;
        public float CrossOffsetX;
        public float CrossOffsetY;
        public int AddLineNum;
        public int CrossColor;//1(Y),2(M)4(C)8(K)0x10(Lc)0x20(Lm)

        public SDoubleSidePrint(object oo)
        {
            PenWidth = 1.0f / 25.4f; // 1mm 
#if ALLWIN
            CrossFlag = 1;
            CrossWidth = 15.0f / 25.4f;
            CrossHeight = 15.0f / 25.4f;
            CrossOffsetX = 10.0f / 25.4f;
            CrossOffsetY = 35.0f / 25.4f;
            AddLineNum = 12;
            CrossColor = 0x8;
#else
            CrossFlag = 0;
            CrossWidth = 10.0f / 25.4f;
            CrossHeight = 10.0f / 25.4f;
            CrossOffsetX = 10.0f / 25.4f;
            CrossOffsetY = 100.0f / 25.4f;
            AddLineNum = 5;
            CrossColor = 0xff;
#endif
        }
    };

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct InkWaveform
    {
        public byte TemparatureNumber;
        public byte VoltageNumber;
        public byte MaxmumHeadNumber;
        public byte PrintHeadCategory;
        public byte ColorNumber;
        public byte StartDegree;
        public byte EndDegree;
        public byte SupportTemperature;
        public byte SupportVoltage;
        public byte SupportAutomatic;
    };

    [Serializable]
    public struct ConstructDataSetting {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]//每排孔是否打开
        public byte[] OpenLine;
        public byte PointDivMode;//隔开几个点出一个 0：二抽一 1三抽一 2 四抽一
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] PointDivStart;//隔几个点出一个孔的起始  eg：四抽一 ：  0 0 1 0 为第三个出
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] reverse;
    }

    public enum SPEED : byte
    {
        SPEED_NULL,
        SPEED_50M,
        SPEED_55M,
        SPEED_60M,
        SPEED_70M,
    };

    public enum INK : byte
    {
        INK_NULL,
        INK_UV,
        INK_B,
        INK_C,
        INK_E,
        INK_E2,
        INK_F,
        INK_NE,
        INK_SPEED,
        INK_MG2,
        INK_R,
    };
    public enum KEYKIN : short
    {
        WAVEFORM,
        OTHER,
    }
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public class MyStruct
    {
        public short len; //
        public char Index; //
        public INK Inktype; //
        public SPEED Speed; //
        public char Volume; //
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public char[] HeadList; //
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public char[] ColorOrder; //
        public int FireFreq;
        public short PulseWidth;
        public short DataType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
        public char[] rev;
        //char Data[30 * n]			//30 * strlen(ColorOrder)
        //short Crc16;				//2byte 小端

        public MyStruct()
        {
            len = 0;
            Index = (char)0;
            Inktype = INK.INK_NULL;
            Speed = SPEED.SPEED_NULL;
            Volume = (char)0;
            HeadList = new char[6];
            ColorOrder = new char[16];
            rev = new char[24];
            FireFreq = 0;
            PulseWidth = 0;
            DataType = (short)KEYKIN.WAVEFORM;
        }
    };

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public class InkVoltageCurve
    {
        public short len;
        public char Index;
        public INK Inktype;
        public SPEED Speed;
        public char Volume;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public char[] HeadList;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public char[] ColorOrder;
        public int FireFreq;
        public short PulseWidth;
        public short DataType;
        public InkVoltageCurve()
        {
            HeadList = new char[6];
            ColorOrder = new char[16];
        }
    };

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct ManualCleanParam 
    {
        //通用参数
        public uint nFlag;//s系统cjcs；A+系统cjca；
        public uint nXStartPos;// 第一组喷头清洗开始的x位置
        public byte bySwapHeaders;// 每次刮片刮喷头个数(4*2配色时为2，6*1配色时为1等)
        //S系统参数
        public uint nYEndPos;// 刮片Y向停止位置(Y2)
        public ushort zCylinderTime;// IO控制汽缸持续时间，ms单位
        public ushort CleanPumpTime;// 清洗泵墨持续时间，ms单位
        public byte byYSpeed;// 刮片刮喷头速度
        public byte autoWetFlag;// 自动保湿 1：开启 0：关闭
        public byte autoWetWaitTime;// 自动保湿等待时间 单位分钟
        //A+系统参数
        public ushort ySpeedHz;//刮片运动速度，单位Hz
        public ushort yZeroDelay;//光电触发后，刮片继续移动一段距离，延时停止，单位ms
        public byte DisableFlag;//功能关闭标志，1关闭，0使能
        public byte OriginOffset;//保湿平台原点偏移百分比，0-100
        //S系统参数
        public uint nYStartPos1;// 刮片Y向起始位置1(S)
        public uint nYStartPos2;// 刮片Y向停止位置2 喷头双排排列时有效(S)
        public uint cleanBeltOutTime; // 提供24v输出用于清洗导带,持续时间，单位ms(S系统 208ID)
        public uint pressInkTime; // Press Ink Time
        public uint wiperCleanStart; //Wiper Clean Start
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct ALLWINCleanParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; //!< 'A', 'W', 'C', 0
        public uint xPos; //!< 喷头清洗开始的x位置(小车)
        public uint yPos; //!< 刮片Y向停止位置(刮片)
        public uint zPos; //!< 喷头清洗开始的z位置(刮墨平台)
        public ushort CleanPumpTime; //!< 清洗泵持续时间，ms单位 
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] rev; //!< 保留[6]
    }
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct DocanTextileParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag;			        //!< ('D', 'C', 'T', '/0');
        public int xStartPos;		        //!< 清洗开始的x位置 脉冲
        public int zCLeanPos;		        //!< 清洗Z高度 脉冲
        public ushort PressInkTime;	        //!< 压墨持续时间 ms
        public ushort ZSensorErrRange;      //!< Z位移计传感器允许误差值
        public ushort ZSensorCurVal;	    //!< 当前Z位移计传感器AD采集值，SW只读，用于显示
        public ushort zSensorInitFlag;      //!< 传感器参数已经初始化，FW使用，软件无需理会，界面隐藏
    }
    
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct BoHaoParam
    {
        public uint Flag;			        //!< ('B', 'H', 0, 0);
        public uint PressInkTime;		    //!< 压墨时间
        public uint ZCleanPos;		        //!<墨栈轴刮墨位置
        public uint YCleanPos;	            //!< 刮墨时Y轴移动到的位置
        public uint XDestFar;               //!< X轴远离原点方向的目的地
        public uint XDestOrigin;	        //!< X轴靠近原点方向的目的地
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct PositionSetting_LeCai
    {
        public uint nFlag;
        public uint nForwardDis;        //进料距离
        public uint nZSensorOffset;     //Z距定位传感器位置
        public uint nDefaultPos;        //Z默认所待位置
    }



    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct T125df
    {
        public ushort T1;
        public ushort T2;
        public ushort T5;
        public ushort Td;
        public ushort Ffire;
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    ///外设扩展设置,和数据处理不相干,只有界面和板卡需要知道的参数
    public struct PeripheralExtendedSettings
    {
        /// <summary>
        /// 往返运动长度,用来消除Y轴电机间隙
        /// </summary>
        public float BackAndForthLen;

        public FloraParamUI FloraParam;

        /// <summary>
        /// 双面喷调整的步进
        /// </summary>
        public float StepAdjust;

        /// <summary>
        /// 用于临时存储双平台Y1和Y2原点，打印前根据当前平台赋值给Y打印原点
        /// </summary>
        public float fYOrigin1;

        public float fYOrigin2;
        public HapondMotorParam MotorParam;
        /// <summary>
        /// 多次打印时每次只打一层,分多次打印
        /// </summary>
        public bool EnableSingleLayerMode;

        public float fCalculateRollLength;
        public bool bEnableDetectRollLength;
        public float fDiameterCore;
        public float fDiameterRoll;
        public float fMediaThickness;
        public WhiteInkCycle WhiteInkMixing;
        /// <summary>
        /// 是否为平板模式
        /// 0x843 越达卷板切换专用;纯软件的设置;
        /// </summary>
        public bool IsFlatMode;
        /// <summary>
        /// 卷式机作为平板应用时的y原点,实际用作业间距实现
        /// 越达卷板切换专用
        /// </summary>
        public float fRoll2FlatJobSpace;
        /// <summary>
        /// 越达京瓷导带机平台矫正
        /// </summary>
        public float PlatformCorrect;//

        public UVOffsetDistanceUI UVOffsetDis;

        public int YSpeed;
        public PeripheralExtendedSettings(object oo)
        {
            BackAndForthLen = 0;
            FloraParam = new FloraParamUI();
            StepAdjust = 10/2.54f;
            fYOrigin1 = 0;
            fYOrigin2 = 0;
            MotorParam = new HapondMotorParam();
            EnableSingleLayerMode = false;
            fCalculateRollLength = 0;
            fDiameterCore = 0;
            fDiameterRoll = 0;
            fMediaThickness = 0;
            bEnableDetectRollLength = false;
            WhiteInkMixing = new WhiteInkCycle();
            IsFlatMode = false;
            fRoll2FlatJobSpace = 0;
            PlatformCorrect = 0;

            UVOffsetDis = new UVOffsetDistanceUI();
            UVOffsetDis.OffsetDistArray = new float[8];
            YSpeed = 0;
        }
    }

    [Serializable]
    public struct UVOffsetDistanceUI
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public float[] OffsetDistArray;
    }

	[Serializable]
    public struct KeditecSprayParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag;
        public uint SpraySwitch; // 喷淋开关，决定喷或不喷
        public uint XSprayAreaAStart; // A区喷淋嘴覆盖的最远坐标X（给PLC做喷淋嘴映射）
        public uint XSprayAreaAEnd; // A区喷淋嘴覆盖的最近坐标X
        public uint YSpraySpeed; // Y喷淋时小车移动速度
        public uint XSprayAreaBStart; // B区喷淋嘴覆盖的最远坐标X（给PLC做喷淋嘴映射）
        public uint XSprayAreaBEnd; // B区喷淋嘴覆盖的最近坐标X
        public int  YSprayOffset;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =32)]
        public byte[] rev; // 保留

        public KeditecSprayParam(object oo)
        {
            Flag = new char[4];
            SpraySwitch = 0;
            XSprayAreaAStart = 0;
            XSprayAreaAEnd = 0;
            XSprayAreaBStart = 0;
            XSprayAreaBEnd = 0;
            YSpraySpeed = 0;
            YSprayOffset = 0;
            rev = new byte[32];
        }
    }

    /// <summary>
    /// req = 0x7A, index = 0;只能写不能读，主板不存
    /// 全局/外设参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct PrintingParaType
    {
        public uint rev; //!< rev
        public uint YAdjustDistance; //!< 用于机械比较差的卷轴机，在打印之前先退纸，然后进纸消除张力(脉冲单位).
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)] 
        public byte[] reserve; //保留

        public PrintingParaType(object oo)
        {
            YAdjustDistance = 0;
            reserve = new byte[56];
            rev = 0;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct FloraParamUI
    {
        public float cappingYPlace; //执行清洗时Y的位置，只有T50用(要求只有T50机型才显示，即0x7cb)
        public float cappingZPlace; //保湿时Z位置
        public float cleanZPlace; //清洗时Z位置
        public uint purgeInkTime; //压墨时间单位0.1秒
        public uint purgeInkRecoverTime; //压墨恢复时间单位0.1秒
        public float suckStartPlace; //吸风开始位置，结束位置在原点(T50为Y,T180为X)
        public float suckEndPlace; //吸风结束位置，只有T50用(要求只有T50机型才显示，即0x7cb)
        public bool bIsNeedPrepare; //是否需要喷预处理液, 0 不需要，1需要
        public bool bIsNeedCleanFlash; //是否需要刮墨后闪喷
        public float speed; //英寸/秒
        public float preOffset;					//喷预处理液位置到打印原点偏移(只有T50机型才显示)
        public byte cleanSlotNum; // 清洗槽数量,一般就是喷头排数
        public float cleanSlotSpace; // 清洗槽间距,一般为喷头间距
        public uint cleanMotorSpeed;			// T180, 清洗马达速度
        public ushort doWetDelay; 			// 空闲多久，系统自动进入保湿(unit:s)
        public ushort waitTime;//白墨打印后，打印彩墨前的等待时间可设置(0-600s) 
        public float speedBack;  //喷完处理液回退速度 英寸/秒
        public float scraperStart1;			// 第一排刮片起始位置
        public float scraperStart2;			// 第二排刮片起始位置
    }

    /// <summary>
    /// 每个job打印开始前下发的设置
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct FwJobSettings
    {
        public uint xStartPos; // 彩神预处理液xstart
        public uint xEndPos; //彩神预处理液xend
        public uint yStartPos; //彩神预处理液ystart
        public uint yEndPos; //彩神预处理液yend
    }

    /// <summary>
    /// 每个job打印开始前下发的设置
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct PrintHeadOrder
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; //('P', 'H', 'O', 'r')
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 58)] 
        public byte[] Order; //A+最多16个头,S系统可能喷头更多，

        public PrintHeadOrder(object o)
        {
            Flag = new char[4];
            Order = new byte[58];
        }
    }

    public struct SJetMovePosInfo
    {
        public byte m_nAxil;
        public uint m_nDstPos;
        public uint m_nSpeed;
    }
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct HighFlash
    {
        public uint bOpenHF; // 是否开启高频闪喷
        public uint HFSprayfreq; // 高频闪喷频率HZ
        public uint HFSprayFireNum; // 高频闪喷一个周期内出的点火数.
        public uint HFSprayPeriod; // 高频闪喷循环周期ms
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct LDPParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; // 'LDP'
        public byte bAutoCap; //是否自动cap
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] rev;
        public uint waitTime; //空闲等待时间
    }

    //海邦达用的数据结构
    public struct HapondMotorParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag;         //!< MAKE_4CHAR_CONST_BE('H', 'A', 'P', 'D')
        public byte sysMode;      //!< hapond 系统电机控制模式，力矩or脉冲
        public byte rev;          //!< 备用
        public short torValue;        //!< 电机单元，扭矩模式，控制扭矩的大小
        public uint headAdjustHei;   //!< 电机单元，模式切换T2P，反向移动七分之一喷头高度
        public short torSwitchStepVal;//扭矩切换是，平滑递增/减 步进值
        public short torSwitchStepTime;//扭矩切换时，平滑递增/减 步进时间ms
    }

    //通用的数据结构
    public struct HapondMotorParam_Auto
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag;         //!< MAKE_4CHAR_CONST_BE('Y', 'T', 'O', 'Q')
        public byte sysMode;        //!< hapond 系统电机控制模式，力矩or脉冲
        public byte motorType;      //
        public short torValue;      //!< 电机单元，扭矩模式，控制扭矩的大小
        public uint headAdjustHei;   //!< 电机单元，模式切换T2P，反向移动七分之一喷头高度
        public short torSwitchStepVal;//扭矩切换是，平滑递增/减 步进值
        public short torSwitchStepTime;//扭矩切换时，平滑递增/减 步进时间ms
        public short StepTimeOut;   //步进超时时间ms
        public byte SwitchType;
    }

    public struct WhiteInkCycle
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag;//!< 'WICF'//white ink cycle flag

        public uint PulseTime;	//!< 脉宽

        public uint CycTime;		//!< 周期

        //public uint rev; //
        public ushort StirPulse;//白墨搅拌 脉宽s
        public ushort StirCyc;//白墨搅拌 周期s
    }

    public struct GZUVX2Param
    {
        public uint Flag;//('G','Z','X',0)
        public ushort UVX2Power;//x2功率
        public ushort UVX1Power;//x1功率
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
        public byte[] Rev;
    }


    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct NozzleLineData
    {
        public ushort ID;
        public byte HeadID;
        public byte ColorID;
        public short DataChannel;
        public short VoltageChannel;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Name;
    };


    public struct HeadSerialNumber
    {
        public HeadSerialNumberHead head;
        public List<List<byte>> data;
    }

    public struct HeadSerialNumberHead
    {
        public ushort Type;
        public ushort Size;
        public ushort Reserve;
        public byte SnNum;
        public byte SnLen;
    }

    public struct WaveName
    {
        public WaveNameHead WaveNameHead;
        public List<NameHead> NameHead;
        public List<string> Data;
    }

    public struct WaveNameHead
    {
        public ushort Type;
        public ushort Size;
        public ushort Reserve;
        public ushort NameNum;
    }

    public struct NameHead
    {
        public ushort Ptr;
        public ushort Len;
    }

    #region 同步墨量 墨量统计
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct InkOfMonths
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] flag;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public InkOfColors[] inkOfColors;
    }
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct InkOfColors
    {
        /// <summary>
        /// 颜色1到8的使用墨量
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public uint[] colors; // 
    }
    [StructLayout(LayoutKind.Sequential,Pack = 4)]
    [Serializable]
    public struct AreaOfMonths
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] flag;
        /// <summary>
        /// 1-12个月的打印面积
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public double[] Area;
    }
    #endregion

    public class ChannelInfo
    {
        public int Id
        { get; set; }

        public string OriginalName
        { get; set; }

        public string Name
        { get; set; }

        public string OriginalValue
        { get; set; }

        public byte Value
        { get; set; }
    }
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct HbdGetRuningParam_tag
    {
        public byte size;      //!< 数据包大小
        public byte cmd;      //!< 命令字
        public ushort fixedCurve;  //!< 头板版本里存储的波形温度曲线标志
        public uint verRunFlag;  //!< 头板版本里存储的各个功能运行标志
        public uint curRunFlag;  //!< 当前运行中的头板各个功能实际运行标志
        public uint TCurveMode;  //!< 当前头板人为设置的温度曲线模式
    }

    [Serializable]
    public struct DynamicData
    {
        public byte curcolorindex;
        public byte curgroupindex;
        public byte curinterleaveindex;
        public int row;
        public int line;
    }
}
