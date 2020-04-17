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

namespace BYHXPrinterManager.Main
{
	/// <summary>
	/// Summary description for HeadLayoutControl.
	/// </summary>
	public class HeadLayoutControl : System.Windows.Forms.UserControl
	{
		private int m_nButtonNum = 0;
		private System.Windows.Forms.RadioButton [] m_radioButtonPointer;
		int m_nButtonHeight;
		int m_nButtonWidth;

		private bool m_bZeroSpace = false;

		private float m_Scale;
		private bool m_bScaleMin_Y;
		private int	 m_MarginMiddle;	 
		private SPrinterProperty m_sPrinterProperty;

		private const int m_Margin = 4;



		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private System.Windows.Forms.RadioButton m_RadioButtonSample;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HeadLayoutControl()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(HeadLayoutControl));
			this.m_RadioButtonSample = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// m_RadioButtonSample
			// 
			this.m_RadioButtonSample.AccessibleDescription = resources.GetString("m_RadioButtonSample.AccessibleDescription");
			this.m_RadioButtonSample.AccessibleName = resources.GetString("m_RadioButtonSample.AccessibleName");
			this.m_RadioButtonSample.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_RadioButtonSample.Anchor")));
			this.m_RadioButtonSample.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("m_RadioButtonSample.Appearance")));
			this.m_RadioButtonSample.BackColor = System.Drawing.SystemColors.Control;
			this.m_RadioButtonSample.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_RadioButtonSample.BackgroundImage")));
			this.m_RadioButtonSample.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_RadioButtonSample.CheckAlign")));
			this.m_RadioButtonSample.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_RadioButtonSample.Dock")));
			this.m_RadioButtonSample.Enabled = ((bool)(resources.GetObject("m_RadioButtonSample.Enabled")));
			this.m_RadioButtonSample.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_RadioButtonSample.FlatStyle")));
			this.m_RadioButtonSample.Font = ((System.Drawing.Font)(resources.GetObject("m_RadioButtonSample.Font")));
			this.m_RadioButtonSample.Image = ((System.Drawing.Image)(resources.GetObject("m_RadioButtonSample.Image")));
			this.m_RadioButtonSample.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_RadioButtonSample.ImageAlign")));
			this.m_RadioButtonSample.ImageIndex = ((int)(resources.GetObject("m_RadioButtonSample.ImageIndex")));
			this.m_RadioButtonSample.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_RadioButtonSample.ImeMode")));
			this.m_RadioButtonSample.Location = ((System.Drawing.Point)(resources.GetObject("m_RadioButtonSample.Location")));
			this.m_RadioButtonSample.Name = "m_RadioButtonSample";
			this.m_RadioButtonSample.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_RadioButtonSample.RightToLeft")));
			this.m_RadioButtonSample.Size = ((System.Drawing.Size)(resources.GetObject("m_RadioButtonSample.Size")));
			this.m_RadioButtonSample.TabIndex = ((int)(resources.GetObject("m_RadioButtonSample.TabIndex")));
			this.m_RadioButtonSample.Text = resources.GetString("m_RadioButtonSample.Text");
			this.m_RadioButtonSample.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_RadioButtonSample.TextAlign")));
			this.m_RadioButtonSample.Visible = ((bool)(resources.GetObject("m_RadioButtonSample.Visible")));
			this.m_RadioButtonSample.CheckedChanged += new System.EventHandler(this.m_RadioButtonSample_CheckedChanged);
			// 
			// HeadLayoutControl
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Controls.Add(this.m_RadioButtonSample);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "HeadLayoutControl";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.ResumeLayout(false);

		}
		#endregion

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void OnPrinterPropertyChange(SPrinterProperty sPrinterProperty)
		{
			m_sPrinterProperty = sPrinterProperty;
			if(m_sPrinterProperty.eSingleClean == SingleCleanEnum.SingleHead)
				m_nButtonNum = m_sPrinterProperty.GetPhyHeadNum();//.nHeadNum;
			else if (m_sPrinterProperty.eSingleClean == SingleCleanEnum.SingleColor)
			{
			    if (m_sPrinterProperty.IsMirrorArrangement())
			        m_nButtonNum = m_sPrinterProperty.nColorNum*2;
			    else
			        m_nButtonNum = m_sPrinterProperty.nColorNum;
                m_nButtonNum /= m_sPrinterProperty.nOneHeadDivider;
			}
		    LayoutFree(m_sPrinterProperty);
			CreateComponent();
			LayoutComponent();
			AppendComponent();
		}
		public void ResetAllButton()
		{
			for(int i = 0; i < m_radioButtonPointer.Length; i ++)
			{
				if(this.m_radioButtonPointer[i].Checked ==true)
					this.m_radioButtonPointer[i].Checked = false;
			}
		}

		private void CreateComponent()
		{
			this.m_radioButtonPointer = new RadioButton[m_nButtonNum];
			
			for(int i = 0; i < m_nButtonNum; i ++)
			{
				this.m_radioButtonPointer[i] = new RadioButton();
			}
		}
		protected void LayoutComponent()
		{
			this.SuspendLayout();
			float[] xOffset = m_sPrinterProperty.GetXOffset();
			float[] yOffset = m_sPrinterProperty.get_YOffset();

			for(int i = 0; i < m_nButtonNum; i ++)
			{

				RadioButton radio = this.m_radioButtonPointer[i];

				ControlClone.RadioButtonClone(radio,m_RadioButtonSample);
				//this.m_RadioButtonSample.Appearance = System.Windows.Forms.Appearance.Button;
				//this.m_RadioButtonSample.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
				//this.m_RadioButtonSample.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

				radio.Size = new Size(this.m_nButtonWidth,this.m_nButtonHeight);
				radio.Visible = true;
				radio.TabIndex = m_RadioButtonSample.TabIndex + i;
                radio.Tag = i;

				int groupIndex = i/m_sPrinterProperty.nColorNum;
				int colorIndex = i%m_sPrinterProperty.nColorNum;
				Point locate;
				if(m_bZeroSpace)
				{
					locate =  new Point(this.Width - m_Margin - (int)(m_Scale * i) - this.m_nButtonWidth, 
						this.m_nButtonHeight*groupIndex + m_Margin);
				}
				else
				{
					locate =  new Point(this.Width - m_Margin - (int)(m_Scale * xOffset[i]) - this.m_nButtonWidth, 
						   (int)(m_Scale * yOffset[colorIndex]) + this.m_nButtonHeight*groupIndex + m_Margin);
				}
				if(m_bScaleMin_Y)
					locate.X += m_MarginMiddle;
				else
					locate.Y += m_MarginMiddle;

				radio.Location =locate;
				radio.BackColor = m_sPrinterProperty.GetButtonColor(colorIndex);
				//radio.Paint += new PaintEventHandler(m_radioButtonExample_Paint);
				radio.CheckedChanged += new System.EventHandler(this.m_RadioButtonSample_CheckedChanged);
			}

		}

		private void AppendComponent()
		{
			for(int i = 0; i < m_nButtonNum; i++)
			{
				this.Controls.Add(this.m_radioButtonPointer[i]);
			}
			this.ResumeLayout(false);
		}
		private void LayoutFree(SPrinterProperty sPrinterProperty)
		{
			int nGroupNum = m_nButtonNum/m_sPrinterProperty.nColorNum;
			/////////////////////////////////////////////////////////////
			///Calculate max source width
			////////////////////////////////////////////////////////////
			float fXoffsetMax = 0;
			float[] xOffset = sPrinterProperty.GetXOffset();
			for(int i = 0; i <m_nButtonNum; i++)
			{
				if(fXoffsetMax < xOffset[i])
				{
					fXoffsetMax = xOffset[i];
				}
			}
	
			float fYoffsetMax = 0;
			float[] yOffset = sPrinterProperty.get_YOffset();
			for(int i = 0; i <m_sPrinterProperty.nColorNum; i++)
			{
				if(fYoffsetMax < yOffset[i])
				{
					fYoffsetMax = yOffset[i];
				}
			}
			m_bZeroSpace = ((Math.Abs(fXoffsetMax)<0.0001) &&  (Math.Abs(fYoffsetMax)<0.0001));

			//Assert Headheight:Headwidth = 1:4
			//Assert Headheight == Two Head Space
			const int RatioHeightWidth = 3; //界面显示喷头的宽高比
			float srcButtonHeight = Math.Abs(xOffset[1] - xOffset[0]);
			float srcButtonWidth  = srcButtonHeight/RatioHeightWidth;
			double angle = m_sPrinterProperty.fHeadAngle * Math.PI/180;
			float maxSrcWidth = (float)(fXoffsetMax + 
				Math.Abs(srcButtonHeight *Math.Sin(angle)+srcButtonWidth *Math.Cos(angle)));
			float maxSrcHeight = fYoffsetMax + 
				(float)((nGroupNum - 1) * srcButtonHeight +
				(Math.Abs(srcButtonHeight *Math.Cos(angle)+ srcButtonWidth *Math.Sin(angle))));
	

			/////////////////////////////////////////////////////////////
			///Calculate max source width
			////////////////////////////////////////////////////////////
			int maxClientWidth = this.Width - m_Margin * 2;
			int maxClientHeight = this.Height  - m_Margin *2;
			if(m_bZeroSpace)
			{
				m_Scale = (float)(maxClientWidth-1)/(m_nButtonNum);
				m_bScaleMin_Y = false;
				m_MarginMiddle =(int)(maxClientHeight - 1 - maxSrcHeight * m_Scale )/2  ;
				m_nButtonHeight = (int)(m_Scale);
				m_nButtonWidth = (int )(m_nButtonHeight / 4);
			}
			else
			{
				float scaleX =  ((float)(maxClientWidth-1)/maxSrcWidth);
				float scaleY = ((float)(maxClientHeight-1)/maxSrcHeight);
				m_Scale = Math.Min(scaleX,scaleY);
				if(m_Scale == scaleX)
				{
					m_bScaleMin_Y = false;
					m_MarginMiddle =(int)(maxClientHeight - 1 - maxSrcHeight * m_Scale )/2  ;
				}
				else
				{
					m_bScaleMin_Y = true;
					m_MarginMiddle = (int)(maxClientWidth - 1 - maxSrcWidth * m_Scale )/2;
				}
				//Modify Region 
				m_nButtonHeight = (int)(srcButtonHeight * m_Scale);
				m_nButtonWidth = (int )(srcButtonWidth * m_Scale);

			}
		}

		private void m_RadioButtonSample_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			System.Windows.Forms.RadioButton rdbSet = (RadioButton)sender;
			Graphics grfx = e.Graphics;
			ButtonState bs = ButtonState.Normal; 
			if(rdbSet == GetChildAtPoint(PointToClient(MousePosition)) || rdbSet.Capture)
			{
				bs = ButtonState.Pushed;
			}
			else if(rdbSet.Checked == true)
			{
				bs = ButtonState.Checked;
			}
			grfx.RotateTransform(m_sPrinterProperty.fHeadAngle);
			ControlPaint.DrawButton(grfx,rdbSet.ClientRectangle,bs);
		}

		private void m_RadioButtonSample_CheckedChanged(object sender, System.EventArgs e)
		{
			System.Windows.Forms.RadioButton rdbSet = (RadioButton)sender;
			if(rdbSet.Checked)
			{
				CoreInterface.SendJetCommand((int)JetCmdEnum.SingleClean,(int)rdbSet.Tag) ;
			}
		}



	}
}
