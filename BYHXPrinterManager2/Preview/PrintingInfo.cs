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
using System.Globalization;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace BYHXPrinterManager.JobListView
{
    /// <summary>
    /// Summary description for PrintingInfo.
    /// </summary>
    public class PrintingInfo : System.Windows.Forms.UserControl
    {
        DateTime m_StartTime = DateTime.Now;
        //private string m_sJobInfo = "";
        private float m_fArea = 0;

        private IPrinterChange m_iPrinterChange = null;
        private bool m_bCreateImageWithPercent = false;
        private bool m_bPrintingPreview = true;
        private UILengthUnit m_curUnit;
        private UIJob m_curJob;

        private BYHXPrinterManager.Preview.PrintingPreview m_PrintPreview;
        private SplitContainer splitContainer1;
        private Label labelstrSize;
        private Label labelstrRes;
        private Label labelstrDeep;
        private Label labelstrPass;
        private Label labelstrDir;
        private Label labelsLangID;
        private Label labelfilepath;
        private Label labelstrTime;
        private Label labelstrPercentage;
        private Label labelstrArea;
        private Label labelstrEff;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private BYHXPrinterManager.Setting.ToolbarSetting_1 toolbarSetting_11;

        public bool SpliterOrientathion
        {
            get
            {
                return this.splitContainer1.Orientation == Orientation.Vertical;
            }
            set
            {
                if (value)
                    this.splitContainer1.Orientation = Orientation.Vertical;
                else
                    this.splitContainer1.Orientation = Orientation.Horizontal;
            }
        }

        public bool IsSpliterFixed
        {
            get { return this.splitContainer1.IsSplitterFixed; }
            set { this.splitContainer1.IsSplitterFixed = value; }
        }
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public PrintingInfo()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call

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

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintingInfo));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_PrintPreview = new BYHXPrinterManager.Preview.PrintingPreview();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelfilepath = new System.Windows.Forms.Label();
            this.labelsLangID = new System.Windows.Forms.Label();
            this.labelstrDir = new System.Windows.Forms.Label();
            this.labelstrPass = new System.Windows.Forms.Label();
            this.labelstrDeep = new System.Windows.Forms.Label();
            this.labelstrRes = new System.Windows.Forms.Label();
            this.labelstrSize = new System.Windows.Forms.Label();
            this.labelstrEff = new System.Windows.Forms.Label();
            this.labelstrArea = new System.Windows.Forms.Label();
            this.labelstrPercentage = new System.Windows.Forms.Label();
            this.labelstrTime = new System.Windows.Forms.Label();
            this.toolbarSetting_11 = new BYHXPrinterManager.Setting.ToolbarSetting_1();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_PrintPreview);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Panel2.Controls.Add(this.toolbarSetting_11);
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // m_PrintPreview
            // 
            this.m_PrintPreview.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.m_PrintPreview, "m_PrintPreview");
            this.m_PrintPreview.Divider = false;
            this.m_PrintPreview.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
            this.m_PrintPreview.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_PrintPreview.Name = "m_PrintPreview";
            this.m_PrintPreview.DoubleClick += new System.EventHandler(this.m_PrintPreview_DoubleClick);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelfilepath, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.labelsLangID, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.labelstrDir, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.labelstrPass, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.labelstrDeep, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.labelstrRes, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelstrSize, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelstrEff, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelstrArea, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelstrPercentage, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelstrTime, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // label11
            // 
            this.label11.AutoEllipsis = true;
            this.label11.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            this.label11.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // label10
            // 
            this.label10.AutoEllipsis = true;
            this.label10.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            this.label10.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // label9
            // 
            this.label9.AutoEllipsis = true;
            this.label9.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            this.label9.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // label8
            // 
            this.label8.AutoEllipsis = true;
            this.label8.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.label8.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // label7
            // 
            this.label7.AutoEllipsis = true;
            this.label7.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.label7.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // label6
            // 
            this.label6.AutoEllipsis = true;
            this.label6.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.label6.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // label5
            // 
            this.label5.AutoEllipsis = true;
            this.label5.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.label5.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.label4.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.label3.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.label2.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.label1.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // labelfilepath
            // 
            resources.ApplyResources(this.labelfilepath, "labelfilepath");
            this.labelfilepath.AutoEllipsis = true;
            this.labelfilepath.BackColor = System.Drawing.Color.Silver;
            this.labelfilepath.Name = "labelfilepath";
            this.labelfilepath.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // labelsLangID
            // 
            resources.ApplyResources(this.labelsLangID, "labelsLangID");
            this.labelsLangID.AutoEllipsis = true;
            this.labelsLangID.BackColor = System.Drawing.Color.Silver;
            this.labelsLangID.Name = "labelsLangID";
            this.labelsLangID.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // labelstrDir
            // 
            resources.ApplyResources(this.labelstrDir, "labelstrDir");
            this.labelstrDir.AutoEllipsis = true;
            this.labelstrDir.BackColor = System.Drawing.Color.Silver;
            this.labelstrDir.Name = "labelstrDir";
            this.labelstrDir.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // labelstrPass
            // 
            resources.ApplyResources(this.labelstrPass, "labelstrPass");
            this.labelstrPass.AutoEllipsis = true;
            this.labelstrPass.BackColor = System.Drawing.Color.Silver;
            this.labelstrPass.Name = "labelstrPass";
            this.labelstrPass.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // labelstrDeep
            // 
            resources.ApplyResources(this.labelstrDeep, "labelstrDeep");
            this.labelstrDeep.AutoEllipsis = true;
            this.labelstrDeep.BackColor = System.Drawing.Color.Silver;
            this.labelstrDeep.Name = "labelstrDeep";
            this.labelstrDeep.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // labelstrRes
            // 
            resources.ApplyResources(this.labelstrRes, "labelstrRes");
            this.labelstrRes.AutoEllipsis = true;
            this.labelstrRes.BackColor = System.Drawing.Color.Silver;
            this.labelstrRes.Name = "labelstrRes";
            this.labelstrRes.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // labelstrSize
            // 
            resources.ApplyResources(this.labelstrSize, "labelstrSize");
            this.labelstrSize.AutoEllipsis = true;
            this.labelstrSize.BackColor = System.Drawing.Color.Silver;
            this.labelstrSize.Name = "labelstrSize";
            this.labelstrSize.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // labelstrEff
            // 
            resources.ApplyResources(this.labelstrEff, "labelstrEff");
            this.labelstrEff.AutoEllipsis = true;
            this.labelstrEff.BackColor = System.Drawing.Color.Silver;
            this.labelstrEff.Name = "labelstrEff";
            this.labelstrEff.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // labelstrArea
            // 
            resources.ApplyResources(this.labelstrArea, "labelstrArea");
            this.labelstrArea.AutoEllipsis = true;
            this.labelstrArea.BackColor = System.Drawing.Color.Silver;
            this.labelstrArea.Name = "labelstrArea";
            this.labelstrArea.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // labelstrPercentage
            // 
            resources.ApplyResources(this.labelstrPercentage, "labelstrPercentage");
            this.labelstrPercentage.AutoEllipsis = true;
            this.labelstrPercentage.BackColor = System.Drawing.Color.Silver;
            this.labelstrPercentage.Name = "labelstrPercentage";
            this.labelstrPercentage.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // labelstrTime
            // 
            resources.ApplyResources(this.labelstrTime, "labelstrTime");
            this.labelstrTime.AutoEllipsis = true;
            this.labelstrTime.BackColor = System.Drawing.Color.Silver;
            this.labelstrTime.Name = "labelstrTime";
            this.labelstrTime.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // toolbarSetting_11
            // 
            resources.ApplyResources(this.toolbarSetting_11, "toolbarSetting_11");
            this.toolbarSetting_11.BackColor = System.Drawing.Color.Silver;
            this.toolbarSetting_11.Divider = false;
            this.toolbarSetting_11.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
            this.toolbarSetting_11.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.toolbarSetting_11.Name = "toolbarSetting_11";
            this.toolbarSetting_11.TreeColorGradient = true;
            // 
            // PrintingInfo
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            resources.ApplyResources(this, "$this");
            this.Name = "PrintingInfo";
            this.DoubleClick += new System.EventHandler(this.PreviewAndInfo_DoubleClick);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        public void OnPrintingStart()
        {
            m_StartTime = DateTime.Now;
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            this.toolbarSetting_11.OnPrinterPropertyChange(sp);
        }
        public void OnPrinterSettingChange(SPrinterSetting ss)
        {
            this.toolbarSetting_11.OnPrinterSettingChange(ss);
        }
        public void OnPreferenceChange(UIPreference up)
        {
            this.toolbarSetting_11.OnPreferenceChange(up);
            UpdateJobInfo(up.Unit, m_curJob);
            if (m_curUnit != up.Unit)
            {
                m_curUnit = up.Unit;
                if (m_curJob != null)
                {
                    UpdateJobInfo(m_curUnit, m_curJob);
                }
                else if (m_bPrintingPreview && m_bCreateImageWithPercent)
                {
                    OnSwitchToPrintingPreview();
                }
            }
        }
        public void OnGetPrinterSetting(ref SPrinterSetting ss)
		{
            this.toolbarSetting_11.OnGetPrinterSetting(ref ss);
		}

		public void OnPrinterStatusChanged(JetStatusEnum status)
		{
            this.toolbarSetting_11.OnPrinterStatusChanged(status);
		}

        public void SetPrinterStatusChanged(JetStatusEnum status)
        {
            this.toolbarSetting_11.SetPrinterStatusChanged(status);
        }

        public void SetPrinterChange(IPrinterChange ic)
        {
            m_iPrinterChange = ic;
            toolbarSetting_11.SetPrinterChange(ic);
            m_PrintPreview.SetPrinterChange(ic);
        }
        public void SetCreateImageWithPercent(bool bPre)
        {
            m_bCreateImageWithPercent = bPre;
        }

        public void SetPrintingPreview(bool bPre)
        {
            m_bPrintingPreview = bPre;
            m_PrintPreview.SetPrintingPreview(bPre);
        }
        public void OnUpdatePrintingInfo(Image preview)
        {
            UpdatePreviewImage(preview);

            SPrtFileInfo jobInfo = new SPrtFileInfo();
            if (CoreInterface.Printer_GetJobInfo(ref jobInfo) > 0)
            {
                UpdatePrintingJobInfo(m_curUnit, jobInfo);
                SizeF previewsize = GetJobSize(jobInfo.sImageInfo.nImageWidth, jobInfo.sImageInfo.nImageHeight, jobInfo.sFreSetting.nResolutionX, jobInfo.sFreSetting.nResolutionY);
                UpdateJobSizeInfo(previewsize);
            }
            else
            {
                //??????????????????????????????????????????  should do it
                UpdatePrintingJobInfo(m_curUnit, jobInfo);
                UpdateJobSizeInfo(SizeF.Empty);
            }
        }
        public void OnSwitchToPrintingPreview()
        {
            bool bPrinting = m_bPrintingPreview;
            SPrtFileInfo jobInfo = new SPrtFileInfo();
            if (CoreInterface.Printer_GetJobInfo(ref jobInfo) > 0)
            {
                UpdatePrintingJobInfo(m_curUnit, jobInfo);
                CreatePrintingPreviewAsPercentage(0);
                SizeF previewsize = GetJobSize(jobInfo.sImageInfo.nImageWidth, jobInfo.sImageInfo.nImageHeight, jobInfo.sFreSetting.nResolutionX, jobInfo.sFreSetting.nResolutionY);
                UpdateJobSizeInfo(previewsize);
            }
            else
            {
                //??????????????????????????????????????????  should do it
                UpdatePrintingJobInfo(m_curUnit, jobInfo);
                UpdatePreviewImage(null);
                UpdateJobSizeInfo(SizeF.Empty);
            }
        }
        public void UpdatePercentage(int percent)
        {
            m_PrintPreview.UpdatePercentage(percent);
            ///////////////////////////////////////////////
            ///this should only call by the third RIP software
            if (m_bCreateImageWithPercent && m_bPrintingPreview)
                CreatePrintingPreviewAsPercentage(percent);
            if (percent > 0)
            {
                UpdateJobInfoTextWith(percent);
            }
        }
        public void UpdateUIJobPreview(UIJob job)
        {
            if (job != null)
            {
                UpdatePreviewImage(job.Miniature);
                UpdateJobSizeInfo(job.JobSize);
                UpdateJobInfo(m_curUnit, job);
            }
            else
            {
                //No one job in joblist
                UpdatePreviewImage(null);
                UpdateJobSizeInfo(SizeF.Empty);
                UpdateJobInfo(m_curUnit, null);
            }
            m_curJob = job;
        }


        private void UpdatePreviewImage(Image preview)
        {
            m_PrintPreview.UpdatePreviewImage(preview);
        }
        private void UpdateJobSizeInfo(SizeF size)
        {
            m_PrintPreview.UpdateJobSizeInfo(size);
        }
        private void UpdateJobInfo(UILengthUnit unit, UIJob jobInfo)
        {
            InitNull();
            if (jobInfo != null)
            {
                //float width = this.CalcRealJobWidth(unit,jobInfo.JobSize.Width);
                UpdateJobInfoText(unit, jobInfo.JobSize.Width, jobInfo.JobSize.Height, jobInfo.ResolutionX, jobInfo.ResolutionY,
                    jobInfo.ColorDeep, jobInfo.PassNumber, jobInfo.PrintingDirection, jobInfo.FileLocation, jobInfo.LangID);
            }
        }

        private void InitNull()
        {
            //this.m_LabelPrintingJobInfo.Text = null;
            foreach (Control lab in this.tableLayoutPanel1.Controls)
                if (this.tableLayoutPanel1.GetColumn(lab) == 1 && lab.Name != labelstrEff.Name)
                    lab.Text = null;
        }

        private void UpdateJobInfoText(UILengthUnit unit, float width, float height, int resX, int resY,
            int deep, int passnum, int direction, string filepath, int LangID)
        {

            string unitStr = ResString.GetUnitSuffixDispName(unit);
            string pass = ResString.GetDisplayPass();

            string strSize = string.Format("{0}x{1} {2}",
                UIPreference.ToDisplayLength(unit, width).ToString("f1"),
                UIPreference.ToDisplayLength(unit, height).ToString("f1"), unitStr);
            width = this.CalcRealJobWidth(unit, width);
            m_fArea = UIPreference.ToDisplayLength(UILengthUnit.Meter, width) * UIPreference.ToDisplayLength(UILengthUnit.Meter, height);

            string strRes = string.Format("{0}x{1}",
                (resX),
                (resY));

            string strDeep;
            if (1 == deep)
            {
                strDeep = "Normal";
            }
            else
            {
                strDeep = "High Quality";
            }


            string strPass = string.Format("{0} {1}", passnum, pass);
            string strDir = ResString.GetEnumDisplayName(typeof(PrintDirection), (PrintDirection)direction);
            string sLangID = "";
            if (LangID != 0xffff)
            {
                try
                {
                    foreach (CultureInfo cInfo in CultureInfo.GetCultures(CultureTypes.AllCultures))
                    {
                        if (cInfo.LCID == LangID)
                        {
                            sLangID = cInfo.EnglishName;
                            break;
                        }
                    }

                }
                catch (Exception)
                {
                    sLangID = "";
                }
            }
            //DisplayName EnglishName

            //this.m_LabelPrintingJobInfo.Text= 
            //    m_sJobInfo= 
            //    strSize 
            //    + "\n" + strRes 
            //    + "\n" + strDeep
            //    + "\n" + strPass
            //    + "\n" + strDir
            //    + "\n" + sLangID
            //    + "\n" + filepath;
            this.labelstrSize.Text = strSize;
            this.labelstrRes.Text = strRes;
            this.labelstrDeep.Text = strDeep;
            this.labelstrPass.Text = strPass;
            this.labelstrDir.Text = strDir;
            this.labelsLangID.Text = sLangID;
            this.labelfilepath.Text = filepath;
        }
        private void UpdatePrintingJobInfo(UILengthUnit unit, SPrtFileInfo jobInfo)
        {
            float width = 0;
            if (jobInfo.sFreSetting.nResolutionX != 0)
            {
                width = (float)jobInfo.sImageInfo.nImageWidth / (float)(jobInfo.sFreSetting.nResolutionX);
                //width = this.CalcRealJobWidth(unit,width);
                //width = UIPreference.ToDisplayLength(unit,width);
            }
            float height = 0;
            if (jobInfo.sFreSetting.nResolutionY != 0)
            {
                height = (float)jobInfo.sImageInfo.nImageHeight / (float)(jobInfo.sFreSetting.nResolutionY);
                //height = UIPreference.ToDisplayLength(unit,height);
            }
            InitNull();
            UpdateJobInfoText(unit, width, height, jobInfo.sFreSetting.nResolutionX * jobInfo.sImageInfo.nImageResolutionX, jobInfo.sFreSetting.nResolutionY * jobInfo.sImageInfo.nImageResolutionY,
                jobInfo.sImageInfo.nImageColorDeep, jobInfo.sFreSetting.nPass, jobInfo.sFreSetting.nBidirection, null, 0);
        }

        private void UpdateJobInfoTextWith(int percent)
        {
            TimeSpan time = DateTime.Now - m_StartTime;
            string strTime = time.Hours.ToString() + ":" + time.Minutes.ToString() + ":" + time.Seconds.ToString();
            string strPercentage = percent.ToString() + "%";

            string unitStr = ResString.GetUnitSuffixDispName(UILengthUnit.Meter);
            string strArea = (m_fArea * percent / 100.0f).ToString() + " " + unitStr + "2";
            float efficient = 0;
            if (time.TotalSeconds != 0)
                efficient = (float)(m_fArea * percent / 100.0f) / (float)time.TotalHours;
            string strEff = efficient.ToString() + " " + unitStr + "2/h";
            //this.m_LabelPrintingJobInfo.Text = strTime
            //+ "\n" + strPercentage 
            //+ "\n" + strArea
            //+ "\n" + strEff
            //+ "\n" + m_sJobInfo;
            this.labelstrTime.Text = strTime;
            this.labelstrPercentage.Text = strPercentage;
            this.labelstrArea.Text = strArea;
            this.labelstrEff.Text = strEff;
        }

        private SizeF GetJobSize(int imgWidth, int imgHeight, int resX, int resY)
        {
            float width = 0;

            if (resX != 0)
            {
                width = (float)imgWidth / (float)resX;
            }
            float height = 0;
            if (resY != 0)
            {
                height = (float)imgHeight / (float)resY;
            }
            return new SizeF(width, height);
        }
        unsafe private void CreatePrintingPreviewAsPercentage(int percentage)
        {
            SPrtFileInfo jobInfo = new SPrtFileInfo();
            if (CoreInterface.Printer_GetJobInfo(ref jobInfo) <= 0) return;
            IntPtr handle = (IntPtr)jobInfo.sImageInfo.nImageData;
            if (handle == IntPtr.Zero) return;
            SPrtImagePreview previewData = (SPrtImagePreview)Marshal.PtrToStructure(handle, typeof(SPrtImagePreview));
            Bitmap image = SerialFunction.CreateImageWithPreview(previewData);

            if (image != null)
            {
                int clipy = (image.Height * percentage / 100);
                int clipheight = image.Height - clipy;

                if (clipheight > 0)
                {
                    BitmapData data = image.LockBits(new Rectangle(0, clipy, image.Width, clipheight), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                    byte* buf = (byte*)data.Scan0;
                    int size = data.Stride * clipheight;

                    for (int i = 0; i < size; i++)
                    {
                        buf[i] = 0xff;
                    }

                    image.UnlockBits(data);
                }

                UpdatePreviewImage(image);
            }
        }

        private void PreviewAndInfo_DoubleClick(object sender, System.EventArgs e)
        {
            m_iPrinterChange.OnSwitchPreview();
        }

        private void m_PrintPreview_DoubleClick(object sender, System.EventArgs e)
        {
            m_iPrinterChange.OnSwitchPreview();
        }

        private void m_LabelPrintingJobInfo_DoubleClick(object sender, System.EventArgs e)
        {
            m_iPrinterChange.OnSwitchPreview();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SelectNextControl(this.m_PrintPreview, true, true, false, true);
        }
		private float CalcRealJobWidth(UILengthUnit unit, float jobwidth)
		{
			float ret = 0;
			AllParam allparam = this.m_iPrinterChange.GetAllParam();
			float orginX = allparam.PrinterSetting.sFrequencySetting.fXOrigin;
			float colorbarspace = allparam.PrinterSetting.sBaseSetting.sStripeSetting.fStripeOffset;
			float colorbarwidth = allparam.PrinterSetting.sBaseSetting.sStripeSetting.fStripeWidth;
			InkStrPosEnum place = allparam.PrinterSetting.sBaseSetting.sStripeSetting.eStripePosition;
			float pw = allparam.PrinterSetting.sBaseSetting.fPaperWidth;
			switch (place)
			{
				case InkStrPosEnum.Both:
					ret = (colorbarwidth+colorbarspace)*2 + jobwidth;
					if(orginX + ret>pw)
						ret = pw-orginX;
					break;
				case InkStrPosEnum.Left:
				case InkStrPosEnum.Right:
					ret = (colorbarwidth+colorbarspace) + jobwidth;
					if(orginX + ret>pw)
						ret = pw-orginX;
					break;
				case InkStrPosEnum.None:
					ret =jobwidth;
					if( orginX+ret>pw)
						ret = pw-orginX;
					break;
			}
			return ret;
		}

        private void tableLayoutPanel1_Click(object sender, EventArgs e)
        {
            this.m_PrintPreview.Focus();
        }
    }

}
