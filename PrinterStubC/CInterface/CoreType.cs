/* 
	��Ȩ���� 2006��2007��������Դ��о�Ƽ����޹�˾����������Ȩ����
	ֻ�б�������Դ��о�Ƽ����޹�˾��Ȩ�ĵ�λ���ܸ��ĳ�д�ʹ�����
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
		Cyan			    =	(byte)'C', //0x43
		Magenta			=	(byte)'M', //0x4D
		Yellow			=	(byte)'Y', //0x59
		Black			=	(byte)'K', //0x4B
		
		LightCyan		=	(byte)'c',//0x63
		LightMagenta	=(byte)'m',//0x6D
		LightYellow		=	(byte)'y',//0x79
		LightBlack		=	(byte)'k',//0x6B
		
		Red				=	(byte)'R',//0x52
		Blue				=	(byte)'B',//0x47
		Green			=	(byte)'G',//0x42
		Orange			=	(byte)'O',//0x4F


		White			=	(byte)'W',//0x57
		Vanish			=	(byte)'V',//0x56

        SkyBlue = (byte)'S',//0x53
        Gray = (byte)'a',//0x61
        Pink = (byte)'P',//0x50
		NULL			=	(byte)' ',
	} 

	public enum ColorEnum_Short: byte
	{
		C			=	(byte)'C',
		M			=	(byte)'M',
		Y			=	(byte)'Y',
		K			=	(byte)'K',

		Lc			=	(byte)'c',
		Lm			=	(byte)'m',
		Ly			=	(byte)'y',//0x79
		Lk			=	(byte)'k',//0x6B

		R				=	(byte)'R',//0x52
		B				=	(byte)'B',//0x47
		G			=	(byte)'G',//0x42
		Or			=	(byte)'O',
		W			=	(byte)'W',
		V			=	(byte)'V',//0x56
        S = (byte)'S',//0x53
        Gr = (byte)'a',//0x61
        P = (byte)'P',//0x50

		N			=	(byte)' ',
	} 

	public enum DefaultColorOrderEnum: byte
	{
		C			=	0,
		M			,
		Y			,
		K			,
		Lc			,
		Lm			,
		Or			,
		Gr			,
		W			,
		N			,
	}
 
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
        /// <summary>
        /// ������(������)
        /// </summary>
		CheckNozzleCmd			=	0x01,
        /// <summary>
        /// (��е)�Ƕȼ��
        /// </summary>
		Mechanical_CheckAngleCmd,
        /// <summary>
        /// (��е)��ֱ���
        /// </summary>
		Mechanical_CheckVerticalCmd,
        /// <summary>
        /// ��У׼
        /// </summary>
		LeftCmd,
        /// <summary>
        /// ��У׼
        /// </summary>
		RightCmd,
        /// <summary>
        /// ˫��У׼
        /// </summary>
		BiDirectionCmd,
		CheckColorAlignCmd,
        /// <summary>
        /// ����У׼(��ȷУ׼)
        /// </summary>
		StepCmd,
		/// <summary>
        /// ��ֱУ׼
		/// </summary>
		VerCmd,
		CheckVerCmd,

        NozzleReplaceCmd = 11,
		SamplePointCmd,
        /// <summary>
        /// ������(У׼��)
        /// </summary>
		NozzleAllCmd,
		Mechnical_CheckOverlapVerticalCmd,
        /// <summary>
        /// ����У׼
        /// </summary>
		EngStepCmd,
        /// <summary>
        /// (��е)��׼��
        /// </summary>
		Mechanical_CrossHeadCmd,
		Step_CheckCmd,
        /// <summary>
        /// (��е)ȫ���
        /// </summary>
        Mechanical_AllCmd,
		CheckAngle2Cmd,

        LeftCmd_LR = 20,
        RightCmd_LR = 21,

		CheckOverLapCmd,
        XOriginCmd,
        PageStep = 24,
        PageCrossHead = 25,
        PageBidirection = 26,
        ZhuozhanCameraOrigin = 28,//������ͷ��32λ����
        OverallCheck = 29,
        /// <summary>
        /// ���У׼
        /// </summary>
        GroupCmdLeft = 30, //��ˮƽУ׼
        GroupCmdRight = 31,
	    NewCrossHeadCmd = 32,    //һͷ˫ɫ�Գ���������У׼
        DropletDetector = 100
    }
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
		ClearErrorCode  = 0x67,

        BackToServiceStation = 0x68,
        StartVPrint = 0x69,
        ClearNewErrorCode=0x70
	}

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
        Axis_4 = 0x08,
        Axis_5 = 0x10,
		Axis_6 = 0x20,

		All = 0x7
	};

    /// <summary>
    /// �ϵ�����
    /// </summary>
    public enum PrintMode
    {
        Normal,
        BreakPoint_NextBand,
        BreakPoint_CurBand,
        BreakPoint_StepAndNextBand,
    };

    /// <summary>
    /// ˮƽУ׼ģʽ,����ȫУ׼����ɫУ׼������У׼
    /// </summary>
    public enum HorizontalCalibrationMode
    {
        Quick,
        Color,
        Full,
        GroupQuick, //���ڿ���
        GroupColor, //������ɫ
        GroupFull, //����ȫ
    }

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
        Offline = 0xd,
	    Maintain = 0xe,
		
		Unknown   = 0x0f
	}
        
	public enum InkStrPosEnum : uint
	{
		Both = 0,
		Left,
		Right,
		None,
}

    public enum PrinterHeadEnum : uint
    {
        Xaar_126 = 1,
        Xaar_XJ128_40W = 2,
        Xaar_XJ128_80W = 3,
        Xaar_500 = 4,

        Spectra_S_128 = 5,
        Spectra_NOVA_256 = 6,
        Spectra_GALAXY_256 = 7,

        Konica_KM512M_14pl = 8,
        Konica_KM256M_14pl = 9,
        Konica_KM128M_14pl = 10,
        Konica_KM512L_42pl = 11,
        Konica_KM256L_42pl = 12,
        Konica_KM128L_42pl = 13,


        Xaar_Electron_35W = 14,
        Xaar_Electron_70W = 15,
        Xaar_Proton382_35pl = 16,
        Xaar_1001_GS6 = 17,


        Spectra_Polaris_15pl = 19,
        Konica_KM512LNX_35pl = 20, //18,
        Spectra_Polaris_35pl = 21,

        EGen5 = 22,

        Konica_KM1024M_14pl = 23,
        Konica_KM1024L_42pl = 24,
        Spectra_Polaris_80pl = 25,
        Xaar_Proton382_60pl = 26,

        Konica_KM512MAX_14pl = 27,
        Konica_KM512LAX_30pl = 28,

        Spectra_Emerald_10pl = 29,
        Spectra_Emerald_30pl = 30,

        Konica_KM512i_MHB_12pl = 31,

        ///tony for 
        Konica_KM512i_LHB_30pl = 32,

        ///tony for KM512i-MHB


        Spectra_PolarisColor4_15pl = 33,
        Spectra_PolarisColor4_35pl = 34,
        Spectra_PolarisColor4_80pl = 35,

        Xaar_Proton382_15pl = 36,
        Konica_KM512i_MAB_C_15pl = 37,
        Konica_KM1024i_MHE_13pl = 38,
        Konica_KM1024S_6pl = 39,

        RICOH_GEN4P_7pl = 40,
        RICOH_GEN4_7pl = 41,
        RICOH_GEN4L_15pl = 42,


        Kyocera_KJ4A_TA06_6pl = 43,
        Kyocera_KJ4B_0300_5pl_1h2c = 44,
        Kyocera_KJ4B_1200_1p5pl = 45,

        Spectra_SG1024MA_25pl = 46,
        Konica_KM1024i_MAE_13pl = 47,
        Konica_KM1024i_LHE_30pl = 48,

        Xaar_501_6pl = 49,
        Xaar_501_12pl = 50,
        Konica_KM1024i_SHE_6pl = 51,

        Konica_KM1800i_3P5pl = 52,
        Spectra_SG1024SA_12pl = 53,
        Konica_KM512i_LNB_30pl = 54,
        Konica_KM1024i_SAE_6pl = 55,

        Konica_KM512_SH_4pl = 56,
        XAAR_1002GS_40pl = 57,
        Konica_KM512i_SH_6pl = 58,

        Kyocera_KJ4B_QA06_5pl= 59, //600DPI  30K   ˮ��   5, 7, 12 and 18pl
        Kyocera_KJ4B_YH06_5pl = 60, //600DPI  40K   ˮ��    5, 7,12, and 18pl(24K)
        Kyocera_KJ4A_AA06_3pl = 61, //600DPI  20K   UV     3, 6, 13pl
        Kyocera_KJ4A_RH06 = 62, //600DPI  30K   UV 
        Kyocera_KJ4A_0300_5pl_1h2c = 63, //300DPI  30K   UV    5, 7, 12 and 18pl(20)	1ͷ2ɫ
        Kyocera_KJ4A_1200_1p5pl = 64, //1200DPI 64K   UV  1.5��3��5pl 

        Konica_KM512i_SAB_6pl = 65,
        Spectra_SG1024XSA_7pl = 66,
        Spectra_SG1024LA_80pl = 67,
        Konica_KM3688_6pl	=	68,
        Konica_M600SH_2C = 69, //!< M600SH ϵ��ͷ��֧�֣�֮�������һͷһɫ�����
        Fujifilm_GMA9905300_5pl = 70,//!< GMA9905300 ��ͷ,300dpi,5pL,99mm(1152 nozzle),no self heat
        Fujifilm_GMA3305300_5pl = 71, //!< GMA3305300 ��ͷ,300dpi,5pL,33mm(384 nozzle),no self heat    

        Konica_KM1024A_6_26pl = 72,
        Epson_S2840 = 73,//������3200
        Epson_5113 = 74,//4720��ͷ����ö��ֵҲ�����
        Ricoh_Gen6 = 75,
        Ricoh_Gen5 = 76,
        Epson_S2840_WaterInk = 77,
        EPSON_S1600_RC_UV = 78,
        EPSON_I3200 = 79,
        XAAR_1201_Y1 = 80,

        UNKOWN
    }

    public enum MoveDirectionEnum :uint
	{
		Left		=	0x1 ,
		Right,
		Up,
		Down,
		Up_Z,
		Down_Z,
		Up_4,
		Down_4,
        Up_5,
        Down_5,
	    Up_6,  // Ŀǰ��Ҫ��Ϊ�˹�������ʹ��
        Down_6,// Ŀǰ��Ҫ��Ϊ�˹�������ʹ��
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

		Parameter_Change = 0x15,
		Power_On,
		Temperature_Change,
		Spectra_Change,
		Spectra_Ready,
		Speed = 0x1a,
		PrintCount,
        PumpInk,
		BlockNotifyUI,
        ElecCount,
		HardPanelDirty = 0x1F,


		UpdaterStatus_Change	=	0x20,
		UpdaterErrorCode,
		UpdaterPercentage,
        CurBandY = 0x23,		// �����ӡ�ٷֱ�

		AbortPrintCmd	=	0x30,
		Motion_Position	=	0x31,
		PrinterReady = 0x32,
		HardPanelCmd = 0x33,

        PrintInfo = 0x36,
        EndFire = 0x38,
        EndMotion = 0x39,     //�����˶�����
        FinishedBand = 0x3A,     //��ӡ��� Bandindex
        FireNum = 0x3C,     //��ӡ��� �����Ŀ Reserve
        FireSize = 0x3D,     //��ӡ��� ���ݴ�Сû�� Reserve
        EndMotionCmd = 0x3E,     //�˶�����          //0x10C_B8 �����˶����
        Ep6Pipe = 0x40,// dll����ƴ���ϴ�

        JobEndParam = 0x4A, //��ӡ����ҵ��Jopprint���صĲ�������λ0bit��ʾ����ӡһpassС���ķ���


	    Send_Begin = 0x50,      //��ʼ������ҵ ��jobid 
	    Send_End = 0x51,          //��ҵ������� ��jobid
	    Print_Begin = 0x52,       //��ʼ��ӡ��ҵ ��jobid
	    Print_End = 0x53,          //��ҵ��ӡ��� ��jobid
	    Band_Begin = 0x54,       //��ʼһband��ӡ �� ��ǰband�� �� jobid
	    Band_End = 0x55           //����һband��ӡ �� ��ǰband�� �� jobid
	}

	public enum Ep6Cmd
	{
		Epson_MainUI_Param = 0x2,
		Epson_Media_Info = 0x5,
		Epson_Quality = 0x6,
		Epson_Calibration = 0x8,
		Epson_STEP = 0x9,
		EP6_CMD_T_MEDIA_HIGH = 0x10,
		EP6_CMD_T_WAVE_READ_READY = 0x11,
		EP6_CMD_T_WAVE_SET_READY = 0x12,
		EP6_CMD_T_SWING_READY = 0x13,
		EP6_CMD_T_WAVE_PRINT_READY = 0x14,
		EP6_CMD_T_MEDIA_INIT_OVER = 0x15,
		EP6_CMD_T_PRT_BEGIN = 0x16,
		EP6_CMD_T_PRT_UNLOAD_FINISH = 0x17,
		EP6_CMD_T_WAVE_NAME_READY = 0x18,
		EP6_CMD_T_WAVE_NAME_FINISH = 0x19,
		EP6_CMD_T_WAVE_POINT_FINISH = 0x1A,
		EP6_CMD_T_WAVE_CHANNEL_FINISH = 0x1B,
		EP6_CMD_T_PRT_START = 0x1C,
		EP6_CMD_T_Y_LIMIT = 0x1D,
		EP6_CMD_T_MoveZ_FINISH = 0x1F,

        EP6_CMD_T_TOUCH_XFAULT = 0x26,
        EP6_CMD_T_TOUCH_XFAULT_A = 0x2D,
        EP6_CMD_REPORT_SCRAPED_TIMES = 0x62,//�ϱ�ƽ�����ε��ѹδ���

		PATHFINDER = 0x101,	// ̽·��������̽��PM�ڲ��ڣ�Ŀǰû���õ�
		REPORT_STATUS = 0x102,	// ״̬��������״̬��������EP0��0x45
		HEADBOARD_PIPE = 0x103,	// �ܵ�����ִ�н��
		REPORT_TEMPERATURE = 0x104,	// �¶ȸ��±��棬ͷ���¶ȱ仯������ֵ
		REPORT_PRINTEDPAGE = 0x105,	// ��ӡ���ȱ���
		REPORT_SPEED 	=	0x106,	// ��ǰX�˶��ٶ�
		REPORT_SINGNAL	=	0x107,	// Scorpion��ӡ���װ��ź�״̬
        REPORT_SCORPION =0x109, //
        Panel_PIPE = 0x10d,	// �ܵ�����ִ�н��
        PumpInkBit = 0x10e,	// ��ī��ʾ 32λ
        LowInkBit = 0x10f,	// ī��״̬����,��Ӧλ��1��ʾȱī,0Ϊ����;
    //    ������ UINT32 ȱī״̬
    //#define LOW_INK_K				BIT(0)	//!< Kɫȱī
    //#define LOW_INK_C				BIT(1)	//!< Cɫȱī
    //#define LOW_INK_M				BIT(2)	//!< Mɫȱī
    //#define LOW_INK_Y				BIT(3)	//!< Yɫȱī
    //#define LOW_INK_G				BIT(4)	//!< Greenɫȱī
    //#define LOW_INK_W				BIT(5)	//!< Whiteɫȱī
    //#define LOW_INK_REV				BIT(6)	//!< ��������

        Fw_Msg = 0x110,//fwֱͨpm�ı���ͨ��//��ʪ��ϴ�����ѹر�
        NOZZLE_CER_FAIL = 0x112, //��ͷ��֤ʧ��
        Origin_Change = 0x113,
        Vender_Message = 0x114,
        IO_State=0x115//IO״̬
	}


	public enum ScorpionCmd: uint
	{
		CappingPlate = 0x01,//��U��:̧�����̡���D��:��������
		Cleanhead = 0x02,// ��P��:��ӡʱ��ϴ����C��:һ����ϴ
		EmergencyStop= 0x03,// ��ͣ������һ�л��Ҫֹͣ���������
		StartStopPrint = 0x04,// ��S��:��ʼ��ӡ����E��:������ӡ
		Heater = 0x05,// �����趨
		VacuumHalogen,//���ʲ����Ժ�PM�������vacuum��halogen�Ľ��㣬����������͸��װ�ARM
		SingleStatus, //�װ���Ŀ�������״̬
		FlushWiping,// ͷ��ϴ����
		MediaUpDown,// �շŲ�
		AllPurge,// All purge
        Wornning = 0x0b,// �澯
        PumpControl = 0x0c,// ��ī���� 
        StartReport = 0x0d,// �������� 
        ColorModeSwitch = 0x0e,// 4ɫ8ɫ�л�
        BottomBoardLog,// 15.	LOG ��Ϣ
        WiperControl,// 16.	Wiper����
        CleaningInkCartridgesLow, // 17.	��ϴī��ȱī
        DoSomethingError,  //ִ��ĳЩ�����������⣬��Ҫ���������ȴ�PM��Ӧ
        MainInkCartridgesLow, // 19.	��ī��ȱī
        SpilledInkWornning = 0x15, // ��ī����

        OpenCloseCleanBox = 0x19,//UV���ã�Ŀǰֻ������Ի������иÿ��Ƶĵ��ã��ر�Ҫע�⣬ֻ���ڷ���վλ�ÿɲ��� 
        OpenCloseDry = 0x1A,// ��S��:��ɿ�ʼ����E��:��ɽ�������ӡ������Y����������ָ�����룬���к�ɲ���
        DaOutControl = 0x1B,//1.29.	����UV��� // 1.28.	����DA��� ��G��/��S��8B��˳���Ӧ8·ͨ����ѹ����Χ��0-100����Ӧʵ�ʵ�ѹ��0-8V����PM��ʾֵ��Ҫ�ӵװ����
        ReportErrorChannel = 0x1C,//1.30.	����ܵ�
        CoolFrontRear = 0x1D,// 1.31.	1.31.	״̬��ѯ

        BottomBoardUpdate = 0x3d, //	�װ�����
    }

    public  enum ScorpionAlarmType
    {
        ALarm = 1,
        Emergency,
    }

    public enum ScorpionAlarm
    {
        //Ink pump alarm
        InkNotEnoughK = 1,
        InkNotEnoughC,
        InkNotEnoughM,
        InkNotEnoughY,
        InkNotEnoughLk,
        InkNotEnoughLc,
        InkNotEnoughLm,
        InkNotEnoughLy,
        InkNotEnoughW,
        // Cleaning wiper time error
        CheckWiperBackSensor = 0x10, //Alarm code : CA0010 �C Please, check Cleaning wiper cylinder and wiper back sensor.
        CheckWiperForwardSensor, //Alarm code : CA0011 �C Please, check Cleaning wiper cylinder and wiper forward sensor 
        // Auto Take up alarm
        CannotDirectionMedia = 51,// Alarm code : CA0051 �C Cannot direction of media. Please, check inverter or media sensor. 
        
        SpilledInkWornning = 0x70, // ����ֶ���ӵ�,���ǰ��ӱ�������
    }

    public enum ScorpionEmergency
    {
        LeftEmergency = 1, //Alarm code : EM0001 -  Left Emergency push button is pressed.
        RightEmergency, //Alarm code : EM0002 �C Right Emergency push button is Pressed.
        LeftHeadCarriage, //Alarm code : EM0003 �C Left Head carriage��s emergency switch was touched.
        RightHeadCarriage, //Alarm code : EM0004 �C Right Head carriage��s emergency switch was touched. 
        ZAxis, //Alarm code : EM0005 �C Z axis��s emergency sensor detected the limited position.
        XLeftAxis, //Alarm code : EM0006 �C X axis ��s Left limited sensor is detected by head carriage.
        XRightAxis, //Alarm code : EM0007 �C X axis��s Right limited sensor is detected by head carriage.
        AirNotEnough, //Alarm code : EM0008 �C pressed air in the machine is not enough.
        MediaLimited, //Alarm code : EM0009 �C Media limited sensor is detected by tension bar.
    }

    public enum HardPanelCmd : byte
    {
        PA_CANCEL = 0x1,
        PA_PAUSE,
        PA_RESUME,
        PRINT_NOZZLE_CHECK = 0x04,
        SET_ORIGINAL = 0x05,
        FROBIDPRINT = 0x06,     //��ֹ��ӡ
        CANCELFROBID = 0x07,    //��ֹ��ӡȡ��
        PRINT_START = 0x08,     //��ʼ��ӡ
        PM_DISABLE_UI = 0x09, // �ҵ�PM����
        PM_ENABLE_UI = 0x0A, // ����ظ�
        FIRESPRAY = 0x0B, // ͬ�������繦��
        SET_ORIGINAL_Y = 0x0C, // ͬ����Y��ԭ��
        PRINT_START_CS = 0x0D,      //����ƽ̨ѡ���ӡ
        MeasureHeight = 0x0F,       //���

        LEFTMOVE = 0x21,        //��������
        RIGHTMOVE = 0x22,       //��������
        PAPERIN = 0x23,         //��ֽ
        PAPEROUT = 0x24,        //��ֽ
        STOPMEVE = 0x25,      //ֹͣ����
        XGoHome=0x26,     //X��ԭ��
        YGoHome = 0x27,     //Y��ԭ��
        UpZ = 0x28,      //Z����
        DownZ = 0x29,      //Z�½�
        MoveXToPos=0x2A,//�ƶ�С����Ŀ��λ��
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
        XAAR128_12HEAD = 1, //Konica KM256 12 head board.
        KM256_12HEAD = 2, //Konica KM256 12 head board.
        SPECTRA = 3,
        KM512_8HEAD_8VOL = 4,
        KM512_8HEAD_16VOL = 5,
        KM256_8HEAD = 6, //Konica KM256 8 head board.

        KM512_6HEAD = 7, //Konica KM512 6 head board , just for cost
        KM512_16HEAD = 8,
        KM256_16HEAD = 9, //Future	
        SPECTRA_256_GZ = 10, //For GZ Spectra NOVA 256

        XAAR128_16HEAD = 11, //XAAR128 12 head board.
        XAAR382_8HEAD = 12, //XAAR128 12 head board.
        KM512_BANNER = 13, //4 KM512 
        NEW512_8HEAD = 14,
        SPECTRA_POLARIS_4 = 15, //For spectra polaris, 2/4/6 heads
        SPECTRA_POLARIS_6 = 16, //For spectra polaris, 2/4/6 heads
        KM512_16HEAD_V2 = 17, //KM512 16heads version 2 	
        SPECTRA_BYHX_V4 = 18, //BYHX_spectra V4

        SPECTRA_BYHX_V5 = 19, //BYHX_spectra V5


        SPECTRA_BYHX_V5_8 = 20, //BYHX_spectra V5, 8 polaris
        SPECTRA_POLARIS_8 = 21, //GZ Polaris 8 heads
        KM1024_8HEAD = 23,
        //#define HEAD_BOARD_TYPE_KM1024_8HEAD_V2		24///aahadd 20101221, km1024_8h v1&v3 all use 23
        KM512_8H_GRAY = 24,

        ///aah add 20110317km512 8head gray	

        KM512_8H_WATER = 25,

        ///aah add 20110418 km512 8head WATER
        KM512_16H_WATER = 26,

        ///aah add 20110418 km512 8head WATER
        KM1024_16HEAD = 27,
        SP_EMERALD_04H = 28, //BYHX-Spectra_Emerald_V1-4Head-4Head
        SP_KM1024_02H = 29, //
        SP_POLARIS_04H = 30,
        KM1024_8H_GRAY = 31,
        SP_KM1024i_02H = 32,
        XAAR382_16HEAD = 33,



        POLARIS_16HEAD = 35,
        EPSON_GEN5_4HEAD = 36,


        _512OVER1024_8HEAD = 34,
        SPECTRA_POLARIS_V7_16H = 35,
        SP_SG1024_02H = 36, //
        KYOCERA_4HEAD = 37, // KYOCERA 4HEAD board--���ͷ�汨��382 8ͷ...
        SP_XAAR1001_02H = 38,
        SG1024_8HEAD = 39,
        KM512I_4H_GRAY_WATER = 40,

        KM1024I_8H_GRAY = 41,
        SG1024_4H = 42,
        SP_XAAR1001_1H = 43,

        SG1024_4H_GRAY = 44,
        SG1024_8H_GRAY_1BIT = 45,
        SG1024_8H_GRAY_2BIT = 46,
        KM1024_4H = 47,
        KM512I_8H_GRAY_WATER = 48,
        KM1024I_16H_GRAY = 49,
        XAAR501_8H = 50,
		KM1800i_2H = 51,
        SP_KM512_1H = 52,
        SP_KM1024_1H = 53,
        SP_KM1024i_1H = 54,
        KYOCERA_4HEAD_1H2C=55,
        KM1800i_8H=56,
        KY600A_4HEAD=64,
        KY_RH06_4HEAD			=65,
        M600SH_4HEAD	=	70,
        EOPS_EPSON_DX6 =71,
        GMA9905300_8H = 72,
        EOPS_SAMBA = 73,
        GMA3305300_8H = 74,	//!< GMA3305300_8Hͷ��,300dpi,5pL,33mm(384 nozzle),no self heat
        SG1024_8H_BY100 = 75,	//!< SG1024 8Hͷ��,200dpi+200dpi=400dpi,8 rows of 128 nozzles,֧�ִ�ϵ��1ɫ��2ɫ������ͷ
        KM1024A_8HEAD		=	76,
        EPSON_S2840_4H = 77,
        EPSON_5113_8H = 78,
        Ricoh_Gen6_4H = 79,
        Ricoh_Gen6_3H4H = 80,		// ��һ�ж�ͷ��Ŷ���, ��ͷ�������Ժ����
        Ricoh_Gen6_16H = 81,
        EPSON_4720_4H = 82,
        EPSON_5113_6H = 83, // Ӳ��ͬEPSON_5113_8H,ֻ��ȥ��������������Ԫ
        EPSON_5113_2H = 84,
        XAAR_1201_2H = 85, // V2
        XAAR_1201_4H = 86,// V2
        EPSON_I3200_4H_8DRV = 87, //i3200
        EPSON_S1600_8H = 88,
    }

    public enum VenderID
    {
#if !LIYUUSB
        LIYU = 0x1,
        INWEAR = 0x2,
        INWEAR_ROLL_UV = 0x82,
        INWEAR_FLAT_UV = 0x93,
        INWEAR_UV = 0x13,

        ALLWIN = 0x3,
        ALLWIN_FLAT_UV = 0x83,
        ALLWIN_ROLL_TEXTILE = 0x203,
        ALLWIN_BELT_TEXTILE = 0x303,

        ALLWIN_TEXTILE4 = 0x1E,
        ALLWIN_ROLL_UV = 0x9E,
        ALLWIN_TEXTILE2 = 0x50,
        ALLWIN_EPSON4 = 0x51,
        ALLWIN_UV_EPSON4 = 0xD1,


        KINCOLOR = 0x4,
        KINCOLOR_FLAT_UV = 0x84,
        HUMAN = 0x5,
        HUMAN_FLAT_UV = 0x85,
        MYJET = 0x6,
        MYJET_FLAT_UV = 0x86,
        LOTUS = 0x7,
        LOTUS_FLAT_UV = 0x87,
        COLORJET = 0x8,
        COLORJET_FLAT_UV = 0x88,
        COLORJET_ROLL_UV = 0x188,
        COLORJET_ROLL_TEXTILE = 0x208,
        COLORJET_BELT_TEXTILE = 0x308,
        HAPOND = 0x9,
        HAPOND_FLAT_UV = 0x89,
        HAPOND_FLAT_UV_S = 0x0189,
        RODINJET = 0xA,
        RODINJET_FLAT_UV = 0x8A,

        GONGZENG = 0xB,
        GONGZENG_FLAT_UV = 0x8B,
        GONGZENG_ROLL_UV = 0x18B,
        GONGZENG_BELT_TEXTILE = 0x30B, //ӡ����
        GONGZENG_DOUBLE_SIDE = 0xA0B,

        BRITEJET = 0xC,
        BRITEJET_FLAT_UV = 0x8C,
        JHF = 0xD,
        JHF_FLAT_UV = 0x8D,
        VOXINDIA = 0xE,
        VOXINDIA_FLAT_UV = 0x8E,

        WITCOLOR = 0xF,
        WITCOLOR_FLAT_UV = 0x8F,
        WITCOLOR_ROLL_UV = 0x018F,

        MONOTECH = 0x10,
        MONOTECH_FLAT_UV = 0x90,

        MEIJET = 0x11,
        MEIJET_FLAT_UV = 0x91,

        DOCAN = 0x12,
        DOCAN_FLAT_UV = 0x92,
        DOCAN_ROLL_UV = 0x192,
        DOCAN_BELT_TEXTILE  =  0x312,
        DOCAN_BELT = 0x0812,

        MICOLOR = 0x14,
        MICOLOR_FLAT_UV = 0x94,

        HUMAN_STITCH = 0x15,
        HUMAN_ROLL_UV = 0x95,

        TOPJET = 0x16,
        TOPJET_FLAT_UV = 0x96,


        DESIGN = 0x17,
        DESIGN_FLAT_UV = 0x97,

        TATE = 0x18,
        TATE_FLAT_UV = 0x98,

        RABILY = 0x19,
        RABILY_FLAT_UV = 0x99,

        BOXIN = 0x1A,
        BOXIN_FLAT_UV = 0x9A,


        BEMAJET = 0x1B,
        BEMAJET_FLAT_UV = 0x9B,

        COLORTOP = 0x1C,
        COLORTOP_FLAT_UV = 0x9C,

        INKWIN = 0x1D,
        INKWIN_FLAT_UV = 0x9D,


        DGI = 0x1F,
        DGI_FLAT_UV = 0x9F,

        RasterDigital = 0x21,
        RasterDigital_FLAT = 0xA1,

        YATAO = 0x40,

        HUMAN_REV = 0x52,
        HUMAN_TEXTILE = 0x53,

        BinterJET = 0x54,
        SKYSHIP = 0x55,
        SKYSHIP_FLAT_UV = 0xD5,

        BIGPRINTER = 0x56,
        BIGPRINTER_FLAT_UV = 0xD6,

        SIGNSTAR = 0x57,
        LINGFENG = 0x58,
        LINGFENG_RollUV_16H = 0x188,
        LINGFENG_FLAT_UV = 0xD8,


        SCORPION = 0x0059,
        SCORPION_ROLL_UV = 0x239,
        SCORPION_ROLL_TEXTILE = 0x259,

        SAISHUN = 0x0060,
        SAISHUN_BELT_TEXTILE = 0x360,
        SAISHUN_FLAT_TEXTILE = 0x760,

        AUDLEY = 0x0061,
        AUDLEY_BELT_TEXTILE =0X361,

        AOJET = 0x66,
        AOJET_UV = 0xE6,

        FHZL_3D_PRINT = 0x645,
        ZXHZ_3D_PRINT = 0x65E,

        BYHX_ENG = 0x7F,

        LECAI_FLAT_UV = 0xC2,
        NKT_Onepass = 0x46A,
        NKT = 0x6A,
        NKT_FLAT_SLOVENT = 0x16A,
        TLHG = 0x47, //��������
        TLHG_BELT_TEXTILE = 0x347, //��������

        ALLPRINT = 0x49,
        ALLPRINT_BELT_TEXTILE = 0x349,
        ALLPRINT_FLAT_TEXTILE = 0x749,
        ALLPRINT_DOUBLE_FLAT = 0x949,

        YINKELI = 0x4D,
        YINKELI_FLAT_TEXTILE = 0x74D,
#else
        JHF = 0xD,
        JHF_FLAT = 0x8D,
        RasterDigital = 0x21,
        RasterDigital_FLAT = 0xA1,
        LOTUS = 0x22,
        LOTUS_FLAT = 0xA2,

        GETHRAY = 0x23,
        GETHRAY_FLAT = 0xA3,
#endif
        KEDITEC = 0x44, //20141027				(����)	
        KEDITEC_ROLL_TEXTILE = 0x244, //20141027				(����)	
        KEDITEC_FLAT_TEXTILE = 0x744, //20150911				(����)	
        //FLORA(����)	0x4B	0XCB			0X34B				0X74B
        FLORA = 0x4b,
        FLORA_FLAT_UV = 0xcb,
        FLORA_BELT_TEXTILE = 0x34b, //T180
        FLORA_FLAT_TEXTILE = 0x74b, //T50

        BluePrint = 0x20,
        BluePrint_FLAT_UV = 0xA0,
        BluePrint_ROLL_UV = 0x1A0,
        BluePrint_BELT_UV = 0x820,
        ORIC = 0x6F,
        BoHui = 0x76, //(����)
        BoHui_FLAT_TEXTILE = 0x776, //(����)

        FangDa = 0x77, //FangDa (����)
        FangDa_BELT_TEXTILE = 0x377, //FangDa
        ZHUOZHAN = 0x79, //׿չ
        ZHUOZHAN_BELT_UV = 0x0879, //׿չBELT_UV
        QZDY = 0x7A, //(Ȫ�ݶ�ӡ)
        KeSaiEn = 0x7B, //(������)
        FuMeiLiYin = 0x7C, //(������ӡ)
        HengYin = 0x7D, //(��ӡ)
        DXSC = 0x7E, //(�����Ӵ�)
        DXSC_FLAT_UV = 0xfe,
        DXSC_FLAT_GLASS = 0xB7E,

        YUEDA_BELT_UV = 0x843,

        BIHONG = 0x29,
        BIHONG_FLAT_TEXTILE = 0x729,

        RuiZhi = 0x8002, //����
        JiKai = 0x800E, //��������
        JiKai_3D = 0x860E,
        HuiLiCai = 0x8705,
        LongYuan = 0x8604,//¡Դ
        PuQi = 0x8300,//����
        PuQi_ROLL_TEXTILE = 0x8200,
        HanTuo = 0x8712,
        JianRui = 0x8066,
        JianRui_FLAT_UV = 0x80E6,
        MAX_VENDERID
    }

    public enum INTBIT : uint
    {
        Bit_0 = 0x01,
        Bit_1 = 0x02,
        Bit_2 = 0x04,
        Bit_3 = 0x08,
        Bit_4 = 0x10,
        Bit_5 = 0x20,//��ɫ����
        Bit_6 = 0x40,//����
        Bit_7 = 0x80,
        Bit_8 = 0x100,
        Bit_9 = 0x200,
        Bit_10 = 0x400, //��ͷ����
        Bit_11 = 0x800,
        Bit_12 = 0x1000,
        Bit_13 = 0x2000,
    }

    public enum FeatherType : byte
    {
		Gradient = 0,
		Uniform =1,
        Wave = 2,
		Advance =3,
        Uv =4,
        //Joint = 80,
	    Debug = 0xFF,
    }
	public enum EpsonFeatherType : byte
	{
		None = 0,
		Small, 
		Medium,
		Large,
        //SuperLarge,
        Custom,
	}
	 public enum EpsonAutoCleanWay: byte
	 {
//		 Default = 0,
//		 Customized,
		 Strong,
		 Normal,
		 Weak,
		 Refill
	 }

	public enum MultipleInkEnum : byte
	{
		Default = 0,
		Double, 
		More,
	}

	
	public enum Xaar382TempMode:byte
	{
		None = 0,
		InternalMode = 3,
//		ExternalMode = 2,
	}

	public enum PipeCmdResult
	{
		ExecOk = 0, // ����ִ�гɹ�
		CmdErr = 1, // �������
		ExecFail = 2, // ����ִ��ʧ��
	}

  
    public enum UVStatus
    {
        OFF = 0,
        ON60 = 1,
        ON100 = 2
    }
    public enum UVStatus_ALLWIN
    {
        OFF = 0,
        LOW = 1,
        MID = 2,
        HIGH = 3
    }
    public enum UvLightType:uint
    {
        /// <summary>
        /// SubZero_055_085S
        /// </summary>
        UvLightType1 = 0,//
        /// <summary>
        /// new type
        /// </summary>
        UvLightType2 = 1,
        /// <summary>
        /// non-polar �޼�UV��
        /// </summary>
        UvLightType3 = 2
    }
    public enum BoolEx
    {
        Global,
        True,
        False
    }

    public enum CalibrationSubStep
    {
        //#if EPSONLCD
        GroupLeft = 0,
        GroupRight,
        Bidirection,
        Left,
        Right,
        //#else
        //		Left =0,
        //		Right,
        //		Bidirection,
        //#endif
        Vertical,
        Overlap,
        Step,
        Origin,
        All
    }
    public enum CalibrationDescription
    {
        Welcome,
        Mechanical,
        Calibration,
        Finish,
    }
    public enum CalibrationTitle
    {
        Welcome,
        Mechanical,
        Calibration,
        Finish,
    }

    public enum EnumVoltageTemp
    {
        TemperatureCur2 = 7,
        TemperatureSet = 6,
        TemperatureCur = 5,
        PulseWidth = 1,
        VoltageBase = 3,
        VoltageCurrent = 0,
        VoltageAjust = 2, //Adjust
        XaarVolInk = 8,
        XaarVolOffset = 9,
    }
}
