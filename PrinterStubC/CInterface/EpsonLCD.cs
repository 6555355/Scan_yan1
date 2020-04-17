using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Text;
using System.Windows.Data;
using PrinterStubC.Utility;

namespace BYHXPrinterManager
{
	public enum XCoordinatesDirection 
	{
		//		Default = 0,
		YcontinueYinterleave,// ��X��������꣬��Ycontinue group����Yinterleave group��
		YinterleaveYcontinue, // ��X��������꣬��Yinterleave group����Ycontinue group��
	}

	public enum SymmetricType 
	{
		Type0 = 0, // 0���������Ϊ�ԳƵ�λ���ڲ�ͬ��ͷ����ɫ�ǶԳƵģ���ʽ0���������򸲸ǹ�ϵ���µ�ɫ�Ч��Ӧ�ø��á�
		Type1,  //1�������ڲ�Ϊ�ԳƵ�λ����һ��ͷ�ڲ���ɫ�ǶԳƵģ�����ʽ1��������ƴ�壬������ɫ֮��ľ���Ƚ�խ��
	}


	public enum Cali_Pattern_Type
	{
		DotCheck = 0,
		NozzleCheck,
		StepCheck,
		StepCheck_Micro,
		StepVerify,
		LeftCheck,
		RightCheck,
		BiDirCheck,
		VerticalCheck,
		InterleaveCheck,
		YIntervalCheck,
		AngleLeftCheck,
		AngleRightCheck
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 0)]
	public struct CaliPrintSetting
	{
		public Cali_Pattern_Type type;
 
		public byte VSDModel; // �ڶ���Byte��VSD Model,�Ϸ�ֵ��1-4��default��3��
		public byte DotSetting; // ������Byte�ǵ��С,�Ϸ�ֵ��0-1-2-3������Ϊ�Զ���С�д�default�Ǵ�
		public byte DPIModel; // ���ĸ�Byte�Ƿֱ���,�Ϸ�ֵ��1-2-3-4-5������Ϊ720,360,540,270,1440��default��360��
		public ushort StartPos; // ��������Byte����ʼλ��,��λ������DPI��һ���㡣
    
		//only for Y Step calibration.
		public byte MediaType;//���߸�Byte�ǽ������ͣ���ʱ����Ϊ,  Index: 0:GlossPaper, 1:Vinyl, 2:PP, 3:Film, 4:Other1, 5:Other2, 6:Other3.
		public byte PassNum;//�ڰ˸�Byte��Pass number.��1~32.
 
		public byte Option;           //�ھŸ�Byte��һ������������������ʹ�á�default��0. 
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 0)]
	public struct CaliConfig
	{
		public byte len;
		public byte version;
		public byte BaseColor; //color code. it define the title color. 
		//and, for most calibration, it define calibration base color. current is K.
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=2)]
		public byte[] YStepColor; //current is K,M
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=59)]
		public 	byte[] reserved;
	}

	//	EPSON need the more factory data. So, FactoryWrite and WriteBoardConfig tools must change.
	//#define COLORCODE_K 'K'
	//#define COLORCODE_C 'C'
	//#define COLORCODE_M 'M'
	//#define COLORCODE_Y 'Y'
	//
	//#define MAX_NAME_LEN 16
	//#define FACTORYDATA_EX_VER 0x1
	//#define FACTORYDATA_EX_MASK_HEADDIR_IS_POS  0x1
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 0)]
	public struct EPR_FactoryData_Ex
	{
		public 	byte len;  //current len is sizeof(struct EPR_FactoryData_Ex)
		public 	byte version;   //current version is 0x02
 
		public 	ushort m_nXEncoderDPI;  //720/600 DPI.
		public 	int m_nBitFlagEx; //Bit0 : bHeadDirIsPositive, current dir is positive. Bit1 : bIsWeakSoventPrinter, default is 0.
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=8)]
		public 	byte[] m_nColorOrder; //content is color code.
		//�п��ܳ�����ͷ�����ƴ������
		// �������������
		// YInterleaveNum С�� ��ͷ�����ƴ��������һͷ��ɫ�����ӣ��磬EPSON 1ͷ��ɫ��
		// YInterleaveNum ���� ��ͷ�����ƴ����������ͨ�����ӣ��� EPSON 2ͷ��ɫ��ǰ��ɫ������ɫ����
		// YInterleaveNum ���� ��ͷ�����ƴ����������ͷƴ������ӣ��� EPSON 2ͷ4ɫ��Y��ƴ���720DPI��
		public 	byte YInterleaveNum; //����ƴ�����Ŀ������EPSON��˫��ɫ��ӡ��Ϊ2; 8ɫ����Ϊ1. ����ricoh��Ӧ��Ϊ2.
		//factoryDataEx.LayoutType bit0 == 0, ��X��������꣬��Ycontinue����Yinterleave��
		//factoryDataEx.LayoutType bit0 == 1, ��X��������꣬��Yinterleave����Ycontinue��
		//  �ڲ���ͷ������ǣ���Ycontinue����Yinterleave��
		//factoryDataEx.LayoutType bit1 == 1, �Գ�ɫ��Y interleave ������2�ı�����2012-7-2: still not support fully.
		//factoryDataEx.LayoutType bit2���Գ�ɫ��ĶԳƷ�ʽ����������ֶԳƷ�ʽ��ʱ���磬EPSON Y ƴ���720DPI)��
		//  0���������Ϊ�ԳƵ�λ���ڲ�ͬ��ͷ����ɫ�ǶԳƵģ���1�������ڲ�Ϊ�ԳƵ�λ����һ��ͷ�ڲ���ɫ�ǶԳƵģ���
		//  ��ʽ0���������򸲸ǹ�ϵ���µ�ɫ�Ч��Ӧ�ø��á���ʽ1��������ƴ�壬������ɫ֮��ľ���Ƚ�խ��
		//factoryDataEx.LayoutType bit3 == 0, Ycontinue group ��X�����ǻ��ơ�ͨ�������Ƹ���ʡ�ռ䡣
		// ���磬EPSON4H/4C����X��λ��ֻ������λ�ã�13��һ��λ�ã�24��һ��λ�á�34��λ�û����ˡ�
		public 	byte LayoutType;   //reserved for special layout. default is 0. 0 means PrintHead is Y continue. 
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_NAME_LEN)]
		public 	byte[] ManufacturerName;
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=CoreConst.MAX_NAME_LEN)]
		public 	byte[] PrinterName;

		//for ricoh. ��ӡ���������������������û��ر����е������顣
		public 	byte MaxGroupNumber; 
		public 	byte Vsd2ToVsd3;      //VSD mode
		public 	byte Vsd2ToVsd3_ColorDeep; //ColorDeep
		public 	byte Only_Used_1head; //�Ƿ�����ͷ��ӡ
		public 	byte Mask_head_used;//�Ǹ�ͷ�Ǵ�ӡͷ��1: head1��2: head2,  4: head3, 8: head4, �ȵ�

		public 	byte reserved;

		public EPR_FactoryData_Ex(SPrinterProperty sp)
		{
			len = (byte)Marshal.SizeOf(typeof(EPR_FactoryData_Ex));
			version = 0x01;
			m_nXEncoderDPI = 720;
			m_nBitFlagEx = 0;
			m_nColorOrder = new byte[8];
			byte[] deforder = new byte[4];
            if (sp.EPSONLCD_DEFINED)
			{
				deforder = new byte[]{75,67,77,89};	//kcmy		
			}
			else
			{
				deforder = new byte[]{89,77,67,75};	//ymck		
			}
			Buffer.BlockCopy(deforder,0,m_nColorOrder,0,deforder.Length);
			YInterleaveNum =2;
			LayoutType = 0;
			ManufacturerName = new byte[CoreConst.MAX_NAME_LEN];
			PrinterName = new byte[CoreConst.MAX_NAME_LEN];
			reserved = 0;
			MaxGroupNumber = 2;
			Vsd2ToVsd3 = 2;
			Vsd2ToVsd3_ColorDeep = 3;
			Only_Used_1head = 0;
			Mask_head_used = 1;
		}
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 0)]
	public struct CLEANPARA
	{
		public 	byte structsize;
		public 	byte flash;    // 1: �Ƿ��Զ����磬0/1=��/��
		public 	byte pause_gohome; // 2: ��ͣ���ԭ��,0/1=��/��
		public 	ushort flash_interval;  // 3-4: ���������Ժ���Ϊ��λ
		public 	byte longflash_beforeprint;  // 5: ��ӡǰ��ԭ����Ƿ�����һ��
		public 	byte autoClean_passInterval;     // 6: ÿ���ٸ� Pass �Զ���ϴһ����ͷ
		public 	byte autoCleanTimes;     // 7: ÿ����ϴ��ͷʱ��ϴ���£���Сֵ2
		public 	byte manualCleanTimes;  //8: �ֶ���ϴ���� 
		public 	byte longflash_passInterval;     // 9: ÿ���ٸ� Pass �Զ�����һ��
		public 	byte blowInk_passInterval;    // 10: ÿ���ٸ� Pass �Զ�ѹīһ��
		public 	ushort flashTimes;             //11,12 ����Ĵ�����B ϵͳ�����, A����
 
		public 	ushort pauseIntervalAfterClean;    //13, 14��ϴ��С��ͣ��ʱ��
		public 	ushort pauseIntervalAfterBlowInk;  //15, 16�����ʱ��
		public 	byte paraFlag;  //17: 0 means the follwoing 2 parameters will use FW setting 
//#if HEAD_EPSON_GEN5
		public 	byte autoClean_way; //0~5:default/customized/strong/normal/weak/refill.  only for AllWIn epson
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 45)]
        public byte[] reserve;//[64 - 19];   // 19-63: ����

#if false    
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=46)]
		public 	byte reserve;//[64 - 18];   // 19-63: ����
#endif
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 0)]
	public struct USB_RPT_MainUI_Param
	{
		public float PrintOrigin; //unit: inch
 
		//the following is valid only when PM send to FW.
		public int PassNum; //ָ������������Ӧ��PASS����
		public int StepModify; //������UI�ϵĲ���������
	};

	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 0)]
	public struct USB_Print_Quality
	{
		//Index: 0, OFF; 1, ECLOSION type1(gradient); 2, ECLOSION type2(Wave). �����𻯲�����PM����������Ψһ����PM����Ĳ�����
		public int PrintQuality;
	};

	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 0)]
	public struct USB_RPT_Media_Info
	{
		public float MediaOrigin;//unit: inch
		public float MediaWidth;//unit: inch
		public float Margin;//unit: inch
	};

	public class EpsonExAllParam
	{
		private string m_TagheadParameterPercent = "headParameterPercent";
//		private string m_TagFeatherPercent = "FeatherPercent";
		private string m_TagCaliConfig = "CaliConfig";
		private string m_TagEPR_FactoryData_Ex = "EPR_FactoryData_Ex";
		private string m_TagCLEANPARA ="CLEANPARA";
		private string m_TagUSB_RPT_MainUI_Param = "USB_RPT_MainUI_Param";
		private string m_TagUSB_Print_Quality = "USB_Print_Quality";
		private string m_TagUSB_RPT_Media_Info ="USB_RPT_Media_Info";

		[MarshalAs(UnmanagedType.ByValArray,SizeConst=4)]
		public sbyte[] headParameterPercent;//��ӦKCMY��˳����K,C,M,Y.
//		public int nFeatherPercent;  //�����𻯲�����PM����������Ψһ����PM����Ĳ�����
//		public CaliPrintSetting sCaliPrintSetting;
		public CaliConfig sCaliConfig;
		public EPR_FactoryData_Ex sEPR_FactoryData_Ex;
		public CLEANPARA sCLEANPARA;
		public USB_RPT_MainUI_Param sUSB_RPT_MainUI_Param;
		public USB_Print_Quality sUSB_Print_Quality;
		public USB_RPT_Media_Info sUSB_RPT_Media_Info;

		public EpsonExAllParam()
		{
			headParameterPercent = new sbyte[4];//��ӦKCMY��˳����K,C,M,Y.
//			nFeatherPercent = 0;  //�����𻯲�����PM����������Ψһ����PM����Ĳ�����
			sCaliConfig = new CaliConfig();
			sEPR_FactoryData_Ex = new EPR_FactoryData_Ex(new SPrinterProperty());
			sCLEANPARA = new CLEANPARA();
			sUSB_RPT_MainUI_Param = new USB_RPT_MainUI_Param();
			sUSB_Print_Quality = new USB_Print_Quality();
			sUSB_RPT_Media_Info = new USB_RPT_Media_Info();
		}

		unsafe public void LoadFromXml(XmlElement elem_AllParam)
		{
			XmlElement root = elem_AllParam;
			XmlElement elem_i;
			elem_i = PubFunc.GetFirstChildByName(root,m_TagheadParameterPercent);
			headParameterPercent = (sbyte[])PubFunc.SystemConvertFromXml(elem_i.OuterXml,typeof(sbyte[]));
			elem_i = PubFunc.GetFirstChildByName(root,m_TagCaliConfig);
			sCaliConfig = (CaliConfig)PubFunc.SystemConvertFromXml(elem_i.OuterXml,typeof(CaliConfig));
			elem_i = PubFunc.GetFirstChildByName(root,m_TagEPR_FactoryData_Ex);
			sEPR_FactoryData_Ex = (EPR_FactoryData_Ex)PubFunc.SystemConvertFromXml(elem_i.OuterXml,typeof(EPR_FactoryData_Ex));
			elem_i = PubFunc.GetFirstChildByName(root,m_TagCLEANPARA);
			sCLEANPARA = (CLEANPARA)PubFunc.SystemConvertFromXml(elem_i.OuterXml,typeof(CLEANPARA));
			elem_i = PubFunc.GetFirstChildByName(root,m_TagUSB_RPT_MainUI_Param);
			sUSB_RPT_MainUI_Param = (USB_RPT_MainUI_Param)PubFunc.SystemConvertFromXml(elem_i.OuterXml,typeof(USB_RPT_MainUI_Param));
			elem_i = PubFunc.GetFirstChildByName(root,m_TagUSB_Print_Quality);
			sUSB_Print_Quality = (USB_Print_Quality)PubFunc.SystemConvertFromXml(elem_i.OuterXml,typeof(USB_Print_Quality));
			elem_i = PubFunc.GetFirstChildByName(root,m_TagUSB_RPT_Media_Info);
			sUSB_RPT_Media_Info = (USB_RPT_Media_Info)PubFunc.SystemConvertFromXml(elem_i.OuterXml,typeof(USB_RPT_Media_Info));
		}
		public void SaveToXml(SelfcheckXmlDocument doc)
		{
			XmlElement root;
			root = doc.CreateElement("","EpsonExAllParam","");
			doc.AppendChild(root);
			string xml = PubFunc.SystemConvertToXml(sCaliConfig,sCaliConfig.GetType());
			xml += PubFunc.SystemConvertToXml(sEPR_FactoryData_Ex,sEPR_FactoryData_Ex.GetType());
			xml += PubFunc.SystemConvertToXml(sCLEANPARA,sCLEANPARA.GetType());
			xml += PubFunc.SystemConvertToXml(sUSB_RPT_MainUI_Param,sUSB_RPT_MainUI_Param.GetType());
			xml += PubFunc.SystemConvertToXml(sUSB_Print_Quality,sUSB_Print_Quality.GetType());
			xml += PubFunc.SystemConvertToXml(sUSB_RPT_Media_Info,sUSB_RPT_Media_Info.GetType());
			root.InnerXml = xml;
		}
	}

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct FloraParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; //"T50" "T180"
        public uint moistYPlace; //ִ����ϴʱY��λ�ã�ֻ��T50��(Ҫ��ֻ��T50���Ͳ���ʾ����0x7cb)
        public uint moistZPlace; //��ʪʱZλ��
        public uint cleanZPlace; //��ϴʱZλ��
        public uint purgeInkTime; //ѹīʱ��
        public uint purgeInkRecoverTime; //ѹī�ָ�ʱ��
        public uint suckStartPlace; //���翪ʼλ�ã�����λ����ԭ��(T50ΪY,T180ΪX)
        public uint suckEndPlace; //�������λ�ã�ֻ��T50��(Ҫ��ֻ��T50���Ͳ���ʾ����0x7cb)
        public byte bIsNeedPrepare; //�Ƿ���Ҫ��Ԥ����Һ, 0 ����Ҫ��1��Ҫ
        public byte cleanSlotNum; // ��ϴ������,һ�������ͷ����
        public ushort doWetDelay; 			// ���ж�ã�ϵͳ�Զ����뱣ʪ(unit:s)
        public float speed; //��Ԥ����Һ�ٶ� Ӣ��/��
        public uint preOff;					//��Ԥ����Һλ�õ���ӡԭ��ƫ��(ֻ��T50���Ͳ���ʾ)
        public uint cleanSlotSpace; // ��ϴ�ۼ��,һ��Ϊ��ͷ���
        public uint cleanMotorSpeed;			// T180, ��ϴ����ٶ�
        public float speedBack;  //���괦��Һ�����ٶ� Ӣ��/��
        public uint scraperStart1;			// ��һ�Ź�Ƭ��ʼλ��
        public uint scraperStart2;			// �ڶ��Ź�Ƭ��ʼλ��

        public FloraParam(object oo)
        {
            Flag = new char[4];
            moistYPlace = 0;
            moistZPlace = 0;
            cleanZPlace = 0;
            purgeInkTime = 0;
            purgeInkRecoverTime = 0;
            suckStartPlace = 0;
            suckEndPlace = 0;
            bIsNeedPrepare = 0;
            doWetDelay =120;
            speed = 0;
            preOff = 0;
            cleanSlotNum = 0;
            cleanSlotSpace = 0;
            cleanMotorSpeed = 0;
            speedBack = 0;
            scraperStart1 = 0;
            scraperStart2 = 0;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct WetParam
    {
        /// <summary>
        /// 'WETP'
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public char[] Flag; //!< 'WETP'

        /// <summary>
        ///  ����֮����Ч�ֽ���(������������β��Ԥ���ֽ�
        /// </summary>
        public ushort ActiveLen; //!< ����֮����Ч�ֽ���(������������β��Ԥ���ֽ�)
        #region �Զ���ʪ
        /// <summary>
        ///  �Զ���ʪʹ��
        /// </summary>
        public byte Enable; //!< �Զ���ʪʹ��

        /// <summary>
        ///  bit0~2 �ֱ��ʾX Y Zλ����Ч�ԣ���1��Ч
        /// </summary>
        public byte PosMask; //!< bit0~2 �ֱ��ʾX Y Zλ����Ч�ԣ���1��Ч

        public int XPos; //!< 
        public int YPos; //!< 
        public int ZPos; //!< 

        /// <summary>
        /// �ȴ�ʱ��,MS
        /// </summary>
        public uint WaitTime; //!< �ȴ�ʱ��,MS
        #endregion

        #region �Զ���ȱʡλ��
        /// <summary>
        /// ��ȱʡλ����ʱʹ��
        /// </summary>
        public byte DefaultBackDelayEnable;
        /// <summary>
        ///  bit0~2 �ֱ��ʾX Y Zλ����Ч�ԣ���1��Ч
        /// </summary>
        public byte DefaultBackPosMask;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] rev;
        public int DefaultBackXPos; //!< 
        public int DefaultBackYPos; //!< 

        /// <summary>
        /// �ȴ�ʱ��,MS
        /// </summary>
        public uint BackDefaultPosWaitTime; //!< �ȴ�ʱ��,MS
        #endregion

        public int cover4Place; //��ͷ������ͷ���ƶ�λ��//�µ���
    }

    /// <summary>
    /// ׿չ����ר�ò���
    /// </summary>
    public struct ZhuoZhanParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; //
        public uint zWorkPos; //!< Z2������λ��
    }

    public struct YStepSpeedParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; //
        public int ySpeed;   
    }

    public struct LingFengParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; //
        //public byte ZEnable;
        public byte WetEnable;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 57)]
        public byte[] rev;
    }

    /// <summary>
    /// ������ͷ��ϴ����
    /// </summary>
    public struct PQCleanParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; ////!< ��ϴ���('P', 'Q', 'C', '\0')
        public int PressInkTime;		//!< ѹīʱ�� IO�л���ѹ
        public int XStartPos;		//!< x ��ϴ��ʼλ��
        public int YDistance;		//!< x ��ϴ���г�
        public int XAdjust;		//!< Z��ϴ�߶�
        public byte CleanNum;		//!< ��ϴ����
        public byte bAutoCap; //�Ƿ��Զ���ʪ
        public byte ScrapTimes; //��ī����
        public byte rev;
        public int CleanZPos; //īջ��ϴλ��
        public int WetZPos; //īջ��ʪλ��
        public int AutoCapWaitTime;		//!< �Զ���ʪ�ȴ�ʱ��(s)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (62-36))]
        public byte[] Reserve;
    }

    /// <summary>
    /// Gma��ͷ��ϴ����
    /// </summary>
    public struct GmaCleanParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; ////!< ��ϴ��� GZ_CLOTH_M_CLEAN	MAKE_4CHAR_CONST_BE('C', 'L', 'C', 'N')
        public int XStartPos;		//!< x ��ϴ��ʼλ��
        public int XDistance;		//!< x ��ϴÿ�ż��
        public int ZCleanPos;		//!< Z��ϴ�߶�
        public int ScraperPos;		//!< ��Ƭ����
        public byte CleanRowNum;		//!< ��ϴ����
        public byte CleanTime;		//!< ��ϴ����
        public ushort rev_54;
        public int ZCarryCleanPos;//�³�Z��ϴ�߶�
        public int YCarryCleanPos;//�³�Y��ϴλ��
    }

    /// <summary>
    /// ���������� �Զ�/�ֶ�����
    /// </summary>
    public struct FloraFlatParam_tag {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag;            // 'FFUF'
        public UInt32 Yadjust_Auto;    //�Զ�������ʱ��е��Yƫ�ƣ����壩
        public UInt32 Yadjust_Manual;  //�ֶ�������ʱ��е��Yƫ�ƣ����壩
        public byte BAutoLoadDeal;             //�Ƿ��ӡʱ�Զ������ϣ�0��1��
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] rev;
    }

    public struct HlcCleanParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; ///MAKE_4CHAR_CONST_BE('H', 'L', 'C', 'N')
        public int PressInkTime;		//!< ѹīʱ�� IO�л���ѹ
        public int RecoveryInkTime;		//!< ѹī��ָ�ʱ�� IO�л���ѹ
        public int XPressInkPos;		//!< ��ϴѹīXλ��
        public int ZCleanPos;			//!< ��ϴZ�߶�
        public int YCleanPos;			//!< ��ϴY�г�
        public int YCleanSpeed;			//!< ��ϴY�ٶ�

    }
    /// <summary>
    /// ��ͷ���֣��ף��ʣ�����
    /// </summary>
    public enum WhiteVarnishLayout
    {
        WFCMVN,		//��Զ��������
        WNCMVF,		//�׽�������Զ
        WFCMVF,		//��Զ������Զ
        WNCFVN,		//�׽���Զ����
        CWC,        //�ʰײ�
        WCC,        //�ײʲ�
    }

    public class EpsonLCD
	{
		public const int MaxGroupNumber = 8;
				
		/// <summary>
		/// ��ӡDotCheck
		///ͨ��USB EP0��������OUT��
		///reqCode��0x7F��index��2��value��0��
		///��һ��Byte��VSD Model,�Ϸ�ֵ��1-4��
		///�ڶ���Byte�Ƿֱ���,�Ϸ�ֵ��1-2-3������Ϊ720,360,240��default��360��
		///�����ĸ�Byte����ʼλ��,��λ������DPI��һ���㡣
		/// </summary>
		public static void PrintDotCheck(CaliPrintSetting cps)
		{
#if false
			byte[] subval = new byte[9];
			uint bufsize = (uint)subval.Length;
			subval[0] =(byte)(cps.type);
			subval[1] =(cps.VSDModel);
			subval[2] =cps.DotSetting;
			subval[3] =(cps.DPIModel);
			byte[] startp = BitConverter.GetBytes(cps.StartPos);
			Buffer.BlockCopy(startp, 0, subval, 4, 2);
			subval[6] = cps.MediaType;
			subval[7] = (cps.PassNum);
			subval[8] = cps.Option;
			if(CoreInterface.SetEpsonEP0Cmd(0x7F,subval,ref bufsize,3,0) == 0)
			{
				Debug.Assert(false,"SetPrint_Quality fialed!");
			}
#endif
		}

		public static void GetCalibrationSetting(ref SCalibrationSetting scali,bool isForDisplay)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(SCalibrationSetting))+4];

			uint tatolLen = (uint)val.Length;
			ushort adr = 0;
			uint maxLenonetime = 64;
			while (tatolLen > 0)
			{
				byte[] subval = new byte[maxLenonetime];
				uint readlen = maxLenonetime;
				if (tatolLen < maxLenonetime)
					readlen = tatolLen;
				subval = new byte[readlen];
				
				bool bfiald = false;
				if(isForDisplay)
					bfiald = CoreInterface.GetEpsonEP0Cmd(0x7F,subval,ref readlen,12,adr) == 0; 
				else
					bfiald = CoreInterface.GetEpsonEP0Cmd(0x7F,subval,ref readlen,8,adr) == 0; 

				if(bfiald)
				{
					Debug.Assert(false,"GetCalibrationSetting fialed!");
					break;
				}				
//				Thread.Sleep(1000);
//				subval = new byte[readlen];
				Buffer.BlockCopy(subval, 0, val, adr, subval.Length);
				adr += (ushort)subval.Length;
				tatolLen -= (ushort)subval.Length;
			}

			byte[] subval1 = new byte[Marshal.SizeOf(typeof(SCalibrationSetting))];
			Buffer.BlockCopy(val,4,subval1,0,subval1.Length);
            scali = (SCalibrationSetting)SerializationUnit.BytesToStruct(subval1, typeof(SCalibrationSetting));
		}

		public static void SetCalibrationSetting(SCalibrationSetting scali)
		{
#if false
			byte[] val = new byte[Marshal.SizeOf(typeof(SCalibrationSetting))+4];
			uint bufsize = (uint)val.Length;
			byte[] lenbuf = BitConverter.GetBytes((ushort)bufsize);
			Buffer.BlockCopy(lenbuf,0,val,0,lenbuf.Length);
			byte[] versionbuf = BitConverter.GetBytes((ushort)0x101);
			Buffer.BlockCopy(versionbuf,0,val,2,versionbuf.Length);
			byte[] subval = SerializationUnit.StructToBytes(scali);
			Buffer.BlockCopy(subval,0,val,4,subval.Length);

			uint totalsize = bufsize;
			ushort addr = 0;
			while (totalsize > 0)
			{
				uint size = 64;
				if (totalsize < size)
					size = totalsize;
				totalsize -= size;

				byte[] Buf = new byte[size];
				Buffer.BlockCopy(val, (int)addr, Buf, 0, (int)size);
				if (CoreInterface.SetEpsonEP0Cmd(0x7F,Buf,ref size,12,addr)==0)
				{
					Debug.Assert(false,"SetCalibrationSetting fialed!");
					return;
				}
//				Thread.Sleep(1000);
				addr += (ushort)(size);
			}
#endif
		}

		public static void GetCaliConfig(ref CaliConfig cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(CaliConfig))];
			uint bufsize = (uint)val.Length;
			if(CoreInterface.GetEpsonEP0Cmd(0x7F,val,ref bufsize,7,0) == 0)
			{
				Debug.Assert(false,"GetCaliConfig fialed!");
			}
            cc = (CaliConfig)SerializationUnit.BytesToStruct(val, typeof(CaliConfig));
		}

        /// <summary>
        /// ������ԡ�KM512_8H_600_Roll_solvent_5.1_VE_0x0B���ı������ļ���ԭ���߼���ֻ�м�⵽��ֽ�Ŵ�ӡ,�����Ļ���֮ǰ���߼�
        /// </summary>
        /// <returns></returns>
        public static bool CheckHasPaper()
        {
            if (SPrinterProperty.IsGongZengNew())
            {
                byte[] val=new byte[3];
                uint bufsize=(uint)val.Length;
                if (CoreInterface.GetEpsonEP0Cmd(0x82, val, ref bufsize, 0, 6) == 0)
                {
                    return true;
                }
                try
                {
                    BitArray bit = new BitArray(BitConverter.GetBytes(val[2]));
                    return !bit.Get(4);
                }
                catch 
                {
                    return true;
                }
            }
            //��������֮ǰ�����ļ��߼�
            return true;
        }


		public static void SetCaliConfig(CaliConfig cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(CaliConfig))];
			uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
			if(CoreInterface.SetEpsonEP0Cmd(0x7F,val,ref bufsize,7,0) == 0)
			{
				Debug.Assert(false,"SetCaliConfig fialed!");
			}
		}

		public static int GetEPR_FactoryData_Ex(ref EPR_FactoryData_Ex cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(EPR_FactoryData_Ex))];
			uint bufsize = (uint)val.Length;
			int ret =CoreInterface.GetEpsonEP0Cmd(0x7F,val,ref bufsize,6,0);
			if( ret == 0)
			{
				Debug.Assert(false,"GetEPR_FactoryData_Ex fialed!");
			}
            cc = (EPR_FactoryData_Ex)SerializationUnit.BytesToStruct(val, typeof(EPR_FactoryData_Ex));
			return ret;
		}

		public static int SetEPR_FactoryData_Ex(EPR_FactoryData_Ex cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(EPR_FactoryData_Ex))];
			uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
			int ret = CoreInterface.SetEpsonEP0Cmd(0x7F,val,ref bufsize,6,0);
			if(ret == 0)
			{
				Debug.Assert(false,"SetEPR_FactoryData_Ex fialed!");
			}
			return ret;
		}
		
		/// <summary>
		/// cmd = 0x64;value = 0;index = 0; 
		/// </summary>
		/// <param name="cc"></param>
		/// 
		public static void GetCleaningOption(ref CLEANPARA cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(CLEANPARA))];
			uint bufsize = (uint)val.Length;
			if(CoreInterface.GetEpsonEP0Cmd(0x64,val,ref bufsize,0,0) == 0)
			{
				Debug.Assert(false,"GetCleaningOption fialed!");
			}
            cc = (CLEANPARA)SerializationUnit.BytesToStruct(val, typeof(CLEANPARA));
		}

		/// <summary>
		/// cmd = 0x64;value = 0;index = 0; 
		/// </summary>
		/// <param name="cc"></param>
		public static void SetCleaningOption(CLEANPARA cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(CLEANPARA))];
			uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
			if(CoreInterface.SetEpsonEP0Cmd(0x64,val,ref bufsize,0,0) == 0)
			{
				Debug.Assert(false,"SetCleaningOption fialed!");
			}
		}

		/// <summary>
		/// read by EP6:
		///ͨ��USB EP6��������IN��
		///#define EP6_CMD_T_MAINUI        0x2 //see struct USB_RPT_MainUI_Param
		///read by EP0:
		///ͨ��USB EP0��������IN��
		///reqCode��0x7F��value��9��
		/// </summary>
		/// <param name="cc"></param>
		public static void GetMainUI_Param(ref USB_RPT_MainUI_Param cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(USB_RPT_MainUI_Param))];
			uint bufsize = (uint)val.Length;
			if(CoreInterface.GetEpsonEP0Cmd(0x7F,val,ref bufsize,9,0) == 0)
			{
				Debug.Assert(false,"GetMainUI_Param fialed!");
			}
			cc = (USB_RPT_MainUI_Param)SerializationUnit.BytesToStruct(val,typeof(USB_RPT_MainUI_Param));
		}

		/// <summary>
		/// two items at mainUI need refresh: Print Origin and step modify.
		///Set by EP0:
		///ͨ��USB EP0��������OUT��
		///reqCode��0x7F��value��9��
		/// </summary>
		/// <param name="cc"></param>
		public static void SetMainUI_Param(USB_RPT_MainUI_Param cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(USB_RPT_MainUI_Param))];
			uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
			if(CoreInterface.SetEpsonEP0Cmd(0x7F,val,ref bufsize,9,0) == 0)
			{
				Debug.Assert(false,"SetMainUI_Param fialed!");
			}

		}

		/// <summary>
		///read by EP0:
		///ͨ��USB EP0��������IN��
		///reqCode��0x7F��value��10
		/// </summary>
		/// <param name="cc"></param>
		public static void GetPrint_Quality(ref USB_Print_Quality cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(USB_Print_Quality))];
			uint bufsize = (uint)val.Length;
			if(CoreInterface.GetEpsonEP0Cmd(0x7F,val,ref bufsize,10,0) == 0)
			{
				Debug.Assert(false,"GetPrint_Quality fialed!");
			}
			cc = (USB_Print_Quality)SerializationUnit.BytesToStruct(val,typeof(USB_Print_Quality));
		}

		/// <summary>
		/// Set by EP0:
		///ͨ��USB EP0��������OUT��
		///reqCode��0x7F��value��10��
		/// </summary>
		/// <param name="cc"></param>
		public static void SetPrint_Quality(USB_Print_Quality cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(USB_Print_Quality))];
			uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
			if(CoreInterface.SetEpsonEP0Cmd(0x7F,val,ref bufsize,10,0) == 0)
			{
				Debug.Assert(false,"SetPrint_Quality fialed!");
			}
			
		}
		
		/// <summary>
		/// 		read by EP6: 
		///ͨ��USB EP6��������IN��
		///read by EP0:
		///ͨ��USB EP0��������IN��
		///reqCode��0x7F��value��11��
		/// </summary>
		/// <param name="cc"></param>
		public static void GetMedia_Info(ref USB_RPT_Media_Info cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(USB_RPT_Media_Info))];
			uint bufsize = (uint)val.Length;
			if(CoreInterface.GetEpsonEP0Cmd(0x7F,val,ref bufsize,11,0) == 0)
			{
				Debug.Assert(false,"GetMedia_Info fialed!");
			}
			cc = (USB_RPT_Media_Info)SerializationUnit.BytesToStruct(val,typeof(USB_RPT_Media_Info));
		}

		/// <summary>
		/// Set by EP0:
		///ͨ��USB EP0��������OUT��
		///reqCode��0x7F��value��11��
		/// </summary>
		/// <param name="cc"></param>
		public static void SetMedia_Info(USB_RPT_Media_Info cc)
		{
			byte[] val = new byte[Marshal.SizeOf(typeof(USB_RPT_Media_Info))];
			uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
			if(CoreInterface.SetEpsonEP0Cmd(0x7F,val,ref bufsize,11,0) == 0)
			{
				Debug.Assert(false,"SetMedia_Info fialed!");
			}
		}

		/// <summary>
		/// �ڣУ��У��ţУӣϣ����Լ�ר�ŵģ����parameter. �����Ʊ����ͷ�ĵ�ѹ������ֻ������һ�������ٷֱȣ�ȡֵ��Χ��-50��+50.
		///b. Get EPSON head parameter.
		///reqCode��0x5C��index��8��������6�ֽڡ������ǣ�
		///��һ���ֽ���0��
		///�ڶ����ֽ���0x5C;
		///�������ǣ�
		///INT8S headParameterPercent[4];  //��ӦKCMY��˳����K,C,M,Y.
		/// </summary>
		/// <param name="cc"></param>
		public static void GetHeadparameter(ref sbyte[] cc)
		{
			cc = new sbyte[4];
			byte[] val = new byte[6];
			uint bufsize = (uint)val.Length;
			if(CoreInterface.GetEpsonEP0Cmd(0x5C,val,ref bufsize,0,8) != 0)
				Buffer.BlockCopy(val,2,cc,0,cc.Length);
		}

		/// <summary>
		/// a. set  EPSON head parameter. ��������ᱣ����EPSONͷ���ϡ�
		///reqCode��0x5C��index��8��������4�ֽڡ�������
		///INT8S headParameterPercent[4];  //��ӦKCMY��˳����K,C,M,Y.
		/// </summary>
		/// <param name="cc"></param>
		public static void SetHeadparameter(sbyte[] cc)
		{
			uint bufsize = (uint)cc.Length;
			byte[] val = new byte[bufsize];
			Buffer.BlockCopy(cc,0,val,0,cc.Length);
			if(CoreInterface.SetEpsonEP0Cmd(0x5C,val,ref bufsize,0,8) == 0)
			{
				Debug.Assert(false,"SetHeadparameter fialed!");
			}
		}

		public static void GetSTEP(ref int nstep,ref int npass)
		{
			byte[] val = new byte[3];
			uint bufsize = (uint)val.Length;
			if(CoreInterface.GetEpsonEP0Cmd(0x7D,val,ref bufsize,0,0) != 0)
			{
				nstep = (int)BitConverter.ToInt16(val,0);
				npass = (int)val[2];
			}
		}

		public static void SaveEpsonCaliParaToFile(string path)
		{
			string msginfo = string.Empty;
			try
			{
				byte[] len = new byte[2];
				uint rlen = (uint)len.Length;
				int ret = CoreInterface.GetEpsonEP0Cmd(0x7F,len,ref rlen,15,0);
				if(ret != 0)
				{
					rlen = (uint)BitConverter.ToInt16(len,0);
					byte[] val = new byte[rlen];

					uint tatolLen = (uint)val.Length;
					ushort adr = 0;
					uint maxLenonetime = 64;
					while (tatolLen > 0)
					{
						byte[] subval = new byte[maxLenonetime];
						uint readlen = maxLenonetime;
						if (tatolLen < maxLenonetime)
							readlen = tatolLen;
						subval = new byte[readlen];
				
						bool bfiald = CoreInterface.GetEpsonEP0Cmd(0x7F,subval,ref readlen,16,adr) == 0; 

						if(bfiald)
						{
							msginfo = ResString.GetResString("EpsonLoadCali_Faild");
							return;
						}				
						//				Thread.Sleep(1000);
						//				subval = new byte[readlen];
						Buffer.BlockCopy(subval, 0, val, adr, subval.Length);
						adr += (ushort)subval.Length;
						tatolLen -= (ushort)subval.Length;
					}

					FileStream fs = new FileStream(path,FileMode.Create,FileAccess.Write,FileShare.Read);
					fs.Write(val,0,val.Length);
					fs.Flush();
					fs.Close();
					msginfo = ResString.GetResString("EpsonSaveCali_Ok");
				}
				else
				{
					msginfo = ResString.GetResString("EpsonLoadCali_GetSizeFaild");
				}
			}
			catch(Exception ex)
			{
				msginfo = ex.Message;
			}
			finally
			{
				MessageBox.Show(msginfo,ResString.GetProductName(),MessageBoxButtons.OK);				
			}
		}
        public static bool IsNewDurationInkCmd() {
            byte[] val = new byte[64];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x54, val, ref bufsize, 0, 2) == 0) {
                return false;
            }
            return ((val[4] & 0x40) > 0);
        }

        public static bool GetDurationInk(ref long[] result, int mbid = CoreConst.DefaultMbid) {
            byte[] val = new byte[66];
            uint bufsize = (uint)val.Length;
            result = new long[8];
            if (CoreInterface.GetEpsonEP0Cmd(0x52, val, ref bufsize, 2, 1) == 1) {
                for (int i = 0; i < 8; i++) {
                    result[i] = BitConverter.ToInt64(val, 2 + 8 * i);
                }
                return true;
            } else {
                return false;
            }
        }

		public static void LoadEpsonCaliParaFromFile(string path)
		{
			
			string msginfo = string.Empty;
			try
			{
				byte[] len = new byte[2];
				uint rlen = (uint)len.Length;
				int ret = CoreInterface.GetEpsonEP0Cmd(0x7F,len,ref rlen,15,0);
				if(ret != 0)
				{
					rlen = (uint)BitConverter.ToInt16(len,0);
					FileStream fs = new FileStream(path,FileMode.Open,FileAccess.ReadWrite,FileShare.ReadWrite);
					//				if( fs.Length != rlen)
					//				{
					//					MessageBox.Show("file format diamatch with current setting.");
					//					fs.Close();
					//					return;
					//				}
					byte[] val = new byte[rlen];
					fs.Read(val,0,(int)fs.Length);
					fs.Close();
					uint bufsize = (uint)val.Length;
					uint totalsize = bufsize;
					ushort addr = 0;
					while (totalsize > 0)
					{
						uint size = 64;
						if (totalsize < size)
							size = totalsize;
						totalsize -= size;

						byte[] Buf = new byte[size];
						Buffer.BlockCopy(val, (int)addr, Buf, 0, (int)size);
						if (CoreInterface.SetEpsonEP0Cmd(0x7F,Buf,ref size,16,addr)==0)
						{
							msginfo = ResString.GetResString("EpsonLoadCali_Faild");
							return;
						}
						//				Thread.Sleep(1000);
						addr += (ushort)(size);
					}
					byte[] retbuf = new byte[64];
					uint buflen = (uint)retbuf.Length;
					if(CoreInterface.GetEpsonEP0Cmd(0x7F,retbuf,ref buflen,15,2)!=0)
					{
						/*
						 *  ���յ� 0  ����ɹ�
			1   �����İ汾���ڻ����汾
			2   �����İ汾��֧�֣����ڵ�ǰ��߰汾       
			3    setuplength���� 
	*/
						byte retsult = retbuf[0];
						switch(retsult)
						{
							case 0:
								msginfo = ResString.GetResString("EpsonLoadCali_Ok");
								break;
							case 1:
								msginfo = ResString.GetResString("EpsonLoadCali_VerTooOld");
								break;
							case 2:
								msginfo = ResString.GetResString("EpsonLoadCali_VerTooLatest");
								break;
							case 3:
								msginfo = ResString.GetResString("EpsonLoadCali_SizeError");
								break;
							case 4:
								msginfo = ResString.GetResString("EpsonLoadCali_FlagError");//flag���� 
								break;
						}
					}
					else
					{
						msginfo = ResString.GetResString("EpsonLoadCali_GetRetFaild");
					}
				}
				else
				{
					msginfo = ResString.GetResString("EpsonLoadCali_GetSizeFaild");
				}
			}
			catch(Exception ex)
			{
				msginfo = ex.Message;
			}
			finally
			{
				MessageBox.Show(msginfo,ResString.GetProductName(),MessageBoxButtons.OK);				
			}
		}
		public static int PrepareReadBaiscWave(ushort id)
		{
			byte[] val = new byte[1];
			uint bufsize = (uint)val.Length;
			int ret = CoreInterface.SetEpsonEP0Cmd(0x7F, val, ref bufsize, 25, id);
			if (ret == 0)
			{
				throw new Exception("Prepare read baisc wave fialed! cmd is 0x7F, value is 25");
			}
			return ret;
		}

		public static int GetWaveDataTotalLength()
		{
			return GetLength(22);
		}
		/// <summary>
		/// ��ȡ���������ܳ���
		/// </summary>
		public static int GetWaveNameTotalLength()
		{
			return GetLength(34);
		}
		/// <summary>
		/// ��ȡͨ�������ܳ���
		/// </summary>
		public static int GetChannelNameTotalLength()
		{
			return GetLength(35);
		}
		/// <summary>
		/// ��ȡӳ����ܳ���
		/// </summary>
		public static int GetWaveMappingTotalLength()
		{
			return GetLength(36);
		}

		private static int GetLength(ushort Cmd)
		{
			byte[] subval = new byte[4];
			uint bufsize = (uint)subval.Length;
			ushort index = 0;
			if (CoreInterface.GetEpsonEP0Cmd(0x7F, subval, ref bufsize, Cmd, index) == 0)
			{
				//throw new Exception(string.Format("GetEpsonEP0Cmd Failed. cmd is 0x7F, value is {0}", Cmd));
                MessageBox.Show(string.Format("GetEpsonEP0Cmd Failed. cmd is 0x7F, value is {0}", Cmd),ResString.GetProductName(), MessageBoxButtons.YesNo, MessageBoxIcon.Error);
			}
			return BitConverter.ToInt32(subval, 0);
		}


		public static byte[] GetWaveData(int Length)
		{
			return GetWaveInfo(Length, 22);
		}
		/// <summary>
		/// ��ȡ��������
		/// </summary>
		public static byte[] GetWaveNameData(int Length)
		{
			return GetWaveInfo(Length, 34);
		}
		/// <summary>
		/// ��ȡͨ������
		/// </summary>
		public static byte[] GetChannelNameData(ushort index)
		{
			byte[] subval = new byte[20];
			uint bufsize = (uint)subval.Length;
			if (CoreInterface.GetEpsonEP0Cmd(0x7F, subval, ref bufsize, 35, index) == 0)
			{
				throw new Exception("GetEpsonEP0Cmd Failed. cmd is 0x7F, value is 35");
			}

			return subval;
		}
		/// <summary>
		/// ��ȡӳ�������
		/// </summary>
		public static byte[] GetWaveMappingData(int Length)
		{
			return GetWaveInfo(Length, 36);
		}

		private static byte[] GetWaveInfo(int Length, ushort Cmd)
		{
			byte[] WaveData = new byte[Length];

			int PacketCount = (int)Math.Ceiling((double)(float)Length / 25.0f);

			int ByteLeft = Length;
			int ByteReceive = 0;
			for (ushort i = 1; i <= PacketCount; i++)
			{
				int ReceiveCount = ByteLeft >= 25 ? 25 : ByteLeft;
				byte[] subval = new byte[ReceiveCount];

				uint bufsize = (uint)subval.Length;
				if (CoreInterface.GetEpsonEP0Cmd(0x7F, subval, ref bufsize, Cmd, i) == 0)
				{
					throw new Exception(string.Format("GetEpsonEP0Cmd Failed. cmd is 0x7F, value is {0}", Cmd));
				}
				else
				{
					Array.Copy(subval, 0, WaveData, ByteReceive, subval.Length);

					ByteLeft -= ReceiveCount;
					ByteReceive += ReceiveCount;
				}
			}

			return WaveData;
		}

		public static int SetWaveDataTotalLength(int Length)
		{
			return SetLength(Length, 26);
		}

		public static int SetAmplitudeTotalLength(int Length)
		{
			return SetLength(Length, 27);
		}

		public static int SetNewAddNodeTotalLength(int Length)
		{
			return SetLength(Length, 28);
		}
		/// <summary>
		///  ���������޸��ܳ����趨
		/// </summary>
		public static int SetWaveNameTotalLength(int Length)
		{
			return SetLength(Length, 29);
		}
		/// <summary>
		/// ����ӳ����ܳ����趨
		/// </summary>
		public static int SetWaveMappingTotalLength(int Length)
		{
			return SetLength(Length, 43);
		}

		public static int WriteWaveData(byte[] WaveData)
		{
			return WriteWaveInfo(WaveData, 26);
		}

		public static int WriteAmplitudeData(byte[] AmplitudeData)
		{
			return WriteWaveInfo(AmplitudeData, 27);
		}

		public static int WriteNewAddNodeData(byte[] Data)
		{
			return WriteWaveInfo(Data, 28);
		}

		/// <summary>
		/// ���������޸������·�
		/// </summary>
		public static int WriteWaveNameData(byte[] Data)
		{
			return WriteWaveInfo(Data, 29);
		}
		/// <summary>
		/// ����ӳ��������·�
		/// </summary>
		public static int WriteWaveMappingData(byte[] Data)
		{
			return WriteWaveInfo(Data, 43);
		}

		private static int WriteWaveInfo(byte[] Info, ushort Cmd)
		{
			int PacketCount = (int)Math.Ceiling((double)(float)Info.Length / 25.0f);

			int ByteLeft = Info.Length;
			int ByteSent = 0;
			for (ushort i = 1; i <= PacketCount; i++)
			{
				int SendDataCount = ByteLeft >= 25 ? 25 : ByteLeft;
				byte[] subval = new byte[SendDataCount];

				Array.Copy(Info, ByteSent, subval, 0, SendDataCount);

				uint bufsize = (uint)subval.Length;
				if (CoreInterface.SetEpsonEP0Cmd(0x7F, subval, ref bufsize, Cmd, i) == 0)
				{
					throw new Exception(string.Format("SetEpsonEP0Cmd Failed. cmd is 0x7F, value is {0}", Cmd));
				}
				else
				{
					ByteLeft -= SendDataCount;
					ByteSent += SendDataCount;
				}
			}

			return Info.Length;
		}

		private static int SetLength(int Length, ushort Cmd)
		{
			byte[] subval = BitConverter.GetBytes(Length);
			uint bufsize = (uint)subval.Length;
			ushort index = 0;

			int ret = CoreInterface.SetEpsonEP0Cmd(0x7F, subval, ref bufsize, Cmd, index);
			if (ret == 0)
			{
				throw new Exception(string.Format("SetEpsonEP0Cmd failed. cmd is 0x7F, value is {0}", Cmd));
			}

			return ret;
		}
		/// <summary>
		/// Ԥ����������
		/// </summary>
		public static void PrepareReadWaveName()
		{
			PrepareReadCommand(42);
		}
		/// <summary>
		/// Ԥ������ӳ���
		/// </summary>
		public static void PrepareReadWaveMapping()
		{
			PrepareReadCommand(44);
		}

		private static void PrepareReadCommand(ushort Cmd)
		{
			byte[] val = new byte[1];
			uint bufsize = (uint)val.Length;
			ushort index = 0;
			if (CoreInterface.SetEpsonEP0Cmd(0x7F, val, ref bufsize, Cmd, index) == 0)
			{
				throw new Exception(string.Format("SetEpsonEP0Cmd Failed. cmd is 0x7F, value is {0}", Cmd));
			}
			return;
		}


        /// <summary>
        /// ��ȡ˫������
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetDoubleYAxis_Info(ref DOUBLE_YAXIS cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(DOUBLE_YAXIS))+2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x78, val, ref bufsize, 0, 1) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(DOUBLE_YAXIS))];
            DOUBLE_YAXIS myStruct = new DOUBLE_YAXIS();
            Array.Copy(val, 2, structData, 0, structData.Length);
            cc = (DOUBLE_YAXIS)SerializationUnit.BytesToStruct(structData, typeof(DOUBLE_YAXIS));
            return true;
        }

        /// <summary>
        /// ����˫������
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetDoubleYAxis_Info(DOUBLE_YAXIS cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(DOUBLE_YAXIS))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x78, val, ref bufsize, 0, 1) == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Get FW Version
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
	    public static bool GetFWVersionInfo(ref byte[] cc)
	    {
            cc = new byte[16];
            byte[] val = new byte[64];
	        uint valsize = (uint) val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x54, val, ref valsize, 0, 1) == 0)
            {
                return false;
            }
            Buffer.BlockCopy(val, 2, cc, 0, cc.Length);
	        return true;
	    }

	    /// <summary>
        /// ��ȡ�¾�̩����������
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetNktParam(ref NKTParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(NKTParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x90) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(NKTParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (NKTParam)SerializationUnit.BytesToStruct(structData, typeof(NKTParam));

            bool flagCheck = true;
            char[] flag = new char[] { 'N', 'K', 'T', '\0' };
            for (int i = 0; i < cc.Flag.Length; i++)
            {
                if (cc.Flag[i] != flag[i])
                {
                    flagCheck = false;
                    break;
                }
            }
            return flagCheck;
        }

        /// <summary>
        /// �����¾�̩����������
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetNktParam(NKTParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(NKTParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x90) == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ��ȡ����������ϴλ�ò���
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetTlhgParam(ref TlhgParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(TlhgParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x40) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(TlhgParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (TlhgParam)SerializationUnit.BytesToStruct(structData, typeof(TlhgParam));
            bool flagCheck = true;
            char[] flag = new char[] { 'T', 'L', 'H', 'G' };
            for (int i = 0; i < cc.Flag.Length; i++)
            {
                if (cc.Flag[i] != flag[i])
                {
                    flagCheck = false;
                    break;
                }
            }
            return flagCheck;
        }

        /// <summary>
        /// ��������������ϴλ�ò���
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetTlhgParam(TlhgParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(TlhgParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x40) == 0)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// ��ȡ3D��ӡ���ò���
        /// </summary>
        /// <param name="cc"></param>
        public static bool Get3DPrint_Info(ref S_3DPrint cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(S_3DPrint)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x10) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(S_3DPrint))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (S_3DPrint)SerializationUnit.BytesToStruct(structData, typeof(S_3DPrint));

            byte[] byteflag = BitConverter.GetBytes(cc.Flag);
            if (Encoding.ASCII.GetString(byteflag) != "FeH\0")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ����3D��ӡ���ò���
        /// </summary>
        /// <param name="cc"></param>
        public static bool Set3DPrint_Info(S_3DPrint cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(S_3DPrint))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x10) == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public static bool GetGZClothMotionParam(ref GZClothMotionParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(GZClothMotionParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0xE3) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(GZClothMotionParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (GZClothMotionParam)SerializationUnit.BytesToStruct(structData, typeof(GZClothMotionParam));

            string flag=new string(cc.Flag);
            if (flag != "CLOT")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ��ȡ����ֽ�������
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetGZCardboardParam(ref GzCardboardParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(GzCardboardParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 2) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(GzCardboardParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (GzCardboardParam)SerializationUnit.BytesToStruct(structData, typeof(GzCardboardParam));
            string flag = new string(cc.Flag);
            if (flag != "GZCB")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ���ù���ֽ�������
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetGZCardboardParam(GzCardboardParam cc)
        {
            cc.Flag =new char[]{'G','Z','C','B'};
            byte[] val = new byte[Marshal.SizeOf(typeof(GzCardboardParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 2) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetGZClothMotionParam(GZClothMotionParam cc)
        {
            cc.Flag = new char[] { 'C', 'L', 'O', 'T' };
            byte[] val = new byte[Marshal.SizeOf(typeof(GZClothMotionParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0xE3) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// ������Colorjet ���һ��������ó�flat
        /// </summary>
        /// <returns></returns>
        public static bool SetFlat()
        {
            byte[] val = new byte[] { 0x01 };
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x82, val, ref bufsize, 0, 0x04) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// ������Colorjet ���һ��������ó�roll
        /// </summary>
        /// <returns></returns>
        public static bool SetRoll()
        {
            byte[] val = new byte[] { 0x02 };
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x82, val, ref bufsize, 0, 0x04) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// ������Colorjet ���һ�������ȡ��ǰģʽ
        /// </summary>
        /// <returns></returns>
        public static bool GetRollOrFlat(ref byte selectedType)
        {
            byte[] val = new byte[3];
            uint bufsize = (uint)val.Length;
            int ret = CoreInterface.GetEpsonEP0Cmd(0x82, val, ref bufsize, 0, 0x04);
            if (ret != 0)
            {
                if (bufsize == 3)
                {
                    selectedType = val[2];
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// �շŲ����ƿ�
        /// </summary>
        /// <returns></returns>
        public static bool RetractableClothBegin()
       {
           byte[] val = new byte[] { (byte) 'B' };
           uint bufsize = (uint)val.Length;
           if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0xE4) == 0)
           {
               return false;
           }
           return true;
       }
        /// <summary>
        /// �շŲ����ƹ�
        /// </summary>
        /// <returns></returns>
       public static bool RetractableClothEnd()
       {
           byte[] val = new byte[] { (byte)'E' };
           uint bufsize = (uint)val.Length;
           if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0xE4) == 0)
           {
               return false;
           }
           return true;
       }
        /// <summary>
        /// ��ȡȫӡ����
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetAllprintParam(ref AllprintParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(AllprintParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x80) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(AllprintParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (AllprintParam)SerializationUnit.BytesToStruct(structData, typeof(AllprintParam));
            string flag = new string(cc.Flag);
            if (flag != "APRT")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ����ȫӡ����
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetAllprintParam(AllprintParam cc)
        {
            cc.Flag = new char[] { 'A', 'P', 'R', 'T' };
            byte[] val = new byte[Marshal.SizeOf(typeof(AllprintParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x80) == 0)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// ��ɳ
        /// </summary>
        /// <param name="cc"></param>
        public static bool LayingSand()
        {
            byte[] val = new byte[2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x11) == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ��ɳ���
        /// </summary>
        public static bool XiaShaFinish()
        {
            byte[] val = new byte[2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x12) == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ��ȡ�ֶ���ϴ����for colorjet
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetManualCleanParam(ref ManualCleanParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(ManualCleanParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x20) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(ManualCleanParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (ManualCleanParam)SerializationUnit.BytesToStruct(structData, typeof(ManualCleanParam));
            string flag = Encoding.ASCII.GetString(BitConverter.GetBytes(cc.nFlag));
            if (CoreInterface.IsS_system())
            {
                if (flag != "CJCS")
                {
                    return false;
                }
                return true;
            }
            else
            {
                if (flag != "CJCA")
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// �����ֶ���ϴ����for colorjet
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetManualCleanParam(ManualCleanParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(ManualCleanParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x20) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// �����ֶ���ϴ����for colorjet ZCenter
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetManualCleanParamZCenter()
        {
            byte[] val = { 0x00 };
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x22) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// �����ֶ���ϴ����for colorjet ZBottom
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetManualCleanParamZBottom()
        {
            byte[] val = {0x00};
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x23) == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ��ʼ��ϴƤ��,����һ��ʱ�����24v
        /// </summary>
        public static bool StartCleanBelt()
        {
            byte[] val = { 0x00 };
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x25) == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ��ֹ��ϴƤ��,ֹͣ���24v
        /// </summary>
        public static bool StopCleanBelt()
        {
            byte[] val = { 0x00 };
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x26) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        ///  ����textile ��ʼ��Zλ�ƼƲ�������������
        /// </summary>
        public static bool DOCANTEXTILE_SENSOR_INIT()
        {
            byte[] val = { 0x00 };
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0131) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// ��ȡ������ϴ����ϴ����
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetDocanTextileCleanParam(ref DocanTextileParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(DocanTextileParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0130) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(DocanTextileParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (DocanTextileParam)SerializationUnit.BytesToStruct(structData, typeof(DocanTextileParam));
            string flag = new string(cc.Flag);
            if (flag != "DCT\0")
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// ���ö�����ϴ����ϴ����
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetDocanTextileCleanParam(DocanTextileParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(DocanTextileParam))];
            uint bufsize = (uint)val.Length;
            cc.Flag = new char[] { 'D', 'C', 'T', '\0' };
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0130) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// ��ȡ�����ϴ����ϴ����
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetBoHaoCleanParam(ref BoHaoParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(BoHaoParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0150) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(BoHaoParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (BoHaoParam)SerializationUnit.BytesToStruct(structData, typeof(BoHaoParam));
            string flag = Encoding.ASCII.GetString(BitConverter.GetBytes(cc.Flag));
            if (flag != "BH\0\0")
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// ���ö�����ϴ����ϴ����
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetBoHaoCleanParam(BoHaoParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(BoHaoParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0150) == 0)
            {
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// ��ȡ������ϴ����ϴ����
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetALLWINCleanParam(ref ALLWINCleanParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(ALLWINCleanParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0110) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(ALLWINCleanParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (ALLWINCleanParam)SerializationUnit.BytesToStruct(structData, typeof(ALLWINCleanParam));
            string flag = new string(cc.Flag);
            if (flag != "AWC\0")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ���ð�����ϴ����ϴ����
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetALLWINCleanParam(ALLWINCleanParam cc)
        {
            cc.Flag = new char[] { 'A', 'W', 'C', '\0' };
            cc.rev=new byte[6];
            byte[] val = new byte[Marshal.SizeOf(typeof(ALLWINCleanParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0110) == 0)
            {
                return false;
            }
            return true;
        }
        ///// <summary>
        ///// ��ȡ�ֲ���ϴ����ϴ����
        ///// </summary>
        ///// <param name="cc"></param>
        //public static bool GetLECAICleanParam(ref LeCaiCleanParam cc)
        //{
        //    byte[] val = new byte[Marshal.SizeOf(typeof(LeCaiCleanParam)) + 2];
        //    uint bufsize = (uint)val.Length;
        //    if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0031) == 0)
        //    {
        //        return false;
        //    }
        //    byte[] structData = new byte[Marshal.SizeOf(typeof(LeCaiCleanParam))];
        //    Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
        //    cc = (LeCaiCleanParam)SerializationUnit.BytesToStruct(structData, typeof(LeCaiCleanParam));
        //    return true;
        //}

        ///// <summary>
        ///// �����ֲ���ϴ����ϴ����
        ///// </summary>
        ///// <param name="cc"></param>
        //public static bool SetLECAICleanParam(LeCaiCleanParam cc)
        //{
        //    cc.Flag = new char[] { 'L', 'C', 'C', '\0' };
        //    cc.rev = new byte[49];
        //    byte[] val = new byte[Marshal.SizeOf(typeof(LeCaiCleanParam))];
        //    uint bufsize = (uint)val.Length;
        //    val = SerializationUnit.StructToBytes(cc);
        //    if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0031) == 0)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        /// <summary>
        /// ��ʼ�ֲ���ϴ
        /// </summary>
        /// <param name="cc"></param>
        public static bool StartLECAIClean()
        {
            byte[] val = new byte[0];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0032) == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// �ֶ���ϴ
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetManualCleanCmd(byte value,byte? level=null)
        {
            if (level.HasValue)
            {
                // for ӡ����
                byte[] val = new byte[] { value,level.Value};
                uint bufsize = (uint)val.Length;
                if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x60) == 0)
                {
                    return false;
                }
            }
            else
            {
                // colorjet
                byte[] val = new byte[] { value };
                uint bufsize = (uint)val.Length;
                if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x21) == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// �㽭�����ֶ���ϴ
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetManualCleanCmd_PQ(byte value)
        {
            byte[] val = new byte[] { value };
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x141) == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ��ֹ�ֶ���ϴ
        /// </summary>
        public static bool StopManualClean()
        {
                byte[] val = new byte[0];
                uint bufsize = (uint)val.Length;
                if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x24) == 0)
                {
                    return false;
                }
            return true;
        }

        /// <summary>
        /// ��ȡ�ֲ�λ�ò���
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetPosition_Info(ref PositionSetting_LeCai cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(PositionSetting_LeCai)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x30) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(PositionSetting_LeCai))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (PositionSetting_LeCai)SerializationUnit.BytesToStruct(structData, typeof(PositionSetting_LeCai));
            string flag = Encoding.ASCII.GetString(BitConverter.GetBytes(cc.nFlag));
            if (flag != "LEC\0")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// �����ֲ�λ�ò���
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetPosition_Info(PositionSetting_LeCai cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(PositionSetting_LeCai))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x30) == 0)
            {
                return false;
            }
            return true;
        }

	    /// <summary>
	    ///     //��req = 0x7A, index = 0;ֻ��д���ܶ������岻��
	    /// ȫ��/�������
	    /// </summary>
	    /// <param name="settings"></param>
	    /// <returns></returns>
	    public static bool SetPeripheralExtendedSettings(AllParam settings)
	    {
	        PrintingParaType para = new PrintingParaType(null);
	        para.YAdjustDistance =
	            (uint) (settings.ExtendedSettings.BackAndForthLen*settings.PrinterProperty.fPulsePerInchY);
	        byte[] val = new byte[Marshal.SizeOf(typeof (PrintingParaType))];
	        uint bufsize = (uint) val.Length;
	        val = SerializationUnit.StructToBytes(para);
	        return CoreInterface.SetEpsonEP0Cmd(0x7a, val, ref bufsize, 0, 0) != 0;
	    }

	    //////////////////////////////////////////////
	    /// <summary>
	    /// ���ò������
	    /// </summary>
	    /// <param name="settings"></param>
	    /// <returns></returns>
        public static bool SetFloraParam(AllParam settings)
        {
            FloraParamUI paramUi = settings.ExtendedSettings.FloraParam;
            FloraParam para = new FloraParam(null);
            ushort pid, vid;
            pid = vid = 0;
	        ushort index = 0xA0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.FLORA_FLAT_TEXTILE)
                {
                    para.Flag = new char[] {'0', 'T', '5', '0' };
                    index = 0xA0;
                    para.suckStartPlace = (uint)(paramUi.suckStartPlace * settings.PrinterProperty.fPulsePerInchY);
                }
                if (vid == (ushort)VenderID.FLORA_BELT_TEXTILE)
                {
                    para.Flag = new char[] {  'T', '1', '8', '0'};
                    index = 0xA1;
                    para.suckStartPlace = (uint)(paramUi.suckStartPlace * settings.PrinterProperty.fPulsePerInchX);
                }
            }
            para.cleanZPlace = (uint)(paramUi.cleanZPlace * settings.PrinterProperty.fPulsePerInchZ);
            para.moistYPlace = (uint)(paramUi.cappingYPlace * settings.PrinterProperty.fPulsePerInchY);
            para.moistZPlace = (uint)(paramUi.cappingZPlace * settings.PrinterProperty.fPulsePerInchZ);
            para.suckEndPlace = (uint)(paramUi.suckEndPlace * settings.PrinterProperty.fPulsePerInchY);
            para.purgeInkRecoverTime = paramUi.purgeInkRecoverTime;
            para.purgeInkTime = paramUi.purgeInkTime;

            byte bitFlag = 0;

            if (paramUi.bIsNeedPrepare)
            {
                bitFlag |= 0x1;
            }
            if (paramUi.bIsNeedCleanFlash)
            {
                bitFlag |= 0x2;
            }

            para.bIsNeedPrepare = bitFlag; // (byte)(paramUi.bIsNeedPrepare ? 1 : 0);
            para.speed = paramUi.speed;
            para.preOff = (uint)(paramUi.preOffset * settings.PrinterProperty.fPulsePerInchX);
	        para.cleanSlotNum = paramUi.cleanSlotNum;
	        para.cleanSlotSpace = (uint) (paramUi.cleanSlotSpace*settings.PrinterProperty.fPulsePerInchX);
	        para.cleanMotorSpeed = paramUi.cleanMotorSpeed;
	        para.doWetDelay = paramUi.doWetDelay;
	        para.speedBack = paramUi.speedBack;
            para.scraperStart1 = (uint)(paramUi.scraperStart1 * settings.PrinterProperty.fPulsePerInchX);
            para.scraperStart2 = (uint)(paramUi.scraperStart2 * settings.PrinterProperty.fPulsePerInchX);
            byte[] val = new byte[Marshal.SizeOf(typeof(FloraParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(para);
            return CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, index) != 0;
        }

        //////////////////////////////////////////////
        /// <summary>
        /// ���ò������
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static bool GetFloraParam(ref AllParam settings)
        {
            FloraParamUI paramUi = settings.ExtendedSettings.FloraParam;
            FloraParam para = new FloraParam(null);
            ushort pid, vid;
            pid = vid = 0;
            ushort index = 0xA0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.FLORA_FLAT_TEXTILE)
                {
                    index = 0xA0;
                }
                if (vid == (ushort)VenderID.FLORA_BELT_TEXTILE)
                {
                    index = 0xA1;
                }
            }
            byte[] val = new byte[Marshal.SizeOf(typeof(FloraParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, index) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(FloraParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            para = (FloraParam)SerializationUnit.BytesToStruct(structData, typeof(FloraParam));
            if (vid == (ushort)VenderID.FLORA_FLAT_TEXTILE)
            {
                string flag = new string(para.Flag);
                if (flag != "0T50")
                {
                    return false;
                }
            }
            if (vid == (ushort)VenderID.FLORA_BELT_TEXTILE)
            {
                string flag = new string(para.Flag);
                if (flag != "T180")
                {
                    return false;
                }
            }
            if (vid == (ushort)VenderID.FLORA_BELT_TEXTILE)
            {
                paramUi.suckStartPlace = para.suckStartPlace / settings.PrinterProperty.fPulsePerInchX;
            }
            if (settings.PrinterProperty.fPulsePerInchZ != 0)
            {
                paramUi.cleanZPlace = para.cleanZPlace / settings.PrinterProperty.fPulsePerInchZ;
                paramUi.cappingZPlace = para.moistZPlace / settings.PrinterProperty.fPulsePerInchZ;                
            }
            if (settings.PrinterProperty.fPulsePerInchY != 0)
            {
                if (vid == (ushort)VenderID.FLORA_FLAT_TEXTILE)
                {
                    paramUi.suckStartPlace = para.suckStartPlace / settings.PrinterProperty.fPulsePerInchY;
                }
                paramUi.cappingYPlace = para.moistYPlace / settings.PrinterProperty.fPulsePerInchY;
                paramUi.suckEndPlace = para.suckEndPlace/settings.PrinterProperty.fPulsePerInchY;
            }
            paramUi.purgeInkRecoverTime = para.purgeInkRecoverTime;
            paramUi.purgeInkTime = para.purgeInkTime;
            paramUi.bIsNeedPrepare = (para.bIsNeedPrepare & 0x1) > 0;
            paramUi.bIsNeedCleanFlash = (para.bIsNeedPrepare & 0x2) > 0;
            paramUi.speed = para.speed;
            paramUi.preOffset = (para.preOff /settings.PrinterProperty.fPulsePerInchX);
            paramUi.doWetDelay = para.doWetDelay;
            settings.ExtendedSettings.FloraParam = paramUi;
            
            return true;
        }

        /// <summary>
        /// ֱͨfw����ҵ�������
        /// </summary>
        /// <param name="jobSettings"></param>
        /// <returns></returns>
        public static bool SetFwJobSettings(FwJobSettings jobSettings)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(FwJobSettings))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(jobSettings);
            return CoreInterface.SetEpsonEP0Cmd(0x83, val, ref bufsize, 0, 1) != 0;
        }
        /// <summary>
        /// ��ͷ��װ��ͷ���Ͷ���
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static bool GetPrintHeadOrder(ref PrintHeadOrder cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(PrintHeadOrder)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x78, val, ref bufsize, 0, 2) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(PrintHeadOrder))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (PrintHeadOrder)SerializationUnit.BytesToStruct(structData, typeof(PrintHeadOrder));
            return true;
        }

        public static bool SetPrintHeadOrder(PrintHeadOrder para)
        {
            para.Flag = new char[] { 'P', 'H', 'O', 'r' };
            byte[] val = new byte[Marshal.SizeOf(typeof(PrintHeadOrder))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(para);
            return CoreInterface.SetEpsonEP0Cmd(0x78, val, ref bufsize, 0, 2) != 0;
        }
        /// <summary>
        /// ��ȡ��ʪ����
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static bool GetWetParam(ref WetParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(WetParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x7f, val, ref bufsize, 0, 2) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(WetParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (WetParam)SerializationUnit.BytesToStruct(structData, typeof(WetParam));
            return true;
        }
        /// <summary>
        /// ���ñ�ʪ����
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public static bool SetWetParam(WetParam para)
        {
            para.Flag = new char[] { 'W', 'E', 'T', 'P' };
            byte[] val = new byte[Marshal.SizeOf(typeof(WetParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(para);
            return CoreInterface.SetEpsonEP0Cmd(0x7f, val, ref bufsize, 0, 2) != 0;
        }

        /// <summary>
        /// ��ȡ׿չר�ò���
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static bool GetZhuoZhanParam(ref ZhuoZhanParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(ZhuoZhanParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x130) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(ZhuoZhanParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (ZhuoZhanParam)SerializationUnit.BytesToStruct(structData, typeof(ZhuoZhanParam));
            string flag = new string(cc.Flag);
            if (flag != "ZZGY")
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// ����׿չר�ò���
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public static bool SetZhuoZhanParam(ZhuoZhanParam para)
        {
            para.Flag = new char[] { 'Z', 'Z', 'G', 'Y' };
            byte[] val = new byte[Marshal.SizeOf(typeof(ZhuoZhanParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(para);
            return CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x130) != 0;
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public static bool SetShakeParam(PhShake_tag cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(PhShake_tag))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            return CoreInterface.SetEpsonEP0Cmd(0x83, val, ref bufsize, 0, 0x05) != 0;
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public static bool GetShakeParam(ref PhShake_tag cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(PhShake_tag)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x83, val, ref bufsize, 0, 0x05) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(PhShake_tag))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (PhShake_tag)SerializationUnit.BytesToStruct(structData, typeof(PhShake_tag));
            return true;
        }
        /// <summary>
        /// ��ȡ�忨��־ BYHX
        /// </summary>
        /// <returns></returns>
        public static bool GetFlagOfBoard(byte[] val)
        {
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x86, val, ref bufsize, 0, 0x11) != 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// ���ð忨��־ BYHX
        /// </summary>
        /// <returns></returns>
        public static bool SetFlagOfBoard()
        {
            string flag = "BYHX";
            byte[] val = System.Text.Encoding.Default.GetBytes(flag);
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x86, val, ref bufsize, 0, 0x11) != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ��ȡ������ͷ��ϴ����
        /// </summary>
        /// <returns></returns>
        public static bool GetPQCleanParam(ref PQCleanParam para)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(PQCleanParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x140) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(PQCleanParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            para = (PQCleanParam)SerializationUnit.BytesToStruct(structData, typeof(PQCleanParam));

            bool flagCheck = true;
            char[] flag = new char[] { 'P', 'Q', 'C', '\0' };
            for (int i = 0; i < para.Flag.Length; i++)
            {
                if (para.Flag[i] != flag[i])
                {
                    flagCheck = false;
                    break;
                }
            }
            return flagCheck;
        }
        /// <summary>
        /// ����������ͷ��ϴ����
        /// </summary>
        /// <returns></returns>
        public static bool SetPQCleanParam(PQCleanParam para)
        {
            para.Flag = new char[] { 'P', 'Q', 'C', '\0' };
            byte[] val = new byte[Marshal.SizeOf(typeof(PQCleanParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(para);
            return CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x140) != 0;
        }

        /// <summary>
        /// ��ȡGma��ͷ��ϴ����
        /// </summary>
        /// <returns></returns>
        public static bool GetGmaCleanParam(ref GmaCleanParam para)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(GmaCleanParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0xe5) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(GmaCleanParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            para = (GmaCleanParam)SerializationUnit.BytesToStruct(structData, typeof(GmaCleanParam));

            bool flagCheck = true;
            char[] flag = new char[] { 'C', 'L', 'C', 'N' };
            for (int i = 0; i < para.Flag.Length; i++)
            {
                if (para.Flag[i] != flag[i])
                {
                    flagCheck = false;
                    break;
                }
            }
            return flagCheck;
        }
        /// <summary>
        /// ����Gma��ͷ��ϴ����
        /// </summary>
        /// <returns></returns>
        public static bool SetGmaCleanParam(GmaCleanParam para)
        {
            para.Flag = new char[] { 'C', 'L', 'C', 'N' };
            byte[] val = new byte[Marshal.SizeOf(typeof(GmaCleanParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(para);
            return CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0xE5) != 0;
        }

        /// <summary>
        /// ��ȡ��������ͷ��ϴ����
        /// </summary>
        /// <returns></returns>
        public static bool GetHlcCleanParam(ref HlcCleanParam para)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(HlcCleanParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x140) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(HlcCleanParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            para = (HlcCleanParam)SerializationUnit.BytesToStruct(structData, typeof(HlcCleanParam));

            bool flagCheck = true;
            char[] flag = new char[] { 'H', 'L', 'C', (char)0 };
            for (int i = 0; i < para.Flag.Length; i++)
            {
                if (para.Flag[i] != flag[i])
                {
                    flagCheck = false;
                    break;
                }
            }
            return flagCheck;
        }
        /// <summary>
        /// ���û�������ͷ��ϴ����
        /// </summary>
        /// <returns></returns>
        public static bool SetHlcCleanParam(HlcCleanParam para)
        {
            para.Flag = new char[] { 'H', 'L', 'C', (char)0 };
            byte[] val = new byte[Marshal.SizeOf(typeof(HlcCleanParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(para);
            return CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x140) != 0;
        }

        //////////////////////////////////////////////
        /// <summary>
        /// ��ȡldp����
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetLDPParam(ref LDPParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(LDPParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x120) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(LDPParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (LDPParam)SerializationUnit.BytesToStruct(structData, typeof(LDPParam));
            string flag = new string(cc.Flag);
            if (flag != "LDP\0")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ����ldp����
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetLDPParam(LDPParam cc)
        {
            cc.Flag = new char[] { 'L', 'D', 'P', (char)0 };
            byte[] val = new byte[Marshal.SizeOf(typeof(LDPParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x120) == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// hapond���ģʽ�л�
        /// </summary>
        /// <param name="cc"></param>
        public static void SetHapondControlMode(HapondMotorParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(HapondMotorParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0100) == 0)
            {
                Debug.Assert(false, "SetHapondControlMode fialed!");
            }
        }

        /// <summary>
        /// hapond���ģʽ�л�
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetHapondControlMode(ref HapondMotorParam cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(HapondMotorParam))+2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x0100) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(HapondMotorParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (HapondMotorParam)SerializationUnit.BytesToStruct(structData, typeof(HapondMotorParam));
            string flag = new string(cc.Flag);
            if (flag != "HAPD")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ͨ��˫Y���ģʽ�л�
        /// </summary>
        /// <param name="cc"></param>
        public static void SetMotionSetting(HapondMotorParam_Auto cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(HapondMotorParam_Auto))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x8B, val, ref bufsize, 0, 0x1) == 0)
            {
                Debug.Assert(false, "SetMotionSetting fialed!");
            }
        }

        /// <summary>
        /// ͨ��˫Y���ģʽ�л�
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetMotionSetting(ref HapondMotorParam_Auto cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(HapondMotorParam_Auto)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x8B, val, ref bufsize, 0, 0x1) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(HapondMotorParam_Auto))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (HapondMotorParam_Auto)SerializationUnit.BytesToStruct(structData, typeof(HapondMotorParam_Auto));
            string flag = new string(cc.Flag);
            if (flag != "YTOQ")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ��ȡ��īѭ�����ݲ���
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetWhiteInkCycleParam(ref WhiteInkCycle cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(WhiteInkCycle)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x82, val, ref bufsize, 0, 5) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(WhiteInkCycle))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (WhiteInkCycle)SerializationUnit.BytesToStruct(structData, typeof(WhiteInkCycle));
            return true;
        }

        /// <summary>
        /// ��īѭ�����ݲ���
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetWhiteInkCycleParam(WhiteInkCycle cc)
        {
            cc.Flag = new char[] { 'W', 'I', 'C', 'F' };
            byte[] val = new byte[Marshal.SizeOf(typeof(WhiteInkCycle))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x82, val, ref bufsize, 0, 5) == 0)
            {
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// ����UV��X2����
        /// </summary>
        /// <param name="cc"></param>
        public static bool GetGZUVX2Param(ref GZUVX2Param cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(GZUVX2Param)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0xE8) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(GZUVX2Param))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            cc = (GZUVX2Param)SerializationUnit.BytesToStruct(structData, typeof(GZUVX2Param));
            string flag = Encoding.ASCII.GetString(BitConverter.GetBytes(cc.Flag));
            if (flag != "GZX\0")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ��ȡUV��X2����
        /// </summary>
        /// <param name="cc"></param>
        public static bool SetGZUVX2Param(GZUVX2Param cc)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(GZUVX2Param))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0xE8) == 0)
            {
                return false;
            }
            return true;
        }
        //////////////////////////////////////////////

        /// <summary>
        /// �Ƿ�֧�־���ͷ�����õ�ѹ
        /// </summary>
        /// <returns></returns>
        public static bool IsSupportVoltage_KY()
        {
            byte[] val = new byte[64];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x54, val, ref bufsize, 0, 0x2) != 0)
            {
                
                if ((val[4] & 0x8) > 0)
                    return true;
            }

            return false;
        }


        /// <summary>
        /// ��ȡY�����ٶ�
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static bool GetYStepSpeed(ref int ySpeed)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(YStepSpeedParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x82, val, ref bufsize, 0, 7) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(YStepSpeedParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            YStepSpeedParam cc = (YStepSpeedParam)SerializationUnit.BytesToStruct(structData, typeof(YStepSpeedParam));
            string flag = new string(cc.Flag);
            if (flag == "STSP")
            {
                ySpeed = cc.ySpeed;
            }
            else
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// ����Y�����ٶ�
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public static bool SetYStepSpeed(int ySpeed)
        {
            YStepSpeedParam cc = new YStepSpeedParam();
            cc.Flag = new char[] { 'S', 'T', 'S', 'P' };
            cc.ySpeed = ySpeed;
            byte[] val = new byte[Marshal.SizeOf(typeof(YStepSpeedParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            return CoreInterface.SetEpsonEP0Cmd(0x82, val, ref bufsize, 0, 7) != 0;
        }


        public static bool GetLingFengPara(ref LingFengParam param)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(LingFengParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x27) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(LingFengParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            param = (LingFengParam)SerializationUnit.BytesToStruct(structData, typeof(LingFengParam));
            string flag = new string(param.Flag);
            if (flag != "CJR\0")
            {
                return false;
            }
            return true;
        }

        public static bool SetLingFengParam(LingFengParam cc)
        {
            cc.Flag = new char[] { 'C', 'J', 'R', (char)0 };
            byte[] val = new byte[Marshal.SizeOf(typeof(LingFengParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(cc);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x27) == 0)
            {
                return false;
            }
            return true;
        }

        public static bool SetKeepwat(int flag)
        {
            byte[] subval = new byte[1];
            uint bufsize = (uint)subval.Length;
            subval[0] = (byte)flag;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x7C, subval, ref bufsize, 5, (ushort)0);
            return (ret != 0);
        }

        public static int GetKeepwat()
        {
            int ret = 0;
            byte[] subval = new byte[3];
            uint bufsize = (uint)subval.Length;
            subval[0] = 0x7C;
            subval[1] = 5;
            if (CoreInterface.GetEpsonEP0Cmd(0x7C, subval, ref bufsize, 5, (ushort)0) != 0)
            {
                ret = subval[2];
            }

            return ret;
        }

        public static byte[] GetChanneParamValue_Y2(int channelNum)
        {
            byte[] val = new byte[channelNum];
            uint readlen = (uint)val.Length;

            CoreInterface.GetEpsonEP0Cmd(0x7F, val, ref readlen, 25, 0);

            return val;
        }

        public static void GetAllChannelsFromBoard_Y2(int channelNum, List<string> NameList, ref  List<ChannelInfo> channels)
        {
            byte[] channelParamValues = EpsonLCD.GetChanneParamValue_Y2(channelNum);

            for (int i = 1; i <= channelNum; i++)
            {
                ChannelInfo channel = new ChannelInfo();
                channel.Id = i;
                channel.Name = NameList[i - 1];//EpsonLCD.GetChannelName(i);
                channel.OriginalName = ""; //EpsonLCD.GetChannelNameDebug(i);
                channel.OriginalValue = BitConverter.ToString(channelParamValues, i - 1, 1);

                channel.Value = channelParamValues[i - 1];
                channels.Add(channel);
            }
        }

        public static int SetChannelParamValue_Y2(byte[] raios)
        {
            uint uSize = (uint)raios.Length;
            return CoreInterface.SetEpsonEP0Cmd(0x7F, raios, ref uSize, 33, 0);
        }

        /// <summary>
        /// UV��ƫ�ƾ�������
        /// </summary>
        /// <returns></returns>
        public static bool SetUVOffsetDistToFw(UVOffsetDistanceUI uvOffset, float fPulsePerInchX)
        {
            byte[] buf = new byte[uvOffset.OffsetDistArray.Length * 4];
            uint bufsize = (uint)buf.Length;

            for (int i = 0; i < uvOffset.OffsetDistArray.Length; i++)
            {
                Buffer.BlockCopy(BitConverter.GetBytes((int)(uvOffset.OffsetDistArray[i] * fPulsePerInchX)), 0, buf, i * 4, 4);
            }


            int ret = CoreInterface.SetEpsonEP0Cmd(0x82, buf, ref bufsize, 0, 0x0A);
            return ret == 1;
        }

        public static bool GetHumanParam(ref HumanParam param)
        {
            byte[] val = new byte[Marshal.SizeOf(typeof(HumanParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x160) == 0)
            {
                return false;
            }
            byte[] structData = new byte[Marshal.SizeOf(typeof(HumanParam))];
            Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
            param = (HumanParam)SerializationUnit.BytesToStruct(structData, typeof(HumanParam));
            string flag = Encoding.ASCII.GetString(BitConverter.GetBytes(param.Flag));
            if (flag != "HUMA")
            {
                return false;
            }
            return true;
        }

        public static bool SetHumanParam(HumanParam param)
        {
            param.Flag = BitConverter.ToUInt32(Encoding.ASCII.GetBytes("HUMA"),0);
            byte[] val = new byte[Marshal.SizeOf(typeof(HumanParam))];
            uint bufsize = (uint)val.Length;
            val = SerializationUnit.StructToBytes(param);
            if (CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x160) == 0)
            {
                return false;
            }
            return true;
        }

        public static bool SetTemperaturProfil(bool flag)
        {
            uint select = (uint)(flag ? 0x00000100 : 0x00000120);
            List<byte> value = new List<byte>();
            value.Add(0xC5);
            value.Add(0xDE);
            value.AddRange(BitConverter.GetBytes(select));
            uint bufsize = (uint)value.Count;
            if (CoreInterface.SetEpsonEP0Cmd(0x80, value.ToArray(), ref bufsize, 0, 0) != 0)
            {
                return true;
            }
            return false;
        }

        public static bool GetTemperaturProfileState()
        {
            byte[] val = new byte[8];
            uint bufsize = (uint)val.Length;
            val[0] = 0xC5;
            val[1] = 0xE3;
            if (CoreInterface.SetEpsonEP0Cmd(0x80, val, ref bufsize, 0, 0) != 0)
            {
                return true;
            }
            return false;
        }
        public static bool SetWorkPosInfo(SPrinterSetting ss)
        {
            uint workPos1 = 0xffffffff;
            uint workPos2 = 0xffffffff;

            if((ss.sExtensionSetting.WorkPosEnable & 1) == 1 )
            {
                workPos1 = (uint)ss.sExtensionSetting.WorkPosList[0];
            } 
            if((ss.sExtensionSetting.WorkPosEnable >> 1 & 1) == 1)
            {
                workPos2 = (uint)ss.sExtensionSetting.WorkPosList[1];
            }

            byte[] val = new byte[8];
            uint bufsize = (uint)val.Length;
            byte[] temp = new byte[4];
            temp = BitConverter.GetBytes(workPos1);
            Array.Copy(temp, 0, val, 0, 4);
            temp = BitConverter.GetBytes(workPos2);
            Array.Copy(temp, 0, val, 4, 4);

            if (CoreInterface.SetEpsonEP0Cmd(0x7F, val, ref bufsize, 92, 0) != 0)
            {
                return true;
            }
            return false;
        }
    }

    public static class InkAreaStaticsHelper
    {
        /// <summary>
        /// ���õ�ǰ����
        /// </summary>
        /// <returns></returns>
        public static bool SetCurrentDate(DateTime dataTimeNow)
        {
            ushort Year = (ushort)dataTimeNow.Date.Year;
            byte Month = (byte)dataTimeNow.Date.Month;
            byte Day = (byte)dataTimeNow.Date.Day;
            byte[] valYear = BitConverter.GetBytes(Year);
            byte[] val = new byte[4];
            val[0] = valYear[0];
            val[1] = valYear[1];
            val[2] = Month;
            val[3] = Day;
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x86, val, ref bufsize, 0, 0x12) != 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// �¼�¼ī������
        /// </summary>
        /// <returns></returns>
        public static bool ClearInkQuantity()
        {
            byte[] val = new byte[0];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x86, val, ref bufsize, 0, 0x13) != 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// �¼�¼��ӡ�������
        /// </summary>
        /// <returns></returns>
        public static bool ClearPrintArea()
        {
            byte[] val = new byte[0];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x86, val, ref bufsize, 0, 0x14) != 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// ����¼�¼ī����IVPM�� + 32bit,12����*8����ɫ��ml��λ
        /// </summary>
        /// <returns></returns>
        public static bool GetInkQuantity(byte[] val)
        {
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x86, val, ref bufsize, 0, 0x15) != 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// �����¼�¼ī����IVPM�� + 32bit,12����*8����ɫ��ml��λ
        /// </summary>
        /// <returns></returns>
        public static bool SetInkQuantity(byte[] val)
        {
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x86, val, ref bufsize, 0, 0x15) != 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// ����¼�¼��ӡ�����PAPM�� + double��12���£���λͬ�ܴ�ӡ�����
        /// </summary>
        /// <returns></returns>
        public static bool GetPrintArea(byte[] val)
        {
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x86, val, ref bufsize, 0, 0x16) != 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// �����¼�¼��ӡ�����PAPM�� + double��12���£���λͬ�ܴ�ӡ�����
        /// </summary>
        /// <returns></returns>
        public static bool SetPrintArea(byte[] val)
        {
            uint bufsize = (uint)val.Length;
            if (CoreInterface.SetEpsonEP0Cmd(0x86, val, ref bufsize, 0, 0x16) != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// �Ѱ忨��ī��ͳ����Ϣͬ�������ش���
        /// ����ʱ�����һ���¼,���¿�ʼ��¼
        /// ����·ݲ�ͬ,�����ȥ�굱ǰ�µ�����,���¼���
        /// </summary>
        /// <returns></returns>
        public static bool SynchronizeInkAndArea(DateTime dt)
        {
            SBoardInfo sBoardInfo = new SBoardInfo();
            if (CoreInterface.GetBoardInfo(0, ref sBoardInfo) != 0)
            {
                string filePath = GetInkStaticsFileName(sBoardInfo.m_nBoardSerialNum);
                FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                byte[] ink = new byte[Marshal.SizeOf(typeof(InkOfMonths))];
                byte[] area = new byte[Marshal.SizeOf(typeof(AreaOfMonths))];
                BinaryReader br = new BinaryReader(fs);
                byte[] yearBytes = BitConverter.GetBytes(dt.ToBinary());
                DateTime lastYear = DateTime.Now;
                // ���ϴ�ͬ�����ļ��ж�ȡ��ͬ�������
                if (fs.Length > ink.Length + area.Length)
                {
                    br.Read(yearBytes, ink.Length + area.Length, yearBytes.Length);
                    lastYear = DateTime.FromBinary(BitConverter.ToInt64(yearBytes,0));
                }

                BinaryWriter bw = new BinaryWriter(fs);
                if (GetInkQuantity(ink) && GetPrintArea(area))
                {
                    // ����·ݲ�ͬ,���������,���¼���
                    if (dt.Month != lastYear.Month)
                    {
                        //���ȥ�굱ǰ�µ�����,InkOfColors
                        byte[] zoreBytes = new byte[Marshal.SizeOf(typeof(InkOfColors))];//һ���¸���ɫ�����ݳ���InkOfColors
                        Buffer.BlockCopy(zoreBytes, 0, ink, 4 + (dt.Month-1) * zoreBytes.Length, zoreBytes.Length);
                        //
                        zoreBytes = new byte[4];//����õ�double���ͳ�����4
                        Buffer.BlockCopy(zoreBytes, 0, area, 4 + (dt.Month - 1) * zoreBytes.Length, zoreBytes.Length);
                      
                        if (SetInkQuantity(ink) && SetPrintArea(area))
                        {
                            //MessageBox.Show(ResString.GetResString("SynchronizeInkAndAreaSuccess"),
                            //    ResString.GetProductName());
                        }
                    }
                    fs.Seek(0, SeekOrigin.Begin);
                    bw.Write(ink);
                    bw.Write(area);
                    bw.Write(dt.ToBinary());
                    fs.Close();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// ��ȡ�忨�Ƿ�֧��ͬ��ī������
        /// </summary>
        /// <returns></returns>
        public static bool CanSynchronizeInkAndArea()
        {
            byte[] val = new byte[64];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x54, val, ref bufsize, 0, 0x2) != 0)
            {
                if ((val[4] & 1) == 1)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// �ж��Ƿ�����˰忨
        /// </summary>
        /// <returns></returns>
        public static bool BoardHasChanged()
        {
            string[] files = GetAllInkStaticsFiles();
            if (files.Length == 1) // ����ֻ��һ���ļ�ʱ,����Ϊ�����˰忨����;����ļ�ʱ�޷�ȷ�ϵ����ĸ��ļ�������,�ͻ��������������������;
            {
                SBoardInfo sBoardInfo = new SBoardInfo();
                if (CoreInterface.GetBoardInfo(0, ref sBoardInfo) != 0)
                {
                    string filePath =GetInkStaticsFileName(sBoardInfo.m_nBoardSerialNum);
                    if (!File.Exists(filePath))
                    {
                        return true;// ���ش���ī�������ļ�,���͵�ǰ�忨��ƥ��,����Ϊ�����˰忨
                    }
                    return false;
                }
            }
            return false;
        }

        private static string strInkStaticsFilePre = "InkS";
        /// <summary>
        /// ���忨id��ȡ��ͳһǰ׺���ļ�����
        /// </summary>
        /// <param name="boardid"></param>
        /// <returns></returns>
        public static string GetInkStaticsFileName(uint boardid)
        {
            return Path.Combine(Application.StartupPath, strInkStaticsFilePre + boardid + ".dll");
        }
        /// <summary>
        /// ��ȡ��Ŀ¼������ī�������ļ�,���Ӧֻ��һ��
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllInkStaticsFiles()
        {
            string[] files = Directory.GetFiles(Application.StartupPath, strInkStaticsFilePre + "*.dll");
            return files;
        }

        /// <summary>
        /// �������ǰ���������������ī�������ļ�
        /// </summary>
        public static void ClearInkStaticsFiles()
        {
            string curFilePath = string.Empty;
            SBoardInfo sBoardInfo = new SBoardInfo();
            if (CoreInterface.GetBoardInfo(0, ref sBoardInfo) != 0)
            {
                curFilePath = GetInkStaticsFileName(sBoardInfo.m_nBoardSerialNum);
            }
            string[] files = GetAllInkStaticsFiles();
            for (int i = 0; i < files.Length; i++)
            {
                if (curFilePath != files[i])
                    File.Delete(files[i]);
            }
        }

        
    }

}