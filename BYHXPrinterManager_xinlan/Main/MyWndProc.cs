using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.IO;
using BYHXPrinterManager.GradientControls;

namespace BYHXPrinterManager.Main
{
	/// <summary>
	/// Form2 的摘要说明。
	/// </summary>
	public class MyWndProc
	{
		private const int SM_CXSMICON = 49; //以像素计算的小图标的尺寸，小图标一般出现在窗口标题栏上。
		private const int SM_CYSMICON = 50; //以像素计算的小图标的尺寸，小图标一般出现在窗口标题栏上。
		private const int SM_CYCAPTION = 4; //以像素计算的普通窗口标题的高度
		private const int SM_CXSIZEFRAME = 32; //围绕可改变大小的窗口的边框的厚度
		private const int SM_CYSIZEFRAME = 33; //围绕可改变大小的窗口的边框的厚度
		private const int SM_CXSIZE = 30; //以像素计算的标题栏按钮的尺寸
		private const int SM_CYSIZE = 1; //以像素计算的标题栏按钮的尺寸
		private const int SM_CXBORDER = 5;//返回以像素值为单位的Windows窗口边框的宽度和高度，如果Windows的为3D形态，则等同于SM_CXEDGE参数
		private const int SM_CYBORDER = 6;//返回以像素值为单位的Windows窗口边框的宽度和高度，如果Windows的为3D形态，则等同于SM_CXEDGE参数
		private const int WM_SYSCOMMAND = 0x112;
//		private const int SC_CLOSE = 0xF060;
//		private const int SC_MINIMIZE = 0xF020;
//		private const int SC_MAXIMIZE = 0xF030;
//		private const int SC_RESTORE = 0xF120;

		const uint TPM_LEFTBUTTON   =   0; 
		const uint TPM_RIGHTBUTTON   =   2; 
		const uint TPM_LEFTALIGN   =   0; 
		const uint TPM_CENTERALIGN   =   4; 
		const uint TPM_RIGHTALIGN   =   8; 
		const uint TPM_TOPALIGN   =   0; 
		const uint TPM_VCENTERALIGN   =   0x10; 
		const uint TPM_BOTTOMALIGN   =   0x20; 
		const uint TPM_RETURNCMD   =   0x100; 


		#region   DllImport 
		[DllImport( "User32.dll ")] 
		static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert); 
		[DllImport( "User32.dll ")] 
		static extern bool GetCursorPos(out Point lpPoint); 
		[DllImport( "User32.dll ")] 
		static extern int TrackPopupMenu(IntPtr hMenu, uint uFlags, 
			int x, int y, int nReserved, IntPtr hWnd, out Rectangle prcRect); 
		[DllImport( "User32.DLL ")] 
		public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam); 

		[DllImport("uxtheme", ExactSpelling = true, CharSet = CharSet.Unicode)]
		public extern static Int32 SetWindowTheme(IntPtr hWnd, String textSubAppName, String textSubIdList);
		[DllImport ("User32.dll")]
		private static extern IntPtr GetWindowDC(IntPtr hwnd);
		[DllImport ("User32.dll")]
		private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);
		[DllImport("user32")]
		public static extern int GetSystemMetrics(int nIndex); 
		//		[DllImport("user32.dll")]
		//		public static extern int GetWindowRect(int hwnd, ref Rectangle rc);

		[DllImportAttribute("gdi32.dll")]   
		public static extern IntPtr CreateRoundRectRgn(int nLeftRect,int nTopRect,int nRightRect,int nBottomRect,int nWidthEllipse,int nHeightEllipse);   

		[DllImportAttribute("user32.dll")]   
		public static extern int SetWindowRgn(IntPtr hWnd,IntPtr hRgn, bool bRedraw);  
		#endregion 

		public static bool bMainMenu_Visible = false;
		public static bool bisMax = false;
		public static bool loadSucessed = false;
//		public static bool bNcLButtonDown = false;

		public static Image imgBtnRes;
		public static Image imgBtnMin;
		public static Image imgBtnMax;
		public static Image imgBtnClose;
		public static Image imgBtnCustom;
        public static Image ImgFormheader;//不透明背景.jpg");//pic_titbg02.gif");

        public static Image imgTools;
        public static Image imgToolSetting;
		public static Image imgFrameLeft;
		public static Image imgFrameRight;
		public static Image imgFrameBottom;
		public static Image imgbackground_main;
		public static Image imgbackground;

		public static Rectangle m_rect = new Rectangle();//(205, 6, 20, 20);
		public static Rectangle m_rectMin = new Rectangle();
		public static Rectangle m_rectMax = new Rectangle();
		public static Rectangle m_rectClo = new Rectangle();
		public static RectangleF rcIcon = new Rectangle();
		public static RectangleF rcCaption= new Rectangle();

        //Color.FromArgb(153, 187, 225), Color.FromArgb(234, 243, 252)
	    public static Color HeaderColor1 = Color.FromArgb(0x65,0x93,0xb7);
	    public static Color HeaderColor2 = Color.FromArgb(0x51,0x7c,0x9e);
		public static Color HeaderColor3 = Color.FromArgb(0x48,0x74,0x97);

        public static bool CanDrawFormHeader{get
        {
            return
                !(imgBtnCustom == null || imgBtnClose == null || imgBtnMax == null || imgBtnMin == null ||
                  ImgFormheader == null);
        }}

		public static bool LoadRenderResource(string skinPath)
		{
		    try
		    {
		        if (string.IsNullOrEmpty(skinPath) || !Directory.Exists(skinPath))
		        {
		            loadSucessed = false;
		            return loadSucessed;
		        }
		        string imgpath = string.Empty;
		        imgpath = Path.Combine(skinPath, "btnRes.png");
		        imgBtnRes = Image.FromFile(imgpath);
		        imgpath = Path.Combine(skinPath, "btnMin.png");
		        imgBtnMin = Image.FromFile(imgpath);
		        imgpath = Path.Combine(skinPath, "btnMax.png");
		        imgBtnMax = Image.FromFile(imgpath);
		        imgpath = Path.Combine(skinPath, "btnClose.png");
		        imgBtnClose = Image.FromFile(imgpath);
		        imgpath = Path.Combine(skinPath, "btnCustom.png");
		        imgBtnCustom = Image.FromFile(imgpath);
		        imgpath = Path.Combine(skinPath, "formheader.png");
		        ImgFormheader = Image.FromFile(imgpath); //不透明背景.jpg");//pic_titbg02.gif");

		        imgpath = Path.Combine(skinPath, "toolsbar.png");
		        imgTools = Image.FromFile(imgpath);
		        imgpath = Path.Combine(skinPath, "toolbarSetting.png");
		        imgToolSetting = Image.FromFile(imgpath);
		        imgpath = Path.Combine(skinPath, "V_border_L.png");
		        imgFrameLeft = Image.FromFile(imgpath);
		        imgpath = Path.Combine(skinPath, "V_border_R.png");
		        imgFrameRight = Image.FromFile(imgpath);
		        imgpath = Path.Combine(skinPath, "H_border.png");
		        imgFrameBottom = Image.FromFile(imgpath);
		        imgpath = Path.Combine(skinPath, "background_main.png");
		        imgbackground_main = Image.FromFile(imgpath);
		        imgpath = Path.Combine(skinPath, "background.png");
		        imgbackground = Image.FromFile(imgpath);
		        loadSucessed = true;
		    }
		    catch (Exception)
		    {
		        loadSucessed = false;
		    }
		    return loadSucessed;
		}


		public static void PaintFormCaption(ref Message m,Form mform,bool isMainForm)
		{
			if(!loadSucessed)
				return;

            if (!CanDrawFormHeader)
                return;

			SizeF szIcon = new SizeF(GetSystemMetrics(SM_CXSMICON),GetSystemMetrics(SM_CXSMICON));
			int xSizeFrame = SystemInformation.FrameBorderSize.Width;//GetSystemMetrics(SM_CXSIZEFRAME)
			int ySizeFrame = SystemInformation.FrameBorderSize.Height;//GetSystemMetrics(SM_CYSIZEFRAME)
			int iCaptionH = SystemInformation.CaptionHeight;//GetSystemMetrics(SM_CYCAPTION)
			SizeF szCaptionButton = SystemInformation.CaptionButtonSize; //new SizeF(GetSystemMetrics(SM_CXSIZE),GetSystemMetrics(SM_CYSIZE))
			int xBorder = SystemInformation.Border3DSize.Width;//GetSystemMetrics(SM_CXBORDER)
			int yBorder = SystemInformation.Border3DSize.Height;//GetSystemMetrics(SM_CYBORDER)

            //重新计算按钮位置
            ReCALCButtonRectangle(mform, SystemInformation.FrameBorderSize, SystemInformation.Border3DSize, szCaptionButton);

			rcIcon = new RectangleF(xSizeFrame,ySizeFrame,szIcon.Width,szIcon.Height);
			//获得标题栏大小
			SizeF barSi = new SizeF(mform.Width,iCaptionH + ySizeFrame);
			rcCaption = new RectangleF(new Point(0,0),barSi);
			//重画代码   
			switch(m.Msg)    
			{
				case 0x84://WM_NCHITTEST
				{
					Point mousePoint = new Point((int)m.LParam);
					mousePoint.Offset(-mform.Left, -mform.Top);
					if(isMainForm)
					{
//						if(m_rect.Contains(mousePoint))
//						{
//							m.Result = (IntPtr)NCHitTestResult.HTMENU;
//						}
						if(m_rectMin.Contains(mousePoint))
						{
							m.Result = (IntPtr)NCHitTestResult.HTMINBUTTON;
						}
						if(m_rectMax.Contains(mousePoint) && mform.WindowState != FormWindowState.Maximized)
						{
							m.Result = (IntPtr)NCHitTestResult.HTMAXBUTTON;
						}
					}
					if(m_rectClo.Contains(mousePoint))
					{
						m.Result = (IntPtr)NCHitTestResult.HTCLOSE;
					}
					break;
				}
				case 0x03://WM_MOVE
				case 0x0f://WM_PAINT
					//				case 0x4e://WM_NOTIFY
				case 0x86://WM_NCACTIVATE
				case 0x85://WM_NCPAINT
				case 0x06: //WM_ACTIVATE
					#region /WM_ACTIVATE WM_NCPAINT WM_NCACTIVATE
				{
					DrawNormalCaption(m,isMainForm,mform);
					break;
				}
					#endregion
				case 0xA0://WM_NCMOUSEMOVE 
					#region //WM_NCMOUSEMOVE 
				{
					Point mousePoint = new Point((int)m.LParam);
					mousePoint.Offset(-mform.Left, -mform.Top);
					if(isMainForm)
					{
//						if(m_rect.Contains(mousePoint))
//						{
//							//画自定义按钮
//							DrawCustomButton(m,mform,ButtonState.All,imgBtnCustom,m_rect);
//						}
//						else 
//						{
//							//画自定义按钮
//							DrawCustomButton(m,mform,ButtonState.Normal,imgBtnCustom,m_rect);
//						}

						if(m_rectMin.Contains(mousePoint))
						{
							DrawMyCaptionButton(m,mform,ButtonState.All,imgBtnMin,m_rectMin);
						}
						else
						{
							DrawMyCaptionButton(m,mform,ButtonState.Normal,imgBtnMin,m_rectMin);
						}

						if(m_rectMax.Contains(mousePoint))
						{
							if(mform.WindowState == FormWindowState.Maximized)
							{
								DrawMyCaptionButton(m,mform,ButtonState.All,imgBtnRes,m_rectMax);
							}
							else
							{
								DrawMyCaptionButton(m,mform,ButtonState.All,imgBtnMax,m_rectMax);
							}
						}
						else
						{
							if(mform.WindowState == FormWindowState.Maximized)
							{
								DrawMyCaptionButton(m,mform,ButtonState.Normal,imgBtnRes,m_rectMax);
								//								mform.WindowState = FormWindowState.Normal;
							}
							else
							{
								DrawMyCaptionButton(m,mform,ButtonState.Normal,imgBtnMax,m_rectMax);
								//								mform.WindowState = FormWindowState.Maximized;
							}
						}
					}
					if(m_rectClo.Contains(mousePoint))
					{
						DrawMyCaptionButton(m,mform,ButtonState.All,imgBtnClose,m_rectClo);
					}
					else
					{
						DrawMyCaptionButton(m,mform,ButtonState.Normal,imgBtnClose,m_rectClo);
					}
					break;
				}
					#endregion
				case 0x83://WM_NCCALCSIZE
				{

                    //重新计算按钮位置
                    ReCALCButtonRectangle(mform, SystemInformation.FrameBorderSize, SystemInformation.Border3DSize, szCaptionButton);
					break;
				}
			}
		}

		public static bool NCButtonClick(Message m,Form mform,bool isMainForm)
		{
            if (!loadSucessed)
                return true;
            if (!CanDrawFormHeader)
		        return true;

			//			if(m.Msg != 0xA1) return;
			SizeF szIcon = new SizeF(GetSystemMetrics(SM_CXSMICON),GetSystemMetrics(SM_CXSMICON));
			int xSizeFrame = SystemInformation.FrameBorderSize.Width;//GetSystemMetrics(SM_CXSIZEFRAME)
			int ySizeFrame = SystemInformation.FrameBorderSize.Height;//GetSystemMetrics(SM_CYSIZEFRAME)
			int iCaptionH = SystemInformation.CaptionHeight;//GetSystemMetrics(SM_CYCAPTION)
			SizeF szCaptionButton = SystemInformation.CaptionButtonSize; //new SizeF(GetSystemMetrics(SM_CXSIZE),GetSystemMetrics(SM_CYSIZE))
			int xBorder = SystemInformation.Border3DSize.Width;//GetSystemMetrics(SM_CXBORDER)
			int yBorder = SystemInformation.Border3DSize.Height;//GetSystemMetrics(SM_CYBORDER)

			//重新计算按钮位置
            ReCALCButtonRectangle(mform, SystemInformation.FrameBorderSize, SystemInformation.Border3DSize, szCaptionButton);

			switch(m.Msg)    
			{
				case 0xA3://WM_NCLBUTTONDBLCLK
				{
					// 屏蔽自画按钮的双击事件
					Point mousePoint = new Point((int)m.LParam);
					mousePoint.Offset(-mform.Left, -mform.Top);
					if(isMainForm)
					{
						if(//m_rect.Contains(mousePoint) || 
							m_rectMin.Contains(mousePoint) || m_rectMax.Contains(mousePoint))
						{
							return false;
						}
					}
					if(m_rectClo.Contains(mousePoint))
					{
						return false;
					}
					break;
				}
				case 0xA1://WM_NCLBUTTONDOWN
					#region //WM_NCLBUTTONDOWN
				{
					Point mousePoint = new Point((int)m.LParam);
					mousePoint.Offset(-mform.Left, -mform.Top);

					if(isMainForm)
					{
//						if(m_rect.Contains(mousePoint))
//						{
//							//画自定义按钮
////							DrawCustomButton(m,mform,ButtonState.Pushed,imgBtnCustom,m_rect);
//
//							bMainMenu_Visible = !bMainMenu_Visible;
//							for(int i = 0;i < mform.Menu.MenuItems.Count;i++)
//								mform.Menu.MenuItems[i].Visible = bMainMenu_Visible;
//						}

						if(m_rectMin.Contains(mousePoint))
						{
//							DrawMyCaptionButton(m,mform,ButtonState.Pushed,imgBtnMin,m_rectMin);
							mform.WindowState = FormWindowState.Minimized;
							return false;
						}

						if(m_rectMax.Contains(mousePoint))
						{
							if(mform.WindowState == FormWindowState.Maximized)
							{
//								DrawMyCaptionButton(m,mform,ButtonState.Pushed,imgBtnRes,m_rectMax);
								mform.WindowState = FormWindowState.Normal;
							}
							else
							{
//								DrawMyCaptionButton(m,mform,ButtonState.Pushed,imgBtnMax,m_rectMax);
								mform.WindowState = FormWindowState.Maximized;
							}
							return false;
						}
						if(rcIcon.Contains(mousePoint))
						{
							Point vPoint; 
							Rectangle vRect; 
							GetCursorPos(out vPoint); 
							Form sf = new Form();
							sf.WindowState = mform.WindowState;
							SendMessage(mform.Handle, WM_SYSCOMMAND, TrackPopupMenu( 
								GetSystemMenu(sf.Handle, false), 
								TPM_RETURNCMD | TPM_LEFTBUTTON, vPoint.X, vPoint.Y,   
								0, mform.Handle, out vRect),   0); 
							sf.Dispose();
						}
					}
					if(m_rectClo.Contains(mousePoint))
					{
//						DrawMyCaptionButton(m,mform,ButtonState.Pushed,imgBtnClose,m_rectClo);
						if(isMainForm)
						{
							string info = ResString.GetEnumDisplayName(typeof(Confirm),Confirm.Exit);
							if(MessageBox.Show(info,ResString.GetProductName(),MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
							{
								return false;
							}
						}
						mform.Close();
					}
					break;
				}
					#endregion
			}
			return true;
		}

        private static void ReCALCButtonRectangle(Form mform, SizeF SizeFrame, SizeF Border3DSize, SizeF CaptionButtonSize)
        {
            int imargin = 2;// 按钮间距
            m_rect.X = Convert.ToInt32(mform.Width - SizeFrame.Width - 3 * imargin
                - (imgBtnCustom.Width + imgBtnClose.Width + imgBtnMax.Width + imgBtnMin.Width));//4 * szCaptionButton.Width);
            m_rect.Y = Convert.ToInt32(SizeFrame.Height + Border3DSize.Height);//6
            m_rect.Width = imgBtnCustom.Width;//(int)(szCaptionButton.Width);
            m_rect.Height = Convert.ToInt32(CaptionButtonSize.Height);//20;

            m_rectMin = new Rectangle(m_rect.Right + imargin, m_rect.Y, imgBtnMin.Width, m_rect.Height);
            m_rectMax = new Rectangle(m_rectMin.Right + imargin, m_rect.Y, imgBtnMax.Width, m_rect.Height);
            m_rectClo = new Rectangle(m_rectMax.Right + imargin, m_rect.Y, imgBtnClose.Width, m_rect.Height);
        }

		private static void DrawNormalCaption(Message m,bool isMainForm,Form mform)
		{
			SizeF szIcon = new SizeF(GetSystemMetrics(SM_CXSMICON),GetSystemMetrics(SM_CXSMICON));
			int xSizeFrame = SystemInformation.FrameBorderSize.Width;//GetSystemMetrics(SM_CXSIZEFRAME)
			int ySizeFrame = SystemInformation.FrameBorderSize.Height;//GetSystemMetrics(SM_CYSIZEFRAME)

			//把DC转换为.NET的Graphics就可以很方便地使用Framework提供的绘图功能了
			IntPtr hDC = GetWindowDC(m.HWnd);
			if(hDC == IntPtr.Zero || mform.WindowState == FormWindowState.Minimized)
				return;
			Graphics gs = Graphics.FromHdc(hDC);

			//画标题栏区域背景
            gs.DrawImage(ImgFormheader, rcCaption);//显示背景图片
			//					CrystalDrawing.LinearFillRoundRectWithDefColor(gs,new RectangleF(new PointF(xSizeFrame,ySizeFrame),barSi),1);

			//画图标
			gs.DrawImage(mform.Icon.ToBitmap(),rcIcon);
			//画标题
			PointF rc = new PointF(szIcon.Width + xSizeFrame,ySizeFrame);
			StringFormat sf = (StringFormat)StringFormat.GenericDefault.Clone();
			sf.Alignment = StringAlignment.Near;
			sf.LineAlignment = StringAlignment.Center;
			gs.DrawString(mform.Text,mform.Font,SystemBrushes.WindowText,rc,StringFormat.GenericDefault);

			//画自定义按钮
			if(isMainForm)
			{
//				DrawCustomButton(gs,ButtonState.Normal,imgBtnCustom,m_rect);

				//画最大,最小,关闭按钮			
				//						ControlPaint.DrawCaptionButton(gs,m_rectMin,CaptionButton.Minimize,ButtonState.Normal);
				DrawMyCaptionButton(gs,ButtonState.Normal,imgBtnMin,m_rectMin);
				if(mform.WindowState == FormWindowState.Maximized)
				{
					//							ControlPaint.DrawCaptionButton(gs,m_rectMax,CaptionButton.Restore,ButtonState.Normal);
					DrawMyCaptionButton(gs,ButtonState.Normal,imgBtnRes,m_rectMax);
				}
				else
				{
					//							ControlPaint.DrawCaptionButton(gs,m_rectMax,CaptionButton.Maximize,ButtonState.Normal);
					DrawMyCaptionButton(gs,ButtonState.Normal,imgBtnMax,m_rectMax);
				}
			}
			//					ControlPaint.DrawCaptionButton(gs,m_rectClo,CaptionButton.Close,ButtonState.Normal);
			DrawMyCaptionButton(gs,ButtonState.Normal,imgBtnClose,m_rectClo);

			// 画边框
			DrawBorder(gs,xSizeFrame,ySizeFrame,mform);

			//释放GDI资源
			ReleaseDC(m.HWnd, hDC);
			gs.Dispose();
		}


		private static void DrawBorder(Graphics gs,int xSizeFrame,int ySizeFrame,Form mform)
		{
			RectangleF rcFrameLeft = new RectangleF(0,SystemInformation.CaptionHeight + SystemInformation.FrameBorderSize.Height,xSizeFrame,mform.Height);
			RectangleF rcFrameRight = new RectangleF(mform.Width - xSizeFrame,SystemInformation.CaptionHeight + SystemInformation.FrameBorderSize.Height,xSizeFrame,mform.Height);
			RectangleF rcFrameBottom = new RectangleF(0,mform.Height - ySizeFrame,mform.Width,ySizeFrame);

			gs.DrawImage(imgFrameLeft,rcFrameLeft);
			gs.DrawImage(imgFrameRight,rcFrameRight);
			gs.DrawImage(imgFrameBottom,rcFrameBottom);

			gs.DrawPath(SystemPens.WindowFrame, RoundRectRgn(new Rectangle(mform.Location,new Size(Convert.ToInt32(mform.Width-SystemPens.WindowFrame.Width),Convert.ToInt32(mform.Height -SystemPens.WindowFrame.Width))),10));
		}


		private static void DrawCustomButton(Message m,Form mform, ButtonState bState,Image img,RectangleF rect)
		{
			//把DC转换为.NET的Graphics就可以很方便地使用Framework提供的绘图功能了
			IntPtr hDC = GetWindowDC(m.HWnd);
			if(hDC == IntPtr.Zero || mform.WindowState == FormWindowState.Minimized)
				return;
			Graphics grfx = Graphics.FromHdc(hDC);

			DrawCustomButton(grfx,bState,img,rect);

			//释放GDI资源
			ReleaseDC(m.HWnd, hDC);
			grfx.Dispose();
		}

		private static void DrawCustomButton(Graphics grfx, ButtonState bState,Image img,RectangleF rect)
		{
			if (img != null)
			{	
				SizeF ImageSize = new SizeF(img.Width,img.Height /2);
				RectangleF rectDest = new RectangleF(rect.Location, ImageSize);
				RectangleF rectSrc;

				//				if (bMainMenu_Visible)
				//				{
				//					rectSrc = new RectangleF(new PointF(0, 0), ImageSize);
				//				}		
				//				else
				//					rectSrc = new RectangleF(new PointF(0, 0), ImageSize);
				if (bState == ButtonState.Pushed)
				{
					rectSrc = new RectangleF(new PointF(0, 0), ImageSize);
				}		
				else if (bState == ButtonState.Normal)
					rectSrc = new RectangleF(new PointF(0, ImageSize.Height), ImageSize);
				else
					rectSrc = new RectangleF(new PointF(0, ImageSize.Height), ImageSize);
					
				//				if(bState != ButtonState.Normal)
				//					grfx.DrawRectangle(Pens.White,rect.X+2,rect.Y+2,ImageSize.Width-4,ImageSize.Height-4);
				//				else
				//					grfx.DrawRectangle(Pens.Transparent,rect.X+2,rect.Y+2,ImageSize.Width-4,ImageSize.Height-4);
				grfx.DrawImage(img, rectDest, rectSrc, GraphicsUnit.Pixel);
			}
			else
			{
				Point loc = new Point(Convert.ToInt32(rect.Location.X),Convert.ToInt32(rect.Location.Y));
				ControlPaint.DrawButton(grfx,new Rectangle(loc,rect.Size.ToSize()),ButtonState.Pushed);
				StringFormat strFmt = new StringFormat();
				strFmt.Alignment = StringAlignment.Center;
				strFmt.LineAlignment = StringAlignment.Center;
				if(!bMainMenu_Visible)
					grfx.DrawString("︾",Form.DefaultFont, Brushes.Black, rect, strFmt);
				else
					grfx.DrawString("︽", Form.DefaultFont, Brushes.Black, rect, strFmt);
			}
		}


		private static void DrawMyCaptionButton(Message m,Form mform,ButtonState bState,Image img,RectangleF rect)
		{
			if (img != null)
			{	
				//把DC转换为.NET的Graphics就可以很方便地使用Framework提供的绘图功能了
				IntPtr hDC = GetWindowDC(m.HWnd);
				if(hDC == IntPtr.Zero || mform.WindowState == FormWindowState.Minimized)
					return;
				Graphics grfx = Graphics.FromHdc(hDC);

				DrawMyCaptionButton(grfx,bState,img,rect);

				//释放GDI资源
				ReleaseDC(m.HWnd, hDC);
				grfx.Dispose();
			}
		}

		private static void DrawMyCaptionButton(Graphics grfx,ButtonState bState,Image img,RectangleF rect)
		{
			if (img != null)
			{	
				SizeF ImageSize = new SizeF(img.Width,img.Height /8);
				RectangleF rectDest = new RectangleF(rect.Location, ImageSize);
				RectangleF rectSrc;

				if (bState == ButtonState.Pushed)
				{
					rectSrc = new RectangleF(new PointF(0, 0), ImageSize);
				}		
				else if (bState == ButtonState.Normal)
					rectSrc = new RectangleF(new PointF(0, ImageSize.Height * 2), ImageSize);
				else
					rectSrc = new RectangleF(new PointF(0, ImageSize.Height),ImageSize);
					
				grfx.DrawImage(img, rectDest, rectSrc, GraphicsUnit.Pixel);
			}
		}


		public static void DrawRoundForm(Form fl)
		{
			if(!loadSucessed)
				return;
			//			IntPtr   regionHandle   =CreateRoundRectRgn(0,0,fl.Width,fl.Height,20,20);   
			Region   roundRegion   =   null;   
			//			SetWindowRgn(fl.Handle,regionHandle,true);
			//			roundRegion   =   Region.FromHrgn(regionHandle);
			roundRegion = new Region(RoundRectRgn(fl.Bounds,10));
			fl.Region = roundRegion;
		}

		/// <summary>
		/// 取得一个图片中非透明色部分的区域。
		/// </summary>
		/// <param name="Picture">取其区域的图片。</param>
		/// <param name="TransparentColor">透明色。</param>
		/// <returns>图片中非透明色部分的区域</returns>
		private Region BmpRgn(Bitmap Picture, Color TransparentColor)
		{
			int nWidth = Picture.Width;
			int nHeight = Picture.Height;
			Region rgn = new Region();
			rgn.MakeEmpty();
			bool isTransRgn;//前一个点是否在透明区
			Color curColor;//当前点的颜色
			Rectangle curRect = new Rectangle();
			curRect.Height = 1;
			int x = 0, y = 0;

			//逐像素扫描这个图片，找出非透明色部分区域并合并起来。
			for(y = 0; y < nHeight; ++y)
			{
				isTransRgn = true;
				for (x = 0; x < nWidth; ++x)
				{
					curColor = Picture.GetPixel(x,y);
					if(curColor == TransparentColor || x == nWidth - 1)//如果遇到透明色或行尾
					{
						if(isTransRgn == false)//退出有效区
						{
							curRect.Width = x - curRect.X;
							rgn.Union(curRect);
						}
					}

					else//非透明色
					{
						if(isTransRgn == true)//进入有效区
						{
							curRect.X = x;
							curRect.Y = y;
						}
					}//if curColor
					isTransRgn = curColor == TransparentColor;     
				}//for x
			}//for y
			return rgn;
		}


		public static GraphicsPath RoundRectRgn(RectangleF rect, int arcSize)
		{
			GraphicsPath path = new GraphicsPath();
			path.StartFigure();
			if(arcSize == 0)
				path.AddRectangle(rect);
			else
			{
				// top left
				path.AddArc(new RectangleF(0, 0, arcSize, arcSize), 180, 90);

				// top right
				path.AddArc(new RectangleF(rect.Width - arcSize, 0, arcSize, arcSize), 270, 90);

				// right
				path.AddLine(rect.Width,arcSize,rect.Width,rect.Height);

				// bottom
				path.AddLine(rect.Width,rect.Height,0,rect.Height);

				// left
				path.AddLine(0,rect.Height,0,arcSize);
			}
			// close the figure and automatically join the path
			path.CloseFigure();

			return path;
		}

		public static void DrawToolbarBackGroundImage(Graphics g,RectangleF rc,Image img)
		{
			rc.Inflate(0,2);
			//SolidBrush bb = new SolidBrush(Color.FromArgb(89,132,166));
			//g.FillRectangle(bb,rc);
			CrystalDrawing.LinearFillRoundRect(g,rc,HeaderColor1,HeaderColor2,5,LinearGradientMode.Vertical);
            if (img == null)
			{
                if (loadSucessed && imgTools != null)
					g.DrawImage(imgTools,rc);
			}
			else
				g.DrawImage(img,rc);
		}
	}

	public enum SysParam 
	{ 
		MaxSize = 61488, 
		MinSize = 61472, 
		Close = 61536, 
		Restore=61728, 

		Top = 61443, 
		Down = 61446, 
		Left = 61441, 
		Right = 61442, 

		TitleClick = 61458, 
		TitleDoubleClick = 61490, 

		IconMenu=61587, 

		TopLeft=61444, 
		TopRight=61445, 
		DownLeft=61447, 
		DownRight=61448, 

		ScroolX=61574, 
		ScroolY=61559 
	} 


	public enum NCHitTestResult:int
	{
		HTERROR       = -2,
		HTTRANSPARENT = -1,
		HTNOWHERE     = 0,
		HTCLIENT      = 1,
		HTCAPTION     = 2,
		HTSYSMENU     = 3,
		HTGROWBOX     = 4,
		HTSIZE        = HTGROWBOX,
		HTMENU        = 5,
		HTHSCROLL     = 6,
		HTVSCROLL     = 7,
		HTMINBUTTON   = 8,
		HTMAXBUTTON   = 9,
		HTLEFT        = 10,
		HTRIGHT       = 11,
		HTTOP         = 12,
		HTTOPLEFT     = 13,
		HTTOPRIGHT    = 14,
		HTBOTTOM      = 15,
		HTBOTTOMLEFT  = 16,
		HTBOTTOMRIGHT = 17,
		HTBORDER      = 18,
		HTREDUCE      = HTMINBUTTON,
		HTZOOM        = HTMAXBUTTON,
		HTSIZEFIRST   = HTLEFT,
		HTSIZELAST    = HTBOTTOMRIGHT,
		HTOBJECT      = 19,
		HTCLOSE       = 20,
		HTHELP        = 21
	};
}
