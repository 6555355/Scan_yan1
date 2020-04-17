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
	/// <summary>
	/// Summary description for UIEnumPool.
	/// </summary>
	public enum UILengthUnit
	{
		Inch = 0,
		Feet,
		Millimeter,
		Centimeter,
		Meter,
        Null
	}
	public enum UIViewMode
	{
		TopDown = 0,
		LeftRight,
		NotifyIcon,
		OldView
	}

	public enum JobListColumnHeader
	{
		Name = 0,
		Status,
		Size,
		Resolution,
		Passes,
		BiDirection,
		Copies,
		PrintedPasses,
		PrintedDate,
		PrintTime,
		Location,
	}

	public enum JobStatus
	{
		Idle,
		Waiting,
		Printing,
		Aborting,
		Paused,
		Printed,
		Error,
        GenDoudleFile,
        Creating,
		Unknown
	}


	public enum LangID
	{
		English,
		ChineseSimplified,
		ChineseTraditional,
		Korean,
		Portuguese,
		Turkish,
		Unkown
	}
	public enum PrintDirection:byte
	{
        /// <summary>
        /// ����,��ʼ��������
        /// </summary>
		Unidirection=0, 
        /// <summary>
        /// ˫��,��ʼ��������
        /// </summary>
        Bidirection,
        /// <summary>
        /// ����,��ʼ������
        /// </summary>
        UnidirectionBackWard,
        /// <summary>
        /// ˫��,��ʼ������
        /// </summary>
        BidirectionBackWard,
	}
	public enum JobOperation
	{
		Open = 0,
		Print,
		Abort,
		Pause,
		Resume,
		Delete,
		Edit,
		Reset
	}
	public enum Confirm
	{
		Delete = 0,
		Exit,
		AbortPrinter,
		ResetPrinter,
		ClearEnv,
		SaveToBoard,
		LoadFromBoard,
		GoHome,
        PrintNow,
        Print,
	}
	public enum DisplayTime
	{
		Hour,
		Minute,
		Second,
		MilliSecond
	}
	public enum UpdateDisplay
	{
		Begin,
		Success,
		Failed,
		Status,
		Percentage,
	}
	public enum UpdaterError
	{
		WrongFile,
		VersionNoMatch
	}
	public enum UIError
	{
		NoHelpFile,
		NoAcrobat,
		FileNotExist,
		CannotPrintStaus,
		SetOriginFailed,
		MeasureMediaWidthFailed,
		SaveSetToBoardFail,
		LoadSetFromBoardFail,
		SetPasswordFail,
		SaveAboutFail,
		GetHWSettingFail,
		UpdateFail,
		SetHWSettingFail,
		ClearFWFactoryData,
		NullFactoryData,
		OnlyOneProgram,
		SaveRealTimeFail,
		GetRealTimeInfoFail,
        EnumUSBFail,
        NoUSB,
        FWFactoryDataDiffer,
        SetCleanParamFail,
        ManualCleanNotSupport,
        ColorNumGroupNumError,
        ColorNumError,
        SsystemMapDismatch,//sϵͳ�˿�ӳ������������ͷ�����
        TriggerFail
	}
	public enum FileFilter
	{
		Prn,
		Env,
		Txt,
		Dat,
		Prg,
		Tvc,
        Plb
	}
	public enum UISuccess
	{
		RstoreSetting,
		ClearEnv,
		SaveSetToBoardSuccess,
		LoadSetFromBoardSuccess,
		SetPassword,
		SetHWSetting,
		UpdateSuccess,
		ClearFWFactoryData,
        SetCleanParamSuccess,
        TriggerSuccess,
        SaveAboutSuccess
	}
	public enum DispName
	{
		Pass,
		Head,
		Color,
	}

	public enum XResDivMode
	{
		HighPrecision = 1,
		HighQuality,
		PrintMode3,
		PrintMode4
	}
    public enum UIInfo
    {
        FactoryDataValue
    }
}
