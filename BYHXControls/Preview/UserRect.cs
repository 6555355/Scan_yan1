using System;
//using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace BYHXPrinterManager.Preview
{
	public class UserRect 
	{        
		private PictureBox mPictureBox;
		public RectangleF rect;
		public bool allowDeformingDuringMovement=false ;
		private bool mIsClick=false;
		private bool mMove=false;        
		private int oldX;
		private int oldY;
		public int sizeNodeRect= 5;
		private Bitmap mBmp=null;
		private PosSizableRect nodeSelected = PosSizableRect.None;
		private int angle = 30;
		private bool m_Visble = false;
		public delegate void OnClipRecChangedEventHandler(object sender, OnClipRecChangedEventArgs e);
		public event OnClipRecChangedEventHandler OnClipRecChanged;
		private Point pCur = new Point(0);
		public bool clipAll = false;
		private RectangleF rectbackup;

		public bool Visble
		{
			get { return m_Visble; }
			set 
			{
				m_Visble = value;
				if (value && this.OnClipRecChanged != null)
					this.OnClipRecChanged(this, new OnClipRecChangedEventArgs(this.rect));
			}
		}

		public SizeF Size
		{
			get { return this.rect.Size; }
			set { this.rect.Size = value; }
		}
		private enum PosSizableRect
		{            
			UpMiddle,
			LeftMiddle,
			LeftBottom,
			LeftUp,
			RightUp,
			RightMiddle,
			RightBottom,
			BottomMiddle,
			None

		};

		public UserRect(Rectangle r)
		{
			rect = r;
			mIsClick = false;
		}

		public void Draw(Graphics g)
		{
			g.FillRectangle(this.GetGDIBrush(), rect);
			g.DrawRectangle(new Pen(Color.Red),rect.X,rect.Y,rect.Width,rect.Height);
            
			foreach (PosSizableRect pos in Enum.GetValues(typeof(PosSizableRect)))
			{
				RectangleF rect1 = GetRect(pos);
				g.DrawRectangle(new Pen(Color.Red),rect1.X,rect1.Y,rect1.Width,rect1.Height);
			}                       
		}

		public void SetBitmapFile(string filename)
		{
            if(File.Exists(filename))
			    this.mBmp = new Bitmap(filename);
		}

		public void SetBitmap(Bitmap bmp)
		{
			this.mBmp = bmp;
		}

		public void SetPictureBox(PictureBox p)
		{
			this.mPictureBox = p;
			mPictureBox.MouseDown +=new MouseEventHandler(mPictureBox_MouseDown);
			mPictureBox.MouseUp += new MouseEventHandler(mPictureBox_MouseUp);
			mPictureBox.MouseMove += new MouseEventHandler(mPictureBox_MouseMove);            
			mPictureBox.Paint += new PaintEventHandler(mPictureBox_Paint);
			mPictureBox.DoubleClick += new EventHandler(mPictureBox_DoubleClick);
		}

		private void mPictureBox_Paint(object sender, PaintEventArgs e)
		{
			if (!this.m_Visble)
				return;
			try
			{
				Draw(e.Graphics);
			}
			catch (Exception exp)
			{
				System.Console.WriteLine(exp.Message);
			}
            
		}

		private void mPictureBox_MouseDown(object sender, MouseEventArgs e)
		{

			nodeSelected = PosSizableRect.None;
			nodeSelected = GetNodeSelectable(new Point(e.X,e.Y));

			if (rect.Contains(new Point(e.X, e.Y)) || nodeSelected != PosSizableRect.None)
			{
				mIsClick = true;
				ChangeCursor(new Point(e.X,e.Y));
			}

			oldX = e.X;
			oldY = e.Y;
		}

		private void mPictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			mIsClick = false;
			if(mMove)
			{
				mMove = false;
				if (this.OnClipRecChanged != null)
					this.OnClipRecChanged(this, new OnClipRecChangedEventArgs(rect));
			}
		}

		private void mPictureBox_MouseMove(object sender, MouseEventArgs e)
		{
			pCur = new Point(e.X,e.Y);

			if (mIsClick == false)
			{
				ChangeCursor(new Point(e.X,e.Y));
				return;
			}
			else
			{
				mMove = true;
			}
			
			RectangleF backupRect = rect;

			switch (nodeSelected)
			{
				case PosSizableRect.LeftUp:
					rect.X += e.X - oldX;
					rect.Width -= e.X - oldX;                    
					rect.Y += e.Y - oldY;
					rect.Height -= e.Y - oldY;
					break;
				case PosSizableRect.LeftMiddle:
					rect.X += e.X - oldX;
					rect.Width -= e.X - oldX;
					break;
				case PosSizableRect.LeftBottom:
					rect.Width -= e.X - oldX;
					rect.X += e.X - oldX;
					rect.Height += e.Y - oldY;
					break;
				case PosSizableRect.BottomMiddle:
					rect.Height += e.Y - oldY;
					break;
				case PosSizableRect.RightUp:
					rect.Width += e.X - oldX;
					rect.Y += e.Y - oldY;
					rect.Height -= e.Y - oldY;
					break;
				case PosSizableRect.RightBottom:
					rect.Width +=  e.X - oldX;
					rect.Height += e.Y - oldY;
					break;
				case PosSizableRect.RightMiddle:
					rect.Width += e.X - oldX;
					break;

				case PosSizableRect.UpMiddle:
					rect.Y += e.Y - oldY;
					rect.Height -= e.Y - oldY;
					break;

				default:
					if (mMove)
					{
						rect.X = rect.X + e.X - oldX;
						rect.Y = rect.Y + e.Y - oldY;
					}
					break;
			}

			oldX = e.X;
			oldY = e.Y;

			if(!TestIfRectInsideArea())
				rect = backupRect;
			mPictureBox.Invalidate();
			clipAll =false;
		}

		private bool TestIfRectInsideArea()
		{
			// Test if rectangle still inside the area.
			if (rect.X < 0) 
				return false;//rect.X = 0;
			if (rect.Y < 0) 
				return false;//rect.Y = 0;
			if (rect.Width <= 0)
				return false;//rect.Width = 1;
			if (rect.Height <= 0) 
				return false;//rect.Height = 1;

			if (rect.X + rect.Width > mPictureBox.ClientRectangle.Width)
			{
				//				rect.X = mPictureBox.ClientRectangle.Width - rect.Width;
				//			    rect.Width = mPictureBox.Width - rect.X - sizeNodeRect; // -1 to be still show 
				//			    if (allowDeformingDuringMovement == false)
				//			    {
				//			        mIsClick = false;
				//			    }
				return false;
			}
			if (rect.Y + rect.Height > mPictureBox.ClientRectangle.Height)
			{
				//				rect.Y = mPictureBox.ClientRectangle.Height - rect.Height;
				//			    rect.Height = mPictureBox.Height - rect.Y - sizeNodeRect;// -1 to be still show 
				//			    if (allowDeformingDuringMovement == false)
				//			    {
				//			        mIsClick = false;
				//			    }
				return false;
			}
			return true;
		}        

		private RectangleF CreateRectSizableNode(float x, float y)
		{
			return new RectangleF(x - sizeNodeRect / 2, y - sizeNodeRect / 2, sizeNodeRect, sizeNodeRect);   
		}

		private RectangleF GetRect(PosSizableRect p)
		{
			switch (p)
			{
				case PosSizableRect.LeftUp:
					return CreateRectSizableNode(rect.X, rect.Y);
                 
				case PosSizableRect.LeftMiddle:
					return CreateRectSizableNode(rect.X, rect.Y + +rect.Height / 2);                    

				case PosSizableRect.LeftBottom:
					return CreateRectSizableNode(rect.X, rect.Y +rect.Height);                                   

				case PosSizableRect.BottomMiddle:
					return CreateRectSizableNode(rect.X  + rect.Width / 2,rect.Y + rect.Height);

				case PosSizableRect.RightUp:
					return CreateRectSizableNode(rect.X + rect.Width,rect.Y );

				case PosSizableRect.RightBottom:
					return CreateRectSizableNode(rect.X  + rect.Width,rect.Y  + rect.Height);

				case PosSizableRect.RightMiddle:
					return CreateRectSizableNode(rect.X  + rect.Width, rect.Y  + rect.Height / 2);

				case PosSizableRect.UpMiddle:
					return CreateRectSizableNode(rect.X + rect.Width/2, rect.Y);
				default :
					return new Rectangle();
			}
		}

		private PosSizableRect GetNodeSelectable(Point p)
		{
			foreach (PosSizableRect r in Enum.GetValues(typeof(PosSizableRect)))
			{
				if (GetRect(r).Contains(p))
				{
					return r;                    
				}
			}
			return PosSizableRect.None;
		}

		private void ChangeCursor(Point p)
		{
			mPictureBox.Cursor = GetCursor(GetNodeSelectable(p));
		}

		/// <summary>
		/// Get cursor for the handle
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		private Cursor GetCursor(PosSizableRect p)
		{
			switch (p)
			{
				case PosSizableRect.LeftUp:
					return Cursors.SizeNWSE;               

				case PosSizableRect.LeftMiddle:
					return Cursors.SizeWE;

				case PosSizableRect.LeftBottom:
					return Cursors.SizeNESW;

				case PosSizableRect.BottomMiddle:
					return Cursors.SizeNS;

				case PosSizableRect.RightUp:
					return Cursors.SizeNESW;

				case PosSizableRect.RightBottom:
					return Cursors.SizeNWSE;

				case PosSizableRect.RightMiddle:
					return Cursors.SizeWE;

				case PosSizableRect.UpMiddle:
					return Cursors.SizeNS;
				default:
					return Cursors.Default;
			}
		}

		protected System.Drawing.Color GetForeColor()
		{
			return System.Drawing.Color.FromArgb(128, 128, 128, 128);
		}

		protected System.Drawing.Color GetBackColor()
		{
			return System.Drawing.Color.FromArgb(128, 255, 255, 255);
		}

		protected System.Drawing.Drawing2D.HatchBrush GetGDIBrush()
		{
			return new System.Drawing.Drawing2D.HatchBrush(
				System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal,
				GetForeColor(),
				GetBackColor());
		}

		public bool SetClipRectangle(RectangleF rec)
		{
			RectangleF back =this.rect;
			this.rect = rec;
			bool ret =TestIfRectInsideArea();
			if(!ret)
				this.rect = back;
			clipAll =false;
			return ret;
		}

		private void mPictureBox_DoubleClick(object sender, EventArgs e)
		{
			if(this.Visble && this.rect.Contains(pCur))
			{
				clipAll = !clipAll;
				if(clipAll)
				{
					rectbackup = this.rect;
					this.rect = new RectangleF(0,0,this.mPictureBox.Width-sizeNodeRect,this.mPictureBox.Height-sizeNodeRect);
				}
				else
				{
					this.rect = rectbackup;
				}
				if (this.OnClipRecChanged != null)
					this.OnClipRecChanged(this, new OnClipRecChangedEventArgs(rect));
				this.mPictureBox.Refresh();
			}
		}
	}

	public class OnClipRecChangedEventArgs : EventArgs
	{
		private RectangleF info;

		public OnClipRecChangedEventArgs(RectangleF info)
		{
			this.info = info;
		}

		// The help info of the contextmenu item
		public RectangleF ClipRectangle { get { return info; } }
	}
}
