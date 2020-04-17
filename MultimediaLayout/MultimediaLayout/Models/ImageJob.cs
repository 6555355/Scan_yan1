using BYHXPrinterManager.JobListView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BYHXPrinterManager;

namespace MultimediaLayout.Models
{
    /// <summary>
    /// 对应UIJob的类
    /// </summary>
    public class ImageJob:ICloneable
    {
        private double width;
        private double height;
        private string previewFile;
        private string sourceUrl;
        private int resolutionX;   
        private int resolutionY;

       
        public ImageJob(double width, double height, string imageSource, string sourceUrl)
        {
            this.height = height;
            this.width = width;
            this.previewFile = imageSource;
            this.sourceUrl = sourceUrl;
        }

        public ImageJob(UIJob job)
        {
            this.height = job.JobSize.Height;
            this.width = job.JobSize.Width;
            this.previewFile = PubFunc.GetFullPreviewPath(job.PreViewFile);
            this.sourceUrl = job.FileLocation;
            this.resolutionX = job.ResolutionX;
            this.resolutionY = job.ResolutionY;
        }

        public int ResolutionY
        {
            get { return resolutionY; }
            set { resolutionY = value; }
        }

        public int ResolutionX
        {
            get { return resolutionX; }
            set { resolutionX = value; }
        }

        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        public double Height
        {
            get { return height; }
            set { height = value; }
        }  

        public string PreviewFile
        {
            get { return previewFile; }
            set { previewFile = value; }
        }

        public string SourceUrl
        {
            get { return sourceUrl; }
            set { sourceUrl = value; }
        }

        public string ImageName
        {
            get
            {
                return System.IO.Path.GetFileName(this.sourceUrl);
            }
        }

        public object Clone()
        {
            ImageJob job = new ImageJob(this.width, this.height, this.previewFile, this.sourceUrl);
            job.PreviewFile = this.PreviewFile;
            job.ResolutionX = this.ResolutionX;
            job.ResolutionY = this.ResolutionY;
            return job;
        }
    }
}
