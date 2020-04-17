using MultimediaLayout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace MultimediaLayout.Models
{
    /// <summary>
    /// 存储图片相关参数。图片大小的单位为英寸。
    /// </summary>
    [Serializable]
    public class ImageJobParam
    {
        /// <summary>
        /// 内部包含预览图的Border
        /// </summary>
        [NonSerialized]
        public Border ImageBorder;

        /// <summary>
        /// 图片实际宽度，单位英寸
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// 图片实际高度，单位英寸
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// 图片到原点的Y坐标，单位英寸
        /// </summary>
        public double Top { get; set; }

        /// <summary>
        /// 图片到原点的X坐标，单位英寸
        /// </summary>
        public double Left { get; set; }

        /// <summary>
        /// 图片源路径
        /// </summary>
        public string SourceUrl { get; set; }

        /// <summary>
        /// 图片对应预览图路径
        /// </summary>
        public string PreviewFile { get; set; }

        /// <summary>
        /// 图片在所在Canvas中的左边距，单位为WPF控件单位，即1/96英寸
        /// </summary>
        public double CanvasLeft { get; set; }

        /// <summary>
        /// 图片在所在Canvas中的上边距，单位为WPF控件单位，即1/96英寸
        /// </summary>
        public double CanvasTop { get; set; }

        public int ResolutionY
        {
            get;
            set;
        }

        public int ResolutionX
        {
            get;
            set;
        }


        public ImageJobParam(Border imageBorder,double width,double height,string sourceUrl,string previewFile)
        {
            ImageBorder = imageBorder;
            Width = width;
            Height = height;
            SourceUrl = sourceUrl;
            PreviewFile = previewFile;
            if (imageBorder != null)
            {
                CanvasLeft = Canvas.GetLeft(imageBorder);
                CanvasTop = Canvas.GetTop(imageBorder);
            }
        }

        public ImageJobParam(Border imageBorder,ImageJob job)
        {
            ImageBorder = imageBorder;
            Width = job.Width;
            Height = job.Height;
            SourceUrl = job.SourceUrl;
            PreviewFile = job.PreviewFile;
            ResolutionX = job.ResolutionX;
            ResolutionY = job.ResolutionY;
            if (imageBorder != null)
            {
                CanvasLeft = Canvas.GetLeft(imageBorder);
                CanvasTop = Canvas.GetTop(imageBorder);
            }
        }

        /// <summary>
        /// 保存到本地文件之前更新在对应Canvas中的坐标
        /// </summary>
        public void UpdateCanvasParam()
        {
            CanvasLeft = Canvas.GetLeft(ImageBorder);
            CanvasTop = Canvas.GetTop(ImageBorder);
        }

    }
}
