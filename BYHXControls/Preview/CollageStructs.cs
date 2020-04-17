using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;
//using System.Xml.Serialization;
using System.Diagnostics;
using System.Linq;
using BYHXPrinterManager.Setting;

namespace BYHXPrinterManager.Preview
{
	public class JobClip
	{
		/*Childs内的src为空时则应用父辈的src属性*/
		public string src;
		public string TopSrc;
		public SPrtFileInfo PrtFileInfo;
//		public Image Miniature = null;
        public string SrcMiniature = null;
		public Rectangle ClipRect;
		public int W;
		public int H;
		public int Rotation;
		public int Left;
		public int Top;
		public string Note;
//		public string NoteFontName;
//		public float NoteFontSize;
		public Font NoteFont;
		public int NoteMargin;
		public int NotePosition;//0:left;1:top;2:right;3:bottom
		public JobClip[] Childs;
		public int Margin_L;
		public int Margin_R;
		public int Margin_T;
		public int Margin_B;
		public int XCnt;
		public int YCnt;
        public int XDis;
        /// <summary>
        /// 第二个砖排列间距
        /// </summary>
        public int XDis2;
        public int YDis;
        public int XAddtion;
        public int YAddtion;
        /*IsParent = true时Childs不为空,src可为空.
                 * IsParent = false时Childs为空,src不可为空.*/
		public bool IsParent;
		public bool AutoSizeToContent;
		public bool isSimpleMode;
		public bool noClip;
		public string NoteImageFileName;
		public int AddtionInfoMask;
	    public string realNoteText;

		public JobClip()
		{
			Init(false);
		}
		public JobClip(bool isparent)
		{
			Init(isparent);
		}

	    /// <summary>
	    /// 是否为新景泰3砖非等间距模式
	    /// </summary>
	    public bool IsNktTreeTileMode
	    {
	        get
	        {
	            bool isNkt = SPrinterProperty.IsTILE_PRINT_ID();
	            return isNkt && XCnt == 3 && XDis != XDis2;
	        }
	    }

	    private void Init(bool isparent)
		{
			IsParent = isparent;
//			NoteFontName = "Arial";
			Note = TopSrc = src = string.Empty;
			PrtFileInfo = new SPrtFileInfo();
			ClipRect = new Rectangle(1,1,1,1);
			W = H = Left = Top =Rotation= 0;
//			NoteFontSize = 16;
			NotePosition =3;
			Childs = null;
			NoteMargin=Margin_L=Margin_R=Margin_T=Margin_B=0;
			XCnt = YCnt = 1;
			XDis = YDis =0;
			AutoSizeToContent = true;
			isSimpleMode = true;
			SrcMiniature = null;
			noClip = true;
			NoteFont = new Font("Arial",16,GraphicsUnit.Point);
			AddtionInfoMask =0;
		}


		public Size JobSize
		{
			get
			{
				return GetBound(this).Size;
			}
		}


		public Font GetNoteFont()
		{
//			if(NoteFontName != null && NoteFontName != string.Empty)
//				return new Font(NoteFontName, NoteFontSize, GraphicsUnit.Point);
//			else
//				return new Font("Arial",16,GraphicsUnit.Point);
			return this.NoteFont;
		}


        private string GetRealNoteText(JobClip jobclip, SPrinterSetting? ss=null,bool bGetBound= false)
		{
		    if (!string.IsNullOrEmpty(realNoteText))
		        return realNoteText;
			string text = string.Empty;
			if(jobclip.AddtionInfoMask != 0)
			{
                int resolutionX = jobclip.PrtFileInfo.sFreSetting.nResolutionX * jobclip.PrtFileInfo.sImageInfo.nImageResolutionX;
                int resolutionY = jobclip.PrtFileInfo.sFreSetting.nResolutionY * jobclip.PrtFileInfo.sImageInfo.nImageResolutionY;
                if ((jobclip.AddtionInfoMask & 0x00001) > 0)//JobSize
				{
					UILengthUnit unit = UILengthUnit.Centimeter;//将图片尺寸单位改为厘米
					string unitStr	= ResString.GetUnitSuffixDispName(unit);
                    float width = 0;
                    float height = 0;
                    if (bGetBound)
				    {
                        width = (float)jobclip.PrtFileInfo.sImageInfo.nImageWidth / (float)(jobclip.PrtFileInfo.sFreSetting.nResolutionX);
                        height = (float)jobclip.PrtFileInfo.sImageInfo.nImageHeight / (float)(jobclip.PrtFileInfo.sFreSetting.nResolutionY);
                    }
                    else
                    {
                        width = (float)JobSize.Width / (float)resolutionX;
                        height = (float)JobSize.Height / (float)resolutionY;
                    }
					string strSize = string.Format("{0}x{1} {2}",
						UIPreference.ToDisplayLength(unit,width).ToString("f1"),
						UIPreference.ToDisplayLength(unit,height).ToString("f1"),unitStr);
					text += "\n" + strSize; 
				}
				if ((jobclip.AddtionInfoMask & 0x00010)> 0)//Resolution
				{
					string strRes = string.Format("{0}x{1}",
                        (resolutionX),
                        (resolutionY));
					text += "\n" + strRes; 
				}
				if	((jobclip.AddtionInfoMask & 0x00100)> 0)//PassNum
				{
                    string strPass = string.Format("{0} {1}", jobclip.PrtFileInfo.sFreSetting.nPass, ResString.GetDisplayPass());
				    if (ss.HasValue)
				    {
                        strPass = string.Format("{0} {1}", ss.Value.sFrequencySetting.nPass, ResString.GetDisplayPass());
                    }
					text += "\n" + strPass; 
				}
				if ((jobclip.AddtionInfoMask & 0x01000)> 0)//Direction
				{
					string strDir = ResString.GetEnumDisplayName(typeof(PrintDirection),(PrintDirection)jobclip.PrtFileInfo.sFreSetting.nBidirection);
                    if (ss.HasValue)
                    {
                        strDir = ResString.GetEnumDisplayName(typeof(PrintDirection), (PrintDirection)ss.Value.sFrequencySetting.nBidirection);
                    }
                    text += "\n" + strDir; 
				}
				if	( (jobclip.AddtionInfoMask & 0x10000)> 0)//Filepath
				{
					text += "\n" + jobclip.src; 
				}
			    if ((jobclip.AddtionInfoMask & 0x100000) > 0) //Voltage&pluseWidth
			    {
			        text += GetRealtimeInfoString();
			    }
			}
			if(jobclip.Note != null && jobclip.Note != string.Empty)
				text += "\n" + jobclip.Note;
			return text;
		}
        private string GetRealtimeInfoString()
        {
            string text = string.Empty;
            float ADJUST_VOL_PER_HEAD = 8;
            float VOL_COUNT_PER_HEAD = 3;
            float Plus_COUNT_PER_HEAD = 5;
            int headNum = 0;
            PrinterHeadEnum headtype = PrinterHeadEnum.UNKOWN;
            SFWFactoryData factoryData = new SFWFactoryData();
            if (CoreInterface.GetFWFactoryData(ref factoryData) != 0)
            {
                headtype = (PrinterHeadEnum)factoryData.m_nHeadType;
                if (factoryData.m_nGroupNum > 0)
                {
                    headNum = factoryData.m_nGroupNum * factoryData.m_nColorNum + factoryData.m_nWhiteInkNum +
                              factoryData.m_nOverCoatInkNum;
                }
                else
                {
                    headNum = (Math.Abs(factoryData.m_nGroupNum) * factoryData.m_nColorNum) / 2 + factoryData.m_nWhiteInkNum +
                              factoryData.m_nOverCoatInkNum;
                }
            }

            uint uiHtype = 0;
            CoreInterface.GetUIHeadType(ref uiHtype);
            bool m_bKonic512 = (uiHtype & 0x01) != 0;
            //bool m_bXaar382 = (uiHtype & 0x02) != 0;
            //bool m_bSpectra = (uiHtype & 0x04) != 0;
            bool m_bPolaris = (uiHtype & 0x08) != 0;
            bool m_bPolaris_V5_8 = (uiHtype & 0x10) != 0;
            bool m_bExcept = (uiHtype & 0x20) != 0;
            bool m_bPolaris_V7_16 = (uiHtype & 0x40) != 0;
            bool m_bKonic1024i_Gray = (uiHtype & 0x80) != 0;
            bool m_bSpectra_SG1024_Gray = (uiHtype & 0x100) != 0;
            bool m_bXaar501 = (uiHtype & 0x200) != 0;
            //bool m_bVerArrangement = ((sp.bSupportBit1 & 2) != 0);
            //bool m_bMirrorArrangement = SPrinterProperty.IsMirrorArrangement();
            bool m_b1head2color = (factoryData.m_nGroupNum < 0);
            bool m_Konic512_1head2color = m_b1head2color && m_bKonic512;
            bool m_bPolaris_V7_16_Emerald = m_bPolaris_V7_16 &&
                                            (headtype == PrinterHeadEnum.Spectra_Emerald_10pl ||
                                             headtype == PrinterHeadEnum.Spectra_Emerald_30pl);
            bool m_bPolaris_V7_16_Polaris = m_bPolaris_V7_16 &&
                                            (headtype == PrinterHeadEnum.Spectra_PolarisColor4_15pl ||
                                             headtype == PrinterHeadEnum.Spectra_PolarisColor4_35pl ||
                                             headtype == PrinterHeadEnum.Spectra_PolarisColor4_80pl ||
                                             headtype == PrinterHeadEnum.Spectra_Polaris_15pl ||
                                             headtype == PrinterHeadEnum.Spectra_Polaris_35pl ||
                                             headtype == PrinterHeadEnum.Spectra_Polaris_80pl);
            bool m_bPolaris_V5_8_Emerald = m_bPolaris_V5_8 &&
                                           (headtype == PrinterHeadEnum.Spectra_Emerald_10pl ||
                                            headtype == PrinterHeadEnum.Spectra_Emerald_30pl);
            bool m_bRicoHead = headtype == PrinterHeadEnum.RICOH_GEN4_7pl || headtype == PrinterHeadEnum.RICOH_GEN4L_15pl ||
                               headtype == PrinterHeadEnum.RICOH_GEN4P_7pl;
            bool m_bKyocera = SPrinterProperty.IsKyocera(headtype);

            if (m_bPolaris)
                VOL_COUNT_PER_HEAD = 2;
            if (m_bSpectra_SG1024_Gray)
                VOL_COUNT_PER_HEAD = 3;
            if (m_bKonic1024i_Gray)
                VOL_COUNT_PER_HEAD = 3;
            if (m_bPolaris_V7_16_Polaris)
                VOL_COUNT_PER_HEAD = 1f / 2f;
            if (m_bKyocera)
                VOL_COUNT_PER_HEAD = 2;

            int m_HeadNum = headNum;

            Plus_COUNT_PER_HEAD = VOL_COUNT_PER_HEAD;
            if (m_bSpectra_SG1024_Gray)
            {
                Plus_COUNT_PER_HEAD = 5;
            }

            SRealTimeCurrentInfo realTimeCurrentInfo = new SRealTimeCurrentInfo();
            if (CoreInterface.GetRealTimeInfo(ref realTimeCurrentInfo, AutoInkTestHelper.Rmask) != 0)
            {
                text += "\nVoltageBase=";
                for (int i = 0; i < m_HeadNum; i++)
                {
                    string volstring = string.Join(",", realTimeCurrentInfo.cVoltageBase.Skip((int)(i * VOL_COUNT_PER_HEAD)).Take((int)VOL_COUNT_PER_HEAD));
                    text += string.Format("[H{0}={1}],", i + 1, volstring);
                }
                text += "\nVoltage=";
                for (int i = 0; i < m_HeadNum; i++)
                {
                    string volstring = string.Join(",", realTimeCurrentInfo.cVoltage.Skip((int)(i * VOL_COUNT_PER_HEAD)).Take((int)VOL_COUNT_PER_HEAD));
                    text += string.Format("[H{0}={1}],", i + 1, volstring);
                }
                text += "\nPulseWidth=";
                for (int i = 0; i < m_HeadNum; i++)
                {
                    string volstring = string.Join(",", realTimeCurrentInfo.cPulseWidth.Skip((int)(i * Plus_COUNT_PER_HEAD)).Take((int)VOL_COUNT_PER_HEAD));
                    text += string.Format("[H{0}={1}],", i + 1, volstring);
                }
            }
            else
            {
                text += "\n GetRealTimeInfo fail.";
            }
            return text;
        }

	    private Rectangle GetBound(JobClip jobclip)
		{
			Rectangle ret;
			string text =GetRealNoteText(jobclip,null,true);
			if(!isSimpleMode)
			{
				ret = new Rectangle(jobclip.Left,jobclip.Top,0,0);
				if(jobclip.IsParent)
				{
					for(int i=0;i<jobclip.Childs.Length;i++)
					{
						Rectangle ret1 = GetBound(jobclip.Childs[i]);
						if(ret1.Right>ret.Width)
							ret.Width = ret1.Right;
						if(ret1.Bottom>ret.Height)
							ret.Height = ret1.Bottom;
					}
					ret.Width += jobclip.Margin_L + jobclip.Margin_R;
					ret.Height += jobclip.Margin_B + jobclip.Margin_T;
					return ret;
				}
				else
				{
//					int ResolutionX = PrtFileInfo.sFreSetting.nResolutionX*PrtFileInfo.sImageInfo.nImageResolutionX;
//					int ResolutionY = PrtFileInfo.sFreSetting.nResolutionY*PrtFileInfo.sImageInfo.nImageResolutionY;

//					if(ResolutionX == 0 || ResolutionY == 0)
//					{
//						return Rectangle.Empty;
//					}
					int width = jobclip.ClipRect.Width;// / ResolutionX;
					int height = jobclip.ClipRect.Height;// / ResolutionY;
					if(text != null && text != string.Empty)
					{
						Size notesize = this.getNoteSize(width,text,this.GetNoteFont(),false);
						//0:left;1:top;2:right;3:bottom
						if(this.NotePosition == 0 ||this.NotePosition == 2)
							width += notesize.Height + jobclip.NoteMargin;
						else
							height += notesize.Height + jobclip.NoteMargin;
					}
					if(jobclip.Left + width>ret.Width)
						ret.Width = jobclip.Margin_L + jobclip.Margin_R + width;
					if(jobclip.Top + height>ret.Height)
						ret.Height = jobclip.Margin_B +jobclip. Margin_T + height;

					return ret;
				}
			}
			else
			{
				ret = new Rectangle(0,0,0,0);
//				int ResolutionX = PrtFileInfo.sFreSetting.nResolutionX*PrtFileInfo.sImageInfo.nImageResolutionX;
//				int ResolutionY = PrtFileInfo.sFreSetting.nResolutionY*PrtFileInfo.sImageInfo.nImageResolutionY;

//				if(ResolutionX == 0 || ResolutionY == 0)
//				{
//					return Rectangle.Empty;
//				}
				int width = jobclip.ClipRect.Width;// / ResolutionX;
				int height = jobclip.ClipRect.Height;// / ResolutionY;
				if(noClip)
				{
					width = jobclip.PrtFileInfo.sImageInfo.nImageWidth;
					height = jobclip.PrtFileInfo.sImageInfo.nImageHeight;
				}
                if (IsNktTreeTileMode)
                    width = width * jobclip.XCnt + jobclip.XDis +jobclip.XDis2 + jobclip.XAddtion;
                else
                    width = width * jobclip.XCnt + jobclip.XDis * (jobclip.XCnt - 1) + jobclip.XAddtion;
				height = height * jobclip.YCnt + jobclip.YDis*(jobclip.YCnt-1) + jobclip.YAddtion;

				if(text != null && text != string.Empty)
				{
					Size notesize = this.getNoteSize(width,text,this.GetNoteFont(),false);
					//0:left;1:top;2:right;3:bottom
					if(this.NotePosition == 0 ||this.NotePosition == 2)
						width += notesize.Height + jobclip.NoteMargin;
					else
						height += notesize.Height + jobclip.NoteMargin;
				}
				ret.Width = jobclip.Margin_L + jobclip.Margin_R + width;
				ret.Height = jobclip.Margin_B +jobclip. Margin_T + height;

				this.W = ret.Width;
				this.H = ret.Height;
				return ret;
			}
		}


		private Size getNoteSize(int w,string notetxt,Font notefont,bool isVertical)
		{
			//to do
			Bitmap image = new Bitmap(100,100);
			image.SetResolution(PrtFileInfo.sFreSetting.nResolutionX*PrtFileInfo.sImageInfo.nImageResolutionX,
				PrtFileInfo.sFreSetting.nResolutionY*PrtFileInfo.sImageInfo.nImageResolutionY);
			Graphics g = Graphics.FromImage(image);
			SizeF texts = g.MeasureString(notetxt, notefont, new SizeF(w,float.MaxValue),StringFormat.GenericDefault);
			g.Dispose();
			//to do
		    texts.Width = Math.Max(texts.Width, w);

            texts.Width += 1.0f;
			texts.Height += 1.0f;
			return texts.ToSize();
		}


		private Bitmap GetPreviewNoteImage(Rectangle imgsize,int realWidth,string notetxt,Font notefont,bool isVertical)
		{
			Bitmap bmp = new Bitmap(imgsize.Width, imgsize.Height);
			Graphics grp = Graphics.FromImage(bmp);
			Bitmap retBmp = this.CreatNoteImage(realWidth,notetxt,notefont,isVertical);
            retBmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            grp.DrawImage(retBmp,imgsize,new Rectangle(0,0,retBmp.Width,retBmp.Height),GraphicsUnit.Pixel);
			grp.Dispose();
            retBmp.Dispose();
			return bmp;
		}


		private Bitmap CreatNoteImage(int width,string notetxt,Font notefont,bool isVertical)
		{
			string text = notetxt;
			Size texts = new Size();
			texts = getNoteSize(width,text,notefont,isVertical);

			int imgWidth = texts.Width;
			int imgHeight = texts.Height;

            Bitmap retBmp = new Bitmap(imgWidth, imgHeight, PixelFormat.Format24bppRgb);
			retBmp.SetResolution(PrtFileInfo.sFreSetting.nResolutionX*PrtFileInfo.sImageInfo.nImageResolutionX,
				PrtFileInfo.sFreSetting.nResolutionY*PrtFileInfo.sImageInfo.nImageResolutionY);

			Graphics grp = Graphics.FromImage(retBmp);

			int tnWidth = imgWidth, tnHeight = imgHeight;
			if (retBmp.Width > retBmp.Height)
				tnHeight = Convert.ToInt32(((float)retBmp.Height / (float)retBmp.Width) * tnWidth);
			else if (retBmp.Width < retBmp.Height)
				tnWidth = Convert.ToInt32(((float)retBmp.Width / (float)retBmp.Height) * tnHeight);

			int iLeft = (imgWidth / 2) - (tnWidth / 2);
			int iTop = (imgHeight / 2) - (tnHeight / 2);

//			grp.PixelOffsetMode = PixelOffsetMode.None;
//			grp.InterpolationMode = InterpolationMode.HighQualityBicubic;
			Matrix myMatrix1 = grp.Transform;
			Matrix myMatrix2 = new Matrix();
			myMatrix2 = new Matrix(1, 0, 0, -1, 0, retBmp.Height);
			myMatrix1.Multiply(myMatrix2, MatrixOrder.Append);
			grp.Transform = myMatrix1;

			Brush brush = new SolidBrush(Color.Black);
			Rectangle textRec = new Rectangle((imgWidth / 2) - (retBmp.Width / 2), (imgHeight / 2) - (retBmp.Height / 2), retBmp.Width, retBmp.Height);
			grp.FillRectangle(new SolidBrush(Color.White),textRec);

			grp.DrawString(text, notefont, brush, textRec,StringFormat.GenericDefault);

			//			Pen pn = new Pen(borderColor, 1); //Color.Wheat
			//			grp.DrawRectangle(pn, 0, 0, retBmp.Width - 1, retBmp.Height - 1);
			grp.Dispose();

			if(NoteImageFileName == null || NoteImageFileName == string.Empty)
			{
				string path = Path.Combine(System.Windows.Forms.Application.StartupPath,"Preview");
				string fpath = this.src;
				if(fpath == null || fpath == string.Empty)
					fpath = this.TopSrc;
				this.NoteImageFileName = Path.Combine(path,GeneratePreviewName(fpath));
			}
			retBmp.Save(NoteImageFileName,ImageFormat.Bmp);
			return retBmp;
		}

		public void SetMode(bool bsmode)
		{
			if(IsParent)
			{
				for(int i = 0; i< this.Childs.Length; i++)
				{
					this.Childs[i].SetMode(bsmode);
				}
			}
			else
			{
				this.isSimpleMode = bsmode;
			}
		}


		public Bitmap CreateClipsMiniature()
		{
			string text =GetRealNoteText(this);

			float CoefficientX,CoefficientY;
			int marginl,marginr,margint,marginb;
            int xdis, xdis2, ydis;
			int left,top,notemargin;

			string fpath = this.src;
			if(string.IsNullOrEmpty(fpath))
				fpath = this.TopSrc;

		    Image srcMiniature = null;
		    if (File.Exists(SrcMiniature))
                srcMiniature = new Bitmap(SrcMiniature);
            if (srcMiniature == null)
			{
                SPrtFileInfo jobInfo = new SPrtFileInfo();
                IntPtr imgadataPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SPrtImagePreview)));
                jobInfo.sImageInfo.nImageData = imgadataPtr;
                int bret = CoreInterface.Printer_GetFileInfo(fpath, ref this.PrtFileInfo, 1);
                srcMiniature = SerialFunction.CreateImageWithImageInfo(this.PrtFileInfo.sImageInfo);
                Marshal.FreeHGlobal(imgadataPtr);
            }

            CoefficientX = (float)PrtFileInfo.sImageInfo.nImageWidth / srcMiniature.Width;
            CoefficientY = (float)PrtFileInfo.sImageInfo.nImageHeight / srcMiniature.Height;
			Rectangle scaledclip = new Rectangle();
			if(noClip)
                scaledclip = new Rectangle(0, 0, srcMiniature.Width, srcMiniature.Height);
			else
				scaledclip = new Rectangle( Convert.ToInt32(this.ClipRect.Left/ CoefficientX),Convert.ToInt32(this.ClipRect.Top/CoefficientY),
					Convert.ToInt32(this.ClipRect.Width/ CoefficientX),Convert.ToInt32(this.ClipRect.Height/CoefficientY));
			marginl = Convert.ToInt32(Margin_L/CoefficientX);
			marginr = Convert.ToInt32(Margin_R/CoefficientX);
			margint = Convert.ToInt32(Margin_T/CoefficientY);
			marginb = Convert.ToInt32(Margin_B/CoefficientY);
            xdis = Convert.ToInt32(XDis / CoefficientX);
            xdis2 = Convert.ToInt32(XDis2 / CoefficientX);
			ydis = Convert.ToInt32(YDis/CoefficientY);
			left = Convert.ToInt32(Left/CoefficientX);
			top = Convert.ToInt32(Top/CoefficientY);
			if(this.NotePosition == 0 ||this.NotePosition == 2)
				notemargin = Convert.ToInt32(NoteMargin/CoefficientX);
			else
				notemargin = Convert.ToInt32(NoteMargin/CoefficientY);
            Rectangle size = GetBound(this);

		    int maxSize = 600*800*3;// 单个预览图最大尺寸
		    float dstCoefficient = 1;
		    if (size.Width/CoefficientX*size.Height/CoefficientY > maxSize)
		    {
		        dstCoefficient =(float) Math.Sqrt((size.Width/CoefficientX*size.Height/CoefficientY)/maxSize);
                dstCoefficient = Math.Min(20, dstCoefficient); // dstCoefficient最大不超过20
		    }
            size.Width = (int)Math.Max(1, (float)size.Width / dstCoefficient);
            size.Height = (int)Math.Max(1, (float)size.Height / dstCoefficient);
            Bitmap ret = null;
			Graphics g;
			try
			{
				ret = new Bitmap(Math.Max(1,Convert.ToInt32(size.Width/CoefficientX)),Math.Max(1,Convert.ToInt32(size.Height/CoefficientY)));
				g = Graphics.FromImage(ret);

				if(isSimpleMode)
				{
                    int w = (int)(scaledclip.Width / dstCoefficient);
                    int h = (int)(scaledclip.Height / dstCoefficient);
                    int marginlInt = (int)(marginl / dstCoefficient);
                    int margintInt = (int)(margint / dstCoefficient);
                    int xdisInt = (int)(xdis / dstCoefficient);
                    int xdis2Int = (int)(xdis2 / dstCoefficient);
                    int ydisInt = (int)(ydis / dstCoefficient);
					for(int i =0;i<this.XCnt;i++)
					{
                        int left1 = marginlInt + i * (w + xdisInt);
					    if (IsNktTreeTileMode && i == 2)
					    {
                            left1 = marginlInt + (w + xdisInt) + (w + xdis2Int);
					    }
					    int top1 = 0;
                        for(int j =0 ;j<this.YCnt;j++)
						{
							top1 = margintInt + j*(h + ydisInt);
                            Rectangle dstrect = new Rectangle(left1, top1, w, h);
                            g.DrawImage(srcMiniature, dstrect, scaledclip, GraphicsUnit.Pixel);
                            Debug.WriteLine("====CreateClipsMiniature===" + dstrect.ToString());
						}
					    if (YAddtion > 0)
					    {
                            top1 += this.YCnt > 0 ?(h + ydisInt):0;
					        Rectangle scaledclip1 = new Rectangle();
					        if (noClip)
                                scaledclip1 = new Rectangle(0, 0, srcMiniature.Width, srcMiniature.Height);
					        else
					            scaledclip1 = new Rectangle(Convert.ToInt32(this.ClipRect.Left/CoefficientX), Convert.ToInt32(this.ClipRect.Top/CoefficientY),
                                                    Convert.ToInt32(this.ClipRect.Width / CoefficientX), Convert.ToInt32(this.YAddtion / CoefficientY));
                            Rectangle dstrect = new Rectangle(left1, top1, w, h);
                            g.DrawImage(srcMiniature, dstrect, scaledclip1, GraphicsUnit.Pixel);
					    }
					}
					if(text != null && text != string.Empty)
					{
                        int notetop = margint + YCnt * (scaledclip.Height + ydis) + this.YAddtion - ydis + notemargin;
                        notetop =(int)(notetop/ dstCoefficient);
                        Rectangle nrect = new Rectangle(marginl,0,ret.Width -marginl-marginr,ret.Height - marginb-notetop);
						if(nrect.Size.Width != 0 && nrect.Size.Height != 0)
						{
						    Font prevewFont = this.GetNoteFont();
                            Bitmap bmp = GetPreviewNoteImage(nrect, size.Width, text, prevewFont, this.NotePosition == 0 || this.NotePosition == 2);
                            Rectangle dstrect = new Rectangle(marginlInt, (int)(ret.Height - marginb - bmp.Height), (int)(bmp.Width / dstCoefficient), (int)(bmp.Height / dstCoefficient));
							g.DrawImage(bmp,dstrect,new Rectangle(0,0,bmp.Width,bmp.Height),GraphicsUnit.Pixel);
						}
						else
						{
							this.CreatNoteImage(size.Width,text,this.GetNoteFont(),this.NotePosition == 0 ||this.NotePosition == 2);
						}
					}
				}
				else
				{
					if(IsParent)
					{
						for(int i =0;i<this.Childs.Length;i++)
						{	
							Bitmap bmp = this.Childs[i].CreateClipsMiniature();
							RectangleF dstrect = new RectangleF(left/dstCoefficient,top/dstCoefficient,bmp.Width/dstCoefficient,bmp.Height/dstCoefficient);
							g.DrawImage(bmp,dstrect,new Rectangle(0,0,bmp.Width,bmp.Height),GraphicsUnit.Pixel);
						}
					}
					else
					{
                        RectangleF dstrect = new RectangleF(marginl / dstCoefficient, margint / dstCoefficient, scaledclip.Width / dstCoefficient, scaledclip.Height / dstCoefficient);
                        g.DrawImage(srcMiniature, dstrect, scaledclip, GraphicsUnit.Pixel);
						if(text != null && text != string.Empty)
						{
							int notetop =margint + YCnt*(scaledclip.Height + ydis) -ydis + notemargin;
							Rectangle nrect = new Rectangle(marginl,notetop,ret.Width -marginl-marginr,ret.Height - marginb-notetop);
							Bitmap bmp = GetPreviewNoteImage(nrect,size.Width,text,this.GetNoteFont(),this.NotePosition == 0 ||this.NotePosition == 2);
                            dstrect = new RectangleF(marginl / dstCoefficient, (margint + scaledclip.Height + notemargin) / dstCoefficient, bmp.Width / dstCoefficient, bmp.Height / dstCoefficient);
							g.DrawImage(bmp,dstrect,new Rectangle(0,0,bmp.Width,bmp.Height),GraphicsUnit.Pixel);
						}
					}
				}
				g.Dispose();
                srcMiniature.Dispose();
                //GC.Collect();
			}
			catch(Exception ex)
			{
				ret = null;
				Debug.Assert(false,ex.Message + ex.StackTrace);
			}
			return ret;
		}

	    /// <summary>
	    /// 更新note数据,电压和脉宽参数
	    /// </summary>
	    public void UpdateNoteTextAndImage(SPrinterSetting ss)
	    {
	        realNoteText = string.Empty;
	        realNoteText = GetRealNoteText(this, ss);
	        Rectangle size = GetBound(this);

	        NoteImageFileName = string.Empty;
	        this.CreatNoteImage(size.Width, realNoteText, GetNoteFont(), this.NotePosition == 0 || this.NotePosition == 2);
	    }


	    public JobClip Clone()
		{
			JobClip ret =(JobClip)MemberwiseClone();
//			if(this.Miniature!= null)
//				ret.Miniature = (Image)this.Miniature.Clone();
			if(this.SrcMiniature!= null)
				ret.SrcMiniature = (string)this.SrcMiniature.Clone();
			
			return ret;
		}


		public string SystemConvertToXml()
		{
			string xml = "<JobClip>";
			xml += PubFunc.SystemConvertToXml(src,typeof(string));
			xml += PubFunc.SystemConvertToXml(TopSrc,typeof(string));
			xml +=PubFunc.SystemConvertToXml(PrtFileInfo,typeof(SPrtFileInfo));
			xml +=PubFunc.SystemConvertToXml(ClipRect,typeof(Rectangle));
			xml +=PubFunc.SystemConvertToXml(W,typeof(int));
			xml +=PubFunc.SystemConvertToXml(H,typeof(int));
			xml +=PubFunc.SystemConvertToXml(Rotation,typeof(int));
			xml +=PubFunc.SystemConvertToXml(Left,typeof(int));
			xml +=PubFunc.SystemConvertToXml(Top,typeof(int));
			xml +=PubFunc.SystemConvertToXml(Note,typeof(string));
//			xml +=PubFunc.SystemConvertToXml(NoteFontName,typeof(string));
//			xml += PubFunc.SystemConvertToXml(NoteFontSize,typeof(float));
			FontConverter fc = new FontConverter();
			xml += PubFunc.SystemConvertToXml(fc.ConvertToString(NoteFont),typeof(string));
			xml +=PubFunc.SystemConvertToXml(NoteMargin,typeof(int));
			xml +=PubFunc.SystemConvertToXml(NotePosition,typeof(int));
			if(Childs != null)
			{
				for(int i = 0; i<Childs.Length ;i++)
					xml +=Childs[i].SystemConvertToXml();
			}
			else
				xml +=PubFunc.SystemConvertToXml(Childs,typeof(string));
			xml +=PubFunc.SystemConvertToXml(Margin_L,typeof(int));
			xml +=PubFunc.SystemConvertToXml(Margin_R,typeof(int));
			xml +=PubFunc.SystemConvertToXml(Margin_T,typeof(int));
			xml +=PubFunc.SystemConvertToXml(Margin_B,typeof(int));
			xml +=PubFunc.SystemConvertToXml(XCnt,typeof(int));
			xml +=PubFunc.SystemConvertToXml(YCnt,typeof(int));
			xml +=PubFunc.SystemConvertToXml(XDis,typeof(int));
			xml +=PubFunc.SystemConvertToXml(YDis,typeof(int));
			xml +=PubFunc.SystemConvertToXml(IsParent,typeof(bool));
			xml +=PubFunc.SystemConvertToXml(AutoSizeToContent,typeof(bool));
			xml +=PubFunc.SystemConvertToXml(isSimpleMode,typeof(bool));
			xml +=PubFunc.SystemConvertToXml(noClip,typeof(bool));
			xml +=PubFunc.SystemConvertToXml(NoteImageFileName,typeof(string));
			xml +=PubFunc.SystemConvertToXml(AddtionInfoMask,typeof(int));
            xml += PubFunc.SystemConvertToXml(XAddtion, typeof(int));
            xml += PubFunc.SystemConvertToXml(YAddtion, typeof(int));
            xml += PubFunc.SystemConvertToXml(XDis2, typeof(int));
            xml += "</JobClip>";
			return xml;
		}


		public static JobClip SystemConvertFromXml(XmlElement jobElemenet,Type type)
		{	
			JobClip job =new JobClip();
			XmlNode currNode = jobElemenet.FirstChild;
			job.src = (string)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(string));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.TopSrc = (string)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(string));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.PrtFileInfo  = (SPrtFileInfo)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(SPrtFileInfo));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.ClipRect = (Rectangle)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(Rectangle));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.W = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.H = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.Rotation = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.Left = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.Top = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.Note = (string)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(string));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
//			job.NoteFontName = (string)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(string));
//			currNode = currNode.NextSibling;
//			if(currNode == null ) return job;
//			job.NoteFontSize = (float)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(float));
			FontConverter fc = new FontConverter();
			if(currNode.InnerText!=null &&currNode.InnerText!=string.Empty)
				job.NoteFont = (Font)fc.ConvertFromString(currNode.InnerText);
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.NoteMargin = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.NotePosition = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			if(currNode.ChildNodes.Count>0)
				job.Childs = new JobClip[currNode.ChildNodes.Count];
			for(int i =0;i<currNode.ChildNodes.Count;i++)
				job.Childs[i] = JobClip.SystemConvertFromXml((XmlElement)currNode.ChildNodes[i],typeof(JobClip));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.Margin_L = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.Margin_R = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.Margin_T = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.Margin_B = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.XCnt = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.YCnt = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.XDis = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.YDis = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.IsParent = (bool)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(bool));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.AutoSizeToContent = (bool)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(bool));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.isSimpleMode = (bool)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(bool));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.noClip = (bool)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(bool));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.NoteImageFileName = (string)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(string));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.AddtionInfoMask = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
            job.XAddtion = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(int));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.YAddtion = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(int));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.XDis2 = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(int));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            return job;
		}
		

		private string GeneratePreviewName(string jobName)
		{
            string m_PreviewFolder = "Preview";
            string previewName = Path.GetFileNameWithoutExtension(jobName);
            string mPreviewFolder = System.Windows.Forms.Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder;
            if (!Directory.Exists(mPreviewFolder))
            {
                Directory.CreateDirectory(mPreviewFolder);
            }
            string mPreviewFile = previewName;
            for (int i = 0; i < int.MaxValue; i++)
            {
                mPreviewFile = previewName + "_Note_" + i.ToString("D3") + ".bmp";
                string cur = mPreviewFolder + Path.DirectorySeparatorChar + mPreviewFile;
                if (!File.Exists(cur))
                    return mPreviewFile;
            }
			return "";
		}

	
		/// <summary>
		/// 计算输入纸宽可用最大拼贴数
		/// </summary>
		/// <param name="realpaperW">最终可用纸宽,去掉彩条和边距等</param>
		/// <returns></returns>
		public int CalcXMaxCount(int realpaperW)
		{
			int realPgW = (realpaperW + this.XDis);
			int width = this.ClipRect.Width;// / ResolutionX;
			if(noClip)
				width = this.PrtFileInfo.sImageInfo.nImageWidth;
			string text =GetRealNoteText(this);
			if((text != null && text != string.Empty) && (this.NotePosition == 0 ||this.NotePosition == 2))
			{
				int subwidth = width * this.XCnt + this.XDis*(this.XCnt-1);
				Size notesize = this.getNoteSize(subwidth,text,this.GetNoteFont(),false);
				realPgW -= notesize.Height + this.NoteMargin;
			}
			return (int)(realPgW/(width + this.XDis));
		}
	}
}
