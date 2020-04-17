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
using System.Drawing;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Threading;
using System.Globalization;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;


namespace BYHXPrinterManager
{
	/// <summary>
	/// Summary description for UIPreference.
	/// </summary>
	
	public class UIPreference
	{
		public int ViewModeIndex;
		public int LangIndex;
		public UILengthUnit Unit;
		public bool BeepBeforePrint;
		public bool DelJobAfterPrint;
	    public bool DelFileAfterPrint;
		public string WorkingFolder;
		public JobListColumnHeader[] JobListHeaderList; 
		public bool EnableHotForlder;
		public string HotForlderPath;
        public string PrintedAreaLogConfig;
		public bool bAlternatingPrint;
        public bool bShowAttentionOnLoad;
        public bool bShowMeasureFormBeforPrint;//打印前提示测高
        public bool ReverseHoriMoveDirection;//左右运动反向
        public bool ReverseVertMoveDirection;//前后运动反向
        public bool ReverseZMoveDirection;//Z轴运动反向
        /// <summary>
        /// 扫描向电机，0x01第一个工作台(AXIS_X); 0x08第二个工作台(AXIS_4)；0xff 自动切换
        /// 彩神t-shirt机
        /// </summary>
        public byte ScanningAxis; //
        public byte JobModeIndex;
        public byte MediaModeIndex;
        public bool HotFolderPrintImmediately;
        public byte CloseNozzleSetMode;
		public UIPreference()
		{
			ArrayList langlist;
			UIPreference.InitializeLanguage(out langlist);
			
			int local_LangIndex = 0;
			int local_LangIndex_father = 0;

//			MessageBox.Show("UI:"+Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName +
//					"- " + Thread.CurrentThread.CurrentUICulture.ThreeLetterWindowsLanguageName
//				+ "_" +Thread.CurrentThread.CurrentUICulture.Name 
//				+ "_" +Thread.CurrentThread.CurrentUICulture.LCID.ToString("X") )
//				;
			for (int i=0; i< langlist.Count;i++)
			{
				CultureInfo curCulture = new CultureInfo((int)langlist[i]);
//				MessageBox.Show("List:"+curCulture.TwoLetterISOLanguageName +
//					"- " + curCulture.ThreeLetterISOLanguageName + curCulture.LCID.ToString()
//				+ "_" +curCulture.Name 
//				+ "_" +curCulture.LCID.ToString("X") )
					;
				if((int)langlist[i] == Thread.CurrentThread.CurrentUICulture.LCID)
				{
					local_LangIndex =(int)langlist[i];
					break;
				}
				else if(curCulture.TwoLetterISOLanguageName == Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName
					&& curCulture.ThreeLetterWindowsLanguageName == Thread.CurrentThread.CurrentUICulture.ThreeLetterWindowsLanguageName)
				{
					local_LangIndex =curCulture.LCID;
					break;
				}
				else if (curCulture.TwoLetterISOLanguageName == Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName)
				{
					local_LangIndex_father = curCulture.LCID;
				}
			}
			if(local_LangIndex != 0)
			{
				LangIndex = local_LangIndex;
			}
			else if(local_LangIndex_father != 0)
			{
				LangIndex = local_LangIndex_father;
			}
			else 
			{
				LangIndex = Thread.CurrentThread.CurrentUICulture.LCID;
            }

#if SHIDAO||ALLWIN
			ViewModeIndex = (int)UIViewMode.LeftRight;
#else
            ViewModeIndex = (int)PubFunc.GetDefaultViewMode();
#endif
            if (PubFunc.IsFhzl3D())
            {
                ViewModeIndex = (int)UIViewMode.OldView;
            }
            this.Unit = UILengthUnit.Centimeter;
			this.BeepBeforePrint = true;
			this.DelJobAfterPrint = false;
		    this.DelFileAfterPrint = false;
			this.WorkingFolder = Application.StartupPath;
			JobListHeaderList = (JobListColumnHeader[])Enum.GetValues(typeof(JobListColumnHeader));
			this.EnableHotForlder = false;
			this.HotForlderPath = Application.StartupPath;
		    this.PrintedAreaLogConfig = "*";
			bShowAttentionOnLoad = true;
		    bShowMeasureFormBeforPrint = false;
		    DefaultCanleType = CanleType.AlwaysQuestion;
		    SkinName = "Default";
		    HotFolderPrintImmediately = true;
		}

        /// <summary>
        /// 主界面取消打印按钮的默认行为
        /// </summary>
        public CanleType DefaultCanleType { get; set; }

        /// <summary>
        /// 皮肤名称
        /// </summary>
        public string SkinName { get; set; }

		public float ToDisplayLength(float length)
		{
			return ToDisplayLength(Unit,length);
		}

		public static  float ToDisplayLength(UILengthUnit unit,float length)
		{
			switch(unit)
			{
				case UILengthUnit.Inch:
				{
					break;
				}
				case UILengthUnit.Feet:
				{
					length /= 12;
					break;
				}
				case UILengthUnit.Millimeter:
				{
					length *= 25.4F;
					break;
				}
				case UILengthUnit.Centimeter:
				{
					length *= 2.54F;
					break;
				}
				case UILengthUnit.Meter:
				{
					length *= 0.0254F;
					break;
				}
                default:
                break;
			}
			return length;
		}

		public float ToInchLength(float length)
		{
			return ToInchLength(Unit,length);
		}

        /// <summary>
        /// 界面绑定显示名称
        /// </summary>
        public string UnitDisplayName
        {
            get
            {
                return GetUnitDisplayName();
            }
        }

		public string GetUnitDisplayName()
		{
			return ResString.GetEnumDisplayName(typeof(UILengthUnit),Unit);
		}

		public static  float ToInchLength(UILengthUnit unit,float length)
		{
			switch(unit)
			{
				case UILengthUnit.Inch:
				{
					break;
				}
				case UILengthUnit.Feet:
				{
					length *= 12;
					break;
				}
				case UILengthUnit.Millimeter:
				{
					length /= 25.4F;
					break;
				}
				case UILengthUnit.Centimeter:
				{
					length /= 2.54F;
					break;
				}
				case UILengthUnit.Meter:
				{
					length /= 0.0254F;
					break;
				}
                default:
                    break;
			}
			return length;
		}
		public static void  NumericUpDownToolTip(string tipstring,NumericUpDown ctrlObj,ToolTip tooltip)
		{
			foreach(Control c in ctrlObj.Controls) 
			{ 
				tooltip.SetToolTip(c, tipstring); 
			}
		}

        public static void OnFloatNumericUpDownUnitChanged(UILengthUnit newUnit, UILengthUnit oldUnit, NumericUpDown ctrlObj)
        {
            float maxValue = UIPreference.ToInchLength(oldUnit, Decimal.ToSingle(ctrlObj.Maximum));
            float minValue = UIPreference.ToInchLength(oldUnit, Decimal.ToSingle(ctrlObj.Minimum));
			float inchValue = UIPreference.ToInchLength(oldUnit,Decimal.ToSingle(ctrlObj.Value));

			switch(newUnit)
			{
				case UILengthUnit.Feet:
					ctrlObj.DecimalPlaces = 2;
					ctrlObj.Increment = new Decimal(0.01f);
					break;
				case UILengthUnit.Inch:
					ctrlObj.DecimalPlaces = 2;
					ctrlObj.Increment = new Decimal(0.1f);
					break;
				case UILengthUnit.Meter:
					ctrlObj.DecimalPlaces = 4;
					ctrlObj.Increment = new Decimal(0.001f);
					break;
				case UILengthUnit.Centimeter:
					ctrlObj.DecimalPlaces = 2;
					ctrlObj.Increment = new Decimal(0.1f);
					break;
				case UILengthUnit.Millimeter:
					ctrlObj.DecimalPlaces = 1;
					ctrlObj.Increment = 1;
					break;
				default:
					ctrlObj.DecimalPlaces = 1;
					ctrlObj.Increment = 1;
                    Debug.Assert(false);
					break;
			}
		    if (UIPreference.ToDisplayLength(newUnit, maxValue) >= (double) decimal.MaxValue)
		        ctrlObj.Maximum = decimal.MaxValue;
		    else
		        ctrlObj.Maximum = new Decimal(UIPreference.ToDisplayLength(newUnit, maxValue));
            if (UIPreference.ToDisplayLength(newUnit, minValue) <= (double)decimal.MinValue)
                ctrlObj.Minimum = decimal.MinValue;
            else
                ctrlObj.Minimum = new Decimal(UIPreference.ToDisplayLength(newUnit, minValue));
			decimal val = new Decimal(UIPreference.ToDisplayLength(newUnit,inchValue));
			ClampWithMinMax(ctrlObj.Minimum,ctrlObj.Maximum,ref val,ctrlObj.Visible);
			ctrlObj.Value = val;
		}

        public static void SetSelectIndexAndClampWithMax(ComboBox ctrlObj,int index)
		{
			if(ctrlObj == null)
				return;
			Decimal decIndex = new decimal(index);
			ClampWithMinMax(-1,ctrlObj.Items.Count-1,ref decIndex,ctrlObj.Visible);
			ctrlObj.SelectedIndex = (int)decIndex;
		}

		public static void SetValueAndClampWithMinMax(NumericUpDown ctrlObj,float val)
		{
			// UILengthUnit.Inch 即无需变换单位直接应用val值
			SetValueAndClampWithMinMax(ctrlObj,UILengthUnit.Inch,val);
		}

		public static void SetValueAndClampWithMinMax(NumericUpDown ctrlObj,UILengthUnit DisUnit,float val)
		{
			Decimal curvalue = new Decimal(UIPreference.ToDisplayLength(DisUnit,val)); 
			ClampWithMinMax(ctrlObj.Minimum,ctrlObj.Maximum,ref curvalue,ctrlObj.Visible);
			ctrlObj.Value	=	curvalue;
		}
		private static void ClampWithMinMax(Decimal min,Decimal max,ref Decimal cur,bool bVisible)
		{
			if(cur<min)
			{
				cur = min;
				if(bVisible)
					MessageBox.Show(ResString.GetResString("ClampWithMinMax"),ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
			if(cur>max)
			{
				cur = max;
				if(bVisible)			
					MessageBox.Show(ResString.GetResString("ClampWithMinMax"),ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Information);
			}		
		}

		public int IndexOf(JobListColumnHeader header)
		{
			int ret = -1;
			for (int i=0; i< JobListHeaderList.Length;i++)
			{
				if(header == JobListHeaderList[i])
				{
					return i;
				}
			}
			return ret;
		}
		public UIPreference DeepCopy()
		{
			UIPreference clone = (UIPreference)this.MemberwiseClone();
			clone.JobListHeaderList = (JobListColumnHeader[])this.JobListHeaderList.Clone();
			return clone;
		}


		public string SystemConvertToXml()
		{
			string xml = "<UIPreference>";
			xml += PubFunc.SystemConvertToXml(ViewModeIndex,typeof(int));
			xml +=PubFunc.SystemConvertToXml(LangIndex,typeof(int));
			xml +=PubFunc.SystemConvertToXml(Unit,typeof(UILengthUnit));
			xml +=PubFunc.SystemConvertToXml(BeepBeforePrint,typeof(bool));
			xml +=PubFunc.SystemConvertToXml(DelJobAfterPrint,typeof(bool));
			xml +=PubFunc.SystemConvertToXml(WorkingFolder,typeof(string));
			xml +=PubFunc.SystemConvertToXml(JobListHeaderList,typeof(JobListColumnHeader[]));
			xml +=PubFunc.SystemConvertToXml(EnableHotForlder,typeof(bool));
			xml +=PubFunc.SystemConvertToXml(HotForlderPath,typeof(string));
			xml +=PubFunc.SystemConvertToXml(PrintedAreaLogConfig,typeof(string));
			xml +=PubFunc.SystemConvertToXml(bAlternatingPrint,typeof(bool));
			xml +=PubFunc.SystemConvertToXml(bShowAttentionOnLoad,typeof(bool));
            xml += PubFunc.SystemConvertToXml(DelFileAfterPrint, typeof(bool));
            xml += PubFunc.SystemConvertToXml(ReverseHoriMoveDirection, typeof(bool));
            xml += PubFunc.SystemConvertToXml(ReverseVertMoveDirection, typeof(bool));
            xml += PubFunc.SystemConvertToXml(ReverseZMoveDirection, typeof(bool));
            xml += PubFunc.SystemConvertToXml(SkinName, typeof(string));
            xml += PubFunc.SystemConvertToXml(bShowMeasureFormBeforPrint, typeof(bool));
            xml += PubFunc.SystemConvertToXml(JobModeIndex, typeof(byte));
            xml += PubFunc.SystemConvertToXml(MediaModeIndex, typeof(byte));
            xml += PubFunc.SystemConvertToXml(HotFolderPrintImmediately, typeof(bool));
            xml += PubFunc.SystemConvertToXml(CloseNozzleSetMode, typeof(byte));
            xml += "</UIPreference>";
			return xml;
		}

		public static object SystemConvertFromXml(XmlElement jobElemenet,Type type)
		{	
			UIPreference job =new UIPreference();
			XmlNode currNode = jobElemenet.FirstChild;
			job.ViewModeIndex = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.LangIndex  = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.Unit = (UILengthUnit)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(UILengthUnit));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.BeepBeforePrint = (bool)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(bool));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.DelJobAfterPrint = (bool)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(bool));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.WorkingFolder = (string)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(string));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.JobListHeaderList = (JobListColumnHeader[])PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(JobListColumnHeader[]));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.EnableHotForlder = (bool)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(bool));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.HotForlderPath = (string)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(string));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.PrintedAreaLogConfig = (string)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(string));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.bAlternatingPrint = (bool)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(bool));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.bShowAttentionOnLoad = (bool)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(bool));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
            job.DelFileAfterPrint = (bool)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(bool));
			currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.ReverseHoriMoveDirection = (bool)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(bool));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.ReverseVertMoveDirection = (bool)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(bool));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.ReverseZMoveDirection = (bool)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(bool));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.SkinName = (string)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(string));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.bShowMeasureFormBeforPrint = (bool)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(bool));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.JobModeIndex = (byte)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(byte));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.MediaModeIndex = (byte)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(byte));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.HotFolderPrintImmediately = (bool)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(bool));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.CloseNozzleSetMode = (byte)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(byte));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            return job;
		}
		static public void InitializeLanguage(out ArrayList langlist)
		{
			string  folder = Application.StartupPath;
			langlist = new ArrayList();
			CultureInfo nutrual = new CultureInfo("en-US");
			langlist.Add(nutrual.LCID);
			try
			{
				DirectoryInfo dir = new DirectoryInfo(folder);
				DirectoryInfo [] dirs = dir.GetDirectories();
				foreach (DirectoryInfo f in dirs) 
				{
					try
					{
						//CultureInfo cInfo = new CultureInfo(f.Name);
						foreach ( CultureInfo cInfo in CultureInfo.GetCultures( CultureTypes.AllCultures ) )  
						{
							if ( cInfo.Name == f.Name)  
							{
								langlist.Add(cInfo.LCID);
								break;
							}
						}
					}
					catch
					{
						continue;
					}
				}
				
			}
			catch(Exception)
			{
			}
		}

	}

    public enum CanleType
    {
        None =0,
        PrintingJob=1,
        All = 2,
        AlwaysQuestion,
    }
}
