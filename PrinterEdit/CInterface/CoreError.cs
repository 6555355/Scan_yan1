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
	public enum Password_UserResume:byte
	{
	};

	public enum COMCommand_Abort : byte
	{
		Command					    = 0xF1, 	//错误的命令
		Parameter					= 0xF2, 	//错误的参数
		MoveAgain					= 0xF3, 	//运行中错误 已经运动,又发运动命令 
		TxTimeOut					= 0xF4, 	//发送超时
		FormatErr					= 0xF5, 	//校验和错误
		Encoder					    = 0xF6,     //告诉上位机光栅错误
		MeasureSensor				= 0xF7,		//纸宽传感器错误
		NoPaper					    = 0xF8,		//没有介质
		PaperJamX					= 0xF9,		//X轴运动受阻
		PaperJamY					= 0xFA,		//Y轴运动受阻
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
		SX2 = 0,			//USB chip. 经过0.5秒，USB芯片还没有正常启动，USB芯片或者损坏
		FPGA0,				//FPGA chip 1. 上电后，FPGA的nSTATUS持续为高，FPGA芯片或者损坏 
		FPGA1,				//FPGA chip 2. 拉低nCONFIG之后，CONFIG_Done或者nSTATUS还是高
		FPGA2,				//FPGA chip 3. nCONFIG拉高之后, nSTATUS保持高.
		FPGA3,				//FPGA chip 4. When config FPGA, FPGA report err, and retry 10 times
		HEADTOMAINROAD,     //UPDATE Main Board Failed
		BYHX_DATA,			////板子没有经过BYHX初始化
		EEPROM_CHK=16, //BYHX_TOOL check EEPROM 没有通过。
	}
	public enum CoreBoard_Abort:byte
	{
		SX2RESET=0,					//USB chip异常重启
		INTERNAL_WRONGHEADER,		//Wrong data header
		INTERNAL_WRONGHEADERSIZE, //Wrong data header size
		INTERNAL_JOBSTARTHEADER,	//Job header不应附带额外数据
		INTERNAL_BANDDATASIZE,	//Band Header中的BAND数据数量和实际BAND数据数量不符
		INTERNAL_WRONGFORMAT,		//得到的串口数据格式不对
		INTERNAL_DMA0WORKING,		//DMA0 still working after a band complete
		INTERNAL_PRINTPOINT,		//Wrong startpoint and endpoint when print
		INTERNAL_OLIMIT,			//Band的打印起始点小于原点
		INTERNAL_OPPLIMIT,		//图像结束位置超出了打印机最远点,Image too width
		DSPINITS1,				 //运动控制第一阶段初始化没有通过
		DSPINITS2,				 //运动控制第二阶段初始化没有通过
		HEADINITS1,				 //头板第一阶段初始化没有通过
		HEADINITS2,				 //头板第二阶段初始化没有通过
		HEADTOMAINROAD,			 //主板的LVDS接收芯片没有LOCK,或线没有插
		INTERNAL_BANDDIRECTION,    //Band定义中的方向值超出定义
		DSPUPDATE_FAIL,			 //更新失败：主板写入阶段
		EEPROM_READ,				//(STATUS_FTA+17) //读取EEPROM失败	
		EEPROM_WRITE,				//(STATUS_FTA+18) //写入EEPROM失败	
		FACTORY_DATA,				//(STATUS_FTA+19) //板子没有经过出厂初始化设置
		HEADBOARD_RESET,			//(STATUS_FTA+20) //头板被重新启动
		SPECTRAHVBINITS1,			//(STATUS_FTA+21) //Spectra High Voltage Board第一阶段初始化失败
		PRINTHEAD_NOTMATCH,		    //(STATUS_FTA+22) //头板报告的喷头种类与FactoryData里面的设定不匹配， 请更换头板或重新设定硬件设置。
		MANUFACTURERID_NOTMATCH,    //(STATUS_FTA+23) //控制系统与FW的生产厂商不匹配，需更换系统或者升级FW 

	}
	public enum CoreBoard_Pause:byte
	{
		EP0OVERUN_SETUPDATA,		  //EP0命令被打断
		USB1_USB1CONNECT,			  //连接到USB1口
		UART1_TXTIMEOUT,			  //运动通讯超时
		UART2_TXTIMEOUT,			  //头板与主板通讯超时
		INTERNAL_PRINTDATA,		  //Band数据没有打印完成
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
		HVB,				//(STATUS_INI+5)  //Spectra 正在初始化高压板
	}
	public enum CoreBoard_Warning:byte
	{
		UNKNOWNHEADERTYPE = 0,			 //未定义的数据头标示，将被忽略
		EP0OVERUN_REQUEST_IGNORE,	 //EP0数据传输未完成，又收到新的EP0命令，旧的数据传输忽略
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
