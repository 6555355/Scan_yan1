/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Diagnostics;

//using BYHXPrinterManager.JobListView;
namespace BYHXPrinterManager
{
	/// <summary>
	/// Summary description for ResString
	/// </summary>
	public class ResString
	{
		public ResString()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static string GetResString(string name)
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ResString));
			return resources.GetString(name);
		}
		private static string GetUnkownEnumDisplayName(Type enumType,object enumValue)
		{
            return enumValue.ToString();//"Unknown Enum:" + enumType.Name + "_" +  enumValue.ToString();
		}
		private static string GetEnumResourceName(Type enumType,object enumValue)
		{
			return enumType.Name+"_"+enumValue.ToString();
		}
		public static string GetEnumDisplayName(Type enumType,object enumValue)
		{
			if(!enumType.IsEnum)
			{
				Debug.Assert(false);
				return null;
			}
			string resname = GetEnumResourceName(enumType,enumValue);
			string name = GetResString(resname);
			if(name == null)
                name = GetUnkownEnumDisplayName(enumType, enumValue);
			return name;
		}

		public static string GetUnitSuffixDispName(UILengthUnit unit)
		{

			string resname = "Suffix_" + typeof(UILengthUnit).Name+"_"+unit.ToString();
			string name = GetResString(resname);
			if(name == null)
                name = GetUnkownEnumDisplayName(typeof(UILengthUnit), unit);
			return name;
		}
		public static string GetProductName()
		{
#if LIYUUSB
			return GetResString("SoftwareProductName2");
#else
			return GetResString("SoftwareProductName");
#endif
		}
		public static string GetDisplayPass()
		{
			return GetResString("DispName_Pass");
		}
		public static string GetPrintJobAnyway()
		{
			return GetResString("PrintJobAnyway");
		}
		public static string GetPrintingProgress()
		{
			return GetResString("PrintingProgress");
		}
		public static string GetUpdatingProgress()
		{
			return GetResString("UpdatingProgress");
		}
		public static string GetNoPreviewImage()
		{
			return GetResString("NoPreviewImage");
		}
		public static string GetCreatingPreviewImage()
		{
			return GetResString("CreatingPreviewImage");
		}
	}
}
