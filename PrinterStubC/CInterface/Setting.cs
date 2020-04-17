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
using System.Xml;
using System.IO;

namespace BYHXPrinterManager
{
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
    public struct SFrequencySetting : ICloneable
	{
		public int		nResolutionX;
		public int		nResolutionY;
		public byte		nBidirection;
		public byte		nPass;
	
		[MarshalAs(UnmanagedType.I4)]
		public SpeedEnum		nSpeed;
		///byte		nResIndex;
		public int			bUsePrinterSetting;
		public float		fXOrigin;

	    #region Implementation of ICloneable

	    /// <summary>
	    /// Creates a new object that is a copy of the current instance.
	    /// </summary>
	    /// <returns>
	    /// A new object that is a copy of this instance.
	    /// </returns>
	    /// <filterpriority>2</filterpriority>
	    public object Clone()
	    {
	        SFrequencySetting ret = new SFrequencySetting();
            ret.nResolutionX = nResolutionX;
            ret.nResolutionY = nResolutionY;
            ret.nBidirection = nBidirection;
            ret.nPass = nPass;
	        ret.nSpeed = this.nSpeed;
	        ret.bUsePrinterSetting = this.bUsePrinterSetting;
	        ret.fXOrigin = this.fXOrigin;
	        return ret;
	    }

	    #endregion
	}
	public enum EnumStripeType
	{
		Normal = 0x1,
		ColorMixed = 0x2,
		HeightWithImage = 0x4,
	}
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SColorBarSetting:ICloneable
	{
		public float			fStripeOffset;
		public float			fStripeWidth;
		[MarshalAs(UnmanagedType.I4)]
		public InkStrPosEnum	eStripePosition;
        public byte bNormalStripeType;
        public byte nStripInkPercent;//彩条墨量（百分比）当前仅支持：25%,50%,75%,100%
        public byte rev1;
        /// <summary>
        /// 0:软件彩条;1机械彩条
        /// </summary>
        public byte StripeType;

	    #region Implementation of ICloneable

	    /// <summary>
	    /// Creates a new object that is a copy of the current instance.
	    /// </summary>
	    /// <returns>
	    /// A new object that is a copy of this instance.
	    /// </returns>
	    /// <filterpriority>2</filterpriority>
	    public object Clone()
	    {
	        SColorBarSetting ret = new SColorBarSetting();
            ret.fStripeOffset = this.fStripeOffset;
            ret.fStripeWidth = this.fStripeWidth;
            ret.eStripePosition = this.eStripePosition;
            ret.bNormalStripeType = this.bNormalStripeType;
	        ret.StripeType = this.StripeType;
	        return ret;
	    }

	    #endregion
	}
	public enum EnumWhiteInkImage
	{
		All=0,
		Rip,
		Image
	};
	public enum EnumWhiteInkOperation
	{
		OR = 0,
		OR_Not = 1,
		Intersect = 2,
		Intersect_Not =3,
	};
	public enum EnumLayerType
	{
		Color = 0,
		White = 1,
        Varnish = 2,
        Color2 = 3, //AWB模式下指示数据来源于B
        //Contrast = 3,
	};

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct DOUBLE_YAXIS
    {
        public uint Flag; //flag
        public uint YResolution; //Y轴分辨率
        public float fMaxoffsetpos; //两轴最大偏差位置，英寸单位
        public float fMaxTolerancepos; //两轴最大容差位置，校正位置偏差使用，英寸单位
        public float DoubeYRatio; //双轴偏差比例系数
        public float DrvEncRatio1; // 14 Y驱动脉冲和编码器反馈的比率1
        public float DrvEncRatio2; // 18 Y驱动脉冲和编码器反馈的比率2

        public byte bCorrectoffset; //是否支持矫正位置功能
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 19)]
        public byte[] rev; //保留11个字节
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct CleanStep
    {
        /// <summary>
        /// 
        /// </summary>
        public uint Xpos;

        /// <summary>
        /// 
        /// </summary>
        public uint Ypos;

        /// <summary>
        /// // 单位s
        /// </summary>
        public uint DelaySeconds; 
    }
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct CleanPos_tag

    {
        public uint Xpos;
        public uint Ypos;
        public ushort Xspeed;
        public ushort Yspeed;
    }
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct SandPos_tag
    {
        public uint Xpos;
        public uint Ypos;
    }
	[Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct S_3DPrint
    {
        public uint Flag;            //Flag

        /// <summary>
        /// 平台下降距离
        /// </summary>
        public uint nZDownDis;      //
        /// <summary>
        /// 压墨位置
        /// </summary>
        public CleanStep purge;		// 
        /// <summary>
        /// 刮墨位置
        /// </summary>
        public CleanStep wipe;			// 
        /// <summary>
        /// 清洗频率，单位job
        /// </summary>
        public uint rate;			// 
        public SandPos_tag sand;         // 下沙位置.
        public CleanPos_tag clean;     // 清洗位置及速度.
    }

	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SBaseSetting:ICloneable
	{
		public float	fLeftMargin;
		public float	fPaperWidth;
		public float	fTopMargin;
		public float	fPaperHeight;
		public float    fZSpace;
		public float    fPaperThick;
		public float    fYOrigin;


		public float	fJobSpace; 
		public float	fStepTime; 
		public int		nAccDistance;
		public float    fMeasureMargin;

		public byte		nAdvanceFeatherPercent;
        public byte     nFeatherType;
		public byte		nXResutionDiv;
		[MarshalAs(UnmanagedType.I1)]
		public bool		bNozzleBlock;
        //[MarshalAs(UnmanagedType.I1)]
		public byte		bFeatherMaxNew;  //Not Use it ??????????????????????
        /// <summary>
        /// bool型变量仅为兼容老的配置文件,程序中不应使用次属性
        /// </summary>
	    public bool bFeatherMax // 兼容老的setting.xml配置文件
	    {
	        get { return bFeatherMaxNew == 1; }
	        set { bFeatherMaxNew = (byte) (value ? 1 : 0); }
	    }
		public byte		nYPrintSpeed;
		[MarshalAs(UnmanagedType.I1)]
		public bool		bMeasureBeforePrint;
		[MarshalAs(UnmanagedType.I1)]
		public bool		bAutoYCalibration;

		[MarshalAs(UnmanagedType.I1)]
		public bool		bYPrintContinue;
		[MarshalAs(UnmanagedType.I1)]
		public bool		bUseMediaSensor;
        /// <summary>
        /// 打印x方向白[注意:历史原因变量名与含义相反]
        /// </summary>
		[MarshalAs(UnmanagedType.I1)]
		public bool		bIgnorePrintWhiteX;
        /// <summary>
        /// 打印y方向白[注意:历史原因变量名与含义相反]
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
		public bool		bIgnorePrintWhiteY;
		public byte		multipleWriteInk;
        public float fYAddDistance;//uint		eRev23;//Not Use it ??????????????????????
		public int         nFeatherPercent;

		public SColorBarSetting sStripeSetting;
		public float fFeatherWavelength;
		public ushort nSpotColor1Mask; //BIT 15: BIT8 KCMYLMLCORGR :  BIT5:INTERSEC & ,BIT4:NOT ~, BIT3-BIT0: 0: 不打印 1： ALL 2：RIP 3：Image 
		public ushort nSpotColor2Mask;
		public uint  nLayerColorArray;  //平排：BIT01= Layer1ColrIndex ,BIT23 = Layer2ColrIndex ;BIT45 = Layer3ColrIndex
											// 错排： BIT0= 彩色打印: BIT1： 白色打印: BIT2： 亮油打印 

		public float fAutoCleanPosMov;
		public float fAutoCleanPosLen;

        [MarshalAs(UnmanagedType.I1)]
        public bool bAutoCenterPrint; 

        [MarshalAs(UnmanagedType.I1)]
        public bool bUseFeather; // 20140708 gzw modify
        public ushort bitRegion; //功能位 1:VolumeConvert
        //public byte reserve6;
        public float fZAdjustmentDistance; // 打印完后Z轴移动距离，单位英寸

		[MarshalAs(UnmanagedType.I1)]
		public bool  bMirrorX;
		public byte  nWhiteInkLayer; // 表示白墨的层数， 0：C  1： C  2：WC  3：WCV  4：CWCV 5： VCWCV  Other no defaultvalue， MAX Is 8
		[MarshalAs(UnmanagedType.I1)]
		public bool  bReversePrint;
		public byte  multipleInk;


	    #region Implementation of ICloneable

	    /// <summary>
	    /// Creates a new object that is a copy of the current instance.
	    /// </summary>
	    /// <returns>
	    /// A new object that is a copy of this instance.
	    /// </returns>
	    /// <filterpriority>2</filterpriority>
        public object Clone()
	    {
	        SBaseSetting ret = new SBaseSetting();
	        ret.fLeftMargin = this.fLeftMargin;
	        ret.fPaperWidth = this.fPaperWidth;
	        ret.fTopMargin = this.fTopMargin;
	        ret.fPaperHeight = this.fPaperHeight;
	        ret.fZSpace = this.fZSpace;
	        ret.fPaperThick = this.fPaperThick;
	        ret.fYOrigin = this.fYOrigin;

	        ret.fJobSpace = this.fJobSpace;
	        ret.fStepTime = this.fStepTime;
	        ret.nAccDistance = this.nAccDistance;
	        ret.fMeasureMargin = this.fMeasureMargin;

	        ret.nAdvanceFeatherPercent = this.nAdvanceFeatherPercent;
	        ret.nFeatherType = this.nFeatherType;
	        ret.nXResutionDiv = this.nXResutionDiv;
	        ret.bNozzleBlock = this.bNozzleBlock;
	        ret.bFeatherMax = this.bFeatherMax;
	        ret.nYPrintSpeed = this.nYPrintSpeed;
	        ret.bMeasureBeforePrint = this.bMeasureBeforePrint;
	        ret.bAutoYCalibration = this.bAutoYCalibration;
	        ret.bYPrintContinue = this.bYPrintContinue;
	        ret.bUseMediaSensor = this.bUseMediaSensor;
	        ret.bIgnorePrintWhiteX = this.bIgnorePrintWhiteX;
	        ret.bIgnorePrintWhiteY = this.bIgnorePrintWhiteY;
	        ret.multipleWriteInk = this.multipleWriteInk;
            ret.fYAddDistance = this.fYAddDistance; //Not Use it
	        ret.nFeatherPercent = this.nFeatherPercent;
	        ret.sStripeSetting = (SColorBarSetting) this.sStripeSetting.Clone();


	        ret.fFeatherWavelength = this.fFeatherWavelength;
	        ret.nSpotColor1Mask = this.nSpotColor1Mask;
	        ret.nSpotColor2Mask = this.nSpotColor2Mask;
	        ret.nLayerColorArray = this.nLayerColorArray;


	        ret.fAutoCleanPosMov = this.fAutoCleanPosMov;
	        ret.fAutoCleanPosLen = this.fAutoCleanPosLen;

            ret.bitRegion = this.bitRegion;
            ret.bUseFeather = this.bUseFeather;
	        ret.bMirrorX = this.bMirrorX;
	        ret.nWhiteInkLayer = this.nWhiteInkLayer;
	        ret.bReversePrint = this.bReversePrint;
            ret.multipleInk = this.multipleInk;
            ret.bAutoCenterPrint = this.bAutoCenterPrint;

	        return ret;
	    }

	    #endregion
	}
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SBaseSetting_V1
	{
		public float	fLeftMargin;
		public float	fPaperWidth;
		public float	fTopMargin;
		public float	fPaperHeight;
		public float    fZSpace;
		public float    fPaperThick;
		public float    fYOrigin;


		public float	fJobSpace; 
		public float	fStepTime; 
		public int		nAccDistance;
		public float    fMeasureMargin;

		public byte		nAdvanceFeatherPercent;
		public byte     nFeatherType;
		public byte		bPrintMode;
		[MarshalAs(UnmanagedType.I1)]
		public bool		bNozzleBlock;
		public byte		nRev21;
		public byte		nYPrintSpeed;
		[MarshalAs(UnmanagedType.I1)]
		public bool		bMeasureBeforePrint;
		[MarshalAs(UnmanagedType.I1)]
		public bool		bAutoYCalibration;

		[MarshalAs(UnmanagedType.I1)]
		public bool		bYPrintContinue;
		[MarshalAs(UnmanagedType.I1)]
		public bool		bUseMediaSensor;
		[MarshalAs(UnmanagedType.I1)]
		public bool		bIgnorePrintWhiteX;
		[MarshalAs(UnmanagedType.I1)]
		public bool		bIgnorePrintWhiteY;
		[MarshalAs(UnmanagedType.I1)]
		public bool		bRev22;//Not Use it ??????????????????????
		public uint		eRev23;//Not Use it ??????????????????????
		public int         nFeatherPercent;

		public SColorBarSetting sStripeSetting;
	}
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SMoveSetting:ICloneable
	{
		public byte	nXMoveSpeed;				// 小车速度，取值范围1-8
		public byte	nYMoveSpeed;				// 进退纸速度，取值范围1-8
		public byte	nZMoveSpeed;				// Z轴速度，取值范围1-8
		public byte	n4MoveSpeed;			// 编码分频 ??????should only for Move or Print 

	    #region Implementation of ICloneable

	    /// <summary>
	    /// Creates a new object that is a copy of the current instance.
	    /// </summary>
	    /// <returns>
	    /// A new object that is a copy of this instance.
	    /// </returns>
	    /// <filterpriority>2</filterpriority>
	    public object Clone()
	    {
	        SMoveSetting ret = new SMoveSetting();
	        ret.nXMoveSpeed = nXMoveSpeed;
	        ret.nYMoveSpeed = nYMoveSpeed;
	        ret.nZMoveSpeed = nZMoveSpeed;
	        ret.n4MoveSpeed = n4MoveSpeed;
	        return ret;
	    }

	    #endregion
	}
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
    public struct SCleanerSetting : ICloneable
	{
		public int	nCleanerPassInterval;
		public int	nSprayPassInterval;
 
		public int	nCleanerTimes;
		public int	nSprayFireInterval;//闪喷间隔，以毫秒为单位
	
		public int	nSprayTimes;
		public int	nCleanIntensity;
								
		[MarshalAs(UnmanagedType.I1)]
		public bool	bSprayWhileIdle;
		[MarshalAs(UnmanagedType.I1)]
		public bool	bSprayBeforePrint;

		public ushort nPauseTimeAfterSpraying;
		public ushort nPauseTimeAfterCleaning;

	    #region Implementation of ICloneable

	    /// <summary>
	    /// Creates a new object that is a copy of the current instance.
	    /// </summary>
	    /// <returns>
	    /// A new object that is a copy of this instance.
	    /// </returns>
	    /// <filterpriority>2</filterpriority>
	    public object Clone()
	    {
	        SCleanerSetting ret = new SCleanerSetting();
            ret.nCleanerPassInterval = this.nCleanerPassInterval;
            ret.nSprayPassInterval = this.nSprayPassInterval;
	        ret.nCleanerTimes = this.nCleanerTimes;
	        ret.nSprayFireInterval = this.nSprayFireInterval;
	        ret.nSprayTimes = this.nSprayTimes;
	        ret.nCleanIntensity = this.nCleanIntensity;
	        ret.bSprayWhileIdle = this.bSprayWhileIdle;
	        ret.bSprayBeforePrint = this.bSprayBeforePrint;
            ret.nPauseTimeAfterSpraying = nPauseTimeAfterSpraying;
            ret.nPauseTimeAfterCleaning = nPauseTimeAfterCleaning;
	        return ret;
	    }       

	    #endregion
	}	
	
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SRealTimeCurrentInfo
	{
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_VOL_TEMP_NUM)]
		public float [] cTemperatureCur2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_VOL_TEMP_NUM)]
		public float [] cTemperatureSet;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_VOL_TEMP_NUM)]
		public float [] cTemperatureCur;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_VOL_TEMP_NUM)]
		public float [] cPulseWidth;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_VOL_TEMP_NUM)]
		public float [] cVoltage;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_VOL_TEMP_NUM)]
		public float [] cVoltageBase;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_VOL_TEMP_NUM)]
		public float [] cVoltageCurrent;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_HEAD_NUM)]
        public float[] cXaarVoltageInk;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_HEAD_NUM)]
        public float[] cXaarVoltageOffset;
		[MarshalAs(UnmanagedType.I1)]
		public bool	bAutoVoltage;

        public SRealTimeCurrentInfo(bool bInit)
        {
            cTemperatureCur2 = new float[CoreConst.MAX_VOL_TEMP_NUM];
            cTemperatureSet = new float[CoreConst.MAX_VOL_TEMP_NUM];
            cTemperatureCur = new float[CoreConst.MAX_VOL_TEMP_NUM];
            cPulseWidth = new float[CoreConst.MAX_VOL_TEMP_NUM];
            cVoltage = new float[CoreConst.MAX_VOL_TEMP_NUM];
            cVoltageBase = new float[CoreConst.MAX_VOL_TEMP_NUM];
            cVoltageCurrent = new float[CoreConst.MAX_VOL_TEMP_NUM];
            cXaarVoltageInk = new float[CoreConst.MAX_HEAD_NUM];
            cXaarVoltageOffset = new float[CoreConst.MAX_HEAD_NUM];
            bAutoVoltage = false;
        }
	};
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SRealTimeCurrentInfo_382
	{
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_HEAD_NUM)]
		public float [] cTemperature;  //RO
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_HEAD_NUM)]
		public float [] cPWM;  //0 - 255   RO
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_HEAD_NUM)]
		public float [] cVtrim; //-128 - 127  RW
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_HEAD_NUM)]
		public float [] cTargetTemp;  //RO
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_HEAD_NUM)]
		public int [] cTempControlMode;  //RO
	};
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SHeadInfoType_382
	{
		public uint   SerNo; //3 values
		public ushort FirmVer;
		public ushort Type;
		public ushort WfmID1;
		public ushort WfmID2;
		public ushort WfmID3;
		public ushort WfmID4;
		public ushort DualBank;
	};
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct SHeadInfoType_501
    {
        public byte head;		//waveform 基本信息
        public byte color;		//waveform 基本信息
        public byte saveID;		//waveform 基本信息
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] waveformID;		//waveform 基本信息
    };
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
    public struct CRCFileHead : ICloneable
	{
		public int Flag;
		public int Len;
		public int Crc;
		public int reserve;

	    #region Implementation of ICloneable

	    /// <summary>
	    /// Creates a new object that is a copy of the current instance.
	    /// </summary>
	    /// <returns>
	    /// A new object that is a copy of this instance.
	    /// </returns>
	    /// <filterpriority>2</filterpriority>
	    public object Clone()
	    {
	       CRCFileHead ret = new CRCFileHead();
	        ret.Flag = this.Flag;
	        ret.Len = this.Len;
	        ret.Crc = this.Crc;
	        ret.reserve = this.reserve;
	        return ret;
	    }

	    #endregion
	};


    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct SExtensionSetting : ICloneable
    {
        public float fPaper2Left;
        public float fPaper2Width;
        public float fPaper3Left;
        public float fPaper3Width;
        public DOUBLE_YAXIS sDouble_Yaxis;
        /// <summary>
        /// byte,平台距离Y
        /// </summary>
        public float FlatSpaceY;
        public byte Tpower;
        public byte TSpeed;
        public byte LineWidth;
        [MarshalAs(UnmanagedType.I1)]
        public bool bYBackOrigin;//打印完成后是否回Y原点

        public ushort ManualSprayFrequency; //手动闪喷频率
        public ushort ManualSprayTime;//手动闪喷时间
        /// <summary>
        /// 从加密狗读出的板卡id,用于fw和板卡id做校验[工正专用]
        /// </summary>
        public uint BoardId;
        public float UVLightInAdvance;//UV灯偏移提前距离
        public float fRunDistanceAfterPrint;//unit =inch  // scorpion
        public int RunSpeed; //   // scorpion
        public uint iLeftRightMask;//左右UV灯,左打和右打有效设置,普通模式  // scorpion
        public uint iLeftRightMaskReverse;//左右UV灯,左打和右打有效设置,反向模式  // scorpion
        public byte eUvLightType;    // scorpion
        [MarshalAs(UnmanagedType.I1)]
        public bool AutoRunAfterPrint;  // scorpion
        [MarshalAs(UnmanagedType.I1)]
        public bool IsNormalMode;  // scorpion
        [MarshalAs(UnmanagedType.I1)]
        public bool IsFullMode;  // scorpion
        public byte fullL;  // scorpion
        public byte fullR;  // scorpion
        public byte halfL;  // scorpion
        public byte halfR;  // scorpion
        public float fXRightHeadToCurosr; // hapond
        public float fYRightHeadToCurosr;// hapond

        [MarshalAs(UnmanagedType.I1)]
        public bool bEnableAnotherUvLight;  // 使能第二排uv等.与小车平行额外增加的X1上					//708

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 69)]
        public byte[] reserve2;
        public byte idleFlashUseStrongParams;  // 空闲闪喷使用强喷参数
        public byte flashInWetStatus;  // 保湿时启用闪喷功能

        [MarshalAs(UnmanagedType.I1)]
        public bool bAutoPausePerPage;  // 层间暂停

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public short[] MultiLayer;//

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] ContrastColor;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public sbyte[] OriginCaliValue;

        public byte Phase;											//671
        public byte PhaseShift;									//670
        public byte CalType;								//669
        [MarshalAs(UnmanagedType.I1)]
        public bool bExquisiteFeather;  // 是否为精细校准
        /// <summary>
        /// 世导喷淋AB俩个区域间距
        /// </summary>
        public float ABOffsetX;
        [MarshalAs(UnmanagedType.I1)]
        public bool BackBeforePrint;  // scorpion

        [MarshalAs(UnmanagedType.I1)]
        public bool bJointFeather;//是否启用喷头间羽化
        [MarshalAs(UnmanagedType.I1)]
        public bool bConstantStep;  // 是否启用等步进功能
        public byte multipleVanishInk;//亮油多倍墨量
        public float zMaxLength; //z轴最大行程,
        public float MeasureWidthSensorPos;//测宽传感器安装位置;(距离第一个喷头的位置)

        public byte ClipSpliceUnitIsPixel;	// 剪切、拼接功能的数据接口单位是否为像素; 0 英寸，1 像素; 20180614		
        public byte bOverPrint;		// 是否使用堆叠份数; 20180720	
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] nOverPrint;	// 白、彩、亮油对应的堆叠份数; 20180720

        public byte OneStepSkipWhite; //一步跳白
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] pad2;

        public byte nCalibrationNum; //校准打印次数
        public byte bCalibrationNoStep; //喷检，校准不步进,1是不步进
        public byte nCurJobIndex; //当前作业的位置索引
        public byte bDoublePrint; //是否使用双面喷
        public byte nDoublePrintBandNum; //双面喷生效band数
        public byte bIsNewCalibration; //1表示新校准

        public byte reserve12;
        public byte bGreyRip;

        public float FeatherNozzle;//羽化喷孔数

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_COLOR_NUM)]
        public byte[] ColorGreyMask;
        //后面留个界面使用
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] WorkPosList;
        public byte WorkPosEnable;
        public byte GenDaoLayout;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] res2;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 596)]
        public byte[] reserve;
        #region Implementation of ICloneable

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public object Clone()
        {
            SExtensionSetting ret = new SExtensionSetting();
            ret.fPaper2Left = this.fPaper2Left;
            ret.fPaper2Width = this.fPaper2Width;
            ret.fPaper3Left = this.fPaper3Left;
            ret.fPaper3Width = this.fPaper3Width;
            ret.eUvLightType = this.eUvLightType;
            ret.fRunDistanceAfterPrint = this.fRunDistanceAfterPrint;
            ret.RunSpeed = this.RunSpeed;
            ret.AutoRunAfterPrint = this.AutoRunAfterPrint;
            ret.bEnableAnotherUvLight = this.bEnableAnotherUvLight;

            ret.iLeftRightMask = this.iLeftRightMask;
            ret.iLeftRightMaskReverse = this.iLeftRightMaskReverse;
            ret.IsNormalMode = this.IsNormalMode;
            ret.IsFullMode = this.IsFullMode;
            ret.fullL = this.fullL;
            ret.fullR = this.fullR;
            ret.halfL = this.halfL;
            ret.halfR = this.halfR;
            ret.Tpower = this.Tpower;
            ret.TSpeed = this.TSpeed;
            ret.reserve = (byte[])this.reserve.Clone();
            //ret.reserve12 = (byte[])this.reserve12.Clone();

            ret.fXRightHeadToCurosr = fXRightHeadToCurosr; // hapond
            ret.fYRightHeadToCurosr = fYRightHeadToCurosr; // hapond
            ret.MultiLayer = (short[])MultiLayer.Clone(); //

            ret.ContrastColor = (int[])ContrastColor.Clone();

            ret.OriginCaliValue = (sbyte[])OriginCaliValue.Clone();

            ret.bAutoPausePerPage = bAutoPausePerPage; // 层间暂停
            ret.Phase = Phase;
            ret.PhaseShift = PhaseShift;
            ret.CalType = CalType;
            ret.flashInWetStatus = this.flashInWetStatus;
            ret.idleFlashUseStrongParams = this.idleFlashUseStrongParams;

            ret.ColorGreyMask = (byte[])ColorGreyMask.Clone();
            return ret;
        }

        #endregion
    }


    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct SPrinterSetting : ICloneable
    {
        public CRCFileHead sCrcCali;
        public SCalibrationSetting sCalibrationSetting;
        public CRCFileHead sCrcOther;
        public SCleanerSetting sCleanSetting; ///??????Whether this parameter transfer to Board not save in Panel
        public SMoveSetting sMoveSetting;

        public SFrequencySetting sFrequencySetting;
        public SBaseSetting sBaseSetting;
        public SExtensionSetting sExtensionSetting;//public SRealTimeCurrentInfo sRealTimeSetting;
        //public SRealTimeCurrentInfo	sRealTimeSetting;
        public int nHeadFeature2;
        /// <summary>
        /// 固定色序=1,大小步=2,默认=0
        /// </summary>
        public int nKillBiDirBanding;
        public SUVSetting UVSetting;
        public SZSetting ZSetting;
        public LayoutSetting layoutSetting;
        public CRCFileHead sCrcTail;

        #region Implementation of ICloneable

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public object Clone()
        {
            SPrinterSetting ret = new SPrinterSetting();
            ret.sCrcCali = (CRCFileHead)this.sCrcCali.Clone();
            ret.sCalibrationSetting = (SCalibrationSetting)this.sCalibrationSetting.Clone();
            ret.sCleanSetting = (SCleanerSetting)this.sCleanSetting.Clone();
            ret.sMoveSetting = (SMoveSetting)this.sMoveSetting.Clone();
            ret.sFrequencySetting = (SFrequencySetting)this.sFrequencySetting.Clone();
            ret.sBaseSetting = (SBaseSetting)this.sBaseSetting.Clone();
            ret.sExtensionSetting = (SExtensionSetting)this.sExtensionSetting.Clone();
            ret.nHeadFeature2 = this.nHeadFeature2;
            ret.nKillBiDirBanding = this.nKillBiDirBanding;
            ret.UVSetting = (SUVSetting)this.UVSetting.Clone();
            ret.ZSetting = (SZSetting)this.ZSetting.Clone();
            ret.sCrcTail = (CRCFileHead)this.sCrcTail.Clone();
            return ret;
        }

        #endregion
    }

	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SUVSetting:ICloneable
	{
		public float  fLeftDisFromNozzel;	//左灯距零喷头距离
		public float  fRightDisFromNozzel;	//右灯距零喷头距离
		public float  fShutterOpenDistance;	//快门提前打开距离
		public uint iLeftRightMask;//左右UV灯,左打和右打有效设置
        public byte eUvLightType;
        public byte rev1;
        public byte rev2;
        public byte rev3;
		public int reserve1;
		public int reserve2;
	    #region Implementation of ICloneable

	    /// <summary>
	    /// Creates a new object that is a copy of the current instance.
	    /// </summary>
	    /// <returns>
	    /// A new object that is a copy of this instance.
	    /// </returns>
	    /// <filterpriority>2</filterpriority>
	    public object Clone()
	    {
	        SUVSetting ret = new SUVSetting();
	        ret.fLeftDisFromNozzel = this.fLeftDisFromNozzel;
	        ret.fRightDisFromNozzel = this.fRightDisFromNozzel;
	        ret.fShutterOpenDistance = this.fShutterOpenDistance;
	        ret.iLeftRightMask = this.iLeftRightMask;
            ret.eUvLightType = this.eUvLightType;
            ret.reserve1 = this.reserve1;
            ret.reserve2 = this.reserve2; 
            return ret;
	    }

	    #endregion
	}

    [Serializable]
    public struct UvPowerLevelMap
    {
        public uint Flag;//FW组需要的标识，如MAKE_4CHAR_CONST('L','M','A','P')
        [MarshalAs(UnmanagedType.I4)]
        public UvLightType LightType; //int 
        public byte LeftL1PowerOnReady; // 左灯就绪状态档位为1时的功率值  0--100
        public byte LeftL2PowerOnReady;// 左灯就绪状态档位为2时的功率值  0--100
        public byte LeftL3PowerOnReady;// 左灯就绪状态档位为3时的功率值  0--100
        public byte LeftL4PowerOnReady;// 左灯就绪状态档位为4时的功率值  0--100
        public byte RightL1PowerOnReady;// 右灯就绪状态档位为1时的功率值  0--100
        public byte RightL2PowerOnReady;// 右灯就绪状态档位为2时的功率值  0--100
        public byte RightL3PowerOnReady;// 右灯就绪状态档位为3时的功率值  0--100
        public byte RightL4PowerOnReady;// 右灯就绪状态档位为4时的功率值  0--100
        public byte LeftL1PowerOnPrint;// 左灯打印状态档位为1时的功率值  0--100
        public byte LeftL2PowerOnPrint;// 左灯打印状态档位为2时的功率值  0--100
        public byte LeftL3PowerOnPrint;// 左灯打印状态档位为3时的功率值  0--100
        public byte LeftL4PowerOnPrint;// 左灯打印状态档位为4时的功率值  0--100
        public byte RightL1PowerOnPrint;// 右灯打印状态档位为1时的功率值  0--100
        public byte RightL2PowerOnPrint;// 右灯打印状态档位为2时的功率值  0--100
        public byte RightL3PowerOnPrint;// 右灯打印状态档位为3时的功率值  0--100
        public byte RightL4PowerOnPrint;// 右灯打印状态档位为4时的功率值  0--100
    }

	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SZSetting:ICloneable
	{
		public float  fSensorPosZ;
		public float  fHeadToPaper;  //喷头距离打印介质高度
		public float  fMesureHeight; //测高模块离地板高度
		public float  fMesureXCoor;	//测高模块的X向位置
		public float  fMesureMaxLen;	//车头的最大行程 ZMax
		public short  fMeasureSpeedZ;	//车头的Z speed
		public short  reserve;
		public float  reserve1;
		public float  fMesureYCoor;//测高模块的Y向位置

	    #region Implementation of ICloneable

	    /// <summary>
	    /// Creates a new object that is a copy of the current instance.
	    /// </summary>
	    /// <returns>
	    /// A new object that is a copy of this instance.
	    /// </returns>
	    /// <filterpriority>2</filterpriority>
	    public object Clone()
	    {
	        SZSetting ret = new SZSetting();
	        ret.fSensorPosZ = this.fSensorPosZ;
	        ret.fHeadToPaper = this.fHeadToPaper;
	        ret.fMesureHeight = this.fMesureHeight;
	        ret.fMesureXCoor = this.fMesureXCoor;
	        ret.fMesureMaxLen = this.fMesureMaxLen;
	        ret.fMeasureSpeedZ = this.fMeasureSpeedZ;
	        ret.reserve = this.reserve;
	        ret.reserve1 = this.reserve1;
	        ret.fMesureYCoor = this.fMesureYCoor;
	        return ret;
	    }

	    #endregion
	}
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SPrinterSetting_V1
	{
		public CRCFileHead  sCrcCali;
		public SCalibrationSetting  sCalibrationSetting;
		public CRCFileHead  sCrcOther;
		public SCleanerSetting		sCleanSetting; ///??????Whether this parameter transfer to Board not save in Panel
		public SMoveSetting			sMoveSetting;

		public SFrequencySetting	sFrequencySetting;
		public SBaseSetting_V1		sBaseSetting;
		public SRealTimeCurrentInfo	 sRealTimeSetting;
		public int nHeadFeature2;
		public int nKillBiDirBanding;
		public CRCFileHead  sCrcTail;
	}

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct GzCardboardParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] 
        public char[] Flag;

        public int WaitMediaReadyTime; //the time wait media ready, the unit is ms

        /// <summary>
        /// 打印完成后墨栈自动上升延时时间.unit:ms;
        /// </summary>
        public int AutoCappingDelayTime; // unit:ms;

    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct GZClothMotionParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag;    //'C', 'L', 'O', 'T'
        public byte enable;   //勾上(1)，卷布电机工作，不勾(0)，不工作
        public byte mode;     //双面(1).单面(2)
        public ushort rev;     //不用
        public uint speed;   //HZ单位
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct NKTParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] 
        public char[] Flag; //!< 'NKT\0'，内部标记，不上送
        public uint BeltSpeed; //!< 皮带速度，即检测到砖以前皮带运动速度
        public uint FeedSpeed; //!< 进料速度，即检测到砖以后送砖到位时的皮带运动速度
        public uint StepSpeed; //!< 打印时候的步进速度
        public uint DetectorOffset; //!< 电眼到打印位置的距离，脉冲
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 44)]
        public byte[] reserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct TlhgParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; //!< 'T', 'L', 'H', 'G'
        public uint XCleanPos; //!< 清洗时X坐标
        public uint YCleanPos; //!< 清洗时Y坐标
        public uint ZCleanPos; //!< 清洗时Z坐标
        public uint zWorkPos;
        public uint addWetPos; //!< 加湿位置x轴
        public byte addWetInterval;//加湿间隔pass数
        public byte suckTimes;//吸风次数
        public uint suckStartPos; //吸风开始位置x
        public uint suckEndPos;// 吸风结束位置x
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
        public byte[] reserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct AllprintParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; // 'APRT'
        public byte bIsYCloseLoop;
        public byte rev1;
        public ushort LayerDelay;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)] 
        public byte[] rev;
    }
    /// <summary>
    /// 震荡参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct PhShake_tag
    {
        public ushort bOpenshake;					// 0-1 开启震荡
        public ushort shakeDual;					// 2-3 shake持续点火数.
        public ushort shakeNull;					// 4-5 shake 后多少点火后开始打印.
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct HumanParam
    {
        public uint Flag;        //!< 'H', 'U', 'M', 'A'      
        public uint PressInkOnTime;    //!< 压墨时间  毫秒
        public uint PressInkRecoverTime;  //!< 压墨恢复时间  毫秒
        public int ScarperLen;      //!< 刮片位置 脉冲
        public int CleanXpos;      //!< 清洗X位置 脉冲
    }

}
