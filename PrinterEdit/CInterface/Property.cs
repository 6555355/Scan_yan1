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
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml;
using System.IO;

//using DemoTreeView;
namespace BYHXPrinterManager
{
	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SPrinterProperty
	{
		public int				nRev1;
		public int				nRev2;
		[MarshalAs(UnmanagedType.I1)]
		public bool			bSupportFeather;
		[MarshalAs(UnmanagedType.I1)]
		public bool			bSupportHeadHeat;
		[MarshalAs(UnmanagedType.I1)]
		public bool 		bSupportRev1;
		[MarshalAs(UnmanagedType.I1)]
		public bool 		bSupportRev2;
		[MarshalAs(UnmanagedType.I1)]
		public bool			bSupportRev3;
		[MarshalAs(UnmanagedType.I1)]
		public bool			bSupportRev4;
		[MarshalAs(UnmanagedType.I1)]
		public bool 		bSupportRev5;
		public byte 		nCarriageReturnNum;


		//Common Printer Property	
		[MarshalAs(UnmanagedType.I4)]
		public PrinterHeadEnum ePrinterHead;
		[MarshalAs(UnmanagedType.I4)]
		public SingleCleanEnum	eSingleClean;
		public byte			nColorNum;
		public byte			nHeadNum;
		public byte			nHeadNumPerColor;
		public byte			nHeadNumPerGroupY;
		public byte			nHeadNumPerRow;
		public byte			nHeadHeightNum;
		public byte			nElectricNum;
		public byte			nResNum;
		public byte			nMediaType;
		public byte         nPassListNum;
		
		//One bit Property
		[MarshalAs(UnmanagedType.I1)]
		public bool			bSupportHardPanel;
		[MarshalAs(UnmanagedType.I1)]
		public bool			bSupportAutoClean;
		[MarshalAs(UnmanagedType.I1)]
		public bool 		bSupportPaperSensor;
		[MarshalAs(UnmanagedType.I1)]
		public bool 		bSupportWhiteInk;
		[MarshalAs(UnmanagedType.I1)]
		public bool 			bSupportUV;
		[MarshalAs(UnmanagedType.I1)]
		public bool 			bSupportHandFlash;
		[MarshalAs(UnmanagedType.I1)]
		public bool 			bSupportService;
		[MarshalAs(UnmanagedType.I1)]
		public bool 			bSupportMilling;
		[MarshalAs(UnmanagedType.I1)]
		public bool 			bSupportZMotion;

		[MarshalAs(UnmanagedType.I1)]
		public bool			bHeadInLeft;
		[MarshalAs(UnmanagedType.I1)]
		public bool			bPowerOnRenewProperty;
		[MarshalAs(UnmanagedType.I1)]
		public bool			bHeadElectricReverse;
		[MarshalAs(UnmanagedType.I1)]
		public bool			bHeadSerialReverse;
		[MarshalAs(UnmanagedType.I1)]
		public bool			bInternalMap;
		[MarshalAs(UnmanagedType.I1)]
		public bool			bElectricMap;

		//Clip Setting  
		public float			fMaxPaperWidth;
		public float			fMaxPaperHeight;
		//Arrange as mechaical 
		public float			fHeadAngle;
		public float 			fHeadYSpace;
		public float			fHeadXColorSpace;
		public float			fHeadXGroupSpace;

		public int				nResX;
		public int				nResY;
		public int				nStepPerHead;   ///????????????????? this value should put some place that user can Setting, this include Clean Pos 
		public float            fPulsePerInchX;
		public float            fPulsePerInchY;
		public float            fPulsePerInchZ;


		[MarshalAs(UnmanagedType.ByValArray,SizeConst=16)]
		public	byte	[]pHeadMask;
		//Color Order
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_HEAD_NUM)]
		public byte			[]pElectricMap;
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_COLOR_NUM)]
		public byte		[]eColorOrder;
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=(int)SpeedEnum.CustomSpeed)]
		public byte		[]eSpeedMap;
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_X_PASS_NUM)]
		public byte		[]pPassList;

		
		public void GetPassListNumber(out int passListNum, out int[]passListArray )
		{
			passListNum = nPassListNum;
			passListArray = new int[passListNum];
			int i = 0;
			for ( i=0; i<passListNum;i++)
			{
				passListArray[i] = pPassList[i];
			}
		}
		public float [] GetXOffset()
		{
			float [] xoffset = new float[nHeadNum];
			int groupnum = nHeadNum/nColorNum;
			int phylinenum = nColorNum;
			int phygroupnum = nHeadNum/phylinenum;
			for (int k = 0; k <phygroupnum; k++)
			{
				double group0_X = fHeadXGroupSpace *  k;
				if(fHeadXGroupSpace<0)
					group0_X = (- phygroupnum + 1 + k) * fHeadXGroupSpace;
				for (int i=0; i< phylinenum; i++)
				{
					int id = k * phylinenum + i;
					double color_X = fHeadXColorSpace * i;
					if(fHeadXColorSpace< 0)
						color_X = fHeadXColorSpace * ( i - phylinenum + 1);
					xoffset[id] = 
						(float)(group0_X + color_X); 
				}
			}
		
//			for (int j=0; j< groupnum;j++)
//			{
//				for (int i=0;i<nColorNum;i++)
//				{
//					if(fHeadXGroupSpace>0)
//						xoffset[j*nColorNum +i] = fHeadXColorSpace *i + fHeadXGroupSpace *j;
//					else
//						xoffset[j*nColorNum +i] = fHeadXColorSpace *i + fHeadXGroupSpace *(groupnum-1-j);
//
//				}
//			}
			return xoffset;
		}
		public float [] get_YOffset()
		{
			float [] yoffset = new float[nHeadNum];
			for (int i=0; i< nColorNum;i++)
			{
				if(fHeadYSpace>=0)
					yoffset[i] = i*fHeadYSpace;
				else
					yoffset[i] =(nColorNum-1- i)*fHeadYSpace;

			}
			return yoffset;
		}
		public int Get_ResXList(int index)
		{
#if false
			if(index == 1 && 
				(ePrinterHead == PrinterHeadEnum.Konica_KM_512_L ||
				ePrinterHead == PrinterHeadEnum.Konica_KM_256_L ||
				ePrinterHead == PrinterHeadEnum.Konica_KM_128_L )
				)
				return 240;
			else
				return (nResX >>index);
#else
			int []RES720  = new int []{720,360,240,180};
			int j=0; 
			if(nResX != RES720[0])
				j++;
			return RES720[j+index];
#endif
		}
		public string Get_ColorIndex(int index)
		{
			try
			{
				index = index%nColorNum;
				return ((ColorEnum_Short)eColorOrder[index]).ToString();
			}
			catch(Exception)
			{
				return "";
			}
		}
	}

}
