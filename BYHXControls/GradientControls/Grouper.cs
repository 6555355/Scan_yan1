/*
 [PLEASE DO NOT MODIFY THIS HEADER INFORMATION]---------------------
 Title: Grouper
 Description: A rounded groupbox with special painting features. 
 Date Created: December 17, 2005
 Author: Adam Smith
 Author Email: ibulwark@hotmail.com
 Websites: http://www.ebadgeman.com | http://www.codevendor.com
 
 Version History:
 1.0a - Beta Version - Release Date: December 17, 2005 
 -------------------------------------------------------------------
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace BYHXPrinterManager.GradientControls
{
	/// <summary>A special custom rounding GroupBox with many painting features.</summary>
	[ToolboxBitmap(typeof(Grouper), "CodeVendor.Controls.Grouper.bmp")]
	[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
	public class Grouper : System.Windows.Forms.GroupBox
	{
		#region Variables

		private System.ComponentModel.Container components = null;
		private int V_RoundCorners = 5;
//		private string V_GroupTitle = "The Grouper";
		private System.Drawing.Color V_BorderColor = SystemColors.AppWorkspace;// Color.Black;
        private Color V_TitleTextColor = SystemColors.ActiveCaption;
		private float V_BorderThickness = 1;
		private bool V_ShadowControl = false;
		private System.Drawing.Color V_BackgroundColor = Color.White;
		private System.Drawing.Color V_BackgroundGradientColor = Color.White;
		private GroupBoxGradientMode V_BackgroundGradientMode = GroupBoxGradientMode.None;
		private System.Drawing.Color V_ShadowColor = Color.DarkGray;
		private int V_ShadowThickness = 3;
		private System.Drawing.Image V_GroupImage = null;
		private bool V_PaintGroupBox = false;
//		private System.Drawing.Color V_BackColor = Color.Transparent;
		private Style _GradientColors = new Style();
		private Style _TitleGradientColors = new Style();
		private TitleStyles V_TitleStyle = TitleStyles.Default;
		#endregion

		#region Properties

//		/// <summary>This feature will paint the background color of the control.</summary>
//		[Category("Appearance"), Description("This feature will paint the background color of the control.")]
//		public override System.Drawing.Color BackColor{get{return V_BackColor;} set{V_BackColor=value; this.Refresh();}}

		/// <summary>This feature will paint the group title background to the specified color if PaintGroupBox is set to true.</summary>
		[Category("Appearance"), Description("This feature will paint the group title background to the specified color if PaintGroupBox is set to true.")]
		public Style TitileGradientColors
		{
			get { return _TitleGradientColors; }
			set
			{
				if (value != _TitleGradientColors)
				{
                    if (value == null)
                        value = new Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
                    _TitleGradientColors = value;
					this.Refresh();
				}
			}
		}

		/// <summary>This feature will paint the group title background to the CustomGroupBoxColor.</summary>
		[Category("Appearance"), Description("This feature will paint the group title background to the CustomGroupBoxColor.")]
		public bool PaintGroupBox{get{return V_PaintGroupBox;} set{V_PaintGroupBox=value; this.Refresh();}}

		/// <summary>This feature can add a 16 x 16 image to the group title bar.</summary>
		[Category("Appearance"), Description("This feature can add a 16 x 16 image to the group title bar.")]
		public System.Drawing.Image GroupImage{get{return V_GroupImage;} set{V_GroupImage=value; this.Refresh();}}

		/// <summary>This feature will change the control's shadow color.</summary>
		[Category("Appearance"), Description("This feature will change the control's shadow color.")]
		public System.Drawing.Color ShadowColor{get{return V_ShadowColor;} set{V_ShadowColor=value; this.Refresh();}}

		/// <summary>This feature will change the size of the shadow border.</summary>
		[Category("Appearance"), Description("This feature will change the size of the shadow border.")]
		public int ShadowThickness
		{
			get{return V_ShadowThickness;} 
			set
			{
				if(value>10)
				{
					V_ShadowThickness=10;
				}
				else
				{
					if(value<1){V_ShadowThickness=1;}
					else{V_ShadowThickness=value; }
				}

				this.Refresh();
			}
		}
		
		[Category("Appearance"), Description("This Colors to create a gradient background.")]
		public Style GradientColors
		{
			get { return _GradientColors; }
			set
			{
				if (value != _GradientColors)
				{
                    if(value == null)
                        value = new Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
					_GradientColors = value;
					this.Refresh();
				}
			}
		}
		/// <summary>This feature turns on background gradient painting.</summary>
		[Category("Appearance"), Description("This feature turns on background gradient painting.")]
		public GroupBoxGradientMode BackgroundGradientMode{get{return V_BackgroundGradientMode;} set{V_BackgroundGradientMode=value; this.Refresh();}}

		/// <summary>This feature will round the corners of the control.</summary>
		[Category("Appearance"), Description("This feature will round the corners of the control.")]
		public int RoundCorners
		{
			get{return V_RoundCorners;} 
			set
			{
				if(value>25)
				{
					V_RoundCorners=25;
				}
				else
				{
					if(value<1){V_RoundCorners=1;}
					else{V_RoundCorners=value; }
				}
				
				this.Refresh();
			}
		}

		/// <summary>This feature will add a group title to the control.</summary>
//		[Bindable(true),Localizable(true),Browsable(true),Category("Appearance")]
//		[ Description("This feature will add a group title to the control.")]
//		public string GroupTitle{get{return this.Text;} set{this.Text = V_GroupTitle =value; this.Refresh();}}

		/// <summary>This feature will allow you to change the color of the control's border.</summary>
		[Category("Appearance"), Description("This feature will allow you to change the color of the control's border.")]
        private System.Drawing.Color BorderColor { get { return V_BorderColor; } set { V_BorderColor = value; this.Refresh(); } }

        /// <summary>This feature will allow you to change the color of the control's border.</summary>
        [Category("Appearance"), Description("This feature will allow you to change the color of the control's TitleText.")]
        public System.Drawing.Color TitleTextColor { get { return V_TitleTextColor; } set { V_TitleTextColor = value; this.Refresh(); } }

		/// <summary>This feature will allow you to set the control's border size.</summary>
		[Category("Appearance"), Description("This feature will allow you to set the control's border size.")]
		public float BorderThickness
		{
			get{return V_BorderThickness;} 
			set
			{
				if(value>3)
				{
					V_BorderThickness=3;
				}
				else
				{
					if(value<1){V_BorderThickness=1;}
					else{V_BorderThickness=value;}
				}
				this.Refresh();
			}
		}

		/// <summary>This feature will allow you to turn on control shadowing.</summary>
		[Category("Appearance"), Description("This feature will allow you to turn on control shadowing.")]
		public bool ShadowControl{get{return V_ShadowControl;} set{V_ShadowControl=value; this.Refresh();}}

		/// <summary>TitleStyle</summary>
		[Category("Appearance"), Description("TitleStyle")]
		public TitleStyles TitleStyle
		{
			get{return V_TitleStyle;} 
			set
			{
				V_TitleStyle=value;
				if(V_TitleStyle == TitleStyles.Standard)
				{
//					foreach(Control ccc in this.Controls)
//					{
//						if(ccc is Label)
//							((Label)ccc).FlatStyle = FlatStyle.System;
//					}
					this.FlatStyle = FlatStyle.System;
				}
				else
				{
//					foreach(Control ccc in this.Controls)
//					{
//						if(ccc is Label)
//							((Label)ccc).FlatStyle = FlatStyle.Standard;
//					}
					this.FlatStyle = FlatStyle.Standard;
				}
				this.Refresh();
			}
		}
		#endregion

		#region Constructor
		
		/// <summary>This method will construct a new GroupBox control.</summary>
		public Grouper()
		{
			InitializeStyles();
			InitializeGroupBox();
		}

	
		#endregion

		#region DeConstructor

		/// <summary>This method will dispose of the GroupBox control.</summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing){if(components!=null){components.Dispose();}}
			base.Dispose(disposing);
		}


		#endregion

		#region Initialization

		/// <summary>This method will initialize the controls custom styles.</summary>
		private void InitializeStyles()
		{
			//Set the control styles----------------------------------
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			//--------------------------------------------------------
		}


		/// <summary>This method will initialize the GroupBox control.</summary>
		private void InitializeGroupBox()
		{
			components = new System.ComponentModel.Container();
			this.Resize+=new EventHandler(GroupBox_Resize);
//			this.DockPadding.All = 20;
			this.Name = "GroupBox";
			this.Size = new System.Drawing.Size(368, 288);
		}

	
		#endregion

		#region Protected Methods
		
		/// <summary>Overrides the OnPaint method to paint control.</summary>
		/// <param name="e">The paint event arguments.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			if(this.TitleStyle == TitleStyles.Standard)
			{
				base.OnPaint(e);
//				PaintBaseStyle(e.Graphics);
			}
			else
			{
				PaintBack(e.Graphics);
				PaintGroupText(e.Graphics);
			}		
		}

		#endregion

		#region Private Methods

		/// <summary>This method will paint the group title.</summary>
		/// <param name="g">The paint event graphics object.</param>
		private void PaintGroupText(System.Drawing.Graphics g)
		{
			//Check if string has something-------------
			if(this.Text==string.Empty){return;}
			//------------------------------------------

			//Set Graphics smoothing mode to Anit-Alias-- 
			g.SmoothingMode = SmoothingMode.AntiAlias;
			//-------------------------------------------

			//Declare Variables------------------
			SizeF StringSize = g.MeasureString(this.Text, this.Font);
			Size StringSize2 = StringSize.ToSize();
			if(this.GroupImage!=null){StringSize2.Width+=18;}
			int ArcWidth = this.RoundCorners;
			int ArcHeight = this.RoundCorners;
			int ArcX1 = 20;
			int ArcX2 = (StringSize2.Width+34) - (ArcWidth + 1);
			int ArcY1 = 0;
			int ArcY2 = 24 - (ArcHeight + 1);
			if(this.TitleStyle == TitleStyles.XPStyle)
			{
				ArcX1 = 0;
				ArcX2 = this.Width - (ArcWidth + 1);
				ArcY1 = 0;
				ArcY2 = 24 - (ArcHeight + 1);
			}

			System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
			System.Drawing.Brush BorderBrush = new SolidBrush(this.BorderColor);
			System.Drawing.Pen BorderPen = new Pen(BorderBrush, this.BorderThickness);
			System.Drawing.Drawing2D.LinearGradientBrush BackgroundGradientBrush = null;
			System.Drawing.Brush BackgroundBrush = null;//(this.PaintGroupBox) ? new LinearGradientBrush(this.CustomGroupBoxColor) : new SolidBrush(this.BackgroundColor);
			System.Drawing.SolidBrush TextColorBrush = new SolidBrush(this.TitleTextColor);
			System.Drawing.SolidBrush ShadowBrush = null;
			System.Drawing.Drawing2D.GraphicsPath ShadowPath  = null;
			//-----------------------------------

			//Check if shadow is needed----------
			if(this.ShadowControl)
			{
				ShadowBrush = new SolidBrush(this.ShadowColor);
				ShadowPath = new System.Drawing.Drawing2D.GraphicsPath();
				ShadowPath.AddArc(ArcX1+(this.ShadowThickness-1), ArcY1+(this.ShadowThickness-1), ArcWidth, ArcHeight, 180, GroupBoxConstants.SweepAngle); // Top Left
				ShadowPath.AddArc(ArcX2+(this.ShadowThickness-1), ArcY1+(this.ShadowThickness-1), ArcWidth, ArcHeight, 270, GroupBoxConstants.SweepAngle); //Top Right
				ShadowPath.AddArc(ArcX2+(this.ShadowThickness-1), ArcY2+(this.ShadowThickness-1), ArcWidth, ArcHeight, 360, GroupBoxConstants.SweepAngle); //Bottom Right
				ShadowPath.AddArc(ArcX1+(this.ShadowThickness-1), ArcY2+(this.ShadowThickness-1), ArcWidth, ArcHeight, 90, GroupBoxConstants.SweepAngle); //Bottom Left
				ShadowPath.CloseAllFigures();

				//Paint Rounded Rectangle------------
				g.FillPath(ShadowBrush, ShadowPath);
				//-----------------------------------
			}
			//-----------------------------------

			//Create Rounded Rectangle Path------
			path.AddArc(ArcX1, ArcY1, ArcWidth, ArcHeight, 180, GroupBoxConstants.SweepAngle); // Top Left
			path.AddArc(ArcX2, ArcY1, ArcWidth, ArcHeight, 270, GroupBoxConstants.SweepAngle); //Top Right
			path.AddArc(ArcX2, ArcY2, ArcWidth, ArcHeight, 360, GroupBoxConstants.SweepAngle); //Bottom Right
			path.AddArc(ArcX1, ArcY2, ArcWidth, ArcHeight, 90, GroupBoxConstants.SweepAngle); //Bottom Left
			path.CloseAllFigures(); 
			//-----------------------------------

			//Check if Gradient Mode is enabled--
			if(this.PaintGroupBox)
			{
				//Paint Rounded Rectangle------------
				BackgroundGradientBrush = new LinearGradientBrush(path.GetBounds(),this.TitileGradientColors.Color1,this.TitileGradientColors.Color2,(LinearGradientMode)this.BackgroundGradientMode);
				g.FillPath(BackgroundGradientBrush, path);
				//-----------------------------------
			}
			else
			{
				if(this.BackgroundGradientMode==GroupBoxGradientMode.None)
				{
					//Paint Rounded Rectangle------------
					BackgroundBrush = new SolidBrush(this.BackColor);
					g.FillPath(BackgroundBrush, path);
					//-----------------------------------
				}
				else
				{
					BackgroundGradientBrush = new LinearGradientBrush(new Rectangle(0,0,this.Width,this.Height), this.GradientColors.Color1, this.GradientColors.Color2, (LinearGradientMode)this.BackgroundGradientMode);
				
					//Paint Rounded Rectangle------------
					g.FillPath(BackgroundGradientBrush, path);
					//-----------------------------------
				}
			}
			//-----------------------------------

			//Paint Borded-----------------------
			g.DrawPath(BorderPen, path);
			//-----------------------------------

			//Paint Text-------------------------
			int CustomStringWidth = (this.GroupImage!=null) ? 44 : 28;
			g.DrawString(this.Text, this.Font, TextColorBrush, CustomStringWidth, 5);
			//-----------------------------------

			//Draw GroupImage if there is one----
			if(this.GroupImage!=null)
			{
				g.DrawImage(this.GroupImage, 28,4, 16, 16);
			}
			//-----------------------------------

			//Destroy Graphic Objects------------
			if(path!=null){path.Dispose();}
			if(BorderBrush!=null){BorderBrush.Dispose();}
			if(BorderPen!=null){BorderPen.Dispose();}
			if(BackgroundGradientBrush!=null){BackgroundGradientBrush.Dispose();}
			if(BackgroundBrush!=null){BackgroundBrush.Dispose();}
			if(TextColorBrush!=null){TextColorBrush .Dispose();}
			if(ShadowBrush!=null){ShadowBrush.Dispose();}
			if(ShadowPath!=null){ShadowPath.Dispose();}
			//-----------------------------------
		}

		
		/// <summary>This method will paint the control.</summary>
		/// <param name="g">The paint event graphics object.</param>
		private void PaintBack(System.Drawing.Graphics g)
		{
			//Set Graphics smoothing mode to Anit-Alias-- 
			g.SmoothingMode = SmoothingMode.AntiAlias;
			//-------------------------------------------

			//Declare Variables------------------
			int ArcWidth = this.RoundCorners * 2;
			int ArcHeight = this.RoundCorners * 2;
			int ArcX1 = 0;
			int ArcX2 = (this.ShadowControl) ? (this.Width - (ArcWidth + 1))-this.ShadowThickness : this.Width - (ArcWidth + 1);
			int ArcY1 = 10;
			int ArcY2 = (this.ShadowControl) ? (this.Height - (ArcHeight + 1))-this.ShadowThickness : this.Height - (ArcHeight + 1);
			System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
			System.Drawing.Brush BorderBrush = new SolidBrush(this.BorderColor);
			System.Drawing.Pen BorderPen = new Pen(BorderBrush, this.BorderThickness);
			System.Drawing.Drawing2D.LinearGradientBrush BackgroundGradientBrush = null;
			System.Drawing.Brush BackgroundBrush = new SolidBrush(this.BackColor);
			System.Drawing.SolidBrush ShadowBrush = null;
			System.Drawing.Drawing2D.GraphicsPath ShadowPath  = null;
			//-----------------------------------

			//Check if shadow is needed----------
			if(this.ShadowControl)
			{
				ShadowBrush = new SolidBrush(this.ShadowColor);
				ShadowPath = new System.Drawing.Drawing2D.GraphicsPath();
				ShadowPath.AddArc(ArcX1+this.ShadowThickness, ArcY1+this.ShadowThickness, ArcWidth, ArcHeight, 180, GroupBoxConstants.SweepAngle); // Top Left
				ShadowPath.AddArc(ArcX2+this.ShadowThickness, ArcY1+this.ShadowThickness, ArcWidth, ArcHeight, 270, GroupBoxConstants.SweepAngle); //Top Right
				ShadowPath.AddArc(ArcX2+this.ShadowThickness, ArcY2+this.ShadowThickness, ArcWidth, ArcHeight, 360, GroupBoxConstants.SweepAngle); //Bottom Right
				ShadowPath.AddArc(ArcX1+this.ShadowThickness, ArcY2+this.ShadowThickness, ArcWidth, ArcHeight, 90, GroupBoxConstants.SweepAngle); //Bottom Left
				ShadowPath.CloseAllFigures();

				//Paint Rounded Rectangle------------
				g.FillPath(ShadowBrush, ShadowPath);
				//-----------------------------------
			}
			//-----------------------------------

			//Create Rounded Rectangle Path------
			path.AddArc(ArcX1, ArcY1, ArcWidth, ArcHeight, 180, GroupBoxConstants.SweepAngle); // Top Left
			path.AddArc(ArcX2, ArcY1, ArcWidth, ArcHeight, 270, GroupBoxConstants.SweepAngle); //Top Right
			path.AddArc(ArcX2, ArcY2, ArcWidth, ArcHeight, 360, GroupBoxConstants.SweepAngle); //Bottom Right
			path.AddArc(ArcX1, ArcY2, ArcWidth, ArcHeight, 90, GroupBoxConstants.SweepAngle); //Bottom Left
			path.CloseAllFigures(); 
			//-----------------------------------

			//Check if Gradient Mode is enabled--
			if(this.BackgroundGradientMode==GroupBoxGradientMode.None)
			{
				//Paint Rounded Rectangle------------
				g.FillPath(BackgroundBrush, path);
				//-----------------------------------
			}
			else
			{
				BackgroundGradientBrush = new LinearGradientBrush(new Rectangle(0,0,this.Width,this.Height), this.GradientColors.Color1, this.GradientColors.Color2, (LinearGradientMode)this.BackgroundGradientMode);
				
				//Paint Rounded Rectangle------------
				g.FillPath(BackgroundGradientBrush, path);
				//-----------------------------------
			}
			//-----------------------------------

			//Paint Borded-----------------------
			g.DrawPath(BorderPen, path);
			//-----------------------------------

			//Destroy Graphic Objects------------
			if(path!=null){path.Dispose();}
			if(BorderBrush!=null){BorderBrush.Dispose();}
			if(BorderPen!=null){BorderPen.Dispose();}
			if(BackgroundGradientBrush!=null){BackgroundGradientBrush.Dispose();}
			if(BackgroundBrush!=null){BackgroundBrush.Dispose();}
			if(ShadowBrush!=null){ShadowBrush.Dispose();}
			if(ShadowPath!=null){ShadowPath.Dispose();}
			//-----------------------------------
		}

		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Grouper));
			// 
			// Grouper
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
//			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
//			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
//			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "Grouper";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));

		}

	
		/// <summary>This method fires when the GroupBox resize event occurs.</summary>
		/// <param name="sender">The object the sent the event.</param>
		/// <param name="e">The event arguments.</param>
		private void GroupBox_Resize(object sender, EventArgs e)
		{
			this.Refresh();
		}

		public void CloneGrouperStyle(Grouper v_SampleGrouper)
		{
			this.TitileGradientColors = v_SampleGrouper.TitileGradientColors;
			this.TitleStyle = v_SampleGrouper.TitleStyle;
			this.TitleTextColor = v_SampleGrouper.TitleTextColor;
			this.ShadowThickness = v_SampleGrouper.ShadowThickness;
			this.ShadowControl = v_SampleGrouper.ShadowControl;
			this.ShadowColor = v_SampleGrouper.ShadowColor;
			this.RoundCorners = v_SampleGrouper.RoundCorners;
			this.PaintGroupBox = v_SampleGrouper.PaintGroupBox;
			this.GroupImage = v_SampleGrouper.GroupImage;
			this.GradientColors = v_SampleGrouper.GradientColors;
			this.ForeColor = v_SampleGrouper.ForeColor;
//			this.Font = v_SampleGrouper.Font;
			this.FlatStyle = v_SampleGrouper.FlatStyle;
			this.BorderThickness = v_SampleGrouper.BorderThickness;
			this.BackgroundImage = v_SampleGrouper.BackgroundImage;
			this.BackgroundGradientMode = v_SampleGrouper.BackgroundGradientMode;
			this.BackColor = v_SampleGrouper.BackColor;
			this.Refresh();
		}
		#endregion
	}

	#region Enumerations

	/// <summary>A special gradient enumeration.</summary>
	public enum GroupBoxGradientMode
	{
		/// <summary>Specifies no gradient mode.</summary>
		None = 4,

		/// <summary>Specifies a gradient from upper right to lower left.</summary>
		BackwardDiagonal = 3 ,

		/// <summary>Specifies a gradient from upper left to lower right.</summary>
		ForwardDiagonal = 2,

		/// <summary>Specifies a gradient from left to right.</summary>
		Horizontal = 0,

		/// <summary>Specifies a gradient from top to bottom.</summary>
		Vertical = 1
	}
		
	/// <summary>
	/// TitleStyles
	/// </summary>
	public enum TitleStyles
	{
		/// <summary>
		/// Default
		/// </summary>
		Default = 0,
		/// <summary>
		/// XPStyle
		/// </summary>
		XPStyle = 1,
		/// <summary>
		/// System
		/// </summary>
		Standard = 2
	}
	#endregion
}
