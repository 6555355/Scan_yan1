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
	public enum Password_UserResume:byte
	{
	};

	public enum COMCommand_Abort : byte
	{
		Command					    = 0xF1, 	//���������
		Parameter					= 0xF2, 	//����Ĳ���
		MoveAgain					= 0xF3, 	//�����д��� �Ѿ��˶�,�ַ��˶����� 
		TxTimeOut					= 0xF4, 	//���ͳ�ʱ
		FormatErr					= 0xF5, 	//У��ʹ���
		Encoder					    = 0xF6,     //������λ����դ����
		MeasureSensor				= 0xF7,		//ֽ����������
		NoPaper					    = 0xF8,		//û�н���
		PaperJamX					= 0xF9,		//X���˶�����
		PaperJamY					= 0xFA,		//Y���˶�����
		IndexNoMatch				= 0xFB,
		LimitSensor				    = 0xFC     //Limit Error
	}

	public enum	Software: byte
	{
		BoardCommunication,	//USB Send Command ErrorPause, Abort, Move,	USB Board Command Error 
		NoDevice,			//Open USB Error	USB Board Open Error
		Parser,				//File Format Error
		PrintMode,			//Print Mode Parameter Error
		MediaTooSmall,		//Media too small
		Shakhand,			//Security
		MismatchID,			//Cannot found match ID
		VersionNoMatch,		//Min Support version
		Language,
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
		Unknown
	}
	public enum ErrorAction : byte
	{
		Service		= 0x80,
		Abort		= 0x40,
		Pause		= 0x20,
		UserResume  = 0x10,
		Init		= 0x08,
		Warning		= 0x04,
		Updating    = 0x02
	}

	public struct SErrorCode
	{
		public byte nErrorCode;
		public byte nErrorSub;
		public byte nErrorCause;
		public byte nErrorAction;

		public SErrorCode(int message)
		{
			nErrorCode = (byte)message;
			message = message >> 8;
			nErrorSub = (byte)message;
			message = message >> 8;
			nErrorCause = (byte)message;
			message = message >> 8;
			nErrorAction = (byte)message;
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

	}


	public enum CoreBoard_Service:byte
	{
		SX2 = 0,			//USB chip. ����0.5�룬USBоƬ��û������������USBоƬ������
		FPGA0,				//FPGA chip 1. �ϵ��FPGA��nSTATUS����Ϊ�ߣ�FPGAоƬ������ 
		FPGA1,				//FPGA chip 2. ����nCONFIG֮��CONFIG_Done����nSTATUS���Ǹ�
		FPGA2,				//FPGA chip 3. nCONFIG����֮��, nSTATUS���ָ�.
		FPGA3,				//FPGA chip 4. When config FPGA, FPGA report err, and retry 10 times
		HEADTOMAINROAD,     //UPDATE Main Board Failed
		BYHX_DATA,			////����û�о���BYHX��ʼ��
		EEPROM_CHK=16, //BYHX_TOOL check EEPROM û��ͨ����
	}
	public enum CoreBoard_Abort:byte
	{
		SX2RESET=0,					//USB chip�쳣����
		INTERNAL_WRONGHEADER,		//Wrong data header
		INTERNAL_WRONGHEADERSIZE, //Wrong data header size
		INTERNAL_JOBSTARTHEADER,	//Job header��Ӧ������������
		INTERNAL_BANDDATASIZE,	//Band Header�е�BAND����������ʵ��BAND������������
		INTERNAL_WRONGFORMAT,		//�õ��Ĵ������ݸ�ʽ����
		INTERNAL_DMA0WORKING,		//DMA0 still working after a band complete
		INTERNAL_PRINTPOINT,		//Wrong startpoint and endpoint when print
		INTERNAL_OLIMIT,			//Band�Ĵ�ӡ��ʼ��С��ԭ��
		INTERNAL_OPPLIMIT,		//ͼ�����λ�ó����˴�ӡ����Զ��,Image too width
		DSPINITS1,				 //�˶����Ƶ�һ�׶γ�ʼ��û��ͨ��
		DSPINITS2,				 //�˶����Ƶڶ��׶γ�ʼ��û��ͨ��
		HEADINITS1,				 //ͷ���һ�׶γ�ʼ��û��ͨ��
		HEADINITS2,				 //ͷ��ڶ��׶γ�ʼ��û��ͨ��
		HEADTOMAINROAD,			 //�����LVDS����оƬû��LOCK,����û�в�
		INTERNAL_BANDDIRECTION,    //Band�����еķ���ֵ��������
		DSPUPDATE_FAIL,			 //����ʧ�ܣ�����д��׶�
		EEPROM_READ,				//(STATUS_FTA+17) //��ȡEEPROMʧ��	
		EEPROM_WRITE,				//(STATUS_FTA+18) //д��EEPROMʧ��	
		FACTORY_DATA,				//(STATUS_FTA+19) //����û�о���������ʼ������
		HEADBOARD_RESET,			//(STATUS_FTA+20) //ͷ�屻��������
		SPECTRAHVBINITS1,			//(STATUS_FTA+21) //Spectra High Voltage Board��һ�׶γ�ʼ��ʧ��
		PRINTHEAD_NOTMATCH,		    //(STATUS_FTA+22) //ͷ�屨�����ͷ������FactoryData������趨��ƥ�䣬 �����ͷ��������趨Ӳ�����á�
		MANUFACTURERID_NOTMATCH,    //(STATUS_FTA+23) //����ϵͳ��FW���������̲�ƥ�䣬�����ϵͳ��������FW 

	}
	public enum CoreBoard_Pause:byte
	{
		EP0OVERUN_SETUPDATA,		  //EP0������
		USB1_USB1CONNECT,			  //���ӵ�USB1��
		UART1_TXTIMEOUT,			  //�˶�ͨѶ��ʱ
		UART2_TXTIMEOUT,			  //ͷ��������ͨѶ��ʱ
		INTERNAL_PRINTDATA,		  //Band����û�д�ӡ���
		FPGA_LESSDATA,			 //Print data is less than fire number or empty when trigger
		FPGA_ULTRADATA,			 //Print data is more than fire number
		FPGA_WRONGSTATUS,			//(STATUS_ERR+7)
	}

	public enum CoreBoard_Initialing:byte
	{
		ARM = 0,				
		SX2,			
		FPGA,			
		DSP,			
		HEADBOARD,	
		HVB,				//(STATUS_INI+5)  //Spectra ���ڳ�ʼ����ѹ��
	}
	public enum CoreBoard_Warning:byte
	{
		UNKNOWNHEADERTYPE = 0,			 //δ���������ͷ��ʾ����������
		EP0OVERUN_REQUEST_IGNORE,	 //EP0���ݴ���δ��ɣ����յ��µ�EP0����ɵ����ݴ������
		PUMP_CYAN,					
		PUMP_MAGENTA,				
		PUMP_YELLOW,					
		PUMP_BLACK,					
		PUMP_LIGHTCYAN,				
		PUMP_LIGHTMAGENTA,			
	}
	public enum CoreBoard_Updating:byte
	{
		UPDATING = 0,
		UPDATE_SUCCESS 				= 1, 	//Update Success
		DSP_BEGIN_TIMEOUT			= 2,
		DSP_DATA_TIMEOUT,		//DSP update data command timeout
		DSP_END_TIMEOUT,			//DSP update end command timeout
		ILIGALFILE ,				//Ilegal update file
		INTERNAL_DATA,			//Ilegal update data
		CHECKSUM,				//Update data checksum error
		FLASHOP	,			 	//ARM flash erease or write error, 10 times retry
	}

}
