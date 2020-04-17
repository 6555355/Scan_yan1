/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.IO;
using PrinterStubC.Common;

namespace BYHXPrinterManager.Preview
{
	/// <summary>
	/// Summary description for PrintingPreview.
	/// </summary>
	public class PrintingPreview : GradientControls.CrystalPanel //System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private IPrinterChange m_iPrinterChange;
		private bool m_bPrintingPreview = true;
		private const int c_Margin = 8;
		private int   m_Percentage = 0;
		private SizeF m_JobSize = SizeF.Empty;
		private BYHXPrinterManager.Preview.MyPictureBox m_PictureBoxImage;
        private ImageList imageList1;
        private IContainer components;
        private RotateFlipType rotate = RotateFlipType.RotateNoneFlipNone;
	
        public MyPictureBox ImagePictureBox
        {
            get { return m_PictureBoxImage; }
            set { m_PictureBoxImage = value; }
        }
        public RotateFlipType Rotate
        {
            get { return rotate; }
            set { rotate = value; }
        }

		public PrintingPreview()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PrintingPreview));
			this.m_PictureBoxImage = new BYHXPrinterManager.Preview.MyPictureBox();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// m_PictureBoxImage
			// 
			this.m_PictureBoxImage.AccessibleDescription = resources.GetString("m_PictureBoxImage.AccessibleDescription");
			this.m_PictureBoxImage.AccessibleName = resources.GetString("m_PictureBoxImage.AccessibleName");
			this.m_PictureBoxImage.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_PictureBoxImage.Anchor")));
			this.m_PictureBoxImage.BackColor = System.Drawing.Color.White;
			this.m_PictureBoxImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_PictureBoxImage.BackgroundImage")));
			this.m_PictureBoxImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_PictureBoxImage.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_PictureBoxImage.Dock")));
			this.m_PictureBoxImage.Enabled = ((bool)(resources.GetObject("m_PictureBoxImage.Enabled")));
			this.m_PictureBoxImage.Font = ((System.Drawing.Font)(resources.GetObject("m_PictureBoxImage.Font")));
			this.m_PictureBoxImage.Image = ((System.Drawing.Image)(resources.GetObject("m_PictureBoxImage.Image")));
			this.m_PictureBoxImage.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_PictureBoxImage.ImeMode")));
			this.m_PictureBoxImage.Location = ((System.Drawing.Point)(resources.GetObject("m_PictureBoxImage.Location")));
			this.m_PictureBoxImage.Name = "m_PictureBoxImage";
			this.m_PictureBoxImage.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_PictureBoxImage.RightToLeft")));
			this.m_PictureBoxImage.Size = ((System.Drawing.Size)(resources.GetObject("m_PictureBoxImage.Size")));
			this.m_PictureBoxImage.SizeMode = ((System.Windows.Forms.PictureBoxSizeMode)(resources.GetObject("m_PictureBoxImage.SizeMode")));
			this.m_PictureBoxImage.TabIndex = ((int)(resources.GetObject("m_PictureBoxImage.TabIndex")));
			this.m_PictureBoxImage.TabStop = false;
			this.m_PictureBoxImage.Text = resources.GetString("m_PictureBoxImage.Text");
			this.m_PictureBoxImage.Visible = ((bool)(resources.GetObject("m_PictureBoxImage.Visible")));
			this.m_PictureBoxImage.Paint += new System.Windows.Forms.PaintEventHandler(this.m_PictureBoxImage_Paint);
			this.m_PictureBoxImage.DoubleClick += new System.EventHandler(this.m_PictureBoxImage_DoubleClick);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList1.ImageSize = ((System.Drawing.Size)(resources.GetObject("imageList1.ImageSize")));
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// PrintingPreview
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackColor = System.Drawing.Color.White;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Controls.Add(this.m_PictureBoxImage);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "PrintingPreview";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.SizeChanged += new System.EventHandler(this.PrintingPreview_SizeChanged);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PrintingPreview_KeyUp);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrintingPreview_KeyDown);
			this.ResumeLayout(false);

		}
		#endregion

		public void UpdatePreviewImage(Image preview)
		{
            if (m_PictureBoxImage.Image!=null)
                m_PictureBoxImage.Image.Dispose();
            if (preview != null)
            {
                //Image temp = (Image) preview.Clone();
                preview.RotateFlip(this.Rotate);
                m_PictureBoxImage.Image = preview;
            }
            else
                m_PictureBoxImage.Image = null;
			Refresh();
		}

		public void UpdateJobSizeInfo(SizeF size)
		{
				m_JobSize = size;

				//Invalidate();
				CalculatePreviewRectangle();
		}
		public void UpdatePercentage(int percent)
		{
			m_Percentage = percent;
			Refresh();
		}
		public void SetPrintingPreview( bool bPre)
		{
			m_bPrintingPreview = bPre;
		}
		public void SetPrinterChange(IPrinterChange ic)
		{
			m_iPrinterChange = ic;
		}

		private void CalculatePreviewRectangle()
		{
			//if (this.m_PictureBoxImage.Image != null && this.m_PictureBoxImage.Image.Width != 0 && this.m_PictureBoxImage.Image.Height!=0)
#if LIYUUSB
            if (!m_JobSize.IsEmpty)
#endif
            {
                float width;
                float height;
				int x = c_Margin;
				int y = c_Margin;
                if (this.Rotate == RotateFlipType.Rotate270FlipXY ||
                this.Rotate == RotateFlipType.Rotate90FlipNone ||
                this.Rotate == RotateFlipType.Rotate270FlipNone ||
                this.Rotate == RotateFlipType.Rotate90FlipXY ||
                this.Rotate == RotateFlipType.Rotate90FlipX ||
                this.Rotate == RotateFlipType.Rotate270FlipY ||
                this.Rotate == RotateFlipType.Rotate90FlipY ||
                this.Rotate == RotateFlipType.Rotate270FlipX)
                {
                    width = m_JobSize.Height;
                    height = m_JobSize.Width;
                }
                else
                {
                    width = m_JobSize.Width;
                    height = m_JobSize.Height;
                }
				int ClientWidth = this.ClientSize.Width - c_Margin * 2;
				int ClientHeight = this.ClientSize.Height - c_Margin * 2;
				int temp = 0;
                if (width * ClientHeight > ClientWidth * height)
                {
                    temp = ClientHeight;
                    ClientHeight = (int)((float)ClientWidth * height / width);
                    y += (temp - ClientHeight) / 2;
                }
                else
                {
                    temp = ClientWidth;
                    ClientWidth = (int)((float)ClientHeight * width / height);
                    x += (temp - ClientWidth) / 2;
                }
				Rectangle rect = new Rectangle(x,y,ClientWidth,ClientHeight);
				if(m_PictureBoxImage.Location != rect.Location)
					m_PictureBoxImage.Location = rect.Location;
				if(m_PictureBoxImage.Size != rect.Size)
					m_PictureBoxImage.Size = rect.Size;
			}
#if LIYUUSB
            else
            {
                this.m_PictureBoxImage.Location = new Point(c_Margin, c_Margin);
                this.m_PictureBoxImage.Size = new Size(this.ClientSize.Width - c_Margin * 2, this.ClientSize.Height - c_Margin * 2);
            }
#endif
		}

		private void PrintingPreview_SizeChanged(object sender, System.EventArgs e)
		{
			CalculatePreviewRectangle();
		}

		private void m_PictureBoxImage_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			if (this.m_PictureBoxImage.Image == null)
			{
				string noPreviewInfo = string.Empty;
				if(m_JobSize.Width == 0 || m_JobSize.Height == 0)
				{
#if !LIYUUSB
					noPreviewInfo = ResString.GetNoPreviewImage();
#elif(LIYUUSB&&YASLAN )
					Assembly myAssembly = Assembly.GetExecutingAssembly();
					string[] names = myAssembly.GetManifestResourceNames();
                    string logopath = Path.Combine(Application.StartupPath,"Setup\\LOGO.png");
                    if (File.Exists(logopath))
                    {
                        Stream myStream1 = new FileStream(logopath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);// myAssembly.GetManifestResourceStream("BYHXPrinterManager.Preview.yaslanLOGO.png");
                        if (myStream1 != null)
                            m_PictureBoxImage.Image = new Bitmap(myStream1);// this.imageList1.Images[0];
                    }
#else
                    m_PictureBoxImage.Image = this.imageList1.Images[0];
#endif
				}
				else
				{
					noPreviewInfo = ResString.GetCreatingPreviewImage();
#if !LIYUUSB
				}
#endif
				if (!m_bPrintingPreview)
				{
					Rectangle layoutRect = this.m_PictureBoxImage.ClientRectangle;
					StringFormat sf = new StringFormat();
					sf.Alignment = StringAlignment.Center;
					sf.LineAlignment = StringAlignment.Center;
					e.Graphics.DrawString(noPreviewInfo, this.Font, SystemBrushes.WindowText, layoutRect, sf);
				}
#if LIYUUSB
				}
#endif
				return;
			}
			if (!m_bPrintingPreview)
				return;
			Rectangle pictureBoxRect = this.m_PictureBoxImage.ClientRectangle;
			Size bmpSize = m_PictureBoxImage.Image.Size;
			Rectangle prtRect;
			//			if (mbRotatePreview)
			//			{
			//				int height = bmpSize.Height*miPrintPercent/99;
			//				prtRect = new Rectangle(pictureBoxRect.Left+(pictureBoxRect.Width - bmpSize.Width)/2,
			//					pictureBoxRect.Bottom -(pictureBoxRect.Height - bmpSize.Height)/2-height-1,
			//					bmpSize.Width,height);
			//			}
			//			else
		    if (UIFunctionOnOff.PreviewRotate180)
            {
                prtRect = new Rectangle(pictureBoxRect.Left,//+(pictureBoxRect.Width - bmpSize.Width)/2,
                    pictureBoxRect.Height * (100 - m_Percentage) / 100,//+(pictureBoxRect.Height - bmpSize.Height)/2,
                    //bmpSize.Width,bmpSize.Height*m_Percentage/99);
                    pictureBoxRect.Width,
                    pictureBoxRect.Height * m_Percentage / 100);
            }
            else
            {
                prtRect = new Rectangle(pictureBoxRect.Left, //+(pictureBoxRect.Width - bmpSize.Width)/2,
                    pictureBoxRect.Top, //+(pictureBoxRect.Height - bmpSize.Height)/2,
                    //bmpSize.Width,bmpSize.Height*m_Percentage/99);
                    pictureBoxRect.Width,
                    pictureBoxRect.Height * m_Percentage / 100);
            }

            //			if (this.menuItemView_RedLine.Checked)
			//			{
			//				Pen pen = new Pen(Color.Red);
			//				g.DrawRectangle(pen,prtRect);
			//			}
			//			else if (this.menuItemView_SolidBrush.Checked)	
			//			{
			//				SolidBrush brush = new SolidBrush(Color.FromArgb(180, Color.White));
			//				g.FillRectangle(brush,prtRect);
			//			}
			//			else
			{
				HatchBrush brush = new HatchBrush(HatchStyle.ForwardDiagonal, Color.Purple, Color.FromArgb(32, Color.White));
				g.FillRectangle(brush,prtRect);
			}					
		}
		private void PrintingPreview_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			m_iPrinterChange.NotifyUIKeyDownAndUp(e.KeyData,true);
		}
		private void PrintingPreview_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			m_iPrinterChange.NotifyUIKeyDownAndUp(e.KeyData,false);
		}
		private void m_PictureBoxImage_DoubleClick(object sender, System.EventArgs e)
		{
			m_iPrinterChange.OnSwitchPreview();
		}
	}
}
