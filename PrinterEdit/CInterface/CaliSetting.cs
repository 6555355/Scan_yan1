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
using System.Xml;
using System.IO;

namespace BYHXPrinterManager
{
	//This struct is revise value, aimed at head color align.
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SCalibrationHorizonSetting
	{
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_HEAD_NUM)]
		public sbyte [] XLeftArray;
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_HEAD_NUM)]
		public sbyte [] XRightArray;
		public sbyte nBidirRevise;
	}

	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SCalibrationHorizonArray
	{
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_RESLIST_NUM*CoreConst.MAX_SPEED_NUM*CoreConst.SIZEOF_CalibrationHorizonSetting)]
		public byte [] Data;

		public SCalibrationHorizonArray(byte [] data)
		{
			Data = new byte[CoreConst.MAX_RESLIST_NUM*CoreConst.MAX_SPEED_NUM*CoreConst.SIZEOF_CalibrationHorizonSetting];
			if(data == null)
				return;
			if(data.Length == Data.Length)
			{
				data.CopyTo(Data,0);
			}
			else
			{
				Debug.Assert(false);
			}
		}

		public SCalibrationHorizonSetting this[int index]
		{
			get
			{
				if(Data == null || index*CoreConst.SIZEOF_CalibrationHorizonSetting >= Data.Length)
				{
					//Debug.Assert(false);
					return new SCalibrationHorizonSetting();
				}

				int length = Marshal.SizeOf(typeof(SCalibrationHorizonSetting));
				IntPtr ptr = Marshal.AllocHGlobal(length);
				Marshal.Copy(Data,index*length,ptr,length);
				SCalibrationHorizonSetting sSetting = (SCalibrationHorizonSetting)Marshal.PtrToStructure(ptr,typeof(SCalibrationHorizonSetting));
				Marshal.FreeHGlobal(ptr);

				return sSetting;
			}
			set
			{
				if(Data == null)
					Data = new byte[CoreConst.MAX_RESLIST_NUM*CoreConst.MAX_SPEED_NUM*CoreConst.SIZEOF_CalibrationHorizonSetting];
				Debug.Assert(index*CoreConst.SIZEOF_CalibrationHorizonSetting < Data.Length);
				int length = Marshal.SizeOf(typeof(SCalibrationHorizonSetting));
				IntPtr ptr = Marshal.AllocHGlobal(length);
				Marshal.StructureToPtr(value,ptr,false);
				Marshal.Copy(ptr,Data,index*length,length);
				Marshal.FreeHGlobal(ptr);
			}
		}

		public int Length
		{
			get
			{
				return CoreConst.MAX_RESLIST_NUM*CoreConst.MAX_SPEED_NUM;
			}
		}


#if false
		public XmlElement ConvertToXml(string name, Type type,XmlDocument doc)
		{
			XmlElement root = DNetXmlSerializer.ClassFieldToXml(name,this,type,doc);
			root.SetAttribute(DNetXmlSerializer.m_tagLenName,Length.ToString());
			XmlElement elem_i;
			for (int i=0;i< Length;i++)
			{
				elem_i = DNetXmlSerializer.ClassFieldToXml(typeof(SCalibrationHorizonSetting).Name,this[i],typeof(SCalibrationHorizonSetting),doc);
				root.AppendChild(elem_i);
			}
			return root;
		}
		public static object ConvertFromXml(XmlElement root, Type type)
		{
			SCalibrationHorizonArray oc = (SCalibrationHorizonArray)DNetXmlSerializer.ClassFieldFromXml(root,type);
			string sLen = root.GetAttribute(DNetXmlSerializer.m_tagLenName);
			int Len = Convert.ToInt32(sLen);
			
			XmlNode elem_i = root.FirstChild;
			object     object_i ;
			for (int i=0;i< Len;i++)
			{
				object_i = DNetXmlSerializer.ConvertFromXml((XmlElement)elem_i,typeof(SCalibrationHorizonSetting));
				oc[i] = (SCalibrationHorizonSetting)object_i;
				elem_i = elem_i.NextSibling;	
			}		
			return oc;
		}
#endif	
	}
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SCalibrationSetting
	{
		public int	  nStepPerHead; //????????????????????????????????????????????
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_PASS_NUM)]
		public int [] nPassStepArray;

		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_HEAD_NUM)]
		public int [] nVerticalArray;

		public int nLeftAngle;
		public int nRightAngle;

		public SCalibrationHorizonArray sCalibrationHorizonArray;
	}
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SAdvanceCalibrationSetting
	{
		[MarshalAs(UnmanagedType.I1)]
		public bool bIgnoreAngle;
		[MarshalAs(UnmanagedType.I1)]
		public bool bIgnoreYoffset;
		[MarshalAs(UnmanagedType.I1)]
		public bool bIgnoreXOffset;
	}

}
