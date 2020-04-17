/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;
using System.Timers;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Resources;
using System.Xml.Serialization;
using BYHXPrinterManager.Preview;

namespace BYHXPrinterManager.JobListView
{
	/// <summary>
	/// Summary description for UIJob.
	/// </summary>
	public class UIJob
	{
		public JobStatus			Status				= JobStatus.Unknown;
	    private string				_name				= null;
		private int					copies				= 1;
        private DateTime printedDate = DateTime.Now;
		private string				sFileLocation		= null;
		private string				sFileLocation2		= null;
		public int					LangID				= 0;
	    public int _JobID;

		public string				Miniature			= null;
        //public Image ThumbnailImage = null;
        
        private string preViewFile = null;
        private string tilePreViewFile = null;

        public SPrtFileInfo			PrtFileInfo;		
		public SPrtFileInfo			PrtFileInfo2;		
		public JobClip Clips;
		private bool bIsClip;
		private bool bIsTile;
		private bool isSimpleMode;
		public SJobSetting_UI sJobSetting;
        private bool bIsBackSideJob;
	    private bool bIsCreatingDoubleSideFile;
        private float fSetWidth;
        private float fSetHeight;
        private bool bIsHeight;

		public UIJob()
		{
			Status				= JobStatus.Unknown;
			_name				= null;
			copies				= 1;
			Miniature			= null;
            //ThumbnailImage = null;
			PrintedDate			= DateTime.Now;
			sFileLocation	=sFileLocation2	= null;
			LangID				= 0;
			PrtFileInfo		=PrtFileInfo2	= new SPrtFileInfo();
			Clips				= new JobClip(false);
			bIsClip=bIsTile = false;
			IsSimpleMode = true;
			sJobSetting= new SJobSetting_UI();
		    _JobID = 0;
		}

	    public string TilePreViewFile
	    {
            get
            {
                //if (string.IsNullOrEmpty(tilePreViewFile))
                //{
                //    tilePreViewFile = GeneratePreviewName(FileLocation);
                //}
                return tilePreViewFile;
            }
            set
            {
                tilePreViewFile = value;
            }
        }
        public string PreViewFile
        {
            get
            {
                //if (string.IsNullOrEmpty(preViewFile))
                //{
                //    preViewFile = GeneratePreviewName(FileLocation);
                //}
                return preViewFile;
            }
            set
            {
                preViewFile = value;
            }
        }
	    public int Copies
	    {
            get { return this.copies; }
            set
            {
                if(copies!=value)
                {
                    copies = value;
                }
            }
	    }
        public DateTime PrintedDate
        {
            get { return printedDate; }
            set { printedDate = value; }
        }
		public SizeF JobSize
		{
			get
			{
				if(ResolutionX == 0 || ResolutionY == 0)
				{
					return SizeF.Empty;
				}
			
				float width = (float)Dimension.Width / (float)ResolutionX;
				float height = (float)Dimension.Height / (float)ResolutionY;

				return new SizeF(width,height);

			}
		}
        /// <summary>
        /// 作业的打印总长度
        /// </summary>
        public double PrintHeight { get; set; }
        /// <summary>
        /// 本次打印作业已完成整的Cpoys的使用时间
        /// </summary>
        public TimeSpan UsedTime = TimeSpan.Zero;
        private TimeSpan m_allCopiesTime = TimeSpan.Zero;
        public TimeSpan AllCopiesTime
        {
            get { return m_allCopiesTime; }
            set
            {
                if (m_allCopiesTime != value)
                {
                    m_allCopiesTime = value;
                }
            }
        }
        public bool IsAbort = false;
		public int ResolutionX
		{
			get
			{
				return PrtFileInfo.sFreSetting.nResolutionX*PrtFileInfo.sImageInfo.nImageResolutionX;
			}
		}

		public int ResolutionY
		{
			get
			{
				return PrtFileInfo.sFreSetting.nResolutionY*PrtFileInfo.sImageInfo.nImageResolutionY;
			}
		}
		public Size Dimension
		{
			get
			{
				if(!IsClipOrTile)
				{
					return new Size(PrtFileInfo.sImageInfo.nImageWidth,PrtFileInfo.sImageInfo.nImageHeight);
				}
				else
				{
					return Clips.JobSize;
				}
			}
		}

		public int PassNumber
		{
			get
			{
				return PrtFileInfo.sFreSetting.nPass;
			}
		}

		public byte PrintingDirection
		{
			get
			{
				return PrtFileInfo.sFreSetting.nBidirection;
			}
		}

		public UIJob Clone()
		{
			UIJob ret = (UIJob)MemberwiseClone();
			if(this.Miniature!= null)
				ret.Miniature = (string)this.Miniature.Clone();
            if (this.TilePreViewFile != null)
                ret.TilePreViewFile = (string)this.TilePreViewFile.Clone();
            //if (ThumbnailImage != null)
            //    ret.ThumbnailImage = (Image)this.ThumbnailImage.Clone();
			ret.Clips = this.Clips.Clone();
			return ret;
		}

		public int ColorDeep
		{
			get
			{
				return PrtFileInfo.sImageInfo.nImageColorDeep;
			}
		}
		
		public bool IsSimpleMode
		{
			get
			{
				return isSimpleMode;
			}
			set
			{
				isSimpleMode = value;
				if(IsClipOrTile)
					Clips.SetMode(isSimpleMode);
			}
		}

		public bool IsClip
		{
			get
			{
				return bIsClip;
			}
			set
			{
				bIsClip = value;
			}
		}

		public bool IsTile
		{
			get
			{
				return bIsTile;
			}
			set
			{
				bIsTile = value;
			}
		}

		public string FileLocation
		{
			get
			{
				return sFileLocation;
			}
			set
			{
				sFileLocation = value;
				this.Clips.TopSrc=this.Clips.src = sFileLocation;
			}
		}

		public string FileLocation2
		{
			get
			{
				return sFileLocation2;
			}
			set
			{
				sFileLocation2 = value;
			}
		}

        public Padding JobMargin
	    {
            get
            {
                if (this.myClips != null)
                    return new Padding(this.myClips.Margin_L, this.myClips.Margin_T, this.myClips.Margin_R,
                                       this.myClips.Margin_B);
                return new Padding();
            }
	    }

		public bool IsClipOrTile
		{
			get
			{
				return this.ClipCase != ClipCaseEnum.Default  ;
			}
		}
		public ClipCaseEnum ClipCase
		{
			get
			{
				bool hasNote = Clips.AddtionInfoMask != 0 || (Clips.Note != null && Clips.Note!=string.Empty);
			    if (!bIsTile && !bIsClip&&!hasNote)
                    return ClipCaseEnum.Default;
				else if(!bIsTile && bIsClip && !hasNote)
					return ClipCaseEnum.OnlyClip;
				else if(bIsTile && !bIsClip && !hasNote)
					return ClipCaseEnum.OnlyTile;
				else if(!bIsTile && bIsClip && hasNote)
					return ClipCaseEnum.OnlyClipAndNote;
				else if(bIsTile && !bIsClip && hasNote)
					return ClipCaseEnum.OnlyTileAndNote;
				else if(bIsTile && bIsClip && !hasNote)
					return ClipCaseEnum.OnlyClipAndTile;
				else if(bIsTile && bIsClip && hasNote)
					return ClipCaseEnum.ClipAndTileAndNote;
                else if (!bIsTile && !bIsClip && hasNote)
                    return ClipCaseEnum.OnlyTileAndNote;
                else
					return ClipCaseEnum.Default;
			}
		}

	    public string Name
	    {
	        get { return _name; }
            set { _name = value;
            }
	    }

	    public int JobID
	    {
            get { return _JobID; }
            set { _JobID = value; }
	    }

	    public bool IsBackSideJob
	    {
            get
            {
                string fileName = Path.GetFileNameWithoutExtension(this.sFileLocation);
                return fileName != null && fileName.EndsWith(CoreConst.DoublePrintPrtPosName);
            }
	        //set { bIsBackSideJob = value; }
	    }

	    public bool IsCreatingDoubleSideFile
	    {
            get
            {
                return bIsCreatingDoubleSideFile;
            }
	        set
	        {
	            bIsCreatingDoubleSideFile = value;
	        }
	    }

        public float SetWidth
        {
            get
            {
                return fSetWidth;
            }
            set
            {
                fSetWidth = value;
            }
        }

        public float SetHeight
        {
            get
            {
                return fSetHeight;
            }
            set
            {
                fSetHeight = value;
            }
        }

        public bool IsHeight
        {
            get
            {
                return bIsHeight;
            }
            set
            {
                bIsHeight = value;
            }
        }

        //public byte[] ReadBuffer(int startindex,int readLen)
        //{
        //    return Clips.ReadBuffer(startindex,readLen);
        //}

		public string SystemConvertToXml()
		{
			string xml = "<UIJob>";
			xml += PubFunc.SystemConvertToXml(Status,typeof(JobStatus));
			xml +=PubFunc.SystemConvertToXml(_name,typeof(string));
			xml +=PubFunc.SystemConvertToXml(copies,typeof(int));
			xml +=PubFunc.SystemConvertToXml(PrintedDate,typeof(DateTime));
 			xml +=PubFunc.SystemConvertToXml(FileLocation,typeof(string));
			xml +=PubFunc.SystemConvertToXml(LangID,typeof(int));
			//xml +=PubFunc.SystemConvertToXml(Miniature,typeof(Image));
			xml +=PubFunc.SystemConvertToXml(PreViewFile,typeof(string));
			xml +=PubFunc.SystemConvertToXml(PrtFileInfo,typeof(SPrtFileInfo));
			xml += Clips.SystemConvertToXml();//PubFunc.SystemConvertToXml(Clips,typeof(JobClip));
			xml +=PubFunc.SystemConvertToXml(bIsClip,typeof(bool));
			xml +=PubFunc.SystemConvertToXml(bIsTile,typeof(bool));
			xml +=PubFunc.SystemConvertToXml(isSimpleMode,typeof(bool));
			xml +=PubFunc.SystemConvertToXml(sJobSetting,typeof(SJobSetting_UI));
            xml += PubFunc.SystemConvertToXml(TilePreViewFile, typeof(string));
            xml += PubFunc.SystemConvertToXml(FileLocation2, typeof(string));
            xml += PubFunc.SystemConvertToXml(PrintHeight, typeof(double));
            xml += PubFunc.SystemConvertToXml(fSetWidth, typeof(float));
            xml += PubFunc.SystemConvertToXml(fSetHeight, typeof(float));
            xml += PubFunc.SystemConvertToXml(bIsHeight, typeof(bool));
          	xml += "</UIJob>";
			return xml;
		}

		public static object SystemConvertFromXml(XmlElement jobElemenet,Type type)
		{	
			UIJob job =new UIJob();
			XmlNode currNode = jobElemenet.FirstChild;
			job.Status = (JobStatus)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(JobStatus));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.Name  = (string)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(string));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.Copies = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.PrintedDate = (DateTime)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(DateTime));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.FileLocation = (string)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(string));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.LangID = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(int));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			//				job.Miniature = (Image)PubFunc.SystemConvertFromXml(cur.OuterXml,typeof(Image));
			//			currNode = currNode.NextSibling;
			//			if(currNode == null ) return job;
			job.PreViewFile = (string)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(string));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.PrtFileInfo = (SPrtFileInfo)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(SPrtFileInfo));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.Clips = JobClip.SystemConvertFromXml((XmlElement)currNode,typeof(JobClip));//)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(JobClip));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.bIsClip = (bool)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(bool));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.bIsTile = (bool)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(bool));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.IsSimpleMode = (bool)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(bool));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
			job.sJobSetting = (SJobSetting_UI)PubFunc.SystemConvertFromXml(currNode.OuterXml,typeof(SJobSetting_UI));
			currNode = currNode.NextSibling;
			if(currNode == null ) return job;
            job.TilePreViewFile = (string)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(string));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.FileLocation2 = (string)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(string));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.PrintHeight = (double)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(double));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            
            job.fSetWidth = (float)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(float));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.fSetHeight = (float)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(float));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.bIsHeight = (bool)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(bool));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
			return job;
		}


        const string m_PreviewFolder = "Preview";
        public string GeneratePreviewName(bool bclip)
        {
            string previewName = Path.GetFileNameWithoutExtension(FileLocation);
            string mPreviewFolder = Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder;
            if (!Directory.Exists(mPreviewFolder))
            {
                Directory.CreateDirectory(mPreviewFolder);
            }
            string mPreviewFile = previewName;
            for (int i = 0; i < 1000; i++)
            {
                if(bclip)
                    mPreviewFile = previewName + "_" + i.ToString("D3") + "_clip.bmp";
                else
                    mPreviewFile = previewName + "_" + i.ToString("D3") + ".bmp";
                string cur = mPreviewFolder + Path.DirectorySeparatorChar + mPreviewFile;
                if (!File.Exists(cur))
                    return mPreviewFile;
            }
            return "";
        }

		private int ReadInt(byte []buffer,int offset)
		{
			int ret = buffer[offset] + (buffer[offset+1]<<8)+ (buffer[offset+2]<<16)+ (buffer[offset+3]<<24);
			return ret;
		}
		private void WriteInt(byte []buffer,int offset,int Value)
		{
			buffer[offset] =  (byte)(Value&0xff);
			buffer[offset+1] = (byte)((Value>>8)&0xff);
			buffer[offset+2] = (byte)((Value>>16)&0xff);
			buffer[offset+3] = (byte)((Value>>24)&0xff);
		}

		private int OneStepImageHeight = 0;
		private int nsrcBytePerLine = 0;
		private int ndstBytePerLine = 0;
		private int colorNum = 0;
		private JobClip myClips;
        private int srcCurhindex = 0;
        private int srcBytePlineAllColor = 0;
        private long srcStartBytePos = 0;

	    /// <summary>
	    /// 读取拼贴后文件头数据
	    /// </summary>
	    /// <param name="step"></param>
	    /// <param name="reader"></param>
	    /// <param name="buffer"></param>
	    /// <param name="MaxBufSize"></param>
	    /// <param name="bFirst"></param>
	    /// <param name="bTerminal"></param>
	    /// <param name="bReversePrint"></param>
	    /// <returns></returns>
        public int ReadPrintFileHeader(JobFileReader reader,
	        byte[] buffer, int MaxBufSize, bool bReversePrint)
	    {
	        int ret = 0;
	        long seekpos = 0;
	        srcWidth = srcHeight = 0;

            int readBytes = reader.ReadHeader(buffer);
                srcWidth = reader.SrcWidth;
                srcHeight = reader.SrcHeight;
                colorNum = reader.ColorNum;
                nsrcBytePerLine = reader.BytePerLine;
                int offset = 2 * 4;
                srcBytePlineAllColor = nsrcBytePerLine * colorNum;
                myClips = Clips;
	        if (!IsClip)
	        {
	            myClips.ClipRect = new Rectangle(0, 0, srcWidth, srcHeight);
	        }
	        if (!IsTile)
	        {
	            myClips.XCnt = 1;
	            myClips.YCnt = 1;
	        }

	        //////////////////////////e//////////////////////////
	        if (srcWidth < myClips.ClipRect.Width + myClips.ClipRect.X)
	        {
	            myClips.ClipRect.Width = srcWidth - myClips.ClipRect.X;
	        }

	        if (srcHeight < myClips.ClipRect.Height + myClips.ClipRect.Y)
	        {
	            myClips.ClipRect.Height = srcHeight - myClips.ClipRect.Y;
	        }

	        if (JobFileReader.IsCsPrt)
	            ndstBytePerLine = (myClips.W*this.ColorDeep + 7)/8;
	        else
	            ndstBytePerLine = (myClips.W*this.ColorDeep + 31)/32*4;
	        offset = 2*4;
	        WriteInt(buffer, offset, myClips.W);
	        offset = 3*4;
            if (IsHeight)
            {
                WriteInt(buffer, offset, CoreInterface.JobLineCount);
            }
            else
            {
                WriteInt(buffer, offset, myClips.H);
            }
	        offset = 9*4;
	        WriteInt(buffer, offset, ndstBytePerLine);

	        OneStepImageHeight = Math.Min((MaxBufSize - JobFileReader.HEADERSIZE)/(ndstBytePerLine*colorNum),
	            (MaxBufSize - JobFileReader.HEADERSIZE)/(nsrcBytePerLine*colorNum));
	        if (OneStepImageHeight > myClips.ClipRect.Height)
	            OneStepImageHeight = myClips.ClipRect.Height;
	        else if (OneStepImageHeight < 1)
	            OneStepImageHeight = 1;
	        srcStartBytePos = JobFileReader.HEADERSIZE + myClips.ClipRect.Y*(long) srcBytePlineAllColor;
	        srcCurhindex = 0;
	        if (!bReversePrint)
	            reader.Seek(srcStartBytePos, SeekOrigin.Begin);
	        ret += JobFileReader.HEADERSIZE;
	        return ret;
	    }


	    /// <summary>
        /// 目前只支持字节对齐宽度的剪切拼贴,否则有问题
        /// </summary>
        /// <param name="step"></param>
        /// <param name="reader"></param>
        /// <param name="buffer"></param>
        /// <param name="MaxBufSize"></param>
        /// <param name="bFirst"></param>
        /// <param name="bTerminal"></param>
        /// <param name="bReversePrint"></param>
        /// <returns></returns>
        public long LineCount = 0;
        public int ReadPrintBuf(int step, JobFileReader reader,
            byte[] buffer, int MaxBufSize, bool bFirst, out bool bTerminal, bool bReversePrint, out bool bEnd)
        {
            bTerminal = false;
            bEnd = false;
            int bufIndex = 0;
            int ret = 0;
            long seekpos = 0;
            if (bFirst)
            {
                srcWidth = srcHeight = 0;
                int readBytes = reader.ReadHeader(buffer);
                srcWidth = reader.SrcWidth;
                srcHeight = reader.SrcHeight;
                colorNum = reader.ColorNum;
                nsrcBytePerLine = reader.BytePerLine;
                srcBytePlineAllColor = nsrcBytePerLine * colorNum;
                myClips = Clips;
                if (!IsClip)
                {
                    myClips.ClipRect = new Rectangle(0, 0, srcWidth, srcHeight);
                }
                if (!IsTile)
                {
                    myClips.XCnt = 1;
                    myClips.YCnt = 1;
                }

                //////////////////////////e//////////////////////////
                if (srcWidth < myClips.ClipRect.Width + myClips.ClipRect.X)
                {
                    myClips.ClipRect.Width = srcWidth - myClips.ClipRect.X;
                }

                if (srcHeight < myClips.ClipRect.Height + myClips.ClipRect.Y)
                {
                    myClips.ClipRect.Height = srcHeight - myClips.ClipRect.Y;
                }

                //if(srcWidth < myClips.ClipRect.Width  *  myClips.XCnt + myClips.XDis *(myClips.XCnt -1))
                //{
                //    myClips.ClipRect.Width = (myClips.W- myClips.XDis *(myClips.XCnt -1))/myClips.XCnt;
                //}
                if (JobFileReader.IsCsPrt)
                    ndstBytePerLine = (myClips.W * this.ColorDeep + 7) / 8;
                else
                    ndstBytePerLine = (myClips.W * this.ColorDeep + 31) / 32 * 4;
                srcStartBytePos = JobFileReader.HEADERSIZE + myClips.ClipRect.Y * (long)srcBytePlineAllColor;
                srcCurhindex = 0;
                if (!bReversePrint)
                    reader.Seek(srcStartBytePos, SeekOrigin.Begin);
            }
            {
                int size = OneStepImageHeight * colorNum * ndstBytePerLine;
                for (int i = 0; i < size; i++)
                    buffer[i] = 0;
            }
            if (OneStepImageHeight != 0)
            {
                int curStepImageHeight = OneStepImageHeight;
                int curHeight = 0;
                int srcBlockSize = OneStepImageHeight * srcBytePlineAllColor;
                byte[] srcBuf = new byte[srcBlockSize];
                int curDesY = (step * OneStepImageHeight);
                int bottomDstY = ((step + 1) * OneStepImageHeight);
                int dstBottomY = (int)(myClips.ClipRect.Height * myClips.YCnt + myClips.YDis * (myClips.YCnt - 1) + myClips.YAddtion);
                int SrcEndY = myClips.ClipRect.Height + myClips.ClipRect.Y;
                if (curDesY >= dstBottomY)
                {
                    bTerminal = true;
                    return 0;
                }
                if (bottomDstY >= dstBottomY)
                {
                    bottomDstY = dstBottomY;
                    curStepImageHeight = bottomDstY - curDesY;
                    bTerminal = true;
                }

                curDesY = (curDesY) % (myClips.ClipRect.Height + myClips.YDis);
                bottomDstY = (bottomDstY - 1) % (myClips.ClipRect.Height + myClips.YDis);
                if (curDesY < bottomDstY)
                {
                    if (curDesY < myClips.ClipRect.Height)
                    {
                        bool bReload = false;
                        int height = myClips.ClipRect.Height - curDesY;

                        if (height > curStepImageHeight)
                        {
                            height = curStepImageHeight;
                        }
                        else
                        {
                            bReload = true;
                        }

                        if (IsHeight && (LineCount + height > CoreInterface.JobLineCount))
                        {
                            height = (int)(CoreInterface.JobLineCount - LineCount);
                            curHeight = height;
                            bEnd = true;
                        }

                        srcBlockSize = height * colorNum * nsrcBytePerLine;
                        if (bReversePrint)
                        {
                            seekpos = JobFileReader.HEADERSIZE + (long)(SrcEndY - srcCurhindex - height) * (long)srcBytePlineAllColor;
                            reader.Seek(seekpos, SeekOrigin.Begin);
                        }
                        reader.Read(srcBuf, 0, srcBlockSize);
                        LineCount += height;
                        srcCurhindex += height;
                        if (myClips.IsNktTreeTileMode)
                        {
                            CoreInterface.TileImage(srcBuf, myClips.ClipRect.X * this.ColorDeep, nsrcBytePerLine * 8,
     buffer, bufIndex * 8, ndstBytePerLine * 8,
     height * colorNum, myClips.ClipRect.Width * this.ColorDeep, myClips.XCnt - 1, (myClips.XDis + myClips.ClipRect.Width) * this.ColorDeep,
     colorNum, bReversePrint);
                            int width = myClips.ClipRect.Width;// / ResolutionX;
                            int tile2dstPos = ((myClips.Margin_L + width * 2 + myClips.XDis + myClips.XDis2) * this.ColorDeep + 31) / 32 * 4;
                            CoreInterface.TileImage(srcBuf, myClips.ClipRect.X * this.ColorDeep, nsrcBytePerLine * 8,
    buffer, tile2dstPos * 8, ndstBytePerLine * 8,
    height * colorNum, myClips.ClipRect.Width * this.ColorDeep, 1, (myClips.XDis2 + myClips.ClipRect.Width) * this.ColorDeep,
    colorNum, bReversePrint);
                        }
                        else
                        {
                            CoreInterface.TileImage(srcBuf, myClips.ClipRect.X * this.ColorDeep, nsrcBytePerLine * 8,
                            buffer, bufIndex * 8, ndstBytePerLine * 8,
                            height * colorNum, myClips.ClipRect.Width * this.ColorDeep, myClips.XCnt, (myClips.XDis + myClips.ClipRect.Width) * this.ColorDeep,
                            colorNum, bReversePrint);
                        }
                        if (bReload)
                        {
                            srcCurhindex = 0;
                            if (!bReversePrint)
                                reader.Seek(srcStartBytePos, SeekOrigin.Begin);
                        }
                    }
                }
                else
                {
                    if (curDesY >= myClips.ClipRect.Height)
                    {
                        int height = bottomDstY + 1;

                        if (IsHeight && (LineCount + height > CoreInterface.JobLineCount))
                        {
                            height = (int)(CoreInterface.JobLineCount - LineCount);
                            curHeight = height;
                            bEnd = true;
                        }

                        int dstdeta = (curStepImageHeight - height) * ndstBytePerLine * colorNum * 8;
                        srcBlockSize = height * colorNum * nsrcBytePerLine;
                        srcCurhindex = 0;
                        if (!bReversePrint)
                            reader.Seek(srcStartBytePos, SeekOrigin.Begin);
                        else
                        {
                            seekpos = JobFileReader.HEADERSIZE + (long)(SrcEndY - srcCurhindex - height) * (long)srcBytePlineAllColor;
                            reader.Seek(seekpos, SeekOrigin.Begin);
                        }
                        reader.Read(srcBuf, 0, srcBlockSize);
                        LineCount += height;
                        srcCurhindex += height;
                        if (myClips.IsNktTreeTileMode)
                        {
                            CoreInterface.TileImage(srcBuf, myClips.ClipRect.X * this.ColorDeep, nsrcBytePerLine * 8,
                                buffer, bufIndex * 8 + dstdeta, ndstBytePerLine * 8,
                                height * colorNum, myClips.ClipRect.Width * this.ColorDeep, myClips.XCnt - 1,
                                (myClips.XDis + myClips.ClipRect.Width) * this.ColorDeep,
                                colorNum, bReversePrint);
                            int width = myClips.ClipRect.Width; // / ResolutionX;
                            int tile2dstPos = ((myClips.Margin_L + width * 2 + myClips.XDis + myClips.XDis2) * this.ColorDeep + 31) / 32 * 4;
                            CoreInterface.TileImage(srcBuf, myClips.ClipRect.X * this.ColorDeep, nsrcBytePerLine * 8,
                                buffer, tile2dstPos * 8 + dstdeta, ndstBytePerLine * 8,
                                height * colorNum, myClips.ClipRect.Width * this.ColorDeep, 1,
                                (myClips.XDis2 + myClips.ClipRect.Width) * this.ColorDeep,
                                colorNum, bReversePrint);
                        }
                        else
                        {
                            CoreInterface.TileImage(srcBuf, myClips.ClipRect.X * this.ColorDeep, nsrcBytePerLine * 8,
                                buffer, bufIndex * 8 + dstdeta, ndstBytePerLine * 8,
                                height * colorNum, myClips.ClipRect.Width * this.ColorDeep, myClips.XCnt,
                                (myClips.XDis + myClips.ClipRect.Width) * this.ColorDeep,
                                colorNum, bReversePrint);
                        }
                    }
                    else
                    {
                        // 处理前一块拼贴图的结束部分
                        int height = myClips.ClipRect.Height - curDesY;
                        if (IsHeight && (LineCount + height > CoreInterface.JobLineCount))
                        {
                            height = (int)(CoreInterface.JobLineCount - LineCount);
                            curHeight = height;
                            bEnd = true;
                        }

                        srcBlockSize = height * colorNum * nsrcBytePerLine;
                        if (bReversePrint)
                        {
                            seekpos = JobFileReader.HEADERSIZE + (long)(SrcEndY - srcCurhindex - height) * (long)srcBytePlineAllColor;
                            reader.Seek(seekpos, SeekOrigin.Begin);
                        }
                        reader.Read(srcBuf, 0, srcBlockSize);
                        LineCount += height;
                        srcCurhindex += height;
                        if (myClips.IsNktTreeTileMode)
                        {
                            CoreInterface.TileImage(srcBuf, myClips.ClipRect.X * this.ColorDeep, nsrcBytePerLine * 8,
                                buffer, bufIndex * 8, ndstBytePerLine * 8,
                                height * colorNum, myClips.ClipRect.Width * this.ColorDeep, myClips.XCnt - 1,
                                (myClips.XDis + myClips.ClipRect.Width) * this.ColorDeep,
                                colorNum, bReversePrint);
                            int width = myClips.ClipRect.Width; // / ResolutionX;
                            int tile2dstPos = ((myClips.Margin_L + width * 2 + myClips.XDis + myClips.XDis2) *
                                               this.ColorDeep + 31) / 32 * 4;
                            CoreInterface.TileImage(srcBuf, myClips.ClipRect.X * this.ColorDeep, nsrcBytePerLine * 8,
                                buffer, tile2dstPos * 8, ndstBytePerLine * 8,
                                height * colorNum, myClips.ClipRect.Width * this.ColorDeep, 1,
                                (myClips.XDis2 + myClips.ClipRect.Width) * this.ColorDeep,
                                colorNum, bReversePrint);
                        }
                        else
                        {
                            CoreInterface.TileImage(srcBuf, myClips.ClipRect.X * this.ColorDeep, nsrcBytePerLine * 8,
                                buffer, bufIndex * 8, ndstBytePerLine * 8,
                                height * colorNum, myClips.ClipRect.Width * this.ColorDeep, myClips.XCnt,
                                (myClips.XDis + myClips.ClipRect.Width) * this.ColorDeep,
                                colorNum, bReversePrint);
                        }
#if true
                        if ((IsHeight && !bEnd) || !bTerminal)
                        {
                            // 处理下一块拼贴图的开始部分
                            height = bottomDstY + 1;
                            if (IsHeight && (LineCount + height > CoreInterface.JobLineCount))
                            {
                                height = (int)(CoreInterface.JobLineCount - LineCount);
                                curHeight = height;
                                bEnd = true;
                            }

                            int dstdeta = (curStepImageHeight - height) * ndstBytePerLine * colorNum * 8;
                            srcCurhindex = 0;
                            if (!bReversePrint)
                                reader.Seek(srcStartBytePos, SeekOrigin.Begin);
                            else
                            {
                                seekpos = JobFileReader.HEADERSIZE + (long)(SrcEndY - srcCurhindex - height) * (long)srcBytePlineAllColor;
                                reader.Seek(seekpos, SeekOrigin.Begin);
                            }
                            srcBlockSize = height * colorNum * nsrcBytePerLine;
                            reader.Read(srcBuf, 0, srcBlockSize);
                            LineCount += height;
                            srcCurhindex += height;
                            if (myClips.IsNktTreeTileMode)
                            {
                                CoreInterface.TileImage(srcBuf, myClips.ClipRect.X * this.ColorDeep, nsrcBytePerLine * 8,
                                    buffer, bufIndex * 8 + dstdeta, ndstBytePerLine * 8,
                                    height * colorNum, myClips.ClipRect.Width * this.ColorDeep, myClips.XCnt - 1,
                                    (myClips.XDis + myClips.ClipRect.Width) * this.ColorDeep,
                                    colorNum, bReversePrint);
                                int width = myClips.ClipRect.Width; // / ResolutionX;
                                int tile2dstPos = ((myClips.Margin_L + width * 2 + myClips.XDis + myClips.XDis2) *
                                                   this.ColorDeep + 31) / 32 * 4;
                                CoreInterface.TileImage(srcBuf, myClips.ClipRect.X * this.ColorDeep, nsrcBytePerLine * 8,
                                    buffer, tile2dstPos * 8 + dstdeta, ndstBytePerLine * 8,
                                    height * colorNum, myClips.ClipRect.Width * this.ColorDeep, 1,
                                    (myClips.XDis2 + myClips.ClipRect.Width) * this.ColorDeep,
                                    colorNum, bReversePrint);
                            }
                            else
                            {
                                CoreInterface.TileImage(srcBuf, myClips.ClipRect.X * this.ColorDeep, nsrcBytePerLine * 8,
                                    buffer, bufIndex * 8 + dstdeta, ndstBytePerLine * 8,
                                    height * colorNum, myClips.ClipRect.Width * this.ColorDeep, myClips.XCnt,
                                    (myClips.XDis + myClips.ClipRect.Width) * this.ColorDeep,
                                    colorNum, bReversePrint);
                            }
                        }
#endif
                    }
                }

                if (IsHeight && bEnd)
                {
                    ret += curHeight * ndstBytePerLine * colorNum;
                }
                else
                {
                    ret += curStepImageHeight * ndstBytePerLine * colorNum;
                }
            }
            return ret;
        }

		unsafe public void Draw24BitImageTo1BitBuffer(byte[] buffer24, int bufIndex24,  
			byte[] buffer, int bufIndex, int w, int h,int colorNum)
		{
			int x,y;
			x=y=0;
			{
				const byte white_middle = 0xff / 4 * 3;
                int stride_src = (w * 24 + 31) / 32 * 4;
                int stride_dst = (w + 31) / 32 * 4 * colorNum;
                if (JobFileReader.IsCsPrt)
			    {
			        stride_src = (w * 24 + 7) / 8;
                    stride_dst = (w + 7) / 8 * colorNum;
                }

                int srcLineIndex = bufIndex24;
				int dstLineIndex = y * stride_dst + bufIndex + x / 8;
				fixed (byte * src_Ptr = buffer24)
				{
					bool bOneByte = (x + w - 1) / 8 == (x / 8);
					for (int j = 0; j < h; j++)
					{
						int i = 0;
						byte* src = src_Ptr + srcLineIndex;
						if (bOneByte)
						{
							byte mask = (byte)(0x80 >> ((x) & 7));
							for (i = 0; i < w; i++)
							{
							    if ((*src & *(src + 1) & *(src + 2)) < white_middle)
							    {
							        for (int k = 0; k < colorNum; k++)
							        {
                                        buffer[k * stride_dst/colorNum + dstLineIndex] |= mask;							            
							        }
							    }
							    else
							    {
                                    for (int k = 0; k < colorNum; k++)
                                    {
                                        buffer[k * stride_dst / colorNum + dstLineIndex] &= (byte)~mask;
                                    }
							    }
								src += 3;
								mask >>= 1;
							}
						}
						else
						{
							byte mask = (byte)(0x80 >> ((x) & 7));
							int offset = dstLineIndex;
							if ((x % 8) != 0)
							{
								int bit = 8 - (x % 8);
								for (i = 0; i < bit; i++)
								{
                                    if ((*src & *(src + 1) & *(src + 2)) < white_middle)
                                    {
                                        for (int k = 0; k < colorNum; k++)
                                        {
                                            buffer[k * stride_dst / colorNum + offset] |= mask;
                                        }
                                    }
                                    else
                                    {
                                        for (int k = 0; k < colorNum; k++)
                                        {
                                            buffer[k * stride_dst / colorNum + offset] &= (byte)~mask;
                                        }
                                    }
                                    src += 3;
									mask >>= 1;
								}
								offset++;
							}
							int byteWidth;
							if ((x % 8) != 0)
								byteWidth = (w - (8 - (x % 8))) / 8;
							else
								byteWidth = (w) / 8;
							for (i = 0; i < byteWidth; i++)
							{
								byte bytevalue = 0;
								for (mask = 0x80; mask != 0; mask >>= 1)
								{
									if ((*src & *(src + 1) & *(src + 2)) < white_middle)
										bytevalue |= mask;

									src += 3;
								}
                                for (int k = 0; k < colorNum; k++)
                                {
                                    buffer[k * stride_dst / colorNum + offset] = bytevalue;
                                }
                                offset++;
							}
							mask = 0x80;
							if ((x + w) % 8 != 0)
							{
								int bit = (x + w) % 8;
								for (i = 0; i < bit; i++)
								{
                                    if ((*src & *(src + 1) & *(src + 2)) < white_middle)
                                    {
                                        for (int k = 0; k < colorNum; k++)
                                        {
                                            buffer[k * stride_dst / colorNum + offset] |= mask;
                                        }
                                    }
                                    else
                                    {
                                        for (int k = 0; k < colorNum; k++)
                                        {
                                            buffer[k * stride_dst / colorNum + offset] &= (byte)~mask;
                                        }
                                    }
									src += 3;
									mask >>= 1;
								}
								offset++;
							}

						}
						srcLineIndex += stride_src;
						dstLineIndex += stride_dst;
					}
				}

			}

		}

        unsafe public void Draw24BitImageTo2BitBuffer(byte[] buffer24, int bufIndex24,
    byte[] buffer, int bufIndex, int w, int h, int colorNum,int colorDeep)
        {
            int x, y;
            x = y = 0;
            //bufIndex += (w + 31) / 32 * 4 * 3;
            {
                const byte MASK_BYTE_OFFSET = 0x3; //0x7
                const byte MASK_FIRST = 0xC0;//0x40//小点 //0xC0//-大点 //0x80//-中点;
                const byte white_middle = 0xff / 4 * 3;
                int stride_src = (w * 24 + 31) / 32 * 4;
                int stride_dst = (w * colorDeep + 31) / 32 * 4 * colorNum;
                if (JobFileReader.IsCsPrt)
                {
                    stride_src = (w*24 + 7)/8;
                    stride_dst = (w*colorDeep + 7)/8*colorNum;
                }
                int srcLineIndex = bufIndex24;
                int dstLineIndex = y * stride_dst + bufIndex + x * colorDeep / 8;
                fixed (byte* src_Ptr = buffer24)
                {
                    bool bOneByte = (x + w - 1) * colorDeep / 8 == (x * colorDeep / 8);
                    for (int j = 0; j < h; j++)
                    {
                        int i = 0;
                        byte* src = src_Ptr + srcLineIndex;
                        if (bOneByte)
                        {
                            byte mask = (byte)(MASK_FIRST >> ((x << 1) & MASK_BYTE_OFFSET));
                            for (i = 0; i < w; i++)
                            {
                                if ((*src & *(src + 1) & *(src + 2)) < white_middle)
                                {
                                    for (int k = 0; k < colorNum; k++)
                                    {
                                        buffer[k * stride_dst / colorNum + dstLineIndex] |= mask;
                                    }
                                }
                                else
                                {
                                    for (int k = 0; k < colorNum; k++)
                                    {
                                        buffer[k * stride_dst / colorNum + dstLineIndex] &= (byte)~mask;
                                    }
                                }
                                src += 3;
                                mask >>= colorDeep;
                            }
                        }
                        else
                        {
                            byte mask = (byte)(MASK_FIRST >> ((x << 1) & MASK_BYTE_OFFSET));
                            int offset = dstLineIndex;
                            if ((x % 4) != 0)
                            {
                                int bit = 4 - (x % 4);
                                for (i = 0; i < bit; i++)
                                {
                                    if ((*src & *(src + 1) & *(src + 2)) < white_middle)
                                    {
                                        for (int k = 0; k < colorNum; k++)
                                        {
                                            buffer[k * stride_dst / colorNum + offset] |= mask;
                                        }
                                    }
                                    else
                                    {
                                        for (int k = 0; k < colorNum; k++)
                                        {
                                            buffer[k * stride_dst / colorNum + offset] &= (byte)~mask;
                                        }
                                    }
                                    src += 3;
                                    mask >>= colorDeep;
                                }
                                offset++;
                            }
                            int byteWidth;
                            if ((x % 4) != 0)
                                byteWidth = (w - (4 - (x % 4))) / 4;
                            else
                                byteWidth = (w) / 4;
                            for (i = 0; i < byteWidth; i++)
                            {
                                byte bytevalue = 0;
                                for (mask = MASK_FIRST; mask != 0; mask >>= colorDeep)
                                {
                                    if ((*src & *(src + 1) & *(src + 2)) < white_middle)
                                        bytevalue |= mask;

                                    src += 3;
                                }
                                for (int k = 0; k < colorNum; k++)
                                {
                                    buffer[k * stride_dst / colorNum + offset] = bytevalue;
                                }
                                offset++;
                            }
                            mask = MASK_FIRST;
                            if ((x + w) % 8 != 0)
                            {
                                int bit = (x + w) % 8;
                                for (i = 0; i < bit; i++)
                                {
                                    if ((*src & *(src + 1) & *(src + 2)) < white_middle)
                                    {
                                        for (int k = 0; k < colorNum; k++)
                                        {
                                            buffer[k * stride_dst / colorNum + offset] |= mask;
                                        }
                                    }
                                    else
                                    {
                                        for (int k = 0; k < colorNum; k++)
                                        {
                                            buffer[k * stride_dst / colorNum + offset] &= (byte)~mask;
                                        }
                                    }
                                    src += 3;
                                    mask >>= MASK_FIRST;
                                }
                                offset++;
                            }

                        }
                        srcLineIndex += stride_src;
                        dstLineIndex += stride_dst;
                    }
                }

            }

        }

	    public bool HasNote
	    {
	        get
	        {
                if (Clips==null
                    ||(Clips.AddtionInfoMask == 0 && string.IsNullOrEmpty(Clips.Note))
                    || string.IsNullOrEmpty(Clips.NoteImageFileName))
                {
                    return false;
                }
	            return true;
	        }	
	    }
		private FileStream LabelReader;
		private int SrcLabelDeep = 24;
		int srcWidth = 0;
		int srcHeight = 0;
        int srcHindex = 0;
		public int ReadLabel(int step,
            byte[] buffer, int MaxBufSize, bool bFirst, out bool bTerminal, bool bReversePrint)
		{
			bTerminal = false;
			int bufIndex = 0;
			int ret = 0;
            if (!HasNote)
			{
				bTerminal = true;
				return 0;
			}
			if(bFirst)
			{
				LabelReader = new FileStream(myClips.NoteImageFileName,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
				int readBytes	= LabelReader.Read(buffer,0,0x36);
				int offset = 0x12;
				srcWidth = ReadInt(buffer,offset);
				offset = 0x16;
				srcHeight = ReadInt(buffer,offset);;
				nsrcBytePerLine = (srcWidth*SrcLabelDeep +31)/32 *4;

                int dstBottomY = myClips.ClipRect.Height * myClips.YCnt + myClips.YDis * (myClips.YCnt - 1) + myClips.YAddtion;
				int BlankY = dstBottomY + myClips.NoteMargin;


                int TotalHeight = myClips.H - (myClips.ClipRect.Height * myClips.YCnt + myClips.YDis * (myClips.YCnt - 1) + myClips.YAddtion);
				OneStepImageHeight = ((MaxBufSize)/(ndstBytePerLine*colorNum));
				if(OneStepImageHeight > srcHeight)
					OneStepImageHeight = srcHeight;
				else if(OneStepImageHeight <1)
					OneStepImageHeight = 1;
                srcHindex = 0;
			}
			//Clear Buffer;
			{
				int size = OneStepImageHeight*colorNum*ndstBytePerLine;
				for (int i=0; i<size;i++)
					buffer[i] = 0;
			}
			
			if(OneStepImageHeight != 0)
			{
				int curStepImageHeight = OneStepImageHeight;
				int srcBlockSize = OneStepImageHeight *nsrcBytePerLine;
                
				int curDesY    = (step * OneStepImageHeight);
				int bottomDstY = ((step+1) * OneStepImageHeight);
                int TotalHeight = myClips.H - (myClips.ClipRect.Height * myClips.YCnt + myClips.YDis * (myClips.YCnt - 1) + myClips.YAddtion);
				
				if(curDesY >= TotalHeight)
				{
					bTerminal = true;
					return 0;
				}
				if(bottomDstY>= TotalHeight)
				{
					bottomDstY = TotalHeight;
					curStepImageHeight = bottomDstY - curDesY;
					bTerminal = true;
				}

				{
                    if ((!bReversePrint&&bottomDstY <= myClips.NoteMargin)
                        || (bReversePrint && bottomDstY > TotalHeight - myClips.NoteMargin))
					{
					
					}
					else 
					{
						int height = curStepImageHeight;
                        if (!bReversePrint && curDesY < myClips.NoteMargin)
						{
							height = bottomDstY - myClips.NoteMargin;
							bufIndex +=  (myClips.NoteMargin -  curDesY)* colorNum* ndstBytePerLine;
						}
                        if (bReversePrint)
                        {
                            long seekpos = (srcHeight - srcHindex - height) * (long)nsrcBytePerLine;
                            LabelReader.Seek(seekpos, SeekOrigin.Begin);
                        } 
						srcBlockSize = height * nsrcBytePerLine;
                        byte[] srcBuf = new byte[srcBlockSize];
                        LabelReader.Read(srcBuf, 0, srcBlockSize);
                        srcHindex += height;
						
						int  nOneBitBytePerLine = (srcWidth*ColorDeep+31)/32 *4 ;
						int bufsize = nOneBitBytePerLine* height * colorNum;
						byte [] OneBitBuffer = new byte[nOneBitBytePerLine* height * colorNum];
						for (int k=0;k<bufsize;k++)
							OneBitBuffer[k]= 0;
					    if (ColorDeep == 1)
					    {
					        Draw24BitImageTo1BitBuffer(srcBuf,0,OneBitBuffer,0,srcWidth,height,colorNum);
					    }
                        else
                        {
                            Draw24BitImageTo2BitBuffer(srcBuf, 0, OneBitBuffer, 0, srcWidth, height, colorNum, ColorDeep);
                        }
                        CoreInterface.TileImage(OneBitBuffer, 0, nOneBitBytePerLine * 8,
							buffer,bufIndex*8, ndstBytePerLine*8,
							height*colorNum,srcWidth*this.ColorDeep,1,0, colorNum, bReversePrint);
					}
				}
				if(bTerminal)
				{
					LabelReader.Close();
					LabelReader = null;
				}
				ret += curStepImageHeight* ndstBytePerLine*colorNum;
			}
			return ret;
		}

        public int SendRevPrt(int line, JobFileReader fileStream, bool bFirst, ref byte[] buffer, int MaxBufSize, out bool bTerminal)
		{
			bTerminal = false;
			int buf_offset = 0;
			if(bFirst)
			{
				fileStream.Seek(0,SeekOrigin.Begin);
				srcWidth=srcHeight=0;
                int readBytes = fileStream.ReadHeader(buffer);
                srcWidth = fileStream.SrcWidth;
                srcHeight = fileStream.SrcHeight;
                colorNum = fileStream.ColorNum;
                nsrcBytePerLine = fileStream.BytePerLine;

				nsrcBytePerLine *= colorNum;
				if(nsrcBytePerLine > MaxBufSize)
				{
					buffer = new byte [nsrcBytePerLine];
					fileStream.Seek(0,SeekOrigin.Begin);
					readBytes	= fileStream.Read(buffer,buf_offset,JobFileReader.HEADERSIZE);
                }
				buf_offset+= readBytes;
			}
			if(line < srcHeight && line>= 0 )
			{
				long  curlineoffset= (long)JobFileReader.HEADERSIZE +  (long)(srcHeight-1-line) * (long)nsrcBytePerLine; 
				fileStream.Seek(curlineoffset ,SeekOrigin.Begin);
				int readBytes = fileStream.Read(buffer,buf_offset,nsrcBytePerLine );
				buf_offset += readBytes;
				return  buf_offset;
			}
			else
			{
				bTerminal = true;
				return  buf_offset;
			}
		}

	}

	public enum ClipCaseEnum
	{
		Default,
		OnlyClip,
		OnlyTile,
		OnlyClipAndNote,
		OnlyTileAndNote,
		OnlyClipAndTile,
		ClipAndTileAndNote
	}
}
