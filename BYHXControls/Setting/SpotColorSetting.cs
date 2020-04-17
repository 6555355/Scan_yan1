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

namespace BYHXPrinterManager.Setting
{
	public class SpotColorSetting : BYHXPrinterManager.Setting.BYHXUserControl
	{
		private System.Windows.Forms.CheckBox m_CheckBoxMirror;
		private BYHXPrinterManager.GradientControls.Grouper grouper3;
		private System.Windows.Forms.CheckBox CheckBoxReversePrint;
		private BYHXPrinterManager.Setting.SpotColorMaskSetting spotColorMaskSetting1;
		private System.Windows.Forms.ComboBox comboBox1;
		private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxInkStripe;
		private System.Windows.Forms.Label labelLayerNum;
		private Table table;
		private BYHXPrinterManager.Setting.SpotColorMaskSetting spotColorMaskSetting2;
		private BYHXPrinterManager.GradientControls.Grouper m_groupOffset;
		private System.Windows.Forms.CheckBox checkBoxColorNoPrint;
		private System.Windows.Forms.CheckBox checkBoxWhiteNoPrint;
		private System.Windows.Forms.CheckBox checkBoxVarnishNoPrint;
		private System.Windows.Forms.ComboBox cbo_MultipleInk;
		private System.Windows.Forms.Label label1;
        private CheckBox checkBox14plTo42pl;
        private Panel panel1;
        private CheckBox checkBoxEnableSingleLayerMode;

        private System.ComponentModel.IContainer components = null;

	    private bool hasLoaded = false;
		public SpotColorSetting()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpotColorSetting));
            BYHXPrinterManager.Style style9 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style10 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style11 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style3 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style12 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style13 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style6 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style14 = new BYHXPrinterManager.Style();
            this.table = new XPTable.Models.Table();
            this.m_CheckBoxMirror = new System.Windows.Forms.CheckBox();
            this.CheckBoxReversePrint = new System.Windows.Forms.CheckBox();
            this.grouper3 = new BYHXPrinterManager.GradientControls.Grouper();
            this.spotColorMaskSetting1 = new BYHXPrinterManager.Setting.SpotColorMaskSetting();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.m_GroupBoxInkStripe = new BYHXPrinterManager.GradientControls.Grouper();
            this.labelLayerNum = new System.Windows.Forms.Label();
            this.spotColorMaskSetting2 = new BYHXPrinterManager.Setting.SpotColorMaskSetting();
            this.m_groupOffset = new BYHXPrinterManager.GradientControls.Grouper();
            this.checkBoxVarnishNoPrint = new System.Windows.Forms.CheckBox();
            this.checkBoxWhiteNoPrint = new System.Windows.Forms.CheckBox();
            this.checkBoxColorNoPrint = new System.Windows.Forms.CheckBox();
            this.cbo_MultipleInk = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox14plTo42pl = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxEnableSingleLayerMode = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.table)).BeginInit();
            this.grouper3.SuspendLayout();
            this.m_GroupBoxInkStripe.SuspendLayout();
            this.m_groupOffset.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // table
            // 
            resources.ApplyResources(this.table, "table");
            this.table.Name = "table";
            // 
            // m_CheckBoxMirror
            // 
            this.m_CheckBoxMirror.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_CheckBoxMirror, "m_CheckBoxMirror");
            this.m_CheckBoxMirror.Name = "m_CheckBoxMirror";
            this.m_CheckBoxMirror.UseVisualStyleBackColor = false;
            // 
            // CheckBoxReversePrint
            // 
            this.CheckBoxReversePrint.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.CheckBoxReversePrint, "CheckBoxReversePrint");
            this.CheckBoxReversePrint.Name = "CheckBoxReversePrint";
            this.CheckBoxReversePrint.UseVisualStyleBackColor = false;
            this.CheckBoxReversePrint.CheckedChanged += new System.EventHandler(this.CheckBoxMirrorPrint_CheckedChanged);
            // 
            // grouper3
            // 
            this.grouper3.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouper3.BorderThickness = 1F;
            this.grouper3.Controls.Add(this.m_CheckBoxMirror);
            this.grouper3.Controls.Add(this.CheckBoxReversePrint);
            style9.Color1 = System.Drawing.Color.LightBlue;
            style9.Color2 = System.Drawing.Color.SteelBlue;
            this.grouper3.GradientColors = style9;
            this.grouper3.GroupImage = null;
            resources.ApplyResources(this.grouper3, "grouper3");
            this.grouper3.Name = "grouper3";
            this.grouper3.PaintGroupBox = false;
            this.grouper3.RoundCorners = 10;
            this.grouper3.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper3.ShadowControl = false;
            this.grouper3.ShadowThickness = 3;
            this.grouper3.TabStop = false;
            style10.Color1 = System.Drawing.Color.LightBlue;
            style10.Color2 = System.Drawing.Color.SteelBlue;
            this.grouper3.TitileGradientColors = style10;
            this.grouper3.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouper3.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            this.grouper3.Enter += new System.EventHandler(this.grouper3_Enter);
            // 
            // spotColorMaskSetting1
            // 
            this.spotColorMaskSetting1.Divider = false;
            resources.ApplyResources(this.spotColorMaskSetting1, "spotColorMaskSetting1");
            style11.Color1 = System.Drawing.SystemColors.Control;
            style11.Color2 = System.Drawing.SystemColors.Control;
            this.spotColorMaskSetting1.GradientColors = style11;
            this.spotColorMaskSetting1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.spotColorMaskSetting1.GrouperTitleStyle = null;
            this.spotColorMaskSetting1.Name = "spotColorMaskSetting1";
            this.spotColorMaskSetting1.SpotColorMask = ((ushort)(0));
            // 
            // comboBox1
            // 
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // m_GroupBoxInkStripe
            // 
            this.m_GroupBoxInkStripe.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxInkStripe.BorderThickness = 1F;
            this.m_GroupBoxInkStripe.Controls.Add(this.labelLayerNum);
            this.m_GroupBoxInkStripe.Controls.Add(this.comboBox1);
            this.m_GroupBoxInkStripe.Controls.Add(this.table);
            style3.Color1 = System.Drawing.Color.LightBlue;
            style3.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxInkStripe.GradientColors = style3;
            this.m_GroupBoxInkStripe.GroupImage = null;
            resources.ApplyResources(this.m_GroupBoxInkStripe, "m_GroupBoxInkStripe");
            this.m_GroupBoxInkStripe.Name = "m_GroupBoxInkStripe";
            this.m_GroupBoxInkStripe.PaintGroupBox = false;
            this.m_GroupBoxInkStripe.RoundCorners = 10;
            this.m_GroupBoxInkStripe.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxInkStripe.ShadowControl = false;
            this.m_GroupBoxInkStripe.ShadowThickness = 3;
            this.m_GroupBoxInkStripe.TabStop = false;
            style12.Color1 = System.Drawing.Color.LightBlue;
            style12.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxInkStripe.TitileGradientColors = style12;
            this.m_GroupBoxInkStripe.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxInkStripe.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // labelLayerNum
            // 
            resources.ApplyResources(this.labelLayerNum, "labelLayerNum");
            this.labelLayerNum.BackColor = System.Drawing.Color.Transparent;
            this.labelLayerNum.Name = "labelLayerNum";
            // 
            // spotColorMaskSetting2
            // 
            this.spotColorMaskSetting2.Divider = false;
            resources.ApplyResources(this.spotColorMaskSetting2, "spotColorMaskSetting2");
            style13.Color1 = System.Drawing.SystemColors.Control;
            style13.Color2 = System.Drawing.SystemColors.Control;
            this.spotColorMaskSetting2.GradientColors = style13;
            this.spotColorMaskSetting2.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.spotColorMaskSetting2.GrouperTitleStyle = null;
            this.spotColorMaskSetting2.Name = "spotColorMaskSetting2";
            this.spotColorMaskSetting2.SpotColorMask = ((ushort)(0));
            // 
            // m_groupOffset
            // 
            this.m_groupOffset.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_groupOffset.BorderThickness = 1F;
            this.m_groupOffset.Controls.Add(this.checkBoxVarnishNoPrint);
            this.m_groupOffset.Controls.Add(this.checkBoxWhiteNoPrint);
            this.m_groupOffset.Controls.Add(this.checkBoxColorNoPrint);
            style6.Color1 = System.Drawing.Color.LightBlue;
            style6.Color2 = System.Drawing.Color.SteelBlue;
            this.m_groupOffset.GradientColors = style6;
            this.m_groupOffset.GroupImage = null;
            resources.ApplyResources(this.m_groupOffset, "m_groupOffset");
            this.m_groupOffset.Name = "m_groupOffset";
            this.m_groupOffset.PaintGroupBox = false;
            this.m_groupOffset.RoundCorners = 10;
            this.m_groupOffset.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_groupOffset.ShadowControl = false;
            this.m_groupOffset.ShadowThickness = 3;
            this.m_groupOffset.TabStop = false;
            style14.Color1 = System.Drawing.Color.LightBlue;
            style14.Color2 = System.Drawing.Color.SteelBlue;
            this.m_groupOffset.TitileGradientColors = style14;
            this.m_groupOffset.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_groupOffset.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // checkBoxVarnishNoPrint
            // 
            this.checkBoxVarnishNoPrint.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.checkBoxVarnishNoPrint, "checkBoxVarnishNoPrint");
            this.checkBoxVarnishNoPrint.Name = "checkBoxVarnishNoPrint";
            this.checkBoxVarnishNoPrint.UseVisualStyleBackColor = false;
            this.checkBoxVarnishNoPrint.CheckedChanged += new System.EventHandler(this.checkBoxNoPrint_CheckedChanged);
            // 
            // checkBoxWhiteNoPrint
            // 
            this.checkBoxWhiteNoPrint.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.checkBoxWhiteNoPrint, "checkBoxWhiteNoPrint");
            this.checkBoxWhiteNoPrint.Name = "checkBoxWhiteNoPrint";
            this.checkBoxWhiteNoPrint.UseVisualStyleBackColor = false;
            this.checkBoxWhiteNoPrint.CheckedChanged += new System.EventHandler(this.checkBoxNoPrint_CheckedChanged);
            // 
            // checkBoxColorNoPrint
            // 
            this.checkBoxColorNoPrint.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.checkBoxColorNoPrint, "checkBoxColorNoPrint");
            this.checkBoxColorNoPrint.Name = "checkBoxColorNoPrint";
            this.checkBoxColorNoPrint.UseVisualStyleBackColor = false;
            this.checkBoxColorNoPrint.CheckedChanged += new System.EventHandler(this.checkBoxNoPrint_CheckedChanged);
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
            this.panel1.Controls.Add(this.spotColorMaskSetting2);
            this.panel1.Controls.Add(this.spotColorMaskSetting1);
            this.panel1.Name = "panel1";
            // 
            // checkBoxEnableSingleLayerMode
            // 
            resources.ApplyResources(this.checkBoxEnableSingleLayerMode, "checkBoxEnableSingleLayerMode");
            this.checkBoxEnableSingleLayerMode.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEnableSingleLayerMode.Name = "checkBoxEnableSingleLayerMode";
            this.checkBoxEnableSingleLayerMode.UseVisualStyleBackColor = false;
            // 
            // SpotColorSetting
            // 
            this.Controls.Add(this.checkBoxEnableSingleLayerMode);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBox14plTo42pl);
            this.Controls.Add(this.cbo_MultipleInk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grouper3);
            this.Controls.Add(this.m_GroupBoxInkStripe);
            this.Controls.Add(this.m_groupOffset);
            this.Name = "SpotColorSetting";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.table)).EndInit();
            this.grouper3.ResumeLayout(false);
            this.m_GroupBoxInkStripe.ResumeLayout(false);
            this.m_GroupBoxInkStripe.PerformLayout();
            this.m_groupOffset.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
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
			this.m_GroupBoxInkStripe.Visible = sp.bSupportWhiteInk;
			this.m_groupOffset.Visible = sp.bSupportWhiteInkYoffset;

			bool bsupportwhite = (sp.nWhiteInkNum&0x0F) >0;
			bool bsupportVarnish = (sp.nWhiteInkNum>>4) > 0;
            checkBoxWhiteNoPrint.Visible =
			this.spotColorMaskSetting1.Visible = bsupportwhite;
            checkBoxVarnishNoPrint.Visible =
			this.spotColorMaskSetting2.Visible = bsupportVarnish;
			if(!bsupportwhite && bsupportVarnish)
				this.spotColorMaskSetting2.Location  =this.spotColorMaskSetting1.Location;

			this.spotColorMaskSetting1.OnPrinterPropertyChange(sp);
			this.spotColorMaskSetting2.OnPrinterPropertyChange(sp);
			this.spotColorMaskSetting1.Title = ResString.GetResString("EnumLayerType_White");
			this.spotColorMaskSetting2.Title = ResString.GetResString("EnumLayerType_Varnish");

            grouper3.Visible = false;//白墨不显示反向功能
			if(sp.bSupportWhiteInk)
			{
				this.table.BeginUpdate();
				TextColumn textColumn = new TextColumn(ResString.GetResString("ColorLayer"), table.Width/2-2);
				textColumn.Editable = false;
				ComboBoxColumn comboboxColumn = new ComboBoxColumn("Ink Type", table.Width/2-2);
				this.table.ColumnModel = new ColumnModel(new Column[] {textColumn,comboboxColumn});
				ComboBoxCellEditor cbeditor = (ComboBoxCellEditor)this.table.ColumnModel.GetCellEditor(1);
				cbeditor.Items.Clear();
				cbeditor.Items.Add(ResString.GetResString("EnumLayerType_Color"));
				if(bsupportwhite)
					cbeditor.Items.Add(ResString.GetResString("EnumLayerType_White"));
				if(bsupportVarnish)
					cbeditor.Items.Add(ResString.GetResString("EnumLayerType_Varnish"));
                cbeditor.Items.Add(string.Format("{0}2", ResString.GetResString("EnumLayerType_Color")));
                this.table.ColumnResizing = false;
				this.table.EndUpdate();

				this.comboBox1.Items.Clear();
				for(int i = 0; i < MAXLAYERCOUNT; i++)
					this.comboBox1.Items.Add(i+1);

				grouper3.Top = this.m_GroupBoxInkStripe.Bottom + 8;
			}

			if(sp.bSupportWhiteInkYoffset)
			{
				this.m_groupOffset.Location = this.m_GroupBoxInkStripe.Location;
				grouper3.Top = this.m_groupOffset.Bottom + 8;
			}

            //cbo_MultipleInk.Items.Clear();
            //foreach (MultipleInkEnum place in Enum.GetValues(typeof(MultipleInkEnum)))
            //{
            //    string cmode = ResString.GetEnumDisplayName(typeof(MultipleInkEnum), place);
            //    cbo_MultipleInk.Items.Add(cmode);
            //}
            checkBox14plTo42pl.Visible = SPrinterProperty.IsSurpportVolumeConvert(sp.ePrinterHead);
            this.isDirty = false;
		}

		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
			bool bsupportwhite = (spp.nWhiteInkNum&0x0F) >0;
			bool bsupportVarnish = (spp.nWhiteInkNum>>4) > 0;

			CheckBoxReversePrint.Checked =  ss.sBaseSetting.bReversePrint;
			this.m_CheckBoxMirror.Checked = ss.sBaseSetting.bMirrorX;

            if (ss.sBaseSetting.nSpotColor1Mask == 0)
                ss.sBaseSetting.nSpotColor1Mask = (ushort)0xFF00;

            if (ss.sBaseSetting.nSpotColor2Mask == 0)
                ss.sBaseSetting.nSpotColor2Mask = (ushort)0xFF00;

            this.spotColorMaskSetting1.SpotColorMask = ss.sBaseSetting.nSpotColor1Mask;
            this.spotColorMaskSetting2.SpotColorMask = ss.sBaseSetting.nSpotColor2Mask;
            if (spp.bSupportWhiteInk)
			{

				this.comboBox1.SelectedIndex = ss.sBaseSetting.nWhiteInkLayer!=0?ss.sBaseSetting.nWhiteInkLayer-1:0;
			
				TableModel tb = new TableModel();
				string color = ResString.GetResString("EnumLayerType_Color");
                string color2 = string.Format("{0}2", ResString.GetResString("EnumLayerType_Color"));
                string White = bsupportwhite ? ResString.GetResString("EnumLayerType_White") : color;
				string Varnish =bsupportVarnish?ResString.GetResString("EnumLayerType_Varnish"):color;
				string layer = ResString.GetResString("ColorLayer");
				int layercount = ss.sBaseSetting.nWhiteInkLayer ==0?1:(int)ss.sBaseSetting.nWhiteInkLayer;
				for(int i = 0; i < layercount; i++)
				{
					uint layercolor = (ss.sBaseSetting.nLayerColorArray >>(2*i))&0x03;
					switch((EnumLayerType)layercolor)
					{
						case EnumLayerType.Color:
							tb.Rows.Add( new Row(new Cell[] {new Cell(layer+" "+ (i+1) +":"),new Cell(color)}));
							break;
						case EnumLayerType.White:
							tb.Rows.Add( new Row(new Cell[] {new Cell(layer+" "+ (i+1) +":"),new Cell(White)}));
							break;
						case EnumLayerType.Varnish:
							tb.Rows.Add( new Row(new Cell[] {new Cell(layer+" "+ (i+1) +":"),new Cell(Varnish)}));
							break;
                        default:
                            tb.Rows.Add(new Row(new Cell[] { new Cell(layer + " " + (i + 1) + ":"), new Cell(color2) }));
                            break;
					}
				}
				this.table.BeginUpdate();
				this.table.TableModel = tb;
				this.table.TableModel.RowHeight = 21;
				this.table.EndUpdate();
			}

			if(spp.bSupportWhiteInkYoffset)
			{
				this.checkBoxColorNoPrint.Checked = (ss.sBaseSetting.nLayerColorArray&0x01)!=0;
				this.checkBoxWhiteNoPrint.Checked = (ss.sBaseSetting.nLayerColorArray&0x02)!=0;
				this.checkBoxVarnishNoPrint.Checked = (ss.sBaseSetting.nLayerColorArray&0x04)!=0;
			}
			UIPreference.SetSelectIndexAndClampWithMax(this.cbo_MultipleInk,ss.sBaseSetting.multipleWriteInk);
            this.checkBox14plTo42pl.Checked = (ss.sBaseSetting.bitRegion & 1) != 0;

			this.isDirty = false;
		}
		public void OnGetPrinterSetting(ref SPrinterSetting ss)
		{
            //ss.sBaseSetting.bMirrorX = this.m_CheckBoxMirror.Checked;
            //ss.sBaseSetting.bReversePrint = CheckBoxReversePrint.Checked;
			ss.sBaseSetting.nSpotColor1Mask = this.spotColorMaskSetting1.SpotColorMask;
			ss.sBaseSetting.nSpotColor2Mask = this.spotColorMaskSetting2.SpotColorMask;
            if (spp.bSupportWhiteInk)
			{
                string color = ResString.GetResString("EnumLayerType_Color");
                string color2 = string.Format("{0}2", ResString.GetResString("EnumLayerType_Color"));
				string White = ResString.GetResString("EnumLayerType_White");
				string Varnish = ResString.GetResString("EnumLayerType_Varnish");

				ss.sBaseSetting.nWhiteInkLayer = (byte)(this.comboBox1.SelectedIndex + 1);
				int layerColorArray =  0;
				for(int i = 0; i < this.table.TableModel.Rows.Count;i++)
				{
					int layercolor = 0;
					Cell cell = this.table.TableModel.Rows[i].Cells[1];
#if false
					ComboBoxCellEditor editor =  (ComboBoxCellEditor)this.table.ColumnModel.GetCellEditor(1);
					for(int j = 0;j<editor.Items.Count;j++)
					{
						if(editor.Items[j].ToString() == cell.Text)
						{
							layercolor = j;
							break;
						}
					}
#else
				    if (cell.Text == color)
				        layercolor = (int) EnumLayerType.Color;//0;
					if(cell.Text ==  White)
                        layercolor = (int)EnumLayerType.White;//1;
					if(cell.Text == Varnish)
                        layercolor = (int)EnumLayerType.Varnish;//2;
                    if (cell.Text == color2)
                        layercolor = (int)EnumLayerType.Color2;//3;
#endif
					layerColorArray |= (layercolor<<(i*2));
				}
				ss.sBaseSetting.nLayerColorArray = (uint)layerColorArray;
			}
			
			if(spp.bSupportWhiteInkYoffset)
			{
				uint mask = 0;
				if(this.checkBoxColorNoPrint.Checked)
					mask |= 0x01;
				if(this.checkBoxWhiteNoPrint.Checked)
					mask |= 0x02;
				if(this.checkBoxVarnishNoPrint.Checked)
					mask |= 0x04;
				ss.sBaseSetting.nLayerColorArray = mask;
			}
			ss.sBaseSetting.multipleWriteInk = (byte)this.cbo_MultipleInk.SelectedIndex;
            if (this.checkBox14plTo42pl.Checked)
                ss.sBaseSetting.bitRegion |= 1;
            else
                ss.sBaseSetting.bitRegion &= 0xfffe;

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


		private void CheckBoxMirrorPrint_CheckedChanged(object sender, System.EventArgs e)
		{
            //if (!SPrinterProperty.IsGongZengMeasureBeforPrint())
            //{
            //    this.m_CheckBoxMirror.Checked = CheckBoxReversePrint.Checked;
            //}
		}

		
		public void SetGroupBoxStyle(Grouper ts)
		{
			this.GrouperTitleStyle = ts;
			this.spotColorMaskSetting1.GrouperTitleStyle = ts;
			this.spotColorMaskSetting2.GrouperTitleStyle = ts;
		}

		private void m_GroupBoxWhiteInk_Enter(object sender, System.EventArgs e)
		{
		
		}

		private void grouper3_Enter(object sender, System.EventArgs e)
		{
		
		}

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.table.BeginUpdate();
			bool bsupportwhite = (spp.nWhiteInkNum&0x0F) >0;
			bool bsupportVarnish = (spp.nWhiteInkNum>>4) > 0;

			string color = ResString.GetResString("EnumLayerType_Color");
			string spotcolor1 = bsupportwhite?ResString.GetResString("EnumLayerType_White"):color;
			string spotcolor2 = bsupportVarnish?ResString.GetResString("EnumLayerType_Varnish"):color;
			string layer = ResString.GetResString("ColorLayer");
			switch(comboBox1.SelectedIndex)
			{
				case 0:
					this.table.TableModel = new TableModel(new Row[] {
																		 new Row(new Cell[] {new Cell(layer+" 1:"),new Cell(color)})
																	 });
					break;			
				case 1:
					this.table.TableModel = new TableModel(new Row[] {	
																		 new Row(new Cell[] {new Cell(layer+" 1:"),new Cell(spotcolor1)}),
																		 new Row(new Cell[] {new Cell(layer+" 2:"),new Cell(color)})
																	 });
					break;
				case 2:
					this.table.TableModel = new TableModel(new Row[] {	
																		 new Row(new Cell[] {new Cell(layer+" 1:"),new Cell(spotcolor1)}),
																		 new Row(new Cell[] {new Cell(layer+" 2:"),new Cell(color)}),
																		 new Row(new Cell[] {new Cell(layer+" 3:"),new Cell(spotcolor2)})
																	 });
					break;
				case 3:
					this.table.TableModel = new TableModel(new Row[] {	
																		 new Row(new Cell[] {new Cell(layer+" 1:"),new Cell(color)}),
																		 new Row(new Cell[] {new Cell(layer+" 2:"),new Cell(spotcolor1)}),
																		 new Row(new Cell[] {new Cell(layer+" 3:"),new Cell(color)}),
																		 new Row(new Cell[] {new Cell(layer+" 4:"),new Cell(spotcolor2)})
																	 });
					break;
				case 4:
					this.table.TableModel = new TableModel(new Row[] {	
																		 new Row(new Cell[] {new Cell(layer+" 1:"),new Cell(spotcolor2)}),
																		 new Row(new Cell[] {new Cell(layer+" 2:"),new Cell(color)}),
																		 new Row(new Cell[] {new Cell(layer+" 3:"),new Cell(spotcolor1)}),
																		 new Row(new Cell[] {new Cell(layer+" 4:"),new Cell(color)}),
																		 new Row(new Cell[] {new Cell(layer+" 5:"),new Cell(spotcolor2)})
																	 });
					break;
				case 5:
					this.table.TableModel = new TableModel(new Row[] {	
																		 new Row(new Cell[] {new Cell(layer+" 1:"),new Cell(spotcolor1)}),
																		 new Row(new Cell[] {new Cell(layer+" 2:"),new Cell(color)}),
																		 new Row(new Cell[] {new Cell(layer+" 3:"),new Cell(spotcolor1)}),
																		 new Row(new Cell[] {new Cell(layer+" 4:"),new Cell(color)}),
																		 new Row(new Cell[] {new Cell(layer+" 5:"),new Cell(spotcolor2)}),
																		 new Row(new Cell[] {new Cell(layer+" 6:"),new Cell(spotcolor2)})
																	 });
					break;
				case 6:
					this.table.TableModel = new TableModel(new Row[] {	
																		 new Row(new Cell[] {new Cell(layer+" 1:"),new Cell(spotcolor1)}),
																		 new Row(new Cell[] {new Cell(layer+" 2:"),new Cell(color)}),
																		 new Row(new Cell[] {new Cell(layer+" 3:"),new Cell(spotcolor1)}),
																		 new Row(new Cell[] {new Cell(layer+" 4:"),new Cell(color)}),
																		 new Row(new Cell[] {new Cell(layer+" 5:"),new Cell(spotcolor2)}),
																		 new Row(new Cell[] {new Cell(layer+" 6:"),new Cell(spotcolor1)}),
																		 new Row(new Cell[] {new Cell(layer+" 7:"),new Cell(spotcolor2)})
																	 });
					break;
				case 7:
					this.table.TableModel = new TableModel(new Row[] {	
																		 new Row(new Cell[] {new Cell(layer+" 1:"),new Cell(spotcolor1)}),
																		 new Row(new Cell[] {new Cell(layer+" 2:"),new Cell(color)}),
																		 new Row(new Cell[] {new Cell(layer+" 3:"),new Cell(spotcolor1)}),
																		 new Row(new Cell[] {new Cell(layer+" 4:"),new Cell(color)}),
																		 new Row(new Cell[] {new Cell(layer+" 5:"),new Cell(spotcolor2)}),
																		 new Row(new Cell[] {new Cell(layer+" 6:"),new Cell(spotcolor1)}),
																		 new Row(new Cell[] {new Cell(layer+" 7:"),new Cell(color)}),
																		 new Row(new Cell[] {new Cell(layer+" 8:"),new Cell(spotcolor2)})
																	 });
					break;
			}
			this.table.TableModel.RowHeight = 21;
			this.table.EndUpdate();
		}

		private void checkBoxNoPrint_CheckedChanged(object sender, System.EventArgs e)
		{
		    if (!hasLoaded)
		        return;
            if (
                (!checkBoxVarnishNoPrint.Visible||(checkBoxVarnishNoPrint.Visible && this.checkBoxVarnishNoPrint.Checked))
                && (!checkBoxWhiteNoPrint.Visible||(checkBoxWhiteNoPrint.Visible&&this.checkBoxWhiteNoPrint.Checked))
                && (!checkBoxColorNoPrint.Visible || (checkBoxColorNoPrint.Visible && this.checkBoxColorNoPrint.Checked))
                )
			{
				(sender as CheckBox).Checked = false;
			}
		}
	}
}

