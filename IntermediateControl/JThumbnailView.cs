using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using BYHXPrinterManager.JobListView;
//using ShellDll;

namespace BYHXPrinterManager.Browser
{
    public class JThumbnailView : ListView
    {
        private Thread myWorker = null;

        //        public event EventHandler OnLoadComplete;
        //        public ShellItem selectedItem;

        private PrivewFileManager m_PrivewFileManager = null;
        public string m_LastPreviewForlder = string.Empty;

        //Create a Bitmpap Object. 
        Bitmap animatedImage = IntermediateControl.Properties.Resources.status_anim;
        private Bitmap bmpAnimate = null;
        bool currentlyAnimating = false;
        private System.Windows.Forms.Timer AnimateTime = new System.Windows.Forms.Timer();
        private int AnimateItemIndex = -1;

        public delegate void CallbackWorkingJobFinished(int jobPercent);
        public JThumbnailView()
        {
            InitializeComponent();

			if(PubFunc.IsInDesignMode())
				return;
            ImageList il = new ImageList();
            il.ImageSize = new Size(thumbNailSize, thumbNailSize);
            il.ColorDepth = ColorDepth.Depth32Bit;
            il.TransparentColor = Color.White;
            LargeImageList = il;
            HandleDestroyed += new EventHandler(JThumbnailView_HandleDestroyed);

            this.ListViewItemSorter = new ListViewColumnSorter();
            this.ColumnClick += new ColumnClickEventHandler(ListViewHelper.ListView_ColumnClick);
//
            m_PrivewFileManager = new PrivewFileManager();
            this.folderName = m_LastPreviewForlder = m_PrivewFileManager.GetTheLastForderName();

            this.InitListHeader();
            this.AnimateTime.Tick += new EventHandler(AnimateTime_Tick);
            this.AnimateTime.Interval = 200;
        }


		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.SuspendLayout();
			// 
			// JThumbnailView
			// 
			this.AllowDrop = true;
			this.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.JThumbnailView_ItemDrag);
            this.ResumeLayout(false);

		}

		#endregion

        //private ListViewColumnSorter mListViewColumnSorter;
        //public ListViewColumnSorter ListViewColumnSorter
        //{
        //    get { return mListViewColumnSorter; }
        //    set
        //    {
        //        mListViewColumnSorter = value;
        //        this.ListViewItemSorter = value;
        //    }
        //}


        private int thumbNailSize = 95;
        public int ThumbNailSize
        {
            get { return thumbNailSize; }
            set { thumbNailSize = value; }
        }

        private Color thumbBorderColor = Color.Wheat;
        public Color ThumbBorderColor
        {
            get { return thumbBorderColor; }
            set { thumbBorderColor = value; }
        }

        public bool IsLoading
        {
            get 
            {
                return myWorker!=null ? myWorker.IsAlive:false;//myWorker.IsBusy;
            }
        }

        public void InitListHeader()
        {
            this.Items.Clear();
            this.Columns.Clear();

            ColumnHeader[] columnHeader = new ColumnHeader[6];
            int j = 0;
            foreach (int i in Enum.GetValues(typeof(JobListColumnHeader)))
            {
                JobListColumnHeader cur = (JobListColumnHeader)i;
                if (cur != JobListColumnHeader.Copies
                    && cur != JobListColumnHeader.PrintedDate
                    && cur != JobListColumnHeader.PrintedPasses
                    && cur != JobListColumnHeader.Status
                    && cur != JobListColumnHeader.PrintTime)
                {
                    columnHeader[j] = new ColumnHeader();
                    string cmode = ResString.GetEnumDisplayName(typeof(JobListColumnHeader), cur);
                    columnHeader[j].Text = cmode;
                    columnHeader[j].Width = 150;
                    columnHeader[j].TextAlign = HorizontalAlignment.Center;
                    j++;
                }
            }
            //this.SuspendLayout();
            this.Columns.AddRange(columnHeader);
            //this.ResumeLayout();
        }

        private ListViewItem GetJTListViewItem(string[] subItems, UIJob job)
        {
            ListViewItem ret = new ListViewItem(job.Name, 0);
            for (int i = 0; i < subItems.Length; i++)
            {
                subItems[i] = "";
            }
            ret.SubItems.AddRange(subItems);
            string passDispName = ResString.GetDisplayPass();
            int j = 0;
            foreach (int i in Enum.GetValues(typeof(JobListColumnHeader)))
            {
                JobListColumnHeader cur = (JobListColumnHeader)i;
                switch (cur)
                {
                    case JobListColumnHeader.Name:
                        ret.SubItems[j].Text = job.Name;
                        j++;
                        break;
                    //case JobListColumnHeader.Status:
                    //    this.SubItems[i].Text = ResString.GetEnumDisplayName(typeof(JobStatus), job.Status);
                    //    break;
                    case JobListColumnHeader.Size:
                        ret.SubItems[j].Text = GetJobSize((float)job.ResolutionX, (float)job.ResolutionY, job.Dimension);
                        j++;
                        break;
                    case JobListColumnHeader.Resolution:
                        ret.SubItems[j].Text = job.ResolutionX.ToString() + "x" + job.ResolutionY.ToString();
                        j++;
                        break;
                    case JobListColumnHeader.Passes:
                        ret.SubItems[j].Text = job.PassNumber.ToString() + " " + passDispName;
                        j++;
                        break;
                    case JobListColumnHeader.BiDirection:
                        ret.SubItems[j].Text = ResString.GetEnumDisplayName(typeof(PrintDirection), (PrintDirection)job.PrintingDirection);
                        j++;
                        break;
                    //case JobListColumnHeader.Copies:
                    //    this.SubItems[i].Text = job.Copies.ToString();
                    //    break;

                    //case JobListColumnHeader.PrintedPasses:
                    //    this.SubItems[i].Text = job.PassNumber.ToString() + " " + passDispName;
                    //    break;
                    //case JobListColumnHeader.PrintedDate:
                    //    if (bPrinted)
                    //    {
                    //        string timeInfo = job.PrintedDate.ToString("u", DateTimeFormatInfo.InvariantInfo);
                    //        int len = timeInfo.Length;
                    //        if (len > 0 && !char.IsDigit(timeInfo, len - 1))
                    //            timeInfo = timeInfo.Substring(0, len - 1);
                    //        this.SubItems[i].Text = timeInfo;
                    //    }
                    //    break;
                    case JobListColumnHeader.Location:
                        ret.SubItems[j].Text = job.FileLocation;
                        j++;
                        break;
                }
            }
            ret.Tag = job.FileLocation;
            return ret;
        }

        public string GetJobSize(float xRes, float yRes, Size jobSize)
        {
            float width = 0;
            float height = 0;
            if (xRes != 0)
                width = (float)jobSize.Width / xRes;
            if (yRes != 0)
                height = (float)jobSize.Height / yRes;

            return width.ToString("f2") + "x" + height.ToString("f2") + " " + ResString.GetUnitSuffixDispName(UILengthUnit.Inch);
        }

        private delegate void SetThumbnailDelegate(Image image,string key);
        private void SetThumbnail(Image image,string key)
        {
            if (Disposing)
                return;

            if (this.InvokeRequired && (myWorker.ThreadState == ThreadState.Running || myWorker.ThreadState == ThreadState.Background))
            {
                SetThumbnailDelegate d = new SetThumbnailDelegate(SetThumbnail);
                this.Invoke(d, new object[] { image, key });
            }
            else
            {
				try
				{
					int index = 0;

					LargeImageList.Images.Add(image); //Images[i].repl  
					index = LargeImageList.Images.Count - 1;

					Items[index - 1].ImageIndex = index;
				}
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.Assert(false);
					return;
				}
            }
        }

        private bool canReLoad = false;
        public bool CanReLoad
        {
            get { return canReLoad; }
            set { canReLoad = value; }
        }


        private string folderName = string.Empty;//Path.GetDirectoryName(Application.ExecutablePath);
        public string FolderName
        {
            get { return folderName; }
            set
            {
                if (!Directory.Exists(value))
                    return;
                folderName = value;
                if (CanReLoad)
                {
                    ReLoadItems();
                }
            }
        }

        public Image GetThumbNail(string fileName)
        {
            Image retImage = GetThumbNail(fileName, thumbNailSize, thumbNailSize, thumbBorderColor);

            return retImage;
        }

        public Image GetThumbNail(string fileName, int imgWidth, int imgHeight, Color penColor)
        {
            bool hasError = false;
            string previewFileName = m_LastPreviewForlder + Path.GetFileNameWithoutExtension(fileName) + ".bmp";
            Bitmap bmp = null;
            try
            {
                if (File.Exists(previewFileName))
                    bmp = new Bitmap(previewFileName);
                else
                {
                    if (File.Exists(fileName))
                    {
                        SPrtFileInfo jobInfo = new SPrtFileInfo();
                        Int32 bret = 0;
                        bret = CoreInterface.Printer_GetFileInfo(fileName, ref jobInfo, 1);
                        if (bret == 1)
                        {
                            bmp = SerialFunction.CreateImageWithImageInfo(jobInfo.sImageInfo);
                            if (!Directory.Exists(m_LastPreviewForlder))
                                Directory.CreateDirectory(m_LastPreviewForlder);
                            bmp.Save(previewFileName);
                        }
                        else
                        {
                            hasError = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                hasError = true;
                System.Diagnostics.Debug.Assert(true, ex.Message + ex.StackTrace);
            }
            if (bmp == null || hasError)
            {
                bmp = this.CreatDefaultBmp("¼ÓÔØÊ§°Ü!!", Color.Wheat, Color.Red);
            }

            Bitmap retBmp = this.CreatDefaultBmp(bmp,penColor,Color.Black);

            return retBmp;
        }

        private void AddDefaultThumb(ImageList m_LargeImageList)
        {
            Bitmap bmp = this.CreatDefaultBmp("Loading...", Color.Wheat,Color.Green);
            m_LargeImageList.Images.Add(bmp);
        }
		private Bitmap CreatDefaultBmp(object Oo, Color borderColor, Color textColor)
		{
			string text = string.Empty;
			SizeF texts = new Size();
			Bitmap bmp = null;
			if (Oo is Bitmap)
			{
				bmp = (Bitmap)Oo;
			}
			else
			{
				text = (string)Oo;
				Graphics g = this.CreateGraphics();
				texts = g.MeasureString(text, JThumbnailView.DefaultFont, LargeImageList.ImageSize,StringFormat.GenericDefault);
				g.Dispose();
				bmp = new Bitmap((int)texts.Width, (int)texts.Height);
			}

			int imgWidth = LargeImageList.ImageSize.Width;
			int imgHeight = LargeImageList.ImageSize.Height;

			Bitmap retBmp = new Bitmap(imgWidth, imgHeight, PixelFormat.Format64bppPArgb);
			Graphics grp = Graphics.FromImage(retBmp);

			int tnWidth = imgWidth, tnHeight = imgHeight;

			if (bmp.Width > bmp.Height)
				tnHeight = (int)(((float)bmp.Height / (float)bmp.Width) * tnWidth);
			else if (bmp.Width < bmp.Height)
				tnWidth = (int)(((float)bmp.Width / (float)bmp.Height) * tnHeight);

			int iLeft = (imgWidth / 2) - (tnWidth / 2);
			int iTop = (imgHeight / 2) - (tnHeight / 2);

			grp.PixelOffsetMode = PixelOffsetMode.None;
			grp.InterpolationMode = InterpolationMode.HighQualityBicubic;

			if (Oo is Bitmap)
				grp.DrawImage(bmp, iLeft, iTop, tnWidth, tnHeight);
			else
			{
				Brush brush = new SolidBrush(textColor);
				Rectangle textRec = new Rectangle((imgWidth / 2) - (bmp.Width / 2), (imgHeight / 2) - (bmp.Height / 2), bmp.Width, bmp.Height);
				grp.DrawString(text, JThumbnailView.DefaultFont, brush, textRec);
			}

			Pen pn = new Pen(borderColor, 1); //Color.Wheat
			grp.DrawRectangle(pn, 0, 0, retBmp.Width - 1, retBmp.Height - 1);
			grp.Dispose();
			return retBmp;
		}

        private delegate void DoWorkDelegate(object sender);
        //private void bwLoadImages_DoWork(object sender, DoWorkEventArgs e)
        public void bwLoadImages_DoWork()
        {
            //if (this.InvokeRequired)
            //{
            //    DoWorkDelegate d = new DoWorkDelegate(bwLoadImages_DoWork);
            //    this.Invoke(d, new object[] { sender });
            //}
            //else
            {
                //LargeImageList.Images.Clear();
                //AddDefaultThumb(LargeImageList);
                //                string[] fileList = (string[])sender;// e.Argument;
                foreach (string fileName in ItemList)
                {
                    this.StartAnimateImage();
                    this.AnimateItemIndex = this.LargeImageList.Images.Count - 1;
                    Image img = GetThumbNail(fileName);
                    this.StopAnimateImage();
                    SetThumbnail(img, fileName);
                }
                
            }
        }

		private ArrayList ItemList = new ArrayList();
        public void ReLoadItems()
        {
			if(folderName == null || folderName == "" || !Directory.Exists(folderName))
				return;

            this.StopAnimateImage();

            if (myWorker != null && myWorker.IsAlive)
                myWorker.Abort();

			string strFilter = "*.prt;*.prn";
            //string strFilter = "*.jpg;*.png;*.gif;*.bmp";

            ArrayList fileList = new ArrayList();
            string[] arExtensions = strFilter.Split(';');

            foreach (string filter in arExtensions)
            {
                string[] strFiles = Directory.GetFiles(folderName, filter);
                fileList.AddRange(strFiles);
            }

            fileList.Sort();

            while (myWorker != null && myWorker.ThreadState == ThreadState.AbortRequested)
            {
            }

            LargeImageList.Images.Clear();
            //            SmallImageList.Images.Clear();
            AddDefaultThumb(this.LargeImageList);
            //            AddDefaultThumb(this.SmallImageList);

            this.Items.Clear();
            if (fileList.Count > 0)
            {
                BeginUpdate();
                m_LastPreviewForlder = this.m_PrivewFileManager.Add(folderName);
                ListViewItem[] newListItemsArray = new ListViewItem[fileList.Count];
				string[] subItems = new string[this.Columns.Count];
                for (int i = 0; i < newListItemsArray.Length; i++)
                {
                    newListItemsArray[i] = GetJTListViewItem(subItems, CreatJob(fileList[i].ToString()));
                    //newListItemsArray[i] = new ListViewItem(Path.GetFileName(fileList[i].ToString()),0);
                    //newListItemsArray[i].Tag = fileList[i].ToString();
                }
                this.Items.AddRange(newListItemsArray);
                EndUpdate();
                ItemList = fileList;

                //this.AnimateTime.Start();
                ThreadStart threadStart = new ThreadStart(bwLoadImages_DoWork);
                myWorker = new Thread(threadStart);
                myWorker.CurrentCulture = Thread.CurrentThread.CurrentCulture;
                myWorker.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
                myWorker.IsBackground = true;
                myWorker.Start();
            }
        }

        private UIJob CreatJob(string fileName)
        {
            UIJob job = new UIJob();
            job.Name = Path.GetFileName(fileName);
            job.PreViewFile = m_LastPreviewForlder + Path.GetFileNameWithoutExtension(fileName) + ".bmp";
            job.Status = JobStatus.Idle;

            SPrtFileInfo jobInfo = new SPrtFileInfo();
            Int32 bret = 0;
            bret = CoreInterface.Printer_GetFileInfo(fileName, ref jobInfo, 0);
            //if (bret == 1)
            {
                job.PrtFileInfo = jobInfo;
                job.FileLocation = fileName;
            }
            return job;
        }

        private void JThumbnailView_HandleDestroyed(object sender, EventArgs e)
        {
            if (this.myWorker != null)
            {
                if (this.myWorker.IsAlive)
                    this.myWorker.Abort();
                //while (!this.myWorker.IsAlive)
                //{
                //    this.myWorker = null;
                //}
            }
            this.StopAnimateImage();
            this.m_PrivewFileManager.SaveListToXML();
        }

        private void AnimateTime_Tick(object sender, EventArgs e)
        {
            //Begin the animation.
            AnimateImage();

            //Get the next frame ready for rendering.
            ImageAnimator.UpdateFrames();
        }

        //This method begins the animation.
        public void AnimateImage()
        {
            if (!currentlyAnimating)
            {
                //Begin the animation only once.
                ImageAnimator.Animate(animatedImage, new EventHandler(this.OnFrameChanged));
                currentlyAnimating = true;
            }
        }

        private delegate void StartAnimate();
        private void StartAnimateImage()
        {
            if (this.InvokeRequired)
            {
                StartAnimate d = new StartAnimate(StartAnimateImage);
                this.Invoke(d);
            }
            else
            {
                this.AnimateTime.Start();
            }
        }

        private void StopAnimateImage()
        {
            this.AnimateTime.Stop();
            ImageAnimator.StopAnimate(this.animatedImage, this.OnFrameChanged);
            this.AnimateItemIndex = -1;
            this.currentlyAnimating = false;
            //this.Invalidate();
        }

        private void OnFrameChanged(object o, EventArgs e)
        {
            try
            {
                if (this.View != View.LargeIcon)
                    return;
                if (this.InvokeRequired)
                {
                    EventHandler d = new EventHandler(OnFrameChanged);
                    this.BeginInvoke(d);
                }
                else
                {
                    //Force a call to the Paint event handler. 
                    if (this.AnimateItemIndex >= 0)
                        this.Invalidate(Items[this.AnimateItemIndex].Bounds);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false);
                return;
            }
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            if (e.ItemIndex == this.AnimateItemIndex && this.View == View.LargeIcon)
            {
                AutoSizeMode aa = this.GetAutoSizeMode();
                Size sz = e.Item.ImageList.ImageSize;
                Point loc = new Point(e.Bounds.Location.X + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Y);
                RectangleF rcf = new RectangleF(loc, sz);

                if (bmpAnimate == null)
                    bmpAnimate = this.CreatDefaultBmp("Loading...", Color.Wheat, Color.Green);
                Graphics g = Graphics.FromImage(bmpAnimate);
                g.DrawImage(this.animatedImage, new RectangleF(0, bmpAnimate.Height - animatedImage.Height, bmpAnimate.Width, animatedImage.Height));
                g.Dispose();
                //Image img = this.CreatDefaultBmp(this.animatedImage,thumbBorderColor,SystemColors.ControlText);
                e.Graphics.DrawImage(bmpAnimate, rcf);

                RectangleF rcText = this.GetItemRect(e.Item.Index, ItemBoundsPortion.Label);
                StringFormat sf = (StringFormat)StringFormat.GenericTypographic.Clone();
                sf.Alignment = StringAlignment.Center;
                sf.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.FitBlackBox;
                e.Graphics.DrawString(e.Item.Text, e.Item.Font, SystemBrushes.ControlText, rcText, sf);
            }
            else
            {
                e.DrawDefault = true;
                base.OnDrawItem(e);
            }
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
            base.OnDrawColumnHeader(e);
        }

        private void JThumbnailView_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            ArrayList files = new ArrayList();
            foreach (ListViewItem lvi in this.SelectedItems)
                files.Add(lvi.Tag.ToString());
            //			DataObjectEx data = new DataObjectEx();
            //			data.SetData(DataFormats.FileDrop, (string[])files.ToArray(typeof(string)));
            DragDropEffects res = this.DoDragDrop((string[])files.ToArray(typeof(string)), DragDropEffects.Copy | DragDropEffects.Move);
        }

        public void SelectAll()
        {
            foreach (ListViewItem lvi in this.Items)
            {
                lvi.Selected = true;
            }
        }
        public void Inverse()
        {
            ArrayList selIndexs = new ArrayList();
            for(int i =0; i < this.SelectedIndices.Count; i++)
            {
                selIndexs.Add(this.SelectedIndices[i]);
            }
            
            foreach (ListViewItem lvi in this.Items)
            {
                if (!selIndexs.Contains(lvi.Index))
                    lvi.Selected = true;
                else
                    lvi.Selected = false;
            }
        }
    }
}
