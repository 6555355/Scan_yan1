/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using ErrRes;

namespace BYHXPrinterManager
{
	public enum Password_UserResume:byte
	{
	};

	public enum COMCommand_Abort : short
	{
		Command				= 0xF1,   //错误的命令
		Parameter			= 0xF2,   //错误的参数
		MoveAgain			= 0xF3,   //运行中错误 已经运动,又发运动命令 
		TxTimeOut			= 0xF4,   //发送超时
		FormatErr			= 0xF5,   //校验和错误
		Encoder				= 0xF6,   //告诉上位机光栅错误
		MeasureSensor		= 0xF7,	  //纸宽传感器错误
		NoPaper				= 0xF8,	  //没有介质
		PaperJamX			= 0xF9,	  //X轴运动受阻
		PaperJamY			= 0xFA,	  //Y轴运动受阻
		IndexNoMatch		= 0xFB,
		LimitSensor			= 0xFC,   //Limit Error
		StepEncoder         = 0xFD,

		ReadEEPROM          = 0xE1,   //Read EEPROM error 
		WriteEEPROM         = 0xE2,   //Write EEPROM error
		WriteEEPROMTwice    = 0xE3,   //Write EEPROM  2 次，一般 报告这个错误时，MB 没有应答信号
		TimeOver            = 0xE4,   //限制的时间用光了
		TimeWarning         = 0xE5,   //到限制的时间还有50个小时
		Lang                = 0xE6,   //语言和 限制的不匹配
		IllegalContent      = 0xE7,   //错误的EEPROM 的内容，EEPROM 配置没有完成
		IllegalPwd          = 0xE8,   //错误的密码
        SwDogEey            = 0xE9,   //必须使用加密狗软件

		RatioEEPROM         = 0xEA,   //EEPROM读得齿轮比值与默认齿轮比值相差太大
		RatioMeasure        = 0xEB,   //自动测量得到的齿轮比值与当前齿轮比值相差太大

        NotifyMeasure       = 0xEC,
        NotifyGoHome        = 0xED,
        SensorPause         = 0xEE,   //错误的传感器暂停

		NoSupportCorrectRatio = 0xEF, //伺服电机，齿轮比为1.0f,需更改电机参数

        //cheney add.
        OverCurrent_Protect = 0xDF,   //电流过载保护 
        OverTemp_Protect    = 0xDE,   //温度过高保护 
        InternalErr_1       = 0xDD,   //内部错误1
        InternalErr_2       = 0xDC,   //内部错误2
        OutOfPrintRang      = 0xD1,   //打印越界
        InitERR             = 0xD2,   //dsp初始化失败
        DoubleYERR          = 0xD3,   //双轴位置偏差过大，急停
        NoPaperCancelPrint = 0xD4,   // 缺纸,自动取消打印
        TouchSensor = 0xD7,   // 触发当前轴传感器,运动终止
    }

	public enum	Software: byte
	{
		BoardCommunication,	//USB Send Command ErrorPause, Abort, Move,	USB Board Command Error 
		NoDevice,			//Open USB Error	USB Board Open Error
		Parser,				//File Format Error
		PrintMode,			//Print Mode Parameter Error
		MediaTooSmall,      //Media too small  添加的介质宽太小
        Shakhand,			//Security
		MismatchID,			//Cannot found match ID
		VersionNoMatch,		//Min Support version
		Language,

		FileResolutionNoMatchPrinter,
		FileNoSupportResolution,
		FileNoSupportForamt,
		FilePassMatchResolution,
		FileColorNumber,
		StepTolTooMuch,
		GetHWSettingFail=15,

        MustUpdateFW, //必须升级 FW 的版本
        FWIsNotDogKey, //必须升级 加密狗的 FW 的版本
        UpdateFileWrongFormat, //错误的升级文件格式
        DataMiss,                 //未读取到文件数据
        ColorDeep = 20,   //0x14 颜色深度不匹配
        MediaHeightTooSmall=21,//添加的介质高太小
        OldMainBoard = 22,//旧的主板需要更换
        MainBoardNoSupportSetKyVol = 23,//主板不支持设置京瓷电压功能

        SwVersionQaNoTest = 24, // 此软件版本是未经QA测试的版本

        Unknown
    }

	public enum SensorError : byte
	{
	}

	public enum ErrorCause : byte
	{
		COM		= 1,
		CoreBoard,
		Software,
		Sensor,
		Passwd,
        CoreBoard_FPGA =6,
        HEADBOARD =7,
        // add 
		HeadBoard1,
		HeadBoard2,
		HeadBoard3,
		HeadBoard4,
		HeadBoard5,
		HeadBoard6,
		HeadBoard7,		

        EnumCmdSrc_FX2 = 0x10,
        EnumCmdSrc_DSP = 0x11,
        EnumCmdSrc_HEADBOARD = 0x12,
        EnumCmdSrc_FPGA = 0x13,
        EnumCmdSrc_EEPROM = 0x14,
		Unknown
	}
	public enum ErrorAction : byte
	{
		Service		= 0x80,
		Abort		= 0x40,
		UserResume  = 0x20,
		Pause		= 0x10,
		Init		= 0x08,
		Warning		= 0x04,
		Updating    = 0x02,
		FwMsg    = 0x01,
	}

	public struct SErrorCode
	{
        private short _n16ErrorCode;
		public byte nErrorCode;
		public byte nErrorSub;
		public byte nErrorCause;
		public byte nErrorAction;
        /// <summary>
        /// 新格式的错误码，除ErrorCause==Software外都用这个字段
        /// </summary>
        public short n16ErrorCode
        {
            get { return _n16ErrorCode; }
            set
            {
                _n16ErrorCode = value;
                byte[] bytes = BitConverter.GetBytes(_n16ErrorCode);
                nErrorCode = bytes[0];
                nErrorSub = bytes[1];
            }
        }
		public SErrorCode(int message)
		{
			nErrorCode = (byte)message;
			message = message >> 8;
			nErrorSub = (byte)message;
			message = message >> 8;
			nErrorCause = (byte)message;
			message = message >> 8;
			nErrorAction = (byte)message;
            byte[] bytes = new byte[] { nErrorCode, nErrorSub };
            _n16ErrorCode = BitConverter.ToInt16(bytes, 0);
		}

	    public int ToInt32()
	    {
	        return (nErrorAction << 24) + (nErrorCause << 16) + (nErrorSub << 8) + nErrorCode;
	    }

		public static bool IsWarningError(int iErrCode)
		{
			SErrorCode error = new SErrorCode(iErrCode);
			return ((ErrorAction)error.nErrorAction == ErrorAction.Warning);
		}

		public static bool IsOnlyPauseError(int iErrCode)
		{
			SErrorCode error = new SErrorCode(iErrCode);
			return ((ErrorAction)error.nErrorAction == ErrorAction.UserResume);
		}

        public static bool IsGzCartonShowError(int iErrCode)
        {
            SErrorCode error = new SErrorCode(iErrCode);
            ErrorAction action = (ErrorAction) error.nErrorAction;
            return (action == ErrorAction.Abort
                //|| action == ErrorAction.Service
                //|| action == ErrorAction.Warning
                );
        }

        /// <summary>
        /// 判断是否为印可丽缺纸报警,20150522 gzw
        /// </summary>
        /// <param name="iErrCode"></param>
        /// <returns></returns>
        public static bool IsNoPaperError(int iErrCode)
        {
            SErrorCode error = new SErrorCode(iErrCode);
            ErrorAction action = (ErrorAction)error.nErrorAction;
            return (action == ErrorAction.Warning
                && (error.n16ErrorCode == (int)CoreBoard_Warning.NO_MATERIAL)
                && error.nErrorCause == (int) ErrorCause.CoreBoard
                );
        }

		public static string GetInfoFromErrCode(int code )
		{
		    if (code != 0)
		    {
		        string errString = "";
		        SErrorCode errorcode = new SErrorCode(code);
		        ErrorCause cause = (ErrorCause) errorcode.nErrorCause;
		        switch (cause)
		        {
		            case ErrorCause.EnumCmdSrc_DSP:
		            case ErrorCause.COM:
		                COMCommand_Abort comcode = (COMCommand_Abort) errorcode.n16ErrorCode;
		                errString = comcode.GetType().Name + "_" + comcode.ToString();
		                break;
		            case ErrorCause.CoreBoard:
		            {
		                switch ((ErrorAction) errorcode.nErrorAction)
		                {
		                    case ErrorAction.Service:
		                        CoreBoard_Service scode = (CoreBoard_Service) errorcode.n16ErrorCode;
		                        errString = scode.GetType().Name + "_" + scode.ToString();
		                        break;
		                    case ErrorAction.Abort:
		                        CoreBoard_Fatal acode = (CoreBoard_Fatal) errorcode.n16ErrorCode;
		                        errString = acode.GetType().Name + "_" + acode.ToString();
		                        break;
		                    case ErrorAction.UserResume:
		                        CoreBoard_Err pcode = (CoreBoard_Err) errorcode.n16ErrorCode;
		                        errString = pcode.GetType().Name + "_" + pcode.ToString();
		                        break;
#if false
							case ErrorAction.UserResume:
								Password_UserResume ucode = (Password_UserResume)errorcode.nErrorCode;
								errString = ucode.GetType().Name +"_"+ ucode.ToString();
								break;
#endif
		                    case ErrorAction.Init:
		                        CoreBoard_Initialing icode = (CoreBoard_Initialing) errorcode.n16ErrorCode;
		                        errString = icode.GetType().Name + "_" + icode.ToString();
		                        break;
		                    case ErrorAction.Warning:
                                CoreBoard_Warning wcode = (CoreBoard_Warning)errorcode.n16ErrorCode;
		                        errString = wcode.GetType().Name + "_" + wcode.ToString();
		                        break;
		                    case ErrorAction.Updating:
                                CoreBoard_Updating upcode = (CoreBoard_Updating)errorcode.n16ErrorCode;
		                        errString = upcode.GetType().Name + "_" + upcode.ToString();
		                        break;
		                }
		            }
		                break;
		            case ErrorCause.Software:
		                Software softcode = (Software) errorcode.nErrorCode;
		                errString = softcode.GetType().Name + "_" + softcode.ToString();
		                break;
		            case ErrorCause.Sensor:
		                SensorError sensorcode = (SensorError) errorcode.nErrorCode;
		                errString = sensorcode.GetType().Name + "_" + sensorcode.ToString();
		                break;
#if false
					case ErrorCause.Passwd:
						Password_UserResume passcode = (Password_UserResume)errorcode.nErrorCode;
						errString = passcode.GetType().Name +"_"+ passcode.ToString();
						break;
#endif
		            case ErrorCause.EnumCmdSrc_FX2:
		                switch ((ErrorAction) errorcode.nErrorAction)
		                {
		                    case ErrorAction.Service:
		                        EnumCmdSrc_FX2_Service scode = (EnumCmdSrc_FX2_Service) errorcode.n16ErrorCode;
		                        errString = scode.GetType().Name + "_" + scode.ToString();
		                        break;
		                    case ErrorAction.Warning:
                                EnumCmdSrc_FX2_Warning wcode = (EnumCmdSrc_FX2_Warning)errorcode.n16ErrorCode;
		                        errString = wcode.GetType().Name + "_" + wcode.ToString();
		                        break;
		                }
		                break;
		            case ErrorCause.EnumCmdSrc_HEADBOARD:
		                EnumCmdSrc_HEADBOARD headbordcode = (EnumCmdSrc_HEADBOARD) errorcode.n16ErrorCode;
		                errString = headbordcode.GetType().Name + "_" + headbordcode.ToString();
		                break;
		            case ErrorCause.EnumCmdSrc_FPGA:
                        EnumCmdSrc_FPGA_Abort aFPGAcode = (EnumCmdSrc_FPGA_Abort)errorcode.n16ErrorCode;
		                errString = aFPGAcode.GetType().Name + "_" + aFPGAcode.ToString();
		                break;
		            case ErrorCause.EnumCmdSrc_EEPROM:
                        EnumCmdSrc_EEPROM_Error eepromCode = (EnumCmdSrc_EEPROM_Error)errorcode.n16ErrorCode;
		                errString = eepromCode.GetType().Name + "_" + eepromCode.ToString();
		                break;
		        }

                //string msg = ErrRes.GetResString(code);
		        string msg = ErrResManager.GetResString_SQLite(code, PubFunc.GetVID(),0);
                // 如果是"触发当前轴传感器",则获取具体传感器信息
		        if (errorcode.nErrorCause == (byte) ErrorCause.COM &&
                    errorcode.n16ErrorCode == (short)COMCommand_Abort.TouchSensor)
		        {
		            byte[] buf = new byte[64];
		            uint bufsize = (uint) buf.Length;
		            int ret = CoreInterface.GetEpsonEP0Cmd(0x83, buf, ref bufsize, 0, 4);
		            if (ret != 0)
		            {
                        if (buf[2] == (short)COMCommand_Abort.TouchSensor) // 验证错误号是否匹配
		                {
		                    switch (buf[6])
		                    {
                                case 1:
		                        {
		                            msg += string.Format("[X-{0}]", buf[7] == 0 ? "origin" : "end");
		                            break;
		                        }
                                case 2:
                                {
                                    msg += string.Format("[Y-{0}]", buf[7] == 0 ? "origin" : "end");
                                    break;
                                }
                                case 4:
                                {
                                    msg += string.Format("[Z-{0}]", buf[7] == 0 ? "origin" : "end");
                                    break;
                                }
                            }
		                }
		            }
		        }
		        if (errorcode.nErrorCause >= (int) ErrorCause.HEADBOARD
		            && errorcode.nErrorCause <= (int) ErrorCause.HeadBoard7
                    && string.IsNullOrEmpty(msg)
                    && errorcode.n16ErrorCode <= 0xff)
		        {
		            // 当错误来源为头板且错误号小于0xff,且找不到匹配的错误信息时,尝试使用主板来源搜索错误信息;
                    //兼容老的错误在新机制下来源由主板变为头板的情况
		            errorcode.nErrorCause = (byte) ErrorCause.CoreBoard;
                    //msg = ErrRes.GetResString(errorcode.ToInt32());
                    msg = ErrResManager.GetResString_SQLite(errorcode.ToInt32(), PubFunc.GetVID(),0);
                }
		        // 数据库查询不到错误信息时,尝试在程序资源文件中查找
                if (string.IsNullOrEmpty(msg) && !string.IsNullOrEmpty(errString))
                {
                    msg = ErrResManager.GetResString(errString,ErrResEnum.Y1);
                }

		        if (errorcode.nErrorSub != 0 && msg!=null)
                    msg = string.Format(msg, errorcode.nErrorSub);
                if ((ErrorAction)errorcode.nErrorAction == ErrorAction.FwMsg && string.IsNullOrEmpty(msg))
                    return "";
		        if (string.IsNullOrEmpty(msg))
		            return "[" + code.ToString("X8") + "] Unknown Error"; // + errString;
		        else
		            return "[" + code.ToString("X8") + "]" + msg;
		    }
		    else
		        return "";
		}

		static public string GetEnumDisplayName(Type enumType,object enumValue)
		{
			string errString = enumType.Name +"_"+ enumValue.ToString();
			string msg = ErrResManager.GetResString(errString, ErrResEnum.Y1);
            if (string.IsNullOrEmpty(msg))
				return "Unknown Enum:" + errString;
			return msg;
		}

        static public string GetResString(string errString)
        {
            string msg = ErrResManager.GetResString(errString, ErrResEnum.Y1);
            if (string.IsNullOrEmpty(msg))
                return "Unknown Enum:" + errString;
            return msg;
        }

        /// <summary>
        /// 判断此错误号是否启用
        /// </summary>
        /// <param name="code">错误号</param>
        /// <param name="vid">厂商ID</param>
        /// <returns>是否启用</returns>
        public static bool IsEnable(int code)
        {
            return ErrResManager.IsEnable(code, PubFunc.GetVID(),0);
        }
	}


	public enum CoreBoard_Service:short
	{
		SX2 = 0,			        //(STATUS_SVC+0)USB chip. 经过0.5秒，USB芯片还没有正常启动，USB芯片或者损坏
		FPGA0,				        //(STATUS_SVC+1)FPGA chip 1. 上电后，FPGA的nSTATUS持续为高，FPGA芯片或者损坏 
		FPGA1,				        //(STATUS_SVC+2)FPGA chip 2. 拉低nCONFIG之后，CONFIG_Done或者nSTATUS还是高
		FPGA2,				        //(STATUS_SVC+3)FPGA chip 3. nCONFIG拉高之后, nSTATUS保持高.
		FPGA3,				        //(STATUS_SVC+4)FPGA chip 4. When config FPGA, FPGA report err, and retry 10 times
		HEADTOMAINROAD,             //(STATUS_SVC+5)UPDATE Main Board Failed
		BYHX_DATA,			        //(STATUS_SVC+6)  //板子没有经过BYHX初始化
        SDRAM_CHECK = 7,            //(STATUS_SVC+7)  //SDRAM 的Check没有通过
        FLASH,				        //(STATUS_SVC+8)  //Flash. Cann't read FPGA from flash.
		SDRAM_NOMATCH,		        //(STATUS_SVC+9)  //SDRAM 太小，不能支持当前的设置。//I change the code of this status 
		READBOARDID_FAIL,	        //(STATUS_SVC+10) //Read board id Error。实际上是操作加密芯片失败
        ARM_MOTION,       	        //(STATUS_SVC+11) //ARM Motion system initial error.系统的ARM请求初始化错误.
        HEADBOARD_INIT_FAIL,		//(STATUS_SVC+12) //HEAD board initial fail。头板初始化失败，实际上是EPSON头板操作加密芯片失败
        HEADBOARD_NO_FW,            //(STATUS_SVC+13) //有头板没有合法的Firmware

        EEPROM_CHK = 16,	        //(STATUS_SVC+16) //BYHX_TOOL check EEPROM 没有通过。

        HB_WAVEFROM_Download_fail = 0x62,   //WAVEFROM下载错误（0x08020062）
        HB_WAVEFROM_Init_fail = 0x63,       //WAVEFROM初始化配置错误（0x080020063）
        HB_OPRATION_EEPROM_FAIL = 0x64,     //hb操作eeprom失败
        HB_FPGA_ERROR = 0x65,               //头板FPGA错误
        HBFW_ERROR,                         //(HBERR_SVC + 102)	//头板的FW和板子不匹配，需要更换头板
        HEADVENDERDISMATHCH,                // 喷头厂商不匹配
    }

    public enum CoreBoard_Fatal : short
    {
        SX2RESET = 0,                       //USB chip异常重启
        INTERNAL_WRONGHEADER,               //Wrong data header
        INTERNAL_WRONGHEADERSIZE,           //Wrong data header size
        INTERNAL_JOBSTARTHEADER,            //Job header不应附带额外数据
        INTERNAL_BANDDATASIZE,              //Band Header中的BAND数据数量和实际BAND数据数量不符
        INTERNAL_WRONGFORMAT,               //得到的串口数据格式不对
        INTERNAL_DMA0WORKING,               //DMA0 still working after a band complete
        INTERNAL_PRINTPOINT,                //Wrong startpoint and endpoint when print
        INTERNAL_OLIMIT,                    //Band的打印起始点小于原点
        INTERNAL_OPPLIMIT,                  //图像结束位置超出了打印机最远点,Image too width
        DSPINITS1,                          //运动控制第一阶段初始化没有通过
        DSPINITS2,                          //运动控制第二阶段初始化没有通过
        HEADINITS1,                         //头板第一阶段初始化没有通过
        HEADINITS2,                         //头板第二阶段初始化没有通过
        HEADTOMAINROAD,                     //主板的LVDS接收芯片没有LOCK,或线没有插
        INTERNAL_BANDDIRECTION,             //Band定义中的方向值超出定义
        DSPUPDATE_FAIL,                     //更新失败：主板写入阶段
        EEPROM_READ,                        //(STATUS_FTA+17) //读取EEPROM失败	
        EEPROM_WRITE,                       //(STATUS_FTA+18) //写入EEPROM失败	
        FACTORY_DATA,                       //(STATUS_FTA+19) //板子没有经过出厂初始化设置
        HEADBOARD_RESET,                    //(STATUS_FTA+20) //头板被重新启动
        SPECTRAHVBINITS1,                   //(STATUS_FTA+21) //Spectra High Voltage Board第一阶段初始化失败
        PRINTHEAD_NOTMATCH,                 //(STATUS_FTA+22) //头板报告的喷头种类与FactoryData里面的设定不匹配， 请更换头板或重新设定硬件设置。
        MANUFACTURERID_NOTMATCH,            //(STATUS_FTA+23) //控制系统与FW的生产厂商不匹配，需更换系统或者升级FW
        LIMITEDTIME_RUNOUT,                 //(STATUS_FTA+24) //严重错误: 超过限时时间，请输入新的密码.
        INTERNAL_SW1,                       //(STATUS_FTA+25) //Internal error, blank band and y-distance == 0
        USB1_USB1CONNECT = 0x1A,            //STATUS_FTA+26)  //连接到USB1口   //This error is moved to Fatal error, 2.0版本这里写错了，写为(STATUS_ERR+26)
        UILANGUAGE_NOT_MATCH,               //(STATUS_FTA+27) //严重错误: 软件使用的语言设置与权限不符，请重新设置软件语言或者输入新的语言选配密码, 并重启打印机.
        INTERNAL_WRONGPINCONF,              //(STATUS_FTA+28) //PINCONF写入FPGA错误
        FACTORY_COLORNUM_WRONG,             //(STATUS_FTA+29) //Factory 中写入的color number 不对
        HB_EEPROM_WRT_ERR,                  //(STATUS_FTA+30) //头板的EEPROM写入失败
        HB_OVERHEAT,                        //(STATUS_FTA+31) //喷头加热超过了55度，所有加热被强行关闭。
        SHAKEHAND_ERR,                      //(STATUS_FTA+32) //软件错误或者版本错误，没有通过握手协议。
        SWVERSION_ERR,                      //(STATUS_FTA+33) //固件要求特殊的软件版本，版本错误。
        NOT_SUPPORT_2C_SHARE_1H,            //(STATUS_FTA+34) //当前系统设置不支持一头两色。
        LIMITEDINK_RUNOUT = 35,             //(STATUS_FTA+35) //超过墨水用量限制，请重新输入口令
        SWVERSION_ERR_INK_PWD,              //(STATUS_FTA+36) //固件要求软件版本支持墨水密码，版本错误，
        EPSON_HEAD_BAD = 37,                //(STATUS_FTA+37) //Print head error.
        EPSON_HEAD_FPGA_ERR = 38,           //(STATUS_FTA+38) //head board FPGA error. For KM1024, write FPGA parameter error
        SECURITY_CHIP_ERR = 39,             //(STATUS_FTA+39) //Write board id error. //only take place with write_board_tools_updater.
        T_JET_PASSNUM_ERR,                  //(STATUS_FTA+40) //aahadd for t-jet
        CALIBRATION_DATA,                   //(STATUS_FTA+41) //出厂初始化设置出错。
        HEADBOARD_CONFIG,                   //(STATUS_FTA+42) //头板配置不正确
        NO_TEMPERATURE_SENSOR,              //(STATUS_FTA+43) //检测不到温度传感器
        INK_SPILL,                          //(STATUS_FTA+44) //负压墨盒渝墨
        INK_SUPPLY_LOW,                     //(STATUS_FTA+45) //供墨墨盒缺墨		
        FUNC_BOARD_STARTUP,                 //(STATUS_FTA+46) //scorpion printer function board startup
        PRINT_DATA_ERROR,                   //(STATUS_FTA+47) //打印数据传输错误或丢失
        FACTORY_DATA_ERROR,                 //(STATUS_FTA+48) //FACTORY_DATA数据错误
        PANEL_EX_LOST,                      //(STATUS_FTA+49) //辅助控制板通信失败
        PANEL_EX_BUSY,                      //(STATUS_FTA+50) //辅助控制板忙
        HB_OVERCOOL = 56,                   //(STATUS_FTA+56) //喷头温度过低（异常），加热控制被强行关闭
        OVERCHANGE,                         //(STATUS_FTA+57) //喷头温度变化过于剧烈（异常），加热控制被强行关闭
        UV_RUNOUT = 59,                     //(STATUS_FTA+59) //UV灯时间用尽
		EMERGENCY_STOP = 0x3e,				//(STATUS_FTA+0x3E)	// scorpion功能板报急停
        EXT_INIT_ERR=0x44,                       //(STATUS_FTA+0x44)	// 外部设备初始化未完成
        DOUBLEY_ERRPOS = 0x45,              //(STATUS_FTA+0x45) //双Y轴，两轴位置偏差过大
        TEMP_RATIO_UNSUPPORT = 0x60,        //(STATUS_FTA+60) //设置了温度比率但是头板不支持 we report this error when factory set none-5 but headboard not support temperature scale
        WAVEFORM_INVALID = 0x65,            //(HBERR_FTA + 101)	//没有waveform有效标识
        FULL_HALF_VOLT_ERROR,               //(HBERR_FTA + 102)	//全半压输出异常(半压大于全压) 目前仅KM1024I有该检测
        EEPROM_PARAM_UNVALID,               //(HBERR_FTA + 103)	//eeprom参数非法，需要重新设置
    }

    public enum CoreBoard_Err:short
	{
		EP0OVERUN_SETUPDATA,		    //EP0命令被打断
		//USB1_USB1CONNECT,			    //连接到USB1口 //This error is moved to Fatal error
		UART2_TXTIMEOUT = 2,            //运动通讯超时
		UART1_TXTIMEOUT = 3,			//头板与主板通讯超时
		INTERNAL_PRINTDATA,             //Band数据没有打印完成
		FPGA_LESSDATA,                  //Print data is less than fire number or empty when trigger
		FPGA_ULTRADATA,                 //Print data is more than fire number
		FPGA_WRONGSTATUS,               //(STATUS_ERR+7)
		UV_CTR_WRONGSTATUS,             //(STATUS_ERR+8)  //Internal Status
		FPGA_WRONGCOMMAND,              //(STATUS_ERR+9)  //FPGA 收到错误的命令
		MOTION_SPEED_BAD,               //(STATUS_ERR+10) //运动速度太快或不均匀，数据还在出书，却有点火命令
		INTERNAL_MOVING_TIMEOUT,		//(STATUS_ERR+11) //运动命令发出30秒后，没有收到运动完成命令
		INTERNAL_WRONGAUTOCLEAN,		//(STATUS_ERR+12) //错误的AUTOCLEAN命令，不应该发生
		INTERNAL_WRONGBANDINFO,			//(STATUS_ERR+13) //错误的AUTOCLEAN命令，不应该发生

		CURVE_EXCEEDSIZE,			    //(STATUS_ERR+14) //发送的温度电压曲线数据大小超过额定范围；
		CURVE_PCSND_DATA,			    //(STATUS_ERR+15) //printmanager发送的数据错误；
		CURVE_SAVE_ROM,			        //(STATUS_ERR+16) //主板数据存储到eeprom失败；
		CURVE_NO_SPACE,			        //(STATUS_ERR+17) //主板eeprom没有足够空间存储新接收的数据；
		CURVE_UNKN_HDNUM,			    //(STATUS_ERR+18) //printmanager发送的喷头号不存在；
		CURVE_MAP,			            //(STATUS_ERR+19) //主板eeprom曲线数据映射到内存错误
		CURVE_READ_ROM,			        //(STATUS_ERR+20) //主板读取eeprom失败
		CURVE_ROM_CHKSUM,			    //(STATUS_ERR+21) //曲线数据checksum校验错误
		HB_NOSUPPORT_FUNC,			    //(STATUS_ERR+22) //头板类型不支持此功能
		PUMPINKTIMEOUT,                 //(STATUS_ERR+23) //泵墨时间超过10秒,可能是液位传感器异常

        HIGH_VOL_TOO_LOW = 0x22,        //(STATUS_ERR+34) //高压掉电

        PM_FEATURE_UNSUPPORTED = 37,    //(STATUS_ERR+37) //we report this error when factory set none-5 and PM not acknowledge the temperature scale feature
        PM_SCALE_NOT_MATCH,             //(STATUS_ERR+38) //PM操作温度的时候，倍率和预设的倍率不一致 we report this error when PM EP0 value not same as tmp_ratio in factory data
        ILLEGAL_VOL,          // (STATUS_ERR+39) // 存在不合法电压值

        NOT_SUPPORT_PRINT= 0x28,          // (STATUS_ERR+40) // 
        EEPROM_LOCKED,          // (STATUS_ERR+41) // EEPROM USB配置 未解锁
        EXTBOARD_UART_FAIL,          // (STATUS_ERR+42) // 
        EXTBOARD_ERROR,          // (STATUS_ERR+43) // 
        NO_PAPER,       		//(STATUS_ERR+44) //缺纸告警
        SAND_LAYING = 0x2E,             //(STATUS_ERR+0x2E)//峰华卓立3D打印，铺沙过程中禁止其他动作

        FPGA_INT = 0x40,			    //(STATUS_ERR+0x40)	//FPGA产生了异常通知
        HEAD_OFFSET	,			        //(STATUS_ERR+0x41)	//喷头偏移设定错误
        NO_DMA_LLI ,			        //(STATUS_ERR+0x42)	//没有DMA链表项，即系统生成DMA链表速度不够
        DMA_LLI_LEN,			        //(STATUS_ERR+0x43) //这个表示链表需求超出程序定义
FPGA_UNDERFLOW,			        //			(STATUS_ERR+0x44)	// FPGA数据缓冲区下溢
USB_TRANSFER,			        //			(STATUS_ERR+0x45)	// USB数据传输异常，在退出闪喷的时候，缓存打印数据尚未准备好。
FPGA_PROGRAM,			        //			(STATUS_ERR+0x46)	// FPGA逻辑不对，程序非期望运行

		INTERNAL_DEBUG1 =0x50,			//(STATUS_ERR+0x50) //Debug status 1
		INTERNAL_DEBUG2 =0x51,			//(STATUS_ERR+0x51) //Debug status 1
        CADG_CTL_LINE_FAULT,//(STATUS_ERR+0x52) // 机器控制线路故障 此时 应该停止打印工作，通知人员进行线路检查。
        CADG_INIT_PLC,//(STATUS_ERR+0x53) 正在初始化plc硬件，表示plc 出现bug
        CADG_PRINT_FAULT,//(STATUS_ERR+0x54)  打印过程中，出现阻止打印的故障，需要调用ep0来查询具体错误。
        NOSUPPORT_DELAY=0x56, //(STATUS_ERR+0x56)    //头板版本不支持高低压延时的波形，请更换头板版本或者更改波形CAN_COMM_TIMEOUT = 0x60,//	(STATUS_ERR + 0x60)	// CAN通讯错误，发送超时（应该不会发生吧）
        CAN_COMM_TIMEOUT = 0x60,//	(STATUS_ERR + 0x60)	// CAN通讯错误，发送超时（应该不会发生吧）

        CAN_COMM_ERROR, //(STATUS_ERR + 0x61)	// CAN通讯错误，错误计数器超限
        CAN_BUFFER_ERROR, //(STATUS_ERR + 0x62)	// CAN通讯错误，重组缓冲区错误（应该不会发生吧）
        CAN_CONTROLLER_ERROR, //(STATUS_ERR + 0x63)	// CAN控制器错误
        WAVEFORM_INDEX, //(HBERR_ERR + 0x64)	//!< WAVEFORM下载失败, from PM to ARM
        WAVEFORM_CONFIG, // (HBERR_ERR + 0x65)	//!< WAVEFORM配置失败, from ARM to print head
        TargetTempLocked = 0x66, //(STATUS_ERR + 0x66)	// 目标温度未解锁

        CUR_ACTION_NOT_SUPPORT = 0x82,  //(STATUS_ERR+0x82)	//打印机状态不支持当前所设动作(ColorJet Clean)

        LVDS_DISCONNECT = 0x84,      //    (STATUS_ERR+0x84)   //泵墨消息超过5s没更新，很可能是LVDS连接断开
        InkSpilledAlarm = 0x94,      //    (STATUS_ERR+0x94)   //溢墨报警

        SET_VOLT_ERROR = 0xA0,          //(HBERR_ERR+0xA0)  //目前仅用于KM1024I,全半压设置检验出错
    }

	public enum CoreBoard_Initialing:short
	{
		ARM = 0,							 //正在初始化主控板
		SX2,								 //正在初始化USB通讯
		FPGA,								 //正在初始化FPGA
		DSP,								 //正在初始化运动
		HEADBOARD,							 //正在初始化头板
		HVB,				//(STATUS_INI+5) //Spectra 正在初始化高压板
        PANEL,              // 初始化pcl
	}
	public enum CoreBoard_Warning:short
	{
		UNKNOWNHEADERTYPE = 0,              //未定义的数据头标示，将被忽略
		EP0OVERUN_REQUEST_IGNORE,           //EP0数据传输未完成，又收到新的EP0命令，旧的数据传输忽略
		PUMP_CYAN,					
		PUMP_MAGENTA,				
		PUMP_YELLOW,					
		PUMP_BLACK,					
		PUMP_LIGHTCYAN,				
		PUMP_LIGHTMAGENTA,	
		TIME_PASSWORD_WRONGINPUT,               //(STATUS_WAR+8)  //输入限时密码错误，含有含有非法字符; 本次输入密码无效, 请重新输入密码.
		TIME_PASSWORD_WRONG,                    //(STATUS_WAR+9)  //不是一个合法限时密码; 本次输入密码无效, 请重新输入密码.
		TIME_PASSWORD_MANUFACTURERIDNOTMATCH,   //(STATUS_WAR+10) //限时密码和厂商不匹配; 本次输入密码无效, 请重新输入密码.
		TIME_PASSWORD_BOARDIDNOTMATCH,          //(STATUS_WAR+11) //限时密码和板子不匹配; 本次输入密码无效, 请重新输入密码.
		LIMITEDTIME_FIRST,                      //(STATUS_WAR+12) //第一次警告:再有100个小时就超出限时了, 请向厂家索取密码.
		LIMITEDTIME_SECOND,                     //(STATUS_WAR+13) //第二次警告: 再有50个小时就超出限时了,请向厂家索取密码.
		LIMITEDTIME_LAST,                       //(STATUS_WAR+14) //最后一次警告: 再有1个小时就超出限时并停止打印, 请立即向厂家索取密码.
		/// <summary>
		/// 20100609日修改,之前该消息STATUS_WAR+15以下的错误均比实际值小1。
		/// 原因:消息值从14跳至16，编码是未注意到
		/// </summary>
		OPTION_PASSWORD_WRONGINPUT = 16,        //(STATUS_WAR+16) //输入选配密码错误，含有非0~F的值
		OPTION_PASSWORD_WRONG,                  //(STATUS_WAR+17) //不是一个合法选配密码
		OPTION_PASSWORD_MANUFACTURERIDNOTMATCH, //(STATUS_WAR+18) //选配密码和厂商不匹配
		OPTION_PASSWORD_BOARDIDNOTMATCH,        //(STATUS_WAR+19) //选配密码和板子不匹配
		PUMP_PROHIBIT_SENSOR,                   //(STATUS_WAR+20) //使能禁止墨盒泵墨传感器时
		OVER_SPEED_NOT_SUPPORT,                 //(STATUS_WAR+21) //KM512LN 试图超频，但是头板不支持，取消超频

		INK_PASSWORD_WRONGINPUT,                //(STATUS_WAR+22) //输入墨水密码错误，含有非0~F的值
		INK_PASSWORD_WRONG,                     //(STATUS_WAR+23) //不是一个合法墨水密码
		INK_PASSWORD_MANUFACTURERIDNOTMATCH,    //(STATUS_WAR+24) //墨水密码和厂商不匹配
		INK_PASSWORD_BOARDIDNOTMATCH,           //(STATUS_WAR+25) //墨水密码和板子不匹配

		LIMITEDINK_FIRST,                       //(STATUS_WAR+26) //再用10升就超出墨水限制了，第一次警告
		LIMITEDINK_SECOND,                      //(STATUS_WAR+27) //再用5升就超出墨水限制了，第二次警告
		LIMITEDINK_LAST,                        //(STATUS_WAR+28) //再用1升就超出墨水限制了，最后一次警告

		INK_PASSWORD_UNKNOWN_DOT_VOLUME,        //(STATUS_WAR+29) //当前，只支持Konica和Polaris. 其他的头的墨滴大小不对，如果其他头需要INK password 功能，就会报这个warning。

		PUMP_SPOTCOLOR1,                        //(STATUS_WAR+30) //专色1正在泵墨
		PUMP_SPOTCOLOR2,                        //(STATUS_WAR+31) //专色2正在泵墨

		FPGA_LESSDATA,                          //(STATUS_WAR+32) //Print data is less than fire number or empty when trigger
		FPGA_ULTRADATA,                         //(STATUS_WAR+33) //Print data is more than fire number

		EPSONHEAD_TEMP_TOO_LOW,                 //(STATUS_WAR+34) //Head temperature too low.
		EPSONHEAD_TEMP_TOO_HIGH,                //(STATUS_WAR+35) //Head temperature too high.
		EPSONHEAD_NOT_CONNECT,                  //(STATUS_WAR+36) //Print head is not connected.
		EPSON_FPGA_ULTRADATA,                   //(STATUS_WAR+37) //core board FPGA report data too more.
		EPSON_FPGA_LESSDATA,                    //(STATUS_WAR+38) //core board FPGA report data too less. .

		UNKNOWN_NOZZLE_COUNT,                   //(STATUS_WAR+39) //当前喷头的喷嘴数未知。
        EPSON_DRIVER_OVERHEAT,                  //(STATUS_WAR+40) //串口数据频繁响应
		EPSON_XHOT_ERROR,                       //(STATUS_WAR+41) //XHOT error.EPSON Head over heat.EPSON 打印头温度过高
		INK_SUPPLY_SLOW,                        //(STATUS_WAR+42) //连续供墨超过10秒

		SCORPION_WARNING,                       //(STATUS_WAR+43) //Scorpion打印机产生了告警

		HB_FW_VERSION,                          //(STATUS_WAR+44) //升级包包含头板的FW版本太低，已不支持
		HB_FW_ID_ERROR,                         //(STATUS_WAR+45) //头板FW ID不匹配，当前头板不能升级该FW
		HB_FW_UPDATER,                          //(STATUS_WAR+46) //头板升级过程发生错误
		HBFPGA_FW_UPDATER,                      //(STATUS_WAR+47) //头板FPGA升级过程发生错误
		FUNC_FW_UPDATER,                        //(STATUS_WAR+48) //功能板升级过程发生错误

		EPSON_HEAD_FPGA_ERR,                    //(STATUS_WAR+49) //头板FPGA错误 
		EPSON_HEAD_LVDS_ERR,                    //(STATUS_WAR+50) //主板报告LVDS失锁
		EPSON_HEAD_REPORT_LVDS_ERR,             //(STATUS_WAR+51) //头板报告LVDS失锁
        
        PUMPLONG_ALARM,                         //(STATUS_WAR+52) //泵墨超时，如果作为警告的话
        TILE_IN_TILE,                           //(STATUS_WAR+53) //电眼在上一个砖的图像区内，电眼信号被丢弃
        TILE_POS_FULL,                          //(STATUS_WAR+54) //电眼缓冲区已满，当前电眼信号被丢弃
        PASSED_TILE,                            //(STATUS_WAR+55) //打印数据就绪时，本电眼所在的砖已经过去了，丢弃
		PASSWORD_WRONGINPUT,                    //(STATUS_WAR+56) //输入选配密码错误,含有非0-f的值
		PASSWORD_WRONG,                         //(STATUS_WAR+57) //不是一个合法的选配密码
		PASSWORD_MANUFACTURERIDNOTMATH,         //(STATUS_WAR+58) //选配密码和厂商不匹配
		PASSWORD_BOARDIDNOTMATH,                //(STATUS_WAR+59) //选配密码和板子不匹配
		PASSWORD_FIRST,                         //(STATUS_WAR+60) //UV时间超时第一次报警
		PASSWORD_SECOND,                        //(STATUS_WAR+61) //UV时间超时第二次报警
		PASSWORD_LAST,                          //(STATUS_WAR+62) //UV时间超时最后一次报警
        EnterBakPrintMode,                      // (STATUS_WAR+63) //主板进入后备打印状态
        NO_PAPER,							    // (STATUS_WAR+0x40) 没纸了

        DOUBLEY_CORRECTOVERLIMIT = 0x43,        //(STATUS_WAR+67) //双轴位置偏差矫正次数超限
		NO_MATERIAL,							//(STATUS_WAR+0x44)	// 没有打印材料了
        OLD_HB_BOOTLOADER = 0x45, //(STATUS_WAR+0x45)	//头板bootloader太老，导致升级过慢
        PUMP_C9 = 0x54,	            	//(STATUS_WAR+0x54)//US:Pumping C9.
        PUMP_C10 = 0x55,	            	//(STATUS_WAR+0x55)//US:Pumping C10.
        PUMP_C11 = 0x56,	            	//(STATUS_WAR+0x56)//US:Pumping C11.
        PUMP_C12 = 0x57,	            	//(STATUS_WAR+0x57)//US:Pumping C12.
        PUMP_C13 = 0x58,	            	//(STATUS_WAR+0x58)//US:Pumping C13.
        PUMP_C14 = 0x59,	            	//(STATUS_WAR+0x59)//US:Pumping C14.
        PUMP_C15 = 0x5a,	            	//(STATUS_WAR+0x5a)//US:Pumping C15.
        PUMP_C16 = 0x5b,	            	//(STATUS_WAR+0x5b)//US:Pumping C16.
	}
    public enum CoreBoard_Updating : short
	{
		UPDATING = 0,
		UPDATE_SUCCESS = 1, 	    //(STATUS_UPDATING+1) //Update Success
		DSP_BEGIN_TIMEOUT = 2,      //(STATUS_UPDATING+2)
		DSP_DATA_TIMEOUT,		    //(STATUS_UPDATING+3) //DSP update data command timeout
		DSP_END_TIMEOUT,		    //(STATUS_UPDATING+4) //DSP update end command timeout
		ILIGALFILE ,				//(STATUS_UPDATING+5) //Ilegal update file
		INTERNAL_DATA,			    //(STATUS_UPDATING+6) //Ilegal update data
		CHECKSUM,				    //(STATUS_UPDATING+7) //Update data checksum error
		EREASE,				        //(STATUS_UPDATING+8)
		FLASHOP,				    //(STATUS_UPDATING+9) //ARM flash erease or write error, 10 times retry
#if !LIYUUSB
        GZ_ILIGALFILE,				//(STATUS_UPDATING+10) //Need special GZ FW.
        INKP_ILIGALFILE,			//(STATUS_UPDATING+11) //Need special ink password FW.
        UPDATEERR_16H_ILIGALFILE,	//(STATUS_UPDATING+12) //8head HW can't update 16H FW.
        TRY_AGAIN,       	        //(STATUS_UPDATING+13)
		//20110214 安爱辉添加A系统错误,从30开始为A+等预留
        UPDATE_ERROR = 17,     //(STATUS_UPDATING+17)
        UPDATE_TIMEOUT = 18,   //(STATUS_UPDATING+18)
        PCDATA_ERR = 30,		                //(STATUS_UPDATING+30)///print manager 发送数据错误
		NO_HEADNUM,		                        //(STATUS_UPDATING+31)///print manager发送喷头号不存在
		HBDATA_ERR,		                        //(STATUS_UPDATING+32)///主板发送到头板数据错误
		PRHD_SETERR,		                    //(STATUS_UPDATING+33)///头板设置喷头失败
#else
        FX2NOEEPROM,//(STATUS_UPDATING+10)//EEPROM 不可用.
        FX2UPDATELOADER,//(STATUS_UPDATING+11) // 加载升级文件失败！
#endif
	}

    public enum EnumCmdSrc_FX2_Service : short
    {
        EEPROM_READ = 1,            //主板Service Call:读取EEPROM失败
        EEPROM_WRITE = 2, 	        //主板Service Call:写入EEPROM失败
		MAINBOARDSERIOUSERROR = 3,  //主板严重错误:出厂数据没有设置或者设置错误 
		I2CWRITEFPGA_ERROR = 4,     //主板内部错误:I2C写FPGA错误
		I2CREADFPGA_ERROR = 5,      //主板内部错误:I2C读FPGA错误  
        UART1_TXTIMEOUT = 0x06,     //主板dsp应答超时
        ENTERSLEEP = 0x07,          //设备进入了睡眠状态，这个不应该发生。
        DEBUG = 0x30
    }

    public enum EnumCmdSrc_FX2_Warning : short
    {
        CALIBRATIONVNOSET = 1,      //主板警告:校准电压没有设置, 使用缺省值
        BASEVNOSET = 2,             //主板警告:基准电压没有设置, 使用缺省值
        SPRAYPARAMETERSNOSET = 3,   //主板警告:闪喷参数没有设置, 使用缺省值
        USERSETTINGNOSET = 4,       //主板警告:用户数据(墨水类型, 喷射速度没有设置, 使用缺省值
        PUMP_STOP = 0x05,           //因为超过4秒没有收到墨瓶信号，所以关闭所有墨泵。
        SETFPGA_HDTYPE = 0x06,      //aah add20101223 set fpga 8heads or 16heads false!!!
    }
    public enum EnumCmdSrc_HEADBOARD : short
    {
        EEPROM_READ = 1,                 //头板Service Call:读取EEPROM失败
        EEPROM_WRITE = 2, 	             //头板Service Call:写入EEPROM失败
		NOHEADBOARDFEATURESDATA = 3,     //头板严重错误:头板特性数据没有写入或者格式错误
		HEADBOARDFEATURESDATANOMATCH = 4,//头板严重错误:头板特性数据与用户设置不符 
    }

    public enum EnumCmdSrc_FPGA_Abort : short
    {
        NOFINDBYHX = 1,                 //x"4001";--数据帧头找不到BYHX
        HEADERTYPE = 2,                 //x"4002";--header type 不是0，1，2
        JOBHEADERMARKED = 3,            //x"4003";--读取band,job头时置标志混乱，不是job头，job尾也不是band头
        STILLFIREAFTERPRINT = 4,        //x"4004";--band打印完后仍然来点火
        NOTENOUGHFIRE = 5,              //x"4005";--点火个数不够就收到结束打印命令
        GREATERTHANBANDHEADER = 6,      //x"4006";--实际打印数据大于band头中打印数据个数
        LESSTHANBANDHEADER = 7,         //x"4007";--实际打印数据小于band头中打印数据个数
        FIBERNOTCONNECTED = 8           //x”4008”;--光线断链。
    }
    public enum EnumCmdSrc_EEPROM_Error : byte
    { 

    }
}
