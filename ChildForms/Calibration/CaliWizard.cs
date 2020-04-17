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
using System.Reflection;
using System.IO;
using BYHXControls;
using BYHXPrinterManager.GradientControls;
using BYHXPrinterManager.Setting;


namespace BYHXPrinterManager.Calibration
{
	/// <summary>
	/// Summary description for CaliWizard.
	/// </summary>
	public class CaliWizard : System.Windows.Forms.Form
	{
		private int m_subStep = 0;
		private SPrinterSetting m_CaliSetting;
		private IPrinterChange m_iPrinterChange;
		private JetStatusEnum  m_curStatus = JetStatusEnum.Ready;
        private bool hasLoaded = false;
        private byte _curPlatform = CoreConst.AXIS_X;

		private BYHXControls.Wizard m_WizardCalibration;
		private BYHXControls.WizardPage m_WizardPageWelcom;
		private BYHXControls.WizardPage m_WizardPageFinish;
		private BYHXPrinterManager.Setting.CalibrationSetting_Layout m_CalibrationSettingHor;
		private BYHXControls.WizardPage m_WizardPageCali;
		private BYHXControls.WizardPage m_WizardPageMechanic;
		private System.Windows.Forms.Button m_ButtonAngle;
		private System.Windows.Forms.Button m_ButtonVerticalCheck;
		private System.Windows.Forms.Button m_ButtonNozzleCheck;
		private System.Windows.Forms.Button m_ButtonCrossCheck;
        private Grouper grouperMechanic;
        private RadioButton radioButtonCrossCheck;
        private RadioButton radioButtonNozzleCheck;
        private RadioButton radioButtonVerticalCheck;
        private RadioButton radioButtonAngle;
        private Panel panelChoosePlate;
        private ComboBox comboBoxChoosePlate;
        private Label labelChoosePlate;
        private GroupBox groupBoxCameraCari;
        private Button buttonPrintCameraCari;
        private NumericUpDown numYoriginOffset;
        private Label labelYoriginOffset;
        private NumericUpDown numXoriginOffset;
        private Label label1;
	    private BackgroundWorker zhunzhanCameraCariWorker;
	    private volatile bool _abortPrint = false;
        private Button buttonAbort;
        private Button m_ButtonOverallCheck;
        private Button m_ButtonOverLapCheck;
        private FlowLayoutPanel flowLayoutPanel1;
        private Panel panel1;
        private Button btnWelcomePage;
        private Button btnFinishPage;
        private Button btnCaliPage;
        private Button btnMechanicPage;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CaliWizard()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			//SetScale();
            InitializeLocalize(); 
            this.Load += new EventHandler(CaliWizard_Load);
            zhunzhanCameraCariWorker=new BackgroundWorker();
            zhunzhanCameraCariWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(zhunzhanCameraCariWorker_RunWorkerCompleted);
            zhunzhanCameraCariWorker.DoWork += new DoWorkEventHandler(zhunzhanCameraCariWorker_DoWork);
		}

        void zhunzhanCameraCariWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // 发送校准打印命令
            _curPlatform = comboBoxChoosePlate.SelectedIndex == 0 ? CoreConst.AXIS_X : CoreConst.AXIS_4;
            GetOriginValue();
            CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.ZhuozhanCameraOrigin, 4, ref m_CaliSetting, true, _curPlatform);
            // 等待打印结束后,发送回原点命令
            while (true)
            {
                JetStatusEnum status = CoreInterface.GetBoardStatus();
                if (status == JetStatusEnum.Ready)
                {
                    CoreInterface.SendJetCommand((int)JetCmdEnum.BackToHomePointY, (int)AxisDir.Y);
                    break;
                }
                if (_abortPrint)
                    return;
            }
            // 等待卓展拍照后接口返回数据
            //while (true)
            //{
            //    if (_abortPrint)
            //    {
            //        return;
            //    }
            //    SysStorageUnit.SysRRead(Application.StartupPath, 101, 104);
            //    if ((int)SysStorageUnit.SysRegister[104] != 1) continue;
            //    var x = SysStorageUnit.SysRegister[101];
            //    var y = SysStorageUnit.SysRegister[102];
            //    var angle = (float)SysStorageUnit.SysRegister[103];
            //    byte[] buf = new byte[128];
            //    int ret = CoreInterface.RotationImage(job.FileLocation, buf, angle);
            //    if (ret > 0)
            //    {
            //        prtPath = System.Text.Encoding.Default.GetString(buf).Substring(0, ret);
            //        job.FileLocation = prtPath;
            //    }
            //    allParam.PrinterSetting.sFrequencySetting.fXOrigin = (float)x;
            //    allParam.PrinterSetting.sBaseSetting.fYOrigin = (float)y;
            //    SysStorageUnit.SysRegister[104] = 0;
            //    SysStorageUnit.SysRWrite(Application.StartupPath, 104, 104);
            //    break;
            //} 
        }

        void zhunzhanCameraCariWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonPrintCameraCari.Enabled = true;
            _abortPrint = false;
        }
        void CaliWizard_Load(object sender, EventArgs e)
        {
            panelChoosePlate.Visible = false; 
            groupBoxCameraCari.Visible =PubFunc.IsZhuoZhan();
            comboBoxChoosePlate.SelectedIndex = 0;
            if (m_iPrinterChange.GetAllParam().PrinterSetting.sExtensionSetting.bIsNewCalibration == 0)
                m_subStep = 2;
            hasLoaded = true;
            this.m_WizardCalibration.Size = new Size(this.m_WizardCalibration.Width, this.m_WizardCalibration.Height - 1);
            //if (PubFunc.IsZhuoZhan())
            //    comboBoxChoosePlate_SelectedIndexChanged(sender, e);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CaliWizard));
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style2 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style3 = new BYHXPrinterManager.Style();
            this.m_WizardCalibration = new BYHXControls.Wizard();
            this.m_WizardPageWelcom = new BYHXControls.WizardPage();
            this.groupBoxCameraCari = new System.Windows.Forms.GroupBox();
            this.buttonAbort = new System.Windows.Forms.Button();
            this.buttonPrintCameraCari = new System.Windows.Forms.Button();
            this.numYoriginOffset = new System.Windows.Forms.NumericUpDown();
            this.labelYoriginOffset = new System.Windows.Forms.Label();
            this.numXoriginOffset = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.panelChoosePlate = new System.Windows.Forms.Panel();
            this.comboBoxChoosePlate = new System.Windows.Forms.ComboBox();
            this.labelChoosePlate = new System.Windows.Forms.Label();
            this.m_WizardPageFinish = new BYHXControls.WizardPage();
            this.m_WizardPageCali = new BYHXControls.WizardPage();
            this.m_CalibrationSettingHor = new BYHXPrinterManager.Setting.CalibrationSetting_Layout();
            this.m_WizardPageMechanic = new BYHXControls.WizardPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.m_ButtonAngle = new System.Windows.Forms.Button();
            this.m_ButtonVerticalCheck = new System.Windows.Forms.Button();
            this.m_ButtonNozzleCheck = new System.Windows.Forms.Button();
            this.m_ButtonCrossCheck = new System.Windows.Forms.Button();
            this.m_ButtonOverallCheck = new System.Windows.Forms.Button();
            this.m_ButtonOverLapCheck = new System.Windows.Forms.Button();
            this.grouperMechanic = new BYHXPrinterManager.GradientControls.Grouper();
            this.radioButtonCrossCheck = new System.Windows.Forms.RadioButton();
            this.radioButtonNozzleCheck = new System.Windows.Forms.RadioButton();
            this.radioButtonVerticalCheck = new System.Windows.Forms.RadioButton();
            this.radioButtonAngle = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFinishPage = new System.Windows.Forms.Button();
            this.btnCaliPage = new System.Windows.Forms.Button();
            this.btnMechanicPage = new System.Windows.Forms.Button();
            this.btnWelcomePage = new System.Windows.Forms.Button();
            this.m_WizardCalibration.SuspendLayout();
            this.m_WizardPageWelcom.SuspendLayout();
            this.groupBoxCameraCari.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYoriginOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numXoriginOffset)).BeginInit();
            this.panelChoosePlate.SuspendLayout();
            this.m_WizardPageCali.SuspendLayout();
            this.m_WizardPageMechanic.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.grouperMechanic.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_WizardCalibration
            // 
            this.m_WizardCalibration.Controls.Add(this.m_WizardPageWelcom);
            this.m_WizardCalibration.Controls.Add(this.m_WizardPageFinish);
            this.m_WizardCalibration.Controls.Add(this.m_WizardPageCali);
            this.m_WizardCalibration.Controls.Add(this.m_WizardPageMechanic);
            resources.ApplyResources(this.m_WizardCalibration, "m_WizardCalibration");
            this.m_WizardCalibration.HelpEnabled = true;
            this.m_WizardCalibration.Name = "m_WizardCalibration";
            this.m_WizardCalibration.Pages.AddRange(new BYHXControls.WizardPage[] {
            this.m_WizardPageWelcom,
            this.m_WizardPageMechanic,
            this.m_WizardPageCali,
            this.m_WizardPageFinish});
            this.m_WizardCalibration.BeforeSwitchPages += new BYHXControls.Wizard.BeforeSwitchPagesEventHandler(this.m_WizardCalibration_BeforeSwitchPages);
            this.m_WizardCalibration.AfterSwitchPages += new BYHXControls.Wizard.AfterSwitchPagesEventHandler(this.m_WizardCalibration_AfterSwitchPages);
            this.m_WizardCalibration.Cancel += new System.ComponentModel.CancelEventHandler(this.m_WizardCalibration_Cancel);
            this.m_WizardCalibration.Finish += new System.EventHandler(this.m_WizardCalibration_Finish);
            this.m_WizardCalibration.Help += new System.EventHandler(this.m_WizardCalibration_Help);
            this.m_WizardCalibration.Save += new System.EventHandler(this.m_WizardCalibration_Save);
            // 
            // m_WizardPageWelcom
            // 
            this.m_WizardPageWelcom.Controls.Add(this.groupBoxCameraCari);
            this.m_WizardPageWelcom.Controls.Add(this.panelChoosePlate);
            this.m_WizardPageWelcom.Description = "Starting Calibration......";
            resources.ApplyResources(this.m_WizardPageWelcom, "m_WizardPageWelcom");
            this.m_WizardPageWelcom.Name = "m_WizardPageWelcom";
            this.m_WizardPageWelcom.Style = BYHXControls.WizardPageStyle.Welcome;
            this.m_WizardPageWelcom.Title = "Calibration Wizard";
            // 
            // groupBoxCameraCari
            // 
            this.groupBoxCameraCari.Controls.Add(this.buttonAbort);
            this.groupBoxCameraCari.Controls.Add(this.buttonPrintCameraCari);
            this.groupBoxCameraCari.Controls.Add(this.numYoriginOffset);
            this.groupBoxCameraCari.Controls.Add(this.labelYoriginOffset);
            this.groupBoxCameraCari.Controls.Add(this.numXoriginOffset);
            this.groupBoxCameraCari.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBoxCameraCari, "groupBoxCameraCari");
            this.groupBoxCameraCari.Name = "groupBoxCameraCari";
            this.groupBoxCameraCari.TabStop = false;
            // 
            // buttonAbort
            // 
            resources.ApplyResources(this.buttonAbort, "buttonAbort");
            this.buttonAbort.Name = "buttonAbort";
            this.buttonAbort.UseVisualStyleBackColor = true;
            this.buttonAbort.Click += new System.EventHandler(this.buttonAbort_Click);
            // 
            // buttonPrintCameraCari
            // 
            resources.ApplyResources(this.buttonPrintCameraCari, "buttonPrintCameraCari");
            this.buttonPrintCameraCari.Name = "buttonPrintCameraCari";
            this.buttonPrintCameraCari.UseVisualStyleBackColor = true;
            this.buttonPrintCameraCari.Click += new System.EventHandler(this.buttonPrintCameraCari_Click);
            // 
            // numYoriginOffset
            // 
            resources.ApplyResources(this.numYoriginOffset, "numYoriginOffset");
            this.numYoriginOffset.Name = "numYoriginOffset";
            // 
            // labelYoriginOffset
            // 
            resources.ApplyResources(this.labelYoriginOffset, "labelYoriginOffset");
            this.labelYoriginOffset.Name = "labelYoriginOffset";
            // 
            // numXoriginOffset
            // 
            resources.ApplyResources(this.numXoriginOffset, "numXoriginOffset");
            this.numXoriginOffset.Name = "numXoriginOffset";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panelChoosePlate
            // 
            this.panelChoosePlate.BackColor = System.Drawing.Color.Transparent;
            this.panelChoosePlate.Controls.Add(this.comboBoxChoosePlate);
            this.panelChoosePlate.Controls.Add(this.labelChoosePlate);
            resources.ApplyResources(this.panelChoosePlate, "panelChoosePlate");
            this.panelChoosePlate.Name = "panelChoosePlate";
            // 
            // comboBoxChoosePlate
            // 
            this.comboBoxChoosePlate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChoosePlate.FormattingEnabled = true;
            this.comboBoxChoosePlate.Items.AddRange(new object[] {
            resources.GetString("comboBoxChoosePlate.Items"),
            resources.GetString("comboBoxChoosePlate.Items1")});
            resources.ApplyResources(this.comboBoxChoosePlate, "comboBoxChoosePlate");
            this.comboBoxChoosePlate.Name = "comboBoxChoosePlate";
            this.comboBoxChoosePlate.SelectedIndexChanged += new System.EventHandler(this.comboBoxChoosePlate_SelectedIndexChanged);
            // 
            // labelChoosePlate
            // 
            resources.ApplyResources(this.labelChoosePlate, "labelChoosePlate");
            this.labelChoosePlate.Name = "labelChoosePlate";
            // 
            // m_WizardPageFinish
            // 
            this.m_WizardPageFinish.Description = "Calibration has been finished!";
            resources.ApplyResources(this.m_WizardPageFinish, "m_WizardPageFinish");
            this.m_WizardPageFinish.Name = "m_WizardPageFinish";
            this.m_WizardPageFinish.Style = BYHXControls.WizardPageStyle.Finish;
            this.m_WizardPageFinish.Title = "Calibration Wizard";
            // 
            // m_WizardPageCali
            // 
            this.m_WizardPageCali.Controls.Add(this.m_CalibrationSettingHor);
            this.m_WizardPageCali.Description = "Fill the value as the printed image.";
            resources.ApplyResources(this.m_WizardPageCali, "m_WizardPageCali");
            this.m_WizardPageCali.Name = "m_WizardPageCali";
            this.m_WizardPageCali.Title = "Calibration Wizard:Horizontal";
            // 
            // m_CalibrationSettingHor
            // 
            resources.ApplyResources(this.m_CalibrationSettingHor, "m_CalibrationSettingHor");
            this.m_CalibrationSettingHor.ColorCalibrationIndex = 0;
            this.m_CalibrationSettingHor.Divider = false;
            style1.Color1 = System.Drawing.SystemColors.Control;
            style1.Color2 = System.Drawing.SystemColors.Control;
            this.m_CalibrationSettingHor.GradientColors = style1;
            this.m_CalibrationSettingHor.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_CalibrationSettingHor.GrouperTitleStyle = null;
            this.m_CalibrationSettingHor.HorCalibrationMode = 0;
            this.m_CalibrationSettingHor.IsDirty = false;
            this.m_CalibrationSettingHor.IsQuickHorCalibration = false;
            this.m_CalibrationSettingHor.Name = "m_CalibrationSettingHor";
            this.m_CalibrationSettingHor.Platform = ((byte)(0));
            // 
            // m_WizardPageMechanic
            // 
            this.m_WizardPageMechanic.Controls.Add(this.flowLayoutPanel1);
            this.m_WizardPageMechanic.Controls.Add(this.grouperMechanic);
            this.m_WizardPageMechanic.Description = "Adjust head with mechanical tools.";
            resources.ApplyResources(this.m_WizardPageMechanic, "m_WizardPageMechanic");
            this.m_WizardPageMechanic.Name = "m_WizardPageMechanic";
            this.m_WizardPageMechanic.Title = "Mechanical Check";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.m_ButtonAngle);
            this.flowLayoutPanel1.Controls.Add(this.m_ButtonVerticalCheck);
            this.flowLayoutPanel1.Controls.Add(this.m_ButtonNozzleCheck);
            this.flowLayoutPanel1.Controls.Add(this.m_ButtonCrossCheck);
            this.flowLayoutPanel1.Controls.Add(this.m_ButtonOverallCheck);
            this.flowLayoutPanel1.Controls.Add(this.m_ButtonOverLapCheck);
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // m_ButtonAngle
            // 
            resources.ApplyResources(this.m_ButtonAngle, "m_ButtonAngle");
            this.m_ButtonAngle.Name = "m_ButtonAngle";
            this.m_ButtonAngle.Click += new System.EventHandler(this.m_ButtonAngle_Click);
            // 
            // m_ButtonVerticalCheck
            // 
            resources.ApplyResources(this.m_ButtonVerticalCheck, "m_ButtonVerticalCheck");
            this.m_ButtonVerticalCheck.Name = "m_ButtonVerticalCheck";
            this.m_ButtonVerticalCheck.Click += new System.EventHandler(this.m_ButtonVerticalCheck_Click);
            // 
            // m_ButtonNozzleCheck
            // 
            resources.ApplyResources(this.m_ButtonNozzleCheck, "m_ButtonNozzleCheck");
            this.m_ButtonNozzleCheck.Name = "m_ButtonNozzleCheck";
            this.m_ButtonNozzleCheck.Click += new System.EventHandler(this.m_ButtonNozzleCheck_Click);
            // 
            // m_ButtonCrossCheck
            // 
            resources.ApplyResources(this.m_ButtonCrossCheck, "m_ButtonCrossCheck");
            this.m_ButtonCrossCheck.Name = "m_ButtonCrossCheck";
            this.m_ButtonCrossCheck.Click += new System.EventHandler(this.m_ButtonCrossCheck_Click);
            // 
            // m_ButtonOverallCheck
            // 
            resources.ApplyResources(this.m_ButtonOverallCheck, "m_ButtonOverallCheck");
            this.m_ButtonOverallCheck.Name = "m_ButtonOverallCheck";
            this.m_ButtonOverallCheck.Click += new System.EventHandler(this.m_ButtonOverallCheck_Click);
            // 
            // m_ButtonOverLapCheck
            // 
            resources.ApplyResources(this.m_ButtonOverLapCheck, "m_ButtonOverLapCheck");
            this.m_ButtonOverLapCheck.Name = "m_ButtonOverLapCheck";
            this.m_ButtonOverLapCheck.Click += new System.EventHandler(this.m_ButtonOverLapCheck_Click);
            // 
            // grouperMechanic
            // 
            this.grouperMechanic.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperMechanic.BorderThickness = 1F;
            this.grouperMechanic.Controls.Add(this.radioButtonCrossCheck);
            this.grouperMechanic.Controls.Add(this.radioButtonNozzleCheck);
            this.grouperMechanic.Controls.Add(this.radioButtonVerticalCheck);
            this.grouperMechanic.Controls.Add(this.radioButtonAngle);
            style2.Color1 = System.Drawing.Color.LightBlue;
            style2.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperMechanic.GradientColors = style2;
            this.grouperMechanic.GroupImage = null;
            resources.ApplyResources(this.grouperMechanic, "grouperMechanic");
            this.grouperMechanic.Name = "grouperMechanic";
            this.grouperMechanic.PaintGroupBox = false;
            this.grouperMechanic.RoundCorners = 10;
            this.grouperMechanic.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMechanic.ShadowControl = false;
            this.grouperMechanic.ShadowThickness = 3;
            this.grouperMechanic.TabStop = false;
            style3.Color1 = System.Drawing.Color.LightBlue;
            style3.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperMechanic.TitileGradientColors = style3;
            this.grouperMechanic.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouperMechanic.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // radioButtonCrossCheck
            // 
            this.radioButtonCrossCheck.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.radioButtonCrossCheck, "radioButtonCrossCheck");
            this.radioButtonCrossCheck.Name = "radioButtonCrossCheck";
            this.radioButtonCrossCheck.UseVisualStyleBackColor = false;
            // 
            // radioButtonNozzleCheck
            // 
            this.radioButtonNozzleCheck.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.radioButtonNozzleCheck, "radioButtonNozzleCheck");
            this.radioButtonNozzleCheck.Name = "radioButtonNozzleCheck";
            this.radioButtonNozzleCheck.UseVisualStyleBackColor = false;
            // 
            // radioButtonVerticalCheck
            // 
            this.radioButtonVerticalCheck.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.radioButtonVerticalCheck, "radioButtonVerticalCheck");
            this.radioButtonVerticalCheck.Name = "radioButtonVerticalCheck";
            this.radioButtonVerticalCheck.UseVisualStyleBackColor = false;
            // 
            // radioButtonAngle
            // 
            this.radioButtonAngle.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonAngle.Checked = true;
            resources.ApplyResources(this.radioButtonAngle, "radioButtonAngle");
            this.radioButtonAngle.Name = "radioButtonAngle";
            this.radioButtonAngle.TabStop = true;
            this.radioButtonAngle.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnFinishPage);
            this.panel1.Controls.Add(this.btnCaliPage);
            this.panel1.Controls.Add(this.btnMechanicPage);
            this.panel1.Controls.Add(this.btnWelcomePage);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnFinishPage
            // 
            resources.ApplyResources(this.btnFinishPage, "btnFinishPage");
            this.btnFinishPage.Name = "btnFinishPage";
            this.btnFinishPage.UseVisualStyleBackColor = true;
            this.btnFinishPage.Click += new System.EventHandler(this.btnFinishPage_Click);
            // 
            // btnCaliPage
            // 
            resources.ApplyResources(this.btnCaliPage, "btnCaliPage");
            this.btnCaliPage.Name = "btnCaliPage";
            this.btnCaliPage.UseVisualStyleBackColor = true;
            this.btnCaliPage.Click += new System.EventHandler(this.btnCaliPage_Click);
            // 
            // btnMechanicPage
            // 
            resources.ApplyResources(this.btnMechanicPage, "btnMechanicPage");
            this.btnMechanicPage.Name = "btnMechanicPage";
            this.btnMechanicPage.UseVisualStyleBackColor = true;
            this.btnMechanicPage.Click += new System.EventHandler(this.btnMechanicPage_Click);
            // 
            // btnWelcomePage
            // 
            resources.ApplyResources(this.btnWelcomePage, "btnWelcomePage");
            this.btnWelcomePage.Name = "btnWelcomePage";
            this.btnWelcomePage.UseVisualStyleBackColor = true;
            this.btnWelcomePage.Click += new System.EventHandler(this.btnWelcomePage_Click);
            // 
            // CaliWizard
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.m_WizardCalibration);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.Name = "CaliWizard";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.CaliWizard_Closing);
            this.m_WizardCalibration.ResumeLayout(false);
            this.m_WizardPageWelcom.ResumeLayout(false);
            this.groupBoxCameraCari.ResumeLayout(false);
            this.groupBoxCameraCari.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYoriginOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numXoriginOffset)).EndInit();
            this.panelChoosePlate.ResumeLayout(false);
            this.panelChoosePlate.PerformLayout();
            this.m_WizardPageCali.ResumeLayout(false);
            this.m_WizardPageMechanic.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.grouperMechanic.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		public void SetPrinterStatusChanged(JetStatusEnum status)
		{
			m_curStatus = status;
			if(status != JetStatusEnum.Ready)
			{
				m_WizardCalibration.HelpEnabled = false;
                UpdateCheckBtnsEnabled(false);
			}
			else
			{
                this.m_WizardCalibration.HelpEnabled = !_printerProperty.EPSONLCD_DEFINED || (PubFunc.GetUserPermission() == (int)UserPermission.SupperUser);
				UpdateCheckBtnsEnabled(true);
			}
		}

		public void SetPrinterChange(IPrinterChange ic)
		{
			m_iPrinterChange = ic;
			m_CalibrationSettingHor.SetPrinterChange(ic);
		}
		private void SetScale()
		{	
			SizeF sizef = new SizeF();
			sizef = Form.GetAutoScaleSize(this.Font);

			Size size = sizef.ToSize();
			this.AutoScaleBaseSize = size;

		}	
		private void m_WizardCalibration_AfterSwitchPages(object sender, BYHXControls.Wizard.AfterSwitchPagesEventArgs e)
		{
			// get wizard page to be displayed
			WizardPage newPage = this.m_WizardCalibration.Pages[e.NewIndex];

            string Platform = "_";
            Platform += _curPlatform == CoreConst.AXIS_X ? "A" : "B";

			// check if calibration page
			if (newPage == this.m_WizardPageCali)
			{
				CalibrationSubStep cur = (CalibrationSubStep)m_subStep;

                this.Text = ResString.GetResString("CaliWizard_Title") + Platform + ":" + 
					ResString.GetEnumDisplayName(typeof(CalibrationSubStep) ,cur );
				m_CalibrationSettingHor.SetCalibrationStep(cur);
				m_WizardCalibration.HelpVisible = true; 
				m_WizardCalibration.SaveVisible = true;
			}
#if LIYUUSB
            else if (newPage == this.m_WizardPageMechanic)
            {
                this.Text = ResString.GetResString("CaliWizard_Title");
                m_WizardCalibration.HelpVisible = true;
                m_WizardCalibration.SaveVisible = false;
            }
#endif
            else
            {
                this.Text = ResString.GetResString("CaliWizard_Title") + Platform;
                m_WizardCalibration.HelpVisible = false;
                m_WizardCalibration.SaveVisible = false;
            }

		}

		private void m_WizardCalibration_BeforeSwitchPages(object sender, BYHXControls.Wizard.BeforeSwitchPagesEventArgs e)
		{
			// get wizard page already displayed
			WizardPage oldPage = this.m_WizardCalibration.Pages[e.OldIndex];

			// check if we're going forward from options page
			if (oldPage == this.m_WizardPageCali && e.NewIndex > e.OldIndex)
			{
				// check if user selected one option
                if (m_subStep < (int)CalibrationSubStep.All - 1)
                {
                    //if (!Misc.IsGourpCalibration)
                    if (m_iPrinterChange.GetAllParam().PrinterSetting.sExtensionSetting.bIsNewCalibration == 0)
                    {
                        if (m_subStep == (int)CalibrationSubStep.GroupLeft)
                            m_subStep += 2;
                        else if (m_subStep == (int)CalibrationSubStep.GroupRight)
                            m_subStep++;
                    }
                    int subStep = m_subStep;
                    while (subStep + 1 < 9)
                    {//因为CalibrationSubStep最大为9，循环检查界面是否包含下一组控件
                        subStep++;
                        if (m_CalibrationSettingHor.CaliSubStepSupport.Contains((CalibrationSubStep)(subStep)))
                        {
                            m_subStep = subStep;
                            e.NewIndex--;
                            break;
                        }
                    }
                }
			}
            else if (oldPage == this.m_WizardPageCali && e.NewIndex < e.OldIndex)
            {
                if (m_iPrinterChange.GetAllParam().PrinterSetting.sExtensionSetting.bIsNewCalibration == 0)
                {
                    if (m_subStep > (int)(((CalibrationSubStep[])Enum.GetValues(typeof(CalibrationSubStep)))[2]))
                    {
                        m_subStep--;
                        e.NewIndex++;
                    }
 
                }
                else if (m_subStep > (int)(((CalibrationSubStep[])Enum.GetValues(typeof(CalibrationSubStep)))[0]))
                {
                    m_subStep--;
                    e.NewIndex++;
                }
            }
		}

		private void m_WizardCalibration_Cancel(object sender, System.ComponentModel.CancelEventArgs e)
		{

		}

		private void m_WizardCalibration_Finish(object sender, System.EventArgs e)
		{
			this.SaveCalibration();
        }

        private void m_WizardCalibration_Help(object sender, System.EventArgs e)
        {
            m_WizardCalibration.HelpEnabled = false;

            //第二页校准使用PrinterSetting局部变量，防止第一页机械校准的参数跟着变
            SPrinterSetting m_tempSetting = new SPrinterSetting();

            OnGetPrinterSetting(ref m_tempSetting);
            AllParam allParam = this.m_iPrinterChange.GetAllParam();

            // Synchronous PrinterSetting before print job.
            allParam.PrinterProperty.SynchronousCalibrationSettings(ref allParam.PrinterSetting);

            if (CanPrintEpsonCalibration())
            {
                // get wizard page to be displayed
                CalibrationSubStep step = (CalibrationSubStep)m_subStep;
                this.SendEPSONCalibrateCmd(step);
            }
            else
            {
                if (this.m_WizardCalibration.SelectedPage == this.m_WizardPageMechanic)
                {
                    this.m_ButtonMechanicalAll_Click(sender, e);
#if false
                if (this.radioButtonAngle.Checked)
                    this.m_ButtonAngle_Click(sender, e);
                else if (this.radioButtonVerticalCheck.Checked)
                    this.m_ButtonVerticalCheck_Click(sender, e);
                else if (this.radioButtonNozzleCheck.Checked)
                    this.m_ButtonNozzleCheck_Click(sender, e);
                else if (this.radioButtonCrossCheck.Checked)
                    this.m_ButtonCrossCheck_Click(sender, e);
#endif
                    return;
                }
                // get wizard page to be displayed
                CalibrationSubStep step = (CalibrationSubStep)m_subStep;
                if (step == CalibrationSubStep.Step && m_CalibrationSettingHor.GetPass() == 1)
                {
                    m_CalibrationSettingHor.ClearStepCurrentValue();
                }

                m_tempSetting.sFrequencySetting.nPass = (byte)m_CalibrationSettingHor.GetPass();
                m_tempSetting.sFrequencySetting.nSpeed = (SpeedEnum)m_CalibrationSettingHor.GetSpeed();
                m_tempSetting.sFrequencySetting.nResolutionX = m_CalibrationSettingHor.GetResIndex();

                //TODO:快速水平（左右）校准模式处理
                //bool isQuickHorCalibration = m_CalibrationSettingHor.IsQuickHorCalibration;
                //GetPrinterSetting(&sPrinterSetting);
                GetOriginValue();
                int patternNum = 0;
                if (step == CalibrationSubStep.Left || step == CalibrationSubStep.Right)
                {
                    //第一个字节存颜色校准模式，第二个字节存选择的颜色索引,第三个字节存线宽
                    switch ((HorizontalCalibrationMode)m_CalibrationSettingHor.HorCalibrationMode)
                    {
                        case HorizontalCalibrationMode.Full:
                            patternNum = 0;
                            break;
                        case HorizontalCalibrationMode.Color:
                            patternNum = 1 + (m_CalibrationSettingHor.ColorCalibrationIndex << 8);
                            break;
                        case HorizontalCalibrationMode.Quick:
                            patternNum = 2;
                            break;
                        case HorizontalCalibrationMode.GroupQuick:
                            patternNum = 3;
                            break;
                        case HorizontalCalibrationMode.GroupColor:
                            patternNum = 4 + (m_CalibrationSettingHor.ColorCalibrationIndex << 8);
                            break;
                        case HorizontalCalibrationMode.GroupFull:
                            patternNum = 5;
                            break;
                        default:
                            break;
                    }
                }
                //if (isQuickHorCalibration) patternNum = 2;
                _curPlatform = comboBoxChoosePlate.SelectedIndex == 0 ? CoreConst.AXIS_X : CoreConst.AXIS_4;
                switch (step)
                {
                    case CalibrationSubStep.Left:
                        CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.LeftCmd, patternNum, ref m_tempSetting, true, _curPlatform);
                        break;
                    case CalibrationSubStep.Right:
                        CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.RightCmd, patternNum, ref m_tempSetting, true, _curPlatform);
                        break;
                    case CalibrationSubStep.Bidirection:
                        CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.BiDirectionCmd, patternNum, ref m_tempSetting, true, _curPlatform);
                        break;
                    case CalibrationSubStep.Vertical:
                        CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.VerCmd, patternNum, ref m_tempSetting, true, _curPlatform);
                        break;
                    case CalibrationSubStep.Step:
                        //CoreInterface.SendCalibrateCmd(CalibrationCmdEnum.StepCmd,patternNum,ref m_CaliSetting);
                        CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.EngStepCmd, patternNum, ref m_tempSetting, true, _curPlatform);
                        break;
                    case CalibrationSubStep.Overlap:
                        CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.CheckOverLapCmd, patternNum, ref m_tempSetting, true, _curPlatform);
                        break;
                    case CalibrationSubStep.Origin:
                        CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.XOriginCmd, patternNum, ref m_tempSetting, true, _curPlatform);
                        break;
                    case CalibrationSubStep.GroupLeft:
                        CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.GroupCmdLeft, patternNum, ref m_tempSetting, true, _curPlatform);
                        break;
                    case CalibrationSubStep.GroupRight:
                        CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.GroupCmdRight, patternNum, ref m_tempSetting, true, _curPlatform);
                        break;
                    default:
                        break;
                }
            }
        }
        
        private void MoveCalibrationPage()
		{
			this.m_WizardCalibration.SuspendLayout();
			this.m_WizardPageCali.SuspendLayout();
			this.SuspendLayout();

			///////???????????????????????????????????????????????????????????????????????????????????????????????????????????????????
			this.m_WizardPageCali.Controls.Add(this.m_CalibrationSettingHor);
			// 
			// m_CalibrationSettingHor
			// 
			this.m_CalibrationSettingHor.Location = new System.Drawing.Point(8, 72);
			this.m_CalibrationSettingHor.Name = "m_CalibrationSettingHor";
			this.m_CalibrationSettingHor.Size = new System.Drawing.Size(608, 312);
			this.m_CalibrationSettingHor.TabIndex = 0;

			this.m_WizardCalibration.ResumeLayout(false);
			this.m_WizardPageCali.ResumeLayout(false);
			this.ResumeLayout(false);

		}

	    private SPrinterProperty _printerProperty;
		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
		    _printerProperty = sp;
			m_CalibrationSettingHor.OnPrinterPropertyChange(sp);
           if (sp.nHeadNumPerColor >= 2)
			{
				radioButtonCrossCheck.Visible = m_ButtonCrossCheck.Visible = true;
				if(//SPrinterProperty.IsPolaris(sp.ePrinterHead)	|| 
                    SPrinterProperty.IsKonica512(sp.ePrinterHead) && !SPrinterProperty.IsKonica512i(sp.ePrinterHead))
					radioButtonCrossCheck.Visible = m_ButtonCrossCheck.Visible = false;
			}
			else
				radioButtonCrossCheck.Visible = m_ButtonCrossCheck.Visible = false;

#if LIYUUSB
            this.radioButtonNozzleCheck.Visible = false;
            if (radioButtonCrossCheck.Visible)
                this.grouperMechanic.Height = radioButtonNozzleCheck.Top;
            else
                this.grouperMechanic.Height = radioButtonCrossCheck.Top;
#endif

           if (sp.IsTATE())
           {
               m_ButtonOverLapCheck.Visible = true;
           }
           else
           {
               m_ButtonOverLapCheck.Visible = false;
           }
		}
		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
			m_CaliSetting = ss;
			m_CalibrationSettingHor.OnPrinterSettingChange(m_iPrinterChange.GetAllParam());
		}
        public void OnGetPrinterSetting(ref AllParam ss)
		{
		    AllParam allParam = m_iPrinterChange.GetAllParam();
            m_CalibrationSettingHor.OnGetPrinterSetting(allParam);
		    ss.PrinterSetting = allParam.PrinterSetting;
            ss.NozzleOverlap = allParam.NozzleOverlap;
		}
        public void OnGetPrinterSetting(ref SPrinterSetting ss)
        {
            AllParam allParam = m_iPrinterChange.GetAllParam();
            m_CalibrationSettingHor.OnGetPrinterSetting(allParam);
            ss = allParam.PrinterSetting;
        }
		private void CaliWizard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			CaliWizard wizard = ((BYHXPrinterManager.Calibration.CaliWizard)(sender));
			//
			WizardPage newPage = this.m_WizardCalibration.Pages[wizard.m_WizardCalibration.SelectedIndex];

			if(newPage != this.m_WizardPageFinish)
			{
				if (MessageBox.Show(ResString.GetResString("CaliWizard_Cancel"),
				                    this.Text,
				                    MessageBoxButtons.YesNo,
				                    MessageBoxIcon.Question) != DialogResult.Yes)
				{
				    // cancel closing
				    e.Cancel = true;
				    // restart the task
				}
				else
				{
				    m_iPrinterChange.NotifyUICalibrationExit(false, true);
                    SendGzTanchuZhixiangCmd();
                }
			}
			else
			{
			    SendGzTanchuZhixiangCmd();
			}

		}
        private void m_ButtonMechanicalAll_Click(object sender, System.EventArgs e)
        {
            _curPlatform = comboBoxChoosePlate.SelectedIndex == 0 ? CoreConst.AXIS_X : CoreConst.AXIS_4;
            GetOriginValue();
            CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.Mechanical_AllCmd, 0, ref m_CaliSetting, true, _curPlatform);
        }

        public void UpdateCheckBtnsEnabled(bool isEnable) {
            m_ButtonAngle.Enabled = isEnable;
            m_ButtonVerticalCheck.Enabled = isEnable;
            m_ButtonNozzleCheck.Enabled = isEnable;
            m_ButtonCrossCheck.Enabled = isEnable;
            m_ButtonOverallCheck.Enabled = isEnable;
			m_ButtonOverLapCheck.Enabled = isEnable;
        }

		private void m_ButtonAngle_Click(object sender, System.EventArgs e)
		{
            UpdateCheckBtnsEnabled(false);
			if(CanPrintEpsonCalibration())
				this.SendEPSONCalibrateCmd(Cali_Pattern_Type.AngleLeftCheck);
			else
			{
                _curPlatform = comboBoxChoosePlate.SelectedIndex == 0 ? CoreConst.AXIS_X : CoreConst.AXIS_4;
                GetOriginValue();
                CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.Mechanical_CheckAngleCmd, 0, ref m_CaliSetting, true, _curPlatform);
			}		
		}

		private void m_ButtonVerticalCheck_Click(object sender, System.EventArgs e)
		{
            UpdateCheckBtnsEnabled(false);
			if(CanPrintEpsonCalibration())
				this.SendEPSONCalibrateCmd(Cali_Pattern_Type.VerticalCheck);
			else
			{
                _curPlatform = comboBoxChoosePlate.SelectedIndex == 0 ? CoreConst.AXIS_X : CoreConst.AXIS_4;
                GetOriginValue();
				//CoreInterface.SendCalibrateCmd(CalibrationCmdEnum.Mechanical_CrossHeadCmd,1,ref m_CaliSetting);
                CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.Mechanical_CheckVerticalCmd, 0, ref m_CaliSetting, true, _curPlatform);
			}		
		}

		private void m_ButtonNozzleCheck_Click(object sender, System.EventArgs e)
		{
            UpdateCheckBtnsEnabled(false);
			if(CanPrintEpsonCalibration())
				this.SendEPSONCalibrateCmd(Cali_Pattern_Type.NozzleCheck);
			else
			{
                _curPlatform = comboBoxChoosePlate.SelectedIndex == 0 ? CoreConst.AXIS_X : CoreConst.AXIS_4;
                GetOriginValue();
				//CoreInterface.SendCalibrateCmd(CalibrationCmdEnum.CheckNozzleCmd,0,ref m_CaliSetting);
                CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.NozzleAllCmd, 4, ref m_CaliSetting, true, _curPlatform);
			}		
		}
		private void GetOriginValue()
		{
			AllParam allparam = m_iPrinterChange.GetAllParam();
            //CoreInterface.SetPrinterSetting(ref allParam.PrinterSetting);
            //每次打印前从文件读取最新参数,防止主界面修改后打印校准不生效问题
            if (PubFunc.IsZhuoZhan())
                PubFunc.SwitchPrintPlatform(allparam, _curPlatform, _curPlatform);

            m_CaliSetting.sFrequencySetting.fXOrigin = allparam.PrinterSetting.sFrequencySetting.fXOrigin;
			m_CaliSetting.sBaseSetting.fYOrigin = allparam.PrinterSetting.sBaseSetting.fYOrigin;
            //LogWriter.WriteLog(new string[] { string.Format("GetOriginValue,fXOrigin={0},fYOrigin={1}", m_CaliSetting.sFrequencySetting.fXOrigin, m_CaliSetting.sBaseSetting.fYOrigin) }, true);
        }

		private void m_WizardCalibration_Save(object sender, System.EventArgs e)
		{
			this.SaveCalibration();
		}
		private void InitializeLocalize()
		{
			this.Text = ResString.GetResString("CaliWizard_Title");

			m_WizardPageWelcom.Title = ResString.GetEnumDisplayName(typeof(CalibrationTitle) ,CalibrationTitle.Welcome );
			m_WizardPageFinish.Title = ResString.GetEnumDisplayName(typeof(CalibrationTitle) ,CalibrationTitle.Finish );
			m_WizardPageCali.Title = ResString.GetEnumDisplayName(typeof(CalibrationTitle) ,CalibrationTitle.Calibration );
			m_WizardPageMechanic.Title =  ResString.GetEnumDisplayName(typeof(CalibrationTitle) ,CalibrationTitle.Mechanical);

			m_WizardPageWelcom.Description = ResString.GetEnumDisplayName(typeof(CalibrationDescription) ,CalibrationDescription.Welcome );
			m_WizardPageFinish.Description = ResString.GetEnumDisplayName(typeof(CalibrationDescription) ,CalibrationDescription.Finish );
			m_WizardPageCali.Description = ResString.GetEnumDisplayName(typeof(CalibrationDescription) ,CalibrationDescription.Calibration );
			m_WizardPageMechanic.Description =  ResString.GetEnumDisplayName(typeof(CalibrationDescription) ,CalibrationDescription.Mechanical );

			Assembly myAssembly = Assembly.GetExecutingAssembly();
			string[] names = myAssembly.GetManifestResourceNames();
#if !LIYUUSB
			Stream myStream1 = myAssembly.GetManifestResourceStream("BYHXPrinterManager.Calibration.app.png");
			this.m_WizardCalibration.HeaderImage = new Bitmap(myStream1);
			this.grouperMechanic.Visible =false;
            Stream myStream2 = myAssembly.GetManifestResourceStream("BYHXPrinterManager.Calibration.welcomImage.bmp");
            this.m_WizardCalibration.WelcomeImage = new Bitmap(myStream2);
#else
            string iconpath= Application.StartupPath + "\\setup\\app.png";
            if (File.Exists(iconpath))
                this.m_WizardCalibration.HeaderImage = Image.FromFile(iconpath, false);

            this.m_CalibrationSettingHor.SetGroupsTop();
            this.panel1.Visible = false;
            this.grouperMechanic.Visible = false;
			this.grouperMechanic.Location = this.panel1.Location;
			this.grouperMechanic.Text = m_WizardPageMechanic.Title;
			this.radioButtonAngle.Text = this.m_ButtonAngle.Text;
			this.radioButtonCrossCheck.Text = this.m_ButtonCrossCheck.Text;
			this.radioButtonNozzleCheck.Text = this.m_ButtonNozzleCheck.Text;
			this.radioButtonVerticalCheck.Text = this.m_ButtonVerticalCheck.Text;
            Stream myStream2 = myAssembly.GetManifestResourceStream("BYHXPrinterManager.Calibration.welcomImage_b.bmp");
            this.m_WizardCalibration.WelcomeImage = new Bitmap(myStream2);
#endif
		}

		private void m_ButtonCrossCheck_Click(object sender, System.EventArgs e)
		{
            UpdateCheckBtnsEnabled(false);
			if(CanPrintEpsonCalibration())
				this.SendEPSONCalibrateCmd(Cali_Pattern_Type.InterleaveCheck);
			else
			{
                _curPlatform = comboBoxChoosePlate.SelectedIndex == 0 ? CoreConst.AXIS_X : CoreConst.AXIS_4;
                GetOriginValue();
                CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.Mechanical_CrossHeadCmd, 0, ref m_CaliSetting, true, _curPlatform);
				//CoreInterface.SendCalibrateCmd(CalibrationCmdEnum.Step_CheckCmd,100,ref m_CaliSetting);
			}
		}

		public void SetGroupBoxStyle(Grouper ts)
		{
			this.m_CalibrationSettingHor.GrouperTitleStyle = ts;
		}
		
		private void SendEPSONCalibrateCmd(CalibrationSubStep step)
		{
			switch(step)
			{
				case CalibrationSubStep.Left:
					SendEPSONCalibrateCmd(Cali_Pattern_Type.LeftCheck);
					break;
				case CalibrationSubStep.Right:
					SendEPSONCalibrateCmd( Cali_Pattern_Type.RightCheck);
					break;
				case CalibrationSubStep.Bidirection:
					SendEPSONCalibrateCmd( Cali_Pattern_Type.BiDirCheck);
					break;
				case CalibrationSubStep.Vertical:
					SendEPSONCalibrateCmd( Cali_Pattern_Type.VerticalCheck);
					break;
				case CalibrationSubStep.Step:
					SendEPSONCalibrateCmd(Cali_Pattern_Type.StepCheck);
					break;
				default:
					break;
			}
		}

		private void SendEPSONCalibrateCmd(Cali_Pattern_Type cpt)
		{
			this.OnGetPrinterSetting(ref m_CaliSetting);
			CaliPrintSetting cps = new CaliPrintSetting();
			cps.type = cpt;
			cps.VSDModel = (byte)m_CaliSetting.sFrequencySetting.nSpeed;
			
			SSeviceSetting sSeviceSet= new SSeviceSetting();
			if(CoreInterface.GetSeviceSetting(ref sSeviceSet) != 0)
				cps.DotSetting = (byte)sSeviceSet.Vsd2ToVsd3_ColorDeep;
			switch(m_CaliSetting.sFrequencySetting.nResolutionX)
			{
				case 720:
					cps.DPIModel = 1;
					break;
				case 360:
					cps.DPIModel = 2;
					break;
				case 540:
					cps.DPIModel = 3;
					break;
				case 270:
					cps.DPIModel = 4;
					break;
				case 1440:
					cps.DPIModel = 5;
					break;
			}
			cps.StartPos =(ushort)(m_CaliSetting.sFrequencySetting.fXOrigin*m_CaliSetting.sFrequencySetting.nResolutionX);
			cps.MediaType=0;//无效
			cps.PassNum = m_CaliSetting.sFrequencySetting.nPass;
			cps.Option = 0;//保留
			EpsonLCD.PrintDotCheck(cps);
		}


		private void SaveCalibration()
		{
			AllParam m_allParam = m_iPrinterChange.GetAllParam();
			if (m_allParam != null) //save old settings
				m_allParam.SaveToXml(Path.Combine(Application.StartupPath, "Calibration_Pre.xml"), false);

			if(CanPrintEpsonCalibration())
			{
                AllParam sps = new AllParam();
                OnGetPrinterSetting(ref sps);
				EpsonLCD.SetCalibrationSetting(sps.PrinterSetting.sCalibrationSetting);
			}
			else
			{
				OnGetPrinterSetting(ref m_allParam);
				CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
			}

			if (m_allParam != null)// save current settings
				m_allParam.SaveToXml(Path.Combine(Application.StartupPath, "Calibration_Cur.xml"), false);
            //if(PubFunc.IsZhuoZhan())
            //    PubFunc.SavePlatformCariParas(m_allParam, _curPlatform);
			m_iPrinterChange.NotifyUICalibrationExit(true, false);
		}

		private bool CanPrintEpsonCalibration()
		{
#if true
            AllParam m_allParam = m_iPrinterChange.GetAllParam();
            return m_allParam.PrinterProperty.EPSONLCD_DEFINED;
#else
			return false;
#endif
		}

        private void SendGzTanchuZhixiangCmd()
        {
            ushort pid = 0, vid = 0;
            CoreInterface.GetProductID(ref vid, ref pid);
            // 工正纸板机弹纸箱
            if (vid == (ushort)VenderID.GONGZENG || vid == (ushort)VenderID.GONGZENG_FLAT_UV || vid == (ushort)VenderID.GONGZENG_BELT_TEXTILE)
            {
                byte[] buf = new byte[64];
                uint bufsize = (uint)buf.Length;
                CoreInterface.SetEpsonEP0Cmd(0x47, buf, ref bufsize, 0, 0);
            }
        }

	    private void comboBoxChoosePlate_SelectedIndexChanged(object sender, EventArgs e)
	    {
	        if (!hasLoaded)
	            return;
	        byte newPlatform = comboBoxChoosePlate.SelectedIndex == 0 ? CoreConst.AXIS_X : CoreConst.AXIS_4;
	        AllParam allParam = m_iPrinterChange.GetAllParam();
	        OnGetPrinterSetting(ref allParam.PrinterSetting);
	        //CoreInterface.SetPrinterSetting(ref allParam.PrinterSetting);

            PubFunc.SwitchPrintPlatform(allParam, _curPlatform, newPlatform);

	        OnPrinterSettingChange(allParam.PrinterSetting);
            m_CalibrationSettingHor.Platform =_curPlatform = newPlatform;
	    }

        private void buttonPrintCameraCari_Click(object sender, EventArgs e)
        {
            buttonPrintCameraCari.Enabled = false;
            zhunzhanCameraCariWorker.RunWorkerAsync();
        }

        private void buttonAbort_Click(object sender, EventArgs e)
        {
            _abortPrint = true;
        }

        private void m_ButtonOverallCheck_Click(object sender, EventArgs e)
        {
            UpdateCheckBtnsEnabled(false);
            _curPlatform = comboBoxChoosePlate.SelectedIndex == 0 ? CoreConst.AXIS_X : CoreConst.AXIS_4;
            GetOriginValue();
            CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.OverallCheck, 0, ref m_CaliSetting, true, _curPlatform);
        }

        private void m_ButtonOverLapCheck_Click(object sender, EventArgs e)
        {
			UpdateCheckBtnsEnabled(false);
            
            _curPlatform = comboBoxChoosePlate.SelectedIndex == 0 ? CoreConst.AXIS_X : CoreConst.AXIS_4;
            GetOriginValue();

            m_CaliSetting.sFrequencySetting.nResolutionX /= m_CaliSetting.sBaseSetting.nXResutionDiv;
            CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.CheckOverLapCmd, 0, ref m_CaliSetting, true, _curPlatform);
        }

        private void btnWelcomePage_Click(object sender, EventArgs e)
        {
            if (m_WizardCalibration.SelectedIndex == 0) return;
            if (m_subStep != 0) m_subStep = 0;//校准页面的步骤m_subStep重置 ；同下
            m_WizardCalibration.ToPage(0);
        }

        private void btnMechanicPage_Click(object sender, EventArgs e)
        {
            if (m_WizardCalibration.SelectedIndex == 1) return;
            if (m_subStep != 0) m_subStep = 0;
            m_WizardCalibration.ToPage(1);
        }

        private void btnCaliPage_Click(object sender, EventArgs e)
        {
            if (m_WizardCalibration.SelectedIndex == 2) return;
            if (m_subStep != 0) m_subStep = 0;
            m_WizardCalibration.ToPage(2);
        }

        private void btnFinishPage_Click(object sender, EventArgs e)
        {
            if (m_WizardCalibration.SelectedIndex == 3) return;
            if (m_subStep != 9) m_subStep = 9;//校准页面的步骤m_subStep重置
            m_WizardCalibration.ToPage(3);
        }

	}
}
