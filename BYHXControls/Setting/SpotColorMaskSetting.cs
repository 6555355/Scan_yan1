using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
	public class SpotColorMaskSetting : BYHXPrinterManager.Setting.BYHXUserControl
	{
		private BYHXPrinterManager.GradientControls.Grouper grouper1;
		private System.Windows.Forms.Panel groupBox1;
		private System.Windows.Forms.CheckedListBox m_clbimage1;
		private System.Windows.Forms.CheckedListBox m_clbimage1Opt;
		private System.Windows.Forms.NumericUpDown m_numAllpercent1;
		private System.Windows.Forms.RadioButton m_cbkSpotColor1Image;
		private System.Windows.Forms.RadioButton m_cbkSpotColor1All;
		private System.Windows.Forms.RadioButton m_cbkSpotColor1Rip;
		private System.Windows.Forms.CheckedListBox m_clbimage2Opt;
		private System.ComponentModel.IContainer components = null;

		public SpotColorMaskSetting()
		{
			// This call is required by the Windows Form Designer.
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
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style2 = new BYHXPrinterManager.Style();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpotColorMaskSetting));
            this.grouper1 = new BYHXPrinterManager.GradientControls.Grouper();
            this.groupBox1 = new System.Windows.Forms.Panel();
            this.m_clbimage1 = new System.Windows.Forms.CheckedListBox();
            this.m_clbimage1Opt = new System.Windows.Forms.CheckedListBox();
            this.m_clbimage2Opt = new System.Windows.Forms.CheckedListBox();
            this.m_cbkSpotColor1Image = new System.Windows.Forms.RadioButton();
            this.m_cbkSpotColor1Rip = new System.Windows.Forms.RadioButton();
            this.m_numAllpercent1 = new System.Windows.Forms.NumericUpDown();
            this.m_cbkSpotColor1All = new System.Windows.Forms.RadioButton();
            this.grouper1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numAllpercent1)).BeginInit();
            this.SuspendLayout();
            // 
            // grouper1
            // 
            this.grouper1.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.groupBox1);
            this.grouper1.Controls.Add(this.m_cbkSpotColor1Image);
            this.grouper1.Controls.Add(this.m_cbkSpotColor1Rip);
            this.grouper1.Controls.Add(this.m_numAllpercent1);
            this.grouper1.Controls.Add(this.m_cbkSpotColor1All);
            style1.Color1 = System.Drawing.Color.LightBlue;
            style1.Color2 = System.Drawing.Color.SteelBlue;
            this.grouper1.GradientColors = style1;
            this.grouper1.GroupImage = null;
            resources.ApplyResources(this.grouper1, "grouper1");
            this.grouper1.Name = "grouper1";
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 10;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.TabStop = false;
            style2.Color1 = System.Drawing.Color.LightBlue;
            style2.Color2 = System.Drawing.Color.SteelBlue;
            this.grouper1.TitileGradientColors = style2;
            this.grouper1.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouper1.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.groupBox1.Controls.Add(this.m_clbimage1);
            this.groupBox1.Controls.Add(this.m_clbimage1Opt);
            this.groupBox1.Controls.Add(this.m_clbimage2Opt);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            // 
            // m_clbimage1
            // 
            this.m_clbimage1.CheckOnClick = true;
            resources.ApplyResources(this.m_clbimage1, "m_clbimage1");
            this.m_clbimage1.Name = "m_clbimage1";
            this.m_clbimage1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.m_clbimage1_ItemCheck);
            // 
            // m_clbimage1Opt
            // 
            this.m_clbimage1Opt.CheckOnClick = true;
            resources.ApplyResources(this.m_clbimage1Opt, "m_clbimage1Opt");
            this.m_clbimage1Opt.Name = "m_clbimage1Opt";
            // 
            // m_clbimage2Opt
            // 
            this.m_clbimage2Opt.CheckOnClick = true;
            resources.ApplyResources(this.m_clbimage2Opt, "m_clbimage2Opt");
            this.m_clbimage2Opt.Name = "m_clbimage2Opt";
            // 
            // m_cbkSpotColor1Image
            // 
            this.m_cbkSpotColor1Image.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_cbkSpotColor1Image, "m_cbkSpotColor1Image");
            this.m_cbkSpotColor1Image.Name = "m_cbkSpotColor1Image";
            this.m_cbkSpotColor1Image.UseVisualStyleBackColor = false;
            this.m_cbkSpotColor1Image.CheckedChanged += new System.EventHandler(this.m_cbkSpotColor1Image_CheckedChanged);
            // 
            // m_cbkSpotColor1Rip
            // 
            this.m_cbkSpotColor1Rip.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_cbkSpotColor1Rip, "m_cbkSpotColor1Rip");
            this.m_cbkSpotColor1Rip.Name = "m_cbkSpotColor1Rip";
            this.m_cbkSpotColor1Rip.UseVisualStyleBackColor = false;
            this.m_cbkSpotColor1Rip.CheckedChanged += new System.EventHandler(this.m_cbkSpotColor1All_CheckedChanged);
            // 
            // m_numAllpercent1
            // 
            resources.ApplyResources(this.m_numAllpercent1, "m_numAllpercent1");
            this.m_numAllpercent1.Name = "m_numAllpercent1";
            // 
            // m_cbkSpotColor1All
            // 
            this.m_cbkSpotColor1All.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_cbkSpotColor1All, "m_cbkSpotColor1All");
            this.m_cbkSpotColor1All.Name = "m_cbkSpotColor1All";
            this.m_cbkSpotColor1All.UseVisualStyleBackColor = false;
            this.m_cbkSpotColor1All.CheckedChanged += new System.EventHandler(this.m_cbkSpotColor1All_CheckedChanged);
            // 
            // SpotColorMaskSetting
            // 
            this.Controls.Add(this.grouper1);
            this.Name = "SpotColorMaskSetting";
            resources.ApplyResources(this, "$this");
            this.grouper1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_numAllpercent1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private const int OPTIONCOUNT = 2;
		private const int OPTIONINDEX = 4; // 操作标志位存放开始位置

		public string Title
		{
			set
			{
				this.grouper1.Text = value;
			}
		}

		public ushort SpotColorMask
		{
			get
			{
				ushort spotcolo1rmask = 0;
//				if(this.m_cbkSpotColor1Noprint.Checked)
//					spotcolo1rmask = (ushort)EnumWhiteInkImage.None;

				byte val = (byte)(m_numAllpercent1.Value*255/100);
				if(0<val && val < 255)
					val+=1;
				if(this.m_cbkSpotColor1All.Checked)
				{
					spotcolo1rmask = (ushort)((int)val<<8);
					spotcolo1rmask |= (ushort)EnumWhiteInkImage.All;
				}
				else if(this.m_cbkSpotColor1Rip.Checked)
				{
					spotcolo1rmask = (ushort)((int)val<<8);
					spotcolo1rmask |= (ushort)EnumWhiteInkImage.Rip;
				}
				else if(this.m_cbkSpotColor1Image.Checked)
				{
					for(int i = 0; i< this.m_clbimage1.Items.Count;i++)
					{
						ushort m = 1;
						if(this.m_clbimage1.GetItemChecked(i))
						{
							spotcolo1rmask |= (ushort)(m<<(15-i));					
						}
					}	
					int optionindex = OPTIONINDEX;
					for(int i = 0; i< this.m_clbimage1Opt.Items.Count;i++)
					{
						ushort m = 1;
						if(this.m_clbimage1Opt.GetItemChecked(i))
							spotcolo1rmask |= (ushort)(m<<optionindex);
						optionindex++;
					}		

					for(int i = 0; i< this.m_clbimage2Opt.Items.Count;i++)
					{
						ushort m = 1;
						if(this.m_clbimage2Opt.GetItemChecked(i))
							spotcolo1rmask |= (ushort)(m<<optionindex);
						optionindex++;
					}		
					spotcolo1rmask += (ushort)EnumWhiteInkImage.Image;
				}
				return spotcolo1rmask;
			}
			set
			{
				ushort spotcolor1mask = value;
				if((spotcolor1mask & 0x0f) < (int)EnumWhiteInkImage.All || (spotcolor1mask & 0x0f) > (int)EnumWhiteInkImage.Image)
				{
					spotcolor1mask = (ushort)EnumWhiteInkImage.All;
				}
//				this.m_cbkSpotColor1Noprint.Checked = (spotcolor1mask & 0x0f) == (int)EnumWhiteInkImage.None;
				this.m_cbkSpotColor1All.Checked = (spotcolor1mask & 0x0f) == (int)EnumWhiteInkImage.All;
				this.m_cbkSpotColor1Rip.Checked = (spotcolor1mask & 0x0f) == (int)EnumWhiteInkImage.Rip;
				this.m_cbkSpotColor1Image.Checked = (spotcolor1mask & 0x0f) == (int)EnumWhiteInkImage.Image;
                if (this.m_cbkSpotColor1All.Checked || m_cbkSpotColor1Rip.Checked)
				{
					UIPreference.SetValueAndClampWithMinMax(m_numAllpercent1,(spotcolor1mask>>8)*100/255);
				}
				if(this.m_cbkSpotColor1Image.Checked)
				{
					for(int i = 0; i< this.m_clbimage1.Items.Count;i++)
					{
						ushort m = 1;
						this.m_clbimage1.SetItemChecked(i,(spotcolor1mask&(m<<(15-i)))!=0);
					}
					int optionindex = OPTIONINDEX;
					for(int i = 0; i< this.m_clbimage1Opt.Items.Count;i++)
					{
						ushort m = 1;
						this.m_clbimage1Opt.SetItemChecked(i,(spotcolor1mask&(m<<optionindex))!=0);
						optionindex++;
					}
					for(int i = 0; i< this.m_clbimage2Opt.Items.Count;i++)
					{
						ushort m = 1;
						this.m_clbimage2Opt.SetItemChecked(i,(spotcolor1mask&(m<<optionindex))!=0);
						optionindex++;
					}
				}
			}
		}

		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
			m_numAllpercent1.Minimum = new Decimal(0);
			m_numAllpercent1.Maximum = new Decimal(100);

			m_clbimage1.Items.Clear();
			int colorcount = sp.nColorNum - ((sp.nWhiteInkNum&0x0F)+(sp.nWhiteInkNum>>4));
			for(int i = 0;i < colorcount; i++)
			{
				ColorEnum color = (ColorEnum)sp.eColorOrder[i];
				string cmode = ResString.GetEnumDisplayName(typeof(ColorEnum),color);
				m_clbimage1.Items.Add(cmode);
			}
			// m_clbimage1Opt添加的为not
			m_clbimage1Opt.Items.AddRange(new object[]{ResString.GetResString("WhiteInkOptionNOT")});
			// m_clbimage2Opt添加的交集等多元操作符
			m_clbimage2Opt.Items.AddRange(new object[]{ResString.GetResString("WhiteInkOptionIntersect")});
		}

		private void m_cbkSpotColor1All_CheckedChanged(object sender, System.EventArgs e)
		{
			bool bSupport = m_cbkSpotColor1All.Checked || m_cbkSpotColor1Rip.Checked;
			this.m_numAllpercent1.Enabled = bSupport;
			if(!bSupport)
				UIPreference.SetValueAndClampWithMinMax( this.m_numAllpercent1,100);
		}

		private void m_cbkSpotColor1Image_CheckedChanged(object sender, System.EventArgs e)
		{
			this.m_numAllpercent1.Enabled = m_cbkSpotColor1All.Checked || m_cbkSpotColor1Rip.Checked;
			this.m_clbimage1.Enabled = this.m_cbkSpotColor1Image.Checked;
			this.m_clbimage1Opt.Enabled = m_clbimage1.CheckedItems.Count > 0 && this.m_cbkSpotColor1Image.Checked;
			this.m_clbimage1Opt.Enabled = m_clbimage1.CheckedItems.Count > 1 && this.m_cbkSpotColor1Image.Checked;
		}

		private void m_clbimage1_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
			this.m_clbimage1Opt.Enabled = e.NewValue == CheckState.Checked ? true : m_clbimage1.CheckedItems.Count > 1;
			if(!this.m_clbimage1Opt.Enabled)
			{
				for(int i =0; i < this.m_clbimage1Opt.Items.Count; i++)
					this.m_clbimage1Opt.SetItemChecked(i,false);
			}
			this.m_clbimage2Opt.Enabled = e.NewValue == CheckState.Checked ? m_clbimage1.CheckedItems.Count >= 1 : m_clbimage1.CheckedItems.Count > 2;
			if(!this.m_clbimage2Opt.Enabled)
			{
				for(int i =0; i < this.m_clbimage2Opt.Items.Count; i++)
					this.m_clbimage2Opt.SetItemChecked(i,false);
			}
		}

	}
}

