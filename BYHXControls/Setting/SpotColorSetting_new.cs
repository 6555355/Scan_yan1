using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BYHXPrinterManager.GradientControls;
using XPTable;
using XPTable.Editors;
using XPTable.Models;
using System.Resources;
using System.Collections.Generic;

namespace BYHXPrinterManager.Setting
{
	public class SpotColorSetting_new : BYHXPrinterManager.Setting.BYHXUserControl
    {
        private BYHXPrinterManager.Setting.SpotColorMaskSetting spotColorMaskSetting1;
        private BYHXPrinterManager.Setting.SpotColorMaskSetting spotColorMaskSetting2;
		private System.Windows.Forms.ComboBox cbo_MultipleInk;
		private System.Windows.Forms.Label label1;
        private CheckBox checkBox14plTo42pl;
        private Panel panel1;
        private CheckBox checkBoxEnableSingleLayerMode;

        private System.ComponentModel.IContainer components = null;
        private ComboBox cbo_MultipleVavishInk;
        private Label label2;
        private Grouper grouperGrey;
        private NumericUpDown m_numAllpercentBaseGrey;
        private int colorNum = 8;
        private int LineHeight = 23;

        private List<Label> LabelGreyList = new List<Label>();
        private List<NumericUpDown> NumericUpDownGreyList = new List<NumericUpDown>();
        private Label labelBaseGrey;
        private RadioButton radioButtonRip;
        private RadioButton radioButtonAll;

	    private bool hasLoaded = false;
        public SpotColorSetting_new()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
            this.Load += new EventHandler(SpotColorSetting_Load);
		}

        void SpotColorSetting_Load(object sender, EventArgs e)
        {
            hasLoaded =true;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpotColorSetting_new));
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style2 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style3 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style4 = new BYHXPrinterManager.Style();
            this.spotColorMaskSetting1 = new BYHXPrinterManager.Setting.SpotColorMaskSetting();
            this.spotColorMaskSetting2 = new BYHXPrinterManager.Setting.SpotColorMaskSetting();
            this.cbo_MultipleInk = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox14plTo42pl = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grouperGrey = new BYHXPrinterManager.GradientControls.Grouper();
            this.radioButtonRip = new System.Windows.Forms.RadioButton();
            this.radioButtonAll = new System.Windows.Forms.RadioButton();
            this.labelBaseGrey = new System.Windows.Forms.Label();
            this.m_numAllpercentBaseGrey = new System.Windows.Forms.NumericUpDown();
            this.checkBoxEnableSingleLayerMode = new System.Windows.Forms.CheckBox();
            this.cbo_MultipleVavishInk = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.grouperGrey.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numAllpercentBaseGrey)).BeginInit();
            this.SuspendLayout();
            // 
            // spotColorMaskSetting1
            // 
            this.spotColorMaskSetting1.Divider = false;
            resources.ApplyResources(this.spotColorMaskSetting1, "spotColorMaskSetting1");
            style1.Color1 = System.Drawing.SystemColors.Control;
            style1.Color2 = System.Drawing.SystemColors.Control;
            this.spotColorMaskSetting1.GradientColors = style1;
            this.spotColorMaskSetting1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.spotColorMaskSetting1.GrouperTitleStyle = null;
            this.spotColorMaskSetting1.Name = "spotColorMaskSetting1";
            this.spotColorMaskSetting1.SpotColorMask = ((ushort)(0));
            // 
            // spotColorMaskSetting2
            // 
            this.spotColorMaskSetting2.Divider = false;
            resources.ApplyResources(this.spotColorMaskSetting2, "spotColorMaskSetting2");
            style2.Color1 = System.Drawing.SystemColors.Control;
            style2.Color2 = System.Drawing.SystemColors.Control;
            this.spotColorMaskSetting2.GradientColors = style2;
            this.spotColorMaskSetting2.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.spotColorMaskSetting2.GrouperTitleStyle = null;
            this.spotColorMaskSetting2.Name = "spotColorMaskSetting2";
            this.spotColorMaskSetting2.SpotColorMask = ((ushort)(0));
            // 
            // cbo_MultipleInk
            // 
            resources.ApplyResources(this.cbo_MultipleInk, "cbo_MultipleInk");
            this.cbo_MultipleInk.Items.AddRange(new object[] {
            resources.GetString("cbo_MultipleInk.Items"),
            resources.GetString("cbo_MultipleInk.Items1"),
            resources.GetString("cbo_MultipleInk.Items2"),
            resources.GetString("cbo_MultipleInk.Items3"),
            resources.GetString("cbo_MultipleInk.Items4"),
            resources.GetString("cbo_MultipleInk.Items5"),
            resources.GetString("cbo_MultipleInk.Items6"),
            resources.GetString("cbo_MultipleInk.Items7")});
            this.cbo_MultipleInk.Name = "cbo_MultipleInk";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // checkBox14plTo42pl
            // 
            resources.ApplyResources(this.checkBox14plTo42pl, "checkBox14plTo42pl");
            this.checkBox14plTo42pl.BackColor = System.Drawing.Color.Transparent;
            this.checkBox14plTo42pl.Name = "checkBox14plTo42pl";
            this.checkBox14plTo42pl.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.grouperGrey);
            this.panel1.Controls.Add(this.spotColorMaskSetting2);
            this.panel1.Controls.Add(this.spotColorMaskSetting1);
            this.panel1.Name = "panel1";
            // 
            // grouperGrey
            // 
            this.grouperGrey.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperGrey.BorderThickness = 1F;
            this.grouperGrey.Controls.Add(this.radioButtonRip);
            this.grouperGrey.Controls.Add(this.radioButtonAll);
            this.grouperGrey.Controls.Add(this.labelBaseGrey);
            this.grouperGrey.Controls.Add(this.m_numAllpercentBaseGrey);
            resources.ApplyResources(this.grouperGrey, "grouperGrey");
            style3.Color1 = System.Drawing.Color.LightBlue;
            style3.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperGrey.GradientColors = style3;
            this.grouperGrey.GroupImage = null;
            this.grouperGrey.Name = "grouperGrey";
            this.grouperGrey.PaintGroupBox = false;
            this.grouperGrey.RoundCorners = 10;
            this.grouperGrey.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperGrey.ShadowControl = false;
            this.grouperGrey.ShadowThickness = 3;
            this.grouperGrey.TabStop = false;
            style4.Color1 = System.Drawing.Color.LightBlue;
            style4.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperGrey.TitileGradientColors = style4;
            this.grouperGrey.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouperGrey.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // radioButtonRip
            // 
            resources.ApplyResources(this.radioButtonRip, "radioButtonRip");
            this.radioButtonRip.Name = "radioButtonRip";
            this.radioButtonRip.UseVisualStyleBackColor = true;
            // 
            // radioButtonAll
            // 
            resources.ApplyResources(this.radioButtonAll, "radioButtonAll");
            this.radioButtonAll.Checked = true;
            this.radioButtonAll.Name = "radioButtonAll";
            this.radioButtonAll.TabStop = true;
            this.radioButtonAll.UseVisualStyleBackColor = true;
            // 
            // labelBaseGrey
            // 
            resources.ApplyResources(this.labelBaseGrey, "labelBaseGrey");
            this.labelBaseGrey.Name = "labelBaseGrey";
            // 
            // m_numAllpercentBaseGrey
            // 
            resources.ApplyResources(this.m_numAllpercentBaseGrey, "m_numAllpercentBaseGrey");
            this.m_numAllpercentBaseGrey.Name = "m_numAllpercentBaseGrey";
            // 
            // checkBoxEnableSingleLayerMode
            // 
            resources.ApplyResources(this.checkBoxEnableSingleLayerMode, "checkBoxEnableSingleLayerMode");
            this.checkBoxEnableSingleLayerMode.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEnableSingleLayerMode.Name = "checkBoxEnableSingleLayerMode";
            this.checkBoxEnableSingleLayerMode.UseVisualStyleBackColor = false;
            // 
            // cbo_MultipleVavishInk
            // 
            resources.ApplyResources(this.cbo_MultipleVavishInk, "cbo_MultipleVavishInk");
            this.cbo_MultipleVavishInk.Items.AddRange(new object[] {
            resources.GetString("cbo_MultipleVavishInk.Items"),
            resources.GetString("cbo_MultipleVavishInk.Items1"),
            resources.GetString("cbo_MultipleVavishInk.Items2"),
            resources.GetString("cbo_MultipleVavishInk.Items3"),
            resources.GetString("cbo_MultipleVavishInk.Items4"),
            resources.GetString("cbo_MultipleVavishInk.Items5"),
            resources.GetString("cbo_MultipleVavishInk.Items6"),
            resources.GetString("cbo_MultipleVavishInk.Items7")});
            this.cbo_MultipleVavishInk.Name = "cbo_MultipleVavishInk";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // SpotColorSetting_new
            // 
            this.Controls.Add(this.cbo_MultipleVavishInk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxEnableSingleLayerMode);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBox14plTo42pl);
            this.Controls.Add(this.cbo_MultipleInk);
            this.Controls.Add(this.label1);
            this.Name = "SpotColorSetting_new";
            resources.ApplyResources(this, "$this");
            this.panel1.ResumeLayout(false);
            this.grouperGrey.ResumeLayout(false);
            this.grouperGrey.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numAllpercentBaseGrey)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private JetStatusEnum  m_curStatus = JetStatusEnum.Ready;
		private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
		private const int  M_AUTOINDENT = 56;
		private const int OPTIONCOUNT = 2;
		private const int OPTIONINDEX = 4; // 操作标志位存放开始位置
		private const int MAXLAYERCOUNT = 8; // 操作标志位存放开始位置
		private  SPrinterProperty spp;

		private bool isDirty = false;

		public bool IsDirty
		{
			get { return isDirty; }
			set { isDirty = value; }
		}


		public void SetPrinterStatusChanged(JetStatusEnum status)
		{


		}

        public void OnExtendedSettingsChange(PeripheralExtendedSettings ss)
        {
            checkBoxEnableSingleLayerMode.Checked = ss.EnableSingleLayerMode;
        }

        public void OnGetExtendedSettingsChange(ref PeripheralExtendedSettings ss)
        {
            ss.EnableSingleLayerMode = checkBoxEnableSingleLayerMode.Checked;
        }

		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
			spp = sp;

			bool bsupportwhite = (sp.nWhiteInkNum&0x0F) >0;
			bool bsupportVarnish = (sp.nWhiteInkNum>>4) > 0;

			this.spotColorMaskSetting1.Visible = bsupportwhite;
			this.spotColorMaskSetting2.Visible = bsupportVarnish;

            label1.Visible = cbo_MultipleInk.Visible = bsupportwhite;
            label2.Visible = cbo_MultipleVavishInk.Visible = bsupportVarnish;

            this.grouperGrey.Visible = NewLayoutFun.IsSupportGray;
            if (!bsupportwhite && bsupportVarnish)
            {
                this.spotColorMaskSetting2.Location = this.spotColorMaskSetting1.Location;
                label2.Location = label1.Location;
                cbo_MultipleVavishInk.Location = cbo_MultipleInk.Location;
            }

            if (!bsupportwhite && !bsupportVarnish)
            {
                this.grouperGrey.Location = this.spotColorMaskSetting1.Location;
            }
            else if (bsupportwhite && !bsupportVarnish)
            {
                this.grouperGrey.Location = this.spotColorMaskSetting2.Location;
            }

			this.spotColorMaskSetting1.OnPrinterPropertyChange(sp);
			this.spotColorMaskSetting2.OnPrinterPropertyChange(sp);
			this.spotColorMaskSetting1.Title = ResString.GetResString("EnumLayerType_White");
			this.spotColorMaskSetting2.Title = ResString.GetResString("EnumLayerType_Varnish");

            checkBox14plTo42pl.Visible = false; //SPrinterProperty.IsSurpportVolumeConvert(sp.ePrinterHead);
            checkBoxEnableSingleLayerMode.Visible = false;

            colorNum = CoreInterface.GetLayoutColorNum();
            LabelGreyList.Clear();
            NumericUpDownGreyList.Clear();

            if (colorNum > 0)
            {
                labelBaseGrey.Text = NewLayoutFun.GetColorName(CoreInterface.GetLayoutColorID(0));
                LabelGreyList.Add(labelBaseGrey);
                NumericUpDownGreyList.Add(m_numAllpercentBaseGrey);
                LineHeight = 25;

                for (int i = 1; i < colorNum; i++)
                {
                    Label labelGrey = new Label();
                    ControlClone.LabelClone(labelGrey, this.labelBaseGrey);

                    labelGrey.Location = new Point(labelBaseGrey.Location.X, labelBaseGrey.Location.Y + LineHeight);
                    labelGrey.Text = NewLayoutFun.GetColorName(CoreInterface.GetLayoutColorID(i));
                    labelGrey.Width = labelBaseGrey.Width;
                    labelGrey.Visible = true;
                    LabelGreyList.Add(labelGrey);
                    grouperGrey.Controls.Add(labelGrey);

                    NumericUpDown numericUpDownGrey = new NumericUpDown();
                    ControlClone.NumericUpDownClone(numericUpDownGrey, m_numAllpercentBaseGrey);

                    numericUpDownGrey.Location = new Point(m_numAllpercentBaseGrey.Location.X, m_numAllpercentBaseGrey.Location.Y + LineHeight);
                    numericUpDownGrey.Value = 0;
                    numericUpDownGrey.Visible = true;
                    numericUpDownGrey.Width = m_numAllpercentBaseGrey.Width;
                    NumericUpDownGreyList.Add(numericUpDownGrey);
                    grouperGrey.Controls.Add(numericUpDownGrey);

                    LineHeight += 25;
                }
            }

            this.isDirty = false;
		}

		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
			bool bsupportwhite = (spp.nWhiteInkNum&0x0F) >0;
			bool bsupportVarnish = (spp.nWhiteInkNum>>4) > 0;


            if (ss.sBaseSetting.nSpotColor1Mask == 0)
                ss.sBaseSetting.nSpotColor1Mask = (ushort)0xFF00;

            if (ss.sBaseSetting.nSpotColor2Mask == 0)
                ss.sBaseSetting.nSpotColor2Mask = (ushort)0xFF00;

            this.spotColorMaskSetting1.SpotColorMask = ss.sBaseSetting.nSpotColor1Mask;
            this.spotColorMaskSetting2.SpotColorMask = ss.sBaseSetting.nSpotColor2Mask;

            UIPreference.SetSelectIndexAndClampWithMax(this.cbo_MultipleInk, ss.sBaseSetting.multipleWriteInk);
            UIPreference.SetSelectIndexAndClampWithMax(this.cbo_MultipleVavishInk, ss.sExtensionSetting.multipleVanishInk);
            this.checkBox14plTo42pl.Checked = (ss.sBaseSetting.bitRegion & 1) != 0;

            radioButtonRip.Checked = ss.sExtensionSetting.bGreyRip == 1 ? true : false;

            for(int i=0; i<colorNum && i<ss.sExtensionSetting.ColorGreyMask.Length;i++)
            {
                NumericUpDownGreyList[i].Value = ss.sExtensionSetting.ColorGreyMask[i] * 100 / 255;

            }

			this.isDirty = false;
		}
		public void OnGetPrinterSetting(ref SPrinterSetting ss)
		{
			ss.sBaseSetting.nSpotColor1Mask = this.spotColorMaskSetting1.SpotColorMask;
			ss.sBaseSetting.nSpotColor2Mask = this.spotColorMaskSetting2.SpotColorMask;
            
			ss.sBaseSetting.multipleWriteInk = (byte)this.cbo_MultipleInk.SelectedIndex;
            ss.sExtensionSetting.multipleVanishInk= (byte)this.cbo_MultipleVavishInk.SelectedIndex;
            if (this.checkBox14plTo42pl.Checked)
                ss.sBaseSetting.bitRegion |= 1;
            else
                ss.sBaseSetting.bitRegion &= 0xfffe;

            ss.sExtensionSetting.bGreyRip = (byte)(radioButtonRip.Checked ? 1 : 0);

            if (colorNum > 0)
            {
                ss.sExtensionSetting.ColorGreyMask = new byte[CoreConst.MAX_COLOR_NUM];
                for (int i = 0; i < colorNum ; i++)
                {
                    byte val1 = (byte)(NumericUpDownGreyList[i].Value * 255 / 100);
                    if (0 < val1 && val1 < 255)
                        val1 += 1;

                    ss.sExtensionSetting.ColorGreyMask[i] = val1;
                }
            }

		}

		public void OnPreferenceChange( UIPreference up)
		{
			if(m_CurrentUnit != up.Unit)
			{
				OnUnitChange(up.Unit);
				m_CurrentUnit = up.Unit;
				this.isDirty = false;
			}
		}
 
		private void  OnUnitChange(UILengthUnit newUnit)
		{

		}

		
		public void SetGroupBoxStyle(Grouper ts)
		{
			this.GrouperTitleStyle = ts;
			this.spotColorMaskSetting1.GrouperTitleStyle = ts;
			this.spotColorMaskSetting2.GrouperTitleStyle = ts;
		}

	}
}

