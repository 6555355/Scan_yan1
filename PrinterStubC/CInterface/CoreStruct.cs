/* 
	��Ȩ���� 2006��2007��������Դ��о�Ƽ����޹�˾����������Ȩ����
	ֻ�б�������Դ��о�Ƽ����޹�˾��Ȩ�ĵ�λ���ܸ��ĳ�д�ʹ�����
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

    /* JobPrint.dll:������Ű�ӿ�
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
        public double X;//��ƴ��ͼ�ϸ�ͼ����λ��X
        public double Y;//��ƴ��ͼ�ϸ�ͼ����λ��Y
        public ImageTileItemClip ImageTileItemClip;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CoreConst.MAX_PATH)]
        public string File;//prt�����ļ���
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct NoteInfo
    {
        public int FontSize;
        /// <summary>
        /// Regular=0;Bold=1;Italic=2;Underline=4;Strikeout=8
        /// �����������3=Bold|Italic���Դ�����
        /// </summary>
        public int fontStyle;
        /// <summary>
        /// ע����ͼƬ�ľ���
        /// </summary>
        public int NoteMargin;
        /// <summary>
        /// 0:left;1:top;2:right;3:bottom
        /// </summary>
        public int NotePosition;
        /// <summary>
        /// ��ҵ��С���ֱ��ʡ�pass���������ļ�·��
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
        public string File;//prt�����ļ���
        /// <summary>
        /// ����ƴ��ǰ�����ļ��Ķ���Ԥ��ͼ
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
        //��Prt���е�����Լ����п��
        public double ClipX;
        public double ClipY;
        public double ClipW;
        public double ClipH;
    }

    /// <summary>
    /// ���ļ�ƴ����Ϣ
    /// </summary>
    [Serializable]
    public struct MultiFileTileInfo
    {
        public List<ImageUITileItem> TileImages;
        public float Width;//��λӢ��
        public float Height;//��λӢ��
    }

    /*
     * typedef struct contrast_color{
	char Color;//ͼ���:'0';רɫ��:'K' 'C' 'M' 'Y' 'W'...
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
        public int Gray;//0-ff;������Ϊ0-100��������Ҫת����UIValue*255/100��
        public int Mask;
        public int SetType;//Ĭ��0Ϊ������1Ϊ����
        public int Inverse;//�Ƿ�ѡȡ��������ѡ��Ϊ1������Ϊ0
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
        /// ɨ��������0x01��һ������̨(AXIS_X); 0x08�ڶ�������̨(AXIS_4)
        /// ����t-shirt��
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
        /// ��ӡ��ɺ�Y������ԭ��[ȫӡ]
        /// 0:���������;1:��ԭ��; 2:����ԭ��
        /// </summary>
        public byte Yorg;

        /// <summary>
        /// ���δ�ӡ�̴߳���ڼ䵱ǰ��ҵ����λ��
        /// ��������ӡ����
        /// </summary>
        public ushort nJobIndex;

        public int nJobID;

        /// <summary>
        /// ��ǰjob�Ƿ���Ҫ�ȴ���ʼ��ӡ�ź�
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] public bool bNeedWaitPrintStartSignal;

        /// <summary>
        /// ȫӡ˫ƽ̨
        /// 1����ʾͼ����ֻ�в�ɫ��2����ʾ��ҵ�м��в�ɫҲ�а�ɫ//
        /// </summary>
        public byte ColorType; //

        /// <summary>
        /// 0 ��ʾŨ��100%��1 ��ʾŨ�Ƚ���1/255, 255 ��ʾŨ��Ϊ0 
        /// </summary>
        public byte cNegMaxGray; // for skyship

        /// <summary>
        /// �Ƿ�Ϊ����prt�ֶ���δ�ӡʱ�����һ��,ӡȾӦ��,fw�ݴ˾����Ƿ������ӡ����ź�;
        /// 0:�����ӡ;1:����ӡ�м��;2:����ӡ�����һ��
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
        /// ɨ��������0x01��һ������̨(AXIS_X); 0x08�ڶ�������̨(AXIS_4)
        /// ����t-shirt��
        /// </summary>
	    public byte scanningAxis;
	        [MarshalAs(UnmanagedType.I1)]
        public bool bIsDouble4CJob;
            /// <summary>
            /// 0 ��ʾŨ��100%��1 ��ʾŨ�Ƚ���1/255, 255 ��ʾŨ��Ϊ0 
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
        public byte m_nTempCoff;			//�¿�ϵ��
        public ushort m_usReserve;          //Ԥ��
        public byte m_xaar382_pixle_mode;   //45 pixle  or cycle ģʽ
        public byte m_PrintHeadCnt;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] rev;
	    public float ServePos;  // ����վλ��
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
		public ushort m_cCrc;     // �ֽڵ��ۼ�
		public byte   m_bUse;      //�Ƿ����
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
        public byte nEpsonPrintMode; // High Qulity :0 ��High Speed: 1
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)] 
        public byte[] nReserve;
        public byte nAChannel; // prt1��ɫ��
        public byte nBChannel; // prt2��ɫ��
        public byte nCChannel; // prt1��ɫ��
        public byte nDChannel; // prt2��ɫ��
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
        public uint HeadToPaper;//������ʸ߶�
        public uint PaperThick;//���ʺ��
        public uint activeLen;// ���ڰ汾����,��ʾ�˱��������Ч�ֽڳ���
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
        /// //X ����ĵ���
        /// </summary>
        public float fxTotalAdjust; 
        /// <summary>
        /// //Y ����ĵ���
        /// </summary>
        public float fyTotalAdjust;
        /// <summary>
        /// //����ĵ���
        /// </summary>
        public float fLeftTotalAdjust;
        /// <summary>
        /// //����ĵ���
        /// </summary>
        public float fRightTotalAdjust; 
        /// <summary>
        /// //�����Ĳ���
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
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]//ÿ�ſ��Ƿ��
        public byte[] OpenLine;
        public byte PointDivMode;//�����������һ�� 0������һ 1����һ 2 �ĳ�һ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] PointDivStart;//���������һ���׵���ʼ  eg���ĳ�һ ��  0 0 1 0 Ϊ��������
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
        //short Crc16;				//2byte С��

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
        //ͨ�ò���
        public uint nFlag;//sϵͳcjcs��A+ϵͳcjca��
        public uint nXStartPos;// ��һ����ͷ��ϴ��ʼ��xλ��
        public byte bySwapHeaders;// ÿ�ι�Ƭ����ͷ����(4*2��ɫʱΪ2��6*1��ɫʱΪ1��)
        //Sϵͳ����
        public uint nYEndPos;// ��ƬY��ֹͣλ��(Y2)
        public ushort zCylinderTime;// IO�������׳���ʱ�䣬ms��λ
        public ushort CleanPumpTime;// ��ϴ��ī����ʱ�䣬ms��λ
        public byte byYSpeed;// ��Ƭ����ͷ�ٶ�
        public byte autoWetFlag;// �Զ���ʪ 1������ 0���ر�
        public byte autoWetWaitTime;// �Զ���ʪ�ȴ�ʱ�� ��λ����
        //A+ϵͳ����
        public ushort ySpeedHz;//��Ƭ�˶��ٶȣ���λHz
        public ushort yZeroDelay;//��紥���󣬹�Ƭ�����ƶ�һ�ξ��룬��ʱֹͣ����λms
        public byte DisableFlag;//���ܹرձ�־��1�رգ�0ʹ��
        public byte OriginOffset;//��ʪƽ̨ԭ��ƫ�ưٷֱȣ�0-100
        //Sϵͳ����
        public uint nYStartPos1;// ��ƬY����ʼλ��1(S)
        public uint nYStartPos2;// ��ƬY��ֹͣλ��2 ��ͷ˫������ʱ��Ч(S)
        public uint cleanBeltOutTime; // �ṩ24v���������ϴ����,����ʱ�䣬��λms(Sϵͳ 208ID)
        public uint pressInkTime; // Press Ink Time
        public uint wiperCleanStart; //Wiper Clean Start
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct ALLWINCleanParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; //!< 'A', 'W', 'C', 0
        public uint xPos; //!< ��ͷ��ϴ��ʼ��xλ��(С��)
        public uint yPos; //!< ��ƬY��ֹͣλ��(��Ƭ)
        public uint zPos; //!< ��ͷ��ϴ��ʼ��zλ��(��īƽ̨)
        public ushort CleanPumpTime; //!< ��ϴ�ó���ʱ�䣬ms��λ 
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] rev; //!< ����[6]
    }
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct DocanTextileParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag;			        //!< ('D', 'C', 'T', '/0');
        public int xStartPos;		        //!< ��ϴ��ʼ��xλ�� ����
        public int zCLeanPos;		        //!< ��ϴZ�߶� ����
        public ushort PressInkTime;	        //!< ѹī����ʱ�� ms
        public ushort ZSensorErrRange;      //!< Zλ�Ƽƴ������������ֵ
        public ushort ZSensorCurVal;	    //!< ��ǰZλ�Ƽƴ�����AD�ɼ�ֵ��SWֻ����������ʾ
        public ushort zSensorInitFlag;      //!< �����������Ѿ���ʼ����FWʹ�ã����������ᣬ��������
    }
    
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct BoHaoParam
    {
        public uint Flag;			        //!< ('B', 'H', 0, 0);
        public uint PressInkTime;		    //!< ѹīʱ��
        public uint ZCleanPos;		        //!<īջ���īλ��
        public uint YCleanPos;	            //!< ��īʱY���ƶ�����λ��
        public uint XDestFar;               //!< X��Զ��ԭ�㷽���Ŀ�ĵ�
        public uint XDestOrigin;	        //!< X�῿��ԭ�㷽���Ŀ�ĵ�
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct PositionSetting_LeCai
    {
        public uint nFlag;
        public uint nForwardDis;        //���Ͼ���
        public uint nZSensorOffset;     //Z�ඨλ������λ��
        public uint nDefaultPos;        //ZĬ������λ��
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
    ///������չ����,�����ݴ������,ֻ�н���Ͱ忨��Ҫ֪���Ĳ���
    public struct PeripheralExtendedSettings
    {
        /// <summary>
        /// �����˶�����,��������Y������϶
        /// </summary>
        public float BackAndForthLen;

        public FloraParamUI FloraParam;

        /// <summary>
        /// ˫��������Ĳ���
        /// </summary>
        public float StepAdjust;

        /// <summary>
        /// ������ʱ�洢˫ƽ̨Y1��Y2ԭ�㣬��ӡǰ���ݵ�ǰƽ̨��ֵ��Y��ӡԭ��
        /// </summary>
        public float fYOrigin1;

        public float fYOrigin2;
        public HapondMotorParam MotorParam;
        /// <summary>
        /// ��δ�ӡʱÿ��ֻ��һ��,�ֶ�δ�ӡ
        /// </summary>
        public bool EnableSingleLayerMode;

        public float fCalculateRollLength;
        public bool bEnableDetectRollLength;
        public float fDiameterCore;
        public float fDiameterRoll;
        public float fMediaThickness;
        public WhiteInkCycle WhiteInkMixing;
        /// <summary>
        /// �Ƿ�Ϊƽ��ģʽ
        /// 0x843 Խ�����л�ר��;�����������;
        /// </summary>
        public bool IsFlatMode;
        /// <summary>
        /// ��ʽ����Ϊƽ��Ӧ��ʱ��yԭ��,ʵ������ҵ���ʵ��
        /// Խ�����л�ר��
        /// </summary>
        public float fRoll2FlatJobSpace;
        /// <summary>
        /// Խ�ﾩ�ɵ�����ƽ̨����
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
        public uint SpraySwitch; // ���ܿ��أ����������
        public uint XSprayAreaAStart; // A�������츲�ǵ���Զ����X����PLC��������ӳ�䣩
        public uint XSprayAreaAEnd; // A�������츲�ǵ��������X
        public uint YSpraySpeed; // Y����ʱС���ƶ��ٶ�
        public uint XSprayAreaBStart; // B�������츲�ǵ���Զ����X����PLC��������ӳ�䣩
        public uint XSprayAreaBEnd; // B�������츲�ǵ��������X
        public int  YSprayOffset;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =32)]
        public byte[] rev; // ����

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
    /// req = 0x7A, index = 0;ֻ��д���ܶ������岻��
    /// ȫ��/�������
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct PrintingParaType
    {
        public uint rev; //!< rev
        public uint YAdjustDistance; //!< ���ڻ�е�Ƚϲ�ľ�������ڴ�ӡ֮ǰ����ֽ��Ȼ���ֽ��������(���嵥λ).
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)] 
        public byte[] reserve; //����

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
        public float cappingYPlace; //ִ����ϴʱY��λ�ã�ֻ��T50��(Ҫ��ֻ��T50���Ͳ���ʾ����0x7cb)
        public float cappingZPlace; //��ʪʱZλ��
        public float cleanZPlace; //��ϴʱZλ��
        public uint purgeInkTime; //ѹīʱ�䵥λ0.1��
        public uint purgeInkRecoverTime; //ѹī�ָ�ʱ�䵥λ0.1��
        public float suckStartPlace; //���翪ʼλ�ã�����λ����ԭ��(T50ΪY,T180ΪX)
        public float suckEndPlace; //�������λ�ã�ֻ��T50��(Ҫ��ֻ��T50���Ͳ���ʾ����0x7cb)
        public bool bIsNeedPrepare; //�Ƿ���Ҫ��Ԥ����Һ, 0 ����Ҫ��1��Ҫ
        public bool bIsNeedCleanFlash; //�Ƿ���Ҫ��ī������
        public float speed; //Ӣ��/��
        public float preOffset;					//��Ԥ����Һλ�õ���ӡԭ��ƫ��(ֻ��T50���Ͳ���ʾ)
        public byte cleanSlotNum; // ��ϴ������,һ�������ͷ����
        public float cleanSlotSpace; // ��ϴ�ۼ��,һ��Ϊ��ͷ���
        public uint cleanMotorSpeed;			// T180, ��ϴ����ٶ�
        public ushort doWetDelay; 			// ���ж�ã�ϵͳ�Զ����뱣ʪ(unit:s)
        public ushort waitTime;//��ī��ӡ�󣬴�ӡ��īǰ�ĵȴ�ʱ�������(0-600s) 
        public float speedBack;  //���괦��Һ�����ٶ� Ӣ��/��
        public float scraperStart1;			// ��һ�Ź�Ƭ��ʼλ��
        public float scraperStart2;			// �ڶ��Ź�Ƭ��ʼλ��
    }

    /// <summary>
    /// ÿ��job��ӡ��ʼǰ�·�������
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct FwJobSettings
    {
        public uint xStartPos; // ����Ԥ����Һxstart
        public uint xEndPos; //����Ԥ����Һxend
        public uint yStartPos; //����Ԥ����Һystart
        public uint yEndPos; //����Ԥ����Һyend
    }

    /// <summary>
    /// ÿ��job��ӡ��ʼǰ�·�������
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct PrintHeadOrder
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; //('P', 'H', 'O', 'r')
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 58)] 
        public byte[] Order; //A+���16��ͷ,Sϵͳ������ͷ���࣬

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
        public uint bOpenHF; // �Ƿ�����Ƶ����
        public uint HFSprayfreq; // ��Ƶ����Ƶ��HZ
        public uint HFSprayFireNum; // ��Ƶ����һ�������ڳ��ĵ����.
        public uint HFSprayPeriod; // ��Ƶ����ѭ������ms
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct LDPParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; // 'LDP'
        public byte bAutoCap; //�Ƿ��Զ�cap
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] rev;
        public uint waitTime; //���еȴ�ʱ��
    }

    //������õ����ݽṹ
    public struct HapondMotorParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag;         //!< MAKE_4CHAR_CONST_BE('H', 'A', 'P', 'D')
        public byte sysMode;      //!< hapond ϵͳ�������ģʽ������or����
        public byte rev;          //!< ����
        public short torValue;        //!< �����Ԫ��Ť��ģʽ������Ť�صĴ�С
        public uint headAdjustHei;   //!< �����Ԫ��ģʽ�л�T2P�������ƶ��߷�֮һ��ͷ�߶�
        public short torSwitchStepVal;//Ť���л��ǣ�ƽ������/�� ����ֵ
        public short torSwitchStepTime;//Ť���л�ʱ��ƽ������/�� ����ʱ��ms
    }

    //ͨ�õ����ݽṹ
    public struct HapondMotorParam_Auto
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag;         //!< MAKE_4CHAR_CONST_BE('Y', 'T', 'O', 'Q')
        public byte sysMode;        //!< hapond ϵͳ�������ģʽ������or����
        public byte motorType;      //
        public short torValue;      //!< �����Ԫ��Ť��ģʽ������Ť�صĴ�С
        public uint headAdjustHei;   //!< �����Ԫ��ģʽ�л�T2P�������ƶ��߷�֮һ��ͷ�߶�
        public short torSwitchStepVal;//Ť���л��ǣ�ƽ������/�� ����ֵ
        public short torSwitchStepTime;//Ť���л�ʱ��ƽ������/�� ����ʱ��ms
        public short StepTimeOut;   //������ʱʱ��ms
        public byte SwitchType;
    }

    public struct WhiteInkCycle
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag;//!< 'WICF'//white ink cycle flag

        public uint PulseTime;	//!< ����

        public uint CycTime;		//!< ����

        //public uint rev; //
        public ushort StirPulse;//��ī���� ����s
        public ushort StirCyc;//��ī���� ����s
    }

    public struct GZUVX2Param
    {
        public uint Flag;//('G','Z','X',0)
        public ushort UVX2Power;//x2����
        public ushort UVX1Power;//x1����
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

    #region ͬ��ī�� ī��ͳ��
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
        /// ��ɫ1��8��ʹ��ī��
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
        /// 1-12���µĴ�ӡ���
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
        public byte size;      //!< ���ݰ���С
        public byte cmd;      //!< ������
        public ushort fixedCurve;  //!< ͷ��汾��洢�Ĳ����¶����߱�־
        public uint verRunFlag;  //!< ͷ��汾��洢�ĸ����������б�־
        public uint curRunFlag;  //!< ��ǰ�����е�ͷ���������ʵ�����б�־
        public uint TCurveMode;  //!< ��ǰͷ����Ϊ���õ��¶�����ģʽ
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
