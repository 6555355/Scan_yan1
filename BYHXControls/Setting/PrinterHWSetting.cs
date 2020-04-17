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
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text;
using System.Linq;

using BYHXPrinterManager.Main;
using System.Collections.Generic;
using PrinterStubC.Common;
using PrinterStubC.Utility;
using WAF_OnePass.Domain.Utility;
using System.Data;

namespace BYHXPrinterManager.Setting
{
    /// <summary>
    /// Summary description for PrinterHWSetting.
    /// </summary>
    public class PrinterHWSetting : BYHXUserControl
    {
        private ArrayList m_FileHeadList = new ArrayList(); //File Vender.dll
        private ArrayList m_SupportHeadList = new ArrayList(); //File Vender.dll

        //private SPrintAmendProperty m_AmendProperty;
        private bool m_bEpson = false;
        private UIPreference m_CurrentPreference;
        private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
        private const int MAX_HEAD_NUM = 40;
        private byte[] m_WidthList = new byte[] {18, 25, 32, 33, 35, 50, 55, 60, 100};
        private byte[] m_ColorNumList = new byte[] {4, 6, 1, 2, 5, 7, 8};
        private byte[] m_InkType = new byte[] {0xA, 0xB, 0xC};
        private const string sPrinterProductList = "PrinterProductList_";
        private VenderPrinterConfig m_PrinterConfig = new VenderPrinterConfig();
        private int m_nProductId = 0;
        //		private EPR_FactoryData_Ex eprfd = new EPR_FactoryData_Ex();
        public event EventHandler OKButtonClicked;
        private System.Windows.Forms.ComboBox[] m_ComboBoxPrintColorOder;
        private System.Windows.Forms.ComboBox[] m_ComboxBoxRipColorOder;
        private SPrinterProperty m_sPrinterProperty;
        private EPR_FactoryData_Ex epsonExFac = new EPR_FactoryData_Ex(new SPrinterProperty());
        private SFWFactoryData fwData;
        private bool bIsGongzhengEpson = false;
        private bool bIsAllWinEpson = false;
        private bool bDoubleYAxis = false;
        private DOUBLE_YAXIS doubleYaxis = new DOUBLE_YAXIS();
        private bool bSetHeadCount = false;

        private DividerPanel.DividerPanel dividerPanel1;
        private System.Windows.Forms.Button m_ButtonOK;
        private BYHXPrinterManager.GradientControls.Grouper groupBox1;
        private System.Windows.Forms.RadioButton m_RadioButtonEncoder;
        private System.Windows.Forms.RadioButton m_RadioButtonServoEncoder;
        private System.Windows.Forms.Label m_LabelHighSpeed;
        private System.Windows.Forms.Label m_LabelColorNum;
        private System.Windows.Forms.Label m_LabelStripeSpace;
        private System.Windows.Forms.Label m_LabelStripeWidth;
        private System.Windows.Forms.ComboBox m_ComboBoxGroupNumber;
        private System.Windows.Forms.ComboBox m_ComboBoxColorNumber;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownGroupSpace;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownColorSpace;
        private System.Windows.Forms.ToolTip m_ToolTip;
        private System.Windows.Forms.ComboBox m_ComboBoxHeadType;
        private System.Windows.Forms.Label m_LabelHeadType;
        private System.Windows.Forms.Label m_LabelWidth;
        private System.Windows.Forms.ComboBox m_ComboBoxWidth;
        private System.Windows.Forms.Button m_ButtonClear;
        private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxVender;
        private System.Windows.Forms.Label m_LabelYSpace;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownYSpace;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownAngle;
        private System.Windows.Forms.Label m_LabelAngle;
        private System.Windows.Forms.ContextMenu m_ContextMenu;
        private System.Windows.Forms.MenuItem m_menuItemDelete;
        private System.Windows.Forms.Label LableInkType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox m_comboBoxInkType;
        private System.Windows.Forms.ComboBox m_comboBoxJetSpeed;
        private System.Windows.Forms.Button m_ButtonWriteInkCurve;
        private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxInk;
        private System.Windows.Forms.CheckBox m_CheckBoxOneHeadDivider;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label m_LabelWhiteInkNum;
        private System.Windows.Forms.Label m_LabelCoatColorNum;
        private System.Windows.Forms.CheckBox m_CheckBoxIsHeadLeft;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownWhiteColorNum;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownCoatColorNum;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private BYHXPrinterManager.GradientControls.Grouper grouperEpson;
        private System.Windows.Forms.CheckBox m_CheckBoxMirror;
        private BYHXPrinterManager.GradientControls.Grouper grouperPrintColorOrder;
        private System.Windows.Forms.ComboBox comboBoxColorOderSample;
        private System.Windows.Forms.CheckBox m_CheckBoxSupportLcd;
        private System.Windows.Forms.NumericUpDown numericUpDownW;
        private System.Windows.Forms.GroupBox groupBoxDualBank;
        private System.Windows.Forms.RadioButton radioButtonSingleBank;
        private System.Windows.Forms.RadioButton radioButtonDualBank;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox8colorarrangement;
        private CheckBox checkBox4ColorMirror;
        private CheckBox checkBox_VerY;
        private GradientControls.Grouper grouper2;
        private Label label12;
        private Label labelAccDistanceL;
        private Label label10;
        private GradientControls.Grouper grouperRipColorOrder;
        private ComboBox comboxRipcolor;
        private ComboBox combobox_RasterSence;
        private NumericUpDown numericUpDown_PrintSence;
        private NumericUpDown numSpeedUpDistanceL;
        private NumericUpDown numFlatDistance;
        private Label label_FlatDistance;
        private Panel panel3;
        private CheckBox checkBoxZMeasur;
        private CheckBox checkBox8CCompatibilityMode;
        private Panel panelLeft;
        private CheckBox checkBoxStaggered;
        private CheckBox checkBoxMediaSensor;
        private CheckBox checkBoxWhiteInkRight;
        private NumericUpDown m_NumericUpDown_YEncoderDPI;
        private Label m_Label_YEncoderDPI;
        private NumericUpDown m_NumericUpDown_MaxOffsetPos;
        private Label m_Label_MaxOffsetPos;
        private CheckBox m_CheckBoxXaar382Mode;
        private NumericUpDown numServiceStation;
        private Label label13;
        private System.ComponentModel.IContainer components;
        private NumericUpDown numDefaultZ;
        private Label lblDefaultZ;
        private GradientControls.Grouper grouperSSystem;
        private Panel panelCB1;
        private CheckBox checkBoxPort13;
        private CheckBox checkBoxPort12;
        private CheckBox checkBoxPort11;
        private Label label18;
        private Panel panelCB2;
        private CheckBox checkBoxPort24;
        private CheckBox checkBoxPort23;
        private CheckBox checkBoxPort22;
        private CheckBox checkBoxPort21;
        private Label label19;
        private ComboBox comboBoxTopologyMode;
        private Label label14;

        private ColorEnum_Short[] defaultPrintColorOrder = new ColorEnum_Short[8]
        {
            ColorEnum_Short.K, ColorEnum_Short.C,
            ColorEnum_Short.M, ColorEnum_Short.Y,
            ColorEnum_Short.Lm, ColorEnum_Short.Lc, ColorEnum_Short.W, ColorEnum_Short.W
        };

        private NumericUpDown numericUpDownHBNum;
        private Label label16;
        private NumericUpDown numSpeedUpDistanceR;
        private Label labelAccDistanceR;
        private NumericUpDown numzMaxRoute;
        private Label labelzMaxRoute;
        private Button buttonHeadMix;
        private CheckBox checkBoxSupportZendPointSensor;
        private NumericUpDown numFlatDistanceY;
        private Label label_FlatDistanceY;
        private NumericUpDown numericUpDown_YMaxLen;
        private Label label_YMaxLen;
        private TabPage tabPage3;
        private GradientControls.Grouper grouper3;
        private GradientControls.Grouper grouperColorOrder_2;
        private ComboBox comboBoxColorOderSample_2;
        private Panel panel4;
        private GradientControls.Grouper grouper4;
        private CheckBox cbxThreeColor_2;
        private ComboBox cbxWrapHead_2;
        private ComboBox comboBoxWhiteVarnishLayout_2;
        private Label labelWhiteVarnishLayout_2;
        private CheckBox checkBoxWrapAroundOnX_2;
        private Label label7_2;
        private ComboBox comboBoxSymmetricType_2;
        private Label label1_2;
        private ComboBox comboBoxLayoutType_2;
        private CheckBox checkBoxSymmetricColorOder_2;
        private Label label4_2;
        private ComboBox comboBoxXEncoderDPI_2;
        private Label label5_2;
        private Label label6_2;
        private Label label3_2;
        private NumericUpDown numericUpDownYInterleaveNum_2;
        private ComboBox comboBoxMaxGroupNumber_2;
        private CheckBox checkBoxWeakSovent_2;
        private TextBox txtPrinterName_2;
        private Label label8_2;
        private TextBox txtManufacturerName_2;
        private CheckBox checkBoxHeadDir_2;
        private Label label9_2;
        private ComboBox m_ComboBoxBit2Mode_2;
        private Label labelSpeed_2;
        private ComboBox m_ComboBoxSpeed_2;

        private List<SystemConfig> systemConfigMap = new List<SystemConfig>();
        private NumericUpDown m_NumericUpDownVOffset;
        private Label label_VOffset;
        private CheckBox checkBoxPort14;
        private CheckBox checkBoxMotorDebug;
        private ComboBox listHeadCount;
        private Label lblHeadCount;

        private bool IsY2 = false;

        public List<SystemConfig> SystemConfigMap
        {
            get { return systemConfigMap; }
            set { systemConfigMap = value; }
        }

        public PrinterHWSetting()
        {
            InitVenderList();

            //VenderDisp vd = new VenderDisp();
            //for (int i = 0; i < m_FileHeadList.Count; i++)
            //{
            //    vd = (VenderDisp)m_FileHeadList[i];
            //    if (vd.DisplayName.Trim() == "Epson_Gen5_XP600")
            //    {
            //        IsY2 = true;
            //        break;
            //    }
            //}
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            InitSystemConfigMap();
#if !LIYUUSB
            m_GroupBoxInk.Visible = false;
            m_ButtonWriteInkCurve.Visible = false;
#else
            this.m_GroupBoxVender.AutoSize = true;
            this.m_GroupBoxVender.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			bool bfac = PubFunc.IsFactoryUser();
			if(!bfac)
			{
				groupBox1.Visible = false;
				if (m_GroupBoxInk.Visible)
				{
					m_GroupBoxInk.Top = m_GroupBoxVender.Top;
					//m_GroupBoxInk.Height = m_GroupBoxVender.Height;
				}
				m_CheckBoxOneHeadDivider.Visible = false;
				m_CheckBoxIsHeadLeft.Visible = false;
				m_LabelYSpace.Visible = false;
				m_NumericUpDownYSpace.Visible = false;
				m_LabelAngle.Visible = false;
				m_NumericUpDownAngle.Visible = false;
				m_LabelWhiteInkNum.Visible = false;
				m_NumericUpDownWhiteColorNum.Visible = false;
				m_LabelCoatColorNum.Visible = false;
				m_NumericUpDownCoatColorNum.Visible = false;
			}
            m_ComboBoxPrinterList.Visible = false;
            m_ButtonAdd.Visible = false;
#endif
            numericUpDownW.Minimum = 0;
            numericUpDownW.Maximum =
                new decimal(UIPreference.ToInchLength(UILengthUnit.Centimeter, m_WidthList[m_WidthList.Length - 1]*10));
            groupBoxDualBank.Visible = false;

            bDoubleYAxis = PubFunc.IsDoubleYAxis();
            //不支持双轴的场合，不显示
            m_Label_MaxOffsetPos.Visible = m_NumericUpDown_MaxOffsetPos.Visible =
                m_Label_YEncoderDPI.Visible = m_NumericUpDown_YEncoderDPI.Visible = bDoubleYAxis;
#if !(RW_AMEND_PARAMETERS||COLORORDER)
                if (this.tabControl1.TabPages.Contains(this.tabPage2))
                {
                    this.tabControl1.TabPages.Remove(this.tabPage2);
                }
#endif

            grouperSSystem.Visible = CoreInterface.IsS_system();

            if (!IsY2)
            {
                if (this.tabControl1.TabPages.Contains(this.tabPage3))
                {
                    this.tabControl1.TabPages.Remove(this.tabPage3);
                }
            }
            else
            {
                if (this.tabControl1.TabPages.Contains(this.tabPage2))
                {
                    this.tabControl1.TabPages.Remove(this.tabPage2);
                }
            }

            // 调试权限可以输入新的分辨率
            combobox_RasterSence.DropDownStyle = (PubFunc.GetUserPermission() != (int)UserPermission.Operator)
                ? ComboBoxStyle.DropDown
                : ComboBoxStyle.DropDownList;

            InitRasterSencerList();

            label_YMaxLen.Visible = numericUpDown_YMaxLen.Visible =SPrinterProperty.IsAllPrint()|| UIFunctionOnOff.SupportSetYMaxLen;

            HEAD_BOARD_TYPE hbType = (HEAD_BOARD_TYPE)CoreInterface.get_HeadBoardType(true);
            if ((hbType == HEAD_BOARD_TYPE.Ricoh_Gen6_4H && !CoreInterface.IsS_system()) || hbType == HEAD_BOARD_TYPE.Ricoh_Gen6_16H)
            {
                listHeadCount.Items.Clear();
                int phCount = 4;
                if (hbType == HEAD_BOARD_TYPE.Ricoh_Gen6_4H)
                {
                    phCount = 4;
                }
                else if (hbType == HEAD_BOARD_TYPE.Ricoh_Gen6_16H)
                {
                    phCount = 16;
                }
                for (int i = 1; i <= phCount; i++)
                {
                    listHeadCount.Items.Add(i.ToString());
                }

                lblHeadCount.Visible = true;
                listHeadCount.Visible = true;
                bSetHeadCount = true;
            }
            else
            {
                lblHeadCount.Visible = false;
                listHeadCount.Visible = false;
                bSetHeadCount = false;
            }
        }

        private void InitVenderList()
        {
            ArrayList m_VenderList = new ArrayList();
            m_FileHeadList = new ArrayList();
            ArrayList curArrayList = m_VenderList;
            int type = 1;
            string m_UpdaterFileName = Application.StartupPath;
            m_UpdaterFileName += "\\Vender.dll";
            if (File.Exists(m_UpdaterFileName))
            {
                using (StreamReader textReader = new StreamReader(m_UpdaterFileName))
                {
                    string line = "";
                    while ((line = textReader.ReadLine()) != null)
                    {
                        string[] curline = line.Split('=');
                        if (curline == null || curline.Length != 2)
                        {
                            string[] titleLine_Left = line.Split('[');
                            if (titleLine_Left == null || titleLine_Left.Length != 2)
                                continue;
                            string[] titleLine_Right = titleLine_Left[1].Split(']');
                            if (titleLine_Right == null || titleLine_Right.Length != 2)
                                continue;
                            if (titleLine_Right[0] == "PrinterHeadEnum")
                            {
                                curArrayList = m_FileHeadList;
                                type = 2;
                            }
                            continue;
                        }
                        VenderDisp vd = new VenderDisp();
                        string dis = curline[0].TrimStart(null);
                        dis = dis.TrimEnd(null);
                        vd.DisplayName = dis;


                        if (type == 1)
                        {
                            dis = curline[1].Replace("0x", "");
                            dis = dis.Replace(",", "");

                            dis = dis.TrimStart(null);
                            dis = dis.TrimEnd(null);

                            vd.VenderID = Convert.ToInt32(dis, 16);

                            curArrayList.Add(vd);
                        }
                        else
                        {
                            dis = curline[1];
                            dis = dis.Replace(",", "");

                            dis = dis.TrimStart(null);
                            dis = dis.TrimEnd(null);

                            vd.VenderID = Convert.ToInt32(dis, 10);
                            curArrayList.Add(vd);
                        }
                    }
                }
#if false
                PrinterHeadEnum[] array1 = (PrinterHeadEnum[]) Enum.GetValues(typeof (PrinterHeadEnum));
                if (m_FileHeadList.Count < array1.Length - 1)
                {
                    m_FileHeadList = new ArrayList();
                    for (int i = 0; i < array1.Length - 1; i++)
                    {
                        VenderDisp vd = new VenderDisp();
                        vd.DisplayName = array1[i].ToString();
                        vd.VenderID = (int) array1[i];
                        m_FileHeadList.Add(vd);
                    }
                }
#endif
            }
            else
            {
                PrinterHeadEnum[] array1 = (PrinterHeadEnum[]) Enum.GetValues(typeof (PrinterHeadEnum));
                m_FileHeadList = new ArrayList();

                for (int i = 0; i < array1.Length - 1; i++)
                {
                    VenderDisp vd = new VenderDisp();
                    vd.DisplayName = array1[i].ToString();
                    vd.VenderID = (int) array1[i];
                    m_FileHeadList.Add(vd);
                }
            }
        }

        private void InitSystemConfigMap()
        {
            try
            {
                SelfcheckXmlDocument mapDoc = new SelfcheckXmlDocument();
                string configFilePath = System.IO.Path.Combine(Application.StartupPath, "SystemConfigMap.xml");
                if (!File.Exists(configFilePath))
                {
                    //MessageBox.Show(string.Format("{0} No Found!",configFilePath));
                    return;
                }
                mapDoc.Load(configFilePath);
                SystemConfigMap.Clear();
                foreach (XmlNode node in mapDoc.DocumentElement.ChildNodes)
                {
                    SystemConfig item =
                        (SystemConfig) PubFunc.SystemConvertFromXml(node.OuterXml, typeof (SystemConfig));
                    SystemConfigMap.Add(item);
                }
                //SystemConfigMap = (List<SystemConfig>)PubFunc.SystemConvertFromXml(mapDoc.DocumentElement.OuterXml, typeof(List<SystemConfig>));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrinterHWSetting));
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style2 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style3 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style4 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style5 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style6 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style15 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style16 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style7 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style8 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style9 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style10 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style11 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style12 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style13 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style14 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style21 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style22 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style17 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style18 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style19 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style20 = new BYHXPrinterManager.Style();
            this.m_ContextMenu = new System.Windows.Forms.ContextMenu();
            this.m_menuItemDelete = new System.Windows.Forms.MenuItem();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_GroupBoxVender = new BYHXPrinterManager.GradientControls.Grouper();
            this.listHeadCount = new System.Windows.Forms.ComboBox();
            this.lblHeadCount = new System.Windows.Forms.Label();
            this.m_NumericUpDownVOffset = new System.Windows.Forms.NumericUpDown();
            this.label_VOffset = new System.Windows.Forms.Label();
            this.numericUpDown_YMaxLen = new System.Windows.Forms.NumericUpDown();
            this.label_YMaxLen = new System.Windows.Forms.Label();
            this.numServiceStation = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.numericUpDownW = new System.Windows.Forms.NumericUpDown();
            this.m_LabelHighSpeed = new System.Windows.Forms.Label();
            this.m_NumericUpDownGroupSpace = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownColorSpace = new System.Windows.Forms.NumericUpDown();
            this.m_LabelWidth = new System.Windows.Forms.Label();
            this.m_ComboBoxGroupNumber = new System.Windows.Forms.ComboBox();
            this.m_LabelColorNum = new System.Windows.Forms.Label();
            this.m_ComboBoxColorNumber = new System.Windows.Forms.ComboBox();
            this.m_ComboBoxWidth = new System.Windows.Forms.ComboBox();
            this.m_LabelStripeSpace = new System.Windows.Forms.Label();
            this.m_LabelStripeWidth = new System.Windows.Forms.Label();
            this.m_ComboBoxHeadType = new System.Windows.Forms.ComboBox();
            this.m_LabelHeadType = new System.Windows.Forms.Label();
            this.m_LabelYSpace = new System.Windows.Forms.Label();
            this.m_NumericUpDownYSpace = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownAngle = new System.Windows.Forms.NumericUpDown();
            this.m_LabelAngle = new System.Windows.Forms.Label();
            this.m_NumericUpDownWhiteColorNum = new System.Windows.Forms.NumericUpDown();
            this.m_LabelWhiteInkNum = new System.Windows.Forms.Label();
            this.m_NumericUpDownCoatColorNum = new System.Windows.Forms.NumericUpDown();
            this.m_LabelCoatColorNum = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox8colorarrangement = new System.Windows.Forms.GroupBox();
            this.checkBox8CCompatibilityMode = new System.Windows.Forms.CheckBox();
            this.checkBox4ColorMirror = new System.Windows.Forms.CheckBox();
            this.checkBox_VerY = new System.Windows.Forms.CheckBox();
            this.m_CheckBoxSupportLcd = new System.Windows.Forms.CheckBox();
            this.m_CheckBoxMirror = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_CheckBoxXaar382Mode = new System.Windows.Forms.CheckBox();
            this.checkBoxZMeasur = new System.Windows.Forms.CheckBox();
            this.groupBoxDualBank = new System.Windows.Forms.GroupBox();
            this.radioButtonSingleBank = new System.Windows.Forms.RadioButton();
            this.radioButtonDualBank = new System.Windows.Forms.RadioButton();
            this.m_CheckBoxOneHeadDivider = new System.Windows.Forms.CheckBox();
            this.m_CheckBoxIsHeadLeft = new System.Windows.Forms.CheckBox();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.checkBoxSupportZendPointSensor = new System.Windows.Forms.CheckBox();
            this.buttonHeadMix = new System.Windows.Forms.Button();
            this.checkBoxStaggered = new System.Windows.Forms.CheckBox();
            this.checkBoxMediaSensor = new System.Windows.Forms.CheckBox();
            this.checkBoxWhiteInkRight = new System.Windows.Forms.CheckBox();
            this.m_GroupBoxInk = new BYHXPrinterManager.GradientControls.Grouper();
            this.LableInkType = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_comboBoxInkType = new System.Windows.Forms.ComboBox();
            this.m_comboBoxJetSpeed = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new BYHXPrinterManager.GradientControls.Grouper();
            this.m_RadioButtonServoEncoder = new System.Windows.Forms.RadioButton();
            this.m_RadioButtonEncoder = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grouperEpson = new BYHXPrinterManager.GradientControls.Grouper();
            this.checkBoxMotorDebug = new System.Windows.Forms.CheckBox();
            this.grouperSSystem = new BYHXPrinterManager.GradientControls.Grouper();
            this.numericUpDownHBNum = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.panelCB1 = new System.Windows.Forms.Panel();
            this.checkBoxPort14 = new System.Windows.Forms.CheckBox();
            this.checkBoxPort13 = new System.Windows.Forms.CheckBox();
            this.checkBoxPort12 = new System.Windows.Forms.CheckBox();
            this.checkBoxPort11 = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.panelCB2 = new System.Windows.Forms.Panel();
            this.checkBoxPort24 = new System.Windows.Forms.CheckBox();
            this.checkBoxPort23 = new System.Windows.Forms.CheckBox();
            this.checkBoxPort22 = new System.Windows.Forms.CheckBox();
            this.checkBoxPort21 = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.comboBoxTopologyMode = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.grouper2 = new BYHXPrinterManager.GradientControls.Grouper();
            this.numFlatDistanceY = new System.Windows.Forms.NumericUpDown();
            this.label_FlatDistanceY = new System.Windows.Forms.Label();
            this.numzMaxRoute = new System.Windows.Forms.NumericUpDown();
            this.labelzMaxRoute = new System.Windows.Forms.Label();
            this.numSpeedUpDistanceR = new System.Windows.Forms.NumericUpDown();
            this.labelAccDistanceR = new System.Windows.Forms.Label();
            this.lblDefaultZ = new System.Windows.Forms.Label();
            this.numDefaultZ = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDown_YEncoderDPI = new System.Windows.Forms.NumericUpDown();
            this.m_Label_YEncoderDPI = new System.Windows.Forms.Label();
            this.m_NumericUpDown_MaxOffsetPos = new System.Windows.Forms.NumericUpDown();
            this.m_Label_MaxOffsetPos = new System.Windows.Forms.Label();
            this.numFlatDistance = new System.Windows.Forms.NumericUpDown();
            this.label_FlatDistance = new System.Windows.Forms.Label();
            this.numSpeedUpDistanceL = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_PrintSence = new System.Windows.Forms.NumericUpDown();
            this.combobox_RasterSence = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.labelAccDistanceL = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.grouperRipColorOrder = new BYHXPrinterManager.GradientControls.Grouper();
            this.comboxRipcolor = new System.Windows.Forms.ComboBox();
            this.grouperPrintColorOrder = new BYHXPrinterManager.GradientControls.Grouper();
            this.comboBoxColorOderSample = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.grouper3 = new BYHXPrinterManager.GradientControls.Grouper();
            this.grouperColorOrder_2 = new BYHXPrinterManager.GradientControls.Grouper();
            this.comboBoxColorOderSample_2 = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.grouper4 = new BYHXPrinterManager.GradientControls.Grouper();
            this.cbxThreeColor_2 = new System.Windows.Forms.CheckBox();
            this.cbxWrapHead_2 = new System.Windows.Forms.ComboBox();
            this.comboBoxWhiteVarnishLayout_2 = new System.Windows.Forms.ComboBox();
            this.labelWhiteVarnishLayout_2 = new System.Windows.Forms.Label();
            this.checkBoxWrapAroundOnX_2 = new System.Windows.Forms.CheckBox();
            this.label7_2 = new System.Windows.Forms.Label();
            this.comboBoxSymmetricType_2 = new System.Windows.Forms.ComboBox();
            this.label1_2 = new System.Windows.Forms.Label();
            this.comboBoxLayoutType_2 = new System.Windows.Forms.ComboBox();
            this.checkBoxSymmetricColorOder_2 = new System.Windows.Forms.CheckBox();
            this.label4_2 = new System.Windows.Forms.Label();
            this.comboBoxXEncoderDPI_2 = new System.Windows.Forms.ComboBox();
            this.label5_2 = new System.Windows.Forms.Label();
            this.label6_2 = new System.Windows.Forms.Label();
            this.label3_2 = new System.Windows.Forms.Label();
            this.numericUpDownYInterleaveNum_2 = new System.Windows.Forms.NumericUpDown();
            this.comboBoxMaxGroupNumber_2 = new System.Windows.Forms.ComboBox();
            this.checkBoxWeakSovent_2 = new System.Windows.Forms.CheckBox();
            this.txtPrinterName_2 = new System.Windows.Forms.TextBox();
            this.label8_2 = new System.Windows.Forms.Label();
            this.txtManufacturerName_2 = new System.Windows.Forms.TextBox();
            this.checkBoxHeadDir_2 = new System.Windows.Forms.CheckBox();
            this.label9_2 = new System.Windows.Forms.Label();
            this.m_ComboBoxBit2Mode_2 = new System.Windows.Forms.ComboBox();
            this.labelSpeed_2 = new System.Windows.Forms.Label();
            this.m_ComboBoxSpeed_2 = new System.Windows.Forms.ComboBox();
            this.dividerPanel1 = new DividerPanel.DividerPanel();
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.m_ButtonClear = new System.Windows.Forms.Button();
            this.m_ButtonWriteInkCurve = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.m_GroupBoxVender.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownVOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_YMaxLen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numServiceStation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownGroupSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownColorSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownYSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownWhiteColorNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownCoatColorNum)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox8colorarrangement.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBoxDualBank.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.m_GroupBoxInk.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.grouperEpson.SuspendLayout();
            this.grouperSSystem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHBNum)).BeginInit();
            this.panelCB1.SuspendLayout();
            this.panelCB2.SuspendLayout();
            this.grouper2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFlatDistanceY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numzMaxRoute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeedUpDistanceR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDefaultZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDown_YEncoderDPI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDown_MaxOffsetPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFlatDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeedUpDistanceL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_PrintSence)).BeginInit();
            this.grouperRipColorOrder.SuspendLayout();
            this.grouperPrintColorOrder.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.grouper3.SuspendLayout();
            this.grouperColorOrder_2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.grouper4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYInterleaveNum_2)).BeginInit();
            this.dividerPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_ContextMenu
            // 
            this.m_ContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuItemDelete});
            // 
            // m_menuItemDelete
            // 
            this.m_menuItemDelete.Index = 0;
            resources.ApplyResources(this.m_menuItemDelete, "m_menuItemDelete");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_GroupBoxVender);
            this.tabPage1.Controls.Add(this.panelLeft);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            // 
            // m_GroupBoxVender
            // 
            this.m_GroupBoxVender.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxVender.BorderThickness = 1F;
            this.m_GroupBoxVender.Controls.Add(this.listHeadCount);
            this.m_GroupBoxVender.Controls.Add(this.lblHeadCount);
            this.m_GroupBoxVender.Controls.Add(this.m_NumericUpDownVOffset);
            this.m_GroupBoxVender.Controls.Add(this.label_VOffset);
            this.m_GroupBoxVender.Controls.Add(this.numericUpDown_YMaxLen);
            this.m_GroupBoxVender.Controls.Add(this.label_YMaxLen);
            this.m_GroupBoxVender.Controls.Add(this.numServiceStation);
            this.m_GroupBoxVender.Controls.Add(this.label13);
            this.m_GroupBoxVender.Controls.Add(this.numericUpDownW);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelHighSpeed);
            this.m_GroupBoxVender.Controls.Add(this.m_NumericUpDownGroupSpace);
            this.m_GroupBoxVender.Controls.Add(this.m_NumericUpDownColorSpace);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelWidth);
            this.m_GroupBoxVender.Controls.Add(this.m_ComboBoxGroupNumber);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelColorNum);
            this.m_GroupBoxVender.Controls.Add(this.m_ComboBoxColorNumber);
            this.m_GroupBoxVender.Controls.Add(this.m_ComboBoxWidth);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelStripeSpace);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelStripeWidth);
            this.m_GroupBoxVender.Controls.Add(this.m_ComboBoxHeadType);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelHeadType);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelYSpace);
            this.m_GroupBoxVender.Controls.Add(this.m_NumericUpDownYSpace);
            this.m_GroupBoxVender.Controls.Add(this.m_NumericUpDownAngle);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelAngle);
            this.m_GroupBoxVender.Controls.Add(this.m_NumericUpDownWhiteColorNum);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelWhiteInkNum);
            this.m_GroupBoxVender.Controls.Add(this.m_NumericUpDownCoatColorNum);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelCoatColorNum);
            this.m_GroupBoxVender.Controls.Add(this.panel2);
            resources.ApplyResources(this.m_GroupBoxVender, "m_GroupBoxVender");
            style1.Color1 = System.Drawing.Color.LightBlue;
            style1.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxVender.GradientColors = style1;
            this.m_GroupBoxVender.GroupImage = null;
            this.m_GroupBoxVender.Name = "m_GroupBoxVender";
            this.m_GroupBoxVender.PaintGroupBox = false;
            this.m_GroupBoxVender.RoundCorners = 10;
            this.m_GroupBoxVender.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxVender.ShadowControl = false;
            this.m_GroupBoxVender.ShadowThickness = 3;
            this.m_GroupBoxVender.TabStop = false;
            style2.Color1 = System.Drawing.Color.LightBlue;
            style2.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxVender.TitileGradientColors = style2;
            this.m_GroupBoxVender.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxVender.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // listHeadCount
            // 
            this.listHeadCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listHeadCount.FormattingEnabled = true;
            this.listHeadCount.Items.AddRange(new object[] {
            resources.GetString("listHeadCount.Items"),
            resources.GetString("listHeadCount.Items1")});
            resources.ApplyResources(this.listHeadCount, "listHeadCount");
            this.listHeadCount.Name = "listHeadCount";
            // 
            // lblHeadCount
            // 
            this.lblHeadCount.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblHeadCount, "lblHeadCount");
            this.lblHeadCount.Name = "lblHeadCount";
            // 
            // m_NumericUpDownVOffset
            // 
            this.m_NumericUpDownVOffset.DecimalPlaces = 2;
            resources.ApplyResources(this.m_NumericUpDownVOffset, "m_NumericUpDownVOffset");
            this.m_NumericUpDownVOffset.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.m_NumericUpDownVOffset.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownVOffset.Name = "m_NumericUpDownVOffset";
            // 
            // label_VOffset
            // 
            resources.ApplyResources(this.label_VOffset, "label_VOffset");
            this.label_VOffset.BackColor = System.Drawing.Color.Transparent;
            this.label_VOffset.Name = "label_VOffset";
            // 
            // numericUpDown_YMaxLen
            // 
            this.numericUpDown_YMaxLen.DecimalPlaces = 5;
            resources.ApplyResources(this.numericUpDown_YMaxLen, "numericUpDown_YMaxLen");
            this.numericUpDown_YMaxLen.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_YMaxLen.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDown_YMaxLen.Name = "numericUpDown_YMaxLen";
            // 
            // label_YMaxLen
            // 
            resources.ApplyResources(this.label_YMaxLen, "label_YMaxLen");
            this.label_YMaxLen.BackColor = System.Drawing.Color.Transparent;
            this.label_YMaxLen.Name = "label_YMaxLen";
            // 
            // numServiceStation
            // 
            this.numServiceStation.DecimalPlaces = 5;
            resources.ApplyResources(this.numServiceStation, "numServiceStation");
            this.numServiceStation.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numServiceStation.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numServiceStation.Name = "numServiceStation";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Name = "label13";
            // 
            // numericUpDownW
            // 
            this.numericUpDownW.DecimalPlaces = 2;
            resources.ApplyResources(this.numericUpDownW, "numericUpDownW");
            this.numericUpDownW.Name = "numericUpDownW";
            // 
            // m_LabelHighSpeed
            // 
            this.m_LabelHighSpeed.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_LabelHighSpeed, "m_LabelHighSpeed");
            this.m_LabelHighSpeed.Name = "m_LabelHighSpeed";
            // 
            // m_NumericUpDownGroupSpace
            // 
            this.m_NumericUpDownGroupSpace.DecimalPlaces = 5;
            resources.ApplyResources(this.m_NumericUpDownGroupSpace, "m_NumericUpDownGroupSpace");
            this.m_NumericUpDownGroupSpace.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_NumericUpDownGroupSpace.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownGroupSpace.Name = "m_NumericUpDownGroupSpace";
            // 
            // m_NumericUpDownColorSpace
            // 
            this.m_NumericUpDownColorSpace.DecimalPlaces = 5;
            resources.ApplyResources(this.m_NumericUpDownColorSpace, "m_NumericUpDownColorSpace");
            this.m_NumericUpDownColorSpace.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_NumericUpDownColorSpace.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownColorSpace.Name = "m_NumericUpDownColorSpace";
            // 
            // m_LabelWidth
            // 
            this.m_LabelWidth.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_LabelWidth, "m_LabelWidth");
            this.m_LabelWidth.Name = "m_LabelWidth";
            // 
            // m_ComboBoxGroupNumber
            // 
            this.m_ComboBoxGroupNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_ComboBoxGroupNumber.DropDownWidth = 156;
            resources.ApplyResources(this.m_ComboBoxGroupNumber, "m_ComboBoxGroupNumber");
            this.m_ComboBoxGroupNumber.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxGroupNumber.Items"),
            resources.GetString("m_ComboBoxGroupNumber.Items1"),
            resources.GetString("m_ComboBoxGroupNumber.Items2")});
            this.m_ComboBoxGroupNumber.Name = "m_ComboBoxGroupNumber";
            // 
            // m_LabelColorNum
            // 
            this.m_LabelColorNum.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_LabelColorNum, "m_LabelColorNum");
            this.m_LabelColorNum.Name = "m_LabelColorNum";
            // 
            // m_ComboBoxColorNumber
            // 
            this.m_ComboBoxColorNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_ComboBoxColorNumber.DropDownWidth = 156;
            resources.ApplyResources(this.m_ComboBoxColorNumber, "m_ComboBoxColorNumber");
            this.m_ComboBoxColorNumber.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxColorNumber.Items"),
            resources.GetString("m_ComboBoxColorNumber.Items1")});
            this.m_ComboBoxColorNumber.Name = "m_ComboBoxColorNumber";
            this.m_ComboBoxColorNumber.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxColorNumber_SelectedIndexChanged);
            // 
            // m_ComboBoxWidth
            // 
            this.m_ComboBoxWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_ComboBoxWidth.DropDownWidth = 156;
            resources.ApplyResources(this.m_ComboBoxWidth, "m_ComboBoxWidth");
            this.m_ComboBoxWidth.Name = "m_ComboBoxWidth";
            // 
            // m_LabelStripeSpace
            // 
            resources.ApplyResources(this.m_LabelStripeSpace, "m_LabelStripeSpace");
            this.m_LabelStripeSpace.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelStripeSpace.Name = "m_LabelStripeSpace";
            // 
            // m_LabelStripeWidth
            // 
            resources.ApplyResources(this.m_LabelStripeWidth, "m_LabelStripeWidth");
            this.m_LabelStripeWidth.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelStripeWidth.Name = "m_LabelStripeWidth";
            // 
            // m_ComboBoxHeadType
            // 
            this.m_ComboBoxHeadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_ComboBoxHeadType.DropDownWidth = 156;
            resources.ApplyResources(this.m_ComboBoxHeadType, "m_ComboBoxHeadType");
            this.m_ComboBoxHeadType.Name = "m_ComboBoxHeadType";
            this.m_ComboBoxHeadType.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxHeadType_SelectedIndexChanged);
            // 
            // m_LabelHeadType
            // 
            this.m_LabelHeadType.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_LabelHeadType, "m_LabelHeadType");
            this.m_LabelHeadType.Name = "m_LabelHeadType";
            // 
            // m_LabelYSpace
            // 
            resources.ApplyResources(this.m_LabelYSpace, "m_LabelYSpace");
            this.m_LabelYSpace.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelYSpace.Name = "m_LabelYSpace";
            // 
            // m_NumericUpDownYSpace
            // 
            this.m_NumericUpDownYSpace.DecimalPlaces = 5;
            resources.ApplyResources(this.m_NumericUpDownYSpace, "m_NumericUpDownYSpace");
            this.m_NumericUpDownYSpace.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_NumericUpDownYSpace.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownYSpace.Name = "m_NumericUpDownYSpace";
            // 
            // m_NumericUpDownAngle
            // 
            this.m_NumericUpDownAngle.DecimalPlaces = 5;
            resources.ApplyResources(this.m_NumericUpDownAngle, "m_NumericUpDownAngle");
            this.m_NumericUpDownAngle.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_NumericUpDownAngle.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownAngle.Name = "m_NumericUpDownAngle";
            // 
            // m_LabelAngle
            // 
            resources.ApplyResources(this.m_LabelAngle, "m_LabelAngle");
            this.m_LabelAngle.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelAngle.Name = "m_LabelAngle";
            // 
            // m_NumericUpDownWhiteColorNum
            // 
            resources.ApplyResources(this.m_NumericUpDownWhiteColorNum, "m_NumericUpDownWhiteColorNum");
            this.m_NumericUpDownWhiteColorNum.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.m_NumericUpDownWhiteColorNum.Name = "m_NumericUpDownWhiteColorNum";
            this.m_NumericUpDownWhiteColorNum.ValueChanged += new System.EventHandler(this.m_ComboBoxColorNumber_SelectedIndexChanged);
            // 
            // m_LabelWhiteInkNum
            // 
            resources.ApplyResources(this.m_LabelWhiteInkNum, "m_LabelWhiteInkNum");
            this.m_LabelWhiteInkNum.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelWhiteInkNum.Name = "m_LabelWhiteInkNum";
            // 
            // m_NumericUpDownCoatColorNum
            // 
            resources.ApplyResources(this.m_NumericUpDownCoatColorNum, "m_NumericUpDownCoatColorNum");
            this.m_NumericUpDownCoatColorNum.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.m_NumericUpDownCoatColorNum.Name = "m_NumericUpDownCoatColorNum";
            this.m_NumericUpDownCoatColorNum.ValueChanged += new System.EventHandler(this.m_ComboBoxColorNumber_SelectedIndexChanged);
            // 
            // m_LabelCoatColorNum
            // 
            resources.ApplyResources(this.m_LabelCoatColorNum, "m_LabelCoatColorNum");
            this.m_LabelCoatColorNum.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelCoatColorNum.Name = "m_LabelCoatColorNum";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Name = "panel2";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.groupBox8colorarrangement);
            this.panel1.Controls.Add(this.m_CheckBoxSupportLcd);
            this.panel1.Controls.Add(this.m_CheckBoxMirror);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // groupBox8colorarrangement
            // 
            this.groupBox8colorarrangement.BackColor = System.Drawing.Color.Transparent;
            this.groupBox8colorarrangement.Controls.Add(this.checkBox8CCompatibilityMode);
            this.groupBox8colorarrangement.Controls.Add(this.checkBox4ColorMirror);
            this.groupBox8colorarrangement.Controls.Add(this.checkBox_VerY);
            resources.ApplyResources(this.groupBox8colorarrangement, "groupBox8colorarrangement");
            this.groupBox8colorarrangement.Name = "groupBox8colorarrangement";
            this.groupBox8colorarrangement.TabStop = false;
            // 
            // checkBox8CCompatibilityMode
            // 
            resources.ApplyResources(this.checkBox8CCompatibilityMode, "checkBox8CCompatibilityMode");
            this.checkBox8CCompatibilityMode.BackColor = System.Drawing.Color.Transparent;
            this.checkBox8CCompatibilityMode.Name = "checkBox8CCompatibilityMode";
            this.checkBox8CCompatibilityMode.UseVisualStyleBackColor = false;
            // 
            // checkBox4ColorMirror
            // 
            resources.ApplyResources(this.checkBox4ColorMirror, "checkBox4ColorMirror");
            this.checkBox4ColorMirror.BackColor = System.Drawing.Color.Transparent;
            this.checkBox4ColorMirror.Name = "checkBox4ColorMirror";
            this.checkBox4ColorMirror.UseVisualStyleBackColor = false;
            // 
            // checkBox_VerY
            // 
            resources.ApplyResources(this.checkBox_VerY, "checkBox_VerY");
            this.checkBox_VerY.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_VerY.Name = "checkBox_VerY";
            this.checkBox_VerY.UseVisualStyleBackColor = false;
            // 
            // m_CheckBoxSupportLcd
            // 
            resources.ApplyResources(this.m_CheckBoxSupportLcd, "m_CheckBoxSupportLcd");
            this.m_CheckBoxSupportLcd.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxSupportLcd.Name = "m_CheckBoxSupportLcd";
            this.m_CheckBoxSupportLcd.UseVisualStyleBackColor = false;
            this.m_CheckBoxSupportLcd.CheckedChanged += new System.EventHandler(this.m_CheckBoxSupportLcd_CheckedChanged);
            // 
            // m_CheckBoxMirror
            // 
            resources.ApplyResources(this.m_CheckBoxMirror, "m_CheckBoxMirror");
            this.m_CheckBoxMirror.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxMirror.Name = "m_CheckBoxMirror";
            this.m_CheckBoxMirror.UseVisualStyleBackColor = false;
            this.m_CheckBoxMirror.CheckedChanged += new System.EventHandler(this.m_CheckBoxMirror_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_CheckBoxXaar382Mode);
            this.panel3.Controls.Add(this.checkBoxZMeasur);
            this.panel3.Controls.Add(this.groupBoxDualBank);
            this.panel3.Controls.Add(this.m_CheckBoxOneHeadDivider);
            this.panel3.Controls.Add(this.m_CheckBoxIsHeadLeft);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // m_CheckBoxXaar382Mode
            // 
            resources.ApplyResources(this.m_CheckBoxXaar382Mode, "m_CheckBoxXaar382Mode");
            this.m_CheckBoxXaar382Mode.Name = "m_CheckBoxXaar382Mode";
            this.m_CheckBoxXaar382Mode.UseVisualStyleBackColor = true;
            // 
            // checkBoxZMeasur
            // 
            resources.ApplyResources(this.checkBoxZMeasur, "checkBoxZMeasur");
            this.checkBoxZMeasur.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxZMeasur.Name = "checkBoxZMeasur";
            this.checkBoxZMeasur.UseVisualStyleBackColor = false;
            // 
            // groupBoxDualBank
            // 
            this.groupBoxDualBank.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxDualBank.Controls.Add(this.radioButtonSingleBank);
            this.groupBoxDualBank.Controls.Add(this.radioButtonDualBank);
            resources.ApplyResources(this.groupBoxDualBank, "groupBoxDualBank");
            this.groupBoxDualBank.Name = "groupBoxDualBank";
            this.groupBoxDualBank.TabStop = false;
            // 
            // radioButtonSingleBank
            // 
            resources.ApplyResources(this.radioButtonSingleBank, "radioButtonSingleBank");
            this.radioButtonSingleBank.Name = "radioButtonSingleBank";
            // 
            // radioButtonDualBank
            // 
            resources.ApplyResources(this.radioButtonDualBank, "radioButtonDualBank");
            this.radioButtonDualBank.Name = "radioButtonDualBank";
            // 
            // m_CheckBoxOneHeadDivider
            // 
            resources.ApplyResources(this.m_CheckBoxOneHeadDivider, "m_CheckBoxOneHeadDivider");
            this.m_CheckBoxOneHeadDivider.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxOneHeadDivider.Name = "m_CheckBoxOneHeadDivider";
            this.m_CheckBoxOneHeadDivider.UseVisualStyleBackColor = false;
            this.m_CheckBoxOneHeadDivider.CheckedChanged += new System.EventHandler(this.m_CheckBoxOneHeadDivider_CheckedChanged);
            // 
            // m_CheckBoxIsHeadLeft
            // 
            resources.ApplyResources(this.m_CheckBoxIsHeadLeft, "m_CheckBoxIsHeadLeft");
            this.m_CheckBoxIsHeadLeft.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxIsHeadLeft.Name = "m_CheckBoxIsHeadLeft";
            this.m_CheckBoxIsHeadLeft.UseVisualStyleBackColor = false;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.checkBoxSupportZendPointSensor);
            this.panelLeft.Controls.Add(this.buttonHeadMix);
            this.panelLeft.Controls.Add(this.checkBoxStaggered);
            this.panelLeft.Controls.Add(this.checkBoxMediaSensor);
            this.panelLeft.Controls.Add(this.checkBoxWhiteInkRight);
            this.panelLeft.Controls.Add(this.m_GroupBoxInk);
            this.panelLeft.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.panelLeft, "panelLeft");
            this.panelLeft.Name = "panelLeft";
            // 
            // checkBoxSupportZendPointSensor
            // 
            resources.ApplyResources(this.checkBoxSupportZendPointSensor, "checkBoxSupportZendPointSensor");
            this.checkBoxSupportZendPointSensor.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxSupportZendPointSensor.Name = "checkBoxSupportZendPointSensor";
            this.checkBoxSupportZendPointSensor.UseVisualStyleBackColor = false;
            // 
            // buttonHeadMix
            // 
            resources.ApplyResources(this.buttonHeadMix, "buttonHeadMix");
            this.buttonHeadMix.Name = "buttonHeadMix";
            this.buttonHeadMix.UseVisualStyleBackColor = true;
            this.buttonHeadMix.Click += new System.EventHandler(this.buttonHeadMix_Click);
            // 
            // checkBoxStaggered
            // 
            resources.ApplyResources(this.checkBoxStaggered, "checkBoxStaggered");
            this.checkBoxStaggered.Name = "checkBoxStaggered";
            this.checkBoxStaggered.UseVisualStyleBackColor = true;
            // 
            // checkBoxMediaSensor
            // 
            resources.ApplyResources(this.checkBoxMediaSensor, "checkBoxMediaSensor");
            this.checkBoxMediaSensor.Name = "checkBoxMediaSensor";
            this.checkBoxMediaSensor.UseVisualStyleBackColor = true;
            // 
            // checkBoxWhiteInkRight
            // 
            resources.ApplyResources(this.checkBoxWhiteInkRight, "checkBoxWhiteInkRight");
            this.checkBoxWhiteInkRight.Name = "checkBoxWhiteInkRight";
            this.checkBoxWhiteInkRight.UseVisualStyleBackColor = true;
            // 
            // m_GroupBoxInk
            // 
            this.m_GroupBoxInk.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxInk.BorderThickness = 1F;
            this.m_GroupBoxInk.Controls.Add(this.LableInkType);
            this.m_GroupBoxInk.Controls.Add(this.label2);
            this.m_GroupBoxInk.Controls.Add(this.m_comboBoxInkType);
            this.m_GroupBoxInk.Controls.Add(this.m_comboBoxJetSpeed);
            resources.ApplyResources(this.m_GroupBoxInk, "m_GroupBoxInk");
            this.m_GroupBoxInk.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            style3.Color1 = System.Drawing.Color.LightBlue;
            style3.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxInk.GradientColors = style3;
            this.m_GroupBoxInk.GroupImage = null;
            this.m_GroupBoxInk.Name = "m_GroupBoxInk";
            this.m_GroupBoxInk.PaintGroupBox = false;
            this.m_GroupBoxInk.RoundCorners = 10;
            this.m_GroupBoxInk.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxInk.ShadowControl = false;
            this.m_GroupBoxInk.ShadowThickness = 3;
            this.m_GroupBoxInk.TabStop = false;
            style4.Color1 = System.Drawing.Color.LightBlue;
            style4.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxInk.TitileGradientColors = style4;
            this.m_GroupBoxInk.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxInk.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // LableInkType
            // 
            this.LableInkType.BackColor = System.Drawing.Color.Transparent;
            this.LableInkType.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.LableInkType, "LableInkType");
            this.LableInkType.Name = "LableInkType";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // m_comboBoxInkType
            // 
            this.m_comboBoxInkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_comboBoxInkType, "m_comboBoxInkType");
            this.m_comboBoxInkType.Items.AddRange(new object[] {
            resources.GetString("m_comboBoxInkType.Items"),
            resources.GetString("m_comboBoxInkType.Items1"),
            resources.GetString("m_comboBoxInkType.Items2"),
            resources.GetString("m_comboBoxInkType.Items3"),
            resources.GetString("m_comboBoxInkType.Items4"),
            resources.GetString("m_comboBoxInkType.Items5")});
            this.m_comboBoxInkType.Name = "m_comboBoxInkType";
            // 
            // m_comboBoxJetSpeed
            // 
            this.m_comboBoxJetSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_comboBoxJetSpeed, "m_comboBoxJetSpeed");
            this.m_comboBoxJetSpeed.Items.AddRange(new object[] {
            resources.GetString("m_comboBoxJetSpeed.Items"),
            resources.GetString("m_comboBoxJetSpeed.Items1"),
            resources.GetString("m_comboBoxJetSpeed.Items2")});
            this.m_comboBoxJetSpeed.Name = "m_comboBoxJetSpeed";
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.groupBox1.BorderThickness = 1F;
            this.groupBox1.Controls.Add(this.m_RadioButtonServoEncoder);
            this.groupBox1.Controls.Add(this.m_RadioButtonEncoder);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            style5.Color1 = System.Drawing.Color.LightBlue;
            style5.Color2 = System.Drawing.Color.SteelBlue;
            this.groupBox1.GradientColors = style5;
            this.groupBox1.GroupImage = null;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.PaintGroupBox = false;
            this.groupBox1.RoundCorners = 10;
            this.groupBox1.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox1.ShadowControl = false;
            this.groupBox1.ShadowThickness = 3;
            this.groupBox1.TabStop = false;
            style6.Color1 = System.Drawing.Color.LightBlue;
            style6.Color2 = System.Drawing.Color.SteelBlue;
            this.groupBox1.TitileGradientColors = style6;
            this.groupBox1.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.groupBox1.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // m_RadioButtonServoEncoder
            // 
            this.m_RadioButtonServoEncoder.BackColor = System.Drawing.Color.Transparent;
            this.m_RadioButtonServoEncoder.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.m_RadioButtonServoEncoder, "m_RadioButtonServoEncoder");
            this.m_RadioButtonServoEncoder.Name = "m_RadioButtonServoEncoder";
            this.m_RadioButtonServoEncoder.UseVisualStyleBackColor = false;
            // 
            // m_RadioButtonEncoder
            // 
            this.m_RadioButtonEncoder.BackColor = System.Drawing.Color.Transparent;
            this.m_RadioButtonEncoder.Checked = true;
            this.m_RadioButtonEncoder.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.m_RadioButtonEncoder, "m_RadioButtonEncoder");
            this.m_RadioButtonEncoder.Name = "m_RadioButtonEncoder";
            this.m_RadioButtonEncoder.TabStop = true;
            this.m_RadioButtonEncoder.UseVisualStyleBackColor = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grouperEpson);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            // 
            // grouperEpson
            // 
            this.grouperEpson.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperEpson.BorderThickness = 1F;
            this.grouperEpson.Controls.Add(this.checkBoxMotorDebug);
            this.grouperEpson.Controls.Add(this.grouperSSystem);
            this.grouperEpson.Controls.Add(this.grouper2);
            this.grouperEpson.Controls.Add(this.grouperRipColorOrder);
            this.grouperEpson.Controls.Add(this.grouperPrintColorOrder);
            resources.ApplyResources(this.grouperEpson, "grouperEpson");
            style15.Color1 = System.Drawing.Color.LightBlue;
            style15.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperEpson.GradientColors = style15;
            this.grouperEpson.GroupImage = null;
            this.grouperEpson.Name = "grouperEpson";
            this.grouperEpson.PaintGroupBox = false;
            this.grouperEpson.RoundCorners = 10;
            this.grouperEpson.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperEpson.ShadowControl = false;
            this.grouperEpson.ShadowThickness = 3;
            this.grouperEpson.TabStop = false;
            style16.Color1 = System.Drawing.Color.LightBlue;
            style16.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperEpson.TitileGradientColors = style16;
            this.grouperEpson.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouperEpson.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // checkBoxMotorDebug
            // 
            resources.ApplyResources(this.checkBoxMotorDebug, "checkBoxMotorDebug");
            this.checkBoxMotorDebug.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxMotorDebug.Name = "checkBoxMotorDebug";
            this.checkBoxMotorDebug.UseVisualStyleBackColor = false;
            // 
            // grouperSSystem
            // 
            this.grouperSSystem.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperSSystem.BorderThickness = 1F;
            this.grouperSSystem.Controls.Add(this.numericUpDownHBNum);
            this.grouperSSystem.Controls.Add(this.label16);
            this.grouperSSystem.Controls.Add(this.panelCB1);
            this.grouperSSystem.Controls.Add(this.panelCB2);
            this.grouperSSystem.Controls.Add(this.comboBoxTopologyMode);
            this.grouperSSystem.Controls.Add(this.label14);
            resources.ApplyResources(this.grouperSSystem, "grouperSSystem");
            style7.Color1 = System.Drawing.Color.LightBlue;
            style7.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperSSystem.GradientColors = style7;
            this.grouperSSystem.GroupImage = null;
            this.grouperSSystem.Name = "grouperSSystem";
            this.grouperSSystem.PaintGroupBox = false;
            this.grouperSSystem.RoundCorners = 5;
            this.grouperSSystem.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperSSystem.ShadowControl = false;
            this.grouperSSystem.ShadowThickness = 3;
            this.grouperSSystem.TabStop = false;
            style8.Color1 = System.Drawing.Color.LightBlue;
            style8.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperSSystem.TitileGradientColors = style8;
            this.grouperSSystem.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.XPStyle;
            this.grouperSSystem.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // numericUpDownHBNum
            // 
            resources.ApplyResources(this.numericUpDownHBNum, "numericUpDownHBNum");
            this.numericUpDownHBNum.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownHBNum.Name = "numericUpDownHBNum";
            this.numericUpDownHBNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Name = "label16";
            // 
            // panelCB1
            // 
            this.panelCB1.BackColor = System.Drawing.Color.Transparent;
            this.panelCB1.Controls.Add(this.checkBoxPort14);
            this.panelCB1.Controls.Add(this.checkBoxPort13);
            this.panelCB1.Controls.Add(this.checkBoxPort12);
            this.panelCB1.Controls.Add(this.checkBoxPort11);
            this.panelCB1.Controls.Add(this.label18);
            resources.ApplyResources(this.panelCB1, "panelCB1");
            this.panelCB1.Name = "panelCB1";
            // 
            // checkBoxPort14
            // 
            resources.ApplyResources(this.checkBoxPort14, "checkBoxPort14");
            this.checkBoxPort14.Name = "checkBoxPort14";
            this.checkBoxPort14.UseVisualStyleBackColor = true;
            // 
            // checkBoxPort13
            // 
            resources.ApplyResources(this.checkBoxPort13, "checkBoxPort13");
            this.checkBoxPort13.Name = "checkBoxPort13";
            this.checkBoxPort13.UseVisualStyleBackColor = true;
            // 
            // checkBoxPort12
            // 
            resources.ApplyResources(this.checkBoxPort12, "checkBoxPort12");
            this.checkBoxPort12.Name = "checkBoxPort12";
            this.checkBoxPort12.UseVisualStyleBackColor = true;
            // 
            // checkBoxPort11
            // 
            resources.ApplyResources(this.checkBoxPort11, "checkBoxPort11");
            this.checkBoxPort11.Name = "checkBoxPort11";
            this.checkBoxPort11.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Name = "label18";
            // 
            // panelCB2
            // 
            this.panelCB2.BackColor = System.Drawing.Color.Transparent;
            this.panelCB2.Controls.Add(this.checkBoxPort24);
            this.panelCB2.Controls.Add(this.checkBoxPort23);
            this.panelCB2.Controls.Add(this.checkBoxPort22);
            this.panelCB2.Controls.Add(this.checkBoxPort21);
            this.panelCB2.Controls.Add(this.label19);
            resources.ApplyResources(this.panelCB2, "panelCB2");
            this.panelCB2.Name = "panelCB2";
            // 
            // checkBoxPort24
            // 
            resources.ApplyResources(this.checkBoxPort24, "checkBoxPort24");
            this.checkBoxPort24.Name = "checkBoxPort24";
            this.checkBoxPort24.UseVisualStyleBackColor = true;
            // 
            // checkBoxPort23
            // 
            resources.ApplyResources(this.checkBoxPort23, "checkBoxPort23");
            this.checkBoxPort23.Name = "checkBoxPort23";
            this.checkBoxPort23.UseVisualStyleBackColor = true;
            // 
            // checkBoxPort22
            // 
            resources.ApplyResources(this.checkBoxPort22, "checkBoxPort22");
            this.checkBoxPort22.Name = "checkBoxPort22";
            this.checkBoxPort22.UseVisualStyleBackColor = true;
            // 
            // checkBoxPort21
            // 
            resources.ApplyResources(this.checkBoxPort21, "checkBoxPort21");
            this.checkBoxPort21.Name = "checkBoxPort21";
            this.checkBoxPort21.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Name = "label19";
            // 
            // comboBoxTopologyMode
            // 
            this.comboBoxTopologyMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTopologyMode.FormattingEnabled = true;
            this.comboBoxTopologyMode.Items.AddRange(new object[] {
            resources.GetString("comboBoxTopologyMode.Items"),
            resources.GetString("comboBoxTopologyMode.Items1"),
            resources.GetString("comboBoxTopologyMode.Items2")});
            resources.ApplyResources(this.comboBoxTopologyMode, "comboBoxTopologyMode");
            this.comboBoxTopologyMode.Name = "comboBoxTopologyMode";
            this.comboBoxTopologyMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxTopologyMode_SelectedIndexChanged);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Name = "label14";
            // 
            // grouper2
            // 
            this.grouper2.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouper2.BorderThickness = 1F;
            this.grouper2.Controls.Add(this.numFlatDistanceY);
            this.grouper2.Controls.Add(this.label_FlatDistanceY);
            this.grouper2.Controls.Add(this.numzMaxRoute);
            this.grouper2.Controls.Add(this.labelzMaxRoute);
            this.grouper2.Controls.Add(this.numSpeedUpDistanceR);
            this.grouper2.Controls.Add(this.labelAccDistanceR);
            this.grouper2.Controls.Add(this.lblDefaultZ);
            this.grouper2.Controls.Add(this.numDefaultZ);
            this.grouper2.Controls.Add(this.m_NumericUpDown_YEncoderDPI);
            this.grouper2.Controls.Add(this.m_Label_YEncoderDPI);
            this.grouper2.Controls.Add(this.m_NumericUpDown_MaxOffsetPos);
            this.grouper2.Controls.Add(this.m_Label_MaxOffsetPos);
            this.grouper2.Controls.Add(this.numFlatDistance);
            this.grouper2.Controls.Add(this.label_FlatDistance);
            this.grouper2.Controls.Add(this.numSpeedUpDistanceL);
            this.grouper2.Controls.Add(this.numericUpDown_PrintSence);
            this.grouper2.Controls.Add(this.combobox_RasterSence);
            this.grouper2.Controls.Add(this.label12);
            this.grouper2.Controls.Add(this.labelAccDistanceL);
            this.grouper2.Controls.Add(this.label10);
            resources.ApplyResources(this.grouper2, "grouper2");
            style9.Color1 = System.Drawing.Color.LightBlue;
            style9.Color2 = System.Drawing.Color.SteelBlue;
            this.grouper2.GradientColors = style9;
            this.grouper2.GroupImage = null;
            this.grouper2.Name = "grouper2";
            this.grouper2.PaintGroupBox = false;
            this.grouper2.RoundCorners = 10;
            this.grouper2.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper2.ShadowControl = false;
            this.grouper2.ShadowThickness = 3;
            this.grouper2.TabStop = false;
            style10.Color1 = System.Drawing.Color.LightBlue;
            style10.Color2 = System.Drawing.Color.SteelBlue;
            this.grouper2.TitileGradientColors = style10;
            this.grouper2.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.XPStyle;
            this.grouper2.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            this.grouper2.Enter += new System.EventHandler(this.grouper2_Enter);
            // 
            // numFlatDistanceY
            // 
            resources.ApplyResources(this.numFlatDistanceY, "numFlatDistanceY");
            this.numFlatDistanceY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numFlatDistanceY.Name = "numFlatDistanceY";
            // 
            // label_FlatDistanceY
            // 
            resources.ApplyResources(this.label_FlatDistanceY, "label_FlatDistanceY");
            this.label_FlatDistanceY.BackColor = System.Drawing.Color.Transparent;
            this.label_FlatDistanceY.Name = "label_FlatDistanceY";
            // 
            // numzMaxRoute
            // 
            this.numzMaxRoute.DecimalPlaces = 1;
            resources.ApplyResources(this.numzMaxRoute, "numzMaxRoute");
            this.numzMaxRoute.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numzMaxRoute.Name = "numzMaxRoute";
            // 
            // labelzMaxRoute
            // 
            resources.ApplyResources(this.labelzMaxRoute, "labelzMaxRoute");
            this.labelzMaxRoute.BackColor = System.Drawing.Color.Transparent;
            this.labelzMaxRoute.Name = "labelzMaxRoute";
            // 
            // numSpeedUpDistanceR
            // 
            resources.ApplyResources(this.numSpeedUpDistanceR, "numSpeedUpDistanceR");
            this.numSpeedUpDistanceR.Name = "numSpeedUpDistanceR";
            // 
            // labelAccDistanceR
            // 
            resources.ApplyResources(this.labelAccDistanceR, "labelAccDistanceR");
            this.labelAccDistanceR.BackColor = System.Drawing.Color.Transparent;
            this.labelAccDistanceR.Name = "labelAccDistanceR";
            // 
            // lblDefaultZ
            // 
            resources.ApplyResources(this.lblDefaultZ, "lblDefaultZ");
            this.lblDefaultZ.BackColor = System.Drawing.Color.Transparent;
            this.lblDefaultZ.Name = "lblDefaultZ";
            // 
            // numDefaultZ
            // 
            resources.ApplyResources(this.numDefaultZ, "numDefaultZ");
            this.numDefaultZ.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numDefaultZ.Name = "numDefaultZ";
            // 
            // m_NumericUpDown_YEncoderDPI
            // 
            resources.ApplyResources(this.m_NumericUpDown_YEncoderDPI, "m_NumericUpDown_YEncoderDPI");
            this.m_NumericUpDown_YEncoderDPI.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_NumericUpDown_YEncoderDPI.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.m_NumericUpDown_YEncoderDPI.Name = "m_NumericUpDown_YEncoderDPI";
            this.m_NumericUpDown_YEncoderDPI.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            // 
            // m_Label_YEncoderDPI
            // 
            resources.ApplyResources(this.m_Label_YEncoderDPI, "m_Label_YEncoderDPI");
            this.m_Label_YEncoderDPI.BackColor = System.Drawing.Color.Transparent;
            this.m_Label_YEncoderDPI.Name = "m_Label_YEncoderDPI";
            // 
            // m_NumericUpDown_MaxOffsetPos
            // 
            this.m_NumericUpDown_MaxOffsetPos.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDown_MaxOffsetPos, "m_NumericUpDown_MaxOffsetPos");
            this.m_NumericUpDown_MaxOffsetPos.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.m_NumericUpDown_MaxOffsetPos.Name = "m_NumericUpDown_MaxOffsetPos";
            // 
            // m_Label_MaxOffsetPos
            // 
            resources.ApplyResources(this.m_Label_MaxOffsetPos, "m_Label_MaxOffsetPos");
            this.m_Label_MaxOffsetPos.BackColor = System.Drawing.Color.Transparent;
            this.m_Label_MaxOffsetPos.Name = "m_Label_MaxOffsetPos";
            // 
            // numFlatDistance
            // 
            resources.ApplyResources(this.numFlatDistance, "numFlatDistance");
            this.numFlatDistance.Name = "numFlatDistance";
            // 
            // label_FlatDistance
            // 
            resources.ApplyResources(this.label_FlatDistance, "label_FlatDistance");
            this.label_FlatDistance.BackColor = System.Drawing.Color.Transparent;
            this.label_FlatDistance.Name = "label_FlatDistance";
            // 
            // numSpeedUpDistanceL
            // 
            resources.ApplyResources(this.numSpeedUpDistanceL, "numSpeedUpDistanceL");
            this.numSpeedUpDistanceL.Name = "numSpeedUpDistanceL";
            // 
            // numericUpDown_PrintSence
            // 
            resources.ApplyResources(this.numericUpDown_PrintSence, "numericUpDown_PrintSence");
            this.numericUpDown_PrintSence.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numericUpDown_PrintSence.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_PrintSence.Name = "numericUpDown_PrintSence";
            this.numericUpDown_PrintSence.Value = new decimal(new int[] {
            720,
            0,
            0,
            0});
            // 
            // combobox_RasterSence
            // 
            this.combobox_RasterSence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobox_RasterSence.FormattingEnabled = true;
            this.combobox_RasterSence.Items.AddRange(new object[] {
            resources.GetString("combobox_RasterSence.Items"),
            resources.GetString("combobox_RasterSence.Items1"),
            resources.GetString("combobox_RasterSence.Items2"),
            resources.GetString("combobox_RasterSence.Items3"),
            resources.GetString("combobox_RasterSence.Items4"),
            resources.GetString("combobox_RasterSence.Items5"),
            resources.GetString("combobox_RasterSence.Items6"),
            resources.GetString("combobox_RasterSence.Items7"),
            resources.GetString("combobox_RasterSence.Items8"),
            resources.GetString("combobox_RasterSence.Items9"),
            resources.GetString("combobox_RasterSence.Items10"),
            resources.GetString("combobox_RasterSence.Items11"),
            resources.GetString("combobox_RasterSence.Items12"),
            resources.GetString("combobox_RasterSence.Items13"),
            resources.GetString("combobox_RasterSence.Items14")});
            resources.ApplyResources(this.combobox_RasterSence, "combobox_RasterSence");
            this.combobox_RasterSence.Name = "combobox_RasterSence";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Name = "label12";
            // 
            // labelAccDistanceL
            // 
            resources.ApplyResources(this.labelAccDistanceL, "labelAccDistanceL");
            this.labelAccDistanceL.BackColor = System.Drawing.Color.Transparent;
            this.labelAccDistanceL.Name = "labelAccDistanceL";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Name = "label10";
            // 
            // grouperRipColorOrder
            // 
            this.grouperRipColorOrder.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperRipColorOrder.BorderThickness = 1F;
            this.grouperRipColorOrder.Controls.Add(this.comboxRipcolor);
            resources.ApplyResources(this.grouperRipColorOrder, "grouperRipColorOrder");
            style11.Color1 = System.Drawing.Color.LightBlue;
            style11.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperRipColorOrder.GradientColors = style11;
            this.grouperRipColorOrder.GroupImage = null;
            this.grouperRipColorOrder.Name = "grouperRipColorOrder";
            this.grouperRipColorOrder.PaintGroupBox = false;
            this.grouperRipColorOrder.RoundCorners = 10;
            this.grouperRipColorOrder.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperRipColorOrder.ShadowControl = false;
            this.grouperRipColorOrder.ShadowThickness = 3;
            this.grouperRipColorOrder.TabStop = false;
            style12.Color1 = System.Drawing.Color.LightBlue;
            style12.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperRipColorOrder.TitileGradientColors = style12;
            this.grouperRipColorOrder.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.XPStyle;
            this.grouperRipColorOrder.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // comboxRipcolor
            // 
            resources.ApplyResources(this.comboxRipcolor, "comboxRipcolor");
            this.comboxRipcolor.Name = "comboxRipcolor";
            // 
            // grouperPrintColorOrder
            // 
            this.grouperPrintColorOrder.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperPrintColorOrder.BorderThickness = 1F;
            this.grouperPrintColorOrder.Controls.Add(this.comboBoxColorOderSample);
            resources.ApplyResources(this.grouperPrintColorOrder, "grouperPrintColorOrder");
            style13.Color1 = System.Drawing.Color.LightBlue;
            style13.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperPrintColorOrder.GradientColors = style13;
            this.grouperPrintColorOrder.GroupImage = null;
            this.grouperPrintColorOrder.Name = "grouperPrintColorOrder";
            this.grouperPrintColorOrder.PaintGroupBox = false;
            this.grouperPrintColorOrder.RoundCorners = 10;
            this.grouperPrintColorOrder.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperPrintColorOrder.ShadowControl = false;
            this.grouperPrintColorOrder.ShadowThickness = 3;
            this.grouperPrintColorOrder.TabStop = false;
            style14.Color1 = System.Drawing.Color.LightBlue;
            style14.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperPrintColorOrder.TitileGradientColors = style14;
            this.grouperPrintColorOrder.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.XPStyle;
            this.grouperPrintColorOrder.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // comboBoxColorOderSample
            // 
            resources.ApplyResources(this.comboBoxColorOderSample, "comboBoxColorOderSample");
            this.comboBoxColorOderSample.Name = "comboBoxColorOderSample";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.grouper3);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            // 
            // grouper3
            // 
            this.grouper3.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouper3.BorderThickness = 1F;
            this.grouper3.Controls.Add(this.grouperColorOrder_2);
            this.grouper3.Controls.Add(this.panel4);
            resources.ApplyResources(this.grouper3, "grouper3");
            style21.Color1 = System.Drawing.Color.LightBlue;
            style21.Color2 = System.Drawing.Color.SteelBlue;
            this.grouper3.GradientColors = style21;
            this.grouper3.GroupImage = null;
            this.grouper3.Name = "grouper3";
            this.grouper3.PaintGroupBox = false;
            this.grouper3.RoundCorners = 10;
            this.grouper3.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper3.ShadowControl = false;
            this.grouper3.ShadowThickness = 3;
            this.grouper3.TabStop = false;
            style22.Color1 = System.Drawing.Color.LightBlue;
            style22.Color2 = System.Drawing.Color.SteelBlue;
            this.grouper3.TitileGradientColors = style22;
            this.grouper3.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouper3.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // grouperColorOrder_2
            // 
            this.grouperColorOrder_2.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperColorOrder_2.BorderThickness = 1F;
            this.grouperColorOrder_2.Controls.Add(this.comboBoxColorOderSample_2);
            resources.ApplyResources(this.grouperColorOrder_2, "grouperColorOrder_2");
            style17.Color1 = System.Drawing.Color.LightBlue;
            style17.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperColorOrder_2.GradientColors = style17;
            this.grouperColorOrder_2.GroupImage = null;
            this.grouperColorOrder_2.Name = "grouperColorOrder_2";
            this.grouperColorOrder_2.PaintGroupBox = false;
            this.grouperColorOrder_2.RoundCorners = 10;
            this.grouperColorOrder_2.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperColorOrder_2.ShadowControl = false;
            this.grouperColorOrder_2.ShadowThickness = 3;
            this.grouperColorOrder_2.TabStop = false;
            style18.Color1 = System.Drawing.Color.LightBlue;
            style18.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperColorOrder_2.TitileGradientColors = style18;
            this.grouperColorOrder_2.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.XPStyle;
            this.grouperColorOrder_2.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // comboBoxColorOderSample_2
            // 
            resources.ApplyResources(this.comboBoxColorOderSample_2, "comboBoxColorOderSample_2");
            this.comboBoxColorOderSample_2.Name = "comboBoxColorOderSample_2";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.grouper4);
            this.panel4.Controls.Add(this.label4_2);
            this.panel4.Controls.Add(this.comboBoxXEncoderDPI_2);
            this.panel4.Controls.Add(this.label5_2);
            this.panel4.Controls.Add(this.label6_2);
            this.panel4.Controls.Add(this.label3_2);
            this.panel4.Controls.Add(this.numericUpDownYInterleaveNum_2);
            this.panel4.Controls.Add(this.comboBoxMaxGroupNumber_2);
            this.panel4.Controls.Add(this.checkBoxWeakSovent_2);
            this.panel4.Controls.Add(this.txtPrinterName_2);
            this.panel4.Controls.Add(this.label8_2);
            this.panel4.Controls.Add(this.txtManufacturerName_2);
            this.panel4.Controls.Add(this.checkBoxHeadDir_2);
            this.panel4.Controls.Add(this.label9_2);
            this.panel4.Controls.Add(this.m_ComboBoxBit2Mode_2);
            this.panel4.Controls.Add(this.labelSpeed_2);
            this.panel4.Controls.Add(this.m_ComboBoxSpeed_2);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // grouper4
            // 
            this.grouper4.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouper4.BorderThickness = 1F;
            this.grouper4.Controls.Add(this.cbxThreeColor_2);
            this.grouper4.Controls.Add(this.cbxWrapHead_2);
            this.grouper4.Controls.Add(this.comboBoxWhiteVarnishLayout_2);
            this.grouper4.Controls.Add(this.labelWhiteVarnishLayout_2);
            this.grouper4.Controls.Add(this.checkBoxWrapAroundOnX_2);
            this.grouper4.Controls.Add(this.label7_2);
            this.grouper4.Controls.Add(this.comboBoxSymmetricType_2);
            this.grouper4.Controls.Add(this.label1_2);
            this.grouper4.Controls.Add(this.comboBoxLayoutType_2);
            this.grouper4.Controls.Add(this.checkBoxSymmetricColorOder_2);
            resources.ApplyResources(this.grouper4, "grouper4");
            style19.Color1 = System.Drawing.Color.LightBlue;
            style19.Color2 = System.Drawing.Color.SteelBlue;
            this.grouper4.GradientColors = style19;
            this.grouper4.GroupImage = null;
            this.grouper4.Name = "grouper4";
            this.grouper4.PaintGroupBox = false;
            this.grouper4.RoundCorners = 10;
            this.grouper4.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper4.ShadowControl = false;
            this.grouper4.ShadowThickness = 3;
            this.grouper4.TabStop = false;
            style20.Color1 = System.Drawing.Color.LightBlue;
            style20.Color2 = System.Drawing.Color.SteelBlue;
            this.grouper4.TitileGradientColors = style20;
            this.grouper4.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.XPStyle;
            this.grouper4.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // cbxThreeColor_2
            // 
            resources.ApplyResources(this.cbxThreeColor_2, "cbxThreeColor_2");
            this.cbxThreeColor_2.Name = "cbxThreeColor_2";
            this.cbxThreeColor_2.UseVisualStyleBackColor = true;
            // 
            // cbxWrapHead_2
            // 
            this.cbxWrapHead_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxWrapHead_2.FormattingEnabled = true;
            this.cbxWrapHead_2.Items.AddRange(new object[] {
            resources.GetString("cbxWrapHead_2.Items"),
            resources.GetString("cbxWrapHead_2.Items1")});
            resources.ApplyResources(this.cbxWrapHead_2, "cbxWrapHead_2");
            this.cbxWrapHead_2.Name = "cbxWrapHead_2";
            // 
            // comboBoxWhiteVarnishLayout_2
            // 
            this.comboBoxWhiteVarnishLayout_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWhiteVarnishLayout_2.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxWhiteVarnishLayout_2, "comboBoxWhiteVarnishLayout_2");
            this.comboBoxWhiteVarnishLayout_2.Name = "comboBoxWhiteVarnishLayout_2";
            // 
            // labelWhiteVarnishLayout_2
            // 
            resources.ApplyResources(this.labelWhiteVarnishLayout_2, "labelWhiteVarnishLayout_2");
            this.labelWhiteVarnishLayout_2.Name = "labelWhiteVarnishLayout_2";
            // 
            // checkBoxWrapAroundOnX_2
            // 
            resources.ApplyResources(this.checkBoxWrapAroundOnX_2, "checkBoxWrapAroundOnX_2");
            this.checkBoxWrapAroundOnX_2.Name = "checkBoxWrapAroundOnX_2";
            // 
            // label7_2
            // 
            this.label7_2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label7_2, "label7_2");
            this.label7_2.Name = "label7_2";
            // 
            // comboBoxSymmetricType_2
            // 
            this.comboBoxSymmetricType_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSymmetricType_2.DropDownWidth = 156;
            resources.ApplyResources(this.comboBoxSymmetricType_2, "comboBoxSymmetricType_2");
            this.comboBoxSymmetricType_2.Items.AddRange(new object[] {
            resources.GetString("comboBoxSymmetricType_2.Items")});
            this.comboBoxSymmetricType_2.Name = "comboBoxSymmetricType_2";
            // 
            // label1_2
            // 
            this.label1_2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label1_2, "label1_2");
            this.label1_2.Name = "label1_2";
            // 
            // comboBoxLayoutType_2
            // 
            this.comboBoxLayoutType_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLayoutType_2.DropDownWidth = 156;
            resources.ApplyResources(this.comboBoxLayoutType_2, "comboBoxLayoutType_2");
            this.comboBoxLayoutType_2.Items.AddRange(new object[] {
            resources.GetString("comboBoxLayoutType_2.Items")});
            this.comboBoxLayoutType_2.Name = "comboBoxLayoutType_2";
            // 
            // checkBoxSymmetricColorOder_2
            // 
            resources.ApplyResources(this.checkBoxSymmetricColorOder_2, "checkBoxSymmetricColorOder_2");
            this.checkBoxSymmetricColorOder_2.Name = "checkBoxSymmetricColorOder_2";
            // 
            // label4_2
            // 
            this.label4_2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label4_2, "label4_2");
            this.label4_2.Name = "label4_2";
            // 
            // comboBoxXEncoderDPI_2
            // 
            this.comboBoxXEncoderDPI_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxXEncoderDPI_2.DropDownWidth = 156;
            resources.ApplyResources(this.comboBoxXEncoderDPI_2, "comboBoxXEncoderDPI_2");
            this.comboBoxXEncoderDPI_2.Items.AddRange(new object[] {
            resources.GetString("comboBoxXEncoderDPI_2.Items"),
            resources.GetString("comboBoxXEncoderDPI_2.Items1"),
            resources.GetString("comboBoxXEncoderDPI_2.Items2"),
            resources.GetString("comboBoxXEncoderDPI_2.Items3"),
            resources.GetString("comboBoxXEncoderDPI_2.Items4"),
            resources.GetString("comboBoxXEncoderDPI_2.Items5"),
            resources.GetString("comboBoxXEncoderDPI_2.Items6"),
            resources.GetString("comboBoxXEncoderDPI_2.Items7"),
            resources.GetString("comboBoxXEncoderDPI_2.Items8")});
            this.comboBoxXEncoderDPI_2.Name = "comboBoxXEncoderDPI_2";
            // 
            // label5_2
            // 
            this.label5_2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label5_2, "label5_2");
            this.label5_2.Name = "label5_2";
            // 
            // label6_2
            // 
            this.label6_2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label6_2, "label6_2");
            this.label6_2.Name = "label6_2";
            // 
            // label3_2
            // 
            resources.ApplyResources(this.label3_2, "label3_2");
            this.label3_2.BackColor = System.Drawing.Color.Transparent;
            this.label3_2.Name = "label3_2";
            // 
            // numericUpDownYInterleaveNum_2
            // 
            this.numericUpDownYInterleaveNum_2.DecimalPlaces = 5;
            resources.ApplyResources(this.numericUpDownYInterleaveNum_2, "numericUpDownYInterleaveNum_2");
            this.numericUpDownYInterleaveNum_2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownYInterleaveNum_2.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDownYInterleaveNum_2.Name = "numericUpDownYInterleaveNum_2";
            // 
            // comboBoxMaxGroupNumber_2
            // 
            this.comboBoxMaxGroupNumber_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMaxGroupNumber_2.DropDownWidth = 156;
            resources.ApplyResources(this.comboBoxMaxGroupNumber_2, "comboBoxMaxGroupNumber_2");
            this.comboBoxMaxGroupNumber_2.Items.AddRange(new object[] {
            resources.GetString("comboBoxMaxGroupNumber_2.Items"),
            resources.GetString("comboBoxMaxGroupNumber_2.Items1"),
            resources.GetString("comboBoxMaxGroupNumber_2.Items2"),
            resources.GetString("comboBoxMaxGroupNumber_2.Items3")});
            this.comboBoxMaxGroupNumber_2.Name = "comboBoxMaxGroupNumber_2";
            // 
            // checkBoxWeakSovent_2
            // 
            this.checkBoxWeakSovent_2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.checkBoxWeakSovent_2, "checkBoxWeakSovent_2");
            this.checkBoxWeakSovent_2.Name = "checkBoxWeakSovent_2";
            this.checkBoxWeakSovent_2.UseVisualStyleBackColor = false;
            // 
            // txtPrinterName_2
            // 
            resources.ApplyResources(this.txtPrinterName_2, "txtPrinterName_2");
            this.txtPrinterName_2.Name = "txtPrinterName_2";
            // 
            // label8_2
            // 
            this.label8_2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label8_2, "label8_2");
            this.label8_2.Name = "label8_2";
            // 
            // txtManufacturerName_2
            // 
            resources.ApplyResources(this.txtManufacturerName_2, "txtManufacturerName_2");
            this.txtManufacturerName_2.Name = "txtManufacturerName_2";
            // 
            // checkBoxHeadDir_2
            // 
            this.checkBoxHeadDir_2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.checkBoxHeadDir_2, "checkBoxHeadDir_2");
            this.checkBoxHeadDir_2.Name = "checkBoxHeadDir_2";
            this.checkBoxHeadDir_2.UseVisualStyleBackColor = false;
            // 
            // label9_2
            // 
            resources.ApplyResources(this.label9_2, "label9_2");
            this.label9_2.Name = "label9_2";
            // 
            // m_ComboBoxBit2Mode_2
            // 
            this.m_ComboBoxBit2Mode_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxBit2Mode_2, "m_ComboBoxBit2Mode_2");
            this.m_ComboBoxBit2Mode_2.Name = "m_ComboBoxBit2Mode_2";
            // 
            // labelSpeed_2
            // 
            resources.ApplyResources(this.labelSpeed_2, "labelSpeed_2");
            this.labelSpeed_2.BackColor = System.Drawing.Color.Transparent;
            this.labelSpeed_2.Name = "labelSpeed_2";
            // 
            // m_ComboBoxSpeed_2
            // 
            this.m_ComboBoxSpeed_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxSpeed_2, "m_ComboBoxSpeed_2");
            this.m_ComboBoxSpeed_2.Name = "m_ComboBoxSpeed_2";
            // 
            // dividerPanel1
            // 
            this.dividerPanel1.AllowDrop = true;
            this.dividerPanel1.BorderSide = System.Windows.Forms.Border3DSide.Top;
            this.dividerPanel1.Controls.Add(this.m_ButtonOK);
            this.dividerPanel1.Controls.Add(this.m_ButtonClear);
            this.dividerPanel1.Controls.Add(this.m_ButtonWriteInkCurve);
            resources.ApplyResources(this.dividerPanel1, "dividerPanel1");
            this.dividerPanel1.Name = "dividerPanel1";
            // 
            // m_ButtonOK
            // 
            resources.ApplyResources(this.m_ButtonOK, "m_ButtonOK");
            this.m_ButtonOK.Name = "m_ButtonOK";
            this.m_ButtonOK.Click += new System.EventHandler(this.m_ButtonOK_Click);
            // 
            // m_ButtonClear
            // 
            this.m_ButtonClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.m_ButtonClear, "m_ButtonClear");
            this.m_ButtonClear.Name = "m_ButtonClear";
            this.m_ButtonClear.Click += new System.EventHandler(this.m_ButtonClear_Click);
            // 
            // m_ButtonWriteInkCurve
            // 
            resources.ApplyResources(this.m_ButtonWriteInkCurve, "m_ButtonWriteInkCurve");
            this.m_ButtonWriteInkCurve.Name = "m_ButtonWriteInkCurve";
            this.m_ButtonWriteInkCurve.Click += new System.EventHandler(this.m_ButtonWriteInkCurve_Click);
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // PrinterHWSetting
            // 
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.dividerPanel1);
            this.Controls.Add(this.progressBar1);
            resources.ApplyResources(this, "$this");
            this.Name = "PrinterHWSetting";
            this.Load += new System.EventHandler(this.PrinterHWSetting_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.m_GroupBoxVender.ResumeLayout(false);
            this.m_GroupBoxVender.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownVOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_YMaxLen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numServiceStation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownGroupSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownColorSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownYSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownWhiteColorNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownCoatColorNum)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox8colorarrangement.ResumeLayout(false);
            this.groupBox8colorarrangement.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBoxDualBank.ResumeLayout(false);
            this.groupBoxDualBank.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.m_GroupBoxInk.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.grouperEpson.ResumeLayout(false);
            this.grouperEpson.PerformLayout();
            this.grouperSSystem.ResumeLayout(false);
            this.grouperSSystem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHBNum)).EndInit();
            this.panelCB1.ResumeLayout(false);
            this.panelCB1.PerformLayout();
            this.panelCB2.ResumeLayout(false);
            this.panelCB2.PerformLayout();
            this.grouper2.ResumeLayout(false);
            this.grouper2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFlatDistanceY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numzMaxRoute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeedUpDistanceR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDefaultZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDown_YEncoderDPI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDown_MaxOffsetPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFlatDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeedUpDistanceL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_PrintSence)).EndInit();
            this.grouperRipColorOrder.ResumeLayout(false);
            this.grouperPrintColorOrder.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.grouper3.ResumeLayout(false);
            this.grouperColorOrder_2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.grouper4.ResumeLayout(false);
            this.grouper4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYInterleaveNum_2)).EndInit();
            this.dividerPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private void m_ButtonOK_Click(object sender, System.EventArgs e)
        {
            if (m_ComboBoxHeadType.SelectedIndex == -1)
            {
                string info = ResString.GetEnumDisplayName(typeof (UIError), UIError.NullFactoryData);
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int select = ((VenderDisp)m_SupportHeadList[m_ComboBoxHeadType.SelectedIndex]).VenderID;
            bool bExFwFactoryData = PrinterHeadEnum.EGen5 == (PrinterHeadEnum)select;
            if (bExFwFactoryData && this.checkBoxSymmetricColorOder_2.Checked
                && !(this.numericUpDownYInterleaveNum_2.Value >= 2 && this.numericUpDownYInterleaveNum_2.Value%2 == 0))
            {
                string info = ResString.GetResString("UIError_YInterleaveNumError");
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!IsY2)
            {
                UIError? retError = CheckColorNumGroupNumWithHbType();
                if (retError.HasValue)
                {
                    string info = ResString.GetEnumDisplayName(typeof(UIError), retError.Value);
                    if (retError.Value == UIError.SsystemMapDismatch || retError.Value == UIError.SetHWSettingFail)
                    {
                        MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        DialogResult ret = MessageBox.Show(info, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (ret == DialogResult.No)
                            return;
                    }
                }
            }

            VenderPrinterConfig config = m_PrinterConfig;
            if (!OnGetVenderPrinterConfig(out config))
            {
                string info = ResString.GetEnumDisplayName(typeof (UIError), UIError.NullFactoryData);
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            m_PrinterConfig = config;

            bool bSet = true;
            //			SFWFactoryData fwData = new SFWFactoryData();
#if !LIYUUSB
            fwData.m_nValidSize = 62;
#endif
            fwData.m_nEncoder = (byte)config.m_nEncoder;

#if !LIYUUSB
            fwData.m_nHeadType = (byte) config.nHeadType;
            fwData.m_nWidth = (byte) config.nWidth;
            fwData.m_nPaper_w_left = config.m_nPaper_w_left;
#else
            VenderPrinterConfig config;
            if (!OnGetVenderPrinterConfig(out config))
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.NullFactoryData);
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fwData.m_nHeadType = (byte)config.nHeadType;
            fwData.m_nWidth = (ushort)config.nWidth;
#endif
            fwData.m_nColorNum = config.nColorNum;
            fwData.m_nGroupNum = config.nGroupNum;
            fwData.m_fHeadXColorSpace = config.fHeadXColorSpace;
            fwData.m_fHeadXGroupSpace = config.fHeadXGroupSpace;
            fwData.m_fHeadYSpace = config.fHeadYSpace;
            fwData.m_fHeadAngle = config.fHeadAngle;
#if !LIYUUSB
            fwData.m_nWhiteInkNum = config.m_nWhiteInkNum;
            fwData.m_nOverCoatInkNum = config.m_nOverCoatInkNum;
            fwData.m_nBitFlag = config.m_nBitFlag;

            fwData.eColorOrder = new byte[8];
            if (IsY2)
            {
                for (int i = 0; i < this.m_ComboBoxColorOder.Length; i++)
                {
                    if (i < CoreConst.MAX_COLOR_NUM)
                    {
                        fwData.eColorOrder[i] = getColorOrder_2(this.m_ComboBoxColorOder[i]);
                    }
                    else
                    {
                        fwData.eColorOrderExt[i - 8] = getColorOrder_2(this.m_ComboBoxColorOder[i]);
                    }
                }

            }
            else
            {
                for (int i = 0; i < this.m_ComboBoxPrintColorOder.Length; i++)
                {
                    if (i < fwData.eColorOrder.Length)
                    {
                        fwData.eColorOrder[i] = getColorOrder(this.m_ComboBoxPrintColorOder[i]);
                    }
                    else
                    {
                        fwData.eColorOrderExt[i - 8] = getColorOrder(this.m_ComboBoxPrintColorOder[i]);
                    }
                }
            }
            //xaar382 pixle模式
            fwData.m_xaar382_pixle_mode = config.m_xaar382_pixle_mode;
            fwData.ServePos = config.ServePos;
            fwData.m_PrintHeadCnt = config.m_PrintHeadCnt;

            fwData.m_nReserve = new byte[CONSTANT.nReserveSizeConst];
#else
			byte jetSpeed = (byte)m_comboBoxJetSpeed.SelectedIndex;
			byte inkType  = m_InkType[m_comboBoxInkType.SelectedIndex];
			if(CoreInterface.SetInkParam(jetSpeed, inkType) == 0)
			{
				string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.SetHWSettingFail);
				MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
#endif

            SPrintAmendProperty amendProperty = config.AmendProperty;
            USER_SET_INFORMATION userInfo = config.UserConfig; //new USER_SET_INFORMATION(true);
            bool bsetFwfd = CoreInterface.SetFWFactoryData(ref fwData) != 0;

            bool bExSet = true;
            if (IsY2)
            {
                bExSet = EpsonLCD.SetEPR_FactoryData_Ex(config.EpsonFactoryData_Ex) != 0;
            }

            bool bSetPap = true; //CoreInterface.SetPrintAmendProperty(ref amendProperty) == 1;
            bool bSetUsi = CoreInterface.SetUserSetInfo(ref userInfo) == 1;
            bool bSetColorDeep = ResetSG1024ColorDeep();
            LogWriter.WriteLog(new string[]
            {
                string.Format(
                    "SetUserSetInfo zDefault={0};m_sPrinterProperty.fPulsePerInchZ={1};m_sPrinterProperty.fPulsePerInchY={2}",
                    userInfo.zDefault, m_sPrinterProperty.fPulsePerInchZ, m_sPrinterProperty.fPulsePerInchY)
            }, true);
            if (bSet && bsetFwfd && bSetColorDeep && bExSet
#if RW_AMEND_PARAMETERS
                && bSetPap && bSetUsi
#endif
                )
            {
                string info = ResString.GetEnumDisplayName(typeof (UISuccess), UISuccess.SetHWSetting);
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (this.OKButtonClicked != null)
                    this.OKButtonClicked(sender, e);
            }
            else
            {
                string info = ResString.GetEnumDisplayName(typeof (UIError), UIError.SetHWSettingFail);
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (bDoubleYAxis)
            {
                doubleYaxis.YResolution = (uint) this.m_NumericUpDown_YEncoderDPI.Value;
                doubleYaxis.fMaxoffsetpos = UIPreference.ToInchLength(m_CurrentUnit,
                    Decimal.ToSingle(this.m_NumericUpDown_MaxOffsetPos.Value));
                EpsonLCD.SetDoubleYAxis_Info(doubleYaxis);
            }
            m_PrinterConfig.doubleYPram = doubleYaxis;
            SavePrinterConfig(m_nProductId);
        }

        private bool ResetSG1024ColorDeep()
        {
            bool ret = true;
            HEAD_BOARD_TYPE hbType = (HEAD_BOARD_TYPE) CoreInterface.get_HeadBoardType(true);
            if (hbType == HEAD_BOARD_TYPE.SG1024_4H || hbType == HEAD_BOARD_TYPE.SG1024_8H_GRAY_1BIT)
            {
                ret = SetColorDeep(1);
            }
            else if (hbType == HEAD_BOARD_TYPE.SG1024_4H_GRAY)
            {
                int colorDeep = PubFunc.GetColorDeep();
                if (colorDeep == -1)
                    colorDeep = 1;
                ret = SetColorDeep(colorDeep);
            }
            return ret;
        }

        private bool SetColorDeep(int colorDeep)
        {
            int mapCount = (int) Math.Pow(2, colorDeep);
            byte[] val = new byte[mapCount + 1];
            int i = 0;
            val[i++] = (byte) colorDeep;
            for (int j = 0; j < mapCount; j++)
                val[i++] = (byte) (j + 1);
            uint bufsize = (uint) val.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x81, val, ref bufsize, 0, 0x4000);
            if (ret == 0)
            {
                MessageBox.Show("Set 1024 gray map fialed!");
                return false;
            }
            CoreInterface.SetOutputColorDeep((byte) colorDeep);
            return true;
        }

        /// <summary>
        /// 检查配置颜色数和组数设置是否合法
        /// </summary>
        /// <returns></returns>
        private UIError? CheckColorNumGroupNumWithHbType()
        {
            if (m_ComboBoxColorNumber.SelectedIndex == -1 || m_ComboBoxGroupNumber.SelectedIndex == -1)
                return UIError.SsystemMapDismatch;
            int colornumber = m_ColorNumList[m_ComboBoxColorNumber.SelectedIndex];
            int nGroupNum = (sbyte) (m_ComboBoxGroupNumber.SelectedIndex + 1);

            int nWhiteInkNum = Decimal.ToByte(m_NumericUpDownWhiteColorNum.Value);
            int nOverCoatInkNum = Decimal.ToByte(m_NumericUpDownCoatColorNum.Value);

            int colorNum = colornumber + nWhiteInkNum + nOverCoatInkNum;
            HEAD_BOARD_TYPE hbType = (HEAD_BOARD_TYPE)CoreInterface.get_HeadBoardType(true);
            if (!CoreInterface.IsS_system())
            {
                int maxHeadNum = PubFunc.GetHeadNumPerHeadborad(hbType);
                if (bShowOneHeadDivider && m_CheckBoxOneHeadDivider.Checked)
                {
                    maxHeadNum *= 2;
                }
                int maxColorNum = Math.Min(CoreConst.MAX_COLOR_NUM, maxHeadNum);

                if (colorNum > maxColorNum)
                    return UIError.ColorNumError;

                int curHeadNum = colorNum*nGroupNum;
                if (curHeadNum > maxHeadNum)
                    return UIError.ColorNumGroupNumError;
            }
            else
            {
                int headBoardNum = (int) numericUpDownHBNum.Value;
                int headCount = colorNum*nGroupNum;
                int fiberCount = comboBoxTopologyMode.SelectedIndex == 0 ? 1 : 2;
                int expansionBoardCount = (comboBoxTopologyMode.SelectedIndex == 0 || comboBoxTopologyMode.SelectedIndex == 1) ? 1 : 2;
   
                int select = ((VenderDisp)m_SupportHeadList[m_ComboBoxHeadType.SelectedIndex]).VenderID;
                PrinterHeadEnum selectedHead = (PrinterHeadEnum)select;

                if (selectedHead == PrinterHeadEnum.Epson_5113 || selectedHead == PrinterHeadEnum.EPSON_I3200)
                {
                    headCount /= 4;
                }
                else if (bShowOneHeadDivider && m_CheckBoxOneHeadDivider.Checked)
                {
                    headCount /= 2;
                }
                var matchSystemConfigs =
                    SystemConfigMap.Where(
                        s =>
                            s.PrinterHeadEnum == selectedHead && s.HeadBoardType == hbType &&
                            (s.HBCountOfExBoardNO1 + s.HBCountOfExBoardNO2) == headBoardNum
                            && s.FiberCount == fiberCount
                            && s.ExpansionBoardCount == expansionBoardCount
                            && s.HeadCount >= headCount
                    );
                if (!matchSystemConfigs.Any())
                {
                    if (hbType == 0)
                    {
                        MessageBox.Show(
                            string.Format("HeadBoardType={0},获取头板类型失败,请确认连线.", hbType), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(
                            string.Format("当前系统配置有误：PrinterHeadEnum={0},HeadBoardType={1},HeadBoardNum={2}", selectedHead,
                                hbType, headBoardNum), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return UIError.SetHWSettingFail;
                } 
                var matchSystemConfig = matchSystemConfigs.OrderBy(q => q.HeadCount).First();
                if (matchSystemConfig.HeadCount < headCount)
                {
                    MessageBox.Show(
                        string.Format("当前系统配置有误：配置计算出的喷头数为{0},超过所有头板支持的最大喷头数总和{1}。", headCount,
                            matchSystemConfig.HeadCount), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return UIError.SetHWSettingFail;
                }
            }
            return null;
        }

        private bool IsAmentProperty(SPrintAmendProperty property)
        {
            if (property.bUseful != CoreConst.BeEnableConstMark)
            {
                return false;
            }
            foreach (byte cData in property.pRipColorOrder)
            {
                if (!Enum.IsDefined(typeof (ColorEnum), cData) && cData != 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void PrinterHWSetting_Load(object sender, System.EventArgs e)
        {
            HEAD_BOARD_TYPE hbType = (HEAD_BOARD_TYPE) CoreInterface.get_HeadBoardType(true);
            // 支持喷头混装设置
            buttonHeadMix.Visible = hbType == HEAD_BOARD_TYPE.KM1024I_8H_GRAY
                || hbType == HEAD_BOARD_TYPE.KM1024_8H_GRAY;
        }

        public bool OnPrinterPropertyChange(SPrinterProperty sp)
        {
            bool ret = true;
            try
            {
                label_FlatDistanceY.Visible = numFlatDistanceY.Visible = sp.nMediaType != 0;
                m_sPrinterProperty = sp;
                bIsAllWinEpson = sp.IsAllWinEpson();
                bIsGongzhengEpson = SPrinterProperty.IsEpson(sp.ePrinterHead); //sp.IsGongZengEpson() || sp.IsTATE();

                label13.Visible = numServiceStation.Visible = SPrinterProperty.IsSurportCapping();
                labelzMaxRoute.Visible = numzMaxRoute.Visible = SPrinterProperty.IsFloraT50OrT180();

                m_ComboBoxHeadType.Items.Clear();

                SSupportList list = new SSupportList();
                if (CoreInterface.GetSupportList(ref list) > 0)
                {
                    m_SupportHeadList = new ArrayList();
                    for (int i = 0; i < list.m_nList.Length; i++)
                    {
                        bool bfinded = false;

                        VenderDisp vd = new VenderDisp();

                        for (int j = 0; j < m_FileHeadList.Count; j++)
                        {
                            int curHead = ((VenderDisp) m_FileHeadList[j]).VenderID;
                            if (list.m_nList[i] == (byte) curHead)
                            {
                                vd = (VenderDisp) m_FileHeadList[j];
                                m_SupportHeadList.Add(vd);
                                bfinded = true;
                                break;
                            }
                        }
                        if (!bfinded && list.m_nList[i] > 0)
                        {
                            vd.DisplayName = list.m_nList[i].ToString();
                            vd.VenderID = list.m_nList[i];
                            m_SupportHeadList.Add(vd);
                        }
                    }
#if INWEAR_REVO

                    for (int i = 0; i < m_SupportHeadList.Count; i++)
                    {
                        VenderDisp venderDisp = (VenderDisp) m_SupportHeadList[i];
                        if (venderDisp.VenderID == (int) PrinterHeadEnum.Konica_KM1024i_MAE_13pl)
                        {
                            VenderDisp vd = venderDisp;
                            vd.VenderID = (int) PrinterHeadEnum.EGen5;//刻意修改枚举值,引发报错
                            m_SupportHeadList.Add(vd);
                            venderDisp.DisplayName = "REVO_H";
                            m_SupportHeadList[i] = venderDisp;
                        }
                        if (venderDisp.VenderID == (int)PrinterHeadEnum.Konica_KM3688_6pl)
                        {
                            VenderDisp vd = venderDisp;
                            vd.VenderID = (int)PrinterHeadEnum.EGen5;//刻意修改枚举值,引发报错
                            m_SupportHeadList.Add(vd);
                            venderDisp.DisplayName = "REVO_E";
                            m_SupportHeadList[i] = venderDisp;
                        }
                    }

#endif
                    for (int i = 0; i < m_SupportHeadList.Count; i++)
                    {
                        VenderDisp venderDisp = (VenderDisp)m_SupportHeadList[i];

                        if (PubFunc.IsColorJet_0x8() && venderDisp.VenderID == (int)PrinterHeadEnum.Konica_KM1024i_MHE_13pl)
                        {
                            venderDisp.DisplayName = "KM6988H";
                        }

                        string cmode = venderDisp.DisplayName;
                        m_ComboBoxHeadType.Items.Add(cmode);
                    }
                    //m_ComboBoxHeadType.Items.Add(PrinterHeadEnum.Spectra_Polaris.ToString());
                }
                else
                {
                    string info = ResString.GetEnumDisplayName(typeof (UIError), UIError.GetHWSettingFail);
                    MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ret = false;
                }
                m_ComboBoxColorNumber.Items.Clear();
                for (int i = 0; i < m_ColorNumList.Length; i++)
                {
                    m_ComboBoxColorNumber.Items.Add(m_ColorNumList[i].ToString());
                }

                m_ComboBoxWidth.Items.Clear();
                for (int i = 0; i < m_WidthList.Length; i++)
                {
                    float width = UIPreference.ToDisplayLength(m_CurrentUnit, (float) m_WidthList[i]*100.0f/25.4f);
                    m_ComboBoxWidth.Items.Add(width.ToString());
                }

                //comboBoxLayoutType.Items.Clear();
                //XCoordinatesDirection[] xCoordinates =
                //    (XCoordinatesDirection[]) Enum.GetValues(typeof (XCoordinatesDirection));
                //for (int i = 0; i < xCoordinates.Length; i++)
                //{
                //    comboBoxLayoutType.Items.Add(ResString.GetEnumDisplayName(typeof (XCoordinatesDirection),
                //        xCoordinates[i]));
                //}

                //comboBoxSymmetricType.Items.Clear();
                //SymmetricType[] symmetricTypes = (SymmetricType[]) Enum.GetValues(typeof (SymmetricType));
                //for (int i = 0; i < symmetricTypes.Length; i++)
                //{
                //    comboBoxSymmetricType.Items.Add(ResString.GetEnumDisplayName(typeof (SymmetricType),
                //        symmetricTypes[i]));
                //}

                //comboBoxMaxGroupNumber.Items.Clear();
                //comboBoxUsedHead.Items.Clear();
                //for (int i = 0; i < EpsonLCD.MaxGroupNumber; i++)
                //{
                //    comboBoxMaxGroupNumber.Items.Add((i + 1).ToString());
                //    comboBoxUsedHead.Items.Add((i + 1).ToString());
                //}

                this.LayoutColorOders(sp, sp.GetRealColorNum());

#if !RW_AMEND_PARAMETERS
                grouper2.Visible = false;
#endif
#if LIYUUSB
			m_comboBoxInkType.Items.Clear();
			for (int i=0;i<m_InkType.Length;i++)
			{
				m_comboBoxInkType.Items.Add(m_InkType[i].ToString("X"));
			}
		
			byte jetSpeed = 0;
			byte inkType = 0;
			if(CoreInterface.GetInkParam(ref jetSpeed,ref inkType) != 0)
			{
				//m_comboBoxInkType=0
				int Selected = -1;
				for (int i=0; i<m_InkType.Length;i++)
				{
					if(m_InkType[i] == inkType)
					{
						Selected = i;
						break;
					}
				}
				UIPreference.SetSelectIndexAndClampWithMax(m_comboBoxInkType,Selected);
				UIPreference.SetSelectIndexAndClampWithMax(m_comboBoxJetSpeed,jetSpeed);
			}
			else
			{
				string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.GetHWSettingFail);
				MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }
#endif
                if (IsY2)
                {
                    comboBoxLayoutType_2.Items.Clear();
                    XCoordinatesDirection[] xCoordinates = (XCoordinatesDirection[])Enum.GetValues(typeof(XCoordinatesDirection));
                    for (int i = 0; i < xCoordinates.Length; i++)
                    {
                        comboBoxLayoutType_2.Items.Add(ResString.GetEnumDisplayName(typeof(XCoordinatesDirection), xCoordinates[i]));
                    }
                    //喷头布局
                    comboBoxWhiteVarnishLayout_2.Items.Clear();
                    WhiteVarnishLayout[] whiteVarnishLayouts = (WhiteVarnishLayout[])Enum.GetValues(typeof(WhiteVarnishLayout));
                    for (int i = 0; i < whiteVarnishLayouts.Length; i++)
                    {
                        comboBoxWhiteVarnishLayout_2.Items.Add(ResString.GetEnumDisplayName(typeof(WhiteVarnishLayout), whiteVarnishLayouts[i]));
                    }

                    comboBoxSymmetricType_2.Items.Clear();
                    SymmetricType[] symmetricTypes = (SymmetricType[])Enum.GetValues(typeof(SymmetricType));
                    for (int i = 0; i < symmetricTypes.Length; i++)
                    {
                        comboBoxSymmetricType_2.Items.Add(ResString.GetEnumDisplayName(typeof(SymmetricType), symmetricTypes[i]));
                    }

                    m_ComboBoxBit2Mode_2.Items.Clear();
                    m_ComboBoxBit2Mode_2.Items.Add("Close");
                    m_ComboBoxBit2Mode_2.Items.Add("Small Dot");
                    m_ComboBoxBit2Mode_2.Items.Add("Middle Dot");
                    m_ComboBoxBit2Mode_2.Items.Add("Large Dot");

                    m_ComboBoxSpeed_2.Items.Clear();
                    m_ComboBoxSpeed_2.Items.Add("VSD_1");
                    m_ComboBoxSpeed_2.Items.Add("VSD_2");
                    m_ComboBoxSpeed_2.Items.Add("VSD_3");
                    m_ComboBoxSpeed_2.Items.Add("VSD_4");

                    comboBoxMaxGroupNumber_2.Items.Clear();
                    for (int i = 1; i <= 8; i++)
                    {
                        comboBoxMaxGroupNumber_2.Items.Add(i.ToString());    
                    }

                    this.LayoutColorOders_2(sp, sp.nColorNum, (int)(sp.nWhiteInkNum & 0xF), (int)(sp.nWhiteInkNum >> 4 & 0xF));

                }

                bool bGetepson = false;
                bGetepson = EpsonLCD.GetEPR_FactoryData_Ex(ref epsonExFac) > 0;
                m_bEpson = bGetepson;

                
                fwData = new SFWFactoryData();
                bool bGet = (CoreInterface.GetFWFactoryData(ref fwData) > 0);
                if (bGet)
                {
                    m_RadioButtonEncoder.Checked = ((fwData.m_nEncoder & (byte) INTBIT.Bit_0) == 0) ? false : true;
                    m_RadioButtonServoEncoder.Checked = !m_RadioButtonEncoder.Checked;
                    int sIndex = -1;
                    for (int i = 0; i < m_ComboBoxHeadType.Items.Count; i++)
                    {
                        int CurHead = ((VenderDisp) m_SupportHeadList[i]).VenderID;
                        if (fwData.m_nHeadType == CurHead)
                        {
                            sIndex = i;
                            break;
                        }
                    }
                    UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxHeadType, sIndex);
                }
                else
                {
                    string info = ResString.GetEnumDisplayName(typeof (UIError), UIError.GetHWSettingFail);
                    MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ret = false;
                }
#if !LIYUUSB
                SBoardInfo sBoardInfo = new SBoardInfo();
                if (CoreInterface.GetBoardInfo(0, ref sBoardInfo) != 0)
                {
                    m_nProductId = sBoardInfo.m_nBoardProductID;
                }
                else
                {
                    string info = ResString.GetEnumDisplayName(typeof (UIError), UIError.GetHWSettingFail);
                    MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ret = false;
                }
#else
			m_nProductId = 0x0100;
#endif

                m_GroupBoxVender.Enabled = true;
                //Should be error because get value maybe illegal value

                VenderPrinterConfig config = m_PrinterConfig;
                if (bGet)
                {
                    config.nHeadType = (PrinterHeadEnum) fwData.m_nHeadType;
                    config.nWidth = fwData.m_nWidth;
                    config.nColorNum = fwData.m_nColorNum;
                    config.nGroupNum = fwData.m_nGroupNum;
                    config.fHeadXColorSpace = fwData.m_fHeadXColorSpace;
                    config.fHeadXGroupSpace = fwData.m_fHeadXGroupSpace;
                    config.fHeadYSpace = fwData.m_fHeadYSpace;
                    config.fHeadAngle = fwData.m_fHeadAngle;
#if !LIYUUSB
                    config.m_nWhiteInkNum = fwData.m_nWhiteInkNum;
                    config.m_nOverCoatInkNum = fwData.m_nOverCoatInkNum;
                    config.m_nBitFlag = fwData.m_nBitFlag;
                    config.m_nPaper_w_left = fwData.m_nPaper_w_left;
#endif
                    config.ServePos = fwData.ServePos;
                    config.m_xaar382_pixle_mode = fwData.m_xaar382_pixle_mode;
                    config.m_nEncoder = fwData.m_nEncoder;
                    config.m_PrintHeadCnt = fwData.m_PrintHeadCnt;
                }
                else
                {
                    config.nHeadType = (PrinterHeadEnum) sp.ePrinterHead;
                    config.nWidth = (byte) (sp.fMaxPaperWidth*0.254f + 0.5f);
                    config.nColorNum = sp.nColorNum;
                    if (SPrinterProperty.IsPolaris(config.nHeadType))
                    {
                        if (sp.nOneHeadDivider > 1)
                            config.nGroupNum = (sbyte) (-sp.nOneHeadDivider);
                        else
                            config.nGroupNum = (sbyte) sp.nHeadNumPerGroupY;
                    }
                    else if (SPrinterProperty.IsKonica512(config.nHeadType))
                    {
                        if (sp.nOneHeadDivider > 1)
                            config.nGroupNum = (sbyte) (-sp.nOneHeadDivider);
                        else
                            config.nGroupNum = (sbyte) (sp.nHeadNumPerGroupY);
                    }
                    else
                        config.nGroupNum = (sbyte) (sp.nHeadNumPerGroupY*sp.nHeadNumPerColor);
                    config.fHeadXColorSpace = sp.fHeadXColorSpace;
                    config.fHeadXGroupSpace = sp.fHeadXGroupSpace;
                    config.fHeadYSpace = sp.fHeadYSpace;
                    config.fHeadAngle = sp.fHeadAngle;
                    //					config.m_nPaper_w_left = sp.n.m_nPaper_w_left;
#if !LIYUUSB
                    config.m_nWhiteInkNum = fwData.m_nWhiteInkNum;
                    config.m_nOverCoatInkNum = fwData.m_nOverCoatInkNum;
                    config.m_nBitFlag = fwData.m_nBitFlag;
#endif
                }
                if (!bGetepson)
                    epsonExFac = new EPR_FactoryData_Ex(sp);
                epsonExFac.m_nColorOrder = fwData.eColorOrder;
                epsonExFac.m_nXEncoderDPI = fwData.m_nEncoder;
                epsonExFac.version = fwData.m_nVersion;
                config.EpsonFactoryData_Ex = epsonExFac;

                USER_SET_INFORMATION userInfo = new USER_SET_INFORMATION(true);
                SPrintAmendProperty property = new SPrintAmendProperty(true);
                bool bGetAmend = true; //CoreInterface.GetPrintAmendProperty(ref property) == 1;
                bool bGetUser = CoreInterface.GetUserSetInfo(ref userInfo) == 1;
                if (!bGetUser)
                    userInfo = new USER_SET_INFORMATION(true);
                bool isGetProperty = bGetAmend && bGetUser;
                if (!isGetProperty && !IsY2)
                {
#if RW_AMEND_PARAMETERS
                    string info = ResString.GetEnumDisplayName(typeof (UIError), UIError.GetHWSettingFail);
                    MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
                }
                config.AmendProperty = property;
                config.UserConfig = userInfo;

                if (bDoubleYAxis && EpsonLCD.GetDoubleYAxis_Info(ref doubleYaxis))
                {
                    config.doubleYPram = doubleYaxis;
                }
                OnSetVenderPrinterConfig(config);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ret = false;
            }
            return ret;
        }

        public void OnGetProperty(ref SPrinterProperty sp, ref bool bChangeProperty)
        {
#if !LIYUUSB
            VenderPrinterConfig config = m_PrinterConfig;
#else
            VenderPrinterConfig config;
			if(!OnGetVenderPrinterConfig(out config))
			{
				string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.NullFactoryData);
				MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;
			}
#endif
            bChangeProperty = false;
            bool bChange =
                (
                    config.nWidth != (int) (sp.fMaxPaperWidth*0.254f + 0.5f) ||
                    config.nColorNum != sp.nColorNum ||
                    config.fHeadXColorSpace != sp.fHeadXColorSpace ||
                    config.fHeadXGroupSpace != sp.fHeadXGroupSpace ||
                    config.nHeadType != sp.ePrinterHead ||
                    config.fHeadYSpace != sp.fHeadYSpace ||
                    config.fHeadAngle != sp.fHeadAngle
#if !LIYUUSB
                    || config.m_nWhiteInkNum != (sp.nWhiteInkNum & 0xf)
                    || config.m_nOverCoatInkNum != ((sp.nWhiteInkNum & 0xf0) >> 4)
                    || ((config.m_nBitFlag & 1) == 0) != sp.bHeadInLeft
                    || ((config.m_nBitFlag & 0xfe)) != ((sp.bSupportBit1 & 0xfe))
#endif

                    );
            if (!bChange)
            {
                if (SPrinterProperty.IsPolaris(config.nHeadType))
                {
                    if (config.nGroupNum > 0)
                    {
                        if (config.nGroupNum != sp.nHeadNumPerGroupY)
                            bChange = true;
                    }
                    else
                    {
                        if (config.nGroupNum != -sp.nOneHeadDivider)
                            bChange = true;
                    }
                }
                else if (SPrinterProperty.IsKonica512(config.nHeadType))
                {
                    if (config.nGroupNum != sp.nHeadNumPerGroupY)
                        bChange = true;
                }
                else
                {
                    if (config.nGroupNum != sp.nHeadNumPerGroupY*sp.nHeadNumPerColor)
                        bChange = true;
                }
            }
            if (bChange)
            {
                bChangeProperty = true;
                sp.fMaxPaperWidth = (float) config.nWidth/0.254f;
                sp.nColorNum = config.nColorNum;
                sbyte nGroupNum = config.nGroupNum;
                if ((config.m_nBitFlag & (uint) INTBIT.Bit_6) != 0)
                {
                    nGroupNum = (sbyte)(nGroupNum / 2);
                }
				if (SPrinterProperty.IsPolaris(config.nHeadType) || SPrinterProperty.IsXAAR1201(config.nHeadType))
                {
                    if (nGroupNum > 0)
                    {
                        sp.nHeadNumPerGroupY = (byte)nGroupNum;
                        sp.nOneHeadDivider = 1;
                        sp.nHeadNumPerColor = 4;
                    }
                    else
                    {
                        sp.nHeadNumPerGroupY = (byte)-nGroupNum;
                        sp.nOneHeadDivider = 2;
                        sp.nHeadNumPerColor = 2;
                    }
                }
                else if (SPrinterProperty.IsKonica512(config.nHeadType))
                {
                    if (nGroupNum > 0)
                    {
                        sp.nHeadNumPerGroupY = (byte)nGroupNum;
                        sp.nHeadNumPerColor = 2;
                        sp.nOneHeadDivider = 1;
                    }
                    else
                    {
                        sp.nHeadNumPerGroupY = (byte)-nGroupNum;
                        sp.nHeadNumPerColor = 1;
                        sp.nOneHeadDivider = 2;
                    }
                }
                else
                {
                    if (nGroupNum > 0)
                    {
                        sp.nHeadNumPerGroupY = (byte)nGroupNum;
                    }
                    else
                    {
                        sp.nHeadNumPerGroupY = (byte)-nGroupNum;
                    }
                    sp.nHeadNumPerColor = 1;
                }
                sp.fHeadXColorSpace = config.fHeadXColorSpace;
                sp.fHeadXGroupSpace = config.fHeadXGroupSpace;
                sp.nHeadNumNew = (ushort) (sp.nColorNum*sp.nHeadNumPerGroupY*sp.nHeadNumPerColor);
                sp.nHeadNumOld = (byte) sp.nHeadNumNew;
                sp.nHeadNumPerRow = (byte) (sp.nColorNum*sp.nHeadNumPerColor);
                sp.ePrinterHead = config.nHeadType;
                sp.fHeadYSpace = config.fHeadYSpace;
                sp.fHeadAngle = config.fHeadAngle;
#if !LIYUUSB
                sp.nWhiteInkNum = (byte) ((config.m_nOverCoatInkNum << 4) + config.m_nWhiteInkNum);
                sp.bHeadInLeft = (config.m_nBitFlag & 1) == 0;
                sp.bSupportBit1 = (byte) ((config.m_nBitFlag & 0xfe) + (sp.bSupportBit1 & 1));

#endif
            }
#if false
			int nResX = Convert.ToInt32(m_ComboBoxResolution.Items[m_ComboBoxResolution.SelectedIndex]);
			if(sp.nResX !=  nResX)
			{
				bChangeProperty = true;
				sp.nResX = nResX;
			}
#endif
        }

        public void OnPrinterSettingChange(SPrinterSetting ss)
        {
            UIPreference.SetValueAndClampWithMinMax(numFlatDistanceY, m_CurrentUnit, ss.sExtensionSetting.FlatSpaceY);
        }

        public void OnGetPrinterSetting(ref SPrinterSetting ss)
        {
            ss.sExtensionSetting.FlatSpaceY = UIPreference.ToInchLength(m_CurrentUnit,
                   Decimal.ToSingle(numFlatDistanceY.Value));
        }

        public void OnPreferenceChange(UIPreference up)
        {
            m_CurrentPreference = up;
            //m_bInitControl = true;
            if (m_CurrentUnit != up.Unit)
            {
                OnUnitChange(up.Unit);
                m_CurrentUnit = up.Unit;
            }
            //m_bInitControl = false;
        }

        private void OnUnitChange(UILengthUnit newUnit)
        {
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, m_NumericUpDownColorSpace);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.m_NumericUpDownColorSpace, this.m_ToolTip);

            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, m_NumericUpDownGroupSpace);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.m_NumericUpDownGroupSpace, this.m_ToolTip);

            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, m_NumericUpDownYSpace);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.m_NumericUpDownYSpace, this.m_ToolTip);

            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, m_NumericUpDownVOffset);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.m_NumericUpDownVOffset, this.m_ToolTip);

            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numericUpDownW);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.numericUpDownW, this.m_ToolTip);

            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numSpeedUpDistanceL);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.numSpeedUpDistanceL, this.m_ToolTip);

            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numSpeedUpDistanceR);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.numSpeedUpDistanceR, this.m_ToolTip);

            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numFlatDistance);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.numFlatDistance, this.m_ToolTip);

            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numFlatDistanceY);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.numFlatDistanceY, this.m_ToolTip);

            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numDefaultZ);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.numDefaultZ, this.m_ToolTip);

            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, m_NumericUpDown_MaxOffsetPos);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.m_NumericUpDown_MaxOffsetPos, this.m_ToolTip);

            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numServiceStation);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.numServiceStation, this.m_ToolTip);

            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numzMaxRoute);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.numzMaxRoute, this.m_ToolTip);

            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numericUpDown_YMaxLen);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.numericUpDown_YMaxLen, this.m_ToolTip);
        }

        private void m_ComboBoxColorNumber_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int colornumber = 4;
            int maxgroup = 0;
            if (m_ComboBoxColorNumber.SelectedIndex != -1)
            {
                colornumber = m_ColorNumList[m_ComboBoxColorNumber.SelectedIndex];
            }
            int colornum = (int) (colornumber + m_NumericUpDownWhiteColorNum.Value + m_NumericUpDownCoatColorNum.Value);
            int maxHeadNum = MAX_HEAD_NUM;
            if (m_bEpson && colornum == 8)
                maxHeadNum *= 2;

            if (!IsY2)
            {
                if (colornum != 0)
                    maxgroup = maxHeadNum / colornum;
                int length = m_ComboBoxGroupNumber.Items.Count;
                if (maxgroup > length)
                {
                    for (int i = length; i < maxgroup; i++)
                        m_ComboBoxGroupNumber.Items.Add((i + 1));
                }
                else if (maxgroup < length)
                {
                    if (this.m_ComboBoxGroupNumber.SelectedIndex >= maxgroup)
                        this.m_ComboBoxGroupNumber.SelectedIndex = maxgroup - 1;
                    for (int i = maxgroup; i < length; i++)
                        m_ComboBoxGroupNumber.Items.Remove((i + 1));
                }
            }

            if (m_NumericUpDownCoatColorNum.Value > 0)
            {
                label_VOffset.Visible = true;
                m_NumericUpDownVOffset.Visible = true;
                
            }
            else
            {
                label_VOffset.Visible = false;
                m_NumericUpDownVOffset.Visible = false;
                m_NumericUpDownVOffset.Value = 0;
            }

            this.LayoutColorOders(this.m_sPrinterProperty, colornum);

            if (IsY2)
            {
                this.LayoutColorOders_2(this.m_sPrinterProperty, colornum, (int)m_NumericUpDownWhiteColorNum.Value, (int)m_NumericUpDownCoatColorNum.Value);
            }

            bool colorarrangementVisible = (colornumber == 4 || colornumber == 6 || colornumber == 8) &&
                                           m_sPrinterProperty.ePrinterHead != PrinterHeadEnum.EGen5;
            this.groupBox8colorarrangement.Visible = colorarrangementVisible;
            if (colorarrangementVisible)
            {
                System.Resources.ResourceManager resources =
                    new System.Resources.ResourceManager(typeof (PrinterHWSetting));
                string textFormat =
                    ((string)
                        (resources.GetObject("groupBox8colorarrangement.Text", Thread.CurrentThread.CurrentUICulture)));
                if (textFormat != null)
                    this.groupBox8colorarrangement.Text = string.Format(textFormat, colornum);
            }
            checkBox_VerY.Visible = m_sPrinterProperty.ePrinterHead != PrinterHeadEnum.EGen5;
            this.checkBox4ColorMirror.Visible = (colornumber == 4 || colornumber == 8) &&
                                                m_sPrinterProperty.ePrinterHead != PrinterHeadEnum.EGen5;
            this.checkBox4ColorMirror.Checked = false;
            this.checkBox8CCompatibilityMode.Visible = (colornumber == 6) &&
                                                       m_sPrinterProperty.ePrinterHead != PrinterHeadEnum.EGen5;
            checkBox8CCompatibilityMode.Checked = false;
            UpdateColorarrAngementVisible();
        }

        private void m_ButtonClear_Click(object sender, System.EventArgs e)
        {
            if (CoreInterface.SendJetCommand((int) JetCmdEnum.ClearFWFactoryData, 0) != 0)
            {
                string info = ResString.GetEnumDisplayName(typeof (UISuccess), UISuccess.ClearFWFactoryData);
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string info = ResString.GetEnumDisplayName(typeof (UIError), UIError.ClearFWFactoryData);
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private bool SavePrinterConfig(int productID)
        {
            string curFile = Application.StartupPath +
                             Path.DirectorySeparatorChar + sPrinterProductList
                             + productID.ToString("X") + ".xml";
            XmlElement root;
            SelfcheckXmlDocument doc = new SelfcheckXmlDocument();

            bool success = true;
            try
            {
                root = doc.CreateElement("", "product_" + productID.ToString("X"), "");
                doc.AppendChild(root);
                string xml = "";
                //for (int i=0; i< list.Count;i++)
                {
                    VenderPrinterConfig job = m_PrinterConfig;
                    xml += job.SystemConvertToXml();
                }
                root.InnerXml = xml;
                doc.Save(curFile);
            }
            catch (Exception e)
            {
                success = false;

                Debug.Assert(false, e.Message + e.StackTrace);
            }
            return success;
        }

        private bool OnGetVenderPrinterConfig(out VenderPrinterConfig config)
        {
            bool bSet = true;
            config = new VenderPrinterConfig();
            int colornumber = 4;
            if (m_ComboBoxColorNumber.SelectedIndex == -1)
                bSet = false;
            else
            {
                colornumber = m_ColorNumList[m_ComboBoxColorNumber.SelectedIndex];
            }
            config.nColorNum = (byte) colornumber;
            if (m_ComboBoxGroupNumber.SelectedIndex == -1)
                bSet = false;
            else
                config.nGroupNum = (sbyte) (m_ComboBoxGroupNumber.SelectedIndex + 1);

            config.fHeadXColorSpace = UIPreference.ToInchLength(m_CurrentUnit,
                Decimal.ToSingle(m_NumericUpDownColorSpace.Value));
            config.fHeadXGroupSpace = UIPreference.ToInchLength(m_CurrentUnit,
                Decimal.ToSingle(m_NumericUpDownGroupSpace.Value));
            config.fHeadYSpace = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownYSpace.Value));

            config.fHeadAngle = Decimal.ToSingle(m_NumericUpDownAngle.Value);

            config.m_nWhiteInkNum = Decimal.ToByte(m_NumericUpDownWhiteColorNum.Value);
            config.m_nOverCoatInkNum = Decimal.ToByte(m_NumericUpDownCoatColorNum.Value);
            uint nBitFlag = 0;
            if (m_CheckBoxIsHeadLeft.Checked)
                nBitFlag |= (uint)INTBIT.Bit_0;
            if (m_CheckBoxMirror.Checked)
                nBitFlag |= (uint)INTBIT.Bit_1;
            //if (m_CheckBoxSupportLcd.Checked)
                nBitFlag |= (uint)INTBIT.Bit_2;
            if (radioButtonDualBank.Checked)
                nBitFlag |= (uint)INTBIT.Bit_3;
#if COLORORDER
            nBitFlag |= (uint)INTBIT.Bit_4;
#endif
            if (checkBox_VerY.Checked)
                nBitFlag |= (uint)INTBIT.Bit_5; // INTBIT.Bit_1;

            {
            if (checkBox4ColorMirror.Checked)
            {
                nBitFlag |= (uint)INTBIT.Bit_6;
            }
            }
            if (checkBoxZMeasur.Checked)
                nBitFlag |= (uint)INTBIT.Bit_9;
            if (checkBox8CCompatibilityMode.Checked)
                nBitFlag |= (uint)INTBIT.Bit_8;

            if (checkBoxStaggered.Checked)
                nBitFlag |= (uint)INTBIT.Bit_10;
            if (checkBoxMediaSensor.Checked)
                nBitFlag |= (uint)INTBIT.Bit_11;
            if (checkBoxWhiteInkRight.Checked)
                nBitFlag |= (uint)INTBIT.Bit_12;

            config.m_nBitFlag = nBitFlag;

            if (m_ComboBoxHeadType.SelectedIndex != -1)
            {
                config.nHeadType =
                    (PrinterHeadEnum) ((VenderDisp) m_SupportHeadList[m_ComboBoxHeadType.SelectedIndex]).VenderID;
            }
            else
                bSet = false;

            if (SPrinterProperty.IsEpson(config.nHeadType))
            {
                float cmW = UIPreference.ToInchLength(m_CurrentUnit, (float) this.numericUpDownW.Value)*2.54f;
                config.nWidth = (int) Math.Round((cmW)/10f);
                config.m_nPaper_w_left = (byte) Math.Round(cmW%10f*10);
            }
            else
            {
                if (this.numericUpDownW.Value == 0)
                    bSet = false;
                else
                {
                    float cmW = UIPreference.ToInchLength(m_CurrentUnit, (float) this.numericUpDownW.Value)*2.54f;
                    config.nWidth = (int) Math.Round((cmW)/10f);
                }
            }
            if (bShowOneHeadDivider && m_CheckBoxOneHeadDivider.Checked)
                config.nGroupNum *= -1;
            //xaar382 pixle mode
            config.m_xaar382_pixle_mode = Decimal.ToByte(m_CheckBoxXaar382Mode.Checked ? 'P' : 'C');
            config.ServePos = UIPreference.ToInchLength(m_CurrentUnit, (float) numServiceStation.Value);
            if (bSetHeadCount)
            {
                config.m_PrintHeadCnt = (byte)(listHeadCount.SelectedIndex + 1);
            }
            else
            {
                config.m_PrintHeadCnt = 0;
            }
            //			if(m_CheckBoxSupportLcd.Checked)
            if (IsY2)
            {
                config.EpsonFactoryData_Ex = new EPR_FactoryData_Ex(m_sPrinterProperty);
                this.OnGetEpsonExFWFactoryData(ref config.EpsonFactoryData_Ex);
            }
            //config.AmendProperty = new SPrintAmendProperty(true);
            this.OnGetAmentProperty(ref config.AmendProperty);
            //config.UserConfig = new USER_SET_INFORMATION(true);
            this.OnGetUserInfo(ref config.UserConfig);
            //			config.AllwinCleanPara = this.allwinCleanControl1.GetCleanParameter();
            byte value1 = 0;
            if (m_RadioButtonEncoder.Checked)
                value1 |= (byte)INTBIT.Bit_0;
            config.m_nEncoder = value1;
            return bSet;
        }

        private void OnSetVenderPrinterConfig(VenderPrinterConfig config)
        {
            try
            {
                doubleYaxis = config.doubleYPram;
                int selectIndex = 0;
                for (int i = 0; i < m_ColorNumList.Length; i++)
                {
                    if (config.nColorNum == m_ColorNumList[i])
                    {
                        selectIndex = i;
                        break;
                    }
                }
                UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxColorNumber, selectIndex);
                m_ComboBoxGroupNumber.Items.Clear();
                int maxgroup = 3;
                //if(config.nColorNum == 4 || config.nColorNum == 6 )
                int colornum = config.nColorNum + config.m_nWhiteInkNum + config.m_nOverCoatInkNum;
                int maxHeadNum = MAX_HEAD_NUM;
                if (SPrinterProperty.IsEpson(config.nHeadType) && colornum == 8)
                    maxHeadNum *= 2;

                if (IsY2)
                {
                    for (int i = 1; i <= 8; i++)
                    {
                        m_ComboBoxGroupNumber.Items.Add(i.ToString());
                    }
                }
                else
                {
                    if (colornum != 0)
                        maxgroup = maxHeadNum / colornum;

                    for (int i = 0; i < maxgroup; i++)
                    {
                        m_ComboBoxGroupNumber.Items.Add((i + 1));
                    }
                }

                int index = -1;
                for (int i = 0; i < m_ComboBoxHeadType.Items.Count; i++)
                {
                    int CurHead = ((VenderDisp) m_SupportHeadList[i]).VenderID;
                    if (CurHead == (int) config.nHeadType)
                    {
                        index = i;
                        break;
                    }
                }
                UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxHeadType, index);
                if (config.nGroupNum > 0)
                {

                    UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxGroupNumber, config.nGroupNum - 1);
                    m_CheckBoxOneHeadDivider.Checked = false;
                }
                else
                {
                    UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxGroupNumber, -(config.nGroupNum) - 1);
                    m_CheckBoxOneHeadDivider.Checked = true;
                }
                UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownColorSpace, m_CurrentUnit,
                    config.fHeadXColorSpace);
                UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownGroupSpace, m_CurrentUnit,
                    config.fHeadXGroupSpace);
                UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownYSpace, m_CurrentUnit, config.fHeadYSpace);
                UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownAngle, config.fHeadAngle);

                UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownWhiteColorNum, config.m_nWhiteInkNum);
                UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownCoatColorNum, config.m_nOverCoatInkNum);
                m_CheckBoxIsHeadLeft.Checked = ((config.m_nBitFlag & (uint) INTBIT.Bit_0) != 0);
                m_CheckBoxMirror.Checked = ((config.m_nBitFlag & (uint) INTBIT.Bit_1) != 0);
                this.m_CheckBoxSupportLcd.Checked = ((config.m_nBitFlag & (uint) INTBIT.Bit_2) != 0); // ???????
                radioButtonDualBank.Checked = ((config.m_nBitFlag & (uint) INTBIT.Bit_3) != 0);
                radioButtonSingleBank.Checked = ((config.m_nBitFlag & (uint) INTBIT.Bit_3) == 0);
                checkBox_VerY.Checked = ((config.m_nBitFlag & (uint) INTBIT.Bit_5) != 0);
                checkBox4ColorMirror.Checked = ((config.m_nBitFlag & (uint) INTBIT.Bit_6) != 0);
                checkBoxZMeasur.Checked = ((config.m_nBitFlag & (uint) INTBIT.Bit_9) != 0);
                checkBox8CCompatibilityMode.Checked = ((config.m_nBitFlag & (uint) INTBIT.Bit_8) != 0);

                checkBoxStaggered.Checked = ((config.m_nBitFlag & (uint) INTBIT.Bit_10) != 0);
                checkBoxMediaSensor.Checked = ((config.m_nBitFlag & (uint) INTBIT.Bit_11) != 0);
                checkBoxWhiteInkRight.Checked = ((config.m_nBitFlag & (uint) INTBIT.Bit_12) != 0);
                //if(SPrinterProperty.IsEpson(config.nHeadType))
                {
                    float width = UIPreference.ToDisplayLength(m_CurrentUnit, config.nWidth*100.0f/25.4f);
                    float leftwidth = UIPreference.ToDisplayLength(m_CurrentUnit, config.m_nPaper_w_left/25.4f);
                    UIPreference.SetValueAndClampWithMinMax(this.numericUpDownW, width + leftwidth);
                }
                //else
                //{
                //    int sIndex = -1;
                //    for (int i=0;i<m_WidthList.Length;i++)
                //    {
                //        if(config.nWidth == m_WidthList[i])
                //        {
                //            sIndex = i;
                //            break;
                //        }
                //    }
                //    UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxWidth,sIndex);
                //}
                //Xaar382 pixle模式
                m_CheckBoxXaar382Mode.Checked = (config.m_xaar382_pixle_mode == 'P') ? true : false;
                numServiceStation.Value = (decimal) UIPreference.ToDisplayLength(m_CurrentUnit, config.ServePos);

                if (listHeadCount.Items.Count > 0)
                {
                    listHeadCount.SelectedIndex = (config.m_PrintHeadCnt - 1 < 0) ? 0 : (config.m_PrintHeadCnt - 1);
                }

                if (IsY2)
                {
                    OnEpsonExFWFactoryDataChanged(config.EpsonFactoryData_Ex);
                }
                else
                {
                    for (int i = 0; i < colornum; i++)
                    {
                        ComboBox cbo = m_ComboBoxPrintColorOder[i];
#if COLORORDER
                        ColorEnum_Short[] names = (ColorEnum_Short[]) Enum.GetValues(typeof (ColorEnum_Short));
                        int selectindex = 0;
                        for (int j = 0; j < names.Length; j++)
                        {
                            if (m_sPrinterProperty.eColorOrder.Length > i && m_sPrinterProperty.eColorOrder[i] == (byte)names[j])
                                selectindex = j;
                        }
                        cbo.SelectedIndex = selectindex;
#else
                        cbo.SelectedIndex = i;
#endif
                    }

                }
                bool bError = false;
                if (!IsAmendProperty(config.AmendProperty))
                {
                    //bError = true; // 20150105 关闭rip色序功能后，关闭检查报警功能
                    config.AmendProperty = new SPrintAmendProperty(true);
                }
                OnAmentDataChanged(config.AmendProperty);
                if (!IsUserInfo(config.UserConfig))
                {
#if RW_AMEND_PARAMETERS
                    bError = true;
#endif
                    config.UserConfig = new USER_SET_INFORMATION(true);
                }
                OnUserinfoChanged(config.UserConfig);
                if (bError)
                    MessageBox.Show(this, ResString.GetResString("GetWrongInfo"), ResString.GetProductName());
                //				this.allwinCleanControl1.SetCleanParameter(config.AllwinCleanPara);
                if (bDoubleYAxis)
                {
                    UIPreference.SetValueAndClampWithMinMax(this.m_NumericUpDown_MaxOffsetPos, m_CurrentUnit, doubleYaxis.fMaxoffsetpos);
                    m_NumericUpDown_YEncoderDPI.Value = bDoubleYAxis ? doubleYaxis.YResolution : 180;
                }

                m_RadioButtonEncoder.Checked = ((config.m_nEncoder & (byte)INTBIT.Bit_0) == 0) ? false : true;
                m_RadioButtonServoEncoder.Checked = !m_RadioButtonEncoder.Checked;
                checkBoxSupportZendPointSensor.Checked = config.UserConfig.bSupportZendPointSensor;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, ResString.GetProductName());
            }
        }

        private void m_ButtonWriteInkCurve_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Job Files (*.xml)|*.xml|All Files (*.*)|*.*";

            openFileDialog1.InitialDirectory = m_CurrentPreference.WorkingFolder;
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.progressBar1.Visible = true;
                this.TopLevelControl.Refresh();
                m_CurrentPreference.WorkingFolder = Path.GetDirectoryName(openFileDialog1.FileName);
                try
                {
                    MyXmlReader mXmlReader = new MyXmlReader();
                    string mFileName = openFileDialog1.FileName + ".txt";
                    mXmlReader.ReadXml(openFileDialog1.FileName);

                    int i = 0;
                    foreach (AREA area in mXmlReader.AREAsFromXml1)
                    {
                        i++;
                        byte[] areaDatas = mXmlReader.GetAREABuffer(area);
                        int retc = CoreInterface.WriteHBEEprom(areaDatas, area.AreaHeader.Size, area.ADDR);
                        if (retc != area.AreaHeader.Size)
                        {
                            MessageBox.Show(this, "Write failed, please try again!", this.Text, MessageBoxButtons.OK);
                            this.progressBar1.Value = 0;
                            this.progressBar1.Visible = false;
                            return;
                        }
                        else
                            this.progressBar1.Value = i*100/mXmlReader.AREAsFromXml1.Count;
                    }
                    if (PubFunc.GetUserPermission() != (int)UserPermission.Operator)
                    {
                        FileStream fileStream = new FileStream(mFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite,
                            FileShare.ReadWrite);
                        ;
                        BinaryWriter bw = new BinaryWriter(fileStream);

                        foreach (AREA area in mXmlReader.AREAsFromXml1)
                        {
                            byte[] areaDatas = mXmlReader.GetAREABuffer(area);
                            bw.Seek(area.ADDR, 0);
                            bw.Write(areaDatas);
                        }

                        bw.Flush();
                        bw.Close();
                    }
                    MessageBox.Show("Write success!");
                    this.progressBar1.Value = 0;
                    this.progressBar1.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private bool SurpportOneHeadDivider(PrinterHeadEnum select)
        {
            if ((SPrinterProperty.IsPolaris(select)||
                 PrinterHeadEnum.Konica_KM512L_42pl == select ||
                 PrinterHeadEnum.Konica_KM512LNX_35pl == select ||
                 PrinterHeadEnum.Konica_KM512M_14pl == select ||
                 PrinterHeadEnum.Konica_KM512_SH_4pl == select
                 || PrinterHeadEnum.RICOH_GEN4_7pl == select
                 || PrinterHeadEnum.RICOH_GEN4L_15pl == select
                 || PrinterHeadEnum.RICOH_GEN4P_7pl == select
                 || SPrinterProperty.IsSG1024(select)
                 || SPrinterProperty.IsKyocera300(select)
                 || PrinterHeadEnum.Ricoh_Gen6 == select
                 || PrinterHeadEnum.Konica_M600SH_2C == select
                )
                // && (headBoardType != HEAD_BOARD_TYPE.SG1024_4H_GRAY && headBoardType != HEAD_BOARD_TYPE.SG1024_8H_GRAY_2BIT)
                )
            {
                return true;
            }
            return false;
        }

        private bool bShowOneHeadDivider = false;

        private void m_ComboBoxHeadType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int select = ((VenderDisp)m_SupportHeadList[m_ComboBoxHeadType.SelectedIndex]).VenderID;
            if (SurpportOneHeadDivider((PrinterHeadEnum)select))
            {
                bShowOneHeadDivider = true;
            }
            else
            {
                bShowOneHeadDivider = false;
            }
            m_CheckBoxOneHeadDivider.Visible = bShowOneHeadDivider;
            if (!bShowOneHeadDivider)
                m_CheckBoxOneHeadDivider.Checked = false;

            //bool bExFWFactoryData =
            //    (int) PrinterHeadEnum.EGen5 == select
            //    || (int) PrinterHeadEnum.RICOH_GEN4_7pl == select
            //    || (int) PrinterHeadEnum.RICOH_GEN4L_15pl == select
            //    || (int) PrinterHeadEnum.RICOH_GEN4P_7pl == select;
            this.numericUpDownW.Visible = true;
            this.m_ComboBoxWidth.Visible = false;
            //if (bExFWFactoryData)
            //{
            //    m_bEpson = (int) PrinterHeadEnum.EGen5 == select;
            //    this.m_CheckBoxSupportLcd.Visible = true;
            //    this.OnEpsonExFWFactoryDataChanged(epsonExFac);
            //}
            //else
            {
                m_bEpson = false;
                this.m_CheckBoxSupportLcd.Visible = false;
                this.m_CheckBoxSupportLcd.Checked = false;
            }
#if !COLORORDER
            this.grouperPrintColorOrder.Visible = false;
                this.grouperRipColorOrder.Visible =false;
#endif
            //			if(select == PrinterHeadEnum.Xaar_Proton382_15pl.ToString()
            //				||select == PrinterHeadEnum.Xaar_Proton382_35pl.ToString()
            //				||select == PrinterHeadEnum.Xaar_Proton382_60pl.ToString()
            //				)
            //				this.groupBoxDualBank.Visible = true;
            //			else
            this.groupBoxDualBank.Visible = false;
        }

        private void m_CheckBoxOneHeadDivider_CheckedChanged(object sender, System.EventArgs e)
        {
            if (m_ComboBoxHeadType.SelectedIndex < 0) return;
            int CurHead = ((VenderDisp) m_SupportHeadList[m_ComboBoxHeadType.SelectedIndex]).VenderID;
            int select = CurHead;
            //string select = m_ComboBoxHeadType.Items[m_ComboBoxHeadType.SelectedIndex].ToString();
            if ((SPrinterProperty.IsPolaris((PrinterHeadEnum)select)
                 || SPrinterProperty.IsSG1024((PrinterHeadEnum)select)
                ) && this.m_CheckBoxOneHeadDivider.Checked)
            {
                this.m_CheckBoxMirror.Visible = true;
                //this.checkBox4ColorMirror.Checked = checkBox4ColorMirror.Visible;
            }
            else
            {
                this.m_CheckBoxMirror.Checked = false;
                this.m_CheckBoxMirror.Visible = false;
            }
        }

        private void SetValueIndex(ref ComboBox cbo, uint value, ushort samplePoint)
        {
            bool isSamplePoint = false;
            double fPitch = 0;
            if (samplePoint != 0)
            {
                isSamplePoint = true;
                fPitch = ((double)samplePoint / (double)value) * 25.4;
            }

            for (int i = 0; i < cbo.Items.Count; i++)
            {
                if (isSamplePoint)
                {
                    if (((string)cbo.Items[i]).Split('[')[0] == value.ToString() && ((string)cbo.Items[i]).IndexOf("(" + fPitch.ToString("G")) > 0)
                    {
                        cbo.SelectedIndex = i;
                        return;
                    }
                }
                else
                {
                    if (((string)cbo.Items[i]).Split('[')[0] == value.ToString())
                    {
                        cbo.SelectedIndex = i;
                        return;
                    }
                }
            }
            cbo.SelectedIndex = 5;
        }

        private bool IsAmendProperty(SPrintAmendProperty property)
        {
            if (property.bUseful == CoreConst.BeEnableConstMark //0x19ED5500//&& property.uPrintSense >= 0
                //&&property.uPrintSense<=10000000
                //&& property.uRasterSense >= 0
                //&& property.uRasterSense <= 10000000
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsIncludePrintSence(string value)
        {
            foreach (string a in combobox_RasterSence.Items)
            {
                if (a.Split('[')[0] == value)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsUserInfo(USER_SET_INFORMATION userInfo)
        {
            if (userInfo.Flag == CoreConst.BeEnableConstMark //0x19ED5500
                //&&userInfo.AccSpaceL>=0
                //&&userInfo.AccSpaceL<=(uint)(UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(100)) * userInfo.uRasterSense)
                //&&userInfo.FlatSpace >=0
                //&&userInfo.FlatSpace<=(uint)(UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(100)) * userInfo.uRasterSense)
                //&&IsIncludePrintSence(userInfo.uRasterSense.ToString())
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnUserinfoChanged(USER_SET_INFORMATION userInfo)
        {
            UIPreference.SetValueAndClampWithMinMax(numericUpDown_PrintSence, userInfo.uPrintSense);
            UIPreference.SetValueAndClampWithMinMax(numFlatDistance, m_CurrentUnit,
                (float)userInfo.FlatSpace / (float)userInfo.uRasterSense);
            UIPreference.SetValueAndClampWithMinMax(numSpeedUpDistanceL, m_CurrentUnit,
                (float) userInfo.AccSpaceL/(float) userInfo.uRasterSense);
            UIPreference.SetValueAndClampWithMinMax(numSpeedUpDistanceR, m_CurrentUnit,
                (float)userInfo.AccSpaceR / (float)userInfo.uRasterSense);
            UIPreference.SetValueAndClampWithMinMax(numericUpDown_YMaxLen, m_CurrentUnit,
                (float)userInfo.yMaxLen);
            SetValueIndex(ref combobox_RasterSence, userInfo.uRasterSense,userInfo.SamplePoint);
            float fPulsePerInchZ = m_sPrinterProperty.fPulsePerInchZ;
            if (fPulsePerInchZ != 0)
                UIPreference.SetValueAndClampWithMinMax(numDefaultZ, m_CurrentUnit,
                    (float) userInfo.zDefault/fPulsePerInchZ);
            if (fPulsePerInchZ != 0)
                UIPreference.SetValueAndClampWithMinMax(numzMaxRoute, m_CurrentUnit,
                    (float) userInfo.zMaxRoute/fPulsePerInchZ);

            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownVOffset, m_CurrentUnit, userInfo.fVOffset);
            checkBoxMotorDebug.Checked = userInfo.motorDebug == 1;
            if (CoreInterface.IsS_system())
            {
                comboBoxTopologyMode.SelectedIndex = userInfo.Topology;
                //numericUpDownHbDataByteWidth.Value = userInfo.HeadBoardDataByteWidth;
                numericUpDownHBNum.Value = userInfo.HeadBoardNum;
                //numericUpDownHeadBoardNumCB1.Value = userInfo.B1HbDataMap;
                //numericUpDownHeadBoardNumCB2.Value = userInfo.B2HbDataMap;
                checkBoxPort11.Checked = (userInfo.HeadPortMask & (1)) != 0;
                checkBoxPort12.Checked = (userInfo.HeadPortMask & (1 << 1)) != 0;
                checkBoxPort13.Checked = (userInfo.HeadPortMask & (1 << 2)) != 0;
                checkBoxPort14.Checked = (userInfo.HeadPortMask & (1 << 3)) != 0;

                checkBoxPort21.Checked = (userInfo.HeadPortMask & (1 << 4)) != 0;
                checkBoxPort22.Checked = (userInfo.HeadPortMask & (1 << 5)) != 0;
                checkBoxPort23.Checked = (userInfo.HeadPortMask & (1 << 6)) != 0;
                checkBoxPort24.Checked = (userInfo.HeadPortMask & (1 << 7)) != 0;
            }
        }

        private void OnAmentDataChanged(SPrintAmendProperty property)
        {
#if false
            if (!IsAmendProperty(property))
            {
                property = new SPrintAmendProperty(true);
            }
#endif
            //this.numericUpDown_PrintSence.Value = property.uPrintSense;

            int len = this.m_ComboxBoxRipColorOder.Length;

            if (property.pRipColorOrder != null)
            {
                //反置riColorOder
                byte[] pDeverse = new byte[property.pRipColorOrder.Length];
                for (int i = 0; i < len; i++)
                {
                    pDeverse[i] = property.pRipColorOrder[len - 1 - i];
                }
                for (int i = 0; i < this.m_ComboxBoxRipColorOder.Length; i++)
                {
                    bool bfind = false;
                    for (int j = 0; j < this.m_ComboxBoxRipColorOder[i].Items.Count; j++)
                    {
                        string itemtext = this.m_ComboxBoxRipColorOder[i].Items[j].ToString();
                        itemtext = itemtext.Split('[')[0];
                        //						string ordertext = (Enum.Parse(typeof(ColorEnum_Short),((char)eprfd.m_nColorOrder[i]).ToString())).ToString();
                        ColorEnum_Short[] values = (ColorEnum_Short[]) Enum.GetValues(typeof (ColorEnum_Short));
                        for (int k = 0; k < values.Length; k++)
                        {
                            if (values[k].ToString() == itemtext)
                            {
                                if ((byte) values[k] == pDeverse[i])
                                {
                                    UIPreference.SetSelectIndexAndClampWithMax(this.m_ComboxBoxRipColorOder[i], j);
                                    bfind = true;
                                    break;
                                }
                            }
                        }
                        string ordertext = string.Format("S{0}", ((char) (pDeverse[i] + 1)).ToString());
                        if (itemtext.Equals(ordertext))
                        {
                            UIPreference.SetSelectIndexAndClampWithMax(this.m_ComboxBoxRipColorOder[i], j);
                            bfind = true;
                            break;
                        }
                    }
                    if (!bfind)
                    {
                        UIPreference.SetSelectIndexAndClampWithMax(this.m_ComboxBoxRipColorOder[i], i);
                    }
                }
            }
        }

        private void OnEpsonExFWFactoryDataChanged(EPR_FactoryData_Ex eprfd)
        {
            switch (eprfd.m_nXEncoderDPI)
            {
                case 1440:
                    UIPreference.SetSelectIndexAndClampWithMax(comboBoxXEncoderDPI_2, 0);
                    break;
                case 1270:
                    UIPreference.SetSelectIndexAndClampWithMax(comboBoxXEncoderDPI_2, 1);
                    break;
                case 1200:
                    UIPreference.SetSelectIndexAndClampWithMax(comboBoxXEncoderDPI_2, 2);
                    break;
                case 726:
                    UIPreference.SetSelectIndexAndClampWithMax(comboBoxXEncoderDPI_2, 3);
                    break;
                case 720:
                    UIPreference.SetSelectIndexAndClampWithMax(comboBoxXEncoderDPI_2, 4);
                    break;
                case 635:
                    UIPreference.SetSelectIndexAndClampWithMax(comboBoxXEncoderDPI_2, 5);
                    break;
                case 600:
                    UIPreference.SetSelectIndexAndClampWithMax(comboBoxXEncoderDPI_2, 6);
                    break;
                case 540:
                    UIPreference.SetSelectIndexAndClampWithMax(comboBoxXEncoderDPI_2, 7);
                    break;
                case 480:
                    UIPreference.SetSelectIndexAndClampWithMax(comboBoxXEncoderDPI_2, 8);
                    break;
            }

            UIPreference.SetSelectIndexAndClampWithMax(comboBoxLayoutType_2, eprfd.LayoutType & 1);
            this.checkBoxSymmetricColorOder_2.Checked = (eprfd.LayoutType & (1 << 1)) > 0;
            UIPreference.SetSelectIndexAndClampWithMax(comboBoxSymmetricType_2, ((eprfd.LayoutType & (1 << 2)) == 0) ? 0 : 1);
            this.checkBoxWrapAroundOnX_2.Checked = (eprfd.LayoutType & (1 << 3)) == 0;

            if (checkBoxWrapAroundOnX_2.Checked)
            {
                cbxWrapHead_2.Enabled = true;
                if ((eprfd.reserved & 0x01) == 0)
                {
                    cbxWrapHead_2.SelectedIndex = 1;
                }
                else
                {
                    cbxWrapHead_2.SelectedIndex = 0;
                }
            }
            else
            {
                cbxWrapHead_2.Enabled = false;
            }

            UIPreference.SetSelectIndexAndClampWithMax(comboBoxWhiteVarnishLayout_2, eprfd.LayoutType >> 4);

            {
                Encoding gb = Encoding.GetEncoding("gb2312");
                if (eprfd.ManufacturerName != null)
                {
                    //					byte[] ManufacturerName = new byte[txtManufacturerName.MaxLength];
                    //					eprfd.ManufacturerName.CopyTo(ManufacturerName,0);
                    txtManufacturerName_2.Text = gb.GetString(eprfd.ManufacturerName); // new string(ManufacturerName);
                }
                if (eprfd.PrinterName != null)
                {
                    //					char[] PrinterName = new char[txtPrinterName.MaxLength];
                    //					eprfd.PrinterName.CopyTo(PrinterName,0);
                    txtPrinterName_2.Text = gb.GetString(eprfd.PrinterName);// new string(PrinterName);
                }
            }


            numericUpDownYInterleaveNum_2.Value = eprfd.YInterleaveNum;
            checkBoxHeadDir_2.Checked = ((eprfd.m_nBitFlagEx & 0x1) != 0);
            checkBoxWeakSovent_2.Checked = ((eprfd.m_nBitFlagEx & 0x2) != 0);

            
            UIPreference.SetSelectIndexAndClampWithMax(this.comboBoxMaxGroupNumber_2, eprfd.MaxGroupNumber - 1);

            m_ComboBoxBit2Mode_2.SelectedIndex = eprfd.Vsd2ToVsd3_ColorDeep;

            m_ComboBoxSpeed_2.SelectedIndex = eprfd.Vsd2ToVsd3;


            if (eprfd.m_nColorOrder != null)
            {
                for (int i = 0; i < this.m_ComboBoxColorOder.Length; i++)
                {
                    if (i >= 8)
                        break;

                    bool bfind = false;
                    for (int j = 0; j < this.m_ComboBoxColorOder[i].Items.Count; j++)
                    {
                        string itemtext = this.m_ComboBoxColorOder[i].Items[j].ToString();
                        //						string ordertext = (Enum.Parse(typeof(ColorEnum_Short),((char)eprfd.m_nColorOrder[i]).ToString())).ToString();
                        ColorEnum_Short[] values = (ColorEnum_Short[])Enum.GetValues(typeof(ColorEnum_Short));
                        for (int k = 0; k < values.Length; k++)
                        {
                            if (values[k].ToString() == itemtext)
                            {
                                if ((byte)values[k] == eprfd.m_nColorOrder[i])
                                {
                                    UIPreference.SetSelectIndexAndClampWithMax(this.m_ComboBoxColorOder[i], j);
                                    bfind = true;
                                    break;
                                }
                            }
                        }
                        string ordertext = string.Format("S{0}", ((char)(eprfd.m_nColorOrder[i] + 1)).ToString());
                        if (itemtext.Equals(ordertext))
                        {
                            UIPreference.SetSelectIndexAndClampWithMax(this.m_ComboBoxColorOder[i], j);
                            bfind = true;
                            break;
                        }
                    }
                    if (!bfind)
                    {
                        UIPreference.SetSelectIndexAndClampWithMax(this.m_ComboBoxColorOder[i], i);
                    }
                }
            }
        }

        private bool OnGetUserInfo(ref USER_SET_INFORMATION userInfo)
        {
#if RW_AMEND_PARAMETERS
            userInfo.Flag = CoreConst.BeEnableConstMark;
#else
            userInfo.Flag = 0;
#endif
            userInfo.uPrintSense = (uint) this.numericUpDown_PrintSence.Value;
            userInfo.uRasterSense = Convert.ToUInt32(this.combobox_RasterSence.Text.ToString().Split('[')[0]);

            ushort samplePoint = 0;
            if (this.combobox_RasterSence.Text.ToString().IndexOf("(") > 0)
            {
                double pitch = 0; 
                try
                {
                    string tmp = this.combobox_RasterSence.Text.ToString().Substring(this.combobox_RasterSence.Text.ToString().IndexOf("("),this.combobox_RasterSence.Text.ToString().IndexOf(")") - this.combobox_RasterSence.Text.ToString().IndexOf("(")).Replace("(","").Replace(")","");
                    pitch = Convert.ToDouble(tmp.Trim());
                    samplePoint = (ushort)(userInfo.uRasterSense * pitch / 25.4);
                }
                catch
                {
                    samplePoint = 0;
                }

            }
            userInfo.SamplePoint = samplePoint;

            userInfo.AccSpaceL =
                (uint)
                    (UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(numSpeedUpDistanceL.Value))*
                     userInfo.uRasterSense);
            userInfo.AccSpaceR =
                (uint)
                    (UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(numSpeedUpDistanceR.Value))*
                     userInfo.uRasterSense); // (uint)this.numericUpDown_SpeedUpDistance.Value;
            userInfo.FlatSpace =
                (uint)
                    (UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(numFlatDistance.Value))*
                     userInfo.uRasterSense);
            userInfo.zDefault =
                (uint)
                    (UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(numDefaultZ.Value))*
                     m_sPrinterProperty.fPulsePerInchZ);
            userInfo.zMaxRoute =
                (uint)
                    (UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(numzMaxRoute.Value))*
                     m_sPrinterProperty.fPulsePerInchZ);
            userInfo.yMaxLen =UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(numericUpDown_YMaxLen.Value));
            userInfo.bSupportZendPointSensor= checkBoxSupportZendPointSensor.Checked;
            userInfo.motorDebug = (byte)(checkBoxMotorDebug.Checked ? 1 : 0);
            userInfo.fVOffset = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownVOffset.Value));
            userInfo.HeadBoardNum = (byte)1;
            if (CoreInterface.IsS_system())
            {
                int colorDeep = PubFunc.GetColorDeep();
                HEAD_BOARD_TYPE hbType = (HEAD_BOARD_TYPE) CoreInterface.get_HeadBoardType(true);

                #region 特殊处理：个别头板只支持1bit或2bit

                if (hbType == HEAD_BOARD_TYPE.SG1024_4H || hbType == HEAD_BOARD_TYPE.SG1024_8H_GRAY_1BIT)
                {
                    colorDeep = 1;
                }
                //else if (hbType == HEAD_BOARD_TYPE.SG1024_4H_GRAY)
                //{
                //    colorDeep = 2;
                //}

                #endregion

                int headBoardNum = (int) numericUpDownHBNum.Value;
                int colornumber = m_ColorNumList[m_ComboBoxColorNumber.SelectedIndex];
                int nGroupNum = (sbyte) (m_ComboBoxGroupNumber.SelectedIndex + 1);
                int nWhiteInkNum = Decimal.ToByte(m_NumericUpDownWhiteColorNum.Value);
                int nOverCoatInkNum = Decimal.ToByte(m_NumericUpDownCoatColorNum.Value);
                int colorNum = colornumber + nWhiteInkNum + nOverCoatInkNum;
                int headCount = colorNum*nGroupNum;
                int fiberCount = comboBoxTopologyMode.SelectedIndex == 0 ? 1 : 2;
                int expansionBoardCount = (comboBoxTopologyMode.SelectedIndex == 0 || comboBoxTopologyMode.SelectedIndex == 1) ? 1 : 2;


                int select = ((VenderDisp)m_SupportHeadList[m_ComboBoxHeadType.SelectedIndex]).VenderID;
                PrinterHeadEnum selectedHead = (PrinterHeadEnum)select;

                if (selectedHead == PrinterHeadEnum.Epson_5113 || selectedHead == PrinterHeadEnum.EPSON_I3200)
                {
                    headCount /= 4;
                }
                else
                {
                    if (bShowOneHeadDivider && m_CheckBoxOneHeadDivider.Checked)
                    {
                        headCount /= 2;
                    }
                }

                var matchSystemConfigs =
                    SystemConfigMap.Where(
                        s =>
                            s.PrinterHeadEnum == selectedHead && s.HeadBoardType == hbType &&
                            (s.HBCountOfExBoardNO1 + s.HBCountOfExBoardNO2) == headBoardNum
                            && s.FiberCount == fiberCount
                            && s.ExpansionBoardCount == expansionBoardCount
                            && s.HeadCount >= headCount
                    );
                if (!matchSystemConfigs.Any())
                {
                    MessageBox.Show(
                        string.Format("The current system configuration is wrong：PrinterHeadEnum={0},HeadBoardType={1},HeadBoardNum={2}", selectedHead,
                            hbType, headBoardNum), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                var matchSystemConfig = matchSystemConfigs.OrderBy(q => q.HeadCount).First(); 
                if (matchSystemConfig.HeadCount < headCount)
                {
                    MessageBox.Show(
                        string.Format("The current system configuration is wrong：The current head number [{0}] more than supported the maximum number of head[{1}]", headCount,
                            matchSystemConfig.HeadCount), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }


                userInfo.HeadBoardNum = (byte) headBoardNum;
                userInfo.Topology = matchSystemConfig.ToplogyMode;
                if (colorDeep == 2)
                {
                    userInfo.HeadBoardDataByteWidth = (byte) (matchSystemConfig.HeadBoardDataWidth2 - 1);
                }
                else //colorDeep==1或者==-1（非灰度，获取失败）
                {
                    userInfo.HeadBoardDataByteWidth = (byte) (matchSystemConfig.HeadBoardDataWidth1 - 1);
                }

                userInfo.B1HbDataMap = matchSystemConfig.B1HbDataMap;
                userInfo.B2HbDataMap = matchSystemConfig.B2HbDataMap;
                //userInfo.B1HbDataMap = (byte)numericUpDownHeadBoardNumCB1.Value;
                int mask = 0;
                mask |= checkBoxPort11.Checked ? 1 : 0;
                mask |= checkBoxPort12.Checked ? (1 << 1) : 0;
                mask |= checkBoxPort13.Checked ? (1 << 2) : 0;
                mask |= checkBoxPort14.Checked ? (1 << 3) : 0;
                if (userInfo.Topology == 2)
                {
                    //userInfo.B2HbDataMap = (byte)numericUpDownHeadBoardNumCB2.Value;
                    mask |= checkBoxPort21.Checked ? (1 << 4) : 0;
                    mask |= checkBoxPort22.Checked ? (1 << 5) : 0;
                    mask |= checkBoxPort23.Checked ? (1 << 6) : 0;
                    mask |= checkBoxPort24.Checked ? (1 << 7) : 0;
                }
                userInfo.HeadPortMask = (byte) mask;
                //if (matchSystemConfig.ExpansionBoardCount == 1)
                //{
                //    MessageBox.Show(
                //        string.Format("配置的系统拓扑结构为：PrinterHeadEnum={0},HeadBoardType={1};\n{2}条光纤,1个扩展板,{3}个头板",
                //            selectedHead, hbType, matchSystemConfig.FiberCount, matchSystemConfig.HBCountOfExBoardNO1),
                //        "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else if (matchSystemConfig.ExpansionBoardCount == 2)
                //{
                //    MessageBox.Show(
                //        string.Format(
                //            "配置的系统拓扑结构为：PrinterHeadEnum={0},HeadBoardType={1};\n{2}条光纤,2个扩展板,扩展板1:{3}个头板,扩展板2:{4}个头板",
                //            selectedHead, hbType, matchSystemConfig.FiberCount, matchSystemConfig.HBCountOfExBoardNO1,
                //            matchSystemConfig.HBCountOfExBoardNO2), "提示", MessageBoxButtons.OK,
                //        MessageBoxIcon.Information);
                //}
            }
            return true;
        }

        private void OnGetAmentProperty(ref SPrintAmendProperty property)
        {
            property = new SPrintAmendProperty(false);
            //int colorLen = fwData.eColorOrder.Length;
            int colorLen = m_ComboxBoxRipColorOder.Length;
            for (int i = 0; i < colorLen; i++)
            {
                property.pRipColorOrder[i] = getColorOrder(m_ComboxBoxRipColorOder[colorLen - 1 - i]);
                    // fwData.eColorOrder[colorLen - 1 - i];
            }
        }

        private void OnGetEpsonExFWFactoryData(ref EPR_FactoryData_Ex eprfd)
        {
            eprfd.len = (byte)Marshal.SizeOf(typeof(EPR_FactoryData_Ex));
            eprfd.version = 0x02;


            switch (comboBoxXEncoderDPI_2.SelectedIndex)
            {
                case 0:
                    eprfd.m_nXEncoderDPI = (ushort)1440;
                    break;
                case 1:
                    eprfd.m_nXEncoderDPI = (ushort)1270;
                    break;
                case 2:
                    eprfd.m_nXEncoderDPI = (ushort)1200;
                    break;
                case 3:
                    eprfd.m_nXEncoderDPI = (ushort)726;
                    break;
                case 4:
                    eprfd.m_nXEncoderDPI = (ushort)720;
                    break;
                case 5:
                    eprfd.m_nXEncoderDPI = (ushort)635;
                    break;
                case 6:
                    eprfd.m_nXEncoderDPI = (ushort)600;
                    break;
                case 7:
                    eprfd.m_nXEncoderDPI = (ushort)540;
                    break;
                case 8:
                    eprfd.m_nXEncoderDPI = (ushort)480;
                    break;
            }

            int nBitFlagEx = 0;
            if (checkBoxHeadDir_2.Checked)
                nBitFlagEx |= 0x1;
            if (checkBoxWeakSovent_2.Checked)
                nBitFlagEx |= 0x2;
            eprfd.m_nBitFlagEx = nBitFlagEx;

            for (int i = 0; i < this.m_ComboBoxColorOder.Length; i++)
            {
                if (i < eprfd.m_nColorOrder.Length)
                    eprfd.m_nColorOrder[i] = getColorOrder_2(this.m_ComboBoxColorOder[i]);
            }

            eprfd.YInterleaveNum = (byte)this.numericUpDownYInterleaveNum_2.Value;
            //			if(this.comboBoxLayoutType.SelectedIndex != 0)
            //			{
            int layoutType = this.comboBoxLayoutType_2.SelectedIndex;
            if (this.checkBoxSymmetricColorOder_2.Checked)
                layoutType |= (1 << 1);
            layoutType |= (this.comboBoxSymmetricType_2.SelectedIndex << 2);
            if (!this.checkBoxWrapAroundOnX_2.Checked)
                layoutType |= (1 << 3);
            else
            {
                if (cbxWrapHead_2.SelectedIndex == 0)
                {
                    eprfd.reserved |= 0x01;
                }
                else if (cbxWrapHead_2.SelectedIndex == 1)
                {
                    eprfd.reserved &= 0xfe;
                }
            }

            int whiteVarnishLayout = comboBoxWhiteVarnishLayout_2.SelectedIndex;
            layoutType |= (whiteVarnishLayout << 4);
            eprfd.LayoutType = (byte)layoutType;

            int offset = 0;
            Encoding gb = Encoding.GetEncoding("gb2312");
            gb.GetBytes(this.txtManufacturerName_2.Text, 0, this.txtManufacturerName_2.Text.Length, eprfd.ManufacturerName, 0);
            gb.GetBytes(this.txtPrinterName_2.Text, 0, this.txtPrinterName_2.Text.Length, eprfd.PrinterName, 0);
            eprfd.MaxGroupNumber = (byte)(this.comboBoxMaxGroupNumber_2.SelectedIndex + 1);

            eprfd.Vsd2ToVsd3_ColorDeep = Convert.ToByte(m_ComboBoxBit2Mode_2.SelectedIndex);
            eprfd.Vsd2ToVsd3 = Convert.ToByte(m_ComboBoxSpeed_2.SelectedIndex);
            //eprfd.Only_Used_1head = (byte)(this.checkBoxOnlyUseOneHead.Checked ? 1 : 0);
            //if (this.comboBoxUsedHead.SelectedIndex != -1)
            //    eprfd.Mask_head_used = (byte)(1 << this.comboBoxUsedHead.SelectedIndex);

        }

        private byte getColorOrder_2(ComboBox cbo)
        {
            byte ret = 0;

			foreach (string name in Enum.GetNames(typeof(ColorEnum_Short)))
			{
				if (name == cbo.Text)
				{
					ret = (byte)Enum.Parse(typeof(ColorEnum_Short), name, true);
					break;
				}
			}
            if (ret == 0)
            {
                if (cbo.SelectedIndex - 4 < 10)
                {
                    ret = (byte)((cbo.SelectedIndex - 4) + '0');
                }
                else
                {
                    switch (cbo.SelectedIndex - 4)
                    {
                        case 10:
                            ret = (byte)('a');
                            break;
                        case 11:
                            ret = (byte)('b');
                            break;
                    }
                }

            }

            return ret;
        }

        private byte getColorOrder(ComboBox cbo)
        {
            byte ret = 0;
            string itemtext = cbo.Text.ToString();
            itemtext = itemtext.Split('[')[0];
#if COLORORDER
            foreach (string name in Enum.GetNames(typeof (ColorEnum_Short)))
            {
                if (name == itemtext)
                {
                    ret = (byte) Enum.Parse(typeof (ColorEnum_Short), name, true);
                    break;
                }
            }
#else
			foreach(string name in Enum.GetNames(typeof(ColorEnum_Short)))
			{	
				if(name == itemtext)
				{
					ret =  (byte)Enum.Parse(typeof(ColorEnum_Short),name,true);
					break;
				}
			}
			if(ret == 0)
				ret = (byte)((cbo.SelectedIndex % 4) + '0'); // 字符的'0','1','2','3'
#endif
            return ret;
        }

        private void m_CheckBoxSupportLcd_CheckedChanged(object sender, System.EventArgs e)
        {
            //			if(this.m_CheckBoxSupportLcd.Checked)
            //			{
            //				EPR_FactoryData_Ex epsonExFac = new EPR_FactoryData_Ex(null);
            //				bool bGetepson = EpsonLCD.GetEPR_FactoryData_Ex(ref epsonExFac)>0;
            //				this.OnEpsonExFWFactoryDataChanged(epsonExFac);
            //				if(!this.tabControl1.TabPages.Contains(this.tabPage2))
            //				{
            //					this.tabControl1.TabPages.Add(this.tabPage2);
            //					this.tabControl1.SelectedTab = this.tabPage2;
            //				}
            //			}
            //			else
            //			{
            //				if(this.tabControl1.TabPages.Contains(this.tabPage2))
            //				{
            //					this.tabControl1.TabPages.Remove(this.tabPage2);
            //				}
            //			}

        }


        private void LayoutColorOders(SPrinterProperty sp, int colornumAll)
        {
            int whiteColorNum = (int)m_NumericUpDownWhiteColorNum.Value;
            int coatColorNum = (int)m_NumericUpDownCoatColorNum.Value;
            int colorNum = colornumAll - whiteColorNum - coatColorNum;
            int space_x = 5;
            int start_x = comboBoxColorOderSample.Location.X;
            int start_y = comboBoxColorOderSample.Location.Y;
            int start_x1 = comboxRipcolor.Location.X;
            int start_y1 = comboxRipcolor.Location.Y;
            int w = (this.grouperPrintColorOrder.Width - (colornumAll - 1)*space_x - 2*start_x)/colornumAll;
            grouperPrintColorOrder.SuspendLayout();
            grouperRipColorOrder.SuspendLayout();
            this.SuspendLayout();
            // 初始化print coloroder
            if (m_ComboBoxPrintColorOder != null && m_ComboBoxPrintColorOder.Length > 0)
            {
                for (int i = 0; i < m_ComboBoxPrintColorOder.Length; i++)
                    this.grouperPrintColorOrder.Controls.Remove(m_ComboBoxPrintColorOder[i]);
            }
            m_ComboBoxPrintColorOder = new ComboBox[colornumAll];
            if (m_ComboBoxPrintColorOder != null && m_ComboBoxPrintColorOder.Length != 0)
            {
                foreach (Control c in this.m_ComboBoxPrintColorOder)
                {
                    grouperPrintColorOrder.Controls.Remove(c);
                }
            }

            for (int i = 0; i < colornumAll; i++)
            {
                m_ComboBoxPrintColorOder[i] = new ComboBox();
                ComboBox cbo = m_ComboBoxPrintColorOder[i];
                cbo.Width = w;
                cbo.Visible = true;
#if COLORORDER
                cbo.Location = new Point(start_x + (w + space_x)*(colornumAll - 1 - i), start_y);

                ColorEnum_Short[] names = (ColorEnum_Short[]) Enum.GetValues(typeof (ColorEnum_Short));
                int selectindex = 0;
                for (int j = 0; j < names.Length; j++)
                {
                    string text = string.Format("{0}[{1}]", names[j],
                        ResString.GetEnumDisplayName(typeof (ColorEnum), (ColorEnum) names[j]));
                    cbo.Items.Add(text);
                    if (i < colorNum)
                    {
                        if (defaultPrintColorOrder.Length > i && defaultPrintColorOrder[i] == names[j])
                            selectindex = j;
                    }
                    else
                    {
                        if (i < colorNum + whiteColorNum)
                        {
                            selectindex = 11; //W
                        }
                        else
                        {
                            if (i < colorNum + whiteColorNum +coatColorNum)
                            {
                                selectindex = 10; //V
                            }
                        }
                    }
                }
                cbo.SelectedIndex = selectindex;
#else
                cbo.Location = new Point(start_x + (w + space_x) * (i), start_y);
				string[] itemNames = Enum.GetNames(typeof(DefaultColorOrderEnum));
				for (int j=0; j< 4; j++)
				{
                    cbo.Items.Add(itemNames[j]);
				}
				for (int j=0; j<colornum-4; j++)
				{
					string cmode = string.Format("S{0}",j + 1);
					cbo.Items.Add(cmode);
                }
                cbo.SelectedIndex = i;
#endif
                this.grouperPrintColorOrder.Controls.Add(cbo);
            }

            // 初始化rip coloroder
            colornumAll = CoreConst.MAX_COLOR_NUM;
            int w1 = (this.grouperPrintColorOrder.Width - (colornumAll - 1)*space_x - 2*start_x1)/colornumAll;
            if (m_ComboxBoxRipColorOder != null && m_ComboxBoxRipColorOder.Length > 0)
            {
                for (int i = 0; i < m_ComboxBoxRipColorOder.Length; i++)
                    this.grouperRipColorOrder.Controls.Remove(m_ComboxBoxRipColorOder[i]);
            }
            m_ComboxBoxRipColorOder = new ComboBox[colornumAll];
            if (m_ComboxBoxRipColorOder != null && m_ComboxBoxRipColorOder.Length != 0)
            {
                foreach (Control c in this.m_ComboxBoxRipColorOder)
                {
                    grouperRipColorOrder.Controls.Remove(c);
                }
            }
            for (int i = 0; i < colornumAll; i++)
            {
                //rip色序
                m_ComboxBoxRipColorOder[i] = new ComboBox();
                ComboBox rcbo = m_ComboxBoxRipColorOder[i];
                rcbo.Width = w1;
                rcbo.Visible = true;
                rcbo.Location = new Point(start_x1 + (w1 + space_x)*(colornumAll - 1 - i), start_y1);
                ColorEnum_Short[] rnames = (ColorEnum_Short[]) Enum.GetValues(typeof (ColorEnum_Short));
                for (int j = 0; j < rnames.Length; j++)
                {
                    string text = string.Format("{0}[{1}]", rnames[j],
                        ResString.GetEnumDisplayName(typeof (ColorEnum), (ColorEnum) rnames[j]));
                    rcbo.Items.Add(text);
                }
                rcbo.SelectedIndex = i;
                this.grouperRipColorOrder.Controls.Add(rcbo);
            }
            grouperPrintColorOrder.ResumeLayout(false);
            grouperRipColorOrder.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.ComboBox[] m_ComboBoxColorOder;
        private void LayoutColorOders_2(SPrinterProperty sp, int colornum, int WhiteInkNum, int OverCoatInkNum)
        {
            int space_x = 5;
            int start_x = comboBoxColorOderSample.Location.X;
            int start_y = comboBoxColorOderSample.Location.Y;

            //if (colornum > 8)
            //{
            //    colornum = colornum - WhiteInkNum - OverCoatInkNum;
            //}

            int w = (this.grouperColorOrder_2.Width - (colornum - 1) * space_x - 2 * start_x) / colornum;
            grouperColorOrder_2.SuspendLayout();
            this.SuspendLayout();
            if (m_ComboBoxColorOder != null && m_ComboBoxColorOder.Length > 0)
            {
                for (int i = 0; i < m_ComboBoxColorOder.Length; i++)
                    this.grouperColorOrder_2.Controls.Remove(m_ComboBoxColorOder[i]);
            }
            m_ComboBoxColorOder = new ComboBox[colornum];
            if (m_ComboBoxColorOder != null && m_ComboBoxColorOder.Length != 0)
            {
                foreach (Control c in this.m_ComboBoxColorOder)
                {
                    grouperColorOrder_2.Controls.Remove(c);
                }
            }
            for (int i = 0; i < colornum; i++)
            {
                m_ComboBoxColorOder[i] = new ComboBox();
                ComboBox cbo = m_ComboBoxColorOder[i];
                cbo.Width = w;
                cbo.Visible = true;

                cbo.Location = new Point(start_x + (w + space_x) * (i), start_y);
                string[] names = Enum.GetNames(typeof(DefaultColorOrderEnum));
                for (int j = 0; j < 4; j++)
                {
                    cbo.Items.Add(names[j]);
                }
                for (int j = 0; j < colornum - 4; j++)
                {
                    string cmode = string.Format("S{0}", j + 1);
                    cbo.Items.Add(cmode);
                }

                cbo.SelectedIndex = i;
                this.grouperColorOrder_2.Controls.Add(cbo);
            }
            grouperColorOrder_2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void m_CheckBoxMirror_CheckedChanged(object sender, EventArgs e)
        {
            UpdateColorarrAngementVisible();
        }

        private void UpdateColorarrAngementVisible()
        {
            bool bShowMirror = !(m_CheckBoxMirror.Checked && m_CheckBoxOneHeadDivider.Checked);
            this.checkBox4ColorMirror.Visible = bShowMirror;
            //this.checkBox4ColorMirror.Checked = m_CheckBoxOneHeadDivider.Checked && !m_CheckBoxMirror.Checked;
            this.groupBox8colorarrangement.Visible = checkBox_VerY.Visible || bShowMirror;
        }

        private void grouper2_Enter(object sender, EventArgs e)
        {

        }

        private void comboBoxTopologyMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTopologyMode.SelectedIndex != 2)
            {
                panelCB2.Visible = false;
            }
            else
            {
                panelCB2.Visible = true;
            }
        }

        private void buttonHeadMix_Click(object sender, EventArgs e)
        {
            int headboardNum = 1;
            if (CoreInterface.IsS_system())
            {
                headboardNum = (int) numericUpDownHBNum.Value;
            }
            PrintHeadOrderTypeForm printHeadOrder = new PrintHeadOrderTypeForm(headboardNum,m_SupportHeadList);
            DialogResult dr = printHeadOrder.ShowDialog();
            if (dr == DialogResult.OK)
            {

            }
        }

        /// <summary>
        /// 从文件读取光栅列表
        /// </summary>
        private void InitRasterSencerList()
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "RasterListConfig.xml");
                if (File.Exists(path))
                {
                    SelfcheckXmlDocument xmldoc = new SelfcheckXmlDocument();
                    xmldoc.Load(path);
                    XmlElement node = xmldoc.DocumentElement;
                    XmlNodeList list = node.GetElementsByTagName(Thread.CurrentThread.CurrentUICulture.Name);
                    if (list == null || list.Count == 0)
                    {
                        list = node.GetElementsByTagName("en-US");
                    }
                    if (list != null && list.Count >= 1)
                    {
                        XmlNode subnode = list[0];
                        if (!string.IsNullOrEmpty(subnode.InnerText))
                        {
                            string[] items = subnode.InnerText.Split(new char[] {','},
                                StringSplitOptions.RemoveEmptyEntries);
                            combobox_RasterSence.Items.Clear();
                            for (int i = 0; i < items.Length; i++)
                            {
                                if (!string.IsNullOrEmpty(items[i].Trim()))
                                    combobox_RasterSence.Items.Add(items[i].Trim());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    /* <SystemConfig>
     <!--喷头类型-->
     <PrinterHeadEnum>Kyocera_KJ4B_0300_5pl_1h2c</PrinterHeadEnum>
     <!--头板类型-->
     <HeadBoardType>KYOCERA_4HEAD</HeadBoardType>
     <!--喷头个数-->
     <HeadCount>4</HeadCount>
     <!--光纤个数-->
     <FiberCount>1</FiberCount>
     <!--扩展板个数-->
     <ExpansionBoardCount>1</ExpansionBoardCount>
     <!--头板个数（1号扩展板）-->
     <HBCountOfExBoardNO1>1</HBCountOfExBoardNO1>
     <!--头板个数(2号扩展板)-->
     <HBCountOfExBoardNO2>0</HBCountOfExBoardNO2>
     <!--头板数据长度1-->
     <HeadBoardDataWidth1>2</HeadBoardDataWidth1>
     <!--头板数据长度2-->
     <HeadBoardDataWidth2>4</HeadBoardDataWidth2>
     <!--扩展板1光纤1数据传输头板端口-->
     <FirstSelectedPortPart1>0001</FirstSelectedPortPart1>
     <!--扩展板1光纤2数据传输头板端口-->
     <FirstSelectedPortPart2>0000</FirstSelectedPortPart2>
     <!--扩展板2光纤1数据传输头板端口-->
     <SecondSelectedPortPart1>0000</SecondSelectedPortPart1>
     <!--扩展板2光纤1数据传输头板端口-->
     <SecondSelectedPortPart2>0000</SecondSelectedPortPart2>
   </SystemConfig>
     */
    /// <summary>
    /// S系统 配置映射
    /// </summary>
    [Serializable]
    public class SystemConfig
    {
        /// <summary>
        /// 喷头类型
        /// </summary>
        public PrinterHeadEnum PrinterHeadEnum { get; set; }
        /// <summary>
        /// 头板类型
        /// </summary>
        public HEAD_BOARD_TYPE HeadBoardType { get; set; }
        /// <summary>
        /// 喷头个数
        /// </summary>
        public int HeadCount { get; set; }
        /// <summary>
        /// 光纤个数
        /// </summary>
        public int FiberCount { get; set; }
        /// <summary>
        /// 扩展板个数
        /// </summary>
        public int ExpansionBoardCount { get; set; }
        /// <summary>
        /// 头板个数（1号扩展板）
        /// </summary>
        public int HBCountOfExBoardNO1 { get; set; }
        /// <summary>
        /// 头板个数（2号扩展板）
        /// </summary>
        public int HBCountOfExBoardNO2 { get; set; }
        /// <summary>
        /// 1bit时的头板数据长度
        /// </summary>
        public byte HeadBoardDataWidth1 { get; set; }
        /// <summary>
        /// 2bit时的头板数据长度
        /// </summary>
        public byte HeadBoardDataWidth2 { get; set; }
        /// <summary>
        /// 扩展板1光纤1数据传输头板端口
        /// </summary>
        public string FirstSelectedPortPart1 { get; set; }
        /// <summary>
        /// 扩展板1光纤2数据传输头板端口
        /// </summary>
        public string FirstSelectedPortPart2 { get; set; }
        /// <summary>
        /// 扩展板2光纤1数据传输头板端口
        /// </summary>
        public string SecondSelectedPortPart1 { get; set; }
        /// <summary>
        /// 扩展板2光纤2数据传输头板端口
        /// </summary>
        public string SecondSelectedPortPart2 { get; set; }


        public byte ToplogyMode
        {
            get
            {
                byte result = 0;
                if (FiberCount == 1 && ExpansionBoardCount == 1)
                {
                    result = 0;
                }
                else if (FiberCount == 2 && ExpansionBoardCount == 1)
                {
                    result = 1;
                }
                else if (FiberCount == 2 && ExpansionBoardCount == 2)
                {
                    result = 2;
                }
                return result;
            }
        }

        public byte B1HbDataMap
        {
            get
            {
                byte ret = 0;
                string temp = FirstSelectedPortPart2 + FirstSelectedPortPart1;
                ret = Convert.ToByte(temp, 2);
                return ret;
            }
        }


        public byte B2HbDataMap
        {
            get
            {
                byte ret = 0;
                string temp = SecondSelectedPortPart2 + SecondSelectedPortPart1;
                ret = Convert.ToByte(temp, 2);
                return ret;
            }
        }

    }

    public struct VenderPrinterConfig
    {
        public PrinterHeadEnum nHeadType;
        public int nWidth;
        public byte nColorNum;
        public sbyte nGroupNum;
        public float fHeadXColorSpace;
        public float fHeadXGroupSpace;
        public float fHeadYSpace;
        public float fHeadAngle;

        public byte m_nWhiteInkNum;
        public byte m_nOverCoatInkNum;

        /// <summary>
        /// bit0:IsHeadLeft;bit1:mirror;bit2:surpportLcd;bit3:DualBank;
        /// bit4:COLORORDER;bit5:VerY;bit6:4ColorMirror;bit7:unkonwn;
        /// bit8:8ColorCompatibilityMode;bit9:ZMeasur;
        /// </summary>
        public uint m_nBitFlag;

        public EPR_FactoryData_Ex EpsonFactoryData_Ex;
        public SPrintAmendProperty AmendProperty;
        public USER_SET_INFORMATION UserConfig;
        public byte m_nPaper_w_left;
        public byte m_xaar382_pixle_mode;
        public float ServePos;
        public DOUBLE_YAXIS doubleYPram;
        public byte m_nEncoder; //光栅/伺服
        public byte m_PrintHeadCnt;

        //public byte []      m_WidthList;
        public string SystemConvertToXml()
        {
            string xml = "<VenderPrinterConfig>";
            //xml += PubFunc.SystemConvertToXml(nHeadType,typeof(PrinterHeadEnum));
            xml += PubFunc.SystemConvertToXml((int)nHeadType, typeof(int));
            xml += PubFunc.SystemConvertToXml(nWidth, typeof(int));
            xml += PubFunc.SystemConvertToXml(nColorNum, typeof(byte));
            xml += PubFunc.SystemConvertToXml(nGroupNum, typeof(sbyte));
            xml += PubFunc.SystemConvertToXml(fHeadXColorSpace, typeof(float));
            xml += PubFunc.SystemConvertToXml(fHeadXGroupSpace, typeof(float));
            xml += PubFunc.SystemConvertToXml(fHeadYSpace, typeof(float));
            xml += PubFunc.SystemConvertToXml(fHeadAngle, typeof(float));

            xml += PubFunc.SystemConvertToXml(m_nWhiteInkNum, typeof(byte));
            xml += PubFunc.SystemConvertToXml(m_nOverCoatInkNum, typeof(byte));
            xml += PubFunc.SystemConvertToXml(m_nBitFlag, typeof(uint));
            xml += PubFunc.SystemConvertToXml(EpsonFactoryData_Ex, typeof(EPR_FactoryData_Ex));
            xml += PubFunc.SystemConvertToXml(m_nPaper_w_left, typeof(byte));
            //			xml += PubFunc.SystemConvertToXml(AllwinCleanPara,typeof(AllwinCleanParameter));
            xml += PubFunc.SystemConvertToXml(AmendProperty, typeof(SPrintAmendProperty));
            xml += PubFunc.SystemConvertToXml(UserConfig, typeof(USER_SET_INFORMATION));
            xml += PubFunc.SystemConvertToXml(m_xaar382_pixle_mode, typeof(byte));
            xml += PubFunc.SystemConvertToXml(doubleYPram, typeof(DOUBLE_YAXIS));
            xml += PubFunc.SystemConvertToXml(m_nEncoder, typeof(byte));
            xml += PubFunc.SystemConvertToXml(m_PrintHeadCnt, typeof(byte));
            xml += "</VenderPrinterConfig>";
            return xml;
        }

        public static object SystemConvertFromXml(XmlElement jobElemenet, Type type)
        {
            VenderPrinterConfig job = new VenderPrinterConfig();
            XmlNode currNode = jobElemenet.FirstChild;
            //job.nHeadType = (PrinterHeadEnum)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(PrinterHeadEnum));
            job.nHeadType = (PrinterHeadEnum)(int)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(int));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.nWidth = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(int));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.nColorNum = (byte)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(byte));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.nGroupNum = (sbyte)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(sbyte));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.fHeadXColorSpace = (float)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(float));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.fHeadXGroupSpace = (float)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(float));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.fHeadYSpace = (float)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(float));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.fHeadAngle = (float)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(float));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.m_nWhiteInkNum = (byte)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(byte));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.m_nOverCoatInkNum = (byte)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(byte));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.m_nBitFlag = (uint)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(uint));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.EpsonFactoryData_Ex = (EPR_FactoryData_Ex)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(EPR_FactoryData_Ex));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.m_nPaper_w_left = (byte)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(byte));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.AmendProperty = (SPrintAmendProperty)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(SPrintAmendProperty));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.UserConfig = (USER_SET_INFORMATION)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(USER_SET_INFORMATION));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.m_xaar382_pixle_mode = (byte)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(byte));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.doubleYPram = (DOUBLE_YAXIS)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(DOUBLE_YAXIS));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.m_nEncoder = (byte)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(byte));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            job.m_PrintHeadCnt = (byte)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(byte));
            currNode = currNode.NextSibling;
            if (currNode == null) return job;
            return job;
        }
        public VenderPrinterConfig Clone()
        {
            return (VenderPrinterConfig)MemberwiseClone();
        }

    }

    /// <summary>
    /// The allwi n_ clean parameter.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AllwinCleanParameter
    {
        // action part:
        /// <summary>
        /// The head mask.
        /// </summary>
        public byte HeadMask;

        // public byte SuckType; //0, standard suck. 1, quick suck.

        // suck part:
        /// <summary>
        /// The suck times.
        /// </summary>
        public byte SuckTimes;

        /// <summary>
        /// The carriage_ x_ suck pos.
        /// </summary>
        public short Carriage_X_SuckPos;

        /// <summary>
        /// The head box_ z_ suck pos.
        /// </summary>
        public short HeadBox_Z_SuckPos;

        /// <summary>
        /// The suck ink time.
        /// </summary>
        public short SuckInkTime; // unit:ms;

        /// <summary>
        /// The input air time.
        /// </summary>
        public short InputAirTime; // unit:ms;

        /// <summary>
        /// The suck waste ink time.
        /// </summary>
        public short SuckWasteInkTime; // unit:ms;

        // wipe part:
        /// <summary>
        /// The wipe times.
        /// </summary>
        public byte WipeTimes;

        /// <summary>
        /// The wiper pos_ y.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public short[] WiperPos_Y;

        /// <summary>
        /// The head box_ z_ wipe pos.
        /// </summary>
        public short HeadBox_Z_WipePos;

        /// <summary>
        /// The carriage_ x_ wipe pos_ start.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public short[] Carriage_X_WipePos_Start;

        /// <summary>
        /// The carriage_ x_ wipe pos_ end.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public short[] Carriage_X_WipePos_End;

        /// <summary>
        /// The carriage_ x_ wipe_ speed.
        /// </summary>
        public byte Carriage_X_Wipe_Speed;

        // flash part
        // 清洗闪喷频率1-10KHZ，清洗闪喷时间10秒，间歇闪喷周期2秒，间歇有效闪喷时间2秒摸式出墨
        // （闪喷频率和闪喷时间和闪喷周期和有效闪喷时间软件可以设置
        /// <summary>
        /// The flash freq interval.
        /// </summary>
        public short FlashFreqInterval; // unit: us.

        /// <summary>
        /// The flash time.
        /// </summary>
        public byte FlashTime; // unit:0.1s.

        /// <summary>
        /// The flash cycle.
        /// </summary>
        public byte FlashCycle; // unit:0.1s.

        /// <summary>
        /// The flash idle in cycle.
        /// </summary>
        public byte FlashIdleInCycle; // unit:0.1s.

        public short HeadBox_Z_FlashPos;//闪喷时Z轴高度 
    } ;
    public struct VenderDisp
    {
        public string DisplayName;
        public int VenderID;
    };
}
