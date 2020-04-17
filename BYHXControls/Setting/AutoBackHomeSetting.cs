using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    public partial class AutoBackHomeSetting : UserControl
    {
        private bool isDirty = false;
        private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
        private SPrinterProperty m_sPrinterProperty;
        private SPrinterSetting _printerSetting;
        public AutoBackHomeSetting()
        {
            InitializeComponent();
        }
        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            m_sPrinterProperty = sp;

            numHomePosX.Minimum =
            numCappingPosX.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numHomePosX.Maximum =
            numCappingPosX.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth));
            numHomePosY.Minimum =
            numCappingPosY.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numHomePosY.Maximum =
            numCappingPosY.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperHeight));
            this.isDirty = false;
        }

        public void OnPrinterSettingChange(SPrinterSetting ss)
        {
            _printerSetting = ss;
            //if (SPrinterProperty.IsSurportCapping())
            {
                WetParam wetParam = new WetParam();
                if (EpsonLCD.GetWetParam(ref wetParam))
                {
                    // 回保湿位置
                    checkBoxEnableCappingPosX.Checked = (wetParam.PosMask & 1) != 0;
                    checkBoxEnableCappingPosY.Checked = (wetParam.PosMask & 2) != 0;
                    checkBoxEnableCappingPosZ.Checked = (wetParam.PosMask & 4) != 0;
                    checkBoxsalverPos.Checked = (wetParam.PosMask & 8) != 0;
                    checkBoxEnableAutoCapping.Checked = wetParam.Enable != 0;
                    if (m_sPrinterProperty.fPulsePerInchX > 0)
                        UIPreference.SetValueAndClampWithMinMax(numCappingPosX, m_CurrentUnit,
                            wetParam.XPos / m_sPrinterProperty.fPulsePerInchX);
                    if (m_sPrinterProperty.fPulsePerInchY > 0)  
                        UIPreference.SetValueAndClampWithMinMax(numCappingPosY, m_CurrentUnit,
                            wetParam.YPos / m_sPrinterProperty.fPulsePerInchY);
                    if (m_sPrinterProperty.fPulsePerInchZ > 0)
                        UIPreference.SetValueAndClampWithMinMax(numCappingPosZ, m_CurrentUnit,
                            wetParam.ZPos / m_sPrinterProperty.fPulsePerInchZ);
                    numCappingDelayTime.Value = (decimal)(wetParam.WaitTime / 1000f);

                    // 回默认位置
                    checkBoxXHomePos.Checked = (wetParam.DefaultBackPosMask & 1) != 0;
                    checkBoxYHomePos.Checked = (wetParam.DefaultBackPosMask & 2) != 0;
                    checkBoxEnableHomeDelay.Checked = wetParam.DefaultBackDelayEnable != 0;
                    if (m_sPrinterProperty.fPulsePerInchX > 0)
                        UIPreference.SetValueAndClampWithMinMax(numHomePosX, m_CurrentUnit,
                            wetParam.DefaultBackXPos / m_sPrinterProperty.fPulsePerInchX);
                    if (m_sPrinterProperty.fPulsePerInchY > 0)
                        UIPreference.SetValueAndClampWithMinMax(numHomePosY, m_CurrentUnit,
                            wetParam.DefaultBackYPos / m_sPrinterProperty.fPulsePerInchY);
                    numAutoHomeDelay.Value = (decimal)(wetParam.BackDefaultPosWaitTime / 1000f);
                    numSalverPos.Value = wetParam.cover4Place;
                }
            }
            this.isDirty = false;
        }

        public void OnGetPrinterSetting(ref SPrinterSetting ss)
        {
            //if (SPrinterProperty.IsSurportCapping())
            {
                WetParam wetParam = new WetParam();
                wetParam.ActiveLen = 18;
                // 回保湿位置
                wetParam.PosMask = (byte)(checkBoxEnableCappingPosX.Checked ? 1 : 0); // xyz都启用
                if (checkBoxEnableCappingPosY.Checked)
                    wetParam.PosMask |= (1 << 1);
                if (checkBoxEnableCappingPosZ.Checked)
                    wetParam.PosMask |= (1 << 2);
                if (checkBoxsalverPos.Checked)
                    wetParam.PosMask |= (1 << 3);
                wetParam.Enable = (byte)(checkBoxEnableAutoCapping.Checked ? 1 : 0);
                wetParam.XPos = (int)(UIPreference.ToInchLength(m_CurrentUnit, (float)numCappingPosX.Value) *
                                       m_sPrinterProperty.fPulsePerInchX);
                wetParam.YPos = (int)(UIPreference.ToInchLength(m_CurrentUnit, (float)numCappingPosY.Value) *
                                       m_sPrinterProperty.fPulsePerInchY);
                wetParam.ZPos = (int)(UIPreference.ToInchLength(m_CurrentUnit, (float)numCappingPosZ.Value) *
                                       m_sPrinterProperty.fPulsePerInchZ);
                wetParam.WaitTime = (uint)((float)numCappingDelayTime.Value * 1000f);

                //回缺省位置
                wetParam.DefaultBackPosMask = (byte)(checkBoxXHomePos.Checked ? 1 : 0); // xyz都启用
                if (checkBoxYHomePos.Checked)
                    wetParam.DefaultBackPosMask |= (1 << 1);
                wetParam.DefaultBackDelayEnable = (byte)(checkBoxEnableHomeDelay.Checked ? 1 : 0);
                wetParam.DefaultBackXPos = (int)(UIPreference.ToInchLength(m_CurrentUnit, (float)numHomePosX.Value) *
                                       m_sPrinterProperty.fPulsePerInchX);
                wetParam.DefaultBackYPos = (int)(UIPreference.ToInchLength(m_CurrentUnit, (float)numHomePosY.Value) *
                                       m_sPrinterProperty.fPulsePerInchY);
                wetParam.BackDefaultPosWaitTime = (uint)((float)numAutoHomeDelay.Value * 1000f);
                wetParam.cover4Place = (int)numSalverPos.Value ;

                if (!EpsonLCD.SetWetParam(wetParam))
                {
                    MessageBox.Show("Set Capping parametas fail!");
                }
            }
        }
        public void OnPreferenceChange(UIPreference up)
        {
            //			if(m_CurrentUnit != up.Unit)
            {
                OnUnitChange(up.Unit);
                m_CurrentUnit = up.Unit;
                this.isDirty = false;
            }
        }

        private void OnUnitChange(UILengthUnit newUnit)
        {
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numCappingPosX);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numCappingPosY);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numCappingPosZ);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numHomePosX);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numHomePosY);

            string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.numCappingPosX, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.numCappingPosY, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.numCappingPosZ, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), numHomePosX, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), numHomePosY, this.m_ToolTip);
            this.isDirty = false;
        }


    }
}
