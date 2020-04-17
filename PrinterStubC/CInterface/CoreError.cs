/* 
	��Ȩ���� 2006��2007��������Դ��о�Ƽ����޹�˾����������Ȩ����
	ֻ�б�������Դ��о�Ƽ����޹�˾��Ȩ�ĵ�λ���ܸ��ĳ�д�ʹ�����
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
		Command				= 0xF1,   //���������
		Parameter			= 0xF2,   //����Ĳ���
		MoveAgain			= 0xF3,   //�����д��� �Ѿ��˶�,�ַ��˶����� 
		TxTimeOut			= 0xF4,   //���ͳ�ʱ
		FormatErr			= 0xF5,   //У��ʹ���
		Encoder				= 0xF6,   //������λ����դ����
		MeasureSensor		= 0xF7,	  //ֽ����������
		NoPaper				= 0xF8,	  //û�н���
		PaperJamX			= 0xF9,	  //X���˶�����
		PaperJamY			= 0xFA,	  //Y���˶�����
		IndexNoMatch		= 0xFB,
		LimitSensor			= 0xFC,   //Limit Error
		StepEncoder         = 0xFD,

		ReadEEPROM          = 0xE1,   //Read EEPROM error 
		WriteEEPROM         = 0xE2,   //Write EEPROM error
		WriteEEPROMTwice    = 0xE3,   //Write EEPROM  2 �Σ�һ�� �����������ʱ��MB û��Ӧ���ź�
		TimeOver            = 0xE4,   //���Ƶ�ʱ���ù���
		TimeWarning         = 0xE5,   //�����Ƶ�ʱ�仹��50��Сʱ
		Lang                = 0xE6,   //���Ժ� ���ƵĲ�ƥ��
		IllegalContent      = 0xE7,   //�����EEPROM �����ݣ�EEPROM ����û�����
		IllegalPwd          = 0xE8,   //���������
        SwDogEey            = 0xE9,   //����ʹ�ü��ܹ����

		RatioEEPROM         = 0xEA,   //EEPROM���ó��ֱ�ֵ��Ĭ�ϳ��ֱ�ֵ���̫��
		RatioMeasure        = 0xEB,   //�Զ������õ��ĳ��ֱ�ֵ�뵱ǰ���ֱ�ֵ���̫��

        NotifyMeasure       = 0xEC,
        NotifyGoHome        = 0xED,
        SensorPause         = 0xEE,   //����Ĵ�������ͣ

		NoSupportCorrectRatio = 0xEF, //�ŷ���������ֱ�Ϊ1.0f,����ĵ������

        //cheney add.
        OverCurrent_Protect = 0xDF,   //�������ر��� 
        OverTemp_Protect    = 0xDE,   //�¶ȹ��߱��� 
        InternalErr_1       = 0xDD,   //�ڲ�����1
        InternalErr_2       = 0xDC,   //�ڲ�����2
        OutOfPrintRang      = 0xD1,   //��ӡԽ��
        InitERR             = 0xD2,   //dsp��ʼ��ʧ��
        DoubleYERR          = 0xD3,   //˫��λ��ƫ����󣬼�ͣ
        NoPaperCancelPrint = 0xD4,   // ȱֽ,�Զ�ȡ����ӡ
        TouchSensor = 0xD7,   // ������ǰ�ᴫ����,�˶���ֹ
    }

	public enum	Software: byte
	{
		BoardCommunication,	//USB Send Command ErrorPause, Abort, Move,	USB Board Command Error 
		NoDevice,			//Open USB Error	USB Board Open Error
		Parser,				//File Format Error
		PrintMode,			//Print Mode Parameter Error
		MediaTooSmall,      //Media too small  ��ӵĽ��ʿ�̫С
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

        MustUpdateFW, //�������� FW �İ汾
        FWIsNotDogKey, //�������� ���ܹ��� FW �İ汾
        UpdateFileWrongFormat, //����������ļ���ʽ
        DataMiss,                 //δ��ȡ���ļ�����
        ColorDeep = 20,   //0x14 ��ɫ��Ȳ�ƥ��
        MediaHeightTooSmall=21,//��ӵĽ��ʸ�̫С
        OldMainBoard = 22,//�ɵ�������Ҫ����
        MainBoardNoSupportSetKyVol = 23,//���岻֧�����þ��ɵ�ѹ����

        SwVersionQaNoTest = 24, // ������汾��δ��QA���Եİ汾

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
        /// �¸�ʽ�Ĵ����룬��ErrorCause==Software�ⶼ������ֶ�
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
        /// �ж��Ƿ�Ϊӡ����ȱֽ����,20150522 gzw
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
                // �����"������ǰ�ᴫ����",���ȡ���崫������Ϣ
		        if (errorcode.nErrorCause == (byte) ErrorCause.COM &&
                    errorcode.n16ErrorCode == (short)COMCommand_Abort.TouchSensor)
		        {
		            byte[] buf = new byte[64];
		            uint bufsize = (uint) buf.Length;
		            int ret = CoreInterface.GetEpsonEP0Cmd(0x83, buf, ref bufsize, 0, 4);
		            if (ret != 0)
		            {
                        if (buf[2] == (short)COMCommand_Abort.TouchSensor) // ��֤������Ƿ�ƥ��
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
		            // ��������ԴΪͷ���Ҵ����С��0xff,���Ҳ���ƥ��Ĵ�����Ϣʱ,����ʹ��������Դ����������Ϣ;
                    //�����ϵĴ������»�������Դ�������Ϊͷ������
		            errorcode.nErrorCause = (byte) ErrorCause.CoreBoard;
                    //msg = ErrRes.GetResString(errorcode.ToInt32());
                    msg = ErrResManager.GetResString_SQLite(errorcode.ToInt32(), PubFunc.GetVID(),0);
                }
		        // ���ݿ��ѯ����������Ϣʱ,�����ڳ�����Դ�ļ��в���
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
        /// �жϴ˴�����Ƿ�����
        /// </summary>
        /// <param name="code">�����</param>
        /// <param name="vid">����ID</param>
        /// <returns>�Ƿ�����</returns>
        public static bool IsEnable(int code)
        {
            return ErrResManager.IsEnable(code, PubFunc.GetVID(),0);
        }
	}


	public enum CoreBoard_Service:short
	{
		SX2 = 0,			        //(STATUS_SVC+0)USB chip. ����0.5�룬USBоƬ��û������������USBоƬ������
		FPGA0,				        //(STATUS_SVC+1)FPGA chip 1. �ϵ��FPGA��nSTATUS����Ϊ�ߣ�FPGAоƬ������ 
		FPGA1,				        //(STATUS_SVC+2)FPGA chip 2. ����nCONFIG֮��CONFIG_Done����nSTATUS���Ǹ�
		FPGA2,				        //(STATUS_SVC+3)FPGA chip 3. nCONFIG����֮��, nSTATUS���ָ�.
		FPGA3,				        //(STATUS_SVC+4)FPGA chip 4. When config FPGA, FPGA report err, and retry 10 times
		HEADTOMAINROAD,             //(STATUS_SVC+5)UPDATE Main Board Failed
		BYHX_DATA,			        //(STATUS_SVC+6)  //����û�о���BYHX��ʼ��
        SDRAM_CHECK = 7,            //(STATUS_SVC+7)  //SDRAM ��Checkû��ͨ��
        FLASH,				        //(STATUS_SVC+8)  //Flash. Cann't read FPGA from flash.
		SDRAM_NOMATCH,		        //(STATUS_SVC+9)  //SDRAM ̫С������֧�ֵ�ǰ�����á�//I change the code of this status 
		READBOARDID_FAIL,	        //(STATUS_SVC+10) //Read board id Error��ʵ�����ǲ�������оƬʧ��
        ARM_MOTION,       	        //(STATUS_SVC+11) //ARM Motion system initial error.ϵͳ��ARM�����ʼ������.
        HEADBOARD_INIT_FAIL,		//(STATUS_SVC+12) //HEAD board initial fail��ͷ���ʼ��ʧ�ܣ�ʵ������EPSONͷ���������оƬʧ��
        HEADBOARD_NO_FW,            //(STATUS_SVC+13) //��ͷ��û�кϷ���Firmware

        EEPROM_CHK = 16,	        //(STATUS_SVC+16) //BYHX_TOOL check EEPROM û��ͨ����

        HB_WAVEFROM_Download_fail = 0x62,   //WAVEFROM���ش���0x08020062��
        HB_WAVEFROM_Init_fail = 0x63,       //WAVEFROM��ʼ�����ô���0x080020063��
        HB_OPRATION_EEPROM_FAIL = 0x64,     //hb����eepromʧ��
        HB_FPGA_ERROR = 0x65,               //ͷ��FPGA����
        HBFW_ERROR,                         //(HBERR_SVC + 102)	//ͷ���FW�Ͱ��Ӳ�ƥ�䣬��Ҫ����ͷ��
        HEADVENDERDISMATHCH,                // ��ͷ���̲�ƥ��
    }

    public enum CoreBoard_Fatal : short
    {
        SX2RESET = 0,                       //USB chip�쳣����
        INTERNAL_WRONGHEADER,               //Wrong data header
        INTERNAL_WRONGHEADERSIZE,           //Wrong data header size
        INTERNAL_JOBSTARTHEADER,            //Job header��Ӧ������������
        INTERNAL_BANDDATASIZE,              //Band Header�е�BAND����������ʵ��BAND������������
        INTERNAL_WRONGFORMAT,               //�õ��Ĵ������ݸ�ʽ����
        INTERNAL_DMA0WORKING,               //DMA0 still working after a band complete
        INTERNAL_PRINTPOINT,                //Wrong startpoint and endpoint when print
        INTERNAL_OLIMIT,                    //Band�Ĵ�ӡ��ʼ��С��ԭ��
        INTERNAL_OPPLIMIT,                  //ͼ�����λ�ó����˴�ӡ����Զ��,Image too width
        DSPINITS1,                          //�˶����Ƶ�һ�׶γ�ʼ��û��ͨ��
        DSPINITS2,                          //�˶����Ƶڶ��׶γ�ʼ��û��ͨ��
        HEADINITS1,                         //ͷ���һ�׶γ�ʼ��û��ͨ��
        HEADINITS2,                         //ͷ��ڶ��׶γ�ʼ��û��ͨ��
        HEADTOMAINROAD,                     //�����LVDS����оƬû��LOCK,����û�в�
        INTERNAL_BANDDIRECTION,             //Band�����еķ���ֵ��������
        DSPUPDATE_FAIL,                     //����ʧ�ܣ�����д��׶�
        EEPROM_READ,                        //(STATUS_FTA+17) //��ȡEEPROMʧ��	
        EEPROM_WRITE,                       //(STATUS_FTA+18) //д��EEPROMʧ��	
        FACTORY_DATA,                       //(STATUS_FTA+19) //����û�о���������ʼ������
        HEADBOARD_RESET,                    //(STATUS_FTA+20) //ͷ�屻��������
        SPECTRAHVBINITS1,                   //(STATUS_FTA+21) //Spectra High Voltage Board��һ�׶γ�ʼ��ʧ��
        PRINTHEAD_NOTMATCH,                 //(STATUS_FTA+22) //ͷ�屨�����ͷ������FactoryData������趨��ƥ�䣬 �����ͷ��������趨Ӳ�����á�
        MANUFACTURERID_NOTMATCH,            //(STATUS_FTA+23) //����ϵͳ��FW���������̲�ƥ�䣬�����ϵͳ��������FW
        LIMITEDTIME_RUNOUT,                 //(STATUS_FTA+24) //���ش���: ������ʱʱ�䣬�������µ�����.
        INTERNAL_SW1,                       //(STATUS_FTA+25) //Internal error, blank band and y-distance == 0
        USB1_USB1CONNECT = 0x1A,            //STATUS_FTA+26)  //���ӵ�USB1��   //This error is moved to Fatal error, 2.0�汾����д���ˣ�дΪ(STATUS_ERR+26)
        UILANGUAGE_NOT_MATCH,               //(STATUS_FTA+27) //���ش���: ���ʹ�õ�����������Ȩ�޲���������������������Ի��������µ�����ѡ������, ��������ӡ��.
        INTERNAL_WRONGPINCONF,              //(STATUS_FTA+28) //PINCONFд��FPGA����
        FACTORY_COLORNUM_WRONG,             //(STATUS_FTA+29) //Factory ��д���color number ����
        HB_EEPROM_WRT_ERR,                  //(STATUS_FTA+30) //ͷ���EEPROMд��ʧ��
        HB_OVERHEAT,                        //(STATUS_FTA+31) //��ͷ���ȳ�����55�ȣ����м��ȱ�ǿ�йرա�
        SHAKEHAND_ERR,                      //(STATUS_FTA+32) //���������߰汾����û��ͨ������Э�顣
        SWVERSION_ERR,                      //(STATUS_FTA+33) //�̼�Ҫ�����������汾���汾����
        NOT_SUPPORT_2C_SHARE_1H,            //(STATUS_FTA+34) //��ǰϵͳ���ò�֧��һͷ��ɫ��
        LIMITEDINK_RUNOUT = 35,             //(STATUS_FTA+35) //����īˮ�������ƣ��������������
        SWVERSION_ERR_INK_PWD,              //(STATUS_FTA+36) //�̼�Ҫ������汾֧��īˮ���룬�汾����
        EPSON_HEAD_BAD = 37,                //(STATUS_FTA+37) //Print head error.
        EPSON_HEAD_FPGA_ERR = 38,           //(STATUS_FTA+38) //head board FPGA error. For KM1024, write FPGA parameter error
        SECURITY_CHIP_ERR = 39,             //(STATUS_FTA+39) //Write board id error. //only take place with write_board_tools_updater.
        T_JET_PASSNUM_ERR,                  //(STATUS_FTA+40) //aahadd for t-jet
        CALIBRATION_DATA,                   //(STATUS_FTA+41) //������ʼ�����ó���
        HEADBOARD_CONFIG,                   //(STATUS_FTA+42) //ͷ�����ò���ȷ
        NO_TEMPERATURE_SENSOR,              //(STATUS_FTA+43) //��ⲻ���¶ȴ�����
        INK_SPILL,                          //(STATUS_FTA+44) //��ѹī����ī
        INK_SUPPLY_LOW,                     //(STATUS_FTA+45) //��īī��ȱī		
        FUNC_BOARD_STARTUP,                 //(STATUS_FTA+46) //scorpion printer function board startup
        PRINT_DATA_ERROR,                   //(STATUS_FTA+47) //��ӡ���ݴ�������ʧ
        FACTORY_DATA_ERROR,                 //(STATUS_FTA+48) //FACTORY_DATA���ݴ���
        PANEL_EX_LOST,                      //(STATUS_FTA+49) //�������ư�ͨ��ʧ��
        PANEL_EX_BUSY,                      //(STATUS_FTA+50) //�������ư�æ
        HB_OVERCOOL = 56,                   //(STATUS_FTA+56) //��ͷ�¶ȹ��ͣ��쳣�������ȿ��Ʊ�ǿ�йر�
        OVERCHANGE,                         //(STATUS_FTA+57) //��ͷ�¶ȱ仯���ھ��ң��쳣�������ȿ��Ʊ�ǿ�йر�
        UV_RUNOUT = 59,                     //(STATUS_FTA+59) //UV��ʱ���þ�
		EMERGENCY_STOP = 0x3e,				//(STATUS_FTA+0x3E)	// scorpion���ܰ屨��ͣ
        EXT_INIT_ERR=0x44,                       //(STATUS_FTA+0x44)	// �ⲿ�豸��ʼ��δ���
        DOUBLEY_ERRPOS = 0x45,              //(STATUS_FTA+0x45) //˫Y�ᣬ����λ��ƫ�����
        TEMP_RATIO_UNSUPPORT = 0x60,        //(STATUS_FTA+60) //�������¶ȱ��ʵ���ͷ�岻֧�� we report this error when factory set none-5 but headboard not support temperature scale
        WAVEFORM_INVALID = 0x65,            //(HBERR_FTA + 101)	//û��waveform��Ч��ʶ
        FULL_HALF_VOLT_ERROR,               //(HBERR_FTA + 102)	//ȫ��ѹ����쳣(��ѹ����ȫѹ) Ŀǰ��KM1024I�иü��
        EEPROM_PARAM_UNVALID,               //(HBERR_FTA + 103)	//eeprom�����Ƿ�����Ҫ��������
    }

    public enum CoreBoard_Err:short
	{
		EP0OVERUN_SETUPDATA,		    //EP0������
		//USB1_USB1CONNECT,			    //���ӵ�USB1�� //This error is moved to Fatal error
		UART2_TXTIMEOUT = 2,            //�˶�ͨѶ��ʱ
		UART1_TXTIMEOUT = 3,			//ͷ��������ͨѶ��ʱ
		INTERNAL_PRINTDATA,             //Band����û�д�ӡ���
		FPGA_LESSDATA,                  //Print data is less than fire number or empty when trigger
		FPGA_ULTRADATA,                 //Print data is more than fire number
		FPGA_WRONGSTATUS,               //(STATUS_ERR+7)
		UV_CTR_WRONGSTATUS,             //(STATUS_ERR+8)  //Internal Status
		FPGA_WRONGCOMMAND,              //(STATUS_ERR+9)  //FPGA �յ����������
		MOTION_SPEED_BAD,               //(STATUS_ERR+10) //�˶��ٶ�̫��򲻾��ȣ����ݻ��ڳ��飬ȴ�е������
		INTERNAL_MOVING_TIMEOUT,		//(STATUS_ERR+11) //�˶������30���û���յ��˶��������
		INTERNAL_WRONGAUTOCLEAN,		//(STATUS_ERR+12) //�����AUTOCLEAN�����Ӧ�÷���
		INTERNAL_WRONGBANDINFO,			//(STATUS_ERR+13) //�����AUTOCLEAN�����Ӧ�÷���

		CURVE_EXCEEDSIZE,			    //(STATUS_ERR+14) //���͵��¶ȵ�ѹ�������ݴ�С�������Χ��
		CURVE_PCSND_DATA,			    //(STATUS_ERR+15) //printmanager���͵����ݴ���
		CURVE_SAVE_ROM,			        //(STATUS_ERR+16) //�������ݴ洢��eepromʧ�ܣ�
		CURVE_NO_SPACE,			        //(STATUS_ERR+17) //����eepromû���㹻�ռ�洢�½��յ����ݣ�
		CURVE_UNKN_HDNUM,			    //(STATUS_ERR+18) //printmanager���͵���ͷ�Ų����ڣ�
		CURVE_MAP,			            //(STATUS_ERR+19) //����eeprom��������ӳ�䵽�ڴ����
		CURVE_READ_ROM,			        //(STATUS_ERR+20) //�����ȡeepromʧ��
		CURVE_ROM_CHKSUM,			    //(STATUS_ERR+21) //��������checksumУ�����
		HB_NOSUPPORT_FUNC,			    //(STATUS_ERR+22) //ͷ�����Ͳ�֧�ִ˹���
		PUMPINKTIMEOUT,                 //(STATUS_ERR+23) //��īʱ�䳬��10��,������Һλ�������쳣

        HIGH_VOL_TOO_LOW = 0x22,        //(STATUS_ERR+34) //��ѹ����

        PM_FEATURE_UNSUPPORTED = 37,    //(STATUS_ERR+37) //we report this error when factory set none-5 and PM not acknowledge the temperature scale feature
        PM_SCALE_NOT_MATCH,             //(STATUS_ERR+38) //PM�����¶ȵ�ʱ�򣬱��ʺ�Ԥ��ı��ʲ�һ�� we report this error when PM EP0 value not same as tmp_ratio in factory data
        ILLEGAL_VOL,          // (STATUS_ERR+39) // ���ڲ��Ϸ���ѹֵ

        NOT_SUPPORT_PRINT= 0x28,          // (STATUS_ERR+40) // 
        EEPROM_LOCKED,          // (STATUS_ERR+41) // EEPROM USB���� δ����
        EXTBOARD_UART_FAIL,          // (STATUS_ERR+42) // 
        EXTBOARD_ERROR,          // (STATUS_ERR+43) // 
        NO_PAPER,       		//(STATUS_ERR+44) //ȱֽ�澯
        SAND_LAYING = 0x2E,             //(STATUS_ERR+0x2E)//�廪׿��3D��ӡ����ɳ�����н�ֹ��������

        FPGA_INT = 0x40,			    //(STATUS_ERR+0x40)	//FPGA�������쳣֪ͨ
        HEAD_OFFSET	,			        //(STATUS_ERR+0x41)	//��ͷƫ���趨����
        NO_DMA_LLI ,			        //(STATUS_ERR+0x42)	//û��DMA�������ϵͳ����DMA�����ٶȲ���
        DMA_LLI_LEN,			        //(STATUS_ERR+0x43) //�����ʾ�������󳬳�������
FPGA_UNDERFLOW,			        //			(STATUS_ERR+0x44)	// FPGA���ݻ���������
USB_TRANSFER,			        //			(STATUS_ERR+0x45)	// USB���ݴ����쳣�����˳������ʱ�򣬻����ӡ������δ׼���á�
FPGA_PROGRAM,			        //			(STATUS_ERR+0x46)	// FPGA�߼����ԣ��������������

		INTERNAL_DEBUG1 =0x50,			//(STATUS_ERR+0x50) //Debug status 1
		INTERNAL_DEBUG2 =0x51,			//(STATUS_ERR+0x51) //Debug status 1
        CADG_CTL_LINE_FAULT,//(STATUS_ERR+0x52) // ����������·���� ��ʱ Ӧ��ֹͣ��ӡ������֪ͨ��Ա������·��顣
        CADG_INIT_PLC,//(STATUS_ERR+0x53) ���ڳ�ʼ��plcӲ������ʾplc ����bug
        CADG_PRINT_FAULT,//(STATUS_ERR+0x54)  ��ӡ�����У�������ֹ��ӡ�Ĺ��ϣ���Ҫ����ep0����ѯ�������
        NOSUPPORT_DELAY=0x56, //(STATUS_ERR+0x56)    //ͷ��汾��֧�ָߵ�ѹ��ʱ�Ĳ��Σ������ͷ��汾���߸��Ĳ���CAN_COMM_TIMEOUT = 0x60,//	(STATUS_ERR + 0x60)	// CANͨѶ���󣬷��ͳ�ʱ��Ӧ�ò��ᷢ���ɣ�
        CAN_COMM_TIMEOUT = 0x60,//	(STATUS_ERR + 0x60)	// CANͨѶ���󣬷��ͳ�ʱ��Ӧ�ò��ᷢ���ɣ�

        CAN_COMM_ERROR, //(STATUS_ERR + 0x61)	// CANͨѶ���󣬴������������
        CAN_BUFFER_ERROR, //(STATUS_ERR + 0x62)	// CANͨѶ�������黺��������Ӧ�ò��ᷢ���ɣ�
        CAN_CONTROLLER_ERROR, //(STATUS_ERR + 0x63)	// CAN����������
        WAVEFORM_INDEX, //(HBERR_ERR + 0x64)	//!< WAVEFORM����ʧ��, from PM to ARM
        WAVEFORM_CONFIG, // (HBERR_ERR + 0x65)	//!< WAVEFORM����ʧ��, from ARM to print head
        TargetTempLocked = 0x66, //(STATUS_ERR + 0x66)	// Ŀ���¶�δ����

        CUR_ACTION_NOT_SUPPORT = 0x82,  //(STATUS_ERR+0x82)	//��ӡ��״̬��֧�ֵ�ǰ���趯��(ColorJet Clean)

        LVDS_DISCONNECT = 0x84,      //    (STATUS_ERR+0x84)   //��ī��Ϣ����5sû���£��ܿ�����LVDS���ӶϿ�
        InkSpilledAlarm = 0x94,      //    (STATUS_ERR+0x94)   //��ī����

        SET_VOLT_ERROR = 0xA0,          //(HBERR_ERR+0xA0)  //Ŀǰ������KM1024I,ȫ��ѹ���ü������
    }

	public enum CoreBoard_Initialing:short
	{
		ARM = 0,							 //���ڳ�ʼ�����ذ�
		SX2,								 //���ڳ�ʼ��USBͨѶ
		FPGA,								 //���ڳ�ʼ��FPGA
		DSP,								 //���ڳ�ʼ���˶�
		HEADBOARD,							 //���ڳ�ʼ��ͷ��
		HVB,				//(STATUS_INI+5) //Spectra ���ڳ�ʼ����ѹ��
        PANEL,              // ��ʼ��pcl
	}
	public enum CoreBoard_Warning:short
	{
		UNKNOWNHEADERTYPE = 0,              //δ���������ͷ��ʾ����������
		EP0OVERUN_REQUEST_IGNORE,           //EP0���ݴ���δ��ɣ����յ��µ�EP0����ɵ����ݴ������
		PUMP_CYAN,					
		PUMP_MAGENTA,				
		PUMP_YELLOW,					
		PUMP_BLACK,					
		PUMP_LIGHTCYAN,				
		PUMP_LIGHTMAGENTA,	
		TIME_PASSWORD_WRONGINPUT,               //(STATUS_WAR+8)  //������ʱ������󣬺��к��зǷ��ַ�; ��������������Ч, ��������������.
		TIME_PASSWORD_WRONG,                    //(STATUS_WAR+9)  //����һ���Ϸ���ʱ����; ��������������Ч, ��������������.
		TIME_PASSWORD_MANUFACTURERIDNOTMATCH,   //(STATUS_WAR+10) //��ʱ����ͳ��̲�ƥ��; ��������������Ч, ��������������.
		TIME_PASSWORD_BOARDIDNOTMATCH,          //(STATUS_WAR+11) //��ʱ����Ͱ��Ӳ�ƥ��; ��������������Ч, ��������������.
		LIMITEDTIME_FIRST,                      //(STATUS_WAR+12) //��һ�ξ���:����100��Сʱ�ͳ�����ʱ��, ���򳧼���ȡ����.
		LIMITEDTIME_SECOND,                     //(STATUS_WAR+13) //�ڶ��ξ���: ����50��Сʱ�ͳ�����ʱ��,���򳧼���ȡ����.
		LIMITEDTIME_LAST,                       //(STATUS_WAR+14) //���һ�ξ���: ����1��Сʱ�ͳ�����ʱ��ֹͣ��ӡ, �������򳧼���ȡ����.
		/// <summary>
		/// 20100609���޸�,֮ǰ����ϢSTATUS_WAR+15���µĴ������ʵ��ֵС1��
		/// ԭ��:��Ϣֵ��14����16��������δע�⵽
		/// </summary>
		OPTION_PASSWORD_WRONGINPUT = 16,        //(STATUS_WAR+16) //����ѡ��������󣬺��з�0~F��ֵ
		OPTION_PASSWORD_WRONG,                  //(STATUS_WAR+17) //����һ���Ϸ�ѡ������
		OPTION_PASSWORD_MANUFACTURERIDNOTMATCH, //(STATUS_WAR+18) //ѡ������ͳ��̲�ƥ��
		OPTION_PASSWORD_BOARDIDNOTMATCH,        //(STATUS_WAR+19) //ѡ������Ͱ��Ӳ�ƥ��
		PUMP_PROHIBIT_SENSOR,                   //(STATUS_WAR+20) //ʹ�ܽ�ֹī�б�ī������ʱ
		OVER_SPEED_NOT_SUPPORT,                 //(STATUS_WAR+21) //KM512LN ��ͼ��Ƶ������ͷ�岻֧�֣�ȡ����Ƶ

		INK_PASSWORD_WRONGINPUT,                //(STATUS_WAR+22) //����īˮ������󣬺��з�0~F��ֵ
		INK_PASSWORD_WRONG,                     //(STATUS_WAR+23) //����һ���Ϸ�īˮ����
		INK_PASSWORD_MANUFACTURERIDNOTMATCH,    //(STATUS_WAR+24) //īˮ����ͳ��̲�ƥ��
		INK_PASSWORD_BOARDIDNOTMATCH,           //(STATUS_WAR+25) //īˮ����Ͱ��Ӳ�ƥ��

		LIMITEDINK_FIRST,                       //(STATUS_WAR+26) //����10���ͳ���īˮ�����ˣ���һ�ξ���
		LIMITEDINK_SECOND,                      //(STATUS_WAR+27) //����5���ͳ���īˮ�����ˣ��ڶ��ξ���
		LIMITEDINK_LAST,                        //(STATUS_WAR+28) //����1���ͳ���īˮ�����ˣ����һ�ξ���

		INK_PASSWORD_UNKNOWN_DOT_VOLUME,        //(STATUS_WAR+29) //��ǰ��ֻ֧��Konica��Polaris. ������ͷ��ī�δ�С���ԣ��������ͷ��ҪINK password ���ܣ��ͻᱨ���warning��

		PUMP_SPOTCOLOR1,                        //(STATUS_WAR+30) //רɫ1���ڱ�ī
		PUMP_SPOTCOLOR2,                        //(STATUS_WAR+31) //רɫ2���ڱ�ī

		FPGA_LESSDATA,                          //(STATUS_WAR+32) //Print data is less than fire number or empty when trigger
		FPGA_ULTRADATA,                         //(STATUS_WAR+33) //Print data is more than fire number

		EPSONHEAD_TEMP_TOO_LOW,                 //(STATUS_WAR+34) //Head temperature too low.
		EPSONHEAD_TEMP_TOO_HIGH,                //(STATUS_WAR+35) //Head temperature too high.
		EPSONHEAD_NOT_CONNECT,                  //(STATUS_WAR+36) //Print head is not connected.
		EPSON_FPGA_ULTRADATA,                   //(STATUS_WAR+37) //core board FPGA report data too more.
		EPSON_FPGA_LESSDATA,                    //(STATUS_WAR+38) //core board FPGA report data too less. .

		UNKNOWN_NOZZLE_COUNT,                   //(STATUS_WAR+39) //��ǰ��ͷ��������δ֪��
        EPSON_DRIVER_OVERHEAT,                  //(STATUS_WAR+40) //��������Ƶ����Ӧ
		EPSON_XHOT_ERROR,                       //(STATUS_WAR+41) //XHOT error.EPSON Head over heat.EPSON ��ӡͷ�¶ȹ���
		INK_SUPPLY_SLOW,                        //(STATUS_WAR+42) //������ī����10��

		SCORPION_WARNING,                       //(STATUS_WAR+43) //Scorpion��ӡ�������˸澯

		HB_FW_VERSION,                          //(STATUS_WAR+44) //����������ͷ���FW�汾̫�ͣ��Ѳ�֧��
		HB_FW_ID_ERROR,                         //(STATUS_WAR+45) //ͷ��FW ID��ƥ�䣬��ǰͷ�岻��������FW
		HB_FW_UPDATER,                          //(STATUS_WAR+46) //ͷ���������̷�������
		HBFPGA_FW_UPDATER,                      //(STATUS_WAR+47) //ͷ��FPGA�������̷�������
		FUNC_FW_UPDATER,                        //(STATUS_WAR+48) //���ܰ��������̷�������

		EPSON_HEAD_FPGA_ERR,                    //(STATUS_WAR+49) //ͷ��FPGA���� 
		EPSON_HEAD_LVDS_ERR,                    //(STATUS_WAR+50) //���屨��LVDSʧ��
		EPSON_HEAD_REPORT_LVDS_ERR,             //(STATUS_WAR+51) //ͷ�屨��LVDSʧ��
        
        PUMPLONG_ALARM,                         //(STATUS_WAR+52) //��ī��ʱ�������Ϊ����Ļ�
        TILE_IN_TILE,                           //(STATUS_WAR+53) //��������һ��ש��ͼ�����ڣ������źű�����
        TILE_POS_FULL,                          //(STATUS_WAR+54) //���ۻ�������������ǰ�����źű�����
        PASSED_TILE,                            //(STATUS_WAR+55) //��ӡ���ݾ���ʱ�����������ڵ�ש�Ѿ���ȥ�ˣ�����
		PASSWORD_WRONGINPUT,                    //(STATUS_WAR+56) //����ѡ���������,���з�0-f��ֵ
		PASSWORD_WRONG,                         //(STATUS_WAR+57) //����һ���Ϸ���ѡ������
		PASSWORD_MANUFACTURERIDNOTMATH,         //(STATUS_WAR+58) //ѡ������ͳ��̲�ƥ��
		PASSWORD_BOARDIDNOTMATH,                //(STATUS_WAR+59) //ѡ������Ͱ��Ӳ�ƥ��
		PASSWORD_FIRST,                         //(STATUS_WAR+60) //UVʱ�䳬ʱ��һ�α���
		PASSWORD_SECOND,                        //(STATUS_WAR+61) //UVʱ�䳬ʱ�ڶ��α���
		PASSWORD_LAST,                          //(STATUS_WAR+62) //UVʱ�䳬ʱ���һ�α���
        EnterBakPrintMode,                      // (STATUS_WAR+63) //�������󱸴�ӡ״̬
        NO_PAPER,							    // (STATUS_WAR+0x40) ûֽ��

        DOUBLEY_CORRECTOVERLIMIT = 0x43,        //(STATUS_WAR+67) //˫��λ��ƫ�������������
		NO_MATERIAL,							//(STATUS_WAR+0x44)	// û�д�ӡ������
        OLD_HB_BOOTLOADER = 0x45, //(STATUS_WAR+0x45)	//ͷ��bootloader̫�ϣ�������������
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
		//20110214 ���������Aϵͳ����,��30��ʼΪA+��Ԥ��
        UPDATE_ERROR = 17,     //(STATUS_UPDATING+17)
        UPDATE_TIMEOUT = 18,   //(STATUS_UPDATING+18)
        PCDATA_ERR = 30,		                //(STATUS_UPDATING+30)///print manager �������ݴ���
		NO_HEADNUM,		                        //(STATUS_UPDATING+31)///print manager������ͷ�Ų�����
		HBDATA_ERR,		                        //(STATUS_UPDATING+32)///���巢�͵�ͷ�����ݴ���
		PRHD_SETERR,		                    //(STATUS_UPDATING+33)///ͷ��������ͷʧ��
#else
        FX2NOEEPROM,//(STATUS_UPDATING+10)//EEPROM ������.
        FX2UPDATELOADER,//(STATUS_UPDATING+11) // ���������ļ�ʧ�ܣ�
#endif
	}

    public enum EnumCmdSrc_FX2_Service : short
    {
        EEPROM_READ = 1,            //����Service Call:��ȡEEPROMʧ��
        EEPROM_WRITE = 2, 	        //����Service Call:д��EEPROMʧ��
		MAINBOARDSERIOUSERROR = 3,  //�������ش���:��������û�����û������ô��� 
		I2CWRITEFPGA_ERROR = 4,     //�����ڲ�����:I2CдFPGA����
		I2CREADFPGA_ERROR = 5,      //�����ڲ�����:I2C��FPGA����  
        UART1_TXTIMEOUT = 0x06,     //����dspӦ��ʱ
        ENTERSLEEP = 0x07,          //�豸������˯��״̬�������Ӧ�÷�����
        DEBUG = 0x30
    }

    public enum EnumCmdSrc_FX2_Warning : short
    {
        CALIBRATIONVNOSET = 1,      //���徯��:У׼��ѹû������, ʹ��ȱʡֵ
        BASEVNOSET = 2,             //���徯��:��׼��ѹû������, ʹ��ȱʡֵ
        SPRAYPARAMETERSNOSET = 3,   //���徯��:�������û������, ʹ��ȱʡֵ
        USERSETTINGNOSET = 4,       //���徯��:�û�����(īˮ����, �����ٶ�û������, ʹ��ȱʡֵ
        PUMP_STOP = 0x05,           //��Ϊ����4��û���յ�īƿ�źţ����Թر�����ī�á�
        SETFPGA_HDTYPE = 0x06,      //aah add20101223 set fpga 8heads or 16heads false!!!
    }
    public enum EnumCmdSrc_HEADBOARD : short
    {
        EEPROM_READ = 1,                 //ͷ��Service Call:��ȡEEPROMʧ��
        EEPROM_WRITE = 2, 	             //ͷ��Service Call:д��EEPROMʧ��
		NOHEADBOARDFEATURESDATA = 3,     //ͷ�����ش���:ͷ����������û��д����߸�ʽ����
		HEADBOARDFEATURESDATANOMATCH = 4,//ͷ�����ش���:ͷ�������������û����ò��� 
    }

    public enum EnumCmdSrc_FPGA_Abort : short
    {
        NOFINDBYHX = 1,                 //x"4001";--����֡ͷ�Ҳ���BYHX
        HEADERTYPE = 2,                 //x"4002";--header type ����0��1��2
        JOBHEADERMARKED = 3,            //x"4003";--��ȡband,jobͷʱ�ñ�־���ң�����jobͷ��jobβҲ����bandͷ
        STILLFIREAFTERPRINT = 4,        //x"4004";--band��ӡ�����Ȼ�����
        NOTENOUGHFIRE = 5,              //x"4005";--�������������յ�������ӡ����
        GREATERTHANBANDHEADER = 6,      //x"4006";--ʵ�ʴ�ӡ���ݴ���bandͷ�д�ӡ���ݸ���
        LESSTHANBANDHEADER = 7,         //x"4007";--ʵ�ʴ�ӡ����С��bandͷ�д�ӡ���ݸ���
        FIBERNOTCONNECTED = 8           //x��4008��;--���߶�����
    }
    public enum EnumCmdSrc_EEPROM_Error : byte
    { 

    }
}
