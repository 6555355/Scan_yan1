using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrinterStubC.Utility;

namespace BYHXPrinterManager.Setting
{
	public class ZAixsSetting : BYHXPrinterManager.Setting.BYHXUserControl
	{
		private System.Windows.Forms.GroupBox groupBoxZ;
		private System.Windows.Forms.NumericUpDown numericUpDownZspeed;
		private System.Windows.Forms.Label labelZspeed;
		private System.Windows.Forms.NumericUpDown numericUpDownMesureXCoor;
		private System.Windows.Forms.Label labelMesureXCoor;
		private System.Windows.Forms.NumericUpDown numericUpDownMesureHeight;
		private System.Windows.Forms.Label labelMesureHeight;
		private System.Windows.Forms.NumericUpDown numericUpDownZspace;
		private System.Windows.Forms.Label labelZspace;
		private System.Windows.Forms.Button m_ButtonMeasureThick;
		private System.Windows.Forms.Button m_ButtonMeasureThick2;
		private System.Windows.Forms.Button m_ButtonManualMove;
		private System.Windows.Forms.NumericUpDown numericUpDown8;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.NumericUpDown numericUpDown7;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.NumericUpDown numericUpDownSensorPosZ;
		private System.Windows.Forms.Label labelSensorPosZ;
		private System.Windows.Forms.Label labelZMax;
		private System.Windows.Forms.NumericUpDown numericUpDownZMax;
		private System.Windows.Forms.ToolTip m_ToolTip;
		private System.Windows.Forms.Button buttonCancel;
		private System.ComponentModel.IContainer components = null;
        private Panel panelPrinting;
        private Button buttonMeasurebeforeprinting;
        private Button buttonCanceljob;
        private Button buttonPrintdirectly;
        private Button buttonManualMoveAndPrint;
        private NumericUpDown m_NumericUpDownThickness;
        private Label m_LabelMediaThickness;

		private SPrinterSetting m_PrinterSetting;
        public event EventHandler<csEventArgs> CustomButtonClicked;
		public ZAixsSetting()
		{
			// 该调用是 Windows 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化
		}

		/// <summary>
		/// 清理所有正在使用的资源。
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

		#region 设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZAixsSetting));
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxZ = new System.Windows.Forms.GroupBox();
            this.m_NumericUpDownThickness = new System.Windows.Forms.NumericUpDown();
            this.m_LabelMediaThickness = new System.Windows.Forms.Label();
            this.numericUpDownZspeed = new System.Windows.Forms.NumericUpDown();
            this.labelZspeed = new System.Windows.Forms.Label();
            this.numericUpDownMesureXCoor = new System.Windows.Forms.NumericUpDown();
            this.labelMesureXCoor = new System.Windows.Forms.Label();
            this.numericUpDownMesureHeight = new System.Windows.Forms.NumericUpDown();
            this.labelMesureHeight = new System.Windows.Forms.Label();
            this.numericUpDownZspace = new System.Windows.Forms.NumericUpDown();
            this.labelZspace = new System.Windows.Forms.Label();
            this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.m_ButtonMeasureThick = new System.Windows.Forms.Button();
            this.m_ButtonMeasureThick2 = new System.Windows.Forms.Button();
            this.m_ButtonManualMove = new System.Windows.Forms.Button();
            this.numericUpDown8 = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.numericUpDownSensorPosZ = new System.Windows.Forms.NumericUpDown();
            this.labelSensorPosZ = new System.Windows.Forms.Label();
            this.labelZMax = new System.Windows.Forms.Label();
            this.numericUpDownZMax = new System.Windows.Forms.NumericUpDown();
            this.panelPrinting = new System.Windows.Forms.Panel();
            this.buttonManualMoveAndPrint = new System.Windows.Forms.Button();
            this.buttonMeasurebeforeprinting = new System.Windows.Forms.Button();
            this.buttonCanceljob = new System.Windows.Forms.Button();
            this.buttonPrintdirectly = new System.Windows.Forms.Button();
            this.groupBoxZ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZspeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMesureXCoor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMesureHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZspace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSensorPosZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZMax)).BeginInit();
            this.panelPrinting.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBoxZ
            // 
            this.groupBoxZ.Controls.Add(this.m_NumericUpDownThickness);
            this.groupBoxZ.Controls.Add(this.m_LabelMediaThickness);
            this.groupBoxZ.Controls.Add(this.numericUpDownZspeed);
            this.groupBoxZ.Controls.Add(this.labelZspeed);
            this.groupBoxZ.Controls.Add(this.numericUpDownMesureXCoor);
            this.groupBoxZ.Controls.Add(this.labelMesureXCoor);
            this.groupBoxZ.Controls.Add(this.numericUpDownMesureHeight);
            this.groupBoxZ.Controls.Add(this.labelMesureHeight);
            this.groupBoxZ.Controls.Add(this.numericUpDownZspace);
            this.groupBoxZ.Controls.Add(this.labelZspace);
            this.groupBoxZ.Controls.Add(this.numericUpDown7);
            this.groupBoxZ.Controls.Add(this.label16);
            resources.ApplyResources(this.groupBoxZ, "groupBoxZ");
            this.groupBoxZ.Name = "groupBoxZ";
            this.groupBoxZ.TabStop = false;
            // 
            // m_NumericUpDownThickness
            // 
            this.m_NumericUpDownThickness.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownThickness, "m_NumericUpDownThickness");
            this.m_NumericUpDownThickness.Name = "m_NumericUpDownThickness";
            // 
            // m_LabelMediaThickness
            // 
            resources.ApplyResources(this.m_LabelMediaThickness, "m_LabelMediaThickness");
            this.m_LabelMediaThickness.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelMediaThickness.Name = "m_LabelMediaThickness";
            // 
            // numericUpDownZspeed
            // 
            resources.ApplyResources(this.numericUpDownZspeed, "numericUpDownZspeed");
            this.numericUpDownZspeed.Name = "numericUpDownZspeed";
            // 
            // labelZspeed
            // 
            resources.ApplyResources(this.labelZspeed, "labelZspeed");
            this.labelZspeed.Name = "labelZspeed";
            // 
            // numericUpDownMesureXCoor
            // 
            resources.ApplyResources(this.numericUpDownMesureXCoor, "numericUpDownMesureXCoor");
            this.numericUpDownMesureXCoor.Name = "numericUpDownMesureXCoor";
            this.numericUpDownMesureXCoor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelMesureXCoor
            // 
            resources.ApplyResources(this.labelMesureXCoor, "labelMesureXCoor");
            this.labelMesureXCoor.Name = "labelMesureXCoor";
            // 
            // numericUpDownMesureHeight
            // 
            resources.ApplyResources(this.numericUpDownMesureHeight, "numericUpDownMesureHeight");
            this.numericUpDownMesureHeight.Name = "numericUpDownMesureHeight";
            this.numericUpDownMesureHeight.ValueChanged += new System.EventHandler(this.numericUpDownMesureHeight_ValueChanged);
            // 
            // labelMesureHeight
            // 
            resources.ApplyResources(this.labelMesureHeight, "labelMesureHeight");
            this.labelMesureHeight.Name = "labelMesureHeight";
            // 
            // numericUpDownZspace
            // 
            resources.ApplyResources(this.numericUpDownZspace, "numericUpDownZspace");
            this.numericUpDownZspace.Name = "numericUpDownZspace";
            this.numericUpDownZspace.ValueChanged += new System.EventHandler(this.numericUpDownZspace_ValueChanged);
            // 
            // labelZspace
            // 
            resources.ApplyResources(this.labelZspace, "labelZspace");
            this.labelZspace.Name = "labelZspace";
            // 
            // numericUpDown7
            // 
            resources.ApplyResources(this.numericUpDown7, "numericUpDown7");
            this.numericUpDown7.Name = "numericUpDown7";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // m_ButtonMeasureThick
            // 
            resources.ApplyResources(this.m_ButtonMeasureThick, "m_ButtonMeasureThick");
            this.m_ButtonMeasureThick.Name = "m_ButtonMeasureThick";
            this.m_ButtonMeasureThick.Click += new System.EventHandler(this.m_ButtonMeasureThick_Click);
            // 
            // m_ButtonMeasureThick2
            // 
            resources.ApplyResources(this.m_ButtonMeasureThick2, "m_ButtonMeasureThick2");
            this.m_ButtonMeasureThick2.Name = "m_ButtonMeasureThick2";
            this.m_ButtonMeasureThick2.Click += new System.EventHandler(this.m_ButtonMeasureThick2_Click);
            // 
            // m_ButtonManualMove
            // 
            resources.ApplyResources(this.m_ButtonManualMove, "m_ButtonManualMove");
            this.m_ButtonManualMove.Name = "m_ButtonManualMove";
            this.m_ButtonManualMove.Click += new System.EventHandler(this.m_ButtonManualMove_Click);
            // 
            // numericUpDown8
            // 
            resources.ApplyResources(this.numericUpDown8, "numericUpDown8");
            this.numericUpDown8.Name = "numericUpDown8";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // numericUpDownSensorPosZ
            // 
            resources.ApplyResources(this.numericUpDownSensorPosZ, "numericUpDownSensorPosZ");
            this.numericUpDownSensorPosZ.Name = "numericUpDownSensorPosZ";
            // 
            // labelSensorPosZ
            // 
            resources.ApplyResources(this.labelSensorPosZ, "labelSensorPosZ");
            this.labelSensorPosZ.Name = "labelSensorPosZ";
            // 
            // labelZMax
            // 
            resources.ApplyResources(this.labelZMax, "labelZMax");
            this.labelZMax.Name = "labelZMax";
            // 
            // numericUpDownZMax
            // 
            resources.ApplyResources(this.numericUpDownZMax, "numericUpDownZMax");
            this.numericUpDownZMax.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownZMax.Name = "numericUpDownZMax";
            // 
            // panelPrinting
            // 
            this.panelPrinting.BackColor = System.Drawing.Color.Transparent;
            this.panelPrinting.Controls.Add(this.buttonManualMoveAndPrint);
            this.panelPrinting.Controls.Add(this.buttonMeasurebeforeprinting);
            this.panelPrinting.Controls.Add(this.buttonCanceljob);
            this.panelPrinting.Controls.Add(this.buttonPrintdirectly);
            resources.ApplyResources(this.panelPrinting, "panelPrinting");
            this.panelPrinting.Name = "panelPrinting";
            // 
            // buttonManualMoveAndPrint
            // 
            resources.ApplyResources(this.buttonManualMoveAndPrint, "buttonManualMoveAndPrint");
            this.buttonManualMoveAndPrint.Name = "buttonManualMoveAndPrint";
            this.buttonManualMoveAndPrint.Click += new System.EventHandler(this.buttonManualMoveAndPrint_Click);
            // 
            // buttonMeasurebeforeprinting
            // 
            resources.ApplyResources(this.buttonMeasurebeforeprinting, "buttonMeasurebeforeprinting");
            this.buttonMeasurebeforeprinting.Name = "buttonMeasurebeforeprinting";
            this.buttonMeasurebeforeprinting.Click += new System.EventHandler(this.buttonMeasurebeforeprinting_Click);
            // 
            // buttonCanceljob
            // 
            resources.ApplyResources(this.buttonCanceljob, "buttonCanceljob");
            this.buttonCanceljob.Name = "buttonCanceljob";
            this.buttonCanceljob.Click += new System.EventHandler(this.buttonCanceljob_Click);
            // 
            // buttonPrintdirectly
            // 
            resources.ApplyResources(this.buttonPrintdirectly, "buttonPrintdirectly");
            this.buttonPrintdirectly.Name = "buttonPrintdirectly";
            this.buttonPrintdirectly.Click += new System.EventHandler(this.buttonPrintdirectly_Click);
            // 
            // ZAixsSetting
            // 
            this.Controls.Add(this.panelPrinting);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxZ);
            this.Controls.Add(this.m_ButtonMeasureThick);
            this.Controls.Add(this.m_ButtonMeasureThick2);
            this.Controls.Add(this.m_ButtonManualMove);
            this.Controls.Add(this.numericUpDown8);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.numericUpDownSensorPosZ);
            this.Controls.Add(this.labelSensorPosZ);
            this.Controls.Add(this.labelZMax);
            this.Controls.Add(this.numericUpDownZMax);
            this.Name = "ZAixsSetting";
            resources.ApplyResources(this, "$this");
            this.Load += new System.EventHandler(this.ZAixsSetting_Load);
            this.groupBoxZ.ResumeLayout(false);
            this.groupBoxZ.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZspeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMesureXCoor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMesureHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZspace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSensorPosZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZMax)).EndInit();
            this.panelPrinting.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        /// <summary>
        /// 是否为打印前测高
        /// </summary>
        public bool IsMeasureBeforePrint { get; set; }

		private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
		public void OnPreferenceChange( UIPreference up)
		{
			if(m_CurrentUnit != up.Unit)
			{
				OnUnitChange(up.Unit);
				m_CurrentUnit = up.Unit;
				//				this.isDirty = false;
			}

            if (SPrinterProperty.IsFloraUv())
            {
                numericUpDownMesureXCoor.Minimum = (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, (float)(40 / 2.54));
                numericUpDownMesureXCoor.Maximum = (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, (float)(350 / 2.54));

                numericUpDown7.Minimum = (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, (float)(5 / 2.54));
                numericUpDown7.Maximum = (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, (float)(155 / 2.54));

            }
		}
		private void  OnUnitChange(UILengthUnit newUnit)
		{
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,this.numericUpDownZspace);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,this.numericUpDownMesureHeight);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,this.numericUpDownZMax);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,this.numericUpDownMesureXCoor);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,this.numericUpDownSensorPosZ);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,this.numericUpDown7);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,this.numericUpDown8);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, m_NumericUpDownThickness);

			string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownZspace, this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownMesureHeight, this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownZMax, this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownMesureXCoor, this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownSensorPosZ, this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDown7, this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDown8, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownThickness, this.m_ToolTip);
        }


		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
            try
            {
                m_PrinterSetting = ss;
                SZSetting zseting = ss.ZSetting;
                if (bLoaded && bAutoMeasure)
                {
                    ByhxZMoveParam param;
                    if (ScorpionCoreInterface.GetByhxZMoveParam(out param))
                    {
                        float zMaxPos1 = ss.ZSetting.fMesureMaxLen;
                        if (zMaxPos1 <= 0)
                        {
                            MessageBox.Show(ResString.GetResString("StrMesureResultError"), ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        float fZSpace = ss.ZSetting.fHeadToPaper;
                        float paperThick = ss.sExtensionSetting.zMaxLength - zMaxPos1;//
                        if (paperThick >= 0)
                        {
                            ss.sBaseSetting.fPaperThick = paperThick; //同步参数到设置窗口
                        }
                        else
                        {
                            if (Math.Abs(paperThick) < 0.001f)
                                ss.sBaseSetting.fPaperThick = 0;
                            else
                                if (!SPrinterProperty.IsYUEDA())
                                {
                                    MessageBox.Show(ResString.GetResString("StrZMaxTooShort"), ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                        }
                        ss.sBaseSetting.fZSpace = fZSpace; //同步参数到设置窗口
                    }
                    //更新测高结果
                    //numericUpDownZMax.Value = (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, zseting.fMesureMaxLen);
                    //numericUpDownSensorPosZ.Value = (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, zseting.fSensorPosZ);

                    bAutoMeasure = false;
                    SetPrinterStatusChanged(CoreInterface.GetBoardStatus()); //参数更新后触发回原点提示
                }
                else
                {
                    numericUpDownZspace.Value = (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, zseting.fHeadToPaper);
                    numericUpDownZspeed.Value = (decimal)zseting.fMeasureSpeedZ;
                    numericUpDownMesureHeight.Value = (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, zseting.fMesureHeight);
                    //numericUpDownZMax.Value = (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, zseting.fMesureMaxLen);
                    //numericUpDownMesureXCoor.Value = (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, zseting.fMesureXCoor);
                    //numericUpDownSensorPosZ.Value = (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, zseting.fSensorPosZ);
                    //numericUpDown7.Value = (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, zseting.fMesureYCoor);
                    numericUpDown8.Value = (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, zseting.reserve1);

                    decimal x = (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, zseting.fMesureXCoor);
                    if (x > numericUpDownMesureXCoor.Maximum || x < numericUpDownMesureXCoor.Minimum)
                    {
                        numericUpDownMesureXCoor.Value = numericUpDownMesureXCoor.Minimum;
                    }
                    else
                    {
                        numericUpDownMesureXCoor.Value = x;
                    }

                    decimal y = (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, zseting.fMesureYCoor);
                    if (y > numericUpDown7.Maximum || y < numericUpDown7.Minimum)
                    {
                        numericUpDown7.Value = numericUpDown7.Minimum;
                    }
                    else
                    {
                        numericUpDown7.Value = y;
                    }
                }

                if ((decimal)UIPreference.ToDisplayLength(m_CurrentUnit, zseting.fMesureMaxLen) > numericUpDownZMax.Maximum || (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, zseting.fMesureMaxLen) < numericUpDownZMax.Minimum)
                {
                    numericUpDownZMax.Value = 0;
                }
                else
                {
                    numericUpDownZMax.Value = (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, zseting.fMesureMaxLen);
                }
                if ((decimal)UIPreference.ToDisplayLength(m_CurrentUnit, zseting.fSensorPosZ) > numericUpDownSensorPosZ.Maximum || (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, zseting.fSensorPosZ) < numericUpDownSensorPosZ.Minimum)
                {
                    numericUpDownSensorPosZ.Value = 0;
                }
                else
                {
                    numericUpDownSensorPosZ.Value = (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, zseting.fSensorPosZ);
                }

                UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownThickness, m_CurrentUnit, ss.sBaseSetting.fPaperThick);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

		public void OnGetPrinterSetting(ref SPrinterSetting ss, ref bool bZSettingChange)
		{
            if (SPrinterProperty.IsFloraUv())
            {
                CheckValue();
            }

			float temp = UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(this.numericUpDownZspace.Value));
			if(temp != ss.ZSetting.fHeadToPaper)
			{
				ss.sBaseSetting.fZSpace=ss.ZSetting.fHeadToPaper		=	temp;
				bZSettingChange = true;
			}
			if(ss.ZSetting.fMeasureSpeedZ		!=	(short)this.numericUpDownZspeed.Value)
			{
				ss.ZSetting.fMeasureSpeedZ		=	(short)this.numericUpDownZspeed.Value;
				bZSettingChange = true;
			}
			temp = UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(this.numericUpDownMesureHeight.Value));
			if(temp != ss.ZSetting.fMesureHeight)
			{
				ss.ZSetting.fMesureHeight		=	temp;
				bZSettingChange = true;
			}
			temp = UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(this.numericUpDownZMax.Value));
			if(temp != ss.ZSetting.fMesureMaxLen)
			{
				ss.ZSetting.fMesureMaxLen		=	temp;
				bZSettingChange = true;
			}
			temp = UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(this.numericUpDownMesureXCoor.Value));
			if(temp != ss.ZSetting.fMesureXCoor)
			{
				ss.ZSetting.fMesureXCoor		=	temp;
				bZSettingChange = true;
			}
			temp = UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(this.numericUpDownSensorPosZ.Value));
			if(temp != ss.ZSetting.fSensorPosZ)
			{
				ss.ZSetting.fSensorPosZ		=	temp;
				bZSettingChange = true;
			}
			temp = UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(this.numericUpDown7.Value));
			if(temp != ss.ZSetting.fMesureYCoor)
			{
				ss.ZSetting.fMesureYCoor		=	temp;
				bZSettingChange = true;
			}
			temp = UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(this.numericUpDown8.Value));
			if(temp != ss.ZSetting.reserve1)
			{
				ss.ZSetting.reserve1		=	temp;
				bZSettingChange = true;
			}
            temp = UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(m_NumericUpDownThickness.Value));
            if (temp != ss.sBaseSetting.fPaperThick)
            {
                ss.sBaseSetting.fPaperThick = temp;
                bZSettingChange = true;
            }
		}

		private bool bMeasuring = false;
        private bool bSelfRaised = false;
        /// <summary>
        /// 是否进行过测高
        /// </summary>
        public bool HasMeasured { get; set; } // 
	    private bool bAutoMeasure = false;
		public void SetPrinterStatusChanged(JetStatusEnum status)
		{
			PrinterOperate po = PrinterOperate.UpdateByPrinterStatus(status);
			if(bSelfRaised && status == JetStatusEnum.Measuring)
			{
				bMeasuring = true;
				bSelfRaised = false;
			}
            if (bMeasuring && status == JetStatusEnum.Ready 
                && !bAutoMeasure //不是自动测高，或者自动测高已经更新了结果
                ) 
			{
				bSelfRaised = bMeasuring = false;
                if (IsMeasureBeforePrint)
                {
                    string m1 = ResString.GetEnumDisplayName(typeof(Confirm), Confirm.PrintNow);
                    DialogResult result = MessageBox.Show(m1, ResString.GetProductName(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    csEventArgs eventArgs = new csEventArgs() {Case = _case,Dr = DialogResult.Cancel};
                    if (result == DialogResult.Yes)
                    {
                        eventArgs.Dr = DialogResult.OK;
                    }
                    //CoreInterface.SendJetCommand((int)JetCmdEnum.BackToHomePoint, (int)AxisDir.X);
                    OnCustomButtonClicked(null, eventArgs);
                }
                else
                {
                    string m1 = ResString.GetEnumDisplayName(typeof(Confirm), Confirm.GoHome);
                    DialogResult result = MessageBox.Show(m1, ResString.GetProductName(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result != DialogResult.No)
                    {
                        CoreInterface.SendJetCommand((int)JetCmdEnum.BackToHomePoint, (int)AxisDir.X);
                    }
                }
			}
			this.buttonCancel.Enabled = po.CanMoveStop;
			this.m_ButtonManualMove.Enabled = 
				this.m_ButtonMeasureThick.Enabled =
                this.m_ButtonMeasureThick2.Enabled = po.CanMoveUp && po.CanMoveDown; 
		}

		private void m_ButtonMeasureThick_Click(object sender, System.EventArgs e)
		{
			bool bdirty = false;
			this.OnGetPrinterSetting(ref this.m_PrinterSetting,ref bdirty);
			if(bdirty)
				CoreInterface.SetPrinterSetting(ref this.m_PrinterSetting);

			CoreInterface.SendJetCommand((int)JetCmdEnum.MeasurePaper,1);
			bSelfRaised = true;
            HasMeasured = true;
		    bAutoMeasure = true;
		}

		private void m_ButtonManualMove_Click(object sender, System.EventArgs e)
		{
			bool bdirty = false;
			this.OnGetPrinterSetting(ref this.m_PrinterSetting,ref bdirty);
			if(bdirty)
				CoreInterface.SetPrinterSetting(ref this.m_PrinterSetting);

			float fZSpace							=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(this.numericUpDownZspace.Value));
            float fPaperThick = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.m_NumericUpDownThickness.Value)); ;
            CoreInterface.MoveZ(02, fZSpace, fPaperThick);
		}

		private void m_ButtonMeasureThick2_Click(object sender, System.EventArgs e)
		{
			bool bdirty = false;
			this.OnGetPrinterSetting(ref this.m_PrinterSetting,ref bdirty);
			if(bdirty)
				CoreInterface.SetPrinterSetting(ref this.m_PrinterSetting);

			CoreInterface.SendJetCommand((int)JetCmdEnum.MeasurePaper,2);
			bSelfRaised = true;
            HasMeasured = true;
            bAutoMeasure = true;
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			int len = 0;
 
			const int port = 1;
			const byte PRINTER_PIPECMDSIZE = 26;
			byte [] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
			//First Send Begin Updater
			m_pData[0] = 4 + 2;
			m_pData[1] = 0x57; ////SciCmd_CMD_AbortMeasure  = 0x57

			m_pData[2] = (byte)(len&0xff);       
			m_pData[3] = (byte)((len>>8)&0xff);  
			m_pData[4] = (byte)((len>>16)&0xff); 
			m_pData[5] = (byte)((len>>24)&0xff); 

			CoreInterface.SetPipeCmdPackage(m_pData,m_pData[0],port);
		}

        private bool bLoaded = false;
        private bool bMeasureCancled = false;
        private void ZAixsSetting_Load(object sender, EventArgs e)
        {
            bool isSimpleUv = SPrinterProperty.IsSimpleUV();
            labelZspeed.Visible = numericUpDownZspeed.Visible = !isSimpleUv;
            if (IsMeasureBeforePrint)
            {
                m_ButtonMeasureThick.Visible = m_ButtonManualMove.Visible =
                    m_ButtonMeasureThick2.Visible = buttonCancel.Visible = false;
                panelPrinting.Visible = true;
                panelPrinting.Location = m_ButtonMeasureThick.Location;
            }
            else
            {
                m_ButtonMeasureThick.Visible = m_ButtonManualMove.Visible =
                    m_ButtonMeasureThick2.Visible = buttonCancel.Visible = true;
                panelPrinting.Visible = false;
            }
            bLoaded = true;
        }

	    private CsButtonCase _case;
        private void buttonCanceljob_Click(object sender, EventArgs e)
        {
            CoreInterface.SendJetCommand((int)JetCmdEnum.BackToHomePoint, (int)AxisDir.X);
            _case = CsButtonCase.CancelJob;
            OnCustomButtonClicked(sender, new csEventArgs() { Case = _case,Dr = DialogResult.Cancel });
        }

        private void buttonPrintdirectly_Click(object sender, EventArgs e)
        {
            //CoreInterface.SendJetCommand((int)JetCmdEnum.BackToHomePoint, (int)AxisDir.X);
            //JetStatusEnum status = CoreInterface.GetBoardStatus();
            _case = CsButtonCase.PrintDirectly;
            OnCustomButtonClicked(sender, new csEventArgs() { Case = _case, Dr = DialogResult.OK });
        }

        private void buttonMeasurebeforeprinting_Click(object sender, EventArgs e)
        {
            bool bdirty = false;
            this.OnGetPrinterSetting(ref this.m_PrinterSetting, ref bdirty);
            if (bdirty)
                CoreInterface.SetPrinterSetting(ref this.m_PrinterSetting);

            CoreInterface.SendJetCommand((int)JetCmdEnum.MeasurePaper, 1);
            bSelfRaised = true;
            HasMeasured = true;
            bAutoMeasure = true; 
            _case = CsButtonCase.MeasureBeforePrint;
        }

        private void OnCustomButtonClicked(object sender, csEventArgs e)
	    {
	        if (CustomButtonClicked != null)
	        {
	            CustomButtonClicked(sender, e);
	        }
        }

        private void buttonManualMoveAndPrint_Click(object sender, EventArgs e)
        {
            bool bdirty = false;
            this.OnGetPrinterSetting(ref this.m_PrinterSetting, ref bdirty);
            if (bdirty)
                CoreInterface.SetPrinterSetting(ref this.m_PrinterSetting);

            //float fZSpace = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.numericUpDownZspace.Value));
            //float fPaperThick = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.m_NumericUpDownThickness.Value)); ;
            //CoreInterface.MoveZ(02, fZSpace, fPaperThick);

            _case = CsButtonCase.PrintDirectly;
            OnCustomButtonClicked(sender, new csEventArgs() {Case = _case, Dr = DialogResult.OK});
        }

        private void numericUpDownMesureHeight_ValueChanged(object sender, EventArgs e)
        {
            //if (SPrinterProperty.IsFloraUv())
            //{
            //    CheckValue();
            //}
        }

        private void numericUpDownZspace_ValueChanged(object sender, EventArgs e)
        {
            //if (SPrinterProperty.IsFloraUv())
            //{
            //    CheckValue();
            //}
        }

        private void CheckValue()
        {
            if (numericUpDownZspace.Value < numericUpDownMesureHeight.Value)
            {
                numericUpDownZspace.Value = numericUpDownMesureHeight.Value;
            }
        }
	}

    public class csEventArgs : EventArgs
    {
        public CsButtonCase Case { get; set; }
        public DialogResult Dr { get; set; }
    }

    public enum CsButtonCase
    {
        MeasureBeforePrint =0,
        PrintDirectly,
        CancelJob,
        None,
    }
}

