/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;

namespace BYHXPrinterManager
{
	public enum ColorEnum: byte
	{
		Cyan			=	(byte)'C',
		Magenta			=	(byte)'M',
		Yellow			=	(byte)'Y',
		Black			=	(byte)'K',
		LightCyan		=	(byte)'c',
		LightMagenta	=	(byte)'m',
		Orange			=	(byte)'O',
		Green			=	(byte)'G',
		White			=	(byte)'W',
		NULL			=	(byte)'w',
	} 
	public enum ColorEnum_Short: byte
	{
		C			=	(byte)'C',
		M			=	(byte)'M',
		Y			=	(byte)'Y',
		K			=	(byte)'K',
		Lc			=	(byte)'c',
		Lm			=	(byte)'m',
		Or			=	(byte)'O',
		Gr			=	(byte)'G',
		W			=	(byte)'W',
		N			=	(byte)'w',
	} 
	//BIT0 : print white Ink (0:NOT PRINT; 1 PRINT)
	//BIT1:  underbase or overcoat(0:UnderBase; 1 OVERCOAT)
	//BIT2:  spotcolor or wholeImage(0:WholeImage; 1 SpotColor) 
	public enum WhiteInkPrintMode:uint
	{
		NotPrintWhiteInk = 0x00,   //000
		UnderBaseWholeImage = 0x01,//001
		OverCoatWholeImage = 0x3,  //011
		UnderBaseSpotColor = 0x5 , //101
		OverCoatSpotColor = 0x7,   //111
	}; 
	public enum BoardEnum :byte
	{
		CoreBoard		= 0x20,
		MotionBoard		= 0x21,
		HeadBoard		= 0x22,
		//PeriphBoard		= 0x21,

		Unknown			= 0xff
	};

	public enum FWUpdatingErrorEnum :byte
	{
	
	};

	public enum CleanCmdEnum :byte
	{
		Exsuction = 0,
		Spray = 1,
		Wipe = 2
	};

	public enum CalibrationCmdEnum :byte
	{
		CheckNozzleCmd			=	0x01,

		Mechanical_CheckAngleCmd,
		Mechanical_CheckVerticalCmd,

		LeftCmd,
		RightCmd,
		BiDirectionCmd,
		CheckColorAlignCmd,

		StepCmd,
		
		VerCmd,
		CheckVerCmd,

		NozzleReplaceCmd,
		SamplePointCmd,
		NozzleAllCmd,
		Mechnical_CheckOverlapVerticalCmd,
		EngStepCmd,
		Mechanical_CrossHeadCmd,
		Step_CheckCmd,
	};
#if LIYUUSB
	public enum JetCmdEnum 
	{
		StartSpray = 0x8D,
		StopSpray = 0x98,
		GotoCleanPos  = 0x10,			
		LeaveCleanPos = 0x11,
		FireSprayHead = 0xB4,			
		AutoSuckHead = 0x13,				
		EnterSingleCleanMode = 0x8e,
		ExitSingleCleanMode = 0x8f,
		CloseCleaner = 0x16,
		SingleClean = 0x17,				//HeadIndex
		Purge = 0x8E,
		StopPurge = 0x8F,				//HeadIndex

		StartMove = 0x20,		//Dir										;Only UI use			
		StopMove  = 0x87,				
		BackToHomePoint = 0xE9,		
		MeasurePaper = 0xc5,			//??????????????????? command will change setting
		SetOrigin = 0x22,				//??????????????????? command will change setting


		Pause = 0x81,
		Resume= 0x97,
		Abort = 0x82,
		StartPrint = 0x33,
		EndPrint = 0x34,


		ResetBoard = 0x80,
		LoadSetting= 0x41,				//??????????????????? command will change setting; Only UI Use
		SaveSetting = 0x42,				//??????????????????? command will change setting; Only UI use
		FixMove = 0x43,					//Dir,nDistance								; Only UI use

		StartProxy = 0x50,
		EndProxy = 0x51,
		ResetProxyCounter = 0x52,
		GetReport = 0x53,

		StartUpdater = 0x60,		//Port Index, This command will specify ep2 to updater or other or print use   
		EndUpdater = 0x61,				//Port Index will reset to print
		FlushBand = 0x62,				//(int bandIndex);
		ClearUpdatingStatus,
		SetLinerEncoderFlag = 0x64,
		SetMotionTimeOut  = 0x65,
		ClearFWFactoryData  = 0x66,
	};
#else
	public enum JetCmdEnum 
	{
		StartSpray = 0x10,
		StopSpray,
		GotoCleanPos,			
		LeaveCleanPos,
		FireSprayHead,			
		AutoSuckHead,				
		EnterSingleCleanMode,
		ExitSingleCleanMode,
		CloseCleaner,
		SingleClean,				//HeadIndex

		StartMove = 0x20,		//Dir										;Only UI use			
		StopMove,				
		BackToHomePoint,		
		MeasurePaper,			//??????????????????? command will change setting
		SetOrigin,				//??????????????????? command will change setting
		BackToHomePointY,		


		Pause = 0x30,
		Resume,
		Abort,
		StartPrint,
		EndPrint,


		ResetBoard = 0x40,
		LoadSetting,				//??????????????????? command will change setting; Only UI Use
		SaveSetting,				//??????????????????? command will change setting; Only UI use
		FixMove,					//Dir,nDistance								; Only UI use




		StartProxy = 0x50,
		EndProxy,
		ResetProxyCounter,
		GetReport,

		StartUpdater = 0x60,			//Port Index, This command will specify ep2 to updater or other or print use   
		EndUpdater,				//Port Index will reset to print
		FlushBand,				//(int bandIndex);
		ClearUpdatingStatus,
		SetLinerEncoderFlag,
		SetMotionTimeOut  = 0x65, 
		ClearFWFactoryData  = 0x66,

	};
#endif
	public enum SpeedEnum :uint
	{
		HighSpeed = 0,
		MiddleSpeed,
		LowSpeed,
		CustomSpeed
	}
	public enum AxisDir:byte
	{
		X = 0x1,
		Y = 0x2,
		Z = 0x4,
		All = 0x7
	};
	public enum JetStatusEnum : byte
	{
		PowerOff	= 0x00,		//Power Off or USB Cable not link
		Ready		= 0x01,			//Ready for printing
		Error		= 0x02,
		Busy		= 0x03,			//Pringing or Printer opend by other
		Pause		= 0x04,

		Aborting	= 0x05,		//Aborting printing
		Moving 		= 0x07,		//Moving
		Cleaning	= 0x08,		//Cleaning
		Measuring	= 0x09,	//Debug Color Align
		Spraying	= 0x0a,		//
		Initializing	= 0x0b,
		Updating        = 0x0c,
		Offline         =0xd,
		
		Unknown   = 0x0f
	}
        
	public enum InkStrPosEnum : uint
	{
		Both = 0,
		Left,
		Right,
		None,
}

	public enum PrinterHeadEnum:uint
	{
		Xaar_126 = 1,
		Xaar_128_360  = 2 ,
		Xaar_128_PLUS  = 3,
		Xaar_500  = 4,

		Spectra_S_128  =  5,
		Spectra_NOVA_256  =  6,
		Spectra_GALAXY_256  =  7,

		Konica_KM_512_M	= 8,
		Konica_KM_256_M	= 9,
		Konica_KM_128_M	= 10,
		Konica_KM_512_L	= 11,
		Konica_KM_256_L	= 12,
		Konica_KM_128_L	= 13,

		UNKOWN
	}

	public enum MoveDirectionEnum :uint
	{
		Left		=	0x1 ,
		Right,
		Up,
		Down,
		Up_Z,
		Down_Z

	}

	public enum SingleCleanEnum:uint
	{
		None		=	0x00,
		SingleColor,
		SingleHead,
		PureManual,
	}

	public enum CoreMsgEnum : uint
	{
		Percentage				=	0x10,		//LPARAM: 0 - 100 percent.
		Job_Begin,								// LPARAM://		0:		Parsering start	//		1:		Printting Start
		Job_End,								// LPARAM://		0:		Parsering end	//		1:		Printting end

		Status_Change,							//LPARAM://		HIGH	new status.
		ErrorCode,								//LPRARM: error code

		Parameter_Change,
		Power_On,
		Temperature_Change,
		Spectra_Change,
		Spectra_Ready,

		UpdaterStatus_Change	=	0x20,
		UpdaterErrorCode,
		UpdaterPercentage,
		AbortPrintCmd	=	0x30,
	}
	public enum HEAD_CATEGORY
	{
		XAAR_128		=1, //Support 2 & 3
		KONICA_KM256	=2, //Support 9 & 12
		SPECTRA			=3, //Support 5, 6, 7
		KONICA_KM512	=4, //Support 8, 11
	}
	public enum HEAD_BOARD_TYPE
	{
		XAAR128_12HEAD		=1,  //Konica KM256 12 head board.
		KM256_12HEAD 		=2,  //Konica KM256 12 head board.
		SPECTRA				=3,
		KM512_8HEAD_8VOL	=4,
		KM512_8HEAD_16VOL	=5,
		KM256_8HEAD 		=6,  //Konica KM256 8 head board.
	}
	public enum VenderID
	{
		LIYU = 1,
		INWEAR = 2,
		ALLWIN = 3,
		KINCOLOR = 4,
		HUMAN = 5,
		INWEAR_FLAT = 0x82,
		ALLWIN_FLAT = 0x83,
		KINCOLOR_FLAT = 0x84,
		HUMAN_FLAT = 0x85,
		MAX_VENDERID
	}
}
