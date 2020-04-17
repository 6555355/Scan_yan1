#region gnu_license
/*
    Crystal Controls - C# control library containing the following tools:
        CrystalControl - base class
        CrystalGradientControl - a control that can either have a gradient background or be totally transparent.
        CrystalLabel - a homegrown label that can have a gradient or transparent background.
        CrystalPanel - a panel that can have a gradient or transparent background.
        CrystalTrackBar - a homegrown trackbar that can have a gradient or transparent background.
        CrystalToolStripTrackBar - a host for CrystalTrackBar that allows it to work in a ToolStrip.
        
        CrystalImageGridView - a control that hosts thumbnail images in a virtual grid.
        CrystalImageGridModel - a data model that holds a collection of CrystalImageItems
                                to feed to CrystalImageGridView.
        CrystalImageItem - a class that describes an Image file.
        CrystalThumbnailer - provides thumbnailing methods for images.

        CrystalCollector - a base class for a controller that links 
                            CrystalImageGridView to the CrystalImageGridModel.
        CrystalFileCollector - a controller that works on disk-based Image files.
        CrystalDesignCollector - a controller that works in Visual Studio toolbox designer.
        CrystalMemoryCollector - a controller that can be used to add images from memory.
        CrystalMemoryZipCollector - a controller that accesses images in zip files by streaming them into memory.
        CrystalZipCollector - a controller that accesses images in zip files by unpacking them.
        CrystalRarCollector - a controller that accesses images in rar files by unpacking them.

        CrystalPictureBox - a picture box control, derived from CrystalGradientControl.
        CrystalPictureShow - a control for viewing images and processing slideshows.
        CrystalComicShow - a control for viewing comic-book images in the CDisplay format.
 
    Copyright (C) 2006, 2008 Richard Guion
    Attilan Software Factory: http://www.attilan.com
    Contact: richard@attilan.com

   Version 1.0.0
        This is a work in progress: USE AT YOUR OWN RISK!  Interfaces/Methods may change!
 
    This library is free software; you can redistribute it and/or
    modify it under the terms of the GNU Lesser General Public
    License as published by the Free Software Foundation; either
    version 2.1 of the License, or (at your option) any later version.

    This library is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public
    License along with this library; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
 */
#endregion

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
namespace BYHXPrinterManager.GradientControls
{
    public class CrystalDrawing
    {
        public static void RoundRectPath(GraphicsPath path, RectangleF rect, int arcSize, int borderWidth)
        {
            path.StartFigure();
			if(arcSize == 0)
				path.AddRectangle(rect);
			else
			{
				float bottom = rect.Y + rect.Height - arcSize - borderWidth;
				float right = rect.X + rect.Width - arcSize - borderWidth;

				// top left
				path.AddArc(new RectangleF(rect.X, rect.Y, arcSize, arcSize), 180, 90);

				// top right
				path.AddArc(new RectangleF(right, rect.Y, arcSize, arcSize), 270, 90);

				// bottom right
				path.AddArc(new RectangleF(right, bottom, arcSize, arcSize), 0, 90);

				// bottom left
				path.AddArc(new RectangleF(rect.X, bottom, arcSize, arcSize), 90, 90);
			}
            // close the figure and automatically join the path
            path.CloseFigure();
        }


        public static void DrawRoundRect(Graphics gfx, Rectangle rect, Color borderColor, 
                                int arcSize, int borderWidth)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                RoundRectPath(path, rect, arcSize, borderWidth);
                gfx.DrawPath(new Pen(borderColor), path);
            }
        }


        public static void FillRoundRect(Graphics gfx, RectangleF rect, Color rectColor, 
                                int arcSize)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                RoundRectPath(path, rect, arcSize, 0);
                gfx.FillPath(new SolidBrush(rectColor), path);
            }
        }


        public static void LinearFillRoundRectWithDefColor(Graphics gfx, RectangleF rect,int arcSize)
        {
			try
			{
				if(rect.Width==0 || rect.Height==0)return;
//				Color color1 = Color.LightSkyBlue;//.FromArgb(0xff,0x75,0xbc,0xd9);
//				Color color2 = Color.DeepSkyBlue;//.FromArgb(0xff,0x00,0x62,0x89);
//				Color color3 = Color.LightSkyBlue;//.FromArgb(0xff,0x00,0x77,0xA7);
				Color color1 = Color.FromArgb(0xff,0x8d,0xd2,0xf3);
				Color color2 = Color.FromArgb(0xff,0x39,0xab,0xe1);
				Color color3 = Color.FromArgb(0xff,0x66,0xc3,0xef);

				LinearFillRoundRect( gfx, rect,color1,color2,color3,arcSize,LinearGradientMode.Vertical);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
        }


		public static void LinearFillRoundRect(Graphics gfx, RectangleF rect, 
			Color color1, Color color2,
			int arcSize,LinearGradientMode Mode)
		{
			try
			{
				if(rect.Width==0 || rect.Height==0)return;
				using (LinearGradientBrush linBrush = new LinearGradientBrush(rect, color1, color2, Mode))
				{
					if(arcSize == 0)
						gfx.FillRectangle(linBrush, rect);
					else
					{
						using (GraphicsPath path = new GraphicsPath())
						{
							//                RoundRectPath(path, rect, arcSize, 1);75bcd9
							RoundRectPath(path, rect, arcSize, 0);
							gfx.FillPath(linBrush, path);
						}
					}
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

//		public static void LinearFillRoundRect(Graphics gfx, Rectangle rect, 
//			Color color1, Color color2,
//			int arcSize,float Mode)
//		{
//			try
//			{
//				if(rect.Width==0 || rect.Height==0)return;
//				using (GraphicsPath path = new GraphicsPath())
//				{
//					//                RoundRectPath(path, rect, arcSize, 1);75bcd9
//					RoundRectPath(path, rect, arcSize, 0);
//					using (LinearGradientBrush linBrush = new LinearGradientBrush(rect, color1, color2, Mode))
//					{
//						gfx.FillPath(linBrush, path);
//					}
//				}
//			}
//			catch(Exception ex)
//			{
//				MessageBox.Show(ex.Message);
//			}
//		}


		public static void LinearFillRoundRect(Graphics gfx, RectangleF rect, 
			Color color1, Color color2, Color color3, 
			int arcSize,LinearGradientMode Mode)
		{
			try
			{
				if(rect.Width==0 || rect.Height==0)return;

				LinearGradientBrush linBrush = new LinearGradientBrush(rect, color1, color2, Mode);
				ColorBlend cb = new ColorBlend(3);
				cb.Colors=new Color[3]{color1,color2,color3};
				cb.Positions = new float[3]{0,0.5f,1};
				linBrush.InterpolationColors =cb;

				if(arcSize == 0)
					gfx.FillRectangle(linBrush, rect);
				else
				{
					using (GraphicsPath path = new GraphicsPath())
					{
						//                RoundRectPath(path, rect, arcSize, 1);75bcd9
						RoundRectPath(path, rect, arcSize, 0);
						gfx.FillPath(linBrush, path);
					}
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

//		public static void LinearFillRoundRect(Graphics gfx, Rectangle rect, 
//			Color color1, Color color2, Color color3, 
//			int arcSize,float Mode)
//		{
//			try
//			{
//				if(rect.Width==0 || rect.Height==0)return;
//				Rectangle rect1 = new Rectangle();
//				Rectangle rect2 = new Rectangle();
//				if(Mode == 90f)
//				{
//					rect1 = new Rectangle(rect.Location,new Size(rect.Width,(int)Math.Ceiling(rect.Height/2)));
//					rect2 = new Rectangle(new Point(rect.X,(int)(Math.Ceiling(rect.Height/2) + rect.Y-1)),new Size(rect.Width,(int)Math.Ceiling(rect.Height/2)));
//				}
//				else
//				{
//					rect1 = new Rectangle(rect.Location,new Size((int)Math.Ceiling(rect.Width/2),rect.Height));
//					rect2 = new Rectangle(new Point((int)(Math.Ceiling(rect.Width/2) + rect.X-1),rect.Y),new Size((int)Math.Ceiling(rect.Width/2),rect.Height));
//				}
//				LinearFillRoundRect( gfx, rect1,color1,color2,arcSize,Mode);
//				LinearFillRoundRect( gfx, rect2,color2,color3,arcSize,Mode);
//			}
//			catch(Exception ex)
//			{
//				MessageBox.Show(ex.Message);
//			}
//		}
    }
}
