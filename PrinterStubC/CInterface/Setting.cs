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
        public byte nStripInkPercent;//����ī�����ٷֱȣ���ǰ��֧�֣�25%,50%,75%,100%
        public byte rev1;
        /// <summary>
        /// 0:�������;1��е����
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
        Color2 = 3, //AWBģʽ��ָʾ������Դ��B
        //Contrast = 3,
	};

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct DOUBLE_YAXIS
    {
        public uint Flag; //flag
        public uint YResolution; //Y��ֱ���
        public float fMaxoffsetpos; //�������ƫ��λ�ã�Ӣ�絥λ
        public float fMaxTolerancepos; //��������ݲ�λ�ã�У��λ��ƫ��ʹ�ã�Ӣ�絥λ
        public float DoubeYRatio; //˫��ƫ�����ϵ��
        public float DrvEncRatio1; // 14 Y��������ͱ����������ı���1
        public float DrvEncRatio2; // 18 Y��������ͱ����������ı���2

        public byte bCorrectoffset; //�Ƿ�֧�ֽ���λ�ù���
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 19)]
        public byte[] rev; //����11���ֽ�
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
        /// // ��λs
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
        /// ƽ̨�½�����
        /// </summary>
        public uint nZDownDis;      //
        /// <summary>
        /// ѹīλ��
        /// </summary>
        public CleanStep purge;		// 
        /// <summary>
        /// ��īλ��
        /// </summary>
        public CleanStep wipe;			// 
        /// <summary>
        /// ��ϴƵ�ʣ���λjob
        /// </summary>
        public uint rate;			// 
        public SandPos_tag sand;         // ��ɳλ��.
        public CleanPos_tag clean;     // ��ϴλ�ü��ٶ�.
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
        /// bool�ͱ�����Ϊ�����ϵ������ļ�,�����в�Ӧʹ�ô�����
        /// </summary>
	    public bool bFeatherMax // �����ϵ�setting.xml�����ļ�
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
        /// ��ӡx�����[ע��:��ʷԭ��������뺬���෴]
        /// </summary>
		[MarshalAs(UnmanagedType.I1)]
		public bool		bIgnorePrintWhiteX;
        /// <summary>
        /// ��ӡy�����[ע��:��ʷԭ��������뺬���෴]
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
		public bool		bIgnorePrintWhiteY;
		public byte		multipleWriteInk;
        public float fYAddDistance;//uint		eRev23;//Not Use it ??????????????????????
		public int         nFeatherPercent;

		public SColorBarSetting sStripeSetting;
		public float fFeatherWavelength;
		public ushort nSpotColor1Mask; //BIT 15: BIT8 KCMYLMLCORGR :  BIT5:INTERSEC & ,BIT4:NOT ~, BIT3-BIT0: 0: ����ӡ 1�� ALL 2��RIP 3��Image 
		public ushort nSpotColor2Mask;
		public uint  nLayerColorArray;  //ƽ�ţ�BIT01= Layer1ColrIndex ,BIT23 = Layer2ColrIndex ;BIT45 = Layer3ColrIndex
											// ���ţ� BIT0= ��ɫ��ӡ: BIT1�� ��ɫ��ӡ: BIT2�� ���ʹ�ӡ 

		public float fAutoCleanPosMov;
		public float fAutoCleanPosLen;

        [MarshalAs(UnmanagedType.I1)]
        public bool bAutoCenterPrint; 

        [MarshalAs(UnmanagedType.I1)]
        public bool bUseFeather; // 20140708 gzw modify
        public ushort bitRegion; //����λ 1:VolumeConvert
        //public byte reserve6;
        public float fZAdjustmentDistance; // ��ӡ���Z���ƶ����룬��λӢ��

		[MarshalAs(UnmanagedType.I1)]
		public bool  bMirrorX;
		public byte  nWhiteInkLayer; // ��ʾ��ī�Ĳ����� 0��C  1�� C  2��WC  3��WCV  4��CWCV 5�� VCWCV  Other no defaultvalue�� MAX Is 8
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
		public byte	nXMoveSpeed;				// С���ٶȣ�ȡֵ��Χ1-8
		public byte	nYMoveSpeed;				// ����ֽ�ٶȣ�ȡֵ��Χ1-8
		public byte	nZMoveSpeed;				// Z���ٶȣ�ȡֵ��Χ1-8
		public byte	n4MoveSpeed;			// �����Ƶ ??????should only for Move or Print 

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
		public int	nSprayFireInterval;//���������Ժ���Ϊ��λ
	
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
        public byte head;		//waveform ������Ϣ
        public byte color;		//waveform ������Ϣ
        public byte saveID;		//waveform ������Ϣ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] waveformID;		//waveform ������Ϣ
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
        /// byte,ƽ̨����Y
        /// </summary>
        public float FlatSpaceY;
        public byte Tpower;
        public byte TSpeed;
        public byte LineWidth;
        [MarshalAs(UnmanagedType.I1)]
        public bool bYBackOrigin;//��ӡ��ɺ��Ƿ��Yԭ��

        public ushort ManualSprayFrequency; //�ֶ�����Ƶ��
        public ushort ManualSprayTime;//�ֶ�����ʱ��
        /// <summary>
        /// �Ӽ��ܹ������İ忨id,����fw�Ͱ忨id��У��[����ר��]
        /// </summary>
        public uint BoardId;
        public float UVLightInAdvance;//UV��ƫ����ǰ����
        public float fRunDistanceAfterPrint;//unit =inch  // scorpion
        public int RunSpeed; //   // scorpion
        public uint iLeftRightMask;//����UV��,�����Ҵ���Ч����,��ͨģʽ  // scorpion
        public uint iLeftRightMaskReverse;//����UV��,�����Ҵ���Ч����,����ģʽ  // scorpion
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
        public bool bEnableAnotherUvLight;  // ʹ�ܵڶ���uv��.��С��ƽ�ж������ӵ�X1��					//708

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 69)]
        public byte[] reserve2;
        public byte idleFlashUseStrongParams;  // ��������ʹ��ǿ�����
        public byte flashInWetStatus;  // ��ʪʱ�������繦��

        [MarshalAs(UnmanagedType.I1)]
        public bool bAutoPausePerPage;  // �����ͣ

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
        public bool bExquisiteFeather;  // �Ƿ�Ϊ��ϸУ׼
        /// <summary>
        /// ��������AB����������
        /// </summary>
        public float ABOffsetX;
        [MarshalAs(UnmanagedType.I1)]
        public bool BackBeforePrint;  // scorpion

        [MarshalAs(UnmanagedType.I1)]
        public bool bJointFeather;//�Ƿ�������ͷ����
        [MarshalAs(UnmanagedType.I1)]
        public bool bConstantStep;  // �Ƿ����õȲ�������
        public byte multipleVanishInk;//���Ͷ౶ī��
        public float zMaxLength; //z������г�,
        public float MeasureWidthSensorPos;//���������װλ��;(�����һ����ͷ��λ��)

        public byte ClipSpliceUnitIsPixel;	// ���С�ƴ�ӹ��ܵ����ݽӿڵ�λ�Ƿ�Ϊ����; 0 Ӣ�磬1 ����; 20180614		
        public byte bOverPrint;		// �Ƿ�ʹ�öѵ�����; 20180720	
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] nOverPrint;	// �ס��ʡ����Ͷ�Ӧ�Ķѵ�����; 20180720

        public byte OneStepSkipWhite; //һ������
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] pad2;

        public byte nCalibrationNum; //У׼��ӡ����
        public byte bCalibrationNoStep; //��죬У׼������,1�ǲ�����
        public byte nCurJobIndex; //��ǰ��ҵ��λ������
        public byte bDoublePrint; //�Ƿ�ʹ��˫����
        public byte nDoublePrintBandNum; //˫������Чband��
        public byte bIsNewCalibration; //1��ʾ��У׼

        public byte reserve12;
        public byte bGreyRip;

        public float FeatherNozzle;//�������

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = CoreConst.MAX_COLOR_NUM)]
        public byte[] ColorGreyMask;
        //������������ʹ��
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

            ret.bAutoPausePerPage = bAutoPausePerPage; // �����ͣ
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
        /// �̶�ɫ��=1,��С��=2,Ĭ��=0
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
		public float  fLeftDisFromNozzel;	//��ƾ�����ͷ����
		public float  fRightDisFromNozzel;	//�ҵƾ�����ͷ����
		public float  fShutterOpenDistance;	//������ǰ�򿪾���
		public uint iLeftRightMask;//����UV��,�����Ҵ���Ч����
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
        public uint Flag;//FW����Ҫ�ı�ʶ����MAKE_4CHAR_CONST('L','M','A','P')
        [MarshalAs(UnmanagedType.I4)]
        public UvLightType LightType; //int 
        public byte LeftL1PowerOnReady; // ��ƾ���״̬��λΪ1ʱ�Ĺ���ֵ  0--100
        public byte LeftL2PowerOnReady;// ��ƾ���״̬��λΪ2ʱ�Ĺ���ֵ  0--100
        public byte LeftL3PowerOnReady;// ��ƾ���״̬��λΪ3ʱ�Ĺ���ֵ  0--100
        public byte LeftL4PowerOnReady;// ��ƾ���״̬��λΪ4ʱ�Ĺ���ֵ  0--100
        public byte RightL1PowerOnReady;// �ҵƾ���״̬��λΪ1ʱ�Ĺ���ֵ  0--100
        public byte RightL2PowerOnReady;// �ҵƾ���״̬��λΪ2ʱ�Ĺ���ֵ  0--100
        public byte RightL3PowerOnReady;// �ҵƾ���״̬��λΪ3ʱ�Ĺ���ֵ  0--100
        public byte RightL4PowerOnReady;// �ҵƾ���״̬��λΪ4ʱ�Ĺ���ֵ  0--100
        public byte LeftL1PowerOnPrint;// ��ƴ�ӡ״̬��λΪ1ʱ�Ĺ���ֵ  0--100
        public byte LeftL2PowerOnPrint;// ��ƴ�ӡ״̬��λΪ2ʱ�Ĺ���ֵ  0--100
        public byte LeftL3PowerOnPrint;// ��ƴ�ӡ״̬��λΪ3ʱ�Ĺ���ֵ  0--100
        public byte LeftL4PowerOnPrint;// ��ƴ�ӡ״̬��λΪ4ʱ�Ĺ���ֵ  0--100
        public byte RightL1PowerOnPrint;// �ҵƴ�ӡ״̬��λΪ1ʱ�Ĺ���ֵ  0--100
        public byte RightL2PowerOnPrint;// �ҵƴ�ӡ״̬��λΪ2ʱ�Ĺ���ֵ  0--100
        public byte RightL3PowerOnPrint;// �ҵƴ�ӡ״̬��λΪ3ʱ�Ĺ���ֵ  0--100
        public byte RightL4PowerOnPrint;// �ҵƴ�ӡ״̬��λΪ4ʱ�Ĺ���ֵ  0--100
    }

	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SZSetting:ICloneable
	{
		public float  fSensorPosZ;
		public float  fHeadToPaper;  //��ͷ�����ӡ���ʸ߶�
		public float  fMesureHeight; //���ģ����ذ�߶�
		public float  fMesureXCoor;	//���ģ���X��λ��
		public float  fMesureMaxLen;	//��ͷ������г� ZMax
		public short  fMeasureSpeedZ;	//��ͷ��Z speed
		public short  reserve;
		public float  reserve1;
		public float  fMesureYCoor;//���ģ���Y��λ��

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
        /// ��ӡ��ɺ�īջ�Զ�������ʱʱ��.unit:ms;
        /// </summary>
        public int AutoCappingDelayTime; // unit:ms;

    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct GZClothMotionParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag;    //'C', 'L', 'O', 'T'
        public byte enable;   //����(1)�����������������(0)��������
        public byte mode;     //˫��(1).����(2)
        public ushort rev;     //����
        public uint speed;   //HZ��λ
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct NKTParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] 
        public char[] Flag; //!< 'NKT\0'���ڲ���ǣ�������
        public uint BeltSpeed; //!< Ƥ���ٶȣ�����⵽ש��ǰƤ���˶��ٶ�
        public uint FeedSpeed; //!< �����ٶȣ�����⵽ש�Ժ���ש��λʱ��Ƥ���˶��ٶ�
        public uint StepSpeed; //!< ��ӡʱ��Ĳ����ٶ�
        public uint DetectorOffset; //!< ���۵���ӡλ�õľ��룬����
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 44)]
        public byte[] reserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct TlhgParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; //!< 'T', 'L', 'H', 'G'
        public uint XCleanPos; //!< ��ϴʱX����
        public uint YCleanPos; //!< ��ϴʱY����
        public uint ZCleanPos; //!< ��ϴʱZ����
        public uint zWorkPos;
        public uint addWetPos; //!< ��ʪλ��x��
        public byte addWetInterval;//��ʪ���pass��
        public byte suckTimes;//�������
        public uint suckStartPos; //���翪ʼλ��x
        public uint suckEndPos;// �������λ��x
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
    /// �𵴲���
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct PhShake_tag
    {
        public ushort bOpenshake;					// 0-1 ������
        public ushort shakeDual;					// 2-3 shake���������.
        public ushort shakeNull;					// 4-5 shake ����ٵ���ʼ��ӡ.
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct HumanParam
    {
        public uint Flag;        //!< 'H', 'U', 'M', 'A'      
        public uint PressInkOnTime;    //!< ѹīʱ��  ����
        public uint PressInkRecoverTime;  //!< ѹī�ָ�ʱ��  ����
        public int ScarperLen;      //!< ��Ƭλ�� ����
        public int CleanXpos;      //!< ��ϴXλ�� ����
    }

}
