/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using BYHXPrinterManager.Setting;
using System.Diagnostics;
using System.Linq;
using System.Text;
using PrinterStubC.Utility;
using WAF_OnePass.Domain.Utility;

namespace BYHXPrinterManager.Main
{
	/// <summary>
	/// Summary description for FactoryDebug.
	/// </summary>
	public class FactoryDebug : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button m_ButtonCancel;
		private System.Windows.Forms.Button m_ButtonOK;
		private System.Windows.Forms.GroupBox m_GroupBoxMoveTest;
		private System.Windows.Forms.ComboBox m_ComboBoxDirection;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownLength;
		private System.Windows.Forms.Label m_LabelLength;
		private System.Windows.Forms.Label m_LabelDirection;
		private System.Windows.Forms.Button m_ButtonMove;
		private System.Windows.Forms.Button m_ButtonStop;
		private System.Windows.Forms.Label m_LabelSpeed;
		private System.Windows.Forms.ComboBox m_ComboBoxSpeed;
		private System.Windows.Forms.Button m_ButtonPosition;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownTimeOut;
		private System.Windows.Forms.Label m_LabelTimeOut;
		private System.Windows.Forms.Button m_ButtonSetTimeout;
		private System.Windows.Forms.Label m_LabelCopyRight;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownPosX;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownPosY;
		private System.Windows.Forms.GroupBox m_GroupBoxPosition;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownDebug1;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownDebug2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownDebug3;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownDebug4;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownSpeedSet;
		private System.Windows.Forms.Button m_ButtonWriteSpeed;
		private DividerPanel.DividerPanel dividerPanel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader chfloatstatusnum;
		private System.Windows.Forms.ColumnHeader chfloatprestatus;
		private System.Windows.Forms.ColumnHeader chfloatcurStatus;
		private System.Windows.Forms.Button buttonGetInkStatus;
		private System.Windows.Forms.ColumnHeader pumpTimeMS;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPagegeneral;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button m_ButtonWriteUV;
		private System.Windows.Forms.Button m_ButtonReadUV;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownLeftUV;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownRightUV;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolTip m_ToolTip;
		private System.Windows.Forms.TextBox txtSerialCmdLen;
		private System.Windows.Forms.Button btnSendSerialCmd;
		private System.Windows.Forms.TextBox txtSerialCmd;
		private System.Windows.Forms.ComboBox m_ComboBoxVenderID;

		private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
		private System.Windows.Forms.NumericUpDown []m_NumericUpDownDelayTimes;
		private System.Windows.Forms.Label   []m_LabelHorHeadIndex;
		private int m_HeadNum =0;
		private int m_TempNum = 0;
		private int m_HeadVoltageNum =0;
		private byte m_StartHeadIndex = 0;
		private byte[] m_pMap;
		private bool m_bSpectra = false;
		private bool m_bKonic512 = false;
		private bool m_bXaar382 = false;
		private bool m_bPolaris = false;
		private	bool m_bExcept = false;
		private bool m_Konic512_1head2color = false;
        private bool m_bSpectra_SG1024_Gray = false;
        private bool m_bPolaris_V5_8 = false;
		private bool m_bPolaris_V5_8_Emerald = false;
		private bool m_bHorArrangement = false;
		private bool m_b1head2color = false;
		private SPrinterProperty m_rsPrinterPropery;
		private System.Windows.Forms.TabPage tabPageColorDeep;
        private System.Windows.Forms.ComboBox cboColorDeep;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button btnAutoTestclb;
		private System.Windows.Forms.TabPage tabPageRabillyDebug;
		private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxTemperature;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label m_LabelHead;
		private System.Windows.Forms.Label m_LabelHorSample;
		private System.Windows.Forms.Label m_LabelCurPulseWidth;
		private System.Windows.Forms.NumericUpDown numericUpDownDelayTime;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownPulseWidthSample;
		private System.Windows.Forms.Button m_ButtonToBoard;
		private System.Windows.Forms.Button m_ButtonRefresh;
		private System.Windows.Forms.Button buttonRead;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Button buttonGet;
		private System.Windows.Forms.Button buttonSet;
		private System.Windows.Forms.NumericUpDown numericUpDownDelayTime1;
		private System.Windows.Forms.NumericUpDown numericUpDownPulseWidth;
		private System.Windows.Forms.NumericUpDown numericUpDownFireFreq;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.NumericUpDown numericUpDownTA;
		private System.Windows.Forms.TabPage tabPageInkTest;
		private System.Windows.Forms.TabPage tabPageAccSpeedTest;
		private System.Windows.Forms.Button buttonAccTimeSet;
		private System.Windows.Forms.NumericUpDown numAccTimeY;
		private System.Windows.Forms.NumericUpDown numAccTimeX;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel1;//Only read for color order
		private const int VOLCOUNTPERPOLARISHEAD = 2;
        private TableLayoutPanel Panel1;
        private TabPage tabPageVPrint;
        private Button buttonStopPrint;
        private Button buttonStartPrint;
        private NumericUpDown m_NumericUpDownPosY1;
        private Label label21;
	    private bool bload = false;
        private TabPage tabPage2;
        private ComboBox comboBoxXaar382Mode;
        private Label label17;
        private Button buttonXaar382Mode;
        private Label label16;
        private NumericUpDown numericUpDown1;
        private Button buttonSetFrieFreq;
        private GroupBox groupBoxGzCarton;
        private Label label20;
        private Button buttonWaitMediaReadyTim;
        private NumericUpDown numWaitMediaReadyTime;
        private Button buttonReadWaitMediaReadyTim;
        private Button m_ButtonMoveNew;
        private Button button3m_ButtonMove;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label22;
        private NumericUpDown numericUpDown2;
        private Button buttonFeedingControl;
        private CheckBox buttonTrayControl;
        private GroupBox groupBox4;
        private Button buttonTrayControlApply;
        private NumericUpDown numAutoCappingDelay;
        private Label label23;
        private AutoInkTest autoInkTest1;
        private NktDyjUserControl nktDyjUserControl1;
        private Button button6;
        private Button button5;
        private Button button3;
        private TabPage tabPageHeadData;
        private GroupBox groupBox6;
        private TextBox textBoxRecieveData;
        private GroupBox groupBox5;
        private TextBox textBoxSendData;
        private Button btnGetHeadData;
        private NumericUpDown numeCmd;
        private Label label24;
        private Button buttonErrorInfoTest;
        private NumericUpDown numIndex;
        private Label label26;
        private NumericUpDown numValue;
        private Label label25;
        private Panel panel3;
        private NumericUpDown numDataLength;
        private Label label27;
        private Panel panel4;
        private Label label28;
        private RadioButton radioButtonASCII;
        private RadioButton radioButtonHex;
        private GroupBox groupBox7;
        private Panel panel5;
        private Button buttonReadWf;
        private NumericUpDown numHbId;
        private Label label29;
        private NumericUpDown numWaveformId;
        private Label label30;
        private Panel panel6;
        private Label label31;
        private RadioButton radioButtonB;
        private RadioButton radioButtonA;
        private Button buttonSaveAS;
        private RadioButton radioButtonShijinzhi;
        private Button button7;
        private Button buttonReadAll;
        private NumericUpDown m_NumericUpDownY2Length;
        private Label label32;
        private Label label33;
        private TextBox textBoxerrorCode;
        private Button buttonAccTimeGet;
        private GroupBox groupBoxGraymapSet;
        private Button buttonGrayMapSet;
        private Label labelGrayMap;
        private Label labelDotbit;
        private Label labelPrinthead;
        private Label labelHeaderBoard;
        private NumericUpDown numericUpDownHeadBoard;
        private ComboBox comboBoxGrayMap3;
        private ComboBox comboBoxGrayMap2;
        private ComboBox comboBoxGrayMap1;
        private ComboBox comboBoxDotBit;
        private ComboBox comboBoxPrintHead;
        private GroupBox groupBoxShakeSetting;
        private Button buttonShakeSet;
        private NumericUpDown numericShockInterval;
        private Label labelShockInterval;
        private NumericUpDown numericIgnitionNumber;
        private Label labelIgnitionNumber;
        private CheckBox checkBoxShake;
        private FlowLayoutPanel flowLayoutPanel1;
               private Button buttonShakeGet;
               private Label label34;
               private TextBox textBox_CalibrateCmd;
               private Button button_Calibrate;
               private Button btnSaveFWLog;
               private TabPage tabPage_Misc;
               private GroupBox groupBoxTemperaturProfil;
               private Button button_TemperatureProfileState;
               private Label label_TemperatureProfileState;
               private Label label_TemperatureProfileStateLabel;
               private Button buttonClose;
               private Button buttonOpen;
               private TabPage m_TabPageWaveMapping;
               private WaveFormTool.WaveMappingControl userControl81;
 private bool bDoubleYAxis = false;

	    public FactoryDebug()
	    {
	        //
	        // Required for Windows Form Designer support
	        //
	        InitializeComponent();

	        //
	        // TODO: Add any constructor code after InitializeComponent call
	        //
	        UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxSpeed, 0);
	        UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxDirection, 0);

	        UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxVenderID, 0);

	        this.numAccTimeX.Minimum = this.numAccTimeY.Minimum = short.MinValue;
	        this.numAccTimeX.Maximum = this.numAccTimeY.Maximum = short.MaxValue;
#if !GZ_CARTON
	        groupBoxGzCarton.Visible = false;
#endif
#if !SHIDAO
            buttonFeedingControl.Visible = buttonTrayControl.Visible = false;
#endif

            readallWorker = new BackgroundWorker();
            readallWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(readallWorker_RunWorkerCompleted);
            readallWorker.DoWork += new DoWorkEventHandler(readallWorker_DoWork);

            m_ComboBoxDirection.Items.Clear();
            MoveDirectionEnum[] directionEnums = (MoveDirectionEnum[])Enum.GetValues(typeof(MoveDirectionEnum));
            for (int i = 0; i < directionEnums.Length; i++)
            {
                m_ComboBoxDirection.Items.Add(directionEnums[i].ToString());
            }
            m_ComboBoxDirection.SelectedIndexChanged += new EventHandler(m_ComboBoxDirection_SelectedIndexChanged);
	        m_ComboBoxDirection.SelectedIndex = 0;
	    }

        void m_ComboBoxDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            int dir = m_ComboBoxDirection.SelectedIndex + 1;
            m_NumericUpDownY2Length.Enabled = dir == 3 || dir == 4;
        }
        AutoResetEvent readAllEvent = new AutoResetEvent(false);
        void readallWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                StreamWriter sw = new StreamWriter((string) e.Argument, false);
                readAllEvent.Reset();
                ushort value = (ushort) numHbId.Value;
                SendAndReciveEp6Data(sw, value, (byte)numWaveformId.Value, 0x10, "serial number");
                SendAndReciveEp6Data(sw, value, (byte)numWaveformId.Value, 0x11, "number of drive times");
                SendAndReciveEp6Data(sw, value, (byte)numWaveformId.Value, 0x1a, "CH group A Reading Driving waveform");
                SendAndReciveEp6Data(sw, value, (byte)numWaveformId.Value, 0x1b, "CH group B Reading Driving waveform");
                SendAndReciveEp6Data(sw, value, (byte)numWaveformId.Value, 0x16, "adjustment voltage");
                SendAndReciveEp6Data(sw, value, (byte)numWaveformId.Value, 0x12, "drive voltage setting");
                SendAndReciveEp6Data(sw, value, (byte)numWaveformId.Value, 0x32, "driving stop function status");
                SendAndReciveEp6Data(sw, value, (byte)numWaveformId.Value, 0x17, "temp. limits for heater setting");
                SendAndReciveEp6Data(sw, value, (byte)numWaveformId.Value,0x82, "heater temp");
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

	    private void SendAndReciveEp6Data(StreamWriter sw, ushort hbid, byte wfId,byte subCmd, string msg)
	    {
	        byte cmd = 0x80;
	        ushort value = (ushort) hbid;
	        ushort index = 0;
	        // heater temp
	        byte[] buf = new byte[10];
	        buf[0] = 0xc4;
	        buf[1] = 0xb8;
	        buf[2] = wfId;
	        buf[3] = 0xfe;
	        buf[4] = 0xfe;
	        buf[5] = 0xfe;
	        buf[6] = 0x04;
            buf[7] = subCmd;
	        uint len = (uint) buf.Length;
	        if (CoreInterface.SetEpsonEP0Cmd(cmd, buf, ref len, value, index) == 0)
	        {
	            MessageBox.Show(string.Format("发送读{0}命令失败！", msg), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
	        }
	        else
	        {
	            readAllEvent.WaitOne();
	            sw.WriteLine(string.Format("*************************{0}******************************", msg));
	            sw.WriteLine(BitConverter.ToString(ep6Data.Skip(7).Take(ep6Data.Length - 9).ToArray()));
	        }
	    }

	    void readallWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                MessageBox.Show("喷头参数读取成功!");
            }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FactoryDebug));
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style2 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style3 = new BYHXPrinterManager.Style();
            this.m_ButtonCancel = new System.Windows.Forms.Button();
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.m_GroupBoxMoveTest = new System.Windows.Forms.GroupBox();
            this.m_NumericUpDownY2Length = new System.Windows.Forms.NumericUpDown();
            this.label32 = new System.Windows.Forms.Label();
            this.m_ButtonMoveNew = new System.Windows.Forms.Button();
            this.m_LabelSpeed = new System.Windows.Forms.Label();
            this.m_ComboBoxSpeed = new System.Windows.Forms.ComboBox();
            this.m_ComboBoxDirection = new System.Windows.Forms.ComboBox();
            this.m_NumericUpDownLength = new System.Windows.Forms.NumericUpDown();
            this.m_LabelLength = new System.Windows.Forms.Label();
            this.m_LabelDirection = new System.Windows.Forms.Label();
            this.m_ButtonMove = new System.Windows.Forms.Button();
            this.m_ButtonStop = new System.Windows.Forms.Button();
            this.m_ButtonPosition = new System.Windows.Forms.Button();
            this.m_NumericUpDownPosX = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownTimeOut = new System.Windows.Forms.NumericUpDown();
            this.m_LabelTimeOut = new System.Windows.Forms.Label();
            this.m_ButtonSetTimeout = new System.Windows.Forms.Button();
            this.m_LabelCopyRight = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_NumericUpDownPosY = new System.Windows.Forms.NumericUpDown();
            this.m_GroupBoxPosition = new System.Windows.Forms.GroupBox();
            this.m_NumericUpDownPosY1 = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_NumericUpDownDebug3 = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownDebug4 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_NumericUpDownDebug1 = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownDebug2 = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_ButtonWriteSpeed = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.m_NumericUpDownSpeedSet = new System.Windows.Forms.NumericUpDown();
            this.listView1 = new System.Windows.Forms.ListView();
            this.chfloatstatusnum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chfloatprestatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chfloatcurStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pumpTimeMS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonGetInkStatus = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPagegeneral = new System.Windows.Forms.TabPage();
            this.button_Calibrate = new System.Windows.Forms.Button();
            this.textBox_CalibrateCmd = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.textBoxerrorCode = new System.Windows.Forms.TextBox();
            this.buttonErrorInfoTest = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.m_ComboBoxVenderID = new System.Windows.Forms.ComboBox();
            this.txtSerialCmdLen = new System.Windows.Forms.TextBox();
            this.btnSendSerialCmd = new System.Windows.Forms.Button();
            this.txtSerialCmd = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.m_ButtonWriteUV = new System.Windows.Forms.Button();
            this.m_ButtonReadUV = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.m_NumericUpDownLeftUV = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownRightUV = new System.Windows.Forms.NumericUpDown();
            this.btnAutoTestclb = new System.Windows.Forms.Button();
            this.tabPageColorDeep = new System.Windows.Forms.TabPage();
            this.groupBoxGraymapSet = new System.Windows.Forms.GroupBox();
            this.comboBoxPrintHead = new System.Windows.Forms.ComboBox();
            this.comboBoxGrayMap3 = new System.Windows.Forms.ComboBox();
            this.comboBoxGrayMap2 = new System.Windows.Forms.ComboBox();
            this.comboBoxGrayMap1 = new System.Windows.Forms.ComboBox();
            this.comboBoxDotBit = new System.Windows.Forms.ComboBox();
            this.numericUpDownHeadBoard = new System.Windows.Forms.NumericUpDown();
            this.buttonGrayMapSet = new System.Windows.Forms.Button();
            this.labelGrayMap = new System.Windows.Forms.Label();
            this.labelDotbit = new System.Windows.Forms.Label();
            this.labelPrinthead = new System.Windows.Forms.Label();
            this.labelHeaderBoard = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonTrayControlApply = new System.Windows.Forms.Button();
            this.buttonTrayControl = new System.Windows.Forms.CheckBox();
            this.buttonFeedingControl = new System.Windows.Forms.Button();
            this.Panel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonRead = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.cboColorDeep = new System.Windows.Forms.ComboBox();
            this.tabPageRabillyDebug = new System.Windows.Forms.TabPage();
            this.m_GroupBoxTemperature = new BYHXPrinterManager.GradientControls.Grouper();
            this.label11 = new System.Windows.Forms.Label();
            this.m_LabelHead = new System.Windows.Forms.Label();
            this.m_LabelHorSample = new System.Windows.Forms.Label();
            this.m_LabelCurPulseWidth = new System.Windows.Forms.Label();
            this.numericUpDownDelayTime = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownPulseWidthSample = new System.Windows.Forms.NumericUpDown();
            this.m_ButtonToBoard = new System.Windows.Forms.Button();
            this.m_ButtonRefresh = new System.Windows.Forms.Button();
            this.tabPageInkTest = new System.Windows.Forms.TabPage();
            this.autoInkTest1 = new BYHXPrinterManager.Setting.AutoInkTest();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label22 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label15 = new System.Windows.Forms.Label();
            this.numericUpDownTA = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label14 = new System.Windows.Forms.Label();
            this.numericUpDownDelayTime1 = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.numericUpDownPulseWidth = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label12 = new System.Windows.Forms.Label();
            this.numericUpDownFireFreq = new System.Windows.Forms.NumericUpDown();
            this.buttonSet = new System.Windows.Forms.Button();
            this.buttonGet = new System.Windows.Forms.Button();
            this.tabPageAccSpeedTest = new System.Windows.Forms.TabPage();
            this.groupBoxShakeSetting = new System.Windows.Forms.GroupBox();
            this.buttonShakeGet = new System.Windows.Forms.Button();
            this.buttonShakeSet = new System.Windows.Forms.Button();
            this.numericShockInterval = new System.Windows.Forms.NumericUpDown();
            this.labelShockInterval = new System.Windows.Forms.Label();
            this.numericIgnitionNumber = new System.Windows.Forms.NumericUpDown();
            this.labelIgnitionNumber = new System.Windows.Forms.Label();
            this.checkBoxShake = new System.Windows.Forms.CheckBox();
            this.buttonAccTimeGet = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.buttonAccTimeSet = new System.Windows.Forms.Button();
            this.numAccTimeY = new System.Windows.Forms.NumericUpDown();
            this.numAccTimeX = new System.Windows.Forms.NumericUpDown();
            this.tabPageVPrint = new System.Windows.Forms.TabPage();
            this.buttonStopPrint = new System.Windows.Forms.Button();
            this.buttonStartPrint = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.nktDyjUserControl1 = new BYHXPrinterManager.Setting.NktDyjUserControl();
            this.groupBoxGzCarton = new System.Windows.Forms.GroupBox();
            this.numAutoCappingDelay = new System.Windows.Forms.NumericUpDown();
            this.label23 = new System.Windows.Forms.Label();
            this.buttonReadWaitMediaReadyTim = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.buttonWaitMediaReadyTim = new System.Windows.Forms.Button();
            this.numWaitMediaReadyTime = new System.Windows.Forms.NumericUpDown();
            this.btnSaveFWLog = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBoxXaar382Mode = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.buttonXaar382Mode = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.buttonSetFrieFreq = new System.Windows.Forms.Button();
            this.tabPageHeadData = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.buttonReadAll = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label31 = new System.Windows.Forms.Label();
            this.radioButtonB = new System.Windows.Forms.RadioButton();
            this.radioButtonA = new System.Windows.Forms.RadioButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.numWaveformId = new System.Windows.Forms.NumericUpDown();
            this.label30 = new System.Windows.Forms.Label();
            this.numHbId = new System.Windows.Forms.NumericUpDown();
            this.label29 = new System.Windows.Forms.Label();
            this.buttonReadWf = new System.Windows.Forms.Button();
            this.numIndex = new System.Windows.Forms.NumericUpDown();
            this.label26 = new System.Windows.Forms.Label();
            this.numValue = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.textBoxRecieveData = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.radioButtonShijinzhi = new System.Windows.Forms.RadioButton();
            this.buttonSaveAS = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.radioButtonASCII = new System.Windows.Forms.RadioButton();
            this.radioButtonHex = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBoxSendData = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.numDataLength = new System.Windows.Forms.NumericUpDown();
            this.label27 = new System.Windows.Forms.Label();
            this.btnGetHeadData = new System.Windows.Forms.Button();
            this.numeCmd = new System.Windows.Forms.NumericUpDown();
            this.label24 = new System.Windows.Forms.Label();
            this.tabPage_Misc = new System.Windows.Forms.TabPage();
            this.groupBoxTemperaturProfil = new System.Windows.Forms.GroupBox();
            this.button_TemperatureProfileState = new System.Windows.Forms.Button();
            this.label_TemperatureProfileState = new System.Windows.Forms.Label();
            this.label_TemperatureProfileStateLabel = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.button3m_ButtonMove = new System.Windows.Forms.Button();
            this.dividerPanel1 = new DividerPanel.DividerPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.m_TabPageWaveMapping = new System.Windows.Forms.TabPage();
            this.userControl81 = new WaveFormTool.WaveMappingControl();
            this.m_GroupBoxMoveTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownY2Length)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownPosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownTimeOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownPosY)).BeginInit();
            this.m_GroupBoxPosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownPosY1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownDebug3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownDebug4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownDebug1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownDebug2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownSpeedSet)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPagegeneral.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownLeftUV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownRightUV)).BeginInit();
            this.tabPageColorDeep.SuspendLayout();
            this.groupBoxGraymapSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeadBoard)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.tabPageRabillyDebug.SuspendLayout();
            this.m_GroupBoxTemperature.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelayTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownPulseWidthSample)).BeginInit();
            this.tabPageInkTest.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTA)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelayTime1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPulseWidth)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFireFreq)).BeginInit();
            this.tabPageAccSpeedTest.SuspendLayout();
            this.groupBoxShakeSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericShockInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericIgnitionNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAccTimeY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAccTimeX)).BeginInit();
            this.tabPageVPrint.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBoxGzCarton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAutoCappingDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitMediaReadyTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabPageHeadData.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWaveformId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHbId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValue)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDataLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeCmd)).BeginInit();
            this.tabPage_Misc.SuspendLayout();
            this.groupBoxTemperaturProfil.SuspendLayout();
            this.dividerPanel1.SuspendLayout();
            this.m_TabPageWaveMapping.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.m_ButtonCancel, "m_ButtonCancel");
            this.m_ButtonCancel.Name = "m_ButtonCancel";
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.m_ButtonOK, "m_ButtonOK");
            this.m_ButtonOK.Name = "m_ButtonOK";
            // 
            // m_GroupBoxMoveTest
            // 
            this.m_GroupBoxMoveTest.Controls.Add(this.m_NumericUpDownY2Length);
            this.m_GroupBoxMoveTest.Controls.Add(this.label32);
            this.m_GroupBoxMoveTest.Controls.Add(this.m_ButtonMoveNew);
            this.m_GroupBoxMoveTest.Controls.Add(this.m_LabelSpeed);
            this.m_GroupBoxMoveTest.Controls.Add(this.m_ComboBoxSpeed);
            this.m_GroupBoxMoveTest.Controls.Add(this.m_ComboBoxDirection);
            this.m_GroupBoxMoveTest.Controls.Add(this.m_NumericUpDownLength);
            this.m_GroupBoxMoveTest.Controls.Add(this.m_LabelLength);
            this.m_GroupBoxMoveTest.Controls.Add(this.m_LabelDirection);
            this.m_GroupBoxMoveTest.Controls.Add(this.m_ButtonMove);
            this.m_GroupBoxMoveTest.Controls.Add(this.m_ButtonStop);
            this.m_GroupBoxMoveTest.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.m_GroupBoxMoveTest, "m_GroupBoxMoveTest");
            this.m_GroupBoxMoveTest.Name = "m_GroupBoxMoveTest";
            this.m_GroupBoxMoveTest.TabStop = false;
            // 
            // m_NumericUpDownY2Length
            // 
            resources.ApplyResources(this.m_NumericUpDownY2Length, "m_NumericUpDownY2Length");
            this.m_NumericUpDownY2Length.Maximum = new decimal(new int[] {
            2000000000,
            0,
            0,
            0});
            this.m_NumericUpDownY2Length.Name = "m_NumericUpDownY2Length";
            this.m_NumericUpDownY2Length.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label32.Name = "label32";
            // 
            // m_ButtonMoveNew
            // 
            resources.ApplyResources(this.m_ButtonMoveNew, "m_ButtonMoveNew");
            this.m_ButtonMoveNew.Name = "m_ButtonMoveNew";
            this.m_ButtonMoveNew.Click += new System.EventHandler(this.button3m_ButtonMove_Click);
            // 
            // m_LabelSpeed
            // 
            resources.ApplyResources(this.m_LabelSpeed, "m_LabelSpeed");
            this.m_LabelSpeed.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_LabelSpeed.Name = "m_LabelSpeed";
            // 
            // m_ComboBoxSpeed
            // 
            resources.ApplyResources(this.m_ComboBoxSpeed, "m_ComboBoxSpeed");
            this.m_ComboBoxSpeed.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxSpeed.Items"),
            resources.GetString("m_ComboBoxSpeed.Items1"),
            resources.GetString("m_ComboBoxSpeed.Items2"),
            resources.GetString("m_ComboBoxSpeed.Items3"),
            resources.GetString("m_ComboBoxSpeed.Items4"),
            resources.GetString("m_ComboBoxSpeed.Items5"),
            resources.GetString("m_ComboBoxSpeed.Items6")});
            this.m_ComboBoxSpeed.Name = "m_ComboBoxSpeed";
            // 
            // m_ComboBoxDirection
            // 
            this.m_ComboBoxDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxDirection, "m_ComboBoxDirection");
            this.m_ComboBoxDirection.Name = "m_ComboBoxDirection";
            // 
            // m_NumericUpDownLength
            // 
            resources.ApplyResources(this.m_NumericUpDownLength, "m_NumericUpDownLength");
            this.m_NumericUpDownLength.Maximum = new decimal(new int[] {
            2000000000,
            0,
            0,
            0});
            this.m_NumericUpDownLength.Name = "m_NumericUpDownLength";
            this.m_NumericUpDownLength.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // m_LabelLength
            // 
            resources.ApplyResources(this.m_LabelLength, "m_LabelLength");
            this.m_LabelLength.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_LabelLength.Name = "m_LabelLength";
            // 
            // m_LabelDirection
            // 
            resources.ApplyResources(this.m_LabelDirection, "m_LabelDirection");
            this.m_LabelDirection.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_LabelDirection.Name = "m_LabelDirection";
            // 
            // m_ButtonMove
            // 
            resources.ApplyResources(this.m_ButtonMove, "m_ButtonMove");
            this.m_ButtonMove.Name = "m_ButtonMove";
            this.m_ButtonMove.Click += new System.EventHandler(this.m_ButtonMove_Click);
            // 
            // m_ButtonStop
            // 
            resources.ApplyResources(this.m_ButtonStop, "m_ButtonStop");
            this.m_ButtonStop.Name = "m_ButtonStop";
            this.m_ButtonStop.Click += new System.EventHandler(this.m_ButtonStop_Click);
            // 
            // m_ButtonPosition
            // 
            resources.ApplyResources(this.m_ButtonPosition, "m_ButtonPosition");
            this.m_ButtonPosition.Name = "m_ButtonPosition";
            this.m_ButtonPosition.Click += new System.EventHandler(this.m_ButtonPosition_Click);
            // 
            // m_NumericUpDownPosX
            // 
            resources.ApplyResources(this.m_NumericUpDownPosX, "m_NumericUpDownPosX");
            this.m_NumericUpDownPosX.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.m_NumericUpDownPosX.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownPosX.Name = "m_NumericUpDownPosX";
            this.m_NumericUpDownPosX.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // m_NumericUpDownTimeOut
            // 
            resources.ApplyResources(this.m_NumericUpDownTimeOut, "m_NumericUpDownTimeOut");
            this.m_NumericUpDownTimeOut.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.m_NumericUpDownTimeOut.Name = "m_NumericUpDownTimeOut";
            this.m_NumericUpDownTimeOut.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // m_LabelTimeOut
            // 
            resources.ApplyResources(this.m_LabelTimeOut, "m_LabelTimeOut");
            this.m_LabelTimeOut.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_LabelTimeOut.Name = "m_LabelTimeOut";
            // 
            // m_ButtonSetTimeout
            // 
            resources.ApplyResources(this.m_ButtonSetTimeout, "m_ButtonSetTimeout");
            this.m_ButtonSetTimeout.Name = "m_ButtonSetTimeout";
            this.m_ButtonSetTimeout.Click += new System.EventHandler(this.m_ButtonSetTimeout_Click);
            // 
            // m_LabelCopyRight
            // 
            resources.ApplyResources(this.m_LabelCopyRight, "m_LabelCopyRight");
            this.m_LabelCopyRight.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_LabelCopyRight.Name = "m_LabelCopyRight";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.Name = "label2";
            // 
            // m_NumericUpDownPosY
            // 
            resources.ApplyResources(this.m_NumericUpDownPosY, "m_NumericUpDownPosY");
            this.m_NumericUpDownPosY.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.m_NumericUpDownPosY.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownPosY.Name = "m_NumericUpDownPosY";
            this.m_NumericUpDownPosY.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // m_GroupBoxPosition
            // 
            this.m_GroupBoxPosition.Controls.Add(this.m_NumericUpDownPosY1);
            this.m_GroupBoxPosition.Controls.Add(this.label21);
            this.m_GroupBoxPosition.Controls.Add(this.label5);
            this.m_GroupBoxPosition.Controls.Add(this.label6);
            this.m_GroupBoxPosition.Controls.Add(this.m_NumericUpDownDebug3);
            this.m_GroupBoxPosition.Controls.Add(this.m_NumericUpDownDebug4);
            this.m_GroupBoxPosition.Controls.Add(this.label3);
            this.m_GroupBoxPosition.Controls.Add(this.label4);
            this.m_GroupBoxPosition.Controls.Add(this.m_NumericUpDownDebug1);
            this.m_GroupBoxPosition.Controls.Add(this.m_NumericUpDownDebug2);
            this.m_GroupBoxPosition.Controls.Add(this.label2);
            this.m_GroupBoxPosition.Controls.Add(this.label1);
            this.m_GroupBoxPosition.Controls.Add(this.m_ButtonPosition);
            this.m_GroupBoxPosition.Controls.Add(this.m_NumericUpDownPosX);
            this.m_GroupBoxPosition.Controls.Add(this.m_NumericUpDownPosY);
            this.m_GroupBoxPosition.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.m_GroupBoxPosition, "m_GroupBoxPosition");
            this.m_GroupBoxPosition.Name = "m_GroupBoxPosition";
            this.m_GroupBoxPosition.TabStop = false;
            // 
            // m_NumericUpDownPosY1
            // 
            resources.ApplyResources(this.m_NumericUpDownPosY1, "m_NumericUpDownPosY1");
            this.m_NumericUpDownPosY1.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.m_NumericUpDownPosY1.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownPosY1.Name = "m_NumericUpDownPosY1";
            this.m_NumericUpDownPosY1.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label21.Name = "label21";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label6.Name = "label6";
            // 
            // m_NumericUpDownDebug3
            // 
            resources.ApplyResources(this.m_NumericUpDownDebug3, "m_NumericUpDownDebug3");
            this.m_NumericUpDownDebug3.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.m_NumericUpDownDebug3.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownDebug3.Name = "m_NumericUpDownDebug3";
            this.m_NumericUpDownDebug3.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // m_NumericUpDownDebug4
            // 
            this.m_NumericUpDownDebug4.Hexadecimal = true;
            resources.ApplyResources(this.m_NumericUpDownDebug4, "m_NumericUpDownDebug4");
            this.m_NumericUpDownDebug4.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.m_NumericUpDownDebug4.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownDebug4.Name = "m_NumericUpDownDebug4";
            this.m_NumericUpDownDebug4.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label4.Name = "label4";
            // 
            // m_NumericUpDownDebug1
            // 
            resources.ApplyResources(this.m_NumericUpDownDebug1, "m_NumericUpDownDebug1");
            this.m_NumericUpDownDebug1.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.m_NumericUpDownDebug1.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownDebug1.Name = "m_NumericUpDownDebug1";
            this.m_NumericUpDownDebug1.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // m_NumericUpDownDebug2
            // 
            resources.ApplyResources(this.m_NumericUpDownDebug2, "m_NumericUpDownDebug2");
            this.m_NumericUpDownDebug2.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.m_NumericUpDownDebug2.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownDebug2.Name = "m_NumericUpDownDebug2";
            this.m_NumericUpDownDebug2.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_ButtonWriteSpeed);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.m_NumericUpDownSpeedSet);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // m_ButtonWriteSpeed
            // 
            resources.ApplyResources(this.m_ButtonWriteSpeed, "m_ButtonWriteSpeed");
            this.m_ButtonWriteSpeed.Name = "m_ButtonWriteSpeed";
            this.m_ButtonWriteSpeed.Click += new System.EventHandler(this.m_ButtonWriteSpeed_Click);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label10.Name = "label10";
            // 
            // m_NumericUpDownSpeedSet
            // 
            resources.ApplyResources(this.m_NumericUpDownSpeedSet, "m_NumericUpDownSpeedSet");
            this.m_NumericUpDownSpeedSet.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.m_NumericUpDownSpeedSet.Name = "m_NumericUpDownSpeedSet";
            this.m_ToolTip.SetToolTip(this.m_NumericUpDownSpeedSet, resources.GetString("m_NumericUpDownSpeedSet.ToolTip"));
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chfloatstatusnum,
            this.chfloatprestatus,
            this.chfloatcurStatus,
            this.pumpTimeMS});
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.Name = "listView1";
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // chfloatstatusnum
            // 
            resources.ApplyResources(this.chfloatstatusnum, "chfloatstatusnum");
            // 
            // chfloatprestatus
            // 
            resources.ApplyResources(this.chfloatprestatus, "chfloatprestatus");
            // 
            // chfloatcurStatus
            // 
            resources.ApplyResources(this.chfloatcurStatus, "chfloatcurStatus");
            // 
            // pumpTimeMS
            // 
            resources.ApplyResources(this.pumpTimeMS, "pumpTimeMS");
            // 
            // buttonGetInkStatus
            // 
            resources.ApplyResources(this.buttonGetInkStatus, "buttonGetInkStatus");
            this.buttonGetInkStatus.Name = "buttonGetInkStatus";
            this.buttonGetInkStatus.Click += new System.EventHandler(this.buttonGetInkStatus_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPagegeneral);
            this.tabControl1.Controls.Add(this.tabPageColorDeep);
            this.tabControl1.Controls.Add(this.tabPageRabillyDebug);
            this.tabControl1.Controls.Add(this.tabPageInkTest);
            this.tabControl1.Controls.Add(this.tabPageAccSpeedTest);
            this.tabControl1.Controls.Add(this.tabPageVPrint);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPageHeadData);
            this.tabControl1.Controls.Add(this.m_TabPageWaveMapping);
            this.tabControl1.Controls.Add(this.tabPage_Misc);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPagegeneral
            // 
            this.tabPagegeneral.Controls.Add(this.button_Calibrate);
            this.tabPagegeneral.Controls.Add(this.textBox_CalibrateCmd);
            this.tabPagegeneral.Controls.Add(this.label34);
            this.tabPagegeneral.Controls.Add(this.label33);
            this.tabPagegeneral.Controls.Add(this.textBoxerrorCode);
            this.tabPagegeneral.Controls.Add(this.buttonErrorInfoTest);
            this.tabPagegeneral.Controls.Add(this.label9);
            this.tabPagegeneral.Controls.Add(this.m_ComboBoxVenderID);
            this.tabPagegeneral.Controls.Add(this.txtSerialCmdLen);
            this.tabPagegeneral.Controls.Add(this.btnSendSerialCmd);
            this.tabPagegeneral.Controls.Add(this.txtSerialCmd);
            this.tabPagegeneral.Controls.Add(this.groupBox1);
            this.tabPagegeneral.Controls.Add(this.buttonGetInkStatus);
            this.tabPagegeneral.Controls.Add(this.m_GroupBoxPosition);
            this.tabPagegeneral.Controls.Add(this.m_NumericUpDownTimeOut);
            this.tabPagegeneral.Controls.Add(this.m_GroupBoxMoveTest);
            this.tabPagegeneral.Controls.Add(this.m_LabelTimeOut);
            this.tabPagegeneral.Controls.Add(this.m_ButtonSetTimeout);
            this.tabPagegeneral.Controls.Add(this.listView1);
            this.tabPagegeneral.Controls.Add(this.groupBox2);
            this.tabPagegeneral.Controls.Add(this.btnAutoTestclb);
            resources.ApplyResources(this.tabPagegeneral, "tabPagegeneral");
            this.tabPagegeneral.Name = "tabPagegeneral";
            // 
            // button_Calibrate
            // 
            resources.ApplyResources(this.button_Calibrate, "button_Calibrate");
            this.button_Calibrate.Name = "button_Calibrate";
            this.button_Calibrate.UseVisualStyleBackColor = true;
            this.button_Calibrate.Click += new System.EventHandler(this.button_Calibrate_Click);
            // 
            // textBox_CalibrateCmd
            // 
            resources.ApplyResources(this.textBox_CalibrateCmd, "textBox_CalibrateCmd");
            this.textBox_CalibrateCmd.Name = "textBox_CalibrateCmd";
            // 
            // label34
            // 
            resources.ApplyResources(this.label34, "label34");
            this.label34.Name = "label34";
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            // 
            // textBoxerrorCode
            // 
            resources.ApplyResources(this.textBoxerrorCode, "textBoxerrorCode");
            this.textBoxerrorCode.Name = "textBoxerrorCode";
            // 
            // buttonErrorInfoTest
            // 
            resources.ApplyResources(this.buttonErrorInfoTest, "buttonErrorInfoTest");
            this.buttonErrorInfoTest.Name = "buttonErrorInfoTest";
            this.buttonErrorInfoTest.UseVisualStyleBackColor = true;
            this.buttonErrorInfoTest.Click += new System.EventHandler(this.buttonErrorInfoTest_Click);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // m_ComboBoxVenderID
            // 
            this.m_ComboBoxVenderID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxVenderID, "m_ComboBoxVenderID");
            this.m_ComboBoxVenderID.Name = "m_ComboBoxVenderID";
            // 
            // txtSerialCmdLen
            // 
            resources.ApplyResources(this.txtSerialCmdLen, "txtSerialCmdLen");
            this.txtSerialCmdLen.Name = "txtSerialCmdLen";
            this.txtSerialCmdLen.ReadOnly = true;
            // 
            // btnSendSerialCmd
            // 
            resources.ApplyResources(this.btnSendSerialCmd, "btnSendSerialCmd");
            this.btnSendSerialCmd.Name = "btnSendSerialCmd";
            this.btnSendSerialCmd.Click += new System.EventHandler(this.btnSendSerialCmd_Click);
            // 
            // txtSerialCmd
            // 
            resources.ApplyResources(this.txtSerialCmd, "txtSerialCmd");
            this.txtSerialCmd.Name = "txtSerialCmd";
            this.txtSerialCmd.TextChanged += new System.EventHandler(this.txtSerialCmd_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.m_ButtonWriteUV);
            this.groupBox1.Controls.Add(this.m_ButtonReadUV);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.m_NumericUpDownLeftUV);
            this.groupBox1.Controls.Add(this.m_NumericUpDownRightUV);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label8.Name = "label8";
            // 
            // m_ButtonWriteUV
            // 
            resources.ApplyResources(this.m_ButtonWriteUV, "m_ButtonWriteUV");
            this.m_ButtonWriteUV.Name = "m_ButtonWriteUV";
            this.m_ButtonWriteUV.Click += new System.EventHandler(this.m_ButtonWriteUV_Click);
            // 
            // m_ButtonReadUV
            // 
            resources.ApplyResources(this.m_ButtonReadUV, "m_ButtonReadUV");
            this.m_ButtonReadUV.Name = "m_ButtonReadUV";
            this.m_ButtonReadUV.Click += new System.EventHandler(this.m_ButtonReadUV_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label7.Name = "label7";
            // 
            // m_NumericUpDownLeftUV
            // 
            resources.ApplyResources(this.m_NumericUpDownLeftUV, "m_NumericUpDownLeftUV");
            this.m_NumericUpDownLeftUV.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.m_NumericUpDownLeftUV.Minimum = new decimal(new int[] {
            100000000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownLeftUV.Name = "m_NumericUpDownLeftUV";
            // 
            // m_NumericUpDownRightUV
            // 
            resources.ApplyResources(this.m_NumericUpDownRightUV, "m_NumericUpDownRightUV");
            this.m_NumericUpDownRightUV.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.m_NumericUpDownRightUV.Minimum = new decimal(new int[] {
            100000000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownRightUV.Name = "m_NumericUpDownRightUV";
            // 
            // btnAutoTestclb
            // 
            resources.ApplyResources(this.btnAutoTestclb, "btnAutoTestclb");
            this.btnAutoTestclb.Name = "btnAutoTestclb";
            this.btnAutoTestclb.Click += new System.EventHandler(this.btnAutoTestclb_Click);
            // 
            // tabPageColorDeep
            // 
            this.tabPageColorDeep.Controls.Add(this.groupBoxGraymapSet);
            this.tabPageColorDeep.Controls.Add(this.groupBox4);
            this.tabPageColorDeep.Controls.Add(this.buttonFeedingControl);
            this.tabPageColorDeep.Controls.Add(this.Panel1);
            this.tabPageColorDeep.Controls.Add(this.buttonRead);
            this.tabPageColorDeep.Controls.Add(this.button4);
            this.tabPageColorDeep.Controls.Add(this.cboColorDeep);
            resources.ApplyResources(this.tabPageColorDeep, "tabPageColorDeep");
            this.tabPageColorDeep.Name = "tabPageColorDeep";
            // 
            // groupBoxGraymapSet
            // 
            this.groupBoxGraymapSet.Controls.Add(this.comboBoxPrintHead);
            this.groupBoxGraymapSet.Controls.Add(this.comboBoxGrayMap3);
            this.groupBoxGraymapSet.Controls.Add(this.comboBoxGrayMap2);
            this.groupBoxGraymapSet.Controls.Add(this.comboBoxGrayMap1);
            this.groupBoxGraymapSet.Controls.Add(this.comboBoxDotBit);
            this.groupBoxGraymapSet.Controls.Add(this.numericUpDownHeadBoard);
            this.groupBoxGraymapSet.Controls.Add(this.buttonGrayMapSet);
            this.groupBoxGraymapSet.Controls.Add(this.labelGrayMap);
            this.groupBoxGraymapSet.Controls.Add(this.labelDotbit);
            this.groupBoxGraymapSet.Controls.Add(this.labelPrinthead);
            this.groupBoxGraymapSet.Controls.Add(this.labelHeaderBoard);
            resources.ApplyResources(this.groupBoxGraymapSet, "groupBoxGraymapSet");
            this.groupBoxGraymapSet.Name = "groupBoxGraymapSet";
            this.groupBoxGraymapSet.TabStop = false;
            // 
            // comboBoxPrintHead
            // 
            this.comboBoxPrintHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPrintHead.FormattingEnabled = true;
            this.comboBoxPrintHead.Items.AddRange(new object[] {
            resources.GetString("comboBoxPrintHead.Items"),
            resources.GetString("comboBoxPrintHead.Items1"),
            resources.GetString("comboBoxPrintHead.Items2"),
            resources.GetString("comboBoxPrintHead.Items3"),
            resources.GetString("comboBoxPrintHead.Items4"),
            resources.GetString("comboBoxPrintHead.Items5"),
            resources.GetString("comboBoxPrintHead.Items6"),
            resources.GetString("comboBoxPrintHead.Items7")});
            resources.ApplyResources(this.comboBoxPrintHead, "comboBoxPrintHead");
            this.comboBoxPrintHead.Name = "comboBoxPrintHead";
            // 
            // comboBoxGrayMap3
            // 
            this.comboBoxGrayMap3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGrayMap3.FormattingEnabled = true;
            this.comboBoxGrayMap3.Items.AddRange(new object[] {
            resources.GetString("comboBoxGrayMap3.Items"),
            resources.GetString("comboBoxGrayMap3.Items1"),
            resources.GetString("comboBoxGrayMap3.Items2"),
            resources.GetString("comboBoxGrayMap3.Items3"),
            resources.GetString("comboBoxGrayMap3.Items4"),
            resources.GetString("comboBoxGrayMap3.Items5"),
            resources.GetString("comboBoxGrayMap3.Items6"),
            resources.GetString("comboBoxGrayMap3.Items7")});
            resources.ApplyResources(this.comboBoxGrayMap3, "comboBoxGrayMap3");
            this.comboBoxGrayMap3.Name = "comboBoxGrayMap3";
            // 
            // comboBoxGrayMap2
            // 
            this.comboBoxGrayMap2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGrayMap2.FormattingEnabled = true;
            this.comboBoxGrayMap2.Items.AddRange(new object[] {
            resources.GetString("comboBoxGrayMap2.Items"),
            resources.GetString("comboBoxGrayMap2.Items1"),
            resources.GetString("comboBoxGrayMap2.Items2"),
            resources.GetString("comboBoxGrayMap2.Items3"),
            resources.GetString("comboBoxGrayMap2.Items4"),
            resources.GetString("comboBoxGrayMap2.Items5"),
            resources.GetString("comboBoxGrayMap2.Items6"),
            resources.GetString("comboBoxGrayMap2.Items7")});
            resources.ApplyResources(this.comboBoxGrayMap2, "comboBoxGrayMap2");
            this.comboBoxGrayMap2.Name = "comboBoxGrayMap2";
            // 
            // comboBoxGrayMap1
            // 
            this.comboBoxGrayMap1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGrayMap1.FormattingEnabled = true;
            this.comboBoxGrayMap1.Items.AddRange(new object[] {
            resources.GetString("comboBoxGrayMap1.Items"),
            resources.GetString("comboBoxGrayMap1.Items1"),
            resources.GetString("comboBoxGrayMap1.Items2"),
            resources.GetString("comboBoxGrayMap1.Items3"),
            resources.GetString("comboBoxGrayMap1.Items4"),
            resources.GetString("comboBoxGrayMap1.Items5"),
            resources.GetString("comboBoxGrayMap1.Items6"),
            resources.GetString("comboBoxGrayMap1.Items7")});
            resources.ApplyResources(this.comboBoxGrayMap1, "comboBoxGrayMap1");
            this.comboBoxGrayMap1.Name = "comboBoxGrayMap1";
            // 
            // comboBoxDotBit
            // 
            this.comboBoxDotBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDotBit.FormattingEnabled = true;
            this.comboBoxDotBit.Items.AddRange(new object[] {
            resources.GetString("comboBoxDotBit.Items"),
            resources.GetString("comboBoxDotBit.Items1")});
            resources.ApplyResources(this.comboBoxDotBit, "comboBoxDotBit");
            this.comboBoxDotBit.Name = "comboBoxDotBit";
            this.comboBoxDotBit.SelectedIndexChanged += new System.EventHandler(this.comboBoxDotBit_SelectedIndexChanged);
            // 
            // numericUpDownHeadBoard
            // 
            resources.ApplyResources(this.numericUpDownHeadBoard, "numericUpDownHeadBoard");
            this.numericUpDownHeadBoard.Name = "numericUpDownHeadBoard";
            // 
            // buttonGrayMapSet
            // 
            resources.ApplyResources(this.buttonGrayMapSet, "buttonGrayMapSet");
            this.buttonGrayMapSet.Name = "buttonGrayMapSet";
            this.buttonGrayMapSet.UseVisualStyleBackColor = true;
            this.buttonGrayMapSet.Click += new System.EventHandler(this.buttonGrayMapSet_Click);
            // 
            // labelGrayMap
            // 
            resources.ApplyResources(this.labelGrayMap, "labelGrayMap");
            this.labelGrayMap.Name = "labelGrayMap";
            // 
            // labelDotbit
            // 
            resources.ApplyResources(this.labelDotbit, "labelDotbit");
            this.labelDotbit.Name = "labelDotbit";
            // 
            // labelPrinthead
            // 
            resources.ApplyResources(this.labelPrinthead, "labelPrinthead");
            this.labelPrinthead.Name = "labelPrinthead";
            // 
            // labelHeaderBoard
            // 
            resources.ApplyResources(this.labelHeaderBoard, "labelHeaderBoard");
            this.labelHeaderBoard.Name = "labelHeaderBoard";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonTrayControlApply);
            this.groupBox4.Controls.Add(this.buttonTrayControl);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // buttonTrayControlApply
            // 
            resources.ApplyResources(this.buttonTrayControlApply, "buttonTrayControlApply");
            this.buttonTrayControlApply.Name = "buttonTrayControlApply";
            this.buttonTrayControlApply.UseVisualStyleBackColor = true;
            this.buttonTrayControlApply.Click += new System.EventHandler(this.buttonTrayControlApply_Click);
            // 
            // buttonTrayControl
            // 
            resources.ApplyResources(this.buttonTrayControl, "buttonTrayControl");
            this.buttonTrayControl.Name = "buttonTrayControl";
            this.buttonTrayControl.UseVisualStyleBackColor = true;
            this.buttonTrayControl.CheckedChanged += new System.EventHandler(this.buttonTrayControl_CheckedChanged);
            // 
            // buttonFeedingControl
            // 
            resources.ApplyResources(this.buttonFeedingControl, "buttonFeedingControl");
            this.buttonFeedingControl.Name = "buttonFeedingControl";
            this.buttonFeedingControl.UseVisualStyleBackColor = true;
            this.buttonFeedingControl.Click += new System.EventHandler(this.button3_Click);
            // 
            // Panel1
            // 
            resources.ApplyResources(this.Panel1, "Panel1");
            this.Panel1.Name = "Panel1";
            // 
            // buttonRead
            // 
            resources.ApplyResources(this.buttonRead, "buttonRead");
            this.buttonRead.Name = "buttonRead";
            this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // cboColorDeep
            // 
            resources.ApplyResources(this.cboColorDeep, "cboColorDeep");
            this.cboColorDeep.Items.AddRange(new object[] {
            resources.GetString("cboColorDeep.Items"),
            resources.GetString("cboColorDeep.Items1"),
            resources.GetString("cboColorDeep.Items2")});
            this.cboColorDeep.Name = "cboColorDeep";
            this.cboColorDeep.SelectedIndexChanged += new System.EventHandler(this.cboColorDeep_SelectedIndexChanged);
            // 
            // tabPageRabillyDebug
            // 
            this.tabPageRabillyDebug.Controls.Add(this.m_GroupBoxTemperature);
            this.tabPageRabillyDebug.Controls.Add(this.m_ButtonToBoard);
            this.tabPageRabillyDebug.Controls.Add(this.m_ButtonRefresh);
            resources.ApplyResources(this.tabPageRabillyDebug, "tabPageRabillyDebug");
            this.tabPageRabillyDebug.Name = "tabPageRabillyDebug";
            // 
            // m_GroupBoxTemperature
            // 
            this.m_GroupBoxTemperature.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxTemperature.BorderThickness = 1F;
            this.m_GroupBoxTemperature.Controls.Add(this.label11);
            this.m_GroupBoxTemperature.Controls.Add(this.m_LabelHead);
            this.m_GroupBoxTemperature.Controls.Add(this.m_LabelHorSample);
            this.m_GroupBoxTemperature.Controls.Add(this.m_LabelCurPulseWidth);
            this.m_GroupBoxTemperature.Controls.Add(this.numericUpDownDelayTime);
            this.m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownPulseWidthSample);
            resources.ApplyResources(this.m_GroupBoxTemperature, "m_GroupBoxTemperature");
            style1.Color1 = System.Drawing.Color.LightBlue;
            style1.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxTemperature.GradientColors = style1;
            this.m_GroupBoxTemperature.GroupImage = null;
            this.m_GroupBoxTemperature.Name = "m_GroupBoxTemperature";
            this.m_GroupBoxTemperature.PaintGroupBox = false;
            this.m_GroupBoxTemperature.RoundCorners = 10;
            this.m_GroupBoxTemperature.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxTemperature.ShadowControl = false;
            this.m_GroupBoxTemperature.ShadowThickness = 3;
            this.m_GroupBoxTemperature.TabStop = false;
            style2.Color1 = System.Drawing.Color.LightBlue;
            style2.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxTemperature.TitileGradientColors = style2;
            this.m_GroupBoxTemperature.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxTemperature.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Name = "label11";
            // 
            // m_LabelHead
            // 
            resources.ApplyResources(this.m_LabelHead, "m_LabelHead");
            this.m_LabelHead.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelHead.Name = "m_LabelHead";
            // 
            // m_LabelHorSample
            // 
            resources.ApplyResources(this.m_LabelHorSample, "m_LabelHorSample");
            this.m_LabelHorSample.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelHorSample.Name = "m_LabelHorSample";
            // 
            // m_LabelCurPulseWidth
            // 
            resources.ApplyResources(this.m_LabelCurPulseWidth, "m_LabelCurPulseWidth");
            this.m_LabelCurPulseWidth.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelCurPulseWidth.Name = "m_LabelCurPulseWidth";
            // 
            // numericUpDownDelayTime
            // 
            this.numericUpDownDelayTime.DecimalPlaces = 1;
            resources.ApplyResources(this.numericUpDownDelayTime, "numericUpDownDelayTime");
            this.numericUpDownDelayTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownDelayTime.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDownDelayTime.Name = "numericUpDownDelayTime";
            // 
            // m_NumericUpDownPulseWidthSample
            // 
            this.m_NumericUpDownPulseWidthSample.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownPulseWidthSample, "m_NumericUpDownPulseWidthSample");
            this.m_NumericUpDownPulseWidthSample.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.m_NumericUpDownPulseWidthSample.Name = "m_NumericUpDownPulseWidthSample";
            this.m_NumericUpDownPulseWidthSample.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.m_NumericUpDownPulseWidthSample.ValueChanged += new System.EventHandler(this.m_NumericUpDownVolBaseSample_ValueChanged);
            this.m_NumericUpDownPulseWidthSample.Enter += new System.EventHandler(this.m_NumericUpDownTempSetSample_Enter);
            // 
            // m_ButtonToBoard
            // 
            resources.ApplyResources(this.m_ButtonToBoard, "m_ButtonToBoard");
            this.m_ButtonToBoard.Name = "m_ButtonToBoard";
            this.m_ButtonToBoard.Click += new System.EventHandler(this.m_ButtonToBoard_Click);
            // 
            // m_ButtonRefresh
            // 
            resources.ApplyResources(this.m_ButtonRefresh, "m_ButtonRefresh");
            this.m_ButtonRefresh.Name = "m_ButtonRefresh";
            this.m_ButtonRefresh.Click += new System.EventHandler(this.m_ButtonRefresh_Click);
            // 
            // tabPageInkTest
            // 
            this.tabPageInkTest.Controls.Add(this.autoInkTest1);
            this.tabPageInkTest.Controls.Add(this.panel2);
            this.tabPageInkTest.Controls.Add(this.buttonSet);
            this.tabPageInkTest.Controls.Add(this.buttonGet);
            resources.ApplyResources(this.tabPageInkTest, "tabPageInkTest");
            this.tabPageInkTest.Name = "tabPageInkTest";
            // 
            // autoInkTest1
            // 
            this.autoInkTest1.Divider = false;
            resources.ApplyResources(this.autoInkTest1, "autoInkTest1");
            style3.Color1 = System.Drawing.SystemColors.Control;
            style3.Color2 = System.Drawing.SystemColors.Control;
            this.autoInkTest1.GradientColors = style3;
            this.autoInkTest1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.autoInkTest1.GrouperTitleStyle = null;
            this.autoInkTest1.IsDirty = false;
            this.autoInkTest1.Name = "autoInkTest1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel5);
            this.panel2.Controls.Add(this.tableLayoutPanel4);
            this.panel2.Controls.Add(this.tableLayoutPanel3);
            this.panel2.Controls.Add(this.tableLayoutPanel2);
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // tableLayoutPanel5
            // 
            resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
            this.tableLayoutPanel5.Controls.Add(this.label22, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.numericUpDown2, 1, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DecimalPlaces = 3;
            resources.ApplyResources(this.numericUpDown2, "numericUpDown2");
            this.numericUpDown2.Name = "numericUpDown2";
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.label15, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.numericUpDownTA, 1, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // numericUpDownTA
            // 
            this.numericUpDownTA.DecimalPlaces = 3;
            resources.ApplyResources(this.numericUpDownTA, "numericUpDownTA");
            this.numericUpDownTA.Name = "numericUpDownTA";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.label14, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.numericUpDownDelayTime1, 1, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // numericUpDownDelayTime1
            // 
            this.numericUpDownDelayTime1.DecimalPlaces = 3;
            resources.ApplyResources(this.numericUpDownDelayTime1, "numericUpDownDelayTime1");
            this.numericUpDownDelayTime1.Name = "numericUpDownDelayTime1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.numericUpDownPulseWidth, 1, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // numericUpDownPulseWidth
            // 
            this.numericUpDownPulseWidth.DecimalPlaces = 3;
            resources.ApplyResources(this.numericUpDownPulseWidth, "numericUpDownPulseWidth");
            this.numericUpDownPulseWidth.Name = "numericUpDownPulseWidth";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownFireFreq, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // numericUpDownFireFreq
            // 
            resources.ApplyResources(this.numericUpDownFireFreq, "numericUpDownFireFreq");
            this.numericUpDownFireFreq.Name = "numericUpDownFireFreq";
            // 
            // buttonSet
            // 
            resources.ApplyResources(this.buttonSet, "buttonSet");
            this.buttonSet.Name = "buttonSet";
            this.buttonSet.Click += new System.EventHandler(this.buttonSet_Click);
            // 
            // buttonGet
            // 
            resources.ApplyResources(this.buttonGet, "buttonGet");
            this.buttonGet.Name = "buttonGet";
            this.buttonGet.Click += new System.EventHandler(this.buttonGet_Click);
            // 
            // tabPageAccSpeedTest
            // 
            this.tabPageAccSpeedTest.Controls.Add(this.groupBoxShakeSetting);
            this.tabPageAccSpeedTest.Controls.Add(this.buttonAccTimeGet);
            this.tabPageAccSpeedTest.Controls.Add(this.label18);
            this.tabPageAccSpeedTest.Controls.Add(this.label19);
            this.tabPageAccSpeedTest.Controls.Add(this.buttonAccTimeSet);
            this.tabPageAccSpeedTest.Controls.Add(this.numAccTimeY);
            this.tabPageAccSpeedTest.Controls.Add(this.numAccTimeX);
            resources.ApplyResources(this.tabPageAccSpeedTest, "tabPageAccSpeedTest");
            this.tabPageAccSpeedTest.Name = "tabPageAccSpeedTest";
            // 
            // groupBoxShakeSetting
            // 
            this.groupBoxShakeSetting.Controls.Add(this.buttonShakeGet);
            this.groupBoxShakeSetting.Controls.Add(this.buttonShakeSet);
            this.groupBoxShakeSetting.Controls.Add(this.numericShockInterval);
            this.groupBoxShakeSetting.Controls.Add(this.labelShockInterval);
            this.groupBoxShakeSetting.Controls.Add(this.numericIgnitionNumber);
            this.groupBoxShakeSetting.Controls.Add(this.labelIgnitionNumber);
            this.groupBoxShakeSetting.Controls.Add(this.checkBoxShake);
            resources.ApplyResources(this.groupBoxShakeSetting, "groupBoxShakeSetting");
            this.groupBoxShakeSetting.Name = "groupBoxShakeSetting";
            this.groupBoxShakeSetting.TabStop = false;
            // 
            // buttonShakeGet
            // 
            resources.ApplyResources(this.buttonShakeGet, "buttonShakeGet");
            this.buttonShakeGet.Name = "buttonShakeGet";
            this.buttonShakeGet.UseVisualStyleBackColor = true;
            this.buttonShakeGet.Click += new System.EventHandler(this.buttonShakeGet_Click);
            // 
            // buttonShakeSet
            // 
            resources.ApplyResources(this.buttonShakeSet, "buttonShakeSet");
            this.buttonShakeSet.Name = "buttonShakeSet";
            this.buttonShakeSet.UseVisualStyleBackColor = true;
            this.buttonShakeSet.Click += new System.EventHandler(this.buttonShakeSet_Click);
            // 
            // numericShockInterval
            // 
            resources.ApplyResources(this.numericShockInterval, "numericShockInterval");
            this.numericShockInterval.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.numericShockInterval.Name = "numericShockInterval";
            // 
            // labelShockInterval
            // 
            resources.ApplyResources(this.labelShockInterval, "labelShockInterval");
            this.labelShockInterval.Name = "labelShockInterval";
            // 
            // numericIgnitionNumber
            // 
            resources.ApplyResources(this.numericIgnitionNumber, "numericIgnitionNumber");
            this.numericIgnitionNumber.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.numericIgnitionNumber.Name = "numericIgnitionNumber";
            // 
            // labelIgnitionNumber
            // 
            resources.ApplyResources(this.labelIgnitionNumber, "labelIgnitionNumber");
            this.labelIgnitionNumber.Name = "labelIgnitionNumber";
            // 
            // checkBoxShake
            // 
            resources.ApplyResources(this.checkBoxShake, "checkBoxShake");
            this.checkBoxShake.Name = "checkBoxShake";
            this.checkBoxShake.UseVisualStyleBackColor = true;
            // 
            // buttonAccTimeGet
            // 
            resources.ApplyResources(this.buttonAccTimeGet, "buttonAccTimeGet");
            this.buttonAccTimeGet.Name = "buttonAccTimeGet";
            this.buttonAccTimeGet.UseVisualStyleBackColor = true;
            this.buttonAccTimeGet.Click += new System.EventHandler(this.buttonAccTimeGet_Click);
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // buttonAccTimeSet
            // 
            resources.ApplyResources(this.buttonAccTimeSet, "buttonAccTimeSet");
            this.buttonAccTimeSet.Name = "buttonAccTimeSet";
            this.buttonAccTimeSet.Click += new System.EventHandler(this.buttonAccTimeSet_Click);
            // 
            // numAccTimeY
            // 
            resources.ApplyResources(this.numAccTimeY, "numAccTimeY");
            this.numAccTimeY.Name = "numAccTimeY";
            // 
            // numAccTimeX
            // 
            resources.ApplyResources(this.numAccTimeX, "numAccTimeX");
            this.numAccTimeX.Name = "numAccTimeX";
            // 
            // tabPageVPrint
            // 
            this.tabPageVPrint.Controls.Add(this.buttonStopPrint);
            this.tabPageVPrint.Controls.Add(this.buttonStartPrint);
            resources.ApplyResources(this.tabPageVPrint, "tabPageVPrint");
            this.tabPageVPrint.Name = "tabPageVPrint";
            this.tabPageVPrint.UseVisualStyleBackColor = true;
            // 
            // buttonStopPrint
            // 
            resources.ApplyResources(this.buttonStopPrint, "buttonStopPrint");
            this.buttonStopPrint.Name = "buttonStopPrint";
            this.buttonStopPrint.Click += new System.EventHandler(this.buttonStopPrint_Click);
            // 
            // buttonStartPrint
            // 
            resources.ApplyResources(this.buttonStartPrint, "buttonStartPrint");
            this.buttonStartPrint.Name = "buttonStartPrint";
            this.buttonStartPrint.Click += new System.EventHandler(this.buttonStartPrint_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.flowLayoutPanel1);
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Controls.Add(this.button5);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.comboBoxXaar382Mode);
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Controls.Add(this.buttonXaar382Mode);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.numericUpDown1);
            this.tabPage2.Controls.Add(this.buttonSetFrieFreq);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.nktDyjUserControl1);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxGzCarton);
            this.flowLayoutPanel1.Controls.Add(this.btnSaveFWLog);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // nktDyjUserControl1
            // 
            resources.ApplyResources(this.nktDyjUserControl1, "nktDyjUserControl1");
            this.nktDyjUserControl1.Name = "nktDyjUserControl1";
            // 
            // groupBoxGzCarton
            // 
            this.groupBoxGzCarton.Controls.Add(this.numAutoCappingDelay);
            this.groupBoxGzCarton.Controls.Add(this.label23);
            this.groupBoxGzCarton.Controls.Add(this.buttonReadWaitMediaReadyTim);
            this.groupBoxGzCarton.Controls.Add(this.label20);
            this.groupBoxGzCarton.Controls.Add(this.buttonWaitMediaReadyTim);
            this.groupBoxGzCarton.Controls.Add(this.numWaitMediaReadyTime);
            resources.ApplyResources(this.groupBoxGzCarton, "groupBoxGzCarton");
            this.groupBoxGzCarton.Name = "groupBoxGzCarton";
            this.groupBoxGzCarton.TabStop = false;
            // 
            // numAutoCappingDelay
            // 
            resources.ApplyResources(this.numAutoCappingDelay, "numAutoCappingDelay");
            this.numAutoCappingDelay.Name = "numAutoCappingDelay";
            this.m_ToolTip.SetToolTip(this.numAutoCappingDelay, resources.GetString("numAutoCappingDelay.ToolTip"));
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            // 
            // buttonReadWaitMediaReadyTim
            // 
            resources.ApplyResources(this.buttonReadWaitMediaReadyTim, "buttonReadWaitMediaReadyTim");
            this.buttonReadWaitMediaReadyTim.Name = "buttonReadWaitMediaReadyTim";
            this.buttonReadWaitMediaReadyTim.Click += new System.EventHandler(this.buttonReadWaitMediaReadyTim_Click);
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // buttonWaitMediaReadyTim
            // 
            resources.ApplyResources(this.buttonWaitMediaReadyTim, "buttonWaitMediaReadyTim");
            this.buttonWaitMediaReadyTim.Name = "buttonWaitMediaReadyTim";
            this.buttonWaitMediaReadyTim.Click += new System.EventHandler(this.buttonWaitMediaReadyTim_Click);
            // 
            // numWaitMediaReadyTime
            // 
            resources.ApplyResources(this.numWaitMediaReadyTime, "numWaitMediaReadyTime");
            this.numWaitMediaReadyTime.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numWaitMediaReadyTime.Name = "numWaitMediaReadyTime";
            // 
            // btnSaveFWLog
            // 
            resources.ApplyResources(this.btnSaveFWLog, "btnSaveFWLog");
            this.btnSaveFWLog.Name = "btnSaveFWLog";
            this.btnSaveFWLog.UseVisualStyleBackColor = true;
            this.btnSaveFWLog.Click += new System.EventHandler(this.btnSaveFWLog_Click);
            // 
            // button6
            // 
            resources.ApplyResources(this.button6, "button6");
            this.button6.Name = "button6";
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            resources.ApplyResources(this.button5, "button5");
            this.button5.Name = "button5";
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // comboBoxXaar382Mode
            // 
            this.comboBoxXaar382Mode.FormattingEnabled = true;
            this.comboBoxXaar382Mode.Items.AddRange(new object[] {
            resources.GetString("comboBoxXaar382Mode.Items"),
            resources.GetString("comboBoxXaar382Mode.Items1")});
            resources.ApplyResources(this.comboBoxXaar382Mode, "comboBoxXaar382Mode");
            this.comboBoxXaar382Mode.Name = "comboBoxXaar382Mode";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // buttonXaar382Mode
            // 
            resources.ApplyResources(this.buttonXaar382Mode, "buttonXaar382Mode");
            this.buttonXaar382Mode.Name = "buttonXaar382Mode";
            this.buttonXaar382Mode.Click += new System.EventHandler(this.buttonXaar382Mode_Click);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // numericUpDown1
            // 
            resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
            this.numericUpDown1.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            // 
            // buttonSetFrieFreq
            // 
            resources.ApplyResources(this.buttonSetFrieFreq, "buttonSetFrieFreq");
            this.buttonSetFrieFreq.Name = "buttonSetFrieFreq";
            this.buttonSetFrieFreq.Click += new System.EventHandler(this.buttonSetFrieFreq_Click);
            // 
            // tabPageHeadData
            // 
            this.tabPageHeadData.Controls.Add(this.groupBox7);
            this.tabPageHeadData.Controls.Add(this.numIndex);
            this.tabPageHeadData.Controls.Add(this.label26);
            this.tabPageHeadData.Controls.Add(this.numValue);
            this.tabPageHeadData.Controls.Add(this.label25);
            this.tabPageHeadData.Controls.Add(this.groupBox6);
            this.tabPageHeadData.Controls.Add(this.groupBox5);
            this.tabPageHeadData.Controls.Add(this.btnGetHeadData);
            this.tabPageHeadData.Controls.Add(this.numeCmd);
            this.tabPageHeadData.Controls.Add(this.label24);
            resources.ApplyResources(this.tabPageHeadData, "tabPageHeadData");
            this.tabPageHeadData.Name = "tabPageHeadData";
            this.tabPageHeadData.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.buttonReadAll);
            this.groupBox7.Controls.Add(this.button7);
            this.groupBox7.Controls.Add(this.panel6);
            this.groupBox7.Controls.Add(this.panel5);
            this.groupBox7.Controls.Add(this.buttonReadWf);
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            // 
            // buttonReadAll
            // 
            resources.ApplyResources(this.buttonReadAll, "buttonReadAll");
            this.buttonReadAll.Name = "buttonReadAll";
            this.buttonReadAll.UseVisualStyleBackColor = true;
            this.buttonReadAll.Click += new System.EventHandler(this.buttonReadAll_Click);
            // 
            // button7
            // 
            resources.ApplyResources(this.button7, "button7");
            this.button7.Name = "button7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label31);
            this.panel6.Controls.Add(this.radioButtonB);
            this.panel6.Controls.Add(this.radioButtonA);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.Name = "label31";
            // 
            // radioButtonB
            // 
            resources.ApplyResources(this.radioButtonB, "radioButtonB");
            this.radioButtonB.Name = "radioButtonB";
            this.radioButtonB.TabStop = true;
            this.radioButtonB.UseVisualStyleBackColor = true;
            // 
            // radioButtonA
            // 
            resources.ApplyResources(this.radioButtonA, "radioButtonA");
            this.radioButtonA.Checked = true;
            this.radioButtonA.Name = "radioButtonA";
            this.radioButtonA.TabStop = true;
            this.radioButtonA.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.numWaveformId);
            this.panel5.Controls.Add(this.label30);
            this.panel5.Controls.Add(this.numHbId);
            this.panel5.Controls.Add(this.label29);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // numWaveformId
            // 
            resources.ApplyResources(this.numWaveformId, "numWaveformId");
            this.numWaveformId.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numWaveformId.Name = "numWaveformId";
            this.m_ToolTip.SetToolTip(this.numWaveformId, resources.GetString("numWaveformId.ToolTip"));
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.Name = "label30";
            // 
            // numHbId
            // 
            resources.ApplyResources(this.numHbId, "numHbId");
            this.numHbId.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numHbId.Name = "numHbId";
            this.m_ToolTip.SetToolTip(this.numHbId, resources.GetString("numHbId.ToolTip"));
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // buttonReadWf
            // 
            resources.ApplyResources(this.buttonReadWf, "buttonReadWf");
            this.buttonReadWf.Name = "buttonReadWf";
            this.buttonReadWf.UseVisualStyleBackColor = true;
            this.buttonReadWf.Click += new System.EventHandler(this.buttonReadWf_Click);
            // 
            // numIndex
            // 
            this.numIndex.Hexadecimal = true;
            resources.ApplyResources(this.numIndex, "numIndex");
            this.numIndex.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numIndex.Name = "numIndex";
            this.numIndex.Value = new decimal(new int[] {
            65529,
            0,
            0,
            0});
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            // 
            // numValue
            // 
            this.numValue.Hexadecimal = true;
            resources.ApplyResources(this.numValue, "numValue");
            this.numValue.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numValue.Name = "numValue";
            this.numValue.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.textBoxRecieveData);
            this.groupBox6.Controls.Add(this.panel4);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // textBoxRecieveData
            // 
            resources.ApplyResources(this.textBoxRecieveData, "textBoxRecieveData");
            this.textBoxRecieveData.Name = "textBoxRecieveData";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.radioButtonShijinzhi);
            this.panel4.Controls.Add(this.buttonSaveAS);
            this.panel4.Controls.Add(this.label28);
            this.panel4.Controls.Add(this.radioButtonASCII);
            this.panel4.Controls.Add(this.radioButtonHex);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // radioButtonShijinzhi
            // 
            resources.ApplyResources(this.radioButtonShijinzhi, "radioButtonShijinzhi");
            this.radioButtonShijinzhi.Name = "radioButtonShijinzhi";
            this.radioButtonShijinzhi.TabStop = true;
            this.radioButtonShijinzhi.UseVisualStyleBackColor = true;
            // 
            // buttonSaveAS
            // 
            resources.ApplyResources(this.buttonSaveAS, "buttonSaveAS");
            this.buttonSaveAS.Name = "buttonSaveAS";
            this.buttonSaveAS.UseVisualStyleBackColor = true;
            this.buttonSaveAS.Click += new System.EventHandler(this.buttonSaveAS_Click);
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // radioButtonASCII
            // 
            resources.ApplyResources(this.radioButtonASCII, "radioButtonASCII");
            this.radioButtonASCII.Name = "radioButtonASCII";
            this.radioButtonASCII.TabStop = true;
            this.radioButtonASCII.UseVisualStyleBackColor = true;
            this.radioButtonASCII.CheckedChanged += new System.EventHandler(this.radioButtonHex_CheckedChanged);
            // 
            // radioButtonHex
            // 
            resources.ApplyResources(this.radioButtonHex, "radioButtonHex");
            this.radioButtonHex.Checked = true;
            this.radioButtonHex.Name = "radioButtonHex";
            this.radioButtonHex.TabStop = true;
            this.radioButtonHex.UseVisualStyleBackColor = true;
            this.radioButtonHex.CheckedChanged += new System.EventHandler(this.radioButtonHex_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBoxSendData);
            this.groupBox5.Controls.Add(this.panel3);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // textBoxSendData
            // 
            resources.ApplyResources(this.textBoxSendData, "textBoxSendData");
            this.textBoxSendData.Name = "textBoxSendData";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.numDataLength);
            this.panel3.Controls.Add(this.label27);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // numDataLength
            // 
            resources.ApplyResources(this.numDataLength, "numDataLength");
            this.numDataLength.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numDataLength.Name = "numDataLength";
            this.numDataLength.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            // 
            // btnGetHeadData
            // 
            resources.ApplyResources(this.btnGetHeadData, "btnGetHeadData");
            this.btnGetHeadData.Name = "btnGetHeadData";
            this.btnGetHeadData.UseVisualStyleBackColor = true;
            this.btnGetHeadData.Click += new System.EventHandler(this.btnGetHeadData_Click);
            // 
            // numeCmd
            // 
            this.numeCmd.Hexadecimal = true;
            resources.ApplyResources(this.numeCmd, "numeCmd");
            this.numeCmd.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numeCmd.Name = "numeCmd";
            this.numeCmd.Value = new decimal(new int[] {
            254,
            0,
            0,
            0});
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            // 
            // tabPage_Misc
            // 
            this.tabPage_Misc.Controls.Add(this.groupBoxTemperaturProfil);
            resources.ApplyResources(this.tabPage_Misc, "tabPage_Misc");
            this.tabPage_Misc.Name = "tabPage_Misc";
            this.tabPage_Misc.UseVisualStyleBackColor = true;
            // 
            // groupBoxTemperaturProfil
            // 
            this.groupBoxTemperaturProfil.Controls.Add(this.button_TemperatureProfileState);
            this.groupBoxTemperaturProfil.Controls.Add(this.label_TemperatureProfileState);
            this.groupBoxTemperaturProfil.Controls.Add(this.label_TemperatureProfileStateLabel);
            this.groupBoxTemperaturProfil.Controls.Add(this.buttonClose);
            this.groupBoxTemperaturProfil.Controls.Add(this.buttonOpen);
            resources.ApplyResources(this.groupBoxTemperaturProfil, "groupBoxTemperaturProfil");
            this.groupBoxTemperaturProfil.Name = "groupBoxTemperaturProfil";
            this.groupBoxTemperaturProfil.TabStop = false;
            // 
            // button_TemperatureProfileState
            // 
            resources.ApplyResources(this.button_TemperatureProfileState, "button_TemperatureProfileState");
            this.button_TemperatureProfileState.Name = "button_TemperatureProfileState";
            this.button_TemperatureProfileState.UseVisualStyleBackColor = true;
            this.button_TemperatureProfileState.Click += new System.EventHandler(this.button_TemperatureProfileState_Click);
            // 
            // label_TemperatureProfileState
            // 
            resources.ApplyResources(this.label_TemperatureProfileState, "label_TemperatureProfileState");
            this.label_TemperatureProfileState.Name = "label_TemperatureProfileState";
            // 
            // label_TemperatureProfileStateLabel
            // 
            resources.ApplyResources(this.label_TemperatureProfileStateLabel, "label_TemperatureProfileStateLabel");
            this.label_TemperatureProfileStateLabel.Name = "label_TemperatureProfileStateLabel";
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonOpen
            // 
            resources.ApplyResources(this.buttonOpen, "buttonOpen");
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // button3m_ButtonMove
            // 
            resources.ApplyResources(this.button3m_ButtonMove, "button3m_ButtonMove");
            this.button3m_ButtonMove.Name = "button3m_ButtonMove";
            // 
            // dividerPanel1
            // 
            this.dividerPanel1.AllowDrop = true;
            this.dividerPanel1.BorderSide = System.Windows.Forms.Border3DSide.Top;
            this.dividerPanel1.Controls.Add(this.button1);
            this.dividerPanel1.Controls.Add(this.button2);
            resources.ApplyResources(this.dividerPanel1, "dividerPanel1");
            this.dividerPanel1.Name = "dividerPanel1";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            // 
            // m_TabPageWaveMapping
            // 
            this.m_TabPageWaveMapping.Controls.Add(this.userControl81);
            resources.ApplyResources(this.m_TabPageWaveMapping, "m_TabPageWaveMapping");
            this.m_TabPageWaveMapping.Name = "m_TabPageWaveMapping";
            this.m_TabPageWaveMapping.UseVisualStyleBackColor = true;
            // 
            // userControl81
            // 
            this.userControl81.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.userControl81, "userControl81");
            this.userControl81.Name = "userControl81";
            // 
            // FactoryDebug
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.m_LabelCopyRight);
            this.Controls.Add(this.dividerPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FactoryDebug";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FactoryDebug_Load);
            this.m_GroupBoxMoveTest.ResumeLayout(false);
            this.m_GroupBoxMoveTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownY2Length)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownTimeOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownPosY)).EndInit();
            this.m_GroupBoxPosition.ResumeLayout(false);
            this.m_GroupBoxPosition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownPosY1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownDebug3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownDebug4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownDebug1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownDebug2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownSpeedSet)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPagegeneral.ResumeLayout(false);
            this.tabPagegeneral.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownLeftUV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownRightUV)).EndInit();
            this.tabPageColorDeep.ResumeLayout(false);
            this.groupBoxGraymapSet.ResumeLayout(false);
            this.groupBoxGraymapSet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeadBoard)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.tabPageRabillyDebug.ResumeLayout(false);
            this.m_GroupBoxTemperature.ResumeLayout(false);
            this.m_GroupBoxTemperature.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelayTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownPulseWidthSample)).EndInit();
            this.tabPageInkTest.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTA)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelayTime1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPulseWidth)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFireFreq)).EndInit();
            this.tabPageAccSpeedTest.ResumeLayout(false);
            this.tabPageAccSpeedTest.PerformLayout();
            this.groupBoxShakeSetting.ResumeLayout(false);
            this.groupBoxShakeSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericShockInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericIgnitionNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAccTimeY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAccTimeX)).EndInit();
            this.tabPageVPrint.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBoxGzCarton.ResumeLayout(false);
            this.groupBoxGzCarton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAutoCappingDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitMediaReadyTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabPageHeadData.ResumeLayout(false);
            this.tabPageHeadData.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWaveformId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHbId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValue)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDataLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeCmd)).EndInit();
            this.tabPage_Misc.ResumeLayout(false);
            this.groupBoxTemperaturProfil.ResumeLayout(false);
            this.groupBoxTemperaturProfil.PerformLayout();
            this.dividerPanel1.ResumeLayout(false);
            this.m_TabPageWaveMapping.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion


	    public void OnPrinterPropertyChange(SPrinterProperty sp)
	    {
            bool isNkt = SPrinterProperty.IsTILE_PRINT_ID();
	        nktDyjUserControl1.Visible = isNkt;
            nktDyjUserControl1.OnPrinterPropertyChange(sp);

            this.numericUpDownDelayTime1.Maximum = this.numericUpDownFireFreq.Maximum =
	            this.numericUpDownPulseWidth.Maximum = this.numericUpDownTA.Maximum = Decimal.MaxValue;

	        this.numericUpDownDelayTime1.Minimum = this.numericUpDownFireFreq.Minimum =
	            this.numericUpDownPulseWidth.Minimum = this.numericUpDownTA.Minimum = Decimal.MinValue;
	        numericUpDown1.Maximum = (decimal) (sp.fPulsePerInchX*200/2.54f); //按最大2m/s和光栅分辨率计算最大点火频率;
	        bool bEnableInktest = false;
            //bool bEnableVPrint = false; //2017-3-29 技术支持提需求去掉此限制
	        int funcMask = 0;
	        BYHX_SL_RetValue ret = BYHXSoftLock.GetFunctionWords(ref funcMask);
	        if (ret == BYHX_SL_RetValue.SUCSESS)
	        {
	            bEnableInktest = (funcMask & (1 << 31)) != 0;
                //bEnableVPrint = (funcMask & (1 << 30)) != 0;
	        }
//#if KONIC_INK_FULL_TEST
//            bEnableInktest = true;
//#endif
            if (!bEnableInktest)
	        {
	            if (this.tabControl1.Contains(tabPageInkTest))
	                this.tabControl1.TabPages.Remove(tabPageInkTest);
	        }
	        tableLayoutPanel1.Visible =
	            tableLayoutPanel3.Visible =
	                tableLayoutPanel4.Visible =
	                    tableLayoutPanel5.Visible = bEnableInktest;

            //if (!bEnableVPrint)
            //{
            //    if (this.tabControl1.Contains(tabPageVPrint))
            //        this.tabControl1.TabPages.Remove(tabPageVPrint);
            //}

	        m_rsPrinterPropery = sp;


	        switch (m_rsPrinterPropery.ePrinterHead)
	        {
                case PrinterHeadEnum.Konica_KM1024S_6pl:
                case PrinterHeadEnum.Konica_KM3688_6pl:
	            case PrinterHeadEnum.Konica_KM512_SH_4pl:
	            {
	                this.cboColorDeep.Items.Clear();
	                for (int i = 0; i < 3; i++)
	                    this.cboColorDeep.Items.Add(i + 1);
	                break;
	            }
	            case PrinterHeadEnum.Xaar_501_6pl:
	            case PrinterHeadEnum.Xaar_501_12pl:
	            {
	                this.cboColorDeep.Items.Clear();
	                for (int i = 0; i < 4; i++)
	                    this.cboColorDeep.Items.Add(i + 1);
	                break;
	            }
	            default:
	            {
	                this.cboColorDeep.Items.Clear();
                    bool bPowerOn = CoreInterface.GetBoardStatus() != JetStatusEnum.PowerOff;
                    HEAD_BOARD_TYPE headBoardType = (HEAD_BOARD_TYPE)CoreInterface.get_HeadBoardType(bPowerOn);
                    bool bSurrport = headBoardType == HEAD_BOARD_TYPE.SG1024_4H || headBoardType == HEAD_BOARD_TYPE.SG1024_8H_GRAY_1BIT;
	                if (bSurrport)
	                {
                        this.cboColorDeep.Items.Add(1);
                    }
	                else
	                {
                        for (int i = 0; i < 2; i++)
                            this.cboColorDeep.Items.Add(i + 1);
                    }
	                break;
	            }
	        }


	        this.cboColorDeep.SelectedIndex = 0;
	        this.cboColorDeep_SelectedIndexChanged(null, null);
#if !PULSEWIDTH_TEST
	        this.listView1.Visible = this.buttonGetInkStatus.Visible = false;
	        this.tabControl1.TabPages.Remove(this.tabPageRabillyDebug);
#else
#if DELAY_TIME
            InitRabilyTestUI(sp);
#else
            label11.Visible = false;
            m_LabelCurPulseWidth.Location = label11.Location;
            m_NumericUpDownPulseWidthSample.Location = numericUpDownDelayTime.Location;
#endif
#endif
#if false
#if !LIYUUSB
            this.listView1.Visible = this.buttonGetInkStatus.Visible = false;
            ushort Vid, Pid;
            Vid = Pid = 0;

            if (CoreInterface.GetProductID(ref Vid, ref Pid) != 0)
            {
                if (Vid != (ushort) VenderID.RABILY && Vid != (ushort) VenderID.RABILY_FLAT_UV)
                {
                    this.tabControl1.TabPages.Remove(this.tabPageRabillyDebug);
                }
                else
                {
                    InitRabilyTestUI(sp);
                }
            }
            else
#endif
            {
                this.tabControl1.TabPages.Remove(this.tabPageRabillyDebug);
                for (int i = 0; i < 16; i++)
                    m_ComboBoxVenderID.Items.Add(i.ToString());
            }
#endif

            groupBoxShakeSetting.Visible = SPrinterProperty.IsKyocera(m_rsPrinterPropery.ePrinterHead);
            groupBoxTemperaturProfil.Visible = m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Epson_5113
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.EPSON_I3200;
	    }

	    public void InitRabilyTestUI(SPrinterProperty sp)
	    {
#if true
	        uint uiHtype = 0;
	        CoreInterface.GetUIHeadType(ref uiHtype);
	        m_bKonic512 = (uiHtype & 0x01) != 0;
	        m_bXaar382 = (uiHtype & 0x02) != 0;
	        m_bSpectra = (uiHtype & 0x04) != 0;
	        m_bPolaris = (uiHtype & 0x08) != 0;
	        m_bPolaris_V5_8 = (uiHtype & 0x10) != 0;
	        m_bExcept = (uiHtype & 0x20) != 0;
            m_bSpectra_SG1024_Gray = (uiHtype & 0x100) != 0;
#else
			m_bSpectra = SPrinterProperty.IsSpectra(sp.ePrinterHead);
			m_bKonic512 = SPrinterProperty.IsKonica512 (sp.ePrinterHead);
			m_bXaar382 = (sp.ePrinterHead == PrinterHeadEnum.Xaar_Proton382_35pl||sp.ePrinterHead == PrinterHeadEnum.Xaar_Proton382_60pl);			
			m_bPolaris = SPrinterProperty.IsPolaris(sp.ePrinterHead);
			//			m_bPolaris_V5_8 = SPrinterProperty.IsPolaris(sp.ePrinterHead);
			SBoardInfo sBoardInfo = new SBoardInfo();
			if( CoreInterface.GetBoardInfo(0,ref sBoardInfo) != 0)
			{
				m_bExcept = (sBoardInfo.m_nBoardManufatureID == 0xB || sBoardInfo.m_nBoardManufatureID == 0x8b);
			}
#endif
	        m_bHorArrangement = ((sp.bSupportBit1 & 2) != 0);
	        m_b1head2color = (m_rsPrinterPropery.nOneHeadDivider == 2);
	        m_Konic512_1head2color = m_b1head2color && m_bKonic512;
	        m_bPolaris_V5_8_Emerald = m_bPolaris_V5_8 &&
	                                  (sp.ePrinterHead == PrinterHeadEnum.Spectra_Emerald_10pl
	                                   || sp.ePrinterHead == PrinterHeadEnum.Spectra_Emerald_30pl);

	        //m_ColorNum = sp.nColorNum;
	        //m_GroupNum = sp.nHeadNum/sp.nColorNum;
	        m_HeadNum = sp.nHeadNum;
	        m_TempNum = sp.nHeadNum;
	        m_HeadVoltageNum = sp.nHeadNum;
	        if (m_bKonic512)
	        {
	            m_HeadNum /= 2; //sp.nHeadNumPerColor;
	            m_TempNum = m_HeadNum;
	        }
	        else if (m_bPolaris)
	        {
	            if (!m_bExcept)
	            {
	                if (m_bPolaris_V5_8)
	                {
	                    if (m_bPolaris_V5_8_Emerald)
	                    {
	                        m_HeadNum = m_HeadNum/sp.nHeadNumPerColor; // * VOLCOUNTPERPOLARISHEAD;
	                        m_TempNum = m_HeadNum;
	                        m_HeadVoltageNum = m_HeadNum*VOLCOUNTPERPOLARISHEAD;
	                    }
	                    else
	                    {
	                        m_HeadNum = m_HeadNum/4;
	                        m_HeadVoltageNum = m_HeadNum;
	                        m_TempNum = m_HeadVoltageNum*VOLCOUNTPERPOLARISHEAD;
	                    }
	                }
	                else
	                {
	                    m_HeadNum = m_HeadNum/4;
	                    m_HeadVoltageNum = m_HeadNum*VOLCOUNTPERPOLARISHEAD;
	                    m_TempNum = m_HeadVoltageNum;
	                }
	            }
	            else
	            {
	                m_HeadNum = sp.nHeadNum/4;
	                //						if(m_b1head2color)//是否为一头两色
	                //						{
	                //							if(m_HeadNum <sp.nColorNum)
	                //								m_HeadNum = sp.nColorNum;
	                //						}
#if GONGZENG_DOUBLE
						m_HeadVoltageNum = m_HeadNum*2;
#else
	                m_HeadVoltageNum = m_HeadNum;
#endif
	            }
	        }
	        int m_HeadPulseWidthNum = m_HeadVoltageNum;
	        if (m_bSpectra_SG1024_Gray)
	        {
                const int Plus_COUNT_PER_HEAD = 5;
	            m_HeadPulseWidthNum = (int) (m_HeadNum*Plus_COUNT_PER_HEAD);
                if (!CoreInterface.IsSG1024_AS_8_HEAD())
	            {
	                if (m_b1head2color)
	                {
	                    m_HeadNum /= 2;
	                    m_TempNum /= 2;
	                    m_HeadVoltageNum /= 2;
	                    m_HeadPulseWidthNum /= 2;
	                }
	            }
	        }
	        m_StartHeadIndex = 0;
	        //m_pMap = (byte[])sp.pElectricMap.Clone();
	        //CoreInterface.GetHeadMap(m_pMap, m_pMap.Length);
	        int nmap_input = 1;
	        if (sp.ePrinterHead == PrinterHeadEnum.Spectra_S_128)
	        {
	            nmap_input = 2;
	        }
	        int imax = Math.Max(m_HeadVoltageNum, m_TempNum); //修改原因:北极星v5-8时电压数小于温度数时温度map会出错
	        imax = Math.Max(imax, m_HeadPulseWidthNum);
	        m_pMap = new byte[imax];
	        for (int i = 0; i < imax; i++)
	        {
	            m_pMap[i] = (byte) i;
	        }

            //if(!Misc.SupportWaveMapping())
            if (!SPrinterProperty.IsXAAR1201(sp.ePrinterHead))
            {
                if (tabControl1.TabPages.Contains(m_TabPageWaveMapping))
                    tabControl1.TabPages.Remove(m_TabPageWaveMapping);
            }
            else
            {
                userControl81.InitUiList();
            }

			ClearComponent();
			CreateComponent();
			LayoutComponent();
			AppendComponent();
		}
		
		public void OnPreferenceChange( UIPreference up)
		{
			if(m_CurrentUnit != up.Unit)
			{
				OnUnitChange(up.Unit);
				m_CurrentUnit = up.Unit;
				//				this.isDirty = false;
			}
            nktDyjUserControl1.OnPreferenceChange(up);
		}
		private void  OnUnitChange(UILengthUnit newUnit)
		{
		}

        SPrinterSetting _ss;
		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
            _ss = ss;
		}

		public void OnGetPrinterSetting(ref SPrinterSetting ss)
		{
		}


		private void m_ButtonMove_Click(object sender, System.EventArgs e)
		{
            int speed; 
			int dir = m_ComboBoxDirection.SelectedIndex+1;
            if (!PubFunc.ParseSeedString(m_ComboBoxSpeed.Text, out speed, (MoveDirectionEnum) dir))
                return;
            int len = Decimal.ToInt32(m_NumericUpDownLength.Value);
            //移动第二个Y轴
            if (dir == 3 || dir == 4)
                MoveY2((int)m_NumericUpDownY2Length.Value);

            // 老的移动
			const int port = 1;
			const byte PRINTER_PIPECMDSIZE = 26;
			byte [] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
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

        /// <summary>
        /// 移动第二个Y轴
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="speed"></param>
        /// <param name="len"></param>
	    private void MoveY2(int len)
	    {
            const int port = 1;
            const byte PRINTER_PIPECMDSIZE = 26;
            byte[] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
            //First Send Begin Updater
            m_pData[0] = 4 + 2;
            m_pData[1] = 0x76; //Move cmd
            m_pData[2] = (byte)(len & 0xff); //Move cmd
            m_pData[3] = (byte)((len >> 8) & 0xff); //Move cmd
            m_pData[4] = (byte)((len >> 16) & 0xff); //Move cmd
            m_pData[5] = (byte)((len >> 24) & 0xff); //Move cmd

            CoreInterface.SetPipeCmdPackage(m_pData, m_pData[0], port);
	    }

		private void m_ButtonStop_Click(object sender, System.EventArgs e)
		{
			//CoreInterface.SendJetCommand((int)JetCmdEnum.StopMove,0);
			const int port = 1;
			const byte PRINTER_PIPECMDSIZE = 26;
			byte [] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
			//First Send Begin Updater
			m_pData[0] = 2;
			m_pData[1] = 0x3a; //Move cmd

			CoreInterface.SetPipeCmdPackage(m_pData,m_pData[0],port);

		}
		private void GetXAndYPosFromBoard()
		{
			MotionDebugInfo info;
			if( GetXAndYPos(out info) )
			{
				UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownPosX,info.xPos);
                if (!bDoubleYAxis)
                	UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownPosY,info.yPos);
                
				UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownDebug1,info.nDebugInt1);
				UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownDebug2,info.nDebugInt2);
				UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownDebug3,info.nDebugInt3);
				UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownDebug4,info.nDebugInt4);

			}
		}
		private void m_ButtonPosition_Click(object sender, System.EventArgs e)
		{
			const int port = 1;
			const byte PRINTER_PIPECMDSIZE = 26;
			byte [] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
			//First Send Begin Updater
			m_pData[0] = 2;
			m_pData[1] = 0x2c; //Move cmd

			CoreInterface.SetPipeCmdPackage(m_pData,m_pData[0],port);

			Thread.Sleep(1000);
			GetXAndYPosFromBoard();

		    if (bDoubleYAxis)
		    {
		        //Y和Y1的位置改为从ARM获取
		        Thread.Sleep(1000);
		        MotionDebugInfoFromARM motionDebugInfo = new MotionDebugInfoFromARM();
		        if (GetYHomePosition(ref motionDebugInfo))
		        {
		            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownPosY, motionDebugInfo.yPosition);
		            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownPosY1, motionDebugInfo.y1Position);
		        }
		    }
		}

        /// <summary>
        /// 从ARM获取Y和Y1的位置
        /// </summary>
        /// <param name="position"></param>
	    private bool GetYHomePosition(ref MotionDebugInfoFromARM position)
	    {
            byte[] val = new byte[64];
			uint bufsize = (uint)val.Length;
			int ret = CoreInterface.GetEpsonEP0Cmd(0x43, val, ref bufsize, 0, 0);
	        if (ret == 0)
	        {
	            MessageBox.Show("Get Y home position fialed!");
	            return false;
	        }
	        else
	        {
                int xposIndex = 2;
                position.xPosition = (val[xposIndex + 3] << 24) + (val[xposIndex + 2] << 16) + (val[xposIndex + 1] << 8) + val[xposIndex + 0];
                xposIndex += 4;
                position.yPosition = (val[xposIndex + 3] << 24) + (val[xposIndex + 2] << 16) + (val[xposIndex + 1] << 8) + val[xposIndex + 0];
                xposIndex += 4;
                position.zPosition = (val[xposIndex + 3] << 24) + (val[xposIndex + 2] << 16) + (val[xposIndex + 1] << 8) + val[xposIndex + 0];
                xposIndex += 4;
                position.fPosition = (val[xposIndex + 3] << 24) + (val[xposIndex + 2] << 16) + (val[xposIndex + 1] << 8) + val[xposIndex + 0];
                xposIndex += 4;
                position.y1Position = (val[xposIndex + 3] << 24) + (val[xposIndex + 2] << 16) + (val[xposIndex + 1] << 8) + val[xposIndex + 0];
                return true;
	        }
	    }

        /// <summary>
        /// 从DSP获取X和Y的位置
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
	    private bool GetXAndYPos(out  MotionDebugInfo info)
		{
			byte [] buf = new byte[64];
			info = new MotionDebugInfo();
			if( CoreInterface.GetDebugInfo(buf,64) != 0)
			{
				int xposIndex = 4;
				info.xPos = (buf[xposIndex+3]<<24) + (buf[xposIndex+2]<<16) + (buf[xposIndex+1]<<8) + buf[xposIndex+0];
				xposIndex+=4;
				info.yPos = (buf[xposIndex+3]<<24) + (buf[xposIndex+2]<<16) + (buf[xposIndex+1]<<8) + buf[xposIndex+0];
				xposIndex+=4;
				info.nDebugInt1 = (buf[xposIndex+3]<<24) + (buf[xposIndex+2]<<16) + (buf[xposIndex+1]<<8) + buf[xposIndex+0];
				xposIndex+=4;
				info.nDebugInt2 = (buf[xposIndex+3]<<24) + (buf[xposIndex+2]<<16) + (buf[xposIndex+1]<<8) + buf[xposIndex+0];
				xposIndex+=4;
				info.nDebugInt3 = (buf[xposIndex+3]<<24) + (buf[xposIndex+2]<<16) + (buf[xposIndex+1]<<8) + buf[xposIndex+0];
				xposIndex+=4;
				info.nDebugInt4 = (buf[xposIndex+3]<<24) + (buf[xposIndex+2]<<16) + (buf[xposIndex+1]<<8) + buf[xposIndex+0];
				return true;
			}
			return false;
		}
		private void m_ButtonSetTimeout_Click(object sender, System.EventArgs e)
		{
#if !LIYUUSB
			CoreInterface.SendJetCommand((int)JetCmdEnum.SetMotionTimeOut,Decimal.ToInt32(m_NumericUpDownTimeOut.Value));
#endif	
		}

        /*
        //设置UV灯参数，全是脉冲单位
        字节1-4：正向打印中，后灯距离喷头位置
        字节5-8：反向打印中，前灯距离喷头位置×
        字节9-12：两灯间距
        字节12-16：uv灯开灯模式：后灯正向打印有效bit1，反向打印有效bit2。
        前灯正向打印有效bit3，反向打印有效bit4。
        字节16-20：提前开灯距离，脉冲单位
        */
		private void m_ButtonWriteUV_Click(object sender, System.EventArgs e)
		{
			int LeftUV = Decimal.ToInt32(m_NumericUpDownLeftUV.Value);
			int RightUV = Decimal.ToInt32(m_NumericUpDownRightUV.Value);

			const int port = 1;
			const byte PRINTER_PIPECMDSIZE = 26;
			byte [] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
			//First Send Begin Updater
			m_pData[0] = 8 + 2;
			m_pData[1] = 0x43; 			//SciCmd_CMD_SetUvParam               = 0x43

			m_pData[2] = (byte)(LeftUV&0xff); //Move cmd
			m_pData[3] = (byte)((LeftUV>>8)&0xff); //Move cmd
			m_pData[4] = (byte)((LeftUV>>16)&0xff); //Move cmd
			m_pData[5] = (byte)((LeftUV>>24)&0xff); //Move cmd

			
			m_pData[6] = (byte)(RightUV&0xff); //Move cmd
			m_pData[7] = (byte)((RightUV>>8)&0xff); //Move cmd
			m_pData[8] = (byte)((RightUV>>16)&0xff); //Move cmd
			m_pData[9] = (byte)((RightUV>>24)&0xff); //Move cmd

			CoreInterface.SetPipeCmdPackage(m_pData,m_pData[0],port);
		}

		private void m_ButtonReadUV_Click(object sender, System.EventArgs e)
		{
		
		}
        /// <summary>
        /// 设置打印时的点火频率，字节1-4：打印时的点火频率值，hz单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void m_ButtonWriteSpeed_Click(object sender, System.EventArgs e)
		{
			int Speed = Decimal.ToInt32(m_NumericUpDownSpeedSet.Value);

			const int port = 1;
			const byte PRINTER_PIPECMDSIZE = 26;
			byte [] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
			//First Send Begin Updater
			m_pData[0] = 4 + 2;
			m_pData[1] = 0x44; 			

			m_pData[2] = (byte)(Speed&0xff); //Move cmd
			m_pData[3] = (byte)((Speed>>8)&0xff); //Move cmd
			m_pData[4] = (byte)((Speed>>16)&0xff); //Move cmd
			m_pData[5] = (byte)((Speed>>24)&0xff); //Move cmd

			
			CoreInterface.SetPipeCmdPackage(m_pData,m_pData[0],port);
	
		}

		private void buttonGetInkStatus_Click(object sender, System.EventArgs e)
		{
			byte[] val = new byte[64];
			CoreInterface.GetDebugInk(val,val.Length);
			int pumbtime = (val[3]<<8) +val[4];
			string[] subitem = new string[]{val[0].ToString("X2"),val[1].ToString("X2"),val[2].ToString("X2"),pumbtime.ToString("X4")};
			ListViewItem item =new ListViewItem(subitem);
			this.listView1.Items.Add(item);
		}

		private void ClearComponent()
		{
			if (m_LabelHorHeadIndex == null || m_LabelHorHeadIndex.Length == 0)
				return;

			foreach (Control c in this.m_LabelHorHeadIndex)
			{
				m_GroupBoxTemperature.Controls.Remove(c);
			}
			foreach(Control c in this.m_NumericUpDownDelayTimes)
			{
				m_GroupBoxTemperature.Controls.Remove(c);
			}
		}

		private void CreateComponent()
		{
			this.m_LabelHorHeadIndex = new Label[m_HeadNum];
			this.m_NumericUpDownDelayTimes = new NumericUpDown[m_HeadVoltageNum];
			 

			for(int i = 0; i < m_HeadNum; i ++)
			{
				this.m_LabelHorHeadIndex[i] = new Label();
			}
			for(int i = 0; i < m_HeadVoltageNum; i ++)
			{
				this.m_NumericUpDownDelayTimes[i] = new NumericUpDown();
			}
			this.SuspendLayout();
		}

		private void AppendComponent()
		{
			for (int i=0; i<m_HeadNum; i++)
			{
				m_GroupBoxTemperature.Controls.Add(this.m_LabelHorHeadIndex[i]); 
			}
			for(int i = 0; i < m_HeadVoltageNum; i ++)
			{
				m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownDelayTimes[i]); 
			}
			m_GroupBoxTemperature.ResumeLayout(false);
			
			this.ResumeLayout(false);
		}
		private void LayoutRow_HeadIndex(Point startP,Point spaceP)
		{
			int start_x = startP.X;
			int start_y = startP.Y;
			int space_y = spaceP.Y;
			int space_x = spaceP.X;
			int	width_con = this.m_NumericUpDownPulseWidthSample.Width;
			#region Layout lables
			int j = 0;
			for (int i=0; i<m_HeadNum; i++)
			{
				Label label = this.m_LabelHorHeadIndex[i];
				ControlClone.LabelClone(label,this.m_LabelHorSample);
				label.Location = new Point(start_x + space_x * i,this.m_LabelHorSample.Location.Y);//start_y );
				label.Width = width_con;

				label.Text = (i).ToString();
				//					+"  ("+m_rsPrinterPropery.Get_ColorIndex(i+m_StartHeadIndex) +")";
				label.Visible = true;
			}
			#endregion
		}

		private void LayoutRows_VolAndPulseW(Point startP,Point spaceP,int new_space_y,int width_con)
		{
			int start_x = startP.X;
			int start_y = startP.Y;
			int space_y = spaceP.Y;
			int space_x = spaceP.X;
			#region layout PulseWidth

			for (int i=0; i<m_HeadVoltageNum; i++)
			{
				int curX = start_x+ space_x * i;
				int curY = start_y + space_y;

				if(m_bKonic512
#if GONGZENG_DOUBLE
					|| m_bExcept
#endif
					)
				{
					curX = start_x+ space_x * (i/2);
					if( (i &1) != 0)
					{
						curY += space_y;
					}
				}
				else if(m_bPolaris && !m_bExcept)
				{
					if( i>=m_HeadVoltageNum/2 && (!m_bPolaris_V5_8 || m_bPolaris_V5_8_Emerald))
					{
						curX = start_x+ space_x * (i-m_HeadVoltageNum/2);
						curY += space_y;
					}
					else
					{
						curX = start_x+ space_x * i;
					}
				}

				NumericUpDown textBox = this.m_NumericUpDownDelayTimes[i];

				//ControlClone.NumericUpDownClone(textBox,this.m_NumericUpDownPulseWidthSample);
				ControlClone.NumericUpDownClone(textBox,this.numericUpDownDelayTime);

				textBox.Location = new Point(curX,curY);
				textBox.TabIndex = m_TempNum *3+m_HeadVoltageNum*3 +i;
				//textBox.TabStop = false;
				InitNumericUpDownBySample(textBox,width_con);
			}
			#endregion
		}
		private void LayoutComponent()
		{
			const int control_margin = 8;
			m_GroupBoxTemperature.SuspendLayout();
			this.SuspendLayout();

			///True Layout
			///
			int start_x,end_x,space_x,width_con;
			int  start_y,space_y;
			start_x = this.m_NumericUpDownPulseWidthSample.Left;
			start_y = this.m_LabelHorSample.Top;
			space_y = this.m_NumericUpDownPulseWidthSample.Height + control_margin;
			end_x = this.m_GroupBoxTemperature.Width;
			width_con = this.m_NumericUpDownPulseWidthSample.Width;
			LayoutHelper.CalculateHorNum(m_HeadNum,start_x,end_x,ref width_con,out space_x);

			// recalculat groupbox size
			int new_space_y = space_y;

			// Layout lables
			Point startP = new Point(start_x,start_y);
			Point spaceP = new Point(space_x,space_y);
			LayoutRow_HeadIndex(startP,spaceP);//( start_x, start_y,  space_y, space_x);

			//layout Voltage and PulseWidth
			LayoutRows_VolAndPulseW(startP,spaceP,new_space_y,width_con);
		}

		private void InitNumericUpDownBySample(NumericUpDown textBox,int width_con)
		{
			textBox.Width = width_con;
			textBox.Text = "0";
			textBox.Visible = true;
			textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
			textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_NumericUpDownTempSetSample_KeyPress);
			textBox.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
			textBox.Enter += new System.EventHandler(this.m_NumericUpDownTempSetSample_Enter);
			textBox.ValueChanged += new System.EventHandler(this.m_NumericUpDownVolBaseSample_ValueChanged);
		}

		
		private void m_CheckBoxControl_Leave(object sender, System.EventArgs e)
		{
#if true
			NumericUpDown textBox = (NumericUpDown)sender;
			float val = float.Parse(textBox.Text);
			textBox.BeginInit();
			textBox.Value = new Decimal(val);
			textBox.EndInit();
#else
			bool isValidNumber = true;
			try
			{
				float val = float.Parse(textBox.Text);
				textBox.Value = new Decimal(val);
			}
			catch(Exception )
			{
				//Console.WriteLine(ex.Message);
				isValidNumber = false;
			}

			if(!isValidNumber)
			{
				SystemCall.Beep(200,50);
				textBox.Focus();
				textBox.Select(0,textBox.Text.Length);
			}
#endif
		}
		private void m_NumericUpDownTempSetSample_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(!Char.IsLetter(e.KeyChar) && !(Char.IsPunctuation(e.KeyChar) && e.KeyChar != '.'&&e.KeyChar != '-' ))
			{
				e.Handled = false;
			}
			else
			{
				e.Handled = true;
			}
		}
		private void m_TextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Enter)
			{
				m_CheckBoxControl_Leave(sender,e);
				(sender as NumericUpDown).Select(0,0);
			}
		}
		private void m_NumericUpDownVolBaseSample_ValueChanged(object sender, System.EventArgs e)
		{
			NumericUpDown cur = (NumericUpDown)sender;
			cur.Text = cur.Value.ToString();
		}

		private void m_NumericUpDownTempSetSample_Enter(object sender, System.EventArgs e)
		{
			NumericUpDown cur = (NumericUpDown)sender;
			cur.Select(0, cur.ToString().Length);
		}


		public void OnRealTimeChange()
		{
			SRealTimeCurrentInfo info = new SRealTimeCurrentInfo();
			uint Rmask = CalcRWMask(true);
			int plusWidth = 0;
			if(CoreInterface.SetGetKonicPulseWidth(ref plusWidth,0)==0)
			{
				MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.GetRealTimeInfoFail),ResString.GetProductName());
			}
			else
			{
				UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownPulseWidthSample,plusWidth/50f);
			}
#if DELAY_TIME
			ushort[] delayTimes = new ushort[CoreConst.MAX_HEAD_NUM];

			if(CoreInterface.SetGetAdjustClock(delayTimes,m_HeadNum,0)==0)
			{
				MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.GetRealTimeInfoFail),ResString.GetProductName());
			}
			else
			{
				float[] delayTs = new float[CoreConst.MAX_HEAD_NUM];
				for(int i = 0 ; i < delayTs.Length; i++)
				{
					delayTs[i] =delayTimes[i]/50f;
				}
//				ReMapBuffer(ref delayTs,m_rsPrinterPropery.nColorNum);
				for (int i=0;i<m_HeadVoltageNum;i++)
				{
					int nMap = m_pMap[i];
					UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownDelayTimes[i],delayTs[nMap]);
				}	
			}
#endif
        }

		public void ApplyToBoard()
		{
#if DELAY_TIME
			float[] delayTimes = new float[CoreConst.MAX_HEAD_NUM];
			for (int i=0;i<m_HeadVoltageNum;i++)
			{
				int nMap = m_pMap[i];
				delayTimes[nMap] = Decimal.ToUInt16(m_NumericUpDownDelayTimes[i].Value);
			}
			if((m_Konic512_1head2color				
				||(m_bExcept && m_b1head2color)
				) && !m_bHorArrangement)
			{
//				ReMapBuffer(ref plusWidths,m_rsPrinterPropery.nColorNum);
//				ReMapBuffer(ref delayTimes,m_rsPrinterPropery.nColorNum);
			}
 			ushort[] delayTs = new ushort[CoreConst.MAX_HEAD_NUM];
			for(int i = 0 ; i < delayTs.Length; i++)
			{
				delayTs[i] =(ushort)(delayTimes[i]*50);
			}

            if (CoreInterface.SetGetAdjustClock(delayTs, m_HeadVoltageNum, 1) != 0)
            {
            }
            else
                MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.SaveRealTimeFail));
        
#endif            
            
            int plusWs = (int)(m_NumericUpDownPulseWidthSample.Value * 50);
            if (CoreInterface.SetGetKonicPulseWidth(ref plusWs, 1) != 0)
			{
			}
			else
				MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.SaveRealTimeFail));
		}

		private void m_ButtonRefresh_Click(object sender, System.EventArgs e)
		{
			this.OnRealTimeChange();
		}

		private void m_ButtonToBoard_Click(object sender, System.EventArgs e)
		{
			this.ApplyToBoard();
		}
		private uint CalcRWMask(bool isRead)
		{
			uint ret = 0;
			ret |= 1<<(int)EnumVoltageTemp.PulseWidth;
			return ret;
		}

		private void ReMapVolAndTempToUI(SRealTimeCurrentInfo info)
		{
			ReMapBuffer(ref info.cPulseWidth,m_rsPrinterPropery.nColorNum);
		}

		private void ReMapBuffer(ref float[] buffer,int colornum)
		{
			for(int i = 0 ;i<buffer.Length;)
			{
				if(i/colornum%2==1)
				{
					for(int j =0 ;j< m_rsPrinterPropery.nColorNum;j+=2)
					{
						if(i+j+1>=buffer.Length)
							break;
						float t =buffer[i+j];
						buffer[i+j] = buffer[i+j+1];
						buffer[i+j+1] =t;
					}
					i+=colornum;
				}
				else
					i++;
			}
		}

		private void btnSendSerialCmd_Click(object sender, System.EventArgs e)
		{
			try
			{
				int len = this.txtSerialCmd.Text.Length/2;
				byte[] cmds= new byte[len];
				for(int i = 0; i< len; i++)
				{
					byte cmd = byte.Parse(this.txtSerialCmd.Text.Substring(i*2,2),NumberStyles.HexNumber);
					cmds[i] = cmd;
				}
				if(CoreInterface.Set382ComCmd(cmds,len,this.m_ComboBoxVenderID.SelectedIndex,0)==0)
					MessageBox.Show("失败!");
				else
					MessageBox.Show("成功!");
			}
			catch(Exception ex)
			{
				MessageBox.Show(this,ex.Message);
			}
		}

		private void txtSerialCmd_TextChanged(object sender, System.EventArgs e)
		{
			this.txtSerialCmdLen.Text = (this.txtSerialCmd.Text.Length/2).ToString();
		}

	    private bool CheckGetSetGrayMapSurpport()
	    {
            bool bPowerOn = CoreInterface.GetBoardStatus() != JetStatusEnum.PowerOff;
            bool bSurrport = (m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Konica_KM1024S_6pl
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Konica_KM3688_6pl
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Konica_KM1024M_14pl
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Konica_KM1024L_42pl
                || SPrinterProperty.IsKonica512i(m_rsPrinterPropery.ePrinterHead)
                || SPrinterProperty.IsKonica1024i(m_rsPrinterPropery.ePrinterHead)
                || SPrinterProperty.IsSG1024(m_rsPrinterPropery.ePrinterHead)
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Xaar_501_6pl
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Xaar_501_12pl
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Konica_KM512_SH_4pl
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Konica_KM512M_14pl
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Konica_KM512L_42pl
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Konica_KM1800i_3P5pl
                || SPrinterProperty.IsKyocera(m_rsPrinterPropery.ePrinterHead)
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Konica_M600SH_2C
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Fujifilm_GMA9905300_5pl
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Fujifilm_GMA3305300_5pl
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Konica_KM1024A_6_26pl
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Epson_5113
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.EPSON_I3200
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Ricoh_Gen6
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Epson_S2840
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Epson_S2840_WaterInk
                || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.EPSON_S1600_RC_UV
                );
            if (!bSurrport)
                MessageBox.Show(ResString.GetResString("CurSystemNoSurpportThisFeature"));
	        return bSurrport;
	    }

	    private ComboBox[] comboboxs;
		private void cboColorDeep_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            if (!bload) return;
            //if (!CheckGetSetGrayMapSurpport())
            //    return;
            int pow = this.cboColorDeep.SelectedIndex + 1;
			int count = (int)Math.Pow(2,pow)-1;
			comboboxs = new ComboBox[count];
			this.Panel1.SuspendLayout();
			this.Panel1.Controls.Clear();
			for(int i = 0; i < count; i++)
			{
				ComboBox cmb = new ComboBox();
				cmb.Size = this.cboColorDeep.Size;
				cmb.Location = new Point(8,cboColorDeep.Location.Y + (8 + cmb.Height)*i);
                if (SPrinterProperty.IsKonica512i(m_rsPrinterPropery.ePrinterHead)
                    || SPrinterProperty.IsKonica1024i(m_rsPrinterPropery.ePrinterHead)
                    || SPrinterProperty.IsSG1024(m_rsPrinterPropery.ePrinterHead))
                {
                    for (int j = 0; j < 4; j++)
                        cmb.Items.Add(j);
                }
                else
                {
                    switch (m_rsPrinterPropery.ePrinterHead)
                    {
                        case PrinterHeadEnum.Xaar_501_12pl:
                        case PrinterHeadEnum.Xaar_501_6pl:
                            {
                                for (int j = 0; j < 16; j++)
                                    cmb.Items.Add(j);
                                break;
                            }
                        case PrinterHeadEnum.Konica_KM1024S_6pl:
                        case PrinterHeadEnum.Konica_KM3688_6pl:
                        case PrinterHeadEnum.Konica_KM512_SH_4pl:
                        case PrinterHeadEnum.Kyocera_KJ4B_0300_5pl_1h2c:
                        case PrinterHeadEnum.Kyocera_KJ4B_1200_1p5pl:
                        case PrinterHeadEnum.Kyocera_KJ4B_QA06_5pl:
                        case PrinterHeadEnum.Kyocera_KJ4B_YH06_5pl:
                        case PrinterHeadEnum.Kyocera_KJ4A_AA06_3pl:
                        case PrinterHeadEnum.Kyocera_KJ4A_RH06:
                        case PrinterHeadEnum.Kyocera_KJ4A_TA06_6pl:
                        case PrinterHeadEnum.Kyocera_KJ4A_0300_5pl_1h2c:
                        case PrinterHeadEnum.Kyocera_KJ4A_1200_1p5pl:
                        case PrinterHeadEnum.Konica_KM1800i_3P5pl:
                            {
                                for (int j = 0; j < 8; j++)
                                    cmb.Items.Add(j);
                                break;
                            }
                        case PrinterHeadEnum.Konica_KM1024M_14pl:
                        case PrinterHeadEnum.Konica_KM1024A_6_26pl:
                            {
                                for (int j = 0; j < 4; j++)
                                    cmb.Items.Add(j);
                                break;
                            }
                        default:
                            {
                                for (int j = 0; j < 4; j++)
                                    cmb.Items.Add(j);
                                break;
                            }
                    }
			    }
			    comboboxs[i] = cmb;
                if(cmb.Items.Count > i+1)
                    comboboxs[i].SelectedIndex = i + 1;
                this.Panel1.Controls.Add(cmb, i / Panel1.RowCount,i%Panel1.RowCount);
			}
			this.Panel1.ResumeLayout(true);
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
            if (!CheckGetSetGrayMapSurpport())
                return;
            //			req = 0x81
			//index = 0x4000
			//value = 0x0000.
			//data 序列
			//byte1 : color deep: 1/2
			//byte2..8:map对应的level值.[主板只设置map0/1/2/3给头版fpga]
            double oldColorDeep = PubFunc.GetColorDeep();
            double newColorDeep=this.cboColorDeep.SelectedIndex+1;
            double ratio = 1;
            if (oldColorDeep >= 1)//获取到灰度值才进行计算
            {
                ratio=newColorDeep / oldColorDeep;
            }
		    if (comboboxs == null || comboboxs.Length == 0)
		    {
                string info = "请确认输入了灰度映射值?";
		        MessageBox.Show(info, "", MessageBoxButtons.OK);
		        return;
		    }
            if (this.cboColorDeep.SelectedIndex == 0 && comboboxs.Length>0&& comboboxs[0].SelectedIndex == 0)
            {
                string info = "色深为1时映射设置为0会导致打印不出图像,确定仍然这样设置吗?";
                if (MessageBox.Show(info,"",MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }
			byte[] val = new byte[this.comboboxs.Length +1];
			int i = 0;
			val[i++] = (byte)(this.cboColorDeep.SelectedIndex +1);
			for(int j = 0 ; j < this.comboboxs.Length; j++)
				val[i++] = (byte) this.comboboxs[j].SelectedIndex;
			uint bufsize = (uint)val.Length;
			int ret = CoreInterface.SetEpsonEP0Cmd(0x81, val, ref bufsize, 0, 0x4000);
			if (ret == 0)
			{
				MessageBox.Show("Set 1024 gray map fialed!");
			}
            LogWriter.WriteLog(new string[] { "设置灰度：SetEpsonEP0Cmd Called" }, true);
			CoreInterface.SetOutputColorDeep((byte)(this.cboColorDeep.SelectedIndex +1));
            if(CoreInterface.IsS_system())
            {
                USER_SET_INFORMATION userInfo = new USER_SET_INFORMATION(true);
                if (CoreInterface.GetUserSetInfo(ref userInfo) != 0)
                {
                    userInfo.HeadBoardDataByteWidth = (byte)((userInfo.HeadBoardDataByteWidth+1) * ratio-1);
                    LogWriter.WriteLog(new string[] { string.Format("设置数据宽度值为:{0}",userInfo.HeadBoardDataByteWidth) }, true);
                    CoreInterface.SetUserSetInfo(ref userInfo);
                }
            }
          
		}

		private void buttonRead_Click(object sender, System.EventArgs e)
		{
            //if (!CheckGetSetGrayMapSurpport())
            //    return;
            byte[] val = new byte[64];
			uint bufsize = (uint)val.Length;
			int ret = CoreInterface.GetEpsonEP0Cmd(0x81, val, ref bufsize, 0, 0x4000);
			if (ret == 0)
			{
				MessageBox.Show("Get gray map fialed!");
			}
			else
			{
				int mode = val[0] -1;
				if(mode >= 0 && mode < this.cboColorDeep.Items.Count)
				{
					this.cboColorDeep.SelectedIndex = mode;
					this.cboColorDeep_SelectedIndexChanged(null,null);
					for(int i = 0; i < this.comboboxs.Length; i++)
					{
						int index = val[i+1];
						if(index >= 0 && this.comboboxs[i].Items.Count > index)
							this.comboboxs[i].SelectedIndex = index;
						else
							this.comboboxs[i].SelectedIndex = -1;							
					}
				}
				else
				{
					MessageBox.Show("(mode >= 0 && mode < this.cboColorDeep.Items.Count)==false");
				}
			}
		}

		private void btnAutoTestclb_Click(object sender, System.EventArgs e)
		{
			const int port = 1;
			const byte PRINTER_PIPECMDSIZE = 26;
			byte [] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
			//First Send Begin Updater
			m_pData[0] = 2;
			m_pData[1] = 0x59; //自动测算齿轮比

			CoreInterface.SetPipeCmdPackage(m_pData,m_pData[0],port);
		}

		private void buttonGet_Click(object sender, System.EventArgs e)
		{
            T125df t125Df = new T125df();
            byte[] buf = SerializationUnit.StructToBytes(t125Df);
            uint bufsize = (uint)buf.Length;
            //if (CoreInterface.SetGetKonicInkTest(ref plusw, ref delayTime, ref fireFreq, ref ta, 0) != 0)
            if (CoreInterface.SetGetKonicInkTest_struct(buf, ref bufsize, 0) != 0)
            {
                t125Df = (T125df) SerializationUnit.BytesToStruct(buf, typeof (T125df));
                this.numericUpDownPulseWidth.Value = new decimal(t125Df.T1/ 50f);
                this.numericUpDownFireFreq.Value = t125Df.Ffire;
                this.numericUpDownDelayTime1.Value = new decimal(t125Df .Td/ 50f);
				this.numericUpDownTA.Value =  new decimal((t125Df.T5+t125Df.Td)/50f);
                this.numericUpDown2.Value = new decimal(t125Df.T2 / 50f);
            }
			else
				MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.SaveRealTimeFail));

		}

	    private void buttonSet_Click(object sender, System.EventArgs e)
	    {
	        ushort plusw = (ushort) (this.numericUpDownPulseWidth.Value*50);
	        uint bufsize = 0;
#if true//KONIC_INK_FULL_TEST
	        ushort fireFreq = (ushort) this.numericUpDownFireFreq.Value;
	        ushort delayTime = (ushort) (this.numericUpDownDelayTime1.Value*50);
	        ushort ta = (ushort) (numericUpDownTA.Value*50);
#else
            byte[] val = new byte[64];
            bufsize = (uint)val.Length;
            int ret = CoreInterface.GetEpsonEP0Cmd(0x81, val, ref bufsize, 0, 0x4000);
            if (ret == 0)
            {
                MessageBox.Show("Get gray map fialed!");
                return;
            }
            int colordeep = val[0];
		    const int _NUM = 5;
            ushort fireFreq = (ushort)(950000 * 50 / ((plusw * 3 * _NUM) * colordeep)); ;
            ushort delayTime = 0;
            ushort ta = (ushort)(plusw * 5);
#endif
	        T125df t125Df = new T125df();
	        t125Df.Ffire = fireFreq;
	        t125Df.T1 = plusw;
            t125Df.T2 = (ushort)(numericUpDown2.Value * 50); //
	        t125Df.Td = delayTime;
            t125Df.T5 = (ushort)((ta - delayTime));

	        byte[] buf = SerializationUnit.StructToBytes(t125Df);
	        bufsize = (uint) buf.Length;
	        //if (CoreInterface.SetGetKonicInkTest(ref plusw, ref delayTime, ref fireFreq, ref ta, 1) != 0)
	        if (CoreInterface.SetGetKonicInkTest_struct(buf, ref bufsize, 1) != 0)
	        {
	        }
	        else
	            MessageBox.Show(ResString.GetEnumDisplayName(typeof (UIError), UIError.SaveRealTimeFail));

	    }

	    private void buttonAccTimeSet_Click(object sender, System.EventArgs e)
	    {
	        byte[] buf = new byte[12];
	        uint bufsize = (uint) buf.Length;
	        buf[0] = (byte) 'A';
	        buf[1] = (byte) 'C';
	        buf[2] = (byte) 'C';
	        buf[3] = (byte) 'T';
	        Buffer.BlockCopy(BitConverter.GetBytes((int) this.numAccTimeX.Value), 0, buf, 4, 4);
	        Buffer.BlockCopy(BitConverter.GetBytes((int) this.numAccTimeY.Value), 0, buf, 8, 4);
	        int ret = CoreInterface.SetEpsonEP0Cmd(0x82, buf, ref bufsize, 0, 3);
	        if (ret == 0) //如果主板是老版本,不支持新接口,则尝试用老接口设置
	        {
	            const int port = 1;
	            const byte PRINTER_PIPECMDSIZE = 26;
	            byte[] m_pData = new byte[PRINTER_PIPECMDSIZE + 6];
	            //First Send Begin Updater
	            m_pData[0] = 6;
	            m_pData[1] = 0x5D; //

	            Buffer.BlockCopy(BitConverter.GetBytes((short) this.numAccTimeX.Value), 0, m_pData, 2, 2);
	            Buffer.BlockCopy(BitConverter.GetBytes((short) this.numAccTimeY.Value), 0, m_pData, 4, 2);

	            CoreInterface.SetPipeCmdPackage(m_pData, m_pData[0], port);
	        }
	    }

	    private void FactoryDebug_Load(object sender, EventArgs e)
        {
            bload = true;
            //DoubleYAxis support check
            bDoubleYAxis = PubFunc.IsDoubleYAxis();
            if (bDoubleYAxis && SPrinterProperty.IsDocanPrintMode())
            {
                label2.Text = "Left Y";
                label21.Text = "Right Y";
            }
            this.label21.Visible = this.m_NumericUpDownPosY1.Visible = bDoubleYAxis;
            
            string path = Path.Combine(Application.StartupPath, FILENAME);
            if (File.Exists(path))
            {
                using (TextReader reader = new StreamReader(path))
                {
                    string line = reader.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        string[] splits = line.Split(new char[] {'='});
                        if (splits[0].ToLower() == FIRE_REQ)
                        {
                            int value;
                            if (int.TryParse(splits[0], out value))
                                m_NumericUpDownSpeedSet.Value=numericUpDown1.Value = value;
                        }

                        if (splits[0].ToLower() == XAAR382_MODE)
                        {
                            int value;
                            if (int.TryParse(splits[0], out value))
                                comboBoxXaar382Mode.SelectedIndex = value;
                        }
                        line = reader.ReadLine();
                    }
                    reader.Close();
                }
            }

	        if (SPrinterProperty.IsKyocera(m_rsPrinterPropery.ePrinterHead)&&CoreInterface.GetBoardStatus()!=JetStatusEnum.PowerOff)
	        {
	            buttonShakeGet_Click(null,null);
            }
            if ((m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Epson_5113 || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.EPSON_I3200 || m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.EPSON_S1600_RC_UV)
                && CoreInterface.GetBoardStatus() != JetStatusEnum.PowerOff)
            {
                button_TemperatureProfileState_Click(null, null);
            }
        }

        private const string FILENAME = "debug.ini";
        private const string FIRE_REQ = "firereq";
	    private const string XAAR382_MODE = "xaar382mode";
        private void SaveDebugParaToFile(string key,string strValue )
        {
            string path = Path.Combine(Application.StartupPath, FILENAME);
            Stream stream = new FileStream(path,FileMode.OpenOrCreate,FileAccess.ReadWrite,FileShare.Read);
            Dictionary<string,string> contents = new Dictionary<string, string>();
            bool bFinded = false;
            using (TextReader reader = new StreamReader(stream))
            {
                string line = reader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    string[] splits = line.Split(new char[] { '=' });
                    if (splits[0] == key)
                    {
                        bFinded = true;
                        contents.Add(splits[0], strValue);
                    }
                    else
                        contents.Add(splits[0], splits[1]);
                    line = reader.ReadLine();
                }
                reader.Close();
            }
            if (!bFinded)
            {
                contents.Add(key, strValue);
            }

            stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
            using (TextWriter reader = new StreamWriter(stream))
            {
                foreach (KeyValuePair<string, string> pair in contents)
                {
                    reader.WriteLine(string.Format("{0}={1}",pair.Key,pair.Value));
                }
                reader.Flush();
            }
            stream.Close();
        }

        private void buttonStartPrint_Click(object sender, EventArgs e)
        {
            CoreInterface.SendJetCommand((int)JetCmdEnum.StartVPrint, 0);
        }

        private void buttonStopPrint_Click(object sender, EventArgs e)
        {
            CoreInterface.Printer_Abort();
        }

        /// <summary>
        /// 设置点火频率,for xaar和北极星
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetFrieFreq_Click(object sender, EventArgs e)
        {
            const int port = 1;
            const byte PRINTER_PIPECMDSIZE = 26;
            byte[] m_pData = new byte[PRINTER_PIPECMDSIZE + 6];
            //First Send Begin Updater
            m_pData[0] = 6;
            m_pData[1] = 0x44; //

            Buffer.BlockCopy(BitConverter.GetBytes((int)this.numericUpDown1.Value), 0, m_pData, 2, 4);

            CoreInterface.SetPipeCmdPackage(m_pData, m_pData[0], port);
            SaveDebugParaToFile(FIRE_REQ, this.numericUpDown1.Value.ToString());
        }

        private void buttonXaar382Mode_Click(object sender, EventArgs e)
        {
            if (comboBoxXaar382Mode.SelectedIndex == -1)
            {
                MessageBox.Show("选中值不能为空.");
                return;
            }
            byte[] val = new byte[64];
            val[0] = (byte) comboBoxXaar382Mode.SelectedIndex;
            uint bufsize = (uint)val.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x6e, val, ref bufsize, 0, 10);
            if (ret == 0)
            {
                MessageBox.Show("Set Xaar382 Mode fialed!");
            }
            else
            {
                SaveDebugParaToFile(XAAR382_MODE, comboBoxXaar382Mode.SelectedIndex.ToString());                
            }
        }

        private void buttonWaitMediaReadyTim_Click(object sender, EventArgs e)
        {
            GzCardboardParam cc = new GzCardboardParam();
            cc.WaitMediaReadyTime = (int)numWaitMediaReadyTime.Value;
            cc.AutoCappingDelayTime = (int)numAutoCappingDelay.Value;
            if(EpsonLCD.SetGZCardboardParam(cc))
                MessageBox.Show("设置成功.");
            else
            {
                MessageBox.Show("设置失败.");
            }
        }

        private void buttonReadWaitMediaReadyTim_Click(object sender, EventArgs e)
        {
            GzCardboardParam cc = new GzCardboardParam();
            if (EpsonLCD.GetGZCardboardParam(ref cc))
            {
                numWaitMediaReadyTime.Value = cc.WaitMediaReadyTime;
                numAutoCappingDelay.Value = cc.AutoCappingDelayTime;
                //MessageBox.Show("设置成功.");
            }
            else
            {
                MessageBox.Show("读取失败.");
            }
        }

        private void button3m_ButtonMove_Click(object sender, EventArgs e)
        {
            int speed;
            if (!int.TryParse(m_ComboBoxSpeed.Text, out speed))
                return;
            int dir = m_ComboBoxDirection.SelectedIndex + 1;
            int len = Decimal.ToInt32(m_NumericUpDownLength.Value);

            //移动第二个Y轴
            if (dir == 3 || dir == 4)
                MoveY2((int)m_NumericUpDownY2Length.Value);

            // 老的移动
            const int port = 1;
            const byte PRINTER_PIPECMDSIZE = 26;
            byte[] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
            //First Send Begin Updater
            m_pData[0] = 9 + 2;
            m_pData[1] = 0x70; //Move cmd

            m_pData[2] = (byte)dir; //Move cmd
            m_pData[3] = (byte)(speed & 0xff); //Move cmd
            m_pData[4] = (byte)((speed >> 8) & 0xff); //Move cmd
            m_pData[5] = (byte)((speed >> 16) & 0xff); //Move cmd
            m_pData[6] = (byte)((speed >> 24) & 0xff); //Move cmd
            m_pData[7] = (byte)(len & 0xff); //Move cmd
            m_pData[8] = (byte)((len >> 8) & 0xff); //Move cmd
            m_pData[9] = (byte)((len >> 16) & 0xff); //Move cmd
            m_pData[10] = (byte)((len >> 24) & 0xff); //Move cmd

            CoreInterface.SetPipeCmdPackage(m_pData, m_pData[0], port);
        }

	    private byte _manualformRear = 0;
	    private byte _manualformFront = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            ManualTakeMediaForm manualform = new ManualTakeMediaForm();
            if (manualform.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _manualformRear = manualform.Rear;
                _manualformFront = manualform.Front;
                SetMediaTakeUpDownMode();
            }
        }

        private void SetMediaTakeUpDownMode()
        {
            byte[] data = null;
            // 
            {
                data = new byte[3];
                int i = 0;
                data[i++] = 1;
                data[i++] = _manualformRear;
                data[i++] = _manualformFront; //对于Front，只能取CW（Rear在前）
            }
            uint bufsize = (uint) data.Length;
            CoreInterface.SetEpsonEP0Cmd(0x92, data,ref bufsize, 0, 0x53);
        }

        private void buttonTrayControl_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void buttonTrayControlApply_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1] { (byte)(buttonTrayControl.Checked ? 1 : 0) };
            uint bufsize = (uint)data.Length;
            CoreInterface.SetEpsonEP0Cmd(0x92, data, ref bufsize, 0, 0x54);
        }
        /// <summary>
        /// 主界面ep6数据更新后,通知控件处理数据
        /// </summary>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        public void WaveformEp6MsgUpdated(IntPtr wParam, IntPtr lParam)
        {
            userControl81.ProceedKernelMessage(wParam, lParam);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.PageStep, 0, ref _ss,false);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.PageCrossHead, 0, ref _ss,false);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.PageBidirection, 0, ref _ss,false);
        }

        private void buttonErrorInfoTest_Click(object sender, EventArgs e)
        {
            var msg = SErrorCode.GetInfoFromErrCode(Convert.ToInt32(textBoxerrorCode.Text.Trim(), 16));
            //var msg = SErrorCode.GetInfoFromErrCode(Convert.ToInt32("2003060c", 16));
            MessageBox.Show(msg);
        }

	    private void btnGetHeadData_Click(object sender, EventArgs e)
	    {
//            孟谦 15:25:19 
//req 0xFE  index 0xfff9 value = 数据长度
//最大4096
	        byte cmd = (byte) numeCmd.Value;
	        ushort value = (ushort) numValue.Value;
	        ushort index = (ushort) numIndex.Value;

	        byte[] buf = GetBufferFromHexString();
	        if (buf.Length == 0)
	        {
	            buf = new byte[1];
	        }
	        uint len = (uint) buf.Length;
            CoreInterface.SetEpsonEP0Cmd(cmd, buf, ref len, value, index);
	    }

	    private byte[] GetBufferFromHexString()
	    {
	        int lenAll = (int) numDataLength.Value;
	        string curText = GetHexString();
	        byte[] buf = new byte[lenAll];
	        for (int i = 0; i < lenAll; i++)
	        {
                if (curText.Length > (i + 1) * 2)
	                buf[i] = byte.Parse(curText.Substring(i*2, 2),NumberStyles.HexNumber);
	        }
	        return buf;
	    }

	    private string GetHexString()
	    {
            return textBoxSendData.Text.Replace(" ", "").Trim();
	        ;
	    }
	    private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            int lenAll = (int) numDataLength.Value;
	        string curText = GetHexString();
            int lenCur = curText.Length;
            textBoxSendData.Text = string.Empty;
            for (int i = 0; i < lenAll; i++)
            {
                if (lenCur>(i+1)*2)
                    textBoxSendData.Text += string.Format("{0} ", curText.Substring(i*2, 2));
                else
                    textBoxSendData.Text += string.Format("{0} ", "00");
            }

        }

	    private byte[] ep6Data;
	   const int readWaveform = 11;
	    public void OnEp6DataChanged(int ep6Cmd,int index,byte[] buf)
	    {
            ep6Data = new byte[buf.Length];
            Buffer.BlockCopy(buf,0,ep6Data,0,buf.Length);
            UpdateEp6Ui();
            readAllEvent.Set();
	    }

        private void radioButtonHex_CheckedChanged(object sender, EventArgs e)
        {
            UpdateEp6Ui();
        }

	    private void UpdateEp6Ui()
	    {
	        if (ep6Data == null)
	            return;
            textBoxRecieveData.Text = string.Empty;
            if (radioButtonHex.Checked)
            {
                //textBoxRecieveData.Text = BitConverter.ToString(ep6Data);
                for (int i = 0; i < ep6Data.Length; i++)
                {
                    textBoxRecieveData.Text += string.Format("{0:X2} ", ep6Data[i]);
                }
            }
            else if (radioButtonShijinzhi.Checked)
            {
                for (int i = 0; i < ep6Data.Length; i++)
                {
                    textBoxRecieveData.Text += string.Format("{0}", ep6Data[i]+Environment.NewLine);
                }
            }
            else
            {
                textBoxRecieveData.Text = ASCIIEncoding.ASCII.GetString(ep6Data);
            }
        }

        private void buttonReadWf_Click(object sender, EventArgs e)
        {
            byte cmd = 0x80;
            ushort value = (ushort) numHbId.Value;
            ushort index =0;

            byte[] buf = new byte[10];
            buf[0] = 0xc4;
            buf[1] = 0xb8;
            buf[2] = (byte) numWaveformId.Value;
            buf[3] = 0xfe;
            buf[4] = 0xfe;
            buf[5] = 0xfe;
            buf[6] = 0x04;
            buf[7] = (byte) (radioButtonA.Checked?0x1a:0x1b);
            uint len = (uint)buf.Length;
            if (CoreInterface.SetEpsonEP0Cmd(cmd, buf, ref len, value, index) == 0)
            {
                MessageBox.Show("发送读waveform命令失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

	    private void buttonSaveAS_Click(object sender, EventArgs e)
	    {
	        SaveFileDialog saveas = new SaveFileDialog();
	        saveas.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //桌面路径
	        DialogResult dr = saveas.ShowDialog(this);
	        if (dr == DialogResult.OK)
	        {
                if (radioButtonHex.Checked)
                {
                    FileStream wfStream = new FileStream(saveas.FileName, FileMode.CreateNew);
                    wfStream.Write(ep6Data, 0, ep6Data.Length);
                    wfStream.Flush();
                    wfStream.Close();
                }
                else if (radioButtonShijinzhi.Checked)
                {
                    StreamWriter sw = new StreamWriter(saveas.FileName, false);
                    for (int i = 0; i < ep6Data.Length; i++)
                    {
                        sw.WriteLine(string.Format("{0}", ep6Data[i]));
                    }
                    sw.Flush();
                    sw.Close();
                }
                else
                {
                    StreamWriter sw = new StreamWriter(saveas.FileName, false);
                    sw.WriteLine(ASCIIEncoding.ASCII.GetString(ep6Data));
                    sw.Flush();
                    sw.Close();
                }
	        }
	    }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveas = new SaveFileDialog();
            saveas.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //桌面路径
            saveas.Filter = "文本文件|*.txt";
            saveas.DefaultExt = "txt";
            DialogResult dr = saveas.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveas.FileName, false);
                int j = 0;
                for (int i = 7; i < ep6Data.Length; i++)
                {
                    sw.WriteLine(string.Format("{0}", ep6Data[i]));
                    j++;
                    if (j == 224)
                        break;
                }
                sw.Flush();
                sw.Close();
            }
        }

	    private BackgroundWorker readallWorker;
        private void buttonReadAll_Click(object sender, EventArgs e)
        {
            if (readallWorker.IsBusy)
            {
                MessageBox.Show("正在读取,请等待当前读取完成后重试.");
            }
            else
            {
                SaveFileDialog saveas = new SaveFileDialog();
                saveas.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //桌面路径
                saveas.Filter = "文本文件|*.txt";
                saveas.DefaultExt = "txt";
                DialogResult dr = saveas.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    readallWorker.RunWorkerAsync(saveas.FileName);
                }

            }
        }


        private void buttonAccTimeGet_Click(object sender, EventArgs e)
        {
            byte[] buf = new byte[64];
            uint bufsize = (uint)buf.Length;
            int ret = CoreInterface.GetEpsonEP0Cmd(0x82, buf, ref bufsize, 0, 3);
            if (ret != 0)
            {
                this.numAccTimeX.Value = BitConverter.ToInt32(buf, 4);
                this.numAccTimeY.Value = BitConverter.ToInt32(buf, 8);
            }
        }


        private void buttonGrayMapSet_Click(object sender, EventArgs e)
        {
            byte[] buf = new byte[12];
            buf[0] = 0x8d;
            buf[1] = (byte)(1 << comboBoxPrintHead.SelectedIndex);
            if (comboBoxDotBit.SelectedIndex == 0)
            {
                buf[3] = 0x00;
                buf[4] = (byte)((comboBoxGrayMap1.SelectedIndex << 4) | 0x07);
            }
            else if (comboBoxDotBit.SelectedIndex == 1)
            {
                buf[3] = 0x01;
                buf[4] = (byte)((comboBoxGrayMap1.SelectedIndex << 4) | 0x07);
                buf[5] = (byte)((comboBoxGrayMap3.SelectedIndex << 4) | comboBoxGrayMap2.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Please select dotbit!");
                return;
            }
            uint bufsize = (uint)buf.Length;
            MessageBox.Show(
                CoreInterface.SetEpsonEP0Cmd(0x80, buf, ref bufsize, (ushort)numericUpDownHeadBoard.Value, 0) == 0
                    ? "Set gray map fialed!"
                    : "Set gray map success!");
        }

        private void comboBoxDotBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxDotBit.SelectedIndex)
            {
                case 0:
                    comboBoxGrayMap1.Visible = true;
                    comboBoxGrayMap2.Visible = false;
                    comboBoxGrayMap3.Visible = false;
                    break;
                case 1:
                    comboBoxGrayMap1.Visible = true;
                    comboBoxGrayMap2.Visible = true;
                    comboBoxGrayMap3.Visible = true;
                    break;
                default:
                    comboBoxGrayMap1.Visible = false;
                    comboBoxGrayMap2.Visible = false;
                    comboBoxGrayMap3.Visible = false;
                    break;
            }
        }

	    private void buttonShakeSet_Click(object sender, EventArgs e)
	    {
	        PhShake_tag cc = new PhShake_tag()
	        {
	            bOpenshake = (ushort) (checkBoxShake.Checked ? 1 : 0),
	            shakeDual = (ushort) numericIgnitionNumber.Value,
	            shakeNull = (ushort) numericShockInterval.Value,
	        };
	        if (EpsonLCD.SetShakeParam(cc))
	            MessageBox.Show("设置成功.");
	        else
	            MessageBox.Show("设置失败.");
	    }

	    private void buttonShakeGet_Click(object sender, EventArgs e)
        {
            PhShake_tag cc = new PhShake_tag();
            if (EpsonLCD.GetShakeParam(ref cc))
            {
                checkBoxShake.Checked = cc.bOpenshake == 1;
                numericIgnitionNumber.Value = cc.shakeDual;
                numericShockInterval.Value = cc.shakeNull;
            }
        }

        private void button_Calibrate_Click(object sender, EventArgs e)
        {
            int cmd = 0;
            if (int.TryParse(textBox_CalibrateCmd.Text.Trim(), out cmd))
            {
                CoreInterface.SendCalibrateCmd(cmd, 0, ref _ss, false);
            }
            else
            {
                MessageBox.Show(@"input error !");
            }
        }

        private void btnSaveFWLog_Click(object sender, EventArgs e)
        {
            CoreInterface.SaveFWLog();
        }

        private bool GetTemperaturProfileStateFlag = false;
        private bool CurrentTemperaturProfileState = false;
        private void button_TemperatureProfileState_Click(object sender, EventArgs e)
        {
            GetTemperaturProfileStateFlag = false;
            groupBoxTemperaturProfil.Enabled = false;
            label_TemperatureProfileState.Text = "unknown";
            if (!EpsonLCD.GetTemperaturProfileState())
            {
                MessageBox.Show("Get TemperaturProfile State Failed!");
                return;
            }
            Thread thread = new Thread(GetTemperaturProfileState_TimeOut) { IsBackground = true };
            thread.Start();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            CurrentTemperaturProfileState = true;
            GetTemperaturProfileStateFlag = false;
            groupBoxTemperaturProfil.Enabled = false;
            if (!EpsonLCD.SetTemperaturProfil(true))
            {
                MessageBox.Show("Open TemperaturProfil Failed!");
                return;
            }
            Thread thread = new Thread(GetTemperaturProfileState_TimeOut) { IsBackground = true };
            thread.Start();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            CurrentTemperaturProfileState = false;
            GetTemperaturProfileStateFlag = false;
            groupBoxTemperaturProfil.Enabled = false;
            if (!EpsonLCD.SetTemperaturProfil(false))
            {
                MessageBox.Show("Close TemperaturProfil Failed!");
                return;
            }
            Thread thread = new Thread(GetTemperaturProfileState_TimeOut) { IsBackground = true };
            thread.Start();
        }

        private void GetTemperaturProfileState_TimeOut()
        {
            for (int i = 0; i < 50; i++)
            {
                if (!GetTemperaturProfileStateFlag)
                {
                    Thread.Sleep(100);
                }
                else
                {
                    break;
                }
            }
            Invoke(new Action<bool>(o => groupBoxTemperaturProfil.Enabled = o), true);
            if (!GetTemperaturProfileStateFlag)
            {
                MessageBox.Show("Get TemperaturProfile State Failed!:TimeOut(5s)");
            }
        }

        public void SetTemperaturProfileState(byte[] info)
        {
            try
            {
                List<byte> buffer = new List<byte>(info);
                HbdGetRuningParam_tag param = (HbdGetRuningParam_tag)PubFunc.BytesToStruct(buffer.GetRange(1, Marshal.SizeOf(typeof(HbdGetRuningParam_tag))).ToArray(), typeof(HbdGetRuningParam_tag));
                GetTemperaturProfileStateFlag = true;
                switch (((param.curRunFlag >> 5) & 1))
                {
                    case 0:
                        label_TemperatureProfileState.Text = "Open";
                        break;
                    case 1:
                        label_TemperatureProfileState.Text = "Close";
                        break;
                    default:
                        label_TemperatureProfileState.Text = "unknown";
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void SetTemperaturProfile(byte info)
        {
            GetTemperaturProfileStateFlag = true;
            if (info == 1)
            {
                MessageBox.Show("success!");
                if (CurrentTemperaturProfileState)
                {
                    label_TemperatureProfileState.Text = "Open";
                }
                else
                {
                    label_TemperatureProfileState.Text = "Close";
                }
            }
            else
            {
                MessageBox.Show("Failed!");
            }
        }

    }

    public struct MotionDebugInfo
	{
		public int xPos;
		public int yPos;
		public int nDebugInt1;
		public int nDebugInt2;
		public int nDebugInt3;
		public int nDebugInt4;
	}

    public struct MotionDebugInfoFromARM
    {
        public int xPosition;
        public int yPosition;
        public int zPosition;
        public int fPosition;
        public int y1Position;
    }
}
