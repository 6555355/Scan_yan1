/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
//#define COLORORDER

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

using BYHXPrinterManager.Main;
using System.Collections.Generic;


namespace BYHXPrinterManager.Setting
{
    /// <summary>
    /// Summary description for PrinterHWSetting.
    /// </summary>
    public class MbIdSetting : BYHXUserControl
    {
        private ArrayList m_FileHeadList = new ArrayList(); //File Vender.dll
        private ArrayList m_SupportHeadList = new ArrayList(); //File Vender.dll

        //private SPrintAmendProperty m_AmendProperty;
        private bool m_bEpson = false;
        private UIPreference m_CurrentPreference;
        private UILengthUnit m_CurrentUnit = UILengthUnit.Inch;
        const int MAX_HEAD_NUM = 16;
        byte[] m_WidthList = new byte[] { 18, 25, 32, 33, 35, 50, 55, 60, 100 };
        byte[] m_ColorNumList = new byte[] { 4, 6, 1, 2, 5, 7, 8 };
        byte[] m_InkType = new byte[] { 0xA, 0xB, 0xC };
        private const string sPrinterProductList = "PrinterProductList_";
        VenderPrinterConfig m_PrinterConfig = new VenderPrinterConfig();
		int m_nProductId = 0;
//		private EPR_FactoryData_Ex eprfd = new EPR_FactoryData_Ex();
		public event EventHandler OKButtonClicked;
        private System.Windows.Forms.ComboBox[] m_ComboBoxPrintColorOder;
        private System.Windows.Forms.ComboBox[] m_ComboxBoxRipColorOder;
		private SPrinterProperty m_sPrinterProperty;
		private SFWFactoryData fwData;
		private bool bIsGongzhengEpson = false;
		private bool bIsAllWinEpson = false;
        private bool bDoubleYAxis = false;
        DOUBLE_YAXIS doubleYaxis = new DOUBLE_YAXIS();
        private System.Windows.Forms.ToolTip m_ToolTip;
        private System.Windows.Forms.ContextMenu m_ContextMenu;
        private System.Windows.Forms.MenuItem m_menuItemDelete;
        private System.Windows.Forms.ProgressBar progressBar1;
        private GroupBox groupBox2;
        private ListBox listBox1;
        private Button btnEnumMbId;
        private NumericUpDown numericUpDown1;
        private Button buttonSetMbId;
        private Label labelMbId;
        private System.ComponentModel.IContainer components;

        private ColorEnum_Short[] defaultPrintColorOrder = new ColorEnum_Short[8] { ColorEnum_Short.K, ColorEnum_Short.C, 
            ColorEnum_Short.M,ColorEnum_Short.Y, 
            ColorEnum_Short.Lm, ColorEnum_Short.Lc, ColorEnum_Short.W, ColorEnum_Short.W };
        public MbIdSetting()
		{
            InitVenderList();
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
                PrinterHeadEnum[] array1 = (PrinterHeadEnum[])Enum.GetValues(typeof(PrinterHeadEnum));
                if (m_FileHeadList.Count < array1.Length-1)
                {
                    m_FileHeadList = new ArrayList();
                    for (int i = 0; i < array1.Length - 1; i++)
                    {
                        VenderDisp vd = new VenderDisp();
                        vd.DisplayName = array1[i].ToString();
                        vd.VenderID = (int)array1[i];
                        m_FileHeadList.Add(vd);
                    }
                }
            }
            else
            {
                PrinterHeadEnum[] array1 = (PrinterHeadEnum[])Enum.GetValues(typeof(PrinterHeadEnum));
                m_FileHeadList = new ArrayList();

                for (int i = 0; i < array1.Length-1; i++)
                {
                    VenderDisp vd = new VenderDisp();
                    vd.DisplayName = array1[i].ToString();
                    vd.VenderID = (int)array1[i];
                    m_FileHeadList.Add(vd);
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MbIdSetting));
            this.m_ContextMenu = new System.Windows.Forms.ContextMenu();
            this.m_menuItemDelete = new System.Windows.Forms.MenuItem();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnEnumMbId = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.buttonSetMbId = new System.Windows.Forms.Button();
            this.labelMbId = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_ContextMenu
            // 
            this.m_ContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuItemDelete});
            resources.ApplyResources(this.m_ContextMenu, "m_ContextMenu");
            // 
            // m_menuItemDelete
            // 
            resources.ApplyResources(this.m_menuItemDelete, "m_menuItemDelete");
            this.m_menuItemDelete.Index = 0;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Controls.Add(this.btnEnumMbId);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.m_ToolTip.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // listBox1
            // 
            resources.ApplyResources(this.listBox1, "listBox1");
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Name = "listBox1";
            this.m_ToolTip.SetToolTip(this.listBox1, resources.GetString("listBox1.ToolTip"));
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // btnEnumMbId
            // 
            resources.ApplyResources(this.btnEnumMbId, "btnEnumMbId");
            this.btnEnumMbId.Name = "btnEnumMbId";
            this.m_ToolTip.SetToolTip(this.btnEnumMbId, resources.GetString("btnEnumMbId.ToolTip"));
            this.btnEnumMbId.UseVisualStyleBackColor = true;
            this.btnEnumMbId.Click += new System.EventHandler(this.btnEnumMbId_Click);
            // 
            // numericUpDown1
            // 
            resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
            this.numericUpDown1.Name = "numericUpDown1";
            this.m_ToolTip.SetToolTip(this.numericUpDown1, resources.GetString("numericUpDown1.ToolTip"));
            // 
            // buttonSetMbId
            // 
            resources.ApplyResources(this.buttonSetMbId, "buttonSetMbId");
            this.buttonSetMbId.Name = "buttonSetMbId";
            this.m_ToolTip.SetToolTip(this.buttonSetMbId, resources.GetString("buttonSetMbId.ToolTip"));
            this.buttonSetMbId.Click += new System.EventHandler(this.buttonSetMbId_Click);
            // 
            // labelMbId
            // 
            resources.ApplyResources(this.labelMbId, "labelMbId");
            this.labelMbId.Name = "labelMbId";
            this.m_ToolTip.SetToolTip(this.labelMbId, resources.GetString("labelMbId.ToolTip"));
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            this.m_ToolTip.SetToolTip(this.progressBar1, resources.GetString("progressBar1.ToolTip"));
            // 
            // MbIdSetting
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.buttonSetMbId);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.labelMbId);
            this.Name = "MbIdSetting";
            this.m_ToolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.PrinterHWSetting_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        private void PrinterHWSetting_Load(object sender, System.EventArgs e)
        {
            var mbidList = GetEnumMbID(); //PubFunc.ExcuteEnumMBId(true);
            this.listBox1.Items.Clear();
            for (int i = 0; i < mbidList.Count; i++)
            {
                listBox1.Items.Add(mbidList[i]);
            }

            if (mbidList.Count > 0)
            {
                listBox1.SelectedIndex = 0;
            }
        }

		public void OnPreferenceChange( UIPreference up)
		{
			m_CurrentPreference = up;
			//m_bInitControl = true;
			if(m_CurrentUnit != up.Unit)
			{
				OnUnitChange(up.Unit);
				m_CurrentUnit = up.Unit;
			}
			//m_bInitControl = false;
		}
		private void  OnUnitChange(UILengthUnit newUnit)
		{
        }

        private bool bShowOneHeadDivider = false;

        private void buttonSetMbId_Click(object sender, System.EventArgs e)
        {
            SinglepassYataoFactoryParam para = new SinglepassYataoFactoryParam(null);
            para.MbId = (int)this.numericUpDown1.Value;
            int ret = this.SetSinglePassYaTaoFactoryParam(para);
            if (ret == 0)
            {
                MessageBox.Show("Failed to set mainboard ID");
            }
            else
            {
                MessageBox.Show("Set the mainboard ID success");
            }
        }

        private int SetSinglePassYaTaoFactoryParam(SinglepassYataoFactoryParam param)
        {
            byte[] buf = PubFunc.StructToBytes(param);
            uint bufsize = (uint)buf.Length;
            int ret = 0;
            //modify by ljp 2014-5-20
            if (listBox1.SelectedIndex < 0)
            {
                ret = CoreInterface.SetEpsonEP0Cmd(0x7b, buf, ref bufsize, 0, 1);
            }
            else
            {
                int mbid = (int)listBox1.Items[listBox1.SelectedIndex];
                ret = CoreInterface.SetEpsonEP0Cmd(0x7b, buf, ref bufsize, 0, 1, mbid);
            }
            return ret;
        }

        private void btnEnumMbId_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            var mbidList = GetEnumMbID(); //PubFunc.ExcuteEnumMBId(true);
            for (int i = 0; i < mbidList.Count; i++)
            {
                listBox1.Items.Add(mbidList[i]);
            }
            if (mbidList.Count > 0)
            {
                listBox1.SelectedIndex = 0;
            }
        }

        private List<int> GetEnumMbID()
        {
            List<int> ret = new List<int>();

            byte[] val = new byte[Marshal.SizeOf(typeof(SinglepassYataoFactoryParam)) + 2];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x7b, val, ref bufsize, 0, 1) != 0)
            {
                byte[] structData = new byte[Marshal.SizeOf(typeof(SinglepassYataoFactoryParam))];
                Buffer.BlockCopy(val, 2, structData, 0, structData.Length);
                SinglepassYataoFactoryParam obj = (SinglepassYataoFactoryParam)PubFunc.BytesToStruct(structData, typeof(SinglepassYataoFactoryParam));

                ret.Add(obj.MbId);
            }

            return ret;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
	}

	[StructLayout(LayoutKind.Sequential)]
	[Serializable]
	public struct SinglepassYataoFactoryParam : ICloneable
	{
		private ushort encoderResX;
		private ushort flashFreq;	// 闪喷间隔，大概以ms为单位，最大0xFF0(4080)

		private int dummy;	// 
		private int boardId;
		private sbyte colorBits;	// 色深，Bit数
		private sbyte nLineGroup;	// 
		private byte printDir;

		public SinglepassYataoFactoryParam(object o)
		{
			this.encoderResX = 0;
			this.dummy = 0;
			this.boardId = 0;
			this.flashFreq = 200;
			colorBits = 2;
			nLineGroup = 1;
			printDir = 0;
		}

		public int MbId
		{
			get
			{
				return boardId;
			}
			set
			{
				boardId = value;
			}
		}

		public ushort EncoderResX
		{
			get
			{
				return this.encoderResX;
			}
			set
			{
				this.encoderResX = value;
			}
		}

		public ushort FlashFreq
		{
			get
			{
				return this.flashFreq;
			}
			set
			{
				this.flashFreq = value;
			}
		}

		public sbyte ColorBits
		{
			get { return colorBits; }
			set { colorBits = value; }
		}

		public sbyte LineGroup
		{
			get { return nLineGroup; }
			set { nLineGroup = value; }
		}

		public byte PrintDir
		{
			get { return printDir; }
			set { printDir = value; }
		}
		public string GetCommondProperty()
		{
			return string.Empty;
		}

		public object Clone()
		{
			SinglepassYataoFactoryParam temp = new SinglepassYataoFactoryParam(null);
			temp.EncoderResX = this.encoderResX;
			temp.FlashFreq = this.flashFreq;
			temp.MbId = this.boardId;
			temp.ColorBits = this.colorBits;
			temp.LineGroup = this.nLineGroup;
			temp.PrintDir = this.printDir;
			return temp;
		}
	}

}
