using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BYHXPrinterManager;
using ShellDll;

namespace FileBrowser
{
    internal partial class JThumbnailView : BrowserListView
    {
        private Thread myWorker = null;

        public event EventHandler OnLoadComplete;
        public ShellItem selectedItem;

        private PrivewFileManager m_PrivewFileManager=null;
        public string m_LastPreviewForlder = string.Empty;

        //Create a Bitmpap Object. 
        Bitmap animatedImage = null;//new Bitmap("F:\\WorkSpace\\New_BYHXPrinterManager\\FileBrowser\\Resources\\status_anim.gif");
        bool currentlyAnimating = false;
        private System.Windows.Forms.Timer AnimateTime = new System.Windows.Forms.Timer();
        private ListViewItem AnimateItem = null;

        public delegate void CallbackWorkingJobFinished(int jobPercent);
        public JThumbnailView()
        {
            InitializeComponent();
            ImageList il = new ImageList();
            il.ImageSize = new Size(thumbNailSize, thumbNailSize);
            il.ColorDepth = ColorDepth.Depth32Bit;
            il.TransparentColor = Color.White;
            LargeImageList = il;
            HandleDestroyed +=new EventHandler(JThumbnailView_HandleDestroyed);

            this.ListViewColumnSorter = new ListViewColumnSorter();
            this.ColumnClick += new ColumnClickEventHandler(ListViewHelper.ListView_ColumnClick);

            m_PrivewFileManager = new PrivewFileManager();
            this.folderName = m_LastPreviewForlder = m_PrivewFileManager.GetTheLastForderName();

            this.AnimateTime.Tick +=new EventHandler(AnimateTime_Tick);
            this.AnimateTime.Interval = 10;
        }

        private ListViewColumnSorter mListViewColumnSorter;

        public ListViewColumnSorter ListViewColumnSorter
        {
            get { return mListViewColumnSorter; }
            set { mListViewColumnSorter = value; }
        }


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


        private delegate void SetThumbnailDelegate(Image image,string key);
        private void SetThumbnail(Image image,string key)
        {
            if (Disposing)
                return;

            if (this.InvokeRequired)
            {
                SetThumbnailDelegate d = new SetThumbnailDelegate(SetThumbnail);
                this.Invoke(d, new object[] { image, key });
            }
            else
            {
                try
                {
                    int index = 0;
                    if (!LargeImageList.Images.ContainsKey(key))
                    {
                        LargeImageList.Images.Add(key, image); //Images[i].repl  
                        index = LargeImageList.Images.Count - 1;
                    }
                    else
                        index = LargeImageList.Images.IndexOfKey(key);

                    Items[index - 1].ImageIndex = index;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Assert(false);
                    return;
                }
            }
        }

        private bool canLoad = false;
        public bool CanLoad
        {
            get { return canLoad; }
            set { canLoad = value; }
        }


        private string folderName = string.Empty;//Path.GetDirectoryName(Application.ExecutablePath);
        public string FolderName
        {
            get { return folderName; }
            set
            {
                //if (!Directory.Exists(value))
                //    return;
                folderName = value;
                if (CanLoad)
                {
                    if (myWorker != null && myWorker.IsAlive)
                        myWorker.Abort();
                    ReLoadItems();
                }
            }
        }

        void myWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (OnLoadComplete != null)
                OnLoadComplete(this, new EventArgs());
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
            else if (!File.Exists(previewFileName))
            {
                bmp.Save(previewFileName);
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
            Size texts = new Size();
            Bitmap bmp = null;
            if (Oo is Bitmap)
            {
                bmp = (Bitmap)Oo;
            }
            else
            {
                text = (string)Oo;
                texts = TextRenderer.MeasureText(text, JThumbnailView.DefaultFont, LargeImageList.ImageSize,TextFormatFlags.NoPadding);
                bmp = new Bitmap(texts.Width, texts.Height);
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
        public void bwLoadImages_DoWork(object sender)
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
                string[] fileList = (string[])sender;// e.Argument;
                foreach (string fileName in fileList)
                {
                    SetThumbnail(GetThumbNail(fileName), fileName);
                }
            }
        }

        public void LoadItems()
        {
            while (myWorker != null && myWorker.ThreadState == ThreadState.AbortRequested)
            {
            }
            //BeginUpdate();
            //Items.Clear();
            LargeImageList.Images.Clear();
            SmallImageList.Images.Clear();
            AddDefaultThumb(this.LargeImageList);
            AddDefaultThumb(this.SmallImageList);

            string[] fileList = new string[this.Items.Count];
            for (int i = 0; i < this.Items.Count; i++)
            //foreach (string fileName in fileList)
            {
                this.Items[i].ImageIndex = 0;
                Bitmap image = ShellImageList.GetIcon(((ShellItem)this.Items[i].Tag).ImageIndex, true).ToBitmap();
                this.SmallImageList.Images.Add(image);

                fileList[i] = ShellItem.GetRealPath((ShellItem)this.Items[i].Tag);
            }

            //EndUpdate();

            ParameterizedThreadStart threadStart = new ParameterizedThreadStart(this.bwLoadImages_DoWork);
            myWorker = new Thread(threadStart);
            myWorker.CurrentCulture = Thread.CurrentThread.CurrentCulture;
            myWorker.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
            myWorker.IsBackground = true;
            myWorker.Start(fileList);
            //this.AnimateTime.Start();
        }

        public void ReLoadItems()
        {
            //string strFilter = "*.prt;*.prn";
            //string strFilter = "*.jpg;*.png;*.gif;*.bmp";

            //List<string> fileList = new List<string>();
            //string[] arExtensions = strFilter.Split(';');

            //foreach (string filter in arExtensions)
            //{
            //    string[] strFiles = Directory.GetFiles(folderName, filter);
            //    fileList.AddRange(strFiles);
            //}

            //fileList.Sort();
            if (this.Items.Count > 0)
            {
                m_LastPreviewForlder = this.m_PrivewFileManager.Add(folderName);
                LoadItems();
            }
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
            this.m_PrivewFileManager.SaveListToXML();
        }

        private void AnimateTime_Tick(object sender, EventArgs e)
        {
            //DrawListViewItemEventArgs ex = new DrawListViewItemEventArgs(this.AnimateItem.ListView.CreateGraphics(), 
            //    this.AnimateItem,this.AnimateItem.Bounds, this.AnimateItem.Index, ListViewItemStates.Indeterminate);
            Graphics g = this.CreateGraphics();
            //Begin the animation.
            AnimateImage();

            //Get the next frame ready for rendering.
            ImageAnimator.UpdateFrames();

            //Draw the next frame in the animation.
            g.DrawImage(this.animatedImage, new Point(0, 0));
            g.Dispose();
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

        private void OnFrameChanged(object o, EventArgs e)
        {
            //Force a call to the Paint event handler. 
            this.Invalidate();
        }
    }
}
