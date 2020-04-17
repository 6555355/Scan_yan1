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
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;

//using DemoTreeView;
namespace BYHXPrinterManager
{
	public struct CONSTANT
	{
		const string m_Dot = ".";
		public static CultureInfo CreateNativeCulture()
		{
			CultureInfo cultInfo = new CultureInfo("en-US");
			cultInfo.NumberFormat.NumberDecimalSeparator = m_Dot;
			return cultInfo;
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SPrtImageInfo
	{
		public int		nImageType;
		public int		nImageWidth;
		public int		nImageHeight;
		public int		nImageColorNum;
		public int		nImageColorDeep;
		public int		nImageResolutionX;
		public int		nImageResolutionY;
		public int		nImageDataSize;
		public int   	nImageData;
	}
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SPrtImagePreview
	{
		public int		nImageType;
		public int		nImageWidth;
		public int		nImageHeight;
		public int		nImageColorNum;
		public int		nImageColorDeep;
		public int		nImageResolutionX;
		public int		nImageResolutionY;
		public int		nImageDataSize;
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_PREVIEW_BUFFER_SIZE)]
		public byte[]	nImageData;
	}

	
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SPrtFileInfo
	{
		public SFrequencySetting	sFreSetting;
		public SPrtImageInfo		sImageInfo;
		public int					nVersion;
		[MarshalAs(UnmanagedType.ByValTStr,	SizeConst=CoreConst.MAX_NAME)]
		public string				sRipSource;
		[MarshalAs(UnmanagedType.ByValTStr,	SizeConst=CoreConst.MAX_NAME)]
		public string				sJobName;

	}
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SUSBDeviceInfo
	{
		public ushort  nProductID;
		public ushort  nVendorID;

		[MarshalAs(UnmanagedType.ByValTStr,SizeConst=CoreConst.MAX_USBINFO_STRINGLEN*2)]
		public string sProduct;
		[MarshalAs(UnmanagedType.ByValTStr,	SizeConst=CoreConst.MAX_USBINFO_STRINGLEN*2)]
		public string sManufacturer;
		[MarshalAs(UnmanagedType.ByValTStr,	SizeConst=CoreConst.MAX_USBINFO_STRINGLEN*2)]
		public string sSerialNumber;
	}

	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SSeviceSetting
	{
		public uint		unColorMask;
		public uint		unPassMask;
		public int		nCalibrationHeadIndex;
	}
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SBoardInfo
	{
		public ushort  m_nResultFlag;
		public ushort  m_nNull;
		public uint    m_nBoradVersion;
		public uint    m_nMTBoradVersion;
		public ushort	m_nBoardManufatureID;
		public ushort	m_nBoardProductID;
		public uint	m_nBoardSerialNum;
		[MarshalAs(UnmanagedType.ByValTStr,	SizeConst=(CoreConst.BOARD_DATE_LEN))]
		public string	sProduceDateTime;
		[MarshalAs(UnmanagedType.ByValTStr,	SizeConst=(CoreConst.BOARD_DATE_LEN))]
		public string	sMTProduceDateTime;
		[MarshalAs(UnmanagedType.ByValTStr,	SizeConst=(CoreConst.BOARD_DATE_LEN))]
		public string	sReserveProduceDateTime;
		public uint	    m_nHBBoardVersion;
	};


	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	struct SDetailError
	{
		public ushort	m_nWho;
		public ushort	m_nReserve;
		public uint	m_What;
	};
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SFWFactoryData
	{
		public byte  m_nValidSize;
		public byte  m_nEncoder;
		public byte  m_nHeadType;
		public byte  m_nWidth;

		public byte m_nColorNum;
		public byte m_nGroupNum;
		public float m_fHeadXColorSpace;
		public float m_fHeadXGroupSpace;

		[MarshalAs(UnmanagedType.ByValArray,SizeConst=62-14)]
		public byte  []m_nReserve;
	};
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SMotionDebug
	{
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=64)]
		public byte  []m_nReserve;
	};

	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SSupportList
	{
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=8)]
		public byte  []m_nList;
	};
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SWriteHeadBoardInfo
	{
		public uint    m_nHeadFeature1; //XAAR = 0, Konica = 1, Spectra = 2 
		public uint    m_nHeadFeature2; //
	};



}
