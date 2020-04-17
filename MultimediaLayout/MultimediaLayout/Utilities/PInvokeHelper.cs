using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MultimediaLayout.Utilities
{
    public struct POINT
    {
        public int X;
        public int Y;
        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    /* JobPrint.dll:多介质排版接口
     * struct image_interface{
	        double x;
	        double y;
	        const char * file;
        };
       int ImageTile(struct image_str argv[], int num, float height, float width,  char * file)
     */
    public struct IMAGE_INFO
    {
      public  double X;
      public  double Y;
      public string File;
    }

    public class PInvokeHelper
    {
        /// <summary>   
        /// 设置鼠标的坐标   
        /// </summary>   
        /// <param name="x">横坐标</param>   
        /// <param name="y">纵坐标</param>          
        [DllImport("User32")]
        public extern static void SetCursorPos(int x, int y);
        
        /// <summary>   
        /// 获取鼠标的坐标   
        /// </summary>   
        /// <param name="lpPoint">传址参数，坐标point类型</param>   
        /// <returns>获取成功返回真</returns>   
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out POINT pt);

      
        /// <summary>
        /// 多介质图片拼贴排版
        /// </summary>
        /// <param name="images">拼贴使用的图片信息数组</param>
        /// <param name="imageCount">图片个数</param>
        /// <param name="height">prt高度（单位：英寸）</param>
        /// <param name="width">prt宽度（单位：英寸）</param>
        /// <param name="prtFile">拼贴得到的prt文件名</param>
        /// <returns>
        ///   0:不合法的文件 
        ///   1:文件格式错误
        ///   2:分辨率不匹配
        ///   3:图片灰度不匹配
        ///   4:
        ///   5:
        ///   -1:ture
        /// </returns>
        [DllImport("JobPrint.dll", CharSet = CharSet.Ansi)]
        public static extern int ImageTile(IMAGE_INFO[] images, int imageCount, float height, float width,byte[] prtFile);
    }
}
