/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BYHXPrinterManager.GradientControls;
using BYHXPrinterManager.Main;
using PrinterStubC.Common;

namespace BYHXPrinterManager.Setting
{
	/// <summary>
	/// Summary description for SettingForm.
	/// </summary>
	public class SettingForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl m_TabControlSetting;
		private System.Windows.Forms.TabPage m_TabPagePrinterSetting;
		private System.Windows.Forms.TabPage m_TabPageCaliSetting;
		private System.Windows.Forms.TabPage m_TabPagePreference;
		private BYHXPrinterManager.Setting.BaseSetting m_BaseSetting;
		private System.Windows.Forms.Button m_ButtonOK;
		private BYHXPrinterManager.Setting.CalibrationSetting m_CalibrationSetting;
		private System.Windows.Forms.Button m_ButtonCancel;
		private BYHXPrinterManager.Setting.PreferenceSetting m_PreferenceSetting;
		private System.Windows.Forms.TabPage m_TabPageService;
		private BYHXPrinterManager.Setting.SeviceSetting m_SeviceSetting;
		private BYHXPrinterManager.Setting.EpsonBaseSetting m_epsonBaseSetting;
		private System.Windows.Forms.TabPage m_TabPageMoveSetting;
		private BYHXPrinterManager.Setting.MoveSetting m_MoveSetting;
		private System.Windows.Forms.TabPage m_TabPageWriteColor;
		private BYHXPrinterManager.Setting.SpotColorSetting_new spotColorSetting1;
        private TabPage m_tabPageDoublePrint;
        private DoublePrintSetting doublePrintSetting1;
        private TabPage m_TabPage_3DPrinter;
        private TabPage tabPageUserExtention;
        private UserExtensionSetting userExtensionSetting;
        private Printer3DSetting m_Printer3DSetting;
        private Panel panel1;
        private TabPage tabPageUVPowerLevel;
        private DefineUVPowerLevel defineUVPowerLevel1;
        private TabPage tabPagJobModes;
        private JobConfigModes jobConfigModes1;
        private TabPage tabPageMediaMode;
        private MediaConfigs mediaConfigs1;
        private TabPage tabPagePressureSet;
        private PressureSetting pressureSetting1;
        private TemperatureSetting temperatureSetting1;
        private TabPage m_tabPageCloseColorNozzle;
        private CloseColorNozzle_New closeColorNozzle1;
        private TabPage m_tabPageCustomCloseNozzle;
        private CustomCloseNozzle customCloseNozzle1;
        private TabPage m_tabPageWorkPosSetting;
        private WorkposSetting workposSetting1;
        private TabPage m_tabPageCustomDebugQuality;
        private DebugQuality debugQuality1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
		public SettingForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			//SetScale();
		}
		private void SetScale()
		{	
			SizeF sizef = new SizeF();
			sizef = Form.GetAutoScaleSize(this.Font);

			Size size = new Size();
			size.Height = Convert.ToInt32(sizef.Height);
			size.Width = Convert.ToInt32(sizef.Width);

			this.AutoScaleBaseSize = size;

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style2 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style3 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style4 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style5 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style6 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style7 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style8 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style9 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style10 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style11 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style12 = new BYHXPrinterManager.Style();
            this.m_TabControlSetting = new System.Windows.Forms.TabControl();
            this.m_TabPagePrinterSetting = new System.Windows.Forms.TabPage();
            this.m_epsonBaseSetting = new BYHXPrinterManager.Setting.EpsonBaseSetting();
            this.m_BaseSetting = new BYHXPrinterManager.Setting.BaseSetting();
            this.m_TabPageMoveSetting = new System.Windows.Forms.TabPage();
            this.m_MoveSetting = new BYHXPrinterManager.Setting.MoveSetting();
            this.m_TabPagePreference = new System.Windows.Forms.TabPage();
            this.m_PreferenceSetting = new BYHXPrinterManager.Setting.PreferenceSetting();
            this.m_TabPageCaliSetting = new System.Windows.Forms.TabPage();
            this.m_CalibrationSetting = new BYHXPrinterManager.Setting.CalibrationSetting();
            this.m_TabPageWriteColor = new System.Windows.Forms.TabPage();
            this.spotColorSetting1 = new BYHXPrinterManager.Setting.SpotColorSetting_new();
            this.m_tabPageDoublePrint = new System.Windows.Forms.TabPage();
            this.doublePrintSetting1 = new BYHXPrinterManager.Setting.DoublePrintSetting();
            this.m_TabPageService = new System.Windows.Forms.TabPage();
            this.m_SeviceSetting = new BYHXPrinterManager.Setting.SeviceSetting();
            this.m_TabPage_3DPrinter = new System.Windows.Forms.TabPage();
            this.m_Printer3DSetting = new BYHXPrinterManager.Setting.Printer3DSetting();
            this.tabPageUserExtention = new System.Windows.Forms.TabPage();
            this.userExtensionSetting = new BYHXPrinterManager.Setting.UserExtensionSetting();
            this.tabPageUVPowerLevel = new System.Windows.Forms.TabPage();
            this.defineUVPowerLevel1 = new BYHXPrinterManager.Setting.DefineUVPowerLevel();
            this.tabPagJobModes = new System.Windows.Forms.TabPage();
            this.jobConfigModes1 = new BYHXPrinterManager.Setting.JobConfigModes();
            this.tabPageMediaMode = new System.Windows.Forms.TabPage();
            this.mediaConfigs1 = new BYHXPrinterManager.Setting.MediaConfigs();
            this.tabPagePressureSet = new System.Windows.Forms.TabPage();
            this.temperatureSetting1 = new BYHXPrinterManager.Setting.TemperatureSetting();
            this.pressureSetting1 = new BYHXPrinterManager.Setting.PressureSetting();
            this.m_tabPageCloseColorNozzle = new System.Windows.Forms.TabPage();
            this.closeColorNozzle1 = new BYHXPrinterManager.Setting.CloseColorNozzle_New();
            this.m_tabPageCustomCloseNozzle = new System.Windows.Forms.TabPage();
            this.customCloseNozzle1 = new BYHXPrinterManager.Setting.CustomCloseNozzle();
            this.m_tabPageWorkPosSetting = new System.Windows.Forms.TabPage();
            this.workposSetting1 = new BYHXPrinterManager.Setting.WorkposSetting();
            this.m_tabPageCustomDebugQuality = new System.Windows.Forms.TabPage();
            this.debugQuality1 = new BYHXPrinterManager.Setting.DebugQuality();
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.m_ButtonCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_TabControlSetting.SuspendLayout();
            this.m_TabPagePrinterSetting.SuspendLayout();
            this.m_TabPageMoveSetting.SuspendLayout();
            this.m_TabPagePreference.SuspendLayout();
            this.m_TabPageCaliSetting.SuspendLayout();
            this.m_TabPageWriteColor.SuspendLayout();
            this.m_tabPageDoublePrint.SuspendLayout();
            this.m_TabPageService.SuspendLayout();
            this.m_TabPage_3DPrinter.SuspendLayout();
            this.tabPageUserExtention.SuspendLayout();
            this.tabPageUVPowerLevel.SuspendLayout();
            this.tabPagJobModes.SuspendLayout();
            this.tabPageMediaMode.SuspendLayout();
            this.tabPagePressureSet.SuspendLayout();
            this.m_tabPageCloseColorNozzle.SuspendLayout();
            this.m_tabPageCustomCloseNozzle.SuspendLayout();
            this.m_tabPageWorkPosSetting.SuspendLayout();
            this.m_tabPageCustomDebugQuality.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_TabControlSetting
            // 
            this.m_TabControlSetting.Controls.Add(this.m_TabPagePrinterSetting);
            this.m_TabControlSetting.Controls.Add(this.m_TabPageMoveSetting);
            this.m_TabControlSetting.Controls.Add(this.m_TabPagePreference);
            this.m_TabControlSetting.Controls.Add(this.m_TabPageCaliSetting);
            this.m_TabControlSetting.Controls.Add(this.m_TabPageWriteColor);
            this.m_TabControlSetting.Controls.Add(this.m_tabPageDoublePrint);
            this.m_TabControlSetting.Controls.Add(this.m_TabPageService);
            this.m_TabControlSetting.Controls.Add(this.m_TabPage_3DPrinter);
            this.m_TabControlSetting.Controls.Add(this.tabPageUserExtention);
            this.m_TabControlSetting.Controls.Add(this.tabPageUVPowerLevel);
            this.m_TabControlSetting.Controls.Add(this.tabPagJobModes);
            this.m_TabControlSetting.Controls.Add(this.tabPageMediaMode);
            this.m_TabControlSetting.Controls.Add(this.tabPagePressureSet);
            this.m_TabControlSetting.Controls.Add(this.m_tabPageCloseColorNozzle);
            this.m_TabControlSetting.Controls.Add(this.m_tabPageCustomCloseNozzle);
            this.m_TabControlSetting.Controls.Add(this.m_tabPageWorkPosSetting);
            this.m_TabControlSetting.Controls.Add(this.m_tabPageCustomDebugQuality);
            resources.ApplyResources(this.m_TabControlSetting, "m_TabControlSetting");
            this.m_TabControlSetting.Name = "m_TabControlSetting";
            this.m_TabControlSetting.SelectedIndex = 0;
            // 
            // m_TabPagePrinterSetting
            // 
            this.m_TabPagePrinterSetting.Controls.Add(this.m_epsonBaseSetting);
            this.m_TabPagePrinterSetting.Controls.Add(this.m_BaseSetting);
            resources.ApplyResources(this.m_TabPagePrinterSetting, "m_TabPagePrinterSetting");
            this.m_TabPagePrinterSetting.Name = "m_TabPagePrinterSetting";
            // 
            // m_epsonBaseSetting
            // 
            this.m_epsonBaseSetting.Divider = false;
            resources.ApplyResources(this.m_epsonBaseSetting, "m_epsonBaseSetting");
            style1.Color1 = System.Drawing.SystemColors.Control;
            style1.Color2 = System.Drawing.SystemColors.Control;
            this.m_epsonBaseSetting.GradientColors = style1;
            this.m_epsonBaseSetting.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_epsonBaseSetting.GrouperTitleStyle = null;
            this.m_epsonBaseSetting.IsDirty = false;
            this.m_epsonBaseSetting.Name = "m_epsonBaseSetting";
            // 
            // m_BaseSetting
            // 
            this.m_BaseSetting.Divider = false;
            resources.ApplyResources(this.m_BaseSetting, "m_BaseSetting");
            style2.Color1 = System.Drawing.SystemColors.Control;
            style2.Color2 = System.Drawing.SystemColors.Control;
            this.m_BaseSetting.GradientColors = style2;
            this.m_BaseSetting.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_BaseSetting.GrouperTitleStyle = null;
            this.m_BaseSetting.IsDirty = false;
            this.m_BaseSetting.Name = "m_BaseSetting";
            // 
            // m_TabPageMoveSetting
            // 
            this.m_TabPageMoveSetting.Controls.Add(this.m_MoveSetting);
            resources.ApplyResources(this.m_TabPageMoveSetting, "m_TabPageMoveSetting");
            this.m_TabPageMoveSetting.Name = "m_TabPageMoveSetting";
            // 
            // m_MoveSetting
            // 
            this.m_MoveSetting.Divider = false;
            style3.Color1 = System.Drawing.SystemColors.Control;
            style3.Color2 = System.Drawing.SystemColors.Control;
            this.m_MoveSetting.GradientColors = style3;
            this.m_MoveSetting.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_MoveSetting.GrouperTitleStyle = null;
            this.m_MoveSetting.IsDirty = false;
            resources.ApplyResources(this.m_MoveSetting, "m_MoveSetting");
            this.m_MoveSetting.Name = "m_MoveSetting";
            // 
            // m_TabPagePreference
            // 
            this.m_TabPagePreference.Controls.Add(this.m_PreferenceSetting);
            resources.ApplyResources(this.m_TabPagePreference, "m_TabPagePreference");
            this.m_TabPagePreference.Name = "m_TabPagePreference";
            // 
            // m_PreferenceSetting
            // 
            this.m_PreferenceSetting.Divider = false;
            resources.ApplyResources(this.m_PreferenceSetting, "m_PreferenceSetting");
            style4.Color1 = System.Drawing.SystemColors.Control;
            style4.Color2 = System.Drawing.SystemColors.Control;
            this.m_PreferenceSetting.GradientColors = style4;
            this.m_PreferenceSetting.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_PreferenceSetting.GrouperTitleStyle = null;
            this.m_PreferenceSetting.IsDirty = false;
            this.m_PreferenceSetting.Name = "m_PreferenceSetting";
            // 
            // m_TabPageCaliSetting
            // 
            this.m_TabPageCaliSetting.Controls.Add(this.m_CalibrationSetting);
            resources.ApplyResources(this.m_TabPageCaliSetting, "m_TabPageCaliSetting");
            this.m_TabPageCaliSetting.Name = "m_TabPageCaliSetting";
            // 
            // m_CalibrationSetting
            // 
            resources.ApplyResources(this.m_CalibrationSetting, "m_CalibrationSetting");
            this.m_CalibrationSetting.ColorCalibrationIndex = 0;
            this.m_CalibrationSetting.Divider = false;
            style5.Color1 = System.Drawing.SystemColors.Control;
            style5.Color2 = System.Drawing.SystemColors.Control;
            this.m_CalibrationSetting.GradientColors = style5;
            this.m_CalibrationSetting.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_CalibrationSetting.GrouperTitleStyle = null;
            this.m_CalibrationSetting.HorCalibrationMode = 0;
            this.m_CalibrationSetting.IsDirty = false;
            this.m_CalibrationSetting.IsQuickHorCalibration = false;
            this.m_CalibrationSetting.Name = "m_CalibrationSetting";
            this.m_CalibrationSetting.Platform = ((byte)(0));
            // 
            // m_TabPageWriteColor
            // 
            this.m_TabPageWriteColor.Controls.Add(this.spotColorSetting1);
            resources.ApplyResources(this.m_TabPageWriteColor, "m_TabPageWriteColor");
            this.m_TabPageWriteColor.Name = "m_TabPageWriteColor";
            // 
            // spotColorSetting1
            // 
            this.spotColorSetting1.Divider = false;
            style6.Color1 = System.Drawing.SystemColors.Control;
            style6.Color2 = System.Drawing.SystemColors.Control;
            this.spotColorSetting1.GradientColors = style6;
            this.spotColorSetting1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.spotColorSetting1.GrouperTitleStyle = null;
            this.spotColorSetting1.IsDirty = false;
            resources.ApplyResources(this.spotColorSetting1, "spotColorSetting1");
            this.spotColorSetting1.Name = "spotColorSetting1";
            // 
            // m_tabPageDoublePrint
            // 
            this.m_tabPageDoublePrint.BackColor = System.Drawing.SystemColors.Control;
            this.m_tabPageDoublePrint.Controls.Add(this.doublePrintSetting1);
            resources.ApplyResources(this.m_tabPageDoublePrint, "m_tabPageDoublePrint");
            this.m_tabPageDoublePrint.Name = "m_tabPageDoublePrint";
            // 
            // doublePrintSetting1
            // 
            this.doublePrintSetting1.Divider = false;
            style7.Color1 = System.Drawing.SystemColors.Control;
            style7.Color2 = System.Drawing.SystemColors.Control;
            this.doublePrintSetting1.GradientColors = style7;
            this.doublePrintSetting1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.doublePrintSetting1.GrouperTitleStyle = null;
            resources.ApplyResources(this.doublePrintSetting1, "doublePrintSetting1");
            this.doublePrintSetting1.Name = "doublePrintSetting1";
            // 
            // m_TabPageService
            // 
            this.m_TabPageService.Controls.Add(this.m_SeviceSetting);
            resources.ApplyResources(this.m_TabPageService, "m_TabPageService");
            this.m_TabPageService.Name = "m_TabPageService";
            // 
            // m_SeviceSetting
            // 
            this.m_SeviceSetting.Divider = false;
            resources.ApplyResources(this.m_SeviceSetting, "m_SeviceSetting");
            style8.Color1 = System.Drawing.SystemColors.Control;
            style8.Color2 = System.Drawing.SystemColors.Control;
            this.m_SeviceSetting.GradientColors = style8;
            this.m_SeviceSetting.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_SeviceSetting.GrouperTitleStyle = null;
            this.m_SeviceSetting.IsDirty = false;
            this.m_SeviceSetting.Name = "m_SeviceSetting";
            // 
            // m_TabPage_3DPrinter
            // 
            this.m_TabPage_3DPrinter.BackColor = System.Drawing.SystemColors.Control;
            this.m_TabPage_3DPrinter.Controls.Add(this.m_Printer3DSetting);
            resources.ApplyResources(this.m_TabPage_3DPrinter, "m_TabPage_3DPrinter");
            this.m_TabPage_3DPrinter.Name = "m_TabPage_3DPrinter";
            // 
            // m_Printer3DSetting
            // 
            this.m_Printer3DSetting.Divider = false;
            style9.Color1 = System.Drawing.SystemColors.Control;
            style9.Color2 = System.Drawing.SystemColors.Control;
            this.m_Printer3DSetting.GradientColors = style9;
            this.m_Printer3DSetting.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_Printer3DSetting.GrouperTitleStyle = null;
            this.m_Printer3DSetting.IsDirty = false;
            resources.ApplyResources(this.m_Printer3DSetting, "m_Printer3DSetting");
            this.m_Printer3DSetting.Name = "m_Printer3DSetting";
            // 
            // tabPageUserExtention
            // 
            this.tabPageUserExtention.Controls.Add(this.userExtensionSetting);
            resources.ApplyResources(this.tabPageUserExtention, "tabPageUserExtention");
            this.tabPageUserExtention.Name = "tabPageUserExtention";
            this.tabPageUserExtention.UseVisualStyleBackColor = true;
            // 
            // userExtensionSetting
            // 
            this.userExtensionSetting.BackColor = System.Drawing.SystemColors.Control;
            this.userExtensionSetting.IsDirty = false;
            resources.ApplyResources(this.userExtensionSetting, "userExtensionSetting");
            this.userExtensionSetting.Name = "userExtensionSetting";
            // 
            // tabPageUVPowerLevel
            // 
            this.tabPageUVPowerLevel.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageUVPowerLevel.Controls.Add(this.defineUVPowerLevel1);
            resources.ApplyResources(this.tabPageUVPowerLevel, "tabPageUVPowerLevel");
            this.tabPageUVPowerLevel.Name = "tabPageUVPowerLevel";
            // 
            // defineUVPowerLevel1
            // 
            resources.ApplyResources(this.defineUVPowerLevel1, "defineUVPowerLevel1");
            this.defineUVPowerLevel1.Name = "defineUVPowerLevel1";
            // 
            // tabPagJobModes
            // 
            this.tabPagJobModes.Controls.Add(this.jobConfigModes1);
            resources.ApplyResources(this.tabPagJobModes, "tabPagJobModes");
            this.tabPagJobModes.Name = "tabPagJobModes";
            this.tabPagJobModes.UseVisualStyleBackColor = true;
            // 
            // jobConfigModes1
            // 
            resources.ApplyResources(this.jobConfigModes1, "jobConfigModes1");
            this.jobConfigModes1.Name = "jobConfigModes1";
            // 
            // tabPageMediaMode
            // 
            this.tabPageMediaMode.Controls.Add(this.mediaConfigs1);
            resources.ApplyResources(this.tabPageMediaMode, "tabPageMediaMode");
            this.tabPageMediaMode.Name = "tabPageMediaMode";
            this.tabPageMediaMode.UseVisualStyleBackColor = true;
            // 
            // mediaConfigs1
            // 
            resources.ApplyResources(this.mediaConfigs1, "mediaConfigs1");
            this.mediaConfigs1.Name = "mediaConfigs1";
            // 
            // tabPagePressureSet
            // 
            this.tabPagePressureSet.BackColor = System.Drawing.SystemColors.Control;
            this.tabPagePressureSet.Controls.Add(this.temperatureSetting1);
            this.tabPagePressureSet.Controls.Add(this.pressureSetting1);
            resources.ApplyResources(this.tabPagePressureSet, "tabPagePressureSet");
            this.tabPagePressureSet.Name = "tabPagePressureSet";
            // 
            // temperatureSetting1
            // 
            resources.ApplyResources(this.temperatureSetting1, "temperatureSetting1");
            this.temperatureSetting1.Name = "temperatureSetting1";
            // 
            // pressureSetting1
            // 
            resources.ApplyResources(this.pressureSetting1, "pressureSetting1");
            this.pressureSetting1.Name = "pressureSetting1";
            // 
            // m_tabPageCloseColorNozzle
            // 
            this.m_tabPageCloseColorNozzle.BackColor = System.Drawing.SystemColors.Control;
            this.m_tabPageCloseColorNozzle.Controls.Add(this.closeColorNozzle1);
            resources.ApplyResources(this.m_tabPageCloseColorNozzle, "m_tabPageCloseColorNozzle");
            this.m_tabPageCloseColorNozzle.Name = "m_tabPageCloseColorNozzle";
            // 
            // closeColorNozzle1
            // 
            this.closeColorNozzle1.Divider = false;
            style10.Color1 = System.Drawing.SystemColors.Control;
            style10.Color2 = System.Drawing.SystemColors.Control;
            this.closeColorNozzle1.GradientColors = style10;
            this.closeColorNozzle1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.closeColorNozzle1.GrouperTitleStyle = null;
            resources.ApplyResources(this.closeColorNozzle1, "closeColorNozzle1");
            this.closeColorNozzle1.Name = "closeColorNozzle1";
            // 
            // m_tabPageCustomCloseNozzle
            // 
            this.m_tabPageCustomCloseNozzle.BackColor = System.Drawing.SystemColors.Control;
            this.m_tabPageCustomCloseNozzle.Controls.Add(this.customCloseNozzle1);
            resources.ApplyResources(this.m_tabPageCustomCloseNozzle, "m_tabPageCustomCloseNozzle");
            this.m_tabPageCustomCloseNozzle.Name = "m_tabPageCustomCloseNozzle";
            // 
            // customCloseNozzle1
            // 
            this.customCloseNozzle1.Divider = false;
            style11.Color1 = System.Drawing.SystemColors.Control;
            style11.Color2 = System.Drawing.SystemColors.Control;
            this.customCloseNozzle1.GradientColors = style11;
            this.customCloseNozzle1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.customCloseNozzle1.GrouperTitleStyle = null;
            resources.ApplyResources(this.customCloseNozzle1, "customCloseNozzle1");
            this.customCloseNozzle1.Name = "customCloseNozzle1";
            // 
            // m_tabPageWorkPosSetting
            // 
            this.m_tabPageWorkPosSetting.Controls.Add(this.workposSetting1);
            resources.ApplyResources(this.m_tabPageWorkPosSetting, "m_tabPageWorkPosSetting");
            this.m_tabPageWorkPosSetting.Name = "m_tabPageWorkPosSetting";
            this.m_tabPageWorkPosSetting.UseVisualStyleBackColor = true;
            // 
            // workposSetting1
            // 
            this.workposSetting1.Divider = false;
            resources.ApplyResources(this.workposSetting1, "workposSetting1");
            style12.Color1 = System.Drawing.SystemColors.Control;
            style12.Color2 = System.Drawing.SystemColors.Control;
            this.workposSetting1.GradientColors = style12;
            this.workposSetting1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.workposSetting1.GrouperTitleStyle = null;
            this.workposSetting1.Name = "workposSetting1";
            // 
            // m_tabPageCustomDebugQuality
            // 
            this.m_tabPageCustomDebugQuality.Controls.Add(this.debugQuality1);
            resources.ApplyResources(this.m_tabPageCustomDebugQuality, "m_tabPageCustomDebugQuality");
            this.m_tabPageCustomDebugQuality.Name = "m_tabPageCustomDebugQuality";
            this.m_tabPageCustomDebugQuality.UseVisualStyleBackColor = true;
            // 
            // debugQuality1
            // 
            resources.ApplyResources(this.debugQuality1, "debugQuality1");
            this.debugQuality1.Name = "debugQuality1";
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.m_ButtonOK, "m_ButtonOK");
            this.m_ButtonOK.Name = "m_ButtonOK";
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.m_ButtonCancel, "m_ButtonCancel");
            this.m_ButtonCancel.Name = "m_ButtonCancel";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_ButtonCancel);
            this.panel1.Controls.Add(this.m_ButtonOK);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // SettingForm
            // 
            this.CancelButton = this.m_ButtonCancel;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.m_TabControlSetting);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SettingForm";
            this.ShowInTaskbar = false;
            this.m_TabControlSetting.ResumeLayout(false);
            this.m_TabPagePrinterSetting.ResumeLayout(false);
            this.m_TabPageMoveSetting.ResumeLayout(false);
            this.m_TabPagePreference.ResumeLayout(false);
            this.m_TabPageCaliSetting.ResumeLayout(false);
            this.m_TabPageWriteColor.ResumeLayout(false);
            this.m_tabPageDoublePrint.ResumeLayout(false);
            this.m_TabPageService.ResumeLayout(false);
            this.m_TabPage_3DPrinter.ResumeLayout(false);
            this.tabPageUserExtention.ResumeLayout(false);
            this.tabPageUVPowerLevel.ResumeLayout(false);
            this.tabPagJobModes.ResumeLayout(false);
            this.tabPageMediaMode.ResumeLayout(false);
            this.tabPagePressureSet.ResumeLayout(false);
            this.m_tabPageCloseColorNozzle.ResumeLayout(false);
            this.m_tabPageCustomCloseNozzle.ResumeLayout(false);
            this.m_tabPageWorkPosSetting.ResumeLayout(false);
            this.m_tabPageCustomDebugQuality.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

	    private SPrinterProperty _printerProperty;
		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
		    try
		    {
		        _printerProperty = sp;
		        m_TabPagePrinterSetting.SuspendLayout();
		        this.m_TabControlSetting.SuspendLayout();
		        this.SuspendLayout();
		        if (sp.EPSONLCD_DEFINED)
		        {
		            this.m_BaseSetting.Dock = DockStyle.None;
		            this.m_BaseSetting.Visible = false;
		            this.m_epsonBaseSetting.Visible = true;
		            this.m_epsonBaseSetting.Dock = DockStyle.Fill;
		        }
		        else
		        {
		            this.m_epsonBaseSetting.Dock = DockStyle.None;
		            this.m_epsonBaseSetting.Visible = false;
		            this.m_BaseSetting.Visible = true;
		            this.m_BaseSetting.Dock = DockStyle.Fill;
		        }

		        this.m_TabControlSetting.ResumeLayout(false);
		        this.m_TabPagePrinterSetting.ResumeLayout(false);
		        this.ResumeLayout(false);

		        if (this.m_TabControlSetting.TabPages.Contains(this.m_TabPageCaliSetting))
		            this.m_TabControlSetting.TabPages.Remove(this.m_TabPageCaliSetting);

		        if (sp.EPSONLCD_DEFINED)
		        {
		            //if (this.m_TabControlSetting.TabPages.Contains(this.m_TabPageCaliSetting))
		            //    this.m_TabControlSetting.TabPages.Remove(this.m_TabPageCaliSetting);
		            if (this.m_TabControlSetting.TabPages.Contains(this.m_TabPageMoveSetting))
		                this.m_TabControlSetting.TabPages.Remove(this.m_TabPageMoveSetting);
		        }
#if !ALLWIN
		        //if (sp.IsDocan())
		        {
		            if (this.m_TabControlSetting.TabPages.Contains(this.tabPageUVPowerLevel))
		                this.m_TabControlSetting.TabPages.Remove(this.tabPageUVPowerLevel);
		        }
#endif
		        if (sp.EPSONLCD_DEFINED)
		            m_epsonBaseSetting.OnPrinterPropertyChange(sp);
		        else
		            m_BaseSetting.OnPrinterPropertyChange(sp);
		        if (this.m_TabControlSetting.TabPages.Contains(this.m_TabPageCaliSetting))
		            m_CalibrationSetting.OnPrinterPropertyChange(sp);
		        m_SeviceSetting.OnPrinterPropertyChange(sp);
		        m_MoveSetting.OnPrinterPropertyChange(sp);
		        spotColorSetting1.OnPrinterPropertyChange(sp);
		        this.m_PreferenceSetting.OnPrinterPropertyChange(sp);
		        this.m_Printer3DSetting.OnPrinterPropertyChange(sp);
		        this.userExtensionSetting.OnPrinterPropertyChange(sp);
		        doublePrintSetting1.OnPrinterPropertyChange(sp);
		        //			m_RealTimeSetting.OnPrinterPropertyChange(sp);
		        bool bNoRealPage =
		            ((sp.ePrinterHead == PrinterHeadEnum.Xaar_XJ128_80W) ||
		             (sp.ePrinterHead == PrinterHeadEnum.Xaar_XJ128_40W));
		        //			if( bNoRealPage && m_TabControlSetting.TabPages.Contains(m_TabPageRealTime))
		        //			{
		        //				m_TabControlSetting.TabPages.Remove(m_TabPageRealTime);
		        //			}
		        //			else if(!bNoRealPage && !m_TabControlSetting.TabPages.Contains(m_TabPageRealTime))
		        //			{
		        //				m_TabControlSetting.TabPages.Add(m_TabPageRealTime);
		        //			}
		        bool bFacUser = (PubFunc.GetUserPermission() != (int) UserPermission.Operator);
		        if (!bFacUser && m_TabControlSetting.TabPages.Contains(m_TabPageService))
		        {
		            m_TabControlSetting.TabPages.Remove(m_TabPageService);
		        }
		        else if (bFacUser && !m_TabControlSetting.TabPages.Contains(m_TabPageService))
		        {
		            m_TabControlSetting.TabPages.Add(m_TabPageService);
		        }

		        //bool isInwearSimpleUi = SPrinterProperty.IsInwearSimpleUi();
		        if (!(sp.bSupportWhiteInk))
		        {
		            this.m_TabControlSetting.TabPages.Remove(this.m_TabPageWriteColor);
		        }

		        m_TabControlSetting.SelectedIndex = 0;

		        SwitchToAdvancedMode(PubFunc.IsKingColorAdvancedMode || PubFunc.Is3DPrintMachine());

		        if (!PubFunc.SupportDoubleSidePrint)
		        {
		            this.m_TabControlSetting.TabPages.Remove(this.m_tabPageDoublePrint);
		        }

		        //判断是否为3D打印
		        if (!PubFunc.Is3DPrintMachine()
		            && !PubFunc.IsKINCOLOR_FLAT_UV()
		            && this.m_TabControlSetting.TabPages.Contains(this.m_TabPage_3DPrinter))
		        {
		            this.m_TabControlSetting.TabPages.Remove(this.m_TabPage_3DPrinter);
		        }

		        //判断是否需要显示用户扩展设置
		        if (!PubFunc.IsUserExtensionFormNeed() &&
		            this.m_TabControlSetting.TabPages.Contains(this.tabPageUserExtention))
		        {
		            this.m_TabControlSetting.TabPages.Remove(this.tabPageUserExtention);
		        }

		        //判断是否显示打印模式
		        if ((!UIFunctionOnOff.SupportPrintMode||!PubFunc.IsFactoryUser())
		            && this.m_TabControlSetting.TabPages.Contains(this.tabPagJobModes))
		        {
		            this.m_TabControlSetting.TabPages.Remove(this.tabPagJobModes);
		        }

		        if (!UIFunctionOnOff.SupportMediaMode || !PubFunc.IsFactoryUser()
		            && this.m_TabControlSetting.TabPages.Contains(this.tabPageMediaMode))
		        {
		            this.m_TabControlSetting.TabPages.Remove(this.tabPageMediaMode);
		        }

                if (!sp.ShowGzPurgeControl() && this.m_TabControlSetting.TabPages.Contains(tabPagePressureSet))
                {
                    this.m_TabControlSetting.TabPages.Remove(this.tabPagePressureSet);
                }
		        else
		        {
		            temperatureSetting1.Visible = !SPrinterProperty.IsGONGZENG_DOUBLE_SIDE();
		            pressureSetting1.OnPrinterPropertyChange(sp);
		        }

		        if (!UIFunctionOnOff.SupportCloseNozzle)
		        {
		            this.m_TabControlSetting.TabPages.Remove(this.m_tabPageCloseColorNozzle);
		        }

		        if (!PubFunc.IsCustomCloseNozzle())
		        {
		            this.m_TabControlSetting.TabPages.Remove(this.m_tabPageCustomCloseNozzle);
		        }

		        if (!SPrinterProperty.IsJianRui())
		        {
		            this.m_TabControlSetting.TabPages.Remove(this.m_tabPageWorkPosSetting);
		        }
		        else
		        {
                    workposSetting1.OnPrinterPropertyChange(sp);
		        }

		        if (!PubFunc.SupportDebugQuality())
                {
                    this.m_TabControlSetting.TabPages.Remove(this.m_tabPageCustomDebugQuality);
                }
                else
                {
                    debugQuality1.OnPrinterPropertyChange(sp);
                }
		    }
		    catch (Exception ex)
		    {
		        MessageBox.Show(ex.Message);
		    }
		}

        private void SwitchToAdvancedMode(bool bAdvancedMode)
        {
           if (!bAdvancedMode)
            {
                if (this.m_TabControlSetting.TabPages.Contains(m_TabPagePreference))
                    this.m_TabControlSetting.TabPages.Remove(m_TabPagePreference);
                if (this.m_TabControlSetting.TabPages.Contains(m_TabPageMoveSetting))
                    this.m_TabControlSetting.TabPages.Remove(m_TabPageMoveSetting);
            }
        }

		public void OnPrinterSettingChange( AllParam allParam)
		{
		    try
		    {
		        SPrinterSetting ss = allParam.PrinterSetting;
		        EpsonExAllParam epsonAllparam = allParam.EpsonAllParam;

		        if (allParam.PrinterProperty.EPSONLCD_DEFINED)
		            m_epsonBaseSetting.OnPrinterSettingChange(ss, epsonAllparam);
		        else
		        {
		            m_BaseSetting.OnPrinterSettingChange(ss);
		        }

		        m_BaseSetting.OnExtendedSettingsChange(allParam.ExtendedSettings);
		        if (this.m_TabControlSetting.TabPages.Contains(this.m_TabPageCaliSetting))
		            m_CalibrationSetting.OnPrinterSettingChange(allParam);
		        m_SeviceSetting.OnPrinterSettingChange(ss, epsonAllparam.sCaliConfig);
		        m_SeviceSetting.OnServiceSettingChange(allParam.SeviceSetting, epsonAllparam.sCaliConfig);
		        //m_RealTimeSetting.OnPrinterSettingChange(ss);
		        m_MoveSetting.OnPrinterSettingChange(ss);
		        spotColorSetting1.OnPrinterSettingChange(ss);
		        spotColorSetting1.OnExtendedSettingsChange(allParam.ExtendedSettings);
		        this.doublePrintSetting1.OnScorpionSettingsChanged(allParam.DoubleSidePrint);
		        this.m_Printer3DSetting.OnPrinterSettingChange(allParam);
		        this.userExtensionSetting.OnPrinterSettingChange(allParam);
		        defineUVPowerLevel1.OnPrinterSettingChange(allParam);

		        closeColorNozzle1.OnPrinterSettingChange(allParam);

		        customCloseNozzle1.OnPrinterSettingChange(ss);
		        if (SPrinterProperty.IsJianRui())
		        {
		            workposSetting1.OnPrinterSettingChange(allParam.PrinterSetting);
		        }
				if (PubFunc.SupportDebugQuality())
                {
                    debugQuality1.OnPrinterSettingChange(allParam);
                }

		    }
		    catch (Exception ex)
		    {
		        MessageBox.Show(ex.Message);
		    }
		}
		public void OnPreferenceChange( UIPreference up)
		{
		    try
		    {
                if (_printerProperty.EPSONLCD_DEFINED)
                    m_epsonBaseSetting.OnPreferenceChange(up);
                else
                    m_BaseSetting.OnPreferenceChange(up);
		        if (this.m_TabControlSetting.TabPages.Contains(this.m_TabPageCaliSetting))
                    m_CalibrationSetting.OnPreferenceChange(up);
                m_PreferenceSetting.OnPreferenceChange(up);
                //m_RealTimeSetting.OnPreferenceChange(up);
                m_MoveSetting.OnPreferenceChange(up);
                spotColorSetting1.OnPreferenceChange(up);
                doublePrintSetting1.OnPreferenceChange(up);
                m_Printer3DSetting.OnPreferenceChange(up);
                userExtensionSetting.OnPreferenceChange(up);
		        if (SPrinterProperty.IsJianRui())
		        {
		            workposSetting1.OnPreferenceChange(up);
		        }
           }
		    catch (Exception ex)
		    {
		        MessageBox.Show(ex.Message);
		    }
        }
		public void OnGetPrinterSetting(ref AllParam allParam,ref bool bChangeProperty)
		{
		    try
		    {
		        if (_printerProperty.EPSONLCD_DEFINED)
		        {
		            m_epsonBaseSetting.OnGetPrinterSetting(ref allParam.PrinterSetting, ref allParam.EpsonAllParam);
		        }
		        else
		        {
		            m_BaseSetting.OnGetPrinterSetting(ref allParam.PrinterSetting);
		        }

		        m_BaseSetting.OnGetExtendedSettingsChange(ref allParam.ExtendedSettings);

		        if (this.m_TabControlSetting.TabPages.Contains(this.m_TabPageCaliSetting))
		            m_CalibrationSetting.OnGetPrinterSetting(allParam);
		        m_PreferenceSetting.OnGetPreference(ref allParam.Preference);
		        m_SeviceSetting.OnGetProperty(ref allParam.PrinterProperty, ref bChangeProperty);
		        //m_RealTimeSetting.OnGetPrinterSetting(ref allParam.PrinterSetting);
		        m_MoveSetting.OnGetPrinterSetting(ref allParam.PrinterSetting);
		        if (allParam.PrinterProperty.bSupportWhiteInk || allParam.PrinterProperty.bSupportWhiteInkYoffset)
		        {
		            spotColorSetting1.OnGetPrinterSetting(ref allParam.PrinterSetting);
		            spotColorSetting1.OnGetExtendedSettingsChange(ref allParam.ExtendedSettings);
		        }

		        if (_printerProperty.EPSONLCD_DEFINED)
		        {
		            m_SeviceSetting.OnGetServiceSetting(ref allParam.EpsonAllParam.sCaliConfig);
		            EpsonLCD.SetCaliConfig(allParam.EpsonAllParam.sCaliConfig);
		        }

		        //else
		        {
		            SSeviceSetting sSeviceSet = allParam.SeviceSetting;
		            m_SeviceSetting.OnGetServiceSetting(ref sSeviceSet);
		            if (allParam.Preference.ScanningAxis == CoreConst.AXIS_X
		                || allParam.Preference.ScanningAxis == CoreConst.AXIS_4)
		            {
		                sSeviceSet.scanningAxis = allParam.Preference.ScanningAxis;
		            }
		            else
		            {
		                sSeviceSet.scanningAxis = CoreConst.AXIS_X;
		            }

		            CoreInterface.SetSeviceSetting(ref sSeviceSet);
		            allParam.SeviceSetting = sSeviceSet;
		        }
		        SDoubleSidePrint doubleSide = allParam.DoubleSidePrint;
		        doublePrintSetting1.OnGetScorpionSettings(ref doubleSide);
		        allParam.DoubleSidePrint = doubleSide;

		        //获取3D打印页签中的控件值
		        m_Printer3DSetting.OnGetPrinterSetting(allParam);
		        //获取用户设置的值
		        userExtensionSetting.OnGetPrinterSetting(allParam);
		        defineUVPowerLevel1.OnGetPrinterSetting(ref allParam.PrinterSetting, ref allParam.UvPowerLevelMap);
		        //判断是否显示打印模式
		        if ((UIFunctionOnOff.SupportPrintMode && PubFunc.IsFactoryUser()))
		        {
		            jobConfigModes1.OnGetPrinterSetting();
		        }

		        if (UIFunctionOnOff.SupportMediaMode)
		        {
		            mediaConfigs1.OnGetPrinterSetting();
		        }

		        if (UIFunctionOnOff.SupportCloseNozzle)
		        {
		            closeColorNozzle1.OnGetPrinterSetting(ref allParam);
		        }

		        if (SPrinterProperty.IsJianRui())
		        {
		            workposSetting1.OnGetPrinterSetting(ref allParam.PrinterSetting);
		        }
				if (UIFunctionOnOff.SupportDebugQuality)
                {
                    debugQuality1.OnGetPrinterSetting(ref allParam);
                }

		    }
		    catch (Exception ex)
		    {
		        MessageBox.Show(ex.Message);
		    }
		}
				

		public void OnRealTimeChange()
		{
		}

		public void SetPrinterStatusChanged(JetStatusEnum status)
		{
		    try
		    {
                if (_printerProperty.EPSONLCD_DEFINED)
                    m_epsonBaseSetting.SetPrinterStatusChanged(status);
                else
                    m_BaseSetting.SetPrinterStatusChanged(status);
                m_MoveSetting.SetPrinterStatusChanged(status);
            }
		    catch (Exception ex)
		    {
		        MessageBox.Show(ex.Message);
		    }
		}
		protected override void WndProc(ref Message m)
		{  
			base.WndProc(ref m);
//			MyWndProc.PaintFormCaption(ref m,this,false);
		}
		public void SetGroupBoxStyle(Grouper ts)
		{
		    try
		    {
                this.m_BaseSetting.GrouperTitleStyle = ts;
                this.m_CalibrationSetting.GrouperTitleStyle = ts;
                this.m_epsonBaseSetting.GrouperTitleStyle = ts;
                this.m_MoveSetting.GrouperTitleStyle = ts;
                this.spotColorSetting1.SetGroupBoxStyle(ts);
                this.m_Printer3DSetting.SetGroupBoxStyle(ts);
            }
		    catch (Exception ex)
		    {
		        MessageBox.Show(ex.Message);
		    }
		}

	}
}
