﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace BYHXPrinterManager.Setting
{
    public partial class ColorjetManualCleanSetting : UserControl
    {
        private SPrinterSetting m_PrinterSetting;
        private bool bCanCleanFlg = true;
        private bool bColorJet = true;
        private bool _isSsystem = true;
        private SPrinterProperty m_sPrinterProperty;
        private ManualCleanParam _manualCleanParamData;
        public ColorjetManualCleanSetting()
        {
            InitializeComponent();

            bColorJet = PubFunc.IsColorJet_Belt_Textile();
            this.m_ButtonStartClean.Enabled = bCanCleanFlg;

            //
            _isSsystem = CoreInterface.IsS_system();

            m_GroupBoxCleanParam.Visible = bColorJet;
            panelColorJetAPlusSystem.Visible = 
            buttonZCenter.Visible=
            buttonZBottom.Visible = true;  // 20180901 colorjet要求修改
            panelAutoCapping.Visible = _isSsystem;
        }

        private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
        public void OnPreferenceChange(UIPreference up)
        {
            if (m_CurrentUnit != up.Unit)
            {
                OnUnitChange(up.Unit);
                m_CurrentUnit = up.Unit;
            }
        }
        private void OnUnitChange(UILengthUnit newUnit)
        {
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.m_NumericUpDownXStartPos);
            //UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.m_NumericUpDownYEndPos);
            //UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numericXAxisPos);
            //UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numericBladeYMobileDistance);
            //UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, this.numericScrapPlatformZPos);

            string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownXStartPos, this.m_ToolTip);
            //UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownYEndPos, this.m_ToolTip);
            //UIPreference.NumericUpDownToolTip(newUnitdis, this.numericXAxisPos, this.m_ToolTip);
            //UIPreference.NumericUpDownToolTip(newUnitdis, this.numericBladeYMobileDistance, this.m_ToolTip);
            //UIPreference.NumericUpDownToolTip(newUnitdis, this.numericScrapPlatformZPos, this.m_ToolTip);
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            m_sPrinterProperty = sp;
            m_NumericUpDownXStartPos.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            m_NumericUpDownXStartPos.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth));
        }
        public void OnPrinterSettingChange(SPrinterSetting ss, SPrinterProperty sp)
        {
            m_PrinterSetting = ss; //Printer Setting backup
            if (bColorJet)
            {
                if (EpsonLCD.GetManualCleanParam(ref _manualCleanParamData) == false)
                {
                    string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.ManualCleanNotSupport);
                    MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //色序初始化
            panelColorMask.SuspendLayout();
            panelColorMask.Controls.Clear();
            if (bColorJet)
            {
                for (int n = 0; n < sp.nColorNum/Math.Abs(sp.nOneHeadDivider); n++)
                {
                    RadioButton colorBox = new RadioButton();
                    colorBox.Text = string.Format("CH{0}", n + 1); //((ColorEnum_Short) sp.eColorOrder[n]).ToString();
                    colorBox.TextAlign = ContentAlignment.MiddleLeft;
                    colorBox.AutoSize = true;
                    panelColorMask.Controls.Add(colorBox);
                }
            }
            else
            {
                for (int n = 0; n < sp.nColorNum; n++)
                {
                    CheckBox colorBox = new CheckBox();
                    colorBox.Text = string.Format("CH{0}", n + 1); //((ColorEnum_Short) sp.eColorOrder[n]).ToString();
                    colorBox.TextAlign = ContentAlignment.MiddleLeft;
                    colorBox.AutoSize = true;
                    panelColorMask.Controls.Add(colorBox);
                }
            }
            panelColorMask.ResumeLayout(false);

            if (bColorJet)
            {
                //ColorJet 通用参数
                UIPreference.SetValueAndClampWithMinMax(this.m_NumericUpDownXStartPos, m_CurrentUnit, _manualCleanParamData.nXStartPos / m_sPrinterProperty.fPulsePerInchX);
                m_ComboBoxHeaderNumPerTime.Items.Clear();//每次清洗喷头数初始化
                for (int n = 1; n <= sp.nHeadNumPerGroupY; n++)
                {
                    m_ComboBoxHeaderNumPerTime.Items.Add(n);
                }
                m_ComboBoxHeaderNumPerTime.SelectedIndex = 0;
                //ColorJet S系统参数
                {
                    UIPreference.SetValueAndClampWithMinMax(this.numericWetWaitTime, _manualCleanParamData.autoWetWaitTime);
                    checkBoxWetFlag.Checked = _manualCleanParamData.autoWetFlag == 1;
                }
                //ColorJet A+系统参数
                {
                    UIPreference.SetValueAndClampWithMinMax(this.numericOriginOffset, _manualCleanParamData.OriginOffset);
                    UIPreference.SetValueAndClampWithMinMax(this.numericSpeed, _manualCleanParamData.ySpeedHz);
                    UIPreference.SetValueAndClampWithMinMax(this.numericDelayTime, _manualCleanParamData.yZeroDelay);
                    checkBoxFunctionOn.Checked = _manualCleanParamData.DisableFlag == 0;
                }
                UIPreference.SetValueAndClampWithMinMax(this.numCleanBeltTime, _manualCleanParamData.cleanBeltOutTime/1000f);
            }
        }

        public void OnGetPrinterSetting(ref SPrinterSetting ss)
        {
            if (bColorJet)
            {
                //ColorJet 通用参数
                _manualCleanParamData.nXStartPos = (uint)Math.Round((UIPreference.ToInchLength(m_CurrentUnit, (float)m_NumericUpDownXStartPos.Value)) *
                                       m_sPrinterProperty.fPulsePerInchX);
                _manualCleanParamData.bySwapHeaders = (byte)(m_ComboBoxHeaderNumPerTime.SelectedIndex + 1);
                if(_isSsystem)
                    _manualCleanParamData.nFlag = BitConverter.ToUInt32(System.Text.Encoding.ASCII.GetBytes("CJCS"), 0);
                else
                    _manualCleanParamData.nFlag = BitConverter.ToUInt32(System.Text.Encoding.ASCII.GetBytes("CJCA"), 0);
                //ColorJet S系统参数
                {
                    _manualCleanParamData.autoWetWaitTime = (byte)(this.numericWetWaitTime.Value);
                    _manualCleanParamData.autoWetFlag = (byte)(checkBoxWetFlag.Checked ? 1 : 0);
                }
                //ColorJet A+系统参数
                {
                    _manualCleanParamData.OriginOffset = (byte)(this.numericOriginOffset.Value);
                    _manualCleanParamData.ySpeedHz = Decimal.ToUInt16(this.numericSpeed.Value);
                    _manualCleanParamData.yZeroDelay = Decimal.ToUInt16(this.numericDelayTime.Value);
                    _manualCleanParamData.DisableFlag = (byte)(checkBoxFunctionOn.Checked ? 0 : 1);
                }
                _manualCleanParamData.cleanBeltOutTime = (uint) ((float)numCleanBeltTime.Value*1000f);

                this.m_ButtonStartClean.Enabled = bCanCleanFlg = true;
                if (EpsonLCD.SetManualCleanParam(_manualCleanParamData) == false)
                {
                    string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.SetCleanParamFail);
                    MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.m_ButtonStartClean.Enabled = bCanCleanFlg = false;
                }
            }
        }

        /// <summary>
        /// 设置清洗参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_ButtonSet_Click(object sender, EventArgs e)
        {
            this.m_ButtonStartClean.Enabled = bCanCleanFlg = true;
            OnGetPrinterSetting(ref m_PrinterSetting);
            if (EpsonLCD.SetManualCleanParam(_manualCleanParamData) == false) 
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.SetCleanParamFail);
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_ButtonStartClean.Enabled = bCanCleanFlg = false;
            }
        }

        /// <summary>
        /// 设置清洗颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_ButtonStartClean_Click(object sender, EventArgs e)
        {
            byte color = 0;//(byte) (m_ComboBoxColorIndex.SelectedIndex + 1);
            for (int i = 0; i < panelColorMask.Controls.Count; i++)
            {
                if (bColorJet)
                {
                    if (panelColorMask.Controls[i] is RadioButton)
                    {
                        if (((RadioButton)panelColorMask.Controls[i]).Checked)
                        {
                            color = (byte)(i*Math.Abs(m_sPrinterProperty.nOneHeadDivider));
                        }
                    }
                }
                else
                {
                    if (panelColorMask.Controls[i] is CheckBox)
                    {
                        if (((CheckBox)panelColorMask.Controls[i]).Checked)
                        {
                            color |= (byte)(1 << i);
                        }
                    }
                }
            }
            byte? level = null;
            if (checkBoxAll.Checked)
                color = 0xff;

            EpsonLCD.SetManualCleanCmd(color, level);
        }

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            panelColorMask.Enabled = !checkBoxAll.Checked;
        }

        private void m_ButtonCancel_Click(object sender, EventArgs e)
        {
            EpsonLCD.StopManualClean();
        }

        private void buttonZCenter_Click(object sender, EventArgs e)
        {
            EpsonLCD.SetManualCleanParamZCenter();
        }

        private void buttonZBottom_Click(object sender, EventArgs e)
        {
            EpsonLCD.SetManualCleanParamZBottom();
        }

        private void buttonCleanBelt_Click(object sender, EventArgs e)
        {
            EpsonLCD.StartCleanBelt();
        }

        private void buttonStopCleanBelt_Click(object sender, EventArgs e)
        {
            EpsonLCD.StopCleanBelt();
        }
    }
}
