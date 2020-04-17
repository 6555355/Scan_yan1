using BYHXPrinterManager.JobListView;
using BYHXPrinterManager.Preview;
using MultimediaLayout.Controls;
using MultimediaLayout.Helper;
using MultimediaLayout.Models;
using MultimediaLayout.Utilities;
using MultimediaLayout.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BYHXPrinterManager;
using LayoutHelper = MultimediaLayout.Helper.LayoutHelper;


namespace MultimediaLayout.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged, IDropTarget
    {
        private ObservableCollection<ImageJob> imageJobs=new ObservableCollection<ImageJob>();

        public ObservableCollection<ImageJob> ImageJobs
        {
            get { return imageJobs; }
            set { imageJobs = value; }
        }

        public int JobCount
        {
            get
            {
                return ImageJobs.Count;
            }
        }

        public MainViewModel(IEnumerable<UIJob> jobs,List<Paper> originPapers)
        {
            LayoutHelper.Instance.SetPapers(originPapers);
            LoadJobList(jobs);
        }

        private void LoadJobList(IEnumerable<UIJob> jobs)
        {
            foreach (var job in jobs)
            {
                ImageJob imageJob = new ImageJob(job);
                ImageJobs.Add(imageJob);
            }
        }


        #region IDropTarget Impl

        private DragCanvas targetDragCanvas;

        void IDropTarget.DragOver(DropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
            dropInfo.Effects = DragDropEffects.Move;
            targetDragCanvas = dropInfo.VisualTarget as DragCanvas;
        }

        void IDropTarget.Drop(DropInfo dropInfo)
        {
            ImageJob job = dropInfo.Data as ImageJob;
            if (!LayoutHelper.Instance.CheckResolution(job.ResolutionX, job.ResolutionY))
            {
                string msg = LocalizationHelper.GetStringResource("ResolutionDiffer");
                MessageBox.Show(msg);
                return;
            }

            Border imageBorder = LayoutHelper.Instance.GenImageWithBorder(job.Width, job.Height,PubFunc.GetFullPreviewPath(job.PreviewFile));
            Point p = new Point(10, 10);
            POINT pp;
            if (PInvokeHelper.GetCursorPos(out pp))
            {
                p = targetDragCanvas.PointFromScreen(new Point(pp.X, pp.Y));
                if (p.X + imageBorder.Width > targetDragCanvas.ActualWidth)
                {
                    p.X = targetDragCanvas.ActualWidth - imageBorder.Width;
                }
                if (p.Y + imageBorder.Height > targetDragCanvas.ActualHeight)
                {
                    p.Y = targetDragCanvas.ActualHeight - imageBorder.Height;
                }
            }
            if (p.X <0 || p.Y <0)
            {
                string msg = LocalizationHelper.GetStringResource("ImageTooLarge");
                MessageBox.Show(msg);
                return;
            }
            Canvas.SetLeft(imageBorder, p.X);
            Canvas.SetTop(imageBorder, p.Y);
            targetDragCanvas.Children.Add(imageBorder);
            //LayoutHelper.Instance.AddLayoutData((int)targetDragCanvas.Tag, new ImageJobParam(imageBorder, job.Width, job.Height, job.SourceUrl, job.PreviewFile));
            LayoutHelper.Instance.AddLayoutData((int)targetDragCanvas.Tag, new ImageJobParam(imageBorder,job));
            //targetDragCanvas.ResetZOrder();     
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
