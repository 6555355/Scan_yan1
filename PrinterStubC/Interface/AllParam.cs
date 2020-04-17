/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
#define SETTINGINCS
#define USE_C_PROPERYFILE
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Data;
using System.IO;
using System.Xml;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

using CSharpZip;
using PrinterStubC.Utility;

//using BYHXPrinterManager.JobListView;
namespace BYHXPrinterManager
{
	/// <summary>
	/// Summary description for AllParam.
	/// </summary>
	/// 
	public class AllParam
	{
		private const int  CALI_FLAG = 0x43414C49;   //((uint)'CALI');
		private const int  OTHER_FLAG = 0x4F544845; //((uint)'OTHE');
		private const int  TAIL_FLAG = 0x5441494C;  //((uint)'TAIL');
		private const int  XMLHeaderLen = 8;
		private const int  XMLHeader_FLAG = 0x3158;  //((uint)'X1');

		private string m_SettingFile = "Setting.xml";
		private string m_TagPrinterSetting = "SPrinterSetting";
		private string m_TagPreference = "UIPreference";
        private string m_TagDoubleSidePrintSettings = "SDoubleSidePrint";
        private string m_TagUvPowerLevelMap = "UvPowerLevelMap";
        private string m_PIDVID_SettingFile = "";

		public UIPreference Preference;
		public SPrinterProperty PrinterProperty;
		public SPrinterSetting  PrinterSetting;
		public EpsonExAllParam  EpsonAllParam;
        public SDoubleSidePrint DoubleSidePrint;
	    public SCalibrationHorizonArrayUI NewCalibrationHorizonArray;
	    public SSeviceSetting SeviceSetting;
        public UvPowerLevelMap UvPowerLevelMap;
        public PeripheralExtendedSettings ExtendedSettings;
        public S_3DPrint s3DPrint;
        public PositionSetting_LeCai sPS_lecai;
        public SCalibrationGroupUI CalibrationGroupUILeft;
        public SCalibrationGroupUI CalibrationGroupUIRight;
        public SNozzleOverlap NozzleOverlap;
        public SPrintQualityUI PrintQualityUI;
		public AllParam()
		{
			//
			// TODO: Add constructor logic here
			//
			Preference = new UIPreference();
			PrinterProperty = new SPrinterProperty();
			PrinterSetting = new SPrinterSetting();
			EpsonAllParam = new EpsonExAllParam();
            ExtendedSettings = new PeripheralExtendedSettings(null);
            SDoubleSidePrint param = new SDoubleSidePrint(null);
            DoubleSidePrint = param;
            SeviceSetting = new SSeviceSetting();
            CoreInterface.GetSeviceSetting(ref SeviceSetting);
            s3DPrint = new S_3DPrint();
            sPS_lecai = new PositionSetting_LeCai();
            CalibrationGroupUILeft = new SCalibrationGroupUI();
            CalibrationGroupUIRight = new SCalibrationGroupUI();
            NozzleOverlap = new SNozzleOverlap();
            PrintQualityUI = new SPrintQualityUI();
		}
		unsafe public void LoadFromXml(string filename,bool bFromC)
		{
			if(bFromC)
			{
				Preference = new UIPreference();
#if SETTINGINCS
				int  bPropertyChanged, bSettingChanged;
				SPrinterProperty  sPrinterProperty;
				SPrinterSetting sPrinterSetting;
						
				PowerOnEvent(out bPropertyChanged, out bSettingChanged,out sPrinterProperty,out sPrinterSetting);
				if(bPropertyChanged != 0)
				{
					PrinterProperty = sPrinterProperty;
				}
				if(bSettingChanged != 0)
				{
					PrinterSetting = sPrinterSetting;
				}
#else
				CoreInterface.GetSPrinterProperty(ref PrinterProperty);
				CoreInterface.GetPrinterSetting(ref PrinterSetting);
#endif
			}
			string curFile = filename;
			if(filename == null || filename=="")
				curFile = Application.StartupPath + Path.DirectorySeparatorChar + m_SettingFile;
			if(!File.Exists(curFile))
			{
                SDoubleSidePrint param = new SDoubleSidePrint(null);
                DoubleSidePrint = param;
                NewCalibrationHorizonArray = new SCalibrationHorizonArrayUI(null);
                CalibrationGroupUILeft = new SCalibrationGroupUI(null);
                CalibrationGroupUIRight = new SCalibrationGroupUI(null);
                NozzleOverlap = new SNozzleOverlap(null);
                PrintQualityUI = new SPrintQualityUI(null);
                return;
			}
			SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
		    if (!doc.Load(curFile))
		        return;
			XmlElement root = doc.DocumentElement;
			XmlElement elem_i;
            elem_i = PubFunc.GetFirstChildByName(root, typeof(SSeviceSetting).Name);
		    if (elem_i != null)
		    {
		        SeviceSetting = (SSeviceSetting)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SSeviceSetting));
		        SeviceSetting.unColorMask = SeviceSetting.unPassMask = 0; // 颜色和pass 的mask不使用上次界面存储的值,防止测试后忘记恢复回去
		    }
			elem_i = PubFunc.GetFirstChildByName(root,m_TagPreference);
            if (elem_i != null)
                Preference = (UIPreference)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(UIPreference));
            elem_i = PubFunc.GetFirstChildByName(root, m_TagDoubleSidePrintSettings);
            if (elem_i != null)
                DoubleSidePrint = (SDoubleSidePrint)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SDoubleSidePrint));
            elem_i = PubFunc.GetFirstChildByName(root, typeof(PeripheralExtendedSettings).Name);
            if (elem_i != null)
                ExtendedSettings = (PeripheralExtendedSettings)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(PeripheralExtendedSettings));
            elem_i = PubFunc.GetFirstChildByName(root, m_TagUvPowerLevelMap);
            if (elem_i != null)
                UvPowerLevelMap = (UvPowerLevelMap)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(UvPowerLevelMap));
            if (!bFromC)
			{
                int sCrcCali_Flag = PrinterSetting.sCrcCali.Flag;
                int sCrcCali_Len = PrinterSetting.sCrcCali.Len;

                int sCrcOther_Flag = PrinterSetting.sCrcOther.Flag;
                int sCrcOther_Len = PrinterSetting.sCrcOther.Len;
                int sCrcTail_Len = PrinterSetting.sCrcTail.Flag;

				elem_i = PubFunc.GetFirstChildByName(root,m_TagPrinterSetting);
				PrinterSetting = (SPrinterSetting)PubFunc.SystemConvertFromXml(elem_i.OuterXml,typeof(SPrinterSetting));
                PrinterSetting.sCrcCali.Flag = sCrcCali_Flag;
                PrinterSetting.sCrcCali.Len = sCrcCali_Len;
                PrinterSetting.sCrcOther.Flag = sCrcOther_Flag;//((uint)'CALI');
                PrinterSetting.sCrcOther.Len = sCrcOther_Len;
                PrinterSetting.sCrcTail.Flag = sCrcTail_Len;//((uint)'CALI');
                
                //elem_i = DNetXmlSerializer.GetFirstChildByName(root,m_TagPrinterProperty);
                //PrinterProperty = (SPrinterProperty)PubFunc.SystemConvertFromXml(elem_i.OuterXml,typeof(SPrinterProperty));
                elem_i = PubFunc.GetFirstChildByName(root, NewCalibrationHorizonArray.GetType().Name);
                if (elem_i != null)
                {
                    NewCalibrationHorizonArray =
                        (SCalibrationHorizonArrayUI)
                            PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SCalibrationHorizonArrayUI));
                }
                else
                {
                    ////20171207 注释掉
                    //Debug.Assert(false, "SCalibrationHorizonArrayUI losted !!!!!!");
                    //兼容性处理
                    NewCalibrationHorizonArray = new SCalibrationHorizonArrayUI(null);
                }
                SyncCalibrationHorizonSetting(PrinterSetting);
                elem_i = PubFunc.GetFirstChildByName(root, typeof(SCalibrationGroupUI).Name);
                if (elem_i != null)
                    CalibrationGroupUILeft = (SCalibrationGroupUI)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SCalibrationGroupUI));
                elem_i = PubFunc.GetSecondChildByName(root, typeof(SCalibrationGroupUI).Name);
                if (elem_i != null)
                    CalibrationGroupUIRight = (SCalibrationGroupUI)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SCalibrationGroupUI));
                elem_i = PubFunc.GetFirstChildByName(root, typeof(SNozzleOverlap).Name);
                if (elem_i != null)
                    NozzleOverlap = (SNozzleOverlap)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SNozzleOverlap));
                elem_i = PubFunc.GetFirstChildByName(root, typeof(SPrintQualityUI).Name);
                if (elem_i != null)
                    PrintQualityUI = (SPrintQualityUI)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SPrintQualityUI));
            }
		}
        /// <summary>
        /// 将设置参数存入xml保存下来
        /// </summary>
        /// <param name="filename">文件完整地址+文件名</param>
        /// <param name="bFromC">对于客户的详细设置参数要放在厂商目录下，其他情况下不存这部分内容</param>
		public void SaveToXml(string filename,bool bFromC)
		{
			if(bFromC)
			{
#if SETTINGINCS
				SaveCurrentSetting();
#else
				CoreInterface.SavePrinterSetting();
#endif
			}
			string curFile = filename;
			if(filename == null || filename=="")
                filename = curFile = Application.StartupPath + Path.DirectorySeparatorChar + m_SettingFile;
            curFile = filename + ".tmp";

			XmlElement root;
			SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
			root = doc.CreateElement("","AllParam","");
			doc.AppendChild(root);
            string xml = ToXmlString(bFromC);
			root.InnerXml = xml;
			doc.Save(curFile);
            if (File.Exists(filename))
                File.Copy(curFile,filename,true);
            else
                File.Move(curFile, filename);
        }
        public string ToXmlString(bool bFromC)
        {
            string xml = string.Format("{0}{1}{2}{3}{4}",
                PubFunc.SystemConvertToXml(Preference, Preference.GetType()),
                PubFunc.SystemConvertToXml(DoubleSidePrint, DoubleSidePrint.GetType()),
                PubFunc.SystemConvertToXml(SeviceSetting, SeviceSetting.GetType()),
                PubFunc.SystemConvertToXml(UvPowerLevelMap, UvPowerLevelMap.GetType()),
                PubFunc.SystemConvertToXml(ExtendedSettings, ExtendedSettings.GetType()));
            if (!bFromC)
            {
                xml = string.Format("{0}{1}", xml, bFromCSettingToXmlString());
                //xml += PubFunc.SystemConvertToXml(PrinterProperty,PrinterProperty.GetType());
            }
            return xml;
        }
        public string bFromCSettingToXmlString()
        {
            string xml =string.Format("{0}{1}{2}{3}{4}{5}",
                PubFunc.SystemConvertToXml(PrinterSetting, PrinterSetting.GetType()),
                PubFunc.SystemConvertToXml(NewCalibrationHorizonArray, NewCalibrationHorizonArray.GetType()),
                PubFunc.SystemConvertToXml(CalibrationGroupUILeft, CalibrationGroupUILeft.GetType()),
                PubFunc.SystemConvertToXml(CalibrationGroupUIRight, CalibrationGroupUIRight.GetType()),
                PubFunc.SystemConvertToXml(NozzleOverlap, NozzleOverlap.GetType()),
                PubFunc.SystemConvertToXml(PrintQualityUI, PrintQualityUI.GetType()));
            return xml;
        }
		public int GetLanguage()
		{
			string curFile = Application.StartupPath + Path.DirectorySeparatorChar + m_SettingFile;
			if(!File.Exists(curFile))
				return Preference.LangIndex; //????should be same with other case
			SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
			if(!doc.Load(curFile))
                return Preference.LangIndex;
			XmlElement root = doc.DocumentElement;
			XmlElement elem_i;

			elem_i = PubFunc.GetFirstChildByName(root,m_TagPreference);
			Preference = (UIPreference)PubFunc.SystemConvertFromXml(elem_i.OuterXml,typeof(UIPreference));
			return Preference.LangIndex;
		}

        public string GetSkinName()
        {
            string curFile = Application.StartupPath + Path.DirectorySeparatorChar + m_SettingFile;
            if (!File.Exists(curFile))
                return Preference.SkinName;//????should be same with other case
            SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
			if(!doc.Load(curFile))
                return Preference.SkinName; 
            XmlElement root = doc.DocumentElement;
            XmlElement elem_i;

            elem_i = PubFunc.GetFirstChildByName(root, m_TagPreference);
            Preference = (UIPreference)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(UIPreference));
            if (string.IsNullOrEmpty(Preference.SkinName))
                Preference.SkinName = "Default";
            return Preference.SkinName;
        }

		private string GetSettingPath(ushort Vid, ushort Pid)
		{
			string path1 = Application.StartupPath + Path.DirectorySeparatorChar + Vid.ToString("X4");
			string path2 = path1 + Path.DirectorySeparatorChar + Pid.ToString("X4");
			string ret = path2 + Path.DirectorySeparatorChar +  "Setting.xml";
			if (!Directory.Exists(path1))
			{
				Directory.CreateDirectory(path1);
			}
			if (!Directory.Exists(path2))
			{
				Directory.CreateDirectory(path2);
			}
			return ret;
		}
		private unsafe void CreateFactoryDefaultPrinterSetting( )
		{
			int length =  Marshal.SizeOf(typeof(SPrinterSetting));
			IntPtr ptr = Marshal.AllocHGlobal(length);
			byte * bPtr = (byte *)ptr;
			for (int i=0; i<length;i++)
				*bPtr++=0;
			SPrinterSetting sfdPS = (SPrinterSetting)Marshal.PtrToStructure(ptr,typeof(SPrinterSetting));
			Marshal.FreeHGlobal(ptr);

			//SPrinterSetting sfdPS = new SPrinterSetting();
			SPrinterProperty sPP = PrinterProperty;
			//memset(	sfdPS,0,sizeof(SPrinterSetting));

			sfdPS.sCrcCali.Flag = CALI_FLAG;
			sfdPS.sCrcCali.Len =  Marshal.SizeOf(typeof(SCalibrationSetting));
			sfdPS.sCrcOther.Flag = OTHER_FLAG;
			sfdPS.sCrcOther.Len =  Marshal.SizeOf(typeof(SPrinterSetting)) -  Marshal.SizeOf(typeof(SCalibrationSetting)) -  Marshal.SizeOf(typeof(CRCFileHead))*3;
			sfdPS.sCrcTail.Flag = TAIL_FLAG;
			sfdPS.sCrcTail.Len = 0;

			sfdPS.sBaseSetting.bIgnorePrintWhiteX = false;
			sfdPS.sBaseSetting.bIgnorePrintWhiteY = false;
			sfdPS.sBaseSetting.bUseMediaSensor  = true;
			sfdPS.sBaseSetting.fLeftMargin = 0;
			sfdPS.sBaseSetting.fTopMargin = 0;
			sfdPS.sBaseSetting.fPaperWidth = sPP.fMaxPaperWidth;
			sfdPS.sBaseSetting.fPaperHeight = sPP.fMaxPaperHeight;
			sfdPS.sBaseSetting.fJobSpace = 0;
			sfdPS.sBaseSetting.fStepTime = 1.0f;
			sfdPS.sBaseSetting.nAccDistance = 1600;//720 DPI
			sfdPS.sBaseSetting.sStripeSetting.bNormalStripeType = 0x1;
			sfdPS.sBaseSetting.sStripeSetting.eStripePosition = InkStrPosEnum.Both;
			sfdPS.sBaseSetting.sStripeSetting.fStripeOffset = 0.3937f;//0.25;
			sfdPS.sBaseSetting.sStripeSetting.fStripeWidth = 0.5f; //1//
			sfdPS.sBaseSetting.nXResutionDiv = 1;

			if(sPP.bSupportUV)
			{
				sfdPS.sCleanSetting.nCleanerPassInterval = 0;
				sfdPS.sCleanSetting.nSprayPassInterval = 0;
			}
			else
			{
				sfdPS.sCleanSetting.nCleanerPassInterval = 100;
				sfdPS.sCleanSetting.nSprayPassInterval = 0;
			}
			sfdPS.sCleanSetting.nCleanerTimes = 1;
#if LIYUUSB
			sfdPS.sCleanSetting.nSprayFireInterval = 1000;
#else
			sfdPS.sCleanSetting.nSprayFireInterval = 100;
#endif
			if(sPP.bSupportUV)
				sfdPS.sCleanSetting.nSprayFireInterval = 2000;

				sfdPS.sCleanSetting.nSprayTimes = 100;
			sfdPS.sCleanSetting.nCleanIntensity = 0;
			sfdPS.sCleanSetting.bSprayWhileIdle = true;

			sfdPS.sCleanSetting.nPauseTimeAfterSpraying = 1500;
			sfdPS.sCleanSetting.nPauseTimeAfterCleaning = 4000; //Tianyuan suggestion

			sfdPS.sFrequencySetting.fXOrigin = 0;
			sfdPS.sFrequencySetting.nResolutionX = sPP.nResX;
			sfdPS.sFrequencySetting.nResolutionY = sPP.nResY * sPP.nHeadNumPerColor;
			sfdPS.sFrequencySetting.nSpeed = SpeedEnum.HighSpeed;
			sfdPS.sFrequencySetting.nPass = 4;
			sfdPS.sFrequencySetting.nBidirection = 1;
			sfdPS.sFrequencySetting.bUsePrinterSetting = 1;

#if false
			for (int i= 0;i< CoreConst.MAX_HEAD_NUM;i++)
			{
				sfdPS.sRealTimeSetting.cTemperatureSet[i] = 20.0f;
				sfdPS.sRealTimeSetting.cTemperatureCur[i] = 20.0f;
				sfdPS.sRealTimeSetting.cPulseWidth[i] = 5.1f;
				sfdPS.sRealTimeSetting.cVoltage[i] = 0.0f;
				sfdPS.sRealTimeSetting.cVoltageBase[i] = 15.3f;
			}
#endif
			sfdPS.sCalibrationSetting.nStepPerHead = sPP.nStepPerHead;
			sfdPS.sBaseSetting.fPaperThick = 30.0f/25.4f;
			sfdPS.sBaseSetting.fZSpace = 2.0f/25.4f;
			sfdPS.sBaseSetting.nFeatherPercent = 0; // 0 not do feather have problem //Becasue the Nozzle have left
			sfdPS.nKillBiDirBanding = 0;
			sfdPS.sBaseSetting.bYPrintContinue = true;
//			sfdPS.sBaseSetting.nWhiteGray = 0xff;


			sfdPS.UVSetting.fLeftDisFromNozzel = sPP.fHeadXColorSpace *sPP.nColorNum;
			sfdPS.UVSetting.fRightDisFromNozzel = sPP.fHeadXColorSpace;
			sfdPS.UVSetting.fShutterOpenDistance = 0;

			sfdPS.ZSetting.fHeadToPaper  =   3.0f/25.4f;
			sfdPS.ZSetting.fMeasureSpeedZ = 0;
			sfdPS.ZSetting.fMesureHeight = 0;
			sfdPS.ZSetting.fMesureXCoor = 0;

            sfdPS.ZSetting.fSensorPosZ = 0;
            sfdPS.ZSetting.fMesureMaxLen = 0;
            if (PubFunc.IsDocan_Belt())
            {
                sfdPS.sCleanSetting.nCleanerPassInterval = 0;
            }

			if(SPrinterProperty.IsEpson(sPP.ePrinterHead))
			{
				sfdPS.sCleanSetting.nCleanerPassInterval = 0;
				sfdPS.sCleanSetting.nSprayPassInterval = 0;
				sfdPS.sCleanSetting.bSprayBeforePrint = false;
				sfdPS.sCleanSetting.bSprayWhileIdle = false;
				sfdPS.sBaseSetting.sStripeSetting.eStripePosition = InkStrPosEnum.None;
				sfdPS.sFrequencySetting.bUsePrinterSetting = 0;
				sfdPS.sBaseSetting.nXResutionDiv = 2;
			}
            sfdPS.sExtensionSetting.LineWidth = 3;
            this.ExtendedSettings.EnableSingleLayerMode = SPrinterProperty.IsFloraT50OrT180();
            sfdPS.sMoveSetting.nXMoveSpeed = 4;
            sfdPS.sMoveSetting.nYMoveSpeed = 4;
            sfdPS.sMoveSetting.nZMoveSpeed = 4;
            sfdPS.sMoveSetting.n4MoveSpeed = 4;
            PrinterSetting = sfdPS;
			this.Preference.bAlternatingPrint = false;
            this.Preference.bShowAttentionOnLoad = false;
            this.Preference.bShowMeasureFormBeforPrint = SPrinterProperty.IsFloraUv() || SPrinterProperty.IsFloraT50OrT180() || SPrinterProperty.IsGongZengMeasureBeforPrint();

            ////////////// 和dll逻辑保持一致,如果配色里有M,就默认用M作为基准色//////////////////////
            int colornum = sPP.GetRealColorNum();
            for (int i = 0; i < colornum; i++)
            {
                if ((sPP.eColorOrder[i] == (byte) ColorEnum_Short.K))
                {
                    SeviceSetting.nCalibrationHeadIndex = i;
                    break;
                }
            }
		}

		private SPrinterSetting LoadCurrentSetting()
		{
			string curFile = m_PIDVID_SettingFile;
			SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
		    if (!doc.Load(curFile))
		    {
                CreateFactoryDefaultPrinterSetting();
		        return PrinterSetting;
		    }

			XmlElement root = doc.DocumentElement;
			XmlElement elem_i;

			elem_i = PubFunc.GetFirstChildByName(root,m_TagPrinterSetting);
			SPrinterSetting sPrinterSetting = (SPrinterSetting)PubFunc.SystemConvertFromXml(elem_i.OuterXml,typeof(SPrinterSetting));
		    if (sPrinterSetting.sExtensionSetting.LineWidth <= 0)
		        sPrinterSetting.sExtensionSetting.LineWidth = 3;
            elem_i = PubFunc.GetFirstChildByName(root, NewCalibrationHorizonArray.GetType().Name);
            if (elem_i != null && !string.IsNullOrEmpty(elem_i.InnerText))
            {
                NewCalibrationHorizonArray = (SCalibrationHorizonArrayUI)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SCalibrationHorizonArrayUI));
            }
            else
            {
                NewCalibrationHorizonArray = new SCalibrationHorizonArrayUI(null);
            }
            SyncCalibrationHorizonSetting(sPrinterSetting);

            elem_i = PubFunc.GetFirstChildByName(root, CalibrationGroupUILeft.GetType().Name);
            if (elem_i != null && !string.IsNullOrEmpty(elem_i.InnerText))
            {
                CalibrationGroupUILeft = (SCalibrationGroupUI)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SCalibrationGroupUI));
            }
            else
            {
                //兼容性处理
                CalibrationGroupUILeft = new SCalibrationGroupUI(null);
            }
            elem_i = PubFunc.GetSecondChildByName(root, CalibrationGroupUIRight.GetType().Name);
            if (elem_i != null && !string.IsNullOrEmpty(elem_i.InnerText))
            {
                CalibrationGroupUIRight = (SCalibrationGroupUI)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SCalibrationGroupUI));
            }
            else
            {
                //兼容性处理
                CalibrationGroupUIRight = new SCalibrationGroupUI(null);
            }
            elem_i = PubFunc.GetFirstChildByName(root, NozzleOverlap.GetType().Name);
            if (elem_i != null && !string.IsNullOrEmpty(elem_i.InnerText))
            {
                NozzleOverlap = (SNozzleOverlap)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SNozzleOverlap));
            }
            else
            {
                //兼容性处理
                NozzleOverlap = new SNozzleOverlap(null);
            }
            elem_i = PubFunc.GetFirstChildByName(root, PrintQualityUI.GetType().Name);
            if (elem_i != null && !string.IsNullOrEmpty(elem_i.InnerText))
            {
                PrintQualityUI = (SPrintQualityUI)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SPrintQualityUI));
            }
            else
            {
                //兼容性处理
                PrintQualityUI = new SPrintQualityUI(null);
            }
			return sPrinterSetting;
		}

        /// <summary>
        /// 将老的的数据结构中的水平校准参数同步到新数据结构中
        /// </summary>
	    private void SyncCalibrationHorizonSetting(SPrinterSetting ps)
	    {
            for (int i = 0; i < ps.sCalibrationSetting.sCalibrationHorizonArray.Length; i++)
            {
                var curHor = ps.sCalibrationSetting.sCalibrationHorizonArray[i];
                var newHor = NewCalibrationHorizonArray.HorizonSettings[i];
                Array.Copy(curHor.XLeftArray, 0, newHor.XLeftArray, 0, curHor.XLeftArray.Length);
                Array.Copy(curHor.XRightArray, 0, newHor.XRightArray, 0, curHor.XRightArray.Length);
                NewCalibrationHorizonArray.HorizonSettings[i] = newHor;
            }
        }

		private void SaveCurrentSetting()
		{
			string curFile = m_PIDVID_SettingFile;
			XmlElement root;
			SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
			root = doc.CreateElement("","AllParam","");
			doc.AppendChild(root);

			string xml = "";
			xml += PubFunc.SystemConvertToXml(PrinterSetting,PrinterSetting.GetType());
            xml += PubFunc.SystemConvertToXml(NewCalibrationHorizonArray, NewCalibrationHorizonArray.GetType());
            xml += PubFunc.SystemConvertToXml(CalibrationGroupUILeft, CalibrationGroupUILeft.GetType());
            xml += PubFunc.SystemConvertToXml(CalibrationGroupUIRight, CalibrationGroupUIRight.GetType());
            xml += PubFunc.SystemConvertToXml(NozzleOverlap, NozzleOverlap.GetType());
            xml += PubFunc.SystemConvertToXml(PrintQualityUI, PrintQualityUI.GetType());
            root.InnerXml = xml;
			if(curFile !=null  && curFile != "")
				doc.Save(curFile);
		}
		public int PowerOnLoadSetting(out SPrinterSetting sPrinterSetting)
		{
			ushort Vid = 0;
			ushort Pid = 0;
			int ret = CoreInterface.GetProductID(ref Vid,ref Pid);
		    if (ret == 0)
		    {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.GetHWSettingFail);
                MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
			m_PIDVID_SettingFile = GetSettingPath(Vid,Pid);
			if(!File.Exists(m_PIDVID_SettingFile))
			{
				CreateFactoryDefaultPrinterSetting();
				ChangeSettingAsPP(ref PrinterSetting);
                ChangeSettingAsVenderId(ref PrinterSetting);
                SaveCurrentSetting();
			}
			sPrinterSetting = LoadCurrentSetting();
			//Modify Setting as Property
			ChangeSettingAsPP(ref sPrinterSetting);

			CoreInterface.SetPrinterSetting(ref sPrinterSetting);
			return 1;
		}
		private void ChangeSettingAsPP(ref SPrinterSetting sPrinterSetting)
		{
			if(sPrinterSetting.sBaseSetting.fPaperWidth> PrinterProperty.fMaxPaperWidth)
				sPrinterSetting.sBaseSetting.fPaperWidth = PrinterProperty.fMaxPaperWidth;
            if (sPrinterSetting.sBaseSetting.fPaperHeight > PrinterProperty.fMaxPaperHeight
                ||(sPrinterSetting.sBaseSetting.fPaperHeight <= CoreConst.DOUBLE_DELTA && PrinterProperty.fMaxPaperHeight > CoreConst.DOUBLE_DELTA)
                )
                sPrinterSetting.sBaseSetting.fPaperHeight = PrinterProperty.fMaxPaperHeight;

			int nEncoderRes, nPrinterRes;
			nEncoderRes = nPrinterRes = 0;
			CoreInterface.GetPrinterResolution(ref nEncoderRes, ref nPrinterRes);

			int nResx = nPrinterRes;
			PrinterHeadEnum nHeadType = PrinterProperty.ePrinterHead;
            if (SPrinterProperty.IsHighResolution(nHeadType))
			{
				nResx = nPrinterRes;
			}
			else
			{
				nResx = nPrinterRes/2;
			}
			sPrinterSetting.sFrequencySetting.nResolutionX = nResx ;

			int nResY = PrinterProperty.nResY;
#if KINCOLOR_PENTUJI
			nResY = 18;
#endif
			if(nHeadType <= PrinterHeadEnum.Xaar_500 
				|| nHeadType == PrinterHeadEnum.Xaar_Electron_35W
				|| nHeadType == PrinterHeadEnum.Xaar_Electron_70W)
			{
				nResY = 185;
			}
			else if(nHeadType == PrinterHeadEnum.Spectra_S_128
				||SPrinterProperty.IsPolaris(nHeadType))
			{
				nResY = 50;
			}
			else if(nHeadType == PrinterHeadEnum.Spectra_GALAXY_256)
			{
				nResY = 100;
			}
			else if(nHeadType == PrinterHeadEnum.Spectra_NOVA_256
             || SPrinterProperty.IsKonica512i(nHeadType)
             )
			{
				nResY = 90;
			}
			else if( 
				nHeadType == PrinterHeadEnum.RICOH_GEN4_7pl
				||nHeadType == PrinterHeadEnum.RICOH_GEN4P_7pl
				||nHeadType == PrinterHeadEnum.RICOH_GEN4L_15pl
				)
			{
				nResY = 150;
			}
            else if (nHeadType == PrinterHeadEnum.XAAR_1201_Y1)
            {
                nResY = 300;
            }
            else if (SPrinterProperty.IsSG1024(nHeadType))
			{
                if (CoreInterface.IsSG1024_AS_8_HEAD())
			        nResY = 50;
			    else
			    {
			        nResY = 400;
                    if (PrinterProperty.nOneHeadDivider == 2)
                        nResY /= 2;
                    if (PrinterProperty.IsMirrorArrangement())
                        nResY *= 2;
                }
			}
			else if (SPrinterProperty.IsKyocera(nHeadType))
			{
			    if (SPrinterProperty.IsKyocera300(nHeadType)
                    || SPrinterProperty.IsKyocera600(nHeadType))
			        nResY = 600;
			    if (SPrinterProperty.IsKyocera1200(nHeadType))
			        nResY = 1200;
			    if (PrinterProperty.nOneHeadDivider == 2)
			        nResY /= 2;
			    if (PrinterProperty.IsMirrorArrangement())
			        nResY *= 2;
			}
			else if (SPrinterProperty.IsKonica1024i(nHeadType))
			{
			    if (CoreInterface.IsKm1024I_AS_4HEAD())
			        nResY = 90;
			    else
			        nResY = 360;
			}
            else if (nHeadType==PrinterHeadEnum.Konica_M600SH_2C)
            {
                nResY = 600;
                if (PrinterProperty.nOneHeadDivider == 2)
                    nResY /= 2;
                if (PrinterProperty.IsMirrorArrangement())
                    nResY *= 2;
            }
		    if(PrinterProperty.fHeadAngle != 0.0f)
				nResY = (int)((double)nResY/(double)Math.Cos(PubFunc.ConvAngleToRadian(PrinterProperty.fHeadAngle)));


            if (SPrinterProperty.IsKyocera(nHeadType)
                || nHeadType == PrinterHeadEnum.Konica_M600SH_2C
                )//京瓷喷头600dpi 16排,不能整除,特殊处理
                sPrinterSetting.sFrequencySetting.nResolutionY = (nResY);
            else if (nHeadType == PrinterHeadEnum.Epson_S2840 || nHeadType == PrinterHeadEnum.Epson_S2840_WaterInk || nHeadType == PrinterHeadEnum.EPSON_S1600_RC_UV)
                sPrinterSetting.sFrequencySetting.nResolutionY = (nResY * PrinterProperty.nHeadNumPerColor);
            else
                sPrinterSetting.sFrequencySetting.nResolutionY = (nResY * PrinterProperty.nHeadNumPerColor);
		}

        /// <summary>
        /// 根据厂商id更改默认配置
        /// </summary>
        /// <param name="sPrinterSetting"></param>
        private void ChangeSettingAsVenderId(ref SPrinterSetting sPrinterSetting)
        {
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (PrinterProperty.IsDocan())
                {
                    //sfdPS.sBaseSetting.bYPrintContinue = true;  //平板应该做false 但是卷材不显示 无法改动所以这个不做

                    sPrinterSetting.sBaseSetting.bFeatherMaxNew = 1;  //羽化强
                    sPrinterSetting.sCleanSetting.nSprayFireInterval = 3000; //3S 空闲闪喷
                    sPrinterSetting.sCleanSetting.bSprayWhileIdle = true;


                    byte NormalType = (byte)EnumStripeType.Normal | (byte)EnumStripeType.HeightWithImage;  //与图像同高
                    sPrinterSetting.sBaseSetting.sStripeSetting.bNormalStripeType = NormalType; 

                    sPrinterSetting.sBaseSetting.fPaperThick =0f / 25.4f;   //介质厚度0 间距2mm
                    sPrinterSetting.sBaseSetting.fZSpace = 2.0f *10 / 25.4f; //2.0 是比较合适的距离，但是为了安全设置成为20mm，扩大10倍

                    sPrinterSetting.sMoveSetting.nXMoveSpeed = 4;
                    sPrinterSetting.sMoveSetting.nYMoveSpeed = 2;
                    sPrinterSetting.sMoveSetting.nZMoveSpeed = 4;

                    //基准步进 在Property。bin  卷轴 是 56669 平板是 112437 
                    sPrinterSetting.UVSetting.fLeftDisFromNozzel = 62.0f/2.54f;  //62cm
                    sPrinterSetting.UVSetting.fRightDisFromNozzel = 27.0f / 2.54f; //27cm
                    sPrinterSetting.UVSetting.fShutterOpenDistance = 10.0f / 2.54f;  //10cm

                    sPrinterSetting.UVSetting.iLeftRightMask = 0x8 | 0x4 | 0x2 | 0x1;//
                    //
                    sPrinterSetting.sFrequencySetting.nPass = 8; // 8pass

                    sPrinterSetting.sBaseSetting.nSpotColor1Mask = ((70 * 255 / 100) << 8) | (int)EnumWhiteInkImage.All;
                    this.Preference.Unit = UILengthUnit.Millimeter;


                }
                else if (vid == (ushort)VenderID.GONGZENG_ROLL_UV || vid == (ushort)VenderID.GONGZENG_FLAT_UV || vid == (ushort)VenderID.GONGZENG)
                {
                    sPrinterSetting.sBaseSetting.bUseFeather = true;
                    sPrinterSetting.sBaseSetting.bFeatherMaxNew = 1;
                    sPrinterSetting.sBaseSetting.nFeatherPercent = 101;
                    sPrinterSetting.sExtensionSetting.zMaxLength = 10f/2.54f; //10cm
                    Preference.ViewModeIndex = 1;
                }
                if (vid == (ushort) VenderID.GONGZENG_ROLL_UV || vid == (ushort) VenderID.GONGZENG_FLAT_UV ||
                    vid == (ushort) VenderID.GONGZENG || vid == (ushort) VenderID.GONGZENG_BELT_TEXTILE)
                {
                    sPrinterSetting.sCleanSetting.nCleanerPassInterval = 0;
                    sPrinterSetting.sMoveSetting.nXMoveSpeed = 4;
                    sPrinterSetting.sMoveSetting.nYMoveSpeed = 1;//工正要改成1
                    sPrinterSetting.sMoveSetting.nZMoveSpeed = 4;
                    sPrinterSetting.sMoveSetting.n4MoveSpeed = 4;
                    if (PrinterProperty.bSupportUV)
                        Preference.Unit = UILengthUnit.Millimeter;
                }
            }
        }

		public void PowerOnEvent(out int  bPropertyChanged, out int bSettingChanged, out SPrinterProperty  sPrinterProperty, out SPrinterSetting sPrinterSetting)
		{
			sPrinterProperty = new SPrinterProperty();
			sPrinterProperty = PrinterProperty;
			bPropertyChanged = CoreInterface.GetSPrinterProperty(ref PrinterProperty);
			if(bPropertyChanged == 0)
			{
				Debug.Assert(false);
			}
			else
			{
				sPrinterProperty = PrinterProperty;
			}
#if SETTINGINCS
			bSettingChanged =PowerOnLoadSetting(out sPrinterSetting);
#else
			sPrinterSetting = new SPrinterSetting();
			sPrinterSetting = PrinterSetting;
			bSettingChanged = CoreInterface.GetPrinterSetting(ref sPrinterSetting);
			if(bSettingChanged == 0)
			{
				Debug.Assert(false);
			}
			else
			{
			}
#endif
		}
		public void PowerOffEvent()
		{
#if SETTINGINCS
			SaveCurrentSetting();
#else
			CoreInterface.SavePrinterSetting();
#endif
		}

		public int GetFWSetting()
		{
			int iRet = 0;
#if SETTINGINCS
            // 如果是压缩xml格式,先读取压缩大小
            int length = Marshal.SizeOf(typeof(SPrinterSetting_V1));
            byte[] xmlheadbuf = new byte[XMLHeaderLen];
            iRet = CoreInterface.GetFWSetting(xmlheadbuf, xmlheadbuf.Length);
		    if (iRet != 0)
		    {
                bool bxml = false;
                if ((xmlheadbuf[0] == (XMLHeader_FLAG & 0xff)) && (xmlheadbuf[1] == ((XMLHeader_FLAG >> 8) & 0xff)))
                    bxml = true;
                if (bxml)
                {
                    length = xmlheadbuf[2] + (xmlheadbuf[3] << 8);
                }
            }
            // 读取完整数据
            byte[] srcBuf = new byte[length + XMLHeaderLen];
			iRet = CoreInterface.GetFWSetting(srcBuf, srcBuf.Length);
			if(iRet != 0)
			{
				bool bxml= false;
				if((srcBuf[0] == (XMLHeader_FLAG&0xff)) && (srcBuf[1] == ((XMLHeader_FLAG>>8)&0xff)))
					bxml = true;
				if(bxml)
				{
                    byte[] dstBuf = new byte[500*1024];
					int  compsize = srcBuf[2] + (srcBuf[3]<<8);
					int dcompsize = CSharpZip.GZipInterface.GZipComp(srcBuf,XMLHeaderLen,compsize,dstBuf,0,dstBuf.Length,false);
					
					string  decodedString = Encoding.ASCII.GetString(dstBuf);
					
					SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
					doc.LoadXml(decodedString);
					XmlElement root = doc.DocumentElement;
					XmlElement elem_i;

                    elem_i = PubFunc.GetFirstChildByName(root, typeof(SSeviceSetting).Name);
                    if (elem_i != null)
                    {
                        SeviceSetting = (SSeviceSetting)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SSeviceSetting));
                        SeviceSetting.unColorMask = SeviceSetting.unPassMask = 0; // 颜色和pass 的mask不使用上次界面存储的值,防止测试后忘记恢复回去
                    }
                    elem_i = PubFunc.GetFirstChildByName(root, m_TagPreference);
                    if (elem_i != null)
                        Preference = (UIPreference)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(UIPreference));
                    elem_i = PubFunc.GetFirstChildByName(root, m_TagDoubleSidePrintSettings);
                    if (elem_i != null)
                        DoubleSidePrint = (SDoubleSidePrint)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SDoubleSidePrint));
                    elem_i = PubFunc.GetFirstChildByName(root, typeof(PeripheralExtendedSettings).Name);
                    if (elem_i != null)
                        ExtendedSettings = (PeripheralExtendedSettings)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(PeripheralExtendedSettings));
                    elem_i = PubFunc.GetFirstChildByName(root, m_TagUvPowerLevelMap);
                    if (elem_i != null)
                        UvPowerLevelMap = (UvPowerLevelMap)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(UvPowerLevelMap));

                    int sCrcCali_Flag = PrinterSetting.sCrcCali.Flag;
                    int sCrcCali_Len = PrinterSetting.sCrcCali.Len;

                    int sCrcOther_Flag = PrinterSetting.sCrcOther.Flag;
                    int sCrcOther_Len = PrinterSetting.sCrcOther.Len;
                    int sCrcTail_Len = PrinterSetting.sCrcTail.Flag;

                    elem_i = PubFunc.GetFirstChildByName(root, m_TagPrinterSetting);
                    PrinterSetting = (SPrinterSetting)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SPrinterSetting));
                    PrinterSetting.sCrcCali.Flag = sCrcCali_Flag;
                    PrinterSetting.sCrcCali.Len = sCrcCali_Len;
                    PrinterSetting.sCrcOther.Flag = sCrcOther_Flag;//((uint)'CALI');
                    PrinterSetting.sCrcOther.Len = sCrcOther_Len;
                    PrinterSetting.sCrcTail.Flag = sCrcTail_Len;//((uint)'CALI');

                    //elem_i = DNetXmlSerializer.GetFirstChildByName(root,m_TagPrinterProperty);
                    //PrinterProperty = (SPrinterProperty)PubFunc.SystemConvertFromXml(elem_i.OuterXml,typeof(SPrinterProperty));
                    elem_i = PubFunc.GetFirstChildByName(root, NewCalibrationHorizonArray.GetType().Name);
                    if (elem_i != null)
                    {
                        NewCalibrationHorizonArray =
                            (SCalibrationHorizonArrayUI)
                                PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SCalibrationHorizonArrayUI));
                    }
                    else
                    {
                        ////20171207 注释掉
                        //Debug.Assert(false, "SCalibrationHorizonArrayUI losted !!!!!!");
                        //兼容性处理
                        NewCalibrationHorizonArray = new SCalibrationHorizonArrayUI(null);
                    }
                    SyncCalibrationHorizonSetting(PrinterSetting);
                    elem_i = PubFunc.GetFirstChildByName(root, typeof(SCalibrationGroupUI).Name);
                    if (elem_i != null)
                        CalibrationGroupUILeft = (SCalibrationGroupUI)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SCalibrationGroupUI));
                    elem_i = PubFunc.GetSecondChildByName(root, typeof(SCalibrationGroupUI).Name);
                    if (elem_i != null)
                        CalibrationGroupUIRight = (SCalibrationGroupUI)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SCalibrationGroupUI));
                    elem_i = PubFunc.GetFirstChildByName(root, typeof(SNozzleOverlap).Name);
                    if (elem_i != null)
                        NozzleOverlap = (SNozzleOverlap)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SNozzleOverlap));
                    elem_i = PubFunc.GetFirstChildByName(root, typeof(SPrintQualityUI).Name);
                    if (elem_i != null)
                        PrintQualityUI = (SPrintQualityUI)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SPrintQualityUI));
                    
				}
				else
				{
					//Old Convert to Xml
					IntPtr ptr = Marshal.AllocHGlobal(length);
					Marshal.Copy(srcBuf,0,ptr,length);
					SPrinterSetting_V1 obj = (SPrinterSetting_V1)Marshal.PtrToStructure(ptr,typeof(SPrinterSetting_V1));
					Marshal.FreeHGlobal(ptr);

					string xml = PubFunc.SystemConvertToXml(obj,obj.GetType());

					//Then Load to new Xml
					PrinterSetting = (SPrinterSetting)PubFunc.SystemConvertFromXml(xml,typeof(SPrinterSetting));
				}
				iRet = CoreInterface.SetPrinterSetting(ref PrinterSetting);
			}

#else
			iRet = CoreInterface.SendJetCommand((int)JetCmdEnum.LoadSetting,0);
			if(iRet != 0)
				iRet = CoreInterface.GetPrinterSetting(ref PrinterSetting);
#endif
			return iRet;
		}
		public int SetFWSetting()
		{
			int iRet = 0;

#if SETTINGINCS
			XmlElement root;
			SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
			root = doc.CreateElement("","AllParam","");
			doc.AppendChild(root);
            string xml = ToXmlString(false);
			//xml += PubFunc.SystemConvertToXml(PrinterProperty,PrinterProperty.GetType());
            byte[] twodata = SerializationUnit.StructToBytes(this.CalibrationGroupUILeft);
            byte[] twodata1 = SerializationUnit.StructToBytes(this.CalibrationGroupUIRight);
            byte[] twodata2 = SerializationUnit.StructToBytes(this.PrinterSetting);

            int l = twodata2.Length + twodata.Length + twodata1.Length;
			root.InnerXml = xml;
			byte [] srcBuf = Encoding.ASCII.GetBytes(root.OuterXml);

            byte[] dstBuf = new byte[srcBuf.Length+8];
			int compsize = CSharpZip.GZipInterface.GZipComp(srcBuf,0,srcBuf.Length,dstBuf,XMLHeaderLen,dstBuf.Length-XMLHeaderLen,true);
		    if (compsize > 0)
		    {
		        dstBuf[0] = (byte) (XMLHeader_FLAG & 0xff);
		        dstBuf[1] = (byte) ((XMLHeader_FLAG >> 8) & 0xff);
		        dstBuf[2] = (byte) (compsize & 0xff);
		        dstBuf[3] = (byte) ((compsize >> 8) & 0xff);
		        dstBuf[4] = 0;
		        dstBuf[5] = 0;
		        dstBuf[6] = 0;
		        dstBuf[7] = 0;

		        iRet = CoreInterface.SetFWSetting(dstBuf, compsize + XMLHeaderLen);
		    }
#else
			iRet = CoreInterface.SendJetCommand((int)JetCmdEnum.SaveSetting,0);
#endif
			return iRet;
		}
	}
}
