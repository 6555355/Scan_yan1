using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
	public class MoveSetting : BYHXPrinterManager.Setting.BYHXUserControl
	{
		private System.ComponentModel.IContainer components = null;

		public MoveSetting()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
            if (PubFunc.IsInDesignMode())
                return;
            bool isVisible = (!PubFunc.IsFhzl3D() && !SPrinterProperty.IsSimpleUV() && (PubFunc.GetUserPermission() == (int)UserPermission.SupperUser)) || PubFunc.IsZhuoZhan();
            button4Up.Visible = button4Down.Visible = label1.Visible = m_ComboBox4speed.Visible = isVisible;
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
            this.components = new System.ComponentModel.Container();
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style2 = new BYHXPrinterManager.Style();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveSetting));
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.m_GroupBoxMove = new BYHXPrinterManager.GradientControls.Grouper();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button4Up = new System.Windows.Forms.Button();
            this.button4Down = new System.Windows.Forms.Button();
            this.m_ComboBox4speed = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonMoveUp = new System.Windows.Forms.Button();
            this.buttonMoveDown = new System.Windows.Forms.Button();
            this.buttonMoveB = new System.Windows.Forms.Button();
            this.button_MoveF = new System.Windows.Forms.Button();
            this.button_MoveR = new System.Windows.Forms.Button();
            this.buttonMoveLeft = new System.Windows.Forms.Button();
            this.m_ComboBoxZspeed = new System.Windows.Forms.ComboBox();
            this.m_LabelZspeed = new System.Windows.Forms.Label();
            this.m_LabelLeftEdge = new System.Windows.Forms.Label();
            this.m_LabelY = new System.Windows.Forms.Label();
            this.m_NumericUpDownMoveYSpeed = new System.Windows.Forms.ComboBox();
            this.m_NumericUpDownMoveXSpeed = new System.Windows.Forms.ComboBox();
            this.m_LabelLength = new System.Windows.Forms.Label();
            this.m_NumericUpDownLength = new System.Windows.Forms.NumericUpDown();
            this.m_ButtonStop = new System.Windows.Forms.Button();
            this.m_GroupBoxMove.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownLength)).BeginInit();
            this.SuspendLayout();
            // 
            // m_GroupBoxMove
            // 
            this.m_GroupBoxMove.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxMove.BorderThickness = 1F;
            this.m_GroupBoxMove.Controls.Add(this.button4Up);
            this.m_GroupBoxMove.Controls.Add(this.button4Down);
            this.m_GroupBoxMove.Controls.Add(this.m_ComboBox4speed);
            this.m_GroupBoxMove.Controls.Add(this.label1);
            this.m_GroupBoxMove.Controls.Add(this.buttonMoveUp);
            this.m_GroupBoxMove.Controls.Add(this.buttonMoveDown);
            this.m_GroupBoxMove.Controls.Add(this.buttonMoveB);
            this.m_GroupBoxMove.Controls.Add(this.button_MoveF);
            this.m_GroupBoxMove.Controls.Add(this.button_MoveR);
            this.m_GroupBoxMove.Controls.Add(this.buttonMoveLeft);
            this.m_GroupBoxMove.Controls.Add(this.m_ComboBoxZspeed);
            this.m_GroupBoxMove.Controls.Add(this.m_LabelZspeed);
            this.m_GroupBoxMove.Controls.Add(this.m_LabelLeftEdge);
            this.m_GroupBoxMove.Controls.Add(this.m_LabelY);
            this.m_GroupBoxMove.Controls.Add(this.m_NumericUpDownMoveYSpeed);
            this.m_GroupBoxMove.Controls.Add(this.m_NumericUpDownMoveXSpeed);
            this.m_GroupBoxMove.Controls.Add(this.m_LabelLength);
            this.m_GroupBoxMove.Controls.Add(this.m_NumericUpDownLength);
            this.m_GroupBoxMove.Controls.Add(this.m_ButtonStop);
            style1.Color1 = System.Drawing.Color.LightBlue;
            style1.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxMove.GradientColors = style1;
            this.m_GroupBoxMove.GroupImage = null;
            resources.ApplyResources(this.m_GroupBoxMove, "m_GroupBoxMove");
            this.m_GroupBoxMove.Name = "m_GroupBoxMove";
            this.m_GroupBoxMove.PaintGroupBox = false;
            this.m_GroupBoxMove.RoundCorners = 10;
            this.m_GroupBoxMove.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxMove.ShadowControl = false;
            this.m_GroupBoxMove.ShadowThickness = 3;
            this.m_GroupBoxMove.TabStop = false;
            style2.Color1 = System.Drawing.Color.LightBlue;
            style2.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxMove.TitileGradientColors = style2;
            this.m_GroupBoxMove.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxMove.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            // 
            // button4Up
            // 
            resources.ApplyResources(this.button4Up, "button4Up");
            this.button4Up.ImageList = this.imageList1;
            this.button4Up.Name = "button4Up";
            this.button4Up.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4Down
            // 
            resources.ApplyResources(this.button4Down, "button4Down");
            this.button4Down.ImageList = this.imageList1;
            this.button4Down.Name = "button4Down";
            this.button4Down.Click += new System.EventHandler(this.button2_Click);
            // 
            // m_ComboBox4speed
            // 
            resources.ApplyResources(this.m_ComboBox4speed, "m_ComboBox4speed");
            this.m_ComboBox4speed.Items.AddRange(new object[] {
            resources.GetString("m_ComboBox4speed.Items"),
            resources.GetString("m_ComboBox4speed.Items1"),
            resources.GetString("m_ComboBox4speed.Items2"),
            resources.GetString("m_ComboBox4speed.Items3"),
            resources.GetString("m_ComboBox4speed.Items4"),
            resources.GetString("m_ComboBox4speed.Items5"),
            resources.GetString("m_ComboBox4speed.Items6")});
            this.m_ComboBox4speed.Name = "m_ComboBox4speed";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // buttonMoveUp
            // 
            resources.ApplyResources(this.buttonMoveUp, "buttonMoveUp");
            this.buttonMoveUp.ImageList = this.imageList1;
            this.buttonMoveUp.Name = "buttonMoveUp";
            this.buttonMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
            // 
            // buttonMoveDown
            // 
            resources.ApplyResources(this.buttonMoveDown, "buttonMoveDown");
            this.buttonMoveDown.ImageList = this.imageList1;
            this.buttonMoveDown.Name = "buttonMoveDown";
            this.buttonMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
            // 
            // buttonMoveB
            // 
            resources.ApplyResources(this.buttonMoveB, "buttonMoveB");
            this.buttonMoveB.ImageList = this.imageList1;
            this.buttonMoveB.Name = "buttonMoveB";
            this.buttonMoveB.Click += new System.EventHandler(this.buttonMoveB_Click);
            // 
            // button_MoveF
            // 
            resources.ApplyResources(this.button_MoveF, "button_MoveF");
            this.button_MoveF.ImageList = this.imageList1;
            this.button_MoveF.Name = "button_MoveF";
            this.button_MoveF.Click += new System.EventHandler(this.button_MoveF_Click);
            // 
            // button_MoveR
            // 
            resources.ApplyResources(this.button_MoveR, "button_MoveR");
            this.button_MoveR.ImageList = this.imageList1;
            this.button_MoveR.Name = "button_MoveR";
            this.button_MoveR.Click += new System.EventHandler(this.button_MoveR_Click);
            // 
            // buttonMoveLeft
            // 
            resources.ApplyResources(this.buttonMoveLeft, "buttonMoveLeft");
            this.buttonMoveLeft.ImageList = this.imageList1;
            this.buttonMoveLeft.Name = "buttonMoveLeft";
            this.buttonMoveLeft.Click += new System.EventHandler(this.buttonMoveLeft_Click);
            // 
            // m_ComboBoxZspeed
            // 
            resources.ApplyResources(this.m_ComboBoxZspeed, "m_ComboBoxZspeed");
            this.m_ComboBoxZspeed.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxZspeed.Items"),
            resources.GetString("m_ComboBoxZspeed.Items1"),
            resources.GetString("m_ComboBoxZspeed.Items2"),
            resources.GetString("m_ComboBoxZspeed.Items3"),
            resources.GetString("m_ComboBoxZspeed.Items4"),
            resources.GetString("m_ComboBoxZspeed.Items5"),
            resources.GetString("m_ComboBoxZspeed.Items6")});
            this.m_ComboBoxZspeed.Name = "m_ComboBoxZspeed";
            // 
            // m_LabelZspeed
            // 
            resources.ApplyResources(this.m_LabelZspeed, "m_LabelZspeed");
            this.m_LabelZspeed.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelZspeed.Name = "m_LabelZspeed";
            // 
            // m_LabelLeftEdge
            // 
            resources.ApplyResources(this.m_LabelLeftEdge, "m_LabelLeftEdge");
            this.m_LabelLeftEdge.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelLeftEdge.Name = "m_LabelLeftEdge";
            // 
            // m_LabelY
            // 
            resources.ApplyResources(this.m_LabelY, "m_LabelY");
            this.m_LabelY.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelY.Name = "m_LabelY";
            // 
            // m_NumericUpDownMoveYSpeed
            // 
            resources.ApplyResources(this.m_NumericUpDownMoveYSpeed, "m_NumericUpDownMoveYSpeed");
            this.m_NumericUpDownMoveYSpeed.Items.AddRange(new object[] {
            resources.GetString("m_NumericUpDownMoveYSpeed.Items"),
            resources.GetString("m_NumericUpDownMoveYSpeed.Items1"),
            resources.GetString("m_NumericUpDownMoveYSpeed.Items2"),
            resources.GetString("m_NumericUpDownMoveYSpeed.Items3"),
            resources.GetString("m_NumericUpDownMoveYSpeed.Items4"),
            resources.GetString("m_NumericUpDownMoveYSpeed.Items5"),
            resources.GetString("m_NumericUpDownMoveYSpeed.Items6")});
            this.m_NumericUpDownMoveYSpeed.Name = "m_NumericUpDownMoveYSpeed";
            // 
            // m_NumericUpDownMoveXSpeed
            // 
            resources.ApplyResources(this.m_NumericUpDownMoveXSpeed, "m_NumericUpDownMoveXSpeed");
            this.m_NumericUpDownMoveXSpeed.Items.AddRange(new object[] {
            resources.GetString("m_NumericUpDownMoveXSpeed.Items"),
            resources.GetString("m_NumericUpDownMoveXSpeed.Items1"),
            resources.GetString("m_NumericUpDownMoveXSpeed.Items2"),
            resources.GetString("m_NumericUpDownMoveXSpeed.Items3"),
            resources.GetString("m_NumericUpDownMoveXSpeed.Items4"),
            resources.GetString("m_NumericUpDownMoveXSpeed.Items5"),
            resources.GetString("m_NumericUpDownMoveXSpeed.Items6")});
            this.m_NumericUpDownMoveXSpeed.Name = "m_NumericUpDownMoveXSpeed";
            // 
            // m_LabelLength
            // 
            resources.ApplyResources(this.m_LabelLength, "m_LabelLength");
            this.m_LabelLength.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelLength.Name = "m_LabelLength";
            // 
            // m_NumericUpDownLength
            // 
            resources.ApplyResources(this.m_NumericUpDownLength, "m_NumericUpDownLength");
            this.m_NumericUpDownLength.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.m_NumericUpDownLength.Name = "m_NumericUpDownLength";
            this.m_NumericUpDownLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // m_ButtonStop
            // 
            resources.ApplyResources(this.m_ButtonStop, "m_ButtonStop");
            this.m_ButtonStop.Name = "m_ButtonStop";
            this.m_ButtonStop.Click += new System.EventHandler(this.m_ButtonStop_Click);
            // 
            // MoveSetting
            // 
            this.Controls.Add(this.m_GroupBoxMove);
            this.Name = "MoveSetting";
            resources.ApplyResources(this, "$this");
            this.m_GroupBoxMove.ResumeLayout(false);
            this.m_GroupBoxMove.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownLength)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private JetStatusEnum  m_curStatus = JetStatusEnum.Ready;
		private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
		private const int  M_AUTOINDENT = 56;
		private SPrinterProperty m_SPrinterProperty;
		
		private System.Windows.Forms.ToolTip m_ToolTip;
		private System.Windows.Forms.Label m_LabelLeftEdge;
		private System.Windows.Forms.Label m_LabelY;
		private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxMove;
		private System.Windows.Forms.ComboBox m_NumericUpDownMoveXSpeed;
		private System.Windows.Forms.ComboBox m_NumericUpDownMoveYSpeed;
		private System.Windows.Forms.Label m_LabelZspeed;
		private System.Windows.Forms.ComboBox m_ComboBoxZspeed;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownLength;
		private System.Windows.Forms.Label m_LabelLength;
		private System.Windows.Forms.Button m_ButtonStop;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Button buttonMoveUp;
		private System.Windows.Forms.Button buttonMoveDown;
		private System.Windows.Forms.Button buttonMoveB;
		private System.Windows.Forms.Button button_MoveF;
		private System.Windows.Forms.Button button_MoveR;
		private System.Windows.Forms.Button buttonMoveLeft;
        private Button button4Up;
        private Button button4Down;
        private ComboBox m_ComboBox4speed;
        private Label label1;
		
		private bool isDirty = false;
		
		public bool IsDirty
		{
			get { return isDirty; }
			set { isDirty = value; }
		}

		public void SetPrinterStatusChanged(JetStatusEnum status)
		{
			m_curStatus = status;
            bool bEnabled = (status != JetStatusEnum.PowerOff);
            this.buttonMoveLeft.Enabled = this.button_MoveR.Enabled = m_SPrinterProperty.fPulsePerInchX != 0 && bEnabled;
            this.button_MoveF.Enabled = this.buttonMoveB.Enabled = m_SPrinterProperty.fPulsePerInchY != 0 && bEnabled;
            this.buttonMoveUp.Enabled = this.buttonMoveDown.Enabled = m_SPrinterProperty.fPulsePerInchZ != 0 && bEnabled;
            this.button4Down.Enabled = this.button4Up.Enabled = bEnabled;
            this.isDirty = false;

		}
		private void ToolTipInit()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BaseSetting));

			UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownLength.ToolTip"),this.m_NumericUpDownLength,this.m_ToolTip);
		}

		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
			m_SPrinterProperty = sp;
			this.isDirty = false;
		}

		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
			UIPreference.SetSelectIndexAndClampWithMax(m_NumericUpDownMoveXSpeed,ss.sMoveSetting.nXMoveSpeed-1);
			UIPreference.SetSelectIndexAndClampWithMax(m_NumericUpDownMoveYSpeed,ss.sMoveSetting.nYMoveSpeed-1);
            UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxZspeed, ss.sMoveSetting.nZMoveSpeed - 1);
            UIPreference.SetSelectIndexAndClampWithMax(m_ComboBox4speed, ss.sMoveSetting.n4MoveSpeed - 1);
			this.isDirty = false;
		}
		public void OnGetPrinterSetting(ref SPrinterSetting ss)
		{
			ss.sMoveSetting.nXMoveSpeed							=	Decimal.ToByte(m_NumericUpDownMoveXSpeed.SelectedIndex+1);		
			ss.sMoveSetting.nYMoveSpeed							=	Decimal.ToByte(m_NumericUpDownMoveYSpeed.SelectedIndex+1);
            ss.sMoveSetting.nZMoveSpeed = Decimal.ToByte(m_ComboBoxZspeed.SelectedIndex + 1);
            ss.sMoveSetting.n4MoveSpeed = Decimal.ToByte(m_ComboBox4speed.SelectedIndex + 1);		
		}

        private UIPreference m_UIPreference;
		public void OnPreferenceChange( UIPreference up)
		{
            m_UIPreference = up;
			if(m_CurrentUnit != up.Unit)
			{
				OnUnitChange(up.Unit);
				m_CurrentUnit = up.Unit;
				this.isDirty = false;
			}
		}
 
		private void  OnUnitChange(UILengthUnit newUnit)
		{
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownLength);

			string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownLength, this.m_ToolTip);
			this.isDirty = false;
		}

		private void Move(int speed,int dir,float flen)
		{
			const int port = 1;
			const byte PRINTER_PIPECMDSIZE = 26;
			byte [] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
			int len = 0;
			switch(dir)
			{
				case 1:
				case 2:
					len = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit,flen)*m_SPrinterProperty.fPulsePerInchX);
					break;
				case 3:
				case 4:
                    len = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, flen) * m_SPrinterProperty.fPulsePerInchY);
                    //len = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit,flen)*CoreInterface.GetfPulsePerInchY(0));
					break;
				case 5:
				case 6:
					len = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit,flen)*m_SPrinterProperty.fPulsePerInchZ);
					break;
			}
			//First Send Begin Updater
			m_pData[0] = 6 + 2;
			m_pData[1] = 0x31; //Move cmd

			m_pData[2] = (byte)dir; //Move cmd
			m_pData[3] = (byte)speed; //Move cmd
			m_pData[4] = (byte)(len&0xff); //Move cmd
			m_pData[5] = (byte)((len>>8)&0xff); //Move cmd
			m_pData[6] = (byte)((len>>16)&0xff); //Move cmd
			m_pData[7] = (byte)((len>>24)&0xff); //Move cmd

			CoreInterface.SetPipeCmdPackage(m_pData,m_pData[0],port);
		}
		private void MoveCmdEnum(int speed,MoveDirectionEnum dir,float flen)
		{
			const int port = 1;
			const byte PRINTER_PIPECMDSIZE = 26;
			byte [] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
			int len = 0;
			switch(dir)
			{
				case MoveDirectionEnum.Left:
				case MoveDirectionEnum.Right:
					len = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit,flen)*m_SPrinterProperty.fPulsePerInchX);
					break;
				case MoveDirectionEnum.Down:
				case MoveDirectionEnum.Up:
                    //len = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, flen) * m_SPrinterProperty.fPulsePerInchY);
                    float pulse = m_SPrinterProperty.fPulsePerInchY;
					pulse = pulse *UIPreference.ToInchLength(m_CurrentUnit,flen);
					len = Convert.ToInt32(pulse);
					break;
				case MoveDirectionEnum.Down_Z:
				case MoveDirectionEnum.Up_Z:
					len = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit,flen)*m_SPrinterProperty.fPulsePerInchZ);
					break;
			}
			CoreInterface.MoveCmd((int)dir,len,speed);
		}
		private void buttonMoveLeft_Click(object sender, System.EventArgs e)
		{
			float len = Decimal.ToSingle(m_NumericUpDownLength.Value);
            MoveDirectionEnum dirEnum = PubFunc.GetRealMoveDir(MoveDirectionEnum.Left, m_SPrinterProperty,m_UIPreference);
            int speed;
            if (!PubFunc.ParseSeedString(m_NumericUpDownMoveXSpeed.Text, out speed, dirEnum))
                return;
            MoveCmdEnum(speed, dirEnum, len);

		}

		private void button_MoveR_Click(object sender, System.EventArgs e)
		{
            int dir = 2;
			float len = Decimal.ToSingle(m_NumericUpDownLength.Value);
            MoveDirectionEnum dirEnum = PubFunc.GetRealMoveDir(MoveDirectionEnum.Right, m_SPrinterProperty, m_UIPreference);
            int speed;
            if (!PubFunc.ParseSeedString(m_NumericUpDownMoveXSpeed.Text, out speed, dirEnum))
                return;
            MoveCmdEnum(speed, dirEnum, len);
		}

		private void button_MoveF_Click(object sender, System.EventArgs e)
		{
            int speed;
			float len = Decimal.ToSingle(m_NumericUpDownLength.Value);
            MoveDirectionEnum dirEnum = PubFunc.GetRealMoveDir(MoveDirectionEnum.Down, m_SPrinterProperty, m_UIPreference);
            if (!PubFunc.ParseSeedString(m_NumericUpDownMoveYSpeed.Text, out speed, dirEnum))
                return;
            MoveCmdEnum(speed, dirEnum, len);
		}

		private void buttonMoveB_Click(object sender, System.EventArgs e)
		{
            int speed;
			float len = Decimal.ToSingle(m_NumericUpDownLength.Value);
            MoveDirectionEnum dirEnum = PubFunc.GetRealMoveDir(MoveDirectionEnum.Up, m_SPrinterProperty, m_UIPreference);
            if (!PubFunc.ParseSeedString(m_NumericUpDownMoveYSpeed.Text, out speed, dirEnum))
                return;
            MoveCmdEnum(speed, dirEnum, len);
		}

		private void buttonMoveDown_Click(object sender, System.EventArgs e)
		{
            int speed;
			float len = Decimal.ToSingle(m_NumericUpDownLength.Value);
			//this.Move(speed,dir,len);	
            MoveDirectionEnum dirEnum = PubFunc.GetRealMoveDir(MoveDirectionEnum.Down_Z, m_SPrinterProperty, m_UIPreference);
            if (!PubFunc.ParseSeedString(m_ComboBoxZspeed.Text, out speed, dirEnum))
                return;
            MoveCmdEnum(speed, dirEnum, len);
		}

		private void buttonMoveUp_Click(object sender, System.EventArgs e)
		{
            int speed;
			float len = Decimal.ToSingle(m_NumericUpDownLength.Value);
			//this.Move(speed,dir,len);
            MoveDirectionEnum dirEnum = PubFunc.GetRealMoveDir(MoveDirectionEnum.Up_Z, m_SPrinterProperty, m_UIPreference);
            if (!PubFunc.ParseSeedString(m_ComboBoxZspeed.Text, out speed, dirEnum))
                return;
            MoveCmdEnum(speed, dirEnum, len);
		}

		private void m_ButtonStop_Click(object sender, System.EventArgs e)
		{
			CoreInterface.SendJetCommand((int)JetCmdEnum.StopMove,0);
		}

        private void button1_Click(object sender, EventArgs e)
        {
            int speed;
            float len = Decimal.ToSingle(m_NumericUpDownLength.Value);
            MoveDirectionEnum dirEnum = MoveDirectionEnum.Up_4;
            if (!PubFunc.ParseSeedString(m_ComboBox4speed.Text, out speed, dirEnum))
                return;
            MoveCmdEnum(speed, dirEnum, len);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int speed;
            float len = Decimal.ToSingle(m_NumericUpDownLength.Value);
            MoveDirectionEnum dirEnum = MoveDirectionEnum.Down_4;
            if (!PubFunc.ParseSeedString(m_ComboBox4speed.Text, out speed, dirEnum))
                return;
            MoveCmdEnum(speed, dirEnum, len);
        }
	}
}

