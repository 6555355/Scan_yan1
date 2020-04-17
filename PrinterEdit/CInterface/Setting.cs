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
	public struct SFrequencySetting
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
	}

	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SColorBarSetting
	{
		public float			fStripeOffset;
		public float			fStripeWidth;
		[MarshalAs(UnmanagedType.I4)]
		public InkStrPosEnum	eStripePosition;
		[MarshalAs(UnmanagedType.I1)]
		public bool				bNormalStripeType;
	}

	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SBaseSetting
	{
		public float	fLeftMargin;
		public float	fPaperWidth;
		public float	fTopMargin;
		public float	fPaperHeight;
		public float    fZSpace;
		public float    fPaperThick;
		public float    fYOrigin;


		public float	fJobSpace; 
		public int		nAccDistance;

		[MarshalAs(UnmanagedType.I1)]
		public bool		bYPrintContinue;
		[MarshalAs(UnmanagedType.I1)]
		public bool		bUseMediaSensor;
		[MarshalAs(UnmanagedType.I1)]
		public bool		bIgnorePrintWhiteX;
		[MarshalAs(UnmanagedType.I1)]
		public bool		bIgnorePrintWhiteY;
		[MarshalAs(UnmanagedType.I1)]
		public bool		bPrintWhiteInk;
		[MarshalAs(UnmanagedType.I4)]
		public WhiteInkPrintMode		eWhiteInkPrintMode;
		public int         nFeatherPercent;


		public SColorBarSetting sStripeSetting;
	}
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SMoveSetting
	{
		public byte	nXMoveSpeed;				// С���ٶȣ�ȡֵ��Χ1-8
		public byte	nYMoveSpeed;				// ����ֽ�ٶȣ�ȡֵ��Χ1-8
		public ushort	usMotorEncoder;			// �����Ƶ ??????should only for Move or Print 
	}
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SCleanerSetting
	{
		public int	nCleanerPassInterval;
		public int	nSprayPassInterval;
 
		public int	nCleanerTimes;
		public int	nSprayFireInterval;//���������Ժ���Ϊ��λ
	
		public int	nCleanIntensity;
								
		[MarshalAs(UnmanagedType.I1)]
		public bool	bSprayWhileIdle;
	}	
	
#if LIYUUSB
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SRealTimeCurrentInfo
	{
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_HEAD_NUM)]
		public sbyte [] cTemperature;
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_HEAD_NUM)]
		public sbyte [] cTankTemperature;
		public sbyte cPulseWidth;
		public sbyte cUvStatus;
	};
#else
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SRealTimeCurrentInfo
	{
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_HEAD_NUM)]
		public float [] cTemperatureSet;
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_HEAD_NUM)]
		public float [] cTemperatureCur;
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_HEAD_NUM)]
		public float [] cPulseWidth;
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_HEAD_NUM)]
		public float [] cVoltage;
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_HEAD_NUM)]
		public float [] cVoltageBase;
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_HEAD_NUM)]
		public float [] cVoltageCurrent;
		[MarshalAs(UnmanagedType.I1)]
		public bool	bAutoVoltage;
	};
#endif

	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SPrinterSetting
	{
		public SCleanerSetting		sCleanSetting; ///??????Whether this parameter transfer to Board not save in Panel
		public SMoveSetting			sMoveSetting;

		public SFrequencySetting	sFrequencySetting;
		public SBaseSetting			sBaseSetting;
		public SRealTimeCurrentInfo	 sRealTimeSetting;
		public SCalibrationSetting  sCalibrationSetting;
		public int nHeadFeature2;
		public int nKillBiDirBanding;
	}

}
