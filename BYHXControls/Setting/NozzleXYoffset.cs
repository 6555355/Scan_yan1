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
using System.IO;
using System.Collections.Generic;

namespace BYHXPrinterManager.Setting
{
    /// <summary>
    /// Summary description for KonicTemperature1.
    /// </summary>
    public class NozzleXYoffset : System.Windows.Forms.UserControl
    {
        private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
        private SPrinterProperty m_rsPrinterPropery;//Only read for color order

        private const int MAX_CHANAL = 16;
        private int m_HeadNum = 0;

        private byte m_StartHeadIndex = 0;
        private byte[] m_pMap;
        private bool m_bSpectra = false;
        private bool m_bKonic512 = false;
        private System.Windows.Forms.CheckBox[] m_CheckBoxHeadMask;
        private System.Windows.Forms.NumericUpDown[] m_NumericUpDownXs;
        private System.Windows.Forms.NumericUpDown[] m_NumericUpDownYs;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private System.Windows.Forms.Button m_ButtonToBoard;
        private System.Windows.Forms.Button m_ButtonRefresh;
        private System.Windows.Forms.GroupBox m_GroupBoxTemperature;
        private System.Windows.Forms.Label m_LabelHead;
        private System.Windows.Forms.Label m_labelX;
        private System.Windows.Forms.Label m_LabelY;
        private System.Windows.Forms.CheckBox m_CheckboxHeadSample;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownXSample;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownYSample;
        private System.Windows.Forms.Button m_ButtonDefault;
        private System.ComponentModel.IContainer components;

        private List<float[]> XYOffsetValues = new List<float[]>();
        private ToolTip m_ToolTip;
        private Label labelHeadAngle;
        private NumericUpDown m_numHeadAngle;
        private string XYoffsetPath = Path.Combine(Application.StartupPath, "XYOffset.bin");

        public NozzleXYoffset()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
            if(!this.m_LabelY.Visible)
            {
                this.labelHeadAngle.Left = this.m_LabelY.Left;
                this.m_numHeadAngle.Left = this.m_NumericUpDownYSample.Left;
            }
            else
            {
                this.labelHeadAngle.Left = this.m_LabelY.Left;
                this.m_numHeadAngle.Left = this.m_NumericUpDownYSample.Left;
                int offset = this.m_LabelY.Top - this.m_labelX.Top;
                this.labelHeadAngle.Top += offset;
                this.m_numHeadAngle.Top += offset;
                this.m_GroupBoxTemperature.Height += offset;
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

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NozzleXYoffset));
            this.m_ButtonToBoard = new System.Windows.Forms.Button();
            this.m_ButtonRefresh = new System.Windows.Forms.Button();
            this.m_GroupBoxTemperature = new System.Windows.Forms.GroupBox();
            this.labelHeadAngle = new System.Windows.Forms.Label();
            this.m_numHeadAngle = new System.Windows.Forms.NumericUpDown();
            this.m_LabelHead = new System.Windows.Forms.Label();
            this.m_labelX = new System.Windows.Forms.Label();
            this.m_LabelY = new System.Windows.Forms.Label();
            this.m_CheckboxHeadSample = new System.Windows.Forms.CheckBox();
            this.m_NumericUpDownXSample = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownYSample = new System.Windows.Forms.NumericUpDown();
            this.m_ButtonDefault = new System.Windows.Forms.Button();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.m_GroupBoxTemperature.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numHeadAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownXSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownYSample)).BeginInit();
            this.SuspendLayout();
            // 
            // m_ButtonToBoard
            // 
            this.m_ButtonToBoard.AccessibleDescription = null;
            this.m_ButtonToBoard.AccessibleName = null;
            resources.ApplyResources(this.m_ButtonToBoard, "m_ButtonToBoard");
            this.m_ButtonToBoard.BackgroundImage = null;
            this.m_ButtonToBoard.Font = null;
            this.m_ButtonToBoard.Name = "m_ButtonToBoard";
            this.m_ToolTip.SetToolTip(this.m_ButtonToBoard, resources.GetString("m_ButtonToBoard.ToolTip"));
            this.m_ButtonToBoard.Click += new System.EventHandler(this.m_ButtonToBoard_Click);
            // 
            // m_ButtonRefresh
            // 
            this.m_ButtonRefresh.AccessibleDescription = null;
            this.m_ButtonRefresh.AccessibleName = null;
            resources.ApplyResources(this.m_ButtonRefresh, "m_ButtonRefresh");
            this.m_ButtonRefresh.BackgroundImage = null;
            this.m_ButtonRefresh.Font = null;
            this.m_ButtonRefresh.Name = "m_ButtonRefresh";
            this.m_ToolTip.SetToolTip(this.m_ButtonRefresh, resources.GetString("m_ButtonRefresh.ToolTip"));
            this.m_ButtonRefresh.Click += new System.EventHandler(this.m_ButtonRefresh_Click);
            // 
            // m_GroupBoxTemperature
            // 
            this.m_GroupBoxTemperature.AccessibleDescription = null;
            this.m_GroupBoxTemperature.AccessibleName = null;
            resources.ApplyResources(this.m_GroupBoxTemperature, "m_GroupBoxTemperature");
            this.m_GroupBoxTemperature.BackgroundImage = null;
            this.m_GroupBoxTemperature.Controls.Add(this.labelHeadAngle);
            this.m_GroupBoxTemperature.Controls.Add(this.m_numHeadAngle);
            this.m_GroupBoxTemperature.Controls.Add(this.m_LabelHead);
            this.m_GroupBoxTemperature.Controls.Add(this.m_labelX);
            this.m_GroupBoxTemperature.Controls.Add(this.m_LabelY);
            this.m_GroupBoxTemperature.Controls.Add(this.m_CheckboxHeadSample);
            this.m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownXSample);
            this.m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownYSample);
            this.m_GroupBoxTemperature.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_GroupBoxTemperature.Font = null;
            this.m_GroupBoxTemperature.Name = "m_GroupBoxTemperature";
            this.m_GroupBoxTemperature.TabStop = false;
            this.m_ToolTip.SetToolTip(this.m_GroupBoxTemperature, resources.GetString("m_GroupBoxTemperature.ToolTip"));
            // 
            // labelHeadAngle
            // 
            this.labelHeadAngle.AccessibleDescription = null;
            this.labelHeadAngle.AccessibleName = null;
            resources.ApplyResources(this.labelHeadAngle, "labelHeadAngle");
            this.labelHeadAngle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelHeadAngle.Font = null;
            this.labelHeadAngle.Name = "labelHeadAngle";
            this.m_ToolTip.SetToolTip(this.labelHeadAngle, resources.GetString("labelHeadAngle.ToolTip"));
            // 
            // m_numHeadAngle
            // 
            this.m_numHeadAngle.AccessibleDescription = null;
            this.m_numHeadAngle.AccessibleName = null;
            resources.ApplyResources(this.m_numHeadAngle, "m_numHeadAngle");
            this.m_numHeadAngle.DecimalPlaces = 5;
            this.m_numHeadAngle.Font = null;
            this.m_numHeadAngle.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.m_numHeadAngle.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_numHeadAngle.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.m_numHeadAngle.Name = "m_numHeadAngle";
            this.m_ToolTip.SetToolTip(this.m_numHeadAngle, resources.GetString("m_numHeadAngle.ToolTip"));
            // 
            // m_LabelHead
            // 
            this.m_LabelHead.AccessibleDescription = null;
            this.m_LabelHead.AccessibleName = null;
            resources.ApplyResources(this.m_LabelHead, "m_LabelHead");
            this.m_LabelHead.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_LabelHead.Font = null;
            this.m_LabelHead.Name = "m_LabelHead";
            this.m_ToolTip.SetToolTip(this.m_LabelHead, resources.GetString("m_LabelHead.ToolTip"));
            // 
            // m_labelX
            // 
            this.m_labelX.AccessibleDescription = null;
            this.m_labelX.AccessibleName = null;
            resources.ApplyResources(this.m_labelX, "m_labelX");
            this.m_labelX.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_labelX.Font = null;
            this.m_labelX.Name = "m_labelX";
            this.m_ToolTip.SetToolTip(this.m_labelX, resources.GetString("m_labelX.ToolTip"));
            // 
            // m_LabelY
            // 
            this.m_LabelY.AccessibleDescription = null;
            this.m_LabelY.AccessibleName = null;
            resources.ApplyResources(this.m_LabelY, "m_LabelY");
            this.m_LabelY.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_LabelY.Font = null;
            this.m_LabelY.Name = "m_LabelY";
            this.m_ToolTip.SetToolTip(this.m_LabelY, resources.GetString("m_LabelY.ToolTip"));
            // 
            // m_CheckboxHeadSample
            // 
            this.m_CheckboxHeadSample.AccessibleDescription = null;
            this.m_CheckboxHeadSample.AccessibleName = null;
            resources.ApplyResources(this.m_CheckboxHeadSample, "m_CheckboxHeadSample");
            this.m_CheckboxHeadSample.BackgroundImage = null;
            this.m_CheckboxHeadSample.Font = null;
            this.m_CheckboxHeadSample.Name = "m_CheckboxHeadSample";
            this.m_ToolTip.SetToolTip(this.m_CheckboxHeadSample, resources.GetString("m_CheckboxHeadSample.ToolTip"));
            // 
            // m_NumericUpDownXSample
            // 
            this.m_NumericUpDownXSample.AccessibleDescription = null;
            this.m_NumericUpDownXSample.AccessibleName = null;
            resources.ApplyResources(this.m_NumericUpDownXSample, "m_NumericUpDownXSample");
            this.m_NumericUpDownXSample.DecimalPlaces = 5;
            this.m_NumericUpDownXSample.Font = null;
            this.m_NumericUpDownXSample.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_NumericUpDownXSample.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_NumericUpDownXSample.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownXSample.Name = "m_NumericUpDownXSample";
            this.m_ToolTip.SetToolTip(this.m_NumericUpDownXSample, resources.GetString("m_NumericUpDownXSample.ToolTip"));
            // 
            // m_NumericUpDownYSample
            // 
            this.m_NumericUpDownYSample.AccessibleDescription = null;
            this.m_NumericUpDownYSample.AccessibleName = null;
            resources.ApplyResources(this.m_NumericUpDownYSample, "m_NumericUpDownYSample");
            this.m_NumericUpDownYSample.DecimalPlaces = 5;
            this.m_NumericUpDownYSample.Font = null;
            this.m_NumericUpDownYSample.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.m_NumericUpDownYSample.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_NumericUpDownYSample.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownYSample.Name = "m_NumericUpDownYSample";
            this.m_ToolTip.SetToolTip(this.m_NumericUpDownYSample, resources.GetString("m_NumericUpDownYSample.ToolTip"));
            // 
            // m_ButtonDefault
            // 
            this.m_ButtonDefault.AccessibleDescription = null;
            this.m_ButtonDefault.AccessibleName = null;
            resources.ApplyResources(this.m_ButtonDefault, "m_ButtonDefault");
            this.m_ButtonDefault.BackgroundImage = null;
            this.m_ButtonDefault.Font = null;
            this.m_ButtonDefault.Name = "m_ButtonDefault";
            this.m_ToolTip.SetToolTip(this.m_ButtonDefault, resources.GetString("m_ButtonDefault.ToolTip"));
            this.m_ButtonDefault.Click += new System.EventHandler(this.m_ButtonDefault_Click);
            // 
            // NozzleXYoffset
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = null;
            this.Controls.Add(this.m_ButtonToBoard);
            this.Controls.Add(this.m_ButtonRefresh);
            this.Controls.Add(this.m_GroupBoxTemperature);
            this.Controls.Add(this.m_ButtonDefault);
            this.Name = "NozzleXYoffset";
            this.m_ToolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.m_GroupBoxTemperature.ResumeLayout(false);
            this.m_GroupBoxTemperature.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numHeadAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownXSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownYSample)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            m_rsPrinterPropery = sp;
            m_bSpectra = (sp.ePrinterHead == PrinterHeadEnum.Spectra_S_128);
            m_bKonic512 = SPrinterProperty.IsKonica512(sp.ePrinterHead);
            if (CheckComponentChange(sp))
            {
                m_HeadNum = sp.nHeadNum;
                if (m_bKonic512)
                {
                    m_HeadNum /= sp.nHeadNumPerColor;
                    if (sp.fHeadAngle != 0)
                    {
                        this.labelHeadAngle.Visible = this.m_numHeadAngle.Visible = this.m_numHeadAngle.Enabled = true;
                    }
                }

                //m_HeadNum = MAX_CHANAL;
                m_StartHeadIndex = 0;
                m_pMap = new byte[MAX_CHANAL]; //(byte[])sp.pElectricMap.Clone();
                for (int i = 0; i < MAX_CHANAL; i++)
                {
#if false
					m_pMap[i] =(byte)( MAX_CHANAL/nmap_input - 1 - m_pMap[i]) ;//(byte)(i*3 +  nStartIndex);
#else
                    m_pMap[i] = (byte)i;
#endif
                }
                CreateComponent();
                LayoutComponent();
                AppendComponent();
            }
        }
        public void OnPrinterSettingChange(SPrinterSetting ss)
        {
        }
        public void OnGetPrinterSetting(ref SPrinterSetting ss)
        {
        }
        public void OnPreferenceChange(UIPreference up)
        {
            //if (m_CurrentUnit != up.Unit)
            //{
                OnUnitChange(up.Unit);
                m_CurrentUnit = up.Unit;
            //}
        }

        private void OnUnitChange(UILengthUnit newUnit)
        {
            for (int i = 0; i < m_HeadNum; i++)
            {
                UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.m_NumericUpDownXs[i]);
                UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.m_NumericUpDownYs[i]);

                UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.m_NumericUpDownXs[i], this.m_ToolTip);
                UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.m_NumericUpDownYs[i], this.m_ToolTip);
            }
        }

        private void OnGetRealTimeFromUI()
        {
#if !LIYUUSB
            for (int i = 0; i < m_HeadNum; i++)
            {
                int nMap = m_pMap[i];
                XYOffsetValues[0][i] = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownXs[i].Value));
                XYOffsetValues[1][i] = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownYs[i].Value));
            }
            XYOffsetValues[2][0] = Decimal.ToSingle(this.m_numHeadAngle.Value);
#endif
        }
        private void OnSetRealTimeToUI()
        {
#if !LIYUUSB
            for (int i = 0; i < m_HeadNum; i++)
            {
                //int nMap = m_pMap[i];
                m_NumericUpDownXs[i].Value = new decimal(UIPreference.ToDisplayLength(m_CurrentUnit, XYOffsetValues[0][i]));
                m_NumericUpDownYs[i].Value = new decimal(UIPreference.ToDisplayLength(m_CurrentUnit, XYOffsetValues[1][i]));
            }
            this.m_numHeadAngle.Value = new decimal(XYOffsetValues[2][0]);
#endif
        }

        public void OnRealTimeChange()
        {
            this.DefaultRealTimeValue();
            if (!File.Exists(this.XYoffsetPath))
            {
                return;
            }
            using (StreamReader sr = new StreamReader(this.XYoffsetPath))
            {
                //while (!sr.EndOfStream)
                for (int j = 0; j < 3; j++)
                {
                    if (sr.EndOfStream)
                        break;
                    string strval = sr.ReadLine();
                    if (string.IsNullOrEmpty(strval))
                    {
                        continue;
                    }

                    string[] vals = strval.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (j < 2)
                    {
                        int headnum = Math.Min(this.m_HeadNum, vals.Length);
                        for (int i = 0; i < headnum; i++)
                        {
                            float temp = 0;
                            if (float.TryParse(vals[i], out temp))
                            {
                                this.XYOffsetValues[j][i] = temp;
                            }
                        }
                    }
                    else
                    {
                        float temp = 0;
                        if (float.TryParse(vals[0], out temp))
                        {
                            this.XYOffsetValues[j][0] = temp;
                        }
                    }
                }
            }
            this.OnSetRealTimeToUI();
        }
        private bool CheckComponentChange(SPrinterProperty sp)
        {
            if (m_HeadNum != sp.nHeadNum)
                return true;
            return false;
        }
        private void CreateComponent()
        {
            this.m_CheckBoxHeadMask = new CheckBox[m_HeadNum];
            this.m_NumericUpDownXs = new NumericUpDown[m_HeadNum];
            this.m_NumericUpDownYs = new NumericUpDown[m_HeadNum];

            for (int i = 0; i < m_HeadNum; i++)
            {
                this.m_CheckBoxHeadMask[i] = new CheckBox();
                this.m_NumericUpDownXs[i] = new NumericUpDown();
                this.m_NumericUpDownYs[i] = new NumericUpDown();
            }
            this.SuspendLayout();
        }
        private void AppendComponent()
        {

            for (int i = 0; i < m_HeadNum; i++)
            {
                m_GroupBoxTemperature.Controls.Add(this.m_CheckBoxHeadMask[i]);
                m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownXs[i]);
                m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownYs[i]);
            }
            m_GroupBoxTemperature.ResumeLayout(false);

            this.ResumeLayout(false);
        }
        private void LayoutComponent()
        {

            m_GroupBoxTemperature.SuspendLayout();

            ///True Layout
            ///
            int start_x, end_x, space_x, width_con;
            int start_y, end_y, space_y;
            start_x = this.m_CheckboxHeadSample.Left;
            start_y = this.m_CheckboxHeadSample.Top;
            space_y = this.m_CheckboxHeadSample.Height + 8;
            end_x = this.m_GroupBoxTemperature.Width;
            width_con = this.m_CheckboxHeadSample.Width;
            CalculateHorNum(m_HeadNum, start_x, end_x, ref width_con, out space_x);
            int CurY = start_y + space_y;
            end_y = CurY;

            int buttonY = m_GroupBoxTemperature.Bottom + space_y;
            m_ButtonToBoard.Location = new Point(m_ButtonToBoard.Location.X, buttonY);
            m_ButtonRefresh.Location = new Point(m_ButtonRefresh.Location.X, buttonY);
            m_ButtonDefault.Location = new Point(m_ButtonDefault.Location.X, buttonY);

            float maxval = (float)(100.0f / 2.54);
            float minval = (float)(-100.0f / 2.54);
            for (int i = 0; i < m_HeadNum; i++)
            {
                int curX = start_x + space_x * i;
                int curY = start_y;

                //curY += space_y;
                CheckBox checkbox = this.m_CheckBoxHeadMask[i];
                ControlClone.CheckBoxClone(checkbox, this.m_CheckboxHeadSample);
                checkbox.Location = new Point(curX, curY);
                checkbox.Width = width_con;
                checkbox.TabIndex = m_HeadNum + i;
                checkbox.TabStop = false;
                checkbox.Text = (i + 1).ToString();
                checkbox.Visible = true;

                curY += space_y;
                NumericUpDown textBox = this.m_NumericUpDownXs[i];
                ControlClone.NumericUpDownClone(textBox, this.m_NumericUpDownXSample);
                textBox.Minimum = new decimal(minval);
                textBox.Maximum = new decimal(maxval);
                //textBox.Increment = new decimal(0.1f * cofficient_voltage);
                textBox.Location = new Point(curX, curY);
                textBox.Width = width_con;
                textBox.TabIndex = m_HeadNum + i;
                textBox.TabStop = false;
                textBox.Text = "0";
                textBox.Visible = true;
                textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
                textBox.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);

                curY += space_y;
                textBox = this.m_NumericUpDownYs[i];
                ControlClone.NumericUpDownClone(textBox, this.m_NumericUpDownYSample);
                textBox.Minimum = new decimal(minval);
                textBox.Maximum = new decimal(maxval);
                //textBox.Increment = new decimal(0.1f * cofficient_voltage);
                textBox.Location = new Point(curX, curY);
                textBox.Width = width_con;
                textBox.TabIndex = m_HeadNum * 2 + i;
                textBox.Text = "0";
                textBox.Visible = false;
                textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
                textBox.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
            }

        }
        private void CalculateHorNum(int num, int start_x, int end_x, ref int width, out int space)
        {
            const int m_HorGap = 4;
            const int m_Margin = 8;

            //space = (end_x - start_x - m_Margin + m_HorGap)/num ;
            if (num > 1)
                space = (end_x - start_x - m_Margin - width) / (num - 1);
            else
                space = end_x - start_x - m_Margin - width;
            if ((width + m_HorGap) > space)
            {
                width = (end_x - start_x - m_HorGap * (num - 1) - m_HorGap) / num;
                space = width + m_HorGap;
            }
        }

        private void m_CheckBoxControl_Leave(object sender, System.EventArgs e)
        {
#if true
            NumericUpDown textBox = (NumericUpDown)sender;
            bool isValidNumber = true;
            try
            {
                float val = float.Parse(textBox.Text);
                textBox.Value = new Decimal(val);
            }
            catch (Exception)
            {
                //Console.WriteLine(ex.Message);
                isValidNumber = false;
            }

            if (!isValidNumber)
            {
                SystemCall.Beep(200, 50);
                textBox.Focus();
                textBox.Select(0, textBox.Text.Length);
            }
#endif
        }

        private void m_TextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                m_CheckBoxControl_Leave(sender, e);
            }
        }

        private void m_ButtonRefresh_Click(object sender, System.EventArgs e)
        {
            OnRealTimeChange();
        }

        private void m_ButtonToBoard_Click(object sender, System.EventArgs e)
        {
            OnGetRealTimeFromUI();
            ApplyToBoard();
        }
        public void ApplyToBoard()
        {
            using (StreamWriter sw = new StreamWriter(XYoffsetPath, false))
            {
                foreach (float[] vals in XYOffsetValues)
                {
                    string str = string.Empty;
                    foreach (float ff in vals)
                        str += ff.ToString() + ",";
                    sw.WriteLine(str.Substring(0, str.Length - 1));
                }
            }
            byte[] ret = new byte[m_HeadNum];
            for (int j = 0; j < m_HeadNum; j++)
            {
                ret[j] = this.m_CheckBoxHeadMask[j].Checked ? (byte)1 : (byte)0;
            }
            byte[] hex = new byte[8];
            byte[] realRet = new byte[m_HeadNum / 8];
            int i = 0;
            foreach (byte bt in ret)
            {
                i++;
                hex[(i - 1) % 8] = bt;
                if (i % 8 == 0)
                {
                    byte ihex = 0;
                    string hexStr = string.Empty;
                    for (int m = 0; m < 8; m++)
                    {
                        ihex += (byte)(hex[m] * Math.Pow(2, m % 8));
                    }
                    realRet[i / 8 - 1] = ihex;
                }
            }
            //CoreInterface.UpdateHeadMask(realRet, m_HeadNum);
            CoreInterface.NotifyPrinterPropertyChange();
        }

        private void m_ButtonDefault_Click(object sender, System.EventArgs e)
        {
            this.DefaultRealTimeValue();
            this.OnSetRealTimeToUI();
        }
        private void DefaultRealTimeValue()
        {
            this.XYOffsetValues = new List<float[]>(3)
                { new float[CoreConst.MAX_HEAD_NUM], new float[CoreConst.MAX_HEAD_NUM], new float[1] 
                };
        }
    }
}
