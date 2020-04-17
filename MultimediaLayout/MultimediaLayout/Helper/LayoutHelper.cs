using BYHXPrinterManager.JobListView;
using BYHXPrinterManager.Preview;
using MultimediaLayout.Controls;
using MultimediaLayout.Models;
using MultimediaLayout.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MultimediaLayout.Helper
{
    public class LayoutHelper : INotifyPropertyChanged
    {
        public readonly string LAYOUT_FILE_NAME = "multimediaLayout.dat";
        private List<DragCanvas> dragCanvasList = new List<DragCanvas>(); 
        private double zoomRatio = 1;
        private Dictionary<int, List<ImageJobParam>> layoutDatas = new Dictionary<int, List<ImageJobParam>>();
        private List<Paper> realPapers;//实际大小的纸张
        private List<Paper> papers;//界面显示大小的纸张
        private double layoutAreaWidth=0;

        /// <summary>
        /// 整个排版工作区宽度(WPF单位)
        /// </summary>
        public double LayoutAreaWidth
        {
            get { return layoutAreaWidth; }
            set 
            {
                layoutAreaWidth = value;
                if (layoutAreaWidth != 0)
                {
                    RefreshPapers();
                }
            }
        }

        /// <summary>
        /// 实际排版工作区宽度（单位：英寸）
        /// </summary>
        public double RealTotalWidth
        {
            get
            {
                double realTotalWidth = 0;
                foreach (var p in realPapers)
                {
                    realTotalWidth += (p.Width + p.LeftMargin);
                }
                return realTotalWidth;
            }
        }

        /// <summary>
        /// 实际大小的纸张集合，单位为英寸
        /// </summary>
        public List<Paper> RealPapers
        {
            get
            {
                return realPapers;
            }
        }

        /// <summary>
        /// 转换后的Paper集合，单位为WPF控件单位。（1/96英寸）
        /// </summary>
        public List<Paper> Papers
        {
            get { return papers; }
            set 
            {
                papers = value;
                RaisePropertyChanged("Papers");
            }
        }

        public Dictionary<int, List<ImageJobParam>> LayoutDatas
        {
            get { return layoutDatas; }
        }

        /// <summary>
        /// 真实大小（单位：英寸）到WPF控件大小（单位：1/96英寸）的缩放比例
        /// </summary>
        public double ZoomRatio
        {
            get { return zoomRatio; }
            set { zoomRatio = value; }
        }

        public DragCanvas FirstDragCanvas
        {
            get
            {
                return dragCanvasList.FirstOrDefault(d => (int)d.Tag == 0);
            }
        }


        public string MultimediaLayoutDir
        {
            get
            {
                var fullDir = Path.Combine(System.Windows.Forms.Application.StartupPath, "MultimediaLayout");
                if(!Directory.Exists(fullDir))
                {
                    Directory.CreateDirectory(fullDir);
                }
                return fullDir;
            }
        }

        private static readonly LayoutHelper instance = new LayoutHelper();

        public static LayoutHelper Instance
        {
            get { return LayoutHelper.instance; }
        } 

        private LayoutHelper()
        {
            
            RestoreLayoutData();
        }

        public void SetPapers(List<Paper> papers)
        {
            realPapers = papers;
            if (LayoutAreaWidth != 0)
            {
                RefreshPapers();
            }
        }

        private  void RefreshPapers()
        {
            if (LayoutAreaWidth == 0)
            {
                throw new InvalidOperationException("The value of LayoutAreaWidth is zero.Please set the value when MainWindow loaded! ");
            }
           
            if (RealTotalWidth == 0)
            {
                throw new ArgumentException("The total width of all the papers is zero.");
            }

            var UIPapers = new List<Paper>();//勿漏掉,每次重新创建是为了让相应的Canvas重新加载触发Loaded事件。
            int mediaCount = realPapers.Count;
            if (mediaCount >= 1)
            {
                //zoomRatio = CoreConst.BasePaperWidth / realPapers[0].Width;
                zoomRatio = LayoutAreaWidth / RealTotalWidth;

                for (int j = 0; j < realPapers.Count; j++)
                {
                    Paper p = new Paper(realPapers[j].Width * zoomRatio, realPapers[j].LeftMargin * zoomRatio, j);
                    //p.Id = j;
                    UIPapers.Add(p);
                }
            }
            Papers = UIPapers;
        }

        public void UpdatePaperSize(List<Paper> originPapers)
        {
            this.SetPapers(originPapers);
        }

        /// <summary>
        /// 还原应用程序上次保存的排版数据
        /// </summary>
        private void RestoreLayoutData()
        {
            var oldData = SerializationUtility.FileDeSerialize(LAYOUT_FILE_NAME) as Dictionary<int, List<ImageJobParam>>;
            if (oldData != null)
            {
                layoutDatas = oldData;
            }
        }

        public void AddLayoutData(int canvasTag, ImageJobParam job)
        {
            if (!layoutDatas.ContainsKey(canvasTag))
            {
                layoutDatas.Add(canvasTag, new List<ImageJobParam>() { job });
            }
            else
            {
                layoutDatas[canvasTag].Add(job);
            }
        }

        public void RemoveLayoutData(int canvasTag, UIElement element)
        {
            var findIndex = dragCanvasList.FindIndex(d => (int)d.Tag == canvasTag);
            if (findIndex >= 0)
            {
                dragCanvasList[findIndex].Children.Remove(element);
            }
            if (layoutDatas.ContainsKey(canvasTag))
            {
                var find = layoutDatas[canvasTag].FirstOrDefault(j => j.ImageBorder == (Border)element);
                layoutDatas[canvasTag].Remove(find);
            }
        }

        public void ClearLayoutData(int canvasTag)
        {
            var findIndex = dragCanvasList.FindIndex(d => (int)d.Tag == canvasTag);
            if (findIndex >= 0)
            {
                dragCanvasList[findIndex].Children.Clear();
            }
            if (layoutDatas.ContainsKey(canvasTag))
            {
                layoutDatas[canvasTag].Clear();
            }
        }

        public void ClearAllLayoutData()
        {
            foreach (var tag in layoutDatas.Keys)
            {
                var findIndex = dragCanvasList.FindIndex(d => (int)d.Tag == tag);
                if (findIndex >= 0)
                {
                    dragCanvasList[findIndex].Children.Clear();
                }
                layoutDatas[tag].Clear();
            }
        }

        public bool CheckResolution(int resolutionX, int resolutionY)
        {
            foreach (var canvasTag in LayoutDatas.Keys)
            {
                foreach (var job in LayoutDatas[canvasTag])
                {
                    if (job.ResolutionX != resolutionX || job.ResolutionY != resolutionY)
                    {
                       
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 当Canvas控件加载完毕时，更新LayoutHelper持有的Canvas数据
        /// </summary>
        /// <param name="dragCanvas"></param>
        public void OnDragCanvasLoaded(DragCanvas dragCanvas)
        {
            var index = dragCanvasList.FindIndex(d => (int)d.Tag == (int)dragCanvas.Tag);
            if (index != -1)
            {
                dragCanvasList.RemoveAt(index);
            }
            dragCanvasList.Add(dragCanvas);
            int tag = (int)dragCanvas.Tag;
            if (layoutDatas.ContainsKey(tag))
            {
                foreach (var item in layoutDatas[tag])
                {
                    if (!File.Exists(item.PreviewFile))
                        continue;
                    //从内存中还原的数据。发生场景：排版窗口关闭后重新打开
                    if (item.ImageBorder != null)
                    {
                        DragCanvas oldParent = item.ImageBorder.Parent as DragCanvas;
                        if (oldParent != null)
                        {
                            //解除原有的父子关系
                            oldParent.Children.Remove(item.ImageBorder);
                        }
                    }
                    //若是从本地文件还原的数据，ImageBorder没有被序列化到本地需要重新生成。发生场景：整个应用程序关闭后重新打开
                    else
                    {
                        item.ImageBorder = GenImageWithBorder(item.Width, item.Height, item.PreviewFile);
                        Canvas.SetLeft(item.ImageBorder, item.CanvasLeft);
                        Canvas.SetTop(item.ImageBorder, item.CanvasTop);
                    }
                   
                    dragCanvas.Children.Add(item.ImageBorder);
                    
                }
            }
        }

        public Border GenImageWithBorder(double imageWidth, double imageHeight, string imagePath)
        {
            Border imageBorder = new Border();
            imageBorder.Background = Brushes.White;
            imageBorder.BorderThickness = new Thickness(1);
            imageBorder.BorderBrush = MainWindow.DottedLineBrush;
            imageBorder.Width = imageWidth * zoomRatio;
            imageBorder.Height = imageHeight * zoomRatio;
            Image image = new Image();// App.Current.FindResource("demoImage") as Image;
            //image.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Shade360CMYK.jpg"));
            image.Width = imageWidth * zoomRatio;
            image.Height = imageHeight * zoomRatio;
            image.Source = new BitmapImage(new Uri(imagePath));
            image.SnapsToDevicePixels = true;
            imageBorder.Child = image;
            imageBorder.Loaded += (o, e) =>
                {
                    imageBorder.ToolTip = string.Format("image actual width*height:{0}*{1}{6}border actual width*height:{2}*{3}{6}inch:imageWidth*imageHeight:{4}*{5}{6}zoomRatio:{7}",
                        image.ActualWidth, image.ActualHeight,
                        imageBorder.ActualWidth, imageBorder.ActualHeight, 
                        imageWidth, imageHeight,
                        Environment.NewLine,zoomRatio);
                };
            return imageBorder;
        }

        public void SaveToFile()
        {


            foreach (var tag in layoutDatas.Keys)
            {
                for (int i = 0; i < layoutDatas[tag].Count; i++)
                {
                    layoutDatas[tag][i].UpdateCanvasParam();         
                }
            }
            SerializationUtility.FileSerialize(LAYOUT_FILE_NAME, LayoutDatas);
        }


        #region 为打印提供数据的接口
        /// <summary>
        /// 计算每个图片相对于原点(0,0)的坐标。取第一个Canvas的左顶点作为原点。
        /// </summary>
        private void ReCalculateCoord()
        {
            var firstCanvas = FirstDragCanvas;
            Console.WriteLine("First Canvas Width:{0}", firstCanvas.Width);
            foreach (var canvasTag in LayoutDatas.Keys)
            {
                List<ImageJobParam> jobs = LayoutDatas[canvasTag];
                for (int i = 0; i < jobs.Count; i++)
                {
                    var job = jobs[i];
                    Border b = job.ImageBorder;
                    var left = Canvas.GetLeft(b);
                    var top = Canvas.GetTop(b);
                    var p = b.TranslatePoint(new Point(0, 0), firstCanvas);
                    Console.WriteLine("Border:{0}*{1}   Job:{2}*{3}", b.Width, b.Height, job.Width, job.Height);
                    Console.WriteLine("Orgin:left-{0},top-{1}   Transform:left-{2},top-{3}; Canvas Tag:{4}", left, top, p.X, p.Y, canvasTag);
                    job.Left = p.X / zoomRatio;
                    job.Top = p.Y / zoomRatio;
                    Console.WriteLine("相对于（0,0）的实际坐标，单位-英寸：Job （Left，Right):({0},{1})", job.Left, job.Top);
                }
            }
        }

        public UIJob NewUIJob
        {
            get
            {
                return GenNewUIJob();
            }
        }

        public UIJob GenNewUIJob()
        {
            ReCalculateCoord();
            UIJob newUIJob = new UIJob();
            List<ImageJobParam> imageJobs = new List<ImageJobParam>();
            foreach (var canvasTag in LayoutDatas.Keys)
            {
                imageJobs.AddRange(LayoutDatas[canvasTag]);
            }
            newUIJob.Clips.Childs = new JobClip[imageJobs.Count];
            for (int i = 0; i < imageJobs.Count; i++)
            {
                JobClip jobClip = new JobClip(false);
                jobClip.src = imageJobs[i].SourceUrl;
                jobClip.Left = (int)imageJobs[i].Left;
                jobClip.Top = (int)imageJobs[i].Top;
                jobClip.W = (int)imageJobs[i].Width;
                jobClip.H = (int)imageJobs[i].Height;
                newUIJob.Clips.Childs[i] = jobClip;
            }
            return newUIJob;
        }



        public string GenPrtData()
        {
            string fileName=string.Empty;
            ReCalculateCoord();
            byte[] prtFile=new byte[64];
            List<ImageJobParam> imageJobs = new List<ImageJobParam>();
            
            foreach (var canvasTag in LayoutDatas.Keys)
            {
                imageJobs.AddRange(LayoutDatas[canvasTag]);
            }
            IMAGE_INFO[] images = new IMAGE_INFO[imageJobs.Count];
            double prtHeight = 30;
            for (int i = 0; i < imageJobs.Count; i++)
            {
                IMAGE_INFO image = new IMAGE_INFO()
                {
                    X=imageJobs[i].Left,
                    Y=imageJobs[i].Top,
                    File =imageJobs[i].SourceUrl
                };
                images[i] = image;
                if (prtHeight < image.Y + imageJobs[i].Height)
                {
                    prtHeight = image.Y + imageJobs[i].Height;
                }
            }
            prtHeight = prtHeight + 5;


           int rect = PInvokeHelper.ImageTile(images, images.Length, (float)prtHeight, (float)RealTotalWidth, prtFile);
           if (rect == -1)
           {
               var prtName = System.Text.UnicodeEncoding.ASCII.GetString(prtFile).Replace("./", "").Replace("\0", "").TrimEnd();
               try
               {
                   var newFileName = Path.Combine(MultimediaLayoutDir, DateTime.Now.ToString("yyyyMMddHHmmss") + ".prt");
                   File.Copy(prtName, newFileName);
                   fileName = newFileName;
               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.ToString());
               }
              
           }
           return fileName;
        }
        #endregion
        #region INotifyPropertyChanged Impl
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
